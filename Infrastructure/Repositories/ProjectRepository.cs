using ballastLaneApi.Domain.Entities;
using Npgsql;
using ballastLaneApi.Infrastructure.DataAccess;
using ballastLaneApi.Infrastructure.Interfaces;

namespace ballastLaneApi.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            var projects = new List<Project>();
            using (var conn = new NpgsqlConnection(_context.ConnectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("SELECT * FROM Project", conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        projects.Add(new Project
                        {
                            ProjectId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Cost = reader.GetDouble(3),
                            Sales = reader.GetInt16(4)
                        });
                    }
                }
            }
            return projects;
        }

        public async Task<Project> GetProject(int id)
        {
            Project project = null;
            using (var conn = new NpgsqlConnection(_context.ConnectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("SELECT * FROM project WHERE project_id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            project = new Project
                            {
                                ProjectId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Cost = reader.GetDouble(3),
                                Sales = reader.GetInt16(4)
                            };
                        }
                    }
                }
            }
            return project;
        }

        public async Task<Project> CreateProject(Project project)
        {
            using (var conn = new NpgsqlConnection(_context.ConnectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("INSERT INTO project (Name, Description) VALUES (@Name, @Description) RETURNING project_id", conn))
                {
                    cmd.Parameters.AddWithValue("Name", project.Name);
                    cmd.Parameters.AddWithValue("Description", project.Description);
                    project.ProjectId = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return project;
        }

        public async Task<Project> UpdateProject(Project project)
        {
            using (var conn = new NpgsqlConnection(_context.ConnectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("UPDATE project SET Name = @Name, Description = @Description, Cost = @Cost, Sales = @Sales WHERE project_id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("Id", project.ProjectId);
                    cmd.Parameters.AddWithValue("Name", project.Name);
                    cmd.Parameters.AddWithValue("Description", project.Description);
                    cmd.Parameters.AddWithValue("Cost", project.Cost);
                    cmd.Parameters.AddWithValue("Sales", project.Sales);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return project;
        }

        public async Task DeleteProject(int id)
        {
            using (var conn = new NpgsqlConnection(_context.ConnectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("DELETE FROM project WHERE project_id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}