Function EXVBPOR99_POSTFIELD_IO_NARRATIVE_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , prf
prf = "P" 
x = SetFieldValue("IO-PRINT(1)",prf) 
SetfocusonField("IO-NARRATIVE(2) ")  
EXVBPOR99_POSTFIELD_IO_NARRATIVE_1 = trgReturnValue
End Function

Function EXVBPOR99_POSTFIELD_IO_NARRATIVE_2(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , prf
prf = "P" 
x = SetFieldValue("IO-PRINT(2)",prf) 
 
EXVBPOR99_POSTFIELD_IO_NARRATIVE_2 = trgReturnValue
End Function

Function EXVBPOR99_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, wsLineType,part,wsSuffix, desc, output, uom ,gl ,supp ,wsprefix, wsRR, PO
x = GetFieldValue("IO-LINE-TYPE",wsLineType) 
x = EnableField("IO-LINE-TYPE")
x = EnableField("IO-UNIT")
x = GetFieldValue("OP-SUPPLIER",supp) 
If wsLineType = "1" Then
	'msgbox wsLineType
	x = GetFieldValue("IO-PART",part)
End If
'msgbox "check gl " 
If wsLineType = "2" Then
	x = GetFieldValue("IO-GL-CODE",gl) 
	'msgbox "gl " &  gl 
	If (gl = "") Or (gl = "51500") Then 
		'  	   msgbox "  incorrect  " 
		x= MsgBox("Error Gl code is mandatory or invalid ",4, "Line Type query")
		trgReturnValue = False
	End If 
End If 
EXVBPOR99_POSTSCREEN_ALL = trgReturnValue
End Function

Function EXVBPOR99_PRESCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True

EXVBPOR99_PRESCREEN_ALL = trgReturnValue
End Function

Function EXVBPOR99_POSTFIELD_IO_PART(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , part , desc , price , uom , vatc , pcc , scc, gl , vatco , ty ,supp , wsprefix
Dim partallow 
Dim wststock, arrstock, stock, allocfirm  
Dim alloccom, allocsal, qtyrqn,result
Dim wsLineType , wsSuffix ,supplier 
x = GetFieldValue("IO-PART",part) 
x = GetFieldValue("OP-SUPPLIER",supp) 
part = UCASE(part) 
'wsSuffix = mid(part,8,10)
'wsprefix = mid(supp,1,3) 
'msgbox wsSuffix 
'If wsprefix <> "MMD" Then 
'	Select Case wsSuffix
'		Case "-84"
'			x=MsgBox("-84 suffix parts not allowed - Please Re-enter ")
'		Case "-85"
'			x=MsgBox("-85 suffix parts not allowed - Please Re-enter ")
'	End Select
'End If 
x = GetFieldValue("OP-SUPPLIER",supplier) 
Set xml = CreateObject("Microsoft.XMLHTTP")   
xml.Open "GET","Triggers/evolution/getsupvat.asp?supplier=" & supplier , False  
' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 
vatc = complete 
'msgbox "vat code " & vatc 
vatc = trim(vatc)
Set xml = CreateObject("Microsoft.XMLHTTP")   
part = trim(part) 
'msgbox "part " & part  
xml.Open "GET","Triggers/evolution/por16getpart.asp?part=" & part , False   
' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 
output = split(complete, "|") 
' Open the connection to the remote server.   
desc = output(0)
uom  = output(1)
partallow = cint(output(2))

If desc = "NSP" Then 
	ty = "2" 
	wsLineType = ty
	x = SetFieldValue("IO-LINE-TYPE",ty) 
	x = DisableField("IO-LINE-TYPE")
	ty="EACH"
	x = SetFieldValue("IO-UNIT",ty) 
	x = DisableField("IO-UNIT")
	ty = part
	'x = SetFieldValue("IO-PART-DESC",part)
	'x = EnableField("IO-PART-DESC")
  x = EnableField("IO-GL-CODE")
	ty = "VI" 
	x = SetFieldValue("IO-VAT-CODE",ty) 
	'x = SetFocusOnField("IO-PART-DESC")
Else
	If partallow = 1 Then
     result = DisableField("IO-QTY") 	  
     msgbox("Check urgently with Willem before Ordering this part!")
	End If
	ty = "1" 
	wsLineType = ty
	x = SetFieldValue("IO-LINE-TYPE",ty) 
	x = DisableField("IO-LINE-TYPE")
	x = SetFieldValue("UF-X-DESC",desc)
	' x = SetFieldValue("IO-PART-DESC","")  
	' x = DisableField("IO-PART-DESC") 
	'ty = "VI" 
	x = SetFieldValue("IO-VAT-CODE",vatc)
	x = SetFieldValue("IO-UNIT",uom) 
	x = DisableField("IO-UNIT")
	x = DisableField("IO-GL-CODE")
    Set xml = CreateObject("Microsoft.XMLHTTP")  
	' Open the connection to the remote server.  
	xml.Open "GET","Triggers/evolution/partstock.asp?part=" & Part,False  
	' Send the request and returns the data:  
	xml.Send 
	wststock = xml.responseText
	arrstock = split(wststock,"|") 
	If Isarray(arrstock) Then
		stock = cInt(arrstock(0)) 	
		allocfirm = cInt(arrstock(1))
		alloccom = cInt(arrstock(2)) 	
		allocsal = cInt(arrstock(3)) 	
		qtyrqn = cInt(arrstock(4)) 
	Else
		msgbox wsstock   	
		stock = 0 
	End If
	result = SetFieldValue("UF-X-STOCK",stock)
	x = SetFocusOnField("IO-QTY")
End If


EXVBPOR99_POSTFIELD_IO_PART = trgReturnValue
End Function

Function EXVBPOR99_PREFIELD_IO_PRICE(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True

EXVBPOR99_PREFIELD_IO_PRICE = trgReturnValue
End Function

Function EXVBPOR99_POSTFIELD_IO_QTY(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , part , desc , price , uom , vatc , pcc , scc, gl , vatco , ty 
Dim wststock, arrstock, stock, allocfirm  
Dim alloccom, allocsal, qtyrqn,result , wdate, dd,mon, yy ,mm ,Ordate
Dim wsLineType , wsSuffix ,supplier 

x = GetFieldValue("IO-PART",part) 
x = GetFieldValue("OP-SUPPLIER",supplier) 
x = GetFieldValue("IO-LINE-TYPE",ty) 
ty = ty * 1
If ty = 1 Then 
x = GetFieldValue("IO-DATE-REQ",wdate) 
If wdate > "" Then 
dd = mid(wdate,1,2) 
mm = mid(wdate,3,2) 
yy = mid(wdate,5,2)
'msgbox mm 
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
'msgbox Ordate

    'Set xml = CreateObject("Microsoft.XMLHTTP")  
    'xml .Open "GET","Triggers/evolution/getpoprice.asp?part=" & part & "&supplier=" & supplier & "&ordate=" & Ordate, False   
    ' Send the request and returns the data:   
    'xml.Send  
    'complete = xml.responseText  'msgbox complete
    'price = complete
    'If price <>  "0" Then 
    '    x = SetFieldValue("IO-PRICE",price) 
    'End If 
 End If 
End If 
EXVBPOR99_POSTFIELD_IO_QTY = trgReturnValue
End Function

Function EXVBPOR99_POSTFIELD_UF_CONPART_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , part ,pfx ,desc,price,uom,vatc,gl , lt,supp , defsup
x = GetFieldValue("UF-CONPART",part) 
part = trim(part) 
'msgbox part 
pfx = mid(part,1,4)
'msgbox "pfx " & pfx  
If pfx = "CON0" Then 
	'msgbox "part " & part 
	Set xml = CreateObject("Microsoft.XMLHTTP")   
	xml.Open "GET","Triggers/evolution/getdes.asp?part=" & part , False   
	' Send the request and returns the data:   
	xml.Send  
	complete = xml.responseText 
	If complete <>  "ERROR" Then 
		'msgbox complete 
		output = split(complete, "~") 
		desc = output(0)
		price  = output(1)
		uom = output(2) 
		vatc = output(3) 
		gl = output(4)
		defsup = output(5) 
		lt = 2
		x = SetFieldValue("IO-PART",part) 
		x = SetFieldValue("IO-PART-DESC",desc) 
		x = SetFieldValue("IO-PRICE", price) 
		x = SetFieldValue("IO-VAT-CODE",vatc) 
		x = SetFieldValue("IO-GL-CODE",gl) 
		x = SetFieldValue("IO-LINE-TYPE", lt) 
		SetfocusonField ("IO-QTY") 
		x = GetFieldValue("OP-SUPPLIER",supp)
		defsup = trim(defsup) 
		supp = trim(supp) 
		If defsup <> supp Then 
			msgbox "Not Default Supplier" 
		End If  
	End If	
End If 

EXVBPOR99_POSTFIELD_UF_CONPART_1 = trgReturnValue
End Function

Function EXVBPOR99_POSTFIELD_IO_PRICE(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim wsPart, xml, complete, wsSupplier, wsPrice, wsType, wsInput, wsStatus
trgReturnValue=True

x = GetFieldValue("IO-PART",wsPart)
x = GetFieldValue("IO-PRICE",wsInput)
x = GetFieldValue("OP-SUPPLIER",wsSupplier)
x = GetFieldValue("IO-LINE-TYPE",wsType)
x = GetFieldValue("OP-H-STATUS",wsStatus)
'x = msgbox("Status =" & wsStatus)

Set xml = CreateObject("Microsoft.XMLHTTP")   
xml.Open "GET","/triggers/evolution/PurchPSPPrice.asp?part=" & wsPart & "&Supplier=" & wsSupplier, False
xml.Send
wsPrice = xml.responseText

If wsType = "1" Then
	If wsPrice = 0 Then
     x = msgbox("Part has no price for this supplier. Please enter in PSP20/24 before proceeding",4)
  	  'x = SetFieldValue("IO-PRICE",wsPrice)
		trgReturnValue=False
	Else
		If wsPrice <> wsInput Then
			x = msgbox("Part price for this supplier does not match current PSP Price. Please update in PSP20/24 before proceeding",4)
   	  'x = SetFieldValue("IO-PRICE",wsPrice)
		trgReturnValue=False
		End If
	End If
   
End If

EXVBPOR99_POSTFIELD_IO_PRICE = trgReturnValue
End Function
