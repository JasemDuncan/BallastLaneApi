
using ballastLaneApi.Application.ViewModels;
using ballastLaneApi.Domain.Entities;
using ballastLaneApi.Infrastructure.Interfaces;
using ballastLaneApi.Infrastructure.DataAccess;
using Npgsql;

namespace ballastLaneApi.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        User user = null;
        using (var conn = new NpgsqlConnection(_dataContext.ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("SELECT * FROM \"user\" WHERE Email = @Email", conn))
            {
                cmd.Parameters.AddWithValue("Email", email);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new User
                        {
                            UserId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            LastName = reader.GetString(2),
                            UserName = reader.GetString(3),
                            Password = reader.GetString(4),
                            Email = reader.GetString(5),
                            Salt = reader.GetString(6)
                        };
                    }
                }
            }
        }
        return user;
    }

    public async Task<User> GetUserById(int id)
    {
        User user = null;
        using (var conn = new NpgsqlConnection(_dataContext.ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("SELECT * FROM \"user\" WHERE user_id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("Id", id);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new User
                        {
                            UserId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            LastName = reader.GetString(2),
                            UserName = reader.GetString(3),
                            Password = reader.GetString(4),
                            Email = reader.GetString(5),
                            Salt = reader.GetString(6)
                        };
                    }
                }
            }
        }
        return user;
    }

    public async Task<User> CreateUser(User user)
    {
        using (var conn = new NpgsqlConnection(_dataContext.ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO \"user\" (Email, Password, Name, Lastname, username, salt) VALUES (@Email, @Password, @Name, @LastName, @UserName, @Salt) RETURNING user_id", conn))
            {
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("Password", user.Password);
                cmd.Parameters.AddWithValue("Name", user.Name);
                cmd.Parameters.AddWithValue("LastName", user.LastName);
                cmd.Parameters.AddWithValue("UserName", user.UserName);
                cmd.Parameters.AddWithValue("Salt", user.Salt);
                user.UserId = (int)await cmd.ExecuteScalarAsync();
            }
        }
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        using (var conn = new NpgsqlConnection(_dataContext.ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("UPDATE \"user\" SET UserName=@UserName, Name=@Name, LastName=@LastName, Email = @Email, Password = @Password WHERE user_id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("Id", user.UserId);
                cmd.Parameters.AddWithValue("UserName", user.UserName);
                cmd.Parameters.AddWithValue("Name", user.Name);
                cmd.Parameters.AddWithValue("LastName", user.LastName);
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("Password", user.Password);
                await cmd.ExecuteNonQueryAsync();
            }
        }
        return user;
    }
}