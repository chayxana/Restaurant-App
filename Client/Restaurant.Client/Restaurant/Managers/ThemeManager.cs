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
				Primary = (Color) App.Current.Resources[$"{color}Primary"],
				Light = (Color) App.Current.Resources[$"{color}Light"],
				Dark = (Color) App.Current.Resources[$"{color}Dark"]
			};
		}
	}
}