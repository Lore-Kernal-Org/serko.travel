using Newtonsoft.Json;
using Serko.Travel.Core.Exceptions;
using Serko.Travel.Core.Interfaces;
using Serko.Travel.Core.Models;
using Serko.Travel.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Serko.Travel.WebAPI.Controllers
{
	public class SerkoController : ApiController
	{
		IParseTextService service;

		public SerkoController(IParseTextService service)
		{
			this.service = service;
		}

		/// <summary>
		/// Api health check
		/// </summary>
		/// <returns></returns>
		public HttpResponseMessage Get()
		{
			return Request.CreateResponse(HttpStatusCode.OK, "");
		}

		[HttpPost]
		public IHttpActionResult Post([FromBody] string value)
		{
			var email = new Email();
			try
			{
				email = service.ExtractData(value);
			}
			catch (MissingTotalException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (InvalidXMLTagException ex)
			{
				return  BadRequest(ex.Message);
			}
			
			var json =  JsonConvert.SerializeObject(email);

			return Ok(json);
		}
	}
}