﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using NUnit.Framework;
using Ranorex;
using Ranorex.Core;
using RanorexTestFramework.Base;




namespace RanorexTestFramework.utilities
{/// <summary>
	/// Safe Actions containsall the safe operations
	/// </summary>
	public class SafeActions
	{
		public int index = 1;
		System.Timers.Timer _timer;
		System.Timers.Timer _visibleTimer;
		System.Timers.Timer _visiblenotTimer;
		readonly TimeSpan timeout = new TimeSpan(0, 1, 0);
		private WebDocument webdooc;
		
		/// <summary>
		/// Safe Actiosn constructor to receive current WebDocument instance
		/// </summary>
		/// <param name="webdoc"></param>
		public SafeActions(WebDocument webdoc)
		{
			webdooc = webdoc;
		}
		/// <summary>
		/// <description>
		/// waitandfindelement will verify 3 properties of an element
		/// 1. Element existance in the application
		/// 2. Element Visibility
		/// 3.Element Enability
		/// </description>
		/// </summary>
		/// <param name="identifier">Element locator(Usually Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the Element(to pass to reports)</param>
		/// <param name="waitTimeInSec">Timeout</param>
		/// <returns>It will return Element object</returns>
		public Element WaitAndFindelement(String identifier, String friendlyname="",int waitTimeInSec=60)
		{
			
			Element ele;
			try
			{
				
				ele = webdooc.FindSingle(identifier,TimeSpan.FromSeconds(waitTimeInSec));
			}
			catch(Ranorex.ElementNotFoundException)
			{
				Report.Failure(friendlyname+" not found after waiting for "+waitTimeInSec+"seconds");
				webdooc.EnsureVisible();
				Report.Screenshot();
				Assert.Fail(friendlyname+" not found after waiting for "+waitTimeInSec+" Seconds ");
				return null;
			}
			catch(RanorexException)
			{
				Report.Failure(friendlyname+" not found after waiting for "+waitTimeInSec+" Seconds");
				webdooc.EnsureVisible();
				Report.Screenshot();
				Assert.Fail("Exception at Element "+friendlyname);
				return null;
			}
			//Verifying element visibility
			if(!VerifyElementVisible(identifier,ele,waitTimeInSec))
			{
				Report.Failure(friendlyname+" is not Visible after waiting for "+waitTimeInSec+" Seconds");
				webdooc.EnsureVisible();
				Report.Screenshot();
				Assert.Fail(friendlyname+" is not Visible after waiting for "+waitTimeInSec+" Seconds");
				return null;
			}
			
			//Verifying element enability
			
			if(!VerifyElementEnabled(identifier,ele,waitTimeInSec))
			{
				Report.Failure(friendlyname+" does not enabled even after waiting for "+waitTimeInSec+" Seconds");
				webdooc.EnsureVisible();
				Report.Screenshot();
				Assert.Fail(friendlyname+" does not enabled after waiting for "+waitTimeInSec+" Seconds");;
			}
			
			ele.EnsureVisible();
			ele.Focus();
			SetHighlight(ele);
			return ele;
			
			
		}
		
		
		
		/// <summary>
		/// safeClick will perform left click operation on the element
		/// </summary>
		/// <param name="identifier">Element location(Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="waitTimeInSec">Timeout</param>

