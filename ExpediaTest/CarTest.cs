using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using System.Collections.Generic;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestgetCarLocation()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            String location = "Louisville";
            String location2 = "New Zealand";

            using (mocks.Ordered())
            {
                Expect.Call(mockDB.getCarLocation(117)).Return(location);
                Expect.Call(mockDB.getCarLocation(42)).Return(location2);
                Expect.Call(mockDB.getCarLocation(Arg<int>.Is.Anything)).Return("Car not rented");
            }

            mocks.ReplayAll();

            Car target = new Car(50);
            target.Database = mockDB;

            String result;

            result = target.getCarLocation(117);
            Assert.AreEqual(location, result);

            result = target.getCarLocation(42);
            Assert.AreEqual(location2, result);

            result = target.getCarLocation(25);
            Assert.AreEqual("Car not rented", result);

            mocks.VerifyAll();
        }

        [TestMethod()]
        public void TestMileage()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            int Miles = 100;

            Expect.Call(mockDatabase.Miles).PropertyBehavior();

            mocks.ReplayAll();
            mockDatabase.Miles = Miles;
            var target = new Car(10);
            target.Database = mockDatabase;

            int mileCount = target.Mileage;
            Assert.AreEqual(mileCount, Miles);

            mocks.VerifyAll();
        }

        [TestMethod()]
        public void TestisBMW()
        {
            var target = ObjectMother.BMW();
            String carName = target.Name;
            Assert.AreEqual(carName, "BMW that moves hard and fast");
        }
	}
}
