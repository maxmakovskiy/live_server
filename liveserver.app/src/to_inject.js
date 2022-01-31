(function() {
    var websocket = new WebSocket('ws://localhost:80/'); 

    function init() {
        doWebSocket();
    }

    function doWebSocket() {
        websocket.onopen = function(e) {
            onOpen(e);
        };

        websocket.onmessage = function(e) {
            onMessage(e);
        };
    }

    function onOpen(event) {
        console.log('CONNECTED');
    }

    function onMessage(event) {
        if (event.data === 'changed') {
            websocket.close();
            location.reload();
        }
        console.log('RECEIVE: ' + event.data);
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
