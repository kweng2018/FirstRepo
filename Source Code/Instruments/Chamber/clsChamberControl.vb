Imports VB6 = Microsoft.VisualBasic
Public Class clsGetResp
    Public sTX As String
    Function resp() As String
        resp = RS232_TX_RX(sTX)
    End Function
End Class

Public Class clsChamberCTRL


#Region "FP93 Parameters"

#Region "Error Code"
    Public Code_00 As String
    Public Code_01 As String
    Public Code_07 As String
    Public Code_08 As String
    Public Code_09 As String
    Public Code_0A As String
    Public Code_0B As String
    Public Code_0C As String
#End Region

#Region "BasicParam"
    Public STX As Integer = 2
    Public ETX As Integer = 3
    Public MacAdd As String = "011"
    Public cmdRead As String = "R"
    Public cmdWrite As String = "W"
    Public ControllerType As String
#End Region

#Region "Global Parameters"

    Public DPosition As Double = 0.1
    Public myUnit As String
    Public MeasRange As String
    Public SC_L As String
    Public SC_H As String
    Public SV_Low_Limit As String
    Public SV_High_Limit As String

    Public Structure PID_Settings
        Public Proportional_Band As String
        Public Integral_Time As String
        Public Derivative_Time As String
        Public Manual_Reset As String
        Public Hysteresis As String
        Public LL_Output_Limiter As String
        Public HL_Output_Limiter As String
        Public Target_Value_Function As String
    End Structure
    Public PIDx(5) As PID_Settings

    Public Zone_1_SP As String
    Public Zone_2_SP As String
    Public Zone_3_SP As String
    Public Zone_Hysteresis As String
    Public Zone_PID As String

    Public Structure EV_Setting
        Public MD As String
        Public SP As String
        Public DF As String
        Public STB As String
    End Structure
    Public EVx(2) As EV_Setting

    Public DI2 As String
    Public DI3 As String
    Public DI4 As String
    'Public AO1_MD As String
    'Public AO1_L As String
    'Public AO1_H As String
    'Public COM_MEM As String
    ' Public ACTMD As String
    Public Proportional_Cycle As String
    Public KLock As String
    Public PV_Bias As String
    Public PV_Filter As String
    Public PRG_MD As String
    Public Start_Pattern As String
    Public Pattern_No As String
    Public Time_Mode As String
    Public SHT_Mode As String
    Public InputAbMode As String
    Public AutoTune As String

    Public COMMode As String

    Public OutputMode As String
    Public OutputRange As String

    Public Fix_SV As String
    Public Fix_PID As String
#End Region

#Region "Pattern Parameters"
    Public Structure Pattern_Param
        Public Step_Num As String
        Public Repeat_Num As String
        Public Start_SV As String
        Public GUA_Zone As String
        Public Start_PV As String
        Public EV1 As String
        Public EV2 As String
        Public EV3 As String
        Public Time_Signal_1_ON_Step As String
        Public Time_Signal_1_OFF_Step As String
        Public Time_Signal_1_ON_Time As String
        Public Time_Signal_1_OFF_Time As String
        Public Time_Signal_2_ON_Step As String
        Public Time_Signal_2_OFF_Step As String
        Public Time_Signal_2_ON_Time As String
        Public Time_Signal_2_OFF_Time As String

        Structure Step_Setting
            Public SV As String
            Public Time As String
            Public PID As String
        End Structure

        Public Step1 As Step_Setting
        Public Step2 As Step_Setting
        Public Step3 As Step_Setting
        Public Step4 As Step_Setting
        Public Step5 As Step_Setting
        Public Step6 As Step_Setting
        Public Step7 As Step_Setting
        Public Step8 As Step_Setting
        Public Step9 As Step_Setting
        Public Step10 As Step_Setting
    End Structure

    Public PatternX(3) As Pattern_Param

    Public Event_DO_None As String
    Public Event_DO_HLDeviation As String
    Public Event_DO_LLDeviation As String
    Public Event_DO_Outside_HL_LDeviation As String
    Public Event_DO_Within_HL_LDeviation As String
    Public Event_DO_HL_Absolute_Value As String
    Public Event_DO_LL_Absolute_Value As String
    Public Event_DO_Scaleover As String
    Public Event_DO_Hold As String
    Public Event_DO_Guarantee_Soak As String
    Public Event_DO_Time_Signal_1 As String
    Public Event_DO_Time_Signal_2 As String
    Public Event_DO_RUN_Status As String
    Public Event_DO_Step_Signal As String
    Public Event_DO_End_Signal As String
    Public Event_DO_Fix As String

    Public DI_None As String
    Public DI_Hold As String
    Public DI_Advance As String
    Public DI_Fix_Level As String
    Public DI_Start_Pattern_No_2_Bits As String
    Public DI_Start_Pattern_No_3_Bits As String
#End Region

#End Region



    Private Const RespLength As Integer = 4
    Private Const Step_Item_SV As Integer = 1
    Private Const Step_Item_TM As Integer = 2

