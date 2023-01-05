using ECommerce.API.Controllers;
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
using System.Runtime.CompilerServices;

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
        /*[Fact]
        public void PutUser() {
            var mockLog = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(context, mockLog.Object);

            var user = new User(2,"test","test", "test", "test");
            //ProductDTO product = new ProductDTO();


            //product[0].id = 1;
            //product[0].quantity = 1;

            var result = controller.PutUser(user.UserId,user).Result;

            //Assert.Equal(productExpected, result);
            //Assert.Equal("",result.ToString()); ;
            Assert.Equal("",result.ToString());
            Assert.NotNull(result);

        }*/
        [Fact]
        public void restock() {
            var mockLog = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(context, mockLog.Object);

            var productExpected = new Product(1, "test", 1, 10, "test", "test");
            //ProductDTO product = new ProductDTO();
            ProductDTO[] product = new ProductDTO[] { new ProductDTO(1, 1) };

            //product[0].id = 1;
            //product[0].quantity = 1;

            var result = controller.Restock(product).Result;
            //Assert.Equal(productExpected, result);
            //Assert.Equal("",result.ToString()); ;
            Assert.NotNull(result);

        }
        [Fact]
        public void restock_error()
        {
            var mockLog = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(context, mockLog.Object);

            var productExpected = new Product(1, "test", 1, 10, "test", "test");
            //ProductDTO product = new ProductDTO();
            ProductDTO[] product = new ProductDTO[] { new ProductDTO(-12, -100) };

            //product[0].id = 1;
            //product[0].quantity = 1;

            var result = controller.Restock(product).Result;
            //Assert.Equal(productExpected, result);
            //Assert.Equal("",result.ToString()); ;
            Assert.NotNull(result);

        }
        
        /*[Fact]
        public void GetUser_SpecificUser() {
            var mockLog = new Mock<ILogger<UserController>>();
            var controller = new UserController(context,mockLog.Object);
            //var user = new User("first","last","email","password");
            //var productExpected = new Product(1, "test", 1, 10, "test", "test");

            var result = controller.GetUser(1).Result;
            Assert.NotNull(result);
        }
        [Fact]
        public void GetAll_Users() {
            var mockLog = new Mock<ILogger<UserController>>();
            var controller = new UserController(context, mockLog.Object);
            //var user = new User("test","test","test","test");
            //var productExpected = new Product(1, "test", 1, 10, "test", "test");
            var result = controller.GetAll().Result;
            //Assert.Equal(productExpected, result);
            Assert.NotNull(result);
        }
        /*[Fact]
        public void PostUser(){
            //Task SaveAsync() { return Task.FromResult("s"); }
            var mockLog = new Mock<ILogger<UserController>>();
            var controller = new UserController(context, mockLog.Object);
            var user = new User(33,"test", "test", "test", "test");
            var mockSave = new Mock<Context>();
            mockSave.Setup(m => m.SaveAsync()).Returns(Task.CompletedTask);
            //mockSave.Setup(m => m.SaveAsync());
            var result = controller.PostUser(user).Result;

            Assert.NotNull(result);
        }*/
        [Fact]
        public void FindEmail()
        {
            var mockLog = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(context, mockLog.Object);

            var result = controller.FindEmail("email").Result;

            Assert.Equal("True", result.ToString());
        }
        [Fact]
        public void FindEmail_invalid() {
            var mockLog = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(context, mockLog.Object);
            var user = new User(1, "test", "test", "test", "test");

            var result = controller.FindEmail(user.UserEmail).Result;

            Assert.Equal("False",result.ToString());
        }
        /*[Fact]
        public void FindPassword()
        {
            var mockLog = new Mock<ILogger<UserController>>();
            var controller = new UserController(context, mockLog.Object);

            var result = controller.FindPassword("password").Result;

            Assert.Equal("True", result.ToString());
        }
        [Fact]
        public void FindPassword_invalid()
        {
            var mockLog = new Mock<ILogger<UserController>>();
            var controller = new UserController(context, mockLog.Object);

            var result = controller.FindPassword("invalid").Result;

            Assert.Equal("False", result.ToString());
        }
        [Fact]
        public void PostUser() {
            var mockLog = new Mock<ILogger<UserController>>();
            var controller = new UserController(context, mockLog.Object);
            User u = new User(1, "first", "last", "email", "password");
            var result = controller.PostUser(u).Result;
            Assert.Equal("Microsoft.AspNetCore.Mvc.NoContentResult", result.ToString());

        }
        [Fact]
        public void PostUser_in()
        {
            var mockLog = new Mock<ILogger<UserController>>();
            var controller = new UserController(context, mockLog.Object);
            User u = new User(3, "d", "d", "d", "d");
            var mockCon = new Mock<IContext>();
            mockCon.Setup(m => m.SaveAsync()).Verifiable();
            
            var result = controller.PostUser(u).Result;
            Assert.Equal("Microsoft.AspNetCore.Mvc.NoContentResult", result.ToString());

        }*/
    }
}
