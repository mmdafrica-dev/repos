<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="MMD_Website_AfricaC.Demo" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lbllocalIP" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <script>
        $('#myTab a').click(function (e) {
            e.preventDefault()
            $(this).tab('show')
        })
    </script>
    <div id="content">
        <ul class="nav nav-tabs" role="tablist" id="menuBar" runat="server">

        </ul>

        <div class="tab-content" id="tabContent" runat="server" >
            
        </div>
    </div>
</asp:Content>

