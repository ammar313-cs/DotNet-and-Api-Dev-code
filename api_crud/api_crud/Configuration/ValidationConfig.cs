
namespace api_crud.Validation_Configuration{
public class ValidationConfig
{
    public string? PropertyName { get; set; }
    public bool? Required { get; set; }
    public int? MaxLength { get; set; }
    public string? RegexPattern { get; set; }
    public string? RegexErrorMessage { get; set; }
    public bool? EmailAddress { get; set; }
}

}