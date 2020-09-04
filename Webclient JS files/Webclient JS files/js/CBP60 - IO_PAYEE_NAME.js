Dim x, custno, cname, xml, complete, rno
x = GetFieldValue("IO-CUS-SUP", custno)
x = GetFieldValue("IO-LEDG-TYPE", rno)
Set xml = CreateObject("Microsoft.XMLHTTP")
If rno = 3 Then
'nothing
Else
xml.Open "GET", "Triggers/evolution/getcs.asp?custno=" & custno & "&rno=" & rno, False
xml.Send
complete = xml.responseText
cname = complete
x = SetFieldValue("IO-PAYEE-NAME", cname)
End If


var x, complete, custno, rno;
var fieldobj = new Object;

x = GetFieldValue("IO-CUS-SUP", fieldobj);
custno = fieldobj.value;
x = GetFieldValue("IO-LEDG-TYPE", fieldobj);
rno = fieldobj.value;
complete = GetXMLurl("Triggers/evolution/getcs.asp?custno=" + custno + "&rno=" + rno);

fieldobj.value = complete;
x = SetFieldValue("IO-PAYEE-NAME", fieldobj);