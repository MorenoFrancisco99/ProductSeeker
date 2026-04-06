using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ProductSeeker;

public static class ErrorMappingExtensions
{
    /// <summary>
    /// Maps a domain <see cref="Error"/> instance to the corresponding HTTP <see cref="ActionResult"/>.
    /// </summary>
    /// <remarks>
    /// This method centralizes the translation between domain-level errors and HTTP responses,
    /// allowing controllers to remain concise and preventing repetition of error-handling logic.
    /// <para>
    /// The mapping between <see cref="ErrorType"/> and HTTP responses is as follows:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <see cref="ErrorType.NotFound"/> → returns <see cref="NotFoundObjectResult"/> (HTTP 404).
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <see cref="ErrorType.Forbidden"/> → returns an <see cref="ObjectResult"/> with status code 403 (HTTP 403).
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <see cref="ErrorType.Validation"/> → returns <see cref="UnprocessableEntityObjectResult"/> (HTTP 422).
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Any other error type → returns an <see cref="ObjectResult"/> with status code 500 (HTTP 500).
    /// </description>
    /// </item>
    /// </list>
    /// <para>
    /// The response body contains a concatenation of the error <see cref="Error.Description"/> and
    /// optional <see cref="Error.Metadata"/> to provide additional context.
    /// </para>
    /// </remarks>
    /// <param name="error">The domain error to be translated into an HTTP response.</param>
    /// <returns>
    /// An <see cref="ActionResult"/> representing the HTTP response corresponding to the provided error.
    /// </returns>
    public static ActionResult ToActionResult(this Error error)
    {
        return error.Type switch
        {
            ErrorType.NotFound => new NotFoundObjectResult($"{error.Description}" + $"{JsonSerializer.Serialize(error.Metadata)}"),
            ErrorType.Forbidden => new ObjectResult($"{error.Description}" + $"{JsonSerializer.Serialize(error.Metadata)}") { StatusCode = 403 },
            ErrorType.Validation => new UnprocessableEntityObjectResult($"{error.Description}" + $"{JsonSerializer.Serialize(error.Metadata)}"),
            ErrorType.Conflict => new ConflictObjectResult($"{error.Description}" + $"{JsonSerializer.Serialize(error.Metadata)}"),
            _ => new ObjectResult("An unexpected error occurred.") { StatusCode = 500 }
        };
    }
}
