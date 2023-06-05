using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using NewAuthCustomAccountTestEnv.Data;
using NewAuthCustomAccountTestEnv.Models;

namespace NewAuthCustomAccountTestEnv.Controllers
{
    public class AdminUserController : Controller
    {
        #region Fields

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SqliteConnection DatabaseConnection = new("DataSource=AuthDb.db;");

        #endregion Fields

        #region Public Constructors

        public AdminUserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        public IActionResult AdminPowerLog()
        {
            if (User.IsInRole("Admin"))
            {
                return View("AdminPowerLog", ImportLog());
            }
            throw new UnauthorizedAccessException();
        }

        public IActionResult UserManagementIndex()
        {
            if (User.IsInRole("Admin"))
            {
                return View("UserManagementIndex", ImportUsers());
            }
            throw new UnauthorizedAccessException();
        }

        [HttpPost]
        public IActionResult UserManagerDelete(string id)
        {
            if (!ValidateId(id))
            {
                return BadRequest(ModelState);
            }

            UserModel selecteduser = GetUserById(id);
            return View(selecteduser);
        }

        [HttpDelete]
        public IActionResult UserManagerDeleteUser(string Id, string name, string email, string Actor)
        {
            if (LogPowerAction("DELETE", Id))
            {
                DatabaseConnection.Open();
                using (SqliteCommand fmd = DatabaseConnection.CreateCommand())
                {
                    fmd.CommandText = @"DELETE FROM AspNetUsers WHERE Id = $id AND Name = $name AND Email = $email;";

                    fmd.Parameters.AddWithValue("$name", name);
                    fmd.Parameters.AddWithValue("$id", Id);
                    fmd.Parameters.AddWithValue("$email", email);

                    fmd.ExecuteNonQuery();

                    DatabaseConnection.Close();
                }
            }
            return UserManagementIndex();
        }

        [HttpPost]
        public IActionResult UserManagerDetails(string id)
        {
            if (User.IsInRole("Admin"))
            {
                if (!ValidateId(id))
                {
                    return BadRequest();
                }

                UserModel selecteduser = GetUserById(id);
                return View(selecteduser);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        [HttpPost]
        public IActionResult UserManagerEdit(string id)
        {
            if (User.IsInRole("Admin"))
            {
                if (!ValidateId(id))
                {
                    return BadRequest();
                }

                UserModel selecteduser = GetUserById(id);
                return View(selecteduser);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        [HttpPost]
        public IActionResult UserManagerUpdate(string Id, string name, string username, string email, int coins, int Failedloginattempts, bool isadmin)
        {
            if (LogPowerAction("UPDATE", Id))
            {
                UserModel user = new(Id, name, username, email, coins, isadmin, Failedloginattempts);
                DatabaseConnection.Open();
                using (SqliteCommand fmd = DatabaseConnection.CreateCommand())
                {
                    fmd.CommandText = @"UPDATE AspNetUsers SET Name = $name, Coins = $coins, IsAdmin = $isadmin,  UserName = $username, AccessFailedCount = $Failedloginattempts, Email = $email WHERE id = $id;";

                    fmd.Parameters.AddWithValue("$coins", user.Coins);
                    fmd.Parameters.AddWithValue("$username", user.Username);
                    fmd.Parameters.AddWithValue("$name", user.Name);
                    fmd.Parameters.AddWithValue("$id", user.Id);
                    fmd.Parameters.AddWithValue("$isadmin", user.IsAdmin);
                    fmd.Parameters.AddWithValue("$Failedloginattempts", user.Failedloginattempts);
                    fmd.Parameters.AddWithValue("$email", user.Email);

                    fmd.ExecuteNonQuery();

                    DatabaseConnection.Close();
                }
            }
            return UserManagementIndex();
        }

        #endregion Public Methods

        #region Private Methods

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

        private UserModel GetUserById(string id)
        {
            return ImportUsers().FirstOrDefault(a => a.Id == id) ?? new(id, "", "", "", 0, false, 0);
        }

        private List<AdminPowerLogModel> ImportLog()
        {
            List<AdminPowerLogModel> ImportedLogs = new();
            DatabaseConnection.Open();
            using (SqliteCommand fmd = DatabaseConnection.CreateCommand())
            {
                fmd.CommandText = @"SELECT ID, Actor, Operation, TargetUser, Date FROM AdminPowerLog;";
                SqliteDataReader r = fmd.ExecuteReader();
                while (r.Read())
                {
                    string Id = (string)r["ID"];
                    string ActorId = (string)r["Actor"];
                    string Operation = (string)r["Operation"];
                    string Target = (string)r["TargetUser"];
                    string Date = (string)r["Date"];
                    ImportedLogs.Add(new AdminPowerLogModel(Id, ActorId, Operation, Target, Date));
                }

                DatabaseConnection.Close();
            }

            return ImportedLogs;
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

        private bool LogPowerAction(string operationType, string targetuser)
        {
            string? actor = _userManager.GetUserAsync(User).Result?.Id;
            if (string.IsNullOrEmpty(actor))
            {
                return false;
            }

            try
            {
                DatabaseConnection.Open();
                using (SqliteCommand fmd = DatabaseConnection.CreateCommand())
                {
                    fmd.CommandText = "INSERT INTO AdminPowerLog (ID, Actor, Operation, TargetUser, Date) VALUES ($ID, $actor, $Operation, $targetuser, $Date);";
                    string ID = new Random().Next() + "" + new Random().Next() + "temporary"; //TO DO: implement auto increment!!!
                    fmd.Parameters.AddWithValue("$ID", ID);
                    fmd.Parameters.AddWithValue("$Operation", operationType);
                    fmd.Parameters.AddWithValue("$actor", actor);
                    fmd.Parameters.AddWithValue("$targetuser", targetuser);
                    fmd.Parameters.AddWithValue("$Date", DateTime.Now.ToString());

                    var testvar = fmd.ExecuteNonQuery();

                    DatabaseConnection.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ValidateId(string id)
        {
            if (ImportUsers() == null || ImportUsers().FirstOrDefault(a => a.Id == id) == null)
            {
                return false;
            }

            return true;
        }

        #endregion Private Methods
    }
}