#Region "Common Function"

    Private Function Get_BCC(ByVal cmd As String) As String
        Dim BCC_DEC As Integer = 0, BCC_HEX As String = String.Empty

        For i As Integer = 1 To Len(cmd)
            BCC_DEC = BCC_DEC + Asc(Mid(cmd, i, 1))
        Next
        BCC_HEX = Hex(BCC_DEC)
        Get_BCC = Right("00" & BCC_HEX, 2)
    End Function

    Public Function Ini_CMD(ByVal MacAdd As String, ByVal exestr As String, ByVal addstr As String, ByVal datalength As String, Optional ByVal datacontent As String = "") As String
        Ini_CMD = String.Empty
        If exestr = cmdWrite Then
            Ini_CMD = Chr(STX) & MacAdd & exestr & addstr & datalength & "," & datacontent & Chr(ETX)
        ElseIf exestr = cmdRead Then
            Ini_CMD = Chr(STX) & MacAdd & exestr & addstr & datalength & Chr(ETX)
        End If
        Ini_CMD = Ini_CMD & Get_BCC(Ini_CMD) & ControlChars.Cr
    End Function

    Public Function Get_Resp(ByVal Full_Resp As String) As String
        Dim status As String = Get_RespStatus(Full_Resp)
        If status = OK Then
            Dim StartPosition As Integer, EndPosition As Integer
            StartPosition = InStr(Full_Resp, ",") + 1
            EndPosition = InStr(Full_Resp, Chr(ETX))
            If EndPosition > StartPosition Then
                Get_Resp = Mid(Full_Resp, StartPosition, EndPosition - StartPosition)
            Else
                Return "Data length error."
            End If
        Else
            Return status
        End If
    End Function

    Public Function SplitMultiResp(ByVal Multi_Resp As String) As String()
        Dim i As Integer
        Dim respArr() As String = Nothing
        If Multi_Resp.Length Mod 4 = 0 Then
            i = CInt(Multi_Resp.Length / 4)
            If i > 0 Then
                ReDim respArr(i - 1)
                For index As Integer = 0 To i - 1
                    respArr(index) = Mid(Multi_Resp, index * RespLength + 1, RespLength)
                Next
            End If
        End If
        Return respArr
    End Function

    Public Function Get_RespStatus(ByVal Full_Resp As String) As String
        Try
            Select Case Mid(Full_Resp, 6, 2)
                Case "00"
                    Return "OK"
                Case "01"
                    Return Code_01
                Case "07"
                    Return Code_07
                Case "08"
                    Return Code_08
                Case "09"
                    Return Code_09
                Case "0A"
                    Return Code_0A
                Case "0B"
                    Return Code_0B
                Case "0C"
                    Return Code_0C
                Case Else
                    Return Full_Resp
            End Select
        Catch ex As Exception
            Return Full_Resp
        End Try
    End Function

    Private Function Hex2Bin(ByVal Resp_1 As String) As String
        Select Case Resp_1
            Case "0"
                Hex2Bin = "0000"
            Case "1"
                Hex2Bin = "0001"
            Case "2"
                Hex2Bin = "0010"
            Case "3"
                Hex2Bin = "0011"
            Case "4"
                Hex2Bin = "0100"
            Case "5"
                Hex2Bin = "0101"
            Case "6"
                Hex2Bin = "0110"
            Case "7"
                Hex2Bin = "0111"
            Case "8"
                Hex2Bin = "1000"
            Case "9"
                Hex2Bin = "1001"
            Case "a", "A"
                Hex2Bin = "1010"
            Case "b", "B"
                Hex2Bin = "1011"
            Case "c", "C"
                Hex2Bin = "1100"
            Case "d", "D"
                Hex2Bin = "1101"
            Case "e", "E"
                Hex2Bin = "1111"
            Case Else
                Hex2Bin = "Error"
        End Select
    End Function

    Private Function Bin2Hex(ByVal Bin_String As String) As String
        Dim TempSum As Integer = 0
        For i As Integer = 1 To Bin_String.Length
            If CInt(Mid(Bin_String, i, 1)) = 1 Then TempSum = TempSum + CInt(2 ^ (Bin_String.Length - i))
        Next
        Bin2Hex = Hex(TempSum)
    End Function

    Private Function Dec2Hex(ByVal eValue As Integer) As String
        Dec2Hex = Right("0000" & Hex(eValue), RespLength)
    End Function

    Public Function Resp2Double(ByVal resp_4 As String) As String
        Try
            Return VB6.Format(CDbl("&H" & resp_4), "0.0")
        Catch ex As Exception
            Return resp_4
        End Try
    End Function

    Public Function Resp2Double_10(ByVal resp_4 As String) As String
        Try
            Return VB6.Format(CDbl("&H" & resp_4) * DPosition, "0.0")
        Catch ex As Exception
            Return resp_4
        End Try
    End Function

    Public Function Resp2Integer(ByVal resp_4 As String) As String
        Try
            Return CInt("&H" & resp_4).ToString
        Catch ex As Exception
            Return resp_4
        End Try
    End Function

    Public Function Resp2Time(ByVal resp_4 As String) As String
        Try
            Return CInt(Left(resp_4, 2)).ToString & ":" & VB6.Format(CInt(Right(resp_4, 2)), "00")
        Catch ex As Exception
            Return resp_4
        End Try
    End Function

    Public Function Resp2Integer_DEC(ByVal resp_4 As String) As String
        Try
            Return CInt(resp_4).ToString
        Catch ex As Exception
            Return resp_4
        End Try
    End Function

    Public Function Resp2Boolean(ByVal resp_4 As String) As String
        Try
            Return CBool(Resp2Integer(resp_4)).ToString
        Catch ex As Exception
            Return resp_4
        End Try
    End Function

    Public Function Resp2Bin(ByVal resp_4 As String) As String
        Try
            Resp2Bin = ""
            For i As Integer = 1 To resp_4.Length
                Resp2Bin = Resp2Bin & Hex2Bin(Mid(resp_4, i, 1))
            Next
        Catch ex As Exception
            Return resp_4
        End Try
    End Function

    Public Function Dbl2Write(ByVal eValue As Double) As String
        Return Right("0000" & Hex(eValue), RespLength)
    End Function

    Public Function Dbl2Write_10(ByVal eValue As Double) As String
        Return Right("0000" & Hex(eValue / DPosition), RespLength)
    End Function

    Public Function Int2Write(ByVal eValue As Integer) As String
        Return Right("0000" & Hex(eValue), RespLength)
    End Function

    Public Function Time2Write(ByVal eValue As String) As String
        Return Right("00" & Strings.Split(eValue, ":")(0), 2) & Right("00" & Strings.Split(eValue, ":")(1), 2)
    End Function

    Public Function Bool2Write(ByVal eValue As Boolean) As String
        If eValue = False Then
            Bool2Write = "0000"
        Else
            Bool2Write = "0001"
        End If
    End Function

    Public Function String2Write(ByVal eValue As String) As String
        String2Write = Right("0000" & eValue, RespLength)
    End Function

    Public Function ReadData(ByVal Address As String, ByVal Length As Integer) As String
        Dim resp As String
        Dim myData As New clsGetResp
        myData.sTX = Ini_CMD(MacAdd, cmdRead, Address, Length.ToString) & ControlChars.Cr
        resp = Strings.Split(myData.resp, SplitChar)(1)
        resp = Get_Resp(resp)
        Return resp
    End Function

    Public Overloads Function WriteData(ByVal Address As String, ByVal eValue As Double) As String
        Dim resp As String
        Dim myData As New clsGetResp
        myData.sTX = Ini_CMD(MacAdd, cmdWrite, Address, "0", Dbl2Write_10(eValue)) & ControlChars.Cr
        resp = Strings.Split(myData.resp, SplitChar)(1)
        Return Get_RespStatus(resp)
    End Function

    Public Overloads Function WriteData(ByVal Address As String, ByVal eValue As Integer) As String
        Dim resp As String
        Dim myData As New clsGetResp
        myData.sTX = Ini_CMD(MacAdd, cmdWrite, Address, "0", Int2Write(eValue)) & ControlChars.Cr
        resp = Strings.Split(myData.resp, SplitChar)(1)
        Return Get_RespStatus(resp)
    End Function

    Public Overloads Function WriteData(ByVal Address As String, ByVal eValue As Boolean) As String
        Dim resp As String
        Dim myData As New clsGetResp
        myData.sTX = Ini_CMD(MacAdd, cmdWrite, Address, "0", Bool2Write(eValue)) & ControlChars.Cr
        resp = Strings.Split(myData.resp, SplitChar)(1)
        Return Get_RespStatus(resp)
    End Function

    Public Overloads Function WriteData(ByVal Address As String, ByVal eValue As String) As String
        Dim resp As String
        Dim myData As New clsGetResp
        myData.sTX = Ini_CMD(MacAdd, cmdWrite, Address, "0", String2Write(eValue)) & ControlChars.Cr
        resp = Strings.Split(myData.resp, SplitChar)(1)
        Return Get_RespStatus(resp)
    End Function

#End Region

#Region "Response Result Functions"

    Public Function GetControllerType(ByVal MultiResp As String) As String
        Dim respArr As String() = Nothing, Controller As String = String.Empty
        respArr = SplitMultiResp(MultiResp)
        If respArr IsNot Nothing Then
            For i As Integer = 0 To respArr.Length - 1
                Dim tmp As Integer
                tmp = CInt("&H" & Mid(respArr(i), 1, 2))
                If tmp > 0 Then Controller = Controller & Chr(tmp)
                tmp = CInt("&H" & Mid(respArr(i), 3, 2))
                If tmp > 0 Then Controller = Controller & Chr(tmp)
            Next

            If UCase(Controller) = "FP93" Then
                Return Controller
            Else
                Return "Type Error."
            End If
        Else
            Return MultiResp
        End If
    End Function

    Public Function Get_Time_Format(ByVal resp_4 As String) As String
        If resp_4.Length = 4 Then
            Return (Mid(resp_4, 1, 2) & ":" & Mid(resp_4, 3, 2))
        Else
            Return "Response format error."
        End If
    End Function

    Public Function Set_Time_Format(ByVal resp_4 As String) As String
        If InStr(resp_4, ":") > 0 Then
            Dim datastr() As String = Strings.Split(resp_4, ":")
            Return (Right("00" & datastr(0), 2) & Right("00" & datastr(1), 2))
        Else
            Return "0000."
        End If
    End Function

    Public Overloads Function Get_Temp_Format(ByVal resp_4 As String) As String
        If resp_4.Length = 4 Then
            Return Resp2Double_10(resp_4)
        Else
            Return "Response format error."
        End If
    End Function

    Public Function Set_Temp_Format(ByVal eValue As Double) As String
        Return Dbl2Write_10(eValue)
    End Function

    Public Function GetPoint(ByVal resp_4 As String) As String
        Select Case Resp2Integer(resp_4)
            Case "0"
                Return "1.0"
            Case "1"
                Return "0.1"
            Case "2"
                Return "0.01"
            Case "3"
                Return "0.001"
            Case Else
                Return "0.1"
        End Select
    End Function

#End Region

