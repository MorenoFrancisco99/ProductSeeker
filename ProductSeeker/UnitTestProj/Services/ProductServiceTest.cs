
namespace UnitTestProj.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using ProductSeeker;
using ProductSeeker.Data.Models;

public class ProductServiceTest
{
    private readonly IProductRepository _mockProductRepo;
    private readonly IStoreRepository _mockStoreRepo;
    private readonly IValidator<ProductCoreModel> _mockCoreValidator;
    private readonly IServiceProvider _serviceProvider;

    //IServiceProvider dependencies
    private readonly UserManager<AppUser> _userManagerMock;



    public ProductServiceTest()
    {
        _mockProductRepo = Substitute.For<IProductRepository>();
        _mockStoreRepo = Substitute.For<IStoreRepository>();
        _mockCoreValidator = Substitute.For<IValidator<ProductCoreModel>>();

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

        //Due to reflection being used to get the validators for the specs, 
        // we need to set up a service provider with the validators and their dependencies 
        // in order to test the GetValidatorForSpec method and the CreateProductSpec method, 
        // which uses it internally to get the appropriate validator for the spec in runtime.
        _serviceProvider = new ServiceCollection()
            .AddSingleton(_mockProductRepo)
            .AddSingleton(_userManagerMock)
            .AddValidatorsFromAssemblyContaining<FoodValidator>()
            .BuildServiceProvider(); 

    }


    //NOTE: this method is private in the actual service, but I made it public here just for testing purposes. You can test it indirectly through the CreateProductSpec method, which uses it internally to get the appropriate validator for the spec being created.
    // [Fact]
    //  public async Task GetValidatorForSpec_FoodCategory_ReturnsFoodValidator()
    //  {
    //      // Arrange
    //      var productService = new ProductService(_mockProductRepo, _mockStoreRepo, _serviceProvider, _mockCoreValidator);
    //      var foodSpec = new FoodProductModel
    //      {
    //          Category = CategoriesEnum.ProductCategories.Food
    //      };
    //     // Act
    //      var validator = productService.GetValidatorForSpec(foodSpec);
    //     // Assert
    //      Assert.IsType<FoodValidator>(validator);
    //  }

/*----------------CreateProductCore Tests-----------------*/

    [Fact]
    public async Task CreateProductCore_ValidData_ReturnSuccess()
    {
        var productService = new ProductService(_mockProductRepo, _mockStoreRepo, _serviceProvider, _mockCoreValidator);
        
    }


    [Fact]
    public async Task CreateProductSpec_ValidData_ReturnsSuccess()
    {
       
        // Arrange
        var productService = new ProductService(_mockProductRepo, _mockStoreRepo, _serviceProvider, _mockCoreValidator);
        
        var dto = new POSTFoodProductDTO
        {
            EAN = "12345678",
            ProductCoreId = 1,
            NetContent = 500,
            UnitOfMeasure = UnitOfMeasureEnum.Unit.g,
            TACC = false
        };
        string userID = "user123";
        var returnedSpec = new FoodProductModel
        {
            EAN = "12345678",
            ProductCoreId = 1,
            Category = CategoriesEnum.ProductCategories.Food,
            NetContent = 500,
            UnitOfMeasure = UnitOfMeasureEnum.Unit.g,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "1",
            TACC = false,
            IsActive = true
        };

        //mock validator repo dependencies for the validator to work properly and return a successful validation result

        _mockProductRepo.GetCoreByID(1).Returns(new ProductCoreModel
        {
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123",
            ProductName = "Arroz",
            Brand = "Gallo",
            Id = 1,
            Category = CategoriesEnum.ProductCategories.Food
        });
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" }); 

        _mockProductRepo.CreateSpec(Arg.Any<ProductSpecModel>()).Returns(returnedSpec);

        // Act
        var result = await productService.CreateProductSpec(dto, userID);

        // Assert
        Assert.True(result.IsSuccess);
    }




   
}