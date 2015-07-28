using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using NUnit.Framework;
using Ranorex;
using Ranorex.Core;
using RanorexTestFramework.PageConstants;
using RanorexTestFramework.utilities;

namespace RanorexTestFramework.PageParts
{
	/// <summary>
	/// Operations which can be perfomrmed under ICG_Login Page
	/// It will extend the SafeActions class to perform operations
	/// </summary>
	public class ICGLoginPage : SafeActions
	{

		WebDocument webdoc;
		ICGLoginPage_Elements loginpage;
		ICGHome_Elements homepage;
		/// <summary>
		/// Constuctor which will take the WebDocument Object from test and pass it over to safeActions
		/// </summary>
		/// <param name="webdocument"></param>
		public ICGLoginPage(WebDocument webdocument): base(webdocument)
		{
			webdoc = webdocument;
			loginpage=new ICGLoginPage_Elements(webdocument);
			homepage=new ICGHome_Elements(webdocument);
			
		}
		
		
		/// <summary>
		/// <description>
		/// Perfoms Login Operation by using safeAction methods in SafeActions Class
		/// SafeAction will take RanoreXpath Strings from PageConstants
		/// </description>
		/// </summary>
		/// <param name="_username">User name will be taken from test</param>
		/// <param name="_password">Password will be taken from test</param>
		public void PerformLogin(String _username,String _password)
		{
			Report.LogHtml(ReportLevel.Info,"<i>Started 'Login' Operation</i>");
			SafeType(loginpage._username_text,_username,"'Username Field' in 'Login' page");
			SafeType(loginpage._password_text,_password,"'Password Field' in 'Login' Page");
			SafeClick(loginpage._login_button,"'Login' Button in 'Login' Page");
			WaitUntilElementDisappears(homepage.LOADING_IMAGE_STATUS,"LOADING_IMAGE_STATUS");
		}
		/// <summary>
		/// <description>
		/// Verify Login Operation by using safeAction methods in SafeActions Class
		/// SafeAction will take RanoreXpath Strings from PageConstants
		/// </description>
		/// </summary>
		public void VerifyLogin()
		{
			if(!SafeVerifyElementVisible(homepage.SIGNOUT_LINK,"Home Page_Sing out"))
			{
				Report.Failure("Login unsuccessful - Sign out element not found even after waiting for 30 sec");
				Report.Screenshot();
				Assert.Fail("Login unsuccessful - Sign out element not found even after waiting for 30 sec");
			}
			
		}
		
		
		
		

	}
}
