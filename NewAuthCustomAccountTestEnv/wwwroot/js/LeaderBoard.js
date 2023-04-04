const connection = new signalR.HubConnectionBuilder().withUrl("/LeaderBoardNotifyHub").build();

connection.on("UpdateLeaderBoard", function () {
    //console.log("iets ontvangen tenminste");
    window.location.reload();
});

connection.start()
    .then(function () {
        //wanneer connectie gelukt is: log op console
        console.log("Connection started");
        const date = new Date();
        document.getElementById("latest-update-label").innerHTML = "Latest Sync: " + date.getHours() + ':' + date.getMinutes() + '/' + date.toLocaleDateString();
    })
    .catch(function (err) {
        //wanneer connectie faalt: log de error op console
        console.error(err.toString());
    });