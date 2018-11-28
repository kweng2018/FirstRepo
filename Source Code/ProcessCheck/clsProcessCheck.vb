Option Explicit On

Public Class clsProcessCheck
    'Copyright 2007, Andrew Corp. Proprietary Information

    'clsProcessCheck
    'Copyright 2002, Andrew Corp
    '
    'Class to handle Celestica Toronto Process Check method.
    '1. Set RequestFile and ResponseFiles absolute path and file names, if you don't want to use the defaults
    '1B. Set CommandLine property of the string you want in the ExecCmd line, if you don't want to use the default
    '2. Assign OperatorID, UUTType and ProcessStep properties
    '3. For each serial number under test, call AddRecord function.
    '4. Call GetProcCheck function. Returns TRUE if PC completed OK, False if PC failed because of a system error
    '5. Call ValidProcessCheck. If TRUE, it is OK to continue testing
    '                           If FALSE, there is something wrong with OperatorID, unit SerialNumber, or something
    '6. If you want, you can search the response file to convert Celestica and Andrew bar codes

    Private pTestStep As String
    Private pOperatorID As String
    Private pUUTType As String
    Private pReqFile As String 'Path/Name of Request File
    Private pRespFile As String 'Path/Name of Response File
    Private pPCString() As String 'Last Process Check results string
    Private pPCCheckOK As Boolean 'TRUE if data returned from PCCheck is valid
    Private Structure UUTInputDataType
        Public Barcode As String
        Public Slot As String
    End Structure
    Private UUTData() As UUTInputDataType
    Private pUnitCount As Integer 'Pointer for UUTData
    Private pCommandLine As String

    Private Structure STARTUPINFO
        Public cb As Long
        Public lpReserved As String
        Public lpDesktop As String
        Public lpTitle As String
        Public dwX As Long
        Public dwY As Long
        Public dwXSize As Long
        Public dwYSize As Long
        Public dwXCountChars As Long
        Public dwYCountChars As Long
        Public dwFillAttribute As Long
        Public dwFlags As Long
        Public wShowWindow As Integer
        Public cbReserved2 As Integer
        Public lpReserved2 As Long
        Public hStdInput As Long
        Public hStdOutput As Long
        Public hStdError As Long
    End Structure

    Private Structure PROCESS_INFORMATION
        Public hProcess As Long
        Public hThread As Long
        Public dwProcessID As Long
        Public dwThreadID As Long
    End Structure

    Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal _
       hHandle As Long, ByVal dwMilliseconds As Long) As Long

    Private Declare Function CreateProcessA Lib "kernel32" (ByVal _
       lpApplicationName As String, ByVal lpCommandLine As String, ByVal _
       lpProcessAttributes As Long, ByVal lpThreadAttributes As Long, _
       ByVal bInheritHandles As Long, ByVal dwCreationFlags As Long, _
       ByVal lpEnvironment As Long, ByVal lpCurrentDirectory As String, _
    ByVal lpStartupInfo As STARTUPINFO, ByVal lpProcessInformation As _
       PROCESS_INFORMATION) As Long

    Private Declare Function CloseHandle Lib "kernel32" _
       (ByVal hObject As Long) As Long

    Private Declare Function GetExitCodeProcess Lib "kernel32" _
       (ByVal hProcess As Long, ByVal lpExitCode As Long) As Long

    'Constants used by the API functions
    Const WM_CLOSE = &H10
    Const INFINITE = &HFFFFFFFF
    Private Const NORMAL_PRIORITY_CLASS = &H20&

    'Private Function ExecCmd(ByVal cmdline$, Optional ByVal TimeOutMilliseconds As Long = 100)
    '    'Starts an external command, application waits for command to terminate
    '    'If TimeOut=0 then wait infinite, otherwise wait for timeout, then continue
    '    Dim proc As PROCESS_INFORMATION
    '    Dim start As STARTUPINFO
    '    Dim Ret&
    '    Dim TimeOutt As Long
    '    ' Initialize the STARTUPINFO structure:
    '    start.cb = Len(start)
    '    TimeOutt = IIf(TimeOutMilliseconds = 0, INFINITE, TimeOutMilliseconds)

    '    ' Start the shelled application:
    '    Ret& = CreateProcessA(vbNullString, cmdline$, 0&, 0&, 1&, _
    '        NORMAL_PRIORITY_CLASS, 0&, vbNullString, start, proc)

    '    ' Wait for the shelled application to finish:
    '    Ret& = WaitForSingleObject(proc.hProcess, TimeOutt)
    '    Call GetExitCodeProcess(proc.hProcess, Ret&)
    '    Call CloseHandle(proc.hThread)
    '    Call CloseHandle(proc.hProcess)
    '    ExecCmd = Ret&
    'End Function
    Public Property OperatorID() As String
        Get
            OperatorID = pOperatorID
        End Get
        Set(ByVal value As String)
            pOperatorID = value
        End Set
    End Property

    Public Property UUTType() As String
        Get
            UUTType = pOperatorID
        End Get
        Set(ByVal value As String)
            pUUTType = value
        End Set
    End Property


    Public Property TestStep() As String
        Get
            TestStep = pTestStep
        End Get
        Set(ByVal value As String)
            pTestStep = value
        End Set
    End Property


    Public Property CommandLine() As String
        Get
            CommandLine = pCommandLine
        End Get
        Set(ByVal value As String)
            pCommandLine = value
        End Set
    End Property


    Public Function AddRecord(ByVal Barcode As String, Optional ByVal Slot As String = "Slot") As Integer
        'Adds Serial Number record to data
        pUnitCount = pUnitCount + 1
        ReDim Preserve UUTData(0 To pUnitCount - 1)
        UUTData(pUnitCount - 1).Barcode = Barcode
        UUTData(pUnitCount - 1).Slot = Slot
    End Function

    Public Sub New()
        pUnitCount = 0
        ' ReDim UUTData(0 To 0)
        pReqFile = "C:\PCRequest.txt"
        pRespFile = "C:\PCResponse.txt"
        pTestStep = Test_Group
        ReDim pPCString(50)
        pPCCheckOK = False
        pUUTType = Chr(0)
        pCommandLine = "C:\ODC\ProcessCheck.exe"
    End Sub

    Private Function BuildReqFile() As Boolean
        'Builds Request file
        Dim FP As Long
        Dim Strg As String, i As Integer
        On Error GoTo ErrHandlerBReqFile
        'Write common section
        Strg = "OperatorID=" & "Operator" & NL & _
                "End Common" & NL2
        'Write the PC section

        For i = 0 To slotnum
            If BI_Data(i).UnitInfo.UnitActive Then
                Strg = Strg & _
                "TestStep=" & Test_Group & NL & _
                "UUTType=" & "" & NL & _
                "BarCode=" & BI_Data(i).UnitInfo.UnitSN & NL & _
                "Slot=" & CStr(i + 1) & NL & _
                "END PC" & NL2
            End If
        Next

        'write out string to file
        FP = FreeFile()
        FileOpen(FP, pReqFile, OpenMode.Output)
        Print(FP, Strg)
        FileClose(FP)
        'Open "C:\PCRequest.txt" For Output As FP
        'Print #FP, Strg
        '    Close(FP)
