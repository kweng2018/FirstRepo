Option Explicit On

Public Class CN_PS
    Implements iPS
    'Private vdev As New VisaDevice
    Private Id As String = "COM"
    Private Outp As Boolean

    Private pCommPort As New System.IO.Ports.SerialPort
    Private pPortName As String
    Private pChan As String
    Private bytByte() As Byte
    Private Const StartStr As String = "05"
    Private Const FixStr As String = "46 46 57 57 30 44"
    Private Const BL As String = " "

    Public ReadOnly Property IdString() As String Implements iPS.IdString
        Get
            Return Id
        End Get
    End Property

    Private Property PortName() As String
        Get
            PortName = pPortName
        End Get
        Set(ByVal vNewValue As String)
            pPortName = vNewValue
        End Set
    End Property

    Private Property Channel() As String
        Get
            Channel = pChan
        End Get
        Set(ByVal value As String)
            Select Case value
                Case "00", "0"
                    pChan = "30 30"
                Case "01", "1"
                    pChan = "30 31"
                Case "02", "2"
                    pChan = "30 32"
                Case "03", "3"
                    pChan = "30 33"
                Case "04", "4"
                    pChan = "30 34"
                Case "05", "5"
                    pChan = "30 35"
                Case "06", "6"
                    pChan = "30 36"
                Case "07", "7"
                    pChan = "30 37"
                Case "08", "8"
                    pChan = "30 38"
                Case "09", "9"
                    pChan = "30 39"
                Case "10"
                    pChan = "30 40"
                Case "11"
                    pChan = "30 41"
                Case "12"
                    pChan = "30 42"
                Case "13"
                    pChan = "30 43"
                Case "14"
                    pChan = "30 44"
                Case "15"
                    pChan = "30 45"
                Case "16"
                    pChan = "30 46"
                Case Else
                    pChan = "30 30"
            End Select
        End Set
    End Property

    Public Sub Initialize(ByVal Address As String) Implements iPS.Initialize
        Try
            Dim tmpStr() As String
            tmpStr = Strings.Split(Address, ":")

            PortName = tmpStr(0)
            Channel = tmpStr(1)

            If Not pCommPort Is Nothing Then
                If pCommPort.IsOpen Then pCommPort.Close()
                pCommPort.PortName = PortName
                pCommPort.Parity = IO.Ports.Parity.None
                pCommPort.DataBits = 8
                pCommPort.BaudRate = 19200
                pCommPort.StopBits = IO.Ports.StopBits.One
                pCommPort.Encoding = System.Text.Encoding.ASCII
                pCommPort.Open()
                If pCommPort.IsOpen Then pCommPort.Close()
            End If
        Catch ex As Exception
            Throw New Exception("COM_PORT PS Error: " & ex.Message)
        End Try
    End Sub

    Public Property OutputOn() As Boolean Implements iPS.OutputOn
        Get
            Return Outp
        End Get
        Set(ByVal value As Boolean)
            'Dim tmp As String
            If value Then

            Else
                Setup(0, 0)
            End If
            Outp = value
        End Set
    End Property

    Public Function ReadCurrent() As Double Implements iPS.ReadCurrent
        'Dim tmp As String
        'tmp = Mid(vdev.viQuery("iout? "), 5)
        'Return CDbl(tmp)
    End Function

    Public Function ReadVoltage() As Double Implements iPS.ReadVoltage
        'Dim tmp As String
        'tmp = Mid(vdev.viQuery("vout? "), 5)
        'Return CDbl(tmp)
    End Function

    Public Sub Reset() Implements iPS.Reset
        'Call vdev.viQuery("*rst;*opc?")
    End Sub

    Public Sub Setup(ByVal Voltage As Double, ByVal CurrentLim As Double) Implements iPS.Setup
        Dim cmd As String
        If Voltage >= 20 And Voltage <= 32 Then
            cmd = cmdGet("VOLT1")
        ElseIf Voltage >= 40 And Voltage <= 52 Then
            cmd = cmdGet("VOLT2")
        Else
            cmd = cmdGet("OFF")
        End If
        ComWrite(cmd)
        System.Threading.Thread.Sleep(50)
    End Sub


    Private Sub ComWrite(ByVal cmd As String)
        Try
            Dim longth As Integer
            Err.Clear()
            longth = strHexToByteArray(cmd)

            If longth > 0 Then
                pCommPort.PortName = PortName
                If pCommPort.IsOpen Then pCommPort.Close()
                pCommPort.Parity = IO.Ports.Parity.None
                pCommPort.DataBits = 8
                pCommPort.BaudRate = 19200
                pCommPort.StopBits = IO.Ports.StopBits.One
                pCommPort.Encoding = System.Text.Encoding.ASCII
                pCommPort.Open()
                pCommPort.Write(bytByte, 0, bytByte.Length)
                If pCommPort.IsOpen Then pCommPort.Close()
            End If
        Catch ex As Exception
            If pCommPort.IsOpen Then pCommPort.Close()
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Function strHexToByteArray(ByVal strText As String) As Integer

        Dim HexData As Integer          '十六进制(二进制)数据字节对应值
        Dim hstr As String           '高位字符
        Dim lstr As String           '低位字符
        Dim HighHexData As Integer      '高位数值
        Dim LowHexData As Integer       '低位数值
        Dim HexDataLen As Integer       '字节数
        Dim StringLen As Integer        '字符串长度
        Dim Account As Integer          '计数

        'strTestn = ""                   '设初值
        HexDataLen = 0
        strHexToByteArray = 0

        StringLen = Len(Trim(strText))
        Account = StringLen \ 2
        ReDim bytByte(Account)
        Dim n As Integer
        For n = 1 To StringLen
            Do                                              '清除空格
                hstr = Mid(strText, n, 1)
                n = n + 1
                If (n - 1) > StringLen Then
                    HexDataLen = HexDataLen - 1

                    Exit For
                End If
            Loop While hstr = " "

            Do
                lstr = Mid(strText, n, 1)
                n = n + 1
                If (n - 1) > StringLen Then
                    HexDataLen = HexDataLen - 1
                    Exit For
                End If
            Loop While lstr = " "
            n = n - 1
            If n > StringLen Then
                HexDataLen = HexDataLen - 1
                Exit For
            End If

            HighHexData = ConvertHexChr(hstr)
            LowHexData = ConvertHexChr(lstr)

            If HighHexData = -1 Or LowHexData = -1 Then     '遇到非法字符中断转化
                HexDataLen = HexDataLen - 1
                Exit For
            Else
                HexData = HighHexData * 16 + LowHexData
                bytByte(HexDataLen) = HexData
                HexDataLen = HexDataLen + 1
            End If

        Next n

        If HexDataLen > 0 Then                              '修正最后一次循环改变的数值
            HexDataLen = HexDataLen - 1
            ReDim Preserve bytByte(HexDataLen)
        Else
            ReDim Preserve bytByte(0)
        End If


        If StringLen = 0 Then                               '如果是空串,则不会进入循环体
            strHexToByteArray = 0
        Else
            strHexToByteArray = HexDataLen + 1
        End If


    End Function

    Private Function ConvertHexChr(ByVal str As String) As Integer

        Dim test As Integer

        test = Asc(str)
        If test >= Asc("0") And test <= Asc("9") Then
            test = test - Asc("0")
        ElseIf test >= Asc("a") And test <= Asc("f") Then
            test = test - Asc("a") + 10
        ElseIf test >= Asc("A") And test <= Asc("F") Then
            test = test - Asc("A") + 10
        Else
            test = -1                                       '出错信息
        End If
        ConvertHexChr = test

    End Function

    Private Function cmdGet(ByVal PowerStatus As String) As String

        Select Case PowerStatus
            Case "REM"
                cmdGet = Channel & BL & FixStr & BL & "30 52 45 4D"
                cmdGet = GetBCC(cmdGet)
            Case "VOLT1"
                cmdGet = Channel & BL & FixStr & BL & "30 31 31 38"
                cmdGet = GetBCC(cmdGet)
            Case "VOLT2"
                cmdGet = Channel & BL & FixStr & BL & "30 31 45 30"
                cmdGet = GetBCC(cmdGet)
            Case "OFF"
                cmdGet = Channel & BL & FixStr & BL & "30 4F 46 46"
                cmdGet = GetBCC(cmdGet)
            Case "ADJ"
                cmdGet = Channel & BL & FixStr & BL & "30 41 44 4A"
                cmdGet = GetBCC(cmdGet)
            Case Else
                Throw New Exception("COM_PORT Power Supplier Status error!")
        End Select

    End Function

    'Private Function GetBCC(ByVal cmd As String) As String
    '    Dim ValueString() As String = Strings.Split(cmd, BL)
    '    Dim Count As Integer = 0
    '    For i As Integer = 0 To ValueString.Length - 1
    '        Count += CInt("&H" & ValueString(i))
    '    Next
    '    '05 30 30 46 46 57 57 30 44 30 31 45 30 E9
    '    Return cmd & BL & Right("00" & Hex(Count), 2)
    'End Function

    Private Function GetBCC(ByVal cmd As String) As String
        Dim ValueString() As String = Strings.Split(cmd, BL)
        Dim Count As Integer = 0
        For i As Integer = 0 To ValueString.Length - 1
            Count += CInt("&H" & ValueString(i))
        Next
        Dim tmp As String = Right("00" & Hex(Count), 2)
        Return StartStr & BL & cmd & BL & Asc2Hex(Left(tmp, 1)) & BL & Asc2Hex(Right(tmp, 1))
    End Function

    Private Function Asc2Hex(ByVal MyAscii As String) As String
        If Len(MyAscii) > 1 Then Call Err.Raise(vbObjectError + 1, "Ascii2Hex", "Invalid Ascii conversion")
        If MyAscii = "0" Then
            Return "30"
        ElseIf MyAscii = "1" Then
            Return "31"
        ElseIf MyAscii = "2" Then
            Return "32"
        ElseIf MyAscii = "3" Then
            Return "33"
        ElseIf MyAscii = "4" Then
            Return "34"
        ElseIf MyAscii = "5" Then
            Return "35"
        ElseIf MyAscii = "6" Then
            Return "36"
        ElseIf MyAscii = "7" Then
            Return "37"
        ElseIf MyAscii = "8" Then
            Return "38"
        ElseIf MyAscii = "9" Then
            Return "39"
        ElseIf MyAscii = "A" Then
            Return "41"
        ElseIf MyAscii = "B" Then
            Return "42"
        ElseIf MyAscii = "C" Then
            Return "43"
        ElseIf MyAscii = "D" Then
            Return "44"
        ElseIf MyAscii = "E" Then
            Return "45"
        ElseIf MyAscii = "F" Then
            Return "46"
        End If
    End Function
End Class



