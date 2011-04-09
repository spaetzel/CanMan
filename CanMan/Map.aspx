<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="CanMan.Map" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script> 
<script type="text/javascript" src="http://code.google.com/apis/gears/gears_init.js"></script> 

<script type="text/javascript"> 

var routePoints = [<asp:Literal id="routePointsLiteral" runat="server"/>];
 
var initialLocation;
var siberia = new google.maps.LatLng(60, 105);
var newyork = new google.maps.LatLng(40.69847032728747, -73.9514422416687);
var browserSupportFlag =  new Boolean();
var map;
var infowindow = new google.maps.InfoWindow();
  
function initializeMap() {
    var myOptions = {
        zoom: 8,
        center: routePoints[0],
    mapTypeId: google.maps.MapTypeId.ROADMAP
  };
  map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

  return map;
}



$(function () {
    var map = initializeMap();

    var xml = unescape($('#gpx').val());

    
    var route = new google.maps.Polyline({
    path: routePoints,
    strokeColor: "#FF0000",
    strokeOpacity: 1.0,
    strokeWeight: 2
  });

  route.setMap(map);

});
</script> 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Map</h2>
    
    <div id="map_canvas" width="500px" height="500px"></div>
</asp:Content>
