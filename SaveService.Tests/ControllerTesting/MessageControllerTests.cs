using Moq;
using SaveService.Resources.Api.Controllers;
using SaveService.Resources.Api.Models;
using SaveService.Resources.Api.Repository;
using System.Net;
using Xunit;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SaveService.Tests.ControllerTesting
{
    public class MessageControllerTests
    {
        Mock<IMessageRepo> _mock;

        public MessageControllerTests()
        {
            _mock = new Mock<IMessageRepo>();
        }

        [Fact]
        public void Get_ShouldReturnBadRequestIfIdIsWrong()
        {
            int id = -10;
            var expected = HttpStatusCode.BadRequest;
            var controller = new MessageController(_mock.Object);

            var actual = (HttpStatusCode)controller.Get(id).Result.Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Get_ShouldReturnNotFoundIfMessageNotFound()
        {
            int id = 500;
            string login = "first";
            var expected = HttpStatusCode.NotFound;
            _mock.Setup(m => m.GetAsync(id, login)).Throws<Exception>();
            var controller = new MessageController(_mock.Object);

            var actual = (HttpStatusCode)controller.Get(id).Result.Value;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Save_ShouldReturnBadRequestIfTextIsNullOrEmptyOrWhiteSpace(string text)
        {
            var expected = HttpStatusCode.BadRequest;
            var controller = new MessageController(_mock.Object);

            var actual = (HttpStatusCode)controller.Save(text).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Edit_ShouldReturnBadRequestIfIdIsWrong()
        {
            int id = -5;
            var expected = HttpStatusCode.BadRequest;
            var controller = new MessageController(_mock.Object);

            var actual = (HttpStatusCode)controller.Edit("test", id).Result;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Edit_ShouldReturnBadRequestIfTextIsNullOrEmptyOrWhiteSpace(string text)
        {
            int id = 1;
            var expected = HttpStatusCode.BadRequest;
            var controller = new MessageController(_mock.Object);

            var actual = (HttpStatusCode)controller.Edit(text, id).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Edit_ShouldReturnNotFoundIfMessageNotFound()
        {
            int id = 500;
            string text = "hello";
            string login = "admin";
            var expected = HttpStatusCode.NotFound;
            _mock.Setup(m => m.EditAsync(text, id, login)).Throws<Exception>();
            var controller = new MessageController(_mock.Object);

            var actual = (HttpStatusCode)controller.Edit(text, id).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete_ShouldReturnBadRequestIfIdIsWrong()
        {
            int id = -15;
            var expected = HttpStatusCode.BadRequest;
            var controller = new MessageController(_mock.Object);

            var actual = (HttpStatusCode)controller.Delete(id).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete_ShouldReturnNotFoundIfMessageNotFound()
        {
            int id = 500;
            string login = "admin";
            var expected = HttpStatusCode.NotFound;
            _mock.Setup(m => m.DeleteAsync(id, login)).Throws<NullReferenceException>();
            var controller = new MessageController(_mock.Object);

            var actual = (HttpStatusCode)controller.Delete(id).Result;

            Assert.Equal(expected, actual);
        }
    }
}
