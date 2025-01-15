using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using BusinessLogicLayerTest.MockRepositorys;
using System;

namespace BusinessLogicLayerTest
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _userService;
        private FakeUserRepository _fakeUserRepository;

        [TestInitialize]
        public void Setup()
        {
            _fakeUserRepository = new FakeUserRepository();
            _userService = new UserService(_fakeUserRepository);
        }

        [TestMethod]
        public void Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            string username = "testuser1";
            string password = "Password1";

            // Act
            var user = _userService.Login(username, password);

            // Assert
            Assert.IsNotNull(user, "Gebruiker mag niet null zijn.");
            Assert.AreEqual(username, user.Username, "De gebruikersnaam komt niet overeen.");
        }

        [TestMethod]
        public void Login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            string username = "testuser1";
            string password = "WrongPassword";

            // Act
            var user = _userService.Login(username, password);

            // Assert
            Assert.IsNull(user, "Gebruiker zou null moeten zijn bij ongeldige inloggegevens.");
        }

        [TestMethod]
        public void Register_NewUser_AddsUser()
        {
            // Arrange
            string username = "newuser";
            string password = "Password123";

            // Act
            bool result = _userService.Register(username, password);

            // Assert
            Assert.IsTrue(result, "Gebruiker had succesvol toegevoegd moeten worden.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_ExistingUser_ThrowsException()
        {
            // Arrange
            string username = "testuser1";
            string password = "Password1";

            // Act
            _userService.Register(username, password);

            // Assert wordt hier niet bereikt, omdat een exceptie wordt verwacht.
        }
    }
}
