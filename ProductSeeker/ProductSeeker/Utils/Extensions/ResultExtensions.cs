using ProductSeeker;

public static class ResultExtensions
{
    public static Error WithMetadata(this Error error, string key, object value)
    {
        var metadata = error.Metadata is null
            ? new Dictionary<string, object>()
            : new Dictionary<string, object>(error.Metadata);

        metadata[key] = value;

        return error with { Metadata = metadata };
    }

    public static Result<T> WithMetadata<T>(this Result<T> result, string key, object value)
    {
        var metadata = result.Metadata is null
            ? new Dictionary<string, object>()
            : new Dictionary<string, object>(result.Metadata);

        metadata[key] = value;

        return result with { Metadata = metadata };
    }
}