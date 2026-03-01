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

        RuleFor(x => x.Attributes)
        .Must((model, attributes) =>
        {
            //Get attributes of the given category 
            _rules.Categories.TryGetValue(model.Category, out var catConfig);

            var requiredAtt = catConfig.Attributes
            .Where(x => x.Value.IsRequired)
            .Select(x => x.Key);

            var recievedAtt = attributes.Select(a => a.AttributeKey);

            //We get the required attributes thar are not present in recieved attributes
            var requiredMissing = requiredAtt.Except(recievedAtt);


            return !requiredMissing.Any();
        })
        .WithMessage()
        
    }



}
