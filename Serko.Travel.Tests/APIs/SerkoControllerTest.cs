using System;
using System.Net;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serko.Travel.Core.Exceptions;
using Serko.Travel.Core.Interfaces;
using Serko.Travel.Core.Models;
using Serko.Travel.WebAPI.Controllers;

namespace Serko.Travel.Tests.APIs
{
	[TestClass]
	public class SerkoControllerTest
	{

		private Mock<IParseTextService> serviceMock;
		private SerkoController controller;
		private string xml = string.Empty;

		[TestInitialize()]
		public void Initialize()
		{
			// Arrange
			serviceMock = new Mock<IParseTextService>();
			controller = new SerkoController(serviceMock.Object);
			xml = @" 
				Hi Yvaine,
				Please create an expense claim for the below. Relevant details are marked up as requested…
				<expense><cost_centre>DEV002</cost_centre> <total>890.55</total><payment_method>personal
				card</payment_method>
				</expense>
				From: Ivan Castle
				Sent: Friday, 16 February 2018 10:32 AM
				To: Antoine Lloyd 
				Subject: test
				Hi Antoine,
				Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development
				team’s project end celebration dinner</description> on <date>Tuesday 27 April 2017</date>. We expect to
				arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
				Regards,
				Ivan
			";
		}

		[TestMethod]
		public void SerkoController_Post_Success()
		{
			//Arrange
			var email = new Email()
			{
				Claim = new Claim()
				{
					CostCenter = "Cost Center",
					PaymentMethod = "Payment Method",
					Total = (decimal) 8.0
				}
			};
			
			serviceMock.Setup(x => x.ExtractData("mixed xml")).Returns(email);

			// Action
			var status = controller.Post("");

			// Assert
			Assert.IsInstanceOfType(status, typeof(OkNegotiatedContentResult<string>));
		}

		[TestMethod]
		public void SerkoController_Post_Failed_MissingTotal()
		{
			//Arrange
			serviceMock.Setup(x => x.ExtractData("mixed xml")).Throws<MissingTotalException>();

			// Action
			var status = controller.Post(xml);

			// Assert
			Assert.IsInstanceOfType(status, typeof(NotFoundResult));
		}
	}
}
