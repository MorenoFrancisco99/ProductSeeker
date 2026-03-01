namespace ProductSeeker;

public class BusinessRulesConfig
{
    public string Version {get; init;}
    public Dictionary<string, CategoryConfig> Categories { get; init; }

}

public class CategoryConfig
{
    public Dictionary<string, AttributeConfig> Attributes { get; init; }
}

public class AttributeConfig
{
    public string DataType { get; init; }
    public bool IsRequired { get; init; }
}
