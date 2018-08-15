using Serko.Travel.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serko.Travel.Core.Models;
using Serko.Travel.Core.Exceptions;
using Serko.Travel.Core.Interfaces;

namespace Serko.Travel.Core.Services
{
	public class ParseTextService : IParseTextService
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

		public async Task<Email> ExtractDataAsync(string byEmail)
		{
			var email = await Task.Run(() => {
				var emailDeserialized = XMLHelper.DeserializeObject<Email>(XMLHelper.ParseToXML(byEmail));
				System.Threading.Thread.Sleep(5000);
				IsValidEmailContent(emailDeserialized);
				return emailDeserialized;
			});			

			return email;
		}
	}
}
