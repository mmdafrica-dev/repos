Function EXVBSIV76_POSTFIELD_IO_QUANTITY(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True

EXVBSIV76_POSTFIELD_IO_QUANTITY = trgReturnValue
End Function

Function EXVBSIV76_POSTFIELD_IO_LEVEL_3(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , cc ,pfx , gl ,Valonly
x = GetFieldValue("IO-LEVEL-3",cc) 
'msgbox cc 
x = SetFieldValue("IO-LINE-CONTRACT",cc) 
pfx = mid(cc,1,2)
'msgbox ("cc " & cc & " pfx " & pfx ) 
pfx = UCASE(pfx) 
If pfx = "SN" Then 
	gl = "41050" 
	x = SetFieldValue("IO-GL-CODE",gl)
End If 	
If pfx = "SR" Then 
	gl = "41100" 
	x = SetFieldValue("IO-GL-CODE",gl)
End If 	
If pfx = "SS" Then 
	gl = "41200" 
	x = SetFieldValue("IO-GL-CODE",gl)
End If 	


 
EXVBSIV76_POSTFIELD_IO_LEVEL_3 = trgReturnValue
End Function

Function EXVBSIV76_POSTFIELD_UF_TSAP_2(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , currcd , tsap 
x = GetFieldValue("OP-INV-CURR",currcd)
If currcd = "TSA" Then 
   x = GetFieldValue("UF-TSAP",tsap) 
   tsap = tsap * 1
   tsap = tsap / 10 
   x = SetFieldValue("IO-PRICE",tsap)
   SetFocusonField("IO-LINE-DISC") 
 End If  
EXVBSIV76_POSTFIELD_UF_TSAP_2 = trgReturnValue
End Function