#Region "Set Read-Only Commands"
    '读取控制器序列代码
    Public ReadOnly Property Read_FP93Name() As String
        Get
            Return GetControllerType(ReadData("0040", 3))
        End Get
    End Property
    'Public Function cmdReadControllerType() As String
    '    cmdReadControllerType = Ini_CMD(MacAdd, cmdRead, "0040", "3") & ControlChars.Cr
    'End Function

    '读取温度测量值
    '一次性读取测量值，设定值，控制输出值，执行标志，事件输出标志
    Public ReadOnly Property Read_PV_SV_OP_EF_EOF() As String()
        Get
            Dim binstring As String
            Dim resp As String = ReadData("0100", 5)
            Dim arr As String() = SplitMultiResp(resp)
            If arr IsNot Nothing Then
                arr(0) = Get_Temp_Format(arr(0))
                arr(1) = Get_Temp_Format(arr(1))
                arr(2) = Get_Temp_Format(arr(2))

                arr(3) = String.Empty

                binstring = String.Empty
                For i As Integer = 1 To arr(4).Length
                    binstring = binstring & Hex2Bin(Mid(arr(4), i, 1))
                Next
                arr(4) = binstring

                binstring = String.Empty
                For i As Integer = 1 To arr(5).Length
                    binstring = binstring & Hex2Bin(Mid(arr(5), i, 1))
                Next
                arr(5) = binstring
            End If
            Return arr
        End Get
    End Property


    Public ReadOnly Property Read_Meas_Value() As String
        Get
            Return Get_Temp_Format(ReadData("0100", 0))
        End Get
    End Property

    'Public Function cmdReadMeasTemp() As String
    '    cmdReadMeasTemp = Ini_CMD(MacAdd, cmdRead, "0100", "0") & ControlChars.Cr
    'End Function

    '读取当前执行的设定值
    Public ReadOnly Property Read_SV_Value_In_Execution() As String
        Get
            Return Get_Temp_Format(ReadData("0101", 0))
        End Get
    End Property

    'Public Function cmdReadExeValue() As String
    '    cmdReadExeValue = Ini_CMD(MacAdd, cmdRead, "0101", "0") & ControlChars.Cr
    'End Function

    '读取控制输出的值
    Public ReadOnly Property Read_Control_Output_Value() As String
        Get
            Return Get_Temp_Format(ReadData("0102", 0))
        End Get
    End Property

    'Public Function cmdReadOutValue() As String
    '    cmdReadOutValue = Ini_CMD(MacAdd, cmdRead, "0102", "0") & ControlChars.Cr
    'End Function

    '读取执行标志（不执行时=0）
    Public ReadOnly Property Read_Action_Flag() As String
        Get
            Dim binstring As String = String.Empty
            Dim resp As String = ReadData("0104", 0)
            If resp.Length = 4 Then
                For i As Integer = 1 To resp.Length
                    binstring = binstring & Hex2Bin(Mid(resp, i, 1))
                Next
            Else
                binstring = resp
            End If
            Return binstring
        End Get
    End Property

    'Public Function cmdReadExeFLG() As String
    '    cmdReadExeFLG = Ini_CMD(MacAdd, cmdRead, "0104", "0") & ControlChars.Cr
    'End Function

    '读取事件输出标志（无事件输出时=0000）
    Public ReadOnly Property Read_Evnt_DO_Output_Flag() As String
        Get
            Dim binstring As String = String.Empty
            Dim resp As String = ReadData("0105", 0)
            If resp.Length = 4 Then
                For i As Integer = 1 To resp.Length
                    binstring = binstring & Hex2Bin(Mid(resp, i, 1))
                Next
            Else
                binstring = resp
            End If
            Return binstring
        End Get
    End Property

    'Public Function cmdReadEvFLG() As String
    '    cmdReadEvFLG = Ini_CMD(MacAdd, cmdRead, "0105", "0") & ControlChars.Cr
    'End Function

    '读取当前执行的PID号
    Public ReadOnly Property Read_PID_In_Execution() As String
        Get
            Return Resp2Integer(ReadData("0107", 0))
        End Get
    End Property

    'Public Function cmdReadExePID() As String
    '    cmdReadExePID = Ini_CMD(MacAdd, cmdRead, "0107", "0") & ControlChars.Cr
    'End Function

    '    EXE_FLG和EV_FLG的详细说明如下：
    '                 D15  D14  D13  D12  D11 D10  D9   D8    D7   D6   D5   D4   D3   D2   D1      D0
    'EXE_FLG     0      0      0      0      0      0      0   COM   0     0      0     0      0     0    MAN   AT  
    'EV_FLG       0      0      0      0      0      0      0     0       0     0      0     0      0     0    EV2    EV1
    '上限超量程时，EV_FLG的将被赋值为7FFFH
    '下限超量程时，EV_FLG的将被赋值为7FFFH


    '读取DI开关状态标志位
    Public ReadOnly Property Read_DI_Input_State_Flag() As String
        Get
            Dim binstring As String = String.Empty
            Dim resp As String = ReadData("010B", 0)
            If resp.Length = 4 Then
                For i As Integer = 1 To resp.Length
                    binstring = binstring & Hex2Bin(Mid(resp, i, 1))
                Next
            Else
                binstring = resp
            End If
            Return binstring
        End Get
    End Property

    'Public Function cmdReadDIFLG() As String
    '    cmdReadDIFLG = Ini_CMD(MacAdd, cmdRead, "010B", "0") & ControlChars.Cr
    'End Function

    '    DI_FLG的详细说明如下：
    '               D15  D14  D13  D12  D11 D10  D9   D8    D7   D6   D5   D4   D3    D2    D1    D0
    'DI_FLG     0       0      0      0       0     0      0   COM   0      0     0     0    DI4  DI3   DI2  DI1


    '读取单位  0=℃   1=℉
    Public ReadOnly Property Read_Unit_Of_Input() As String
        Get
            Dim resp As String = ReadData("0110", 0)
            If Resp2Integer(resp) = "1" Then
                Return "℉"
            Else
                Return "℃"
            End If
        End Get
    End Property

    'Public Function cmdReadUnit() As String
    '    cmdReadUnit = Ini_CMD(MacAdd, cmdRead, "0110", "0") & ControlChars.Cr
    'End Function

    '一次性读取测量范围，小数点位置，测量范围下限、上限
    Public ReadOnly Property Read_MeasRange_DP_SC_L_H() As String()
        Get
            Dim resp As String = ReadData("0111", 4)
            Dim arr As String() = SplitMultiResp(resp)
            If arr IsNot Nothing Then
                arr(0) = Get_Temp_Format(arr(0))
                arr(1) = String.Empty
                arr(2) = GetPoint(arr(2))
                arr(3) = Get_Temp_Format(arr(3))
                arr(4) = Get_Temp_Format(arr(4))
            End If
            Return arr
        End Get
    End Property

    '读取测量范围（见测量范围代码表）
    Public ReadOnly Property Read_Meas_Range_Code() As String
        Get
            Return Resp2Integer_DEC(ReadData("0111", 0))
        End Get
    End Property

    'Public Function cmdReadRange() As String
    '    cmdReadRange = Ini_CMD(MacAdd, cmdRead, "0111", "0") & ControlChars.Cr
    'End Function

    '读取小数点位置 0=无  1=0.1  2=0.01   3=0.001
    Public ReadOnly Property Read_DPoing() As String
        Get
            Return GetPoint(ReadData("0113", 0))
        End Get
    End Property

    'Public Function cmdReaPoint() As String
    '    cmdReaPoint = Ini_CMD(MacAdd, cmdRead, "0113", "0") & ControlChars.Cr
    'End Function

    '读取测量范围下限值 -1999~9989
    Public ReadOnly Property Read_SC_L() As String
        Get
            Return Get_Temp_Format(ReadData("0114", 0))
        End Get
    End Property

    'Public Function cmdReadMeasLowLimit() As String
    '    cmdReadMeasLowLimit = Ini_CMD(MacAdd, cmdRead, "0114", "0") & ControlChars.Cr
    'End Function

    '读取测量范围上限值 -1989~9999
    Public ReadOnly Property Read_SC_H() As String
        Get
            Return Get_Temp_Format(ReadData("0115", 0))
        End Get
    End Property

    'Public Function cmdReadMeasHighLimit() As String
    '    cmdReadMeasHighLimit = Ini_CMD(MacAdd, cmdRead, "0115", "0") & ControlChars.Cr
    'End Function

    '一次性读取程序执行标志，当前执行的曲线号，曲线重复次数，当前执行曲线的步，当前执行步的剩余时间，当前执行的PID号
    Public ReadOnly Property Read_EF_PTN_RPT_STP_RTM_PID() As String()
        Get
            Dim binstring As String
            Dim resp As String = ReadData("0120", 6)
            Dim arr As String() = SplitMultiResp(resp)

            If arr IsNot Nothing Then
                binstring = String.Empty
                For i As Integer = 1 To arr(0).Length
                    binstring = binstring & Hex2Bin(Mid(arr(0), i, 1))
                Next
                arr(0) = binstring

                arr(1) = Resp2Integer(arr(1))

                arr(2) = String.Empty
                arr(3) = Resp2Integer(arr(3))
                arr(4) = Resp2Integer(arr(4))
                arr(5) = Resp2Integer(arr(5))
                arr(6) = Resp2Integer(arr(6))
            End If
            Return arr
        End Get
    End Property


    '读取程序执行标志
    Public ReadOnly Property Read_Program_Action_Flag() As String
        Get
            Dim binstring As String = String.Empty
            Dim resp As String = ReadData("0120", 0)
            If resp.Length = 4 Then
                For i As Integer = 1 To resp.Length
                    binstring = binstring & Hex2Bin(Mid(resp, i, 1))
                Next
            Else
                binstring = resp
            End If
            Return binstring
        End Get
    End Property

    'Public Function cmdReadExePRG() As String
    '    cmdReadExePRG = Ini_CMD(MacAdd, cmdRead, "0120", "0") & ControlChars.Cr
    'End Function

    '读取当前执行的曲线号
    Public ReadOnly Property Read_Pattern_No_In_Execution() As String
        Get
            Return Resp2Integer(ReadData("0121", 0))
        End Get
    End Property

    'Public Function cmdReadExePTN() As String
    '    cmdReadExePTN = Ini_CMD(MacAdd, cmdRead, "0121", "0") & ControlChars.Cr
    'End Function

    '读取曲线重复次数
    Public ReadOnly Property Read_Pattern_RPT_Number_In_Execution() As String
        Get
            Return Resp2Integer(ReadData("0123", 0))
        End Get
    End Property

    'Public Function cmdReadExeRPT() As String
    '    cmdReadExeRPT = Ini_CMD(MacAdd, cmdRead, "0123", "0") & ControlChars.Cr
    'End Function

    '读取当前执行曲线的步
    Public ReadOnly Property Read_Pattern_Step_No_In_Execution() As String
        Get
            Return Resp2Integer(ReadData("0124", 0))
        End Get
    End Property

    'Public Function cmdReadExeSTP() As String
    '    cmdReadExeSTP = Ini_CMD(MacAdd, cmdRead, "0124", "0") & ControlChars.Cr
    'End Function

    '读取当前执行步的剩余时间Remaining time of step in execution
    Public ReadOnly Property Read_Remaining_Time_Of_Step_In_Execution() As String
        Get
            Return Get_Time_Format(ReadData("0125", 0))
        End Get
    End Property

    'Public Function cmdReadExeTIM() As String
    '    cmdReadExeTIM = Ini_CMD(MacAdd, cmdRead, "0125", "0") & ControlChars.Cr
    'End Function

    '读取当前执行的PID号
    Public ReadOnly Property Read_PID_No_In_Execution() As String
        Get
            Return Resp2Integer(ReadData("0126", 0))
        End Get
    End Property

    'Public Function cmdReadE_PID() As String
    '    cmdReadE_PID = Ini_CMD(MacAdd, cmdRead, "0126", "0") & ControlChars.Cr
    'End Function

    '    E_PRG的详细说明如下：
    '                D15   D14  D13  D12  D11  D10  D9   D8   D7   D6   D5   D4   D3    D2    D1    D0
    'E_PRG     PRG     0      0       0       0    UP   LVL  DW   0     0      0      0     0    GUA HLD  RUN  

    'PRG     1:程序状态   0：定值状态       GUA  1:确保平台    0：无确保平台
    'UP       1:程序状态   0：定值状态       HLD  1:程序保持    0：无程序保持
    'LVL      1:程序状态   0：定值状态       RUN  1:运行        0：无运行
    'DW      1:程序状态   0：定值状态       
    '程序复位时, E_PRG被赋值为7FFFH


