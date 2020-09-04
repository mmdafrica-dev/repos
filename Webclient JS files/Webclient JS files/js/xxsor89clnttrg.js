var x, ws1, ws2, ws3, Ordno, linno, rd;
var fieldObj = new Object;

x = GetFieldValue("IO-REF", fieldObj);
Ordno = fieldObj.value;
complete = GetXMLurl("Triggers/evolution/getokords.asp?Ordno=" & Ordno & "&linno=0");
ws1 = complete;

ws2 = "NO";
if(ws1 == "WMC") {
    ws2 = "YES";
}
if(ws1 == "WP") {
    ws2 = "YES";
}
if(ws1 == "JL") {
    ws2 = "YES";
}
if(ws1 == "SSS") {
    ws2 = "YES";
}
if(ws1 == "CB") {
    ws2 = "YES";
}
if(ws1 == "IB") {
    ws2 = "YES";
}
if(ws1 == "SVH") {
    ws2 = "YES";
}
if(ws1 == "MTH") {
    ws2 = "YES";
}

if (ws2 == "NO") {
    x = alert("You are not authorised to proceed with this order?");
    trgReturnValue = False;
}
