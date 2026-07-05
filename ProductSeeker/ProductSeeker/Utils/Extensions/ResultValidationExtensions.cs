// Utils/Extensions/ValidationResultExtensions.cs
using FluentValidation.Results;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

//Thanks to claudio
public static class ValidationResultExtensions
{
    // <summary>
    /// Converts a <see cref="ValidationResult"/> into a non-generic <see cref="Result"/>.
    /// </summary>
    /// <remarks>
    /// Use this overload when the operation does not return a value on success (e.g., delete, update).
    /// If validation fails, all field errors are packed into <see cref="Errors.ValidationError"/>
    /// under the <c>fields</c> metadata key, grouped by property name.
    /// </remarks>
    /// <param name="validationResult">The result produced by a FluentValidation validator.</param>
    /// <returns>
    /// <see cref="Result.Success()"/> if validation passed;
    /// <see cref="Result.Failure(Error)"/> with a <see cref="ErrorType.Validation"/> error otherwise.
    /// </returns>
    public static Result ToResult(this ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            return Result.Success();

        return Result.Failure(validationResult.ToValidationError());
    }

    /// <summary>
    /// Converts a <see cref="ValidationResult"/> into a <see cref="Result{T}"/>.
    /// </summary>
    /// <remarks>
    /// Use this overload when the operation returns a value on success (e.g., create, get).
    /// The <paramref name="value"/> parameter is only used when validation passes and is wrapped
    /// into a successful <see cref="Result{T}"/> via its implicit operator.
    /// If validation fails, <paramref name="value"/> is ignored entirely and a
    /// <see cref="ErrorType.Validation"/> error is returned instead.
    /// </remarks>
    /// <typeparam name="T">The type of the value returned on success.</typeparam>
    /// <param name="validationResult">The result produced by a FluentValidation validator.</param>
    /// <param name="value">
    /// The value to wrap on success. Pass <c>default!</c> when calling from a failure-only path,
    /// as it will never be evaluated.
    /// </param>
    /// <returns>
    /// A successful <see cref="Result{T}"/> wrapping <paramref name="value"/> if validation passed;
    /// a failed <see cref="Result{T}"/> with a <see cref="ErrorType.Validation"/> error otherwise.
    /// </returns>
    public static Result<T> ToResult<T>(this ValidationResult validationResult)
    {
       

        return validationResult.ToValidationError(); // implicit operator
    }



    /// <summary>
    /// Builds a <see cref="Errors.ValidationError"/> populated with the field-level errors
    /// extracted from a failed <see cref="ValidationResult"/>.
    /// </summary>
    /// <remarks>
    /// Errors are grouped by <see cref="FluentValidation.Results.ValidationFailure.PropertyName"/>
    /// and stored under the <c>fields</c> metadata key as a
    /// <see cref="Dictionary{TKey,TValue}">Dictionary&lt;string, string[]&gt;</see>.
    /// A new <see cref="Error"/> instance is always produced via <see cref="Errors.WithMetadata"/>,
    /// ensuring the shared static <see cref="Errors.ValidationError"/> is never mutated.
    /// </remarks>
    /// <param name="validationResult">A failed <see cref="ValidationResult"/> from FluentValidation.</param>
    /// <returns>
    /// A <see cref="Error"/> of type <see cref="ErrorType.Validation"/> with field errors in its metadata.
    /// </returns>
    private static Error ToValidationError(this ValidationResult validationResult)
    {
        // Agrupa los mensajes de error por campo: { "Name": ["msg1", "msg2"], "Price": ["msg1"] }
        var fieldErrors = validationResult.Errors
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => (object)g.Select(f => f.ErrorMessage).ToArray()
            );

        return Errors.ValidationError.WithMetadata("fields", fieldErrors);
    }

}