ErrHandlerBReqFile:
        BuildReqFile = Err.Number = 0
        If Err.Number <> 0 Then
            Call MsgBox("Error in clsProcessCheck.BuildReqFile" & NL & Err.Description, vbCritical + vbOKOnly, "Error Number=" & Err.Number)
        End If

    End Function
    Public Function GetProcessCheck(ByVal TimeOut As Double) As Boolean
        'Returns TRUE if PC is valid
        'Returns file info in RetString for parsing outside of class
        'Waits for Timeout seconds, If TimeOut=0, then infinite wait

        Dim FP As Long, i As Integer, TS As String, lTO As Long
        'Dim FS As New Scripting.FileSystemObject
        Err.Clear()
        lTO = TimeOut * 1000 'Convert to milliseconds
        On Error Resume Next
        CreateFile(pRespFile) 'Clear out response file
        GetProcessCheck = BuildReqFile()
        On Error Resume Next

        'Shell out to executable, wait for it to exit, then continue
        Shell(CommandLine, AppWinStyle.NormalFocus, True)

        On Error GoTo ehgetpc
        'Verify return file is readable
        i = 0
        FP = FreeFile()
        'Open pRespFile For Input As FP
        FileOpen(FP, pRespFile, OpenMode.Input)
        Do While Not EOF(FP)
            'Line Input #FP, TS
            TS = LineInput(FP)
            If TS <> "" Then
                'Throw out blank lines
                pPCString(i) = UCase(TS)
                i = i + 1
            End If
            If i >= UBound(pPCString) Then
                ReDim Preserve pPCString(i + 10)
            End If
        Loop
        GetProcessCheck = GetProcessCheck And Err.Number = 0 And i > 0 'Return TRUE if no file errors and more than one line was read out
        pPCCheckOK = GetProcessCheck
