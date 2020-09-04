Function EXVBSOR12_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x , predate , postdate ,ordno , premon , postmon , curper  
Dim predd, premm , preyy , postdd , postmm , postyy , fkn 
Dim rdate2 , rtime ,hh , mm ,dt ,rect 
x = GetFieldValue("IO-ORDER-NO", ordno)
Set xml = CreateObject("Microsoft.XMLHTTP")   
xml.Open "GET","Triggers/evolution/getorddate.asp?ordno="   & ordno, False  
' Send the request and returns the data:   
xml.Send  
complete = xml.responseText 
predate = complete 
predd = mid(predate,1,2) 
premm = mid(predate,4,2) 
preyy = mid(predate,9,2) 
predate = predd & premm & preyy 
x = GetFieldValue("IO-DATE-PROMISE",postdate)
'msgbox postdate 
If Postdate <> "" Then 
	postdd = mid(postdate,1,2) 
	postmm = mid(postdate,3,2) 
	postyy = mid(postdate,5,2) 
	'msgbox "dates " & " bf " & predate &  " aft " & postdate 
	If predate <> postdate Then 
		premm = premm * 1
		Select Case premm
			Case 1 
				premon = "Jan"
			Case 2 
				premon = "Feb"
			Case 3 
				premon = "Mar"	
			Case 4 
				premon = "Apr"
			Case 5 
				premon = "May"
			Case 6 
				premon = "Jun"
			Case 7 
				premon = "Jul"
			Case 8 
				premon = "Aug"
			Case 9 
				premon = "Sep"
			Case 10 
				premon = "Oct"
			Case 11 
				premon = "Nov"
			Case 12 
				premon = "Dec"
		End Select 
		predate = predd & "-" & premon & "-" & preyy 
		'msgbox "predate " & predate 
		postmm = postmm * 1
		Select Case postmm
			Case 1 
				postmon = "Jan"
			Case 2 
				postmon = "Feb"
			Case 3 
				postmon = "Mar"	
			Case 4 
				postmon = "Apr"
			Case 5 
				postmon = "May"
			Case 6 
				postmon = "Jun"
			Case 7 
				postmon = "Jul"
			Case 8 
				postmon = "Aug"
			Case 9 
				postmon = "Sep"
			Case 10 
				postmon = "Oct"
			Case 11 
				postmon = "Nov"
			Case 12 
				postmon = "Dec"
		End Select 
		postdate = postdd & "-" & postmon & "-" & postyy 
		'msgbox "postdate " & postdate 
		fkn = "00000000002+" 
		'msgbox fkn 
		Set xml = CreateObject("Microsoft.XMLHTTP")   
		xml.Open "GET","Triggers/evolution/getcurper.asp?fkn="   & fkn, False  
		' Send the request and returns the data:   
		xml.Send  
		complete = xml.responseText 
		curper = complete
		'msgbox "period " & curper 
		Value = Date()
		'msgbox value
		If Not Isdate(Value)Then 
			rdate2 = 0 
		Else 
			d = day(value) 
			m = month(value) 
			y = year(value) 
			If  d < 10 Then 
				d = "0" & d
			End If
			If	m < 10 Then 
				m = "0" & m
			End If 
			rdate2 = y & m & d 
			'msgbox rdate2 
		End If
		rtime = time 
		'msgbox rtime 
		hh = mid(rtime,1,2) 
		mm = mid(rtime,4,2) 
		hhmm = (hh &  mm) 
		dt = (rdate2 & hhmm) 
		'msgbox dt 
		rect = "12"   
		Set xml = CreateObject("Microsoft.XMLHTTP")   
		xml.Open "GET","Triggers/evolution/insfure12.asp?rect=" & rect & "&curper=" & curper  & "&ordno=" & ordno &  "&dt=" & dt & "&predate=" & predate & "&postdate=" & postdate, False   
	' Send the request and returns the data:   
		xml.Send 
		complete = xml.responseText 
		'msgbox "back " & complete 
	End If 	
End If
EXVBSOR12_POSTSCREEN_ALL = trgReturnValue
End Function
