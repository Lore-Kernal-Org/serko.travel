using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serko.Travel.Core.Exceptions
{
	public class InvalidXMLTagException : Exception
	{
		public InvalidXMLTagException() { }
		public InvalidXMLTagException(string message) : base(message) { }
		public InvalidXMLTagException(string message, Exception inner) : base(message, inner) { }

		protected InvalidXMLTagException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
