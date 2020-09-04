function EXVBCBR24_POSTFIELD_IO_TRANS_TYPE(Param1, Param2, Param3, Param4, Param5){
    var x, read1, FieldObj;    FieldObj = new Object;

    x = GetFieldValue("IO-TRANS-TYPE", FieldObj);    read1 = FieldObj.value;    if(read1 == 40) {        FieldObj.value = "CASH";        x = SetFieldValue("IO-CUS-REF", FieldObj);        Setfocusonfield("IO-CUS-SUP");    }
}

function EXVBCBR24_PRESCREEN_ALL(Param1, Param2, Param3, Param4, Param5){    var x, f1, f2, FieldObj;    FieldObj = new Object;    //msgbox "Start"    FieldObj.value = 1;    x = SetFieldValue("IO-LEDG-TYPE", FieldObj);    FieldObj.value = "SJ";    x = SetFieldValue("IO-GEN-CODE", FieldObj);
    Setfocusonfield("IO-GROSS");
}

function EXVBCBR24_POSTFIELD_UF_BANKNAME_1(Param1, Param2, Param3, Param4, Param5) {
    var x, rect, bkbr, bkac, bknm, sess, sessno;    FieldObj = new Object;    x = GetFieldValue("IO-SESSION", FieldObj);    sess = FieldObj.value;    x = GetFieldValue("IO-ENTRY-NO", FieldObj);    sessno = FieldObj.value;    x = GetFieldValue("UF-BANKBR", FieldObj);    bkbr = FieldObj.value;    x = GetFieldValue("UF-BANKACC", FieldObj);    bkac = FieldObj.value;    x = GetFieldValue("UF-BANKNAME", FieldObj);    bknm = FieldObj.value;    rect = "240";    rect = rect * 1;    sessno = sessno * 1;    alert(bknm);    //Create an xmlhttp object:      if (bknm > "") {        alert("bknm more");        xml = CreateObject("Microsoft.XMLHTTP");        alert("off to insert");        complete = GetXMLurl("Triggers/evolution/getsessins.asp?rect=" & rect & "&sess=" & sess & "&sessno=" & sessno & "&bkbr=" & bkbr & "&bkac=" & bkac & "&bknm=" & bknm);        alert(complete);        //alert("finish update");	
    } else {        alert("bkspace?");    }
}

function EXVBCBR24_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5){
    var x, rect, bkbr, bkac, bknm, sess, sessno, cnm, tt;
    x = GetFieldValue("IO-SESSION", FieldObj);    sess = FieldObj.value;    x = GetFieldValue("IO-ENTRY-NO", FieldObj);    sessno = FieldObj.value;    x = GetFieldValue("UF-BANKBR", FieldObj);    bkbr = FieldObj.value;    x = GetFieldValue("UF-BANKACC", FieldObj);    bkac = FieldObj.value;    x = GetFieldValue("UF-BANKNAME", FieldObj);    bknm = FieldObj.value;    x = GetFieldValue("UF-CNAME", FieldObj);    cnm = FieldObj.value;    x = GetFieldValue("IO-TRANS-TYPE", FieldObj);    tt = FieldObj.value;    rect = "240";    rect = rect * 1;    sessno = sessno * 1;    complete = GetXMLurl("Triggers/evolution/getsessins.asp?rect=" & rect & "&sess=" & sess & "&sessno=" & sessno & "&bkbr=" & bkbr & "&bkac=" & bkac & "&bknm=" & bknm & "&cnm=" & cnm & "&tt=" & tt);
}

function ExvbCBR24_POSTFIELD_IO_LEDG_TYPE(Param1, Param2, Param3, Param4, Param5){
    var x, gcode;
    gcode = "CT";
    x = SetFieldValue("IO-GEN-CODE", gcode);
    Setfocusonfield("IO-GROSS");
}