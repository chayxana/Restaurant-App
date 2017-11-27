using System;

namespace Restaurant.Abstractions.Providers
{
	public interface ISettingsProvider
	{
		DateTime LastUpdatedRefreshTokenTime { get; set; }
		string RefreshToken { get; set; }
	}
}