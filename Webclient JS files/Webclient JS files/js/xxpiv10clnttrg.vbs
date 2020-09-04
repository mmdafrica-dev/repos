Function EXVBPIV10_PREFIELD_IO_DOC_TYPE(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True

EXVBPIV10_PREFIELD_IO_DOC_TYPE = trgReturnValue
End Function

Function EXVBPIV10_POSTFIELD_IO_GEN_CODE(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim   x , ty , gc x= GetFieldValue("IO-GEN-CODE",gc) 'msgbox gc gc = UCASE(gc) 'msgbox gc If gc = "PI" Then ty = "I" x = SetFieldValue("IO-DOC-TYPE",ty) SetFocusonField("IO-INV-DATE")End If If gc = "PC" Then ty = "C" x = SetFieldValue("IO-DOC-TYPE",ty) SetFocusonField("IO-INV-DATE")End If 
EXVBPIV10_POSTFIELD_IO_GEN_CODE = trgReturnValue
End Function

Function EXVBPIV10_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , invno , gcode , fno ,inszeroes , suppinv ,supplier , rect 
x = GetFieldValue("IO-INVOICE",invno) 
x = GetFieldValue("IO-GEN-CODE",gcode) 
gcode = UCASE(gcode) 
Set xml = CreateObject("Microsoft.XMLHTTP")   
' Create an xmlhttp object:  
Set xml = CreateObject("Microsoft.XMLHTTP")   
xml.Open "GET","Triggers/evolution/getsys2pi.asp?gcode=" & gcode , False   
 ' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 
invno = complete
invno = invno * 1 
invno = invno + 1 
'msgbox invno 
If invno < 10000 Then 
	iz = "00" 
End If 
If invno > 9999 Then 
	iz = "0" 
End If 
fno = gcode & iz & invno 
'msgbox fno
x = GetFieldValue("IO-SUP-ORD-NO",suppinv)
x = GetFieldValue("IO-SUPPLIER",supplier)
rect = "22"   
Set xml = CreateObject("Microsoft.XMLHTTP")   
' Create an xmlhttp object:  
Set xml = CreateObject("Microsoft.XMLHTTP")   
xml.Open "GET","Triggers/evolution/insfure22.asp?rect=" & rect & "&fno=" & fno & "&suppinv=" & suppinv & "&supplier=" & supplier, False   
 ' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 






EXVBPIV10_POSTSCREEN_ALL = trgReturnValue
End Function
