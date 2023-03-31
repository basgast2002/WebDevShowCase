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