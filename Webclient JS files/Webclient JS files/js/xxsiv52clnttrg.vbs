Function EXVBSIV52_PRESCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , lno , qty , extprice , uprice ,invno 
x = GetFieldValue("IO-LINE-NO", lno)
If lno <> "" Then 	lno = lno * 1 	lno = Formatnumber(lno,0) 	x 	= GetFieldValue("IO-INVOICE",invno) 	Set xml = CreateObject("Microsoft.XMLHTTP")   	xml.Open "GET","Triggers/evolution/getinvdet.asp?lno=" & lno & "&invno=" & invno, False  	' Send the request and returns the data:   	xml.Send  	complete = xml.responseText 	'msgbox complete 	output = split(complete, "~") 	qty = output(0)	uprice = output(1) 	extprice = output(2)	x = SetFieldValue("UF-UNITPRICE",uprice) 	x = SetFieldValue("IO-PRICE",extprice) End If  
EXVBSIV52_PRESCREEN_ALL = trgReturnValue
End Function

Function EXVBSIV52_POSTFIELD_UF_UNITPRICE_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, qty , price , extprice x= GetFieldValue("IO-QUANTITY",qty) qty = qty * 1x = GetFieldValue("UF-UNITPRICE",price) price = price * 1extprice = qty * price extprice = Round(extprice,2) x = SetFieldValue("IO-PRICE",extprice)   
EXVBSIV52_POSTFIELD_UF_UNITPRICE_1 = trgReturnValue
End Function

Function EXVBSIV52_POSTFIELD_IO_LEVEL_3(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , cc ,pfx , gl ,Valonlyx = GetFieldValue("IO-LEVEL-3",cc) x = SetFieldValue("IO-CONTRACT",cc) pfx = mid(cc,1,2)
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
