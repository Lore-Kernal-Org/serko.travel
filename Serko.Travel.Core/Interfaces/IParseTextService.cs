using Serko.Travel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serko.Travel.Core.Interfaces
{
	public interface IParseTextService
	{
		Email ExtractData(string byEmail);
		Task<Email> ExtractDataAsync(string byEmail);
	}
}
