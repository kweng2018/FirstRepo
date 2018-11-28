Public Class Switch_RS422
    Private pCommPort As New System.IO.Ports.SerialPort
    Private pPortNumber As Integer
    Private bytByte() As Byte
    Private Const StartStr As String = "05"
    Private Const FixString As String = "30 30 46 46 57 57 30 44 30 "
    Private Const BL As String = " "

    Public Property PortNumber() As Integer
        Get
            PortNumber = pPortNumber
        End Get
        Set(ByVal vNewValue As Integer)
            pPortNumber = vNewValue
        End Set
    End Property

    Public Sub ComWrite(ByVal cmd As String)
        Dim longth As Integer
        'On Error GoTo ComWriteError
        Err.Clear()
        '------------------------------
        longth = strHexToByteArray(cmd)

        If longth > 0 Then
            'pPortNumber = Val(USBIO24Comm)
            'If Not pCommPort Is Nothing Then
            pCommPort.PortName = "COM" & CStr(pPortNumber)
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
        Exit Sub
ComWriteError:
        If pCommPort.IsOpen Then pCommPort.Close()
        Exit Sub

    End Sub


    Public Function CheckComm() As Boolean
        On Error GoTo ErrHandler
        If Not pCommPort Is Nothing Then
            If pCommPort.IsOpen Then pCommPort.Close()
            pCommPort.Parity = IO.Ports.Parity.None
            pCommPort.DataBits = 8
            pCommPort.BaudRate = 19200
            pCommPort.StopBits = IO.Ports.StopBits.One
            pCommPort.PortName = "COM" & CStr(pPortNumber)
            pCommPort.Encoding = System.Text.Encoding.ASCII
            pCommPort.Open()

            CheckComm = True
            If pCommPort.IsOpen Then pCommPort.Close()
            Exit Function
        End If
ErrHandler:
        If pCommPort.IsOpen Then pCommPort.Close()
        CheckComm = False
    End Function


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


    Public Sub TurnOnOff(ByVal Channel As Integer, ByVal OnOff As Boolean)
        Dim cmd As String
        cmd = cmdGet(Channel, OnOff)
        Call ComWrite(cmd)
        System.Threading.Thread.Sleep(50)
    End Sub

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

    Private Function cmdGet(ByVal Chan As Integer, ByVal PowerStatus As Boolean) As String
        'PowerStatus = UCase(PowerStatus)


        '30 30 46 46 57 57 30 44 30 31     30          31
        '<---------FIX------------> Ch   <-FIX->     On/Off
        'Ch0 ~ Ch8:  30 ~ 39
        'Ch9 ~ Ch12: 41 ~ 43
        'On/Off:  On:31  Off:30
        Select Case Chan
            Case 0
                'Channel2Port = "00"
                If PowerStatus Then
                    cmdGet = FixString & "31 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "31 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 1
                If PowerStatus Then
                    cmdGet = FixString & "32 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "32 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 2
                If PowerStatus Then
                    cmdGet = FixString & "33 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "33 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 3
                If PowerStatus Then
                    cmdGet = FixString & "34 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "34 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 4
                If PowerStatus Then
                    cmdGet = FixString & "35 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "35 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 5
                If PowerStatus Then
                    cmdGet = FixString & "36 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "36 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 6
                If PowerStatus Then
                    cmdGet = FixString & "37 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "37 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 7
                If PowerStatus Then
                    cmdGet = FixString & "38 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "38 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 8
                If PowerStatus Then
                    cmdGet = FixString & "39 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "39 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 9
                If PowerStatus Then
                    cmdGet = FixString & "41 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "41 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 10
                If PowerStatus Then
                    cmdGet = FixString & "42 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "42 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 11
                If PowerStatus Then
                    cmdGet = FixString & "43 30 31"
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = FixString & "43 30 30"
                    cmdGet = GetBCC(cmdGet)
                End If
            Case Else
                Call Err.Raise(1001, "Switch_RS422.Index2PortBit", "Invalid Argument: " & Chan)
                cmdGet = "Error"
        End Select
    End Function
End Class
