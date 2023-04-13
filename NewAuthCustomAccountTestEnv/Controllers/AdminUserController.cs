using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using NewAuthCustomAccountTestEnv.Models;

namespace NewAuthCustomAccountTestEnv.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly SqliteConnection DatabaseConnection = new("DataSource=AuthDb.db;");

        public IActionResult UserManagementIndex()
        {
            return View("UserManagementIndex", ImportUsers());
        }

        [HttpPost]
        public IActionResult UserManagerEdit(string id)
        {
            if (!ValidateId(id))
            {
                return BadRequest();
            }

            UserModel selecteduser = GetUserById(id);
            return View(selecteduser);
        }

        [HttpPost]
        public IActionResult UserManagerDetails(string id)
        {
            if (!ValidateId(id))
            {
                return BadRequest();
            }

            UserModel selecteduser = GetUserById(id);
            return View(selecteduser);
        }

        [HttpPost]
        public IActionResult UserManagerDelete(string id)
        {
            if (!ValidateId(id))
            {
                return BadRequest();
            }

            UserModel selecteduser = GetUserById(id);
            return View(selecteduser);
        }

        [HttpPost]
        public IActionResult UserManagerUpdate(string Id, string name, string username, string email, int coins, int afc, bool isadmin)
        {
            UserModel user = new(Id, name, username, email, coins, isadmin, afc);
            DatabaseConnection.Open();
            using (SqliteCommand fmd = DatabaseConnection.CreateCommand())
            {
                fmd.CommandText = @"UPDATE AspNetUsers SET Name = $name, Coins = $coins, IsAdmin = $isadmin,  UserName = $username, AccessFailedCount = $afc, Email = $email WHERE id = $id;";

                fmd.Parameters.AddWithValue("$coins", user.Coins);
                fmd.Parameters.AddWithValue("$username", user.Username);
                fmd.Parameters.AddWithValue("$name", user.Name);
                fmd.Parameters.AddWithValue("$id", user.Id);
                fmd.Parameters.AddWithValue("$isadmin", user.IsAdmin);
                fmd.Parameters.AddWithValue("$afc", user.Failedloginattempts);
                fmd.Parameters.AddWithValue("$email", user.Email);

                var testvar = fmd.ExecuteNonQuery();

                DatabaseConnection.Close();
            }
            return UserManagementIndex();
        }

        [HttpPost]
        [HttpDelete]
        public IActionResult UserManagerDeleteUser(string Id, string name, string email)
        {
            DatabaseConnection.Open();
            using (SqliteCommand fmd = DatabaseConnection.CreateCommand())
            {
                fmd.CommandText = @"DELETE FROM AspNetUsers WHERE Id = $id AND Name = $name AND Email = $email;";

                fmd.Parameters.AddWithValue("$name", name);
                fmd.Parameters.AddWithValue("$id", Id);
                fmd.Parameters.AddWithValue("$email", email);

                var testvar = fmd.ExecuteNonQuery();

                DatabaseConnection.Close();
            }
            return UserManagementIndex();
        }

        private bool ValidateId(string id)
        {
            if (ImportUsers() == null || ImportUsers().FirstOrDefault(a => a.Id == id) == null)
            {
                return false;
            }

            return true;
        }

        private UserModel GetUserById(string id)
        {
            return ImportUsers().FirstOrDefault(a => a.Id == id) ?? new(id, "", "", "", 0, false, 0);
        }

        private List<UserModel> ImportUsers()
        {
            List<UserModel> ImportedUsers = new();
            DatabaseConnection.Open();
            using (SqliteCommand fmd = DatabaseConnection.CreateCommand())
            {
                fmd.CommandText = @"SELECT Id, Name, Coins, IsAdmin, UserName, Email, AccessFailedCount FROM AspNetUsers;";
                SqliteDataReader r = fmd.ExecuteReader();
                while (r.Read())
                {
                    string Id = (string)r["Id"];
                    int AFC = int.Parse(((long)r["AccessFailedCount"]).ToString());
                    string Name = (string)r["Name"];
                    string Username = (string)r["username"];
                    string Email = (string)r["Email"];
                    int Coins = int.Parse(((long)r["Coins"]).ToString());
                    long IsAdmin = (long)r["IsAdmin"];
                    bool isAdmin = Istrue(IsAdmin);

                    ImportedUsers.Add(new UserModel(Id, Name, Username, Email, Coins, isAdmin, AFC));
                }

                DatabaseConnection.Close();
            }

            return ImportedUsers;
        }

        private static bool Istrue(long input)
        {
            bool result = false;
            switch (input)
            {
                case 0: result = false; break;
                case > 0: result = true; break;
            }
            return result;
        }
    }
}