		public void SafeClick(String identifier, String friendlyname="",int waitTimeInSec = 60,string scroll=null)
		{

			Element ele1 = WaitAndFindelement(identifier,friendlyname,waitTimeInSec);
			
			try
			{
				if(ele1.FlavorElement.GetType().ToString().ToLower().Contains("web"))
				{
					WebElement ele=ele1;
					ele.EnsureVisible();
					ele.Click(MouseButtons.Left);
				}
				else if(ele1.FlavorElement.GetType().ToString()=="Ranorex.Plugin.FlexFlavorElement")
				{
					FlexElement ele=ele1;		
					ele.EnsureVisible();
					
					ele.Click(MouseButtons.Left);
				}
				else{
					Element ele=ele1;
					Mouse.Click(ele);
				}
				Report.Success("Clicked on "+friendlyname);
				webdooc.WaitForDocumentLoaded(TimeSpan.FromSeconds(waitTimeInSec));
				
			}catch(Ranorex.ElementNotFoundException )
			{
				Report.Failure("Unable to click on "+friendlyname);
				Report.Screenshot();
				Assert.Fail("Unable to click on "+friendlyname);
			}catch(RanorexException )
			{
				Report.Failure("Unable to click on "+friendlyname);
				Report.Screenshot();
				Assert.Fail("Unable to click on "+friendlyname+" ");
			}catch(Exception )
			{
				Report.Failure("Exception occured while clicking on "+friendlyname);
				webdooc.EnsureVisible();
				Report.Screenshot();
				Assert.Fail("Exception occured while clicking on "+friendlyname);
			}
			
			
			
		}
		/// <summary>
		/// safeDoubleClick will peform double click operation on the element
		/// </summary>
		/// <param name="identifier">Element location(Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="waitTimeInSec">Timeout</param>
		public void SafeDoubleClick(String identifier,String friendlyname="", int waitTimeInSec = 60)
		{

			Element ele = WaitAndFindelement(identifier,friendlyname,waitTimeInSec);
			try
			{
				Mouse.DoubleClick(ele);
				Report.Success("Double clicked on "+friendlyname);
				webdooc.WaitForDocumentLoaded(TimeSpan.FromSeconds(waitTimeInSec));
			}catch(Ranorex.ElementNotFoundException )
			{
				Report.Failure("Unable to double click on "+friendlyname);
				Report.Screenshot();
				Assert.Fail("Unable to double click on "+friendlyname);
			}catch(RanorexException )
			{
				Report.Failure("Unable to double click on "+friendlyname+"\n");
				Report.Screenshot();
				Assert.Fail("Unable to double click on "+friendlyname+" ");
			}catch(Exception )
			{
				Report.Failure("Exception occured while double clicking on "+friendlyname);
				webdooc.EnsureVisible();
				Report.Screenshot();
				Assert.Fail("Exception occured while double clicking on "+friendlyname);
			}
			
			
			
		}
		/// <summary>
		/// safegetAttribute will get specified attribute of an element
		/// </summary>
		/// <param name="identifier">Element locator(Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="attributename">Attribute name to be retrieved</param>
		/// <param name="waitTimeInSec">Timeout</param>
		/// <returns>Attribute will be returned in String format</returns>
		public String SafegetAttribute(String identifier,String attributename,String friendlyname="", int waitTimeInSec = 60)
		{
			Element ele;
			try
			{
				ele =webdooc.FindSingle(identifier,TimeSpan.FromSeconds(waitTimeInSec));
				ele.EnsureVisible();
			}
			catch(ElementNotFoundException)
			{
				Report.Failure(friendlyname+" does not exist after waiting for "+waitTimeInSec+" Seconds");
				Report.Screenshot();
				Assert.Fail(friendlyname+" does not exist after waiting for "+waitTimeInSec+" Seconds");
				return null;
			}
			
			try
			{
				
				
				return ele.GetAttributeValue(attributename).ToString();
				
			}
			catch (Exception)
			{
				return "Attribute not exist for given element";
			}
		}
		/// <summary>
		/// Moves the mouse pointer to specified element
		/// </summary>
		/// <param name="identifier">Element locator(Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="waitTimeInSec">TimeOUt</param>
		public void SafeMovetoElement(String identifier, String friendlyname="",int waitTimeInSec = 60)
		{
			Element ele = WaitAndFindelement(identifier,friendlyname,waitTimeInSec);
			try
			{
				Mouse.MoveTo(ele,3000);
				Report.Success("Moved to "+friendlyname);
			}
			catch(Ranorex.ElementNotFoundException)
			{
				Report.Failure("Unable to move to element "+friendlyname);
				Report.Screenshot();
				Assert.Fail("Unable to move to element "+friendlyname);
			}catch(RanorexException)
			{
				Report.Failure("Unable to move to "+friendlyname+" ");
				Report.Screenshot();
				Assert.Fail("Unable to move to "+friendlyname+" ");
			}catch(Exception)
			{
				Report.Failure("Unable to move to "+friendlyname+" ");
				webdooc.EnsureVisible();
				Report.Screenshot();
				Assert.Fail("Unable to move to "+friendlyname+" ");
			}
			
		}
		/// <summary>
		/// Moves the mouse pointer to specified coordinates
		/// </summary>
		/// <param name="xcoordinate">X axis</param>
		/// <param name="ycoordinate">Y axis</param>
		public void SafeMovetoCoordinates(int xcoordinate, int ycoordinate)
		{
			Point p=new Point();
			p.X = xcoordinate;
			p.Y = ycoordinate;
			Location l = p;
			try{
				webdooc.MoveTo(l, 3000);
				Report.Success("Moved to ("+p.X+","+p.Y+")");
			}
			catch (Exception )
			{
				
			}
			
		}
		/// <summary>
		/// Enters given text under specified Element
		/// </summary>
		/// <param name="identifier">Element locator</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="inputtext">Text to enter</param>
		/// <param name="waitTimeInSec">Timeout</param>
		public void SafeType(String identifier,String inputtext,String friendlyname="", int waitTimeInSec = 60)
		{
			Element ele1=WaitAndFindelement(identifier,friendlyname,waitTimeInSec);
			RxPath path=identifier;
			
			try
			{
				
				if(ele1.FlavorElement.GetType().ToString()=="Ranorex.Plugin.FlexFlavorElement")
				{
					Mouse.Click(ele1);
					Ranorex.Text txt=ele1;
					txt.TextValue=inputtext;
					Keyboard.Press(Keys.Down);
					
				}
				else
				{
					InputTag ele = ele1;
					
					ele.PressKeys(inputtext);
				}
				Report.Success("Entered "+inputtext+" under "+friendlyname);
				webdooc.WaitForDocumentLoaded(TimeSpan.FromSeconds(waitTimeInSec));
			}catch(Ranorex.ElementNotFoundException )
			{
				Report.Failure("Unable to enter text in "+friendlyname);
				Report.Screenshot();
				Assert.Fail("Unable to enter text in "+friendlyname);
			}catch(Ranorex.RanorexException )
			{
				Report.Failure("Unable to enter text in "+friendlyname);
				Report.Screenshot();
				Assert.Fail("Unable to enter text in "+friendlyname);
			}catch(Exception )
			{
				Report.Failure("Unable to enter text in "+friendlyname+" ");
				webdooc.EnsureVisible();
				Report.Screenshot();
				Assert.Fail("Unable to enter text in "+friendlyname+" ");
			}
			
		}
		/// <summary>
		/// Waits for the element enability
		/// </summary>
		/// <param name="identifier">Element locator(Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="waitTimeInSec">Timeout</param>
		/// <returns>true on success and false after timeout</returns>
		public Boolean SafeVerifyElementEnabled(String identifier,String friendlyname="", int waitTimeInSec = 60)
		{
			Element ele;
			try
			{
				ele=webdooc.FindSingle(identifier,TimeSpan.FromSeconds(waitTimeInSec));
				
			}catch(ElementNotFoundException )
			{
				return false;
			}catch(RanorexException )
			{
				return false;
				
			}catch(Exception )
			{
				return false;
			}
			
			bool boolval=VerifyElementEnabled(identifier,ele,waitTimeInSec);
			
			return boolval;
			
		}
		
