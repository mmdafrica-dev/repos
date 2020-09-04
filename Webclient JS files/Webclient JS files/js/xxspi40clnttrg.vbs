Function EXVBSPI40_POSTFIELD_IO_GL_CODE_1(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, wsGLCode, result
result = GetFieldValue("IO-GL-CODE(1)",wsGLCode)
If Trim(wsGLCode) = "21314" Then
    x = MsgBox("GL Code 21314 not allowed",4,"GL Code Validation")
    'result = SetFieldValue("IO-GL-CODE(1)","")
    'TrgReturnValue = False
End If
EXVBSPI40_POSTFIELD_IO_GL_CODE_1 = trgReturnValue
End Function

Function EXVBSPI40_POSTFIELD_IO_GL_CODE_2(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, wsGLCode, result
result = GetFieldValue("IO-GL-CODE(2)",wsGLCode)
If Trim(wsGLCode) = "21314" Then
    x = MsgBox("GL Code 21314 not allowed",4,"GL Code Validation")
    'result = SetFieldValue("IO-GL-CODE(2)","")
    'TrgReturnValue = False
End If
EXVBSPI40_POSTFIELD_IO_GL_CODE_2 = trgReturnValue
End Function

Function EXVBSPI40_POSTFIELD_IO_GL_CODE_3(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, wsGLCode, result
result = GetFieldValue("IO-GL-CODE(3)",wsGLCode)
If Trim(wsGLCode) = "21314" Then
    x = MsgBox("GL Code 21314 not allowed",4,"GL Code Validation")
End If
EXVBSPI40_POSTFIELD_IO_GL_CODE_3 = trgReturnValue
End Function

Function EXVBSPI40_POSTFIELD_IO_GL_CODE_4(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, wsGLCode, result
result = GetFieldValue("IO-GL-CODE(4)",wsGLCode)
If Trim(wsGLCode) = "21314" Then
    x = MsgBox("GL Code 21314 not allowed",4,"GL Code Validation")
    result = SetFieldValue("IO-GL-CODE(4)","")
    TrgReturnValue = False
End If
EXVBSPI40_POSTFIELD_IO_GL_CODE_4 = trgReturnValue
End Function

Function EXVBSPI40_POSTFIELD_IO_GL_CODE_5(Param1, Param2, Param3, Param4, Param5)
Dim trgReturnValue
trgReturnValue = True
Dim x, wsGLCode, result
result = GetFieldValue("IO-GL-CODE(5)",wsGLCode)
x = MsgBox("GL Code=" & Trim(wsGLCode) & "-")
If Trim(wsGLCode) = "21314" Then
    x = MsgBox("GL Code 21314 not allowed",4,"GL Code Validation")
    result = SetFieldValue("IO-GL-CODE(5)","")
    TrgReturnValue = False
End If
EXVBSPI40_POSTFIELD_IO_GL_CODE_5 = trgReturnValue
End Function
