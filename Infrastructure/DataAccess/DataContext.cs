namespace ballastLaneApi.Infrastructure.DataAccess;
public class DataContext
{
    public string ConnectionString { get; }

    public DataContext(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("DefaultConnection");
    }
}
