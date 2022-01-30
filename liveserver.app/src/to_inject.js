(function() {
    var url = 'ws://localhost:80/';

    function init() {
        doWebSocket();
    }

    function doWebSocket() {
        websocket = new WebSocket(url);

        websocket.onopen = function(e) {
            onOpen(e);
        };

        websocket.onmessage = function(e) {
            onMessage(e);
        };
    }

    function onOpen(event) {
        writeToScreen('CONNECTED');
        send('WebSocket rocks');
    }

    function onMessage(event) {
        writeToScreen('RECEIVE: ' + event.data);
    }

    function send(message) {
        websocket.send(message);
    }

    function writeToScreen(message) {
        var pre = document.createElement('p');
        pre.innerHTML = message;
        document.body.appendChild(pre);
    }

    window.addEventListener('load', init, false);
})();
