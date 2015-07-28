using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ranorex;
using Ranorex.Core;
using RanorexTestFramework.Base;

namespace RanorexTestFramework.PageConstants
{/// <summary>
/// Class contains ICG Home page elements
/// </summary>
    public class ICGHome_Elements
    {
    	/// <summary>
		/// _browserinstance holds the webdocument identifier
		/// </summary>
    	public String _browserinstance;
    	/// <summary>
    	/// Sign out element reference
    	/// </summary>
    	public String SIGNOUT_LINK="//div[@Id='signout']/a";
    	/// <summary>
    	/// quotes and policies link under home page
    	/// </summary>
    	public String QUOTE_SAND_POLICIES_LINK="//a[@innertext='Quotes & Policies']";
    	/// <summary>
    	/// View more referrals link under home page
    	/// </summary>
    	public String VIEWMORE_REFERRALS_LINK="//a[@innertext='View More Referrals »']";
    	/// <summary>
		/// LOADING_IMAGE
		/// </summary>
		public string LOADING_IMAGE_STATUS="//img[@alt='Loading…']";
		/// <summary>
    	/// Constructor to get webdocument reference
    	/// </summary>
    	/// <param name="webdoc">Current webdocument reference</param>
    	public ICGHome_Elements(WebDocument webdoc)
    	{
    		_browserinstance= webdoc.GetPath().ToString();
    		
    		Variables();
    	
    	}
    	/// <summary>
    	/// variables() holds the elements under ICGHome page
    	/// </summary>
    	private void Variables()
    	{
        SIGNOUT_LINK               = _browserinstance+SIGNOUT_LINK;
        QUOTE_SAND_POLICIES_LINK   = _browserinstance+QUOTE_SAND_POLICIES_LINK;
        VIEWMORE_REFERRALS_LINK    = _browserinstance+VIEWMORE_REFERRALS_LINK;
        LOADING_IMAGE_STATUS       = _browserinstance+LOADING_IMAGE_STATUS;
    	}
    }
}
