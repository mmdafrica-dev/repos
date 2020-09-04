Function EXVBSOR87_PRESCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
    Dim x , ov , ordno ,result , ty , sp
    x = GetFieldValue("IO-ORDER-NO",ordno) 
    x = GetFieldValue("IO-LINE-TYPE",ty) 
    ' Create an xmlhttp object:  
    Set xml = CreateObject("Microsoft.XMLHTTP")   
    'msgbox "off to insert" 
    xml.Open "GET","Triggers/evolution/getords.asp?ordno=" & ordno, False   
    ' Send the request and returns the data:
    xml.Send  
    complete = xml.responseText 
    'msgbox complete
    ov = complete 
    x = SetFieldValue("UF-ORDSVAL",ov) 
EXVBSOR87_PRESCREEN_ALL = trgReturnValue
End Function

Function EXVBSOR87_POSTFIELD_IO_PART(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, part, desc, price, uom, vatc, pcc, scc, gl, vatco, cust, ty 
Dim wststock, arrstock, stock, allocfirm, curr, vcd
Dim alloccom, allocsal, qtyrqn, result
Dim wsLineType

x = GetFieldValue("OP-CUSTOMER",cust)
Set xml = CreateObject("Microsoft.XMLHTTP")   
xml.Open "GET","Triggers/evolution/getcusvat.asp?cust=" & cust , False  
' Send the request And returns the data:   
xml.Send  
complete = xml.responseText 
vatc = complete
vatc = trim(vatc) 
x = SetFieldValue("IO-VAT-CODE",vatc)

x = GetFieldValue("IO-PART",part) 
Set xml = CreateObject("Microsoft.XMLHTTP")   
xml.Open "GET","Triggers/evolution/sor87getpart.asp?part=" & part , False   
' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 
'msgbox complete 
desc = complete 
If desc = "NSP" Then 
  x = GetFieldValue("IO-LINE-TYPE",ty)
  If ty = "2" Then 
    x = DisableField("IO-LINE-TYPE")
  Else 
    ty = "2" 
    x = SetFieldValue("IO-LINE-TYPE",ty) 
    x = DisableField("IO-LINE-TYPE")
  End If
  ty = part
  x = SetFocusOnField("IO-PART-DESC") 
Else
  x = GetFieldValue("IO-LINE-TYPE",ty)
  If ty = "1"      	Then 
    x = DisableField("IO-LINE-TYPE")
  Else 
    ty = "1" 
    x = SetFieldValue("IO-LINE-TYPE",ty) 
    x = DisableField("IO-LINE-TYPE")
  End If
  x = SetFieldValue("UF-X-DESWC",desc)
  x = DisableField("IO-PART-DESC") 
  'Set xml = CreateObject("Microsoft.XMLHTTP")  
  ' Open the connection to the remote server.  
  'xml.Open "GET","Triggers/evolution/partstock.asp?part=" & Part,False  
  ' Send the request and returns the data:  
  'xml.Send 
  'wststock = xml.responseText
  'arrstock = split(wststock,"|") 
  'If Isarray(arrstock) Then
  '  stock = cInt(arrstock(0)) 	
  '  allocfirm = cInt(arrstock(1))
  '  alloccom = cInt(arrstock(2)) 	
  '  allocsal = cInt(arrstock(3)) 	
  '  qtyrqn = cInt(arrstock(4)) 
  'Else
  '  'msgbox wsstock   	
  '  stock = 0 
  'End If
  'result = SetFieldValue("UF-X-STOCK",stock)
  x = SetFocusOnField("IO-QTY") 
End If
EXVBSOR87_POSTFIELD_IO_PART = trgReturnValue
End Function

Function EXVBSOR87_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim pfx2 ,  npart , lty , part , pdesc ,nty ,suppf ,ptpfx
Dim ty ,sp ,Ordno , linno ,yn

x = GetFieldValue("IO-ORDER-NO",Ordno) 
x = GetFieldValue("IO-LINE",linno) 
'msgbox "sending" 
Set xml = CreateObject("Microsoft.XMLHTTP") 
xml.Open "GET","Triggers/evolution/getchords.asp?Ordno=" & Ordno & "&linno=" & linno, False   
xml.Send  
complete = xml.responseText 
yn = complete

x = GetFieldValue("IO-LINE-TYPE",ty) 
x = GetFieldValue("IO-ORDER-NO",rord) 
x = GetFieldValue("IO-PART",part)
x = GetFieldValue("IO-CUST-ITM-REF",suppf)  
x = EnableField("IO-LINE-TYPE")
x = EnableField("IO-UNIT")

If yn = "NO" Then 
  If ty = "2" Then 
    x = EnableField("IO-PART-DESC")
  End If
End If

part = UCASE(part) 
rord = UCASE(rord) 
rord = LEFT(rord,2) 
ptpfx = LEFT(part,2) 

Set xml = CreateObject("Microsoft.XMLHTTP") 
xml.Open "GET","Triggers/evolution/sor87getpart.asp?part=" & part , False   
' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 
'msgbox complete
desc = complete 

pdesc = UCASE(desc) 

If yn = "NO" Then 
  If desc = "NSP" Then 
    ty = "2" 
    wsLineType = ty
    x = SetFieldValue("IO-LINE-TYPE",ty) 
  End If 
End If 
EXVBSOR87_POSTSCREEN_ALL = trgReturnValue
End Function

Function EXVBSOR87_POSTFIELD_IO_QTY(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , curr, pr
Dim  part , desc , price , uom , vatc , pcc , scc, gl , vatco , ty 
Dim wststock, arrstock, stock, allocfirm  
Dim alloccom, allocsal, qtyrqn,result , wdate, dd,mon, yy ,mm ,Ordate
Dim wsLineType , wsSuffix ,customer

x = GetFieldValue("OP-PRICE-CURR",curr)
If curr = "TSA" Then 
  'x = SetFocusOnField("UF_TSAPRICE")  
Else
  x = SetFocusOnField("IO-PRICE")  
End If

If curr <> "TSA" Then 
  x = GetFieldValue("IO-PART",part)
  x = GetFieldValue("OP-CUSTOMER",customer) 
  x = GetFieldValue("IO-LINE-TYPE",ty) 
  ty = ty * 1
  If ty = 1 Then 
    x = GetFieldValue("OP-H-DATE-REQ",wdate) 
    Set xml = CreateObject("Microsoft.XMLHTTP")  
    xml.Open "GET","Triggers/evolution/getsoprice.asp?part=" & part & "&customer=" & customer & "&ordate=" & wdate, False   
    ' Send the request and returns the data:   
    xml.Send  
    complete = xml.responseText 
    price = complete
    If price <>  "0" Then 
      x = SetFieldValue("IO-PRICE",price) 
    End If 
  End If 
End If
EXVBSOR87_POSTFIELD_IO_QTY = trgReturnValue
End Function
