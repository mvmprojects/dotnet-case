using AutoMapper;
using dotnet_case.API.Controllers;
using dotnet_case.API.Dtos;
using dotnet_case.API.Profiles;
using dotnet_case.BL.Services;
using dotnet_case.DATA.Repositories;
using dotnet_case.DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_case.TEST
{
    class TracksControllerTests
    {
        private Mock<ITrackService> mockService;
        private TracksController _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var profile = new TracksProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var realMapper = new Mapper(configuration);

            mockService = new Mock<ITrackService>();
            _sut = new TracksController(mockService.Object, realMapper);
        }

        [Test]
        public void FindTracksByAlbumId_ShouldReturnContent()
        {
            mockService.Setup(x => x.FindTracksByAlbumId(1))
                .Returns(new List<TrackModel> { });

            ActionResult<IEnumerable<TrackDto>> action = _sut.FindTracksByAlbumId(1);

            Assert.That(action, Is.Not.Null);
        }

        [Test]
        public void Create_ShouldCallService()
        {
            mockService.Setup(x => x.CreateTrack(new TrackModel() { }));

            _sut.Create(new TrackDto() { }, 1);

            //Mock.Get(mockService).Verify(x => 
            //    x.CreateTrack(It.IsAny<TrackModel>()), Times.Once);

            mockService.Verify(x =>
                x.CreateTrack(It.IsAny<TrackModel>()), Times.Once);
        }

        [Test]
        public void Delete_ShouldCallService()
        {
            mockService.Setup(x => x.DeleteTrack(new TrackModel() { }));
            mockService.Setup(x => x.GetTrack(1)).Returns(new TrackModel() { });

            //_sut.Delete(new TrackDto() { });
            _sut.Delete(1);

            mockService.Verify(x =>
                x.DeleteTrack(It.IsAny<TrackModel>()), Times.Once);
        }

        [Test]
        public void Update_ShouldCallService()
        {
            mockService.Setup(x => x.UpdateTrack(new TrackModel() { }));

            _sut.Update(new TrackDto() { }, 1);

            mockService.Verify(x =>
                x.UpdateTrack(It.IsAny<TrackModel>()), Times.Once);
        }
    }
}
