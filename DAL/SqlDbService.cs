using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using poprawa.DTO.Responses;
using poprawa.Exceptions;

namespace poprawa.DAL
{
    public class SqlDbService : IDbService
    {
        private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog = s18840; Integrated Security = True";
        public GetTasksResponse GetMemberTasks(int id)
        {
            var response = new GetTasksResponse();
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "select * from TeamMember where TeamMember.IdTeamMember = @Id;";
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.Clear();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new TeamMemberNotFoundException($"No such team member: {id}");
                    }

                    while (reader.Read())
                    {
                        response.IdTeamMember = id;
                        response.FirstName = reader["FirstName"].ToString();
                        response.LastName = reader["LastName"].ToString();
                        response.Email = reader["Email"].ToString();
                    }
                }
                
                command.CommandText = "select t.Name as TaskName, t.Description, "
                                      + "t.Deadline as Deadline, p.Name as ProjectName, "
                                      + "tt.Name as TaskTypeName from Task t"
                                      + "inner join TaskType tt on tt.IdTaskType = t.IdTaskType "
                                      + "inner join Project p on p.IdTeam = t.IdTeam "
                                      + "where t.IdAssignedTo = @Id or t.IdCreator = @Id "
                                      + "order by t.Deadline desc;";
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.Clear();
                using (var reader = command.ExecuteReader())
                {
                    response.Tasks = new List<MemberTasks>();
                    while (reader.Read())
                    {
                        var memberTask = new MemberTasks
                        {
                            TaskName = reader["TaskName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Deadline = DateTime.Parse(reader["Deadline"].ToString()),
                            ProjectName = reader["ProjectName"].ToString(),
                            TaskTypeName = reader["TaskTypeName"].ToString()
                        };
                        response.Tasks.Add(memberTask);
                    }
                }
            }
            return response;
        }

        public void DeleteProject(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                
                command.CommandText = "select * from Project p where p.IdTeam = @Id;";
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.Clear();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new ProjectNotFoundExceptionException($"No such team: {id}");
                    }
                }

                command.CommandText = "delete from Project p where p.IdTeam = @Id;";
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.Clear();
                command.ExecuteNonQuery();
                
                command.CommandText = "delete from Task t where t.IdTeam = @Id;";
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.Clear();
                command.ExecuteNonQuery();
                
                transaction.Commit();
            }
        }
    }
}