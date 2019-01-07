using System;
using System.Threading;
using HotelBookingTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pages.HotelBookingTests;

namespace HotelBookingTests
{

    [TestClass]
    [TestCategory("UserCreatesBooking")]
    public class Bookings : BaseTest
    {

        [TestMethod]
        public void TC01UserCreatesBooking()
        {
 
            HotelBookingForm.GoToHotelBookingUrl();
            Thread.Sleep(1000);
            Assert.IsTrue(HotelBookingForm.IsLoaded, "Hotel Booking url page is not loaded");

            HotelBookingForm.PopulateUserDetails(TestData.FirstName1, TestData.Surname1, TestData.Price1, TestData.DepositType1);
            HotelBookingForm.SelectCheckinDate();
            HotelBookingForm.SelectCheckoutDate();
            Thread.Sleep(2000);

            HotelBookingForm.Submit();
            Thread.Sleep(8000);
            Assert.IsTrue(HotelBookingForm.UserisCreatedSucess, "User booking has failed");
            Thread.Sleep(4000);
        }


        [TestMethod]
        public void TC02UserDeletesBooking()
        {

            HotelBookingForm.GoToHotelBookingUrl();
            Assert.IsTrue(HotelBookingForm.IsLoaded, "Hotel Booking url page is not loaded");

            HotelBookingForm.PopulateUserDetails(TestData.FirstName2, TestData.Surname2, TestData.Price2, TestData.DepositType2);
            HotelBookingForm.SelectCheckinDate();
            HotelBookingForm.SelectCheckoutDate();
            Thread.Sleep(2000);

            HotelBookingForm.Submit();
            Thread.Sleep(5000);

            HotelBookingForm.DeleteBooking();
            Assert.IsTrue(HotelBookingForm.UserisDeleted, "User booking has not been deleted");

            Thread.Sleep(4000);

        }


    }
}



