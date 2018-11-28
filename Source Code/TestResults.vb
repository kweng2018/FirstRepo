Imports Microsoft.VisualBasic

Public Class TestResults

    'Here's the intended operation for this module
    ' 1) At startup, initialize the public variables at the top of this module
    ' 2) Call "StartTest" at the beginning of a new test passing in the unit
    '    serial number and the process step
    ' 3) For each of the test groups within an overall test, call StartTestGroup
    '    at the beginning of the test group and StopTestGroup when finished.  This
    '    module will automatically log the amount of time spent in the test group
    '    and the overall status for the test group.
    ' 4) Call RecordNumeric, RecordPassFail, or RecordString to log individual
    '    measurement results during the test.
    ' 5) Call "StopTest" at end of test.  This automatically save results to DCF file

    'Other stuff
    ' - Call GetTestStatus to determine the status of all tests
    ' - Call SetLimitsFile to use a ".csv" limits file

    'The following variables would usually be initialized at start of program
    Public Assembly_Type As String        'e.g., "Niagara_RFIM"
    Public Test_SW_Rev As String          'e.g., "1.2.1"
    Public Test_System_ID As String       'e.g., "RFIM-001"
    Public DCF_File As String             'e.g., "c:\testdata.dcf"
    Public Production_Site_ID As String   'e.g., "AGFV"
    Public Testset_ID As String           'e.g., "50018-7-1","1","2"
    Public Operator_ID As String          'e.g., "1234"
    Public Firmware_Rev As String         'this can be assigned anytime during test
    Public RF_Fixture_ID As String        'e.g., "RFIM-RF-001"
    Public Digi_Fixture_ID As String      'e.g., "RFIM-DF-001"


    'The following variables are set by calling "StartTest", "StopTest" & "StartTestGroup
    Private pSerial_Number As String
    Private pSerial_Number_CM As String
    Private pProcess_Step_ID As String      'e.g., "FTEST1", "PostBurnin"
    Private pStartTestTime As Date
    Private pStopTestTime As Date
    Private pStartGroupTime As Date
    Private pTestGroup As String

    'This variable stores all the test results recorded with "RecordMeasurement"
    Public TestData As New Collection
    Public LimitsData As New Collection

    Const DCF_Rev As String = "1"

    Public ReadOnly Property TestGroup() As String
        Get
            TestGroup = pTestGroup
        End Get
    End Property

    Public ReadOnly Property Serial_Number() As String
        Get
            Serial_Number = pSerial_Number
        End Get
    End Property

    Public ReadOnly Property Serial_Number_CM() As String
        Get
            Serial_Number_CM = pSerial_Number_CM
        End Get
    End Property

    Public ReadOnly Property StopTestTime() As String
        Get
            StopTestTime = pStopTestTime
        End Get
    End Property

    Public ReadOnly Property Process_Step_ID() As String
        Get
            Process_Step_ID = pProcess_Step_ID
        End Get
    End Property

    Public ReadOnly Property StartTestTime() As String
        Get
            StartTestTime = pStartTestTime
        End Get
    End Property

    '*****************************************************************************
    'The following functions should be called at the start/stop of an overall test and
    'at the start/stop of smaller "Test Groups"
    '*****************************************************************************

    Public Sub StartTest(ByVal SN As String, ByVal ProcStep As String, ByVal StarTime As Date, Optional ByVal _
      SN_CM As String = "")
        Call ClearAllData()
        pStartTestTime = StarTime
        pSerial_Number = SN
        pSerial_Number_CM = SN_CM
        pProcess_Step_ID = ProcStep
    End Sub

    Public Sub StopTest()
        pStopTestTime = Now()
        Call Save_DCF()
    End Sub

    Public Sub StartTestGroup(ByVal GroupName As String)
        Call ClearGroupData(GroupName)
        'If myRFIM Is Nothing Then
        '    delay_(1500)
        '    myRFIM = New RFIMComm(strIPaddress, 23)
        'End If
        pStartGroupTime = Now()
        pTestGroup = GroupName
    End Sub

    Public Sub StopTestGroup()
        Dim Duration As Long, status As String

        If pTestGroup = "" Then Exit Sub

        Duration = DateDiff("s", pStartGroupTime, Now())
        'add to check pause routine here

        Call RecordNumeric("TIME_" & pTestGroup, Duration, 0, 10000)

        If AbortTest Then
            status = "FAIL"
        Else
            status = GetGroupStatus(pTestGroup)
        End If
        Call RecordPassFail("STATUS_" & pTestGroup, (status = "PASS"))

        ' PrintData(myfrmMain.optPrintData.Checked)
        pTestGroup = ""
    End Sub

    '*****************************************************************************
    'The following functions should be called to log individual measurement results
    'during the course of the test.
    '*****************************************************************************

    Public Function RecordNumeric(ByVal Testname As String, ByVal Measurement As Double, _
    Optional ByVal ll As Double = 0.0#, Optional ByVal ul As Double = 0.0#) As Boolean

        Dim rec As Measurement, lim As Measurement

        rec = New Measurement
        rec.name = Testname
        rec.NumericType = True
        rec.TestGroup = pTestGroup
        rec.MeasuredValue = Measurement

        'Look-up test limits in LimitsData collection
        On Error Resume Next
        lim = LimitsData(UCase(rec.name))

        If ll <> 0 Or ul <> 0 Then
            rec.LowerLimit = ll  'KW
            rec.UpperLimit = ul
        Else
            If Err.Number = 0 Then
                rec.LowerLimit = lim.LowerLimit
                rec.UpperLimit = lim.UpperLimit
                rec.Units = lim.Units
                ll = rec.LowerLimit
                ul = rec.UpperLimit
            Else
                rec.LowerLimit = ll
                rec.UpperLimit = ul
            End If
        End If

        'Check against the limits
        rec.Passed = (Measurement >= rec.LowerLimit And Measurement <= rec.UpperLimit)

        Call TestData.Add(rec, UCase(Testname))
        RecordNumeric = rec.Passed

        'Call CheckPauseRun()
    End Function

    Public Function RecordString(ByVal Testname As String, ByVal Measurement As String, _
      Optional ByVal MatchStr As String = "*") As Boolean

        Dim rec As Measurement

        rec = New Measurement
        rec.name = Testname
        rec.NumericType = False
        rec.TestGroup = pTestGroup
        rec.MeasuredString = Measurement

        'Check against the limits
        rec.Passed = (Measurement Like MatchStr)

        Call TestData.Add(rec, UCase(Testname))
        RecordString = rec.Passed
    End Function

    Public Sub RecordPassFail(ByVal Testname As String, ByVal IsPass As Boolean)
        Dim rec As Measurement

        rec = New Measurement
        rec.name = Testname
        rec.NumericType = True
        rec.TestGroup = pTestGroup
        rec.LowerLimit = 0
        rec.UpperLimit = 0
        rec.MeasuredValue = IIf(IsPass, 0, -1)
        rec.Passed = IsPass

        Call TestData.Add(rec, UCase(Testname))
    End Sub

    '*****************************************************************************
    'The following functions can be used to clear the measurement list, but this
    'is usually not necessary because the "StartTest" procedures take care of this.
    '*****************************************************************************

    Public Sub ClearAllData()
        Call DeleteMeasurements("*")
        pSerial_Number = ""
    End Sub

    Public Sub ClearGroupData(ByVal GroupName As String)
        Call DeleteMeasurements(GroupName)
        pTestGroup = ""
    End Sub

    Private Sub DeleteMeasurements(ByVal GroupName As String)
        Dim i As Integer, rec As Measurement
        For i = TestData.Count() To 1 Step -1
            rec = TestData(i)
            If GroupName = "*" Or GroupName = rec.TestGroup Then
                Call TestData.Remove(i)
            End If
        Next
    End Sub

    '*****************************************************************************
    'The following functions can be used to determine the test status of individual
    'test groups or the entire test.
    '*****************************************************************************

    Public Function GetGroupFailures(ByVal GroupName As String) As Integer
        Dim NumPass As Integer, NumFail As Integer
        Call CountPassFail(GroupName, NumPass, NumFail)
        GetGroupFailures = NumFail
    End Function

    Public Function GetGroupPasses(ByVal GroupName As String) As Integer
        Dim NumPass As Integer, NumFail As Integer
        Call CountPassFail(GroupName, NumPass, NumFail)
        GetGroupPasses = NumPass
    End Function

    Public Function GetFailures() As Integer
        GetFailures = GetGroupFailures("*")
    End Function

    Public Function GetPasses() As Integer
        GetPasses = GetGroupPasses("*")
    End Function

    Public Sub CountPassFail(ByVal GroupName As String, ByRef NumPass As Integer, _
      ByRef NumFail As Integer)
        Dim rec As Measurement
        NumPass = 0
        NumFail = 0
        For Each rec In TestData
            If GroupName = "*" Or GroupName = rec.TestGroup Then
                If rec.Passed Then
                    NumPass = NumPass + 1
                Else
                    NumFail = NumFail + 1
                End If
            End If
        Next
    End Sub

    Public Function GetGroupStatus(ByVal GroupName As String) As String
        Dim NumPass As Integer, NumFail As Integer

        Call CountPassFail(GroupName, NumPass, NumFail)

        ' Check the user abort flag
        If AbortTest Then
            GetGroupStatus = "FAIL"
        Else
            ' if no abort then continue the pass/fail tally
            If NumFail > 0 Then
                GetGroupStatus = "FAIL"
            Else
                If NumPass >= 0 Then
                    GetGroupStatus = "PASS"
                Else
                    GetGroupStatus = "UNTESTED"
                End If
            End If
        End If
    End Function

    Public Function GetTestStatus() As String
        GetTestStatus = GetGroupStatus("*")
    End Function

    '*****************************************************************************
    'The following functions can be used to save or print the test data.  Note that
    'the Save_DCF is automatically called from StopTest.
    '*****************************************************************************

    Public Sub Save_DCF(Optional ByVal DCF_File As String = "c:\qstatii.dat.dcf")
        Dim fh As Integer, tmp As String, rec As Measurement

        If DCF_File = "" Then Exit Sub
        If TestData.Count = 0 Then Exit Sub

        fh = FreeFile()

        ' Open DCF File for Append
        FileOpen(fh, DCF_File, OpenMode.Append)
        tmp = FormatEventStart()
        PrintLine(fh, tmp)

        For Each rec In TestData
            tmp = FormatProductMeasure(rec)
            PrintLine(fh, tmp)
        Next

        tmp = FormatEventStop()
        PrintLine(fh, tmp)
        FileClose(fh)
    End Sub

    Private Function FormatEventStart() As String
        Dim tmp As String, program_id As String
        Dim TestTimeStr As String
        TestTimeStr = CStr(pStartTestTime)

        If Process_Step_ID Like "RFIM#" Then
            program_id = Right(Process_Step_ID, 2)
        Else
            program_id = Process_Step_ID
        End If

        tmp = "EVENTSTART|"                   '   0    Event Start
        tmp = tmp & DCF_Rev & "|"             '   1    dcf_rev
        'tmp = tmp & Format(pStartTestTime, "yyyymmddHhNnSs") & "00|"  

        tmp = tmp & Format(pStartTestTime.Year, "0000") & Format(pStartTestTime.Month, "00") & _
                    Format(pStartTestTime.Day, "00") & Format(pStartTestTime.Hour, "00") & _
                    Format(pStartTestTime.Minute, "00") & Format(pStartTestTime.Second, "00")
        '   2    event_date_time
        tmp = tmp & "00|"
        tmp = tmp & "TEST|"                   '   3    event_type
        tmp = tmp & Production_Site_ID & "|"  '   4    prod_site_id
        tmp = tmp & "|"                       '   5    prod_line_id 
        tmp = tmp & pProcess_Step_ID & "|"    '   6    process_step_id
        tmp = tmp & Test_System_ID & "|"      '   7    controller_id
        tmp = tmp & "1|"                      '   8    machine_id
        tmp = tmp & "|"                       '   9    machine_type
        tmp = tmp & "|"                       '   10   machine_mode:data_source
        tmp = tmp & "|"                       '   11   machine_step_status
        tmp = tmp & program_id & "|"          '   12   program_id
        tmp = tmp & Test_SW_Rev & "|"         '   13   program_rev
        tmp = tmp & RF_Fixture_ID & "|"       '   14   resource_id
        tmp = tmp & "|"                       '   15   program_rev
        tmp = tmp & Digi_Fixture_ID & "|"     '   16   customer_id
        'tmp = tmp & "|"                       '   17   order_id
        tmp = tmp & Assembly_Type & "|"       '   17   assembly_type
        tmp = tmp & "|"                       '   18   assembly_rev
        tmp = tmp & pSerial_Number & "|"      '   19   asmbly_serial_number
        tmp = tmp & "|"                       '   20   assembly_option
        tmp = tmp & "|"                       '   21   verification_asmbly
        tmp = tmp & "|"                       '   22   lot_id
        tmp = tmp & "|"                       '   23   batch_id
        tmp = tmp & "YES|"                    '   24   sampled
        tmp = tmp & "|"                       '   25   sampling_rate
        tmp = tmp & "|"                       '   26   sampling_method
        tmp = tmp & Operator_ID & "|"         '   27   operator_id
        tmp = tmp & "|"                       '   28   operator_type

        FormatEventStart = tmp
    End Function

    Private Function FormatProductMeasure(ByVal rec As Measurement) As String
        Dim tmp As String, status As String
        status = IIf(rec.Passed, "PASS", "FAIL")

        tmp = "PRODUCTMEASURE|"                 '  0    Measurement Record
        tmp = tmp & DCF_Rev & "|"               '  1    dcf_rev
        tmp = tmp & rec.name & "|"              '  2    test_designator
        tmp = tmp & "|"                         '  3    subtest_designator
        tmp = tmp & status & "|"                '  4    test_status
        tmp = tmp & "|"                         '  5    event_duration
        tmp = tmp & rec.MeasuredString & "|"    '  6    non_numeric
        tmp = tmp & Format(rec.MeasuredValue, "0.000") & "|"  '  7    numeric
        tmp = tmp & rec.Units & "|"                 '  8    units
        tmp = tmp & "||||||||||||"              '  9-20 not used
        tmp = tmp & rec.LowerLimit & "|"        ' 21    gen1
        tmp = tmp & rec.UpperLimit & "|"        ' 22    gen2
        tmp = tmp & rec.TestGroup & "|"         ' 23    gen3
        tmp = tmp & "|"                         ' 24    gen4
        tmp = tmp & "|"                         ' 25    gen5

        FormatProductMeasure = tmp
    End Function

    Private Function FormatEventStop() As String
        Dim tmp As String, status As String, Duration As Double

        ' Set the PASS/FAIL Status to Fail if Operator Aborts Test
        ' MZ 5-11-04
        If AbortTest Then
            status = "ABORT"
        Else
            status = GetTestStatus()  'PASS or FAIL
        End If

        Duration = DateDiff("s", pStartTestTime, pStopTestTime)

        tmp = "EVENTSTOP|"                    '   0    Event Stop
        tmp = tmp & DCF_Rev & "|"             '   1    dcf_rev
        tmp = tmp & status & "|"              '   2    event_status
        tmp = tmp & Duration & "|"            '   3    event_duration
        tmp = tmp & "|"                       '   4    validity
        tmp = tmp & Test_SW_Rev & "|"         '   5    gen1
        tmp = tmp & Firmware_Rev & "|"        '   6    gen2
        tmp = tmp & pSerial_Number_CM & "|"   '   7    gen3
        tmp = tmp & "|"                       '   8    gen4
        tmp = tmp & "|"                       '   9    gen5

        FormatEventStop = tmp
    End Function

    Public Sub PrintData(ByVal fPrint As Boolean, Optional ByVal fname As String = "c:\functest.txt")
        'Dim Data As String, Testname As String, ll As String, ul As String, meas As String
        Dim pf As String, Test_Duration As String, Test_Result As String
        Dim rec As Measurement, Value As String
        Dim PrintFile As Integer

        Test_Duration = DateDiff("s", pStartTestTime, Now())
        Test_Result = GetTestStatus()

        PrintFile = FreeFile()
        FileOpen(PrintFile, fname, OpenMode.Output)

        Print(PrintFile, TAB(10), "UUT Type:", TAB(22), Assembly_Type, TAB(47), "Test Step:", TAB(59), pProcess_Step_ID)
        Print(PrintFile, TAB(10), "Ser Number:", TAB(22), pSerial_Number, TAB(47), "Status:", TAB(59), Test_Result)
        Print(PrintFile, TAB(10), "Date/Time:", TAB(22), Format(pStartTestTime, "General Date"), TAB(47), "Duration:", TAB(59), Test_Duration)
        Print(PrintFile, TAB(10), "Testset ID:", TAB(22), Test_System_ID, TAB(47), "Fixture:", TAB(59), Testset_ID)
        Print(PrintFile, TAB(10), "Operator:", TAB(22), Operator_ID, TAB(47), "Test S/W:", TAB(59), Test_SW_Rev)
        Print(PrintFile, "")
        Print(PrintFile, TAB(5), "TEST NAME", TAB(41), "LL", TAB(50), "MEAS", TAB(62), "UL", TAB(69), "STATUS")

        For Each rec In TestData
            'If rec.TestRun Then
            If rec.NumericType Then
                Value = Format(rec.MeasuredValue, "0.000")
            Else
                Value = rec.MeasuredString
            End If

            If rec.Passed Then
                pf = "OK"
            Else
                pf = "FAIL"
            End If
            Print(PrintFile, TAB(5), rec.name, TAB(40), rec.LowerLimit, TAB(50), Value, TAB(61), rec.UpperLimit, TAB(69), pf)
        Next
        FileClose(PrintFile)

        If fPrint Then
            Call Shell("notepad /p " & fname, vbMinimizedNoFocus)
        End If
    End Sub

    '*****************************************************************************
    'The following functions deal with test limits files.  Only the SetLimitsFile is
    'public.  Call it with empty string for no limits file.
    '*****************************************************************************

    Public Sub SetLimitsFile(ByVal filename As String)
        Call DeleteLimits()
        If filename <> "" Then
            Call ReadLimitsFile(filename)
        End If
    End Sub

    Private Sub ReadLimitsFile(ByVal filename As String)
        Dim fh As Integer, x As Object, y As Measurement

        Try
            fh = FreeFile()
            FileOpen(fh, filename, OpenMode.Input)
            Do Until EOF(fh)
                x = FileSystem.LineInput(fh)
                x = Split(CType(x, String), ",")
                If UBound(x) >= 3 Then
                    y = New Measurement
                    y.name = UCase(x(0))
                    y.LowerLimit = x(1)
                    y.UpperLimit = x(2)
                    y.Units = x(3)
                    Call LimitsData.Add(y, y.name)
                End If
            Loop

            FileClose(fh)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & filename & "ReadLimitsFile", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            FileClose(fh)
        End Try

    End Sub

    Private Sub DeleteLimits()
        Dim i As Integer
        For i = LimitsData.Count() To 1 Step -1
            Call LimitsData.Remove(i)
        Next
    End Sub
    Protected Class Measurement
        'All code and associated files Andrew Corp, Proprietary
        'Use pursuant to Company instructions
        '
        'Copyright 2004, 2006
        'Andrew Corp
        '
        Public name As String
        Public MeasuredValue As Double
        Public MeasuredString As String
        Public NumericType As Boolean
        Public LowerLimit As Double
        Public UpperLimit As Double
        Public Passed As Boolean
        Public TestRun As Boolean
        Public ActiveTest As Integer
        Public TestGroup As String
        'Added 11July2006
        Public Units As String

    End Class

End Class
