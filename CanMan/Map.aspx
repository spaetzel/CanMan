<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="CanMan.Map" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script> 
<script type="text/javascript" src="http://code.google.com/apis/gears/gears_init.js"></script> 

<script type="text/javascript"> 

var routePoints = [<asp:Literal id="routePointsLiteral" runat="server"/>];
 
var initialLocation;
var lastWindow;

var browserSupportFlag =  new Boolean();
var map;
var infowindow = new google.maps.InfoWindow();
  
function initializeMap() {
    var myOptions = {
        zoom: 14,
        center: <asp:Literal id="midPointLiteral" runat="server"/>,
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

    
    <asp:Literal id="setPointsLiteral" runat="server"/>
});
</script> 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pageHeader">
    <asp:HyperLink ID="userLink" runat="server">
        <asp:Image ID="userImage" runat="server" ClientIDMode="Static"/>
    </asp:HyperLink>
    <h2 id="heading" runat="server">Map</h2>
    </div>

    <div id="map_canvas"></div>
    <p>Using the <a href="http://www.yellowapi.ca">YellowApi</a> to display nearby <asp:Label ID="categoriesLabel" runat="server" /></p>
</asp:Content>
