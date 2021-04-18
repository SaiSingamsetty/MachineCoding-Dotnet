using System;
using System.Collections.Generic;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitBills.Models;
using SplitBills.Repository;
using SplitBills.Services;
using SplitBills.Services.Interfaces;
using Xunit;

namespace SplitBills.Tests
{
    public class ServiceTests
    {
        private readonly IBillsService _billsService;
        private readonly IUserService _userService;

        public ServiceTests()
        {
            INotificationService notificationService = new NotificationService();
            _billsService = new BillsService(notificationService);
            _userService = new UserService();
        }

        [Fact]
        public void TestMethod1()
        {
            //Arrange
            var sai = _userService.RegisterUser("sai@gmail.com", "123", "Sai");
            var dinesh = _userService.RegisterUser("dinesh@gmail.com", "234", "Dinesh");
            var dilip = _userService.RegisterUser("dilip@gmail.com", "345", "Dilip");

            var shares = new List<UserShare>()
            {
                new UserShare(dinesh.UserId, 1300),
                new UserShare(dilip.UserId, 1600),
                new UserShare(sai.UserId, 1100)
            };
            var response =
                _billsService.CreateBill("Ooty", "Trip to Ooty-Coonor", 4000, sai.UserId, shares);

            //Act
            var settlementDilipS1 = new Settlement()
            {
                Description = "S1",
                Value = 900,
                SettledDate = DateTime.Now,
                TransactionId = "T1"
            };
            _userService.AddSettlementForABill(response.Id, dilip.Email, settlementDilipS1);

            //Asserts
            var billsData = BillsRepository.BillsData;
        }
    }
}
