using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Menu.API.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Menu.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class CurrencyController : Controller
    {
        public IEnumerable<CurrencyDto> Get()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                                      .Except(CultureInfo.GetCultures(CultureTypes.NeutralCultures))
                                      .Where(x => !x.Equals(CultureInfo.InvariantCulture));

            var result = new List<CurrencyDto>();
            foreach (var culture in cultures)
            {
                try
                {
                    var regionInfo = new RegionInfo(culture.LCID);
                    result.Add(new CurrencyDto
                    {
                        CurrencyName = regionInfo.CurrencyEnglishName,
                        CurrencySymbol = regionInfo.CurrencySymbol,
                        LCID = culture.LCID,
                        CountryName = regionInfo.EnglishName
                    });
                }
                catch (Exception)
                { // Ignore
                }
            }

            return result.Where(x => !string.IsNullOrEmpty(x.CurrencyName))
                        .GroupBy(x => x.CurrencyName)
                        .Select(x => x.FirstOrDefault())
                        .OrderBy(x => x.CurrencyName);
        }
    }
}