using System.Text.Json;

namespace MauiAppDemo.common; 
public class AppSettings
{

    public AppSettings Settings { get; set; }
    public ApiSettings ApiSettings { get; set; }
    public FeatureToggle FeatureToggle { get; set; }
    public TokenServiceSettings TokenServiceSettings { get; set; }

    private void LoadAppSettings()
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "appsettings.json");

        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            Settings = JsonSerializer.Deserialize<AppSettings>(json);
        }
        else
        {
            Settings = new AppSettings(); // Valores por defecto si el archivo no existe
        }
    }

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

public class TokenServiceSettings
{
    public string UrlBase { get; set; }
    public string UrlEmployeers { get; set; }
}

