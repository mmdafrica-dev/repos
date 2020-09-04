Function EXVBSPI22_POSTFIELD_IO_TAX(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, gamt , tamt , tcode , suppc  
x = GetFieldValue("IO-GOODS",gamt) x = GetFieldValue("IO-TAX",tamt) x = GetFieldValue("IO-SUPPLIER",suppc) 
suppc = ucase(suppc) 'msgbox suppc ' Create an xmlhttp object:  Set xml = CreateObject("Microsoft.XMLHTTP")   'msgbox "off to insert" xml.Open "GET","Triggers/evolution/getaccm2.asp?suppc=" & suppc, False    ' Send the request and returns the data:   xml.Send  complete = xml.responseText 'msgbox completetcode = complete x = SetFieldValue("IO-TAX-CODE(1)",tcode) x = SetFieldValue("IO-TAX-GOODS(1)",gamt) x = SetFieldValue("IO-TAX-AMOUNT(1)",tamt) 
EXVBSPI22_POSTFIELD_IO_TAX = trgReturnValue
End Function

Function EXVBSPI22_POSTFIELD_IO_SUPPLIER(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, supplier, complete, curc
x = GetFieldValue("IO-SUPPLIER",supplier) Set xml = CreateObject("Microsoft.XMLHTTP")   
xml.Open "GET","Triggers/evolution/getsupcur.asp?supplier=" & supplier , False  ' Send the request and returns the data:   xml.Send  complete = xml.responseText curc= complete curc= trim(curc)
x = SetFieldValue("IO-CUR-CODE",curc)
EXVBSPI22_POSTFIELD_IO_SUPPLIER = trgReturnValue
End Function

Function EXVBSPI22_POSTFIELD_IO_GEN_CODE(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, gcode 
gcode = "PT" 
x = SetFieldValue("IO-GEN-CODE",gcode) 
SetfocusOnfield("IO-SUPPLER") 
EXVBSPI22_POSTFIELD_IO_GEN_CODE = trgReturnValue
End Function

Function EXVBSPI22_POSTFIELD_IO_DOC_TYPE(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, gcode 
gcode = "PT" 
x = SetFieldValue("IO-GEN-CODE",gcode) 
SetfocusOnfield("IO-SUPPLER")  
EXVBSPI22_POSTFIELD_IO_DOC_TYPE = trgReturnValue
End Function

Function EXVBSPI22_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , invno , gcode , fno ,inszeroes , suppinv ,supplier , rect 
Dim wdate , dd, mm ,yy, mon ,Ordate,tcost  
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
x = GetFieldValue("IO-DOC-DATE",wdate) 
If wdate > "" Then 
	dd = mid(wdate,1,2) 
	mm = mid(wdate,3,2) 
	yy = mid(wdate,5,2)
'	msgbox mm 
	mm = mm * 1
	If mm = 1 Then 
		mon = "Jan"
	End If 
	If mm = 2 Then 
		mon = "Feb"
	End If 
	If mm = 3 Then 
		mon = "Mar"
	End If 
	If mm = 4 Then 
		mon = "Apr"
	End If 
	If mm = 5 Then 
		mon = "May"
	End If 
	If mm = 6 Then 
		mon = "Jun"
	End If 
	If mm = 7 Then 
		mon = "Jul"
	End If 
	If mm = 8 Then 
		mon = "Aug"
	End If 
	If mm = 9 Then 
		mon = "Sep"
	End If 
	If mm = 10 Then 
		mon = "Oct"
	End If 
	If mm = 11 Then 
		mon = "Nov"
	End If 
 	If mm = 12 Then 
		mon = "Dec"
	End If 
	'msgbox mon
 
	Ordate = dd & "-" & mon & "-" & yy 
End If
'msgbox Ordate 
x = GetFieldValue("IO-SUP-REF",suppinv)
suppinv = UCASE(suppinv) 
x = GetFieldValue("IO-SUPPLIER",supplier)
x = GetFieldValue("IO-AMOUNT",tcost) 
rect = "22"   
Set xml = CreateObject("Microsoft.XMLHTTP")   
' Create an xmlhttp object:  
Set xml = CreateObject("Microsoft.XMLHTTP")   
xml.Open "GET","Triggers/evolution/insfure22.asp?rect=" & rect & "&fno=" & fno  & "&Ordate=" & Ordate &  "&tcost=" & tcost & "&suppinv=" & suppinv & "&supplier=" & supplier, False   
 ' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 

 




EXVBSPI22_POSTSCREEN_ALL = trgReturnValue
End Function
