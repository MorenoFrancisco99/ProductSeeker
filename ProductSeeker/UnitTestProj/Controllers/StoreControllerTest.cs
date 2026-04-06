namespace UnitTestProj;

using FluentAssertions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ProductSeeker;
using ProductSeeker.Controllers;
using ProductSeeker.Data.Models;

public class StoreControllerTest
{


    //GET: api/store/core/id
    [Fact]
    public async Task GetCoreByID_ReturnsOk_WhenCoreExists()
    {
        int coreId = 1;
        string userId = "test-user-id";
        var _storeService = Substitute.For<IStoreService>();
        _storeService.GetCoreByID(coreId, userId).Returns(new StoreCoreModel { Id = coreId, Name = "Test Store", IdCreator = userId, Field = "Test Field", CreationSource = CreationSourceEnum.CreationSource.Scrapped });
        var controller = new StoreController(_storeService, null!)
        {
            //Simular usuario
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                        [
                            new Claim(ClaimTypes.NameIdentifier, userId)
                        ]))
                }
            }
        };

        var result = await controller.GetCoreByID(coreId);
        var okResult = result.Result as OkObjectResult; //if the cast fail it should return null
        okResult.Should().NotBeNull();
        var storeCore = okResult.Value as StoreCoreModel;
        storeCore.Should().NotBeNull();
        storeCore.Id.Should().Be(coreId);
        storeCore.Name.Should().Be("Test Store");
        storeCore.IdCreator.Should().Be(userId);
        storeCore.Field.Should().Be("Test Field");
    }

    [Fact]
    public async Task GetCoreByID_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        int coreId = 1;
        var _storeService = Substitute.For<IStoreService>();
        var controller = new StoreController(_storeService, null!)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity()) // No claims, so no user ID
                }
            }
        };

        var result = await controller.GetCoreByID(coreId);

        result.Result.Should().BeOfType<UnauthorizedResult>();
        result.Result.As<UnauthorizedResult>().StatusCode.Should().Be(401);
    }

    [Fact]
    public async Task GetCoreByID_ReturnsNotFound_WhenCoreDoesNotExist()
    {
        int coreId = 1;
        string userId = "test-user-id";
        var _storeService = Substitute.For<IStoreService>();
        _storeService.GetCoreByID(coreId, userId).Returns(Errors.StoreCoreNotFound);
        var controller = new StoreController(_storeService, null!)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                        [
                            new Claim(ClaimTypes.NameIdentifier, userId)
                        ]))
                }
            }
        };

        var result = await controller.GetCoreByID(coreId);
        var notFoundResult = result.Result as NotFoundObjectResult; //if the cast fail it should return null
        notFoundResult.Should().NotBeNull();
        notFoundResult.StatusCode.Should().Be(404);
        //should contain, at least in part, the string: "Store core not found. "
        notFoundResult.Value.ToString().Should().Contain("Store core not found. ");
    }

    [Fact]
    public async Task GetCoreByID_ReturnsForbidden_WhenUserDoesNotOwnResource()
    {
        var _storeService = Substitute.For<IStoreService>();
        int coreId = 1;
        string userId = "test-user-id";
        _storeService.GetCoreByID(coreId, userId).Returns(Errors.UnauthorizedAccess);
        var controller = new StoreController(_storeService, null!)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                        [
                            new Claim(ClaimTypes.NameIdentifier, userId)
                        ]))
                }
            }
        };

        var result = await controller.GetCoreByID(coreId);
        var forbiddenResult = result.Result as ObjectResult; //if the cast fail it should return null
        forbiddenResult.Should().NotBeNull();
        forbiddenResult.StatusCode.Should().Be(403);
        forbiddenResult.Value.ToString().Should().Contain("Unauthorized access. ");
    }





    //POST: api/store
    [Fact]
    public async Task CreateStoreWSpec_ReturnsOk_WhenCreationIsSuccessful()
    {
        var _storeService = Substitute.For<IStoreService>();
        var storeDTO = new StoreWSpecDTO
        {
            Name = "Test Store",
            Field = "Test Field",
            BusinessDays = "Mon-Fri",
            GeoLocation = "Test Location",
            ValidFrom = DateTime.UtcNow,
            ValidTo = DateTime.UtcNow.AddYears(1)
        };

        string userId = "test-user-id";
        var createdStoreCore = new StoreCoreModel
        {
            Id = 1,
            Name = storeDTO.Name,
            Field = storeDTO.Field,
            IdCreator = userId,
            CreationSource = CreationSourceEnum.CreationSource.Scrapped,
            StoreSpecs = new List<StoreSpecModel>
            { 
                new StoreSpecModel 
                {
                        BusinessDays = storeDTO.BusinessDays,
                        GeoLocation = storeDTO.GeoLocation,
                        ValidFrom = (DateTime) storeDTO.ValidFrom,
                        ValidTo = (DateTime) storeDTO.ValidTo,
                        IdCreator = userId,
                        CreationSource = CreationSourceEnum.CreationSource.Scrapped
                } 
            }
        };

        _storeService.CreateStoreWSpec(storeDTO, userId).Returns(createdStoreCore);
        var controller = new StoreController(_storeService, null!)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                        [
                            new Claim(ClaimTypes.NameIdentifier, userId)
                        ]))
                }
            }
        };

        var result = await controller.POSTStoreWSPec(storeDTO);


        var createdAtActionResult = result.Result as CreatedAtActionResult; //if the cast fail it should return null
        createdAtActionResult.Should().NotBeNull();
        createdAtActionResult.StatusCode.Should().Be(201);
        createdAtActionResult.Value.Should().BeEquivalentTo(createdStoreCore);
    }


    
}
