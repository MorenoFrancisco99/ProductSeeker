namespace UnitTestProj;

using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using ProductSeeker;
using ProductSeeker.Data.Models;

public class BaseEntityValidatorTest
{
    private readonly FoodValidator _validator;
    private readonly IProductRepository _repoMock;
    private readonly UserManager<AppUser> _userManagerMock;

    public BaseEntityValidatorTest()
    {
        _repoMock = Substitute.For<IProductRepository>();
        _userManagerMock = Substitute.For<UserManager<AppUser>>(
            Substitute.For<IUserStore<AppUser>>(),
            null, null, null, null, null, null, null, null);
        _validator = new FoodValidator(_repoMock, _userManagerMock);
        // Using FoodValidator to test the rules defined in BaseEntityValidator
    }

    [Fact]
    public async Task Create_MissingRequiredFields_ShouldHaveErrors()
    {
        var food = new FoodProductModel(); // Todo vacío

        _repoMock.GetCoreByID(Arg.Any<int>()).Returns((ProductCoreModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        var result = await _validator.TestValidateAsync(food);

        //BaseEntityValidator errors
        result.ShouldHaveValidationErrorFor(x => x.IsActive); 
        result.ShouldHaveValidationErrorFor(x => x.CreationSource);
        result.ShouldHaveValidationErrorFor(x => x.IdCreator);
    }


    [Fact]
    public async Task Create_InvalidCreationSource_ShouldHaveError()
    {
        var food = new FoodProductModel
        {
            EAN = "12345678",
            ProductCoreId = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            NetContent = 500,
            UnitOfMeasure = UnitOfMeasureEnum.Unit.g,
            CreationSource = (CreationSourceEnum.CreationSource)999 // Invalid value
        };

        _repoMock.GetCoreByID(Arg.Any<int>()).Returns((ProductCoreModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        var result = await _validator.TestValidateAsync(food);

        result.ShouldHaveValidationErrorFor(x => x.CreationSource).WithErrorMessage("Creation source must be a valid value");
    }

    [Fact]
    public async Task Create_UnknownCreationSource_ShouldHaveError()
    {
        var food = new FoodProductModel
        {
            EAN = "12345678",
            ProductCoreId = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            NetContent = 500,
            UnitOfMeasure = UnitOfMeasureEnum.Unit.g,
            CreationSource = CreationSourceEnum.CreationSource.Unknown 
        };

        _repoMock.GetCoreByID(Arg.Any<int>()).Returns((ProductCoreModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        var result = await _validator.TestValidateAsync(food);

        result.ShouldHaveValidationErrorFor(x => x.CreationSource).WithErrorMessage("Creation source cannot be unknown");
    }

    [Fact]
    public async Task Create_NonExistentCreator_ShouldHaveError()
    {
        var food = new FoodProductModel
        {
            EAN = "12345678",
            ProductCoreId = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            NetContent = 500,
            UnitOfMeasure = UnitOfMeasureEnum.Unit.g,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "nonexistentuser"
        };

        _repoMock.GetCoreByID(Arg.Any<int>()).Returns((ProductCoreModel?)null);
        _userManagerMock.FindByIdAsync("nonexistentuser").Returns((AppUser?)null); // User does not exist

        var result = await _validator.TestValidateAsync(food);

        result.ShouldHaveValidationErrorFor(x => x.IdCreator).WithErrorMessage("Creator ID must correspond to an existing user");
    }


}

