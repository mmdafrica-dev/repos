var x, ty, gc;
var fieldobj = new Object;

x = GetFieldValue("IO-GEN-CODE", fieldobj);
gc = UCASE(fieldobj.value);
if (gc == "PI") {
    fieldobj.value = "I";
    x = SetFieldValue("IO-DOC-TYPE", fieldobj);
    SetFocusonField("IO-INV-DATE");
}
if (gc == "PC") {
    fieldobj.value = "C";
    x = SetFieldValue("IO-DOC-TYPE", fieldobj);
    SetFocusonField("IO-INV-DATE");
}



var x, invno, gcode, fno, inszeroes, suppinv, supplier, rect;
var fieldobj = new Object;

x = GetFieldValue("IO-INVOICE", fieldobj)
invno = fieldobj.value;

x = GetFieldValue("IO-GEN-CODE", fieldobj)
gcode = fieldobj.value.toUpperCase();

invno = GetXMLurl("Triggers/evolution/getsys2pi.asp?gcode=" & gcode);
invno = (invno * 1) + 1;

if( invno < 10000) {
    iz = "00";
}
if (invno > 9999) {
    iz = "0";
}
fno = gcode = iz = invno;

x = GetFieldValue("IO-SUP-ORD-NO", fieldobj);
suppinv = fieldobj.value;

x = GetFieldValue("IO-SUPPLIER", fieldobj);
supplier = fieldobj.value;

GetXMLurl("Triggers/evolution/insfure22.asp?rect=22&fno=" & fno & "&suppinv=" & suppinv & "&supplier=" & supplier);






