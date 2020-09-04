var x, cc, pfx, Valonly;
var fieldobj = new Object;

x = GetFieldValue("IO-LEVEL-3", fieldobj);
cc = fieldobj.value;
x = SetFieldValue("IO-LINE-CONTRACT", fieldobj);
pfx = mid(cc, 1, 2);

pfx = pfx.toUpperCase();
if (pfx == "SN") {
    fieldobj.value = "41050";
    x = SetFieldValue("IO-GL-CODE", fieldobj);
}
if (pfx == "SR") {
    fieldobj.value = "41100";
    x = SetFieldValue("IO-GL-CODE", fieldobj);
}
if (pfx == "SS") {
    fieldobj.value = "41200";
    x = SetFieldValue("IO-GL-CODE", fieldobj);
}




var x, currcd, tsap;
var fieldobj = new Object;

x = GetFieldValue("OP-INV-CURR", currcd);
currcd = fieldobj.value;

if (currcd == "TSA") {
    x = GetFieldValue("UF-TSAP", fieldobj);
    tsap = fieldobj.value;
    tsap = tsap * 1;
    tsap = tsap / 10;
    fieldobj.value = tsap
    x = SetFieldValue("IO-PRICE", fieldobj);
    SetFocusonField("IO-LINE-DISC");
}