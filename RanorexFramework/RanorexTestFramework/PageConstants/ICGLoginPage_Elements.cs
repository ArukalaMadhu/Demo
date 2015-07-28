﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ranorex;
using Ranorex.Core;
using RanorexTestFramework.Base;

namespace RanorexTestFramework.PageConstants
{/// <summary>
/// Class contains ICGLogin_Page elements
/// </summary>
    public class ICGLoginPage_Elements
    {
    	public String _browserinstance;
    	//User Name field in login page
    	public String _username_text="//input[#'username']";
    	//Password field in Login page
    	public String _password_text="//input[#'userpassword']";
    	//Login button in login page
    	public String _login_button="//input[#'login-submit']";
    	/// <summary>
    	/// Constructor to get webdocument reference
    	/// </summary>
    	/// <param name="webdoc">webdoc holds the current Webdocument reference</param>
    	public ICGLoginPage_Elements(WebDocument webdoc)
    	{
    		_browserinstance=webdoc.GetPath().ToString();
    		Variables();
    	}
    	/// <summary>
    	/// variables() holds the elements under ICGLogin page
    	/// </summary>
    	private void Variables()
    	{
 	
        _username_text = _browserinstance+_username_text;
        _password_text = _browserinstance+_password_text;
        _login_button = _browserinstance+_login_button;
    	}
    }
}