#End Region

#Region "Set Write-Only Commands"
    '写入在手动方式下设置输出的值
    Public Function Write_Control_Output_Set_Value_In_Manual_Operation(ByVal eValue As String) As String
        Return WriteData("0182", CDbl(eValue))
    End Function

    'Public Function cmdWriteManualOutValue(ByVal eValue As Double) As String
    '    cmdWriteManualOutValue = Ini_CMD(MacAdd, cmdWrite, "0182", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '写入自整定             0=不执行，1=执行
    Public Function Write_AT(ByVal eValue As String) As String
        Return WriteData("0184", CBool(eValue))
        'cmdWriteAT = Ini_CMD(MacAdd, cmdWrite, "0184", "0", Bool2Write(eValue)) & ControlChars.Cr
    End Function

    '写入手动/自动               0=自动，  1=手动
    Public Function Write_Manual_Control(ByVal eValue As String) As String
        Return WriteData("0185", CBool(eValue))
        ' cmdWriteManualCTRL = Ini_CMD(MacAdd, cmdWrite, "0185", "0", Bool2Write(eValue)) & ControlChars.Cr
    End Function

    '写入通讯方式               0=本机，  1=通讯
    Public Function Write_Set_Local_COM(ByVal eValue As String) As String
        Return WriteData("018C", CBool(eValue))
        ' cmdWriteCOM = Ini_CMD(MacAdd, cmdWrite, "018C", "0", Bool2Write(eValue)) & ControlChars.Cr
    End Function

    '写入复位/运行          0=复位，    1=运行
    Public Function Write_RST_RUN(ByVal eValue As String) As String
        Return WriteData("0190", CBool(eValue))
        'cmdWriteRST = Ini_CMD(MacAdd, cmdWrite, "0190", "0", Bool2Write(eValue)) & ControlChars.Cr
    End Function

    '写入程序保持           0=释放保持，1=保持
    Public Function Write_Unhold_Hold(ByVal eValue As String) As String
        Return WriteData("0191", CBool(eValue))
        ' cmdWriteHLD = Ini_CMD(MacAdd, cmdWrite, "0191", "0", Bool2Write(eValue)) & ControlChars.Cr
    End Function

    '写入程序跳步           0=不执行，  1=跳步
    Public Function Write_ADV(ByVal eValue As String) As String
        Return WriteData("0192", CBool(eValue))
        'cmdWriteADV = Ini_CMD(MacAdd, cmdWrite, "0192", "0", Bool2Write(eValue)) & ControlChars.Cr
    End Function

    '写入定值方式的SV值
    Public Function Write_Set_Fix_Value(ByVal eValue As String) As String
        Return WriteData("0300", Set_Temp_Format(CDbl(eValue)))
        'cmdWriteSetFixValue = Ini_CMD(MacAdd, cmdWrite, "0300", "0", Dbl2Write(eValue / DPosition)) & ControlChars.Cr
    End Function

#End Region

#Region "Set Read-Write Commands"

