using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using NewAuthCustomAccountTestEnv.Models;

namespace NewAuthCustomAccountTestEnv.Controllers
{
    public class LeaderBoardController : Controller
    {
        private readonly SqliteConnection DatabaseConnection = new("DataSource=AuthDb.db;");

        public IActionResult LeaderBoard()
        {
            return View(Create());
        }

       

        private List<LeaderboardModel> Create()
        {
            List<LeaderboardModel> ImportedUsers = new List<LeaderboardModel>();
            int Position = 0;
            DatabaseConnection.Open();
            using (SqliteCommand fmd = DatabaseConnection.CreateCommand())
            {
                fmd.CommandText = @"SELECT username, coins FROM AspNetUsers ORDER BY coins DESC;";
                SqliteDataReader r = fmd.ExecuteReader();
                while (r.Read())
                {
                    string Username = (string)r["username"];
                    long Coins = (long)r["coins"];
                    Position++;
                    ImportedUsers.Add(new LeaderboardModel(Position, Username, Coins));
                }

                DatabaseConnection.Close();
            }

            return ImportedUsers;
        }
    }
}