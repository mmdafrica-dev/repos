Function EXVBSIV52_PRESCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , lno , qty , extprice , uprice ,invno 
x = GetFieldValue("IO-LINE-NO", lno)
If lno <> "" Then 
EXVBSIV52_PRESCREEN_ALL = trgReturnValue
End Function

Function EXVBSIV52_POSTFIELD_UF_UNITPRICE_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, qty , price , extprice 
EXVBSIV52_POSTFIELD_UF_UNITPRICE_1 = trgReturnValue
End Function

Function EXVBSIV52_POSTFIELD_IO_LEVEL_3(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , cc ,pfx , gl ,Valonly
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
Valonly = "Y" 
x = SetFieldValue("IO-VALUE-ONLY",Valonly)  
EXVBSIV52_POSTFIELD_IO_LEVEL_3 = trgReturnValue
End Function