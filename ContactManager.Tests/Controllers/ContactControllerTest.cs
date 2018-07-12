﻿using System.Web.Mvc;
using ContactManager.Controllers;
using ContactManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContactManager.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        private Mock<IContactManagerService> _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new Mock<IContactManagerService>();
        }

        [TestMethod]
        //When_MyCondition_Then_ExpectedREsult
        public void When_ValidContact_Then_IndexAction ()
        {
            // Arrange
            var contact = new Contact();
            _service.Expect(s => s.CreateContact(contact)).Returns(true);
            var controller = new ContactController(_service.Object);

            // Act
            var result = (RedirectToRouteResult)controller.Create(contact);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void When_InvalidContact_Expect_BlankView()
        {
            // Arrange
            var contact = new Contact();
            _service.Expect(s => s.CreateContact(contact)).Returns(false);
            var controller = new ContactController(_service.Object);

            // Act
            var result = (ViewResult)controller.Create(contact);

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }

    }
}