#Region "Set Fix Mode Limit"
    'SV下限值
    Public Function ReadWrite_Set_SV_Low_Limit(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double_10(ReadData("030A", 0))
        Else
            Return WriteData("030A", Set_Temp_Format(CDbl(eValue)))
        End If
    End Function

    'Public Overloads Function cmdRWSetSVLowLimit(ByVal eValue As Double) As String
    '    cmdRWSetSVLowLimit = Ini_CMD(MacAdd, cmdWrite, "030A", "0", Dbl2Write(eValue / DPosition)) & ControlChars.Cr
    'End Function

    'SV上限值
    Public Function ReadWrite_Set_SV_High_Limit(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double_10(ReadData("030B", 0))
        Else
            Return WriteData("030B", Set_Temp_Format(CDbl(eValue)))
        End If
    End Function

    'Public Overloads Function cmdRWSetSVHighLimit() As String
    '    cmdRWSetSVHighLimit = Ini_CMD(MacAdd, cmdRead, "030B", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWSetSVHighLimit(ByVal eValue As Double) As String
    '    cmdRWSetSVHighLimit = Ini_CMD(MacAdd, cmdWrite, "030B", "0", Dbl2Write(eValue / DPosition)) & ControlChars.Cr
    'End Function
#End Region

#Region "Global Parameters"

#Region "Part x"
    'x=1,2,3,4,5,6

    'PBx	控制输出的比例带x	读/写
    Public Function ReadWrite_Control_Output_Proportional_Band_x(ByVal PartNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0400"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function


    'Public Overloads Function cmdRWPBx(ByVal PartNo As Integer) As String
    '    Dim StartAdd As String = "0400"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWPBx = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWPBx(ByVal PartNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0400"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWPBx = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'ITx	控制输出的积分时间x	读/写
    Public Function ReadWrite_Control_Output_Integral_Time_x(ByVal PartNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0401"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function


    'Public Overloads Function cmdRWITx(ByVal PartNo As Integer) As String
    '    Dim StartAdd As String = "0401"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWITx = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWITx(ByVal PartNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0401"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWITx = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'DTx	控制输出的微分时间x	读/写
    Public Function ReadWrite_Control_Output_Derivative_Time_x(ByVal PartNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0402"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWDTx(ByVal PartNo As Integer) As String
    '    Dim StartAdd As String = "0402"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWDTx = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWDTx(ByVal PartNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0402"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWDTx = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'MRx	人工补偿x	读/写
    Public Function ReadWrite_Manual_Reset_x(ByVal PartNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0403"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWMRx(ByVal PartNo As Integer) As String
    '    Dim StartAdd As String = "0403"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWMRx = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWMRx(ByVal PartNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0403"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWMRx = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'DFx	回差x	读/写
    Public Function ReadWrite_Hysteresis_x(ByVal PartNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0404"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWDFx(ByVal PartNo As Integer) As String
    '    Dim StartAdd As String = "0404"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWDFx = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWDFx(ByVal PartNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0404"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWDFx = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '01x_L	控制输出下限x	读/写
    Public Function ReadWrite_Control_Output_Low_Limit_x(ByVal PartNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0405"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRW01x_Low(ByVal PartNo As Integer) As String
    '    Dim StartAdd As String = "0405"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRW01x_Low = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRW01x_Low(ByVal PartNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0405"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRW01x_Low = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '01x_H	控制输出上限x	读/写
    Public Function ReadWrite_Control_Output_High_Limit_x(ByVal PartNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0406"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRW01x_High(ByVal PartNo As Integer) As String
    '    Dim StartAdd As String = "0406"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRW01x_High = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRW01x_High(ByVal PartNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0406"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRW01x_High = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'SFx	控制输出抗超调系数x	读/写
    Public Function ReadWrite_Control_Output_Target_Value_Function_x(ByVal PartNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0407"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWSFx(ByVal PartNo As Integer) As String
    '    Dim StartAdd As String = "0407"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWSFx = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWSFx(ByVal PartNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0407"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
    '    cmdRWSFx = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '一次性读取Part x的控制输出的比例带,控制输出的积分时间,控制输出的微分时间,人工补偿,回差,控制输出下限,控制输出上限,控制输出抗超调系数
    Public Function Read_PartX_PB_IT_DT_MR_DF_01x_L_H_SF(ByVal PartNo As Integer) As String()
        Dim resp As String
        Dim StartAdd As String = "0400"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (PartNo - 1))
        resp = ReadData(StartAdd, 7)
        Dim arr As String() = SplitMultiResp(resp)
        If arr IsNot Nothing Then
            For i As Integer = 0 To arr.Length - 1
                arr(i) = Resp2Double(arr(i))
            Next
        End If
        Return arr
    End Function

#End Region

#Region "Zone"
    '区域1
    Public Function ReadWrite_Zone_1_SP(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("04C0", 0))
        Else
            Return WriteData("04C0", CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWZSP1() As String
    '    cmdRWZSP1 = Ini_CMD(MacAdd, cmdRead, "04C0", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWZSP1(ByVal eValue As Double) As String
    '    cmdRWZSP1 = Ini_CMD(MacAdd, cmdWrite, "04C0", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '区域2
    Public Function ReadWrite_Zone_2_SP(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("04C1", 0))
        Else
            Return WriteData("04C1", CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWZSP2() As String
    '    cmdRWZSP2 = Ini_CMD(MacAdd, cmdRead, "04C1", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWZSP2(ByVal eValue As Double) As String
    '    cmdRWZSP2 = Ini_CMD(MacAdd, cmdWrite, "04C1", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '区域3
    Public Function ReadWrite_Zone_3_SP(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("04C2", 0))
        Else
            Return WriteData("04C2", CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWZSP3() As String
    '    cmdRWZSP3 = Ini_CMD(MacAdd, cmdRead, "04C2", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWZSP3(ByVal eValue As Double) As String
    '    cmdRWZSP3 = Ini_CMD(MacAdd, cmdWrite, "04C2", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '一次性读取区域1,2,3
    Public Function Read_Zone_1_2_3_SP() As String()
        Dim resp As String
        resp = ReadData("04C0", 2)
        Dim arr As String() = SplitMultiResp(resp)
        If arr IsNot Nothing Then
            For i As Integer = 0 To arr.Length - 1
                arr(i) = Resp2Double(arr(i))
            Next
        End If
        Return arr
    End Function

    'Public Overloads Function cmdRWZSP_1_2_3() As String
    '    cmdRWZSP_1_2_3 = Ini_CMD(MacAdd, cmdRead, "04C0", "2") & ControlChars.Cr
    'End Function

    '区域回差
    Public Function ReadWrite_Zone_Hysteresis(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("04CA", 0))
        Else
            Return WriteData("04CA", CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWZHYS() As String
    '    cmdRWZHYS = Ini_CMD(MacAdd, cmdRead, "04CA", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWZHYS(ByVal eValue As Double) As String
    '    cmdRWZHYS = Ini_CMD(MacAdd, cmdWrite, "04CA", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '区域PID    0:OFF    1:ON
    Public Function ReadWrite_Zone_PID(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("04CB", 0))
        Else
            Return WriteData("04CB", CInt(eValue))
        End If
    End Function
    '    Public Overloads Function cmdRWZPID() As String
    '        cmdRWZPID = Ini_CMD(MacAdd, cmdRead, "04CB", "0") & ControlChars.Cr
    '    End Function

    '    Public Overloads Function cmdRWZPID(ByVal eValue As Boolean) As String
    '        cmdRWZPID = Ini_CMD(MacAdd, cmdWrite, "04CB", "0", Bool2Write(eValue)) & ControlChars.Cr
    '    End Function
#End Region

#Region "EVx"
    'x=1,2,3

    '事件报警x的模式
    Public Function ReadWrite_EVx_MD(ByVal EVNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0500"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWEVx_MD(ByVal EVNo As Integer) As String
    '    Dim StartAdd As String = "0500"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
    '    cmdRWEVx_MD = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWEVx_MD(ByVal EVNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0500"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
    '    cmdRWEVx_MD = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '事件报警x的设定值
    Public Function ReadWrite_EVx_SP(ByVal EVNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0501"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWEVx_SP(ByVal EVNo As Integer) As String
    '    Dim StartAdd As String = "0501"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
    '    cmdRWEVx_SP = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWEVx_SP(ByVal EVNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0501"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
    '    cmdRWEVx_SP = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '事件报警x的回差
    Public Function ReadWrite_EVx_DF(ByVal EVNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0502"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CDbl(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWEVx_DF(ByVal EVNo As Integer) As String
    '    Dim StartAdd As String = "0502"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
    '    cmdRWEVx_DF = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWEVx_DF(ByVal EVNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0502"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
    '    cmdRWEVx_DF = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '事件报警x的抑制和非抑制状态 OFF:无抑制; 1:初次上电,报警抑制; 2:初次上电脱机状态时,报警抑制; 3:初次上电脱机状态或改变设定值时,报警抑制; 4:脱机状态时抑制,运行状态时无抑制
    Public Function ReadWrite_EVx_STB(ByVal EVNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0503"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
        If eValue Is Nothing Then
            Return Resp2Double(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWEVx_STB(ByVal EVNo As Integer) As String
    '    Dim StartAdd As String = "0503"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
    '    cmdRWEVx_STB = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWEVx_STB(ByVal EVNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "0503"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
    '    cmdRWEVx_STB = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '一次性读取EVx的模式,设定值,回差,抑制和非抑制状态
    Public Function Read_EVx_MD_SP_DF_STB(ByVal EVNo As Integer) As String()
        Dim resp As String
        Dim StartAdd As String = "0500"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H8") * (EVNo - 1))
        resp = ReadData(StartAdd, 3)
        Dim arr As String() = SplitMultiResp(resp)
        If arr IsNot Nothing Then
            For i As Integer = 0 To arr.Length - 1
                arr(i) = Resp2Double(arr(i))
            Next
        End If
        Return arr
    End Function

#End Region

#Region "DO Mode"
    'DO1模式
    Public Function ReadWrite_DO1_MD(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("0518", 0))
        Else
            Return WriteData("0518", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWDO1_MD() As String
    '    cmdRWDO1_MD = Ini_CMD(MacAdd, cmdRead, "0518", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWDO1_MD(ByVal eValue As Double) As String
    '    cmdRWDO1_MD = Ini_CMD(MacAdd, cmdWrite, "0518", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'DO2模式
    Public Function ReadWrite_DO2_MD(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("0519", 0))
        Else
            Return WriteData("0519", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWDO2_MD() As String
    '    cmdRWDO2_MD = Ini_CMD(MacAdd, cmdRead, "0519", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWDO2_MD(ByVal eValue As Double) As String
    '    cmdRWDO2_MD = Ini_CMD(MacAdd, cmdWrite, "0519", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'DO3模式
    Public Function ReadWrite_DO3_MD(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("0528", 0))
        Else
            Return WriteData("0528", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWDO3_MD() As String
    '    cmdRWDO3_MD = Ini_CMD(MacAdd, cmdRead, "0528", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWDO3_MD(ByVal eValue As Double) As String
    '    cmdRWDO3_MD = Ini_CMD(MacAdd, cmdWrite, "0528", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'DO4模式
    Public Function ReadWrite_DO4_MD(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("0529", 0))
        Else
            Return WriteData("0529", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWDO4_MD() As String
    '    cmdRWDO4_MD = Ini_CMD(MacAdd, cmdRead, "0529", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWDO4_MD(ByVal eValue As Double) As String
    '    cmdRWDO4_MD = Ini_CMD(MacAdd, cmdWrite, "0529", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

#End Region

#Region "DI"
    'DI开关2
    Public Function ReadWrite_DI2(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("0581", 0))
        Else
            Return WriteData("0581", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWDI2() As String
    '    cmdRWDI2 = Ini_CMD(MacAdd, cmdRead, "0581", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWDI2(ByVal eValue As Double) As String
    '    cmdRWDI2 = Ini_CMD(MacAdd, cmdWrite, "0581", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'DI开关3
    Public Function ReadWrite_DI3(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("0582", 0))
        Else
            Return WriteData("0582", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWDI3() As String
    '    cmdRWDI3 = Ini_CMD(MacAdd, cmdRead, "0582", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWDI3(ByVal eValue As Double) As String
    '    cmdRWDI3 = Ini_CMD(MacAdd, cmdWrite, "0582", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'DI开关4
    Public Function ReadWrite_DI4(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("0583", 0))
        Else
            Return WriteData("0583", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWDI4() As String
    '    cmdRWDI4 = Ini_CMD(MacAdd, cmdRead, "0583", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWDI4(ByVal eValue As Double) As String
    '    cmdRWDI4 = Ini_CMD(MacAdd, cmdWrite, "0583", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '一次性读取DI开关2,3,4
    Public Overloads Function Read_DI_2_3_4() As String()
        Dim resp As String
        resp = ReadData("0581", 2)
        Dim arr As String() = SplitMultiResp(resp)
        If arr IsNot Nothing Then
            For i As Integer = 0 To arr.Length - 1
                arr(i) = Resp2Integer(arr(i))
            Next
        End If
        Return arr
    End Function
#End Region

#Region "AO"
    '模拟变送模式 0=测量值,1=设定值,2=输出值
    Public Function ReadWrite_AO1_MD(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("05A0", 0))
        Else
            Return WriteData("05A0", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWAO1_MD() As String
    '    cmdRWAO1_MD = Ini_CMD(MacAdd, cmdRead, "05A0", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWAO1_MD(ByVal eValue As Integer) As String
    '    cmdRWAO1_MD = Ini_CMD(MacAdd, cmdWrite, "05A0", "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '模拟变送下限
    Public Function ReadWrite_AO1_Low_Limit(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("05A1", 0))
        Else
            Return WriteData("05A1", Set_Temp_Format(CDbl(eValue)))
        End If
    End Function

    'Public Overloads Function cmdRWAO1_LowLimit() As String
    '    cmdRWAO1_LowLimit = Ini_CMD(MacAdd, cmdRead, "05A1", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWAO1_LowLimit(ByVal eValue As Double) As String
    '    cmdRWAO1_LowLimit = Ini_CMD(MacAdd, cmdWrite, "05A1", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function
    '模拟变送上限
    Public Function ReadWrite_AO1_High_Limit(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Double(ReadData("05A2", 0))
        Else
            Return WriteData("05A2", Set_Temp_Format(CDbl(eValue)))
        End If
    End Function

    'Public Overloads Function cmdRWAO1_HighLimit() As String
    '    cmdRWAO1_HighLimit = Ini_CMD(MacAdd, cmdRead, "05A2", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWAO1_HighLimit(ByVal eValue As Double) As String
    '    cmdRWAO1_HighLimit = Ini_CMD(MacAdd, cmdWrite, "05A2", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    '一次性读取模拟变送模式，下限，上限
    Public Overloads Function Read_AO1_MD_L_H() As String()
        Dim resp As String
        resp = ReadData("05A0", 2)
        Dim arr As String() = SplitMultiResp(resp)
        If arr IsNot Nothing Then

            arr(0) = Resp2Integer(arr(0))
            arr(1) = Get_Temp_Format(arr(1))
            arr(2) = Get_Temp_Format(arr(2))

        End If
        Return arr
    End Function
#End Region

#Region "COM MEM"
    '通讯的存贮模式       0=EEP  1=REM   2=r_E
    Public Function ReadWrite_COM_MEM(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("05B0", 0))
        Else
            Return WriteData("05B0", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWCOM_MEM() As String
    '    cmdRWCOM_MEM = Ini_CMD(MacAdd, cmdRead, "05B0", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWCOM_MEM(ByVal eValue As Double) As String
    '    cmdRWCOM_MEM = Ini_CMD(MacAdd, cmdWrite, "05B0", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

#End Region

#Region "Output Setting"

    '输出的特性           0=反作用     1=整作用
    Public Function ReadWrite_Output_Characteristic(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("0600", 0))
        Else
            Return WriteData("0600", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWACTMD() As String
    '    cmdRWACTMD = Ini_CMD(MacAdd, cmdRead, "0600", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWACTMD(ByVal eValue As Boolean) As String
    '    cmdRWACTMD = Ini_CMD(MacAdd, cmdWrite, "0600", "0", Bool2Write(eValue)) & ControlChars.Cr
    'End Function

    '控制输出的比例周期
    Public Function ReadWrite_Control_Output_Proportional_Cycle(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("0601", 0))
        Else
            Return WriteData("0601", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRW01_CYC() As String
    '    cmdRW01_CYC = Ini_CMD(MacAdd, cmdRead, "0601", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRW01_CYC(ByVal eValue As Double) As String
    '    cmdRW01_CYC = Ini_CMD(MacAdd, cmdWrite, "0601", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

#End Region

#Region "Key Lock"
    '键盘锁  0=无锁定; 1=锁定窗口群组3、4和5; 2=锁定窗口群组1、2、3、4和5; 3=除了RUN、RST全部锁定
    Public Function ReadWrite_Key_Lock(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("0611", 0))
        Else
            Return WriteData("0611", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWKLOCK() As String
    '    cmdRWKLOCK = Ini_CMD(MacAdd, cmdRead, "0611", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWKLOCK(ByVal eValue As Integer) As String
    '    cmdRWKLOCK = Ini_CMD(MacAdd, cmdWrite, "0611", "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function
#End Region

#Region "PV Setting"
    'PV值偏移
    Public Function ReadWrite_PV_Bias(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Get_Temp_Format(ReadData("0701", 0))
        Else
            Return WriteData("0701", Set_Temp_Format(CDbl(eValue)))
        End If
    End Function

    'Public Overloads Function cmdRWPV_B() As String
    '    cmdRWPV_B = Ini_CMD(MacAdd, cmdRead, "0701", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWPV_B(ByVal eValue As Double) As String
    '    cmdRWPV_B = Ini_CMD(MacAdd, cmdWrite, "0701", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

    'PV值滤波
    Public Function ReadWrite_PV_Filter(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Get_Temp_Format(ReadData("0702", 0))
        Else
            Return WriteData("0702", Set_Temp_Format(CDbl(eValue)))
        End If
    End Function
    'Public Overloads Function cmdRWPV_F() As String
    '    cmdRWPV_F = Ini_CMD(MacAdd, cmdRead, "0702", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWPV_F(ByVal eValue As Double) As String
    '    cmdRWPV_F = Ini_CMD(MacAdd, cmdWrite, "0702", "0", Dbl2Write(eValue)) & ControlChars.Cr
    'End Function

#End Region

#Region "Running Setting"
    '控制模式           0=程序模式   1=定值方式
    Public Function ReadWrite_Program_Mode(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("0800", 0))
        Else
            Return WriteData("0800", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWPRG_MD() As String
    '    cmdRWPRG_MD = Ini_CMD(MacAdd, cmdRead, "0800", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWPRG_MD(ByVal eValue As Boolean) As String
    '    cmdRWPRG_MD = Ini_CMD(MacAdd, cmdWrite, "0800", "0", Bool2Write(eValue)) & ControlChars.Cr
    'End Function

    '起始的曲线号
    Public Function ReadWrite_Start_Pattern_No(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("0802", 0))
        Else
            Return WriteData("0802", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWST_PTN() As String
    '    cmdRWST_PTN = Ini_CMD(MacAdd, cmdRead, "0802", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWST_PTN(ByVal eValue As Integer) As String
    '    cmdRWST_PTN = Ini_CMD(MacAdd, cmdWrite, "0802", "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '当前曲线号
    Public Function ReadWrite_Pattern_No(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("0818", 0))
        Else
            Return WriteData("0818", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWPRN_MOD() As String
    '    cmdRWPRN_MOD = Ini_CMD(MacAdd, cmdRead, "0818", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWPRN_MOD(ByVal eValue As Integer) As String
    '    cmdRWPRN_MOD = Ini_CMD(MacAdd, cmdWrite, "0818", "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '时间单位        0=小时/分     1=分/秒
    Public Function ReadWrite_Time_Mode(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("0819", 0))
        Else
            Return WriteData("0819", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWTIM_MOD() As String
    '    cmdRWTIM_MOD = Ini_CMD(MacAdd, cmdRead, "0819", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWTIM_MOD(ByVal eValue As Boolean) As String
    '    cmdRWTIM_MOD = Ini_CMD(MacAdd, cmdWrite, "0819", "0", Bool2Write(eValue)) & ControlChars.Cr
    'End Function

    '急停模式
    Public Function ReadWrite_Instantaneous_Stop_Mode(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("081A", 0))
        Else
            Return WriteData("081A", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWSHT_MOD() As String
    '    cmdRWSHT_MOD = Ini_CMD(MacAdd, cmdRead, "081A", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWSHT_MOD(ByVal eValue As Integer) As String
    '    cmdRWSHT_MOD = Ini_CMD(MacAdd, cmdWrite, "081A", "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '非正常输入模式 0:Hold; 1:Run; 2:Reset
    Public Function ReadWrite_Input_Abnormality_Mode(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("081B", 0))
        Else
            Return WriteData("081B", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWSCO_MOD() As String
    '    cmdRWSCO_MOD = Ini_CMD(MacAdd, cmdRead, "081B", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWSCO_MOD(ByVal eValue As Integer) As String
    '    cmdRWSCO_MOD = Ini_CMD(MacAdd, cmdWrite, "081B", "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

#End Region

#End Region

#Region "Fix Mode PID No."
    '定值方式的PID号
    Public Function ReadWrite_FIX_PID_No(Optional ByVal eValue As String = Nothing) As String
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData("0820", 0))
        Else
            Return WriteData("0820", CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWFix_PID_No() As String
    '    cmdRWFix_PID_No = Ini_CMD(MacAdd, cmdRead, "0820", "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWFix_PID_No(ByVal eValue As Integer) As String
    '    cmdRWFix_PID_No = Ini_CMD(MacAdd, cmdWrite, "0820", "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function
#End Region

#Region "Pattern x Setting"
    ' x = 1, 2, 3, 4
    '程序模式下曲线x的步数
    Public Function ReadWrite_PatternX_Step_Number(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0882"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_STP(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "0882"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_STP = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_STP(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "0882"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_STP = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '程序模式下曲线x的重复次数
    Public Function ReadWrite_PatternX_Executions_Number(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0883"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_RPT(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "0883"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_RPT = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_RPT(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "0883"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_RPT = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '程序模式下曲线x的起始设定值
    Public Function ReadWrite_PatternX_Start_SV_Value(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0884"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Get_Temp_Format(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, Set_Temp_Format(CDbl(eValue)))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_ST_SV(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "0884"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_ST_SV = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_ST_SV(ByVal PatternNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0884"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_ST_SV = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue / DPosition)) & ControlChars.Cr
    'End Function

    '程序模式下曲线x的确保平台区域
    Public Function ReadWrite_PatternX_Guarantee_Zone(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0885"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_GUA_Z(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "0885"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_GUA_Z = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_GUA_Z(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "0885"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_GUA_Z = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '程序模式下曲线x的起始测量值
    Public Function ReadWrite_PatternX_PV_Start(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0887"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Get_Temp_Format(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, Set_Temp_Format(CDbl(eValue)))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_PV_ST(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "0887"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_PV_ST = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_PV_ST(ByVal PatternNo As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "0887"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_PV_ST = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Dbl2Write(eValue / DPosition)) & ControlChars.Cr
    'End Function

    '程序模式下曲线x的EV1事件值 
    Public Function ReadWrite_PatternX_EV1_Level_Value(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0889"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_EV1(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "0889"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_EV1 = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_EV1(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "0889"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_EV1 = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '程序模式下曲线x的EV2事件值 
    Public Function ReadWrite_PatternX_EV2_Level_Value(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "088A"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_EV2(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "088A"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_EV2 = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_EV2(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "088A"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_EV2 = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '程序模式下曲线x的EV3事件值 
    Public Function ReadWrite_PatternX_EV3_Level_Value(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "088B"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_EV3(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "088B"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_EV3 = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_EV3(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "088B"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_EV3 = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '曲线x的第1时间信号的步号  ON/OFF Step No.
    Public Function ReadWrite_PatternX_Time_Signal1_On_Off(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "088E"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function
    'Public Overloads Function cmdRWP0x_TS1STP(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "088E"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS1STP = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_TS1STP(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "088E"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS1STP = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '曲线x的第1时间信号延时开时间 
    Public Function ReadWrite_PatternX_Time_Signal1_On_Time(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "088F"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_TS1_ON(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "088F"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS1_ON = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_TS1_ON(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "088F"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS1_ON = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '曲线x的第1时间信号延时停时间 
    Public Function ReadWrite_PatternX_Time_Signal1_Off_Time(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0890"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_TS1_OFF(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "0890"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS1_OFF = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_TS1_OFF(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "0890"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS1_OFF = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function


    '曲线x的第2时间信号的步号 
    Public Function ReadWrite_PatternX_Time_Signal2_On_Off(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0891"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_TS2STP(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "0891"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS2STP = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_TS2STP(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "0891"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS2STP = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '曲线x的第2时间信号延时开时间 
    Public Function ReadWrite_PatternX_Time_Signal2_On_Time(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0892"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_TS2_ON(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "0892"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS2_ON = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_TS2_ON(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "0892"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS2_ON = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function

    '曲线x的第2时间信号延时停时间 
    Public Function ReadWrite_PatternX_Time_Signal2_Off_Time(ByVal PatternNo As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "0893"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        If eValue Is Nothing Then
            Return Resp2Integer(ReadData(StartAdd, 0))
        Else
            Return WriteData(StartAdd, CInt(eValue))
        End If
    End Function

    'Public Overloads Function cmdRWP0x_TS2_OFF(ByVal PatternNo As Integer) As String
    '    Dim StartAdd As String = "0893"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS2_OFF = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWP0x_TS2_OFF(ByVal PatternNo As Integer, ByVal eValue As Integer) As String
    '    Dim StartAdd As String = "0893"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
    '    cmdRWP0x_TS2_OFF = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", Int2Write(eValue)) & ControlChars.Cr
    'End Function


    '一次性读取程序模式下曲线x的步数,重复次数,起始设定值,确保平台区域
    Public Function Read_PatternX_STP_RPT_SV_GUA(ByVal PatternNo As Integer) As String()
        Dim resp As String
        Dim StartAdd As String = "0882"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        resp = ReadData(StartAdd, 3)
        Dim arr As String() = SplitMultiResp(resp)
        If arr IsNot Nothing Then
            arr(0) = Resp2Integer(arr(0))
            arr(1) = Resp2Integer(arr(1))
            arr(2) = Get_Temp_Format(arr(2))
            arr(3) = Resp2Integer(arr(3))
        End If
        Return arr
    End Function

    '一次性读取程序模式下曲线x的EV1, EV2,EV3事件值 
    Public Function Read_PatternX_EV1_2_3(ByVal PatternNo As Integer) As String()
        Dim resp As String
        Dim StartAdd As String = "0889"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        resp = ReadData(StartAdd, 2)
        Dim tmparr As String() = SplitMultiResp(resp)
        Dim arr(7) As String
        arr(0) = Resp2Integer(Left(tmparr(0), 2))
        arr(1) = Resp2Integer(Right(tmparr(0), 2))
        arr(2) = Resp2Time(tmparr(1))
        arr(3) = Resp2Time(tmparr(2))
        arr(4) = Resp2Integer(Left(tmparr(3), 2))
        arr(5) = Resp2Integer(Right(tmparr(3), 2))
        arr(6) = Resp2Time(tmparr(4))
        arr(7) = Resp2Time(tmparr(5))
        Return arr
    End Function

    '一次性读取程序模式下曲线x的第1时间信号的步号,延时开时间,延时停时间
    Public Function Read_PatternX_TS1_2_STP_ON_OFF(ByVal PatternNo As Integer) As String()
        Dim resp As String
        Dim StartAdd As String = "088E"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1))
        resp = ReadData(StartAdd, 5)
        Dim arr As String() = SplitMultiResp(resp)
        If arr IsNot Nothing Then
            For i As Integer = 0 To arr.Length
                arr(i) = Resp2Integer(arr(i))
            Next
        End If
        Return arr
    End Function


    '读写各个曲线Step的SV值,时间,PID号
    'PatternNo=1: 曲线1; 
    'PatternNo=2: 曲线2; 
    'PatternNo=3: 曲线3; 
    'PatternNo=4: 曲线4; 

    'StepNo=1; 第1步
    'StepNo=2; 第2步
    'StepNo=3; 第3步
    'StepNo=4; 第4步
    'StepNo=5; 第5步
    'StepNo=6; 第6步
    'StepNo=7; 第7步
    'StepNo=8; 第8步
    'StepNo=9; 第9步
    'StepNo=10; 第10步

    'Item mod 4=0: 保留; 
    'Item mod 4=1: SV值 ; 
    'Item mod 4=2: 时间; 
    'Item mod 4=3: PID号; 

    Public Function ReadWrite_PatternX_StepY_ItemZ_Value(ByVal PatternNo As Integer, ByVal StepNo As Integer, ByVal Item As Integer, Optional ByVal eValue As String = Nothing) As String
        Dim StartAdd As String = "08A0"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1) + (StepNo - 1) * RespLength + (Item - 1))
        If eValue Is Nothing Then
            Select Case Item
                Case Step_Item_SV
                    Return Get_Temp_Format(ReadData(StartAdd, 0))
                Case Step_Item_TM
                    Return Get_Time_Format(ReadData(StartAdd, 0))
                Case Else
                    Return Resp2Integer(ReadData(StartAdd, 0))
            End Select
        Else
            Select Case Item
                Case Step_Item_SV
                    Return WriteData(StartAdd, Set_Temp_Format(CDbl(eValue)))
                Case Step_Item_TM
                    Return WriteData(StartAdd, Set_Time_Format(eValue))
                Case Else
                    Return WriteData(StartAdd, CInt(eValue))
            End Select
        End If
    End Function

    'Public Overloads Function cmdRWPattern_STP_Value(ByVal PatternNo As Integer, ByVal StepNo As Integer, ByVal Item As Integer) As String
    '    Dim StartAdd As String = "08A0"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1) + (StepNo - 1) * RespLength + (Item - 1))
    '    cmdRWPattern_STP_Value = Ini_CMD(MacAdd, cmdRead, StartAdd, "0") & ControlChars.Cr
    'End Function

    '一次性读取PatternX, StepY的3个参数值
    Public Function Read_PatternX_StepY_SV_Time_PID(ByVal PatternNo As Integer, ByVal StepNo As Integer) As String()
        Dim resp As String
        Dim StartAdd As String = "08A0"
        StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1) + (StepNo - 1) * RespLength)
        resp = ReadData(StartAdd, 2)

        Dim arr As String() = SplitMultiResp(resp)
        If arr IsNot Nothing Then
            arr(0) = Get_Temp_Format(arr(0))
            arr(1) = Get_Time_Format(arr(1))
            arr(2) = Resp2Integer(arr(2))
        End If
        Return arr
    End Function

    'Public Overloads Function cmdRWPattern_STP_Value(ByVal PatternNo As Integer, ByVal StepNo As Integer) As String
    '    Dim StartAdd As String = "08A0"
    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1) + (StepNo - 1) * RespLength)
    '    cmdRWPattern_STP_Value = Ini_CMD(MacAdd, cmdRead, StartAdd, "2") & ControlChars.Cr
    'End Function

    'Public Overloads Function cmdRWPattern_STP_Value(ByVal PatternNo As Integer, ByVal StepNo As Integer, ByVal Item As Integer, ByVal eValue As Double) As String
    '    Dim StartAdd As String = "08A0"
    '    Dim WriteValue As String = ""

    '    StartAdd = Dec2Hex(CInt("&H" & StartAdd) + CInt("&H0080") * (PatternNo - 1) + (StepNo - 1) * RespLength + (Item - 1))

    '    Select Case Item
    '        Case Step_Item_SV
    '            WriteValue = Dbl2Write(eValue / DPosition)
    '        Case Else
    '            WriteValue = Dbl2Write(eValue)
    '    End Select
    '    cmdRWPattern_STP_Value = Ini_CMD(MacAdd, cmdWrite, StartAdd, "0", WriteValue) & ControlChars.Cr
    'End Function

#End Region

#End Region
End Class
