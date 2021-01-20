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
    class AlbumsControllerTests
    {
        private Mock<IAlbumService> _service;
        private AlbumsController _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var profile = new AlbumsProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var realMapper = new Mapper(configuration);

            _service = new Mock<IAlbumService>();
            _sut = new AlbumsController(_service.Object, realMapper);
        }
    }
}
