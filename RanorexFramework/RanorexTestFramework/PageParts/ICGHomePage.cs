/*
 * Created by SharpDevelop.
 * User: Vijay
 * Date: 6/12/2014
 * Time: 12:09 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using Ranorex;
using RanorexTestFramework.PageConstants;
using RanorexTestFramework.utilities;

namespace RanorexTestFramework.PageParts
{
	/// <summary>
	/// Contians the operations which can be performed under home page
	/// </summary>
	public class ICGHomePage : SafeActions
	{
		WebDocument webdoc;
		ICGHome_Elements homepage;
		/// <summary>
		/// Contructor to grab current webdocument reference and create required page constant objects
		/// </summary>
		/// <param name="webdocument">Current webdocument reference</param>
		public ICGHomePage(WebDocument webdocument): base(webdocument)
		{
			webdoc = webdocument;
			homepage=new ICGHome_Elements(webdocument);
			
		}
		/// <summary>
		/// Navigates to Quotes and policies tab
		/// </summary>
		public void NavigateToQuotesandPoliciies()
		{
			Report.LogHtml(ReportLevel.Info,"<i>Navigating to 'Quotes and Policies' section</i>");
			webdoc.WaitForDocumentLoaded();
			
			SafeClick(homepage.QUOTE_SAND_POLICIES_LINK,"'Quotes and Policies' link in 'Home' page");
			
			
		}
		
			
		
	}
}
