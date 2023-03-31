using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Data.Sqlite;
using NewAuthCustomAccountTestEnv.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace NewAuthCustomAccountTestEnv.Controllers
{
    public class GameController : Controller
    {
        private readonly SqliteConnection DatabaseConnection = new("Datasource= AuthDb.db");
        [BindProperty]
        public InputModel Input { get; set; }
        public string? Username { get; set; }
        public IActionResult Game()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CoinUp(string Username)
        {
            if (string.IsNullOrEmpty(Username))
            {
                ModelState.AddModelError(nameof(InputModel.UserName), "Username cannot be null or empty.");
                return BadRequest(ModelState);
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            try
            {
                DbUp(Username);
                return PartialView("Game"); // Return a 200 OK response if the update was successful
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        private IActionResult DbUp(string Username)
        {
            IActionResult statuscode;
            try
            {
                
                // Query parameters.
                int Newcoins = GetCoins(Username);
                if (Newcoins < 0) {
                    throw new Exception("Invalid Operation!");
                }
                DatabaseConnection.Open();
                // Query text incorporated into SQL command.
                var sqliteUpdate = DatabaseConnection.CreateCommand();
                sqliteUpdate.CommandText = @"UPDATE AspNetUsers SET coins = $newcoins WHERE UserName = $username";

                // Bind the parameters to the query.
                sqliteUpdate.Parameters.AddWithValue("$newcoins", Newcoins);
                sqliteUpdate.Parameters.AddWithValue("$username", Username);

                // Execute SQL.
                sqliteUpdate.ExecuteNonQuery();
                statuscode = Ok();

            }
            catch (Exception e)
            {

                
                statuscode = BadRequest();

            }
            finally
            {

                // Close the database connection.
                DatabaseConnection.Close();
                
            }
            return statuscode;
        }

        private int GetCoins(string Username)
        {
            int coins = -200;
            try
            {
                DatabaseConnection.Open();

                SqliteCommand sqliteGet = new("SELECT coins FROM AspNetUsers WHERE UserName = $username ;", DatabaseConnection);
                
                sqliteGet.Parameters.AddWithValue("$username", Username);

                // Execute SQL.
                sqliteGet.ExecuteNonQuery();

                SqliteDataReader r = sqliteGet.ExecuteReader();
                r.Read();
               var test = r.GetValue(0);
                     coins = int.Parse(r.GetInt64(0).ToString());
                
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                DatabaseConnection.Close();
            }
            return coins + 1;

            

        }
        public class InputModel
        {
            [Required]
            [Display(Name = "UserName")]
            public string UserName
            {
                get;
                set;
            }
        }
    }
}