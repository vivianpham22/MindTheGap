var clientId = '564506460648-knam6scga0e8i8ohtfs0246nnl4ffu39.apps.googleusercontent.com';
var apiKey = 'AIzaSyAsOIuikEWJisvhZ-XctdMiikl0v2LUUfw';
var scopes = 'https://www.googleapis.com/auth/calendar';

function handleClientLoad() {
    gapi.client.setApiKey(apiKey);
    window.setTimeout(checkAuth, 1);
    checkAuth();
}

function checkAuth() {
    gapi.auth.authorize({ client_id: clientId, scope: scopes, immediate: true },
        handleAuthResult);
}

function handleAuthResult(authResult) {
    var authorizeButton = document.getElementById('authorize-button');
    if (authResult) {
        authorizeButton.style.visibility = 'hidden';
        makeApiCall();
    } else {
        authorizeButton.style.visibility = 'block';
        authorizeButton.onclick = handleAuthClick;
    }
}

function handleAuthClick(event) {
    gapi.auth.authorize(
        { client_id: clientId, scope: scopes, immediate: false },
        handleAuthResult);
    return false;
}

function makeApiCall() {
    gapi.client.load('calendar', 'v3', function () {
        var request = gapi.client.calendar.events.list({
            'calendarId': 'primary'
        });

        request.execute(function (resp) {
            for (var i = 0; i < resp.items.length; i++) {
                var li = document.createElement('li');
                li.appendChild(document.createTextNode(resp.items[i].summary));
                document.getElementById('events').appendChild(li);
            }
        });
    });
}