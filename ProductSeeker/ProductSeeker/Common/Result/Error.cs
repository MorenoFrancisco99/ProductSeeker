using Microsoft.AspNetCore.CookiePolicy;

namespace ProductSeeker;

//Tomado de: https://medium.com/@aseem2372005/the-result-pattern-in-c-a-smarter-way-to-handle-errors-c6dee28a0ef0


public enum ErrorType { NotFound, Forbidden, Validation, Conflict}

/*
NotFound: non-existent resource
Forbiddent:
Validation: Invalid data/fields 
Conflict: duplicate resource, bussines rules restriction
*/
public record Error(string Id, ErrorType Type, string Description, Dictionary<string, object>? Metadata = null);

public static class Errors
{
    public static Error ProductCoreNotFound {get;} =new("ProductCoreNotFound", ErrorType.NotFound, "Product core could not be found");
    public static Error ProductSpecNotFound {get;} =new("ProductSpecNotFound", ErrorType.NotFound, "Product spec could not be found");
    public static Error StoreSpecNotFound {get;} =new("StoreSpecNotFound", ErrorType.NotFound, "Store spec could not be found");
    public static Error StoreCoreNotFound {get;} =new("StoreCoreNotFound", ErrorType.NotFound, "Store core could not be found");
    public static Error UserPriceNotFound {get;} =new("UserPriceNotFound", ErrorType.NotFound, "Price of User could not be found");
    
    public static Error FieldsRequired {get; } = new("FieldsRequired", ErrorType.Validation, "At least one of the following fields are required: ");

    public static Error NotOwner {get;} =new("NotOwner", ErrorType.Forbidden, "User is not owner of the resource");
    
    public static Error WithMetadata(this Error err, string key, object value) 
    {
        var metadata = err.Metadata ?? new Dictionary<string, object>();
        
        metadata[key] = value; 

        return err with {Metadata = metadata};
    }
}