using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Moq;
using MyOpenBanking.Application.Services;
using MyOpenBanking.Application.Services.Interface;
using MyOpenBanking.Domain.Entities;
using MyOpenBanking.Domain.Repositories;
using NUnit.Framework;
using System.Collections.Generic;

namespace MyOpenBanking.Application.Tests
{
    public class UserServiceTest
    {
        private IUserService _userService;
        [SetUp]
        public void Setup()
        {
            var userRepository = new Mock<IUserRepository>();
            var configuration = new Mock<IConfiguration>();

            configuration.Setup(c => c.GetSection("JwtKey"))
                .Returns(() =>
                {
                    var listProvider = new List<IConfigurationProvider>();
                    IConfigurationProvider item = new MyConfigurationProvider();
                    listProvider.Add(item);
                    var root = new ConfigurationRoot(listProvider);
                    var section = new ConfigurationSection(root, "JwtKey");
                    section["JwtKey"] = "911eff352b5ab8cf83ed16cfb7ed2167";
                    return section;
                });

            userRepository.Setup(u => u.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(() => {
                    var user = new User(ObjectId.GenerateNewId(), "fulano_test", "fulano_test@mail.com", "12345");
                    return user;
                });

            userRepository.Setup(u => u.GetByUserAndPasswordAsync("fulano", "12345"))
                .ReturnsAsync(() =>
                {
                    var user = new User(ObjectId.GenerateNewId(), "fulano_test", "fulano_test@mail.com", "12345");
                    return user;
                });

            _userService = new UserService(userRepository.Object, configuration.Object, null, null);
        }

        [Test]
        public void TestGetUser()
        {
            var task = _userService.GetUser(ObjectId.GenerateNewId().ToString());
            var user = task.Result;
            Assert.IsNotEmpty(user.Id);
        }

        [Test]
        public void TestAuthenticate()
        {
            var task =  _userService.Authenticate("fulano", "12345");
            var token = task.Result;
            Assert.IsNotEmpty(token);
        }

        public class MyConfigurationProvider : ConfigurationProvider
        {

        }
    }
}