/*
 * Created by SharpDevelop.
 * User: Vijay
 * Date: 6/17/2014
 * Time: 4:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ranorex;
using Ranorex.Core;
using RanorexTestFramework.Base;

namespace RanorexTestFramework.PageConstants
{
	/// <summary>
	/// Class contains ICGQuoteArea Elements.
	/// </summary>
	public class ICGQuoteArea_Elements
	{
		public String _browserinstance;
		//First name field under quote area
		public String _First_name="//div[#'clientHolder']/flexobject[@id='pxClient']/container[@id='main']/element[@type='SuperTabNavigator']/container[@type='Canvas'][1]/container[@type='HDividedBox']/container[@type='VBox'][2]/container[@id='main']/container[@id='quoteForm']/element/element/element[@id='InsuredFirstName']/text[@type='TextInput']";
		//Last Name field under quote area
		public String _Last_name="//div[#'clientHolder']/flexobject[@id='pxClient']/container[@id='main']/element[@type='SuperTabNavigator']/container[@type='Canvas'][1]/container[@type='HDividedBox']/container[@type='VBox'][2]/container[@id='main']/container[@id='quoteForm']/element/element/element[@id='InsuredLastName']/text[@type='TextInput']";
		//street name field under quote area
		public String _Street_name="//div[#'clientHolder']/flexobject[@id='pxClient']/container[@id='main']/element[@type='SuperTabNavigator']/container[@type='Canvas'][1]/container[@type='HDividedBox']/container[@type='VBox'][2]/container[@id='main']/container[@id='quoteForm']/element/element/element[@id='PropertyStreetName']/text[@type='TextInput']";
		//zip code field under quote area
		public String _zip_code="//div[#'clientHolder']/flexobject[@id='pxClient']/container[@id='main']/element[@type='SuperTabNavigator']/container[@type='Canvas'][1]/container[@type='HDividedBox']/container[@type='VBox'][2]/container[@id='main']/container[@id='quoteForm']/element/element/element[@id='PropertyZipCode']/text[@type='TextInput']";
		/// <summary>
		/// onstructor to get webdocument reference
		/// </summary>
		/// <param name="webdoc">webdoc holds the current Webdocument referenc</param>
		public ICGQuoteArea_Elements(WebDocument webdoc)
		{
			_browserinstance=webdoc.GetPath().ToString();
				Variables();
		}
		/// <summary>
    	/// variables() holds the elements under ICGQuote area
    	/// </summary>
		private void Variables()
		{
			
		_First_name =_browserinstance+_First_name;
		_Last_name = _browserinstance+_Last_name;
		_Street_name = _browserinstance+_Street_name;
		_zip_code = _browserinstance+_zip_code;
		}
	}
}
