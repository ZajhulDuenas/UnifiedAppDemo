using Newtonsoft.Json;

namespace MauiAppDemo.common;
public class AppSettings
{
    public ApiSettings ApiSettings { get; set; }
    public FeatureToggle FeatureToggle { get; set; }
    public TokenServiceSettings TokenServiceSettings { get; set; }
    public Logging Logging { get; set; }
}
public class ApiSettings
{
    public string BaseUrl { get; set; }
    public int Timeout { get; set; }
}

public class FeatureToggle
{
    public bool EnableLogging { get; set; }
}

public class Logging
{
    public LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    public string Default { get; set; }

    [JsonProperty("Microsoft.AspNetCore")]
    public string MicrosoftAspNetCore { get; set; }
}



public class TokenServiceSettings
{
    public string UrlBase { get; set; }
    public string UrlEmployeers { get; set; }
}

