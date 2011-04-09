<%@ Page Title="Welcome to CanMan" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CanMan._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        CanMan
    </h2>
    <p>
        Enter your dailymile username: <asp:TextBox ID="username" runat="server" /> 
        <asp:Button ID="submit" Text="Get Routes"  runat="server" 
            onclick="submit_Click"/>

    </p>
</asp:Content>
