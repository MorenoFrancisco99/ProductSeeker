
namespace UnitTestProj.Services;

using System.Linq.Expressions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using ProductSeeker;
using ProductSeeker.Data.Models;
using static CategoriesEnum;
using static ProductSeeker.UnitOfMeasureEnum;
public class ProductServiceTest
{
    private readonly IProductRepository _mockProductRepo;
    private readonly IStoreRepository _mockStoreRepo;
    private readonly IServiceProvider _serviceProvider;

    private readonly IProductService _productService;

    //IServiceProvider dependencies
    private readonly UserManager<AppUser> _userManagerMock;



    public ProductServiceTest()
    {
        _mockProductRepo = Substitute.For<IProductRepository>();
        _mockStoreRepo = Substitute.For<IStoreRepository>();

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


        _productService = new ProductService(_mockProductRepo, _mockStoreRepo, _serviceProvider, new ProductCoreValidator(_userManagerMock));
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
        var core = new POSTProductCoreDTO
        {
            ProductName = "Gaseosa",
            Brand = "CocaCola",
            Category = CategoriesEnum.ProductCategories.Food
        };
        var UserID = "user123";

        _userManagerMock.FindByIdAsync(UserID).Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        _mockProductRepo.CreateCore(Arg.Any<ProductCoreModel>()).Returns(new ProductCoreModel());

        var result = await _productService.CreateProductCore(core, UserID);

        Assert.True(result.IsSuccess);
    }


