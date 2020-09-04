function UpdateMenu(value, menu) {
    document.getElementById("leftmenuheader").innerHTML = menu;
    var leftmenu = document.getElementById("leftmenu");
    var innerDivs = leftmenu.getElementsByTagName("DIV");
    //Session["SelectedMenu"] = value;
    for (var i = 0; i < innerDivs.length; i++) {
        if (innerDivs[i].id === 'menu' + value) {
            innerDivs[i].style.display = 'block';
        } else {
            innerDivs[i].style.display = 'none';
        }
    }
    var state = outerLayout.state;
    if (state.west.isClosed) {
        outerLayout.toggle('west');
    };
}
function MenuItem(menuurl) {
    //what to do
    //document.getElementById('mainContent').src = "";
    //alert(menuurl);
    if (menuurl.length > 0) {
        var n = menuurl.search("ttp:");
        if (n > 0) {
            window.open(menuurl, '_blank');
        } else {
            $.ajax({
                type: "POST",
                url: menuurl,
                success: function (response) {
                    $("#mainContent").html(response);
                },
                error: function (xhr, status, error) {
                    //alert("Status: " + status);
                    //alert("Error: " + error);
                    //alert("xhr: " + xhr.readyState);
                },
                statusCode: {
                    404: function () {
                        //nothing
                        alert('Page not found please contact your developer!');
                    }
                }
            });
            //document.getElementById('mainContent').src = menuurl;
            //alert('im here!');
        }
    }
}
function ChageTheme(wsTheme) {
    //Set the cookies and session variable then reload the page
    setCookie("sitetheme", wsTheme, 365);
    document.getElementById('theme_css').href = '/css/stylesheet.' + wsTheme + '.css';
    document.getElementById('mainiframe').contentWindow.document.getElementById('theme_css').href = '/css/forms.' + wsTheme + '.css';
}
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1);
        if (c.indexOf(name) === 0) return c.substring(name.length, c.length);
    }
    return "";
}
function checkCookie() {
    var usertheme = getCookie("sitetheme");
    if (usertheme !== "") {
        document.getElementById('theme_css').href = 'css/stylesheet.' + usertheme + '.css';
    } else {
        usertheme = "default";
        if (usertheme !== "" && usertheme !== null) {
            setCookie("username", usertheme, 30);
        }
    }
}
function ChageDB(wsDatabase) {
    //what to do
    processAjax('includes/dbupdate.asp?wsDatabase=' + wsDatabase);
}
function processAjax(url) {
    if (window.XMLHttpRequest) { // Non-IE browsers 
        req = new XMLHttpRequest();
        req.onreadystatechange = targetDiv;
        try {
            req.open("POST", url, true);
        }
        catch (e) {
            alert(e);
        }
        req.send(null);
    } else if (window.ActiveXObject) { // IE 
        req = new ActiveXObject("Microsoft.XMLHTTP");
        if (req) {
            req.onreadystatechange = targetDiv;
            req.open("POST", url, true);
            req.send();
        }
    }
    return false;
}
function targetDiv() {
    if (req.readyState === 4) { // Complete 
        if (req.status === 200) { // OK response 
            /*
            alert("Response: " + req.responseText); 
            */
        } else {
            alert("Problem: " + req.statusText + ":" + req.status);
        }
    }
}