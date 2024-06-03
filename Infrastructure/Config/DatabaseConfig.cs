using Microsoft.Extensions.Configuration;

namespace ballastLaneApi.Infrastructure.Config
{
    public class DatabaseConfig
    {
        private readonly IConfiguration _configuration;

        public DatabaseConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            // Aquí obtienes la cadena de conexión de la configuración de la aplicación
            // Puedes cambiar el nombre de la clave según cómo hayas configurado tu appsettings.json
            return _configuration.GetConnectionString("DefaultConnection");
        }
    }
}