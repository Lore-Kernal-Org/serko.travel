using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Serko.Travel.Core.Helpers
{
	public class TextHelper
	{
		public static string ReplaceEmails(string fromText, bool includedBracket)
		{
			Regex reg = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
			Match match;

			List<string> results = new List<string>();
			for (match = reg.Match(fromText); match.Success; match = match.NextMatch())
			{
				if (includedBracket)
				{
					fromText = fromText.Replace($"<{match.Value}>", "");
				}
				else
				{
					fromText = fromText.Replace($"{match.Value}", "");
				}
			}

			return fromText;
		}
	}
}
