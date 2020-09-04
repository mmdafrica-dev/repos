Imports System.Windows
Imports System.IO
Imports Microsoft.Office.Interop.Word
Imports Spire.Pdf
Imports Spire.Pdf.Security
Imports System.Net.Mail
Imports EASendMail

Public Class Form1
    Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

    Dim wsAppPath As String = My.Application.Info.DirectoryPath
    Dim WordApp As New Microsoft.Office.Interop.Word.Application
    Dim worddoc As New Microsoft.Office.Interop.Word.Document

    Dim File As System.IO.File
    Dim oWrite As System.IO.StreamWriter

    Dim evl As New EventLogger
    Dim bResult As Boolean
    Dim wsSrcDir, wsTmpSrcDir, wsTempDir, wsTmpDir, wsPdfDir
    Dim wsNewDir, wsFile, wsType, wsSaveDoc, wsField, wsTestTimeParam
    Dim wsprinter, wsCopies
    Dim wsCo
    Dim wsPdfpDir, wsSavePdf, wsSavePdf1, wsPDFSecurity
    Dim wsCustomer, wsOrder, wsInvoice, wsSupplier, wsCust, wsSecurity, wsEncryptFile
    Dim wsCompany, wsDocument
    Dim s, fso, inf, stopPos, logfile, logfilepath
    Dim RecordArray As String()
    Dim wsRecType
    Dim wsValue, wsTmpVal
    Dim wsJobDir As String
    Dim fil As Object
    Dim LineArray(50, 6) As Object
    Dim TwoLineArray(50, 2) As Object
    Dim HnarrArray()
    Dim InarrArray()
    Dim LnarrArray()
    Dim OdLinestring As String
    Dim linestring As String
    Dim Hnarrstring As String
    Dim Inarrstring As String
    Dim LnarrString As String

    Dim i As Integer, j As Integer
    Dim NewResult
    Dim strSubject As String
    Dim wsEmailServer As String
    Dim wsEmailPort As String
    Dim wsEmailUser As String
    Dim wsEmailPass As String
    Dim wsEmailFrom, wsEmailTo, wsEmailFile
    Dim wsFromEmail, wsToEmail, wsDistEmail, wsCcEmail
    Dim wsSignature, wsSigImage
    Dim wsEmailDir, wsDefEmail
    Dim wsSubject, wsMsg
    Dim wsTestTime
    Dim pdfprint
    Dim wspdfPrintString
    Dim wsDefPrinter
    Dim wsUserPrinterPath
    Dim wsUserPrinter
    Dim wsSecondPrinter
    Dim wsAmended
    Dim wsNow, errlog
    Dim wsTandCs
    Dim wsSpaces As String
    Dim wsCopy As Boolean
    Dim Errtxt
    Dim wsSuppressPrint As Boolean
    Dim PickListArray(250, 12)
    Dim PickListArray1(250, 12)
    Dim RetStr As String
    Dim RetStr1 As String
    Dim RetStr2 As String
    Dim MyRange As Microsoft.Office.Interop.Word.Range
    Dim MyTable1 As Microsoft.Office.Interop.Word.Table
    Dim MyCell As Microsoft.Office.Interop.Word.Cell
    Dim MyCells As Microsoft.Office.Interop.Word.Cells
    Dim MyCols As Microsoft.Office.Interop.Word.Columns
    Dim RetSt As String
    Dim wsDocType As String
    Dim wsSavJob As String
    Dim wsOrderStatus As Integer
    Dim wsOrderText As String


    Dim wsQuoteNarr As String
    Dim paramExportFormat As WdExportFormat = WdExportFormat.wdExportFormatPDF
    Dim paramOpenAfterExport As Boolean = False
    Dim paramExportOptimizeFor As WdExportOptimizeFor = WdExportOptimizeFor.wdExportOptimizeForPrint
    Dim paramExportRange As WdExportRange = WdExportRange.wdExportAllDocument
    Dim paramStartPage As Int32 = 0
    Dim paramEndPage As Int32 = 0
    Dim paramExportItem As WdExportItem = WdExportItem.wdExportDocumentContent
    Dim paramIncludeDocProps As Boolean = True
    Dim paramKeepIRM As Boolean = True
    Dim paramCreateBookmarks As WdExportCreateBookmarks = WdExportCreateBookmarks.wdExportCreateWordBookmarks
    Dim paramDocStructureTags As Boolean = True
    Dim paramBitmapMissingFonts As Boolean = True
    Dim paramUseISO19005_1 As Boolean = False
    Dim sOrdRev As Integer

    Dim wsLaserFicheInUse As Boolean
    Dim wsLFJob As String
    Dim wsLFDoc As String
    Dim wsLFDocType As String
    Dim wsLFDate As String

    Dim wsBJob As String
    Dim wsBInvDate As String
    Dim wsBDesp As String
    Dim wsBCustRef As String

    Dim wsErrLogName As String
    Dim wsBCarrier As String
    Dim wsBCarrierStr As String

    Dim wsemailBody As String
    Dim wsemailSubject As String

    Dim wsInvText As String
    Dim DebugOn As Boolean = False

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        ' So that we only need to set the title of the application once,
        ' we use the AssemblyInfo class (defined in the AssemblyInfo.vb file)
        ' to read the AssemblyTitle attribute.
        '        Dim ainfo As New AssemblyInfo()

        '       Me.Text = ainfo.Title
        '      Me.mnuAbout.Text = String.Format("&About {0} ...", ainfo.Title)

    End Sub
