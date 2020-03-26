namespace Menu.API.Providers
{
    using System.Collections.Generic;
    using Menu.API.Models;

    public interface ICurrencyProvider
    {
        IReadOnlyList<Currency> GetAll();

        Currency GetByCode(string code);
    }
}