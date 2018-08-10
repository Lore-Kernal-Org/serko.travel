using Serko.Travel.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Serko.Travel.Core.Helpers
{
	public class XMLHelper
	{
		public static string ParseToXML(string fromPlainText)
		{
			string plainTextWithoutEmails = TextHelper.ReplaceEmails(fromPlainText, true);

			string reformatPlainText = $"<root>{plainTextWithoutEmails}</root>";
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.LoadXml(reformatPlainText);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw new InvalidXMLTagException(GlobalConstant.INVALID_XML_TAG);
			}
			
			var xmlFragments = from XmlNode node in doc.FirstChild.ChildNodes
							   where node.NodeType == XmlNodeType.Element
							   select node;
			var parsedXML = new StringBuilder();
			foreach (var fragment in xmlFragments)
			{
				parsedXML.Append(fragment.OuterXml);
			}

			// Append root email, it helps to convert to .NET object
			parsedXML.Insert(0, "<email>");
			parsedXML.Append("</email>");

			return parsedXML.ToString();
		}

		public static T DeserializeObject<T>(string fromXml) where T : new()
		{
			if (string.IsNullOrEmpty(fromXml))
			{
				return new T();
			}
			try
			{
				using (var stringReader = new StringReader(fromXml))
				{
					var serializer = new XmlSerializer(typeof(T));
					return (T)serializer.Deserialize(stringReader);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return new T();
			}
		}
	}
}
