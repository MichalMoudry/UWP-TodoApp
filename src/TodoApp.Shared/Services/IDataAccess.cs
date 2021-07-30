using System.Collections.Generic;
using TodoApp.Shared.Models.Extensions;

namespace TodoApp.Shared.Services
{
    /// <summary>
    /// Interface for accessing data.
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Tries to add a new data row to the database.
        /// </summary>
        /// <param name="entity">New entity.</param>
        /// <param name="tableName">Name of the database table.</param>
        /// <returns>Returns true if row is added, false if error occured.</returns>
        bool AddData(Entity entity, string tableName);

        /// <summary>
        /// Tries to add multiple new data rows to the database.
        /// </summary>
        /// <param name="entities">List of new entities.</param>
        /// <param name="tableName">Name of the database table.</param>
        /// <returns>Returns true if all entities are added, false if error occured.</returns>
        bool AddMultipleEntities(List<Entity> entities, string tableName);

        /// <summary>
        /// Method for deleting a single row in the database.
        /// </summary>
        /// <param name="id">Id of the row.</param>
        /// <param name="tableName">Name of the database table.</param>
        /// <returns>Returns true if row is deleted, false if error occured.</returns>
        bool DeleteData(string id, string tableName);

        /// <summary>
        /// Method for deleting multiple rows in the database.
        /// </summary>
        /// <param name="entities">List of entities that will be deleted.</param>
        /// <param name="tableName">Name of the database table.</param>
        /// <returns>Returns true if rows are deleted, false if error occured.</returns>
        bool DeleteMultipleEntities(List<Entity> entities, string tableName);

        /// <summary>
        /// Method for obtaining list of entities from a specific database table.
        /// </summary>
        /// <param name="tableName">Name of the database table.</param>
        /// <returns>Returns list of entities from a specific database table.</returns>
        List<Entity> GetData(string tableName);

        /// <summary>
        /// Method for obtaining a single entity from a specific database table.
        /// </summary>
        /// <param name="tableName">Name of the database table.</param>
        /// <param name="id">Id of the entity.</param>
        /// <returns>Returns single entity from a specific database table.</returns>
        Entity GetEntity(string tableName, string id);

        /// <summary>
        /// Initializes database.
        /// </summary>
        void InitializeDatabase();

        /// <summary>
        /// Updates row in a specific database table.
        /// </summary>
        /// <param name="entity">Entity with assigned id and new data.</param>
        /// <param name="tableName">Name of the database table.</param>
        /// <returns>True if row is updated, false if error occured.</returns>
        bool UpdateData(Entity entity, string tableName);
    }
}