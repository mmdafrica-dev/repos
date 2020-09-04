Function EXVBSOR85_PRESCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , gc , py 
Dim read1, rdate , rdate2 ,yc ,rtime 
'gc = "SN" 
'x = SetFieldValue("IO-GEN-CODE",gc) 
py = "Y" 
x = SetFieldValue("IO-ORDER-LINE",py) 
Value = Date()
'msgbox value
If Not Isdate(Value)Then 
rdate2 = 0 
Else 
d = day(value) 
m = month(value) 
y = year(value) 
yc = y - 2000
If d < 10 Then 
 d = "0" & d
End If
If m < 10 Then 
 m = "0" & m
End If 
If yc < 10 Then 
 yc = "0" & yc
End If 
 
rdate2 = d & m & yc
'msgbox rdate2 
x = SetFieldValue("IO-DATE-REQUEST",rdate2) 

End If

SetFocusonField("IO-GEN-CODE")
EXVBSOR85_PRESCREEN_ALL = trgReturnValue
End Function

Function EXVBSOR85_POSTFIELD_IO_GEN_CODE(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , pfx 
x = GetFieldValue("IO-GEN-CODE",pfx)
x = SetFieldValue("IO-SALESMAN",pfx) 
EXVBSOR85_POSTFIELD_IO_GEN_CODE = trgReturnValue
End Function

Function EXVBSOR85_PREFIELD_IO_CUS_ORD_NO(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , pfx 
x = GetFieldValue("IO-GEN-CODE",pfx) 
x = SetFieldValue("IO-SALESMAN",pfx)  
EXVBSOR85_PREFIELD_IO_CUS_ORD_NO = trgReturnValue
End Function

Function EXVBSOR85_PREFIELD_IO_CARRIER(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, complete,qy , pfx ,seqno, pdate, seqno2,combi , qno , gcode ,sfx , zf
x = GetFieldValue("UF-GQ", qy) 
x = GetFieldValue("IO-GEN-CODE",gcode) 
x = GetFieldValue("IO-CARRIER",qno) 
zf = "0" 
qy = (ucase(qy))
gcode = (ucase(gcode))
If gcode = "SS" Then 
sfx = "S" 
End If 
If gcode = "SN" Then 
sfx = "M" 
End If 
If gcode = "SR" Then 
sfx = "R" 
End If 


'msgbox pfx 
If qy = "Y" Then 
pfx = "20" 
If qno = "" Then 
' Create an xmlhttp object:   
Set xml = CreateObject("Microsoft.XMLHTTP")   
' Open the connection to the remote server.  
'msgbox "quotea"
'msgbox pfx
xml.Open "GET","Triggers/evolution/getcallnbr.asp?pfx=" & pfx   ,False   
' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 
' msgbox complete
seqno  = complete
'msgbox "finish fill off to update"
Set xml = CreateObject("Microsoft.XMLHTTP")   
' Open the connection to the remote server.  
'msgbox pfx
xml.Open "GET","Triggers/evolution/getcallunbr.asp?pfx=" & pfx   ,False   
' Send the request and returns the data:   
xml.Send  
'msgbox "finish update" 
seqno2 = pfx
combi = seqno2 & zf & seqno 	& sfx
x = SetFieldValue("IO-CARRIER",combi) 
End If
End If 
EXVBSOR85_PREFIELD_IO_CARRIER = trgReturnValue
End Function

Function EXVBSOR85_PREFIELD_IO_AGENT(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, complete,qy , pfx ,seqno, pdate, seqno2,combi , qno , gcode ,sfx , zf
x = GetFieldValue("UF-GQ", qy) 
x = GetFieldValue("IO-GEN-CODE",gcode) 
x = GetFieldValue("IO-AGENT",qno) 
zf = "0" 
qy = (ucase(qy))
gcode = (ucase(gcode))
If gcode = "SS" Then 
sfx = "S" 
End If 
If gcode = "SN" Then 
sfx = "M" 
End If 
If gcode = "SR" Then 
sfx = "R" 
End If 


'msgbox pfx 
If qy = "Y" Then 
pfx = "20" 
If qno = "" Then 
' Create an xmlhttp object:   
Set xml = CreateObject("Microsoft.XMLHTTP")   
' Open the connection to the remote server.  
'msgbox "quotea"
'msgbox pfx
xml.Open "GET","Triggers/evolution/getcallnbr.asp?pfx=" & pfx   ,False   
' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 
' msgbox complete
seqno  = complete
'msgbox "finish fill off to update"
Set xml = CreateObject("Microsoft.XMLHTTP")   
' Open the connection to the remote server.  
'msgbox pfx
xml.Open "GET","Triggers/evolution/getcallunbr.asp?pfx=" & pfx   ,False   
' Send the request and returns the data:   
xml.Send  
'msgbox "finish update" 
seqno2 = pfx
combi = seqno2 & zf & seqno 	& sfx
x = SetFieldValue("IO-AGENT",combi) 
End If
End If 

EXVBSOR85_PREFIELD_IO_AGENT = trgReturnValue
End Function

Function EXVBSOR85_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True


EXVBSOR85_POSTSCREEN_ALL = trgReturnValue
End Function
