using ProductSeeker.Data.Models;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.Data;
namespace ProductSeeker;

public class ProductSpecValidator : AbstractValidator<ProductSpecModel>
{
    private readonly BusinessRulesConfig _rules;
    private readonly IProductRepository _prodRepo;
    public ProductSpecValidator(IOptions<BusinessRulesConfig> options, IProductRepository prodrepo)
    {
        _rules = options.Value;
        _prodRepo = prodrepo;

        string _msgError = "";

        RuleFor(x => x.Category)
        .NotNull()
        .Must(Category => _rules.Categories.ContainsKey(Category))
        .WithMessage("Product must have a valid category");

        RuleFor(x => x.ProductCoreId)
        .NotNull()
        .MustAsync(async (prodID, cancellation) =>
        {
            bool exist = await _prodRepo.CoreExist(prodID);
            return exist;
        }).WithMessage("Target product does not exist");


        //1. Each attribute must have a valid key, that means, existing for the given category
        //2. Every attribute flaged as "required" must be present
        //3. The value of the attribute must be valid
        // RuleFor(x => x.Attributes)
        // .Must((model, attributes) =>
        // {
        //     //rule 2

        //     //Get attributes of the given category 
        //     _rules.Categories.TryGetValue(model.Category, out var catConfig);

        //     //get keys of flagged as required
        //     var requiredAtt = catConfig.Attributes
        //     .Where(x => x.Value.IsRequired == true)
        //     .Select(x => x.Key);

        //     //keys of received attributes
        //     var recievedAttKey = attributes.Select(a => a.AttributeKey);

        //     //We get the required attributes thar are not present in recieved attributes
        //     var requiredMissing = requiredAtt.Except(recievedAttKey);

        //     if (requiredMissing.Any())
        //     {
        //         _msgError = $"Missing required fields: {string.Join(",", requiredMissing)}";
        //         return false;
        //     }


        //     //Rule 1
        //     //we get the received attributes that are not pressent on the category attributes
        //     var invalidAtt = recievedAttKey.Except(catConfig.Attributes.Select(x => x.Key));
        //     if(invalidAtt.Any())
        //     {
        //         _msgError =$"Invalid attributes for the category received: {string.Join(",", invalidAtt)}";
        //         return false;
        //     }
            
            
            


        //     return true;
        // })
        // .WithMessage(_msgError);

        

        
        



    }



}
