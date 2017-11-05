using Restaurant.Themes;

namespace Restaurant.Abstractions.Managers
{
	public interface IThemeManager
	{
		MaterialTheme GetThemeFromColor(string color);
	}
}