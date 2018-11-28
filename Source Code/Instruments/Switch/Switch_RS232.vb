
Option Explicit On

Public Class Switch_RS232
    Private pCommPort As New System.IO.Ports.SerialPort
    Private pPortNumber As Integer
    Private bytByte() As Byte
    Private Const StartStr As String = "22"
    Private Const EndStr As String = "33"
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
        Return cmd & BL & Right("00" & Hex(Count), 2)
    End Function

    Private Function cmdGet(ByVal Chan As Integer, ByVal PowerStatus As Boolean) As String
        'PowerStatus = UCase(PowerStatus)
        Select Case Chan
            Case 0
                'Channel2Port = "00"
                If PowerStatus Then
                    cmdGet = StartStr & BL & "00" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "00" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 1
                If PowerStatus Then
                    cmdGet = StartStr & BL & "01" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "01" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 2
                If PowerStatus Then
                    cmdGet = StartStr & BL & "02" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "02" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 3
                If PowerStatus Then
                    cmdGet = StartStr & BL & "03" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "03" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 4
                If PowerStatus Then
                    cmdGet = StartStr & BL & "04" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "04" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 5
                If PowerStatus Then
                    cmdGet = StartStr & BL & "05" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "05" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 6
                If PowerStatus Then
                    cmdGet = StartStr & BL & "06" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "06" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 7
                If PowerStatus Then
                    cmdGet = StartStr & BL & "07" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "07" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 8
                If PowerStatus Then
                    cmdGet = StartStr & BL & "08" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "08" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 9
                If PowerStatus Then
                    cmdGet = StartStr & BL & "09" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "09" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 10
                If PowerStatus Then
                    cmdGet = StartStr & BL & "0A" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "0A" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 11
                If PowerStatus Then
                    cmdGet = StartStr & BL & "0B" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "0B" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 12
                If PowerStatus Then
                    cmdGet = StartStr & BL & "0C" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "0C" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 13
                If PowerStatus Then
                    cmdGet = StartStr & BL & "0D" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "0D" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 14
                If PowerStatus Then
                    cmdGet = StartStr & BL & "0E" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "0E" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 15
                If PowerStatus Then
                    cmdGet = StartStr & BL & "0F" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "0F" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 16
                If PowerStatus Then
                    cmdGet = StartStr & BL & "10" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "10" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 17
                If PowerStatus Then
                    cmdGet = StartStr & BL & "11" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "11" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 18
                If PowerStatus Then
                    cmdGet = StartStr & BL & "12" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "12" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If
            Case 19
                If PowerStatus Then
                    cmdGet = StartStr & BL & "13" & BL & "01" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                Else
                    cmdGet = StartStr & BL & "13" & BL & "00" & BL & EndStr
                    cmdGet = GetBCC(cmdGet)
                End If

            Case Else
                Call Err.Raise(1001, "Switch_RS232.Index2PortBit", "Invalid Argument: " & Chan)
                cmdGet = StartStr & BL & "13" & BL & "00" & BL & EndStr
                cmdGet = GetBCC(cmdGet)
        End Select
    End Function
End Class

