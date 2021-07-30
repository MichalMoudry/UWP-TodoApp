using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;

namespace DataAccessLibrary
{
    public static class DataAccess
    {
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("todoDatabase.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoDatabase.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                //String tableCommand = "CREATE TABLE IF NOT " +
                    //"EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    //"Text_Entry NVARCHAR(2048) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
    }
}