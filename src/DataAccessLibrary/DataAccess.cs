using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using TodoApp.Shared.Models.Extensions;
using TodoApp.Shared.Services;
using Windows.Storage;
using TodoApp.Shared.Enums;
using TodoApp.Shared.Models.Entity;

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
        public bool AddData(Entity entity, string tableName)
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
                    _ = insertCommand.ExecuteReader();
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
        public bool AddMultipleEntities(List<Entity> entities, string tableName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool DeleteData(string id, string tableName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool DeleteMultipleEntities(List<Entity> entities, string tableName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public List<Entity> GetData(string tableName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Entity GetEntity(string tableName, string id)
        {
            throw new NotImplementedException();
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
        public bool UpdateData(Entity entity, string tableName)
        {
            throw new NotImplementedException();
        }
    }
}