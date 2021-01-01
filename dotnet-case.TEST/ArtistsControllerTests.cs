using dotnet_case.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using dotnet_case.DATA.Repositories;
using dotnet_case.API.Dtos;
using AutoMapper;
using dotnet_case.DOMAIN.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using dotnet_case.API.Profiles;
using dotnet_case.BL.Services;

namespace dotnet_case.TEST
{
    public class ArtistsControllerTests
    {
        //private Mock<ICaseRepository> _repo;
        private Mock<IArtistService> _service;
        private ArtistsController _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var profile = new ArtistsProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var realMapper = new Mapper(configuration);

            //_repo = new Mock<ICaseRepository>();
            _service = new Mock<IArtistService>();
            _sut = new ArtistsController(_service.Object, realMapper);
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetListWithCountAsync_ShouldReturnContent()
        {
            // Arrange
            // NOTE: This mapper mock method didn't help much
            //_mapper.Setup(m => m.Map<List<ArtistDto>>(It.IsAny<List>()))
            //    .Returns(new List<ArtistDto>() { new ArtistDto { } });

            _service.Setup(r => r.GetArtistsAsync())
                .Returns(() =>
                {
                    return Task.FromResult(
                        new List<ArtistModel>() { new ArtistModel() { } }
                        );
                });

            // Act
            var actionTask = _sut.GetListWithCountAsync();
            actionTask.Wait();

            //// Assert
            Assert.That(actionTask.Result, Is.Not.Null);
        }

        [Test]
        public void FindArtistByName_ShouldReturnContent()
        {
            // Arrange
            // have the mock return something when the controller calls it
            _service.Setup(x => x.FindArtistByName("MJ"))
                .Returns(new ArtistModel { Name = "MJ" });

            // Act
            IActionResult actionResult = _sut.FindArtistByName("MJ");

            // Assert
            //Assert.IsNotNull(contentResult);
            //Assert.IsInstanceOf(typeof(OkObjectResult), contentResult);
            Assert.That(actionResult, Is.Not.Null);
        }
    }
}