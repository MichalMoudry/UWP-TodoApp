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
        private string _dbPath;

        /// <summary>
        /// Private field with an instance of <see cref="SqliteCommand"/> class.
        /// </summary>
        private SqliteCommand _sqliteCommand;

        /// <inheritdoc/>
        public async Task<bool> AddDataAsync(Entity entity, string tableName)
        {
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={_dbPath}"))
                {
                    db.Open();
                    _sqliteCommand = new SqliteCommand
                    {
                        Connection = db,
                        CommandText = $"INSERT INTO {tableName} VALUES "
                    };
                    List<SqliteParameter> parameters = new List<SqliteParameter>();
                    if (entity is Todo todo)
                    {
                        _sqliteCommand.CommandText += $"(@Id, @IsCompleted, @Name, @Added, @Updated, @AlertDate, @CompletetionDate, @Repetition);";
                        parameters.Add(new SqliteParameter("@Id", todo.Id));
                        parameters.Add(new SqliteParameter("@IsCompleted", Convert.ToInt16(todo.IsCompleted)));
                        parameters.Add(new SqliteParameter("@Name", todo.Name));
                        parameters.Add(new SqliteParameter("@Added", todo.Added));
                        parameters.Add(new SqliteParameter("@Updated", todo.Updated));
                        parameters.Add(new SqliteParameter("@AlertDate", todo.AlertDate));
                        parameters.Add(new SqliteParameter("@CompletetionDate", todo.CompletetionDate));
                        parameters.Add(new SqliteParameter("@Repetition", Convert.ToInt16(todo.Repetition)));
                        _sqliteCommand.Parameters.AddRange(parameters.ToArray());
                    }
                    _ = await _sqliteCommand.ExecuteReaderAsync();
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
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={_dbPath}"))
                {
                    db.Open();
                    _sqliteCommand = new SqliteCommand($"DELETE FROM {tableName} WHERE Id=@Id", db);
                    _ = _sqliteCommand.Parameters.AddWithValue("@Id", id);
                    _ = await _sqliteCommand.ExecuteNonQueryAsync();
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
        public async Task<bool> DeleteMultipleEntitiesAsync(List<Entity> entities, string tableName)
        {
            bool res = false;
            foreach (Entity entity in entities)
            {
                res = await DeleteDataAsync(entity.Id, tableName);
            }
            return res;
        }

        /// <inheritdoc/>
        public async Task<List<Entity>> GetDataAsync(string tableName)
        {
            List<Entity> entities = new List<Entity>();
            using (SqliteConnection db = new SqliteConnection($"Filename={_dbPath}"))
            {
                db.Open();
                _sqliteCommand = new SqliteCommand($"SELECT * from {tableName}", db);
                SqliteDataReader query = await _sqliteCommand.ExecuteReaderAsync();
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
                            Updated = query.GetDateTime(4),
                            AlertDate = query.GetDateTime(5),
                            CompletetionDate = query.GetDateTime(6),
                            Repetition = (TodoRepetition)query.GetInt16(7)
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
            _dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoDatabase.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={_dbPath}"))
            {
                db.Open();

                string tableCommand;

                tableCommand = $"CREATE TABLE IF NOT EXISTS {TableEnums.Todos} (" +
                    "Id VARCHAR2 PRIMARY KEY, " +
                    "IsCompleted CHAR(1) NOT NULL, " +
                    "Name VARCHAR2(4000) NOT NULL, " +
                    "Added DATE NOT NULL, " +
                    "Updated DATE NOT NULL, " + 
                    "AlertDate DATE NOT NULL, " +
                    "CompletetionDate DATE NOT NULL, " +
                    "Repetition CHAR(1) NULL)";

                _sqliteCommand = new SqliteCommand(tableCommand, db);

                _ = _sqliteCommand.ExecuteReader();

                tableCommand = $"CREATE TABLE IF NOT EXISTS {TableEnums.SubTodos} (" +
                    "Id VARCHAR2 PRIMARY KEY, " +
                    "TodoId VARCHAR2(4000), " +
                    "IsCompleted CHAR(1) NOT NULL, " +
                    "Name VARCHAR2(4000) NOT NULL, " +
                    "Added DATE NOT NULL, " +
                    "Updated DATE NOT NULL, " +
                    "FOREIGN KEY (TodoId) REFERENCES Todos (Id) ON DELETE CASCADE)";

                _sqliteCommand = new SqliteCommand(tableCommand, db);

                _ = _sqliteCommand.ExecuteReader();
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateDataAsync(Entity entity, string tableName)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={_dbPath}"))
            {
                db.Open();
                _sqliteCommand = new SqliteCommand()
                {
                    CommandText = $"UPDATE {tableName} SET Updated=@Updated, ",
                    Connection = db
                };
                _ = _sqliteCommand.Parameters.AddWithValue("@Updated", entity.Updated);
                if (entity is Todo todo)
                {
                    _sqliteCommand.CommandText += "IsCompleted=@IsCompleted, Name=@Name, AlertDate=@AlertDate";
                    _ = _sqliteCommand.Parameters.AddWithValue("@Name", todo.Name);
                    _ = _sqliteCommand.Parameters.AddWithValue("@IsCompleted", Convert.ToInt16(todo.IsCompleted));
                    _ = _sqliteCommand.Parameters.AddWithValue("@AlertDate", todo.AlertDate);
                }
                _sqliteCommand.CommandText += " WHERE Id=@Id;";
                _ = _sqliteCommand.Parameters.AddWithValue("@Id", entity.Id);
                _ = await _sqliteCommand.ExecuteReaderAsync();
                db.Close();
            }
            return true;
        }
    }
}