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
        #region Fields

        private readonly SqliteConnection _databaseConnection = new("Datasource= AuthDb.db");

        private readonly string[] _images = {
            "/Images/Cursed_Coins_Gem.png",
            "/Images/pirate-ship.png",
            "/Images/Cursed_Coins_Curse.png",
            "/Images/storm.png",
            "/Images/fish.png",
            "/Images/chicken-leg.png",
            "/Images/dollar.png" };

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private GameModel? _gameValues;

        #endregion Fields

        #region Public Constructors

        public GameController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #endregion Public Constructors

        #region Public Methods

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
                if (user != null)
                {
                    this._gameValues = GetGameValues();
                    if (_gameValues.CoinsEarned < 0)
                    {
                        return Game();
                    }
                    if (user.UserName != null)
                    {
                        DbUp(user.UserName, this._gameValues.CoinsEarned);
                    }
                    return Game();
                }
                throw new UnauthorizedAccessException();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Game()
        {
            if (_gameValues is null)
            {
                GameModel TempGameModel = new(_images[2], _images[2], _images[2], 0);
                return View("Game", TempGameModel);
            }
            return View("Game", this._gameValues);
        }

        public GameModel GetGameValues()
        {
            string temp1 = GetImage();
            string temp2 = GetImage();
            string temp3 = GetImage();

            int tempi = CalculatePayout(temp1, temp2, temp3);

            return new GameModel(temp1, temp2, temp3, tempi);
        }

        public string GetImage()
        {
            int minValue = 0;
            int maxValue = 115;
            int randomNumber = RandomNumberGenerator.GetInt32(minValue, maxValue);

            int result = (int)(Math.Abs(randomNumber) % (maxValue - minValue)) + minValue;
            if (result < 0 || result > 115)
            {
                //chance values contaminated
                if (_userManager.GetUserAsync(User).Result != null)
                {
                    _userManager.GetUserAsync(User).Result.AccessFailedCount++;
                    _userManager.GetUserAsync(User).Result.Coins = 1;
                }
                _signInManager.SignOutAsync();
                throw new Exception("nie best, log op nieuw in");
            }
            else
            {
                switch (result)
                {
                    case <= 5:
                        return _images[0];

                    case > 5 and <= 18:
                        return _images[1];

                    case > 18 and <= 29:
                        return _images[2];

                    case > 29 and <= 40:
                        return _images[3];

                    case > 40 and <= 60:
                        return _images[4];

                    case > 60 and <= 100:
                        return _images[5];

                    case > 100 and <= 115:
                        return _images[6];
                }
            }
            throw new FileNotFoundException("no image found");
        }

        private int CalculatePayout(string img1, string img2, string img3)
        {
            int payout = 0;

            if (img1 == _images[6] || img1 == _images[5] || img1 == _images[4])
            {
                payout++;
            }
            if (img2 == _images[6] || img2 == _images[5] || img2 == _images[4])
            {
                payout++;
            }
            if (img3 == _images[6] || img3 == _images[5] || img3 == _images[4])
            {
                payout++;
            }

            if (img1 == img2 && img2 == img3 && img3 == img1)
            {
                if (img1 == _images[0])
                {
                    payout += 1000;
                }
                if (img1 == _images[1])
                {
                    payout += -20;
                }
                if (img1 == _images[2])
                {
                    if (_userManager.GetUserAsync(User).Result != null)
                    {
                        payout += -_userManager.GetUserAsync(User).Result.Coins;
                    }
                    else
                    {
                        payout = -200;
                    }
                }
                if (img1 == _images[3])
                {
                    payout += 50;
                }
                if (img1 == _images[4])
                {
                    payout += 12;
                }
                if (img1 == _images[5])
                {
                    payout += 7;
                }
                if (img1 == _images[6])
                {
                    payout += 2;
                }
            }

            return payout;
        }

        #endregion Public Methods

        #region Private Methods

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

        #endregion Private Methods

        #region Classes

        private class InputModel
        {
            #region Properties

            [Required]
            [Display(Name = "UserName")]
            public string UserName
            {
                get;
                set;
            } = string.Empty;

            #endregion Properties
        }

        #endregion Classes
    }
}