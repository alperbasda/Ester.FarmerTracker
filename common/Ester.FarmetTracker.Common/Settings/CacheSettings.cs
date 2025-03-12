namespace Ester.FarmetTracker.Common.Settings;

public class CacheSettings
{
    public int SlidingExpiration { get; set; }

    public string Host { get; set; } = default!;

    public string Port { get; set; } = default!;

    public string Password { get; set; } = default!;
}