		private Boolean VerifyElementEnabled(String identifier, Element elem,int waitTimeInSec = 60)
		{
			int waitTimeInMilliSec=waitTimeInSec*1000;
			_timer = new System.Timers.Timer(waitTimeInMilliSec);
			
			Element ele = elem;
			
			index = 1000;
			_timer.Elapsed += (sender,args)=>TimerMethod(ele,index);
			_timer.Interval = 500;
			
			_timer.Enabled = true;
			
			while(!ele.Enabled)
			{
				
				
				if(index>=30000)
				{
					
					break;
				}
				
			}
			index = 1000;
			_timer.Stop();
			
			return ele.Enabled;
		}

		private void TimerMethod(Element ele, int index1)
		{
			
			

			index=index+(int)_timer.Interval;
			
		}
		/// <summary>
		/// Waits for the element visibility
		/// </summary>
		/// <param name="identifier">Element Locator(Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="waitTimeInSec">Timeout</param>
		/// <returns>true on success and false after timeout</returns>
		public Boolean SafeVerifyElementVisible(String identifier,String friendlyname="",  int waitTimeInSec = 60)
		{
			
			Element ele;
			try
			{
				ele=webdooc.FindSingle(identifier,TimeSpan.FromSeconds(waitTimeInSec));
				
			}catch(ElementNotFoundException )
			{
				return false;
				
			}catch(RanorexException )
			{
				return false;
			}catch(Exception )
			{
				return false;
			}
			
			
			bool boolval=VerifyElementVisible(identifier,ele,waitTimeInSec);
			
			return boolval;
		}
		
