/*
 * Created by SharpDevelop.
 * User: Vijay
 * Date: 6/13/2014
 * Time: 11:58 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Ranorex;
using Ranorex.Core;
using RanorexTestFramework.Base;

namespace RanorexTestFramework.PageConstants
{
	/// <summary>
	/// Class contains QuotesandPolicies Page Elements.
	/// </summary>
	public class ICGQuotesandPoliciesPage_Elements
	{
		
		public String _browserinstance;
		//Search loading pop up under quotes and policies page
		public String SEARCH_POPUP="//flexobject[@id='pxClient']//container[@id='activityOverlay']";
		//Transaction history tab in quotes and policies page
		public String _transactionhistory_tab="//container[@type='Canvas']//tabpage[@title='Transaction History']";
		//Search field
		public String _search_box="//container[@type='Canvas']//text[@id='containsTextInput']";
		//First Result in the search results table
		public String _FirstResult="//table[@id='dataGrid']/row[@index='0']";
		
		public String _testcell="//table[@id='dataGrid']/row[@index='0']/cell[1]";
		//Quotes and policies drop down in the search area
		public String _quotesandpoliciesdropdown="//combobox[@id='typeFilterSelector']";
		
		public String _Quoteid="//text[@id='_PolicyViewer2_Label6']";
		public String _quotehead="//tabpage[@title~'CRU']";
		public String _quoteloader="//element[@displayname='_header7']/picture";
		//Quote tab
		public String _quote_tab="//element[@id='navBar']/button[@text='Quote']";
		
		public String _saveandclose_button="//container[@id='widgetPanel']/button[@id='activityButton' and @text='Save & Close']";
		//Premium rate banner
		public String _premiumrate="//container[@id='_PolicyViewer2_GradientHBox6']";
		//Active tab under Quote
		public String _activetab="//element[@id='navBar']/button[@pressed='True']";
		//Unlock button under Unreview pop up
		public String _unlockbutton="//button[@id='ok']";
		public String _previous_button="//button[@id='previous']";
		public String _next_button="//button[@id='next']";
		public String _sign_out="//strong[@innertext='Sign Out »']";
		public String _search_table="//div[#'clientHolder']/flexobject[@id='pxClient']//table[@id='dataGrid']";
		
		/// <summary>
    	/// New Quote button under home page
    	/// </summary>
    	public String NEW_QUOTE_BUTTON="//a[@innertext='New Quote']";
    	/// <summary>
    	///Search loading pop up under quotes and policies page
    	/// </summary>
		public String SEARCH_LOADING_POPUP="//flexobject[@id='pxClient']//container[@id='activityOverlay']";
		/// <summary>
		///Transaction history tab in quotes and policies page
		/// </summary>
		public String TRANSACTION_HISTORY_TAB="//container[@type='Canvas']//tabpage[@title='Transaction History']";
		/// <summary>
		/// New Quote tab
		/// </summary>
		public string NEW_QUOTE_TAB="//tabpage[@title='New Quote']";
		/// <summary>
		/// POLICY_CARRIER_COMBOBOX in New Quote tab
		/// </summary>
		public string POLICY_CARRIER_COMBOBOX="//combobox[@type='ComboBox' and @text='Select a carrier']";
		/// <summary>
		/// PRODUCT_COMBOBOX in New Quote tab
		/// </summary>
		public string PRODUCT_COMBOBOX="//combobox[@type='ComboBox' and @text='Select one']";
		/// <summary>
		/// EFFECTIVE_DATE_BUTTON in New Quote tab
		/// </summary>
		public string EFFECTIVE_DATE_BUTTON="//datetime[@type='DateField']/button[@type='Button'][2]";
		/// <summary>
		/// VIEW_MORE_TRANSACTIONS_LINK in quotes and policies page
		/// </summary>
		public string VIEW_MORE_TRANSACTIONS_LINK="//a[@innertext='View More Transactions »']";
		/// <summary>
		/// Next button in Next Quote tab
		/// </summary>
		public string NEXT_BUTTON ="//button[@id='next']";
		/// <summary>
		/// Retrieving notices status
		/// </summary>
		public string RETRIEVING_NOTICES_STATUS="//p[@innertext='Retrieving Notices…']";
		/// <summary>
		/// LOADING_IMAGE
		/// </summary>
		public string LOADING_IMAGE_STATUS="//img[@alt='Loading…']";
		
		public string SAVE_CLOSE_BUTTON ="//button[@id='activityButton' and @label='Save & Close']";
		/// <summary>
    	/// View more referrals link under home page
    	/// </summary>
    	public String VIEWMORE_REFERRALS_LINK="//a[@innertext='View More Referrals »']";
    	
    	//Quote Form Fields
    	public string FIRSTNAME_FIELD="//element[@id='InsuredFirstName']/text[@type='TextInput']";
    	public string LASTNAME_FIELD="//element[@id='InsuredLastName']/text[@type='TextInput']";
    	public string STREET_NUMBER_FIELD="//element[@id='PropertyStreetNumber']/text[@type='TextInput']";
    	public string STREET_NAME_FIELD="//element[@id='PropertyStreetName']/text[@type='TextInput']";
    	public string CITY_FIELD="//element[@id='PropertyCity']/text[@type='TextInput']";
    	public string ZIP_FIELD="//element[@id='PropertyZipCode']/text[@type='TextInput']";
    	public string VALIDATE_ADDRESS_BUTTON="//element[@id='validateAddress']/button[@text='Validate Address']";
    	
		/// <summary>
		/// constructor to get webdocument reference
		/// </summary>
		/// <param name="webdoc">webdoc holds the current Webdocument referenc</param>
		public ICGQuotesandPoliciesPage_Elements(WebDocument webdoc)
		{
			_browserinstance=webdoc.GetPath().ToString();
			 Variables();
		}
		/// <summary>
    	/// variables() holds the elements under ICGQuote area
    	/// </summary>
		private void Variables()
		{
		SEARCH_POPUP = _browserinstance+SEARCH_POPUP;
		_transactionhistory_tab = _browserinstance+_transactionhistory_tab;
		_search_box = _browserinstance+_search_box;
		_FirstResult = _browserinstance+_FirstResult;
		_testcell = _browserinstance+_testcell;
		_quotesandpoliciesdropdown = _browserinstance+_quotesandpoliciesdropdown;
		_Quoteid=_browserinstance+_Quoteid;
		_quotehead = _browserinstance+_quotehead;
		_quoteloader = _browserinstance+_quoteloader;
		_quote_tab =_browserinstance+_quote_tab;
		_saveandclose_button = _browserinstance+_saveandclose_button;
		_premiumrate = _browserinstance+_premiumrate;
		_activetab = _browserinstance+_activetab;
		_unlockbutton = _browserinstance+_unlockbutton;
		_previous_button = _browserinstance+_previous_button;
		_next_button = _browserinstance+_next_button;		
		_sign_out = _browserinstance+_sign_out;
		_search_table =_browserinstance+_search_table;
		NEW_QUOTE_BUTTON           = _browserinstance+NEW_QUOTE_BUTTON;
        SEARCH_LOADING_POPUP       = _browserinstance+SEARCH_LOADING_POPUP;
        NEW_QUOTE_TAB              = _browserinstance+NEW_QUOTE_TAB;
        POLICY_CARRIER_COMBOBOX    = _browserinstance+POLICY_CARRIER_COMBOBOX;
        PRODUCT_COMBOBOX           = _browserinstance+PRODUCT_COMBOBOX;
        VIEW_MORE_TRANSACTIONS_LINK= _browserinstance+VIEW_MORE_TRANSACTIONS_LINK;
        RETRIEVING_NOTICES_STATUS  = _browserinstance+RETRIEVING_NOTICES_STATUS;
        NEXT_BUTTON                = _browserinstance+NEXT_BUTTON;
        LOADING_IMAGE_STATUS       = _browserinstance+LOADING_IMAGE_STATUS;
        SAVE_CLOSE_BUTTON          = _browserinstance+SAVE_CLOSE_BUTTON;
        VIEWMORE_REFERRALS_LINK    = _browserinstance+VIEWMORE_REFERRALS_LINK;
		}
	
	}
}
