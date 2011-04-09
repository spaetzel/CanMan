<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Routes.aspx.cs" Inherits="CanMan.Routes" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="pageHeader">
    <asp:HyperLink ID="userLink" runat="server">
        <asp:Image ID="userImage" runat="server" ClientIDMode="Static"/>
    </asp:HyperLink>
    <h2 id="heading" runat="server">Map</h2>
    </div>
    <asp:ListView ID="routesList" runat="server">
        <LayoutTemplate>
            <ul>
                <li id="itemPlaceholder" runat="server" />
            </ul>
        </LayoutTemplate>
        <ItemTemplate>
            <li><a href="<%# Eval("Username") %>/<%# Eval("Id") %>"><%# Eval("Name") %></a></li>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
