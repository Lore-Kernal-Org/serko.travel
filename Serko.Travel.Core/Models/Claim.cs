using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serko.Travel.Core.Models
{
	[XmlRoot("expense")]
	public class Claim //: BaseEntity
	{
		[XmlElement("cost_centre")]
		public string CostCenter { get; set; }
		[XmlElement("total")]
		public decimal? Total { get; set; }
		[XmlElement("payment_method")]
		public string PaymentMethod { get; set; }
	}
}
