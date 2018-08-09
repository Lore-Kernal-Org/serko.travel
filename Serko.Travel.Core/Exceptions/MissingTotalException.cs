using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serko.Travel.Core.Exceptions
{
	public class MissingTotalException : Exception
	{
		public MissingTotalException() { }
		public MissingTotalException(string message) : base(message) { }
		public MissingTotalException(string message, Exception inner) : base(message, inner) { }

		protected MissingTotalException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
