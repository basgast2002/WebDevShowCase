const connection = new signalR.HubConnectionBuilder().withUrl("/LeaderBoardNotifyHub").build();
connection.start()
    .then(function () {
        console.log("Connection started");
    })
    .catch(function (err) {
        console.error(err.toString());
    });
var button = document.getElementById("submitbutton");
button.addEventListener("click", function (event) {
    connection.invoke("UpdateConnectedLeaderboard").catch(function (err) {
        return console.error(err.toString());
    });
    console.log("button pressed");
});
function timeout() {
    button.disabled = true;
    setTimeout(function () {
        button.disabled = false;
    }, 2000);
}

var UserName = document.getElementById("username").value;

button.onload = timeout(button);