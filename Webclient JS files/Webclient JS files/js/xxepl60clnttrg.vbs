Function EXVBEPL60_POSTFIELD_UF_LD1_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf, origamt
x = GetFieldValue("UF-LD1", ad) 
ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(1)",origamt) 
    pamt = (origamt / 114) * 100 
    msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(1)",npay) 
    x = SetFieldValue("IO-DISCOUNT(1)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(1)", cf) 
    SetFocusonField("IO-CONFIRM(1)") 
End If 
EXVBEPL60_POSTFIELD_UF_LD1_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_LD2_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
x = GetFieldValue("UF-LD2", ad) 
'ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(2)",origamt) 
    pamt = (origamt / 114) * 100 
    'msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(2)",npay) 
    x = SetFieldValue("IO-DISCOUNT(2)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(2)", cf) 
    SetFocusonField("IO-CONFIRM(2)") 
End If 

EXVBEPL60_POSTFIELD_UF_LD2_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_LD3_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
x = GetFieldValue("UF-LD3", ad) 
'ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(3)",origamt) 
    pamt = (origamt / 114) * 100 
    'msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(3)",npay) 
    x = SetFieldValue("IO-DISCOUNT(3)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(3)", cf) 
    SetFocusonField("IO-CONFIRM(3)") 
End If 

EXVBEPL60_POSTFIELD_UF_LD3_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_LD4_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
x = GetFieldValue("UF-LD4", ad) 
'ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(4)",origamt) 
    pamt = (origamt / 114) * 100 
    'msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(4)",npay) 
    x = SetFieldValue("IO-DISCOUNT(4)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(4)", cf) 
    SetFocusonField("IO-CONFIRM(4)") 
End If 

EXVBEPL60_POSTFIELD_UF_LD4_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_LD5_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
x = GetFieldValue("UF-LD5", ad) 
'ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(5)",origamt) 
    pamt = (origamt / 114) * 100 
    'msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(5)",npay) 
    x = SetFieldValue("IO-DISCOUNT(5)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(5)", cf) 
    SetFocusonField("IO-CONFIRM(5)") 
End If 

EXVBEPL60_POSTFIELD_UF_LD5_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_LD6_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
x = GetFieldValue("UF-LD6", ad) 
'ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(6)",origamt) 
    pamt = (origamt / 114) * 100 
    'msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(6)",npay) 
    x = SetFieldValue("IO-DISCOUNT(6)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(6)", cf) 
    SetFocusonField("IO-CONFIRM(6)")  
End If 

EXVBEPL60_POSTFIELD_UF_LD6_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_LD7_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
x = GetFieldValue("UF-LD7", ad) 
'ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(7)",origamt) 
    pamt = (origamt / 114) * 100 
    'msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(7)",npay) 
    x = SetFieldValue("IO-DISCOUNT(7)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(7)", cf) 
    SetFocusonField("IO-CONFIRM(7)") 
End If 

EXVBEPL60_POSTFIELD_UF_LD7_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_LD8_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
x = GetFieldValue("UF-LD8", ad) 
'ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(8)",origamt) 
    pamt = (origamt / 114) * 100 
    'msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(8)",npay) 
    x = SetFieldValue("IO-DISCOUNT(8)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(8)", cf) 
    SetFocusonField("IO-CONFIRM(8)") 
End If 

EXVBEPL60_POSTFIELD_UF_LD8_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_LD9_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
x = GetFieldValue("UF-LD9", ad) 
'ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(9)",origamt) 
    pamt = (origamt / 114) * 100 
    'msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(9)",npay) 
    x = SetFieldValue("IO-DISCOUNT(9)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(9)", cf) 
    SetFocusonField("IO-CONFIRM(9)")  
End If 

EXVBEPL60_POSTFIELD_UF_LD9_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_LD10_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
x = GetFieldValue("UF-LD10", ad) 
'ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(10)",origamt) 
    pamt = (origamt / 114) * 100 
    'msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(10)",npay) 
    x = SetFieldValue("IO-DISCOUNT(10)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(10)", cf) 
    SetFocusonField("IO-CONFIRM(10)") 
End If 

