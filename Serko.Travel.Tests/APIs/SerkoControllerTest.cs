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

		[TestInitialize()]
		public void Initialize()
		{
			// Setup
			serviceMock = new Mock<IParseTextService>();
			controller = new SerkoController(serviceMock.Object);
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
				},
				Vendor="vendor",
				Description="des",
				ReserveDate ="date"
			};
			
			serviceMock.Setup(x => x.ExtractData(It.IsAny<string>())).Returns(email);

			// Action
			var status = controller.Post(It.IsAny<string>());

			// Assert
			Assert.IsInstanceOfType(status, typeof(OkNegotiatedContentResult<string>));
		}

		[TestMethod]
		public void SerkoController_Post_Failed_MissingTotal()
		{
			//Arrange
			serviceMock.Setup(x => x.ExtractData(It.IsAny<string>())).Throws<MissingTotalException>();

			// Action
			var status = controller.Post(It.IsAny<string>());

			// Assert
			Assert.IsInstanceOfType(status, typeof(BadRequestErrorMessageResult));
		}

		[TestMethod]
		public void SerkoController_Post_Failed_InvalidXMLTagException()
		{
			//Arrange
			serviceMock.Setup(x => x.ExtractData(It.IsAny<string>())).Throws<InvalidXMLTagException>();

			// Action
			var status = controller.Post(It.IsAny<string>());

			// Assert
			Assert.IsInstanceOfType(status, typeof(BadRequestErrorMessageResult));
		}
	}
}
