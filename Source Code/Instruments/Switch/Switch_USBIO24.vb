Option Explicit On
Option Strict Off


Public Class Switch_USBIO24
    Private pComm As New IO.Ports.SerialPort

    Public Function Initialize(ByVal PortNum As Integer) As Boolean
        If pComm.IsOpen Then pComm.Close()
        Initialize = True
        Try
            pComm.PortName = "COM" & CStr(PortNum)
            pComm.Encoding = System.Text.Encoding.ASCII
            Call pComm.Open()
        Catch ex As Exception
            Initialize = False
            AddMessage("COM Port Error: " & ex.ToString)
        End Try
    End Function

    Public Sub SetIoDirection(ByVal Bank As String, ByVal ByteVal As Byte)
        Dim tmp(2) As Byte
        tmp(0) = CByte(Asc("!"))
        tmp(1) = CByte(Asc(Bank.ToUpper))
        tmp(2) = CByte(ByteVal)
        Call Write(tmp)
    End Sub

    Public Sub SetOutput(ByVal Bank As String, ByVal ByteVal As Byte)
        Dim tmp(1) As Byte
        tmp(0) = CByte(Asc(Bank.ToUpper))
        tmp(1) = CByte(ByteVal)
        Call Write(tmp)
        'tmp(1) = CByte(1)

    End Sub

    Public Function GetInput(ByVal Bank As String) As Byte
        Dim tmp(0) As Byte, result() As Byte
        'Dim t As New MyTimer
        tmp(0) = CByte(Asc(Bank.ToLower))

        Call pComm.DiscardInBuffer()
        Call Write(tmp)
        result = Read(1)
        Return result(0)
    End Function

    Public Function IdString() As String
        Dim tmp(0) As Byte, result() As Byte, N As Integer
        tmp(0) = CByte(Asc("?"))
        Call pComm.DiscardInBuffer()

        Call Write(tmp)
        ' Call MyTimer.Delay(500)
        N = pComm.BytesToRead()
        If N > 0 Then
            ReDim result(N - 1)
            Call pComm.Read(result, 0, N)
            'Return Bytes2String(result)
        End If
        Return ("")
    End Function

    Private Sub Write(ByVal b() As Byte)
        Call pComm.Write(b, 0, b.Length)
        'Call pComm.Write(b, 0, 3)
    End Sub

    Private Function Read(ByVal NumBytes As Integer, Optional ByVal Timeout As Double = 0.5) As Byte()
        Dim tmp(0) As Byte, result(NumBytes - 1) As Byte
        'Dim t As New MyTimer

        't.Duration = Timeout
        ' Call t.StartTimer()
        Do
            If pComm.BytesToRead >= NumBytes Then
                Call pComm.Read(result, 0, NumBytes)
                Exit Do
            End If
            'If t.IsExpired Then
            '    Throw New Exception("Timeout reading from USB IO24R")
            'End If
            'Call MyTimer.Delay(50)
        Loop
        Return result
    End Function


End Class
