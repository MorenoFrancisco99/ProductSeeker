using System.Reflection.Metadata.Ecma335;
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


    //TODO Add validators to store and get rid of this Error type. Use ValidationError instead
    public static Error FieldsRequired { get; } = new("FieldsRequired", ErrorType.Validation, "At least one of the following fields are required: ");

    public static Error UnauthorizedAccess { get; } = new("UnauthorizedAccess", ErrorType.Forbidden, "Unauthorized access. ");
    public static Error Duplicate { get; } = new("Duplicate", ErrorType.Conflict, "Resource already exists.");

    //Validation error its used in ResultValidationExtensions to convert the 
    // FluentValidation errors into a Result failure, so it needs to be generic 
    // enough to be used in any validation scenario. The specific validation 
    // errors are added as metadata with the "fields" key, followed by
    // a Dict with the field name as key and the list of errors for that field as value.
    // "fields": { "Name": ["msg1", "msg2"], "Price": ["msg1"] }
    //------>If any other validation type error is added, AppendValidationMetadata has to be adjusted <---------
    //Otherwise it would lead to lost of error info
    public static Error ValidationError { get; } = new("ResourceValidationError", ErrorType.Validation, "Validation error with the resource.");


    public static Error WithMetadata(this Error err, string key, object value)
    {
        var metadata = err.Metadata is null
       ? new Dictionary<string, object>()
       : new Dictionary<string, object>(err.Metadata);

        metadata[key] = value;

        return err with { Metadata = metadata };
    }

    public static Dictionary<string, object>? GetMetadata(this Error err) => err.Metadata;


    /// <summary>
    /// Merges the validation metadata of two <see cref="Error"/> instances of type <see cref="ErrorType.Validation"/>
    /// into a single <see cref="Error"/>, combining field-level error messages under the <c>fields</c> metadata key.
    /// </summary>
    /// <remarks>
    /// Both errors must be of type <see cref="ErrorType.Validation"/> and are expected to follow the
    /// <c>fields</c> metadata convention established by <see cref="ToValidationError"/>:
    /// <code>
    /// "fields": { "Name": ["msg1", "msg2"], "Price": ["msg1"], ... }
    /// </code>
    /// Field errors from both sides are merged; if the same field appears in both, its messages are concatenated.
    /// The returned error is based on <paramref name="rightError"/> with its <c>Metadata</c> replaced by the merged result.
    /// Though not purposefully intended, any metadata keys other than <c>fields</c> present in <paramref name="rightError"/> are preserved.
    ///
    /// <para><b>Assumptions:</b> This method assumes a single validation error shape across the codebase.
    /// If a second validation error type with a different metadata structure is introduced,
    /// the cast to <c>Dictionary&lt;string, string[]&gt;</c> will fail at runtime.
    /// See <see cref="ToValidationError"/> and <see cref="WithMetadata"/> for the originating convention.</para>
    /// </remarks>
    /// <param name="rightError">The base validation error. Its non-<c>fields</c> metadata is preserved in the result.</param>
    /// <param name="leftError">The validation error to merge into <paramref name="rightError"/>.</param>
    /// <returns>
    /// A new <see cref="Error"/> based on <paramref name="rightError"/> with the <c>fields</c> metadata
    /// containing the union of both errors' field messages.
    /// If either error has null metadata, the other is returned as-is.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if either error is not of type <see cref="ErrorType.Validation"/>.
    /// </exception>
    public static Error AppendValidationMetadata(this Error rightError, Error leftError)
    {
        //Validation error is managed in ResultValidationExtensions.cs, which methods are invoked when returning errors from a given service
        //It adds key "fields" followed by the dict of field names and the list of error for that field as a value
        //      "fields": { "Name": ["msg1", "msg2"], "Price": ["msg1"], ... }
        //We have to account for this pattern
        //Note that this method asumes theres only one type of validation error((a broad/general one))
        //If another Val error is added it may lead to lost of error information

        if (rightError.Type != ErrorType.Validation || leftError.Type != ErrorType.Validation)
            throw new ArgumentException("Errors must be Validation type"); 
        if (rightError.Metadata == null)
            return leftError;
        if (leftError.Metadata == null)
            return rightError;

        var metadata = new Dictionary<string, object>();

        var rDict = (Dictionary<string, string[]>)rightError.Metadata["fields"];
        var lDict = (Dictionary<string, string[]>)leftError.Metadata["fields"];

        var merged = rDict.Concat(lDict)
                        .GroupBy(x => x.Key)
                        .ToDictionary(
                            g => g.Key,
                            g => g.SelectMany(x => x.Value).ToArray()
                        );

        metadata["fields"] = merged;
        return rightError with { Metadata = metadata };
    }
}