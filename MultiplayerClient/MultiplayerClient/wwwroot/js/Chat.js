var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().then(function (ans) {
    console.log(ans);

}).catch(function (err) {
        return console.error(err.toString());


 });

//    document.getElementById("sendButton").addEventListener("click", function (event) {
//    var getter = document.getElementById('userInput').value;

//        connection.invoke("AddToGroup", getter).catch(function (err) {
//            return console.error(err.toString());

//        });
       
//});


document.getElementById("removeButton").addEventListener("click", function (event) {
    var getter2 = document.getElementById('userInput').value;
    
    connection.invoke("RemoveFromGroup", getter2).catch(function (err) {
        return console.error(err.toString());

    });

});

document.getElementById("sendrealmessage").addEventListener("click", function (event) {
    var getter2 = document.getElementById('userInput').value;
    var getter3 = document.getElementById('messageInput').value;

    connection.invoke("messageSend", getter2, getter3).catch(function (err) {
        return console.error(err.toString());

    });

});



connection.on("SendR", function (ans5) {

    console.log(ans5);

});



        connection.on("Send", function (ans5) {

            console.log(ans5);

        });








connection.on("newMessage", function (ans5) {

    console.log(ans5);

});
   


document.getElementById("multiConnect").addEventListener("click", function (event) {

    connection.invoke("GetSignalRConnectionInfo").catch(function (err) {
        
        return console.error(err.toString());

    });
    event.preventDefault();

});

document.getElementById("sender").addEventListener("click", function (event) {
    // userId
    var getter2 = document.getElementById('userInput').value;

    // groupname
    var setter = document.getElementById('messageInput').value;
    var getter3 = {
        group_name: setter
    }; 

    connection.invoke("sendThis", getter2, getter3).catch(function (err) {
        return console.error(err.toString());

    });
    event.preventDefault();
         

});  






document.getElementById("addtogroup").addEventListener("click", function (event) {
    //userId
    var getter2 = document.getElementById('userInput').value;

    // groupname
    var setter = document.getElementById('messageInput').value;
    var getter3 = {
        group_name: setter
    }; 

    connection.invoke("addtogroup", getter2, getter3).catch(function (err) {
        return console.error(err.toString());

    });
    event.preventDefault();

});


connection.on("ReceiveFromAdd", function (ans5) {

    console.log(ans5);

});




connection.on("ReceiveMessage3", function (ans5) {

    console.log(ans5);

});



connection.on("newMessageagain", function (ans5) {

    console.log(ans5);

});


var connection2;
connection.on("ReceiveMessage2", function (ans) {
            
    connection2 = new signalR.HubConnectionBuilder()

        .withUrl(ans.url, { accessTokenFactory: () => ans.accessToken })
        .build();


        connection2.start().then(function () {


            console.log("connected");

            connection2.on("newMessage", function (ans5) {

                console.log(ans5);

            });

            connection2.on("ReceiveMessage3", function (ans5) {

                console.log(ans5);

            });

            connection2.on("ReceiveFromAdd", function (ans5) {

                console.log(ans5);

            });






        }).catch(function (err) {
            return console.error(err.toString());

        });

      
         
});