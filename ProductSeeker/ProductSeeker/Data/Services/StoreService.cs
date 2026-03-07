using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Mappers;

namespace ProductSeeker;

public class StoreService : IStoreService
{

    private readonly IStoreRepository _storeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly AplicationDBContext _context;
    public StoreService(IStoreRepository storeRepo,
                        UserManager<AppUser> userManager,
                        AplicationDBContext context)
    {
        _storeRepository = storeRepo;
        _userManager = userManager;
        _context = context;
    }

    public async Task<Result<StoreCoreModel>> GetCoreByID(int CoreId, string userID)
    {
        return await _storeRepository.GetCoreByID(CoreId, userID);
    }

    public async Task<Result<StoreSpecModel>> GetSpecByID(int SpecId, string userID)
    {
        return await _storeRepository.GetSpecByID(SpecId, userID);
    }

    public async Task<Result<StoreCoreModel>> CreateStoreCore(StoreCoreDTO storeDTO, string userID)
    {
        var storeCore = storeDTO.FromStoreCoreDTOToStoreCoreModel(userID);

        return await _storeRepository.CreateCore(storeCore);

    }


    public async Task<Result<StoreSpecModel>> CreateStoreSpec(StoreSpecDTO storeDTO, string userID)
    {
        var coreExistsandBelongsToUser = await _storeRepository.GetCoreByID(storeDTO.StoreCoreId, userID);
        if (!coreExistsandBelongsToUser.IsSuccess)
            return coreExistsandBelongsToUser.Error!; //Error.NotFound o Error.Forbidden


        if (string.IsNullOrWhiteSpace(storeDTO.BusinessDays) && string.IsNullOrWhiteSpace(storeDTO.GeoLocation))
        {
            return Errors.FieldsRequired.WithMetadata("MissingFields", ("BussinesDays", "GeoLocation"));
        }
        var storeModel = storeDTO.FromStoreSpecDTOToStoreSpecModel(userID);

        return await _storeRepository.CreateSpec(storeModel);

    }


    public async Task<bool> IsCoreOwner(int id, string UserId)
    {
        return await _storeRepository.IsCoreOwner(id, UserId);
    }


    public async Task<StoreCoreModel?> CreateStoreWSpec(StoreWSpecDTO storeDTO, string userID)
    {
        throw new NotImplementedException();



        // try
        // {
        //     var (storeCore, storeSpec) = storeDTO.FromStoreWSpecDTOToModels();
        //     await _storeCoreRepo.Add()

        // }catch(Exception ex)
        // {
        //     Console.WriteLine($"Error craeting StoreWSpec: {ex}");
        // }


    }

    public Task<StoreCoreModel> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsSpecOwner(int SpecId, string UserID)
    {
        throw new NotImplementedException();
    }
}
