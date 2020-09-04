Function ExvbSOR89_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , ws1 , ws2 ,ws3 ,Ordno , linno , rd
x = GetFieldValue("IO-REF",Ordno) 
linno = "0" 

Set xml = CreateObject("Microsoft.XMLHTTP") 
xml.Open "GET","Triggers/evolution/getokords.asp?Ordno=" & Ordno & "&linno=" & linno, False   
xml.Send  
complete = xml.responseText 
ws1 = complete 
ws1 = trim(ws1) 
ws1 = complete 
ws1 = trim(ws1) 

'msgbox "ret v " & ws1 
ws2 = "NO"  
If  ws1 = "WMC"  Then 
    ws2 = "YES" 
End If  
If  ws1 = "WP"  Then 
    ws2 = "YES" 
End If 
If  ws1 = "JL"  Then 
    ws2 = "YES" 
End If 
If  ws1 = "SSS"  Then 
    ws2 = "YES" 
End If 
If  ws1 = "CB"  Then 
    ws2 = "YES" 
End If 
If  ws1 = "IB"  Then 
    ws2 = "YES" 
End If 
If  ws1 = "SVH"  Then 
    ws2 = "YES" 
End If 
If  ws1 = "MTH"  Then 
    ws2 = "YES" 
End If 

'msgbox  "is it okay to proceed " & ws2 
If ws2 = "NO" Then 
   x=MsgBox("You are not authorised to proceed with this order?",4,"Line Type query")
   trgReturnValue = False
End If
ExvbSOR89_POSTSCREEN_ALL = trgReturnValue
End Function
