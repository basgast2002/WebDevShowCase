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
            return View(ImportUsers());
        }

        [HttpPost]
        public IActionResult UserManagerEdit(string id)
        {
            if (ImportUsers() == null || ImportUsers().FirstOrDefault(a => a.Id == id) == null)
            {
                return BadRequest();
            }

            UserModel selecteduser = ImportUsers().FirstOrDefault(a => a.Id == id) ?? new(id, "", "", "", 0, false, 0);
            return View(selecteduser);
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
                    long AFC = (long)r["AccessFailedCount"];
                    string Name = (string)r["Name"];
                    string Username = (string)r["username"];
                    string Email = (string)r["Email"];
                    long Coins = (long)r["coins"];
                    long IsAdmin = (long)r["IsAdmin"];
                    bool isAdmin = Istrue(IsAdmin);

                    ImportedUsers.Add(new UserModel(Id, Name, Username, Email, int.Parse(Coins.ToString()), isAdmin, int.Parse(AFC.ToString())));
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