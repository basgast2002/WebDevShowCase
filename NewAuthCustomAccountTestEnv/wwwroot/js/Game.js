const connection = new signalR.HubConnectionBuilder().withUrl("/LeaderBoardNotifyHub").build();
connection.start()
    .then(function () {
        console.log("Connection started");
    })
    .catch(function (err) {
        //console.error(err.toString());
    });
const button = document.getElementById("submitbutton");
button.addEventListener("click", function (event) {
    connection.invoke("UpdateConnectedLeaderboard").catch(function (err) {
        return console.error(err.toString());
    });
    // console.log("button pressed");
});
function timeout() {
    button.disabled = true;
    setTimeout(function () {
        button.disabled = false;
    }, 2000);
}

const UserName = document.getElementById("username").value;

button.onload = timeout(button);

const slideBox1 = document.getElementById('scroller1');
const slideBox2 = document.getElementById('scroller2');
const slideBox3 = document.getElementById('scroller3');

setTimeout(function () {
    slideBox1.style.display = 'none';
    slideBox2.style.display = 'none';
    slideBox3.style.display = 'none';

    document.getElementById("result1").style.display = "block";
    document.getElementById("result2").style.display = "block";
    document.getElementById("result3").style.display = 'block';

    document.getElementById("payoutbanner").style.display = 'flex';
}, 5000);