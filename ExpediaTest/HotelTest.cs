using System;
using Expedia;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class HotelTest
	{
		private Hotel targetHotel;
		private readonly int NightsToRentHotel = 5;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetHotel = new Hotel(NightsToRentHotel);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatHotelInitializes()
		{
			Assert.IsNotNull(targetHotel);
		}
		
		[TestMethod]
		public void TestThatHotelHasCorrectBasePriceForOneDayStay()
		{
			var target = new Hotel(1);
			Assert.AreEqual(45, target.getBasePrice());
		}
		
		[TestMethod]
		public void TestThatHotelHasCorrectBasePriceForTwoDayStay()
		{
			var target = new Hotel(2);
			Assert.AreEqual(90, target.getBasePrice());
		}
		
		[TestMethod]
		public void TestThatHotelHasCorrectBasePriceForTenDaysStay()
		{
			var target = new Hotel(10);
			Assert.AreEqual(450, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatHotelThrowsOnBadLength()
		{
			new Hotel(-5);
		}

        [TestMethod()]
        public void TestThatHotelDoesGetRoomOccupantFromTheDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            String roomOccupant = "Whale Rider";
            String anotherRoomOccupant = "Raptor Wrangler";

            Expect.Call(mockDB.getRoomOccupant(24)).Return(roomOccupant);
            Expect.Call(mockDB.getRoomOccupant(1025)).Return(anotherRoomOccupant);

            mocks.ReplayAll();

            Hotel target = new Hotel(10);
            target.Database = mockDB;            
            String result;

            result = target.getRoomOccupant(1025);
            Assert.AreEqual(anotherRoomOccupant, result);

            result = target.getRoomOccupant(24);
            Assert.AreEqual(roomOccupant, result);

            mocks.VerifyAll();
        }
		
	}
}
