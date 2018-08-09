using Serko.Travel.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serko.Travel.Core.Models;
using Serko.Travel.Core.Exceptions;

namespace Serko.Travel.Core.Services
{
	public class ParseTextService
	{

		public Email ExtractData(string byEmail)
		{
			var parsedXML = XMLHelper.ParseToXML(byEmail);
			
			var email = XMLHelper.DeserializeObject<Email>(parsedXML);
			IsValidEmailContent(email);
			return email;
		}

		private bool IsValidEmailContent(Email email)
		{
			var isValid = true;
			if (email?.Claim?.Total == null)
			{
				throw new MissingTotalException(GlobalConstant.MISSING_TOTAL);
			}

			if (string.IsNullOrEmpty(email?.Claim?.CostCenter))
			{
				email.Claim.CostCenter = GlobalConstant.UNKNOWN;
			}

			return isValid;
		}
	}
}
