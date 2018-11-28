Option Strict Off
Option Explicit On

Module ErrorMod
    'Error Constants
    Public Enum ErrorConstants
        ERR_INVALID_PARAM = vbObjectError + 10
        ERR_POWERON = vbObjectError + 50
        ERR_USER_ABORT = vbObjectError + 100
        ERR_ABORT_ON_FAIL = vbObjectError + 101
        ERR_VISA = vbObjectError + 200
        ERR_COMM = vbObjectError + 300
        ERR_ADJUST_ESG = vbObjectError + 500
        ERR_FILEIO = vbObjectError + 700
    End Enum

    Public Sub RecordErrorResult(ByVal BaseTestName As String, ByVal ErrNumber As ErrorConstants)
        Dim Testname As String
        Select Case ErrNumber
            Case 0
                Exit Sub
            Case ErrorConstants.ERR_USER_ABORT
                Testname = BaseTestName & "_USER_ABORT"
            Case ErrorConstants.ERR_ABORT_ON_FAIL
                Testname = BaseTestName & "_ABORT_ON_FAIL"
            Case ErrorConstants.ERR_POWERON To (ErrorConstants.ERR_POWERON + 49)
                Testname = BaseTestName & "_ERROR_POWER_ON"
            Case ErrorConstants.ERR_COMM To (ErrorConstants.ERR_COMM + 99)
                Testname = BaseTestName & "_ERROR_COMM"
            Case ErrorConstants.ERR_VISA
                Testname = BaseTestName & "_ERROR_VISA"
            Case ErrorConstants.ERR_INVALID_PARAM
                Testname = BaseTestName & "_INVALID_PARAM"
            Case ErrorConstants.ERR_ADJUST_ESG
                Testname = BaseTestName & "_ERROR_ADJUST_ESG"
            Case ErrorConstants.ERR_FILEIO
                Testname = BaseTestName & "_ERROR_FILE_IO"
            Case Else
                Testname = BaseTestName & "_UNEXPECTED_ERROR"
                'Call GUI.RecordResult(Testname, ErrNumber, 0, 0)
                Exit Sub
        End Select

        'Call GUI.RecordResult(Testname, ErrNumber - vbObjectError, 0, 0)
    End Sub

    Public Function ElapsedTime(ByVal StartTime As Single, ByVal StopTime As Single) As Single
        Const SecsPerDay As Double = 24.0# * 60 * 60
        ElapsedTime = New_Mod(StopTime - StartTime, SecsPerDay)
    End Function

    Public Function New_Mod(ByVal Value As Double, ByVal modulus As Double) As Double
        Dim N As Long
        N = Int(Value / modulus)
        New_Mod = Value - N * modulus
    End Function
End Module