    [Fact]
    public async Task CreateProductCore_InvalidData_ReturnFailure()
    {
        //Invalid data should trigger validation error. We dont really care about the specific error
        // just how its handled

        var core = new POSTProductCoreDTO
        {
            ProductName = "", // Invalid: empty product name
            Brand = "CocaCola",
            Category = CategoriesEnum.ProductCategories.Food
        };
        var UserID = "user123";

        _userManagerMock.FindByIdAsync(UserID).Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation = "TestLocation" });

        var result = await _productService.CreateProductCore(core, UserID);

        Assert.False(result.IsSuccess);
    }

    /*----------------CreateProductSpec Tests-----------------*/

    [Fact]
    public async Task CreateProductSpec_ValidData_ReturnsSuccess()
    {

        // Arrange

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
        var result = await _productService.CreateProductSpec(dto, userID);

        // Assert
        Assert.True(result.IsSuccess);
    }


    [Fact]
    public async Task CreateProductSpec_InvalidData_ReturnsFailure()
    {
        //Invalid data should trigger validation error. We dont really care about the specific error
        // just how its handled
        // Arrange

        var dto = new POSTFoodProductDTO
        {
            EAN = "12345678",
            ProductCoreId = 1,
            NetContent = -500, // Invalid: negative net content
            UnitOfMeasure = UnitOfMeasureEnum.Unit.g,
            TACC = false
        };
        string userID = "user123";

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

        // Act
        var result = await _productService.CreateProductSpec(dto, userID);

        // Assert
        Assert.False(result.IsSuccess);
    }


    //----------------Create ProductWSpec Test----------------------------

    private POSTFoodProductWCoreDTO BuildValidFoodDto(string name = "Milk", string brand = "BrandX")
    {
        // Ajustar nombres de propiedades según la definición real del DTO
        return new POSTFoodProductWCoreDTO
        {
            ProductName = name,
            Brand = brand,
            NetContent = 1.5F,
            UnitOfMeasure = Unit.g,
            EAN = "123"
        };
    }

    private POSTFoodProductWCoreDTO BuildInvalidFoodDto()
    {
        // Vacío/incompleto a propósito para forzar fallos de validación de spec
        return new POSTFoodProductWCoreDTO
        {
            ProductName = "Milk",
            Brand = "BrandX",
            // NetContent,UnitOfMeasure y EAN quedan sin setear
        };
    }
    private ProductCoreModel BuildValidCore()
    {
        return new ProductCoreModel
        {
            Id = 1,
            ProductName = "Milk",
            Brand = "BrandX",
            Category = ProductCategories.Food,
            IsActive = true,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123"
        };
    }
    private FoodProductModel BuildValidSpec(int coreId = 1)
    {
        return new FoodProductModel
        {
            Id = 1,
            ProductCoreId = coreId,
            EAN = "123",
            NetContent = 1.5F,
            UnitOfMeasure = Unit.g,
            TACC = false,
            Category = ProductCategories.Food,
            IsActive = true,
            CreationSource = CreationSourceEnum.CreationSource.User,
            IdCreator = "user123"
        };
    }

    [Fact]
    public async Task CreateProductCoreWSpec_CoreAndSpecAlreadyExist_ReturnsDuplicateError()
    {
        var dto = BuildValidFoodDto();
        var existingCore = BuildValidCore();
        var existingSpec =BuildValidSpec();

        _mockProductRepo.FindCore(dto.ProductName, dto.Brand).Returns(existingCore);
        _mockProductRepo.GetSpecByPredicate(existingCore.Id, Arg.Any<Expression<Func<FoodProductModel, bool>>>())
            .Returns(existingSpec);

        var result = await _productService.CreateProductCoreWSpec(dto, "user123");
        
        existingCore.AddSpec(existingSpec);
        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.Duplicate.Id, result.Error.Id); 
        Assert.Equal(existingCore, result.Error.Metadata["Resource"]);

        await _mockProductRepo.DidNotReceive().CreateCore(Arg.Any<ProductCoreModel>());
        await _mockProductRepo.DidNotReceive().CreateSpec(Arg.Any<ProductSpecModel>());
    }

    [Fact]
    public async Task CreateProductCoreWSpec_CoreExistsSpecDoesNot_InvalidSpec_ReturnsValidationError()
    {
        var dto = BuildInvalidFoodDto();
        var existingCore = BuildValidCore();

        _mockProductRepo.FindCore(dto.ProductName, dto.Brand).Returns(existingCore);
        _mockProductRepo.GetSpecByPredicate(existingCore.Id, Arg.Any<Expression<Func<FoodProductModel, bool>>>())
            .Returns((FoodProductModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation ="" });

        var result = await _productService.CreateProductCoreWSpec(dto, "user123");

        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.ValidationError.Id, result.Error.Id);
       
        await _mockProductRepo.DidNotReceive().CreateSpec(Arg.Any<ProductSpecModel>());
        await _mockProductRepo.DidNotReceive().CreateCore(Arg.Any<ProductCoreModel>());
    }

    [Fact]
    public async Task CreateProductCoreWSpec_CoreExistsSpecDoesNot_ValidSpec_CreatesSpecOnly()
    {
        var dto = BuildValidFoodDto();
        var existingCore = BuildValidCore();

        _mockProductRepo.FindCore(dto.ProductName, dto.Brand).Returns(existingCore);
        _mockProductRepo.GetSpecByPredicate(existingCore.Id, Arg.Any<Expression<Func<FoodProductModel, bool>>>())
            .Returns((FoodProductModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation=""});

        _mockProductRepo.GetCoreByID(existingCore.Id).Returns(existingCore);
        _mockProductRepo.GetSpecByEAN(dto.EAN).Returns((ProductSpecModel?)null);

        var result = await _productService.CreateProductCoreWSpec(dto, "user123");

        Assert.True(result.IsSuccess);
        Assert.Equal(existingCore, result.Value); //existingCore and Value references the same instance. Pointless
        Assert.Equal("Core ya existente, spec agregado al core existente", result.Metadata["Message"]);

        result.Value.Specs.Should().ContainSingle(s => s.EAN == dto.EAN);//Checks if spec was added to core's Spec collection
        Assert.True(result.Value.Specs.Any(s => s.EAN == dto.EAN));
        await _mockProductRepo.Received(1).CreateSpec(Arg.Is<ProductSpecModel>(s => s.Id == existingCore.Id));
        await _mockProductRepo.DidNotReceive().CreateCore(Arg.Any<ProductCoreModel>());
    }

    [Fact]
    public async Task CreateProductCoreWSpec_NeitherExist_InvalidCore_ReturnsValidationError()
    {
        var dto = BuildValidFoodDto();
        // Ajustar campos para que ProductCoreValidator falle (ej. ProductName vacío) si NetContent válido no alcanza
        dto.ProductName = "";

        _mockProductRepo.FindCore(dto.ProductName, dto.Brand).Returns((ProductCoreModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation=""});

        var result = await _productService.CreateProductCoreWSpec(dto, "user123");

        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.ValidationError.Id, result.Error.Id);
        await _mockProductRepo.DidNotReceive().CreateCore(Arg.Any<ProductCoreModel>());
        await _mockProductRepo.DidNotReceive().CreateSpec(Arg.Any<ProductSpecModel>());
    }

    [Fact]
    public async Task CreateProductCoreWSpec_NeitherExist_InvalidSpec_ReturnsValidationError()
    {
        var dto = BuildInvalidFoodDto();

        _mockProductRepo.FindCore(dto.ProductName, dto.Brand).Returns((ProductCoreModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation=""});

        var result = await _productService.CreateProductCoreWSpec(dto, "user123");

        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.ValidationError.Id, result.Error.Id);
        await _mockProductRepo.DidNotReceive().CreateCore(Arg.Any<ProductCoreModel>());
    }

    [Fact]
    public async Task CreateProductCoreWSpec_NeitherExist_BothValid_CreatesCoreWithSpec()
    {
        var dto = BuildValidFoodDto();
        var createdCore = BuildValidCore();

        _mockProductRepo.FindCore(dto.ProductName, dto.Brand).Returns((ProductCoreModel?)null);
        _userManagerMock.FindByIdAsync("user123").Returns(new AppUser { Id = "user123", UserName = "testuser", GeoLocation=""});
        _mockProductRepo.CreateCore(Arg.Any<ProductCoreModel>()).Returns(createdCore);

        var result = await _productService.CreateProductCoreWSpec(dto, "user123");

        Assert.True(result.IsSuccess);
        Assert.Equal(createdCore, result.Value);

        await _mockProductRepo.Received(1).CreateCore(Arg.Is<ProductCoreModel>(
            c => c.Specs.Any()));
        await _mockProductRepo.DidNotReceive().CreateSpec(Arg.Any<ProductSpecModel>());
    }


}