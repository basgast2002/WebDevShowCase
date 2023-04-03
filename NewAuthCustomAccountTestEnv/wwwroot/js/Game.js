const connection = new signalR.HubConnectionBuilder().withUrl("/LeaderBoardNotifyHub").build();
connection.start()
    .then(function () {
        console.log("Connection started");
    })
    .catch(function (err) {
        console.error(err.toString());
    });

document.getElementById("submitbutton").addEventListener("click", function (event) {
    connection.invoke("UpdateConnectedLeaderboard").catch(function (err) {
        return console.error(err.toString());
    });
    console.log("button pressed!")
});

/*
const GameModule = (function () {
    class Game {
        constructor() {
        }
        ClickButton() {
            const coinUpForm = document.getElementById('coin-up-form');

            coinUpForm.addEventListener('submit', (event) => {
                event.preventDefault();

                const username = document.getElementById('username').value;

                fetch('/Game/CoinUp', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(username)
                })
                    .then(response => {
                        if (response.ok) {
                            console.log('Coins updated successfully.');
                        } else {
                            throw new Error('Failed to update coins.');
                        }
                    })
                    .catch(error => {
                        console.error(error);
                    });
            });
        }
    }

    const GameMod = new Game();

    return {
        init: function () {
            GameMod.ClickButton();
        }
    }
})();

GameMod.init();
*/