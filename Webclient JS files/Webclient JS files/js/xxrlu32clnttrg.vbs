Function EXVBRLU32_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , pdate , idate , ipost , yy , mm ,dd , peryymmdd , invyymmdd ,i 
	x = GetFieldValue("OP-PERIOD-END",pdate) 
	x = GetFieldValue("OP-DATE-ENT(1)",idate) 
	x = GetFieldValue("IO-POST(1)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  1 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(1)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(2)",idate) 
	x = GetFieldValue("IO-POST(2)",ipost)  
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  2 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(2)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(3)",idate) 
	x = GetFieldValue("IO-POST(3)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  3 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(3)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(4)",idate) 
	x = GetFieldValue("IO-POST(4)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  4 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(4)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(5)",idate) 
	x = GetFieldValue("IO-POST(5)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  5 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(5)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(6)",idate) 
	x = GetFieldValue("IO-POST(6)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  6 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(6)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(7)",idate) 
	x = GetFieldValue("IO-POST(7)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  7 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(7)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(8)",idate) 
	x = GetFieldValue("IO-POST(8)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  8 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(8)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(9)",idate) 
	x = GetFieldValue("IO-POST(9)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  9 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(9)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(10)",idate) 
	x = GetFieldValue("IO-POST(10)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  10 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(10)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(11)",idate) 
	x = GetFieldValue("IO-POST(11)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  11 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(11)") 
		End If  
	End If 
	x = GetFieldValue("OP-DATE-ENT(12)",idate) 
	x = GetFieldValue("IO-POST(12)",ipost) 
	dd = mid(pdate,1,2) 
	mm = mid(pdate,4,2) 
	yy = mid(pdate,7,2) 
	peryymmdd = yy & mm & dd 
	dd = mid(idate,1,2) 
	mm = mid(idate,4,2) 
	yy = mid(idate,7,2) 
	invyymmdd = yy & mm & dd 
	ipost = UCASE(ipost) 
	If ipost  = "Y" Then
		If invyymmdd > peryymmdd Then 
			x=MsgBox("Inv date  12 > per date ?",4,"Line Type query")
			trgReturnValue = False
			setfocusonfield("IO-POST(12)") 
		End If  
	End If 

EXVBRLU32_POSTSCREEN_ALL = trgReturnValue
End Function
