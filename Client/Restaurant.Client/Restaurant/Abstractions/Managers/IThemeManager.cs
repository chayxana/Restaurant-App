using Restaurant.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Abstractions.Managers
{
    public interface IThemeManager
    {
        MaterialTheme GetThemeFromColor(string color);
    }
}
