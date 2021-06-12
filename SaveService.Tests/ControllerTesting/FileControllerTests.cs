using System;
using System.Collections.Generic;
using System.Text;
using SaveService.Repository;
using Xunit;
using Moq;
using System.Net;
using SaveService.Controllers;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace SaveService.Tests.ControllerTesting
{
    public class FileControllerTests
    {
        Mock<IFileRepo> _mock;

        public FileControllerTests()
        {
            _mock = new Mock<IFileRepo>();
        }

        [Fact]
        public void Get_ShouldReturnBadRequestIfIdIsWrong()
        {
            int id = -10;
            var expected = HttpStatusCode.BadRequest;
            var controller = new FileController(_mock.Object);

            var actual = (HttpStatusCode)controller.Get(id).Result.Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Get_ShouldReturnNotFoundIfFileNotFound()
        {
            int id = 500;
            string login = "first";
            var expected = HttpStatusCode.NotFound;
            _mock.Setup(m => m.GetAsync(id, login)).Throws<Exception>();
            var controller = new FileController(_mock.Object);

            var actual = (HttpStatusCode)controller.Get(id).Result.Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Save_ShouldReturnBadRequestIfFileIsNull()
        {
            IFormFile fileToSave = null;
            var expected = HttpStatusCode.BadRequest;
            var controller = new FileController(_mock.Object);

            var actual = (HttpStatusCode)controller.Save(fileToSave).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Edit_ShouldReturnBadRequestIfIdIsWrong()
        {
            int id = -5;
            byte[] buffer = { 1, 2 };
            IFormFile fileToSave = new FormFile(new MemoryStream(buffer), 1, 1, "test", "test");
            var expected = HttpStatusCode.BadRequest;
            var controller = new FileController(_mock.Object);

            var actual = (HttpStatusCode)controller.Edit(fileToSave, id).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Edit_ShouldReturnBadRequestIfFileIsNull()
        {
            int id = 5;
            IFormFile fileToSave = null;
            var expected = HttpStatusCode.BadRequest;
            var controller = new FileController(_mock.Object);

            var actual = (HttpStatusCode)controller.Edit(fileToSave, id).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Edit_ShouldReturnNotFoundIfFileNotFound()
        {
            int id = 500;
            byte[] buffer = {1, 2};
            IFormFile fileToSave = new FormFile(new MemoryStream(buffer), 1, 1, "test", "test");
            string login = "admin";
            var expected = HttpStatusCode.NotFound;
            _mock.Setup(m => m.EditAsync(fileToSave, id, login)).Throws<Exception>();
            var controller = new FileController(_mock.Object);

            var actual = (HttpStatusCode)controller.Edit(fileToSave, id).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete_ShouldReturnBadRequestIfIdIsWrong()
        {
            int id = -15;
            var expected = HttpStatusCode.BadRequest;
            var controller = new FileController(_mock.Object);

            var actual = (HttpStatusCode)controller.Delete(id).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete_ShouldReturnNotFoundIfFileNotFound()
        {
            int id = 500;
            string login = "admin";
            var expected = HttpStatusCode.NotFound;
            _mock.Setup(m => m.DeleteAsync(id, login)).Throws<Exception>();
            var controller = new FileController(_mock.Object);

            var actual = (HttpStatusCode)controller.Delete(id).Result;

            Assert.Equal(expected, actual);
        }
    }
}
