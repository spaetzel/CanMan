<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="CanMan.Map" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script> 
<script type="text/javascript" src="http://code.google.com/apis/gears/gears_init.js"></script> 
<script type="text/javascript" src="Scripts/loadgpx.4.js"></script>
<script type="text/javascript"> 
 
var initialLocation;
var siberia = new google.maps.LatLng(60, 105);
var newyork = new google.maps.LatLng(40.69847032728747, -73.9514422416687);
var browserSupportFlag =  new Boolean();
var map;
var infowindow = new google.maps.InfoWindow();
  
function initializeMap() {
    var myLatlng = new google.maps.LatLng(43,-80);
    var myOptions = {
        zoom: 8,
        center: myLatlng,
    mapTypeId: google.maps.MapTypeId.ROADMAP
  };
  map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

  return map;
}



$(function () {
    var map = initializeMap();

    var xml = unescape($('#gpx').val());

    
    parser = new GPXParser(xml, map);
    parser.SetTrackColour("#ff0000"); 				// Set the track line colour
    parser.SetTrackWidth(5); 						// Set the track line width
    parser.SetMinTrackPointDelta(0.001); 			// Set the minimum distance between track points
    parser.CenterAndZoom(xml, G_NORMAL_MAP); // Center and Zoom the map over all the points.
    parser.AddTrackpointsToMap(); 					// Add the trackpoints
    parser.AddWaypointsToMap(); 						// Add the waypoints
  
});
</script> 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:HiddenField ID="gpx" ClientIDMode="Static" runat="server" />
    <h2>Map</h2>
    
    <div id="map_canvas" width="500px" height="500px"></div>
</asp:Content>