ehgetpc:
        If Err.Number <> 0 Then
            Call MsgBox("Error in clsProcessCheck.GetProcessCheck" & NL & Err.Description, vbCritical, "Error Number=" & Err.Number)
        End If
        FileClose(FP)
        'Put these in after class has been debugged
        On Error Resume Next
        '    Call Kill(pReqFile)
        '    Call Kill(pRespFile)
        Err.Clear()
    End Function
    Public Function Andrew2Celestica(ByVal Andrew As String) As String
        'Converts Andrew BarCode into Celestica Barcode
        'Only valid after sucessful ProcessCheck

        Dim i As Integer, Done As Boolean, Stt As Integer
        Andrew2Celestica = ""
        Done = False
        If pPCCheckOK Then
            'Look thru lines for andrew bar code
            i = LBound(pPCString)
            Do Until i = UBound(pPCString) + 1 Or Done
                Done = InStr(1, pPCString(i), Andrew) <> 0
            Loop
            'i should now point to desired "Andrew" line
            i = i - 1
            'i now pointing at desired "Celestica" line
            Stt = InStr(1, pPCString(i), "=")
            If Stt <= 0 Then Stt = 1
            Andrew2Celestica = Mid(pPCString(i), Stt)
        End If
    End Function
    Public Function Celestica2Andrew(ByVal Andrew As String) As String
        'Converts Andrew BarCode into Celestica Barcode
        'Only valid after sucessful ProcessCheck

        Dim i As Integer, Done As Boolean, Stt As Integer
        Celestica2Andrew = ""
        Done = False
        If pPCCheckOK Then
            'Look thru lines for Celestica bar code
            i = LBound(pPCString)
            Do Until i = UBound(pPCString) + 1 Or Done
                Done = InStr(1, pPCString(i), Andrew) <> 0
            Loop
            'i should now point to desired "Celestica" line
            i = i + 1
            'i now pointing at desired "Andrew" line
            Stt = InStr(1, pPCString(i), "=")
            If Stt <= 0 Then Stt = 1
            Celestica2Andrew = Mid(pPCString(i), Stt)
        End If
    End Function
    Public Function ValidProcessCheck() As Boolean
        '"TRUE if process check returned a PASS
        'Looks at the second line of the PCResponse File, If the string "PASS" appears in it, then the test is assumed to have passed

        ValidProcessCheck = pPCCheckOK And InStr(1, pPCString(1), "PASS")

    End Function



    Public Sub CreateFile(ByVal ResFileName As String)
        Dim FP As Long
        FP = FreeFile()
        FileOpen(FP, ResFileName, OpenMode.Output)
        FileClose(FP)
    End Sub



End Class