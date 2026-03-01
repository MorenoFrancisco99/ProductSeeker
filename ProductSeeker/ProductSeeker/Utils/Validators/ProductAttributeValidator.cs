using FluentValidation;
using ProductSeeker.Data.Models;

namespace ProductSeeker;
public class ProductAttributeValidator : AbstractValidator<ProductSpecAttributeValue>
{   
    
    public ProductAttributeValidator(string itemCategory)
    {
        
    }
}
