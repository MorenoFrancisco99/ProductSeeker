namespace UnitTestProj;

using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using ProductSeeker;
using ProductSeeker.Data.Models;

public class FoodValidatorTests
{
    private readonly IProductRepository _repoMock;
    private readonly FoodValidator _validator;
    private readonly UserManager<AppUser> _userManagerMock;

    public FoodValidatorTests()
    {
        _repoMock = Substitute.For<IProductRepository>();

        var storeMock = Substitute.For<IUserStore<AppUser>>();
        _userManagerMock = Substitute.For<UserManager<AppUser>>(
            storeMock,  // IUserStore<AppUser> — requerido
            null,       // IOptions<IdentityOptions>
            null,       // IPasswordHasher<AppUser>
            null,       // IEnumerable<IUserValidator<AppUser>>
            null,       // IEnumerable<IPasswordValidator<AppUser>>
            null,       // ILookupNormalizer
            null,       // IdentityErrorDescriber
            null,       // IServiceProvider
            null        // ILogger<UserManager<AppUser>>
        );

        _validator = new FoodValidator(_repoMock, _userManagerMock);
        /*
       Validator internally uses productrepo and usermanager to validate the existence of the core and the user.
       Along with validating the equality of the category of the core with the product's category, these are all required validations for the validator to work properly.
       So you need to set up the mocks to return appropriate values for those dependencies in order to test the validator correctly.
   */
    }





    [Fact]
    public async Task Create_ValidFood_ShouldNotHaveErrors()
    {
        var food = new FoodProductModel
        {
            EAN = "12345678",
            ProductCoreId = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            NetContent = 500,
            UnitOfMeasure = UnitOfMeasureEnum.Unit.g,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            TACC = false,
            IsActive = true
        };

        _repoMock.GetCoreByID(1).Returns(new ProductCoreModel
        {
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            ProductName = "Arroz",
            Brand = "Gallo",
            Id = 1,
            Category = CategoriesEnum.ProductCategories.Food
        });

        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });


        var result = await _validator.TestValidateAsync(food);

        result.ShouldNotHaveAnyValidationErrors();
    }



    [Fact]
    public async Task Create_MissingRequiredFields_ShouldHaveErrors()
    {
        var food = new FoodProductModel(); // Todo vacío

        _repoMock.GetCoreByID(Arg.Any<int>()).Returns((ProductCoreModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        var result = await _validator.TestValidateAsync(food);

        //FoodValidator errors

        result.ShouldHaveValidationErrorFor(x => x.NetContent);

        // UoM  cannot be null. As an enum, it will default to Unknown
        //So it will never be null, but it can be an invalid value (Unknown or any value not defined in the enum).
        //It is managed separately in the tests for invalid and unknown values
        //result.ShouldHaveValidationErrorFor(x => x.UnitOfMeasure); 

    }


    [Fact]
    public async Task Create_NetContentZero_ShouldHaveError()
    {
        var food = new FoodProductModel
        {
            EAN = "12345678",
            ProductCoreId = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            NetContent = 0, // Valor no válido
            UnitOfMeasure = UnitOfMeasureEnum.Unit.g,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            TACC = false
        };

        _repoMock.GetCoreByID(food.ProductCoreId).Returns(new ProductCoreModel
        {
            Id = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            ProductName = "Agua",
            Brand = "MarcaAgua"
        });
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        var result = await _validator.TestValidateAsync(food);

        result.ShouldHaveValidationErrorFor(x => x.NetContent)
              .WithErrorMessage("Net content must be greater than zero and less than or equal to 10000");
    }


    [Fact]
    public async Task Create_InvalidUnitOfMeasure_ShouldHaveError()
    {
        var food = new FoodProductModel
        {
            EAN = "12345678",
            ProductCoreId = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            NetContent = 500,
            UnitOfMeasure = (UnitOfMeasureEnum.Unit)999, // Valor no válido
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            TACC = false
        };

        _repoMock.GetCoreByID(food.ProductCoreId).Returns(new ProductCoreModel
        {
            Id = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            ProductName = "Leche",
            Brand = "MarcaLeche"
        });
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        var result = await _validator.TestValidateAsync(food);

        result.ShouldHaveValidationErrorFor(x => x.UnitOfMeasure)
              .WithErrorMessage("Unit of measure must be a valid value");
    }

    [Fact]
    public async Task Create_UnknownUnitOfMeasure_ShouldHaveError()
    {
        var food = new FoodProductModel
        {
            EAN = "12345678",
            ProductCoreId = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            NetContent = 500,
            UnitOfMeasure = UnitOfMeasureEnum.Unit.Unknown,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            TACC = false
        };

        _repoMock.GetCoreByID(food.ProductCoreId).Returns(new ProductCoreModel
        {
            Id = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            ProductName = "Leche",
            Brand = "MarcaLeche"
        });
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        var result = await _validator.TestValidateAsync(food);

        result.ShouldHaveValidationErrorFor(x => x.UnitOfMeasure)
              .WithErrorMessage("Unit of measure cannot be Unknown");
    }
}