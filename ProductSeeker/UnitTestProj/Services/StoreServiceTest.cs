using NSubstitute;
using ProductSeeker;
using ProductSeeker.Data.Models;
namespace UnitTestProj.Services;
public class StoreServiceTest
{
    private readonly IStoreRepository _storeRepository;
    private readonly IStoreService _storeService;
    public StoreServiceTest()
    {
        _storeRepository = Substitute.For<IStoreRepository>();
        _storeService = new StoreService(_storeRepository);

    }

    /*----------------------------CreateCore Tests*/

    [Fact]
    public async Task CreateStoreCore_ValidData_ReturnsCreatedStoreCore()
    {
        // Arrange
        var storeDTO = new StoreCoreDTO
        {
            Name = "Test Store",
            Field = "Retail",
            Description = "A test store for unit testing."
        };
        var userID = "test-user-id";

        _storeRepository.CreateCore(Arg.Any<StoreCoreModel>()).Returns(new StoreCoreModel
        {
            Id = 1,
            Name = storeDTO.Name,
            Field = storeDTO.Field,
            Description = storeDTO.Description,
            IdCreator = userID,
            IsActive = true
        }
        );
        // Act
        var result = await _storeService.CreateStoreCore(storeDTO, userID);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(storeDTO.Name, result.Value.Name);
        Assert.Equal(storeDTO.Description, result.Value.Description);
    }