		private Boolean VerifyElementVisible(String identifier, Element elem,int waitTimeInSec = 60)
		{
			
			int milliSec=waitTimeInSec*1000;
			_visibleTimer = new System.Timers.Timer(milliSec);

			Element ele = elem;
			index = 1000;
			_visibleTimer.Elapsed += (sender, args) => VisibleTimerMethod(ele, index);
			_visibleTimer.Interval = 100;

			_visibleTimer.Enabled = true;
			
			
			while(!ele.Visible)
			{
				
				if(index>=milliSec)
				{
					break;
				}
				
			}
			index = 1000;
			
			_visibleTimer.Close();
			
			return ele.Visible;
			
			
		}
		private void VisibleTimerMethod(Element ele, int index1)
		{
			
			index=index+(int)_visibleTimer.Interval;
			
		}
		/// <summary>
		/// waits untill element disapear
		/// </summary>
		/// <param name="identifier">Element locator(Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="waitTimeInSec">Timeout</param>
		/// <returns>true on success and false after timeout</returns>
		public Boolean SafeVerifyElementNotVisible(String identifier, String friendlyname="",int waitTimeInSec = 60)
		{
			Element ele;
			try
			{
				ele=webdooc.FindSingle(identifier, TimeSpan.FromSeconds(5));
			}catch(ElementNotFoundException )
			{
				return false;
				
			}catch(RanorexException )
			{
				return false;
			}catch(Exception )
			{
				return false;
			}
			
			bool testval=SafeVerifyElementVisible(identifier,friendlyname,3);
			//MessageBox.Show(testval.ToString());
			if(testval)
			{
				
				bool boolval=VerifyElementNotVisible(identifier,ele,waitTimeInSec);
				testval=boolval;
			}
			else{
				return true;
			}
			//MessageBox.Show(testval.ToString());
			return testval;
		}
		
		/// <summary>
		/// waits untill element disapear
		/// </summary>
		/// <param name="identifier">Element locator(Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="waitTimeInSec">Timeout</param>
		/// <returns>true on success and false after timeout</returns>
		public Boolean WaitUntilElementDisappears(String identifier, String friendlyname="",int waitTimeInSec = 60)
		{
			Element ele;
			int waitMinimumTime=5000;
			int milliSec=waitTimeInSec*1000;
			bool flag=true;
			try{
				ele=webdooc.FindSingle(identifier, TimeSpan.FromMilliseconds(waitMinimumTime));
				SetHighlight(ele);
			}catch{}
			while(waitMinimumTime<=waitTimeInSec){
				try{
					ele=webdooc.FindSingle(identifier, TimeSpan.FromMilliseconds(500));
					SetHighlight(ele);
					//if(!ele.Visible)
						flag=true;
				}catch(Exception){
					flag=false;
				}
				if(!flag)
					return true;
				waitMinimumTime=waitMinimumTime+500;
			}
			return false;
		}
		
		public Boolean VerifyElementNotVisible(String identifier, Element elem,int waitTimeInSec = 60)
		{
			int milliSec=waitTimeInSec*1000;
			_visiblenotTimer = new System.Timers.Timer(milliSec);

			
			Element ele = elem;
			
			index = 1000;
			_visiblenotTimer.Elapsed += (sender, args) => NotVisibleTimerMethod(ele, index);
			_visiblenotTimer.Interval = 100;

			_visiblenotTimer.Enabled = true;
			
			
			while(ele.Visible)
			{
				
				
				if(index>=milliSec)
				{
					break;
				}
				
			}
			
			index = 1000;
			_visiblenotTimer.Close();
			
			
			return !ele.Visible;
		}

		private void NotVisibleTimerMethod(Element ele, int index1)
		{
			
			index = index + (int)_visiblenotTimer.Interval;

		}
		
