/*
 * Created by SharpDevelop.
 * User: Vijay
 * Date: 6/12/2014
 * Time: 1:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using NUnit.Core;
using NUnit.Core.Extensibility;
using NUnit.Framework;
//using Ranorex;

namespace RanorexTestFramework.Listeners
{
	/// <summary>
	/// Description of AssertListener.
	/// </summary>
	[NUnitAddinAttribute(Type = ExtensionType.Core,
        Name = "Database Addin",
        Description = "Writes test results to the database.")]
	public class MyNunitExtentions : IAddin, EventListener
	{
		
		public bool Install(IExtensionHost host)
		{
            IExtensionPoint listeners = host.GetExtensionPoint("EventListeners");
            if (listeners == null)
                return false;
            
            listeners.Install(this);
            return true;
        }

        public void RunStarted(string name, int testCount)
        {
        	
        }

        public void RunFinished(TestResult result)
        {
           
        }

        public void RunFinished(Exception exception)
        {
           
        }

        public void TestStarted(TestName testName)
        {
        	MessageBox.Show("test Started");
        }

        public void TestFinished(TestResult result)
        {
            
            //try
            //{
            //    EnerWorks.Utilities.AutomationUtilities.EmailResults();
            //}
            //catch (Exception e)
            //{
            //    listenerLogger.Error(e.Message + "\n" + e.StackTrace);
            //}
//	        if (result.IsFailure)
//	        {
//	        	AutomationUtilities.getScreenshot(BaseClass.getDriver());
//	        }
        }

        public void SuiteStarted(TestName testName)
        {
           
        }

        public void SuiteFinished(TestResult result)
        {
        	MessageBox.Show("Suite Finished");
        }

        public void UnhandledException(Exception exception)
        {
        	
        }

        public void TestOutput(TestOutput testOutput)
        {
//	    	
        }
		
	}
}