    [Fact]
    public async Task CreateStoreCore_InvalidDataMissingName_ReturnsValidationError()
    {
        // Arrange
        var storeDTO = new StoreCoreDTO
        {
            Name = "", // Invalid name
            Field = "Retail",
            Description = "A test store for unit testing."
        };
        var userID = "test-user-id";

        // Act
        var result = await _storeService.CreateStoreCore(storeDTO, userID);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.FieldsRequired.Id, result.Error.Id);
    }

    [Fact]
    public async Task CreateStoreCore_InvalidDataMissingField_ReturnsValidationError()
    {
        // Arrange
        var storeDTO = new StoreCoreDTO
        {
            Name = "Test Store",
            Field = "", // Invalid field
            Description = "A test store for unit testing."
        };
        var userID = "test-user-id";

        // Act
        var result = await _storeService.CreateStoreCore(storeDTO, userID);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.FieldsRequired.Id, result.Error.Id);
    }

    [Fact]
    public async Task CreateStoreCore_InvalidDataNameTooLong_ReturnsValidationError()
    {
        // Arrange
        var storeDTO = new StoreCoreDTO
        {
            Name = new string('A', 51), // Invalid name (too long)
            Field = "Retail",
            Description = "A test store for unit testing."
        };
        var userID = "test-user-id";

        // Act
        var result = await _storeService.CreateStoreCore(storeDTO, userID);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.ValidationError.Id, result.Error.Id);
    }

    [Fact]
    public async Task CreateStoreCore_InvalidDataFieldTooLong_ReturnsValidationError()
    {
        // Arrange
        var storeDTO = new StoreCoreDTO
        {
            Name = "Test Store",
            Field = new string('A', 51), // Invalid field (too long)
            Description = "A test store for unit testing."
        };
        var userID = "test-user-id";

        // Act
        var result = await _storeService.CreateStoreCore(storeDTO, userID);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.ValidationError.Id, result.Error.Id);
    }



    /*----------------------------CreateSpec Tests----------------------------------*/


    // ── StoreCoreNotFound ──────────────────────────────────────────────────────

    [Fact]
    public async Task CreateStoreSpec_CoreNotFound_ReturnsStoreCoreNotFoundError()
    {
        var dto = new StoreSpecDTO { StoreCoreId = 99 };
        _storeRepository.GetCoreByID(dto.StoreCoreId).Returns((StoreCoreModel?)null);

        var result = await _storeService.CreateStoreSpec(dto, "user-1");

        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.StoreCoreNotFound.Id, result.Error.Id);
    }

    // ── FieldsRequired (geolocation + businessDays both absent) ───────────────

    [Fact]
    public async Task CreateStoreSpec_NoGeolocationAndNoBusinessDays_ReturnsFieldsRequiredError()
    {
        var dto = new StoreSpecDTO
        {
            StoreCoreId = 1,
            Latitude = null,
            Longitude = null,
            BusinessDays = ""
        };
        _storeRepository.GetCoreByID(dto.StoreCoreId).Returns(new StoreCoreModel{Name = "Test Store", Field = "Retail"});

        var result = await _storeService.CreateStoreSpec(dto, "user-1");

        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.FieldsRequired.Id, result.Error.Id);
        Assert.True(result.Error.Metadata?.ContainsKey("EmptyFields"));
    }

    [Fact]
    public async Task CreateStoreSpec_LatitudeMissingAndNoBusinessDays_ReturnsFieldsRequiredError()
    {
        var dto = new StoreSpecDTO
        {
            StoreCoreId = 1,
            Latitude = null,
            Longitude = -66.1057,
            BusinessDays = "   "
        };
        _storeRepository.GetCoreByID(dto.StoreCoreId).Returns(new StoreCoreModel{Name = "Test Store", Field = "Retail"});

        var result = await _storeService.CreateStoreSpec(dto, "user-1");

        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.FieldsRequired.Id, result.Error.Id);
    }

    [Fact]
    public async Task CreateStoreSpec_LongitudeMissingAndNoBusinessDays_ReturnsFieldsRequiredError()
    {
        var dto = new StoreSpecDTO
        {
            StoreCoreId = 1,
            Latitude = 18.4655,
            Longitude = null,
            BusinessDays = null
        };
        _storeRepository.GetCoreByID(dto.StoreCoreId).Returns(new StoreCoreModel{Name = "Test Store", Field = "Retail"});

        var result = await _storeService.CreateStoreSpec(dto, "user-1");

        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.FieldsRequired.Id, result.Error.Id);
    }

    // ── Happy path ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task CreateStoreSpec_WithGeolocation_CreatesSpec()
    {
        var dto = new StoreSpecDTO
        {
            StoreCoreId = 1,
            Latitude = 18.4655,
            Longitude = -66.1057,
            BusinessDays = null
        };
        var expected = new StoreSpecModel();
        _storeRepository.GetCoreByID(dto.StoreCoreId).Returns(new StoreCoreModel{Name = "Test Store", Field = "Retail"});
        _storeRepository.CreateSpec(Arg.Any<StoreSpecModel>()).Returns(expected);

        var result = await _storeService.CreateStoreSpec(dto, "user-1");

        Assert.True(result.IsSuccess);
        Assert.Equal(expected, result.Value);
        await _storeRepository.Received(1).CreateSpec(Arg.Any<StoreSpecModel>());
    }

    [Fact]
    public async Task CreateStoreSpec_WithBusinessDays_CreatesSpec()
    {
        var dto = new StoreSpecDTO
        {
            StoreCoreId = 1,
            Latitude = null,
            Longitude = null,
            BusinessDays = "Mon-Fri"
        };
        var expected = new StoreSpecModel();
        _storeRepository.GetCoreByID(dto.StoreCoreId).Returns(new StoreCoreModel{Name = "Test Store", Field = "Retail"});
        _storeRepository.CreateSpec(Arg.Any<StoreSpecModel>()).Returns(expected);

        var result = await _storeService.CreateStoreSpec(dto, "user-1");

        Assert.True(result.IsSuccess);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task CreateStoreSpec_WithBothGeolocationAndBusinessDays_CreatesSpec()
    {
        var dto = new StoreSpecDTO
        {
            StoreCoreId = 1,
            Latitude = 18.4655,
            Longitude = -66.1057,
            BusinessDays = "Mon-Fri"
        };
        var expected = new StoreSpecModel();
        _storeRepository.GetCoreByID(dto.StoreCoreId).Returns(new StoreCoreModel{Name = "Test Store", Field = "Retail"});
        _storeRepository.CreateSpec(Arg.Any<StoreSpecModel>()).Returns(expected);

        var result = await _storeService.CreateStoreSpec(dto, "user-1");

        Assert.True(result.IsSuccess);
    }

}


