using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serko.Travel.Core.Exceptions;
using Serko.Travel.Core.Helpers;
using Serko.Travel.Core.Interfaces;
using Serko.Travel.Core.Models;
using Serko.Travel.Core.Services;

namespace Serko.Travel.Tests.Services
{
	[TestClass]
	public class ParseTextServiceTest
	{
		private IParseTextService service;
		private string xml = string.Empty;

		[TestInitialize()]
		public void Initialize()
		{
			// Setup
			service = new ParseTextService();			
		}

		[TestMethod]
		public void ParseTextService_Return_Valid_Email()
		{
			// Arrange
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
			var expectedEmail = new Email()
			{
				Claim = new Claim()
				{
					CostCenter = "DEV002",
					Total = (Decimal)890.55,
					PaymentMethod = "personal\n\t\t\t\tcard"
				},
				Vendor = "Viaduct Steakhouse",
				Description = "development\n\t\t\t\tteam’s project end celebration dinner",
				ReserveDate = "Tuesday 27 April 2017"
			};

			// Action
			var email = service.ExtractData(xml);

			// Assert
			Assert.AreEqual(email, expectedEmail);
		}

		[TestMethod]
		public void ParseTextService_Return_Valid_Email_UNKNOW_CostCenter()
		{
			// Arrange
			xml = @" 
				Hi Yvaine,
				Please create an expense claim for the below. Relevant details are marked up as requested…
				<expense><cost_centre></cost_centre> <total>890.55</total><payment_method>personal
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

			// Action
			var email = service.ExtractData(xml);

			// Assert
			Assert.AreEqual(email.Claim.CostCenter, GlobalConstant.UNKNOWN);
		}

		[TestMethod]
		[ExpectedException(typeof(MissingTotalException),
			GlobalConstant.MISSING_TOTAL)]
		public void ParseTextService_Failed_MissingTotalException()
		{
			// Arrange
			xml = @" 
				Hi Yvaine,
				Please create an expense claim for the below. Relevant details are marked up as requested…
				<expense><cost_centre>DEV002</cost_centre> <total></total><payment_method>personal
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

			// Action
			var email = service.ExtractData(xml);
		}


		[TestMethod]
		[ExpectedException(typeof(InvalidXMLTagException),
				GlobalConstant.INVALID_XML_TAG)]
		public void ParseTextService_Return_Fail_InvalidXMLTagException()
		{
			// Arrange
			xml = @" 
				Hi Yvaine,
				Please create an expense claim for the below. Relevant details are marked up as requested…
				<expense>DEV002</cost_centre> <total></total><payment_method>personal
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

			// Action
			var email = service.ExtractData(xml);
		}
	}
}
