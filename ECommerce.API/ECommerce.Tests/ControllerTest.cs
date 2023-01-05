﻿using ECommerce.API.Controllers;
using ECommerce.API;
using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ECommerce.Tests
{
    public class ControllerTest : IClassFixture<DB>
    {
        private Context context;
        private readonly ITestOutputHelper _output;

        public ControllerTest(ITestOutputHelper output, DB db) {
            _output = output;
            context = db.context;
        }
        [Fact]
        public void LogIn_ValidUser() {
            var mockLog = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(context, mockLog.Object);
           User userExpected = new User(1,"first","last","email","password");
            UserDTO user = new UserDTO("email","password");
            var result = controller.Login(user).Result.Value;
            // _output.WriteLine("" + result);
            //Assert.Equal(userExpected, result);
            Assert.Equal(userExpected.UserId, result.UserId);
            Assert.Equal(userExpected.UserFirstName, result.UserFirstName);
            Assert.Equal(userExpected.UserLastName, result.UserLastName);
            Assert.Equal(userExpected.UserEmail, result.UserEmail);
            Assert.Equal(userExpected.UserPassword, result.UserPassword);
        }
        [Fact]
        public void LogIn_InValidUser()
        {
            var mockLog = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(context, mockLog.Object);

            UserDTO user = new UserDTO("", "");
            var result = controller.Login(user).Result;
            Assert.Null(result.Value);
        }
        
        [Fact]
        public void Register_InValid()
        {
            var mockLog = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(context, mockLog.Object);
            User user = new User(1, "first", "last", "email", "password");
            //UserDTO user = new UserDTO("not there", "not there");
            var result = controller.Register(user).Result;
            //Assert.Same(,result.statusCode);
            Assert.Equal("Microsoft.AspNetCore.Mvc.BadRequestResult", result.ToString());
        }

        [Fact]
        public void Logout_Valid()
        {
            var mockLog = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(context, mockLog.Object);
            
            var result = controller.Logout();
            //Assert.Same(,result.statusCode);
            Assert.Equal("Microsoft.AspNetCore.Mvc.OkResult", result.ToString());
        }
        
        [Fact]
        public void UserDTO_EmptyConstructor()
        {
            var mockLog = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(context, mockLog.Object);
            var userDTO = new UserDTO();
            userDTO.email = "email";
            userDTO.password = "password";
            var result = controller.Login(userDTO).Result.Value;
            //Assert.Same(,result.statusCode);
            Assert.Equal(1, result.UserId);
        }

        [Fact]
        public void GetOne_GetId()
        {
            var mockLog = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(context, mockLog.Object);

            var productExpected = new Product(1, "test", 1, 10, "test", "test");
            var result = controller.GetOne(1).Result;
            Assert.NotNull(result);
            
        }
        [Fact]
        public void GetOne_Invalid()
        {
            var mockLog = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(context, mockLog.Object);

            var productExpected = new Product(1, "test", 1, 10, "test", "test");
            var result = controller.GetOne(-1).Result.Value;
            //Assert.Equal(productExpected, result);
            Assert.Null(result);

        }
        [Fact]
        public void Get_All()
        {
            var mockLog = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(context, mockLog.Object);

            var productExpected = new Product(1, "test", 1, 10, "test", "test");
            var result = controller.GetAll().Result;
            //Assert.Equal(productExpected, result);
            Assert.NotNull(result);

        }
        [Fact]
        public void Purchase()
        {
            var mockLog = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(context, mockLog.Object);

            var productExpected = new Product(1, "test", 1, 10, "test", "test");
            //ProductDTO product = new ProductDTO();
            ProductDTO[] product = new ProductDTO[] {new ProductDTO(1, 1)};
            
            //product[0].id = 1;
            //product[0].quantity = 1;
            
            var result = controller.Purchase(product).Result;
            //Assert.Equal(productExpected, result);
            //Assert.Equal("",result.ToString()); ;
            Assert.NotNull(result);

        }
        [Fact]
        public void Purchase_ToMuch()
        {
            var mockLog = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(context, mockLog.Object);

            var productExpected = new Product(1, "test", 1, 10, "test", "test");
            //ProductDTO product = new ProductDTO();
            ProductDTO[] product = new ProductDTO[] { new ProductDTO(1, 5) };

            //product[0].id = 1;
            //product[0].quantity = 1;

            var result = controller.Purchase(product).Result;
            //Assert.Equal(productExpected, result);
            //Assert.Equal("",result.ToString()); ;
            Assert.NotNull(result);

        }
        [Fact]
        public void ProductDTO()
        {
            ProductDTO p = new ProductDTO();
            p.quantity = 1;
            p.id = 1;
            Assert.Equal(1, p.quantity);
        }
        [Fact]
        public void UserCreate()
        {
            User user = new User("first","last","email","password");

            Assert.Equal("first",user.UserFirstName);
            Assert.Equal("last", user.UserLastName);
            Assert.Equal("email", user.UserEmail);
            Assert.Equal("password", user.UserPassword);
        }

    }
}
