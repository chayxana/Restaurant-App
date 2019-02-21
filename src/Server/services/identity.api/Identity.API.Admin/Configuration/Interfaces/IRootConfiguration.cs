namespace Identity.API.Admin.Configuration.Interfaces
{
    public interface IRootConfiguration
    {
        IAdminConfiguration AdminConfiguration { get; }
    }
}