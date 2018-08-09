using Ninject.Modules;
using Serko.Travel.Core.Interfaces;
using Serko.Travel.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serko.Travel.WebAPI.Helpers
{
	public class SerkoModule : NinjectModule
	{
		public override void Load()
		{
			this.Bind<IParseTextService>().To<ParseTextService>();
		}
	}
}