namespace AndereBieb;


[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class MyAttribute : Attribute
{
    public int MinAge { get; set; }
    public int MaxAge { get; set; }


}
