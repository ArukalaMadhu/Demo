﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

using NUnit.Framework;
using Ranorex;
using Ranorex.Core;
using RanorexTestFramework.Base;
using RanorexTestFramework.PageParts;
using RanorexTestFramework.utilities;
using NAnt.Core.Attributes;
namespace RanorexTestFramework.Testsuite
{
	/// <summary>
	/// ICG Test - Verifying Quote details 
	/// </summary>
	[TestFixture]
	public class ICGtest : Baseclass
	{
		WebDocument webdoc;
		ICGLoginPage loginpage;
		ICGHomePage homepage;
		ICGQuotesandPoliciesPage quotesandpolicies;
		/// <summary>
		/// Setting Up wecodument and navigating to Domain usl
		/// </summary>
		[SetUp]
		public void initialize()
		{
			//Mandatory for WebDocument initiation
			webdoc = init();			
			loginpage = new ICGLoginPage(webdoc);
			homepage=new ICGHomePage(webdoc);
			quotesandpolicies=new ICGQuotesandPoliciesPage(webdoc);
			
		}
		/// <summary>
		/// Login, Search quote, Verying quote details and sign out
		/// </summary>
		//[Test]
		public void ICGSearchTest_ViewQuoteDetails()
		{
			String _username=ConfigurationManager.AppSettings["Username"].ToString();
			String _password=ConfigurationManager.AppSettings["Password"].ToString();
			loginpage.PerformLogin(_username,_password);
			loginpage.VerifyLogin();
			homepage.NavigateToQuotesandPoliciies();
			String _searchtext=ConfigurationManager.AppSettings["SearchText"].ToString();
			quotesandpolicies.SearchQuotes(_searchtext);
			quotesandpolicies.VerifyOpenedQuote();
			quotesandpolicies.ViewQuoteDetails("test1606c","zenq","Elizabeth St",10012);
			//quotesandpolicies.viewQuoteDetails("test1606c","zen QUALITY","Elizabeth St",10012);
			quotesandpolicies.ViewApplicationDetails("no","married");
			quotesandpolicies.ReviewAndSubmit();
			//quotesandpolicies.signOut();

		}
		
		/// <summary>
		/// Login, Search quote, Verying quote details and sign out
		/// </summary>
		[Test]
		public void ICG002()
		{
			String _username=ConfigurationManager.AppSettings["Username"].ToString();
			String _password=ConfigurationManager.AppSettings["Password"].ToString();
			loginpage.PerformLogin(_username,_password);
			loginpage.VerifyLogin();
			quotesandpolicies.NavigateToNewQuoteTab();
			quotesandpolicies.SelectProduct("Occidental Fire and Casualty Co of NC","HO3 AK","28");
			

		}
		/// <summary>
		/// Close the application after completion fo test
		/// </summary>
		
		[TearDown]
		public void clean()
		{			
			quotesandpolicies.SignOut();
			Report.End();
			
			try{
				webdoc.Close();
			}catch(Exception e)
			{
				Console.WriteLine("Unable to close webdocument");
			}
			
			
		}


	}
}
