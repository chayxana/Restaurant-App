using Restaurant.Abstractions.Managers;
using Restaurant.Themes;
using Xamarin.Forms;

namespace Restaurant.Managers
{
    public class ThemeManager : IThemeManager
    {
        public MaterialTheme GetThemeFromColor(string color)
        {
            return new MaterialTheme
            {
                Primary = (Color)App.Current.Resources["{0}Primary".Fmt(color)],
                Light = (Color)App.Current.Resources["{0}Light".Fmt(color)],
                Dark = (Color)App.Current.Resources["{0}Dark".Fmt(color)]
            };
        }
    }
}
