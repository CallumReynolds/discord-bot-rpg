

namespace RpgApi.Models
{
    // Stores the appsettings.json property values for mapping purposes
    public class RPGDatabaseSettings : IRPGDatabaseSettings
    {
        public string RPGCollectionName {get; set;}
        public string ConnectionString {get; set;}
        public string DatabaseName {get; set;}

    }

    public interface IRPGDatabaseSettings
    {
        string RPGCollectionName {get; set;}
        string ConnectionString {get; set;}
        string DatabaseName {get; set;}

    }
}