using System.Collections;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Mappers;
using static ProductSeeker.CreationSourceEnum;

namespace ProductSeeker;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepo;
    private readonly IStoreRepository _storeRepo;
    private readonly IServiceProvider _serviceProvider;
    private readonly IValidator<ProductCoreModel> _coreValidator;
    public ProductService(IProductRepository prodRepo, IStoreRepository storeRepo,
                          IServiceProvider serviceProvider, IValidator<ProductCoreModel> coreValidator)
    {
        _productRepo = prodRepo;
        _storeRepo = storeRepo;
        _serviceProvider = serviceProvider;
        _coreValidator = coreValidator;

    }



    public async Task<Result<ProductCoreModel>> CreateProductCore(POSTProductCoreDTO productDTO, string userID)
    {
        //Hardcode CreationSource to user
        //Any other source should be used only by admins or system, and that will be handled in a different method
        var model = productDTO.FromPOSTCoreDTOToModel(userID, CreationSource.User);

        var validationResult = await _coreValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<ProductCoreModel>(default!);
        }
        return await _productRepo.CreateCore(model);
    }




    public async Task<Result<ProductSpecModel>> CreateProductSpec(POSTProductSpecDTO dto, string userID)
    {

        //Hardcode CreationSource to user
        //Any other source should be used only by admins, and that will be handled in a different method
        var model = dto.FromPOSTSpecDTOToModel(userID, CreationSource.User);

        var validator = GetValidatorForSpec(model);
        var modelValidationResult = await validator.ValidateAsync(new ValidationContext<object>(model));

        if (!modelValidationResult.IsValid)
            return modelValidationResult.ToResult<ProductSpecModel>(default!);

        return await _productRepo.CreateSpec(model);
    }

    public async Task<Result<AppUserProductPriceModel>> CreateProductPrice(POSTProductPriceDTO dto, string userID)
    {
        var specExists = await _productRepo.GetSpecByID(dto.ProductSpecId);
        if (specExists == null)
            return Errors.ProductSpecNotFound;

        var coreExists = await _storeRepo.GetCoreByID(dto.StoreId);
        if (coreExists == null)
            return Errors.StoreCoreNotFound;

        var model = dto.MapToModel(userID);
        return await _productRepo.CreatePrice(model);

    }



    public Task<IEnumerable<ProductCoreModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ProductCoreModel>> GetCoreByID(int CoreId, string userID)
    {
        var result = await _productRepo.GetCoreByID(CoreId);
        if (result == null)
            return Errors.ProductCoreNotFound;
        return result;
    }

    public async Task<Result<AppUserProductPriceModel>> GetPriceByID(int priceId, string userID)
    {
        var result = await _productRepo.GetPriceByID(priceId);
        if (result == null)
            return Errors.UserPriceNotFound;
        return result;
    }

    public async Task<Result<GETProductSpecDTO>> GetSpecByID(int SpecId, string userID)
    {
        var result = await _productRepo.GetSpecByID(SpecId);
        if (result == null)
            return Errors.ProductSpecNotFound;
        return result.FromModelToGETDTO();
    }



    public async Task<Result<ProductSpecModel>> ADMINCreateProductWCore(POSTProductWCoreDTO dto, string userID)
    {

        var existingCore = await _productRepo.FindCore(dto.ProductName, dto.Brand);


        ProductCoreModel core;
        if (existingCore != null)
        {
            core = existingCore;
        }
        else
        {
            var newCore = new ProductCoreModel
            {
                Category = dto.Category,
                ProductName = dto.ProductName,
                Brand = dto.Brand,
                IdCreator = userID,
                CreationSource = CreationSourceEnum.CreationSource.Scrapped,
                IsActive = true
            };

            var coreValidationResult = await _coreValidator.ValidateAsync(newCore);
            if (!coreValidationResult.IsValid)
                return Errors.ValidationError.WithMetadata("ValidationErrors", coreValidationResult.Errors.Select(e => e.ErrorMessage));


            var createdCore = await _productRepo.CreateCore(newCore);
            if (createdCore == null) return Errors.ProductCoreNotFound;
            core = createdCore;
        }


        //Check if the specs exists
        //Sometimes specs may have the same Identifiers but not the same EA
        var specByIdentifiers = await _productRepo.FindSpecByIdentifiers(core.Id, dto.GetSpecIdentifier());
        if (specByIdentifiers != null) //Exists by identifiers
        {
            var specByEAN = await _productRepo.GetSpecByEAN(dto?.EAN);
            if (specByEAN != null) //Exist by EAN
                return Errors.Duplicate.WithMetadata("Spec Product Already Exists, ID: ", specByIdentifiers.Id);
        }

        var spec = dto.FromPOSTFoodWCoreDTOToSpecModel(userID, CreationSource.Scrapped, core.Id);




        var validator = GetValidatorForSpec(spec);

        var specValidationResult = await validator.ValidateAsync(new ValidationContext<object>(spec));
        if (!specValidationResult.IsValid)
            return Errors.ValidationError.WithMetadata("ValidationErrors", specValidationResult.Errors.Select(e => e.ErrorMessage));




        return await _productRepo.CreateSpec(spec);

    }










    /// <summary>
    /// This method uses reflection to get the appropriate validator for the given product spec. 
    /// The validators must be registered in the DI container for this to work. 
    /// If no validator is found for the given spec type, an exception is thrown.
    /// 
    /// Note that for validator to work, the argument need to be wrapped in a ValidationContext<object>( ), so the validation can be executed with the correct ruleset. 
    /// This is specially important for the spec validators, as they need to execute the rules that depend on the category of the product, and that information is stored in the context.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception">Validator not found for the given spec type</exception>
    private IValidator GetValidatorForSpec(ProductSpecModel spec)
    {


        //Dynamic validations used to be done with ValidatorFactory but that has now been deprecated
        //Using reflection with IServiceProvider is now the recommended way by the author
        //Taken from https://github.com/FluentValidation/FluentValidation/issues/1961

        var validatorType = typeof(IValidator<>).MakeGenericType(spec.GetType());
        var validator = (IValidator)_serviceProvider.GetService(validatorType)!;
        if (validator == null)
            throw new Exception($"No validator found for type {spec.GetType()}");
        return validator;
    }
}


