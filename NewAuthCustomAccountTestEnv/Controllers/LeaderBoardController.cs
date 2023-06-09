﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using NewAuthCustomAccountTestEnv.Models;

namespace NewAuthCustomAccountTestEnv.Controllers
{
    public class LeaderBoardController : Controller
    {
        #region Fields

        private readonly SqliteConnection DatabaseConnection = new("DataSource=AuthDb.db;");

        #endregion Fields

        #region Public Methods

        public IActionResult LeaderBoard()
        {
            return View(Create());
        }

        #endregion Public Methods

        #region Private Methods

        private List<LeaderboardModel> Create()
        {
            List<LeaderboardModel> ImportedUsers = new();
            int Position = 0;
            DatabaseConnection.Open();
            using (SqliteCommand fmd = DatabaseConnection.CreateCommand())
            {
                fmd.CommandText = @"SELECT username, coins, name, LBPrivacy FROM AspNetUsers ORDER BY coins DESC;";
                SqliteDataReader r = fmd.ExecuteReader();
                while (r.Read())
                {
                    if ((long)r["LBPrivacy"] == (long)1)
                    {
                        string Username = (string)r["username"];
                        long Coins = (long)r["coins"];

                        Position++;
                        ImportedUsers.Add(new LeaderboardModel(Position, Username, Coins));
                    }
                }

                DatabaseConnection.Close();
            }

            return ImportedUsers;
        }

        #endregion Private Methods
    }
}