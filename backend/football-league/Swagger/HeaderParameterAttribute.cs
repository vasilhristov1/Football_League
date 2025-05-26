namespace football_league.Swagger;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AddHeaderParameterAttribute : Attribute
{
    public string Name { get; set; }
    public bool Required { get; set; }
}