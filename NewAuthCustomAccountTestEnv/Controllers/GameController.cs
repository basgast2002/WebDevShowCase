using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using NewAuthCustomAccountTestEnv.Data;
using NewAuthCustomAccountTestEnv.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace NewAuthCustomAccountTestEnv.Controllers
{
    public class GameController : Controller
    {
        private readonly SqliteConnection _databaseConnection = new("Datasource= AuthDb.db");
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private GameModel _gameValues;

        public GameController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Game()
        {
            if (_gameValues is null)
            {
                return View("Game");
            }
            return View("Game", this._gameValues);
        }

        [HttpPost]
        [EnableCors("CorsPolicy")]
        public IActionResult CoinUp()
        {
            ApplicationUser? user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                //throw new Exception();
                return NotFound();
            }
            if (User.Identity is null)
            {
                //throw new IdentityNotMappedException();
                return BadRequest();
            }

            if (!_signInManager.IsSignedIn(User))
            {
                //throw new UnauthorizedAccessException();
                return Unauthorized();
            }

            try
            {
                this._gameValues = GetGameValues();
                DbUp(user.UserName, this._gameValues.CoinsEarned);

                return Game();

                //return Ok();
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }

        private void DbUp(string Username, int ExtraCoins)
        {
            IActionResult statuscode;
            try
            {
                // Query parameters.
                int Newcoins = GetCoins(Username) + ExtraCoins;
                if (Newcoins < 0)
                {
                    throw new InvalidOperationException();
                }
                _databaseConnection.Open();
                // Query text incorporated into SQL command.
                var sqliteUpdate = _databaseConnection.CreateCommand();
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
                throw new BadHttpRequestException(e.Message);
            }
            finally
            {
                // Close the database connection.
                _databaseConnection.Close();
            }
        }

        private int GetCoins(string Username)
        {
            int coins = -1;
            try
            {
                _databaseConnection.Open();

                SqliteCommand sqliteGet = new("SELECT coins FROM AspNetUsers WHERE UserName = $username ;", _databaseConnection);

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
                _databaseConnection.Close();
            }
            return coins - 1;
        }

        public GameModel GetGameValues()
        {
            string temp1 = GetImage();
            string temp2 = GetImage();
            string temp3 = GetImage();

            return new GameModel(temp1, temp2, temp3, 0);
        }

        public string GetImage()
        {
            int minValue = 0;
            int maxValue = 115;
            byte[] randomBytes = new byte[4]; // choose the number of bytes you want
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            int randomNumber = BitConverter.ToInt32(randomBytes, 0);
            int result = (int)(Math.Abs(randomNumber) % (maxValue - minValue)) + minValue;
            string[] images = { "/Images/Cursed_Coins_Gem.png", "/Images/pirate-ship.png", "/Images/Cursed_Coins_Curse.png", "/Images/storm.png", "/Images/fish.png", "/Images/chicken-leg.png", "/Images/dollar.png" };
            switch (result)
            {
                case <= 5:
                    return images[0];

                case > 5 and <= 18:
                    return images[1];

                case > 18 and <= 29:
                    return images[2];

                case > 29 and <= 40:
                    return images[3];

                case > 40 and <= 60:
                    return images[4];

                case > 60 and <= 100:
                    return images[5];

                case > 100 and <= 115:
                    return images[6];
            }
            return "400";
        }

        private class InputModel
        {
            [Required]
            [Display(Name = "UserName")]
            public string UserName
            {
                get;
                set;
            } = string.Empty;
        }
    }
}