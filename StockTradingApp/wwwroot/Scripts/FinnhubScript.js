$(document).ready(function () {
    var token = $("#FinnhubToken").val();
    var stockSymbol = $("#StockSymbol").val();

    var socket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);

    //if connection is open then subscribe to stock updates
    socket.addEventListener("open", function () {
        socket.send(JSON.stringify({ type: "subscribe", symbol: stockSymbol }));
    });

    //listen for incoming messages from finnhub (stock price updates)
    socket.addEventListener("message", function (event) {
        var eventData = JSON.parse(event.data);

        //check if an error message is received
        if (eventData.type === "error") {
            $(".price").text(eventData.msg);
            return;
        }

        //check if stock data is received
        if (eventData.data && eventData.data.length > 0) {
            var updatedPrice = eventData.data[0].p;

            //update the UI with the new stock price
            $(".price").text(updatedPrice.toFixed(2));
        }
    });

    //function to unsubscribe from stock updates
    function unsubscribe(symbol) {
        socket.send(JSON.stringify({ type: "unsubscribe", symbol: symbol }));
    }

    //unsubscribe when the page is closed
    $(window).on("unload", function () {
        unsubscribe(stockSymbol);
    });

});