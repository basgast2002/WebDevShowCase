const connection = new signalR.HubConnectionBuilder().withUrl("/LeaderBoardNotifyHub").build();
connection.start()
    .then(function () {
        console.log("Connection started");
    })
    .catch(function (err) {
        console.error(err.toString());
    });

var button = document.getElementById("submitbutton");

var UserName = document.getElementById("username").value;

document.getElementById("submitbutton").addEventListener("click", function (event) {
    connection.invoke("UpdateConnectedLeaderboard").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("submitbutton").addEventListener(onclick, function (event) {
    console.log("button pressed!");

    const xhr = new XMLHttpRequest();
    const url = 'Game/CoinUp';
    const data = { username: UserName }
    const payload = JSON.stringify(data);

    xhr.open('POST', url);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onload = function () {
    };
    xhr.send();
});