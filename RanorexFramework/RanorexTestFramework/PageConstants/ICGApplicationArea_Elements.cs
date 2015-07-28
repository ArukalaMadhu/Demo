﻿/*
 * Created by SharpDevelop.
 * User: Vijay
 * Date: 6/17/2014
 * Time: 4:12 PM
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
	/// Class contians ApplicationArea_Elements.
	/// </summary>
	public class ICGApplicationArea_Elements
	{
		/// <summary>
		/// _browserinstance holds the webdocument identifier
		/// </summary>
		public String _browserinstance;
		/// <summary>
		/// _No_radiobutton holds the 'No' radio button element 
		/// </summary>
		public String _No_radiobutton="//container[@id='appForm']//element[@id='ResidenceHeldInTrust']//radiobutton[@text='No']";
		/// <summary>
		/// _married_radiobutton holds married radio button element 
		/// </summary>
		public String _married_radiobutton="//container[@id='appForm']//element[@id='InsuredMaritalStatus']//radiobutton[@text='Married']";
		/// <summary>
		/// Start review button under review pop up
		/// </summary>
		public String _startreview_button="//button[@id='ok']";
		/// <summary>
		/// Review banner after completion of Review
		/// </summary>
		public String _review_banner="//container[@id='submissionForm']//container[@id='_SubmissionForm_InformationBanner1']";
		/// <summary>
		/// Gets webdocument reference and pass it to all the elements
		/// </summary>
		/// <param name="webdoc">Webdocument reference</param>
		public ICGApplicationArea_Elements(WebDocument webdoc)
		{
			_browserinstance= webdoc.GetPath().ToString();
			Variables();
		}
		/// <summary>
		/// variables() holds all the elements of ICGApplication area
		/// </summary>
		private void Variables()
		{
		_No_radiobutton = _browserinstance+_No_radiobutton;
		_married_radiobutton = _browserinstance+_married_radiobutton;
		_startreview_button = _browserinstance+_startreview_button;
		_review_banner =_browserinstance+_review_banner;
		}
		
	}
}
