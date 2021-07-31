using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using TodoApp.Shared.Models.Extensions;
using TodoApp.Shared.Services;
using Windows.Storage;
using TodoApp.Shared.Enums;
using TodoApp.Shared.Models.Entity;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    /// <summary>
    /// Class for accessing data.
    /// </summary>
    public class DataAccess : IDataAccess
    {
        /// <summary>
        /// Private field for storing path to database file.
        /// </summary>
        private string dbPath;

        /// <inheritdoc/>
        public async Task<bool> AddDataAsync(Entity entity, string tableName)
        {
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={dbPath}"))
                {
                    db.Open();
                    SqliteCommand insertCommand = new SqliteCommand
                    {
                        Connection = db,
                        CommandText = $"INSERT INTO {tableName} VALUES "
                    };
                    if (entity is Todo todo)
                    {
                        insertCommand.CommandText += "(@Id, @IsCompleted, @Name, @Added, @Updated);";
                        _ = insertCommand.Parameters.AddWithValue("@Id", todo.Id);
                        _ = insertCommand.Parameters.AddWithValue("@IsCompleted", Convert.ToInt16(todo.IsCompleted));
                        _ = insertCommand.Parameters.AddWithValue("@Name", todo.Name);
                        _ = insertCommand.Parameters.AddWithValue("@Added", todo.Added);
                        _ = insertCommand.Parameters.AddWithValue("@Updated", todo.Updated);
                    }
                    _ = await insertCommand.ExecuteReaderAsync();
                    db.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> AddMultipleEntitiesAsync(List<Entity> entities, string tableName)
        {
            await Task.Delay(1000);
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteDataAsync(string id, string tableName)
        {
            await Task.Delay(1000);
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteMultipleEntitiesAsync(List<Entity> entities, string tableName)
        {
            await Task.Delay(1000);
            return true;
        }

        /// <inheritdoc/>
        public async Task<List<Entity>> GetDataAsync(string tableName)
        {
            List<Entity> entities = new List<Entity>();
            using (SqliteConnection db = new SqliteConnection($"Filename={dbPath}"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand($"SELECT * from {tableName}", db);
                SqliteDataReader query = await selectCommand.ExecuteReaderAsync();
                while (await query.ReadAsync())
                {
                    if (tableName.Equals(TableEnums.Todos.ToString()))
                    {
                        entities.Add(new Todo
                        {
                            Id = query.GetString(0),
                            IsCompleted = Convert.ToBoolean(query.GetInt16(1)),
                            Name = query.GetString(2),
                            Added = query.GetDateTime(3),
                            Updated = query.GetDateTime(4)
                        });
                    }
                }
                db.Close();
            }
            return entities;
        }

        /// <inheritdoc/>
        public async Task<Entity> GetEntityAsync(string tableName, string id)
        {
            await Task.Delay(1000);
            return null;
        }

        /// <inheritdoc/>
        public async void InitializeDatabase()
        {
            _ = await ApplicationData.Current.LocalFolder.CreateFileAsync("todoDatabase.db", CreationCollisionOption.OpenIfExists);
            dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoDatabase.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbPath}"))
            {
                db.Open();

                string tableCommand;
                SqliteCommand sqliteCommand;

                tableCommand = $"CREATE TABLE IF NOT EXISTS {TableEnums.Todos} (" +
                    "TodoId VARCHAR2 PRIMARY KEY, " +
                    "IsCompleted CHAR(1) NOT NULL, " +
                    "Name VARCHAR2(4000) NOT NULL, " +
                    "Added DATE NOT NULL, " +
                    "Updated DATE NOT NULL)";

                sqliteCommand = new SqliteCommand(tableCommand, db);

                _ = sqliteCommand.ExecuteReader();

                tableCommand = $"CREATE TABLE IF NOT EXISTS {TableEnums.SubTodos} (" +
                    "SubTodoId VARCHAR2 PRIMARY KEY, " +
                    "TodoId VARCHAR2(4000), " +
                    "IsCompleted CHAR(1) NOT NULL, " +
                    "Name VARCHAR2(4000) NOT NULL, " +
                    "Added DATE NOT NULL, " +
                    "Updated DATE NOT NULL, " +
                    "FOREIGN KEY (TodoId) REFERENCES Todos (TodoId) ON DELETE CASCADE)";

                sqliteCommand = new SqliteCommand(tableCommand, db);

                _ = sqliteCommand.ExecuteReader();
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateDataAsync(Entity entity, string tableName)
        {
            await Task.Delay(1000);
            return true;
        }
    }
}