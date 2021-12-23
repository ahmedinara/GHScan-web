   
        var markers = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.Branch));
        /**/
        var myArray = [];
        var obj;

        window.onload = function () {
            fetch('https://ahmedinara00-001-site1.dtempurl.com/api/branchlocation/GetBranchesLocation')
                .then(res => res.json())
                .then(data => obj = data)
                .then(() => console.log(obj))
              @*@foreach (var d in Model.Branch)
    {
        @:myArray.push("@d");
    }
            alert(myArray[0].lng);*@

                alert(markers[0].lng);

                var mapOptions = {
                    center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                    zoom: 8,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                var infoWindow = new google.maps.InfoWindow();
                var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
                for (i = 0; i < markers.length; i++) {

                    var data = markers[i]
                    var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                    var marker = new google.maps.Marker({
                        position: myLatlng,
                        map: map,
                        title: data.title
                    });
                    (function (marker, data) {
                        google.maps.event.addListener(marker, "click", function (e) {
                            infoWindow.setContent(data.descriptionEn);
                            infoWindow.open(map, marker);
                        });
                    })(marker, data);
                }
            }
