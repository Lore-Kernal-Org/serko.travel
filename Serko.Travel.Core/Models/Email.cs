using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serko.Travel.Core.Models
{
	[XmlRoot("email")]
	public class Email
	{
		[XmlElement("expense")]
		public Claim Claim { get; set; }

		[XmlElement("vendor")]
		public string Vendor { get; set; }

		[XmlElement("description")]
		public string Description { get; set; }

		[XmlElement("date")]
		public string ReserveDate { get; set; }

		public override bool Equals(object obj)
		{
			try
			{
				var compared = (Email)obj;
				if (!compared.Claim.Equals(this.Claim)) return false;
				if (compared.Vendor != this.Vendor) return false;
				if (compared.Description != this.Description) return false;
				if (compared.ReserveDate != this.ReserveDate) return false;
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}
	}
}