		/// <summary>
		/// It will bring the element o visibility
		/// </summary>
		/// <param name="identifier">Element locator(Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="waitTimeInSec">Timeout interval</param>
		public void SafeEnsureVisible(String identifier,String friendlyname="",int waitTimeInSec = 60)
		{
			Element ele=WaitAndFindelement(identifier,friendlyname,waitTimeInSec);
			ele.EnsureVisible();
			
		}
		
		public Boolean SafeFindTextInEachRow(String tableidentifier, String texttosearch, String friendlyname="",int waitTimeInSec = 60)
		{
			Table tb=WaitAndFindelement(tableidentifier,friendlyname,waitTimeInSec);
			IList<Row> rows=tb.Rows;
			IList<Column> cols=tb.Columns;
			String s="";
			for(int i=0;i<rows.Count;i++)
			{int count=0;
				IList<Cell> cells=rows[i].Cells;
				foreach(var ele in cells)
				{ if(ele.Text.ToString().ToLower().Contains(texttosearch.ToLower()))
					{
						count=1;
					}
				}
				if(count!=1)
				{
					return false;
				}
				
			}
			return true;
		}
		/// <summary>
		/// It will select an option under drop down
		/// </summary>
		/// <param name="identifier">Element locator(Rxpath)</param>
		/// <param name="friendlyname">Friendly name of the element</param>
		/// <param name="optiontext">Option text to be selected</param>
		/// <param name="waitTimeInSec">Timeout interval</param>
		public void SafeSelectOptionFromComboBox(String identifier,String optiontext, String friendlyname="",int waitTimeInSec = 60)
		{
			Ranorex.ComboBox ele=WaitAndFindelement(identifier,friendlyname,waitTimeInSec);
			string option="";
			try
			{
				ele.Click();
				Report.Success("--------------->"+ele.Children.Count +"");
				while(true){
					option=ele.SelectedItemText;
					if(ele.SelectedItemText.Contains(optiontext)){
						Keyboard.Press(Keys.Enter);
						break;
					}
					Keyboard.Press(Keys.Down);
					if(option==ele.SelectedItemText){
						throw new Exception("'"+optiontext+"' option is not existing in "+friendlyname+" dropdown list");
					}
						
				}
			
			//Keyboard.Press(optiontext);
			//Keyboard.Press(Keys.Enter);
			Report.Success("Selected '"+optiontext+"' under "+friendlyname);
			webdooc.WaitForDocumentLoaded(TimeSpan.FromSeconds(waitTimeInSec));
			
			}catch(Ranorex.ElementNotFoundException )
			{
				Report.Failure("Unable to select "+friendlyname);
				Report.Screenshot();
				Assert.Fail("Unable to select "+friendlyname);
			}catch(Ranorex.RanorexException )
			{
				Report.Failure("Unable to select "+friendlyname);
				Report.Screenshot();
				Assert.Fail("Unable to select "+friendlyname);
			}catch(Exception e)
			{
				Report.Failure(e.Message);
				webdooc.EnsureVisible();
				Report.Screenshot();
				Assert.Fail(e.Message);
				
			}
			
			
		}
		public void SetHighlight(Element ele1)
			
		{
			try
			{
				if(ele1.FlavorElement.GetType().ToString().ToLower().Contains("web"))
				{
					WebElement ele=ele1;
					string bordercolor=ele.GetStyle("border");
					ele.EnsureVisible();
					ele.SetStyle("border","3px solid red");
					Thread.Sleep(100);
					ele.SetStyle("border",bordercolor);
					
					
					
				}
				else if(ele1.FlavorElement.GetType().ToString()=="Ranorex.Plugin.FlexFlavorElement")
				{
					FlexElement ele=ele1;
					ele.EnsureVisible();
					string bordercolor=ele.GetStyle("borderColor");
					string bordetthickness=ele.GetStyle("borderThickness");
					string borderstyle=ele.GetStyle("borderStyle");
					ele.SetStyle("borderColor","#ee0000");
					ele.SetStyle("borderThickness","3");
					ele.SetStyle("borderStyle","solid");
					Thread.Sleep(100);
					ele.SetStyle("borderColor",bordercolor);
					ele.SetStyle("borderThickness",bordetthickness);
					ele.SetStyle("borderStyle",borderstyle);
					
					
					
				}
				else{
					
				}
			}catch(ActionFailedException )
			{
				
			}
			catch(Exception )
			{
				
			}
			
		}

		
	}
}
