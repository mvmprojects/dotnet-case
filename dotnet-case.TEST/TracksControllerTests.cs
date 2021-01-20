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
        private Mock<ITrackService> _service;
        private TracksController _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var profile = new AlbumsProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var realMapper = new Mapper(configuration);

            _service = new Mock<ITrackService>();
            _sut = new TracksController(_service.Object, realMapper);
        }

        [Test]
        public void FindTracksByAlbumId_ShouldReturnContent()
        {
            _service.Setup(x => x.FindTracksByAlbumId(1))
                .Returns(new List<TrackModel> { });

            ActionResult<IEnumerable<TrackDto>> action = _sut.FindTracksByAlbumId(1);

            Assert.That(action, Is.Not.Null);
        }
    }
}