#End Region

    Private Sub form1_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '       WordApp = CreateObject("Word.Application")
        wsNow = Format(Now, "ddmmyyhhmmss")
        wsErrLogName = (wsAppPath & "\logs\err" & wsNow & ".log")
        If DebugOn = True Then
            WordApp.Visible = True
            WordApp.Activate()
        End If

        init_params()
        loop_section()
        subUnload()
    End Sub

    Private Sub init_params()
        Dim stopPos As Integer

        Try
            wsCopies = 1

            oWrite = File.CreateText(wsErrLogName)
            oWrite.WriteLine("Log opened at " & Now)

            fso = CreateObject("Scripting.FileSystemObject")
            inf = fso.OpenTextFile(wsAppPath & "\wordconfig.conf")
            i = 1
            Do Until inf.AtEndOfStream
                Select Case i
                    Case 1
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsSrcDir = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 2
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsNewDir = Trim$(Mid(s, 1, stopPos - 2))
                    Case 3
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsTempDir = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 4
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsTmpDir = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 5
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsEmailServer = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 6
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsEmailPort = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 7
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsPdfDir = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 8
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsCompany = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 9
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsEmailDir = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 10
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsTestTimeParam = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 11
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsDefEmail = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 12
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsDefPrinter = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 13
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsPdfpDir = Trim$(Mid$(s, 1, stopPos - 2))
                    Case 14
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsUserPrinterPath = Trim$(Mid$(s, 1, stopPos - 2))
                        ' 1.0.4 second printer definition
                    Case 15
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsSecondPrinter = Trim$(Mid$(s, 1, stopPos - 2))
                        ' 1.0.4 end
                    Case 16
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        wsJobDir = Trim$(Mid$(s, 1, stopPos - 2))
                        ' 1.0.4 end
                    Case 17
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        If Trim$(Mid$(s, 1, stopPos - 2)) = "True" Then
                            wsSuppressPrint = True
                        Else
                            wsSuppressPrint = False
                            ' 1.0.4 end
                        End If
                    Case 18
                        s = inf.ReadLine
                        stopPos = InStrRev(s, "#")
                        If Trim$(Mid$(s, 1, stopPos - 2)) = "Y" Then
                            wsLaserFicheInUse = True
                        Else
                            wsLaserFicheInUse = False
                        End If
                End Select
                i = i + 1
            Loop
            inf.Close()

            inf = Nothing
            fso = Nothing
            oWrite.WriteLine("wsSrcDir = " & wsSrcDir)
            oWrite.WriteLine("wsNewDir = " & wsNewDir)
            oWrite.WriteLine("wsTempDir = " & wsTempDir)
            oWrite.WriteLine("wsTmpDir = " & wsTmpDir)
            oWrite.WriteLine("wsEmailServer = " & wsEmailServer)
            oWrite.WriteLine("wsEmailPort = " & wsEmailPort)
            oWrite.WriteLine("wsPdfDir = " & wsPdfDir)
            oWrite.WriteLine("wsCompany = " & wsCompany)
            oWrite.WriteLine("wsEmailDir = " & wsEmailDir)
            oWrite.WriteLine("wsTestTime = " & wsTestTimeParam)
            oWrite.WriteLine("wsDefEmail = " & wsDefEmail)
            oWrite.WriteLine("wsDefPrinter = " & wsDefPrinter)
            oWrite.WriteLine("wsPdfpDir = " & wsPdfpDir)
            oWrite.WriteLine("wsUserPrinterPath = " & wsUserPrinterPath)
            oWrite.WriteLine("wsSecondPrinter = " & wsSecondPrinter)
            oWrite.WriteLine("wsJobDir = " & wsJobDir)
            oWrite.WriteLine("wsSuppressPrint = " & wsSuppressPrint)
            oWrite.WriteLine("wsLaserFicheInUse = " & wsLaserFicheInUse)
            oWrite.Flush()

        Catch ex As Exception
            Debug.Print(Err.Number)
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in init params")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub loop_section()
        '        On Error GoTo ErrHandler
        Try
            Do While CInt(wsTestTime) < CInt(wsTestTimeParam)
                wsFile = Dir(wsSrcDir & "\*.TXT")
                If wsFile > "" Then
                    wsSuppressPrint = False
                    select_document()
                    process_file()
                End If
                Sleep(1000)
                wsTestTime = Hour(Now)
            Loop

            '            Exit Sub
            'ErrHandler:
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in loop")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub select_document()
        wsDocument = Mid$(wsFile, 1, 2)
        wsCo = Mid$(wsFile, 4, 2)
    End Sub

    Private Sub process_file()
        '        On Error GoTo ErrHandler
        Try


            wsSpaces = "      "
            fso = CreateObject("Scripting.FileSystemObject")
            If wsCo = "C1" Then
                inf = fso.OpenTextFile(wsSrcDir & "\" & wsFile, 1, False, -2)
            Else
                inf = fso.OpenTextFile(wsSrcDir & "\" & wsFile)
            End If
            oWrite.WriteLine("Processing File " & wsFile)
            i = 0
            ReDim Preserve RecordArray(i)
            Do Until inf.AtEndOfStream
                RecordArray(i) = inf.ReadLine
                If Trim$(Mid$(RecordArray(i), 1, 5)) = "*EOR*" Then
                    extract_lines()
                    ' process completed document
                    i = 0
                    ReDim RecordArray(0)
                Else
                    i = i + 1
                    ReDim Preserve RecordArray(i)
                End If
                '       Debug.Print inf.ReadLine
            Loop
            inf.Close()
            inf = Nothing
            fso = Nothing
            If File.Exists(wsSrcDir & "\complete\" & wsFile) Then
                'Kill(wsSrcDir & "\" & wsFile)
                Kill(wsSrcDir & "\complete\" & wsFile)
            End If
            File.Move(wsSrcDir & "\" & wsFile, wsSrcDir & "\complete\" & wsFile)


            '            Exit Sub
            'ErrHandler:
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process file")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.WriteLine(Err.Erl)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub extract_lines()
        Try

            Select Case UCase$(wsDocument)
                Case "SO"
                    subProcessAck()
                Case "SI"
                    subProcessInvoice()
                Case "PO"
                    subProcessPurchaseOrder()
                Case "DN"
                    subProcessDespatchNote()
                Case "RA"
                    subProcessRemittance()
                Case "CN"
                    subProcessCreditNote()
                Case "ST"
                    subProcessStatement()
                Case "PE"
                    subProcessPerryInv()
                Case "BI"
                    subProcessBeijingInv()
                Case "BL"
                    subProcessBeijingPl()
                Case "CO"
                    subProcessCerts()
                Case "CQ"
                    subProcessCQ()
                Case "PL"
                    subProcessPL()
                Case "PR"
                    subProcessProForma()
                Case "DW"
                    subProcessDW()
                Case "SM"
                    subProcessSM()
                Case "QA"
                    subProcessQA()
                Case Else
                    oWrite.WriteLine("Unable to process file Type " & UCase$(wsDocument) & "")
                    oWrite.Flush()
            End Select

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in extract lines")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try

    End Sub

    Private Sub subProcessAck()

        '        On Error GoTo ErrHandler
        oWrite.WriteLine("Before try1 process ack")
        oWrite.Flush()
        If UBound(RecordArray) < 1 Then
            oWrite.WriteLine("No processable lines " & Now & " SO")
            Exit Sub
        End If
        wsCopies = 1
        Try
            wsQuoteNarr = ""
            wsOrderText = ""
            linestring = ""
            wsDistEmail = ""
            wsToEmail = ""
            wsCcEmail = ""
            wsSuppressPrint = True

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr ' & vbCrL
                End If
            Next
            If InStr(linestring, "INCOTERMS", CompareMethod.Text) > 0 Then
                linestring = Replace(linestring, "INCOTERMS", "INCOTERMS®", , , CompareMethod.Text)
            End If

            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 120))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr ' & vbCrLf
            Next

            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*A*" Then
                    wsOrderStatus = CInt(Trim$(Mid$(RecordArray(i), 4, 4)))
                End If
            Next

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*E*" Then
                    wsToEmail = RTrim$(Mid$(RecordArray(i), 4, 60))
                    If wsToEmail = "miems@mmdafrica.co.za" Then
                        wsSuppressPrint = True
                    End If
                End If
            Next

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*S*" Then
                    wsCcEmail = RTrim$(Mid$(RecordArray(i), 4, 60))
                End If
            Next

            If (wsCo = "SA") Or (wsCo = "TC") Then
                If wsOrderStatus = 2 Then
                    wsOrderText = "SALES ORDER QUOTATION"
                    wsQuoteNarr = "Thank you for the enquiry herewith our quotation as per your request." & vbCr
                    wsQuoteNarr = wsQuoteNarr & "Which we are pleased to confirm on the basis of our General Conditions of Sale." & vbCr
                Else
                    wsOrderText = "SALES ORDER ACKNOWLEDGEMENT"
                    wsQuoteNarr = "Thank you for your order, which we are pleased to confirm on the basis of our" & vbCr
                    wsQuoteNarr = wsQuoteNarr & "General Conditions of Sale. (See attached)" & vbCr
                End If
            End If
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process ack1")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
        Try
            If IsNumeric(Trim$(Mid$(RecordArray(2), 1, 3))) Then
                sOrdRev = CInt(Trim$(Mid$(RecordArray(2), 1, 3)))
            End If
            If (wsToEmail > "" Or wsDistEmail > "") Then
                If wsFromEmail = "" Then
                    If wsCo = "SA" Or wsCo = "TC" Then
                        wsFromEmail = "mtms.admin@mmdafrica.co.za"
                    End If
                End If
                wsEmailFrom = "<" & wsFromEmail & ">"
                wsEmailTo = "<" & wsToEmail & ">"
                wsDistEmail = wsDistEmail
                wsemailBody = "For your Attention"
                wsemailSubject = wsOrderText & " " & Trim$(Mid$(RecordArray(20), 25, 12)) & " Attached for Customer: " & Trim$(Mid$(RecordArray(11), 1, 36))
            Else
                wsEmailFrom = "<" & wsDefEmail & ">"
                wsEmailTo = "<" & wsDefEmail & ">"
            End If

            wsCustomer = Trim$(Mid$(RecordArray(11), 1, 36))
            wsOrder = Trim$(Mid$(RecordArray(20), 25, 12))
            If wsCo = "01" Then
                If Trim$(Mid$(RecordArray(18), 25, 12)) = "CU1082" Then
                    wsCopies = 1
                Else
                    wsCopies = 2
                End If
            End If
            If wsCo = "02" Then
                wsCopies = 2
            End If
            If wsCo = "03" Then
                wsCopies = 2
            End If
            If wsCo = "04" Then
                wsCopies = 2
            End If
            If wsCo = "05" Then
                wsCopies = 2
            End If

            '           ReDim UsedVariables(1)
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process ack2")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try

        Try
            oWrite.WriteLine("Word open next process ack")
            worddoc = WordApp.Documents.Open(wsTempDir & "\SO_" & wsCo & "_template.dotx")
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process adocopen doc")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try

        Try
            If UCase$(wsDocument) = "SO" Then
                If wsOrderStatus = 2 Then
                    '            wsSaveDoc = "QUOTE_" & wsSaveDoc
                    wsSaveDoc = wsNewDir & "\QUOTE_" & wsCo & Trim$(Mid$(RecordArray(20), 25, 12)) & ".doc"
                Else
                    If wsCo = "IM" Or wsCo = "CN" Or wsCo = "SA" Or wsCo = "TC" Then
                        wsSaveDoc = wsNewDir & "\" & wsCo & Trim$(Mid$(RecordArray(20), 25, 7)) & "_" & Trim(CStr(sOrdRev)) & ".doc"
                    Else
                        wsSaveDoc = wsNewDir & "\" & wsCo & Trim$(Mid$(RecordArray(20), 25, 7)) & ".doc"
                        '            wsSaveDoc = wsSaveDoc
                    End If
                End If
            End If
            worddoc.SaveAs(wsSaveDoc)
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process asave doc")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try

        Try
            worddoc.Close()
            worddoc = Nothing
            worddoc = WordApp.Documents.Open(wsSaveDoc)
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process acloseopen doc")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try

        '        Try
        '        oWrite.WriteLine("Word open process ack")
        '        oWrite.WriteLine("Variable Cust" & worddoc.Variables("CUSTOMER").Value)
        '        oWrite.Flush()
        '        Catch ex As Exception
        '        oWrite.WriteLine("Error Occurred at " & Now & " in process awriteline doc")
        '        oWrite.WriteLine(Err)
        '        oWrite.WriteLine(Err.Number)
        '        oWrite.WriteLine(Err.Description)
        '        oWrite.Flush()
        '        End Try

        Try
            With worddoc
                oWrite.WriteLine("Variable CUSTOMER" & Trim$(Mid$(RecordArray(11), 1, 36)))
                oWrite.Flush()
                If Trim$(Mid$(RecordArray(11), 1, 36)) > "" Then
                    .Variables("CUSTOMER").Value = Trim$(Mid$(RecordArray(11), 1, 36))
                Else
                    .Variables("CUSTOMER").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR1" & Trim$(Mid$(RecordArray(12), 1, 36)))
                oWrite.Flush()
                If Trim$(Mid$(RecordArray(12), 1, 36)) > "" Then
                    .Variables("ADDR1").Value = Trim$(Mid$(RecordArray(12), 1, 36))
                Else
                    .Variables("ADDR1").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR2" & Trim$(Mid$(RecordArray(13), 1, 36)))
                If Trim$(Mid$(RecordArray(13), 1, 36)) > "" Then
                    .Variables("ADDR2").Value = Trim$(Mid$(RecordArray(13), 1, 36))
                Else
                    .Variables("ADDR2").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR3" & Trim$(Mid$(RecordArray(14), 1, 36)))
                If Trim$(Mid$(RecordArray(14), 1, 36)) > "" Then
                    .Variables("ADDR3").Value = Trim$(Mid$(RecordArray(14), 1, 36))
                Else
                    .Variables("ADDR3").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR4" & Trim$(Mid$(RecordArray(15), 1, 36)))
                If Trim$(Mid$(RecordArray(15), 1, 36)) > "" Then
                    .Variables("ADDR4").Value = Trim$(Mid$(RecordArray(15), 1, 36))
                Else
                    .Variables("ADDR4").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable PCODE" & Trim$(Mid$(RecordArray(16), 1, 36)))
                If Trim$(Mid$(RecordArray(16), 1, 36)) > "" Then
                    .Variables("PCODE").Value = Trim$(Mid$(RecordArray(16), 1, 36))
                Else
                    .Variables("PCODE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD1" & Trim$(Mid$(RecordArray(11), 38, 36)))
                If Trim$(Mid$(RecordArray(11), 38, 36)) > "" Then
                    .Variables("DADD1").Value = Trim$(Mid$(RecordArray(11), 38, 36))
                Else
                    .Variables("DADD1").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD2" & Trim$(Mid$(RecordArray(12), 38, 36)))
                If Trim$(Mid$(RecordArray(12), 38, 36)) > "" Then
                    .Variables("DADD2").Value = Trim$(Mid$(RecordArray(12), 38, 36))
                Else
                    .Variables("DADD2").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD3" & Trim$(Mid$(RecordArray(13), 38, 36)))
                If Trim$(Mid$(RecordArray(13), 38, 36)) > "" Then
                    .Variables("DADD3").Value = Trim$(Mid$(RecordArray(13), 38, 36))
                Else
                    .Variables("DADD3").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD4" & Trim$(Mid$(RecordArray(14), 38, 36)))
                If Trim$(Mid$(RecordArray(14), 38, 36)) > "" Then
                    .Variables("DADD4").Value = Trim$(Mid$(RecordArray(14), 38, 36))
                Else
                    .Variables("DADD4").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD5" & Trim$(Mid$(RecordArray(15), 38, 36)))
                If Trim$(Mid$(RecordArray(15), 38, 36)) > "" Then
                    .Variables("DADD5").Value = Trim$(Mid$(RecordArray(15), 38, 36))
                Else
                    .Variables("DADD5").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DPCODE" & Trim$(Mid$(RecordArray(16), 38, 12)))
                If Trim$(Mid$(RecordArray(16), 38, 12)) > "" Then
                    .Variables("DPCODE").Value = Trim$(Mid$(RecordArray(16), 38, 12))
                Else
                    .Variables("DPCODE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ORDER" & Trim$(Mid$(RecordArray(20), 25, 7)))
                If Trim$(Mid$(RecordArray(20), 25, 12)) > "" Then
                    If wsCo = "IM" Or wsCo = "CN" Then
                        .Variables("ORDER").Value = Trim$(Mid$(RecordArray(20), 25, 7)) & "_" & Trim(CStr(sOrdRev))
                    Else
                        .Variables("ORDER").Value = Trim$(Mid$(RecordArray(20), 25, 12))

                    End If
                Else
                    .Variables("ORDER").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable VATNO" & Trim$(Mid$(RecordArray(18), 69, 20)))
                If Trim$(Mid$(RecordArray(18), 69, 20)) > "" Then
                    .Variables("VATNO").Value = Trim$(Mid$(RecordArray(18), 69, 20))
                Else
                    .Variables("VATNO").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable REQDATE" & Trim$(Mid$(RecordArray(22), 25, 8)))
                If Trim$(Mid$(RecordArray(22), 25, 8)) > "" Then
                    .Variables("REQDATE").Value = Trim$(Mid$(RecordArray(22), 25, 8))
                Else
                    .Variables("REQDATE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable PROMDATE" & Trim$(Mid$(RecordArray(22), 69, 8)))
                If Trim$(Mid$(RecordArray(22), 69, 8)) > "" Then
                    .Variables("PROMDATE").Value = Trim$(Mid$(RecordArray(22), 69, 8))
                Else
                    .Variables("PROMDATE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ENTDATE" & Trim$(Mid$(RecordArray(1), 1, 8)))
                If Trim$(Mid$(RecordArray(1), 1, 8)) > "" Then
                    .Variables("ENTDATE").Value = Trim$(Mid$(RecordArray(1), 1, 8))
                Else
                    .Variables("ENTDATE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable CURRENCY" & Trim$(Mid$(RecordArray(17), 1, 6)))
                If Trim$(Mid$(RecordArray(17), 1, 6)) > "" Then
                    .Variables("CURRENCY").Value = Trim$(Mid$(RecordArray(17), 1, 6))
                Else
                    .Variables("CURRENCY").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable CUSTREF" & Trim$(Mid$(RecordArray(20), 69, 26)))
                If Trim$(Mid$(RecordArray(20), 69, 20)) > "" Then
                    .Variables("CUSTREF").Value = Trim$(Mid$(RecordArray(20), 69, 26))
                Else
                    .Variables("CUSTREF").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable CUSNO" & Trim$(Mid$(RecordArray(18), 25, 12)))
                If Trim$(Mid$(RecordArray(18), 25, 12)) > "" Then
                    .Variables("CUSNO").Value = Trim$(Mid$(RecordArray(18), 25, 12))
                Else
                    .Variables("CUSNO").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable COMREP" & Trim$(Mid$(RecordArray(24), 13, 20)))
                If Trim$(Mid$(RecordArray(24), 13, 20)) > "" Then
                    .Variables("COMREP").Value = Trim$(Mid$(RecordArray(24), 13, 20))
                Else
                    .Variables("COMREP").Value = wsSpaces
                End If
                If wsOrderText > "" Then
                    oWrite.WriteLine("Variable TITLE" & wsOrderText)
                    oWrite.WriteLine("Variable ORDERNARR" & wsQuoteNarr)
                    .Variables("TITLE").Value = wsOrderText
                    .Variables("ORDERNARR").Value = wsQuoteNarr
                End If
                .Variables("HNARR").Value = Hnarrstring
                .Variables("PRODUCT").Value = linestring
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection
            oWrite.Flush()
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process ack3")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try

        Try
            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            worddoc.Save()
            worddoc.Close()
            worddoc = Nothing
            oWrite.WriteLine("Word closed process ack")
            oWrite.Flush()
            ' Write PDF

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process ack4")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try
        wsCopies = 1
        Print_PDF()
    End Sub

    Private Sub subProcessInvoice()
        Dim i As Integer
        Try
            linestring = ""
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr ' vbCrLf
                End If
            Next
            wsValue = 0
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*F*" Then
                    wsValue = RTrim$(Mid$(RecordArray(i), 77, 13))
                End If
            Next

            ReDim HnarrArray(1)
            j = 1
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr
            Next

            ReDim LnarrArray(1)
            j = 1
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*F*" Then
                    LnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 120))
                    j = j + 1
                    ReDim Preserve LnarrArray(j)
                End If
            Next

            LnarrString = ""
            For i = 0 To UBound(LnarrArray) - 1
                LnarrString = LnarrString & LnarrArray(i) & vbCr
            Next

            wsUserPrinter = ""

            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*E*" Then
                    wsToEmail = RTrim$(Mid$(RecordArray(i), 4, 300)) & ";debtors@mmdafrica.co.za"
                End If
            Next
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*I*" Then
                    wsEmailFile = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next
            wsCopy = False
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 6) = "*copy*" Then
                    wsCopy = True
                End If
            Next

            wsCopies = 1
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*C*" Then
                    wsCopies = CInt(RTrim$(Mid$(RecordArray(i), 4, 2)))
                End If
            Next


            wsFromEmail = "<" & wsDefEmail & ">"
            wsCust = Trim$(Mid$(RecordArray(14), 88, 8))
            wsSecurity = Trim$(Mid$(RecordArray(13), 88, 30))
            wsCustomer = Trim$(Mid$(RecordArray(11), 1, 36))
            wsInvoice = Trim$(Mid$(RecordArray(10), 89, 12))
            wsPDFSecurity = Trim$(Mid$(RecordArray(13), InStr(1, RecordArray(13), "VAT REG") + 7, 30))
            wsemailBody = "Please review and advise"
            wsemailSubject = "Sales Invoice " & wsInvoice & " Attached"

            If wsCo = "SA" Or wsCo = "TC" Then
                If wsCopy = True Then
                    wsInvText = "COPY "
                Else
                    wsInvText = " "
                End If
            End If

            worddoc = WordApp.Documents.Open(wsTempDir & "\SI_" & wsCo & "_template.dotx")
            With worddoc

                If wsCo = "SA" Or wsCo = "TC" Then
                    .Variables("AMENDED").Value = wsInvText
                Else
                    .Variables("AMENDED").Value = wsSpaces
                End If

                If Trim$(Mid$(RecordArray(11), 1, 36)) > "" Then
                    .Variables("CUSTOMER").Value = Trim$(Mid$(RecordArray(11), 1, 36))
                Else
                    .Variables("CUSTOMER").Value = wsSpaces
                End If

                If Trim$(Mid$(RecordArray(12), 1, 36)) > "" Then
                    .Variables("ADDR1").Value = Trim$(Mid$(RecordArray(12), 1, 36))
                Else
                    .Variables("ADDR1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(13), 1, 36)) > "" Then
                    .Variables("ADDR2").Value = Trim$(Mid$(RecordArray(13), 1, 36))
                Else
                    .Variables("ADDR2").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(14), 1, 36)) > "" Then
                    .Variables("ADDR3").Value = Trim$(Mid$(RecordArray(14), 1, 36))
                Else
                    .Variables("ADDR3").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(15), 1, 36)) > "" Then
                    .Variables("ADDR4").Value = Trim$(Mid$(RecordArray(15), 1, 36))
                Else
                    .Variables("ADDR4").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(16), 1, 12)) > "" Then
                    .Variables("PCODE").Value = Trim$(Mid$(RecordArray(16), 1, 12))
                Else
                    .Variables("PCODE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(11), 38, 36)) > "" Then
                    .Variables("DADD1").Value = Trim$(Mid$(RecordArray(11), 38, 36))
                Else
                    .Variables("DADD1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(12), 38, 36)) > "" Then
                    .Variables("DADD2").Value = Trim$(Mid$(RecordArray(12), 38, 36))
                Else
                    .Variables("DADD2").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(13), 38, 36)) > "" Then
                    .Variables("DADD3").Value = Trim$(Mid$(RecordArray(13), 38, 36))
                Else
                    .Variables("DADD3").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(14), 38, 36)) > "" Then
                    .Variables("DADD4").Value = Trim$(Mid$(RecordArray(14), 38, 36))
                Else
                    .Variables("DADD4").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(15), 38, 36)) > "" Then
                    .Variables("DADD5").Value = Trim$(Mid$(RecordArray(15), 38, 36))
                Else
                    .Variables("DADD5").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(16), 38, 12)) > "" Then
                    .Variables("DPCODE").Value = Trim$(Mid$(RecordArray(16), 38, 12))
                Else
                    .Variables("DPCODE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(10), 88, 12)) > "" Then
                    .Variables("INVOICE").Value = Trim$(Mid$(RecordArray(10), 88, 12))
                Else
                    .Variables("INVOICE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(13), 88, 20)) > "" Then
                    .Variables("VATNO").Value = Trim$(Mid$(RecordArray(13), 88, 20))
                Else
                    .Variables("VATNO").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(11), 89, 8)) > "" Then
                    .Variables("INVDATE").Value = Trim$(Mid$(RecordArray(11), 89, 8))
                Else
                    .Variables("INVDATE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(16), 89, 6)) > "" Then
                    .Variables("CURRENCY").Value = Trim$(Mid$(RecordArray(16), 89, 6))
                Else
                    .Variables("CURRENCY").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(17), 89, 17)) > "" Then
                    .Variables("UCR").Value = "UCR: " & Trim$(Mid$(RecordArray(17), 89, 23))
                Else
                    .Variables("UCR").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(12), 94, 22)) > "" Then
                    .Variables("CUSTREF").Value = Trim$(Mid$(RecordArray(12), 94, 26))
                Else
                    .Variables("CUSTREF").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(14), 89, 12)) > "" Then
                    .Variables("CUSNO").Value = Trim$(Mid$(RecordArray(14), 89, 12))
                Else
                    .Variables("CUSNO").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(15), 82, 30)) > "" Then
                    .Variables("TERMS").Value = Trim$(Mid$(RecordArray(15), 82, 30))
                Else
                    .Variables("TERMS").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(1), 1, 12)) > "" Then
                    .Variables("JOB").Value = Trim$(Mid$(RecordArray(1), 1, 12))
                Else
                    .Variables("JOB").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(1), 13, 12)) > "" Then
                    .Variables("DESP").Value = Trim$(Mid$(RecordArray(1), 13, 12))
                Else
                    .Variables("DESP").Value = wsSpaces
                End If
                If Hnarrstring > "" Then
                    .Variables("HNARR").Value = Hnarrstring
                Else
                    .Variables("HNARR").Value = wsSpaces
                End If
                If LnarrString > "" Then
                    .Variables("FNARR").Value = LnarrString
                Else
                    .Variables("FNARR").Value = wsSpaces
                End If
                If linestring > "" Then
                    .Variables("PRODUCT").Value = linestring
                Else
                    .Variables("PRODUCT").Value = wsSpaces
                End If

                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection

            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsSaveDoc = wsNewDir & "\" & Trim$(Mid$(RecordArray(10), 89, 12)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            worddoc.Close()
            worddoc = Nothing

            oWrite.WriteLine("Word closed invoice")
            oWrite.Flush()
            Print_PDF()

            Exit Sub

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process invoice")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessPurchaseOrder()
        wsSuppressPrint = True
        Try

            If UBound(RecordArray) < 1 Then
                oWrite.WriteLine("No processable lines " & Now & " PO")
                Exit Sub
            End If

            wsCopies = 1

            linestring = ""
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr ' vbCrLf
                End If
            Next

            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next
            wsAmended = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*A*" Then
                    wsAmended = Trim$(Mid$(RecordArray(i), 4, 20))
                    wsAmended = wsAmended & " "
                End If
            Next

            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 120))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr ' vbCrLf
            Next

            ReDim InarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*I*" Then
                    InarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 120))
                    j = j + 1
                    ReDim Preserve InarrArray(j)
                End If
            Next

            Inarrstring = ""
            For Me.i = 0 To UBound(InarrArray) - 1
                Inarrstring = Inarrstring & InarrArray(i) & vbCr ' vbCrLf
            Next

            wsFromEmail = ""
            wsToEmail = ""
            wsSignature = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*E*" Then
                    wsToEmail = RTrim$(Mid$(RecordArray(i), 4, 60))
                End If
            Next

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*S*" Then
                    wsDistEmail = RTrim$(Mid$(RecordArray(i), 4, 60))
                End If
            Next

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*D*" Then
                    wsSignature = RTrim$(Mid$(RecordArray(i), 4, 60))
                End If
            Next

            wsCopy = False
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 6) = "*copy*" Then
                    wsCopy = True
                    wsAmended = "AMENDED - "
                End If
            Next

            If (wsToEmail > "" Or wsDistEmail > "") Then
                If wsFromEmail = "" Then
                    If wsCo = "SA" Or wsCo = "TC" Then
                        wsFromEmail = "mtms.admin@mmdafrica.co.za"
                    End If
                End If
                wsEmailFrom = "<" & wsFromEmail & ">"
                wsEmailTo = "<" & wsToEmail & ">"
                wsDistEmail = "<" & wsDistEmail & ">"
            Else
                wsEmailFrom = "<" & wsDefEmail & ">"
                wsEmailTo = "<" & wsDefEmail & ">"
            End If

            If wsSignature > "" Then
                If wsSignature = "WP" Then
                    wsSigImage = "d:\mtms\special\WPSignature.gif"
                End If
            End If

            wsemailBody = "Please review and advise"
            wsemailSubject = "Purchase order " & Trim$(Mid$(RecordArray(8), 96, 12)) & " Attached"
            wsSupplier = Trim$(Mid$(RecordArray(12), 96, 12))
            wsOrder = Trim$(Mid$(RecordArray(8), 96, 12))
            wsSubject = "Purchase order " & wsOrder & " attached for " & wsSupplier
            wsMsg = "Please review and advise "
            '            ReDim UsedVariables(1)

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process PO1")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try


        Try
            oWrite.WriteLine("Word open next process PO")
            worddoc = WordApp.Documents.Open(wsTempDir & "\PO_" & wsCo & "_template.dotx")
            oWrite.WriteLine("Word open process PO")
            oWrite.Flush()

            With worddoc
                oWrite.WriteLine("Variable CUSTOMER" & Trim$(Mid$(RecordArray(8), 1, 36)))
                If Trim$(Mid$(RecordArray(8), 1, 36)) > "" Then
                    .Variables("CUSTOMER").Value = Trim$(Mid$(RecordArray(8), 1, 36))
                Else
                    .Variables("CUSTOMER").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR1" & Trim$(Mid$(RecordArray(9), 1, 36)))
                If Trim$(Mid$(RecordArray(9), 1, 36)) > "" Then
                    .Variables("ADDR1").Value = Trim$(Mid$(RecordArray(9), 1, 36))
                Else
                    .Variables("ADDR1").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR2" & Trim$(Mid$(RecordArray(10), 1, 36)))
                If Trim$(Mid$(RecordArray(10), 1, 36)) > "" Then
                    .Variables("ADDR2").Value = Trim$(Mid$(RecordArray(10), 1, 36))
                Else
                    .Variables("ADDR2").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR3" & Trim$(Mid$(RecordArray(11), 1, 36)))
                If Trim$(Mid$(RecordArray(11), 1, 36)) > "" Then
                    .Variables("ADDR3").Value = Trim$(Mid$(RecordArray(11), 1, 36))
                Else
                    .Variables("ADDR3").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR4" & Trim$(Mid$(RecordArray(12), 1, 36)))
                If Trim$(Mid$(RecordArray(12), 1, 36)) > "" Then
                    .Variables("ADDR4").Value = Trim$(Mid$(RecordArray(12), 1, 36))
                Else
                    .Variables("ADDR4").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable PCODE" & Trim$(Mid$(RecordArray(13), 1, 36)))
                If Trim$(Mid$(RecordArray(13), 1, 36)) > "" Then
                    .Variables("PCODE").Value = Trim$(Mid$(RecordArray(13), 1, 36))
                Else
                    .Variables("PCODE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD1" & Trim$(Mid$(RecordArray(9), 40, 36)))
                If Trim$(Mid$(RecordArray(9), 40, 36)) > "" Then
                    .Variables("DADD1").Value = Trim$(Mid$(RecordArray(9), 40, 36))
                Else
                    .Variables("DADD1").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD2" & Trim$(Mid$(RecordArray(10), 40, 36)))
                If Trim$(Mid$(RecordArray(10), 40, 36)) > "" Then
                    .Variables("DADD2").Value = Trim$(Mid$(RecordArray(10), 40, 36))
                Else
                    .Variables("DADD2").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD3" & Trim$(Mid$(RecordArray(11), 40, 36)))
                If Trim$(Mid$(RecordArray(11), 40, 36)) > "" Then
                    .Variables("DADD3").Value = Trim$(Mid$(RecordArray(11), 40, 36))
                Else
                    .Variables("DADD3").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD4" & Trim$(Mid$(RecordArray(12), 40, 36)))
                If Trim$(Mid$(RecordArray(12), 40, 36)) > "" Then
                    .Variables("DADD4").Value = Trim$(Mid$(RecordArray(12), 40, 36))
                Else
                    .Variables("DADD4").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD5" & Trim$(Mid$(RecordArray(13), 40, 36)))
                If Trim$(Mid$(RecordArray(13), 40, 36)) > "" Then
                    .Variables("DADD5").Value = Trim$(Mid$(RecordArray(13), 40, 36))
                Else
                    .Variables("DADD5").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DPCODE" & Trim$(Mid$(RecordArray(14), 40, 12)))
                If Trim$(Mid$(RecordArray(14), 40, 12)) > "" Then
                    .Variables("DPCODE").Value = Trim$(Mid$(RecordArray(14), 40, 12))
                Else
                    .Variables("DPCODE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ORDER" & Trim$(Mid$(RecordArray(8), 96, 12)))
                If Trim$(Mid$(RecordArray(8), 96, 12)) > "" Then
                    .Variables("ORDER").Value = Trim$(Mid$(RecordArray(8), 96, 12))
                Else
                    .Variables("ORDER").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DATEENT" & Trim$(Mid$(RecordArray(9), 96, 8)))
                If Trim$(Mid$(RecordArray(9), 96, 8)) > "" Then
                    .Variables("DATEENT").Value = Trim$(Mid$(RecordArray(9), 96, 8))
                Else
                    .Variables("DATEENT").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DELDATE" & Trim$(Mid$(RecordArray(10), 96, 8)))
                If Trim$(Mid$(RecordArray(10), 96, 8)) > "" Then
                    .Variables("DELDATE").Value = Trim$(Mid$(RecordArray(10), 96, 8))
                Else
                    .Variables("DELDATE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable SUPREF" & Trim$(Mid$(RecordArray(12), 96, 20)))
                If Trim$(Mid$(RecordArray(12), 96, 12)) > "" Then
                    .Variables("SUPREF").Value = Trim$(Mid$(RecordArray(12), 96, 20))
                Else
                    .Variables("SUPREF").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable BUYER" & Trim$(Mid$(RecordArray(11), 96, 26)))
                If Trim$(Mid$(RecordArray(11), 96, 26)) > "" Then
                    .Variables("BUYER").Value = Trim$(Mid$(RecordArray(11), 96, 26))
                Else
                    .Variables("BUYER").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable RATING" & Trim$(Mid$(RecordArray(13), 89, 12)))
                If Trim$(Mid$(RecordArray(13), 89, 12)) > "" Then
                    .Variables("RATING").Value = Trim$(Mid$(RecordArray(13), 89, 12))
                Else
                    .Variables("RATING").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable CURRENCY" & Trim$(Mid$(RecordArray(14), 1, 6)))
                If Trim$(Mid$(RecordArray(14), 1, 6)) > "" Then
                    .Variables("CURRENCY").Value = Trim$(Mid$(RecordArray(14), 1, 6))
                Else
                    .Variables("CURRENCY").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable EMAILFROM" & wsFromEmail)
                If wsFromEmail > "" Then
                    .Variables("EMAILFROM").Value = wsFromEmail
                Else
                    .Variables("EMAILFROM").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable AMENDED" & wsAmended)
                If wsAmended > "" Then
                    .Variables("AMENDED").Value = wsAmended
                Else
                    .Variables("AMENDED").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable HNARR")
                If Hnarrstring > "" Then
                    .Variables("HNARR").Value = Hnarrstring
                Else
                    .Variables("HNARR").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable INARR")
                If Inarrstring > "" Then
                    .Variables("INARR").Value = Inarrstring
                Else
                    .Variables("INARR").Value = wsSpaces
                End If

                oWrite.WriteLine("Variable PRODUCT")
                If linestring > "" Then
                    .Variables("PRODUCT").Value = linestring
                Else
                    .Variables("PRODUCT").Value = wsSpaces
                End If
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection
            oWrite.Flush()

            ' lock the document to stop changes
            '            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsSaveDoc = wsNewDir & "\" & Trim$(Mid$(RecordArray(8), 96, 12)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()
            oWrite.WriteLine("Word closed process PO")
            oWrite.Flush()

            worddoc = Nothing

            ' Write PDF
            Print_PDF()
            '    MsgBox "Finished!"

            Exit Sub
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process PO2")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessDW()

        Try
            oWrite.WriteLine("DW Start")

            linestring = ""
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr
                End If
            Next

            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr
            Next

            ReDim InarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*I*" Then
                    InarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 120))
                    j = j + 1
                    ReDim Preserve InarrArray(j)
                End If
            Next

            Inarrstring = ""
            For Me.i = 0 To UBound(InarrArray) - 1
                Inarrstring = Inarrstring & InarrArray(i) & vbCr
            Next

            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            wsEmailFrom = "<" & wsDefEmail & ">"
            wsEmailTo = "<" & wsDefEmail & ">"

            wsOrder = Trim$(Mid$(RecordArray(11), 52, 6))

            Try
                oWrite.WriteLine("Word open next process ack")
                worddoc = WordApp.Documents.Open(wsTempDir & "\DW_" & wsCo & "_template.dotx")
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in process adocopen doc")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            Try
                wsSaveDoc = wsNewDir & "\" & Trim$(Mid$(RecordArray(23), 25, 8)) & "(" & Trim$(Mid$(RecordArray(23), 33, 3)) & ")" & "_" & Trim$(Mid$(RecordArray(23), 7, 10)) & "_" & wsOrder & ".doc"
                worddoc.SaveAs(wsSaveDoc)
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            Try
                worddoc.Close()
                worddoc = Nothing
                worddoc = WordApp.Documents.Open(wsSaveDoc)
                If DebugOn = True Then
                    WordApp.Visible = True
                End If
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in process acloseopen doc")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            oWrite.WriteLine("Variable read")
            With worddoc
                oWrite.WriteLine("Variable SUPPLIER" & Trim$(Mid$(RecordArray(15), 7, 36)))
                If Trim$(Mid$(RecordArray(15), 7, 36)) > "" Then
                    .Variables("SUPPLIER").Value = Trim$(Mid$(RecordArray(15), 7, 36))
                Else
                    .Variables("SUPPLIER").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(16), 7, 36)) > "" Then
                    .Variables("DADD1").Value = Trim$(Mid$(RecordArray(16), 7, 36))
                Else
                    .Variables("DADD1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(17), 7, 36)) > "" Then
                    .Variables("DADD2").Value = Trim$(Mid$(RecordArray(17), 7, 36))
                Else
                    .Variables("DADD2").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(18), 7, 36)) > "" Then
                    .Variables("DADD3").Value = Trim$(Mid$(RecordArray(18), 7, 36))
                Else
                    .Variables("DADD3").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(19), 7, 36)) > "" Then
                    .Variables("DADD4").Value = Trim$(Mid$(RecordArray(19), 7, 36))
                Else
                    .Variables("DADD4").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(20), 7, 12)) > "" Then
                    .Variables("DPCODE").Value = Trim$(Mid$(RecordArray(20), 7, 12))
                Else
                    .Variables("DPCODE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(23), 7, 10)) > "" Then
                    .Variables("ORDER").Value = Trim$(Mid$(RecordArray(23), 7, 10))
                Else
                    .Variables("ORDER").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(11), 52, 6)) > "" Then
                    .Variables("DWORDER").Value = Trim$(Mid$(RecordArray(11), 52, 6))
                Else
                    .Variables("DWORDER").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(23), 25, 12)) > "" Then
                    .Variables("WIP").Value = Trim$(Mid$(RecordArray(23), 25, 12))
                Else
                    .Variables("WIP").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(11), 67, 10)) > "" Then
                    .Variables("DESPDATE").Value = Trim$(Mid$(RecordArray(11), 67, 10))
                Else
                    .Variables("DESPDATE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(23), 45, 24)) > "" Then
                    .Variables("CUSTREF").Value = Trim$(Mid$(RecordArray(23), 45, 24))
                Else
                    .Variables("CUSTREF").Value = wsSpaces
                End If
                If Hnarrstring > "" Then
                    .Variables("HNARR").Value = Hnarrstring
                Else
                    .Variables("HNARR").Value = wsSpaces
                End If
                If Inarrstring > "" Then
                    .Variables("INARR").Value = Inarrstring
                Else
                    .Variables("INARR").Value = wsSpaces
                End If
                If linestring > "" Then
                    .Variables("PRODUCT").Value = linestring
                Else
                    .Variables("PRODUCT").Value = wsSpaces
                End If
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection
            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            worddoc.Save()
            worddoc.Close()

            worddoc = Nothing
            oWrite.WriteLine("Word closed process DW")
            oWrite.Flush()

            wsCopies = 3
            ' Write PDF
            Print_PDF()
            wsCopies = 1
            Exit Sub
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process DW")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessSM()

        Try
            oWrite.WriteLine("SM Start")

            linestring = ""
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr
                End If
            Next

            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr
            Next

            ReDim InarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*I*" Then
                    InarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 120))
                    j = j + 1
                    ReDim Preserve InarrArray(j)
                End If
            Next

            Inarrstring = ""
            For Me.i = 0 To UBound(InarrArray) - 1
                Inarrstring = Inarrstring & InarrArray(i) & vbCr
            Next

            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            wsEmailFrom = "<" & wsDefEmail & ">"
            wsEmailTo = "<" & wsDefEmail & ">"

            wsOrder = Trim$(Mid$(RecordArray(4), 23, 8))

            Try
                oWrite.WriteLine("Word open next process ack")
                worddoc = WordApp.Documents.Open(wsTempDir & "\SM_" & wsCo & "_template.dotx")
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in process adocopen doc")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            Try
                wsSaveDoc = wsNewDir & "\" & wsOrder & ".doc"
                worddoc.SaveAs(wsSaveDoc)
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            Try
                worddoc.Close()
                worddoc = Nothing
                worddoc = WordApp.Documents.Open(wsSaveDoc)
                If DebugOn = True Then
                    WordApp.Visible = True
                End If
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in process acloseopen doc")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            oWrite.WriteLine("Variable read")
            With worddoc
                oWrite.WriteLine("Variable DocRef" & Trim$(Mid$(RecordArray(4), 23, 8)))
                If Trim$(Mid$(RecordArray(4), 23, 8)) > "" Then
                    .Variables("DocRef").Value = Trim$(Mid$(RecordArray(4), 23, 8))
                Else
                    .Variables("DocRef").Value = wsSpaces
                End If
                If Hnarrstring > "" Then
                    .Variables("HNARR").Value = Hnarrstring
                Else
                    .Variables("HNARR").Value = wsSpaces
                End If
                If Inarrstring > "" Then
                    .Variables("INARR").Value = Inarrstring
                Else
                    .Variables("INARR").Value = wsSpaces
                End If
                If linestring > "" Then
                    .Variables("PRODUCT").Value = linestring
                Else
                    .Variables("PRODUCT").Value = wsSpaces
                End If
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection
            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            worddoc.Save()
            worddoc.Close()

            worddoc = Nothing
            oWrite.WriteLine("Word closed process SM")
            oWrite.Flush()
            wsCopies = 0
            ' Write PDF
            Print_PDF()
            wsCopies = 1
            Exit Sub
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process DW")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessQA()

        Try
            oWrite.WriteLine("QA Start")

            linestring = ""
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr
                End If
            Next

            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr
            Next

            ReDim InarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*I*" Then
                    InarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 120))
                    j = j + 1
                    ReDim Preserve InarrArray(j)
                End If
            Next

            Inarrstring = ""
            For Me.i = 0 To UBound(InarrArray) - 1
                Inarrstring = Inarrstring & InarrArray(i) & vbCr
            Next

            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            wsEmailFrom = "<" & wsDefEmail & ">"
            wsEmailTo = "<" & wsDefEmail & ">"

            wsOrder = Trim$(Mid$(RecordArray(5), 18, 12))

            Try
                oWrite.WriteLine("Word open next process ack")
                worddoc = WordApp.Documents.Open(wsTempDir & "\QA_" & wsCo & "_template.dotx")
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in process adocopen doc")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            Try
                wsSaveDoc = wsNewDir & "\" & Trim$(Mid$(RecordArray(5), 18, 12)) & "_" & Trim$(Mid$(RecordArray(6), 8, 12)) & ".doc"
                worddoc.SaveAs(wsSaveDoc)
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            Try
                worddoc.Close()
                worddoc = Nothing
                worddoc = WordApp.Documents.Open(wsSaveDoc)
                If DebugOn = True Then
                    WordApp.Visible = True
                End If
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in process acloseopen doc")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            oWrite.WriteLine("Variable read")
            With worddoc
                oWrite.WriteLine("Variable Quality" & Trim$(Mid$(RecordArray(4), 23, 12)))
                If Trim$(Mid$(RecordArray(4), 23, 12)) > "" Then
                    .Variables("Quality").Value = Trim$(Mid$(RecordArray(4), 23, 12))
                Else
                    .Variables("Quality").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(5), 18, 12)) > "" Then
                    .Variables("PORDER").Value = Trim$(Mid$(RecordArray(5), 18, 12))
                Else
                    .Variables("PORDER").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(6), 8, 12)) > "" Then
                    .Variables("PORDERLINE").Value = Trim$(Mid$(RecordArray(6), 8, 12))
                Else
                    .Variables("PORDERLINE").Value = wsSpaces
                End If
                If Hnarrstring > "" Then
                    .Variables("HNARR").Value = Hnarrstring
                Else
                    .Variables("HNARR").Value = wsSpaces
                End If
                If Inarrstring > "" Then
                    .Variables("INARR").Value = Inarrstring
                Else
                    .Variables("INARR").Value = wsSpaces
                End If
                If linestring > "" Then
                    .Variables("PRODUCT").Value = linestring
                Else
                    .Variables("PRODUCT").Value = wsSpaces
                End If
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection
            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            worddoc.Save()
            worddoc.Close()

            worddoc = Nothing
            oWrite.WriteLine("Word closed process DW")
            oWrite.Flush()

            wsCopies = 0
            ' Write PDF
            Print_PDF()
            wsCopies = 1
            Exit Sub
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process DW")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessDespatchNote()

        Try
            oWrite.WriteLine("DN Start")
            wsSuppressPrint = True
            linestring = ""
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr ' vbCrLf
                End If
            Next
            If InStr(linestring, "INCOTERMS", CompareMethod.Text) > 0 Then
                linestring = Replace(linestring, "INCOTERMS", "INCOTERMS®", , , CompareMethod.Text)
            End If

            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr ' vbCrLf
            Next

            ReDim InarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*I*" Then
                    InarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 120))
                    j = j + 1
                    ReDim Preserve InarrArray(j)
                End If
            Next

            Inarrstring = ""
            For Me.i = 0 To UBound(InarrArray) - 1
                Inarrstring = Inarrstring & InarrArray(i) & vbCr ' vbCrLf
            Next

            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next
            wsToEmail = "themba@mmdafrica.co.za;bongani@mmdafrica.co.za;warwick@mmdafrica.co.za"
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*E*" Then
                    wsToEmail = RTrim$(Mid$(RecordArray(i), 4, 60))
                End If
            Next

            wsEmailFrom = "<" & wsDefEmail & ">"
            wsEmailTo = "<" & wsDefEmail & ">"
            wsemailBody = "Please review and advise"
            wsemailSubject = "Despatch note " & Trim$(Mid$(RecordArray(11), 52, 12)) & " Attached"

            ' wsCustomer = Trim$(Mid$(RecordArray(11), 1, 36))
            wsOrder = Trim$(Mid$(RecordArray(22), 7, 12))
            If wsCo = "SA" Then
                wsCopies = 4
            End If

            Try
                oWrite.WriteLine("Word open next process ack")
                worddoc = WordApp.Documents.Open(wsTempDir & "\DN_" & wsCo & "_template.dotx")
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in process adocopen doc")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            Try
                wsSaveDoc = wsNewDir & "\" & Trim$(Mid$(RecordArray(11), 52, 12)) & ".doc"
                worddoc.SaveAs(wsSaveDoc)

            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            Try
                worddoc.Close()
                worddoc = Nothing
                worddoc = WordApp.Documents.Open(wsSaveDoc)
            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in process acloseopen doc")
                oWrite.WriteLine(Err)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(Err.Description)
                oWrite.Flush()
            End Try

            oWrite.WriteLine("Variable read")
            With worddoc
                oWrite.WriteLine("Variable CUSTOMER" & Trim$(Mid$(RecordArray(14), 7, 36)))
                If Trim$(Mid$(RecordArray(14), 7, 36)) > "" Then
                    .Variables("CUSTOMER").Value = Trim$(Mid$(RecordArray(14), 7, 36))
                Else
                    .Variables("CUSTOMER").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(15), 7, 36)) > "" Then
                    .Variables("ADDR1").Value = Trim$(Mid$(RecordArray(15), 7, 36))
                Else
                    .Variables("ADDR1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(16), 7, 36)) > "" Then
                    .Variables("ADDR2").Value = Trim$(Mid$(RecordArray(16), 7, 36))
                Else
                    .Variables("ADDR2").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(17), 7, 36)) > "" Then
                    .Variables("ADDR3").Value = Trim$(Mid$(RecordArray(17), 7, 36))
                Else
                    .Variables("ADDR3").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(18), 7, 36)) > "" Then
                    .Variables("ADDR4").Value = Trim$(Mid$(RecordArray(18), 7, 36))
                Else
                    .Variables("ADDR4").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(19), 7, 36)) > "" Then
                    .Variables("PCODE").Value = Trim$(Mid$(RecordArray(19), 7, 36))
                Else
                    .Variables("PCODE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(14), 49, 36)) > "" Then
                    .Variables("DADD1").Value = Trim$(Mid$(RecordArray(14), 49, 36))
                Else
                    .Variables("DADD1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(15), 49, 36)) > "" Then
                    .Variables("DADD2").Value = Trim$(Mid$(RecordArray(15), 49, 36))
                Else
                    .Variables("DADD2").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(16), 49, 36)) > "" Then
                    .Variables("DADD3").Value = Trim$(Mid$(RecordArray(16), 49, 36))
                Else
                    .Variables("DADD3").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(17), 49, 36)) > "" Then
                    .Variables("DADD4").Value = Trim$(Mid$(RecordArray(17), 49, 36))
                Else
                    .Variables("DADD4").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(18), 49, 36)) > "" Then
                    .Variables("DADD5").Value = Trim$(Mid$(RecordArray(18), 49, 36))
                Else
                    .Variables("DADD5").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(19), 49, 12)) > "" Then
                    .Variables("DPCODE").Value = Trim$(Mid$(RecordArray(19), 49, 12))
                Else
                    .Variables("DPCODE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(22), 7, 12)) > "" Then
                    .Variables("ORDER").Value = Trim$(Mid$(RecordArray(22), 7, 12))
                Else
                    .Variables("ORDER").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(11), 67, 8)) > "" Then
                    .Variables("DESPDATE").Value = Trim$(Mid$(RecordArray(11), 67, 8))
                Else
                    .Variables("DESPDATE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(11), 52, 12)) > "" Then
                    .Variables("CONS").Value = Trim$(Mid$(RecordArray(11), 52, 12))
                Else
                    .Variables("CONS").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(22), 25, 20)) > "" Then
                    .Variables("CUSTREF").Value = Trim$(Mid$(RecordArray(22), 25, 20))
                Else
                    .Variables("CUSTREF").Value = wsSpaces
                End If
                If Hnarrstring > "" Then
                    .Variables("HNARR").Value = Hnarrstring
                Else
                    .Variables("HNARR").Value = wsSpaces
                End If
                If Inarrstring > "" Then
                    .Variables("INARR").Value = Inarrstring
                Else
                    .Variables("INARR").Value = wsSpaces
                End If
                If linestring > "" Then
                    .Variables("PRODUCT").Value = linestring
                Else
                    .Variables("PRODUCT").Value = wsSpaces
                End If
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection

            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            worddoc.Save()
            worddoc.Close()

            worddoc = Nothing
            oWrite.WriteLine("Word closed process DN")
            oWrite.Flush()

            ' Write PDF
            Print_PDF()

            Exit Sub
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process DN")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessRemittance()

        Try
            linestring = ""
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr ' vbCrLf
                End If
            Next


            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 120))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr ' vbCrLf
            Next

            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            wsEmailFrom = "<" & wsDefEmail & ">"
            wsEmailTo = "<" & wsDefEmail & ">"

            ' wsCustomer = Trim$(Mid$(RecordArray(11), 1, 36))
            wsOrder = Trim$(Mid$(RecordArray(3), 91, 12))
            '            ReDim UsedVariables(1)

            worddoc = WordApp.Documents.Open(wsTempDir & "\RA_" & wsCo & "_template.dotx")

            With worddoc
                If Trim$(Mid$(RecordArray(3), 1, 36)) > "" Then
                    .Variables("CUSTOMER").Value = Trim$(Mid$(RecordArray(3), 1, 36))
                Else
                    .Variables("CUSTOMER").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(4), 1, 36)) > "" Then
                    .Variables("ADDR1").Value = Trim$(Mid$(RecordArray(4), 1, 36))
                Else
                    .Variables("ADDR1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(5), 1, 36)) > "" Then
                    .Variables("ADDR2").Value = Trim$(Mid$(RecordArray(5), 1, 36))
                Else
                    .Variables("ADDR2").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(6), 1, 36)) > "" Then
                    .Variables("ADDR3").Value = Trim$(Mid$(RecordArray(6), 1, 36))
                Else
                    .Variables("ADDR3").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(7), 1, 36)) > "" Then
                    .Variables("ADDR4").Value = Trim$(Mid$(RecordArray(7), 1, 36))
                Else
                    .Variables("ADDR4").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(8), 1, 36)) > "" Then
                    .Variables("PCODE").Value = Trim$(Mid$(RecordArray(8), 1, 36))
                Else
                    .Variables("PCODE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(3), 39, 36)) > "" Then
                    .Variables("DADD1").Value = Trim$(Mid$(RecordArray(3), 39, 36))
                Else
                    .Variables("DADD1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(4), 39, 36)) > "" Then
                    .Variables("DADD2").Value = Trim$(Mid$(RecordArray(4), 39, 36))
                Else
                    .Variables("DADD2").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(5), 39, 36)) > "" Then
                    .Variables("DADD3").Value = Trim$(Mid$(RecordArray(5), 39, 36))
                Else
                    .Variables("DADD3").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(6), 39, 36)) > "" Then
                    .Variables("DADD4").Value = Trim$(Mid$(RecordArray(6), 39, 36))
                Else
                    .Variables("DADD4").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(7), 39, 36)) > "" Then
                    .Variables("DADD5").Value = Trim$(Mid$(RecordArray(7), 39, 36))
                Else
                    .Variables("DADD5").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(8), 39, 12)) > "" Then
                    .Variables("DPCODE").Value = Trim$(Mid$(RecordArray(8), 39, 12))
                Else
                    .Variables("DPCODE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(3), 91, 12)) > "" Then
                    .Variables("REMIT").Value = Trim$(Mid$(RecordArray(3), 91, 12))
                Else
                    .Variables("REMIT").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(4), 92, 12)) > "" Then
                    .Variables("CHEQUENO").Value = Trim$(Mid$(RecordArray(4), 92, 12))
                Else
                    .Variables("CHEQUENO").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(5), 92, 6)) > "" Then
                    .Variables("CURRENCY").Value = Trim$(Mid$(RecordArray(5), 92, 6))
                Else
                    .Variables("CURRENCY").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(2), 1, 12)) > "" Then
                    .Variables("CUSNO").Value = Trim$(Mid$(RecordArray(2), 1, 12))
                Else
                    .Variables("CUSNO").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(1), 77, 8)) > "" Then
                    .Variables("CDATE").Value = Trim$(Mid$(RecordArray(1), 77, 8))
                Else
                    .Variables("CDATE").Value = wsSpaces
                End If
                If Hnarrstring > "" Then
                    .Variables("HNARR").Value = Hnarrstring
                Else
                    .Variables("HNARR").Value = wsSpaces
                End If
                If linestring > "" Then
                    .Variables("PRODUCT").Value = linestring
                Else
                    .Variables("PRODUCT").Value = wsSpaces
                End If
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection
            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsSaveDoc = wsNewDir & "\" & Trim$(Mid$(RecordArray(3), 91, 12)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()

            worddoc = Nothing
            oWrite.WriteLine("Word closed process RA")
            oWrite.Flush()

            ' Write PDF
            Print_PDF()
            '    MsgBox "Finished!"

            Exit Sub

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process RA")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessCreditNote()
        wsSuppressPrint = True
        Try
            linestring = ""
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr ' vbCrLf
                End If
            Next
            wsValue = 0
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*F*" Then
                    wsValue = RTrim$(Mid$(RecordArray(i), 77, 13))
                End If
            Next

            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                '    If HnarrArray(i) <> "" Then
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr ' vbCrLf
                '    End If
            Next

            ReDim LnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*F*" Then
                    LnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve LnarrArray(j)
                End If
            Next

            LnarrString = ""
            For Me.i = 0 To UBound(LnarrArray) - 1
                '    If LnarrArray(i) <> "" Then
                LnarrString = LnarrString & LnarrArray(i) & vbCr ' vbCrLf
                '    End If
            Next


            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            '    wsEmailFrom = "<" & Trim$(Mid$(RecordArray(9), 17, 36)) & ">"
            '    wsEmailTo = "<" & Trim$(Mid$(RecordArray(9), 69, 36)) & ">"
            wsEmailFrom = "<" & wsDefEmail & ">"
            wsEmailTo = "<" & wsDefEmail & ">"
            wsCustomer = Trim$(Mid$(RecordArray(11), 1, 36))
            wsInvoice = Trim$(Mid$(RecordArray(10), 89, 12))
            '    wsEmailFile = LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".rtf"
            '            ReDim UsedVariables(1)

            '    Set WordApp = CreateObject("Word.Application")
            worddoc = WordApp.Documents.Open(wsTempDir & "\CN_" & wsCo & "_template.dotx")

            With worddoc
                If Trim$(Mid$(RecordArray(11), 1, 36)) > "" Then
                    .Variables("CUSTOMER").Value = Trim$(Mid$(RecordArray(11), 1, 36))
                Else
                    .Variables("CUSTOMER").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(12), 1, 36)) > "" Then
                    .Variables("ADDR1").Value = Trim$(Mid$(RecordArray(12), 1, 36))
                Else
                    .Variables("ADDR1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(13), 1, 36)) > "" Then
                    .Variables("ADDR2").Value = Trim$(Mid$(RecordArray(13), 1, 36))
                Else
                    .Variables("ADDR2").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(14), 1, 36)) > "" Then
                    .Variables("ADDR3").Value = Trim$(Mid$(RecordArray(14), 1, 36))
                Else
                    .Variables("ADDR3").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(15), 1, 36)) > "" Then
                    .Variables("ADDR4").Value = Trim$(Mid$(RecordArray(15), 1, 36))
                Else
                    .Variables("ADDR4").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(16), 1, 12)) > "" Then
                    .Variables("PCODE").Value = Trim$(Mid$(RecordArray(16), 1, 12))
                Else
                    .Variables("PCODE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(11), 38, 36)) > "" Then
                    .Variables("DADD1").Value = Trim$(Mid$(RecordArray(11), 38, 36))
                Else
                    .Variables("DADD1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(12), 38, 36)) > "" Then
                    .Variables("DADD2").Value = Trim$(Mid$(RecordArray(12), 38, 36))
                Else
                    .Variables("DADD2").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(13), 38, 36)) > "" Then
                    .Variables("DADD3").Value = Trim$(Mid$(RecordArray(13), 38, 36))
                Else
                    .Variables("DADD3").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(14), 38, 36)) > "" Then
                    .Variables("DADD4").Value = Trim$(Mid$(RecordArray(14), 38, 36))
                Else
                    .Variables("DADD4").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(15), 38, 36)) > "" Then
                    .Variables("DADD5").Value = Trim$(Mid$(RecordArray(15), 38, 36))
                Else
                    .Variables("DADD5").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(16), 38, 12)) > "" Then
                    .Variables("DPCODE").Value = Trim$(Mid$(RecordArray(16), 38, 12))
                Else
                    .Variables("DPCODE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(10), 88, 12)) > "" Then
                    .Variables("INVOICE").Value = Trim$(Mid$(RecordArray(10), 88, 12))
                Else
                    .Variables("INVOICE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(13), 88, 20)) > "" Then
                    .Variables("VATNO").Value = Trim$(Mid$(RecordArray(13), 88, 20))
                Else
                    .Variables("VATNO").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(11), 89, 8)) > "" Then
                    .Variables("INVDATE").Value = Trim$(Mid$(RecordArray(11), 89, 8))
                Else
                    .Variables("INVDATE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(16), 89, 6)) > "" Then
                    .Variables("CURRENCY").Value = Trim$(Mid$(RecordArray(16), 89, 6))
                Else
                    .Variables("CURRENCY").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(12), 94, 22)) > "" Then
                    .Variables("CUSTREF").Value = Trim$(Mid$(RecordArray(12), 94, 22))
                Else
                    .Variables("CUSTREF").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(14), 89, 12)) > "" Then
                    .Variables("CUSNO").Value = Trim$(Mid$(RecordArray(14), 89, 12))
                Else
                    .Variables("CUSNO").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(15), 89, 30)) > "" Then
                    .Variables("TERMS").Value = Trim$(Mid$(RecordArray(15), 89, 30))
                Else
                    .Variables("TERMS").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(1), 1, 12)) > "" Then
                    .Variables("INVREF").Value = Trim$(Mid$(RecordArray(1), 1, 12))
                Else
                    .Variables("INVREF").Value = wsSpaces
                End If
                If Hnarrstring > "" Then
                    .Variables("HNARR").Value = Hnarrstring
                Else
                    .Variables("HNARR").Value = wsSpaces
                End If
                If LnarrString > "" Then
                    .Variables("FNARR").Value = LnarrString
                Else
                    .Variables("FNARR").Value = wsSpaces
                End If
                If linestring > "" Then
                    .Variables("PRODUCT").Value = linestring
                Else
                    .Variables("PRODUCT").Value = wsSpaces
                End If
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection

            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsSaveDoc = wsNewDir & "\" & Trim$(Mid$(RecordArray(10), 89, 12)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()

            worddoc = Nothing
            oWrite.WriteLine("Word closed process CN")
            oWrite.Flush()

            ' Write PDF
            'zzzzzzzzzzzzzzzzzzzzzzzzzzzz
            Print_PDF()
            '    MsgBox "Finished!"

            Exit Sub
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process CN")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessStatement()

        Try

            linestring = ""
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr ' vbCrLf
                End If
            Next


            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr ' vbCrLf
            Next

            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            wsEmailFrom = "<" & wsDefEmail & ">"
            wsEmailTo = "<" & wsDefEmail & ">"
            wsCustomer = Trim$(Mid$(RecordArray(11), 1, 36))
            wsOrder = Trim$(Mid$(RecordArray(20), 25, 12))

            '            ReDim UsedVariables(1)

            worddoc = WordApp.Documents.Open(wsTempDir & "\ST_" & wsCo & "_template.dotx")

            With worddoc
                If Trim$(Mid$(RecordArray(1), 2, 36)) > "" Then
                    .Variables("CUSTOMER").Value = Trim$(Mid$(RecordArray(1), 2, 36))
                Else
                    .Variables("CUSTOMER").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(2), 2, 36)) > "" Then
                    .Variables("ADDR1").Value = Trim$(Mid$(RecordArray(2), 2, 36))
                Else
                    .Variables("ADDR1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(3), 2, 36)) > "" Then
                    .Variables("ADDR2").Value = Trim$(Mid$(RecordArray(3), 2, 36))
                Else
                    .Variables("ADDR2").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(4), 2, 36)) > "" Then
                    .Variables("ADDR3").Value = Trim$(Mid$(RecordArray(4), 2, 36))
                Else
                    .Variables("ADDR3").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(5), 2, 36)) > "" Then
                    .Variables("ADDR4").Value = Trim$(Mid$(RecordArray(5), 2, 36))
                Else
                    .Variables("ADDR4").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(6), 2, 36)) > "" Then
                    .Variables("PCODE").Value = Trim$(Mid$(RecordArray(6), 2, 36))
                Else
                    .Variables("PCODE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(1), 63, 12)) > "" Then
                    .Variables("CUSNO").Value = Trim$(Mid$(RecordArray(1), 63, 12))
                Else
                    .Variables("CUSNO").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(3), 63, 8)) > "" Then
                    .Variables("CURRENCY").Value = Trim$(Mid$(RecordArray(3), 63, 8))
                Else
                    .Variables("CURRENCY").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(4), 63, 10)) > "" Then
                    .Variables("ENTDATE").Value = Trim$(Mid$(RecordArray(4), 63, 10))
                Else
                    .Variables("ENTDATE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(2), 63, 10)) > "" Then
                    .Variables("CDATE").Value = Trim$(Mid$(RecordArray(2), 63, 10))
                Else
                    .Variables("CDATE").Value = wsSpaces
                End If
                If linestring > "" Then
                    .Variables("PRODUCT").Value = linestring
                Else
                    .Variables("PRODUCT").Value = wsSpaces
                End If
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection
            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsSaveDoc = wsNewDir & "\" & Trim$(Mid$(RecordArray(1), 63, 12)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()

            worddoc = Nothing
            oWrite.WriteLine("Word closed process statement")
            oWrite.Flush()

            ' Write PDF
            Print_PDF()
            '    MsgBox "Finished!"

            Exit Sub
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process statement")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessPerryInv()

        Try
            linestring = ""
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr ' vbCrLf
                End If
            Next
            wsValue = 0
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*F*" Then
                    wsValue = RTrim$(Mid$(RecordArray(i), 77, 13))
                End If
            Next

            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 100))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                '    If HnarrArray(i) <> "" Then
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr ' vbCrLf
                '    End If
            Next

            ReDim LnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*F*" Then
                    LnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve LnarrArray(j)
                End If
            Next

            LnarrString = ""
            For Me.i = 0 To UBound(LnarrArray) - 1
                '    If LnarrArray(i) <> "" Then
                LnarrString = LnarrString & LnarrArray(i) & vbCr ' vbCrLf
                '    End If
            Next


            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            '    wsEmailFrom = "<" & Trim$(Mid$(RecordArray(9), 17, 36)) & ">"
            '    wsEmailTo = "<" & Trim$(Mid$(RecordArray(9), 69, 36)) & ">"
            wsEmailFrom = "<" & wsDefEmail & ">"
            wsEmailTo = "<" & wsDefEmail & ">"
            wsCustomer = Trim$(Mid$(RecordArray(11), 1, 36))
            wsInvoice = Trim$(Mid$(RecordArray(10), 89, 12))
            If wsCo = "01" Then
                If Trim$(Mid$(wsInvoice, 1, 2)) = "S8" Then
                    wsCopies = 2
                End If
                If Trim$(Mid$(wsInvoice, 1, 2)) = "S3" Then
                    wsCopies = 3
                End If
                If Trim$(Mid$(wsInvoice, 1, 2)) = "S1" Then
                    wsCopies = 2
                End If
            End If
            If wsCo = "03" Then
                If Trim$(Mid$(wsInvoice, 1, 2)) = "S3" Then
                    wsCopies = 3
                End If
            End If
            If wsCo = "04" Then
                wsCopies = 2
            End If
            '
            '    wsEmailFile = LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".rtf"
            '            ReDim UsedVariables(1)

            '    Set WordApp = CreateObject("Word.Application")
            worddoc = WordApp.Documents.Open(wsTempDir & "\PE_" & wsCo & "_template.dotx")

            With worddoc
                If Trim$(Mid$(RecordArray(10), 88, 12)) > "" Then
                    .Variables("INVOICE").Value = Trim$(Mid$(RecordArray(10), 88, 12))
                Else
                    .Variables("INVOICE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(13), 88, 20)) > "" Then
                    .Variables("VATNO").Value = Trim$(Mid$(RecordArray(13), 88, 20))
                Else
                    .Variables("VATNO").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(11), 89, 8)) > "" Then
                    .Variables("INVDATE").Value = Trim$(Mid$(RecordArray(11), 89, 8))
                Else
                    .Variables("INVDATE").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(16), 89, 6)) > "" Then
                    .Variables("CURRENCY").Value = Trim$(Mid$(RecordArray(16), 89, 6))
                Else
                    .Variables("CURRENCY").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(12), 94, 22)) > "" Then
                    .Variables("CUSTREF").Value = Trim$(Mid$(RecordArray(12), 94, 22))
                Else
                    .Variables("CUSTREF").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(14), 89, 12)) > "" Then
                    .Variables("CUSNO").Value = Trim$(Mid$(RecordArray(14), 89, 12))
                Else
                    .Variables("CUSNO").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(15), 82, 30)) > "" Then
                    .Variables("TERMS").Value = Trim$(Mid$(RecordArray(15), 82, 30))
                Else
                    .Variables("TERMS").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(1), 1, 12)) > "" Then
                    .Variables("JOB").Value = Trim$(Mid$(RecordArray(1), 1, 12))
                Else
                    .Variables("JOB").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(1), 13, 12)) > "" Then
                    .Variables("DESP").Value = Trim$(Mid$(RecordArray(1), 13, 12))
                Else
                    .Variables("DESP").Value = wsSpaces
                End If
                If Hnarrstring > "" Then
                    .Variables("HNARR").Value = Hnarrstring
                Else
                    .Variables("HNARR").Value = wsSpaces
                End If
                If LnarrString > "" Then
                    .Variables("FNARR").Value = LnarrString
                Else
                    .Variables("FNARR").Value = wsSpaces
                End If
                If linestring > "" Then
                    .Variables("PRODUCT").Value = linestring
                Else
                    .Variables("PRODUCT").Value = wsSpaces
                End If
                .Range.Fields.Update()
            End With


            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsSaveDoc = wsNewDir & "\" & Trim$(Mid$(RecordArray(10), 89, 12)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()

            worddoc = Nothing

            ' Write PDF
            'zzzzzzzzzzzzzzzzzzzzzzzzzzzz
            oWrite.Flush()
            Print_PDF()
            '    MsgBox "Finished!"

            Exit Sub
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process perry inv")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessBeijingInv()
        Dim wsInvValue As Double
        Dim wsChgsValue As Double
        Dim InvLineArray(250, 5)
        Dim wsChgsDesc As String
        Dim j As Integer
        Dim c As Integer
        Dim z As Integer
        Dim i As Integer
        Dim ro1 As Integer
        Dim co1 As Integer
        Dim RetSt As String
        Dim wsArrayItemCount As Integer
        Dim a1 As String
        Dim wsBTotInv
        Dim pos

        Try

            If UBound(RecordArray) < 1 Then
                oWrite.WriteLine("No processable lines " & Now & " Beijing Inv")
                Exit Sub
            End If

            wsChgsDesc = " "
            RetSt = Chr(13)

            j = 0
            linestring = ""
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    InvLineArray(j, 0) = Mid$(RecordArray(i), 9, 20) ' part
                    InvLineArray(j, 1) = Mid$(RecordArray(i), 29, 40) ' desc
                    InvLineArray(j, 2) = Mid$(RecordArray(i), 73, 10) ' qty
                    InvLineArray(j, 3) = Mid$(RecordArray(i), 83, 10) ' item val
                    InvLineArray(j, 4) = "Z"
                    InvLineArray(j, 5) = Mid$(RecordArray(i), 99, 12) ' line val
                    j = j + 1
                End If
            Next
            j = j - 1

            wsValue = 0
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*F*" Then
                    wsValue = RTrim$(Mid$(RecordArray(i), 77, 13))
                End If
            Next

            ReDim HnarrArray(1)
            j = 0
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next


            ReDim LnarrArray(1)
            j = 1
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*T*" Then
                    LnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve LnarrArray(j)
                End If
            Next

            For i = 0 To UBound(LnarrArray) - 1
                If LnarrArray(i) <> "" Then
                    wsInvValue = Trim$(Mid$(LnarrArray(i), 3, 12))
                    wsChgsValue = Trim$(Mid$(LnarrArray(i), 16, 12))
                End If
            Next

            ReDim LnarrArray(1)

            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*C*" Then
                    wsChgsDesc = Trim$(Mid$(RecordArray(i), 4, 40))
                End If
            Next


            wsUserPrinter = ""

            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            '    wsEmailFrom = "<" & Trim$(Mid$(RecordArray(9), 17, 36)) & ">"
            '    wsEmailTo = "<" & Trim$(Mid$(RecordArray(9), 69, 36)) & ">"
            wsEmailFrom = "<" & wsDefEmail & ">"
            wsEmailTo = "<" & wsDefEmail & ">"
            wsBJob = Trim$(Mid$(RecordArray(1), 1, 12))
            wsBDesp = Trim$(Mid$(RecordArray(1), 13, 12))
            wsBCarrier = (Trim$(Mid$(RecordArray(1), 25, 4)))
            If Trim(wsBCarrier) = "SHIP" Then
                wsBCarrierStr = "XINGANG, P.R.CHINA"
            Else
                wsBCarrierStr = "BEIJING, P.R.CHINA"
            End If
            wsInvoice = Trim$(Mid$(RecordArray(2), 1, 12))
            wsBInvDate = Trim$(Mid$(RecordArray(2), 13, 8))
            wsBCustRef = Trim$(Mid$(RecordArray(2), 21, 20))
            wsCustomer = Trim$(Mid$(RecordArray(3), 1, 12))
            wsBTotInv = CDbl(wsInvValue) + CDbl(wsChgsValue)

            If wsCo = "01" Then
                If Trim$(Mid$(wsInvoice, 1, 2)) = "S8" Then
                    wsCopies = 2
                End If
                If Trim$(Mid$(wsInvoice, 1, 2)) = "S3" Then
                    wsCopies = 3
                End If
                If Trim$(Mid$(wsInvoice, 1, 2)) = "S1" Then
                    wsCopies = 2
                End If
            End If
            If wsCo = "03" Then
                If Trim$(Mid$(wsInvoice, 1, 2)) = "S3" Then
                    wsCopies = 3
                End If
            End If
            If wsCo = "04" Then
                wsCopies = 2
            End If
            '
            '    wsEmailFile = LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".rtf"
            '            ReDim UsedVariables(1)
            '    Set WordApp = CreateObject("Word.Application")
            worddoc = WordApp.Documents.Open(wsTempDir & "\BI_" & wsCo & "_template.dotx")
            If DebugOn = True Then
                WordApp.Visible = True
                WordApp.Activate()
            End If
            With worddoc
                If Trim$(Mid$(RecordArray(2), 21, 20)) > "" Then
                    .Variables("CUSTREF1").Value = Trim$(Mid$(RecordArray(2), 21, 20))
                Else
                    .Variables("CUSTREF1").Value = wsSpaces
                End If
                If Trim$(Mid$(RecordArray(1), 25, 4)) > "" Then
                    .Variables("CARRIER").Value = wsBCarrierStr
                Else
                    .Variables("CARRIER").Value = wsSpaces
                End If
                .Range.Fields.Update()
            End With

            WordApp.Selection.Font.Size = 8
            ro1% = 2 'Numbers of rows
            co1% = 6 'Numbers of columns
            ' header table
            MyTable1 = WordApp.Selection.Tables.Add(WordApp.Selection.Range, ro1%, co1%)
            MyCols = MyTable1.Columns
            MyCols(1).Width = 70
            MyCols(2).Width = 70
            MyCols(3).Width = 70
            MyCols(4).Width = 110
            MyCols(5).Width = 70
            MyCols(6).Width = 70

            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
            MyCell = MyTable1.Cell(1, 1) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Pick List No.")
            MyCell = MyTable1.Cell(1, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Delivery Date")
            MyCell = MyTable1.Cell(1, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Our Order No.")
            MyCell = MyTable1.Cell(1, 4) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Contract No.")
            MyCell = MyTable1.Cell(1, 5) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Invoice No.")
            MyCell = MyTable1.Cell(1, 6) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Invoice Date")

            MyCell = MyTable1.Cell(2, 1) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(wsBDesp)
            MyCell = MyTable1.Cell(2, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(wsBInvDate)
            MyCell = MyTable1.Cell(2, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(wsBJob)
            MyCell = MyTable1.Cell(2, 4) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(wsBCustRef)
            MyCell = MyTable1.Cell(2, 5) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(wsInvoice)
            MyCell = MyTable1.Cell(2, 6) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(wsBInvDate)
            For c% = 1 To 6
                With MyTable1.Cell(1, c%)
                    With .Borders(WdBorderType.wdBorderTop)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 1 To 6
                With MyTable1.Cell(2, c%)
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For i% = 1 To 6
                For c% = 1 To 2
                    With MyTable1.Cell(c%, i%)
                        With .Borders(WdBorderType.wdBorderRight)
                            .LineStyle = WdLineStyle.wdLineStyleSingle
                            .LineWidth = WdLineWidth.wdLineWidth050pt
                            .Color = WdColor.wdColorAutomatic
                        End With
                        With .Borders(WdBorderType.wdBorderLeft)
                            .LineStyle = WdLineStyle.wdLineStyleSingle
                            .LineWidth = WdLineWidth.wdLineWidth050pt
                            .Color = WdColor.wdColorAutomatic
                        End With
                    End With
                Next c%
            Next i%

            MyCell = MyTable1.Cell((ro1% + 1), 4) : MyCell.Select()
            MyCell = Nothing
            MyTable1 = Nothing
            WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)
            WordApp.Selection.Font.Size = 8

            ' main table
            For i% = 0 To 100
                If InvLineArray(i%, 0) > " " Then
                    wsArrayItemCount = wsArrayItemCount + 1
                End If

            Next i%

            z% = UBound(HnarrArray) + wsArrayItemCount + 7

            ro1% = z% 'Numbers of rows
            co1% = 6 'Numbers of columns

            MyTable1 = WordApp.Selection.Tables.Add(WordApp.Selection.Range, ro1%, co1%)
            MyCols = MyTable1.Columns
            MyCols(1).Width = 60
            MyCols(2).Width = 200
            MyCols(3).Width = 60
            MyCols(4).Width = 60
            MyCols(5).Width = 40
            MyCols(6).Width = 60
            j% = 1
            For i% = 0 To wsArrayItemCount - 1
                If j% = 1 Then
                    WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Part Number")
                    MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Description of Goods")
                    MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Quantity")
                    MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.TypeText("Value Each")
                    MyCell = MyTable1.Cell(j%, 5) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.TypeText("VAT Code")
                    MyCell = MyTable1.Cell(j%, 6) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.TypeText("Total Price")
                    j% = j% + 1
                End If

                MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft : WordApp.Selection.TypeText(InvLineArray(i%, 0))
                MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft : WordApp.Selection.TypeText(InvLineArray(i%, 1))
                MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter : WordApp.Selection.TypeText(InvLineArray(i%, 2))
                MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight : WordApp.Selection.TypeText(Trim$(InvLineArray(i%, 3)))
                '        WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphCenter
                MyCell = MyTable1.Cell(j%, 5) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter : WordApp.Selection.TypeText(InvLineArray(i%, 4))
                '        WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphRight
                MyCell = MyTable1.Cell(j%, 6) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight : WordApp.Selection.TypeText(Trim$(InvLineArray(i%, 5)))
                WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight
                j% = j% + 1


            Next i%

            j% = j% + 1
            If CDbl(wsChgsValue) > 0 Then
                MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.TypeText(Trim$(wsChgsDesc))
                MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter : WordApp.Selection.TypeText("1")
                MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight : WordApp.Selection.TypeText(FormatNumber(wsChgsValue, 2))
                '            WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphCenter
                MyCell = MyTable1.Cell(j%, 5) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter : WordApp.Selection.TypeText("Z")
                '            WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphRight
                MyCell = MyTable1.Cell(j%, 6) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight : WordApp.Selection.TypeText(FormatNumber(wsChgsValue, 2))
            End If
            j% = j% + 1
            MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.TypeText("TOTAL AMOUNT PAYABLE ") ' + Trim$(ChgsDesc)
            MyCell = MyTable1.Cell(j%, 6) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight : WordApp.Selection.TypeText(FormatNumber(wsBTotInv, 2))
            With MyTable1.Cell(j%, 6)
                With .Borders(WdBorderType.wdBorderTop)
                    .LineStyle = WdLineStyle.wdLineStyleSingle
                    .LineWidth = WdLineWidth.wdLineWidth050pt
                    .Color = WdColor.wdColorAutomatic
                End With
                With .Borders(WdBorderType.wdBorderBottom)
                    .LineStyle = WdLineStyle.wdLineStyleSingle
                    .LineWidth = WdLineWidth.wdLineWidth050pt
                    .Color = WdColor.wdColorAutomatic
                End With
            End With
            j% = j% + 1
            j% = j% + 1

            For i% = 0 To UBound(HnarrArray)
                MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.TypeText(HnarrArray(i))
                j% = j% + 1
            Next i%



            For c% = 1 To 4
                With MyTable1.Cell(1, c%)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderTop)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 1 To 6
                With MyTable1.Cell(1, c%)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderTop)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For i% = 1 To 6
                For c% = 1 To j%
                    With MyTable1.Cell(c%, i%)
                        With .Borders(WdBorderType.wdBorderLeft)
                            .LineStyle = WdLineStyle.wdLineStyleSingle
                            .LineWidth = WdLineWidth.wdLineWidth050pt
                            .Color = WdColor.wdColorAutomatic
                        End With
                        With .Borders(WdBorderType.wdBorderRight)
                            .LineStyle = WdLineStyle.wdLineStyleSingle
                            .LineWidth = WdLineWidth.wdLineWidth050pt
                            .Color = WdColor.wdColorAutomatic
                        End With
                    End With
                Next c%
            Next i%
            For c% = 1 To 6
                With MyTable1.Cell(j%, c%)
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%

            MyCell = MyTable1.Cell((ro1% + 1), 1) : MyCell.Select()
            MyCell = Nothing
            MyTable1 = Nothing
            WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
            WordApp.Selection.TypeText(RetSt)
            WordApp.Selection.TypeText(RetSt)

            ' insert page break
            pos = WordApp.Selection.Information(WdInformation.wdVerticalPositionRelativeToPage)
            oWrite.WriteLine("Position " & pos)

            If WordApp.Selection.Information(WdInformation.wdVerticalPositionRelativeToPage) > 315 Then

                WordApp.Selection.InsertBreak(WdBreakType.wdPageBreak)
            End If
            WordApp.Selection.Font.Size = 8


            ro1% = 4 'Numbers of rows
            co1% = 3 'Numbers of columns

            MyTable1 = WordApp.Selection.Tables.Add(WordApp.Selection.Range, ro1%, co1%)
            MyCols = MyTable1.Columns
            MyCols(1).Width = 340
            MyCols(2).Width = 80
            MyCols(3).Width = 60
            a1$ = "FOR AND ON BEHALF OF MMD ASIA PACIFIC LTD."
            MyCell = MyTable1.Cell(1, 1) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(a1$)
            MyCell = MyTable1.Cell(1, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Goods Total")
            MyCell = MyTable1.Cell(1, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight : WordApp.Selection.TypeText(FormatNumber(wsInvValue, 2))
            MyCell = MyTable1.Cell(2, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Charges")
            MyCell = MyTable1.Cell(2, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight : WordApp.Selection.TypeText(FormatNumber(wsChgsValue, 2))
            MyCell = MyTable1.Cell(3, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("V.A.T.")
            MyCell = MyTable1.Cell(3, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight : WordApp.Selection.TypeText("0.00")
            a1$ = "DIRECTOR..................................................."
            MyCell = MyTable1.Cell(4, 1) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(a1$)
            MyCell = MyTable1.Cell(4, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Amount Payable GBP £")
            MyCell = MyTable1.Cell(4, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight : WordApp.Selection.TypeText(FormatNumber(wsBTotInv, 2))
            For c% = 1 To 4
                With MyTable1.Cell(c%, 3)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderTop)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            MyCell = MyTable1.Cell((ro1% + 1), 1) : MyCell.Select()
            MyCell = Nothing
            MyTable1 = Nothing
            WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8
            WordApp.Selection.TypeText(RetSt)

            a1$ = "           VAT ANALYSIS" + RetSt
            ' WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.TypeText(a1$)

            ro1% = 2 'Numbers of rows
            co1% = 4 'Numbers of columns

            MyTable1 = WordApp.Selection.Tables.Add(WordApp.Selection.Range, ro1%, co1%)
            MyCols = MyTable1.Columns
            MyCols(1).Width = 40
            MyCols(2).Width = 80
            MyCols(3).Width = 40
            MyCols(4).Width = 40
            MyCell = MyTable1.Cell(1, 1) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Code")
            MyCell = MyTable1.Cell(1, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Goods")
            MyCell = MyTable1.Cell(1, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Rate")
            MyCell = MyTable1.Cell(1, 4) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("VAT")
            MyCell = MyTable1.Cell(2, 1) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Z")
            MyCell = MyTable1.Cell(2, 2) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(FormatNumber(wsInvValue, 2))
            MyCell = MyTable1.Cell(2, 3) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("0.00")
            MyCell = MyTable1.Cell(2, 4) : MyCell.Select() : WordApp.Selection.Font.Size = 8 : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("0.00")
            For c% = 1 To 4
                With MyTable1.Cell(1, c%)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderTop)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 1 To 4
                With MyTable1.Cell(2, c%)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderTop)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            MyCell = MyTable1.Cell((ro1% + 1), 1) : MyCell.Select()
            MyCell = Nothing
            MyTable1 = Nothing
            WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
            WordApp.Selection.TypeText(RetSt)


            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsSaveDoc = wsNewDir & "\BI_" & Trim$(Mid$(RecordArray(2), 1, 12)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()

            worddoc = Nothing

            oWrite.Flush()
            ' Write PDF
            'zzzzzzzzzzzzzzzzzzzzzzzzzzzz
            Print_PDF()
            '    MsgBox "Finished!"

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process Beijing invoice")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessBeijingPl()
        Dim a1 As String
        Dim wsDate1 As String
        Dim wsArrayItemCount As Integer
        Dim wsArrayBoxCount As Integer
        Dim wsArrayBoxOld As String
        Dim wsArrayBoxItemCount(20, 1)
        Dim wsChangeBox As Boolean
        Dim wsChangeBoxTest As String
        Dim w As Integer
        Dim x As Integer
        Dim z As Integer
        Dim j As Integer
        Dim t As Integer
        Dim c As Integer
        Dim i As Integer
        Dim ro1 As Integer
        Dim co1 As Integer
        Dim wsoldBox As String
        Dim RetSt As String
        Dim dimen As String
        Dim plistcount As Integer

        Try


            RetStr1 = Chr(10) + Chr(13)
            RetStr2 = Chr(13) + Chr(10)
            RetStr = Chr(13)
            RetSt = Chr(13)

            wsArrayBoxOld = ""
            wsUserPrinter = ""

            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            j% = 0
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    PickListArray(j%, 0) = Mid$(RecordArray(i), 4, 20) ' part
                    PickListArray(j%, 1) = Mid$(RecordArray(i), 24, 40) ' desc
                    PickListArray(j%, 2) = Mid$(RecordArray(i), 64, 12) ' qty
                    PickListArray(j%, 3) = Mid$(RecordArray(i), 76, 3)   ' box
                    PickListArray(j%, 4) = Mid$(RecordArray(i), 79, 6)   ' box desc
                    PickListArray(j%, 5) = Mid$(RecordArray(i), 85, 12)   ' GROSS WEIGHT
                    PickListArray(j%, 6) = Mid$(RecordArray(i), 97, 10)   ' NET WEIGHT
                    dimen = Trim$(Mid$(RecordArray(i), 107, 5)) + " x "
                    dimen = dimen + Trim$(Mid$(RecordArray(i), 112, 5)) + " x "
                    dimen = dimen + Trim$(Mid$(RecordArray(i), 117, 7))
                    PickListArray(j%, 7) = dimen   ' DIMENTIONS
                    PickListArray(j%, 8) = Mid$(RecordArray(i), 124, 6)   ' uow
                    PickListArray(j%, 9) = Mid$(RecordArray(i), 130, 12)  ' JOB
                    PickListArray(j%, 10) = Mid$(RecordArray(i), 142, 20)  ' CUSSUPREF
                    PickListArray(j%, 11) = Mid$(RecordArray(i), 162, 12)  ' PICK LIST
                    PickListArray(j%, 12) = Mid$(RecordArray(i), 174, 4)  ' CARRIER
                    j% = j% + 1
                End If
            Next
            plistcount = j%

            If Trim(PickListArray(0, 12)) = "SHIP" Then
                wsBCarrierStr = "XINGANG, P.R.CHINA"
            Else
                wsBCarrierStr = "BEIJING, P.R.CHINA"
            End If
            ' Set WordApp = CreateObject("Word.Application")
            worddoc = WordApp.Documents.Open(wsTempDir & "\STD_" & wsCo & "_template.dotx")
            If DebugOn = True Then
                WordApp.Visible = True
                WordApp.Activate()
            End If

            a1$ = "PACKING LIST" + RetSt
            WordApp.Selection.Font.Name = "Courier New"
            WordApp.Selection.Font.Size = 14
            WordApp.Selection.Font.Bold = True
            ' WordApp.Selection.Font.Underline = wdUnderlineSingle
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText(a1$ + RetSt)
            ' WordApp.Selection.Font.Underline = wdUnderlineNone
            ' WordApp.Selection.TypeText RetSt
            WordApp.Selection.Font.Bold = False
            ' WordApp.Selection.Font.Size = 11

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)


            wsDate1 = Date.Today
            a1$ = "Date: " + wsDate1 + RetSt
            ' WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.TypeText(a1$)

            a1$ = "Our Ref: " + PickListArray(0, 9) + RetSt
            wsSavJob = PickListArray(0, 9)
            ' WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            a1$ = ""
            a1$ = a1$ + "To:                                    Shipping Mark:" + RetSt
            WordApp.Selection.Font.Bold = True
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText(a1$)
            WordApp.Selection.Font.Bold = False

            WordApp.Selection.Font.Size = 8
            WordApp.Selection.TypeText(RetSt)
            WordApp.Selection.Font.Size = 8

            a1$ = "BEIJING MMD MINING MACHINERY CO. LTD   BEIJING MMD MINING MACHINERY CO LTD" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "MA PO JUYUAN INDUSTRIAL PARK,          MA PO JUYUAN INDUSTRIAL PARK," + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "SHUNYI DISTRICT,                       SHUNYI DISTRICT," + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "BEIJING, P.R.CHINA                     BEIJING, P.R.CHINA" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "POSTCODE.101300                        POSTCODE.101300" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "TEL: 86 10 69407788                    SHIPPING MARK:" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "FAX: 86 10 69407065                    " + PickListArray(0, 10) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "                                       " + wsBCarrierStr + RetSt
            '            a1$ = "                                       XINGANG, P.R.CHINA" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)


            a1$ = RetSt + "CONTRACT NO. " + PickListArray(0, 10) + RetStr
            WordApp.Selection.Font.Size = 14
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText(a1$ + RetSt)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            a1$ = RetStr + "PACKING LIST NO. " + PickListArray(0, 11) + RetStr
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.Font.Bold = True
            WordApp.Selection.TypeText(a1$ + RetSt)
            WordApp.Selection.Font.Bold = False

            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            wsArrayItemCount = 0

            ' dd 150907
            For i% = 0 To plistcount - 1
                If PickListArray(i%, 0) > " " Then
                    wsArrayItemCount = wsArrayItemCount + 1
                End If
            Next i%

            wsArrayBoxCount = 0
            w% = 0

            For i% = 0 To wsArrayItemCount
                If PickListArray(i%, 3) > " " Then
                    If PickListArray(i%, 3) <> wsArrayBoxOld Then
                        If w% > 0 Then
                            z% = w% - 1
                            wsArrayBoxItemCount(z%, 0) = wsArrayBoxCount
                            wsArrayBoxItemCount(z%, 1) = x%
                        End If
                        wsArrayBoxCount = wsArrayBoxCount + 1
                        wsArrayBoxOld = PickListArray(i%, 3)
                        x% = 1
                        w% = w% + 1
                    Else
                        x% = x% + 1
                    End If
                End If
            Next i%

            z% = w% - 1
            wsArrayBoxItemCount(z%, 0) = wsArrayBoxCount
            wsArrayBoxItemCount(z%, 1) = x%

            t% = 0
            wsChangeBoxTest = ""

            For i% = 0 To wsArrayItemCount - 1

                If PickListArray(i%, 3) <> wsChangeBoxTest Then
                    wsChangeBox = True
                Else
                    wsChangeBox = False
                End If
                If wsChangeBox = True Then
                    If t% > 0 Then

                        For c% = 1 To 4
                            With MyTable1.Cell(1, c%)
                                With .Borders(WdBorderType.wdBorderLeft)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderRight)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderTop)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderBottom)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                            End With
                        Next c%
                        For c% = 1 To 4
                            With MyTable1.Cell(3, c%)
                                With .Borders(WdBorderType.wdBorderLeft)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderRight)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderTop)
                                    .LineStyle = WdLineStyle.wdLineStyleDouble
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderBottom)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                            End With
                        Next c%
                        For c% = 2 To j% - 1
                            With MyTable1.Cell(c%, 1)
                                With .Borders(WdBorderType.wdBorderLeft)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                            End With
                        Next c%
                        For c% = 2 To j% - 1
                            With MyTable1.Cell(c%, 4)
                                With .Borders(WdBorderType.wdBorderRight)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                            End With
                        Next c%
                        For c% = 1 To 4
                            With MyTable1.Cell(j%, c%)
                                With .Borders(WdBorderType.wdBorderBottom)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                            End With
                        Next c%



                        MyCell = MyTable1.Cell((ro1% + 1), 4) : MyCell.Select()
                        MyCell = Nothing
                        MyTable1 = Nothing
                        WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
                        WordApp.Selection.TypeText(RetSt)

                    End If
                    w% = wsArrayBoxItemCount(t%, 1)
                    ro1% = w% + 3 'Numbers of rows
                    co1% = 4 'Numbers of columns
                    t% = t% + 1


                    MyTable1 = WordApp.Selection.Tables.Add(WordApp.Selection.Range, ro1%, co1%)
                    MyCols = MyTable1.Columns
                    MyCols(1).Width = 120
                    MyCols(2).Width = 170
                    MyCols(3).Width = 80
                    MyCols(4).Width = 100
                    j% = 1
                End If
                If j% = 1 Then
                    WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Type of Package")
                    MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Gross Weight Kgs")
                    MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Net Weight Kgs")
                    MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Measurements Cms")
                    wsoldBox = PickListArray(i%, 4)
                    j% = j% + 1
                    MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 4))
                    MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 5))
                    MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 6))
                    MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 7))
                    j% = j% + 1
                    MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Part Number")
                    MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Description of Goods")
                    MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Quantity")
                    MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.TypeText(" ")
                    wsoldBox = PickListArray(i%, 4)
                    j% = j% + 1
                End If
                WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
                MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 0))
                MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 1))
                MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 2))
                MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.TypeText(" ")
                j% = j% + 1

                wsChangeBoxTest = PickListArray(i%, 3)

            Next i%

            For c% = 1 To 4
                With MyTable1.Cell(1, c%)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderTop)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 1 To 4
                With MyTable1.Cell(3, c%)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderTop)
                        .LineStyle = WdLineStyle.wdLineStyleDouble
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 2 To j% - 1
                With MyTable1.Cell(c%, 1)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 2 To j% - 1
                With MyTable1.Cell(c%, 4)
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 1 To 4
                With MyTable1.Cell(j%, c%)
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%

            MyCell = MyTable1.Cell((ro1% + 1), 4) : MyCell.Select()
            MyCell = Nothing
            MyTable1 = Nothing
            WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
            WordApp.Selection.TypeText(RetSt)
            WordApp.Selection.TypeText(RetSt)

            a1$ = RetStr + "FOR AND ON BEHALF OF MMD ASIA PACIFIC LTD." + RetStr
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.Font.Bold = True
            WordApp.Selection.TypeText(a1$ + RetSt)
            WordApp.Selection.Font.Bold = False
            WordApp.Selection.TypeText(RetSt)
            WordApp.Selection.TypeText(RetSt)

            a1$ = RetStr + "DIRECTOR  ..........................................." + RetStr
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.Font.Bold = True
            WordApp.Selection.TypeText(a1$ + RetSt)
            WordApp.Selection.Font.Bold = False

            a1$ = RetStr + "AS THE SELLER " + RetStr
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.Font.Bold = True
            WordApp.Selection.TypeText(a1$ + RetSt)
            WordApp.Selection.Font.Bold = False

            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsDocType = "BL"
            wsSaveDoc = wsNewDir & "\BL_" & Trim$(PickListArray(0, 9)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()

            worddoc = Nothing

            oWrite.Flush()
            ' Write PDF
            'zzzzzzzzzzzzzzzzzzzzzzzzzzzz
            Print_PDF()
            '    MsgBox "Finished!"

            ' worddoc.SaveAs newfile$

            ' Set worddoc = Nothing
            ' Set WordApp = Nothing
            Exit Sub

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process Beijing PL")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub subProcessCerts()

        Dim a1 As String
        Dim wsDate1 As String
        Dim wsArrayItemCount As Integer
        Dim wsArrayBoxItemCount(20, 1)
        Dim i As Integer
        Dim j As Integer
        Dim ro1 As Integer
        Dim co1 As Integer
        Dim RetSt As String
        Dim dimen As String
        Dim plistcount As Integer

        Try

            If UBound(RecordArray) < 1 Then
                oWrite.WriteLine("No processable lines " & Now & " Process Certs")
                oWrite.Flush()
                Exit Sub
            End If
            RetStr1 = Chr(10) + Chr(13)
            RetStr2 = Chr(13) + Chr(10)
            RetSt = Chr(13)
            '
            wsUserPrinter = ""

            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next
            j = 0
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    PickListArray(j%, 0) = Mid$(RecordArray(i), 4, 20) ' part
                    PickListArray(j%, 1) = Mid$(RecordArray(i), 24, 40) ' desc
                    PickListArray(j%, 2) = Mid$(RecordArray(i), 64, 12) ' qty
                    PickListArray(j%, 3) = Mid$(RecordArray(i), 76, 3)   ' box
                    PickListArray(j%, 4) = Mid$(RecordArray(i), 79, 6)   ' box desc
                    PickListArray(j%, 5) = Mid$(RecordArray(i), 85, 12)   ' GROSS WEIGHT
                    PickListArray(j%, 6) = Mid$(RecordArray(i), 97, 10)   ' NET WEIGHT
                    dimen = Trim$(Mid$(RecordArray(i), 107, 5)) + " x "
                    dimen = dimen + Trim$(Mid$(RecordArray(i), 112, 5)) + " x "
                    dimen = dimen + Trim$(Mid$(RecordArray(i), 117, 7))
                    PickListArray(j%, 7) = dimen   ' DIMENTIONS
                    PickListArray(j%, 8) = Mid$(RecordArray(i), 124, 6)   ' uow
                    PickListArray(j%, 9) = Mid$(RecordArray(i), 130, 12)  ' JOB
                    PickListArray(j%, 10) = Mid$(RecordArray(i), 142, 20)  ' CUSSUPREF
                    PickListArray(j%, 11) = Mid$(RecordArray(i), 162, 12)  ' PICK LIST
                    j% = j% + 1
                End If
            Next
            plistcount = j%
            wsSavJob = PickListArray(0, 9)
            ' CERTIFICATE OF ORIGIN SECTION
            worddoc = WordApp.Documents.Open(wsTempDir & "\STD_" & wsCo & "_template.dotx")
            If DebugOn = True Then
                WordApp.Visible = True
                WordApp.Activate()
            End If
            a1$ = "CERTIFICATE OF ORIGIN" + RetSt
            WordApp.Selection.Font.Name = "Courier New"
            WordApp.Selection.Font.Size = 14
            WordApp.Selection.Font.Bold = True
            ' WordApp.Selection.Font.Underline = wdUnderlineSingle
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText(a1$ + RetSt)
            ' WordApp.Selection.Font.Underline = wdUnderlineNone
            ' WordApp.Selection.TypeText RetSt
            WordApp.Selection.Font.Bold = False
            ' WordApp.Selection.Font.Size = 11

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)


            wsDate1 = Date.Today
            a1$ = "Date: " + wsDate1 + RetSt
            ' WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            a1$ = "ISSUED BY:" + RetSt
            ' WordApp.Selection.Font.Bold = True
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            ' WordApp.Selection.Font.Bold = False


            a1$ = "MMD ASIA PACIFIC LTD." + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "ADDRESS:" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "The House of SIZERS" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "THE PROMENADE LAXEY" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)


            a1$ = "ISLE OF MAN, IM4 7DB" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "TEL: 0044 1624 864050" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "FAX: 0044 1624 864069" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "AS THE SELLER" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "TO:" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "BEIJING MMD MINING MACHINERY CO. LTD" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "MA PO JUYUAN INDUSTRIAL PARK," + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "SHUNYI DISTRICT," + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "BEIJING P.R.CHINA" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "POSTCODE. 101300" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "TEL: 86 10 69407788" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "FAX: 86 10 69407065" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "CONTRACT NO. " + PickListArray(0, 10) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            wsArrayItemCount = 0
            ' For i% = 0 To 100
            ' dd 150907
            For i% = 0 To plistcount - 1
                If PickListArray(i%, 1) > " " Then
                    wsArrayItemCount = wsArrayItemCount + 1
                End If
            Next i%


            ro1% = wsArrayItemCount + 1 'Numbers of rows
            co1% = 3 'Numbers of columns


            MyTable1 = WordApp.Selection.Tables.Add(WordApp.Selection.Range, ro1%, co1%)
            MyCols = MyTable1.Columns
            MyCols(1).Width = 120
            MyCols(2).Width = 200
            MyCols(3).Width = 80
            j% = 1
            For i% = 0 To wsArrayItemCount - 1

                If j% = 1 Then
                    MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("PART NO.")
                    MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("DESCRIPTION")
                    MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("QTY")
                    j% = j% + 1
                End If
                MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 0))
                MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 1))
                MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 2))
                j% = j% + 1

            Next i%

            MyCell = MyTable1.Cell((ro1% + 1), 1) : MyCell.Select()
            MyCell = Nothing
            MyTable1 = Nothing
            WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
            WordApp.Selection.TypeText(RetSt)


            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "MMD ASIA PACIFIC LTD HEREBY CERTIFY THAT THE WHOLE OF THE GOODS DETAILED HEREON ARE OF UK ORIGIN." + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "FOR AND ON BEHALF OF MMD ASIA PACIFIC LTD." + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "DIRECTOR" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsDocType = "BO"
            wsSaveDoc = wsNewDir & "\BO_" & Trim$(PickListArray(0, 9)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()

            worddoc = Nothing

            oWrite.Flush()
            ' Write PDF
            'zzzzzzzzzzzzzzzzzzzzzzzzzzzz
            Print_PDF()
            '    MsgBox "Finished!"


            wsUserPrinter = ""

            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next
            '
            ' CERTIFICATE OF QUALITY
            worddoc = WordApp.Documents.Open(wsTempDir & "\STD_" & wsCo & "_template.dotx")
            If DebugOn = True Then
                WordApp.Visible = True
                WordApp.Activate()
            End If

            a1$ = "CERTIFICATE OF QUALITY" + RetSt
            WordApp.Selection.Font.Name = "Courier New"
            WordApp.Selection.Font.Size = 14
            WordApp.Selection.Font.Bold = True
            ' WordApp.Selection.Font.Underline = wdUnderlineSingle
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText(a1$ + RetSt)
            ' WordApp.Selection.Font.Underline = wdUnderlineNone
            ' WordApp.Selection.TypeText RetSt
            WordApp.Selection.Font.Bold = False
            ' WordApp.Selection.Font.Size = 11

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)


            wsDate1 = Date.Today
            a1$ = "Date: " + wsDate1 + RetSt
            ' WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            a1$ = "ISSUED BY:" + RetSt
            ' WordApp.Selection.Font.Bold = True
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            ' WordApp.Selection.Font.Bold = False

            WordApp.Selection.Font.Size = 8

            a1$ = "MMD ASIA PACIFIC LTD." + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "ADDRESS:" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "THE HOUSE OF SIZERS" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "THE PROMENADE, LAXEY" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "ISLE OF MAN, IM4 7DB" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "TEL: 0044 1624 864050" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "FAX: 0044 1624 864069" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "AS THE SELLER" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "TO:" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "BEIJING MMD MINING MACHINERY CO. LTD" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "MA PO JUYUAN INDUSTRIAL PARK," + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "SHUNYI DISTRICT," + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "BEIJING P.R.CHINA" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "POSTCODE. 101300" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "TEL: 86 10 69407788" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "FAX: 86 10 69407065" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "CONTRACT NO. " + PickListArray(0, 10) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            wsArrayItemCount = 0
            ' For i% = 0 To 100
            ' dd 150907

            For i% = 0 To plistcount - 1
                If PickListArray(i%, 1) > " " Then
                    wsArrayItemCount = wsArrayItemCount + 1
                End If
            Next i%


            ro1% = wsArrayItemCount + 1 'Numbers of rows
            co1% = 3 'Numbers of columns


            MyTable1 = WordApp.Selection.Tables.Add(WordApp.Selection.Range, ro1%, co1%)
            MyCols = MyTable1.Columns
            MyCols(1).Width = 120
            MyCols(2).Width = 200
            MyCols(3).Width = 80
            j% = 1
            For i% = 0 To wsArrayItemCount - 1

                If j% = 1 Then
                    MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("PART NO.")
                    MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("DESCRIPTION")
                    MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("QTY")
                    j% = j% + 1
                End If
                MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 0))
                MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 1))
                MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 2))
                j% = j% + 1

            Next i%

            MyCell = MyTable1.Cell((ro1% + 1), 1) : MyCell.Select()
            MyCell = Nothing
            MyTable1 = Nothing
            WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
            WordApp.Selection.TypeText(RetSt)


            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "MMD ASIA PACIFIC LTD HEREBY CERTIFY THAT THE GOODS DETAILED HEREON HAVE BEEN INSPECTED,TESTED AND" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "CONFORM IN ALL RESPECTS WITH THE REQUIREMENTS OF CONTRACT NO. " + Trim(PickListArray(0, 10)) + " AND ARE IN" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "ACCORDANCE WITH BS EN ISO 9001:1994 QUALITY MANAGEMENT SYSTEM. CERTIFICATE 890437 ISSUED BY LLOYDS" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "REGISTER QUALITY ASSURANCE (LRQA)" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "FOR AND ON BEHALF OF MMD ASIA PACIFIC LTD." + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "DIRECTOR" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsDocType = "BQ"
            wsSaveDoc = wsNewDir & "\BQ_" & Trim$(PickListArray(0, 9)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()

            worddoc = Nothing

            oWrite.Flush()
            ' Write PDF
            'zzzzzzzzzzzzzzzzzzzzzzzzzzzz
            Print_PDF()
            '    MsgBox "Finished!"
            Exit Sub

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            worddoc.Close()
            worddoc = Nothing
            If pdfprint = 0 Then
                oWrite.WriteLine("Error Occurred at " & Now & " Beijing PL")
                oWrite.WriteLine(Err)
                oWrite.Flush()
            End If

        End Try

    End Sub

    Private Sub subProcessCQ()

        Dim a1 As String
        Dim wsDate1 As String
        Dim wsArrayItemCount As Integer
        Dim wsArrayBoxItemCount(20, 1)
        Dim i As Integer
        Dim j As Integer
        Dim ro1 As Integer
        Dim co1 As Integer
        Dim RetSt As String
        Dim plistcount As Integer

        RetStr1 = Chr(10) + Chr(13)
        RetStr2 = Chr(13) + Chr(10)
        RetSt = Chr(13)

        Try
            If UBound(RecordArray) < 1 Then
                oWrite.WriteLine("No processable lines " & Now & " UK CQ")
                Exit Sub
            End If
            oWrite.WriteLine("Entering array process CQ")
            '
            wsUserPrinter = ""

            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next
            j = 0
            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    PickListArray1(j%, 0) = Mid$(RecordArray(i), 4, 20) ' part
                    PickListArray1(j%, 1) = Mid$(RecordArray(i), 24, 40) ' desc
                    PickListArray1(j%, 2) = Mid$(RecordArray(i), 64, 12) ' qty
                    PickListArray1(j%, 3) = Mid$(RecordArray(i), 76, 12)   ' gphc ref
                    PickListArray1(j%, 4) = Mid$(RecordArray(i), 88, 20)   ' customer ref
                    PickListArray1(j%, 5) = Mid$(RecordArray(i), 108, 12)   ' UK ref
                    PickListArray1(j%, 6) = Mid$(RecordArray(i), 120, 36)   ' customer
                    PickListArray1(j%, 7) = Mid$(RecordArray(i), 156, 36)   ' addr1
                    PickListArray1(j%, 8) = Mid$(RecordArray(i), 192, 36)   ' addr2
                    PickListArray1(j%, 9) = Mid$(RecordArray(i), 228, 36)  ' addr3
                    PickListArray1(j%, 10) = Mid$(RecordArray(i), 264, 36)  ' addr4
                    PickListArray1(j%, 11) = Mid$(RecordArray(i), 300, 8)  ' p code
                    PickListArray1(j%, 12) = Mid$(RecordArray(i), 308, 12)  ' Inv
                    j% = j% + 1
                End If
            Next

            plistcount = j%


        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " UK CQ during array process")
            oWrite.Flush()
            '
        End Try
        Try

            worddoc = WordApp.Documents.Open(wsTempDir & "\STD_" & wsCo & "_template.dotx")
            If DebugOn = True Then
                WordApp.Visible = True
                WordApp.Activate()
            End If

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " UK CQ during open word process")
            oWrite.WriteLine(Err)
            oWrite.Flush()
            '
        End Try
        Try
            ' UK CERTIFICATE OF QUALITY

            a1$ = "SUPPLIERS DECLARATION" + RetSt
            WordApp.Selection.Font.Name = "Courier New"
            WordApp.Selection.Font.Size = 14
            WordApp.Selection.Font.Bold = True
            ' WordApp.Selection.Font.Underline = wdUnderlineSingle
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText(a1$ + RetSt)
            ' WordApp.Selection.Font.Underline = wdUnderlineNone
            ' WordApp.Selection.TypeText RetSt
            WordApp.Selection.Font.Bold = False
            ' WordApp.Selection.Font.Size = 11

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)


            wsDate1 = Date.Today
            a1$ = "Date: " + wsDate1 + RetSt
            ' WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            a1$ = "TO:" + RetSt
            ' WordApp.Selection.Font.Bold = True
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            ' WordApp.Selection.Font.Bold = False

            WordApp.Selection.Font.Size = 8

            a1$ = "MMD GPHC LTD." + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "THE HOUSE OF SIZERS" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "THE PROMENADE" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "LAXEY" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "ISLE OF MAN" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "IM4 7DB" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "CONSIGNEE:" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = Trim$(PickListArray1(0, 6)) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = Trim$(PickListArray1(0, 7)) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = Trim$(PickListArray1(0, 8)) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = Trim$(PickListArray1(0, 9)) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = Trim$(PickListArray1(0, 10)) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = Trim$(PickListArray1(0, 11)) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = Trim$(PickListArray1(0, 12)) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "MMD GPHC LTD REF: " + PickListArray1(0, 3) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "CUSTOMER ORDER: " + PickListArray1(0, 4) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "MMD UK REF: " + PickListArray1(0, 5) + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            wsArrayItemCount = 0
            ' For i% = 0 To 100
            ' dd 150907

            For i% = 0 To plistcount - 1
                If PickListArray1(i%, 1) > " " Then
                    wsArrayItemCount = wsArrayItemCount + 1
                End If
            Next i%


            ro1% = wsArrayItemCount + 1 'Numbers of rows
            co1% = 3 'Numbers of columns


            MyTable1 = WordApp.Selection.Tables.Add(WordApp.Selection.Range, ro1%, co1%)
            MyCols = MyTable1.Columns
            MyCols(1).Width = 120
            MyCols(2).Width = 200
            MyCols(3).Width = 80
            j% = 1
            For i% = 0 To wsArrayItemCount - 1

                If j% = 1 Then
                    MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("PART NO.")
                    MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("DESCRIPTION")
                    MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("QTY")
                    j% = j% + 1
                End If
                MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray1(i%, 0))
                MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray1(i%, 1))
                MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray1(i%, 2))
                j% = j% + 1

            Next i%

            MyCell = MyTable1.Cell((ro1% + 1), 1) : MyCell.Select()
            MyCell = Nothing
            MyTable1 = Nothing
            WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
            WordApp.Selection.TypeText(RetSt)


            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "MMD MINING MACHINERY DEVELOPMENTS LTD, HEREBY DECLARE THAT THE GOODS DETAILED HEREON ARE" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "OF EC ORIGIN, AND SATISFY THE ORIGINATING PROCESS REQUIREMENTS AS DETAILED IN CUSTOMS NOTICE" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)
            a1$ = "827/828" + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "SUPPORTING DOCUMENTARY EVIDENCE IS RETAINED ON OUR FILES." + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            WordApp.Selection.Font.Size = 8

            a1$ = "FOR AND ON BEHALF OF MMD MINING MACHINERY DEVELOPMENTS LTD." + RetSt
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$)

            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsDocType = "CQ"
            wsSaveDoc = wsNewDir & "\CQ_" & Trim$(PickListArray1(0, 5)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()

            worddoc = Nothing

            oWrite.Flush()
            ' Write PDF
            'zzzzzzzzzzzzzzzzzzzzzzzzzzzz
            Print_PDF()
            '    MsgBox "Finished!"
            Exit Sub

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            worddoc.Close()
            worddoc = Nothing
            If pdfprint = 0 Then
                oWrite.WriteLine("Error Occurred at " & Now & " UK CQ")
                oWrite.WriteLine(Err)
                oWrite.Flush()
            End If

        End Try

    End Sub

    Private Sub subProcessPL()
        Dim a1 As String
        Dim wsDate1 As String
        Dim wsArrayItemCount As Integer
        Dim wsArrayBoxCount As Integer
        Dim wsArrayBoxOld As String
        Dim wsArrayBoxItemCount(20, 1)
        Dim wsChangeBox As Boolean
        Dim wsChangeBoxTest As String
        Dim w As Integer
        Dim x As Integer
        Dim z As Integer
        Dim j As Integer
        Dim t As Integer
        Dim c As Integer
        Dim i As Integer
        Dim ro1 As Integer
        Dim co1 As Integer
        Dim wsoldBox As String
        Dim RetSt As String
        Dim dimen As String

        RetStr1 = Chr(10) + Chr(13)
        RetStr2 = Chr(13) + Chr(10)
        RetStr = Chr(13)
        RetSt = Chr(13)

        oWrite.WriteLine("entering read of array")
        oWrite.Flush()
        j% = 0

        wsUserPrinter = ""
        wsArrayBoxOld = ""

        Try

            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            For i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    PickListArray(j%, 0) = Mid$(RecordArray(i), 4, 20) ' part
                    PickListArray(j%, 1) = Mid$(RecordArray(i), 24, 40) ' desc
                    PickListArray(j%, 2) = Mid$(RecordArray(i), 64, 12) ' qty
                    PickListArray(j%, 3) = Mid$(RecordArray(i), 76, 3)   ' box
                    PickListArray(j%, 4) = Mid$(RecordArray(i), 79, 6)   ' box desc
                    PickListArray(j%, 5) = Mid$(RecordArray(i), 85, 12)   ' GROSS WEIGHT
                    PickListArray(j%, 6) = Mid$(RecordArray(i), 97, 10)   ' NET WEIGHT
                    dimen = Trim$(Mid$(RecordArray(i), 107, 5)) + " x "
                    dimen = dimen + Trim$(Mid$(RecordArray(i), 112, 5)) + " x "
                    dimen = dimen + Trim$(Mid$(RecordArray(i), 117, 7))
                    PickListArray(j%, 7) = dimen   ' DIMENTIONS
                    PickListArray(j%, 8) = Mid$(RecordArray(i), 124, 6)   ' uow
                    PickListArray(j%, 9) = Mid$(RecordArray(i), 130, 12)  ' JOB
                    PickListArray(j%, 10) = Mid$(RecordArray(i), 142, 20)  ' CUSSUPREF
                    PickListArray(j%, 11) = Mid$(RecordArray(i), 162, 12)  ' PICK LIST
                    j% = j% + 1
                End If
            Next
            oWrite.WriteLine("exiting read of array")

            ' Set WordApp = CreateObject("Word.Application")
            worddoc = WordApp.Documents.Open(wsTempDir & "\STD_" & wsCo & "_template.dotx")
            If DebugOn = True Then
                WordApp.Visible = True
                WordApp.Activate()
            End If
            oWrite.WriteLine("word doc opened")
            wsSavJob = PickListArray(0, 9)
            a1$ = "PACKING LIST" + RetSt
            WordApp.Selection.Font.Name = "Courier New"
            WordApp.Selection.Font.Size = 14
            WordApp.Selection.Font.Bold = True
            ' WordApp.Selection.Font.Underline = wdUnderlineSingle
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText(a1$ + RetSt)
            ' WordApp.Selection.Font.Underline = wdUnderlineNone
            ' WordApp.Selection.TypeText RetSt
            WordApp.Selection.Font.Bold = False
            ' WordApp.Selection.Font.Size = 11

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)


            wsDate1 = Date.Today
            a1$ = "Date: " + wsDate1 + RetSt
            ' WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.TypeText(a1$)

            a1$ = "Our Ref: " + PickListArray(0, 9) + RetSt
            ' WordApp.Selection.ParagraphFormat.Alignment = wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.TypeText(a1$)

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            ' CREATE TABLE HERE FOR NAME AND ADDRESS
            ro1% = 7 'Numbers of rows
            co1% = 3 'Numbers of columns

            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 8

            MyTable1 = WordApp.Selection.Tables.Add(WordApp.Selection.Range, ro1%, co1%)
            MyCols = MyTable1.Columns
            MyCols(1).Width = 180
            MyCols(2).Width = 50
            MyCols(3).Width = 180
            MyCell = MyTable1.Cell(1, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Customer:")
            MyCell = MyTable1.Cell(1, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Delivery Address:")
            MyCell = MyTable1.Cell(2, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(Mid$(RecordArray(1), 1, 36))
            MyCell = MyTable1.Cell(2, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(Mid$(RecordArray(3), 1, 36))
            MyCell = MyTable1.Cell(3, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(Mid$(RecordArray(1), 37, 36))
            MyCell = MyTable1.Cell(3, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(Mid$(RecordArray(3), 37, 36))
            MyCell = MyTable1.Cell(4, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(Mid$(RecordArray(1), 73, 36))
            MyCell = MyTable1.Cell(4, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(Mid$(RecordArray(3), 73, 36))
            MyCell = MyTable1.Cell(5, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(Mid$(RecordArray(2), 1, 36))
            MyCell = MyTable1.Cell(5, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(Mid$(RecordArray(4), 1, 36))
            MyCell = MyTable1.Cell(6, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(Mid$(RecordArray(2), 37, 10))
            MyCell = MyTable1.Cell(6, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText(Mid$(RecordArray(4), 37, 36))

            For i% = 1 To 6
                With MyTable1.Cell(i%, 1)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next i%
            For i% = 1 To 6
                With MyTable1.Cell(i%, 3)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next i%
            With MyTable1.Cell(1, 1)
                With .Borders(WdBorderType.wdBorderTop)
                    .LineStyle = WdLineStyle.wdLineStyleDouble
                    .LineWidth = WdLineWidth.wdLineWidth050pt
                    .Color = WdColor.wdColorAutomatic
                End With
            End With
            With MyTable1.Cell(6, 1)
                With .Borders(WdBorderType.wdBorderBottom)
                    .LineStyle = WdLineStyle.wdLineStyleSingle
                    .LineWidth = WdLineWidth.wdLineWidth050pt
                    .Color = WdColor.wdColorAutomatic
                End With
            End With
            With MyTable1.Cell(1, 3)
                With .Borders(WdBorderType.wdBorderTop)
                    .LineStyle = WdLineStyle.wdLineStyleDouble
                    .LineWidth = WdLineWidth.wdLineWidth050pt
                    .Color = WdColor.wdColorAutomatic
                End With
            End With
            With MyTable1.Cell(6, 3)
                With .Borders(WdBorderType.wdBorderBottom)
                    .LineStyle = WdLineStyle.wdLineStyleSingle
                    .LineWidth = WdLineWidth.wdLineWidth050pt
                    .Color = WdColor.wdColorAutomatic
                End With
            End With

            MyCell = MyTable1.Cell((ro1% + 1), 1) : MyCell.Select()
            MyCell = Nothing
            MyTable1 = Nothing
            WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
            WordApp.Selection.TypeText(RetSt)


            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(RetSt)

            a1$ = RetStr + "PACKING LIST NO. " + PickListArray(0, 11) + RetStr
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.Font.Bold = True
            WordApp.Selection.TypeText(a1$ + RetSt)
            WordApp.Selection.Font.Bold = False

            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            wsArrayItemCount = 0

            For i% = 0 To 100
                If PickListArray(i%, 0) > " " Then
                    wsArrayItemCount = wsArrayItemCount + 1
                End If
            Next i%

            wsArrayBoxCount = 0
            w% = 0

            For i% = 0 To wsArrayItemCount
                If PickListArray(i%, 3) > " " Then
                    If PickListArray(i%, 3) <> wsArrayBoxOld Then
                        If w% > 0 Then
                            z% = w% - 1
                            wsArrayBoxItemCount(z%, 0) = wsArrayBoxCount
                            wsArrayBoxItemCount(z%, 1) = x%
                        End If
                        wsArrayBoxCount = wsArrayBoxCount + 1
                        wsArrayBoxOld = PickListArray(i%, 3)
                        x% = 1
                        w% = w% + 1
                    Else
                        x% = x% + 1
                    End If
                End If
            Next i%

            z% = w% - 1
            wsArrayBoxItemCount(z%, 0) = wsArrayBoxCount
            wsArrayBoxItemCount(z%, 1) = x%

            t% = 0
            wsChangeBoxTest = ""

            For i% = 0 To wsArrayItemCount - 1

                If PickListArray(i%, 3) <> wsChangeBoxTest Then
                    wsChangeBox = True
                Else
                    wsChangeBox = False
                End If
                If wsChangeBox = True Then
                    If t% > 0 Then

                        For c% = 1 To 4
                            With MyTable1.Cell(1, c%)
                                With .Borders(WdBorderType.wdBorderLeft)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderRight)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderTop)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderBottom)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                            End With
                        Next c%
                        For c% = 1 To 4
                            With MyTable1.Cell(3, c%)
                                With .Borders(WdBorderType.wdBorderLeft)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderRight)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderTop)
                                    .LineStyle = WdLineStyle.wdLineStyleDouble
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                                With .Borders(WdBorderType.wdBorderBottom)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                            End With
                        Next c%
                        For c% = 2 To j% - 1
                            With MyTable1.Cell(c%, 1)
                                With .Borders(WdBorderType.wdBorderLeft)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                            End With
                        Next c%
                        For c% = 2 To j% - 1
                            With MyTable1.Cell(c%, 4)
                                With .Borders(WdBorderType.wdBorderRight)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                            End With
                        Next c%
                        For c% = 1 To 4
                            With MyTable1.Cell(j%, c%)
                                With .Borders(WdBorderType.wdBorderBottom)
                                    .LineStyle = WdLineStyle.wdLineStyleSingle
                                    .LineWidth = WdLineWidth.wdLineWidth050pt
                                    .Color = WdColor.wdColorAutomatic
                                End With
                            End With
                        Next c%



                        MyCell = MyTable1.Cell((ro1% + 1), 1) : MyCell.Select()
                        MyCell = Nothing
                        MyTable1 = Nothing
                        WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
                        WordApp.Selection.TypeText(RetSt)

                    End If
                    w% = wsArrayBoxItemCount(t%, 1)
                    ro1% = w% + 3 'Numbers of rows
                    co1% = 4 'Numbers of columns
                    t% = t% + 1


                    MyTable1 = WordApp.Selection.Tables.Add(WordApp.Selection.Range, ro1%, co1%)
                    MyCols = MyTable1.Columns
                    MyCols(1).Width = 120
                    MyCols(2).Width = 180
                    MyCols(3).Width = 80
                    MyCols(4).Width = 90
                    j% = 1
                End If
                If j% = 1 Then
                    WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter
                    MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Type of Package")
                    MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Gross Weight Kgs")
                    MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Net Weight Kgs")
                    MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Measurements Cms")
                    wsoldBox = PickListArray(i%, 4)
                    j% = j% + 1
                    MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 4))
                    MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 5))
                    MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 6))
                    MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 7))
                    j% = j% + 1
                    MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Part Number")
                    MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Description of Goods")
                    MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.Font.Bold = True : WordApp.Selection.TypeText("Quantity")
                    MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.TypeText(" ")
                    wsoldBox = PickListArray(i%, 4)
                    j% = j% + 1
                End If
                WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
                MyCell = MyTable1.Cell(j%, 1) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 0))
                MyCell = MyTable1.Cell(j%, 2) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 1))
                MyCell = MyTable1.Cell(j%, 3) : MyCell.Select() : WordApp.Selection.TypeText(PickListArray(i%, 2))
                MyCell = MyTable1.Cell(j%, 4) : MyCell.Select() : WordApp.Selection.TypeText(" ")
                j% = j% + 1

                wsChangeBoxTest = PickListArray(i%, 3)

            Next i%

            For c% = 1 To 4
                With MyTable1.Cell(1, c%)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderTop)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 1 To 4
                With MyTable1.Cell(3, c%)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderTop)
                        .LineStyle = WdLineStyle.wdLineStyleDouble
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 2 To j% - 1
                With MyTable1.Cell(c%, 1)
                    With .Borders(WdBorderType.wdBorderLeft)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 2 To j% - 1
                With MyTable1.Cell(c%, 4)
                    With .Borders(WdBorderType.wdBorderRight)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%
            For c% = 1 To 4
                With MyTable1.Cell(j%, c%)
                    With .Borders(WdBorderType.wdBorderBottom)
                        .LineStyle = WdLineStyle.wdLineStyleSingle
                        .LineWidth = WdLineWidth.wdLineWidth050pt
                        .Color = WdColor.wdColorAutomatic
                    End With
                End With
            Next c%

            MyCell = MyTable1.Cell((ro1% + 1), 1) : MyCell.Select()
            MyCell = Nothing
            MyTable1 = Nothing
            WordApp.Selection.GoToNext(WdGoToItem.wdGoToLine)
            WordApp.Selection.TypeText(RetSt)
            WordApp.Selection.TypeText(RetSt)
            WordApp.Selection.Font.Bold = False
            If wsCo = "CN" Then
                a1$ = RetStr + "FOR AND ON BEHALF OF MMD ASIA PACIFIC LTD." + RetStr
            End If
            If wsCo = "IM" Then
                a1$ = RetStr + "FOR AND ON BEHALF OF MMD GPHC LTD." + RetStr
            End If
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$ + RetSt)
            WordApp.Selection.TypeText(RetSt)
            WordApp.Selection.TypeText(RetSt)


            a1$ = RetStr + "DIRECTOR  ..........................................." + RetStr

            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$ + RetSt)

            a1$ = RetStr + "AS THE SELLER " + RetStr
            WordApp.Selection.Font.Size = 8
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.TypeText(a1$ + RetSt)

            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            wsSaveDoc = wsNewDir & "\PL_" & Trim$(PickListArray(0, 9)) & ".doc"
            worddoc.SaveAs(wsSaveDoc)
            '    worddoc.SaveAs wsNewDir & "\" & LTrim(RTrim(Mid(RecordArray(12), 33, 12))) & ".doc"

            worddoc.Close()

            worddoc = Nothing

            oWrite.Flush()
            ' Write PDF
            'zzzzzzzzzzzzzzzzzzzzzzzzzzzz
            Print_PDF()
            '    MsgBox "Finished!"

            ' worddoc.SaveAs newfile$

            ' Set worddoc = Nothing
            ' Set WordApp = Nothing
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            worddoc.Close()
            worddoc = Nothing
            If pdfprint = 0 Then
                oWrite.WriteLine("Error Occurred at " & Now & " PL")
                oWrite.WriteLine(Err)
                oWrite.Flush()
            End If
        End Try


    End Sub

    Private Sub subProcessProForma()

        '        On Error GoTo ErrHandler
        oWrite.WriteLine("Before try1 process ProForma Invoice")
        oWrite.Flush()
        If UBound(RecordArray) < 1 Then
            oWrite.WriteLine("No processable lines " & Now & " PR")
            Exit Sub
        End If
        wsCopies = 1
        Try
            wsQuoteNarr = ""
            wsOrderText = ""
            linestring = ""
            wsDistEmail = ""
            wsToEmail = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr ' & vbCrL
                End If
            Next
            If InStr(linestring, "INCOTERMS", CompareMethod.Text) > 0 Then
                linestring = Replace(linestring, "INCOTERMS", "INCOTERMS®", , , CompareMethod.Text)
            End If

            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 64))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr ' & vbCrLf
            Next

            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*A*" Then
                    wsOrderStatus = CInt(Trim$(Mid$(RecordArray(i), 4, 4)))
                End If
            Next

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*E*" Then
                    wsToEmail = RTrim$(Mid$(RecordArray(i), 4, 60))
                End If
            Next

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*S*" Then
                    wsDistEmail = RTrim$(Mid$(RecordArray(i), 4, 60))
                End If
            Next

            If (wsCo = "SA") Or (wsCo = "TC") Then
                wsOrderText = "PRO-FORMA INVOICE"
                wsQuoteNarr = "Thank you for your order, which we are pleased to confirm on the basis of our" & vbCr
                wsQuoteNarr = wsQuoteNarr & "General Conditions of Sale. (See attached)" & vbCr
            End If
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process ProForma Invoice")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
        Try
            If IsNumeric(Trim$(Mid$(RecordArray(2), 1, 3))) Then
                sOrdRev = CInt(Trim$(Mid$(RecordArray(2), 1, 3)))
            End If
            If (wsToEmail > "" Or wsDistEmail > "") Then
                If wsFromEmail = "" Then
                    If wsCo = "SA" Or wsCo = "TC" Then
                        wsFromEmail = "mtms.admin@mmdafrica.co.za"
                    End If
                End If
                wsEmailFrom = "<" & wsFromEmail & ">"
                wsEmailTo = "<" & wsToEmail & ">"
                wsDistEmail = "<" & wsDistEmail & ">"
                wsemailBody = "For your Attention"
                wsemailSubject = wsOrderText & " " & Trim$(Mid$(RecordArray(20), 25, 12)) & " Attached for Customer: " & Trim$(Mid$(RecordArray(11), 1, 36))
            Else
                wsEmailFrom = "<" & wsDefEmail & ">"
                wsEmailTo = "<" & wsDefEmail & ">"
            End If

            wsCustomer = Trim$(Mid$(RecordArray(11), 1, 36))
            wsOrder = Trim$(Mid$(RecordArray(20), 25, 12))
            If wsCo = "01" Then
                If Trim$(Mid$(RecordArray(18), 25, 12)) = "CU1082" Then
                    wsCopies = 1
                Else
                    wsCopies = 2
                End If
            End If
            If wsCo = "02" Then
                wsCopies = 2
            End If
            If wsCo = "03" Then
                wsCopies = 2
            End If
            If wsCo = "04" Then
                wsCopies = 2
            End If
            If wsCo = "05" Then
                wsCopies = 2
            End If

            '           ReDim UsedVariables(1)
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process  ProForma Invoice")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try

        Try
            oWrite.WriteLine("Word open next process  ProForma Invoice")
            worddoc = WordApp.Documents.Open(wsTempDir & "\PR_" & wsCo & "_template.dotx")
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process adocopen doc")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try

        Try
            If UCase$(wsDocument) = "PR" Then
                If wsCo = "IM" Or wsCo = "CN" Or wsCo = "SA" Or wsCo = "TC" Then
                    wsSaveDoc = wsNewDir & "\" & wsCo & Trim$(Mid$(RecordArray(20), 25, 7)) & "_" & Trim(CStr(sOrdRev)) & ".doc"
                Else
                    wsSaveDoc = wsNewDir & "\" & wsCo & Trim$(Mid$(RecordArray(20), 25, 7)) & ".doc"
                End If
            End If
            worddoc.SaveAs(wsSaveDoc)
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process asave doc")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try

        Try
            worddoc.Close()
            worddoc = Nothing
            worddoc = WordApp.Documents.Open(wsSaveDoc)
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process acloseopen doc")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try

        Try
            With worddoc
                oWrite.WriteLine("Variable CUSTOMER" & Trim$(Mid$(RecordArray(11), 1, 36)))
                oWrite.Flush()
                If Trim$(Mid$(RecordArray(11), 1, 36)) > "" Then
                    .Variables("CUSTOMER").Value = Trim$(Mid$(RecordArray(11), 1, 36))
                Else
                    .Variables("CUSTOMER").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR1" & Trim$(Mid$(RecordArray(12), 1, 36)))
                oWrite.Flush()
                If Trim$(Mid$(RecordArray(12), 1, 36)) > "" Then
                    .Variables("ADDR1").Value = Trim$(Mid$(RecordArray(12), 1, 36))
                Else
                    .Variables("ADDR1").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR2" & Trim$(Mid$(RecordArray(13), 1, 36)))
                If Trim$(Mid$(RecordArray(13), 1, 36)) > "" Then
                    .Variables("ADDR2").Value = Trim$(Mid$(RecordArray(13), 1, 36))
                Else
                    .Variables("ADDR2").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR3" & Trim$(Mid$(RecordArray(14), 1, 36)))
                If Trim$(Mid$(RecordArray(14), 1, 36)) > "" Then
                    .Variables("ADDR3").Value = Trim$(Mid$(RecordArray(14), 1, 36))
                Else
                    .Variables("ADDR3").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR4" & Trim$(Mid$(RecordArray(15), 1, 36)))
                If Trim$(Mid$(RecordArray(15), 1, 36)) > "" Then
                    .Variables("ADDR4").Value = Trim$(Mid$(RecordArray(15), 1, 36))
                Else
                    .Variables("ADDR4").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable PCODE" & Trim$(Mid$(RecordArray(16), 1, 36)))
                If Trim$(Mid$(RecordArray(16), 1, 36)) > "" Then
                    .Variables("PCODE").Value = Trim$(Mid$(RecordArray(16), 1, 36))
                Else
                    .Variables("PCODE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD1" & Trim$(Mid$(RecordArray(11), 38, 36)))
                If Trim$(Mid$(RecordArray(11), 38, 36)) > "" Then
                    .Variables("DADD1").Value = Trim$(Mid$(RecordArray(11), 38, 36))
                Else
                    .Variables("DADD1").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD2" & Trim$(Mid$(RecordArray(12), 38, 36)))
                If Trim$(Mid$(RecordArray(12), 38, 36)) > "" Then
                    .Variables("DADD2").Value = Trim$(Mid$(RecordArray(12), 38, 36))
                Else
                    .Variables("DADD2").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD3" & Trim$(Mid$(RecordArray(13), 38, 36)))
                If Trim$(Mid$(RecordArray(13), 38, 36)) > "" Then
                    .Variables("DADD3").Value = Trim$(Mid$(RecordArray(13), 38, 36))
                Else
                    .Variables("DADD3").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD4" & Trim$(Mid$(RecordArray(14), 38, 36)))
                If Trim$(Mid$(RecordArray(14), 38, 36)) > "" Then
                    .Variables("DADD4").Value = Trim$(Mid$(RecordArray(14), 38, 36))
                Else
                    .Variables("DADD4").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD5" & Trim$(Mid$(RecordArray(15), 38, 36)))
                If Trim$(Mid$(RecordArray(15), 38, 36)) > "" Then
                    .Variables("DADD5").Value = Trim$(Mid$(RecordArray(15), 38, 36))
                Else
                    .Variables("DADD5").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DPCODE" & Trim$(Mid$(RecordArray(16), 38, 12)))
                If Trim$(Mid$(RecordArray(16), 38, 12)) > "" Then
                    .Variables("DPCODE").Value = Trim$(Mid$(RecordArray(16), 38, 12))
                Else
                    .Variables("DPCODE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ORDER" & Trim$(Mid$(RecordArray(20), 25, 7)))
                If Trim$(Mid$(RecordArray(20), 25, 12)) > "" Then
                    If wsCo = "IM" Or wsCo = "CN" Then
                        .Variables("ORDER").Value = Trim$(Mid$(RecordArray(20), 25, 7)) & "_" & Trim(CStr(sOrdRev))
                    Else
                        .Variables("ORDER").Value = Trim$(Mid$(RecordArray(20), 25, 12))

                    End If
                Else
                    .Variables("ORDER").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable VATNO" & Trim$(Mid$(RecordArray(18), 69, 20)))
                If Trim$(Mid$(RecordArray(18), 69, 20)) > "" Then
                    .Variables("VATNO").Value = Trim$(Mid$(RecordArray(18), 69, 20))
                Else
                    .Variables("VATNO").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable REQDATE" & Trim$(Mid$(RecordArray(22), 25, 8)))
                If Trim$(Mid$(RecordArray(22), 25, 8)) > "" Then
                    .Variables("REQDATE").Value = Trim$(Mid$(RecordArray(22), 25, 8))
                Else
                    .Variables("REQDATE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable PROMDATE" & Trim$(Mid$(RecordArray(22), 69, 8)))
                If Trim$(Mid$(RecordArray(22), 69, 8)) > "" Then
                    .Variables("PROMDATE").Value = Trim$(Mid$(RecordArray(22), 69, 8))
                Else
                    .Variables("PROMDATE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ENTDATE" & Trim$(Mid$(RecordArray(1), 1, 8)))
                If Trim$(Mid$(RecordArray(1), 1, 8)) > "" Then
                    .Variables("ENTDATE").Value = Trim$(Mid$(RecordArray(1), 1, 8))
                Else
                    .Variables("ENTDATE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable CURRENCY" & Trim$(Mid$(RecordArray(17), 1, 6)))
                If Trim$(Mid$(RecordArray(17), 1, 6)) > "" Then
                    .Variables("CURRENCY").Value = Trim$(Mid$(RecordArray(17), 1, 6))
                Else
                    .Variables("CURRENCY").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable CUSTREF" & Trim$(Mid$(RecordArray(20), 69, 26)))
                If Trim$(Mid$(RecordArray(20), 69, 20)) > "" Then
                    .Variables("CUSTREF").Value = Trim$(Mid$(RecordArray(20), 69, 26))
                Else
                    .Variables("CUSTREF").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable CUSNO" & Trim$(Mid$(RecordArray(18), 25, 12)))
                If Trim$(Mid$(RecordArray(18), 25, 12)) > "" Then
                    .Variables("CUSNO").Value = Trim$(Mid$(RecordArray(18), 25, 12))
                Else
                    .Variables("CUSNO").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable COMREP" & Trim$(Mid$(RecordArray(24), 13, 20)))
                If Trim$(Mid$(RecordArray(24), 13, 20)) > "" Then
                    .Variables("COMREP").Value = Trim$(Mid$(RecordArray(24), 13, 20))
                Else
                    .Variables("COMREP").Value = wsSpaces
                End If
                If wsOrderText > "" Then
                    oWrite.WriteLine("Variable TITLE" & wsOrderText)
                    oWrite.WriteLine("Variable ORDERNARR" & wsQuoteNarr)
                    .Variables("TITLE").Value = wsOrderText
                    .Variables("ORDERNARR").Value = wsQuoteNarr
                End If
                .Variables("HNARR").Value = Hnarrstring
                .Variables("PRODUCT").Value = linestring
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection
            oWrite.Flush()
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process ProForma Invoice")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try

        Try
            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            worddoc.Save()
            worddoc.Close()
            worddoc = Nothing

            oWrite.WriteLine("Word closed process ProForma Invoice")
            oWrite.Flush()
            ' Write PDF

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process  ProForma Invoice")
            oWrite.WriteLine(Err)
            oWrite.WriteLine(Err.Number)
            oWrite.WriteLine(Err.Description)
            oWrite.Flush()
        End Try
        wsCopies = 1
        Print_PDF()
    End Sub

    Private Sub subProcessProForma1()

        '        On Error GoTo ErrHandler
        oWrite.WriteLine("Before try1 process ProForma")
        oWrite.Flush()
        If UBound(RecordArray) < 1 Then
            oWrite.WriteLine("No processable lines " & Now & " SO")
            Exit Sub
        End If

        wsCopies = 1

        Try
            wsQuoteNarr = ""
            wsOrderText = ""
            linestring = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*L*" Then
                    linestring = linestring & RTrim$(Mid$(RecordArray(i), 4, 120)) & vbCr ' & vbCrL
                End If
            Next


            ReDim HnarrArray(1)
            j = 1
            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*H*" Then
                    HnarrArray(j) = RTrim$(Mid$(RecordArray(i), 4, 120))
                    j = j + 1
                    ReDim Preserve HnarrArray(j)
                End If
            Next

            Hnarrstring = ""
            For Me.i = 0 To UBound(HnarrArray) - 1
                Hnarrstring = Hnarrstring & HnarrArray(i) & vbCr ' & vbCrLf
            Next

            wsUserPrinter = ""

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*P*" Then
                    wsUserPrinter = RTrim$(Mid$(RecordArray(i), 4, 20))
                End If
            Next

            For Me.i = 0 To UBound(RecordArray) - 1
                If Mid$(RecordArray(i), 1, 3) = "*A*" Then
                    wsOrderStatus = CInt(Trim$(Mid$(RecordArray(i), 4, 4)))
                End If
            Next
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process ProForma")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
        Try
            If IsNumeric(Trim$(Mid$(RecordArray(2), 1, 3))) Then
                sOrdRev = CInt(Trim$(Mid$(RecordArray(2), 1, 3)))
            End If
            wsEmailFrom = "<" & wsDefEmail & ">"
            wsEmailTo = "<" & wsDefEmail & ">"
            wsCustomer = Trim$(Mid$(RecordArray(11), 1, 36))
            wsOrder = Trim$(Mid$(RecordArray(20), 25, 12))
            If wsCo = "01" Then
                If Trim$(Mid$(RecordArray(18), 25, 12)) = "CU1082" Then
                    wsCopies = 1
                Else
                    wsCopies = 2
                End If
            End If
            If wsCo = "02" Then
                wsCopies = 2
            End If
            If wsCo = "03" Then
                wsCopies = 2
            End If
            If wsCo = "04" Then
                wsCopies = 2
            End If
            If wsCo = "05" Then
                wsCopies = 2
            End If

            '           ReDim UsedVariables(1)
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process ProForma")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
        Try
            oWrite.WriteLine("Word open next process ProForma")
            worddoc = WordApp.Documents.Open(wsTempDir & "\PR_" & wsCo & "_template.dotx")
            oWrite.WriteLine("Word open process ProForma")
            With worddoc
                oWrite.WriteLine("Variable CUSTOMER" & Trim$(Mid$(RecordArray(11), 1, 36)))

                If Trim$(Mid$(RecordArray(11), 1, 36)) > "" Then
                    .Variables("CUSTOMER").Value = Trim$(Mid$(RecordArray(11), 1, 36))
                Else
                    .Variables("CUSTOMER").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR1" & Trim$(Mid$(RecordArray(12), 1, 36)))
                If Trim$(Mid$(RecordArray(12), 1, 36)) > "" Then
                    .Variables("ADDR1").Value = Trim$(Mid$(RecordArray(12), 1, 36))
                Else
                    .Variables("ADDR1").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR2" & Trim$(Mid$(RecordArray(13), 1, 36)))
                If Trim$(Mid$(RecordArray(13), 1, 36)) > "" Then
                    .Variables("ADDR2").Value = Trim$(Mid$(RecordArray(13), 1, 36))
                Else
                    .Variables("ADDR2").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR3" & Trim$(Mid$(RecordArray(14), 1, 36)))
                If Trim$(Mid$(RecordArray(14), 1, 36)) > "" Then
                    .Variables("ADDR3").Value = Trim$(Mid$(RecordArray(14), 1, 36))
                Else
                    .Variables("ADDR3").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ADDR4" & Trim$(Mid$(RecordArray(15), 1, 36)))
                If Trim$(Mid$(RecordArray(15), 1, 36)) > "" Then
                    .Variables("ADDR4").Value = Trim$(Mid$(RecordArray(15), 1, 36))
                Else
                    .Variables("ADDR4").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable PCODE" & Trim$(Mid$(RecordArray(16), 1, 36)))
                If Trim$(Mid$(RecordArray(16), 1, 36)) > "" Then
                    .Variables("PCODE").Value = Trim$(Mid$(RecordArray(16), 1, 36))
                Else
                    .Variables("PCODE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD1" & Trim$(Mid$(RecordArray(11), 38, 36)))
                If Trim$(Mid$(RecordArray(11), 38, 36)) > "" Then
                    .Variables("DADD1").Value = Trim$(Mid$(RecordArray(11), 38, 36))
                Else
                    .Variables("DADD1").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD2" & Trim$(Mid$(RecordArray(12), 38, 36)))
                If Trim$(Mid$(RecordArray(12), 38, 36)) > "" Then
                    .Variables("DADD2").Value = Trim$(Mid$(RecordArray(12), 38, 36))
                Else
                    .Variables("DADD2").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD3" & Trim$(Mid$(RecordArray(13), 38, 36)))
                If Trim$(Mid$(RecordArray(13), 38, 36)) > "" Then
                    .Variables("DADD3").Value = Trim$(Mid$(RecordArray(13), 38, 36))
                Else
                    .Variables("DADD3").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD4" & Trim$(Mid$(RecordArray(14), 38, 36)))
                If Trim$(Mid$(RecordArray(14), 38, 36)) > "" Then
                    .Variables("DADD4").Value = Trim$(Mid$(RecordArray(14), 38, 36))
                Else
                    .Variables("DADD4").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DADD5" & Trim$(Mid$(RecordArray(15), 38, 36)))
                If Trim$(Mid$(RecordArray(15), 38, 36)) > "" Then
                    .Variables("DADD5").Value = Trim$(Mid$(RecordArray(15), 38, 36))
                Else
                    .Variables("DADD5").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable DPCODE" & Trim$(Mid$(RecordArray(16), 38, 12)))
                If Trim$(Mid$(RecordArray(16), 38, 12)) > "" Then
                    .Variables("DPCODE").Value = Trim$(Mid$(RecordArray(16), 38, 12))
                Else
                    .Variables("DPCODE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ORDER" & Trim$(Mid$(RecordArray(20), 25, 7)))
                If Trim$(Mid$(RecordArray(20), 25, 12)) > "" Then
                    If wsCo = "IM" Or wsCo = "CN" Then
                        .Variables("ORDER").Value = Trim$(Mid$(RecordArray(20), 25, 7)) & "_" & Trim(CStr(sOrdRev))
                    Else
                        .Variables("ORDER").Value = Trim$(Mid$(RecordArray(20), 25, 12))

                    End If
                Else
                    .Variables("ORDER").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable VATNO" & Trim$(Mid$(RecordArray(18), 69, 20)))
                If Trim$(Mid$(RecordArray(18), 69, 20)) > "" Then
                    .Variables("VATNO").Value = Trim$(Mid$(RecordArray(18), 69, 20))
                Else
                    .Variables("VATNO").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable REQDATE" & Trim$(Mid$(RecordArray(22), 25, 8)))
                If Trim$(Mid$(RecordArray(22), 25, 8)) > "" Then
                    .Variables("REQDATE").Value = Trim$(Mid$(RecordArray(22), 25, 8))
                Else
                    .Variables("REQDATE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable PROMDATE" & Trim$(Mid$(RecordArray(22), 69, 8)))
                If Trim$(Mid$(RecordArray(22), 69, 8)) > "" Then
                    .Variables("PROMDATE").Value = Trim$(Mid$(RecordArray(22), 69, 8))
                Else
                    .Variables("PROMDATE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable ENTDATE" & Trim$(Mid$(RecordArray(1), 1, 8)))
                If Trim$(Mid$(RecordArray(1), 1, 8)) > "" Then
                    .Variables("ENTDATE").Value = Trim$(Mid$(RecordArray(1), 1, 8))
                Else
                    .Variables("ENTDATE").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable CURRENCY" & Trim$(Mid$(RecordArray(17), 1, 6)))
                If Trim$(Mid$(RecordArray(17), 1, 6)) > "" Then
                    .Variables("CURRENCY").Value = Trim$(Mid$(RecordArray(17), 1, 6))
                Else
                    .Variables("CURRENCY").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable CUSTREF" & Trim$(Mid$(RecordArray(20), 69, 26)))
                If Trim$(Mid$(RecordArray(20), 69, 20)) > "" Then
                    .Variables("CUSTREF").Value = Trim$(Mid$(RecordArray(20), 69, 26))
                Else
                    .Variables("CUSTREF").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable CUSNO" & Trim$(Mid$(RecordArray(18), 25, 12)))
                If Trim$(Mid$(RecordArray(18), 25, 12)) > "" Then
                    .Variables("CUSNO").Value = Trim$(Mid$(RecordArray(18), 25, 12))
                Else
                    .Variables("CUSNO").Value = wsSpaces
                End If
                oWrite.WriteLine("Variable QUOTEREF" & Trim$(Mid$(RecordArray(21), 16, 20)))
                If Trim$(Mid$(RecordArray(21), 16, 20)) > "" Then
                    .Variables("QUOTEREF").Value = Trim$(Mid$(RecordArray(21), 16, 20))
                Else
                    .Variables("QUOTEREF").Value = wsSpaces
                End If
                .Variables("HNARR").Value = Hnarrstring
                .Variables("PRODUCT").Value = linestring
                .Range.Fields.Update()
            End With
            Dim oHeader As HeaderFooter
            Dim oSection As Section
            For Each oSection In worddoc.Sections
                For Each oHeader In oSection.Headers
                    If oHeader.Exists Then
                        For Each oField In oHeader.Range.Fields
                            oField.Update()
                        Next oField
                    End If
                Next oHeader
            Next oSection
            oWrite.Flush()
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process ProForma")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try

        Try
            ' lock the document to stop changes
            worddoc.Protect(WdProtectionType.wdAllowOnlyComments, , "jd837djh82")
            '    wsSaveDoc = wsNewDir & "\" & Trim$(Mid$(RecordArray(20), 25, 12)) & ".doc"
            wsSaveDoc = wsNewDir & "\" & wsCo & Trim$(Mid$(RecordArray(20), 25, 7)) & "_" & Trim(CStr(sOrdRev)) & ".doc"

            worddoc.SaveAs(wsSaveDoc)
            worddoc.Close()

            worddoc = Nothing

            oWrite.WriteLine("Word closed process ProForma")
            oWrite.Flush()
            ' Write PDF

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in process ProForma4")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
        Print_PDF()
    End Sub

    Private Sub print_doc()
        Dim worddoc As Document
        Dim j As Integer

        ' On Error GoTo ErrHandler

        Try
            worddoc = WordApp.Documents.Open(wsSaveDoc)

            If wsSuppressPrint = True Then
                Select Case UCase$(wsDocument)
                    Case "SO"
                        oWrite.WriteLine("SO Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "PO"
                        oWrite.WriteLine("PO Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "DN"
                        oWrite.WriteLine("DN Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "DW"
                        oWrite.WriteLine("DW Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "SI"
                        oWrite.WriteLine("SI Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "CN"
                        oWrite.WriteLine("CN Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "BI"
                        oWrite.WriteLine("BI Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "BO"
                        oWrite.WriteLine("BO Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "BQ"
                        oWrite.WriteLine("BQ Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "PE"
                        oWrite.WriteLine("PE Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "BL"
                        oWrite.WriteLine("BL Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "CO"
                        oWrite.WriteLine("CO Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case "PR"
                        oWrite.WriteLine("PR Printing suppressed " & wsSaveDoc & " at " & Now)
                        oWrite.Flush()
                    Case Else
                        If wsUserPrinter = "" Then
                            wsprinter = wsDefPrinter
                        Else
                            wsprinter = wsUserPrinterPath & wsUserPrinter
                        End If

                        oWrite.WriteLine("Printing document " & wsSaveDoc & " on " & wsprinter & " at " & Now)
                        oWrite.Flush()

                        WordApp.ActivePrinter = wsprinter

                        For j = 0 To wsCopies - 1

                            WordApp.Options.PrintBackground = False
                            WordApp.PrintOut()

                            Do While WordApp.BackgroundPrintingStatus > 0
                                i = i + 1
                                Call Sleep(50)
                            Loop
                            Sleep(700)
                        Next

                        oWrite.WriteLine("Printing complete on " & wsprinter & " at " & Now)
                        oWrite.Flush()

                        ' Section 1.0.4 added for second printer at Isle of Man

                        If wsCo = "IM" Then
                            If wsSecondPrinter > " " Then
                                WordApp.ActivePrinter = wsSecondPrinter
                                WordApp.Options.PrintBackground = False
                                WordApp.PrintOut()
                                Do While WordApp.BackgroundPrintingStatus > 0
                                    i = i + 1
                                    '               lstOutput.AddItem ("Loop : " & Trim$(Mid$(RecordArray(12), 33, 12)) & " " & i)
                                    Call Sleep(50)
                                Loop
                                Sleep(700)
                            End If
                        End If
                        ' End of added section

                        wsprinter = wsDefPrinter
                        wsUserPrinter = ""
                End Select
            Else
                If wsUserPrinter = "" Then
                    wsprinter = wsDefPrinter
                Else
                    wsprinter = wsUserPrinterPath & wsUserPrinter
                End If

                oWrite.WriteLine("Printing document " & wsSaveDoc & " on " & wsprinter & " at " & Now)
                oWrite.Flush()

                WordApp.ActivePrinter = wsprinter

                For j = 0 To wsCopies - 1

                    WordApp.Options.PrintBackground = False
                    WordApp.PrintOut()

                    Do While WordApp.BackgroundPrintingStatus > 0
                        i = i + 1
                        '               lstOutput.AddItem ("Loop : " & Trim$(Mid$(RecordArray(12), 33, 12)) & " " & i)
                        Call Sleep(50)
                    Loop
                    Sleep(700)
                Next

                oWrite.WriteLine("Printing complete on " & wsprinter & " at " & Now)
                oWrite.Flush()

                ' Section 1.0.4 added for second printer at Isle of Man

                If wsCo = "IM" Then
                    If wsSecondPrinter > " " Then
                        WordApp.ActivePrinter = wsSecondPrinter
                        WordApp.Options.PrintBackground = False
                        WordApp.PrintOut()
                        Do While WordApp.BackgroundPrintingStatus > 0
                            i = i + 1
                            '               lstOutput.AddItem ("Loop : " & Trim$(Mid$(RecordArray(12), 33, 12)) & " " & i)
                            Call Sleep(50)
                        Loop
                        Sleep(700)
                    End If
                End If
                ' End of added section

                wsprinter = wsDefPrinter
                wsUserPrinter = ""
                ' worddoc.Close()

                ' worddoc = Nothing
            End If

            Exit Sub

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            ' worddoc.Close()
            worddoc = Nothing
            oWrite.WriteLine("Error Occurred at " & Now & " in print doc")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub

    Private Sub Print_PDF()
        ' Dim i As Integer
        ' Dim sValue As String
        Dim fso1 As New Microsoft.VisualBasic.FileIO.FileSystem
        Dim wsDestDir As String
        Dim wsDestFile As String
        Dim wsCopyFlag As Boolean
        Dim wsDestDir1 As String
        Dim wsDateSuf As String
        Dim paramExportFilePath As String
        Dim wsTempLaserfiche As Boolean

        ' On Error GoTo ErrHandler
        Try
            wsDateSuf = Format(Now, "yyMMddhhmmss")
            oWrite.WriteLine("Date Suffix =" & wsDateSuf)
            oWrite.Flush()
            '    WordApp.Visible = False
            wsDestDir = wsJobDir & "\JOBS GPHC\JOB NO. "
            wsDestDir1 = wsDestDir
            wsDestFile = ""
            worddoc = WordApp.Documents.Open(wsSaveDoc)
            If wsSuppressPrint = False Then
                print_doc()
            End If

            If DebugOn = True Then
                WordApp.Visible = True
                WordApp.Activate()
            End If

            wsCopyFlag = False

            Select Case UCase$(wsCo)
                Case "SA"
                    wsDestDir = wsJobDir & "\"
                    wsCopyFlag = True
                Case "TC"
                    wsDestDir = wsJobDir & "\"
                    wsCopyFlag = True
                Case Else
                    wsDestDir = wsJobDir & "\"
                    wsCopyFlag = False
            End Select

            Select Case UCase$(wsDocument)
                Case "SO"
                    wsDestDir1 = wsDestDir & Trim$(Mid$(RecordArray(20), 25, 12))
                    wsDestFile = wsDestDir1 & "\" & Trim$(Mid$(RecordArray(20), 25, 12)) & "_" & Trim(CStr(sOrdRev)) & ".pdf"
                    If wsOrderStatus = 2 Then
                        wsSavePdf = wsPdfDir & "\" & wsCo & "\SalesOrders\QUOTE_" & Trim$(Mid$(RecordArray(20), 25, 12)) & "_" & Trim(CStr(sOrdRev)) & ".pdf"
                        wsSavePdf1 = wsPdfDir & "\" & wsCo & "\SalesOrders\" & Trim$(Mid$(RecordArray(20), 25, 12)) & ".pdf"
                    Else
                        wsSavePdf = wsPdfDir & "\" & wsCo & "\SalesOrders\" & Trim$(Mid$(RecordArray(20), 25, 12)) & "_" & Trim(CStr(sOrdRev)) & ".pdf"
                        wsSavePdf1 = wsPdfDir & "\" & wsCo & "\SalesOrders\" & Trim$(Mid$(RecordArray(20), 25, 12)) & ".pdf"
                    End If
                    wsCopies = 1
                Case "SI"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\SalesInvoices\" & Trim$(Mid$(RecordArray(10), 89, 12)) & ".pdf"
                    If wsCopy = False Then
                        wsSavePdf1 = wsPdfDir & "\" & wsCo & "\SalesInvoices\" & Trim$(Mid$(RecordArray(10), 89, 12)) & "_original.pdf"
                    End If
                    wsDestDir1 = wsDestDir & Trim$(Mid$(RecordArray(1), 1, 12))
                    wsDestFile = wsDestDir1 & "\" & Trim$(Mid$(RecordArray(10), 89, 12)) & ".pdf"
                Case "PO"
                    If Trim$(Mid$(RecordArray(11), 96, 26)) = "" Then
                        wsCopyFlag = False
                    Else
                        If wsCo = "IM" Or wsCo = "CN" Or wsCo = "SA" Or wsCo = "TC" Then
                            If Trim$(Mid$(RecordArray(11), 96, 26)) = "STOCK" Then
                                wsCopyFlag = False
                            Else
                                wsCopyFlag = True
                            End If
                        End If
                    End If
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\PurchaseOrders\" & Trim$(Mid$(RecordArray(8), 96, 12)) & "_" & wsDateSuf & ".pdf"
                    wsSavePdf1 = wsPdfDir & "\" & wsCo & "\PurchaseOrders\" & Trim$(Mid$(RecordArray(8), 96, 12)) & ".pdf"
                    If wsCopyFlag = True Then
                        wsDestDir1 = wsDestDir & Trim$(Mid$(RecordArray(11), 96, 26))
                        wsDestFile = wsDestDir1 & "\" & Trim$(Mid$(RecordArray(8), 96, 12)) & "_" & wsDateSuf & ".pdf"
                    End If
                    wsCopies = 1
                Case "DN"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\DespatchNotes\" & Trim$(Mid$(RecordArray(11), 52, 12)) & ".pdf"
                    wsDestDir1 = wsDestDir & Trim$(Mid$(RecordArray(22), 7, 12))
                    wsDestFile = wsDestDir1 & "\" & Trim$(Mid$(RecordArray(11), 52, 12)) & ".pdf"
                Case "DW"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\DespatchWaybill\" & Trim$(Mid$(RecordArray(11), 52, 6)) & "_" & Trim$(Mid$(RecordArray(23), 7, 10)) & "_" & Trim$(Mid$(RecordArray(23), 25, 8)) & "(" & Trim$(Mid$(RecordArray(23), 33, 3)) & ")" & ".pdf"
                    wsDestDir1 = wsDestDir & "DW"
                    wsDestFile = wsDestDir1 & "\" & Trim$(Mid$(RecordArray(11), 52, 6)) & "_" & Trim$(Mid$(RecordArray(23), 7, 10)) & "_" & Trim$(Mid$(RecordArray(23), 25, 8)) & "(" & Trim$(Mid$(RecordArray(23), 33, 3)) & ")" & ".pdf"
                Case "RA"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\RemittanceAdvices\" & Trim$(Mid$(RecordArray(3), 91, 12)) & ".pdf"
                Case "CN"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\CreditNotes\" & Trim$(Mid$(RecordArray(10), 89, 12)) & ".pdf"
                    wsCopyFlag = False
                Case "ST"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\Statements\" & Trim$(Mid$(RecordArray(1), 63, 12)) & ".pdf"
                    wsCopyFlag = False
                Case "PE"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\SalesInvoices\" & Trim$(Mid$(RecordArray(10), 89, 12)) & ".pdf"
                    wsDestDir1 = wsDestDir & "\" & Trim$(Mid$(RecordArray(1), 1, 12))
                    wsDestFile = wsDestDir1 & "\" & Trim$(Mid$(RecordArray(10), 89, 12)) & ".pdf"
                Case "BI"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\SalesInvoices\BI_" & Trim$(Mid$(RecordArray(2), 1, 12)) & ".pdf"
                    wsDestDir1 = wsDestDir & Trim$(Mid$(RecordArray(1), 1, 12))
                    wsDestFile = wsDestDir1 & "\BI_" & Trim$(Mid$(RecordArray(2), 1, 12)) & ".pdf"
                Case "PL"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\Consignments\PL_" & Trim$(PickListArray(0, 11)) & ".pdf"
                    wsDestDir1 = wsDestDir & Trim$(wsSavJob)
                    wsDestFile = wsDestDir1 & "\PL_" & Trim$(PickListArray(0, 11)) & ".pdf"
                Case "BL"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\Consignments\BL_" & Trim$(PickListArray(0, 11)) & ".pdf"
                    wsDestDir1 = wsDestDir & Trim$(wsSavJob)
                    wsDestFile = wsDestDir1 & "\BL_" & Trim$(PickListArray(0, 11)) & ".pdf"
                Case "CO"
                    Select Case UCase$(wsDocType)
                        Case "BO"
                            wsSavePdf = wsPdfDir & "\" & wsCo & "\Consignments\BO_" & Trim$(PickListArray(0, 9)) & ".pdf"
                            wsDestDir1 = wsDestDir & Trim$(wsSavJob)
                            wsDestFile = wsDestDir1 & "\BO_" & Trim$(PickListArray(0, 9)) & ".pdf"
                        Case "BQ"
                            wsSavePdf = wsPdfDir & "\" & wsCo & "\Consignments\BQ_" & Trim$(PickListArray(0, 9)) & ".pdf"
                            wsDestDir1 = wsDestDir & Trim$(wsSavJob)
                            wsDestFile = wsDestDir1 & "\BQ_" & Trim$(PickListArray(0, 9)) & ".pdf"
                    End Select
                Case "CQ"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\SalesInvoices\CQ_" & Trim$(PickListArray1(0, 12)) & ".pdf"
                    wsDestDir1 = wsDestDir & Trim$(wsSavJob)
                    wsDestFile = wsDestDir1 & "\CQ_" & Trim$(PickListArray1(0, 12)) & ".pdf"
                Case "PR"
                    wsDestDir1 = wsDestDir & Trim$(Mid$(RecordArray(20), 25, 7))
                    wsDestFile = wsDestDir1 & "\PR_" & Trim$(Mid$(RecordArray(20), 25, 7)) & "_" & Trim(CStr(sOrdRev)) & ".pdf"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\SalesInvoices\PR_" & Trim$(Mid$(RecordArray(20), 25, 7)) & "_" & Trim(CStr(sOrdRev)) & ".pdf"
                Case "SM"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\StockMovements\" & Trim$(Mid$(RecordArray(4), 23, 8)) & ".pdf"
                    wsDestDir1 = wsDestDir & "SM"
                    wsDestFile = wsDestDir1 & "\" & Trim$(Mid$(RecordArray(4), 23, 8)) & ".pdf"
                Case "QA"
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\Quality\" & Trim$(Mid$(RecordArray(5), 18, 12)) & "_" & Trim$(Mid$(RecordArray(6), 8, 12)) & ".pdf"
                    wsDestDir1 = wsDestDir & "QA"
                    wsDestFile = wsDestDir1 & "\" & Trim$(Mid$(RecordArray(5), 18, 12)) & "_" & Trim$(Mid$(RecordArray(6), 8, 12)) & ".pdf"
                Case Else
                    wsSavePdf = wsPdfDir & "\" & wsCo & "\Consignments\BQ_" & Trim(PickListArray(0, 9)) & ".pdf"
                    wsDestDir1 = wsDestDir
                    wsDestFile = wsDestDir1 & "\dummy.pdf"
            End Select

            paramExportFilePath = wsSavePdf

            Try

                oWrite.WriteLine("PDF print " & wsSavePdf)
                oWrite.Flush()
                worddoc.BuiltInDocumentProperties("Title") = wsSavePdf
                ' Export it in the specified format.
                worddoc.ExportAsFixedFormat(paramExportFilePath,
                            paramExportFormat, paramOpenAfterExport,
                            paramExportOptimizeFor, paramExportRange, paramStartPage,
                            paramEndPage, paramExportItem, paramIncludeDocProps,
                            paramKeepIRM, paramCreateBookmarks,
                            paramDocStructureTags, paramBitmapMissingFonts,
                            paramUseISO19005_1)
                oWrite.WriteLine("PDF print complete " & wsSavePdf)
                oWrite.Flush()
                If UCase$(wsDocument) = "PO" Or UCase$(wsDocument) = "SO" Or (UCase$(wsDocument) = "SI" And wsCopy = False) Then
                    paramExportFilePath = wsSavePdf1
                    worddoc.BuiltInDocumentProperties("Title") = wsSavePdf1
                    oWrite.WriteLine("PDF print " & wsSavePdf1)
                    oWrite.Flush()
                    ' Export it in the specified format.
                    worddoc.ExportAsFixedFormat(paramExportFilePath,
                                paramExportFormat, paramOpenAfterExport,
                                paramExportOptimizeFor, paramExportRange, paramStartPage,
                                paramEndPage, paramExportItem, paramIncludeDocProps,
                                paramKeepIRM, paramCreateBookmarks,
                                paramDocStructureTags, paramBitmapMissingFonts,
                                paramUseISO19005_1)
                    oWrite.WriteLine("PDF print complete " & wsSavePdf1)
                    oWrite.Flush()
                End If
                If UCase$(wsDocument) = "SI" And wsEmailFile = "NY" And wsCopy = False Then
                    wsEncryptFile = wsSavePdf.Replace(".pdf", "_encrypt.pdf")
                    PDFSecure(wsSavePdf, wsEncryptFile, wsPDFSecurity, "025646587454")
                    wsemailSubject = "INVOICE " & wsInvoice & " for Customer:" & wsCustomer
                    EASendEncryptMail(wsFromEmail, wsToEmail, wsemailSubject, wsSavePdf, wsEmailServer)
                End If

            Catch ex As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine(ex.Message)
                oWrite.WriteLine(Err.Description)
                oWrite.WriteLine(Err.Number)
                oWrite.WriteLine(ex.Message)
                oWrite.Flush()
                ' Respond to the error
                ' Close and release the Document object.
            End Try
            oWrite.WriteLine("Copy Flag " & wsCopyFlag & " File " & wsDestFile)
            oWrite.Flush()
            If wsCopyFlag = True Then
                Try
                    If wsDestFile > "" Then
                        If System.IO.Directory.Exists(Trim$(wsDestDir1)) = True Then
                            System.IO.File.Copy(wsSavePdf, wsDestFile, "True")
                        End If
                    End If
                Catch ex As Exception
                    bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                    oWrite.WriteLine(ex.Message)
                    oWrite.Flush()
                End Try
            End If

            If UCase$(wsDocument) = "PO" Or UCase$(wsDocument) = "SO" Or UCase$(wsDocument) = "PR" Or UCase$(wsDocument) = "SI" Or UCase$(wsDocument) = "DN" Then
                If wsFromEmail = "" Then
                    If wsCo = "SA" Or wsCo = "TC" Then
                        wsFromEmail = "mtms.admin@mmdafrica.co.za"
                    End If
                End If
                oWrite.WriteLine("Email details = TO:" & wsToEmail & " From " & wsEmailFrom)
                oWrite.Flush()
                oWrite.WriteLine("Email details = Dist To:" & wsDistEmail & " From " & wsEmailFrom)
                oWrite.Flush()
                oWrite.WriteLine("Email details = Subject:" & wsemailSubject & " Body " & wsemailBody)
                oWrite.Flush()
                oWrite.WriteLine("Email details = pdf:" & wsSavePdf & " Server " & wsEmailServer)
                oWrite.Flush()
                If Trim(wsToEmail) > "" Or Trim(wsDistEmail) > "" Then
                    If Trim(wsToEmail) > "" Then
                        SendMail(wsFromEmail, wsToEmail, wsCcEmail, wsemailSubject, wsemailBody, wsSavePdf, wsEmailServer)
                    End If
                    If Trim(wsDistEmail) > "" Then
                        SendMail(wsFromEmail, wsDistEmail, "", wsemailSubject, wsemailBody, wsSavePdf, wsEmailServer)
                    End If
                Else
                    oWrite.WriteLine("No email to details " & wsToEmail & " No email sent")
                    oWrite.Flush()
                End If
                wsToEmail = ""
                wsDistEmail = ""
                wsEmailFrom = ""
            End If

            If UCase$(wsDocument) = "RA" Then
                wsLaserFicheInUse = wsTempLaserfiche
            End If

            wsSavePdf = ""
            wsDestDir1 = ""

            worddoc.Close()
            worddoc = Nothing
            ' WordApp = Nothing

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            worddoc.Close()
            worddoc = Nothing
            oWrite.WriteLine("Error Occurred at " & Now & " in print PDF")
            oWrite.WriteLine(ex.Message)
            oWrite.Flush()
        End Try


        Exit Sub
    End Sub

    Private Sub PDFSecure(ByVal FileName As String, ByVal EncryptFile As String, ByVal Security As String, ByVal WriteSec As String)
        'Load File
        Dim doc As New PdfDocument()
        Try
            doc.LoadFromFile(FileName)
            'encrypt
            doc.Security.Encrypt(WriteSec, Security, PdfPermissionsFlags.Print Or PdfPermissionsFlags.FullQualityPrint, PdfEncryptionKeySize.Key256Bit)

            'Save and Launch File
            doc.SaveToFile(EncryptFile)
            doc.Close()
        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in PDFSecure")
            oWrite.WriteLine(ex.Message)
            oWrite.Flush()
        End Try
        doc = Nothing

    End Sub

    Private Sub SendMail(ByVal strFrom As String, ByVal strTo As String, ByVal strCc As String, ByVal strSubject As String, ByVal strBody As String, ByVal strAttachments As String, ByVal strEmailServer As String)

        Dim MailMessage As New System.Net.Mail.MailMessage()
        Dim SmtpServer As New System.Net.Mail.SmtpClient()

        Try
            ' Your SMTP server address
            SmtpServer.Host = strEmailServer
            SmtpServer.Port = 25 'wsEmailPort
            SmtpServer.Credentials = New Net.NetworkCredential("mmdafrica\mtmsadmins", "Net3172ioN@")
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network
            'SmtpServer.EnableSsl = True

            If Not strTo.Equals(String.Empty) Then
                Dim strEmail As String
                Dim strEmails() As String = strTo.Split(";")
                For Each strEmail In strEmails
                    MailMessage.To.Add(strEmail)
                Next
            End If
            If Not strCc = Nothing Then
                Dim strEmail As String
                Dim strEmails() As String = strCc.Split(";")
                For Each strEmail In strEmails
                    MailMessage.CC.Add(strEmail)
                Next
            End If
            MailMessage.From = New System.Net.Mail.MailAddress(strFrom)
            MailMessage.Subject = strSubject
            MailMessage.IsBodyHtml = False
            MailMessage.Body = strBody

            If Not strAttachments = Nothing Then
                Dim strFile As String
                Dim strAttach() As String = strAttachments.Split(";")
                For Each strFile In strAttach
                    MailMessage.Attachments.Add(New System.Net.Mail.Attachment(strFile))
                Next
            End If

            Try
                oWrite.WriteLine("start to send email")
                SmtpServer.Send(MailMessage)

                bResult = evl.WriteToEventLog("Email details = TO:" & strTo & " From " & strFrom, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Information, "MMD_2008")
                bResult = evl.WriteToEventLog("Email Sent.", System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Information, "MMD_2008")
                oWrite.WriteLine("Email details = TO:" & strTo & " From " & strFrom)
                oWrite.Flush()
            Catch ep As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ep.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in SendMail")
                oWrite.WriteLine(ep.Message)
                oWrite.Flush()
            End Try

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in email")
            oWrite.WriteLine(ex.Message)
            oWrite.Flush()
        End Try


    End Sub

    Private Sub SendEncryptMail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strAttachments As String, ByVal strEmailServer As String)
        Dim strBody As String = ""

        Dim MailMessage As New System.Net.Mail.MailMessage()
        Dim SmtpServer As New System.Net.Mail.SmtpClient()

        Try
            Dim sr = New System.IO.StreamReader(wsAppPath & "\emailtemplate.htm")
            strBody = sr.ReadToEnd
            sr.Close()
        Catch ex As Exception

        End Try
        'send the email 
        Try
            If Not strTo.Equals(String.Empty) Then
                Dim strEmail As String
                Dim strEmails() As String = strTo.Split(";")
                For Each strEmail In strEmails
                    MailMessage.To.Add(strEmail)
                Next
            End If

            'MailMessage.Bcc.Add("warwick@mmdafrica.co.za")
            MailMessage.From = New System.Net.Mail.MailAddress(strFrom)
            MailMessage.Subject = strSubject
            MailMessage.IsBodyHtml = True

            ' Your SMTP server address
            SmtpServer.Host = strEmailServer
            Dim oPort As Integer = 25 'wsEmailPort
            SmtpServer.Port = oPort
            MailMessage.Body = strBody
            SmtpServer.Credentials = New Net.NetworkCredential("mmdafrica\mtmsadmins", "Net3172ioN@")
            'SmtpServer.EnableSsl = True
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network

            If Not strAttachments.Equals(String.Empty) Then
                Dim strFile As String
                Dim strAttach() As String = strAttachments.Split(";")
                For Each strFile In strAttach
                    MailMessage.Attachments.Add(New System.Net.Mail.Attachment(strFile.Trim()))
                Next
            End If

            Try
                oWrite.WriteLine("start to send email with embedded image ...")

                SmtpServer.Send(MailMessage)

                bResult = evl.WriteToEventLog("Email details = TO:" & strTo & " From " & strFrom, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Information, "MMD_2008")
                bResult = evl.WriteToEventLog("Email Sent.", System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Information, "MMD_2008")
                oWrite.WriteLine("Email details = TO:" & strTo & " From " & strFrom)
                oWrite.Flush()
            Catch ep As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ep.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in SendEncryptEmail")
                oWrite.WriteLine(ep.Message)
                oWrite.Flush()
            End Try

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in email")
            oWrite.WriteLine(ex.Message)
            oWrite.Flush()
        End Try

    End Sub

    Private Sub EASendMail(ByVal strFrom As String, ByVal strTo As String, ByVal strCc As String, ByVal strSubject As String, ByVal strBody As String, ByVal strAttachments As String, ByVal strEmailServer As String)

        Dim oMail As EASendMail.SmtpMail = New EASendMail.SmtpMail("ES-D1508812687-00822-9352BFA949EEB7C5-2F283BCF82V35CA1")
        Dim oSmtp As EASendMail.SmtpClient = New EASendMail.SmtpClient

        Try
            oMail.To = New AddressCollection(strTo)
            If strCc > "" Then oMail.Cc = New AddressCollection(strCc)
            oMail.From = New EASendMail.MailAddress(strFrom)
            oMail.Subject = strSubject

            ' Your SMTP server address
            Dim oServer As SmtpServer = New SmtpServer(strEmailServer)
            'oServer.Protocol = ServerProtocol.SMTP
            oServer.Protocol = ServerProtocol.ExchangeEWS
            Dim oPort As Integer = wsEmailPort
            oServer.Port = oPort

            oServer.User = "mmdafrica\mtmsadmins"
            oServer.Password = "Pass123word"

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto

            Try
                oMail.TextBody = strBody
                If Not strAttachments.Equals(String.Empty) Then
                    Dim strFile As String
                    Dim strAttach() As String = strAttachments.Split(";")
                    For Each strFile In strAttach
                        oMail.AddAttachment(strFile.Trim())
                    Next
                End If

                oWrite.WriteLine("start to send email")
                oSmtp.SendMail(oServer, oMail)
                bResult = evl.WriteToEventLog("Email details = TO:" & strTo & " From " & strFrom, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Information, "MMD_2008")
                bResult = evl.WriteToEventLog("Email Sent.", System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Information, "MMD_2008")
                oWrite.WriteLine("Email details = TO:" & strTo & " From " & strFrom)
                oWrite.Flush()
            Catch ep As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ep.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in SendMail")
                oWrite.WriteLine(ep.Message)
                oWrite.Flush()
            End Try

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in email")
            oWrite.WriteLine(ex.Message)
            oWrite.Flush()
        End Try


    End Sub

    Private Sub EASendEncryptMail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strAttachments As String, ByVal strEmailServer As String)
        Dim strBody As String = ""

        Dim oMail As EASendMail.SmtpMail = New EASendMail.SmtpMail("ES-D1508812687-00822-9352BFA949EEB7C5-2F283BCF82V35CA1")
        Dim oSmtp As EASendMail.SmtpClient = New EASendMail.SmtpClient()

        Try
            Dim sr = New System.IO.StreamReader(wsAppPath & "\emailtemplate.htm")
            strBody = sr.ReadToEnd
            sr.Close()
        Catch ex As Exception

        End Try
        'send the email 
        Try
            oMail.Reset()
            oMail.To = New AddressCollection(strTo)
            oMail.From = New EASendMail.MailAddress("MMD Africa", strFrom)
            oMail.Subject = strSubject

            ' Your SMTP server address
            Dim oServer As SmtpServer = New SmtpServer(strEmailServer)
            'oServer.Protocol = ServerProtocol.SMTP
            oServer.Protocol = ServerProtocol.ExchangeEWS
            Dim oPort As Integer = wsEmailPort
            oServer.Port = oPort

            oServer.User = "mmdafrica\mtmsadmins"
            oServer.Password = "Pass123word"

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto

            Try
                oMail.ImportHtml(strBody, wsAppPath, ImportHtmlBodyOptions.ImportLocalPictures Or ImportHtmlBodyOptions.ImportCss)
                If Not strAttachments.Equals(String.Empty) Then
                    Dim strFile As String
                    Dim strAttach() As String = strAttachments.Split(";")
                    For Each strFile In strAttach
                        oMail.AddAttachment(strFile.Trim())
                    Next
                End If

                oWrite.WriteLine("start to send email with embedded image ...")
                oSmtp.SendMail(oServer, oMail)
                bResult = evl.WriteToEventLog("Email details = TO:" & strTo & " From " & strFrom, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Information, "MMD_2008")
                bResult = evl.WriteToEventLog("Email Sent.", System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Information, "MMD_2008")
                oWrite.WriteLine("Email details = TO:" & strTo & " From " & strFrom)
                oWrite.Flush()
            Catch ep As Exception
                bResult = evl.WriteToEventLog(Err.Number & ": " & ep.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
                oWrite.WriteLine("Error Occurred at " & Now & " in SendEncryptEmail")
                oWrite.WriteLine(ep.Message)
                oWrite.Flush()
            End Try

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in email")
            oWrite.WriteLine(ex.Message)
            oWrite.Flush()
        End Try

    End Sub

    Private Sub subUnload()
        Dim wsDelFiles, wsKillStr

        Try
            ' WordApp.Quit()
            WordApp = Nothing

            wsDelFiles = Dir(wsNewDir & "\*.doc")
            Do While wsDelFiles > ""
                wsKillStr = wsNewDir & "\" & wsDelFiles
                Kill(wsKillStr)
                wsDelFiles = Dir(wsNewDir & "\*.doc")
            Loop

            oWrite.WriteLine("Log closed at " & Now)
            oWrite.Flush()
            Me.Close()
            Me.Dispose()

        Catch ex As Exception
            bResult = evl.WriteToEventLog(Err.Number & ": " & ex.Message, System.Reflection.MethodBase.GetCurrentMethod.Name, EventLogEntryType.Error, "MMD_2008")
            oWrite.WriteLine("Error Occurred at " & Now & " in unload")
            oWrite.WriteLine(Err)
            oWrite.Flush()
        End Try
    End Sub
End Class