EXVBEPL60_POSTFIELD_UF_LD10_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_LD11_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
x = GetFieldValue("UF-LD1", ad) 
'ad = trim(ad) 
'msgbox "test if f " & ad 
If (ad = "A") Or (ad = "a")  Then 
	 'msgbox   "if "  
    x  = GetFieldValue("OP-ORIG-AMT(11)",origamt) 
    pamt = (origamt / 114) * 100 
    'msgbox pamt
    x = GetFieldValue("OP-SETTLE-DISC", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(11)",npay) 
    x = SetFieldValue("IO-DISCOUNT(11)",damt) 
    cf = "Y"
    x = SetFieldValue("IO-CONFIRM(11)", cf) 
    SetFocusonField("IO-CONFIRM(11)") 
End If 

EXVBEPL60_POSTFIELD_UF_LD11_1 = trgReturnValue
End Function

Function EXVBEPL60_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , suppno ,rsup ,rect ,wok
rect = "111"
x = GetFieldValue("OP-SUPPLIER",suppno) 
Set xml = CreateObject("Microsoft.XMLHTTP")   
xml.Open "GET","Triggers/evolution/getfur111.asp?rect=" & rect & "&suppno=" & suppno, False  
 ' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 
rsup = complete
rsup = rtrim(rsup) 
suppno = rtrim(suppno)
If rsup <> suppno Then
	rect = "111"  
    ' Create an xmlhttp object:  
	Set xml = CreateObject("Microsoft.XMLHTTP")   
	xml.Open "GET","Triggers/evolution/insfure111.asp?rect=" & rect & "&suppno=" & suppno, False   
	' Send the request and returns the data:   
	xml.Send  
	complete = xml.responseText 
End If 	
	
EXVBEPL60_POSTSCREEN_ALL = trgReturnValue
End Function

Function EXVBEPL60_POSTFIELD_UF_DISCOUNT_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , damt , pamt , npay , wamt , ad , dperc , cf 
    msgbox "start"
	x  = GetFieldValue("OP-ORIG-AMT(1)",origamt) 
    pamt = (origamt / 114) * 100 
    x = GetFieldValue("UF-DISCOUNT", dperc)  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(1)",npay) 
    x = SetFieldValue("IO-DISCOUNT(1)",damt)
	
	x  = GetFieldValue("OP-ORIG-AMT(2)",origamt) 
    pamt = (origamt / 114) * 100  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(2)",npay) 
    x = SetFieldValue("IO-DISCOUNT(2)",damt)
	
	x  = GetFieldValue("OP-ORIG-AMT(3)",origamt) 
    pamt = (origamt / 114) * 100  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(3)",npay) 
    x = SetFieldValue("IO-DISCOUNT(3)",damt)
	
	x  = GetFieldValue("OP-ORIG-AMT(4)",origamt) 
    pamt = (origamt / 114) * 100  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(4)",npay) 
    x = SetFieldValue("IO-DISCOUNT(4)",damt)
	
	x  = GetFieldValue("OP-ORIG-AMT(5)",origamt) 
    pamt = (origamt / 114) * 100  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(5)",npay) 
    x = SetFieldValue("IO-DISCOUNT(5)",damt)
	
	x  = GetFieldValue("OP-ORIG-AMT(6)",origamt) 
    pamt = (origamt / 114) * 100  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(6)",npay) 
    x = SetFieldValue("IO-DISCOUNT(6)",damt)
	
	x  = GetFieldValue("OP-ORIG-AMT(7)",origamt) 
    pamt = (origamt / 114) * 100  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(7)",npay) 
    x = SetFieldValue("IO-DISCOUNT(7)",damt)
	
	x  = GetFieldValue("OP-ORIG-AMT(8)",origamt) 
    pamt = (origamt / 114) * 100  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(8)",npay) 
    x = SetFieldValue("IO-DISCOUNT(8)",damt)
	
	x  = GetFieldValue("OP-ORIG-AMT(9)",origamt) 
    pamt = (origamt / 114) * 100  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(9)",npay) 
    x = SetFieldValue("IO-DISCOUNT(9)",damt)
	
	x  = GetFieldValue("OP-ORIG-AMT(10)",origamt) 
    pamt = (origamt / 114) * 100  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(10)",npay) 
    x = SetFieldValue("IO-DISCOUNT(10)",damt)
	
	x  = GetFieldValue("OP-ORIG-AMT(11)",origamt) 
    pamt = (origamt / 114) * 100  
    damt = pamt * dperc / 100 
    damt = round(damt,2)  
    npay = origamt - damt 
	npay = round(npay,2)
    x = SetFieldValue("IO-PAY-AMT(11)",npay) 
    x = SetFieldValue("IO-DISCOUNT(11)",damt)
	msgbox "complete!"
	
EXVBEPL60_POSTFIELD_UF_DISCOUNT_1 = trgReturnValue
End Function
