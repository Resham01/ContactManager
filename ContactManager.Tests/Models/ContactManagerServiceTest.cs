using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContactManager.Tests.Models
{
    [TestClass]
    public class ContactManagerServiceTest
    {
        private Mock<IContactManagerRepository> _mockRepository;
        private ModelStateDictionary _modelState;
        private IContactManagerService _service;

        [TestInitialize]
        public  void Initialize()
        {
            _mockRepository = new Mock<IContactManagerRepository>();
            _modelState = new ModelStateDictionary();
            _service = new ContactManagerService(new ModelStateWrapper(_modelState), _mockRepository.Object);
        }

        [TestMethod]
        public void When_Normal_Contact_Expect_Valid_Object()
        {
            // Arrange
            var contact = new Contact { Id = -1, FIRST_NAME = "Steve", LAST_NAME = "Walther", PHONE = "555-5555", EMAIL = "steve@somewhere.com" };

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_FirstNameMNissing_Expect_Name_Validatio_nError()
        {
            // Arrange
            var contact = new Contact { Id = -1, FIRST_NAME = string.Empty, LAST_NAME = "Walther", PHONE = "555-5555", EMAIL = "steve@somewhere.com" };

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["FirstName"].Errors[0];
            Assert.AreEqual("First name is required.", error.ErrorMessage);
        }

        [TestMethod]
        public void CreateContactRequiredLastName()
        {
            // Arrange
            var contact = new Contact { Id = -1, FIRST_NAME = "Steve",LAST_NAME = string.Empty , PHONE = "555-5555", EMAIL = "steve@somewhere.com" };

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["LastName"].Errors[0];
            Assert.AreEqual("Last name is required.", error.ErrorMessage);
        }

        [TestMethod]
        public void CreateContactInvalidPhone()
        {
            // Arrange
            var contact = new Contact { Id = -1, FIRST_NAME = "Steve", LAST_NAME = "Walther", PHONE = "apple", EMAIL = "steve@somewhere.com" };

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Phone"].Errors[0];
            Assert.AreEqual("Invalid phone number.", error.ErrorMessage);
        }

        [TestMethod]
        public void CreateContactInvalidEmail()
        {
            // Arrange
            var contact = new Contact { Id = -1, FIRST_NAME = "Steve", LAST_NAME = "Walther", PHONE = "555-5555", EMAIL = "apple" };

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Email"].Errors[0];
            Assert.AreEqual("Invalid email address.", error.ErrorMessage);
        }
    }
}