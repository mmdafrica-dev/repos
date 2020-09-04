var x, lno, qty, extprice, uprice, invno;
var fieldobj = new Object;

x = GetFieldValue("IO-LINE-NO", fieldobj)
lno = fieldobj.value;

if (lno != '') {
    lno = lno * 1;

    x = GetFieldValue("IO-INVOICE", fieldobj);
    invno = fieldobj.value;

    complete = GetXMLurl("Triggers/evolution/getinvdet.asp?lno=" & lno & "&invno=" & invno);
    output = split(complete, "~");
    qty = output(0);
    uprice = output(1);
    extprice = output(2);
    fieldobj.value = uprice;
    x = SetFieldValue("UF-UNITPRICE", fieldobj);
    fieldobj.value = extprice;
    x = SetFieldValue("IO-PRICE", fieldobj);
}



