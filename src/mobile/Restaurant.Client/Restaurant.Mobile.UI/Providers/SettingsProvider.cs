using System;
using Plugin.Settings.Abstractions;
using Restaurant.Abstractions.Constants;
using Restaurant.Abstractions.Providers;

namespace Restaurant.Mobile.UI.Providers
{
    public class SettingsProvider : ISettingsProvider
	{
	    private readonly ISettings _settings;

	    public SettingsProvider(ISettings settings)
	    {
		    _settings = settings;
	    }
		
	    public string RefreshToken
	    {
		    get => _settings.GetValueOrDefault(Settings.RefreshTokenKey, string.Empty);
		    set => _settings.AddOrUpdateValue(Settings.RefreshTokenKey, value);
	    }
		
		public DateTime LastUpdatedRefreshTokenTime
	    {
		    get => _settings.GetValueOrDefault(Settings.LastUpdatedRefreshTokenTimeKey, DateTime.MinValue);
		    set => _settings.AddOrUpdateValue(Settings.LastUpdatedRefreshTokenTimeKey, value);
	    }
	}
}
