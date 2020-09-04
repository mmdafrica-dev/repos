Function EXVBCBR24_POSTFIELD_IO_TRANS_TYPE(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
	Dim x , read1 , read2  
	x = GetFieldValue("IO-TRANS-TYPE",read1) 	'msgbox read1	If read1 = 40 Then	   read2 = "CASH" 	  ' msgbox read2	   x = SetFieldValue("IO-CUS-REF",read2)	 Setfocusonfield("IO-CUS-SUP")	End If
EXVBCBR24_POSTFIELD_IO_TRANS_TYPE = trgReturnValue
End Function

Function EXVBCBR24_PRESCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
	Dim x , f1 , f2 	'msgbox "Start" 	f1 = 1	f2 = "SJ"	x = SetFieldValue("IO-LEDG-TYPE",f1)	x = SetFieldValue("IO-GEN-CODE",f2) 
	Setfocusonfield ("IO-GROSS") 
EXVBCBR24_PRESCREEN_ALL = trgReturnValue
End Function

Function EXVBCBR24_POSTFIELD_UF_BANKNAME_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
	Dim x, rect , bkbr , bkac , bknm , sess , sessno 	x = GetFieldValue("IO-SESSION",sess) 	x = GetFieldValue("IO-ENTRY-NO",sessno) 	x = GetFieldValue("UF-BANKBR",bkbr) 	x = GetFieldValue("UF-BANKACC",bkac) 	x = GetFieldValue("UF-BANKNAME",bknm) 	rect = "240" 	rect = rect * 1	sessno = sessno * 1 	msgbox bknm 	' Create an xmlhttp object:  	If bknm > "" Then  		msgbox "bknm more" 		Set xml = CreateObject("Microsoft.XMLHTTP")   		msgbox "off to insert" 		xml.Open "GET","Triggers/evolution/getsessins.asp?rect=" & rect & "&sess=" & sess & "&sessno=" & sessno & "&bkbr=" & bkbr &  "&bkac=" & bkac & "&bknm=" & bknm, False   		' Send the request and returns the data:   		xml.Send  		complete = xml.responseText 		msgbox complete		'msgbox "finish update" 		
	Else		msgbox "bkspace?" 	End If 
EXVBCBR24_POSTFIELD_UF_BANKNAME_1 = trgReturnValue
End Function

Function EXVBCBR24_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
	Dim x, rect , bkbr , bkac , bknm , sess , sessno ,cnm , tt  		
	x = GetFieldValue("IO-SESSION",sess) 	x = GetFieldValue("IO-ENTRY-NO",sessno) 	x = GetFieldValue("UF-BANKBR",bkbr) 	x = GetFieldValue("UF-BANKACC",bkac) 	x = GetFieldValue("UF-BANKNAME",bknm) 	x = GetFieldValue("UF-CNAME",cnm) 	x = GetFieldValue("IO-TRANS-TYPE",tt ) 	rect = "240" 	rect = rect * 1	sessno = sessno * 1 	'msgbox tt 	' Create an xmlhttp object:  	'If tt  = "10" Then  	Set xml = CreateObject("Microsoft.XMLHTTP")   	'msgbox "off to insert" 	xml.Open "GET","Triggers/evolution/getsessins.asp?rect=" & rect & "&sess=" & sess & "&sessno=" & sessno & "&bkbr=" & bkbr &  "&bkac=" & bkac & "&bknm=" & bknm & "&cnm=" & cnm & "&tt=" & tt, False   	'Send the request and returns the data:   	xml.Send  	complete = xml.responseText 	'msgbox complete	'msgbox "finish update" 	'End If 
EXVBCBR24_POSTSCREEN_ALL = trgReturnValue
End Function

Function EXVBCBR24_PREFIELD_UF_CNAME_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
EXVBCBR24_PREFIELD_UF_CNAME_1 = trgReturnValue
End Function

Function ExvbCBR24_POSTFIELD_IO_LEDG_TYPE(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , gcode
gcode = "CT" 
x = SetFieldValue("IO-GEN-CODE",gcode) 
Setfocusonfield("IO-GROSS") 
ExvbCBR24_POSTFIELD_IO_LEDG_TYPE = trgReturnValue
End Function
