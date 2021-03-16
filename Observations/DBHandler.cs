using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observations
{
    static class DBHandler
    {

        static SqlConnection connection;
        static SqlCommand command;
        static bool connected;

        static string LoadConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static void Connect()
        {
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = LoadConnectionString("observations");
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                connected = true;
            }
            catch (Exception ex)
            {

                throw new DBHandlerException($"Could not connect to DB ", ex);
            }
        }

        public static void Disconnect()
        {
            try
            {
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                connected = false;
                throw new DBHandlerException($"Could not disconnect from DB ", ex);
            }
        }

        public static void Insert(SpaceObject _new)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO [Observations]([Name], [Seen], [InSolarSystem]) OUTPUT INSERTED.Id, INSERTED.Name VALUES (@Name, @Seen, @InSolarSystem)";
                command.Parameters.AddWithValue("@Name", _new.Name);
                command.Parameters.AddWithValue("@Seen", _new.Seen);
                command.Parameters.AddWithValue("@InSolarSystem", _new.InSolarSystem);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _new.Id = (int)reader["Id"];
                        _new.Name = reader["Name"].ToString();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw new DBHandlerException("Could not insert into DB ", ex);
            }

        }

        public static void Update(SpaceObject _mod)
        {

            try
            {
                command.Parameters.Clear();
                command.CommandText = "UPDATE [Observations] SET [Name] = @Name WHERE [Id] = @Id;";
                command.Parameters.AddWithValue("@Name", _mod.Name);
                command.Parameters.AddWithValue("@Id", _mod.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new DBHandlerException("Coulndt update ", ex);
            }
        }

        public static void Delete(SpaceObject _del)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "DELETE FROM [Observations] WHERE [Id] = @Id";
                command.Parameters.AddWithValue("@Id", _del.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DBHandlerException("Could not delete from DB ", ex);
            }
        }

        public static List<SpaceObject> ReadAllSpaceObjects()
        {
            command.Parameters.Clear();
            command.CommandText = $"SELECT * FROM [Observations]";
            List<SpaceObject> spaceObjects = new List<SpaceObject>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    SpaceObject so = new SpaceObject(
                        (int)reader["Id"],
                        reader["Name"].ToString(),
                        DateTime.Parse(reader["Seen"].ToString()),
                        Convert.ToBoolean(reader["InSolarSystem"])
                        );
                    spaceObjects.Add(so);
                }
                reader.Close();
            }
            return spaceObjects;

        }
    }
}
