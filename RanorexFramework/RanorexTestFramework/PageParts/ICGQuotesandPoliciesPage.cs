/*
 * Created by SharpDevelop.
 * User: Vijay
 * Date: 6/12/2014
 * Time: 1:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using NUnit.Framework;
using Ranorex;
using RanorexTestFramework.PageConstants;
using RanorexTestFramework.utilities;

namespace RanorexTestFramework.PageParts
{
	/// <summary>
	/// contains all the operations under wuotes and policies tab
	/// </summary>
	public class ICGQuotesandPoliciesPage : SafeActions
	{
		WebDocument webdoc;
		//Objects for required page elements
		ICGQuotesandPoliciesPage_Elements quotes_polices_page;
		ICGQuoteArea_Elements quotearea;
		ICGApplicationArea_Elements applicationarea;
		ICGLoginPage_Elements loginpageobj;
		/// <summary>
		/// contructor to grab current webdocument reference and pass it over to safeactions
		/// </summary>
		/// <param name="webdocument">Current webdocument reference</param>
		public ICGQuotesandPoliciesPage(WebDocument webdocument): base(webdocument)
		{
			webdoc = webdocument;
			quotes_polices_page=new ICGQuotesandPoliciesPage_Elements(webdocument);
			quotearea=new ICGQuoteArea_Elements(webdocument);
			applicationarea=new ICGApplicationArea_Elements(webdocument);
			loginpageobj=new ICGLoginPage_Elements(webdocument);
		}
		/// <summary>
		/// Select quotes from drop down and search based on given text
		/// </summary>
		/// <param name="_search_quote_id">Text to search -will be passed from test</param>
		public void SearchQuotes(String _search_quote_id)
		{
			Report.LogHtml(ReportLevel.Info,"<i>Searching quote with id <b>"+_search_quote_id+"</b></i>");
			
			//Wait until search loading popup disappeares
			SafeVerifyElementNotVisible(quotes_polices_page.SEARCH_POPUP,"Search loading pop up");
			SafeVerifyElementVisible(quotes_polices_page._transactionhistory_tab,"Transaction tab under search area");
			//Selecting Quotes under Quotes and policies drop down
			SafeSelectOptionFromComboBox(quotes_polices_page._quotesandpoliciesdropdown, "Quotes","'Quotes and policies' drop down");
			
			SafeVerifyElementNotVisible(quotes_polices_page.SEARCH_POPUP,"Search loading pop up");
			//Entering text under Search field
			
			SafeType(quotes_polices_page._search_box, _search_quote_id,"Search Field");
			SafeVerifyElementNotVisible(quotes_polices_page.SEARCH_POPUP,"Search loading pop up");
			//Verifying the results
			if(!VerifySearchResults(_search_quote_id))
			{
				Report.Failure("Search result does not contain specified record") ;
				Report.Screenshot();
				Assert.Fail("Search result does not contain specified record");
			}else{
				Report.Success("Search result contains specified record");
			}
			
		}
		/// <summary>
		/// Opens the first quote in the table
		/// </summary>
		/// <returns></returns>
		private String OpenFirstQuote()
		{
			
			Report.LogHtml(ReportLevel.Info,"<i>Opening specified quote</i>");
			SafeVerifyElementVisible(quotes_polices_page._FirstResult,"First Result in table");
			String quoteidentifier=SafegetAttribute(quotes_polices_page._testcell,"text","First cell in first record");
			//Opening the Quote
			SafeDoubleClick(quotes_polices_page._testcell,"First Record in the Search results");
			
			return quoteidentifier;
		}
		/// <summary>
		/// Verifying search results
		/// </summary>
		/// <param name="texttosearch">Search string to be verified in each record under Search results</param>
		/// <returns>true if all the record matched given string</returns>
		public Boolean VerifySearchResults(String texttosearch)
		{
			Report.LogHtml(ReportLevel.Info,"<i>Verifying search results</i>");
			
			Table tb=WaitAndFindelement(quotes_polices_page._search_table,"Search resutls table");
			IList<Row> rows=tb.Rows;
			IList<Column> cols=tb.Columns;
			String s="";
			int count=0;
			for(int i=0;i<rows.Count;i++)
			{
				count=0;
				IList<Cell> cells=rows[i].Cells;
				foreach(var ele in cells)
				{ if(ele.Text.ToString().ToLower().Contains(texttosearch.ToLower()))
					{
						count=1;
					}
				}
				
				
			}
			if(count!=1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		/// <summary>
		/// Verifies whether correct quote has been opened by its id
		/// </summary>
		public void VerifyOpenedQuote()
		{
			//opening the quote in the results
			String id=OpenFirstQuote();
			SafeVerifyElementVisible(quotes_polices_page._quotehead,"Opened Quote tab header");
			String identifier=SafegetAttribute(quotes_polices_page._Quoteid,"text","Quote Id under quote tab header");
			
			VerifyData(identifier,id,"Verify opened quote id");
			
			//Switching to quote area if it is not displayed by default
			SwitchToQuoteArea();
			
		}
		/// <summary>
		/// switch to quote area if landed under other tabs
		/// </summary>
		public void SwitchToQuoteArea()
		{
			
			SafeVerifyElementVisible(quotes_polices_page._saveandclose_button,"Save and close button");
			SafeVerifyElementVisible(quotes_polices_page._premiumrate,"Premium Rate banner");
			if(SafegetAttribute(quotes_polices_page._activetab, "text","Active tab in navigation bar").ToLower()!="quote")
			{
				Report.LogHtml(ReportLevel.Info,"<i>Switching to quote area</i>");
				for (;SafegetAttribute(quotes_polices_page._activetab, "text","Active tab in navigation bar").ToLower()!="quote";)
				{
					
					SafeClick(quotes_polices_page._previous_button,"'previous' button");
					SafeVerifyElementVisible(quotes_polices_page._saveandclose_button,"Save and close button");
					if(SafegetAttribute(quotes_polices_page._activetab, "text","Active tab in navigation bar").ToLower()=="review")
					{
						SafeClick(quotes_polices_page._previous_button,"'previous' button");
						SafeClick(quotes_polices_page._unlockbutton,"'Unlock' button under 'review' pop up");
					}
					
				}
			}
			
			
		}
		/// <summary>
		/// Varifies information under quote area
		/// </summary>
		/// <param name="firstname">takes the first name</param>
		/// <param name="lastname">takes the last name</param>
		/// <param name="streetname">takes the street name</param>
		/// <param name="zipcode">take the zip code</param>
		public void ViewQuoteDetails(String firstname,String lastname,String streetname="",int zipcode=0)
		{
			Report.LogHtml(ReportLevel.Info,"<i>Verifying 'Quote' area details</i>");
			VerifyData(SafegetAttribute(quotearea._First_name,"'First name' field in 'Quote' area","text",20000).ToLower(),firstname.ToLower(),"Verify 'First name' filed value in 'Quote' area");
			VerifyData(SafegetAttribute(quotearea._Last_name,"'Last Name' field in 'Quote' area","text",20000).ToLower(),lastname.ToLower(),"Verify 'Last name' field value in 'Quote' area");
			VerifyData(SafegetAttribute(quotearea._Street_name,"'Street Name' field in 'Quote' area","text",20000).ToLower(),streetname.ToLower(),"Verify 'Street name' field value in 'Quote' area");
			VerifyData(SafegetAttribute(quotearea._zip_code,"'Zip Code' field in 'Quote' area","text",20000),zipcode.ToString(),"Verify 'Zip code' field value in 'Quote' area");
			
			SafeClick(quotes_polices_page._next_button,"'Next' button in 'Quote' area", 2000);
			
		}
		/// <summary>
		/// verifying application area details
		/// </summary>
		/// <param name="residendetrust">takes residence trust option</param>
		/// <param name="maritalstatus">takes maritalstatud option</param>
		public void ViewApplicationDetails(String residendetrust,String maritalstatus)
		{
			Report.LogHtml(ReportLevel.Info,"<i>Verifying 'Application' area details</i>");
			SafeVerifyElementVisible(quotes_polices_page._saveandclose_button,"'save and close' button",40000);
			
			VerifyData(SafegetAttribute(applicationarea._No_radiobutton,"'Residence trust' radio button in 'Application' area","text",20000).ToLower(),residendetrust.ToLower(),"Verify 'Residence trust' setting in 'Application' area");
			
			VerifyData(SafegetAttribute(applicationarea._married_radiobutton,"'Marriage' radio button in 'Applicaiton' area","text",20000).ToLower(),maritalstatus.ToLower(),"Verify 'Marital status' setting in 'Application' area");
			
			SafeClick(quotes_polices_page._next_button,"'Next' button in 'Application' area", 2000);
		}
		/// <summary>
		/// Close the quote after success banner displayed
		/// </summary>
		public void ReviewAndSubmit()
		{
			Report.LogHtml(ReportLevel.Info,"<i>Closing quote</i>");
			SafeVerifyElementVisible(quotes_polices_page._saveandclose_button,"'save and close' button",40000);
			SafeClick(applicationarea._startreview_button,"'Start Review' button under 'review' pop up",3000);
			try
			{
				Validate.IsTrue(SafeVerifyElementVisible(applicationarea._review_banner,"Success banner under submit tab",40000),"Review Banner verification");
			}
			catch(ValidationException )
			{
				Report.Screenshot();
				
				Assert.Fail("Review Success banner not shown");
			}
			
			SafeClick(quotes_polices_page._saveandclose_button,"save and Close button",40000);
			
		}
		/// <summary>
		/// Signing out from the applicaiton
		/// </summary>
		public void SignOut()
		{
			Report.LogHtml(ReportLevel.Info,"<i>Signing out</i>");
			SafeClick(quotes_polices_page._sign_out,"sign out link",30000);
			if(!SafeVerifyElementVisible(loginpageobj._username_text,"Username under login page",30000))
			{
				Report.Failure("User name field under login page not displayed after waiting for 30 sec");
				Report.Screenshot();
				Assert.Fail("User Name field under login page is not displayed after waiting for 30 sec");				
				
			}
			else
			{
				Report.Success("Sign out successful");
			}
			
			
		}
		private void VerifyData(String actual,String expected,String customreportString)
		{
			try
			{
				Validate.AreEqual(actual,expected,customreportString+"[actual = "+actual+", expected = "+expected+"]");
			}catch(ValidationException )
			{
				Report.Screenshot();
				Assert.Fail(customreportString+"--Failed\n[actual = "+actual+", expected = "+expected+"]");
			}

		}
		
		/// <summary>
		/// Navigates to New Quote tab
		/// </summary>
		public void NavigateToNewQuoteTab(){
			
			Report.LogHtml(ReportLevel.Info,"<i>Creating New Quote</i>");
			webdoc.WaitForDocumentLoaded(TimeSpan.FromSeconds(60));
			WaitUntilElementDisappears(quotes_polices_page.RETRIEVING_NOTICES_STATUS,"RETRIEVING_NOTICES_STATUS");
			SafeVerifyElementVisible(quotes_polices_page.VIEW_MORE_TRANSACTIONS_LINK,"'View more transaction' link");
			SafeVerifyElementVisible(quotes_polices_page.NEW_QUOTE_BUTTON,"'New Quote' button in 'Home' page");
			SafeClick(quotes_polices_page.NEW_QUOTE_BUTTON,"'New Quote' button in 'Home' page");
			webdoc.WaitForDocumentLoaded(TimeSpan.FromSeconds(60));
		}
		/// <summary>
		/// Selects specified product from product section
		/// </summary>
		public void SelectProduct(string policyCarrierOption,string productOption,string effective_date){
			
			//Wait until search loading popup disappeares
			//WaitUntilElementDisappears(homepage.SEARCH_LOADING_POPUP,"Search loading pop up");
			
			SafeVerifyElementVisible(quotes_polices_page.TRANSACTION_HISTORY_TAB,"Transaction tab under search area");
			SafeVerifyElementVisible(quotes_polices_page.NEW_QUOTE_TAB,"New Quote Tab");
			SafeSelectOptionFromComboBox(quotes_polices_page.POLICY_CARRIER_COMBOBOX,policyCarrierOption,"Policy carrier dropdown");
			SafeSelectOptionFromComboBox(quotes_polices_page.PRODUCT_COMBOBOX,productOption,"Policy carrier dropdown");
			SafeClick(quotes_polices_page.EFFECTIVE_DATE_BUTTON,"'Effective Date' Button");
			SafeClick(quotes_polices_page._browserinstance+"//flexobject[@id='pxClient']//text[@caption='"+effective_date+"']","Effective Date");
			
		}
		/// <summary>
		/// Provide Quote details
		/// </summary>
		public void EnterQuoteDetails()
		{
			
			SafeVerifyElementEnabled(quotes_polices_page.NEXT_BUTTON,"Next button in new quote tab");
			SafeClick(quotes_polices_page.NEXT_BUTTON,"Next button in new quote tab");
			SafeVerifyElementEnabled(quotes_polices_page.SAVE_CLOSE_BUTTON,"'Save & Close' button");
			
			
			
		}
		
	}
}
