namespace Menu.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using Menu.API.Models;
    using Menu.API.Providers;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyProvider currencyProvider;

        public CurrencyController(ICurrencyProvider currencyProvider)
        {
            this.currencyProvider = currencyProvider;
        }

        [HttpGet]
        public IEnumerable<Currency> Get()
        {
            return currencyProvider.GetAll();
        }
    }
}