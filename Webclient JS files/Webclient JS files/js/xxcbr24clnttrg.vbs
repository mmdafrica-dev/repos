Function EXVBCBR24_POSTFIELD_IO_TRANS_TYPE(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
	Dim x , read1 , read2  
	x = GetFieldValue("IO-TRANS-TYPE",read1) 
EXVBCBR24_POSTFIELD_IO_TRANS_TYPE = trgReturnValue
End Function

Function EXVBCBR24_PRESCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
	Dim x , f1 , f2 
	Setfocusonfield ("IO-GROSS") 
EXVBCBR24_PRESCREEN_ALL = trgReturnValue
End Function

Function EXVBCBR24_POSTFIELD_UF_BANKNAME_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
	Dim x, rect , bkbr , bkac , bknm , sess , sessno 
	Else
EXVBCBR24_POSTFIELD_UF_BANKNAME_1 = trgReturnValue
End Function

Function EXVBCBR24_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
	Dim x, rect , bkbr , bkac , bknm , sess , sessno ,cnm , tt  
	x = GetFieldValue("IO-SESSION",sess) 
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