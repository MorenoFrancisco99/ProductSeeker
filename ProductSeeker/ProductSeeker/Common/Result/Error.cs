using Microsoft.AspNetCore.CookiePolicy;

namespace ProductSeeker;

//Tomado de: https://medium.com/@aseem2372005/the-result-pattern-in-c-a-smarter-way-to-handle-errors-c6dee28a0ef0



/*
   Every new Error type has to be added to Utils/Extesions/ActionResultHandlerExtensions.cs in order to return the correct HTTP status code.
   Otherwise, it will return 500 Internal Server Error by default, which is not ideal for client error handling.
*/
public enum ErrorType { NotFound, Forbidden, Validation, Conflict }

/*
NotFound: non-existent resource
Forbiddent: user is not owner of the resource or doesn't have permissions to access it
Validation: Invalid data/fields 
Conflict: duplicate resource, bussines rules restriction
*/


public record Error(string Id, ErrorType Type, string Description, Dictionary<string, object>? Metadata = null);

public static class Errors
{

    //Maybe unify all NotFound into a single one and append the specific resource in WithMetadata
    public static Error ProductCoreNotFound { get; } = new("ProductCoreNotFound", ErrorType.NotFound, "Product core not found. ");
    public static Error ProductSpecNotFound { get; } = new("ProductSpecNotFound", ErrorType.NotFound, "Product spec not found. ");
    public static Error StoreSpecNotFound { get; } = new("StoreSpecNotFound", ErrorType.NotFound, "Store spec not found. ");
    public static Error StoreCoreNotFound { get; } = new("StoreCoreNotFound", ErrorType.NotFound, "Store core not found. ");
    public static Error UserPriceNotFound { get; } = new("UserPriceNotFound", ErrorType.NotFound, "Price of User not found. ");
    public static Error UserNotFound { get; } = new("UserNotFound", ErrorType.NotFound, "User not found. ");


    public static Error FieldsRequired { get; } = new("FieldsRequired", ErrorType.Validation, "At least one of the following fields are required: ");

    public static Error UnauthorizedAccess { get; } = new("UnauthorizedAccess", ErrorType.Forbidden, "Unauthorized access. ");
    public static Error Duplicate { get; } = new("Duplicate", ErrorType.Conflict, "Resource already exists.");
    public static Error ValidationError { get; } = new("ResourceValidationError", ErrorType.Validation, "Validation error with the resource.");


    public static Error WithMetadata(this Error err, string key, object value)
    {
        var metadata = err.Metadata is null
       ? new Dictionary<string, object>()
       : new Dictionary<string, object>(err.Metadata);

        metadata[key] = value;

        return err with { Metadata = metadata };
    }
}