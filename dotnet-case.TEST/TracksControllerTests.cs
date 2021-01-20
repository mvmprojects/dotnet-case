using AutoMapper;
using dotnet_case.API.Controllers;
using dotnet_case.API.Profiles;
using dotnet_case.BL.Services;
using dotnet_case.DATA.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_case.TEST
{
    class TracksControllerTests
    {
        //private Mock<ICaseRepository> _repo;
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
    }
}
