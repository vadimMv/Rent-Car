// embedded google map & focusing to location
var address = new google.maps.LatLng(31.8068407, 34.6440013);
function initialize() {
    var mapProperties = {
        center: address,
        zoom: 17,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    var map = new google.maps.Map(document.getElementById("map"), mapProperties);

    var marker = new google.maps.Marker({
        position: address,
    });

    marker.setMap(map);
}
google.maps.event.addDomListener(window, 'load', initialize);