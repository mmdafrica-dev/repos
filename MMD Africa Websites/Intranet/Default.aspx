<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Intranet._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lbllocalIP" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="Label3" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="Label4" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
</asp:Content>
