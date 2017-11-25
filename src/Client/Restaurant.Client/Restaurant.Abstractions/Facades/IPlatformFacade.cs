namespace Restaurant.Abstractions.Facades
{
    public interface IPlatformFacade
    {
        string RuntimePlatform { get; }

        string Android { get; }

        string iOS { get; }

        string UWP { get; }
    }
}