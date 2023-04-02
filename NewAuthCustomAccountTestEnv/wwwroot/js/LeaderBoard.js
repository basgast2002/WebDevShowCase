

var connection = new signalR.HubConnectionBuilder().withUrl("/LeaderBoardNotifyHub").build();

connection.on("UpdateLeaderBoard", function () {
    //AJAX post call naar update leaderboard
    console.log("iets ontvangen tenminste");
    window.location.reload();
});

connection.start()
    .then(function () {
        console.log("Connection started");
    })
    .catch(function (err) {
        console.error(err.toString());
    });

