function EXVBCBR24_POSTFIELD_IO_TRANS_TYPE(Param1, Param2, Param3, Param4, Param5){
    var x, read1, FieldObj;

    x = GetFieldValue("IO-TRANS-TYPE", FieldObj);
}

function EXVBCBR24_PRESCREEN_ALL(Param1, Param2, Param3, Param4, Param5){
    Setfocusonfield("IO-GROSS");
}

function EXVBCBR24_POSTFIELD_UF_BANKNAME_1(Param1, Param2, Param3, Param4, Param5) {
    var x, rect, bkbr, bkac, bknm, sess, sessno;
    } else {
}

function EXVBCBR24_POSTSCREEN_ALL(Param1, Param2, Param3, Param4, Param5){
    var x, rect, bkbr, bkac, bknm, sess, sessno, cnm, tt;
    x = GetFieldValue("IO-SESSION", FieldObj);
}

function ExvbCBR24_POSTFIELD_IO_LEDG_TYPE(Param1, Param2, Param3, Param4, Param5){
    var x, gcode;
    gcode = "CT";
    x = SetFieldValue("IO-GEN-CODE", gcode);
    Setfocusonfield("IO-GROSS");
}