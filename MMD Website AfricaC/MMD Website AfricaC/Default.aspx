<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MMD_Website_AfricaC._Default" %>

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
        <ul class="nav nav-tabs" role="tablist">

            <li class="nav-item ">
                <a class="nav-link" href="#tabs_global" role="tab" data-toggle="tab">MMD Global</a>
            </li>
            <%if (isAfrica || isDeveloper)
                    { %>
            <li class="nav-item active">
                <a class="nav-link " href="#tabs_africa" role="tab" data-toggle="tab">MMD Africa</a>
            </li>
            <%} %>
            <%if (isGPHC || isDeveloper)
                    { %>
            <li class="nav-item ">
                <a class="nav-link" href="#tabs_gphc" role="tab" data-toggle="tab">MMD GPHC</a>
            </li>
            <%} %>
            <%if (isUK || isDeveloper)
                    { %>
            <li class="nav-item ">
                <a class="nav-link" href="#tabs_uk" role="tab" data-toggle="tab">MMD UK</a>
            </li>
            <%} %>
            <%if (isIndia || isDeveloper)
                    { %>
            <li class="nav-item">
                <a class="nav-link" href="#tabs_india" role="tab" data-toggle="tab">MMD India</a>
            </li>
            <%} %>
            <%if (isAustralia || isDeveloper)
                    { %>
            <li class="nav-item">
                <a class="nav-link" href="#tabs_australia" role="tab" data-toggle="tab">MMD Australia</a>
            </li>
            <%} %>
            <%if (isGMSAfrica || isDeveloper)
                    { %>
            <li class="nav-item">
                <a class="nav-link" href="#tabs_gms_africa" role="tab" data-toggle="tab">MMD GMS (Africa)</a>
            </li>
            <%} %>
            <%if (isMauritius || isDeveloper)
                    { %>
            <li class="nav-item">
                <a class="nav-link" href="#tabs_gms_mauritius" role="tab" data-toggle="tab">MMD GMS (Mauritius)</a>
            </li>
            <%} %>
        </ul>

        <div class="tab-content">
            <div id="tabs_global" class="tab-pane ">
                <div class="panel panel-mmd1">
                    <div class="panel-heading">
                        <h3 class="panel-title">MMD Global</h3>
                    </div>
                    <div class="panel-body">
                        <div class="btn-group">
                            <p>
                                <a href="http://10.1.0.23:8008" target="_blank" class="btn btn-default">MMD Corporate Portal internal</a>
                            </p>
                            <p>
                                <a href="http://sales.mmdgphc.com" target="_blank" class="btn btn-default">MMD Corporate Portal external</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="https://mmddocs.mmdgphc.com" target="_blank" class="btn btn-default">MMD Laserfiche - Sales</a>
                            </p>
                            <p>
                                <a href="https://10.20.0.4" target="_blank" class="btn btn-default">MMD Laserfiche - Sales (IP)</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="https://mmdlegal.mmdgphc.com" target="_blank" class="btn btn-default">MMD Laserfiche - Legal</a>
                            </p>
                            <p>
                                <a href="https://10.20.0.164" target="_blank" class="btn btn-default">MMD Laserfiche - Legal (IP)</a>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel panel-mmd2">
                    <div class="panel-heading">
                        <h3 class="panel-title">MMD Global Developers</h3>
                    </div>
                    <div class="panel-body">
                        <div class="btn-group">
                            <p>
                                <a href="https://wwhd.evolution-pv.com" target="_blank" class="btn btn-default">Evolution Helpdesk</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="http://10.1.0.22:8080/tfs" target="_blank" class="btn btn-default">MMD TFS Server</a>
                            </p>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="btn-group">
                            <p>
                                <a href="https://support.evolution-pv.com" target="_blank" class="btn btn-default">Evolution Support (Downloads)</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tabs_africa" class="tab-pane active">
                <div class="panel panel-mmd1">
                    <div class="panel-heading">
                        <h3 class="panel-title">MMD Africa</h3>
                    </div>
                    <div class="panel-body">
                        <div class="btn-group">
                            <p>
                                <%if (inAfrica)
                                        { %>
                                <a href="http://mtms" target="_blank" class="btn btn-default">Mtms Web Client</a>
                                <%}
                                    else
                                        { %>
                                <a href="http://10.3.0.5:81" target="_blank" class="btn btn-default">Mtms Web Client</a>
                                <%} %>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <%if (inAfrica)
                                        { %>
                                <a href="http://intranet" target="_blank" class="btn btn-default">Intranet</a>
                                <%}
                                    else
                                        { %>
                                <a href="http://10.3.0.5" target="_blank" class="btn btn-default">Intranet</a>
                                <%} %>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <%if (inAfrica)
                                        { %>
                                <a href="http://mmdonline" target="_blank" class="btn btn-default">MMD Online</a>
                                <%}
                                    else
                                        { %>
                                <a href="http://10.3.0.5:83" target="_blank" class="btn btn-default">MMD Online</a>
                                <%} %>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <%if (inAfrica)
                                        { %>
                                <a href="http://mmdims" target="_blank" class="btn btn-default">MMD IMS</a>
                                <%}
                                    else
                                        { %>
                                <a href="http://10.3.0.5:84" target="_blank" class="btn btn-default">MMD IMS</a>
                                <%} %>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="http://10.3.0.10:8064" target="_blank" class="btn btn-default">New Intranet</a>
                            </p>
                        </div>                        
                        <div class="btn-group">
                            <p>
                                <a href="http://10.3.0.5:85" target="_blank" class="btn btn-default">Dashboard</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="http://10.3.0.5:8888" target="_blank" class="btn btn-default">Speed Test</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="http://mmdsapdm/SOLIDWORKSPDM/" target="_blank" class="btn btn-default">Drawing Vault</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="https://mail.mmdafrica.co.za/owa/" target="_blank" class="btn btn-default">Outlook Web Access</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="http://support.lexion.co.za" target="_blank" class="btn btn-default">Lexion Support</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="http://stocktake" target="_blank" class="btn btn-default">Stock Take</a>
                            </p>
                        </div>
                    </div>
                </div>
                <%if (isDeveloper)
                        { %>
                <div class="panel panel-mmd2">
                    <div class="panel-heading">
                        <h3 class="panel-title">MMD Africa Developers</h3>
                    </div>
                    <script type="text/javascript">
                        function RunDebug(system) {
                            var url = "http://intranet/includes/debug.asp?debug=";
                            if (system == 'intranet') {
                                url = "http://intranet/includes/debug.asp?debug=";
                            } else if (system == 'mmdonline') {
                                url = "http://mmdonline/includes/debug.asp?debug=";
                            } else if (system == 'mmdims') {
                                url = "http://mmdims/includes/debug.asp?debug=";
                            } else {
                                url = "http://" + system + "/includes/debug.asp?debug=";
                            }

                            var check = confirm("do you want to enable debug?");
                            if (check == true) {
                                url += "true";
                            }
                            else {
                                url += "false";
                            }
                            alert(url);
                            var win = window.open(url, '_blank');
                            if (win) {
                                //Browser has allowed it to be opened
                                win.focus();
                            } else {
                                //Browser has blocked it
                                alert('Please allow popups for this website');
                            }
                        }
                    </script>
                    <div class="panel-body">
                        <div class="btn-group">
                            <p>
                                <a href="http://10.3.0.5:82" target="_blank" class="btn btn-default">Mtms Web Client Test</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="#" onclick="RunDebug('intranet');" class="btn btn-default">Intranet - Debug</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="#" onclick="RunDebug('MMDOnline');" class="btn btn-default">MMDonline - Debug</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="#" onclick="RunDebug('10.3.0.5:83');" class="btn btn-default">10.3.0.5:83 - Debug</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="#" onclick="RunDebug('mmdims');" class="btn btn-default">MMDIMS - Debug</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="#" onclick="RunDebug('10.3.0.5:84');" class="btn btn-default">10.3.0.5:84 - Debug</a>
                            </p>
                        </div>
                        <div class="btn-group ">
                            <p>
                                <a href="https://10.3.0.5:1158/em/" target="_blank" class="btn btn-default">Oracle Enterprise Manager - 10.3.0.5</a>
                            </p>
                        </div>
                        <div class="btn-group ">
                            <p>
                                <a href="https://10.3.0.10:1158/em/" target="_blank" class="btn btn-default">Oracle Enterprise Manager - 10.3.0.10</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="http://support.lexion.co.za" target="_blank" class="btn btn-default">Lexion Support</a>
                            </p>
                        </div>
                        <div class="btn-group ">
                            <p>
                                <a href="http://support.lexion.co.za/staff" target="_blank" class="btn btn-default">Lexion Support Dashboard</a>
                            </p>
                        </div>
                    </div>
                </div>
                <%} %>
            </div>

            <div id="tabs_gphc" class="tab-pane">
                <div class="panel panel-mmd1">
                    <div class="panel-heading">
                        <h3 class="panel-title">MMD GPHC</h3>
                    </div>
                    <div class="panel-body">
                        <div class="btn-group">
                            <p>
                                <a href="http://10.1.0.23:8002" target="_blank" class="btn btn-default">Web Client GPHC</a>
                            </p>
                            <p>
                                <a href="http://10.1.0.23:8007" target="_blank" class="btn btn-default">Web Client Asia</a>
                            </p>
                            <p>
                                <a href="http://intranet.mmdgphc.com" target="_blank" class="btn btn-default">Intranet
                                </a>
                                Intranet screens for GPHC
                            </p>
                            <p>
                                <a href="http://extranet.mmdgphc.com/" target="_blank" class="btn btn-default">Extranet
                                </a>
                                External access screens
                            </p>
                            <p>
                                <a href="http://10.1.0.23:8005" target="_blank" class="btn btn-default">Used Parts/Stock
                                </a>
                                List of used stock at external offices
                            </p>
                        </div>

                        <div class="btn-group">
                            <p>
                                <a href="http://10.1.0.23:8006" target="_blank" class="btn btn-default">India Wear Parts
                                </a>
                                Wear part availability from India
                            </p>
                            <p>
                                <a href="http://10.1.0.23:8008" target="_blank" class="btn btn-default">Corporate Portal
                                </a>
                                Internal access version
                            </p>
                            <p>
                                <a href="http://10.1.0.23:8009/" target="_blank" class="btn btn-default">WW Reference List
                                </a>
                            </p>
                            <p>
                                <a href="http://10.1.0.23:8012/" target="_blank" class="btn btn-default">Dashboard
                                </a>
                            </p>
                            <p>
                                <a class="btn btn-default" href="http://10.1.0.23:8018/" target="_blank">GPHC Intranet for UK
                                </a>
                            </p>
                        </div>

                        <div class="btn-group">
                            <p>
                                <a href="http://10.1.0.23:8010" target="_blank" class="btn btn-default">Specsheet
                                </a>
                            </p>
                            <p>
                                <a href="http://10.1.0.23:8013" target="_blank" class="btn btn-default">Enquiries
                                </a>
                            </p>
                            <p>
                                <a href="http://10.1.0.23:8014" target="_blank" class="btn btn-default">Newsletters
                                </a>
                            </p>
                            <p>
                                <a href="http://helpdesk.mmdgphc.com" target="_blank" class="btn btn-default">Old IT Helpdesk
                                </a>
                            </p>
                            <p>
                                <a href="https://ithelpdesk.mmdgphc.com" target="_blank" class="btn btn-default">New IT Helpdesk
                                </a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>

            <div id="tabs_uk" class="tab-pane">
                <div class="panel panel-mmd1">
                    <div class="panel-heading">
                        <h3 class="panel-title">MMD UK</h3>
                    </div>
                    <div class="panel-body">
                    </div>
                </div>
            </div>

            <div id="tabs_india" class="tab-pane">
                <div class="panel panel-mmd1">
                    <div class="panel-heading">
                        <h3 class="panel-title">MMD India</h3>
                    </div>
                    <div class="panel-body">
                    </div>
                </div>
            </div>

            <div id="tabs_australia" class="tab-pane">
                <div class="panel panel-mmd1">
                    <div class="panel-heading">
                        <h3 class="panel-title">MMD Australia</h3>
                    </div>
                    <div class="panel-body">
                    </div>
                </div>
            </div>

            <div id="tabs_gms_africa" class="tab-pane">
                <div class="panel panel-mmd1">
                    <div class="panel-heading">
                        <h3 class="panel-title">MMD GMS (Africa)</h3>
                    </div>
                    <div class="panel-body">
                        <div class="btn-group">
                            <p>
                                <a href="http://10.3.10.12:81" target="_blank" class="btn btn-default">Web Client</a>
                            </p>
                            <p>
                                <a href="http://10.3.10.12:82" target="_blank" class="btn btn-default">Web Client Test</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="http://10.3.10.12:80" target="_blank" class="btn btn-default">Intranet</a>
                            </p>
                            <p>
                                <a href="http://10.3.10.12:8064" target="_blank" class="btn btn-default">New Intranet</a>
                            </p>
                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="http://mmdonline" target="_blank" class="btn btn-default">MMD Online</a>
                            </p>
                            <p>
                                <a href="http://10.3.10.12:83" target="_blank" class="btn btn-default">MMD Online (Ex)</a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>

            <div id="tabs_gms_mauritius" class="tab-pane">
                <div class="panel panel-mmd1">
                    <div class="panel-heading">
                        <h3 class="panel-title">MMD GMS (Mauritius)</h3>
                    </div>
                    <div class="panel-body">
                        <div class="btn-group">
                            <p>
                                <a href="http://10.7.1.4:81" target="_blank" class="btn btn-default">Web Client</a>
                            </p>

                        </div>
                        <div class="btn-group">
                            <p>
                                <a href="http://10.7.1.4:8064" target="_blank" class="btn btn-default">New Intranet</a>
                            </p>
                            <p>
                                <a href="http://10.7.1.4:82" target="_blank" class="btn btn-default">Old Intranet</a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
