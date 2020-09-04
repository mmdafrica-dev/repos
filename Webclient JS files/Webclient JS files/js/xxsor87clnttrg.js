var x, ov, ordno;
var fieldObj = new Object;

x = GetFieldValue("IO-ORDER-NO", fieldObj);
ordno = fieldObj.value;

fieldObj.value = GetXMLurl("Triggers/evolution/getords.asp?ordno=" + ordno);
x = SetFieldValue("UF-ORDSVAL", fieldObj);


var part, pdesc,suppf, ptpfx;
var ty, Ordno, linno, yn;
var fieldObj = new Object;

x = GetFieldValue("IO-ORDER-NO", fieldObj)
Ordno = fieldObj.value;
x = GetFieldValue("IO-LINE", fieldObj)
linno = fieldObj.value;

yn = GetXMLurl("Triggers/evolution/getchords.asp?Ordno=" + Ordno + "&linno=" + linno);

x = GetFieldValue("IO-LINE-TYPE", fieldObj);
ty = fieldObj.value;
x = GetFieldValue("IO-ORDER-NO", fieldObj);
rord = fieldObj.value;
x = GetFieldValue("IO-PART", fieldObj);
part = fieldObj.value;
x = GetFieldValue("IO-CUST-ITM-REF", fieldObj);
suppf = fieldObj.value;
x = EnableField("IO-LINE-TYPE");
x = EnableField("IO-UNIT");

if (yn == "NO") {
    if (ty == "2") {
        x = EnableField("IO-PART-DESC");
    }
}

part = part.toUpperCase();
rord = rord.toUpperCase();
rord = rord.substring(1, 2);
ptpfx = part.substring(1, 2);

desc = GetXMLurl("Triggers/evolution/sor87getpart.asp?part=" + part);

pdesc = desc.toUpperCase();

if (yn == "NO") {
    if (desc == "NSP") {
        fieldObj.value = "2";
        x = SetFieldValue("IO-LINE-TYPE", fieldObj);
    }
}


var x, part, desc, price, uom, vatc, pcc, scc, gl, vatco, cust, ty;
var wststock, arrstock, stock, allocfirm, curr, vcd;
var alloccom, allocsal, qtyrqn, result;
var wsLineType
var fieldObj = new Object;

x = GetFieldValue("OP-CUSTOMER", fieldObj);
cust = fieldObj.value;
vatc = GetXMLurl("Triggers/evolution/getcusvat.asp?cust=" + cust);
fieldObj.value = vatc;
x = SetFieldValue("IO-VAT-CODE", fieldObj);

x = GetFieldValue("IO-PART", fieldObj);
part = fieldObj.value;
desc = GetXMLurl("Triggers/evolution/sor87getpart.asp?part=" + part);
if (desc == "NSP") {
    x = GetFieldValue("IO-LINE-TYPE", fieldObj);
    ty = fieldObj.value;
    if(ty == "2") {
        x = DisableField("IO-LINE-TYPE");
    } else {
        ty = "2"
        ty = fieldObj.value;
        x = SetFieldValue("IO-LINE-TYPE", fieldObj);
        x = DisableField("IO-LINE-TYPE");
    }
    ty = part;
    x = SetFocusOnField("IO-PART-DESC");
} else {
    x = GetFieldValue("IO-LINE-TYPE", ty);
    if (ty == "1") {
        x = DisableField("IO-LINE-TYPE");
    } else {
        ty = "1";
        x = SetFieldValue("IO-LINE-TYPE", ty);
        x = DisableField("IO-LINE-TYPE");
    }
    x = SetFieldValue("UF-X-DESWC", desc);
    x = DisableField("IO-PART-DESC");
    x = SetFocusOnField("IO-QTY");
}
