/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 01-12-2014
 * Time: 17:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.IO;

namespace RanorexTestFramework.utilities
{
	/// <summary>
	/// Description of UtilityMethods.
	/// </summary>
	public class UtilityMethods
	{
		/// <summary>
		/// GetProjectLocation static method returns the project location
		/// </summary>
		/// <returns>returns the project location</returns>
		public static string GetProjectLocation()
        {
            string sDirectory = Environment.CurrentDirectory;
            sDirectory = sDirectory.Substring(0, sDirectory.LastIndexOf(@"\"));
            sDirectory = sDirectory.Substring(0, sDirectory.LastIndexOf(@"\"));
            return sDirectory;
        }

      /// <summary>
	  /// GetAlterDate static method returns the time in the specified format (by default it returns MM/dd/yy format e.x., 10/15/14) 
	  /// </summary>
	  /// <param name="day">Day</param>
	  /// <param name="month">Month</param>
	  /// <param name="year">Year</param>
	  /// <param name="format">format of the date (dafault date format is "MM/dd/yy")</param>
	  /// <returns>returns the current date as MM/dd/yy as default, For past date you need pass nagative values. ex: if day=-1
  	  ///       it consider yesterday date like if month=-1 it returns past month date</returns>
  	  public static string GetAlterDate(int day=0,int month=0,int year=0,string format="MM/dd/yy"){
  	  	if(format.Contains("/"))
  	  		return DateTime.Today.AddDays(day).AddMonths(month).AddYears(year).ToString(format).Replace('-','/');
  	  	else
  	  		return DateTime.Today.AddDays(day).AddMonths(month).AddYears(year).ToString(format);
  		 
  	  }
  	  
      //Method: GetMonthNameFromGivenDate
  	  //Purpose: used to get the Month name from given date based on the month_format (default value is "MMMM" which gives full name of the month)
  	  //@params: date -> date in which from you want to get the month name
  	  //@Param: format -> formate of the date which should match with given 'date' formate
  	  //return: Month name
  	  public static string GetMonthNameFromGivenDate(string date,string date_format, string month_format="MMMM"){
  	  	
	  	  	DateTime dt=new DateTime();
			string d=DateTime.ParseExact(date, date_format,CultureInfo.InvariantCulture).ToString();
			dt = DateTime.Parse(d);
			
		return dt.ToString(month_format);
  		 
  	  }
  	  
  	  public static string GetDayNameFromGivenDate(string date,string date_format, string day_format="dddd"){
  	  	
	  	  	DateTime dt=new DateTime();
			string d=DateTime.ParseExact(date, date_format,CultureInfo.InvariantCulture).ToString();
			dt = DateTime.Parse(d);
			
		return dt.ToString(day_format);
  		 
  	  }
  	  
  	   public static string GetYearFromGivenDate(string date,string date_format, string year_format="yyyy"){
  	  	
	  	  	DateTime dt=new DateTime();
			string d=DateTime.ParseExact(date, date_format,CultureInfo.InvariantCulture).ToString();
			dt = DateTime.Parse(d);
			
		return dt.ToString(year_format);
  		 
  	  }
  	  
  	  //Method: GetTodayMonthName
  	  //Purpose: Used to get the Current month full name
  	  //Param: format -> format of the month (default value is "MMMM")
  	  //return: returns current month full name
  	  public static string GetTodayMonthName(string format="MMMM"){
  	  	return DateTime.Today.ToString(format);
  		 
  	  }
  	  //Method: GetPastMonthName
  	  //Purpose: Used to get the Past month full name from current date based on past_month value
  	  //Param: past_month, format
  	  //return: returns month full name
  	  public static string GetPastMonthName(int past_month=0,string format="MMMM"){
  	  	return DateTime.Today.AddMonths(-past_month).ToString(format);
  		 
  	  }
  	  //Method: GetFutureMonthName
  	  //Purpose: Used to get the future month full name from current date based on future_month value
  	  //return: returns month full name
  	  public static string GetFutureMonthName(int future_month=0,string format="MMMM"){
  	  	return DateTime.Today.AddMonths(future_month).ToString(format);
  		 
  	  }
	  	
      public static string GetCurrentDate(){
      	string currentDate = DateTime.UtcNow.ToString("yyyy/MM/dd");
      	return currentDate;
      }
      public static string GetCurrentDate2(){
      	string currentDate = DateTime.UtcNow.ToString("M/d/yyyy");
      	return currentDate;
      }
      
      public static string GetFutureDate(int month){
      	DateTime dt=DateTime.Now;
		return dt.AddMonths(month).ToString("yyyy/MM/dd");
      }
      
      public static string GetFutureDate2(int month){
      	DateTime dt=DateTime.Now;
		return dt.AddMonths(month).ToString("M/d/yyyy");
      }
      
		//Purpose: Picks the Week of the Day from the given date in the format of 'ddd' (ex., mon,sun)
		//@param date: string type, need to be passed as a argument to call this method 
		//returns the Week of the Day
		public static string GetWeekOFDayFromGivenDate(string date){
			try{
				DateTime dt=new DateTime();
				string d=DateTime.ParseExact(date, "M/d/yyyy",CultureInfo.InvariantCulture).ToString("d-M-yyyy");
				dt = DateTime.Parse(d);
				return dt.ToString("ddd");
			}catch(FormatException){
				throw new FormatException();
			}
		}
	      
		//Purpose: Picks the Day from the given date in the format of 'dd' (ex.,08)
	  	//@param date: string type, need to be passed as a argument to call this method 
	  	//returns the Day	
		public static string GetDayFromGivenDate(string date){
				try{
					DateTime dt=new DateTime();
					string d=DateTime.ParseExact(date, "M/d/yyyy",CultureInfo.InvariantCulture).ToString("d-M-yyyy");
					dt = DateTime.Parse(d);
					return dt.ToString("dd");
				}catch(FormatException){
					throw new FormatException();
				}
			}
	  	
	  public static string GetDayFromGivenDate(string date=null,string date_format="MM/d/yyyy",string day_format="dd"){
	   try{
		    if(date==null)
		     date = DateTime.UtcNow.ToString(date_format).Replace("-","/");
		    DateTime dt=new DateTime();
		    string d=DateTime.ParseExact(date, date_format,CultureInfo.InvariantCulture).ToString("d-M-yyyy");
		    dt = DateTime.Parse(d);
		    if(dt.ToString("dd").StartsWith("0") && day_format=="d")
		     return dt.ToString("dd").Remove(0,1);
		    else
		     return dt.ToString("dd");
		  }catch(FormatException){
		    throw new FormatException();
		 }
	  }
		//Purpose: Picks the Month from the given date in the format of 'MM' (ex.,08)
	  	//@param date: string type, need to be passed as a argument to call this method 
	  	//returns the Month	
		public static string GetMonthFromGivenDate(string date){
			try{
				DateTime dt=new DateTime();
				string d=DateTime.ParseExact(date, "M/d/yyyy",CultureInfo.InvariantCulture).ToString("d-M-yyyy");
				dt = DateTime.Parse(d);
				return dt.ToString("MM");
			}catch(FormatException){
				throw new FormatException();
			}
		}
	  	
	  	//Purpose: Gets the Current time of local system in the format of h:mm
	  	//@Param timeformat : string type, specifies the format of the time either 12hr nor 24hr (by deafault is 12hr)
	  	//returns the current local time
	  	public static string GetCurrentTimeHoursAndMinutes(string timeformat="12hr"){
	  		if(timeformat=="12hr")
	  			return DateTime.Now.ToString("h:mm tt");
	  		else
	  			return DateTime.Now.ToString("H:mm tt");
	  	}
	  	
	  	//Method: GetCurrentDateAsMM_dd_yy
	  	//Purpose: Get the time in the format of MM/dd/yy e.x., 10/15/14
	  	//return: returns the current date as MM/dd/yy
	  	public static string GetTodayDateAsMM_dd_yy(string formate="-"){
	  		return DateTime.Today.ToString("MM/dd/yy").Replace("-",formate);
	  	}
		
	  	//Method: GetPastDateAsMM_dd_yy
	  	//Purpose: Get the time in the format of MM/dd/yy e.x., 10/15/14
	  	//return: returns the past date as MM/dd/yy (e.x., if past_day=1 it returns yesterday date)
	  	public static string GetPastDateAsMM_dd_yy(int past_day,string formate="-"){
	  		return DateTime.Today.AddDays(-past_day).ToString("MM/dd/yy").Replace("-",formate);
	  	}
	  	
	  	//Method: GetFutureDateAsMM_dd_yy
	  	//Purpose: Get the time in the format of MM/dd/yy e.x., 10/15/14
	  	//return: returns the Future date as MM/dd/yy (e.x., if past_day=1 it returns Tomorrow date)
	  	public static string GetFutureDateAsMM_dd_yy(int future_day,string formate="-"){
	  		return DateTime.Today.AddDays(future_day).ToString("MM/dd/yy").Replace("-",formate);
	  	}
	  	
	  	//Method: GetCurrentDayName
	  	//Purpose: Get the full day name e.x, Moday 
	  	//returns: Current day name
	  	public static string GetTodayDayName(){
	  		return DateTime.Today.DayOfWeek.ToString();
	  	}
	  	
	  	//Method: GetPastDayName
	  	//Purpose: Get the past day of full name based on the value which you pass as argument 
	  	//@Param: day -> if day=1 it returns yesterday's day name
	  	//return: returns the day name
	  	public static string GetPastDayName(int past_day){
	  		return DateTime.Today.AddDays(-past_day).DayOfWeek.ToString();
	  	}
	  	
	  	//Method: GetFutureDayName
	  	//Purpose: Get the Future day name based on the value which you pass as argument 
	  	//@Param: day -> if day=1 it returns tomorrow's day name
	  	//return: string
	  	public static string GetFutureDayName(int future_day){
	  		return DateTime.Today.AddDays(future_day).DayOfWeek.ToString();
	  	}
	  	

	}
}
