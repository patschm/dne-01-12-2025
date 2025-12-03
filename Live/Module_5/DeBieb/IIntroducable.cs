
namespace DeBieb
{
    public interface IIntroducable
    {
        int Age { get; set; }
        string? Firstname { get; set; }
        string? Lastname { get; set; }

        Task IntroduceAsync();
    }
}