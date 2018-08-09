using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serko.Travel.Core.Models
{
	[XmlRoot("reservation")]
	public class Reservation
	{
		[XmlElement("vendor")]
		public string Vendor { get; set; }
		[XmlElement("description")]
		public string Description { get; set; }
		[XmlElement("date")]
		public string ReserveDate { get; set; }
	}
}
