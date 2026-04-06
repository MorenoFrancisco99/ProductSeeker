namespace UnitTestProj;

using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using ProductSeeker;
using ProductSeeker.Data.Models;

public class ProductSpecValidatorTest
{
    private readonly FoodValidator _validator;
    private readonly IProductRepository _repoMock;
    private readonly UserManager<AppUser> _userManagerMock;

    public ProductSpecValidatorTest()
    {
        _repoMock = Substitute.For<IProductRepository>();
        _userManagerMock = Substitute.For<UserManager<AppUser>>(
            Substitute.For<IUserStore<AppUser>>(),
            null, null, null, null, null, null, null, null);

        _validator = new FoodValidator(_repoMock, _userManagerMock);

    }

    /*
        Standalone ProductSpecValidator cannot exist without the context of a specific product type
        So we need to use the FoodValidator, which is a concrete implementation of ProductSpecValidator, to test the validation rules defined in the ProdSPecVal class. 
        FoodValidator its instantiated just to be able to test the rules defined in the ProductSpecValidator, which are inherited by FoodValidator.
    */



    [Fact]
    public async Task Create_CoreDoesNotExist_ShouldHaveError()
    {
        var food = new FoodProductModel
        {
            EAN = "12345678",
            ProductCoreId = 99,
            Category = CategoriesEnum.ProductCategories.Food,
            NetContent = 500,
            UnitOfMeasure = UnitOfMeasureEnum.Unit.g,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            TACC = false
        };

        // El core no existe
        _repoMock.GetCoreByID(food.ProductCoreId).Returns((ProductCoreModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        var result = await _validator.TestValidateAsync(food);

        result.ShouldHaveValidationErrorFor(x => x.ProductCoreId)
              .WithErrorMessage("Target product does not exist");
    }



    [Fact]
    public async Task Create_CategoryMismatch_ShouldHaveError()
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
            TACC = false
        };

        // El core existe pero tiene otra categoría
        _repoMock.GetCoreByID(food.ProductCoreId).Returns(new ProductCoreModel
        {
            Id = 1,
            Category = CategoriesEnum.ProductCategories.Unknown,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            ProductName = "Agua",
            Brand = "MarcaAgua"
        });
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });


        var result = await _validator.TestValidateAsync(food);

        result.ShouldHaveValidationErrorFor(x => x.ProductCoreId);

        result.ShouldHaveValidationErrorFor(x => x.ProductCoreId)
              .WithErrorMessage("The category of the spec must match the category of the core. " +
                                "Core: Unknown, Spec: Food");
    }

    [Fact]
     public async Task Create_MissingRequiredFields_ShouldHaveErrors()
    {
        var food = new FoodProductModel(); // Todo vacío

        _repoMock.GetCoreByID(Arg.Any<int>()).Returns((ProductCoreModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        var result = await _validator.TestValidateAsync(food);

        // ProductSpecValidator errors
        result.ShouldHaveValidationErrorFor(x => x.Category);
        result.ShouldHaveValidationErrorFor(x => x.ProductCoreId);
        result.ShouldHaveValidationErrorFor(x => x.EAN);

    }


}