using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using HotelBookingTests;
using HotelBookingTests.Common;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;

namespace Pages.HotelBookingTests
{
    internal class HotelBookingForm : BasePage
    {

        public HotelBookingForm(IWebDriver driver) : base(driver) { }
        internal TestData TestUserDetails { get; private set; }


        public bool IsLoaded
        {
          
            get
            {
                Reporter.LogTestStepForBugLogger(Status.Info, "Validate that Hotel Booking url has opened successfully.");
                _logger.Info($"{TestData.URL} url  has loaded");
                try
                {
                    Reporter.LogPassingTestStepToBugLogger($"{TestData.URL} url  has loaded");
                    return Driver.FindElement(By.XPath("//h1")).Displayed;
                }
                catch (NoSuchElementException)
                {
                    Reporter.LogTestStepForBugLogger(Status.Fail, "HotelBooking form has not loaded successfully.");
                    return false;
                }

            }
        }

        public IWebElement FirstNameField => Driver.FindElement(By.Id("firstname"));

        public IWebElement SurnameField => Driver.FindElement(By.Id("lastname"));

        public IWebElement PriceField => Driver.FindElement(By.Id("totalprice"));

                   
        public bool UserisCreatedSucess
        {

            get
            {
                Reporter.LogTestStepForBugLogger(Status.Info, "Validate that User booking is successful.");
                _logger.Info($"{TestData.FirstName1} {TestData.Surname1} is created");
                try
                {
                    Reporter.LogPassingTestStepToBugLogger($"{TestData.FirstName1} {TestData.Surname1} is created");
                    return Driver.FindElement(By.XPath("//div[@class='row']//*[contains(text(), 'User1')]")).Displayed;
                }
                catch (NoSuchElementException)
                {
                    Reporter.LogTestStepForBugLogger(Status.Fail, "Failed to create user.");
                    return false;
                }

            }
            
         }

        public bool UserisDeleted { 
             get
            {
                Reporter.LogTestStepForBugLogger(Status.Info, "Validate that User booking is deleted successfully.");
                _logger.Info($"{TestData.FirstName1} {TestData.Surname1} is created");
                try
                {
                    Reporter.LogPassingTestStepToBugLogger($"{TestData.FirstName2} {TestData.Surname2} is deleted");
                    return Driver.FindElements(By.XPath("//div[@class='row']//*[contains(text(), 'toBeDeleted')]")).Count == 0;
                }
                catch (NoSuchElementException)
                {
                    Reporter.LogTestStepForBugLogger(Status.Fail, "Failed to delete user.");
                    return false;
                }

            }
            
         }

        internal void GoToHotelBookingUrl()
        {

            _logger.Info($"User Opened url => {TestData.URL}");

            Driver.Navigate().GoToUrl(TestData.URL);

            Reporter.LogPassingTestStepToBugLogger($"Opens url=>{TestData.URL} for Hotel Booking form.");
            _logger.Trace($"Did Booking Page open successfully=>{IsLoaded}");

        }

        internal void PopulateUserDetails(string firstname, string lastname, double price, bool isDeposited)
        {
            FirstNameField.SendKeys(firstname);
            SurnameField.SendKeys(lastname);
            PriceField.SendKeys(Convert.ToString(price));

            if (isDeposited)
            {
                SelectAValueFromList(0);
            }
            else
            {
                SelectAValueFromList(1);
            }



        }

        internal void DeleteBooking()
        {
            ReadOnlyCollection<IWebElement> AllBookings;
            //List<string> BookingsList = new List<string>();

            AllBookings = Driver.FindElements(By.XPath("//div[@class='row']/following-sibling::div"));
            int totalBookings = AllBookings.Count;

            string DeleteButtonxpath = $"//div[@class='row']/following-sibling::div[position()={totalBookings}]";
            string DeleteAttributeID = Driver.FindElement(By.XPath(DeleteButtonxpath)).GetAttribute("id");

            string deleteButtonXpath = $"//*[@id='{DeleteAttributeID}']/div[7]/input";

            Driver.FindElement(By.XPath(deleteButtonXpath)).Click();

            Thread.Sleep(3000);
      
        }


        internal void Submit()
        {
            Driver.FindElement(By.XPath("//*[@value=' Save ']")).Click();
        }

        internal void SelectCheckoutDate()
        {
            
                int TodaysDate = (int)System.DateTime.Now.DayOfWeek;
                IWebElement dateTable = Driver.FindElement(By.Id("checkout"));
                dateTable.Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.XPath("//tr[5]/td[4]/a")).Click();         
    
        }

        internal void SelectCheckinDate()
        {
    
            DateTime TodaysDate = DateTime.Today;
            int day = TodaysDate.Day;

            IWebElement dateTable = Driver.FindElement(By.Id("checkin"));
            dateTable.Click();
            Thread.Sleep(1000);
            ReadOnlyCollection<IWebElement> columns = Driver.FindElements(By.XPath("//td/a"));

            foreach (IWebElement cell in columns)
            {
                if (Convert.ToInt32(cell.Text) == (day))
                {
    
                    dateTable.SendKeys(Keys.Enter);
                    break;
                }
                else
                {
                    continue;
                }
            }


        }



        public void SelectAValueFromList(int i)
        {
            var Select = new SelectElement(Driver.FindElement(By.Id("depositpaid")));
            Select.SelectByIndex(i);
        }

    }
    }