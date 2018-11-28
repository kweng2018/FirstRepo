Imports System.Drawing.Color
Imports VB6 = Microsoft.VisualBasic
Imports System.Xml
Imports System.IO
Imports System.Threading.Thread
Imports AndrewIntegratedProducts.DUTDriverFramework
Imports Microsoft.VisualBasic

Module modGlobal


#Region "Public Parameters"
    Public Plant As String

    Public ShelfID As String
    Public Shelf2Slot As Integer

    Public MessageIndicators As New Collection
    Public AbortTest As Boolean = False
    Public TimeDone As Boolean
    Public ODCCheckResult As Boolean = False

    Public SW_Title As String
    Public SW_Version As String
    Public MTR_Version As String
    Public Assembly_Type As String
    Public Test_Site As String
    Public Test_Group As String
    Public dgvSlotTestData(SlotNum) As System.Windows.Forms.DataGridView
    Public dgvFailureInd(SlotNum) As System.Windows.Forms.DataGridView
    Public dgvSlotInd As System.Windows.Forms.DataGridView

    Public FirstFailTime(SlotNum) As Date
    Public TestItems() As String = New String() {"PollingTime", _
                                                 "Phase", _
                                                 "PA0_Temp", _
                                                 "PA0_VSWR_Temp", _
                                                 "PA1_Temp", _
                                                 "PA1_VSWR_Temp", _
                                                 "LNA0_Temp", _
                                                 "LNA1_Temp", _
                                                 "LNA2_Temp", _
                                                 "LNA3_Temp", _
                                                 "PS_Converter_Temp", _
                                                 "PS_Brick_Temp", _
                                                 "FB_Temp", _
                                                 "RX_Temp", _
                                                 "TX0_Output_Pow", _
                                                 "TX0_VSWR", _
                                                 "TX0_Forward_Power_Detector", _
                                                 "TX0_Reverse_Power_Detector", _
                                                 "TX0_Gain_VCA", _
                                                 "TX0_txDacGain", _
                                                 "TX0_totalTxAttn", _
                                                 "TX0_Gain_TxStep", _
                                                 "TX0_Gain_FbStep", _
                                                 "TX0_Gain_FbTxQuo", _
                                                 "TX0_Gain_GainError", _
                                                 "TX0_PA0_PsVolt", _
                                                 "TX0_PA0_Temp", _
                                                 "TX0_PA0_BiasTemp", _
                                                 "TX0_PA0_Driver1Cur", _
                                                 "TX0_PA0_Driver2Cur", _
                                                 "TX0_PA0_Driver3Cur", _
                                                 "TX0_PA0_Driver4Cur", _
                                                 "TX0_PA0_Final1Cur", _
                                                 "TX0_PA0_Final2Cur", _
                                                 "TX0_PA0_Driver1Dac", _
                                                 "TX0_PA0_Driver2Dac", _
                                                 "TX0_PA0_Driver3Dac", _
                                                 "TX0_PA0_Driver4Dac", _
                                                 "TX0_PA0_Final1Dac", _
                                                 "TX0_PA0_Final2Dac", _
                                                 "TX0_PA0_ampDelayInt", _
                                                 "TX0_PA0_ampDelayFrac", _
                                                 "TX0_PA0_MaxCrossCorrelation", _
                                                 "Tx0_DPD_L1_Table_Max_Gain", _
                                                 "Tx0_DPD_L1_Table_Min_Gain", _
                                                 "Tx0_DPD_L2_3rd_sym_am", _
                                                 "Tx0_DPD_L2_3rd_sym_ph", _
                                                 "Tx0_DPD_L3_3rd_sym_am", _
                                                 "Tx0_DPD_L3_3rd_sym_ph", _
                                                 "Tx0_DPD_L2_5th_sym_am", _
                                                 "Tx0_DPD_L2_5th_sym_ph", _
                                                 "Tx0_DPD_L3_5th_sym_am", _
                                                 "Tx0_DPD_L3_5th_sym_ph", _
                                                 "Tx0_DPD_L2_3rd_asym_am", _
                                                 "Tx0_DPD_L2_3rd_asym_ph", _
                                                 "Tx0_DPD_L2_5th_asym_am", _
                                                 "Tx0_DPD_L2_5th_asym_ph", _
                                                 "TX1_Output_Pow", _
                                                 "TX1_VSWR", _
                                                 "TX1_Forward_Power_Detector", _
                                                 "TX1_Reverse_Power_Detector", _
                                                 "TX1_Gain_VCA", _
                                                 "TX1_txDacGain", _
                                                 "TX1_totalTxAttn", _
                                                 "TX1_Gain_TxStep", _
                                                 "TX1_Gain_FbStep", _
                                                 "TX1_Gain_FbTxQuo", _
                                                 "TX1_Gain_GainError", _
                                                 "TX1_PA1_PsVolt", _
                                                 "TX1_PA1_Temp", _
                                                 "TX1_PA1_BiasTemp", _
                                                 "TX1_PA1_Driver1Cur", _
                                                 "TX1_PA1_Driver2Cur", _
                                                 "TX1_PA1_Driver3Cur", _
                                                 "TX1_PA1_Driver4Cur", _
                                                 "TX1_PA1_Final1Cur", _
                                                 "TX1_PA1_Final2Cur", _
                                                 "TX1_PA1_Driver1Dac", _
                                                 "TX1_PA1_Driver2Dac", _
                                                 "TX1_PA1_Driver3Dac", _
                                                 "TX1_PA1_Driver4Dac", _
                                                 "TX1_PA1_Final1Dac", _
                                                 "TX1_PA1_Final2Dac", _
                                                 "TX1_PA1_ampDelayInt", _
                                                 "TX1_PA1_ampDelayFrac", _
                                                 "TX1_PA1_MaxCrossCorrelation", _
                                                 "Tx1_DPD_L1_Table_Max_Gain", _
                                                 "Tx1_DPD_L1_Table_Min_Gain", _
                                                 "Tx1_DPD_L2_3rd_sym_am", _
                                                 "Tx1_DPD_L2_3rd_sym_ph", _
                                                 "Tx1_DPD_L3_3rd_sym_am", _
                                                 "Tx1_DPD_L3_3rd_sym_ph", _
                                                 "Tx1_DPD_L2_5th_sym_am", _
                                                 "Tx1_DPD_L2_5th_sym_ph", _
                                                 "Tx1_DPD_L3_5th_sym_am", _
                                                 "Tx1_DPD_L3_5th_sym_ph", _
                                                 "Tx1_DPD_L2_3rd_asym_am", _
                                                 "Tx1_DPD_L2_3rd_asym_ph", _
                                                 "Tx1_DPD_L2_5th_asym_am", _
                                                 "Tx1_DPD_L2_5th_asym_ph", _
                                                 "Rx0_LNA_Atten", _
                                                 "Rx0_Rx_Atten0", _
                                                 "Rx0_Rx_Atten1", _
                                                 "Rx0_RSSI_C0", _
                                                 "Rx0_RSSI_C1", _
                                                 "Rx1_LNA_Atten", _
                                                 "Rx1_Rx_Atten0", _
                                                 "Rx1_Rx_Atten1", _
                                                 "Rx1_RSSI_C0", _
                                                 "Rx1_RSSI_C1", _
                                                 "Rx2_LNA_Atten", _
                                                 "Rx2_Rx_Atten0", _
                                                 "Rx2_Rx_Atten1", _
                                                 "Rx2_RSSI_C0", _
                                                 "Rx2_RSSI_C1", _
                                                 "Rx3_LNA_Atten", _
                                                 "Rx3_Rx_Atten0", _
                                                 "Rx3_Rx_Atten1", _
                                                 "Rx3_RSSI_C0", _
                                                 "Rx3_RSSI_C1", _
                                                 "Input_Voltage", _
                                                 "Input_Current", _
                                                 "Input_Power", _
                                                 "Output_Voltage", _
                                                 "AISG_12V_Voltage", _
                                                 "AISG_12V_Current", _
                                                 "AISG_24V_Voltage", _
                                                 "AISG_24V_Current", _
                                                 "DSPRevision", _
                                                 "FPGARevision", _
                                                 "SWRevision", _
                                                 "Alarm", _
                                                 "PAEnable"}
#End Region

#Region "Public Class"
    Public mySwitch_RS232 As New Switch_RS232
    Public mySwitch_RS422 As New Switch_RS422
    Public IO24 As New Switch_USBIO24
    Public mySwitch_9CH_Eth As New Switch_9CH_Eth
    Public PS(5) As iPS
    Public HW_Config_XML As clsXML
    Public SW_Config_XML As clsXML
    Public BI_Test_Results As New TestResults
    Public BI_Param As New clsParamters
    Public BI_HW As New clsBurnInHWConfig
    Public BI_Limit As New clsBurnInLimit
    Public BI_Profile As New clsBurnInProfile
    Public BI_Data(SlotNum) As clsBurnInData
    Public Transceiver(SlotNum) As Viper_Transceiver
    Public moRS232 As New Rs232
    Public Chamber As New clsChamberCTRL
    Public PC As New clsProcessCheck

#End Region

#Region "Public Constant"
    Public Const BLK As String = " "
    Public Const SP As String = "/ "
    Public Const SlotNum As Integer = 1
    Public Const OK As String = "OK"
    Public Const NL As String = Microsoft.VisualBasic.Constants.vbNewLine
    Public Const NL2 As String = NL & NL
    Public Const SplitChar As String = "+"

#End Region

#Region "Public Functions"

#Region "Config XML Operation"

    Public Sub LoadConfigXML()
        'Load HW config
        Try
            HW_Config_XML = New clsXML(Application.StartupPath() & "\HW_Config.XML", True)
            'MsgBox("File path:" & Application.StartupPath & "\HW_Config.XML")

            BI_HW.PlantPC = UCase(HW_Config_XML.Read("HW_Config" & SP & "Test_Plant_Setting", "Test_Plant_PC", ""))
            BI_HW.Plant = HW_Config_XML.Read("HW_Config" & SP & "Test_Plant_Setting", "Test_Plant", "")

            BI_HW.PS_Manual_Control = HW_Config_XML.Read("HW_Config" & SP & "PS_Manual_Setting", "PS_Manual_Control", False)

            BI_HW.Kanban_System_Upload = HW_Config_XML.Read("HW_Config" & SP & "Kanban_System", "Kanban_System_Upload", False)

            BI_HW.Switch_PS_Enabled = HW_Config_XML.Read("HW_Config" & SP & "Net_Switch_Setting", "Switch_PS_Enabled", False)
            BI_HW.Switch_PS_Type = HW_Config_XML.Read("HW_Config" & SP & "Net_Switch_Setting", "Switch_PS_Type", BLK)
            BI_HW.Switch_PS_COM_Port = HW_Config_XML.Read("HW_Config" & SP & "Net_Switch_Setting", "Switch_PS_COM_Port", 0)
            BI_HW.Switch_PS_Channel = HW_Config_XML.Read("HW_Config" & SP & "Net_Switch_Setting", "Switch_PS_Channel", 19)

            BI_HW.Pwr_Chan_Enabled = HW_Config_XML.Read("HW_Config" & SP & "Power_Switch_Setting", "Power_Switch_Enabled", False)
            BI_HW.Pwr_Switch_Type = HW_Config_XML.Read("HW_Config" & SP & "Power_Switch_Setting", "Power_Switch_Type", BLK)
            BI_HW.Pwr_Port_Number = HW_Config_XML.Read("HW_Config" & SP & "Power_Switch_Setting", "Power_Switch_Relay_Port", 0)
            For i As Integer = 0 To SlotNum
                'BI_HW.Pwr_Chan(i) = HW_Config_XML.Read("HW_Config" & SP & "Power_Switch_Setting", "Power_Channel_" & (i + 1 + Shelf2Slot).ToString, 0)
                BI_HW.Pwr_Chan(i) = HW_Config_XML.Read("HW_Config" & SP & "Power_Switch_Setting", "Power_Channel_" & (i + 1 + Shelf2Slot), 0)
            Next

            BI_HW.Anton_Chan_Enabled = HW_Config_XML.Read("HW_Config" & SP & "Anton_Switch_Setting", "Anton_Switch_Enabled", False)
            BI_HW.Anton_Switch_Type = HW_Config_XML.Read("HW_Config" & SP & "Anton_Switch_Setting", "Anton_Switch_Type", BLK)
            BI_HW.Anton_Port_Number = HW_Config_XML.Read("HW_Config" & SP & "Anton_Switch_Setting", "Anton_Switch_Relay_Port", 0)
            For i As Integer = 0 To SlotNum
                'BI_HW.Anton_Chan(i) = HW_Config_XML.Read("HW_Config" & SP & "Anton_Switch_Setting", "Anton_Channel_" & (i + 1 + Shelf2Slot).ToString, 0)
                BI_HW.Anton_Chan(i) = HW_Config_XML.Read("HW_Config" & SP & "Anton_Switch_Setting", "Anton_Channel_" & (i + 1 + Shelf2Slot), 0)
            Next

            BI_HW.Fan_Chan_Enabled = HW_Config_XML.Read("HW_Config" & SP & "Fan_Switch_Setting", "Fan_Switch_Enabled", False)
            BI_HW.Fan_Switch_Type = HW_Config_XML.Read("HW_Config" & SP & "Fan_Switch_Setting", "Fan_Switch_Type", BLK)
            BI_HW.Fan_Port_Number = HW_Config_XML.Read("HW_Config" & SP & "Fan_Switch_Setting", "Fan_Switch_COM_Port", 0)
            For i As Integer = 0 To SlotNum
                'BI_HW.Fan_Chan(i) = HW_Config_XML.Read("HW_Config" & SP & "Fan_Switch_Setting", "Fan_Channel_" & (i + 1).ToString, 0)
                BI_HW.Fan_Chan(i) = HW_Config_XML.Read("HW_Config" & SP & "Fan_Switch_Setting", "Fan_Channel_" & (i + 1 + Shelf2Slot), 0)
            Next

            For I As Integer = 0 To 5
                BI_HW.PS_Control_Enabled(I) = HW_Config_XML.Read("HW_Config" & SP & "Power_Supplier_Setting" & SP & "PS_" & I + 1, "PS_Control_Enabled", False)
                BI_HW.PS_Control_Type(I) = HW_Config_XML.Read("HW_Config" & SP & "Power_Supplier_Setting" & SP & "PS_" & I + 1, "PS_Control_Type", BLK)
                BI_HW.PS_Control_Address(I) = HW_Config_XML.Read("HW_Config" & SP & "Power_Supplier_Setting" & SP & "PS_" & I + 1, "PS_Control_Address", 0)
                BI_HW.PS_Voltage(I) = HW_Config_XML.Read("HW_Config" & SP & "Power_Supplier_Setting" & SP & "PS_" & I + 1, "PS_Voltage", 0)
                BI_HW.PS_Current(I) = HW_Config_XML.Read("HW_Config" & SP & "Power_Supplier_Setting" & SP & "PS_" & I + 1, "PS_Current", 0)
            Next

            BI_HW.Chamber_Control_Enabled = HW_Config_XML.Read("HW_Config" & SP & "Chamber_Setting", "Chamber_Enabled", False)
            BI_HW.Chamber_Port_Number = HW_Config_XML.Read("HW_Config" & SP & "Chamber_Setting", "Chamber_COM_Port", 0)
            BI_HW.Chamber_Run_Pattern = HW_Config_XML.Read("HW_Config" & SP & "Chamber_Setting", "Chamber_Pattern", 1)
            For i As Integer = 0 To 9
                BI_HW.Chamber_Step(i).Temp = HW_Config_XML.Read("HW_Config" & SP & "Chamber_Setting", "Step" & (i + 1).ToString & "_" & "Temp", 0)
                BI_HW.Chamber_Step(i).Time = HW_Config_XML.Read("HW_Config" & SP & "Chamber_Setting", "Step" & (i + 1).ToString & "_" & "Time", 0)
                BI_HW.Chamber_Step(i).PID = HW_Config_XML.Read("HW_Config" & SP & "Chamber_Setting", "Step" & (i + 1).ToString & "_" & "PID", 0)
            Next

            'Read Unit configuration
            BI_HW.ConnectionType = HW_Config_XML.Read("Unit_Config", "ConnectionType", BLK)
            For i As Integer = 0 To SlotNum
                'BI_HW.IPAddress(i) = HW_Config_XML.Read("Unit_Config" & SP & "IP_Address", "IP_Address_" & (i + 1).ToString, BLK)
                BI_HW.IPAddress(i) = HW_Config_XML.Read("Unit_Config" & SP & "IP_Address", "IP_Address_" & (i + 1 + Shelf2Slot).ToString, BLK)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        'Load SW config
        Try
            SW_Config_XML = New clsXML(Application.StartupPath() & "\SW_Config.XML", True)
            SW_Title = SW_Config_XML.Read("Information", "SW_Title", "Burn In")
            SW_Version = SW_Config_XML.Read("Information", "SW_Version", "0.0.0")
            MTR_Version = SW_Config_XML.Read("Information", "MTR_Version", "0.0.0")
            Assembly_Type = SW_Config_XML.Read("Information", "Assembly_Type", "NA")
            Test_Site = SW_Config_XML.Read("Information", "Test_Site", "CEL-CSU")
            Test_Group = SW_Config_XML.Read("Information", "Test_Group", "BurnIn1")

            BI_Param.File_Save_Path = SW_Config_XML.Read("Parameters", "File_Save_Path", "C:\BurnIn\")
            'BI_Param.Matlab_Check_Enabled = SW_Config_XML.Read("Parameters", "Matlab_Check_Enabled", False)
            ''BI_Param.Matlab_Program_Path = SW_Config_XML.Read("Parameters", "Matlab_Program_Path", "NA")
            'BI_Param.Matlab_Picture_Enabled = SW_Config_XML.Read("Parameters", "Matlab_Picture_Enabled", False)
            'BI_Param.Matlab_Picture_Comments = SW_Config_XML.Read("Parameters", "Matlab_Picture_Comments", "NA")
            'BI_Param.Matlab_Picture_Save_Path = SW_Config_XML.Read("Parameters", "Matlab_Picture_Save_Path", "NA")

            'Read Profile
            BI_Profile.BI_Start_Temp = SW_Config_XML.Read("Profile", "BI_Start_Temp", 0)
            BI_Profile.BI_Duration = SW_Config_XML.Read("Profile", "BI_Duration", 0)
            BI_Profile.BI_Cycle = SW_Config_XML.Read("Profile", "BI_Cycle", 0)
            BI_Profile.BI_Polling_Interval = SW_Config_XML.Read("Profile", "BI_Polling_Interval", 10)
            BI_Profile.BI_Power_Cycle = SW_Config_XML.Read("Profile", "BI_Power_Cycle", 0)
            BI_Profile.BI_Power_Cycle_Interval = SW_Config_XML.Read("Profile", "BI_Power_Cycle_Interval", 10)
            BI_Profile.BI_Target_Temp = SW_Config_XML.Read("Profile", "BI_Target_Temp", 0)
            BI_Profile.BI_Heating_Up_Time = SW_Config_XML.Read("Profile", "BI_Heating_Up_Time", 0)
            BI_Profile.BI_Soak_Time = SW_Config_XML.Read("Profile", "BI_Soak_Time", 0)
            BI_Profile.BI_Cooling_Down_Time = SW_Config_XML.Read("Profile", "BI_Cooling_Down_Time", 0)

            'Read Limit
            'Common Items
            BI_Limit.Thres_Hold_Temp = SW_Config_XML.Read("Limit", "Thres_Hold_Temp", 0)
            BI_Limit.Thres_Hold_Time = SW_Config_XML.Read("Limit", "Thres_Hold_Time", 0)
            BI_Limit.RF_On_Time = SW_Config_XML.Read("Limit", "RF_On_Time", 0)

            BI_Limit.BI_Pre_VSWR = SW_Config_XML.Read("Limit", "BI_Pre_VSWR", 0)

            'General Information
            BI_Limit.BI_High_PA_Temp_UL = SW_Config_XML.Read("Limit", "BI_High_PA_Temp_UL", 0)
            BI_Limit.BI_High_PA_Temp_LL = SW_Config_XML.Read("Limit", "BI_High_PA_Temp_LL", 0)
            BI_Limit.BI_Low_PA_Temp_UL = SW_Config_XML.Read("Limit", "BI_Low_PA_Temp_UL", 0)
            BI_Limit.BI_Low_PA_Temp_LL = SW_Config_XML.Read("Limit", "BI_Low_PA_Temp_LL", 0)
            BI_Limit.BI_High_PA_VSWR_Temp_UL = SW_Config_XML.Read("Limit", "BI_High_PA_VSWR_Temp_UL", 0)
            BI_Limit.BI_High_PA_VSWR_Temp_LL = SW_Config_XML.Read("Limit", "BI_High_PA_VSWR_Temp_LL", 0)
            BI_Limit.BI_Low_PA_VSWR_Temp_UL = SW_Config_XML.Read("Limit", "BI_Low_PA_VSWR_Temp_UL", 0)
            BI_Limit.BI_Low_PA_VSWR_Temp_LL = SW_Config_XML.Read("Limit", "BI_Low_PA_VSWR_Temp_LL", 0)

            BI_Limit.BI_PA_Temp_Delta_UL = SW_Config_XML.Read("Limit", "BI_PA_Temp_Delta_UL", 0)
            BI_Limit.BI_PA_Temp_Delta_LL = SW_Config_XML.Read("Limit", "BI_PA_Temp_Delta_LL", 0)

            BI_Limit.BI_High_LNA01_Temp_UL = SW_Config_XML.Read("Limit", "BI_High_LNA01_Temp_UL", 0)
            BI_Limit.BI_High_LNA01_Temp_LL = SW_Config_XML.Read("Limit", "BI_High_LNA01_Temp_LL", 0)
            BI_Limit.BI_Low_LNA01_Temp_UL = SW_Config_XML.Read("Limit", "BI_Low_LNA01_Temp_UL", 0)
            BI_Limit.BI_Low_LNA01_Temp_LL = SW_Config_XML.Read("Limit", "BI_Low_LNA01_Temp_LL", 0)
            BI_Limit.BI_High_LNA23_Temp_UL = SW_Config_XML.Read("Limit", "BI_High_LNA23_Temp_UL", 0)
            BI_Limit.BI_High_LNA23_Temp_LL = SW_Config_XML.Read("Limit", "BI_High_LNA23_Temp_LL", 0)
            BI_Limit.BI_Low_LNA23_Temp_UL = SW_Config_XML.Read("Limit", "BI_Low_LNA23_Temp_UL", 0)
            BI_Limit.BI_Low_LNA23_Temp_LL = SW_Config_XML.Read("Limit", "BI_Low_LNA23_Temp_LL", 0)
            BI_Limit.BI_High_PS_Converter_Temp_UL = SW_Config_XML.Read("Limit", "BI_High_PS_Converter_Temp_UL", 0)
            BI_Limit.BI_High_PS_Converter_Temp_LL = SW_Config_XML.Read("Limit", "BI_High_PS_Converter_Temp_LL", 0)
            BI_Limit.BI_Low_PS_Converter_Temp_UL = SW_Config_XML.Read("Limit", "BI_Low_PS_Converter_Temp_UL", 0)
            BI_Limit.BI_Low_PS_Converter_Temp_LL = SW_Config_XML.Read("Limit", "BI_Low_PS_Converter_Temp_LL", 0)
            BI_Limit.BI_High_PS_Brick_Temp_UL = SW_Config_XML.Read("Limit", "BI_High_PS_Brick_Temp_UL", 0)
            BI_Limit.BI_High_PS_Brick_Temp_LL = SW_Config_XML.Read("Limit", "BI_High_PS_Brick_Temp_LL", 0)
            BI_Limit.BI_Low_PS_Brick_Temp_UL = SW_Config_XML.Read("Limit", "BI_Low_PS_Brick_Temp_UL", 0)
            BI_Limit.BI_Low_PS_Brick_Temp_LL = SW_Config_XML.Read("Limit", "BI_Low_PS_Brick_Temp_LL", 0)

            BI_Limit.BI_PSU_Temp_Delta_UL = SW_Config_XML.Read("Limit", "BI_PSU_Temp_Delta_UL", 0)
            BI_Limit.BI_PSU_Temp_Delta_LL = SW_Config_XML.Read("Limit", "BI_PSU_Temp_Delta_LL", 0)
            ' BI_Limit.BI_PSU_PA_Temp_Delta_UL = SW_Config_XML.Read("Limit", "BI_PSU_PA_Temp_Delta_UL", 0)
            ' BI_Limit.BI_PSU_PA_Temp_Delta_LL = SW_Config_XML.Read("Limit", "BI_PSU_PA_Temp_Delta_LL", 0)

            BI_Limit.BI_High_PSU_PA_Temp_Delta_UL = SW_Config_XML.Read("Limit", "BI_High_PSU_PA_Temp_Delta_UL", 0)
            BI_Limit.BI_High_PSU_PA_Temp_Delta_LL = SW_Config_XML.Read("Limit", "BI_High_PSU_PA_Temp_Delta_LL", 0)
            BI_Limit.BI_Low_PSU_PA_Temp_Delta_UL = SW_Config_XML.Read("Limit", "BI_Low_PSU_PA_Temp_Delta_UL", 0)
            BI_Limit.BI_Low_PSU_PA_Temp_Delta_LL = SW_Config_XML.Read("Limit", "BI_Low_PSU_PA_Temp_Delta_LL", 0)

            BI_Limit.BI_High_FB_Temp_UL = SW_Config_XML.Read("Limit", "BI_High_FB_Temp_UL", 0)
            BI_Limit.BI_High_FB_Temp_LL = SW_Config_XML.Read("Limit", "BI_High_FB_Temp_LL", 0)
            BI_Limit.BI_Low_FB_Temp_UL = SW_Config_XML.Read("Limit", "BI_Low_FB_Temp_UL", 0)
            BI_Limit.BI_Low_FB_Temp_LL = SW_Config_XML.Read("Limit", "BI_Low_FB_Temp_LL", 0)
            BI_Limit.BI_High_RX_Temp_UL = SW_Config_XML.Read("Limit", "BI_High_RX_Temp_UL", 0)
            BI_Limit.BI_High_RX_Temp_LL = SW_Config_XML.Read("Limit", "BI_High_RX_Temp_LL", 0)
            BI_Limit.BI_Low_RX_Temp_UL = SW_Config_XML.Read("Limit", "BI_Low_RX_Temp_UL", 0)
            BI_Limit.BI_Low_RX_Temp_LL = SW_Config_XML.Read("Limit", "BI_Low_RX_Temp_LL", 0)

            'TX_High, UL
            BI_Limit.BI_High_TX_Output_Pow_UL = SW_Config_XML.Read("Limit", "BI_High_TX_Output_Pow_UL", 0)
            BI_Limit.BI_High_TX_VSWR_UL = SW_Config_XML.Read("Limit", "BI_High_TX_VSWR_UL", 0)
            BI_Limit.BI_High_TX_Forward_Power_Detector_UL = SW_Config_XML.Read("Limit", "BI_High_TX_Forward_Power_Detector_UL", 0)
            BI_Limit.BI_High_TX_Reverse_Power_Detector_UL = SW_Config_XML.Read("Limit", "BI_High_TX_Reverse_Power_Detector_UL", 0)
            BI_Limit.BI_High_TX_Gain_VCA_UL = SW_Config_XML.Read("Limit", "BI_High_TX_Gain_VCA_UL", 0)
            BI_Limit.BI_High_TX_txDacGain_UL = SW_Config_XML.Read("Limit", "BI_High_TX_txDacGain_UL", 0)
            BI_Limit.BI_High_TX_totalTxAttn_UL = SW_Config_XML.Read("Limit", "BI_High_TX_totalTxAttn_UL", 0)
            BI_Limit.BI_High_TX_Gain_TxStep_UL = SW_Config_XML.Read("Limit", "BI_High_TX_Gain_TxStep_UL", 0)
            BI_Limit.BI_High_TX_Gain_FbStep_UL = SW_Config_XML.Read("Limit", "BI_High_TX_Gain_FbStep_UL", 0)
            BI_Limit.BI_High_TX_Gain_FbTxQuo_UL = SW_Config_XML.Read("Limit", "BI_High_TX_Gain_FbTxQuo_UL", 0)
            BI_Limit.BI_High_TX_Gain_GainError_UL = SW_Config_XML.Read("Limit", "BI_High_TX_Gain_GainError_UL", 0)
            BI_Limit.BI_High_TX_PA_PsVolt_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_PsVolt_UL", 0)
            BI_Limit.BI_High_TX_PA_Temp_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Temp_UL", 0)
            BI_Limit.BI_High_TX_PA_BiasTemp_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_BiasTemp_UL", 0)
            BI_Limit.BI_High_TX_PA_Driver1Cur_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver1Cur_UL", 0)
            BI_Limit.BI_High_TX_PA_Driver2Cur_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver2Cur_UL", 0)
            BI_Limit.BI_High_TX_PA_Driver3Cur_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver3Cur_UL", 0)
            BI_Limit.BI_High_TX_PA_Driver4Cur_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver4Cur_UL", 0)
            BI_Limit.BI_High_TX_PA_Final1Cur_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Final1Cur_UL", 0)
            BI_Limit.BI_High_TX_PA_Final2Cur_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Final2Cur_UL", 0)
            BI_Limit.BI_High_TX_PA_Driver1Dac_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver1Dac_UL", 0)
            BI_Limit.BI_High_TX_PA_Driver2Dac_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver2Dac_UL", 0)
            BI_Limit.BI_High_TX_PA_Driver3Dac_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver3Dac_UL", 0)
            BI_Limit.BI_High_TX_PA_Driver4Dac_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver4Dac_UL", 0)
            BI_Limit.BI_High_TX_PA_Final1Dac_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Final1Dac_UL", 0)
            BI_Limit.BI_High_TX_PA_Final2Dac_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Final2Dac_UL", 0)
            BI_Limit.BI_High_TX_PA_ampDelayInt_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_ampDelayInt_UL", 0)
            BI_Limit.BI_High_TX_PA_ampDelayFrac_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_ampDelayFrac_UL", 0)
            BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_UL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_MaxCrossCorrelation_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L1_Table_Max_Gain_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L1_Table_Min_Gain_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_3rd_sym_am_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_3rd_sym_ph_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L3_3rd_sym_am_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L3_3rd_sym_ph_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_5th_sym_am_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_5th_sym_ph_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L3_5th_sym_am_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L3_5th_sym_ph_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_3rd_asym_am_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_3rd_asym_ph_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_5th_asym_am_UL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_UL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_5th_asym_ph_UL", 0)

            'TX_High, LL
            BI_Limit.BI_High_TX_Output_Pow_LL = SW_Config_XML.Read("Limit", "BI_High_TX_Output_Pow_LL", 0)
            BI_Limit.BI_High_TX_VSWR_LL = SW_Config_XML.Read("Limit", "BI_High_TX_VSWR_LL", 0)
            BI_Limit.BI_High_TX_Forward_Power_Detector_LL = SW_Config_XML.Read("Limit", "BI_High_TX_Forward_Power_Detector_LL", 0)
            BI_Limit.BI_High_TX_Reverse_Power_Detector_LL = SW_Config_XML.Read("Limit", "BI_High_TX_Reverse_Power_Detector_LL", 0)
            BI_Limit.BI_High_TX_Gain_VCA_LL = SW_Config_XML.Read("Limit", "BI_High_TX_Gain_VCA_LL", 0)
            BI_Limit.BI_High_TX_txDacGain_LL = SW_Config_XML.Read("Limit", "BI_High_TX_txDacGain_LL", 0)
            BI_Limit.BI_High_TX_totalTxAttn_LL = SW_Config_XML.Read("Limit", "BI_High_TX_totalTxAttn_LL", 0)
            BI_Limit.BI_High_TX_Gain_TxStep_LL = SW_Config_XML.Read("Limit", "BI_High_TX_Gain_TxStep_LL", 0)
            BI_Limit.BI_High_TX_Gain_FbStep_LL = SW_Config_XML.Read("Limit", "BI_High_TX_Gain_FbStep_LL", 0)
            BI_Limit.BI_High_TX_Gain_FbTxQuo_LL = SW_Config_XML.Read("Limit", "BI_High_TX_Gain_FbTxQuo_LL", 0)
            BI_Limit.BI_High_TX_Gain_GainError_LL = SW_Config_XML.Read("Limit", "BI_High_TX_Gain_GainError_LL", 0)
            BI_Limit.BI_High_TX_PA_PsVolt_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_PsVolt_LL", 0)
            BI_Limit.BI_High_TX_PA_Temp_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Temp_LL", 0)
            BI_Limit.BI_High_TX_PA_BiasTemp_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_BiasTemp_LL", 0)
            BI_Limit.BI_High_TX_PA_Driver1Cur_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver1Cur_LL", 0)
            BI_Limit.BI_High_TX_PA_Driver2Cur_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver2Cur_LL", 0)
            BI_Limit.BI_High_TX_PA_Driver3Cur_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver3Cur_LL", 0)
            BI_Limit.BI_High_TX_PA_Driver4Cur_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver4Cur_LL", 0)
            BI_Limit.BI_High_TX_PA_Final1Cur_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Final1Cur_LL", 0)
            BI_Limit.BI_High_TX_PA_Final2Cur_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Final2Cur_LL", 0)
            BI_Limit.BI_High_TX_PA_Driver1Dac_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver1Dac_LL", 0)
            BI_Limit.BI_High_TX_PA_Driver2Dac_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver2Dac_LL", 0)
            BI_Limit.BI_High_TX_PA_Driver3Dac_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver3Dac_LL", 0)
            BI_Limit.BI_High_TX_PA_Driver4Dac_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Driver4Dac_LL", 0)
            BI_Limit.BI_High_TX_PA_Final1Dac_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Final1Dac_LL", 0)
            BI_Limit.BI_High_TX_PA_Final2Dac_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_Final2Dac_LL", 0)
            BI_Limit.BI_High_TX_PA_ampDelayInt_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_ampDelayInt_LL", 0)
            BI_Limit.BI_High_TX_PA_ampDelayFrac_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_ampDelayFrac_LL", 0)
            BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_LL = SW_Config_XML.Read("Limit", "BI_High_TX_PA_MaxCrossCorrelation_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L1_Table_Max_Gain_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L1_Table_Min_Gain_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_3rd_sym_am_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_3rd_sym_ph_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L3_3rd_sym_am_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L3_3rd_sym_ph_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_5th_sym_am_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_5th_sym_ph_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L3_5th_sym_am_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L3_5th_sym_ph_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_3rd_asym_am_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_3rd_asym_ph_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_5th_asym_am_LL", 0)
            BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_LL = SW_Config_XML.Read("Limit", "BI_High_Tx_DPD_L2_5th_asym_ph_LL", 0)

            'TX_Low, UL
            BI_Limit.BI_Low_TX_Output_Pow_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_Output_Pow_UL", 0)
            BI_Limit.BI_Low_TX_VSWR_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_VSWR_UL", 0)
            BI_Limit.BI_Low_TX_Forward_Power_Detector_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_Forward_Power_Detector_UL", 0)
            BI_Limit.BI_Low_TX_Reverse_Power_Detector_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_Reverse_Power_Detector_UL", 0)
            BI_Limit.BI_Low_TX_Gain_VCA_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_Gain_VCA_UL", 0)
            BI_Limit.BI_Low_TX_txDacGain_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_txDacGain_UL", 0)
            BI_Limit.BI_Low_TX_totalTxAttn_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_totalTxAttn_UL", 0)
            BI_Limit.BI_Low_TX_Gain_TxStep_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_Gain_TxStep_UL", 0)
            BI_Limit.BI_Low_TX_Gain_FbStep_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_Gain_FbStep_UL", 0)
            BI_Limit.BI_Low_TX_Gain_FbTxQuo_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_Gain_FbTxQuo_UL", 0)
            BI_Limit.BI_Low_TX_Gain_GainError_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_Gain_GainError_UL", 0)
            BI_Limit.BI_Low_TX_PA_PsVolt_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_PsVolt_UL", 0)
            BI_Limit.BI_Low_TX_PA_Temp_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Temp_UL", 0)
            BI_Limit.BI_Low_TX_PA_BiasTemp_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_BiasTemp_UL", 0)
            BI_Limit.BI_Low_TX_PA_Driver1Cur_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver1Cur_UL", 0)
            BI_Limit.BI_Low_TX_PA_Driver2Cur_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver2Cur_UL", 0)
            BI_Limit.BI_Low_TX_PA_Driver3Cur_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver3Cur_UL", 0)
            BI_Limit.BI_Low_TX_PA_Driver4Cur_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver4Cur_UL", 0)
            BI_Limit.BI_Low_TX_PA_Final1Cur_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Final1Cur_UL", 0)
            BI_Limit.BI_Low_TX_PA_Final2Cur_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Final2Cur_UL", 0)
            BI_Limit.BI_Low_TX_PA_Driver1Dac_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver1Dac_UL", 0)
            BI_Limit.BI_Low_TX_PA_Driver2Dac_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver2Dac_UL", 0)
            BI_Limit.BI_Low_TX_PA_Driver3Dac_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver3Dac_UL", 0)
            BI_Limit.BI_Low_TX_PA_Driver4Dac_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver4Dac_UL", 0)
            BI_Limit.BI_Low_TX_PA_Final1Dac_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Final1Dac_UL", 0)
            BI_Limit.BI_Low_TX_PA_Final2Dac_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Final2Dac_UL", 0)
            BI_Limit.BI_Low_TX_PA_ampDelayInt_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_ampDelayInt_UL", 0)
            BI_Limit.BI_Low_TX_PA_ampDelayFrac_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_ampDelayFrac_UL", 0)
            BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_UL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_MaxCrossCorrelation_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L1_Table_Max_Gain_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L1_Table_Min_Gain_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_3rd_sym_am_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_3rd_sym_ph_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L3_3rd_sym_am_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L3_3rd_sym_ph_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_5th_sym_am_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_5th_sym_ph_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L3_5th_sym_am_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L3_5th_sym_ph_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_3rd_asym_am_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_3rd_asym_ph_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_5th_asym_am_UL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_UL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_5th_asym_ph_UL", 0)

            'TX_Low, LL
            BI_Limit.BI_Low_TX_Output_Pow_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_Output_Pow_LL", 0)
            BI_Limit.BI_Low_TX_VSWR_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_VSWR_LL", 0)
            BI_Limit.BI_Low_TX_Forward_Power_Detector_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_Forward_Power_Detector_LL", 0)
            BI_Limit.BI_Low_TX_Reverse_Power_Detector_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_Reverse_Power_Detector_LL", 0)
            BI_Limit.BI_Low_TX_Gain_VCA_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_Gain_VCA_LL", 0)
            BI_Limit.BI_Low_TX_txDacGain_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_txDacGain_LL", 0)
            BI_Limit.BI_Low_TX_totalTxAttn_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_totalTxAttn_LL", 0)
            BI_Limit.BI_Low_TX_Gain_TxStep_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_Gain_TxStep_LL", 0)
            BI_Limit.BI_Low_TX_Gain_FbStep_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_Gain_FbStep_LL", 0)
            BI_Limit.BI_Low_TX_Gain_FbTxQuo_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_Gain_FbTxQuo_LL", 0)
            BI_Limit.BI_Low_TX_Gain_GainError_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_Gain_GainError_LL", 0)
            BI_Limit.BI_Low_TX_PA_PsVolt_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_PsVolt_LL", 0)
            BI_Limit.BI_Low_TX_PA_Temp_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Temp_LL", 0)
            BI_Limit.BI_Low_TX_PA_BiasTemp_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_BiasTemp_LL", 0)
            BI_Limit.BI_Low_TX_PA_Driver1Cur_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver1Cur_LL", 0)
            BI_Limit.BI_Low_TX_PA_Driver2Cur_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver2Cur_LL", 0)
            BI_Limit.BI_Low_TX_PA_Driver3Cur_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver3Cur_LL", 0)
            BI_Limit.BI_Low_TX_PA_Driver4Cur_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver4Cur_LL", 0)
            BI_Limit.BI_Low_TX_PA_Final1Cur_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Final1Cur_LL", 0)
            BI_Limit.BI_Low_TX_PA_Final2Cur_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Final2Cur_LL", 0)
            BI_Limit.BI_Low_TX_PA_Driver1Dac_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver1Dac_LL", 0)
            BI_Limit.BI_Low_TX_PA_Driver2Dac_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver2Dac_LL", 0)
            BI_Limit.BI_Low_TX_PA_Driver3Dac_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver3Dac_LL", 0)
            BI_Limit.BI_Low_TX_PA_Driver4Dac_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Driver4Dac_LL", 0)
            BI_Limit.BI_Low_TX_PA_Final1Dac_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Final1Dac_LL", 0)
            BI_Limit.BI_Low_TX_PA_Final2Dac_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_Final2Dac_LL", 0)
            BI_Limit.BI_Low_TX_PA_ampDelayInt_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_ampDelayInt_LL", 0)
            BI_Limit.BI_Low_TX_PA_ampDelayFrac_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_ampDelayFrac_LL", 0)
            BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_LL = SW_Config_XML.Read("Limit", "BI_Low_TX_PA_MaxCrossCorrelation_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L1_Table_Max_Gain_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L1_Table_Min_Gain_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_3rd_sym_am_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_3rd_sym_ph_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L3_3rd_sym_am_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L3_3rd_sym_ph_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_5th_sym_am_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_5th_sym_ph_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L3_5th_sym_am_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L3_5th_sym_ph_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_3rd_asym_am_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_3rd_asym_ph_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_5th_asym_am_LL", 0)
            BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_LL = SW_Config_XML.Read("Limit", "BI_Low_Tx_DPD_L2_5th_asym_ph_LL", 0)

            'RX_High, UL
            BI_Limit.BI_High_Rx_LNA_Atten_UL = SW_Config_XML.Read("Limit", "BI_High_Rx_LNA_Atten_UL", 0)
            BI_Limit.BI_High_Rx_Rx_Atten0_UL = SW_Config_XML.Read("Limit", "BI_High_Rx_Rx_Atten0_UL", 0)
            BI_Limit.BI_High_Rx_Rx_Atten1_UL = SW_Config_XML.Read("Limit", "BI_High_Rx_Rx_Atten1_UL", 0)
            BI_Limit.BI_High_Rx_RSSI_C0_UL = SW_Config_XML.Read("Limit", "BI_High_Rx_RSSI_C0_UL", 0)
            BI_Limit.BI_High_Rx_RSSI_C1_UL = SW_Config_XML.Read("Limit", "BI_High_Rx_RSSI_C1_UL", 0)

            'RX_High, LL
            BI_Limit.BI_High_Rx_LNA_Atten_LL = SW_Config_XML.Read("Limit", "BI_High_Rx_LNA_Atten_LL", 0)
            BI_Limit.BI_High_Rx_Rx_Atten0_LL = SW_Config_XML.Read("Limit", "BI_High_Rx_Rx_Atten0_LL", 0)
            BI_Limit.BI_High_Rx_Rx_Atten1_LL = SW_Config_XML.Read("Limit", "BI_High_Rx_Rx_Atten1_LL", 0)
            BI_Limit.BI_High_Rx_RSSI_C0_LL = SW_Config_XML.Read("Limit", "BI_High_Rx_RSSI_C0_LL", 0)
            BI_Limit.BI_High_Rx_RSSI_C1_LL = SW_Config_XML.Read("Limit", "BI_High_Rx_RSSI_C1_LL", 0)

            'RX_Low, UL
            BI_Limit.BI_Low_Rx_LNA_Atten_UL = SW_Config_XML.Read("Limit", "BI_Low_Rx_LNA_Atten_UL", 0)
            BI_Limit.BI_Low_Rx_Rx_Atten0_UL = SW_Config_XML.Read("Limit", "BI_Low_Rx_Rx_Atten0_UL", 0)
            BI_Limit.BI_Low_Rx_Rx_Atten1_UL = SW_Config_XML.Read("Limit", "BI_Low_Rx_Rx_Atten1_UL", 0)
            BI_Limit.BI_Low_Rx_RSSI_C0_UL = SW_Config_XML.Read("Limit", "BI_Low_Rx_RSSI_C0_UL", 0)
            BI_Limit.BI_Low_Rx_RSSI_C1_UL = SW_Config_XML.Read("Limit", "BI_Low_Rx_RSSI_C1_UL", 0)

            'RX_Low, LL
            BI_Limit.BI_Low_Rx_LNA_Atten_LL = SW_Config_XML.Read("Limit", "BI_Low_Rx_LNA_Atten_LL", 0)
            BI_Limit.BI_Low_Rx_Rx_Atten0_LL = SW_Config_XML.Read("Limit", "BI_Low_Rx_Rx_Atten0_LL", 0)
            BI_Limit.BI_Low_Rx_Rx_Atten1_LL = SW_Config_XML.Read("Limit", "BI_Low_Rx_Rx_Atten1_LL", 0)
            BI_Limit.BI_Low_Rx_RSSI_C0_LL = SW_Config_XML.Read("Limit", "BI_Low_Rx_RSSI_C0_LL", 0)
            BI_Limit.BI_Low_Rx_RSSI_C1_LL = SW_Config_XML.Read("Limit", "BI_Low_Rx_RSSI_C1_LL", 0)

            'PS
            BI_Limit.BI_High_Input_Voltage_UL = SW_Config_XML.Read("Limit", "BI_High_Input_Voltage_UL", 0)
            BI_Limit.BI_High_Input_Current_UL = SW_Config_XML.Read("Limit", "BI_High_Input_Current_UL", 0)
            BI_Limit.BI_High_Input_Power_UL = SW_Config_XML.Read("Limit", "BI_High_Input_Power_UL", 0)
            BI_Limit.BI_High_Output_Voltage_UL = SW_Config_XML.Read("Limit", "BI_High_Output_Voltage_UL", 0)
            BI_Limit.BI_High_AISG_12V_Voltage_UL = SW_Config_XML.Read("Limit", "BI_High_AISG_12V_Voltage_UL", 0)
            BI_Limit.BI_High_AISG_12V_Current_UL = SW_Config_XML.Read("Limit", "BI_High_AISG_12V_Current_UL", 0)
            BI_Limit.BI_High_AISG_24V_Voltage_UL = SW_Config_XML.Read("Limit", "BI_High_AISG_24V_Voltage_UL", 0)
            BI_Limit.BI_High_AISG_24V_Current_UL = SW_Config_XML.Read("Limit", "BI_High_AISG_24V_Current_UL", 0)

            BI_Limit.BI_High_Input_Voltage_LL = SW_Config_XML.Read("Limit", "BI_High_Input_Voltage_LL", 0)
            BI_Limit.BI_High_Input_Current_LL = SW_Config_XML.Read("Limit", "BI_High_Input_Current_LL", 0)
            BI_Limit.BI_High_Input_Power_LL = SW_Config_XML.Read("Limit", "BI_High_Input_Power_LL", 0)
            BI_Limit.BI_High_Output_Voltage_LL = SW_Config_XML.Read("Limit", "BI_High_Output_Voltage_LL", 0)
            BI_Limit.BI_High_AISG_12V_Voltage_LL = SW_Config_XML.Read("Limit", "BI_High_AISG_12V_Voltage_LL", 0)
            BI_Limit.BI_High_AISG_12V_Current_LL = SW_Config_XML.Read("Limit", "BI_High_AISG_12V_Current_LL", 0)
            BI_Limit.BI_High_AISG_24V_Voltage_LL = SW_Config_XML.Read("Limit", "BI_High_AISG_24V_Voltage_LL", 0)
            BI_Limit.BI_High_AISG_24V_Current_LL = SW_Config_XML.Read("Limit", "BI_High_AISG_24V_Current_LL", 0)

            BI_Limit.BI_Low_Input_Voltage_UL = SW_Config_XML.Read("Limit", "BI_Low_Input_Voltage_UL", 0)
            BI_Limit.BI_Low_Input_Current_UL = SW_Config_XML.Read("Limit", "BI_Low_Input_Current_UL", 0)
            BI_Limit.BI_Low_Input_Power_UL = SW_Config_XML.Read("Limit", "BI_Low_Input_Power_UL", 0)
            BI_Limit.BI_Low_Output_Voltage_UL = SW_Config_XML.Read("Limit", "BI_Low_Output_Voltage_UL", 0)
            BI_Limit.BI_Low_AISG_12V_Voltage_UL = SW_Config_XML.Read("Limit", "BI_Low_AISG_12V_Voltage_UL", 0)
            BI_Limit.BI_Low_AISG_12V_Current_UL = SW_Config_XML.Read("Limit", "BI_Low_AISG_12V_Current_UL", 0)
            BI_Limit.BI_Low_AISG_24V_Voltage_UL = SW_Config_XML.Read("Limit", "BI_Low_AISG_24V_Voltage_UL", 0)
            BI_Limit.BI_Low_AISG_24V_Current_UL = SW_Config_XML.Read("Limit", "BI_Low_AISG_24V_Current_UL", 0)

            BI_Limit.BI_Low_Input_Voltage_LL = SW_Config_XML.Read("Limit", "BI_Low_Input_Voltage_LL", 0)
            BI_Limit.BI_Low_Input_Current_LL = SW_Config_XML.Read("Limit", "BI_Low_Input_Current_LL", 0)
            BI_Limit.BI_Low_Input_Power_LL = SW_Config_XML.Read("Limit", "BI_Low_Input_Power_LL", 0)
            BI_Limit.BI_Low_Output_Voltage_LL = SW_Config_XML.Read("Limit", "BI_Low_Output_Voltage_LL", 0)
            BI_Limit.BI_Low_AISG_12V_Voltage_LL = SW_Config_XML.Read("Limit", "BI_Low_AISG_12V_Voltage_LL", 0)
            BI_Limit.BI_Low_AISG_12V_Current_LL = SW_Config_XML.Read("Limit", "BI_Low_AISG_12V_Current_LL", 0)
            BI_Limit.BI_Low_AISG_24V_Voltage_LL = SW_Config_XML.Read("Limit", "BI_Low_AISG_24V_Voltage_LL", 0)
            BI_Limit.BI_Low_AISG_24V_Current_LL = SW_Config_XML.Read("Limit", "BI_Low_AISG_24V_Current_LL", 0)
            'Read ODC Settings
            BI_Param.ODC_Check = UCase(SW_Config_XML.Read("Parameters", "Performance_ODC_Check", BLK))
            BI_Param.Transfer_Data = UCase(SW_Config_XML.Read("Parameters", "Transfer_Data", BLK))
            'Delta CAM current
            BI_Limit.BI_Delta_TX_PA_Driver1Cur_LL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Driver1Cur_LL", BLK)
            BI_Limit.BI_Delta_TX_PA_Driver1Cur_UL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Driver1Cur_UL", BLK)
            BI_Limit.BI_Delta_TX_PA_Driver2Cur_LL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Driver2Cur_LL", BLK)
            BI_Limit.BI_Delta_TX_PA_Driver2Cur_UL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Driver2Cur_UL", BLK)
            BI_Limit.BI_Delta_TX_PA_Driver3Cur_LL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Driver3Cur_LL", BLK)
            BI_Limit.BI_Delta_TX_PA_Driver3Cur_UL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Driver3Cur_UL", BLK)
            BI_Limit.BI_Delta_TX_PA_Driver4Cur_LL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Driver4Cur_LL", BLK)
            BI_Limit.BI_Delta_TX_PA_Driver4Cur_UL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Driver4Cur_UL", BLK)
            BI_Limit.BI_Delta_TX_PA_Final1Cur_LL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Final1Cur_LL", BLK)
            BI_Limit.BI_Delta_TX_PA_Final1Cur_UL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Final1Cur_UL", BLK)
            BI_Limit.BI_Delta_TX_PA_Final2Cur_LL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Final2Cur_LL", BLK)
            BI_Limit.BI_Delta_TX_PA_Final2Cur_UL = SW_Config_XML.Read("Limit", "BI_Delta_TX_PA_Final2Cur_UL", BLK)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub


    Public Sub SaveConfigXML()
        Try
            'Save Hardware configuration

            HW_Config_XML.Save("HW_Config" & SP & "PS_Manual_Setting", "PS_Manual_Control", BI_HW.PS_Manual_Control)

            HW_Config_XML.Save("HW_Config" & SP & "Kanban_System", "Kanban_System_Upload", BI_HW.Kanban_System_Upload)

            HW_Config_XML.Save("HW_Config" & SP & "Test_Plant_Setting", "Test_Plant_PC", BI_HW.PlantPC)
            HW_Config_XML.Save("HW_Config" & SP & "Test_Plant_Setting", "Test_Plant", BI_HW.Plant)

            'Power Switch control function of Net-Switch 
            HW_Config_XML.Save("HW_Config" & SP & "Net_Switch_Setting", "Switch_PS_Enabled", BI_HW.Switch_PS_Enabled)
            HW_Config_XML.Save("HW_Config" & SP & "Net_Switch_Setting", "Switch_PS_Type", BI_HW.Switch_PS_Type)
            HW_Config_XML.Save("HW_Config" & SP & "Net_Switch_Setting", "Switch_PS_COM_Port", BI_HW.Switch_PS_COM_Port)
            HW_Config_XML.Save("HW_Config" & SP & "Net_Switch_Setting", "Switch_PS_Channel", BI_HW.Switch_PS_Channel)

            HW_Config_XML.Save("HW_Config" & SP & "Power_Switch_Setting", "Power_Switch_Enabled", BI_HW.Pwr_Chan_Enabled)
            HW_Config_XML.Save("HW_Config" & SP & "Power_Switch_Setting", "Power_Switch_Type", BI_HW.Pwr_Switch_Type)
            HW_Config_XML.Save("HW_Config" & SP & "Power_Switch_Setting", "Power_Switch_Relay_Port", BI_HW.Pwr_Port_Number)
            For i As Integer = 0 To SlotNum
                HW_Config_XML.Save("HW_Config" & SP & "Power_Switch_Setting", "Power_Channel_" & (i + 1 + Shelf2Slot).ToString, BI_HW.Pwr_Chan(i))
            Next

            HW_Config_XML.Save("HW_Config" & SP & "Anton_Switch_Setting", "Anton_Switch_Enabled", BI_HW.Anton_Chan_Enabled)
            HW_Config_XML.Save("HW_Config" & SP & "Anton_Switch_Setting", "Anton_Switch_Type", BI_HW.Anton_Switch_Type)
            HW_Config_XML.Save("HW_Config" & SP & "Anton_Switch_Setting", "Anton_Switch_Relay_Port", BI_HW.Anton_Port_Number)
            For i As Integer = 0 To SlotNum
                HW_Config_XML.Save("HW_Config" & SP & "Anton_Switch_Setting", "Anton_Channel_" & (i + 1 + Shelf2Slot).ToString, BI_HW.Anton_Chan(i))
            Next

            HW_Config_XML.Save("HW_Config" & SP & "Fan_Switch_Setting", "Fan_Switch_Enabled", BI_HW.Fan_Chan_Enabled)
            HW_Config_XML.Save("HW_Config" & SP & "Fan_Switch_Setting", "Fan_Switch_Type", BI_HW.Fan_Switch_Type)
            HW_Config_XML.Save("HW_Config" & SP & "Fan_Switch_Setting", "Fan_Switch_COM_Port", BI_HW.Fan_Port_Number)
            For i As Integer = 0 To SlotNum
                HW_Config_XML.Save("HW_Config" & SP & "Fan_Switch_Setting", "Fan_Channel_" & (i + 1).ToString, BI_HW.Fan_Chan(i))
            Next


            For I As Integer = 0 To 5
                HW_Config_XML.Save("HW_Config" & SP & "Power_Supplier_Setting" & SP & "PS_" & I + 1, "PS_Control_Enabled", BI_HW.PS_Control_Enabled(I))
                HW_Config_XML.Save("HW_Config" & SP & "Power_Supplier_Setting" & SP & "PS_" & I + 1, "PS_Control_Type", BI_HW.PS_Control_Type(I))
                HW_Config_XML.Save("HW_Config" & SP & "Power_Supplier_Setting" & SP & "PS_" & I + 1, "PS_Control_Address", BI_HW.PS_Control_Address(I))
                HW_Config_XML.Save("HW_Config" & SP & "Power_Supplier_Setting" & SP & "PS_" & I + 1, "PS_Voltage", BI_HW.PS_Voltage(I))
                HW_Config_XML.Save("HW_Config" & SP & "Power_Supplier_Setting" & SP & "PS_" & I + 1, "PS_Current", BI_HW.PS_Current(I))
            Next

            HW_Config_XML.Save("HW_Config" & SP & "Chamber_Setting", "Chamber_Enabled", BI_HW.Chamber_Control_Enabled)
            HW_Config_XML.Save("HW_Config" & SP & "Chamber_Setting", "Chamber_COM_Port", BI_HW.Chamber_Port_Number)
            HW_Config_XML.Save("HW_Config" & SP & "Chamber_Setting", "Chamber_Pattern", BI_HW.Chamber_Run_Pattern)

            For i As Integer = 0 To 9
                HW_Config_XML.Save("HW_Config" & SP & "Chamber_Setting", "Step" & (i + 1).ToString & "_Temp", BI_HW.Chamber_Step(i).Temp)
                HW_Config_XML.Save("HW_Config" & SP & "Chamber_Setting", "Step" & (i + 1).ToString & "_Time", BI_HW.Chamber_Step(i).Time)
                HW_Config_XML.Save("HW_Config" & SP & "Chamber_Setting", "Step" & (i + 1).ToString & "_PID", BI_HW.Chamber_Step(i).PID)
            Next

            'Save Unit configuration
            HW_Config_XML.Save("Unit_Config", "ConnectionType", BI_HW.ConnectionType)
            For i As Integer = 0 To SlotNum
                'HW_Config_XML.Save("Unit_Config" & SP & "IP_Address", "IP_Address_" & (i + 1).ToString, BI_HW.IPAddress(i))
                HW_Config_XML.Save("Unit_Config" & SP & "IP_Address", "IP_Address_" & (i + 1 + Shelf2Slot).ToString, BI_HW.IPAddress(i))
            Next

            'Save ODC Settings
            SW_Config_XML.Save("Parameters", "Performance_ODC_Check", BI_Param.ODC_Check)
            SW_Config_XML.Save("Parameters", "Transfer_Data", BI_Param.Transfer_Data)
            SW_Config_XML.Save("Parameters", "File_Save_Path", BI_Param.File_Save_Path)

            'SW_Config_XML.Save("Parameters", "Matlab_Check_Enabled", BI_Param.Matlab_Check_Enabled)
            'SW_Config_XML.Save("Parameters", "Matlab_Picture_Enabled", BI_Param.Matlab_Picture_Enabled)
            ''SW_Config_XML.Save("Parameters", "Matlab_Program_Path", BI_Param.Matlab_Program_Path)
            'SW_Config_XML.Save("Parameters", "Matlab_Picture_Comments", BI_Param.Matlab_Picture_Comments)
            'SW_Config_XML.Save("Parameters", "Matlab_Picture_Save_Path", BI_Param.Matlab_Picture_Save_Path)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

#End Region

#Region "Reset Functions"

    Public Sub ResetTestTimeIndicator()
        frmMain.totTestTimeOut.DisplayMinutes = True
        frmMain.totTestTimeOut.MinutesDelay = BI_Profile.BI_Duration
        frmMain.totTestTimeOut.Reset()
        frmMain.totTestTimeOut.Start()
        For slot As Integer = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                BI_Data(slot).RecData.StartTime = Now()
            End If
        Next
    End Sub

    Public Sub Reset_State()

        CleanRecData()
        CleanMeasData()
        CleanUnitInfo()
        Del_DCF()
        CleanMessageInfo()

        TurnSwitchPSOnOff(False)
        TurnSwitchPSOnOff(True)

        For slot As Integer = 0 To SlotNum
            TurnPowerOnOff(slot, False)
            TurnFanOnOff(slot, False)
            TurnAntonOnOff(slot, False)
            KanbanRealTimeUpload(slot, "Idle")
        Next
        ManualTurnPowerOnOff(False)

        TurnAllPSOnOff(False)
        ResetSlotsDGV()
        ResetAllTestDataDGVsColHeader()
        ResetFailureDetectDGV()
        ResetFirstFailTime()

        ODCCheckResult = False
        AbortTest = False
        TimeDone = False
    End Sub

    Private Sub ResetSlotsDGV()
        Try
            dgvSlotInd.Rows.Clear()
            For slot As Integer = 0 To SlotNum
                dgvSlotInd.Rows.Add(New String(2) {"Slot " & slot + 1, "", ""})
                For cellNum As Integer = 0 To 2
                    dgvSlotInd.Rows(slot).Cells(cellNum).Style.ForeColor = Black
                Next
            Next
            dgvSlotInd.Rows(0).Selected = True
            dgvSlotInd.FirstDisplayedScrollingRowIndex = 0
            frmMain.grpTestData.Text = "Test Date: " & dgvSlotInd.CurrentRow.Cells(0).Value
            frmMain.grpDetect.Text = "Failure Indicator: " & dgvSlotInd.CurrentRow.Cells(0).Value
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub ResetFailureDetectDGV()
        Try
            For slot As Integer = 0 To SlotNum
                dgvFailureInd(slot).Rows.Clear()
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ResetAllTestDataDGVsColHeader()
        Try
            For slot As Integer = 0 To SlotNum
                dgvSlotTestData(slot).Columns.Clear()
                For Count As Integer = 0 To TestItems.Length - 1
                    dgvSlotTestData(slot).Columns.Add("col_" & TestItems(Count), TestItems(Count))
                    dgvSlotTestData(slot).Columns(Count).SortMode = DataGridViewColumnSortMode.NotSortable
                    'If Count = 0 Then
                    '    dgvSlotTestData(slot).Columns(Count).Width = 80
                    'Else
                    '    dgvSlotTestData(slot).Columns(Count).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    'End If
                Next
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ResetFirstFailTime()
        For slot As Integer = 0 To SlotNum
            FirstFailTime(slot) = "0:00:00"
        Next
    End Sub

    Private Sub CleanRecData()
        Dim i As Integer
        For i = 0 To SlotNum
            With BI_Data(i).RecData
                .StartTime = Now()
                .AlarmFlag = False
                .SerialNumber = String.Empty
                .AlarmString = "No Alarm"
                .FailureReason = String.Empty

                .PA0_Temp.High = 0.0
                .PA0_Temp.Low = 100.0
                .PA1_Temp.High = 0.0
                .PA1_Temp.Low = 100.0
                .LNA0_Temp.High = 0.0
                .LNA0_Temp.Low = 100.0
                .LNA1_Temp.High = 0.0
                .LNA1_Temp.Low = 100.0
                .FB_Temp.High = 0.0
                .FB_Temp.Low = 100.0

                'General Information
                .PA0_Temp.High = 0.0
                .PA0_Temp.Low = 100.0
                .PA0_VSWR_Temp.High = 0.0
                .PA0_VSWR_Temp.Low = 100.0
                .PA1_Temp.High = 0.0
                .PA1_Temp.Low = 100.0
                .PA1_VSWR_Temp.High = 0.0
                .PA1_VSWR_Temp.Low = 100.0

                .PA_Temp_Delta.High = -100.0
                .PA_Temp_Delta.Low = 100.0

                .LNA0_Temp.High = 0.0
                .LNA0_Temp.Low = 100.0
                .LNA1_Temp.High = 0.0
                .LNA1_Temp.Low = 100.0
                .LNA2_Temp.High = 0.0
                .LNA2_Temp.Low = 100.0
                .LNA3_Temp.High = 0.0
                .LNA3_Temp.Low = 100.0
                .PS_Converter_Temp.High = 0.0
                .PS_Converter_Temp.Low = 100.0
                .PS_Brick_Temp.High = 0.0
                .PS_Brick_Temp.Low = 100.0

                .PSU_Temp_Delta.High = -100.0
                .PSU_Temp_Delta.Low = 100.0
                .PSU_PA_Temp_Delta.High = -100.0
                .PSU_PA_Temp_Delta.Low = 100.0

                .FB_Temp.High = 0.0
                .FB_Temp.Low = 100.0
                .RX_Temp.High = 0.0
                .RX_Temp.Low = 100.0

                'TX0
                .TX0_Output_Pow.High = 0.0
                .TX0_Output_Pow.Low = 100.0
                .TX0_VSWR.High = 0.0
                .TX0_VSWR.Low = 100.0
                .TX0_Forward_Power_Detector.High = 0.0
                .TX0_Forward_Power_Detector.Low = 99999.0
                .TX0_Reverse_Power_Detector.High = 0.0
                .TX0_Reverse_Power_Detector.Low = 99999.0
                .TX0_Gain_VCA.High = 0.0
                .TX0_Gain_VCA.Low = 999.0
                .TX0_txDacGain.High = 0.0
                .TX0_txDacGain.Low = 9999.0
                .TX0_totalTxAttn.High = 0.0
                .TX0_totalTxAttn.Low = 9999.0
                .TX0_Gain_TxStep.High = 0.0
                .TX0_Gain_TxStep.Low = 100.0
                .TX0_Gain_FbStep.High = 0.0
                .TX0_Gain_FbStep.Low = 100.0
                .TX0_Gain_FbTxQuo.High = 0.0
                .TX0_Gain_FbTxQuo.Low = 100.0
                .TX0_Gain_GainError.High = 0.0
                .TX0_Gain_GainError.Low = 100.0
                .TX0_PA0_PsVolt.High = 0.0
                .TX0_PA0_PsVolt.Low = 100.0
                .TX0_PA0_Temp.High = 0.0
                .TX0_PA0_Temp.Low = 100.0
                .TX0_PA0_BiasTemp.High = 0.0
                .TX0_PA0_BiasTemp.Low = 100.0
                .TX0_PA0_Driver1Cur.High = 0.0
                .TX0_PA0_Driver1Cur.Low = 9999.0
                .TX0_PA0_Driver2Cur.High = 0.0
                .TX0_PA0_Driver2Cur.Low = 9999.0
                .TX0_PA0_Driver3Cur.High = 0.0
                .TX0_PA0_Driver3Cur.Low = 9999.0
                .TX0_PA0_Driver4Cur.High = 0.0
                .TX0_PA0_Driver4Cur.Low = 9999.0
                .TX0_PA0_Final1Cur.High = 0.0
                .TX0_PA0_Final1Cur.Low = 9999.0
                .TX0_PA0_Final2Cur.High = 0.0
                .TX0_PA0_Final2Cur.Low = 9999.0
                .TX0_PA0_Driver1Dac.High = 0.0
                .TX0_PA0_Driver1Dac.Low = 9999.0
                .TX0_PA0_Driver2Dac.High = 0.0
                .TX0_PA0_Driver2Dac.Low = 9999.0
                .TX0_PA0_Driver3Dac.High = 0.0
                .TX0_PA0_Driver3Dac.Low = 9999.0
                .TX0_PA0_Driver4Dac.High = 0.0
                .TX0_PA0_Driver4Dac.Low = 9999.0
                .TX0_PA0_Final1Dac.High = 0.0
                .TX0_PA0_Final1Dac.Low = 9999.0
                .TX0_PA0_Final2Dac.High = 0.0
                .TX0_PA0_Final2Dac.Low = 9999.0
                .TX0_PA0_Final2Dac.High = 0.0
                .TX0_PA0_Final2Dac.Low = 999999999.0
                .TX0_PA0_Final2Dac.High = 0.0
                .TX0_PA0_Final2Dac.Low = 999999999.0
                .TX0_PA0_ampDelayInt.High = -0.0
                .TX0_PA0_ampDelayInt.Low = 9999.0
                .TX0_PA0_ampDelayFrac.High = 0.0
                .TX0_PA0_ampDelayFrac.Low = 9999.0
                .TX0_PA0_MaxCrossCorrelation.High = 0.0
                .TX0_PA0_MaxCrossCorrelation.Low = 9999.0
                .TX0_DPD_L1_Table_Max_Gain.High = -999.0
                .TX0_DPD_L1_Table_Max_Gain.Low = 999.0
                .TX0_DPD_L1_Table_Min_Gain.High = -999.0
                .TX0_DPD_L1_Table_Min_Gain.Low = 999.0
                .Tx0_DPD_L2_3rd_sym_am.High = -999.0
                .Tx0_DPD_L2_3rd_sym_am.Low = 999.0
                .Tx0_DPD_L2_3rd_sym_ph.High = -999.0
                .Tx0_DPD_L2_3rd_sym_ph.Low = 999.0
                .Tx0_DPD_L3_3rd_sym_am.High = -999.0
                .Tx0_DPD_L3_3rd_sym_am.Low = 999.0
                .Tx0_DPD_L3_3rd_sym_ph.High = -999.0
                .Tx0_DPD_L3_3rd_sym_ph.Low = 999.0
                .Tx0_DPD_L2_5th_sym_am.High = -999.0
                .Tx0_DPD_L2_5th_sym_am.Low = 999.0
                .Tx0_DPD_L2_5th_sym_ph.High = -999.0
                .Tx0_DPD_L2_5th_sym_ph.Low = 999.0
                .Tx0_DPD_L3_5th_sym_am.High = -999.0
                .Tx0_DPD_L3_5th_sym_am.Low = 999.0
                .Tx0_DPD_L3_5th_sym_ph.High = -999.0
                .Tx0_DPD_L3_5th_sym_ph.Low = 999.0
                .Tx0_DPD_L2_3rd_asym_am.High = -999.0
                .Tx0_DPD_L2_3rd_asym_am.Low = 999.0
                .Tx0_DPD_L2_3rd_asym_ph.High = -999.0
                .Tx0_DPD_L2_3rd_asym_ph.Low = 999.0
                .Tx0_DPD_L2_5th_asym_am.High = -999.0
                .Tx0_DPD_L2_5th_asym_am.Low = 999.0
                .Tx0_DPD_L2_5th_asym_ph.High = -999.0
                .Tx0_DPD_L2_5th_asym_ph.Low = 999.0

                'TX1
                .TX1_Output_Pow.High = 0.0
                .TX1_Output_Pow.Low = 100.0
                .TX1_VSWR.High = 0.0
                .TX1_VSWR.Low = 100.0
                .TX1_Forward_Power_Detector.High = 0.0
                .TX1_Forward_Power_Detector.Low = 99999.0
                .TX1_Reverse_Power_Detector.High = 0.0
                .TX1_Reverse_Power_Detector.Low = 99999.0
                .TX1_Gain_VCA.High = 0.0
                .TX1_Gain_VCA.Low = 999.0
                .TX1_txDacGain.High = 0.0
                .TX1_txDacGain.Low = 9999.0
                .TX1_totalTxAttn.High = 0.0
                .TX1_totalTxAttn.Low = 9999.0
                .TX1_Gain_TxStep.High = 0.0
                .TX1_Gain_TxStep.Low = 100.0
                .TX1_Gain_FbStep.High = 0.0
                .TX1_Gain_FbStep.Low = 100.0
                .TX1_Gain_FbTxQuo.High = 0.0
                .TX1_Gain_FbTxQuo.Low = 100.0
                .TX1_Gain_GainError.High = 0.0
                .TX1_Gain_GainError.Low = 100.0
                .TX1_PA1_PsVolt.High = 0.0
                .TX1_PA1_PsVolt.Low = 100.0
                .TX1_PA1_Temp.High = 0.0
                .TX1_PA1_Temp.Low = 100.0
                .TX1_PA1_BiasTemp.High = 0.0
                .TX1_PA1_BiasTemp.Low = 100.0
                .TX1_PA1_Driver1Cur.High = 0.0
                .TX1_PA1_Driver1Cur.Low = 9999.0
                .TX1_PA1_Driver2Cur.High = 0.0
                .TX1_PA1_Driver2Cur.Low = 9999.0
                .TX1_PA1_Driver3Cur.High = 0.0
                .TX1_PA1_Driver3Cur.Low = 9999.0
                .TX1_PA1_Driver4Cur.High = 0.0
                .TX1_PA1_Driver4Cur.Low = 9999.0
                .TX1_PA1_Final1Cur.High = 0.0
                .TX1_PA1_Final1Cur.Low = 9999.0
                .TX1_PA1_Final2Cur.High = 0.0
                .TX1_PA1_Final2Cur.Low = 9999.0
                .TX1_PA1_Driver1Dac.High = 0.0
                .TX1_PA1_Driver1Dac.Low = 9999.0
                .TX1_PA1_Driver2Dac.High = 0.0
                .TX1_PA1_Driver2Dac.Low = 9999.0
                .TX1_PA1_Driver3Dac.High = 0.0
                .TX1_PA1_Driver3Dac.Low = 9999.0
                .TX1_PA1_Driver4Dac.High = 0.0
                .TX1_PA1_Driver4Dac.Low = 9999.0
                .TX1_PA1_Final1Dac.High = 0.0
                .TX1_PA1_Final1Dac.Low = 9999.0
                .TX1_PA1_Final2Dac.High = 0.0
                .TX1_PA1_Final2Dac.Low = 9999.0
                .TX1_PA1_Final2Dac.High = 0.0
                .TX1_PA1_Final2Dac.Low = 999999999.0
                .TX1_PA1_Final2Dac.High = 0.0
                .TX1_PA1_Final2Dac.Low = 999999999.0
                .TX1_PA1_ampDelayInt.High = 0.0
                .TX1_PA1_ampDelayInt.Low = 9999.0
                .TX1_PA1_ampDelayFrac.High = 0.0
                .TX1_PA1_ampDelayFrac.Low = 9999.0
                .TX1_PA1_MaxCrossCorrelation.High = 0.0
                .TX1_PA1_MaxCrossCorrelation.Low = 9999.0
                .TX1_DPD_L1_Table_Max_Gain.High = -999.0
                .TX1_DPD_L1_Table_Max_Gain.Low = 999.0
                .TX1_DPD_L1_Table_Min_Gain.High = -999.0
                .TX1_DPD_L1_Table_Min_Gain.Low = 999.0
                .Tx1_DPD_L2_3rd_sym_am.High = -999.0
                .Tx1_DPD_L2_3rd_sym_am.Low = 999.0
                .Tx1_DPD_L2_3rd_sym_ph.High = -999.0
                .Tx1_DPD_L2_3rd_sym_ph.Low = 999.0
                .Tx1_DPD_L3_3rd_sym_am.High = -999.0
                .Tx1_DPD_L3_3rd_sym_am.Low = 999.0
                .Tx1_DPD_L3_3rd_sym_ph.High = -999.0
                .Tx1_DPD_L3_3rd_sym_ph.Low = 999.0
                .Tx1_DPD_L2_5th_sym_am.High = -999.0
                .Tx1_DPD_L2_5th_sym_am.Low = 999.0
                .Tx1_DPD_L2_5th_sym_ph.High = -999.0
                .Tx1_DPD_L2_5th_sym_ph.Low = 999.0
                .Tx1_DPD_L3_5th_sym_am.High = -999.0
                .Tx1_DPD_L3_5th_sym_am.Low = 999.0
                .Tx1_DPD_L3_5th_sym_ph.High = -999.0
                .Tx1_DPD_L3_5th_sym_ph.Low = 999.0
                .Tx1_DPD_L2_3rd_asym_am.High = -999.0
                .Tx1_DPD_L2_3rd_asym_am.Low = 999.0
                .Tx1_DPD_L2_3rd_asym_ph.High = -999.0
                .Tx1_DPD_L2_3rd_asym_ph.Low = 999.0
                .Tx1_DPD_L2_5th_asym_am.High = -999.0
                .Tx1_DPD_L2_5th_asym_am.Low = 999.0
                .Tx1_DPD_L2_5th_asym_ph.High = -999.0
                .Tx1_DPD_L2_5th_asym_ph.Low = 999.0

                'RX0
                .Rx0_LNA_Atten.High = 0.0
                .Rx0_LNA_Atten.Low = 100.0
                .Rx0_Rx_Atten0.High = 0.0
                .Rx0_Rx_Atten0.Low = 100.0
                .Rx0_Rx_Atten1.High = 0.0
                .Rx0_Rx_Atten1.Low = 100.0
                .Rx0_RSSI_C0.Low = 0.0
                .Rx0_RSSI_C0.High = -999.0
                .Rx0_RSSI_C1.Low = 0.0
                .Rx0_RSSI_C1.High = -999.0

                'RX1
                .Rx1_LNA_Atten.High = 0.0
                .Rx1_LNA_Atten.Low = 100.0
                .Rx1_Rx_Atten0.High = 0.0
                .Rx1_Rx_Atten0.Low = 100.0
                .Rx1_Rx_Atten1.High = 0.0
                .Rx1_Rx_Atten1.Low = 100.0
                .Rx1_RSSI_C0.Low = 0.0
                .Rx1_RSSI_C0.High = -999.0
                .Rx1_RSSI_C1.Low = 0.0
                .Rx1_RSSI_C1.High = -999.0

                'RX2
                .Rx2_LNA_Atten.High = 0.0
                .Rx2_LNA_Atten.Low = 100.0
                .Rx2_Rx_Atten0.High = 0.0
                .Rx2_Rx_Atten0.Low = 100.0
                .Rx2_Rx_Atten1.High = 0.0
                .Rx2_Rx_Atten1.Low = 100.0
                .Rx2_RSSI_C0.Low = 0.0
                .Rx2_RSSI_C0.High = -999.0
                .Rx2_RSSI_C1.Low = 0.0
                .Rx2_RSSI_C1.High = -999.0

                'RX3
                .Rx3_LNA_Atten.High = 0.0
                .Rx3_LNA_Atten.Low = 100.0
                .Rx3_Rx_Atten0.High = 0.0
                .Rx3_Rx_Atten0.Low = 100.0
                .Rx3_Rx_Atten1.High = 0.0
                .Rx3_Rx_Atten1.Low = 100.0
                .Rx3_RSSI_C0.Low = 0.0
                .Rx3_RSSI_C0.High = -999.0
                .Rx3_RSSI_C1.Low = 0.0
                .Rx3_RSSI_C1.High = -999.0

                'PS
                .Input_Voltage.High = 0.0
                .Input_Voltage.Low = 100.0
                .Input_Current_HP.High = 0.0
                .Input_Current_HP.Low = 999.0
                .Input_Current_LP.High = 0.0
                .Input_Current_LP.Low = 999.0
                .Input_Power_HP.High = 0.0
                .Input_Power_HP.Low = 999.0
                .Input_Power_LP.High = 0.0
                .Input_Power_LP.Low = 999.0
                .Output_Voltage.High = 0.0
                .Output_Voltage.Low = 100.0
                .AISG_12V_Voltage.High = 0.0
                .AISG_12V_Voltage.Low = 100.0
                .AISG_12V_Current.High = 0.0
                .AISG_12V_Current.Low = 100.0
                .AISG_24V_Voltage.High = 0.0
                .AISG_24V_Voltage.Low = 100.0
                .AISG_24V_Current.High = 0.0
                .AISG_24V_Current.Low = 100.0

                '.ACPR.High = -999.0
                '.ACPR.Low = 0.0

                '.Last_CPRIFPGAVer = String.Empty
                .Last_FPGARevision = String.Empty
                .Last_DSPRevision = String.Empty
                .Last_SWRevision = String.Empty
                '.CPRIFPGAVer = String.Empty
                .FPGARevision = String.Empty
                .DSPRevision = String.Empty
                .SWRevision = String.Empty

                .PowerCycleCount = 0

                .RF_On_Time = 0.0
                .Thres_Hold_Time = 0.0

                '.VCA.High = 0
                '.VCA.Low = 99999
                '.OPD.High = 0
                '.OPD.Low = 99999
                '.OPD.Delta = 0

                '.BI_PWR_DETECTOR_SLOPE = 999
                '.BI_PWR_DETECTOR_TEMP_SLOPE = 999
                '.BI_CAR_SQUAR = 999
                '.BI_CAR_LINEAR = 999
                '.BI_PEAK_SQUAR = 999
                '.BI_PEAK_LINEAR = 999
                '.BI_PWR_DETECTOR_MAX_ERR_LOW = 999
                '.BI_CAR_MAX_ERR_LOW = 999
                '.BI_PEAK_MAX_ERR_LOW = 999
                '.BI_PWR_DETECTOR_MAX_ERR_HIGH = 999
                '.BI_CAR_MAX_ERR_HIGH = 999
                '.BI_PEAK_MAX_ERR_HIGH = 999
                '.First_Polling = True
                '.First_Polling_Final2_Current = 0.0
                '.High_Temp_Final2_Current = 0.0
            End With
        Next
    End Sub

    Private Sub CleanMeasData()
        Dim i As Integer
        For i = 0 To SlotNum
            With BI_Data(i).MeasData
                .Last_Polling_Time = Now
                .PollingTime = Now
                .Phase = String.Empty
                .AlarmString = "No Alarm"
                .RamLog = ""
                .FPGARevision = String.Empty
                .DSPRevision = String.Empty
                .SWRevision = String.Empty

                'General Information
                .PA0_Temp = 0.0
                .PA0_VSWR_Temp = 0.0
                .PA1_Temp = 0.0
                .PA1_VSWR_Temp = 0.0

                .PA_Temp_Delta = 0.0

                .LNA0_Temp = 0.0
                .LNA1_Temp = 0.0
                .LNA2_Temp = 0.0
                .LNA3_Temp = 0.0
                .PS_Converter_Temp = 0.0
                .PS_Brick_Temp = 0.0

                .PSU_Temp_Delta = 0.0
                .PSU_PA_Temp_Delta = 0.0

                .FB_Temp = 0.0
                .RX_Temp = 0.0

                'TX0
                .TX0_Output_Pow = 0.0
                .TX0_VSWR = 0.0
                .TX0_Forward_Power_Detector = 0.0
                .TX0_Reverse_Power_Detector = 0.0
                .TX0_Gain_VCA = 0.0
                .TX0_txDacGain = 0.0
                .TX0_totalTxAttn = 0.0
                .TX0_Gain_TxStep = 0.0
                .TX0_Gain_FbStep = 0.0
                .TX0_Gain_FbTxQuo = 0.0
                .TX0_Gain_GainError = 0.0
                .TX0_PA0_PsVolt = 0.0
                .TX0_PA0_Temp = 0.0
                .TX0_PA0_BiasTemp = 0.0
                .TX0_PA0_Driver1Cur = 0.0
                .TX0_PA0_Driver2Cur = 0.0
                .TX0_PA0_Driver3Cur = 0.0
                .TX0_PA0_Driver4Cur = 0.0
                .TX0_PA0_Final1Cur = 0.0
                .TX0_PA0_Final2Cur = 0.0
                .TX0_PA0_Driver1Dac = 0.0
                .TX0_PA0_Driver2Dac = 0.0
                .TX0_PA0_Driver3Dac = 0.0
                .TX0_PA0_Driver4Dac = 0.0
                .TX0_PA0_Final1Dac = 0.0
                .TX0_PA0_Final2Dac = 0.0
                .TX0_PA0_ampDelayInt = 0.0
                .TX0_PA0_ampDelayFrac = 0.0
                .TX0_PA0_MaxCrossCorrelation = 0.0
                .Tx0_DPD_L1_Table_Max_Gain = 0.0
                .Tx0_DPD_L1_Table_Min_Gain = 0.0
                .Tx0_DPD_L2_3rd_sym_am = 0.0
                .Tx0_DPD_L2_3rd_sym_ph = 0.0
                .Tx0_DPD_L3_3rd_sym_am = 0.0
                .Tx0_DPD_L3_3rd_sym_ph = 0.0
                .Tx0_DPD_L2_5th_sym_am = 0.0
                .Tx0_DPD_L2_5th_sym_ph = 0.0
                .Tx0_DPD_L3_5th_sym_am = 0.0
                .Tx0_DPD_L3_5th_sym_ph = 0.0
                .Tx0_DPD_L2_3rd_asym_am = 0.0
                .Tx0_DPD_L2_3rd_asym_ph = 0.0
                .Tx0_DPD_L2_5th_asym_am = 0.0
                .Tx0_DPD_L2_5th_asym_ph = 0.0

                'TX1
                .TX1_Output_Pow = 0.0
                .TX1_VSWR = 0.0
                .TX1_Forward_Power_Detector = 0.0
                .TX1_Reverse_Power_Detector = 0.0
                .TX1_Gain_VCA = 0.0
                .TX1_txDacGain = 0.0
                .TX1_totalTxAttn = 0.0
                .TX1_Gain_TxStep = 0.0
                .TX1_Gain_FbStep = 0.0
                .TX1_Gain_FbTxQuo = 0.0
                .TX1_Gain_GainError = 0.0
                .TX1_PA1_PsVolt = 0.0
                .TX1_PA1_Temp = 0.0
                .TX1_PA1_BiasTemp = 0.0
                .TX1_PA1_Driver1Cur = 0.0
                .TX1_PA1_Driver2Cur = 0.0
                .TX1_PA1_Driver3Cur = 0.0
                .TX1_PA1_Driver4Cur = 0.0
                .TX1_PA1_Final1Cur = 0.0
                .TX1_PA1_Final2Cur = 0.0
                .TX1_PA1_Driver1Dac = 0.0
                .TX1_PA1_Driver2Dac = 0.0
                .TX1_PA1_Driver3Dac = 0.0
                .TX1_PA1_Driver4Dac = 0.0
                .TX1_PA1_Final1Dac = 0.0
                .TX1_PA1_Final2Dac = 0.0
                .TX1_PA1_ampDelayInt = 0.0
                .TX1_PA1_ampDelayFrac = 0.0
                .TX1_PA1_MaxCrossCorrelation = 0.0
                .Tx1_DPD_L1_Table_Max_Gain = 0.0
                .Tx1_DPD_L1_Table_Min_Gain = 0.0
                .Tx0_DPD_L2_3rd_sym_am = 0.0
                .Tx0_DPD_L2_3rd_sym_ph = 0.0
                .Tx0_DPD_L3_3rd_sym_am = 0.0
                .Tx0_DPD_L3_3rd_sym_ph = 0.0
                .Tx0_DPD_L2_5th_sym_am = 0.0
                .Tx0_DPD_L2_5th_sym_ph = 0.0
                .Tx0_DPD_L3_5th_sym_am = 0.0
                .Tx0_DPD_L3_5th_sym_ph = 0.0
                .Tx0_DPD_L2_3rd_asym_am = 0.0
                .Tx0_DPD_L2_3rd_asym_ph = 0.0
                .Tx0_DPD_L2_5th_asym_am = 0.0
                .Tx0_DPD_L2_5th_asym_ph = 0.0

                'RX0
                .Rx0_LNA_Atten = 0.0
                .Rx0_Rx_Atten0 = 0.0
                .Rx0_Rx_Atten1 = 0.0
                .Rx0_RSSI_C0 = 0.0
                .Rx0_RSSI_C1 = 0.0

                'RX1
                .Rx1_LNA_Atten = 0.0
                .Rx1_Rx_Atten0 = 0.0
                .Rx1_Rx_Atten1 = 0.0
                .Rx1_RSSI_C0 = 0.0
                .Rx1_RSSI_C1 = 0.0

                'RX2
                .Rx2_LNA_Atten = 0.0
                .Rx2_Rx_Atten0 = 0.0
                .Rx2_Rx_Atten1 = 0.0
                .Rx2_RSSI_C0 = 0.0
                .Rx2_RSSI_C1 = 0.0

                'RX3
                .Rx3_LNA_Atten = 0.0
                .Rx3_Rx_Atten0 = 0.0
                .Rx3_Rx_Atten1 = 0.0
                .Rx3_RSSI_C0 = 0.0
                .Rx3_RSSI_C1 = 0.0

                'PS
                .Input_Voltage = 0.0
                .Input_Current = 0.0
                .Input_Power = 0.0
                .Output_Voltage = 0.0
                .AISG_12V_Voltage = 0.0
                .AISG_12V_Current = 0.0
                .AISG_24V_Voltage = 0.0
                .AISG_24V_Current = 0.0

                .RF_Status = False

                .PowerCycleCount = 0
            End With
        Next
    End Sub

    Private Sub CleanUnitInfo()
        Dim i As Integer
        For i = 0 To SlotNum
            With BI_Data(i).UnitInfo
                .FanOnOff = False
                .PowerOnOff = False
                .UnitActive = False
                .FileName = String.Empty
                .CSVFilePath = String.Empty
                .CSVFileName = String.Empty
                .JPGFilePath = String.Empty
                .DATFilePath = String.Empty
                .TXTFilePath = String.Empty
                .UnitSN = String.Empty
                .UnitType = String.Empty
            End With
        Next
    End Sub

    Private Sub Del_DCF()
        On Error Resume Next
        Kill("C:\qstatii.dat.dcf")
    End Sub

    Private Sub CleanMessageInfo()
        On Error Resume Next
        frmMain.TextBoxMessage.Clear()
    End Sub

    Public Sub EnableControlBtn(ByVal Enable As Boolean)
        frmMain.tsbStart.Enabled = Enable
        frmMain.MenuStrip1.Enabled = Enable
        If BI_HW.Fan_Chan_Enabled Then
            frmMain.tsbTurnOffAllFans.Enabled = Enable
            frmMain.tsbTurnOnAllFans.Enabled = Enable
        End If
        If BI_HW.Pwr_Chan_Enabled Then
            frmMain.tsbTurnOffAllPowers.Enabled = Enable
            frmMain.tsbTurnOnAllPowers.Enabled = Enable
        End If
        If BI_HW.Chamber_Control_Enabled Then
            frmMain.tsbTurnOffChamber.Enabled = Enable
            frmMain.tsbTurnOnChamber.Enabled = Enable
        End If

        Dim PS_E As Boolean = False
        For PSCount As Integer = 0 To 5
            If BI_HW.PS_Control_Enabled(PSCount) Then PS_E = True
            Exit For
        Next

        If PS_E Then
            frmMain.tsmTurnOffAllPowerSuppliers.Enabled = Enable
            frmMain.tsmTurnOnAllPowerSuppliers.Enabled = Enable
            frmMain.tsbTurnOffPS.Enabled = Enable
            frmMain.tsbTurnOnPS.Enabled = Enable
        End If

        If BI_HW.Anton_Chan_Enabled Then
            frmMain.tsbTurnOnAnton.Enabled = Enable
            frmMain.tsbTurnOffAnton.Enabled = Enable
        End If

        frmMain.tsbConfig.Enabled = Enable
    End Sub


#End Region

    Public Function PathsOK(Optional ByRef ErrorMessage As String = OK) As Boolean
        On Error Resume Next
        Dim Tmp As String
        With My.Computer.FileSystem
            ErrorMessage = "The following directly is not writable: " & NL
            Tmp = "C:\BurnIn"
            .CreateDirectory(Tmp)
            If Not My.Computer.FileSystem.DirectoryExists(Tmp) Then
                ErrorMessage = ErrorMessage & Tmp
                Return False
            End If

            'Tmp = "C:\BurnIn\Voyager\Voyager_800"
            '.CreateDirectory(Tmp)
            'If Not My.Computer.FileSystem.DirectoryExists(Tmp) Then
            '    ErrorMessage = ErrorMessage & Tmp
            '    Return False
            'End If

            Return True
        End With
    End Function

    Public Sub Delay(ByVal msec As Long)
        Dim secs As Long = msec / 1000
        Dim Stt As Date = Now
        Dim StTimer As Single = Timer()
        Do
            Application.DoEvents()
            Application.DoEvents()
            Application.DoEvents()
        Loop Until (Timer() - StTimer) >= msec Or DateDiff(DateInterval.Second, Stt, Now) > secs
    End Sub

    Public Function Timer() As Single
        Timer = CSng(VB6.Timer())
    End Function

    Public Sub AddMessage(ByVal LongMessage As String)
        Dim Indi As Object
        For Each Indi In MessageIndicators
            Try
                Indi.appendtext(Now & ": " & LongMessage & NL)   'Add time information in AddMessage()
                Console.WriteLine(Now & ": " & LongMessage & NL)
            Catch ex As Exception
                Indi.clear()
                Indi.Text = LongMessage
            End Try
        Next
        'Dim aaa As System.Windows.Forms.TextBox
        'aaa = frmMain.TextBoxMessage
        'aaa.AppendText(Now & ": " & LongMessage & NL)
    End Sub

#End Region



    Public Sub DisplayPolling(ByVal slot As Integer)
        dgvSlotInd.Rows(slot).Cells(2).Value = "Polling..."
        dgvSlotInd.Rows(slot).Cells(2).Style.ForeColor = Blue
    End Sub

    Public Sub DisplayAbort(ByVal slot As Integer)
        dgvSlotInd.Rows(slot).Cells(2).Value = "Abort"
        dgvSlotInd.Rows(slot).Cells(2).Style.ForeColor = Red
    End Sub

    Public Sub DisplayReadUnit(ByVal slot As Integer)
        dgvSlotInd.Rows(slot).Cells(2).Value = "Connecting..."
        dgvSlotInd.Rows(slot).Cells(2).Style.ForeColor = Blue
    End Sub

    Public Sub DisplayUnitActive(ByVal slot As Integer)
        If BI_Data(slot).UnitInfo.UnitActive Then
            dgvSlotInd.Rows(slot).Cells(1).Value = BI_Data(slot).UnitInfo.UnitSN
            dgvSlotInd.Rows(slot).Cells(2).Value = "Connected"
            dgvSlotInd.Rows(slot).Cells(2).Style.ForeColor = Black
        Else
            dgvSlotInd.Rows(slot).Cells(1).Value = "Blank"
            dgvSlotInd.Rows(slot).Cells(2).Value = "Blank"
            dgvSlotInd.Rows(slot).Cells(2).Style.ForeColor = Black
        End If
    End Sub

    Public Sub DisplayAlarm(ByVal slot As Integer, ByVal AlarmFlag As Boolean)
        If AlarmFlag Then
            dgvSlotInd.Rows(slot).Cells(2).Value = "Fail"
            dgvSlotInd.Rows(slot).Cells(2).Style.ForeColor = Red
        Else
            dgvSlotInd.Rows(slot).Cells(2).Value = "No Failure"
            dgvSlotInd.Rows(slot).Cells(2).Style.ForeColor = Green
        End If
    End Sub

    Public Sub DisplayTestResult(ByVal slot As Integer, ByVal Status As Boolean)
        If Status Then
            dgvSlotInd.Rows(slot).Cells(2).Value = "Fail"
            dgvSlotInd.Rows(slot).Cells(2).Style.ForeColor = Red
        Else
            dgvSlotInd.Rows(slot).Cells(2).Value = "Pass"
            dgvSlotInd.Rows(slot).Cells(2).Style.ForeColor = Green
        End If
    End Sub


    Public Sub UpdateTestReocrd(ByVal slot As Integer, ByVal ePhase As String)

        Dim TestItemCount As Integer = TestItems.Length - 1
        Dim TestData(TestItemCount) As String

        'V2.0.9
        'BI_Data(slot).MeasData.RamLogSingleFlag = False   'V2.0.3  Split single Ramlog with normal one.

        With BI_Data(slot).MeasData
            For i As Integer = 0 To TestItemCount
                Select Case TestItems(i)
                    Case "PollingTime"
                        TestData(i) = .PollingTime
                    Case "Phase"
                        TestData(i) = ePhase

                    Case "PA0_Temp"
                        TestData(i) = .PA0_Temp
                    Case "PA0_VSWR_Temp"
                        TestData(i) = .PA0_VSWR_Temp
                    Case "PA1_Temp"
                        TestData(i) = .PA1_Temp
                    Case "PA1_VSWR_Temp"
                        TestData(i) = .PA1_VSWR_Temp

                    Case "LNA0_Temp"
                        TestData(i) = .LNA0_Temp
                    Case "LNA1_Temp"
                        TestData(i) = .LNA1_Temp
                    Case "LNA2_Temp"
                        TestData(i) = .LNA2_Temp
                    Case "LNA3_Temp"
                        TestData(i) = .LNA3_Temp
                    Case "PS_Converter_Temp"
                        TestData(i) = .PS_Converter_Temp
                    Case "PS_Brick_Temp"
                        TestData(i) = .PS_Brick_Temp
                    Case "FB_Temp"
                        TestData(i) = .FB_Temp
                    Case "RX_Temp"
                        TestData(i) = .RX_Temp

                    Case "TX0_Output_Pow"
                        TestData(i) = .TX0_Output_Pow
                    Case "TX0_VSWR"
                        TestData(i) = .TX0_VSWR
                    Case "TX0_Forward_Power_Detector"
                        TestData(i) = .TX0_Forward_Power_Detector
                    Case "TX0_Reverse_Power_Detector"
                        TestData(i) = .TX0_Reverse_Power_Detector
                    Case "TX0_Gain_VCA"
                        TestData(i) = .TX0_Gain_VCA
                    Case "TX0_txDacGain"
                        TestData(i) = .TX0_txDacGain
                    Case "TX0_totalTxAttn"
                        TestData(i) = .TX0_totalTxAttn
                    Case "TX0_Gain_TxStep"
                        TestData(i) = .TX0_Gain_TxStep
                    Case "TX0_Gain_FbStep"
                        TestData(i) = .TX0_Gain_FbStep
                    Case "TX0_Gain_FbTxQuo"
                        TestData(i) = .TX0_Gain_FbTxQuo
                    Case "TX0_Gain_GainError"
                        TestData(i) = .TX0_Gain_GainError
                    Case "TX0_PA0_PsVolt"
                        TestData(i) = .TX0_PA0_PsVolt
                    Case "TX0_PA0_Temp"
                        TestData(i) = .TX0_PA0_Temp
                    Case "TX0_PA0_BiasTemp"
                        TestData(i) = .TX0_PA0_BiasTemp
                    Case "TX0_PA0_Driver1Cur"
                        TestData(i) = .TX0_PA0_Driver1Cur
                    Case "TX0_PA0_Driver2Cur"
                        TestData(i) = .TX0_PA0_Driver2Cur
                    Case "TX0_PA0_Driver3Cur"
                        TestData(i) = .TX0_PA0_Driver3Cur
                    Case "TX0_PA0_Driver4Cur"
                        TestData(i) = .TX0_PA0_Driver4Cur
                    Case "TX0_PA0_Final1Cur"
                        TestData(i) = .TX0_PA0_Final1Cur
                    Case "TX0_PA0_Final2Cur"
                        TestData(i) = .TX0_PA0_Final2Cur
                    Case "TX0_PA0_Driver1Dac"
                        TestData(i) = .TX0_PA0_Driver1Dac
                    Case "TX0_PA0_Driver2Dac"
                        TestData(i) = .TX0_PA0_Driver2Dac
                    Case "TX0_PA0_Driver3Dac"
                        TestData(i) = .TX0_PA0_Driver3Dac
                    Case "TX0_PA0_Driver4Dac"
                        TestData(i) = .TX0_PA0_Driver4Dac
                    Case "TX0_PA0_Final1Dac"
                        TestData(i) = .TX0_PA0_Final1Dac
                    Case "TX0_PA0_Final2Dac"
                        TestData(i) = .TX0_PA0_Final2Dac
                    Case "TX0_PA0_ampDelayInt"
                        TestData(i) = .TX0_PA0_ampDelayInt
                    Case "TX0_PA0_ampDelayFrac"
                        TestData(i) = .TX0_PA0_ampDelayFrac
                    Case "TX0_PA0_MaxCrossCorrelation"
                        TestData(i) = .TX0_PA0_MaxCrossCorrelation
                    Case "Tx0_DPD_L1_Table_Max_Gain"
                        TestData(i) = .Tx0_DPD_L1_Table_Max_Gain
                    Case "Tx0_DPD_L1_Table_Min_Gain"
                        TestData(i) = .Tx0_DPD_L1_Table_Min_Gain
                    Case "Tx0_DPD_L2_3rd_sym_am"
                        TestData(i) = .Tx0_DPD_L2_3rd_sym_am
                    Case "Tx0_DPD_L2_3rd_sym_ph"
                        TestData(i) = .Tx0_DPD_L2_3rd_sym_ph
                    Case "Tx0_DPD_L3_3rd_sym_am"
                        TestData(i) = .Tx0_DPD_L3_3rd_sym_am
                    Case "Tx0_DPD_L3_3rd_sym_ph"
                        TestData(i) = .Tx0_DPD_L3_3rd_sym_ph
                    Case "Tx0_DPD_L2_5th_sym_am"
                        TestData(i) = .Tx0_DPD_L2_5th_sym_am
                    Case "Tx0_DPD_L2_5th_sym_ph"
                        TestData(i) = .Tx0_DPD_L2_5th_sym_ph
                    Case "Tx0_DPD_L3_5th_sym_am"
                        TestData(i) = .Tx0_DPD_L3_5th_sym_am
                    Case "Tx0_DPD_L3_5th_sym_ph"
                        TestData(i) = .Tx0_DPD_L3_5th_sym_ph
                    Case "Tx0_DPD_L2_3rd_asym_am"
                        TestData(i) = .Tx0_DPD_L2_3rd_asym_am
                    Case "Tx0_DPD_L2_3rd_asym_ph"
                        TestData(i) = .Tx0_DPD_L2_3rd_asym_ph
                    Case "Tx0_DPD_L2_5th_asym_am"
                        TestData(i) = .Tx0_DPD_L2_5th_asym_am
                    Case "Tx0_DPD_L2_5th_asym_ph"
                        TestData(i) = .Tx0_DPD_L2_5th_asym_ph

                    Case "TX1_Output_Pow"
                        TestData(i) = .TX1_Output_Pow
                    Case "TX1_VSWR"
                        TestData(i) = .TX1_VSWR
                    Case "TX1_Forward_Power_Detector"
                        TestData(i) = .TX1_Forward_Power_Detector
                    Case "TX1_Reverse_Power_Detector"
                        TestData(i) = .TX1_Reverse_Power_Detector
                    Case "TX1_Gain_VCA"
                        TestData(i) = .TX1_Gain_VCA
                    Case "TX1_txDacGain"
                        TestData(i) = .TX1_txDacGain
                    Case "TX1_totalTxAttn"
                        TestData(i) = .TX1_totalTxAttn
                    Case "TX1_Gain_TxStep"
                        TestData(i) = .TX1_Gain_TxStep
                    Case "TX1_Gain_FbStep"
                        TestData(i) = .TX1_Gain_FbStep
                    Case "TX1_Gain_FbTxQuo"
                        TestData(i) = .TX1_Gain_FbTxQuo
                    Case "TX1_Gain_GainError"
                        TestData(i) = .TX1_Gain_GainError
                    Case "TX1_PA1_PsVolt"
                        TestData(i) = .TX1_PA1_PsVolt
                    Case "TX1_PA1_Temp"
                        TestData(i) = .TX1_PA1_Temp
                    Case "TX1_PA1_BiasTemp"
                        TestData(i) = .TX1_PA1_BiasTemp
                    Case "TX1_PA1_Driver1Cur"
                        TestData(i) = .TX1_PA1_Driver1Cur
                    Case "TX1_PA1_Driver2Cur"
                        TestData(i) = .TX1_PA1_Driver2Cur
                    Case "TX1_PA1_Driver3Cur"
                        TestData(i) = .TX1_PA1_Driver3Cur
                    Case "TX1_PA1_Driver4Cur"
                        TestData(i) = .TX1_PA1_Driver4Cur
                    Case "TX1_PA1_Final1Cur"
                        TestData(i) = .TX1_PA1_Final1Cur
                    Case "TX1_PA1_Final2Cur"
                        TestData(i) = .TX1_PA1_Final2Cur
                    Case "TX1_PA1_Driver1Dac"
                        TestData(i) = .TX1_PA1_Driver1Dac
                    Case "TX1_PA1_Driver2Dac"
                        TestData(i) = .TX1_PA1_Driver2Dac
                    Case "TX1_PA1_Driver3Dac"
                        TestData(i) = .TX1_PA1_Driver3Dac
                    Case "TX1_PA1_Driver4Dac"
                        TestData(i) = .TX1_PA1_Driver4Dac
                    Case "TX1_PA1_Final1Dac"
                        TestData(i) = .TX1_PA1_Final1Dac
                    Case "TX1_PA1_Final2Dac"
                        TestData(i) = .TX1_PA1_Final2Dac
                    Case "TX1_PA1_ampDelayInt"
                        TestData(i) = .TX1_PA1_ampDelayInt
                    Case "TX1_PA1_ampDelayFrac"
                        TestData(i) = .TX1_PA1_ampDelayFrac
                    Case "TX1_PA1_MaxCrossCorrelation"
                        TestData(i) = .TX1_PA1_MaxCrossCorrelation
                    Case "Tx1_DPD_L1_Table_Max_Gain"
                        TestData(i) = .Tx1_DPD_L1_Table_Max_Gain
                    Case "Tx1_DPD_L1_Table_Min_Gain"
                        TestData(i) = .Tx1_DPD_L1_Table_Min_Gain
                    Case "Tx1_DPD_L2_3rd_sym_am"
                        TestData(i) = .Tx1_DPD_L2_3rd_sym_am
                    Case "Tx1_DPD_L2_3rd_sym_ph"
                        TestData(i) = .Tx1_DPD_L2_3rd_sym_ph
                    Case "Tx1_DPD_L3_3rd_sym_am"
                        TestData(i) = .Tx1_DPD_L3_3rd_sym_am
                    Case "Tx1_DPD_L3_3rd_sym_ph"
                        TestData(i) = .Tx1_DPD_L3_3rd_sym_ph
                    Case "Tx1_DPD_L2_5th_sym_am"
                        TestData(i) = .Tx1_DPD_L2_5th_sym_am
                    Case "Tx1_DPD_L2_5th_sym_ph"
                        TestData(i) = .Tx1_DPD_L2_5th_sym_ph
                    Case "Tx1_DPD_L3_5th_sym_am"
                        TestData(i) = .Tx1_DPD_L3_5th_sym_am
                    Case "Tx1_DPD_L3_5th_sym_ph"
                        TestData(i) = .Tx1_DPD_L3_5th_sym_ph
                    Case "Tx1_DPD_L2_3rd_asym_am"
                        TestData(i) = .Tx1_DPD_L2_3rd_asym_am
                    Case "Tx1_DPD_L2_3rd_asym_ph"
                        TestData(i) = .Tx1_DPD_L2_3rd_asym_ph
                    Case "Tx1_DPD_L2_5th_asym_am"
                        TestData(i) = .Tx1_DPD_L2_5th_asym_am
                    Case "Tx1_DPD_L2_5th_asym_ph"
                        TestData(i) = .Tx1_DPD_L2_5th_asym_ph

                    Case "Rx0_LNA_Atten"
                        TestData(i) = .Rx0_LNA_Atten
                    Case "Rx0_Rx_Atten0"
                        TestData(i) = .Rx0_Rx_Atten0
                    Case "Rx0_Rx_Atten1"
                        TestData(i) = .Rx0_Rx_Atten1
                    Case "Rx0_RSSI_C0"
                        TestData(i) = .Rx0_RSSI_C0
                    Case "Rx0_RSSI_C1"
                        TestData(i) = .Rx0_RSSI_C1

                    Case "Rx1_LNA_Atten"
                        TestData(i) = .Rx1_LNA_Atten
                    Case "Rx1_Rx_Atten0"
                        TestData(i) = .Rx1_Rx_Atten0
                    Case "Rx1_Rx_Atten1"
                        TestData(i) = .Rx1_Rx_Atten1
                    Case "Rx1_RSSI_C0"
                        TestData(i) = .Rx1_RSSI_C0
                    Case "Rx1_RSSI_C1"
                        TestData(i) = .Rx1_RSSI_C1

                    Case "Rx2_LNA_Atten"
                        TestData(i) = .Rx2_LNA_Atten
                    Case "Rx2_Rx_Atten0"
                        TestData(i) = .Rx2_Rx_Atten0
                    Case "Rx2_Rx_Atten1"
                        TestData(i) = .Rx2_Rx_Atten1
                    Case "Rx2_RSSI_C0"
                        TestData(i) = .Rx2_RSSI_C0
                    Case "Rx2_RSSI_C1"
                        TestData(i) = .Rx2_RSSI_C1

                    Case "Rx3_LNA_Atten"
                        TestData(i) = .Rx3_LNA_Atten
                    Case "Rx3_Rx_Atten0"
                        TestData(i) = .Rx3_Rx_Atten0
                    Case "Rx3_Rx_Atten1"
                        TestData(i) = .Rx3_Rx_Atten1
                    Case "Rx3_RSSI_C0"
                        TestData(i) = .Rx3_RSSI_C0
                    Case "Rx3_RSSI_C1"
                        TestData(i) = .Rx3_RSSI_C1

                    Case "Input_Voltage"
                        TestData(i) = .Input_Voltage
                    Case "Input_Current"
                        TestData(i) = .Input_Current
                    Case "Input_Power"
                        TestData(i) = .Input_Power
                    Case "Output_Voltage"
                        TestData(i) = .Output_Voltage
                    Case "AISG_12V_Voltage"
                        TestData(i) = .AISG_12V_Voltage
                    Case "AISG_12V_Current"
                        TestData(i) = .AISG_12V_Current
                    Case "AISG_24V_Voltage"
                        TestData(i) = .AISG_24V_Voltage
                    Case "AISG_24V_Current"
                        TestData(i) = .AISG_24V_Current

                    Case "DSPRevision"
                        TestData(i) = .DSPRevision
                    Case "FPGARevision"
                        TestData(i) = .FPGARevision
                    Case "SWRevision"
                        TestData(i) = .SWRevision

                    Case "Alarm"
                        TestData(i) = .AlarmString
                    Case "PowerCycleCount"
                        TestData(i) = .PowerCycleCount
                    Case "PAEnable"
                        TestData(i) = .PAEnabled
                End Select
            Next

            'V2.0.3  Split single Ramlog with normal one.
            For i As Integer = 0 To TestItemCount
                If TestData(i) = "-99999" Then .RamLogSingleFlag = True : Exit For
            Next
        End With
        dgvSlotTestData(slot).Rows.Add(TestData)
        Dim RowsCount As Integer = dgvSlotTestData(slot).RowCount - 1
        If RowsCount > 0 Then dgvSlotTestData(slot).FirstDisplayedScrollingRowIndex = dgvSlotTestData(slot).RowCount - 1
    End Sub

    Public Function ChamberOnOff(ByVal OnOff As Boolean) As Boolean
        If BI_HW.Chamber_Control_Enabled Then
            Try
                If Not moRS232.IsOpen Then RS232_Open(True, True)
                If Not (Chamber.Read_FP93Name = "FP93" And Chamber.Write_Set_Local_COM("1") = "OK") Then
                    Return False
                    Exit Function
                End If
                Select Case OnOff
                    Case True
                        Chamber.Write_RST_RUN("0")

                        For i As Integer = 1 To 10
                            Chamber.ReadWrite_PatternX_StepY_ItemZ_Value(BI_HW.Chamber_Run_Pattern, i, 1, BI_HW.Chamber_Step(i - 1).Temp)
                            Chamber.ReadWrite_PatternX_StepY_ItemZ_Value(BI_HW.Chamber_Run_Pattern, i, 2, BI_HW.Chamber_Step(i - 1).Time)
                            Chamber.ReadWrite_PatternX_StepY_ItemZ_Value(BI_HW.Chamber_Run_Pattern, i, 3, BI_HW.Chamber_Step(i - 1).PID)
                        Next
                        Chamber.ReadWrite_Program_Mode("0")
                        Chamber.ReadWrite_Start_Pattern_No(BI_HW.Chamber_Run_Pattern)
                        Chamber.Write_RST_RUN("1")
                    Case False
                        Chamber.Write_RST_RUN("0")
                End Select
                ChamberOnOff = True
            Catch ex As Exception
                ChamberOnOff = False
            Finally
                Try
                    Chamber.Write_Set_Local_COM("0")
                    RS232_Close()
                Catch ex As Exception
                    AddMessage(ex.Message)
                End Try

            End Try
        End If
    End Function

    Public Sub TurnFanOnOff(ByVal slot As Integer, ByVal OnOff As Boolean)
        Try

            If Not BI_HW.Fan_Chan_Enabled Then Exit Sub

            If OnOff Then
                AddMessage("Turn slot " & (slot + 1).ToString & " Fan on.")
            Else
                AddMessage("Turn slot " & (slot + 1).ToString & " Fan off.")
            End If

            Application.DoEvents()

            BI_Data(slot).UnitInfo.FanOnOff = OnOff

            Select Case UCase(BI_HW.Fan_Switch_Type)
                Case "SWITCH_USBIO_24"
                    Dim i As Integer, FansSquence As Integer = 0
                    For i = 0 To SlotNum
                        If BI_Data(i).UnitInfo.FanOnOff Then
                            FansSquence = FansSquence + (2 ^ i)
                        End If
                    Next

                    If IO24.Initialize(CInt(BI_HW.Fan_Port_Number)) Then
                        Call IO24.SetIoDirection("A", FansSquence)
                        Call IO24.SetOutput("A", FansSquence)
                    End If
                Case "SWITCH_RS232"
                    mySwitch_RS232.PortNumber = BI_HW.Fan_Port_Number
                    Call mySwitch_RS232.TurnOnOff(BI_HW.Fan_Chan(slot), BI_Data(slot).UnitInfo.FanOnOff)
                Case "SWITCH_RS422"
                    mySwitch_RS422.PortNumber = BI_HW.Fan_Port_Number
                    Call mySwitch_RS422.TurnOnOff(BI_HW.Fan_Chan(slot), BI_Data(slot).UnitInfo.FanOnOff)
                Case "UUT"
                    If BI_Data(slot).UnitInfo.UnitActive Then
                        If OnOff Then
                            Transceiver(slot).SendCommand("hal -w sysdev 0 OUTPUT_GPIO_AISG24_OFF ON")
                        Else
                            Transceiver(slot).SendCommand("hal -w sysdev 0 OUTPUT_GPIO_AISG24_OFF OFF")
                        End If
                    End If
                Case "SWITCH_9CH_ETH"
                    Call mySwitch_9CH_Eth.TurnRelayOnOff(BI_HW.Anton_Chan(slot), BI_HW.Switch_9CH_EthPort, BI_HW.Anton_Port_Number, OnOff)
                    Delay(50)
            End Select
            'DisplayFanOnOff(slot)
        Catch ex As Exception
            AddMessage("Turn Fan On/Off Error: " & ex.Message)
        End Try

    End Sub

    Public Sub TurnPowerOnOff(ByVal slot As Integer, ByVal OnOff As Boolean)
        Try
            If Not BI_HW.Pwr_Chan_Enabled Then Exit Sub
            If OnOff Then
                AddMessage("Turn slot " & (slot + 1).ToString & " Power on.")
            Else
                AddMessage("Turn slot " & (slot + 1).ToString & " Power off.")
            End If
            Application.DoEvents()
            BI_Data(slot).UnitInfo.PowerOnOff = OnOff
            Select Case UCase(BI_HW.Pwr_Switch_Type)
                Case "SWITCH_USBIO_24"
                    Dim i As Integer, FansSquence As Integer = 0
                    For i = 0 To SlotNum
                        If BI_Data(i).UnitInfo.PowerOnOff Then
                            FansSquence = FansSquence + (2 ^ i)
                        End If
                    Next

                    If IO24.Initialize(CInt(BI_HW.Pwr_Port_Number)) Then
                        Call IO24.SetIoDirection("A", FansSquence)
                        Call IO24.SetOutput("A", FansSquence)
                    End If
                Case "SWITCH_RS232"
                    mySwitch_RS232.PortNumber = BI_HW.Pwr_Port_Number
                    Call mySwitch_RS232.TurnOnOff(BI_HW.Pwr_Chan(slot), BI_Data(slot).UnitInfo.PowerOnOff)
                Case "SWITCH_RS422"
                    mySwitch_RS422.PortNumber = BI_HW.Pwr_Port_Number
                    Call mySwitch_RS422.TurnOnOff(BI_HW.Pwr_Chan(slot), BI_Data(slot).UnitInfo.PowerOnOff)
                Case "SWITCH_9CH_ETH"
                    Call mySwitch_9CH_Eth.TurnRelayOnOff(BI_HW.Pwr_Chan(slot), BI_HW.Switch_9CH_EthPort, BI_HW.Pwr_Port_Number, OnOff)
                    Delay(50)
                Case "AGILENT_PSU"
                    If OnOff Then PS(slot).Setup(48, 15)
                    PS(slot).OutputOn = OnOff
                    Delay(50)
            End Select
            'DisplayPowerOnOff(slot)
        Catch ex As Exception
            AddMessage("Turn Power On/Off Error: " & ex.Message)
        End Try
    End Sub

    Public Sub TurnAntonOnOff(ByVal slot As Integer, ByVal OnOff As Boolean)
        Try
            If Not BI_HW.Anton_Chan_Enabled Then Exit Sub
            If OnOff Then
                AddMessage("Turn slot " & (slot + 1).ToString & " Anton on.")
            Else
                AddMessage("Turn slot " & (slot + 1).ToString & " Anton off.")
            End If
            Application.DoEvents()

            Select Case UCase(BI_HW.Anton_Switch_Type)
                Case "SWITCH_9CH_ETH"
                    Call mySwitch_9CH_Eth.TurnRelayOnOff(BI_HW.Anton_Chan(slot), BI_HW.Switch_9CH_EthPort, BI_HW.Anton_Port_Number, OnOff)
                    Delay(50)
                Case "USB"
                    'Call mySwitch_9CH_Eth.TurnRelayOnOff(BI_HW.Anton_Chan(slot), BI_HW.Switch_9CH_EthPort, BI_HW.Anton_Port_Number, OnOff)
                    Delay(50)
                Case Else

            End Select

        Catch ex As Exception
            AddMessage("Turn Anton On/Off Error: " & ex.Message)
        End Try
    End Sub

    Public Sub ManualTurnPowerOnOff(ByVal OnOff As Boolean)
        Try
            'If BI_HW.PS_Manual_Control Then
            '    If OnOff Then
            '        AddMessage("Manual Turn On Power.")
            '        MessageBox.Show("Please Manual Turn On Power of Shelf:" & ShelfID & ".", "Note", MessageBoxButtons.OK)
            '    Else
            '        AddMessage("Manual Turn Off Power.")
            '        MessageBox.Show("Please Manual Turn Off Power of Shelf:" & ShelfID & ".", "Note", MessageBoxButtons.OK)
            '    End If
            'End If
        Catch ex As Exception
            AddMessage("Manual Turn Power On/Off Error: " & ex.Message)
        End Try
    End Sub

    Public Sub TurnSwitchPSOnOff(ByVal OnOff As Boolean)
        Try
            If Not BI_HW.Switch_PS_Enabled Then Exit Sub
            If OnOff Then
                AddMessage("Turn Switch PS Power on.")
            Else
                AddMessage("Turn Switch PS Power off.")
            End If
            Application.DoEvents()
            'BI_Data(slot).UnitInfo.PowerOnOff = OnOff
            Dim SwitchPSOnOff As Boolean
            SwitchPSOnOff = OnOff
            Select Case UCase(BI_HW.Switch_PS_Type)
                Case "SWITCH_USBIO_24"
                    Dim i As Integer, FansSquence As Integer = 0
                    For i = 0 To SlotNum
                        'If BI_Data(slot).UnitInfo.PowerOnOff Then
                        If SwitchPSOnOff Then
                            FansSquence = FansSquence + (2 ^ i)
                        End If
                    Next
                    If IO24.Initialize(CInt(BI_HW.Switch_PS_COM_Port)) Then
                        Call IO24.SetIoDirection("A", FansSquence)
                        Call IO24.SetOutput("A", FansSquence)
                    End If
                Case "SWITCH_RS232"
                    mySwitch_RS232.PortNumber = BI_HW.Switch_PS_COM_Port
                    Call mySwitch_RS232.TurnOnOff(BI_HW.Switch_PS_Channel, SwitchPSOnOff)
                Case "SWITCH_RS422"
                    mySwitch_RS422.PortNumber = BI_HW.Switch_PS_COM_Port
                    Call mySwitch_RS422.TurnOnOff(BI_HW.Switch_PS_Channel, SwitchPSOnOff)
            End Select
            'DisplayPowerOnOff(slot)
            Delay(5000)
        Catch ex As Exception
            AddMessage("Turn Switch Power On/Off Error: " & ex.Message)
        End Try
    End Sub

    Public Sub IniPS()        'For Power Supply
        'For I As Integer = 0 To 5
        '    If BI_HW.PS_Control_Enabled(I) Then
        '        Try
        '            Select Case UCase(BI_HW.PS_Control_Type(I))
        '                Case "COM_PORT_PS"
        '                    PS(I) = New CN_PS
        '                Case "AGILENT_PS"
        '                    PS(I) = New AgPowerSupply
        '                Case "XDC_PS"
        '                    PS(I) = New XDCPS
        '            End Select
        '            PS(I).Initialize(BI_HW.PS_Control_Address(I))
        '        Catch ex As Exception
        '            AddMessage("PS initialize error: " & ex.Message)
        '        End Try
        '    End If
        'Next
        For i As Integer = 0 To SlotNum
            Try
                Select Case UCase(BI_HW.Pwr_Switch_Type)
                    Case "AGILENT_PSU"
                        PS(i) = New AgPowerSupply
                        PS(i).Initialize(BI_HW.Pwr_Chan(i))
                End Select
            Catch ex As Exception
                AddMessage("PS initialize error: " & ex.Message)
            End Try
        Next
    End Sub

    Public Sub TurnAllPSOnOff(ByVal onoff As Boolean)         'For Power Supply
        Try
            For PSCount As Integer = 0 To 5
                If BI_HW.PS_Control_Enabled(PSCount) Then
                    If onoff Then
                        PS(PSCount).Setup(BI_HW.PS_Voltage(PSCount), BI_HW.PS_Current(PSCount))
                        PS(PSCount).OutputOn = True
                        AddMessage("Turn on PS_" & PSCount + 1)
                    Else
                        PS(PSCount).OutputOn = False
                        AddMessage("Turn off PS_" & PSCount + 1)
                    End If
                End If
            Next
        Catch ex As Exception
            AddMessage("Turn PS on/ff error: " & ex.Message)
        End Try
    End Sub

    Public Sub TurnRFOnOff(ByVal slot As Integer, ByVal OnOff As Boolean)
        Try
            If OnOff Then
                AddMessage("Turn slot " & (slot + 1).ToString & " RF on.")
            Else
                AddMessage("Turn slot " & (slot + 1).ToString & " RF off.")
            End If

            Application.DoEvents()

            'Transceiver(slot).BiasPA(0) = OnOff
            'Transceiver(slot).BiasPA(1) = OnOff
            'V1.1.7
            Transceiver(slot).TxOnOff(1) = OnOff
            Transceiver(slot).TxOnOff(2) = OnOff

            'Transceiver(slot).CustomerOpen()
            'Transceiver(slot).CustomerTxEnable = OnOff

            System.Threading.Thread.Sleep(100)
            BI_Data(slot).MeasData.PAEnabled = OnOff
            Application.DoEvents()

            'Add to close slew rate after each RF On, Light, 2014.10.31
            If OnOff Then
                Transceiver(slot).WriteSlewRate()
                Dim tRetry As Integer = 0
                Do Until Transceiver(slot).CheckSlewRate Or tRetry = 3
                    AddMessage("Check Slew Rate Byte Failed.")
                    Transceiver(slot).WriteSlewRate()
                    tRetry = tRetry + 1
                Loop
            End If
     
        Catch ex As Exception
            AddMessage("Turn RF On/Off Error on slot " & (slot + 1).ToString & ": " & ex.Message)
        End Try
    End Sub

    Public Sub WriteCSVFile()
        Try
            Dim sw As StreamWriter
            For slot As Integer = 0 To SlotNum
                If dgvSlotTestData(slot).RowCount > 1 Then
                    Dim ColCount As Integer = dgvSlotTestData(slot).ColumnCount
                    Dim RowCount As Integer = dgvSlotTestData(slot).RowCount
                    Dim RecData(RowCount) As String

                    For tmpColIndex As Integer = 0 To ColCount - 1
                        If tmpColIndex < ColCount - 1 Then
                            If tmpColIndex = 0 Then
                                RecData(0) = dgvSlotTestData(slot).Columns(tmpColIndex).HeaderText & ","
                            Else
                                RecData(0) = RecData(0) & dgvSlotTestData(slot).Columns(tmpColIndex).HeaderText & ","
                            End If
                        Else
                            RecData(0) = RecData(0) & dgvSlotTestData(slot).Columns(tmpColIndex).HeaderText
                        End If
                    Next

                    For tmpRowIndex As Integer = 0 To RowCount - 1
                        For tmpColIndex As Integer = 0 To ColCount - 1
                            If tmpColIndex < ColCount - 1 Then
                                If tmpColIndex = 0 Then
                                    RecData(tmpRowIndex + 1) = dgvSlotTestData(slot).Rows(tmpRowIndex).Cells(tmpColIndex).Value & ","
                                Else
                                    RecData(tmpRowIndex + 1) = RecData(tmpRowIndex + 1) & dgvSlotTestData(slot).Rows(tmpRowIndex).Cells(tmpColIndex).Value & ","
                                End If
                            Else
                                RecData(tmpRowIndex + 1) = RecData(tmpRowIndex + 1) & dgvSlotTestData(slot).Rows(tmpRowIndex).Cells(tmpColIndex).Value
                            End If
                        Next
                    Next

                    sw = File.AppendText(BI_Data(slot).UnitInfo.CSVFileName)
                    For i As Integer = 0 To RecData.Length - 1
                        sw.WriteLine(RecData(i))
                    Next
                    sw.Close()

                End If
            Next
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Public Sub WriteDatFile()
        Try
            Dim sw As StreamWriter
            For slot As Integer = 0 To SlotNum
                If BI_Data(slot).UnitInfo.UnitActive Then

                    Dim fColCount As Integer = dgvFailureInd(slot).ColumnCount
                    Dim fRowCount As Integer = dgvFailureInd(slot).RowCount
                    Dim FailData(fRowCount + 2) As String

                    FailData(0) = "Serial Number: " & BI_Data(slot).UnitInfo.UnitSN
                    'FailData(1) = "Computer: " & My.Computer.Name & "; Slot: " & slot + 1
                    FailData(1) = "Computer: " & My.Computer.Name & "; Slot: " & ShelfID & slot + 1

                    If BI_Data(slot).RecData.AlarmFlag Then
                        FailData(2) = "Test Result: Fail"
                    Else
                        FailData(2) = "Test Result: Pass"
                    End If

                    If fRowCount > 0 Then
                        For i As Integer = 0 To fRowCount - 1
                            FailData(i + 3) = dgvFailureInd(slot).Rows(i).Cells(0).Value & ": " & dgvFailureInd(slot).Rows(i).Cells(2).Value & "  (LL: " & _
                                              dgvFailureInd(slot).Rows(i).Cells(1).Value & "; HL: " & dgvFailureInd(slot).Rows(i).Cells(3).Value & ")"
                        Next
                    End If


                    Dim ColCount As Integer = dgvSlotTestData(slot).ColumnCount
                    Dim RowCount As Integer = dgvSlotTestData(slot).RowCount
                    Dim RecData(RowCount) As String

                    For tmpColIndex As Integer = 0 To ColCount - 1
                        If tmpColIndex < ColCount - 1 Then
                            If tmpColIndex = 0 Then
                                RecData(0) = dgvSlotTestData(slot).Columns(tmpColIndex).HeaderText & vbTab
                            Else
                                RecData(0) = RecData(0) & dgvSlotTestData(slot).Columns(tmpColIndex).HeaderText & vbTab
                            End If
                        Else
                            RecData(0) = RecData(0) & dgvSlotTestData(slot).Columns(tmpColIndex).HeaderText
                        End If
                    Next

                    For tmpRowIndex As Integer = 0 To RowCount - 1
                        For tmpColIndex As Integer = 0 To ColCount - 1
                            If tmpColIndex < ColCount - 1 Then
                                If tmpColIndex = 0 Then
                                    RecData(tmpRowIndex + 1) = dgvSlotTestData(slot).Rows(tmpRowIndex).Cells(tmpColIndex).Value & vbTab
                                Else
                                    RecData(tmpRowIndex + 1) = RecData(tmpRowIndex + 1) & dgvSlotTestData(slot).Rows(tmpRowIndex).Cells(tmpColIndex).Value & vbTab
                                End If
                            Else
                                RecData(tmpRowIndex + 1) = RecData(tmpRowIndex + 1) & dgvSlotTestData(slot).Rows(tmpRowIndex).Cells(tmpColIndex).Value
                            End If
                        Next
                    Next

                    sw = File.AppendText(BI_Data(slot).UnitInfo.DATFilePath)
                    For i As Integer = 0 To FailData.Length - 1
                        sw.WriteLine(FailData(i))
                    Next

                    For i As Integer = 0 To RecData.Length - 1
                        sw.WriteLine(RecData(i))
                    Next
                    sw.Close()

                End If
            Next
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Public Sub WriteDCFFile()
        Try
            BI_Test_Results.Production_Site_ID = Test_Site
            BI_Test_Results.Test_SW_Rev = SW_Version
            BI_Test_Results.Firmware_Rev = MTR_Version
            BI_Test_Results.Assembly_Type = Assembly_Type
            BI_Test_Results.Test_System_ID = My.Computer.Name.ToString
            BI_Test_Results.StartTestGroup(Test_Group)

            For slot As Integer = 0 To SlotNum
                If BI_Data(slot).UnitInfo.UnitActive Then

                    BI_Test_Results.Assembly_Type = BI_Data(slot).UnitInfo.UnitType

                    With BI_Data(slot).RecData
                        BI_Test_Results.StartTest(.SerialNumber, Test_Group, .StartTime)

                        'General Information
                        BI_Test_Results.RecordNumeric("PA0_Temp_High", .PA0_Temp.High, BI_Limit.BI_High_PA_Temp_LL, BI_Limit.BI_High_PA_Temp_UL)
                        BI_Test_Results.RecordNumeric("PA0_Temp_Low", .PA0_Temp.Low, BI_Limit.BI_Low_PA_Temp_LL, BI_Limit.BI_Low_PA_Temp_UL)

                        BI_Test_Results.RecordNumeric("PA0_VSWR_Temp_High", .PA0_VSWR_Temp.High, BI_Limit.BI_High_PA_VSWR_Temp_LL, BI_Limit.BI_High_PA_VSWR_Temp_UL)
                        BI_Test_Results.RecordNumeric("PA0_VSWR_Temp_Low", .PA0_VSWR_Temp.Low, BI_Limit.BI_Low_PA_VSWR_Temp_LL, BI_Limit.BI_Low_PA_VSWR_Temp_UL)

                        BI_Test_Results.RecordNumeric("PA1_Temp_High", .PA1_Temp.High, BI_Limit.BI_High_PA_Temp_LL, BI_Limit.BI_High_PA_Temp_UL)
                        BI_Test_Results.RecordNumeric("PA1_Temp_Low", .PA1_Temp.Low, BI_Limit.BI_Low_PA_Temp_LL, BI_Limit.BI_Low_PA_Temp_UL)

                        BI_Test_Results.RecordNumeric("PA1_VSWR_Temp_High", .PA1_VSWR_Temp.High, BI_Limit.BI_High_PA_VSWR_Temp_LL, BI_Limit.BI_High_PA_VSWR_Temp_UL)
                        BI_Test_Results.RecordNumeric("PA1_VSWR_Temp_Low", .PA1_VSWR_Temp.Low, BI_Limit.BI_Low_PA_VSWR_Temp_LL, BI_Limit.BI_Low_PA_VSWR_Temp_UL)

                        BI_Test_Results.RecordNumeric("PA_Temp_Delta_High", .PA_Temp_Delta.High, BI_Limit.BI_PA_Temp_Delta_LL, BI_Limit.BI_PA_Temp_Delta_UL)
                        BI_Test_Results.RecordNumeric("PA_Temp_Delta_Low", .PA_Temp_Delta.Low, BI_Limit.BI_PA_Temp_Delta_LL, BI_Limit.BI_PA_Temp_Delta_UL)

                        BI_Test_Results.RecordNumeric("LNA0_Temp_High", .LNA0_Temp.High, BI_Limit.BI_High_LNA01_Temp_LL, BI_Limit.BI_High_LNA01_Temp_UL)
                        BI_Test_Results.RecordNumeric("LNA0_Temp_low", .LNA0_Temp.Low, BI_Limit.BI_Low_LNA01_Temp_LL, BI_Limit.BI_Low_LNA01_Temp_UL)

                        BI_Test_Results.RecordNumeric("LNA1_Temp_High", .LNA1_Temp.High, BI_Limit.BI_High_LNA01_Temp_LL, BI_Limit.BI_High_LNA01_Temp_UL)
                        BI_Test_Results.RecordNumeric("LNA1_Temp_low", .LNA1_Temp.Low, BI_Limit.BI_Low_LNA01_Temp_LL, BI_Limit.BI_Low_LNA01_Temp_UL)

                        BI_Test_Results.RecordNumeric("LNA2_Temp_High", .LNA2_Temp.High, BI_Limit.BI_High_LNA23_Temp_LL, BI_Limit.BI_High_LNA23_Temp_UL)
                        BI_Test_Results.RecordNumeric("LNA2_Temp_low", .LNA2_Temp.Low, BI_Limit.BI_Low_LNA23_Temp_LL, BI_Limit.BI_Low_LNA23_Temp_UL)

                        BI_Test_Results.RecordNumeric("LNA3_Temp_High", .LNA3_Temp.High, BI_Limit.BI_High_LNA23_Temp_LL, BI_Limit.BI_High_LNA23_Temp_UL)
                        BI_Test_Results.RecordNumeric("LNA3_Temp_low", .LNA3_Temp.Low, BI_Limit.BI_Low_LNA23_Temp_LL, BI_Limit.BI_Low_LNA23_Temp_UL)

                        BI_Test_Results.RecordNumeric("PS_Converter_Temp_High", .PS_Converter_Temp.High, BI_Limit.BI_High_PS_Converter_Temp_LL, BI_Limit.BI_High_PS_Converter_Temp_UL)
                        BI_Test_Results.RecordNumeric("PS_Converter_Temp_Low", .PS_Converter_Temp.Low, BI_Limit.BI_Low_PS_Converter_Temp_LL, BI_Limit.BI_Low_PS_Converter_Temp_UL)

                        BI_Test_Results.RecordNumeric("PS_Brick_Temp_High", .PS_Brick_Temp.High, BI_Limit.BI_High_PS_Brick_Temp_LL, BI_Limit.BI_High_PS_Brick_Temp_UL)
                        BI_Test_Results.RecordNumeric("PS_Brick_Temp_Low", .PS_Brick_Temp.Low, BI_Limit.BI_Low_PS_Brick_Temp_LL, BI_Limit.BI_Low_PS_Brick_Temp_UL)


                        BI_Test_Results.RecordNumeric("PSU_Temp_Delta_High", .PSU_Temp_Delta.High, BI_Limit.BI_PSU_Temp_Delta_LL, BI_Limit.BI_PSU_Temp_Delta_UL)
                        BI_Test_Results.RecordNumeric("PSU_Temp_Delta_Low", .PSU_Temp_Delta.Low, BI_Limit.BI_PSU_Temp_Delta_LL, BI_Limit.BI_PSU_Temp_Delta_UL)

                        BI_Test_Results.RecordNumeric("PSU_PA_Temp_Delta_High", .PSU_PA_Temp_Delta.High, BI_Limit.BI_High_PSU_PA_Temp_Delta_LL, BI_Limit.BI_High_PSU_PA_Temp_Delta_UL)
                        BI_Test_Results.RecordNumeric("PSU_PA_Temp_Delta_Low", .PSU_PA_Temp_Delta.Low, BI_Limit.BI_Low_PSU_PA_Temp_Delta_LL, BI_Limit.BI_Low_PSU_PA_Temp_Delta_UL)

                        BI_Test_Results.RecordNumeric("FB_Temp_High", .FB_Temp.High, BI_Limit.BI_High_FB_Temp_LL, BI_Limit.BI_High_FB_Temp_UL)
                        BI_Test_Results.RecordNumeric("FB_Temp_Low", .FB_Temp.Low, BI_Limit.BI_Low_FB_Temp_LL, BI_Limit.BI_Low_FB_Temp_UL)

                        BI_Test_Results.RecordNumeric("RX_Temp_High", .RX_Temp.High, BI_Limit.BI_High_RX_Temp_LL, BI_Limit.BI_High_RX_Temp_UL)
                        BI_Test_Results.RecordNumeric("RX_Temp_Low", .RX_Temp.Low, BI_Limit.BI_Low_RX_Temp_LL, BI_Limit.BI_Low_RX_Temp_UL)

                        'TX0
                        BI_Test_Results.RecordNumeric("TX0_Output_Pow_High", .TX0_Output_Pow.High, BI_Limit.BI_High_TX_Output_Pow_LL, BI_Limit.BI_High_TX_Output_Pow_UL)
                        BI_Test_Results.RecordNumeric("TX0_Output_Pow_Low", .TX0_Output_Pow.Low, BI_Limit.BI_Low_TX_Output_Pow_LL, BI_Limit.BI_Low_TX_Output_Pow_UL)

                        BI_Test_Results.RecordNumeric("TX0_VSWR_High", .TX0_VSWR.High, BI_Limit.BI_High_TX_VSWR_LL, BI_Limit.BI_High_TX_VSWR_UL)
                        BI_Test_Results.RecordNumeric("TX0_VSWR_Low", .TX0_VSWR.Low, BI_Limit.BI_Low_TX_VSWR_LL, BI_Limit.BI_Low_TX_VSWR_UL)

                        BI_Test_Results.RecordNumeric("TX0_Forward_Power_Detector_High", .TX0_Forward_Power_Detector.High, BI_Limit.BI_High_TX_Forward_Power_Detector_LL, BI_Limit.BI_High_TX_Forward_Power_Detector_UL)
                        BI_Test_Results.RecordNumeric("TX0_Forward_Power_Detector_Low", .TX0_Forward_Power_Detector.Low, BI_Limit.BI_Low_TX_Forward_Power_Detector_LL, BI_Limit.BI_Low_TX_Forward_Power_Detector_UL)

                        BI_Test_Results.RecordNumeric("TX0_Reverse_Power_Detector_High", .TX0_Reverse_Power_Detector.High, BI_Limit.BI_High_TX_Reverse_Power_Detector_LL, BI_Limit.BI_High_TX_Reverse_Power_Detector_UL)
                        BI_Test_Results.RecordNumeric("TX0_Reverse_Power_Detector_Low", .TX0_Reverse_Power_Detector.Low, BI_Limit.BI_Low_TX_Reverse_Power_Detector_LL, BI_Limit.BI_Low_TX_Reverse_Power_Detector_UL)

                        BI_Test_Results.RecordNumeric("TX0_Gain_VCA_High", .TX0_Gain_VCA.High, BI_Limit.BI_High_TX_Gain_VCA_LL, BI_Limit.BI_High_TX_Gain_VCA_UL)
                        BI_Test_Results.RecordNumeric("TX0_Gain_VCA_Low", .TX0_Gain_VCA.Low, BI_Limit.BI_Low_TX_Gain_VCA_LL, BI_Limit.BI_Low_TX_Gain_VCA_UL)

                        BI_Test_Results.RecordNumeric("TX0_txDacGain_High", .TX0_txDacGain.High, BI_Limit.BI_High_TX_txDacGain_LL, BI_Limit.BI_High_TX_txDacGain_UL)
                        BI_Test_Results.RecordNumeric("TX0_txDacGain_Low", .TX0_txDacGain.Low, BI_Limit.BI_Low_TX_txDacGain_LL, BI_Limit.BI_High_TX_txDacGain_UL)

                        BI_Test_Results.RecordNumeric("TX0_totalTxAttn_High", .TX0_totalTxAttn.High, BI_Limit.BI_High_TX_totalTxAttn_LL, BI_Limit.BI_High_TX_totalTxAttn_UL)
                        BI_Test_Results.RecordNumeric("TX0_totalTxAttn_Low", .TX0_totalTxAttn.Low, BI_Limit.BI_Low_TX_totalTxAttn_LL, BI_Limit.BI_Low_TX_totalTxAttn_UL)

                        BI_Test_Results.RecordNumeric("TX0_Gain_TxStep_High", .TX0_Gain_TxStep.High, BI_Limit.BI_High_TX_Gain_TxStep_LL, BI_Limit.BI_High_TX_Gain_TxStep_UL)
                        BI_Test_Results.RecordNumeric("TX0_Gain_TxStep_Low", .TX0_Gain_TxStep.Low, BI_Limit.BI_Low_TX_Gain_TxStep_LL, BI_Limit.BI_Low_TX_Gain_TxStep_UL)

                        BI_Test_Results.RecordNumeric("TX0_Gain_FbStep_High", .TX0_Gain_FbStep.High, BI_Limit.BI_High_TX_Gain_FbStep_LL, BI_Limit.BI_High_TX_Gain_FbStep_UL)
                        BI_Test_Results.RecordNumeric("TX0_Gain_FbStep_Low", .TX0_Gain_FbStep.Low, BI_Limit.BI_Low_TX_Gain_FbStep_LL, BI_Limit.BI_Low_TX_Gain_FbStep_UL)

                        BI_Test_Results.RecordNumeric("TX0_Gain_FbTxQuo_High", .TX0_Gain_FbTxQuo.High, BI_Limit.BI_High_TX_Gain_FbTxQuo_LL, BI_Limit.BI_High_TX_Gain_FbTxQuo_UL)
                        BI_Test_Results.RecordNumeric("TX0_Gain_FbTxQuo_low", .TX0_Gain_FbTxQuo.Low, BI_Limit.BI_Low_TX_Gain_FbTxQuo_LL, BI_Limit.BI_Low_TX_Gain_FbTxQuo_UL)

                        BI_Test_Results.RecordNumeric("TX0_Gain_GainError_High", .TX0_Gain_GainError.High, BI_Limit.BI_High_TX_Gain_GainError_LL, BI_Limit.BI_High_TX_Gain_GainError_UL)
                        BI_Test_Results.RecordNumeric("TX0_Gain_GainError_Low", .TX0_Gain_GainError.Low, BI_Limit.BI_Low_TX_Gain_GainError_LL, BI_Limit.BI_Low_TX_Gain_GainError_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_PsVolt_High", .TX0_PA0_PsVolt.High, BI_Limit.BI_High_TX_PA_PsVolt_LL, BI_Limit.BI_High_TX_PA_PsVolt_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_PsVolt_Low", .TX0_PA0_PsVolt.Low, BI_Limit.BI_Low_TX_PA_PsVolt_LL, BI_Limit.BI_Low_TX_PA_PsVolt_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Temp_High", .TX0_PA0_Temp.High, BI_Limit.BI_High_TX_PA_Temp_LL, BI_Limit.BI_High_TX_PA_Temp_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Temp_Low", .TX0_PA0_Temp.Low, BI_Limit.BI_Low_TX_PA_Temp_LL, BI_Limit.BI_Low_TX_PA_Temp_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_BiasTemp_High", .TX0_PA0_BiasTemp.High, BI_Limit.BI_High_TX_PA_BiasTemp_LL, BI_Limit.BI_High_TX_PA_BiasTemp_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_BiasTemp_Low", .TX0_PA0_BiasTemp.Low, BI_Limit.BI_Low_TX_PA_BiasTemp_LL, BI_Limit.BI_Low_TX_PA_BiasTemp_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver1Cur_High", .TX0_PA0_Driver1Cur.High, BI_Limit.BI_High_TX_PA_Driver1Cur_LL, BI_Limit.BI_High_TX_PA_Driver1Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver1Cur_Low", .TX0_PA0_Driver1Cur.Low, BI_Limit.BI_Low_TX_PA_Driver1Cur_LL, BI_Limit.BI_Low_TX_PA_Driver1Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver1Cur_Delta", .TX0_PA0_Driver1Cur.High - .TX0_PA0_Driver1Cur.Low, BI_Limit.BI_Delta_TX_PA_Driver1Cur_LL, BI_Limit.BI_Delta_TX_PA_Driver1Cur_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver2Cur_High", .TX0_PA0_Driver2Cur.High, BI_Limit.BI_High_TX_PA_Driver2Cur_LL, BI_Limit.BI_High_TX_PA_Driver2Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver2Cur_Low", .TX0_PA0_Driver2Cur.Low, BI_Limit.BI_Low_TX_PA_Driver2Cur_LL, BI_Limit.BI_Low_TX_PA_Driver2Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver2Cur_Delta", .TX0_PA0_Driver2Cur.High - .TX0_PA0_Driver2Cur.Low, BI_Limit.BI_Delta_TX_PA_Driver2Cur_LL, BI_Limit.BI_Delta_TX_PA_Driver2Cur_UL)


                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver3Cur_High", .TX0_PA0_Driver3Cur.High, BI_Limit.BI_High_TX_PA_Driver3Cur_LL, BI_Limit.BI_High_TX_PA_Driver3Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver3Cur_Low", .TX0_PA0_Driver3Cur.Low, BI_Limit.BI_Low_TX_PA_Driver3Cur_LL, BI_Limit.BI_Low_TX_PA_Driver3Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver3Cur_Delta", .TX0_PA0_Driver3Cur.High - .TX0_PA0_Driver3Cur.Low, BI_Limit.BI_Delta_TX_PA_Driver3Cur_LL, BI_Limit.BI_Delta_TX_PA_Driver3Cur_UL)


                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver4Cur_High", .TX0_PA0_Driver4Cur.High, BI_Limit.BI_High_TX_PA_Driver4Cur_LL, BI_Limit.BI_High_TX_PA_Driver4Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver4Cur_Low", .TX0_PA0_Driver4Cur.Low, BI_Limit.BI_Low_TX_PA_Driver4Cur_LL, BI_Limit.BI_Low_TX_PA_Driver4Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver4Cur_Delta", .TX0_PA0_Driver4Cur.High - .TX0_PA0_Driver4Cur.Low, BI_Limit.BI_Delta_TX_PA_Driver4Cur_LL, BI_Limit.BI_Delta_TX_PA_Driver4Cur_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Final1Cur_High", .TX0_PA0_Final1Cur.High, BI_Limit.BI_High_TX_PA_Final1Cur_LL, BI_Limit.BI_High_TX_PA_Final1Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Final1Cur_Low", .TX0_PA0_Final1Cur.Low, BI_Limit.BI_Low_TX_PA_Final1Cur_LL, BI_Limit.BI_Low_TX_PA_Final1Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Final1Cur_Delta", .TX0_PA0_Final1Cur.High - .TX0_PA0_Final1Cur.Low, BI_Limit.BI_Delta_TX_PA_Final1Cur_LL, BI_Limit.BI_Delta_TX_PA_Final1Cur_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Final2Cur_High", .TX0_PA0_Final2Cur.High, BI_Limit.BI_High_TX_PA_Final2Cur_LL, BI_Limit.BI_High_TX_PA_Final2Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Final2Cur_Low", .TX0_PA0_Final2Cur.Low, BI_Limit.BI_Low_TX_PA_Final2Cur_LL, BI_Limit.BI_Low_TX_PA_Final2Cur_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Final2Cur_Delta", .TX0_PA0_Final2Cur.High - .TX0_PA0_Final2Cur.Low, BI_Limit.BI_Delta_TX_PA_Final2Cur_LL, BI_Limit.BI_Delta_TX_PA_Final2Cur_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver1Dac_High", .TX0_PA0_Driver1Dac.High, BI_Limit.BI_High_TX_PA_Driver1Dac_LL, BI_Limit.BI_High_TX_PA_Driver1Dac_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver1Dac_Low", .TX0_PA0_Driver1Dac.Low, BI_Limit.BI_Low_TX_PA_Driver1Dac_LL, BI_Limit.BI_Low_TX_PA_Driver1Dac_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver2Dac_High", .TX0_PA0_Driver2Dac.High, BI_Limit.BI_High_TX_PA_Driver2Dac_LL, BI_Limit.BI_High_TX_PA_Driver2Dac_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver2Dac_Low", .TX0_PA0_Driver2Dac.Low, BI_Limit.BI_Low_TX_PA_Driver2Dac_LL, BI_Limit.BI_Low_TX_PA_Driver2Dac_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver3Dac_High", .TX0_PA0_Driver3Dac.High, BI_Limit.BI_High_TX_PA_Driver3Dac_LL, BI_Limit.BI_High_TX_PA_Driver3Dac_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver3Dac_Low", .TX0_PA0_Driver3Dac.Low, BI_Limit.BI_Low_TX_PA_Driver3Dac_LL, BI_Limit.BI_Low_TX_PA_Driver3Dac_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver4Dac_High", .TX0_PA0_Driver4Dac.High, BI_Limit.BI_High_TX_PA_Driver4Dac_LL, BI_Limit.BI_High_TX_PA_Driver4Dac_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Driver4Dac_Low", .TX0_PA0_Driver4Dac.Low, BI_Limit.BI_Low_TX_PA_Driver4Dac_LL, BI_Limit.BI_Low_TX_PA_Driver4Dac_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Final1Dac_High", .TX0_PA0_Final1Dac.High, BI_Limit.BI_High_TX_PA_Final1Dac_LL, BI_Limit.BI_High_TX_PA_Final1Dac_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Final1Dac_Low", .TX0_PA0_Final1Dac.Low, BI_Limit.BI_Low_TX_PA_Final1Dac_LL, BI_Limit.BI_Low_TX_PA_Final1Dac_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_Final2Dac_High", .TX0_PA0_Final2Dac.High, BI_Limit.BI_High_TX_PA_Final2Dac_LL, BI_Limit.BI_High_TX_PA_Final2Dac_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_Final2Dac_Low", .TX0_PA0_Final2Dac.Low, BI_Limit.BI_Low_TX_PA_Final2Dac_LL, BI_Limit.BI_Low_TX_PA_Final2Dac_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_ampDelayInt_High", .TX0_PA0_ampDelayInt.High, BI_Limit.BI_High_TX_PA_ampDelayInt_LL, BI_Limit.BI_High_TX_PA_ampDelayInt_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_ampDelayInt_Low", .TX0_PA0_ampDelayInt.Low, BI_Limit.BI_Low_TX_PA_ampDelayInt_LL, BI_Limit.BI_Low_TX_PA_ampDelayInt_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_ampDelayFrac_High", .TX0_PA0_ampDelayFrac.High, BI_Limit.BI_High_TX_PA_ampDelayFrac_LL, BI_Limit.BI_High_TX_PA_ampDelayFrac_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_ampDelayFrac_Low", .TX0_PA0_ampDelayFrac.Low, BI_Limit.BI_Low_TX_PA_ampDelayFrac_LL, BI_Limit.BI_Low_TX_PA_ampDelayFrac_UL)

                        BI_Test_Results.RecordNumeric("TX0_PA0_MaxCrossCorrelation_High", .TX0_PA0_MaxCrossCorrelation.High, BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_LL, BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_UL)
                        BI_Test_Results.RecordNumeric("TX0_PA0_MaxCrossCorrelation_Low", .TX0_PA0_MaxCrossCorrelation.Low, BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_LL, BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_UL)

                        BI_Test_Results.RecordNumeric("TX0_Tx0_DPD_L1_Table_Max_Gain_High", .TX0_DPD_L1_Table_Max_Gain.High, BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_UL)
                        BI_Test_Results.RecordNumeric("TX0_Tx0_DPD_L1_Table_Max_Gain_Low", .TX0_DPD_L1_Table_Max_Gain.Low, BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_LL, BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_UL)

                        BI_Test_Results.RecordNumeric("TX0_Tx0_DPD_L1_Table_Min_Gain_High", .TX0_DPD_L1_Table_Min_Gain.High, BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_UL)
                        BI_Test_Results.RecordNumeric("TX0_Tx0_DPD_L1_Table_Min_Gain_Low", .TX0_DPD_L1_Table_Min_Gain.Low, BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_LL, BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_3rd_sym_am_High", .Tx0_DPD_L2_3rd_sym_am.High, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_3rd_sym_am_Low", .Tx0_DPD_L2_3rd_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_LL, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_3rd_sym_ph_High", .Tx0_DPD_L2_3rd_sym_ph.High, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_3rd_sym_ph_Low", .Tx0_DPD_L2_3rd_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L3_3rd_sym_am_High", .Tx0_DPD_L3_3rd_sym_am.High, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L3_3rd_sym_am_Low", .Tx0_DPD_L3_3rd_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_LL, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L3_3rd_sym_ph_High", .Tx0_DPD_L3_3rd_sym_ph.High, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L3_3rd_sym_ph_Low", .Tx0_DPD_L3_3rd_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_5th_sym_am_High", .Tx0_DPD_L2_5th_sym_am.High, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_5th_sym_am_Low", .Tx0_DPD_L2_5th_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_LL, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_5th_sym_ph_High", .Tx0_DPD_L2_5th_sym_ph.High, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_5th_sym_ph_Low", .Tx0_DPD_L2_5th_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L3_5th_sym_am_High", .Tx0_DPD_L3_5th_sym_am.High, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L3_5th_sym_am_Low", .Tx0_DPD_L3_5th_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_LL, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L3_5th_sym_ph_High", .Tx0_DPD_L3_5th_sym_ph.High, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L3_5th_sym_ph_Low", .Tx0_DPD_L3_5th_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_3rd_asym_am_High", .Tx0_DPD_L2_3rd_asym_am.High, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_3rd_asym_am_Low", .Tx0_DPD_L2_3rd_asym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_LL, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_3rd_asym_ph_High", .Tx0_DPD_L2_3rd_asym_ph.High, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_3rd_asym_ph_Low", .Tx0_DPD_L2_3rd_asym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_5th_asym_am_High", .Tx0_DPD_L2_5th_asym_am.High, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_5th_asym_am_Low", .Tx0_DPD_L2_5th_asym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_LL, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_5th_asym_ph_High", .Tx0_DPD_L2_5th_asym_ph.High, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx0_DPD_L2_5th_asym_ph_Low", .Tx0_DPD_L2_5th_asym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_UL)

                        'TX1
                        BI_Test_Results.RecordNumeric("TX1_Output_Pow_High", .TX1_Output_Pow.High, BI_Limit.BI_High_TX_Output_Pow_LL, BI_Limit.BI_High_TX_Output_Pow_UL)
                        BI_Test_Results.RecordNumeric("TX1_Output_Pow_Low", .TX1_Output_Pow.Low, BI_Limit.BI_Low_TX_Output_Pow_LL, BI_Limit.BI_Low_TX_Output_Pow_UL)

                        BI_Test_Results.RecordNumeric("TX1_VSWR_High", .TX1_VSWR.High, BI_Limit.BI_High_TX_VSWR_LL, BI_Limit.BI_High_TX_VSWR_UL)
                        BI_Test_Results.RecordNumeric("TX1_VSWR_Low", .TX1_VSWR.Low, BI_Limit.BI_Low_TX_VSWR_LL, BI_Limit.BI_Low_TX_VSWR_UL)

                        BI_Test_Results.RecordNumeric("TX1_Forward_Power_Detector_High", .TX1_Forward_Power_Detector.High, BI_Limit.BI_High_TX_Forward_Power_Detector_LL, BI_Limit.BI_High_TX_Forward_Power_Detector_UL)
                        BI_Test_Results.RecordNumeric("TX1_Forward_Power_Detector_Low", .TX1_Forward_Power_Detector.Low, BI_Limit.BI_Low_TX_Forward_Power_Detector_LL, BI_Limit.BI_Low_TX_Forward_Power_Detector_UL)

                        BI_Test_Results.RecordNumeric("TX1_Reverse_Power_Detector_High", .TX1_Reverse_Power_Detector.High, BI_Limit.BI_High_TX_Reverse_Power_Detector_LL, BI_Limit.BI_High_TX_Reverse_Power_Detector_UL)
                        BI_Test_Results.RecordNumeric("TX1_Reverse_Power_Detector_Low", .TX1_Reverse_Power_Detector.Low, BI_Limit.BI_Low_TX_Reverse_Power_Detector_LL, BI_Limit.BI_Low_TX_Reverse_Power_Detector_UL)

                        BI_Test_Results.RecordNumeric("TX1_Gain_VCA_High", .TX1_Gain_VCA.High, BI_Limit.BI_High_TX_Gain_VCA_LL, BI_Limit.BI_High_TX_Gain_VCA_UL)
                        BI_Test_Results.RecordNumeric("TX1_Gain_VCA_Low", .TX1_Gain_VCA.Low, BI_Limit.BI_High_TX_Gain_VCA_LL, BI_Limit.BI_High_TX_Gain_VCA_UL)

                        BI_Test_Results.RecordNumeric("TX1_txDacGain_High", .TX1_txDacGain.High, BI_Limit.BI_High_TX_txDacGain_LL, BI_Limit.BI_High_TX_txDacGain_UL)
                        BI_Test_Results.RecordNumeric("TX1_txDacGain_Low", .TX1_txDacGain.Low, BI_Limit.BI_Low_TX_txDacGain_LL, BI_Limit.BI_High_TX_txDacGain_UL)

                        BI_Test_Results.RecordNumeric("TX1_totalTxAttn_High", .TX1_totalTxAttn.High, BI_Limit.BI_High_TX_totalTxAttn_LL, BI_Limit.BI_High_TX_totalTxAttn_UL)
                        BI_Test_Results.RecordNumeric("TX1_totalTxAttn_Low", .TX1_totalTxAttn.Low, BI_Limit.BI_Low_TX_totalTxAttn_LL, BI_Limit.BI_Low_TX_totalTxAttn_UL)

                        BI_Test_Results.RecordNumeric("TX1_Gain_TxStep_High", .TX1_Gain_TxStep.High, BI_Limit.BI_High_TX_Gain_TxStep_LL, BI_Limit.BI_High_TX_Gain_TxStep_UL)
                        BI_Test_Results.RecordNumeric("TX1_Gain_TxStep_Low", .TX1_Gain_TxStep.Low, BI_Limit.BI_Low_TX_Gain_TxStep_LL, BI_Limit.BI_Low_TX_Gain_TxStep_UL)

                        BI_Test_Results.RecordNumeric("TX1_Gain_FbStep_High", .TX1_Gain_FbStep.High, BI_Limit.BI_High_TX_Gain_FbStep_LL, BI_Limit.BI_High_TX_Gain_FbStep_UL)
                        BI_Test_Results.RecordNumeric("TX1_Gain_FbStep_Low", .TX1_Gain_FbStep.Low, BI_Limit.BI_Low_TX_Gain_FbStep_LL, BI_Limit.BI_Low_TX_Gain_FbStep_UL)

                        BI_Test_Results.RecordNumeric("TX1_Gain_FbTxQuo_High", .TX1_Gain_FbTxQuo.High, BI_Limit.BI_High_TX_Gain_FbTxQuo_LL, BI_Limit.BI_High_TX_Gain_FbTxQuo_UL)
                        BI_Test_Results.RecordNumeric("TX1_Gain_FbTxQuo_low", .TX1_Gain_FbTxQuo.Low, BI_Limit.BI_Low_TX_Gain_FbTxQuo_LL, BI_Limit.BI_Low_TX_Gain_FbTxQuo_UL)

                        BI_Test_Results.RecordNumeric("TX1_Gain_GainError_High", .TX1_Gain_GainError.High, BI_Limit.BI_High_TX_Gain_GainError_LL, BI_Limit.BI_High_TX_Gain_GainError_UL)
                        BI_Test_Results.RecordNumeric("TX1_Gain_GainError_Low", .TX1_Gain_GainError.Low, BI_Limit.BI_Low_TX_Gain_GainError_LL, BI_Limit.BI_Low_TX_Gain_GainError_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_PsVolt_High", .TX1_PA1_PsVolt.High, BI_Limit.BI_High_TX_PA_PsVolt_LL, BI_Limit.BI_High_TX_PA_PsVolt_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_PsVolt_Low", .TX1_PA1_PsVolt.Low, BI_Limit.BI_Low_TX_PA_PsVolt_LL, BI_Limit.BI_Low_TX_PA_PsVolt_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Temp_High", .TX1_PA1_Temp.High, BI_Limit.BI_High_TX_PA_Temp_LL, BI_Limit.BI_High_TX_PA_Temp_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Temp_Low", .TX1_PA1_Temp.Low, BI_Limit.BI_Low_TX_PA_Temp_LL, BI_Limit.BI_Low_TX_PA_Temp_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_BiasTemp_High", .TX1_PA1_BiasTemp.High, BI_Limit.BI_High_TX_PA_BiasTemp_LL, BI_Limit.BI_High_TX_PA_BiasTemp_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_BIasTemp_Low", .TX1_PA1_BiasTemp.Low, BI_Limit.BI_Low_TX_PA_BiasTemp_LL, BI_Limit.BI_Low_TX_PA_BiasTemp_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver1Cur_High", .TX1_PA1_Driver1Cur.High, BI_Limit.BI_High_TX_PA_Driver1Cur_LL, BI_Limit.BI_High_TX_PA_Driver1Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver1Cur_Low", .TX1_PA1_Driver1Cur.Low, BI_Limit.BI_Low_TX_PA_Driver1Cur_LL, BI_Limit.BI_Low_TX_PA_Driver1Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver1Cur_Delta", .TX1_PA1_Driver1Cur.High - .TX1_PA1_Driver1Cur.Low, BI_Limit.BI_Delta_TX_PA_Driver1Cur_LL, BI_Limit.BI_Delta_TX_PA_Driver1Cur_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver2Cur_High", .TX1_PA1_Driver2Cur.High, BI_Limit.BI_High_TX_PA_Driver2Cur_LL, BI_Limit.BI_High_TX_PA_Driver2Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver2Cur_Low", .TX1_PA1_Driver2Cur.Low, BI_Limit.BI_Low_TX_PA_Driver2Cur_LL, BI_Limit.BI_Low_TX_PA_Driver2Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver2Cur_Delta", .TX1_PA1_Driver2Cur.High - .TX1_PA1_Driver2Cur.Low, BI_Limit.BI_Delta_TX_PA_Driver2Cur_LL, BI_Limit.BI_Delta_TX_PA_Driver2Cur_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver3Cur_High", .TX1_PA1_Driver3Cur.High, BI_Limit.BI_High_TX_PA_Driver3Cur_LL, BI_Limit.BI_High_TX_PA_Driver3Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver3Cur_Low", .TX1_PA1_Driver3Cur.Low, BI_Limit.BI_Low_TX_PA_Driver3Cur_LL, BI_Limit.BI_Low_TX_PA_Driver3Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver3Cur_Delta", .TX1_PA1_Driver3Cur.High - .TX1_PA1_Driver3Cur.Low, BI_Limit.BI_Delta_TX_PA_Driver3Cur_LL, BI_Limit.BI_Delta_TX_PA_Driver3Cur_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver4Cur_High", .TX1_PA1_Driver4Cur.High, BI_Limit.BI_High_TX_PA_Driver4Cur_LL, BI_Limit.BI_High_TX_PA_Driver4Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver4Cur_Low", .TX1_PA1_Driver4Cur.Low, BI_Limit.BI_Low_TX_PA_Driver4Cur_LL, BI_Limit.BI_Low_TX_PA_Driver4Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver4Cur_Delta", .TX1_PA1_Driver4Cur.High - .TX1_PA1_Driver4Cur.Low, BI_Limit.BI_Delta_TX_PA_Driver4Cur_LL, BI_Limit.BI_Delta_TX_PA_Driver4Cur_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Final1Cur_High", .TX1_PA1_Final1Cur.High, BI_Limit.BI_High_TX_PA_Final1Cur_LL, BI_Limit.BI_High_TX_PA_Final1Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Final1Cur_Low", .TX1_PA1_Final1Cur.Low, BI_Limit.BI_Low_TX_PA_Final1Cur_LL, BI_Limit.BI_Low_TX_PA_Final1Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Final1Curr_Delta", .TX1_PA1_Final1Cur.High - .TX1_PA1_Final1Cur.Low, BI_Limit.BI_Delta_TX_PA_Final1Cur_LL, BI_Limit.BI_Delta_TX_PA_Final1Cur_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Final2Cur_High", .TX1_PA1_Final2Cur.High, BI_Limit.BI_High_TX_PA_Final2Cur_LL, BI_Limit.BI_High_TX_PA_Final2Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Final2Cur_Low", .TX1_PA1_Final2Cur.Low, BI_Limit.BI_Low_TX_PA_Final2Cur_LL, BI_Limit.BI_Low_TX_PA_Final2Cur_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Final2Curr_Delta", .TX1_PA1_Final2Cur.High - .TX1_PA1_Final2Cur.Low, BI_Limit.BI_Delta_TX_PA_Final2Cur_LL, BI_Limit.BI_Delta_TX_PA_Final2Cur_UL)


                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver1Dac_High", .TX1_PA1_Driver1Dac.High, BI_Limit.BI_High_TX_PA_Driver1Dac_LL, BI_Limit.BI_High_TX_PA_Driver1Dac_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver1Dac_Low", .TX1_PA1_Driver1Dac.Low, BI_Limit.BI_Low_TX_PA_Driver1Dac_LL, BI_Limit.BI_Low_TX_PA_Driver1Dac_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver2Dac_High", .TX1_PA1_Driver2Dac.High, BI_Limit.BI_High_TX_PA_Driver2Dac_LL, BI_Limit.BI_High_TX_PA_Driver2Dac_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver2Dac_Low", .TX1_PA1_Driver2Dac.Low, BI_Limit.BI_Low_TX_PA_Driver2Dac_LL, BI_Limit.BI_Low_TX_PA_Driver2Dac_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver3Dac_High", .TX1_PA1_Driver3Dac.High, BI_Limit.BI_High_TX_PA_Driver3Dac_LL, BI_Limit.BI_High_TX_PA_Driver3Dac_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver3Dac_Low", .TX1_PA1_Driver3Dac.Low, BI_Limit.BI_Low_TX_PA_Driver3Dac_LL, BI_Limit.BI_Low_TX_PA_Driver3Dac_UL)


                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver4Dac_High", .TX1_PA1_Driver4Dac.High, BI_Limit.BI_High_TX_PA_Driver4Dac_LL, BI_Limit.BI_High_TX_PA_Driver4Dac_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Driver4Dac_Low", .TX1_PA1_Driver4Dac.Low, BI_Limit.BI_Low_TX_PA_Driver4Dac_LL, BI_Limit.BI_Low_TX_PA_Driver4Dac_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Final1Dac_High", .TX1_PA1_Final1Dac.High, BI_Limit.BI_High_TX_PA_Final1Dac_LL, BI_Limit.BI_High_TX_PA_Final1Dac_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Final1Dac_Low", .TX1_PA1_Final1Dac.Low, BI_Limit.BI_Low_TX_PA_Final1Dac_LL, BI_Limit.BI_Low_TX_PA_Final1Dac_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_Final2Dac_High", .TX1_PA1_Final2Dac.High, BI_Limit.BI_High_TX_PA_Final2Dac_LL, BI_Limit.BI_High_TX_PA_Final2Dac_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_Final2Dac_Low", .TX1_PA1_Final2Dac.Low, BI_Limit.BI_Low_TX_PA_Final2Dac_LL, BI_Limit.BI_Low_TX_PA_Final2Dac_UL)


                        BI_Test_Results.RecordNumeric("TX1_PA1_ampDelayInt_High", .TX1_PA1_ampDelayInt.High, BI_Limit.BI_High_TX_PA_ampDelayInt_LL, BI_Limit.BI_High_TX_PA_ampDelayInt_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_ampDelayInt_Low", .TX1_PA1_ampDelayInt.Low, BI_Limit.BI_Low_TX_PA_ampDelayInt_LL, BI_Limit.BI_Low_TX_PA_ampDelayInt_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_ampDelayFrac_High", .TX1_PA1_ampDelayFrac.High, BI_Limit.BI_High_TX_PA_ampDelayFrac_LL, BI_Limit.BI_High_TX_PA_ampDelayFrac_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_ampDelayFrac_Low", .TX1_PA1_ampDelayFrac.Low, BI_Limit.BI_Low_TX_PA_ampDelayFrac_LL, BI_Limit.BI_Low_TX_PA_ampDelayFrac_UL)

                        BI_Test_Results.RecordNumeric("TX1_PA1_MaxCrossCorrelation_High", .TX1_PA1_MaxCrossCorrelation.High, BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_LL, BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_UL)
                        BI_Test_Results.RecordNumeric("TX1_PA1_MaxCrossCorrelation_Low", .TX1_PA1_MaxCrossCorrelation.Low, BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_LL, BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_UL)

                        BI_Test_Results.RecordNumeric("TX1_Tx1_DPD_L1_Table_Max_Gain_High", .TX1_DPD_L1_Table_Max_Gain.High, BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_UL)
                        BI_Test_Results.RecordNumeric("TX1_Tx1_DPD_L1_Table_Max_Gain_Low", .TX1_DPD_L1_Table_Max_Gain.Low, BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_LL, BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_UL)

                        BI_Test_Results.RecordNumeric("TX1_Tx1_DPD_L1_Table_Min_Gain_High", .TX1_DPD_L1_Table_Min_Gain.High, BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_UL)
                        BI_Test_Results.RecordNumeric("TX1_Tx1_DPD_L1_Table_Min_Gain_Low", .TX1_DPD_L1_Table_Min_Gain.Low, BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_LL, BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_3rd_sym_am_High", .Tx1_DPD_L2_3rd_sym_am.High, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_3rd_sym_am_Low", .Tx1_DPD_L2_3rd_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_LL, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_3rd_sym_ph_High", .Tx1_DPD_L2_3rd_sym_ph.High, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_3rd_sym_ph_Low", .Tx1_DPD_L2_3rd_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L3_3rd_sym_am_High", .Tx1_DPD_L3_3rd_sym_am.High, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L3_3rd_sym_am_Low", .Tx1_DPD_L3_3rd_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_LL, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L3_3rd_sym_ph_High", .Tx1_DPD_L3_3rd_sym_ph.High, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L3_3rd_sym_ph_Low", .Tx1_DPD_L3_3rd_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_5th_sym_am_High", .Tx1_DPD_L2_5th_sym_am.High, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_5th_sym_am_Low", .Tx1_DPD_L2_5th_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_LL, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_5th_sym_ph_High", .Tx1_DPD_L2_5th_sym_ph.High, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_5th_sym_ph_Low", .Tx1_DPD_L2_5th_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L3_5th_sym_am_High", .Tx1_DPD_L3_5th_sym_am.High, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L3_5th_sym_am_Low", .Tx1_DPD_L3_5th_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_LL, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L3_5th_sym_ph_High", .Tx1_DPD_L3_5th_sym_ph.High, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L3_5th_sym_ph_Low", .Tx1_DPD_L3_5th_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_3rd_asym_am_High", .Tx1_DPD_L2_3rd_asym_am.High, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_3rd_asym_am_Low", .Tx1_DPD_L2_3rd_asym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_LL, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_3rd_asym_ph_High", .Tx1_DPD_L2_3rd_asym_ph.High, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_3rd_asym_ph_Low", .Tx1_DPD_L2_3rd_asym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_5th_asym_am_High", .Tx1_DPD_L2_5th_asym_am.High, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_5th_asym_am_Low", .Tx1_DPD_L2_5th_asym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_LL, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_UL)

                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_5th_asym_ph_High", .Tx1_DPD_L2_5th_asym_ph.High, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_UL)
                        BI_Test_Results.RecordNumeric("Tx1_DPD_L2_5th_asym_ph_Low", .Tx1_DPD_L2_5th_asym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_LL, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_UL)

                        'RX0
                        BI_Test_Results.RecordNumeric("Rx0_LNA_Atten_High", .Rx0_LNA_Atten.High, BI_Limit.BI_High_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)
                        BI_Test_Results.RecordNumeric("Rx0_LNA_Atten_Low", .Rx0_LNA_Atten.Low, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_Low_Rx_LNA_Atten_UL)

                        BI_Test_Results.RecordNumeric("Rx0_Rx_Atten0_High", .Rx0_Rx_Atten0.High, BI_Limit.BI_High_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)
                        BI_Test_Results.RecordNumeric("Rx0_Rx_Atten0_Low", .Rx0_Rx_Atten0.Low, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_Low_Rx_Rx_Atten0_UL)

                        BI_Test_Results.RecordNumeric("Rx0_Rx_Atten1_High", .Rx0_Rx_Atten1.High, BI_Limit.BI_High_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)
                        BI_Test_Results.RecordNumeric("Rx0_Rx_Atten1_Low", .Rx0_Rx_Atten1.Low, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_Low_Rx_Rx_Atten1_UL)

                        BI_Test_Results.RecordNumeric("Rx0_RSSI_C0_High", .Rx0_RSSI_C0.High, BI_Limit.BI_High_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)
                        BI_Test_Results.RecordNumeric("Rx0_RSSI_C0_Low", .Rx0_RSSI_C0.Low, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_Low_Rx_RSSI_C0_UL)

                        BI_Test_Results.RecordNumeric("Rx0_RSSI_C1_High", .Rx0_RSSI_C1.High, BI_Limit.BI_High_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)
                        BI_Test_Results.RecordNumeric("Rx0_RSSI_C1_Low", .Rx0_RSSI_C1.Low, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_Low_Rx_RSSI_C1_UL)

                        'RX1
                        BI_Test_Results.RecordNumeric("Rx1_LNA_Atten_High", .Rx1_LNA_Atten.High, BI_Limit.BI_High_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)
                        BI_Test_Results.RecordNumeric("Rx1_LNA_Atten_Low", .Rx1_LNA_Atten.Low, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_Low_Rx_LNA_Atten_UL)

                        BI_Test_Results.RecordNumeric("Rx1_Rx_Atten0_High", .Rx1_Rx_Atten0.High, BI_Limit.BI_High_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)
                        BI_Test_Results.RecordNumeric("Rx1_Rx_Atten0_Low", .Rx1_Rx_Atten0.Low, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_Low_Rx_Rx_Atten0_UL)

                        BI_Test_Results.RecordNumeric("Rx1_Rx_Atten1_High", .Rx1_Rx_Atten1.High, BI_Limit.BI_High_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)
                        BI_Test_Results.RecordNumeric("Rx1_Rx_Atten1_Low", .Rx1_Rx_Atten1.Low, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_Low_Rx_Rx_Atten1_UL)

                        BI_Test_Results.RecordNumeric("Rx1_RSSI_C0_High", .Rx1_RSSI_C0.High, BI_Limit.BI_High_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)
                        BI_Test_Results.RecordNumeric("Rx1_RSSI_C0_Low", .Rx1_RSSI_C0.Low, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_Low_Rx_RSSI_C0_UL)

                        BI_Test_Results.RecordNumeric("Rx1_RSSI_C1_High", .Rx1_RSSI_C1.High, BI_Limit.BI_High_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)
                        BI_Test_Results.RecordNumeric("Rx1_RSSI_C1_Low", .Rx1_RSSI_C1.Low, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_Low_Rx_RSSI_C1_UL)

                        'RX2
                        BI_Test_Results.RecordNumeric("RX2_LNA_Atten_High", .Rx2_LNA_Atten.High, BI_Limit.BI_High_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)
                        BI_Test_Results.RecordNumeric("RX2_LNA_Atten_Low", .Rx2_LNA_Atten.Low, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_Low_Rx_LNA_Atten_UL)

                        BI_Test_Results.RecordNumeric("RX2_Rx_Atten0_High", .Rx2_Rx_Atten0.High, BI_Limit.BI_High_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)
                        BI_Test_Results.RecordNumeric("RX2_Rx_Atten0_Low", .Rx2_Rx_Atten0.Low, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_Low_Rx_Rx_Atten0_UL)

                        BI_Test_Results.RecordNumeric("RX2_Rx_Atten1_High", .Rx2_Rx_Atten1.High, BI_Limit.BI_High_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)
                        BI_Test_Results.RecordNumeric("RX2_Rx_Atten1_Low", .Rx2_Rx_Atten1.Low, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_Low_Rx_Rx_Atten1_UL)

                        BI_Test_Results.RecordNumeric("RX2_RSSI_C0_High", .Rx2_RSSI_C0.High, BI_Limit.BI_High_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)
                        BI_Test_Results.RecordNumeric("RX2_RSSI_C0_Low", .Rx2_RSSI_C0.Low, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_Low_Rx_RSSI_C0_UL)

                        BI_Test_Results.RecordNumeric("RX2_RSSI_C1_High", .Rx2_RSSI_C1.High, BI_Limit.BI_High_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)
                        BI_Test_Results.RecordNumeric("RX2_RSSI_C1_Low", .Rx2_RSSI_C1.Low, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_Low_Rx_RSSI_C1_UL)

                        'RX3
                        BI_Test_Results.RecordNumeric("RX3_LNA_Atten_High", .Rx3_LNA_Atten.High, BI_Limit.BI_High_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)
                        BI_Test_Results.RecordNumeric("RX3_LNA_Atten_Low", .Rx3_LNA_Atten.Low, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_Low_Rx_LNA_Atten_UL)

                        BI_Test_Results.RecordNumeric("RX3_Rx_Atten0_High", .Rx3_Rx_Atten0.High, BI_Limit.BI_High_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)
                        BI_Test_Results.RecordNumeric("RX3_Rx_Atten0_Low", .Rx3_Rx_Atten0.Low, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_Low_Rx_Rx_Atten0_UL)

                        BI_Test_Results.RecordNumeric("RX3_Rx_Atten1_High", .Rx3_Rx_Atten1.High, BI_Limit.BI_High_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)
                        BI_Test_Results.RecordNumeric("RX3_Rx_Atten1_Low", .Rx3_Rx_Atten1.Low, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_Low_Rx_Rx_Atten1_UL)

                        BI_Test_Results.RecordNumeric("RX3_RSSI_C0_High", .Rx3_RSSI_C0.High, BI_Limit.BI_High_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)
                        BI_Test_Results.RecordNumeric("RX3_RSSI_C0_Low", .Rx3_RSSI_C0.Low, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_Low_Rx_RSSI_C0_UL)

                        BI_Test_Results.RecordNumeric("RX3_RSSI_C1_High", .Rx3_RSSI_C1.High, BI_Limit.BI_High_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)
                        BI_Test_Results.RecordNumeric("RX3_RSSI_C1_Low", .Rx3_RSSI_C1.Low, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_Low_Rx_RSSI_C1_UL)

                        'PS
                        BI_Test_Results.RecordNumeric("Input_Voltage_High", .Input_Voltage.High, BI_Limit.BI_High_Input_Voltage_LL, BI_Limit.BI_High_Input_Voltage_UL)
                        BI_Test_Results.RecordNumeric("Input_Voltage_Low", .Input_Voltage.Low, BI_Limit.BI_Low_Input_Voltage_LL, BI_Limit.BI_Low_Input_Voltage_UL)

                        BI_Test_Results.RecordNumeric("Input_Current_HP_High", .Input_Current_HP.High, BI_Limit.BI_High_Input_Current_LL, BI_Limit.BI_High_Input_Current_UL)
                        BI_Test_Results.RecordNumeric("Input_Current_HP_Low", .Input_Current_HP.Low, BI_Limit.BI_High_Input_Current_LL, BI_Limit.BI_High_Input_Current_UL)

                        BI_Test_Results.RecordNumeric("Input_Current_LP_High", .Input_Current_LP.High, BI_Limit.BI_Low_Input_Current_LL, BI_Limit.BI_Low_Input_Current_UL)
                        BI_Test_Results.RecordNumeric("Input_Current_LP_Low", .Input_Current_LP.Low, BI_Limit.BI_Low_Input_Current_LL, BI_Limit.BI_Low_Input_Current_UL)

                        BI_Test_Results.RecordNumeric("Input_Power_HP_High", .Input_Power_HP.High, BI_Limit.BI_High_Input_Power_LL, BI_Limit.BI_High_Input_Power_UL)
                        BI_Test_Results.RecordNumeric("Input_Power_HP_Low", .Input_Power_HP.Low, BI_Limit.BI_High_Input_Power_LL, BI_Limit.BI_High_Input_Power_UL)

                        BI_Test_Results.RecordNumeric("Input_Power_LP_High", .Input_Power_LP.High, BI_Limit.BI_Low_Input_Power_LL, BI_Limit.BI_Low_Input_Power_UL)
                        BI_Test_Results.RecordNumeric("Input_Power_LP_Low", .Input_Power_LP.Low, BI_Limit.BI_Low_Input_Power_LL, BI_Limit.BI_Low_Input_Power_UL)

                        BI_Test_Results.RecordNumeric("Output_Voltage_High", .Output_Voltage.High, BI_Limit.BI_High_Output_Voltage_LL, BI_Limit.BI_High_Output_Voltage_UL)
                        BI_Test_Results.RecordNumeric("Output_Voltage_Low", .Output_Voltage.Low, BI_Limit.BI_Low_Output_Voltage_LL, BI_Limit.BI_Low_Output_Voltage_UL)

                        BI_Test_Results.RecordNumeric("AISG_12V_Voltage_High", .AISG_12V_Voltage.High, BI_Limit.BI_High_AISG_12V_Voltage_LL, BI_Limit.BI_High_AISG_12V_Voltage_UL)
                        BI_Test_Results.RecordNumeric("AISG_12V_Voltage_Low", .AISG_12V_Voltage.Low, BI_Limit.BI_Low_AISG_12V_Voltage_LL, BI_Limit.BI_Low_AISG_12V_Voltage_UL)

                        BI_Test_Results.RecordNumeric("AISG_12V_Current_High", .AISG_12V_Current.High, BI_Limit.BI_High_AISG_12V_Current_LL, BI_Limit.BI_High_AISG_12V_Current_UL)
                        BI_Test_Results.RecordNumeric("AISG_12V_Current_Low", .AISG_12V_Current.Low, BI_Limit.BI_Low_AISG_12V_Current_LL, BI_Limit.BI_Low_AISG_12V_Current_UL)

                        BI_Test_Results.RecordNumeric("AISG_24V_Voltage_High", .AISG_24V_Voltage.High, BI_Limit.BI_High_AISG_24V_Voltage_LL, BI_Limit.BI_High_AISG_24V_Voltage_UL)
                        BI_Test_Results.RecordNumeric("AISG_24V_Voltage_Low", .AISG_24V_Voltage.Low, BI_Limit.BI_Low_AISG_24V_Voltage_LL, BI_Limit.BI_Low_AISG_24V_Voltage_UL)

                        BI_Test_Results.RecordNumeric("AISG_24V_Current_High", .AISG_24V_Current.High, BI_Limit.BI_High_AISG_24V_Current_LL, BI_Limit.BI_High_AISG_24V_Current_UL)
                        BI_Test_Results.RecordNumeric("AISG_24V_Current_Low", .AISG_24V_Current.Low, BI_Limit.BI_Low_AISG_24V_Current_LL, BI_Limit.BI_Low_AISG_24V_Current_UL)

                        'Others
                        'BI_Test_Results.RecordString("Software Revision", .SWRevision, "")
                        'BI_Test_Results.RecordString("DSP Revision", .DSPRevision, "")
                        'BI_Test_Results.RecordString("FPGA Revision", .FPGARevision, "")

                        BI_Test_Results.RecordString("Alarm String", .AlarmString, "No Alarm")

                        BI_Test_Results.RecordNumeric("Power_Cycle_Count", .PowerCycleCount, 0, BI_Profile.BI_Power_Cycle)


                        'If Not BI_Test_Results.RecordNumeric("Thres_Hold_Time", .Thres_Hold_Time, BI_Limit.Thres_Hold_Time, 99999) Then
                        '    Check_Limit_Value(slot, "Thres_Hold_Time", .Thres_Hold_Time, BI_Limit.Thres_Hold_Time, 99999)
                        'End If
                        'V1.1.4
                        If Not BI_Test_Results.RecordNumeric("Thres_Hold_Time", .Thres_Hold_Time, BI_Limit.Thres_Hold_Time, BI_Profile.BI_Duration) Then
                            Check_Limit_Value(slot, "Thres_Hold_Time", .Thres_Hold_Time, BI_Limit.Thres_Hold_Time, BI_Profile.BI_Duration)
                        End If

                        'If Not BI_Test_Results.RecordNumeric("RF_On_Time", .RF_On_Time, BI_Limit.RF_On_Time, 99999) Then
                        '    Check_Limit_Value(slot, "RF_On_Time", .RF_On_Time, BI_Limit.RF_On_Time, 99999)
                        'End If
                        'V1.1.4
                        If Not BI_Test_Results.RecordNumeric("RF_On_Time", .RF_On_Time, BI_Limit.RF_On_Time, BI_Profile.BI_Duration) Then
                            Check_Limit_Value(slot, "RF_On_Time", .RF_On_Time, BI_Limit.RF_On_Time, BI_Profile.BI_Duration)
                        End If

                    End With

                    'First FailTime in DCF   
                    If Not FirstFailTime(slot) = "0:00:00" Then
                        BI_Test_Results.RecordString("First_Fail_Time", Format(FirstFailTime(slot).Year, "0000") & Format(FirstFailTime(slot).Month, "00") & _
                                                                        Format(FirstFailTime(slot).Day, "00") & Format(FirstFailTime(slot).Hour, "00") & _
                                                                        Format(FirstFailTime(slot).Minute, "00") & Format(FirstFailTime(slot).Second, "00"), "0")
                    End If

                    If AbortTest Then
                        BI_Test_Results.RecordPassFail("User Abort Testing", False)
                        Check_Limit_String(slot, "User_Abort_Test", "User_Abort", "Aborted")
                    End If

                    BI_Test_Results.StopTest()

                    If BI_Test_Results.GetTestStatus() = "PASS" Then
                        DisplayTestResult(slot, False)
                        BI_Data(slot).RecData.AlarmFlag = False
                    Else
                        DisplayTestResult(slot, True)
                        BI_Data(slot).RecData.AlarmFlag = True
                    End If

                End If
            Next
        Catch ex As Exception
            AddMessage("Write DCF file Error: " & ex.Message)
        End Try

    End Sub

    Public Sub WriteTxtFile()
        'Record frmMain.TextBoxMessage
        Dim TxtFilePath As String
        TxtFilePath = BI_Param.File_Save_Path & Test_Site & "_" & My.Computer.Name.ToString & "_" & Assembly_Type & "_" & VB6.Format(Now, "yyyyMMddHHmmss") & ".txt"

        Try
            BI_Test_Results.Test_SW_Rev = SW_Version
            BI_Test_Results.Firmware_Rev = MTR_Version
            Using sw As StreamWriter = New StreamWriter(TxtFilePath)

                sw.Write(Test_Site & "_" & My.Computer.Name.ToString & " ")
                sw.WriteLine(Assembly_Type & "_" & SW_Version & "_" & MTR_Version)
                sw.WriteLine(frmMain.TextBoxMessage.Text)
                sw.Close()

            End Using

        Catch ex As Exception
            AddMessage("Write Txt File Error: " & ex.Message)
        End Try

    End Sub

    'V1.1.6
    Public Function WriteRamLogFile() As Boolean
        Dim tRamLogFilePath As String
        Dim slot As Integer
        Try
            For slot = 0 To SlotNum
                If BI_Data(slot).UnitInfo.UnitActive Then BI_Data(slot).MeasData.RamLog = Transceiver(slot).GetRamlog
            Next
            For slot = 0 To SlotNum
                If BI_Data(slot).MeasData.RamLog <> "" Then
                    tRamLogFilePath = BI_Param.File_Save_Path & "Viper_1900\" & BI_Data(slot).UnitInfo.FileName & ".txt"
                    Using sw As StreamWriter = New StreamWriter(tRamLogFilePath)
                        sw.WriteLine("Viper" & SP & BI_Data(slot).UnitInfo.UnitSN & NL2)
                        sw.WriteLine("RamLog Info:" & NL & BI_Data(slot).MeasData.RamLog)
                        sw.Close()
                    End Using
                End If
            Next
            Return True
        Catch ex As Exception
            AddMessage("Write RamLog File Error: " & ex.Message)
        End Try
    End Function

    Public Function WriteRamLogFileSingle(ByVal Slot As Integer) As Boolean
        Dim tRamLogFilePath As String
        'Dim slot As Integer
        Try
            AddMessage("Slot:" & (Slot + 1) & " Error, Write Single RamLog File.")
            'For slot = 0 To SlotNum
            If BI_Data(Slot).UnitInfo.UnitActive Then BI_Data(Slot).MeasData.RamLogSingle = Transceiver(Slot).GetRamlog
            'Next
            'For Slot = 0 To SlotNum
            If BI_Data(Slot).MeasData.RamLogSingle <> "" Then
                'V2.0.3  Split single Ramlog with normal one.
                Dim tmpTime As String = VB6.Format(Now, "yyyyMMddHHmmss")
                tRamLogFilePath = BI_Param.File_Save_Path & "Viper_1900\" & BI_Data(Slot).UnitInfo.UnitSN & "_" & tmpTime & "_Single.txt"
                'tRamLogFilePath = BI_Param.File_Save_Path & "Viper_1900\" & BI_Data(Slot).UnitInfo.FileName & "_Single.txt"
                Using sw As StreamWriter = New StreamWriter(tRamLogFilePath)
                    sw.WriteLine("Viper" & SP & BI_Data(Slot).UnitInfo.UnitSN & NL2)
                    sw.WriteLine("RamLog Info:" & NL & BI_Data(Slot).MeasData.RamLogSingle)
                    sw.Close()
                End Using
            End If
            'Next
            AddMessage("Write Single RamLog File OK.")
            Return True
        Catch ex As Exception
            AddMessage("Write RamLog File Error: " & ex.Message)
        End Try
    End Function


    'Public Sub Matlab_Calculate(ByVal slot As Integer)
    '    Try
    '        Dim ShellParameter As String
    '        Dim ShellProcess As New System.Diagnostics.Process
    '        Dim ML_Pic_OnOff As String = "NA"

    '        If BI_Param.Matlab_Picture_Enabled Then ML_Pic_OnOff = "on"

    '        ShellParameter = String.Format("{0} {1} {2} {3} {4}", _
    '          BI_Data(slot).UnitInfo.FileName & ".csv", BI_Data(slot).UnitInfo.CSVFilePath, ML_Pic_OnOff, BI_Param.Matlab_Picture_Comments, BI_Data(slot).UnitInfo.JPGFilePath)
    '        ShellProcess.StartInfo.Arguments = ShellParameter
    '        ShellProcess.StartInfo.FileName = Application.StartupPath() & "\Hydra_BI_Plot.exe"
    '        ShellProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
    '        ShellProcess.Start()
    '        ShellProcess.WaitForExit()

    '        Delay(1000)

    '        Dim sr As StreamReader, tmp(12) As Double
    '        sr = File.OpenText(BI_Data(slot).UnitInfo.TXTFilePath)
    '        For LineNum As Integer = 0 To 12
    '            Dim getstring As String = sr.ReadLine()
    '            If LineNum > 0 Then tmp(LineNum) = CDbl(Strings.Split(getstring, "=")(1))
    '        Next
    '        sr.Close()
    '        Delay(1000)

    '        With BI_Data(slot).RecData
    '            .BI_PWR_DETECTOR_SLOPE = tmp(1)
    '            .BI_PWR_DETECTOR_TEMP_SLOPE = tmp(2)
    '            .BI_CAR_SQUAR = tmp(3)
    '            .BI_CAR_LINEAR = tmp(4)
    '            .BI_PEAK_SQUAR = tmp(5)
    '            .BI_PEAK_LINEAR = tmp(6)
    '            .BI_PWR_DETECTOR_MAX_ERR_LOW = tmp(7)
    '            .BI_PWR_DETECTOR_MAX_ERR_HIGH = tmp(8)
    '            .BI_CAR_MAX_ERR_LOW = tmp(9)
    '            .BI_CAR_MAX_ERR_HIGH = tmp(10)
    '            .BI_PEAK_MAX_ERR_LOW = tmp(11)
    '            .BI_PEAK_MAX_ERR_HIGH = tmp(12)
    '        End With
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub


    Public Sub Check_Limit_Value(ByVal slot As Integer, ByVal Test_Step As String, ByVal MeasValue As Double, ByVal LL As Double, ByVal HL As Double)
        Try
            With BI_Data(slot)
                If MeasValue < LL Or MeasValue > HL Then
                    .RecData.AlarmFlag = True
                    '.RecData.FailureReason = .RecData.FailureReason & Test_Step & "," & LL & "," & MeasValue & "," & HL & NL
                    dgvFailureInd(slot).Rows.Add(New String(3) {Test_Step, LL, MeasValue, HL})
                    If FirstFailTime(slot) = "0:00:00" Then  'First FailTime in DCF 
                        FirstFailTime(slot) = Now
                    End If
                End If
            End With
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Public Sub Check_Limit_String(ByVal slot As Integer, ByVal Test_Step As String, ByVal MeasValue As String, ByVal TargetString As String)
        Try
            With BI_Data(slot)
                If Not UCase(MeasValue) = UCase(TargetString) Then
                    .RecData.AlarmFlag = True
                    dgvFailureInd(slot).Rows.Add(New String(3) {Test_Step, TargetString, MeasValue, TargetString})
                    If FirstFailTime(slot) = "0:00:00" Then   'First FailTime in DCF  
                        FirstFailTime(slot) = Now
                    End If
                End If
            End With
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Public Sub DetectFailures(ByVal slot As Integer)
        Try
            With BI_Data(slot).RecData
                If Not .AlarmFlag Then
                    'Check_Limit_Value(slot, "PA0_Temp_High", .RecData.PA0_Temp.High, BI_Limit.BI_Low_Temp, BI_Limit.BI_High_Temp)
                    'Check_Limit_Value(slot, "PA0_Temp_Low", .RecData.PA0_Temp.Low, BI_Limit.BI_Low_Temp, BI_Limit.BI_High_Temp)
                    'General Information
                    Check_Limit_Value(slot, "PA0_Temp_High", .PA0_Temp.High, BI_Limit.BI_Low_PA_Temp_LL, BI_Limit.BI_High_PA_Temp_UL)
                    Check_Limit_Value(slot, "PA0_Temp_Low", .PA0_Temp.Low, BI_Limit.BI_Low_PA_Temp_LL, BI_Limit.BI_High_PA_Temp_UL)

                    Check_Limit_Value(slot, "PA0_VSWR_Temp_High", .PA0_VSWR_Temp.High, BI_Limit.BI_Low_PA_VSWR_Temp_LL, BI_Limit.BI_High_PA_VSWR_Temp_UL)
                    Check_Limit_Value(slot, "PA0_VSWR_Temp_Low", .PA0_VSWR_Temp.Low, BI_Limit.BI_Low_PA_VSWR_Temp_LL, BI_Limit.BI_High_PA_VSWR_Temp_UL)

                    Check_Limit_Value(slot, "PA1_Temp_High", .PA1_Temp.High, BI_Limit.BI_Low_PA_Temp_LL, BI_Limit.BI_High_PA_Temp_UL)
                    Check_Limit_Value(slot, "PA1_Temp_Low", .PA1_Temp.Low, BI_Limit.BI_Low_PA_Temp_LL, BI_Limit.BI_High_PA_Temp_UL)

                    Check_Limit_Value(slot, "PA1_VSWR_Temp_High", .PA1_VSWR_Temp.High, BI_Limit.BI_Low_PA_VSWR_Temp_LL, BI_Limit.BI_High_PA_VSWR_Temp_UL)
                    Check_Limit_Value(slot, "PA1_VSWR_Temp_Low", .PA1_VSWR_Temp.Low, BI_Limit.BI_Low_PA_VSWR_Temp_LL, BI_Limit.BI_High_PA_VSWR_Temp_UL)

                    Check_Limit_Value(slot, "PA_Temp_Delta_High", .PA_Temp_Delta.High, BI_Limit.BI_PA_Temp_Delta_LL, BI_Limit.BI_PA_Temp_Delta_UL)
                    Check_Limit_Value(slot, "PA_Temp_Delta_Low", .PA_Temp_Delta.Low, BI_Limit.BI_PA_Temp_Delta_LL, BI_Limit.BI_PA_Temp_Delta_UL)

                    Check_Limit_Value(slot, "LNA0_Temp_High", .LNA0_Temp.High, BI_Limit.BI_Low_LNA01_Temp_LL, BI_Limit.BI_High_LNA01_Temp_UL)
                    Check_Limit_Value(slot, "LNA0_Temp_low", .LNA0_Temp.Low, BI_Limit.BI_Low_LNA01_Temp_LL, BI_Limit.BI_High_LNA01_Temp_UL)

                    Check_Limit_Value(slot, "LNA1_Temp_High", .LNA1_Temp.High, BI_Limit.BI_Low_LNA01_Temp_LL, BI_Limit.BI_High_LNA01_Temp_UL)
                    Check_Limit_Value(slot, "LNA1_Temp_low", .LNA1_Temp.Low, BI_Limit.BI_Low_LNA01_Temp_LL, BI_Limit.BI_High_LNA01_Temp_UL)

                    Check_Limit_Value(slot, "LNA2_Temp_High", .LNA2_Temp.High, BI_Limit.BI_Low_LNA23_Temp_LL, BI_Limit.BI_High_LNA23_Temp_UL)
                    Check_Limit_Value(slot, "LNA2_Temp_low", .LNA2_Temp.Low, BI_Limit.BI_Low_LNA23_Temp_LL, BI_Limit.BI_High_LNA23_Temp_UL)

                    Check_Limit_Value(slot, "LNA3_Temp_High", .LNA3_Temp.High, BI_Limit.BI_Low_LNA23_Temp_LL, BI_Limit.BI_High_LNA23_Temp_UL)
                    Check_Limit_Value(slot, "LNA3_Temp_low", .LNA3_Temp.Low, BI_Limit.BI_Low_LNA23_Temp_LL, BI_Limit.BI_High_LNA23_Temp_UL)

                    Check_Limit_Value(slot, "PS_Converter_Temp_High", .PS_Converter_Temp.High, BI_Limit.BI_Low_PS_Converter_Temp_LL, BI_Limit.BI_High_PS_Converter_Temp_UL)
                    Check_Limit_Value(slot, "PS_Converter_Temp_Low", .PS_Converter_Temp.Low, BI_Limit.BI_Low_PS_Converter_Temp_LL, BI_Limit.BI_High_PS_Converter_Temp_UL)

                    Check_Limit_Value(slot, "PS_Brick_Temp_High", .PS_Brick_Temp.High, BI_Limit.BI_Low_PS_Brick_Temp_LL, BI_Limit.BI_High_PS_Brick_Temp_UL)
                    Check_Limit_Value(slot, "PS_Brick_Temp_Low", .PS_Brick_Temp.Low, BI_Limit.BI_Low_PS_Brick_Temp_LL, BI_Limit.BI_High_PS_Brick_Temp_UL)

                    Check_Limit_Value(slot, "PSU_Temp_Delta_High", .PSU_Temp_Delta.High, BI_Limit.BI_PSU_Temp_Delta_LL, BI_Limit.BI_PSU_Temp_Delta_UL)
                    Check_Limit_Value(slot, "PSU_Temp_Delta_Low", .PSU_Temp_Delta.Low, BI_Limit.BI_PSU_Temp_Delta_LL, BI_Limit.BI_PSU_Temp_Delta_UL)

                    Check_Limit_Value(slot, "PSU_PA_Temp_Delta_High", .PSU_PA_Temp_Delta.High, BI_Limit.BI_High_PSU_PA_Temp_Delta_LL, BI_Limit.BI_High_PSU_PA_Temp_Delta_UL)
                    Check_Limit_Value(slot, "PSU_PA_Temp_Delta_Low", .PSU_PA_Temp_Delta.Low, BI_Limit.BI_Low_PSU_PA_Temp_Delta_LL, BI_Limit.BI_High_PSU_PA_Temp_Delta_UL)

                    Check_Limit_Value(slot, "FB_Temp_High", .FB_Temp.High, BI_Limit.BI_Low_FB_Temp_LL, BI_Limit.BI_High_FB_Temp_UL)
                    Check_Limit_Value(slot, "FB_Temp_Low", .FB_Temp.Low, BI_Limit.BI_Low_FB_Temp_LL, BI_Limit.BI_High_FB_Temp_UL)

                    Check_Limit_Value(slot, "RX_Temp_High", .RX_Temp.High, BI_Limit.BI_Low_RX_Temp_LL, BI_Limit.BI_High_RX_Temp_UL)
                    Check_Limit_Value(slot, "RX_Temp_Low", .RX_Temp.Low, BI_Limit.BI_Low_RX_Temp_LL, BI_Limit.BI_High_RX_Temp_UL)

                    'TX0
                    Check_Limit_Value(slot, "TX0_Output_Pow_High", .TX0_Output_Pow.High, BI_Limit.BI_Low_TX_Output_Pow_LL, BI_Limit.BI_High_TX_Output_Pow_UL)
                    Check_Limit_Value(slot, "TX0_Output_Pow_Low", .TX0_Output_Pow.Low, BI_Limit.BI_Low_TX_Output_Pow_LL, BI_Limit.BI_High_TX_Output_Pow_UL)

                    Check_Limit_Value(slot, "TX0_VSWR_High", .TX0_VSWR.High, BI_Limit.BI_Low_TX_VSWR_LL, BI_Limit.BI_High_TX_VSWR_UL)
                    Check_Limit_Value(slot, "TX0_VSWR_Low", .TX0_VSWR.Low, BI_Limit.BI_Low_TX_VSWR_LL, BI_Limit.BI_High_TX_VSWR_UL)

                    Check_Limit_Value(slot, "TX0_Forward_Power_Detector_High", .TX0_Forward_Power_Detector.High, BI_Limit.BI_Low_TX_Forward_Power_Detector_LL, BI_Limit.BI_High_TX_Forward_Power_Detector_UL)
                    Check_Limit_Value(slot, "TX0_Forward_Power_Detector_Low", .TX0_Forward_Power_Detector.Low, BI_Limit.BI_Low_TX_Forward_Power_Detector_LL, BI_Limit.BI_High_TX_Forward_Power_Detector_UL)

                    Check_Limit_Value(slot, "TX0_Reverse_Power_Detector_High", .TX0_Reverse_Power_Detector.High, BI_Limit.BI_Low_TX_Reverse_Power_Detector_LL, BI_Limit.BI_High_TX_Reverse_Power_Detector_UL)
                    Check_Limit_Value(slot, "TX0_Reverse_Power_Detector_Low", .TX0_Reverse_Power_Detector.Low, BI_Limit.BI_Low_TX_Reverse_Power_Detector_LL, BI_Limit.BI_High_TX_Reverse_Power_Detector_UL)

                    Check_Limit_Value(slot, "TX0_Gain_VCA_High", .TX0_Gain_VCA.High, BI_Limit.BI_Low_TX_Gain_VCA_LL, BI_Limit.BI_High_TX_Gain_VCA_UL)
                    Check_Limit_Value(slot, "TX0_Gain_VCA_Low", .TX0_Gain_VCA.Low, BI_Limit.BI_Low_TX_Gain_VCA_LL, BI_Limit.BI_High_TX_Gain_VCA_UL)

                    Check_Limit_Value(slot, "TX0_txDacGain_High", .TX0_txDacGain.High, BI_Limit.BI_Low_TX_txDacGain_LL, BI_Limit.BI_High_TX_txDacGain_UL)
                    Check_Limit_Value(slot, "TX0_txDacGain_Low", .TX0_txDacGain.Low, BI_Limit.BI_Low_TX_txDacGain_LL, BI_Limit.BI_High_TX_txDacGain_UL)

                    Check_Limit_Value(slot, "TX0_totalTxAttn_High", .TX0_totalTxAttn.High, BI_Limit.BI_Low_TX_totalTxAttn_LL, BI_Limit.BI_High_TX_totalTxAttn_UL)
                    Check_Limit_Value(slot, "TX0_totalTxAttn_Low", .TX0_totalTxAttn.Low, BI_Limit.BI_Low_TX_totalTxAttn_LL, BI_Limit.BI_High_TX_totalTxAttn_UL)

                    Check_Limit_Value(slot, "TX0_Gain_TxStep_High", .TX0_Gain_TxStep.High, BI_Limit.BI_Low_TX_Gain_TxStep_LL, BI_Limit.BI_High_TX_Gain_TxStep_UL)
                    Check_Limit_Value(slot, "TX0_Gain_TxStep_Low", .TX0_Gain_TxStep.Low, BI_Limit.BI_Low_TX_Gain_TxStep_LL, BI_Limit.BI_High_TX_Gain_TxStep_UL)

                    Check_Limit_Value(slot, "TX0_Gain_FbStep_High", .TX0_Gain_FbStep.High, BI_Limit.BI_Low_TX_Gain_FbStep_LL, BI_Limit.BI_High_TX_Gain_FbStep_UL)
                    Check_Limit_Value(slot, "TX0_Gain_FbStep_Low", .TX0_Gain_FbStep.Low, BI_Limit.BI_Low_TX_Gain_FbStep_LL, BI_Limit.BI_High_TX_Gain_FbStep_UL)

                    Check_Limit_Value(slot, "TX0_Gain_FbTxQuo_High", .TX0_Gain_FbTxQuo.High, BI_Limit.BI_Low_TX_Gain_FbTxQuo_LL, BI_Limit.BI_High_TX_Gain_FbTxQuo_UL)
                    Check_Limit_Value(slot, "TX0_Gain_FbTxQuo_low", .TX0_Gain_FbTxQuo.Low, BI_Limit.BI_Low_TX_Gain_FbTxQuo_LL, BI_Limit.BI_High_TX_Gain_FbTxQuo_UL)

                    Check_Limit_Value(slot, "TX0_Gain_GainError_High", .TX0_Gain_GainError.High, BI_Limit.BI_Low_TX_Gain_GainError_LL, BI_Limit.BI_High_TX_Gain_GainError_UL)
                    Check_Limit_Value(slot, "TX0_Gain_GainError_Low", .TX0_Gain_GainError.Low, BI_Limit.BI_Low_TX_Gain_GainError_LL, BI_Limit.BI_High_TX_Gain_GainError_UL)

                    Check_Limit_Value(slot, "TX0_PA0_PsVolt_High", .TX0_PA0_PsVolt.High, BI_Limit.BI_Low_TX_PA_PsVolt_LL, BI_Limit.BI_High_TX_PA_PsVolt_UL)
                    Check_Limit_Value(slot, "TX0_PA0_PsVolt_Low", .TX0_PA0_PsVolt.Low, BI_Limit.BI_Low_TX_PA_PsVolt_LL, BI_Limit.BI_High_TX_PA_PsVolt_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Temp_High", .TX0_PA0_Temp.High, BI_Limit.BI_Low_TX_PA_Temp_LL, BI_Limit.BI_High_TX_PA_Temp_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Temp_Low", .TX0_PA0_Temp.Low, BI_Limit.BI_Low_TX_PA_Temp_LL, BI_Limit.BI_High_TX_PA_Temp_UL)

                    Check_Limit_Value(slot, "TX0_PA0_BiasTemp_High", .TX0_PA0_BiasTemp.High, BI_Limit.BI_Low_TX_PA_BiasTemp_LL, BI_Limit.BI_High_TX_PA_BiasTemp_UL)
                    Check_Limit_Value(slot, "TX0_PA0_BiasTemp_Low", .TX0_PA0_BiasTemp.Low, BI_Limit.BI_Low_TX_PA_BiasTemp_LL, BI_Limit.BI_High_TX_PA_BiasTemp_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Driver1Cur_High", .TX0_PA0_Driver1Cur.High, BI_Limit.BI_Low_TX_PA_Driver1Cur_LL, BI_Limit.BI_High_TX_PA_Driver1Cur_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Driver1Cur_Low", .TX0_PA0_Driver1Cur.Low, BI_Limit.BI_Low_TX_PA_Driver1Cur_LL, BI_Limit.BI_High_TX_PA_Driver1Cur_UL)


                    Check_Limit_Value(slot, "TX0_PA0_Driver2Cur_High", .TX0_PA0_Driver2Cur.High, BI_Limit.BI_Low_TX_PA_Driver2Cur_LL, BI_Limit.BI_High_TX_PA_Driver2Cur_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Driver2Cur_Low", .TX0_PA0_Driver2Cur.Low, BI_Limit.BI_Low_TX_PA_Driver2Cur_LL, BI_Limit.BI_High_TX_PA_Driver2Cur_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Driver3Cur_High", .TX0_PA0_Driver3Cur.High, BI_Limit.BI_Low_TX_PA_Driver3Cur_LL, BI_Limit.BI_High_TX_PA_Driver3Cur_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Driver3Cur_Low", .TX0_PA0_Driver3Cur.Low, BI_Limit.BI_Low_TX_PA_Driver3Cur_LL, BI_Limit.BI_High_TX_PA_Driver3Cur_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Driver4Cur_High", .TX0_PA0_Driver4Cur.High, BI_Limit.BI_Low_TX_PA_Driver4Cur_LL, BI_Limit.BI_High_TX_PA_Driver4Cur_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Driver4Cur_Low", .TX0_PA0_Driver4Cur.Low, BI_Limit.BI_Low_TX_PA_Driver4Cur_LL, BI_Limit.BI_High_TX_PA_Driver4Cur_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Final1Cur_High", .TX0_PA0_Final1Cur.High, BI_Limit.BI_Low_TX_PA_Final1Cur_LL, BI_Limit.BI_High_TX_PA_Final1Cur_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Final1Cur_Low", .TX0_PA0_Final1Cur.Low, BI_Limit.BI_Low_TX_PA_Final1Cur_LL, BI_Limit.BI_High_TX_PA_Final1Cur_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Final2Cur_High", .TX0_PA0_Final2Cur.High, BI_Limit.BI_Low_TX_PA_Final2Cur_LL, BI_Limit.BI_High_TX_PA_Final2Cur_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Final2Cur_Low", .TX0_PA0_Final2Cur.Low, BI_Limit.BI_Low_TX_PA_Final2Cur_LL, BI_Limit.BI_High_TX_PA_Final2Cur_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Driver1Dac_High", .TX0_PA0_Driver1Dac.High, BI_Limit.BI_Low_TX_PA_Driver1Dac_LL, BI_Limit.BI_High_TX_PA_Driver1Dac_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Driver1Dac_Low", .TX0_PA0_Driver1Dac.Low, BI_Limit.BI_Low_TX_PA_Driver1Dac_LL, BI_Limit.BI_High_TX_PA_Driver1Dac_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Driver2Dac_High", .TX0_PA0_Driver2Dac.High, BI_Limit.BI_Low_TX_PA_Driver2Dac_LL, BI_Limit.BI_High_TX_PA_Driver2Dac_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Driver2Dac_Low", .TX0_PA0_Driver2Dac.Low, BI_Limit.BI_Low_TX_PA_Driver2Dac_LL, BI_Limit.BI_High_TX_PA_Driver2Dac_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Driver3Dac_High", .TX0_PA0_Driver3Dac.High, BI_Limit.BI_Low_TX_PA_Driver3Dac_LL, BI_Limit.BI_High_TX_PA_Driver3Dac_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Driver3Dac_Low", .TX0_PA0_Driver3Dac.Low, BI_Limit.BI_Low_TX_PA_Driver3Dac_LL, BI_Limit.BI_High_TX_PA_Driver3Dac_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Driver4Dac_High", .TX0_PA0_Driver4Dac.High, BI_Limit.BI_Low_TX_PA_Driver4Dac_LL, BI_Limit.BI_High_TX_PA_Driver4Dac_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Driver4Dac_Low", .TX0_PA0_Driver4Dac.Low, BI_Limit.BI_Low_TX_PA_Driver4Dac_LL, BI_Limit.BI_High_TX_PA_Driver4Dac_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Final1Dac_High", .TX0_PA0_Final1Dac.High, BI_Limit.BI_Low_TX_PA_Final1Dac_LL, BI_Limit.BI_High_TX_PA_Final1Dac_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Final1Dac_Low", .TX0_PA0_Final1Dac.Low, BI_Limit.BI_Low_TX_PA_Final1Dac_LL, BI_Limit.BI_High_TX_PA_Final1Dac_UL)

                    Check_Limit_Value(slot, "TX0_PA0_Final2Dac_High", .TX0_PA0_Final2Dac.High, BI_Limit.BI_Low_TX_PA_Final2Dac_LL, BI_Limit.BI_High_TX_PA_Final2Dac_UL)
                    Check_Limit_Value(slot, "TX0_PA0_Final2Dac_Low", .TX0_PA0_Final2Dac.Low, BI_Limit.BI_Low_TX_PA_Final2Dac_LL, BI_Limit.BI_High_TX_PA_Final2Dac_UL)

                    Check_Limit_Value(slot, "TX0_PA0_ampDelayInt_High", .TX0_PA0_ampDelayInt.High, BI_Limit.BI_Low_TX_PA_ampDelayInt_LL, BI_Limit.BI_High_TX_PA_ampDelayInt_UL)
                    Check_Limit_Value(slot, "TX0_PA0_ampDelayInt_Low", .TX0_PA0_ampDelayInt.Low, BI_Limit.BI_Low_TX_PA_ampDelayInt_LL, BI_Limit.BI_High_TX_PA_ampDelayInt_UL)

                    Check_Limit_Value(slot, "TX0_PA0_ampDelayFrac_High", .TX0_PA0_ampDelayFrac.High, BI_Limit.BI_Low_TX_PA_ampDelayFrac_LL, BI_Limit.BI_High_TX_PA_ampDelayFrac_UL)
                    Check_Limit_Value(slot, "TX0_PA0_ampDelayFrac_Low", .TX0_PA0_ampDelayFrac.Low, BI_Limit.BI_Low_TX_PA_ampDelayFrac_LL, BI_Limit.BI_High_TX_PA_ampDelayFrac_UL)

                    Check_Limit_Value(slot, "TX0_PA0_MaxCrossCorrelation_High", .TX0_PA0_MaxCrossCorrelation.High, BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_LL, BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_UL)
                    Check_Limit_Value(slot, "TX0_PA0_MaxCrossCorrelation_Low", .TX0_PA0_MaxCrossCorrelation.Low, BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_LL, BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_UL)

                    Check_Limit_Value(slot, "TX0_Tx0_DPD_L1_Table_Max_Gain_High", .TX0_DPD_L1_Table_Max_Gain.High, BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_UL)
                    Check_Limit_Value(slot, "TX0_Tx0_DPD_L1_Table_Max_Gain_Low", .TX0_DPD_L1_Table_Max_Gain.Low, BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_UL)

                    Check_Limit_Value(slot, "TX0_Tx0_DPD_L1_Table_Min_Gain_High", .TX0_DPD_L1_Table_Min_Gain.High, BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_UL)
                    Check_Limit_Value(slot, "TX0_Tx0_DPD_L1_Table_Min_Gain_Low", .TX0_DPD_L1_Table_Min_Gain.Low, BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L2_3rd_sym_am_High", .Tx0_DPD_L2_3rd_sym_am.High, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L2_3rd_sym_am_Low", .Tx0_DPD_L2_3rd_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L2_3rd_sym_ph_High", .Tx0_DPD_L2_3rd_sym_ph.High, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L2_3rd_sym_ph_Low", .Tx0_DPD_L2_3rd_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L3_3rd_sym_am_High", .Tx0_DPD_L3_3rd_sym_am.High, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L3_3rd_sym_am_Low", .Tx0_DPD_L3_3rd_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L3_3rd_sym_ph_High", .Tx0_DPD_L3_3rd_sym_ph.High, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L3_3rd_sym_ph_Low", .Tx0_DPD_L3_3rd_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L2_5th_sym_am_High", .Tx0_DPD_L2_5th_sym_am.High, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L2_5th_sym_am_Low", .Tx0_DPD_L2_5th_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L2_5th_sym_ph_High", .Tx0_DPD_L2_5th_sym_ph.High, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L2_5th_sym_ph_Low", .Tx0_DPD_L2_5th_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L3_5th_sym_am_High", .Tx0_DPD_L3_5th_sym_am.High, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L3_5th_sym_am_Low", .Tx0_DPD_L3_5th_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L3_5th_sym_ph_High", .Tx0_DPD_L3_5th_sym_ph.High, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L3_5th_sym_ph_Low", .Tx0_DPD_L3_5th_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L2_3rd_asym_am_High", .Tx0_DPD_L2_3rd_asym_am.High, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L2_3rd_asym_am_Low", .Tx0_DPD_L2_3rd_asym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L2_3rd_asym_ph_High", .Tx0_DPD_L2_3rd_asym_ph.High, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L2_3rd_asym_ph_Low", .Tx0_DPD_L2_3rd_asym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L2_5th_asym_am_High", .Tx0_DPD_L2_5th_asym_am.High, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L2_5th_asym_am_Low", .Tx0_DPD_L2_5th_asym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_UL)

                    Check_Limit_Value(slot, "Tx0_DPD_L2_5th_asym_ph_High", .Tx0_DPD_L2_5th_asym_ph.High, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_UL)
                    Check_Limit_Value(slot, "Tx0_DPD_L2_5th_asym_ph_Low", .Tx0_DPD_L2_5th_asym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_UL)

                    'TX1
                    Check_Limit_Value(slot, "TX1_Output_Pow_High", .TX1_Output_Pow.High, BI_Limit.BI_Low_TX_Output_Pow_LL, BI_Limit.BI_High_TX_Output_Pow_UL)
                    Check_Limit_Value(slot, "TX1_Output_Pow_Low", .TX1_Output_Pow.Low, BI_Limit.BI_Low_TX_Output_Pow_LL, BI_Limit.BI_High_TX_Output_Pow_UL)

                    Check_Limit_Value(slot, "TX1_VSWR_High", .TX1_VSWR.High, BI_Limit.BI_Low_TX_VSWR_LL, BI_Limit.BI_High_TX_VSWR_UL)
                    Check_Limit_Value(slot, "TX1_VSWR_Low", .TX1_VSWR.Low, BI_Limit.BI_Low_TX_VSWR_LL, BI_Limit.BI_High_TX_VSWR_UL)

                    Check_Limit_Value(slot, "TX1_Forward_Power_Detector_High", .TX1_Forward_Power_Detector.High, BI_Limit.BI_Low_TX_Forward_Power_Detector_LL, BI_Limit.BI_High_TX_Forward_Power_Detector_UL)
                    Check_Limit_Value(slot, "TX1_Forward_Power_Detector_Low", .TX1_Forward_Power_Detector.Low, BI_Limit.BI_Low_TX_Forward_Power_Detector_LL, BI_Limit.BI_High_TX_Forward_Power_Detector_UL)

                    Check_Limit_Value(slot, "TX1_Reverse_Power_Detector_High", .TX1_Reverse_Power_Detector.High, BI_Limit.BI_Low_TX_Reverse_Power_Detector_LL, BI_Limit.BI_High_TX_Reverse_Power_Detector_UL)
                    Check_Limit_Value(slot, "TX1_Reverse_Power_Detector_Low", .TX1_Reverse_Power_Detector.Low, BI_Limit.BI_Low_TX_Reverse_Power_Detector_LL, BI_Limit.BI_High_TX_Reverse_Power_Detector_UL)

                    Check_Limit_Value(slot, "TX1_Gain_VCA_High", .TX1_Gain_VCA.High, BI_Limit.BI_Low_TX_Gain_VCA_LL, BI_Limit.BI_High_TX_Gain_VCA_UL)
                    Check_Limit_Value(slot, "TX1_Gain_VCA_Low", .TX1_Gain_VCA.Low, BI_Limit.BI_Low_TX_Gain_VCA_LL, BI_Limit.BI_High_TX_Gain_VCA_UL)

                    Check_Limit_Value(slot, "TX1_txDacGain_High", .TX1_txDacGain.High, BI_Limit.BI_Low_TX_txDacGain_LL, BI_Limit.BI_High_TX_txDacGain_UL)
                    Check_Limit_Value(slot, "TX1_txDacGain_Low", .TX1_txDacGain.Low, BI_Limit.BI_Low_TX_txDacGain_LL, BI_Limit.BI_High_TX_txDacGain_UL)

                    Check_Limit_Value(slot, "TX1_totalTxAttn_High", .TX1_totalTxAttn.High, BI_Limit.BI_Low_TX_totalTxAttn_LL, BI_Limit.BI_High_TX_totalTxAttn_UL)
                    Check_Limit_Value(slot, "TX1_totalTxAttn_Low", .TX1_totalTxAttn.Low, BI_Limit.BI_Low_TX_totalTxAttn_LL, BI_Limit.BI_High_TX_totalTxAttn_UL)

                    Check_Limit_Value(slot, "TX1_Gain_TxStep_High", .TX1_Gain_TxStep.High, BI_Limit.BI_Low_TX_Gain_TxStep_LL, BI_Limit.BI_High_TX_Gain_TxStep_UL)
                    Check_Limit_Value(slot, "TX1_Gain_TxStep_Low", .TX1_Gain_TxStep.Low, BI_Limit.BI_Low_TX_Gain_TxStep_LL, BI_Limit.BI_High_TX_Gain_TxStep_UL)

                    Check_Limit_Value(slot, "TX1_Gain_FbStep_High", .TX1_Gain_FbStep.High, BI_Limit.BI_Low_TX_Gain_FbStep_LL, BI_Limit.BI_High_TX_Gain_FbStep_UL)
                    Check_Limit_Value(slot, "TX1_Gain_FbStep_Low", .TX1_Gain_FbStep.Low, BI_Limit.BI_Low_TX_Gain_FbStep_LL, BI_Limit.BI_High_TX_Gain_FbStep_UL)

                    Check_Limit_Value(slot, "TX1_Gain_FbTxQuo_High", .TX1_Gain_FbTxQuo.High, BI_Limit.BI_Low_TX_Gain_FbTxQuo_LL, BI_Limit.BI_High_TX_Gain_FbTxQuo_UL)
                    Check_Limit_Value(slot, "TX1_Gain_FbTxQuo_low", .TX1_Gain_FbTxQuo.Low, BI_Limit.BI_Low_TX_Gain_FbTxQuo_LL, BI_Limit.BI_High_TX_Gain_FbTxQuo_UL)

                    Check_Limit_Value(slot, "TX1_Gain_GainError_High", .TX1_Gain_GainError.High, BI_Limit.BI_Low_TX_Gain_GainError_LL, BI_Limit.BI_High_TX_Gain_GainError_UL)
                    Check_Limit_Value(slot, "TX1_Gain_GainError_Low", .TX1_Gain_GainError.Low, BI_Limit.BI_Low_TX_Gain_GainError_LL, BI_Limit.BI_High_TX_Gain_GainError_UL)

                    Check_Limit_Value(slot, "TX1_PA1_PsVolt_High", .TX1_PA1_PsVolt.High, BI_Limit.BI_Low_TX_PA_PsVolt_LL, BI_Limit.BI_High_TX_PA_PsVolt_UL)
                    Check_Limit_Value(slot, "TX1_PA1_PsVolt_Low", .TX1_PA1_PsVolt.Low, BI_Limit.BI_Low_TX_PA_PsVolt_LL, BI_Limit.BI_High_TX_PA_PsVolt_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Temp_High", .TX1_PA1_Temp.High, BI_Limit.BI_Low_TX_PA_Temp_LL, BI_Limit.BI_High_TX_PA_Temp_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Temp_Low", .TX1_PA1_Temp.Low, BI_Limit.BI_Low_TX_PA_Temp_LL, BI_Limit.BI_High_TX_PA_Temp_UL)

                    Check_Limit_Value(slot, "TX1_PA1_BiasTemp_High", .TX1_PA1_BiasTemp.High, BI_Limit.BI_Low_TX_PA_BiasTemp_LL, BI_Limit.BI_High_TX_PA_BiasTemp_UL)
                    Check_Limit_Value(slot, "TX1_PA1_BiasTemp_Low", .TX1_PA1_BiasTemp.Low, BI_Limit.BI_Low_TX_PA_BiasTemp_LL, BI_Limit.BI_High_TX_PA_BiasTemp_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Driver1Cur_High", .TX1_PA1_Driver1Cur.High, BI_Limit.BI_Low_TX_PA_Driver1Cur_LL, BI_Limit.BI_High_TX_PA_Driver1Cur_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Driver1Cur_Low", .TX1_PA1_Driver1Cur.Low, BI_Limit.BI_Low_TX_PA_Driver1Cur_LL, BI_Limit.BI_High_TX_PA_Driver1Cur_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Driver2Cur_High", .TX1_PA1_Driver2Cur.High, BI_Limit.BI_Low_TX_PA_Driver2Cur_LL, BI_Limit.BI_High_TX_PA_Driver2Cur_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Driver2Cur_Low", .TX1_PA1_Driver2Cur.Low, BI_Limit.BI_Low_TX_PA_Driver2Cur_LL, BI_Limit.BI_High_TX_PA_Driver2Cur_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Driver3Cur_High", .TX1_PA1_Driver3Cur.High, BI_Limit.BI_Low_TX_PA_Driver3Cur_LL, BI_Limit.BI_High_TX_PA_Driver3Cur_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Driver3Cur_Low", .TX1_PA1_Driver3Cur.Low, BI_Limit.BI_Low_TX_PA_Driver3Cur_LL, BI_Limit.BI_High_TX_PA_Driver3Cur_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Driver4Cur_High", .TX1_PA1_Driver4Cur.High, BI_Limit.BI_Low_TX_PA_Driver4Cur_LL, BI_Limit.BI_High_TX_PA_Driver4Cur_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Driver4Cur_Low", .TX1_PA1_Driver4Cur.Low, BI_Limit.BI_Low_TX_PA_Driver4Cur_LL, BI_Limit.BI_High_TX_PA_Driver4Cur_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Final1Cur_High", .TX1_PA1_Final1Cur.High, BI_Limit.BI_Low_TX_PA_Final1Cur_LL, BI_Limit.BI_High_TX_PA_Final1Cur_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Final1Cur_Low", .TX1_PA1_Final1Cur.Low, BI_Limit.BI_Low_TX_PA_Final1Cur_LL, BI_Limit.BI_High_TX_PA_Final1Cur_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Final2Cur_High", .TX1_PA1_Final2Cur.High, BI_Limit.BI_Low_TX_PA_Final2Cur_LL, BI_Limit.BI_High_TX_PA_Final2Cur_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Final2Cur_Low", .TX1_PA1_Final2Cur.Low, BI_Limit.BI_Low_TX_PA_Final2Cur_LL, BI_Limit.BI_High_TX_PA_Final2Cur_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Driver1Dac_High", .TX1_PA1_Driver1Dac.High, BI_Limit.BI_Low_TX_PA_Driver1Dac_LL, BI_Limit.BI_High_TX_PA_Driver1Dac_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Driver1Dac_Low", .TX1_PA1_Driver1Dac.Low, BI_Limit.BI_Low_TX_PA_Driver1Dac_LL, BI_Limit.BI_High_TX_PA_Driver1Dac_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Driver2Dac_High", .TX1_PA1_Driver2Dac.High, BI_Limit.BI_Low_TX_PA_Driver2Dac_LL, BI_Limit.BI_High_TX_PA_Driver2Dac_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Driver2Dac_Low", .TX1_PA1_Driver2Dac.Low, BI_Limit.BI_Low_TX_PA_Driver2Dac_LL, BI_Limit.BI_High_TX_PA_Driver2Dac_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Driver3Dac_High", .TX1_PA1_Driver3Dac.High, BI_Limit.BI_Low_TX_PA_Driver3Dac_LL, BI_Limit.BI_High_TX_PA_Driver3Dac_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Driver3Dac_Low", .TX1_PA1_Driver3Dac.Low, BI_Limit.BI_Low_TX_PA_Driver3Dac_LL, BI_Limit.BI_High_TX_PA_Driver3Dac_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Driver4Dac_High", .TX1_PA1_Driver4Dac.High, BI_Limit.BI_Low_TX_PA_Driver4Dac_LL, BI_Limit.BI_High_TX_PA_Driver4Dac_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Driver4Dac_Low", .TX1_PA1_Driver4Dac.Low, BI_Limit.BI_Low_TX_PA_Driver4Dac_LL, BI_Limit.BI_High_TX_PA_Driver4Dac_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Final1Dac_High", .TX1_PA1_Final1Dac.High, BI_Limit.BI_Low_TX_PA_Final1Dac_LL, BI_Limit.BI_High_TX_PA_Final1Dac_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Final1Dac_Low", .TX1_PA1_Final1Dac.Low, BI_Limit.BI_Low_TX_PA_Final1Dac_LL, BI_Limit.BI_High_TX_PA_Final1Dac_UL)

                    Check_Limit_Value(slot, "TX1_PA1_Final2Dac_High", .TX1_PA1_Final2Dac.High, BI_Limit.BI_Low_TX_PA_Final2Dac_LL, BI_Limit.BI_High_TX_PA_Final2Dac_UL)
                    Check_Limit_Value(slot, "TX1_PA1_Final2Dac_Low", .TX1_PA1_Final2Dac.Low, BI_Limit.BI_Low_TX_PA_Final2Dac_LL, BI_Limit.BI_High_TX_PA_Final2Dac_UL)

                    Check_Limit_Value(slot, "TX1_PA1_ampDelayInt_High", .TX1_PA1_ampDelayInt.High, BI_Limit.BI_Low_TX_PA_ampDelayInt_LL, BI_Limit.BI_High_TX_PA_ampDelayInt_UL)
                    Check_Limit_Value(slot, "TX1_PA1_ampDelayInt_Low", .TX1_PA1_ampDelayInt.Low, BI_Limit.BI_Low_TX_PA_ampDelayInt_LL, BI_Limit.BI_High_TX_PA_ampDelayInt_UL)

                    Check_Limit_Value(slot, "TX1_PA1_ampDelayFrac_High", .TX1_PA1_ampDelayFrac.High, BI_Limit.BI_Low_TX_PA_ampDelayFrac_LL, BI_Limit.BI_High_TX_PA_ampDelayFrac_UL)
                    Check_Limit_Value(slot, "TX1_PA1_ampDelayFrac_Low", .TX1_PA1_ampDelayFrac.Low, BI_Limit.BI_Low_TX_PA_ampDelayFrac_LL, BI_Limit.BI_High_TX_PA_ampDelayFrac_UL)

                    Check_Limit_Value(slot, "TX1_PA1_MaxCrossCorrelation_High", .TX1_PA1_MaxCrossCorrelation.High, BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_LL, BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_UL)
                    Check_Limit_Value(slot, "TX1_PA1_MaxCrossCorrelation_Low", .TX1_PA1_MaxCrossCorrelation.Low, BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_LL, BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_UL)

                    Check_Limit_Value(slot, "TX1_Tx1_DPD_L1_Table_Max_Gain_High", .TX1_DPD_L1_Table_Max_Gain.High, BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_UL)
                    Check_Limit_Value(slot, "TX1_Tx1_DPD_L1_Table_Max_Gain_Low", .TX1_DPD_L1_Table_Max_Gain.Low, BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_UL)

                    Check_Limit_Value(slot, "TX1_Tx1_DPD_L1_Table_Min_Gain_High", .TX1_DPD_L1_Table_Min_Gain.High, BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_UL)
                    Check_Limit_Value(slot, "TX1_Tx1_DPD_L1_Table_Min_Gain_Low", .TX1_DPD_L1_Table_Min_Gain.Low, BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_LL, BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L2_3rd_sym_am_High", .Tx1_DPD_L2_3rd_sym_am.High, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L2_3rd_sym_am_Low", .Tx1_DPD_L2_3rd_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L2_3rd_sym_ph_High", .Tx1_DPD_L2_3rd_sym_ph.High, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L2_3rd_sym_ph_Low", .Tx1_DPD_L2_3rd_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L3_3rd_sym_am_High", .Tx1_DPD_L3_3rd_sym_am.High, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L3_3rd_sym_am_Low", .Tx1_DPD_L3_3rd_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L3_3rd_sym_ph_High", .Tx1_DPD_L3_3rd_sym_ph.High, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L3_3rd_sym_ph_Low", .Tx1_DPD_L3_3rd_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L2_5th_sym_am_High", .Tx1_DPD_L2_5th_sym_am.High, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L2_5th_sym_am_Low", .Tx1_DPD_L2_5th_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L2_5th_sym_ph_High", .Tx1_DPD_L2_5th_sym_ph.High, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L2_5th_sym_ph_Low", .Tx1_DPD_L2_5th_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L3_5th_sym_am_High", .Tx1_DPD_L3_5th_sym_am.High, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L3_5th_sym_am_Low", .Tx1_DPD_L3_5th_sym_am.Low, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L3_5th_sym_ph_High", .Tx1_DPD_L3_5th_sym_ph.High, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L3_5th_sym_ph_Low", .Tx1_DPD_L3_5th_sym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_LL, BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L2_3rd_asym_am_High", .Tx1_DPD_L2_3rd_asym_am.High, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L2_3rd_asym_am_Low", .Tx1_DPD_L2_3rd_asym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L2_3rd_asym_ph_High", .Tx1_DPD_L2_3rd_asym_ph.High, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L2_3rd_asym_ph_Low", .Tx1_DPD_L2_3rd_asym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L2_5th_asym_am_High", .Tx1_DPD_L2_5th_asym_am.High, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L2_5th_asym_am_Low", .Tx1_DPD_L2_5th_asym_am.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_UL)

                    Check_Limit_Value(slot, "Tx1_DPD_L2_5th_asym_ph_High", .Tx1_DPD_L2_5th_asym_ph.High, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_UL)
                    Check_Limit_Value(slot, "Tx1_DPD_L2_5th_asym_ph_Low", .Tx1_DPD_L2_5th_asym_ph.Low, BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_LL, BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_UL)

                    'RX0
                    Check_Limit_Value(slot, "Rx0_LNA_Atten_High", .Rx0_LNA_Atten.High, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)
                    Check_Limit_Value(slot, "Rx0_LNA_Atten_Low", .Rx0_LNA_Atten.Low, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)

                    Check_Limit_Value(slot, "Rx0_Rx_Atten0_High", .Rx0_Rx_Atten0.High, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)
                    Check_Limit_Value(slot, "Rx0_Rx_Atten0_Low", .Rx0_Rx_Atten0.Low, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)

                    Check_Limit_Value(slot, "Rx0_Rx_Atten1_High", .Rx0_Rx_Atten1.High, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)
                    Check_Limit_Value(slot, "Rx0_Rx_Atten1_Low", .Rx0_Rx_Atten1.Low, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)

                    Check_Limit_Value(slot, "Rx0_RSSI_C0_High", .Rx0_RSSI_C0.High, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)
                    Check_Limit_Value(slot, "Rx0_RSSI_C0_Low", .Rx0_RSSI_C0.Low, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)

                    Check_Limit_Value(slot, "Rx0_RSSI_C1_High", .Rx0_RSSI_C1.High, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)
                    Check_Limit_Value(slot, "Rx0_RSSI_C1_Low", .Rx0_RSSI_C1.Low, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)

                    'RX1
                    Check_Limit_Value(slot, "Rx1_LNA_Atten_High", .Rx1_LNA_Atten.High, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)
                    Check_Limit_Value(slot, "Rx1_LNA_Atten_Low", .Rx1_LNA_Atten.Low, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)

                    Check_Limit_Value(slot, "Rx1_Rx_Atten0_High", .Rx1_Rx_Atten0.High, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)
                    Check_Limit_Value(slot, "Rx1_Rx_Atten0_Low", .Rx1_Rx_Atten0.Low, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)

                    Check_Limit_Value(slot, "Rx1_Rx_Atten1_High", .Rx1_Rx_Atten1.High, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)
                    Check_Limit_Value(slot, "Rx1_Rx_Atten1_Low", .Rx1_Rx_Atten1.Low, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)

                    Check_Limit_Value(slot, "Rx1_RSSI_C0_High", .Rx1_RSSI_C0.High, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)
                    Check_Limit_Value(slot, "Rx1_RSSI_C0_Low", .Rx1_RSSI_C0.Low, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)

                    Check_Limit_Value(slot, "Rx1_RSSI_C1_High", .Rx1_RSSI_C1.High, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)
                    Check_Limit_Value(slot, "Rx1_RSSI_C1_Low", .Rx1_RSSI_C1.Low, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)

                    'RX2
                    Check_Limit_Value(slot, "RX2_LNA_Atten_High", .Rx2_LNA_Atten.High, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)
                    Check_Limit_Value(slot, "RX2_LNA_Atten_Low", .Rx2_LNA_Atten.Low, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)

                    Check_Limit_Value(slot, "RX2_Rx_Atten0_High", .Rx2_Rx_Atten0.High, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)
                    Check_Limit_Value(slot, "RX2_Rx_Atten0_Low", .Rx2_Rx_Atten0.Low, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)

                    Check_Limit_Value(slot, "RX2_Rx_Atten1_High", .Rx2_Rx_Atten1.High, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)
                    Check_Limit_Value(slot, "RX2_Rx_Atten1_Low", .Rx2_Rx_Atten1.Low, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)

                    Check_Limit_Value(slot, "RX2_RSSI_C0_High", .Rx2_RSSI_C0.High, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)
                    Check_Limit_Value(slot, "RX2_RSSI_C0_Low", .Rx2_RSSI_C0.Low, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)

                    Check_Limit_Value(slot, "RX2_RSSI_C1_High", .Rx2_RSSI_C1.High, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)
                    Check_Limit_Value(slot, "RX2_RSSI_C1_Low", .Rx2_RSSI_C1.Low, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)

                    'RX3
                    Check_Limit_Value(slot, "RX3_LNA_Atten_High", .Rx3_LNA_Atten.High, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)
                    Check_Limit_Value(slot, "RX3_LNA_Atten_Low", .Rx3_LNA_Atten.Low, BI_Limit.BI_Low_Rx_LNA_Atten_LL, BI_Limit.BI_High_Rx_LNA_Atten_UL)

                    Check_Limit_Value(slot, "RX3_Rx_Atten0_High", .Rx3_Rx_Atten0.High, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)
                    Check_Limit_Value(slot, "RX3_Rx_Atten0_Low", .Rx3_Rx_Atten0.Low, BI_Limit.BI_Low_Rx_Rx_Atten0_LL, BI_Limit.BI_High_Rx_Rx_Atten0_UL)

                    Check_Limit_Value(slot, "RX3_Rx_Atten1_High", .Rx3_Rx_Atten1.High, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)
                    Check_Limit_Value(slot, "RX3_Rx_Atten1_Low", .Rx3_Rx_Atten1.Low, BI_Limit.BI_Low_Rx_Rx_Atten1_LL, BI_Limit.BI_High_Rx_Rx_Atten1_UL)

                    Check_Limit_Value(slot, "RX3_RSSI_C0_High", .Rx3_RSSI_C0.High, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)
                    Check_Limit_Value(slot, "RX3_RSSI_C0_Low", .Rx3_RSSI_C0.Low, BI_Limit.BI_Low_Rx_RSSI_C0_LL, BI_Limit.BI_High_Rx_RSSI_C0_UL)

                    Check_Limit_Value(slot, "RX3_RSSI_C1_High", .Rx3_RSSI_C1.High, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)
                    Check_Limit_Value(slot, "RX3_RSSI_C1_Low", .Rx3_RSSI_C1.Low, BI_Limit.BI_Low_Rx_RSSI_C1_LL, BI_Limit.BI_High_Rx_RSSI_C1_UL)

                    'PS
                    Check_Limit_Value(slot, "Input_Voltage_High", .Input_Voltage.High, BI_Limit.BI_Low_Input_Voltage_LL, BI_Limit.BI_High_Input_Voltage_UL)
                    Check_Limit_Value(slot, "Input_Voltage_Low", .Input_Voltage.Low, BI_Limit.BI_Low_Input_Voltage_LL, BI_Limit.BI_High_Input_Voltage_UL)

                    Check_Limit_Value(slot, "Input_Current_HP_High", .Input_Current_HP.High, 0, 999)
                    Check_Limit_Value(slot, "Input_Current_HP_Low", .Input_Current_HP.Low, 0, 999)

                    Check_Limit_Value(slot, "Input_Current_LP_High", .Input_Current_LP.High, 0, 999)
                    Check_Limit_Value(slot, "Input_Current_LP_Low", .Input_Current_LP.Low, 0, 999)

                    Check_Limit_Value(slot, "Input_Power_HP_High", .Input_Power_HP.High, 0, 999)
                    Check_Limit_Value(slot, "Input_Power_HP_Low", .Input_Power_HP.Low, 0, 999)

                    Check_Limit_Value(slot, "Input_Power_LP_High", .Input_Power_LP.High, 0, 999)
                    Check_Limit_Value(slot, "Input_Power_LP_Low", .Input_Power_LP.Low, 0, 999)

                    Check_Limit_Value(slot, "Output_Voltage_High", .Output_Voltage.High, BI_Limit.BI_Low_Output_Voltage_LL, BI_Limit.BI_High_Output_Voltage_UL)
                    Check_Limit_Value(slot, "Output_Voltage_Low", .Output_Voltage.Low, BI_Limit.BI_Low_Output_Voltage_LL, BI_Limit.BI_High_Output_Voltage_UL)

                    Check_Limit_Value(slot, "AISG_12V_Voltage_High", .AISG_12V_Voltage.High, BI_Limit.BI_Low_AISG_12V_Voltage_LL, BI_Limit.BI_High_AISG_12V_Voltage_UL)
                    Check_Limit_Value(slot, "AISG_12V_Voltage_Low", .AISG_12V_Voltage.Low, BI_Limit.BI_Low_AISG_12V_Voltage_LL, BI_Limit.BI_High_AISG_12V_Voltage_UL)

                    Check_Limit_Value(slot, "AISG_12V_Current_High", .AISG_12V_Current.High, BI_Limit.BI_Low_AISG_12V_Current_LL, BI_Limit.BI_High_AISG_12V_Current_UL)
                    Check_Limit_Value(slot, "AISG_12V_Current_Low", .AISG_12V_Current.Low, BI_Limit.BI_Low_AISG_12V_Current_LL, BI_Limit.BI_High_AISG_12V_Current_UL)

                    Check_Limit_Value(slot, "AISG_24V_Voltage_High", .AISG_24V_Voltage.High, BI_Limit.BI_Low_AISG_24V_Voltage_LL, BI_Limit.BI_High_AISG_24V_Voltage_UL)
                    Check_Limit_Value(slot, "AISG_24V_Voltage_Low", .AISG_24V_Voltage.Low, BI_Limit.BI_Low_AISG_24V_Voltage_LL, BI_Limit.BI_High_AISG_24V_Voltage_UL)

                    Check_Limit_Value(slot, "AISG_24V_Current_High", .AISG_24V_Current.High, BI_Limit.BI_Low_AISG_24V_Current_LL, BI_Limit.BI_High_AISG_24V_Current_UL)
                    Check_Limit_Value(slot, "AISG_24V_Current_Low", .AISG_24V_Current.Low, BI_Limit.BI_Low_AISG_24V_Current_LL, BI_Limit.BI_High_AISG_24V_Current_UL)

                    If .Last_FPGARevision = String.Empty Then
                        .Last_FPGARevision = .FPGARevision
                    Else
                        Check_Limit_String(slot, "FPGARevision", .Last_FPGARevision, .FPGARevision)
                    End If

                    If .Last_DSPRevision = String.Empty Then
                        .Last_DSPRevision = .DSPRevision
                    Else
                        Check_Limit_String(slot, "DSPRevision", .Last_DSPRevision, .DSPRevision)
                    End If

                    If .Last_SWRevision = String.Empty Then
                        .Last_SWRevision = .SWRevision
                    Else
                        Check_Limit_String(slot, "SWRevision", .Last_SWRevision, .SWRevision)
                    End If

                    Check_Limit_String(slot, "Alarm_Test", .AlarmString, "No Alarm")

                    Check_Limit_Value(slot, "PowerCycleCount", .PowerCycleCount, 0, BI_Profile.BI_Power_Cycle)

                End If

                DisplayAlarm(slot, .AlarmFlag)

            End With
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Public Sub UpdateDCFData(ByVal slot As Integer)
        Try
            Dim tmp As Double = 0.0

            With BI_Data(slot)
                'If .RecData.First_Polling Then
                '    If .MeasData.PAEnabled Then
                '        .RecData.First_Polling = False
                '        .RecData.First_Polling_Final2_Current = .MeasData.FINAL2
                '    End If
                'End If

                If .RecData.PA0_Temp.High <= .MeasData.PA0_Temp Then .RecData.PA0_Temp.High = .MeasData.PA0_Temp
                If .RecData.PA0_Temp.Low > .MeasData.PA0_Temp Then .RecData.PA0_Temp.Low = .MeasData.PA0_Temp

                If .RecData.PA0_VSWR_Temp.High <= .MeasData.PA0_VSWR_Temp Then .RecData.PA0_VSWR_Temp.High = .MeasData.PA0_VSWR_Temp
                If .RecData.PA0_VSWR_Temp.Low > .MeasData.PA0_VSWR_Temp Then .RecData.PA0_VSWR_Temp.Low = .MeasData.PA0_VSWR_Temp

                If .RecData.PA1_Temp.High <= .MeasData.PA1_Temp Then .RecData.PA1_Temp.High = .MeasData.PA1_Temp
                If .RecData.PA1_Temp.Low > .MeasData.PA1_Temp Then .RecData.PA1_Temp.Low = .MeasData.PA1_Temp

                If .RecData.PA1_VSWR_Temp.High <= .MeasData.PA1_VSWR_Temp Then .RecData.PA1_VSWR_Temp.High = .MeasData.PA1_VSWR_Temp
                If .RecData.PA1_VSWR_Temp.Low > .MeasData.PA1_VSWR_Temp Then .RecData.PA1_VSWR_Temp.Low = .MeasData.PA1_VSWR_Temp

                If .RecData.PA_Temp_Delta.High <= .MeasData.PA_Temp_Delta Then .RecData.PA_Temp_Delta.High = .MeasData.PA_Temp_Delta
                If .RecData.PA_Temp_Delta.Low > .MeasData.PA_Temp_Delta Then .RecData.PA_Temp_Delta.Low = .MeasData.PA_Temp_Delta

                If .RecData.LNA0_Temp.High <= .MeasData.LNA0_Temp Then .RecData.LNA0_Temp.High = .MeasData.LNA0_Temp
                If .RecData.LNA0_Temp.Low > .MeasData.LNA0_Temp Then .RecData.LNA0_Temp.Low = .MeasData.LNA0_Temp

                If .RecData.LNA1_Temp.High <= .MeasData.LNA1_Temp Then .RecData.LNA1_Temp.High = .MeasData.LNA1_Temp
                If .RecData.LNA1_Temp.Low > .MeasData.LNA1_Temp Then .RecData.LNA1_Temp.Low = .MeasData.LNA1_Temp

                If .RecData.LNA2_Temp.High <= .MeasData.LNA2_Temp Then .RecData.LNA2_Temp.High = .MeasData.LNA2_Temp
                If .RecData.LNA2_Temp.Low > .MeasData.LNA2_Temp Then .RecData.LNA2_Temp.Low = .MeasData.LNA2_Temp

                If .RecData.LNA3_Temp.High <= .MeasData.LNA3_Temp Then .RecData.LNA3_Temp.High = .MeasData.LNA3_Temp
                If .RecData.LNA3_Temp.Low > .MeasData.LNA3_Temp Then .RecData.LNA3_Temp.Low = .MeasData.LNA3_Temp

                If .RecData.PS_Converter_Temp.High <= .MeasData.PS_Converter_Temp Then .RecData.PS_Converter_Temp.High = .MeasData.PS_Converter_Temp
                If .RecData.PS_Converter_Temp.Low > .MeasData.PS_Converter_Temp Then .RecData.PS_Converter_Temp.Low = .MeasData.PS_Converter_Temp

                If .RecData.PS_Brick_Temp.High <= .MeasData.PS_Brick_Temp Then .RecData.PS_Brick_Temp.High = .MeasData.PS_Brick_Temp
                If .RecData.PS_Brick_Temp.Low > .MeasData.PS_Brick_Temp Then .RecData.PS_Brick_Temp.Low = .MeasData.PS_Brick_Temp

                If .RecData.PSU_Temp_Delta.High <= .MeasData.PSU_Temp_Delta Then .RecData.PSU_Temp_Delta.High = .MeasData.PSU_Temp_Delta
                If .RecData.PSU_Temp_Delta.Low > .MeasData.PSU_Temp_Delta Then .RecData.PSU_Temp_Delta.Low = .MeasData.PSU_Temp_Delta

                If .RecData.PSU_PA_Temp_Delta.High <= .MeasData.PSU_PA_Temp_Delta Then .RecData.PSU_PA_Temp_Delta.High = .MeasData.PSU_PA_Temp_Delta
                If .RecData.PSU_PA_Temp_Delta.Low > .MeasData.PSU_PA_Temp_Delta Then .RecData.PSU_PA_Temp_Delta.Low = .MeasData.PSU_PA_Temp_Delta

                If .RecData.FB_Temp.High <= .MeasData.FB_Temp Then .RecData.FB_Temp.High = .MeasData.FB_Temp
                If .RecData.FB_Temp.Low > .MeasData.FB_Temp Then .RecData.FB_Temp.Low = .MeasData.FB_Temp

                If .RecData.RX_Temp.High <= .MeasData.RX_Temp Then .RecData.RX_Temp.High = .MeasData.RX_Temp
                If .RecData.RX_Temp.Low > .MeasData.RX_Temp Then .RecData.RX_Temp.Low = .MeasData.RX_Temp

                If .MeasData.PAEnabled Then
                    'Tx0
                    If .RecData.TX0_Output_Pow.High <= .MeasData.TX0_Output_Pow Then .RecData.TX0_Output_Pow.High = .MeasData.TX0_Output_Pow
                    If .RecData.TX0_Output_Pow.Low > .MeasData.TX0_Output_Pow Then .RecData.TX0_Output_Pow.Low = .MeasData.TX0_Output_Pow

                    If .RecData.TX0_VSWR.High <= .MeasData.TX0_VSWR Then .RecData.TX0_VSWR.High = .MeasData.TX0_VSWR
                    If .RecData.TX0_VSWR.Low > .MeasData.TX0_VSWR Then .RecData.TX0_VSWR.Low = .MeasData.TX0_VSWR

                    If .RecData.TX0_Forward_Power_Detector.High <= .MeasData.TX0_Forward_Power_Detector Then .RecData.TX0_Forward_Power_Detector.High = .MeasData.TX0_Forward_Power_Detector
                    If .RecData.TX0_Forward_Power_Detector.Low > .MeasData.TX0_Forward_Power_Detector Then .RecData.TX0_Forward_Power_Detector.Low = .MeasData.TX0_Forward_Power_Detector

                    If .RecData.TX0_Reverse_Power_Detector.High <= .MeasData.TX0_Reverse_Power_Detector Then .RecData.TX0_Reverse_Power_Detector.High = .MeasData.TX0_Reverse_Power_Detector
                    If .RecData.TX0_Reverse_Power_Detector.Low > .MeasData.TX0_Reverse_Power_Detector Then .RecData.TX0_Reverse_Power_Detector.Low = .MeasData.TX0_Reverse_Power_Detector

                    If .RecData.TX0_Gain_VCA.High <= .MeasData.TX0_Gain_VCA Then .RecData.TX0_Gain_VCA.High = .MeasData.TX0_Gain_VCA
                    If .RecData.TX0_Gain_VCA.Low > .MeasData.TX0_Gain_VCA Then .RecData.TX0_Gain_VCA.Low = .MeasData.TX0_Gain_VCA

                    If .RecData.TX0_txDacGain.High <= .MeasData.TX0_txDacGain Then .RecData.TX0_txDacGain.High = .MeasData.TX0_txDacGain
                    If .RecData.TX0_txDacGain.Low > .MeasData.TX0_txDacGain Then .RecData.TX0_txDacGain.Low = .MeasData.TX0_txDacGain

                    If .RecData.TX0_totalTxAttn.High <= .MeasData.TX0_totalTxAttn Then .RecData.TX0_totalTxAttn.High = .MeasData.TX0_totalTxAttn
                    If .RecData.TX0_totalTxAttn.Low > .MeasData.TX0_totalTxAttn Then .RecData.TX0_totalTxAttn.Low = .MeasData.TX0_totalTxAttn

                    If .RecData.TX0_Gain_TxStep.High <= .MeasData.TX0_Gain_TxStep Then .RecData.TX0_Gain_TxStep.High = .MeasData.TX0_Gain_TxStep
                    If .RecData.TX0_Gain_TxStep.Low > .MeasData.TX0_Gain_TxStep Then .RecData.TX0_Gain_TxStep.Low = .MeasData.TX0_Gain_TxStep

                    If .RecData.TX0_Gain_FbStep.High <= .MeasData.TX0_Gain_FbStep Then .RecData.TX0_Gain_FbStep.High = .MeasData.TX0_Gain_FbStep
                    If .RecData.TX0_Gain_FbStep.Low > .MeasData.TX0_Gain_FbStep Then .RecData.TX0_Gain_FbStep.Low = .MeasData.TX0_Gain_FbStep

                    If .RecData.TX0_Gain_FbTxQuo.High <= .MeasData.TX0_Gain_FbTxQuo Then .RecData.TX0_Gain_FbTxQuo.High = .MeasData.TX0_Gain_FbTxQuo
                    If .RecData.TX0_Gain_FbTxQuo.Low > .MeasData.TX0_Gain_FbTxQuo Then .RecData.TX0_Gain_FbTxQuo.Low = .MeasData.TX0_Gain_FbTxQuo

                    If .RecData.TX0_Gain_GainError.High <= .MeasData.TX0_Gain_GainError Then .RecData.TX0_Gain_GainError.High = .MeasData.TX0_Gain_GainError
                    If .RecData.TX0_Gain_GainError.Low > .MeasData.TX0_Gain_GainError Then .RecData.TX0_Gain_GainError.Low = .MeasData.TX0_Gain_GainError

                    If .RecData.TX0_PA0_PsVolt.High <= .MeasData.TX0_PA0_PsVolt Then .RecData.TX0_PA0_PsVolt.High = .MeasData.TX0_PA0_PsVolt
                    If .RecData.TX0_PA0_PsVolt.Low > .MeasData.TX0_PA0_PsVolt Then .RecData.TX0_PA0_PsVolt.Low = .MeasData.TX0_PA0_PsVolt

                    If .RecData.TX0_PA0_Temp.High <= .MeasData.TX0_PA0_Temp Then .RecData.TX0_PA0_Temp.High = .MeasData.TX0_PA0_Temp
                    If .RecData.TX0_PA0_Temp.Low > .MeasData.TX0_PA0_Temp Then .RecData.TX0_PA0_Temp.Low = .MeasData.TX0_PA0_Temp

                    If .RecData.TX0_PA0_BiasTemp.High <= .MeasData.TX0_PA0_BiasTemp Then .RecData.TX0_PA0_BiasTemp.High = .MeasData.TX0_PA0_BiasTemp
                    If .RecData.TX0_PA0_BiasTemp.Low > .MeasData.TX0_PA0_BiasTemp Then .RecData.TX0_PA0_BiasTemp.Low = .MeasData.TX0_PA0_BiasTemp

                    If .RecData.TX0_PA0_Driver1Cur.High <= .MeasData.TX0_PA0_Driver1Cur Then .RecData.TX0_PA0_Driver1Cur.High = .MeasData.TX0_PA0_Driver1Cur
                    If .RecData.TX0_PA0_Driver1Cur.Low > .MeasData.TX0_PA0_Driver1Cur Then .RecData.TX0_PA0_Driver1Cur.Low = .MeasData.TX0_PA0_Driver1Cur

                    If .RecData.TX0_PA0_Driver2Cur.High <= .MeasData.TX0_PA0_Driver2Cur Then .RecData.TX0_PA0_Driver2Cur.High = .MeasData.TX0_PA0_Driver2Cur
                    If .RecData.TX0_PA0_Driver2Cur.Low > .MeasData.TX0_PA0_Driver2Cur Then .RecData.TX0_PA0_Driver2Cur.Low = .MeasData.TX0_PA0_Driver2Cur

                    If .RecData.TX0_PA0_Driver3Cur.High <= .MeasData.TX0_PA0_Driver3Cur Then .RecData.TX0_PA0_Driver3Cur.High = .MeasData.TX0_PA0_Driver3Cur
                    If .RecData.TX0_PA0_Driver3Cur.Low > .MeasData.TX0_PA0_Driver3Cur Then .RecData.TX0_PA0_Driver3Cur.Low = .MeasData.TX0_PA0_Driver3Cur

                    If .RecData.TX0_PA0_Driver4Cur.High <= .MeasData.TX0_PA0_Driver4Cur Then .RecData.TX0_PA0_Driver4Cur.High = .MeasData.TX0_PA0_Driver4Cur
                    If .RecData.TX0_PA0_Driver4Cur.Low > .MeasData.TX0_PA0_Driver4Cur Then .RecData.TX0_PA0_Driver4Cur.Low = .MeasData.TX0_PA0_Driver4Cur

                    If .RecData.TX0_PA0_Final1Cur.High <= .MeasData.TX0_PA0_Final1Cur Then .RecData.TX0_PA0_Final1Cur.High = .MeasData.TX0_PA0_Final1Cur
                    If .RecData.TX0_PA0_Final1Cur.Low > .MeasData.TX0_PA0_Final1Cur Then .RecData.TX0_PA0_Final1Cur.Low = .MeasData.TX0_PA0_Final1Cur

                    If .RecData.TX0_PA0_Final2Cur.High <= .MeasData.TX0_PA0_Final2Cur Then .RecData.TX0_PA0_Final2Cur.High = .MeasData.TX0_PA0_Final2Cur
                    If .RecData.TX0_PA0_Final2Cur.Low > .MeasData.TX0_PA0_Final2Cur Then .RecData.TX0_PA0_Final2Cur.Low = .MeasData.TX0_PA0_Final2Cur

                    If .RecData.TX0_PA0_Driver1Dac.High <= .MeasData.TX0_PA0_Driver1Dac Then .RecData.TX0_PA0_Driver1Dac.High = .MeasData.TX0_PA0_Driver1Dac
                    If .RecData.TX0_PA0_Driver1Dac.Low > .MeasData.TX0_PA0_Driver1Dac Then .RecData.TX0_PA0_Driver1Dac.Low = .MeasData.TX0_PA0_Driver1Dac

                    If .RecData.TX0_PA0_Driver2Dac.High <= .MeasData.TX0_PA0_Driver2Dac Then .RecData.TX0_PA0_Driver2Dac.High = .MeasData.TX0_PA0_Driver2Dac
                    If .RecData.TX0_PA0_Driver2Dac.Low > .MeasData.TX0_PA0_Driver2Dac Then .RecData.TX0_PA0_Driver2Dac.Low = .MeasData.TX0_PA0_Driver2Dac

                    If .RecData.TX0_PA0_Driver3Dac.High <= .MeasData.TX0_PA0_Driver3Dac Then .RecData.TX0_PA0_Driver3Dac.High = .MeasData.TX0_PA0_Driver3Dac
                    If .RecData.TX0_PA0_Driver3Dac.Low > .MeasData.TX0_PA0_Driver3Dac Then .RecData.TX0_PA0_Driver3Dac.Low = .MeasData.TX0_PA0_Driver3Dac

                    If .RecData.TX0_PA0_Driver4Dac.High <= .MeasData.TX0_PA0_Driver4Dac Then .RecData.TX0_PA0_Driver4Dac.High = .MeasData.TX0_PA0_Driver4Dac
                    If .RecData.TX0_PA0_Driver4Dac.Low > .MeasData.TX0_PA0_Driver4Dac Then .RecData.TX0_PA0_Driver4Dac.Low = .MeasData.TX0_PA0_Driver4Dac

                    If .RecData.TX0_PA0_Final1Dac.High <= .MeasData.TX0_PA0_Final1Dac Then .RecData.TX0_PA0_Final1Dac.High = .MeasData.TX0_PA0_Final1Dac
                    If .RecData.TX0_PA0_Final1Dac.Low > .MeasData.TX0_PA0_Final1Dac Then .RecData.TX0_PA0_Final1Dac.Low = .MeasData.TX0_PA0_Final1Dac

                    If .RecData.TX0_PA0_Final2Dac.High <= .MeasData.TX0_PA0_Final2Dac Then .RecData.TX0_PA0_Final2Dac.High = .MeasData.TX0_PA0_Final2Dac
                    If .RecData.TX0_PA0_Final2Dac.Low > .MeasData.TX0_PA0_Final2Dac Then .RecData.TX0_PA0_Final2Dac.Low = .MeasData.TX0_PA0_Final2Dac

                    If .RecData.TX0_PA0_ampDelayInt.High <= .MeasData.TX0_PA0_ampDelayInt Then .RecData.TX0_PA0_ampDelayInt.High = .MeasData.TX0_PA0_ampDelayInt
                    If .RecData.TX0_PA0_ampDelayInt.Low > .MeasData.TX0_PA0_ampDelayInt Then .RecData.TX0_PA0_ampDelayInt.Low = .MeasData.TX0_PA0_ampDelayInt

                    If .RecData.TX0_PA0_ampDelayFrac.High <= .MeasData.TX0_PA0_ampDelayFrac Then .RecData.TX0_PA0_ampDelayFrac.High = .MeasData.TX0_PA0_ampDelayFrac
                    If .RecData.TX0_PA0_ampDelayFrac.Low > .MeasData.TX0_PA0_ampDelayFrac Then .RecData.TX0_PA0_ampDelayFrac.Low = .MeasData.TX0_PA0_ampDelayFrac

                    If .RecData.TX0_PA0_MaxCrossCorrelation.High <= .MeasData.TX0_PA0_MaxCrossCorrelation Then .RecData.TX0_PA0_MaxCrossCorrelation.High = .MeasData.TX0_PA0_MaxCrossCorrelation
                    If .RecData.TX0_PA0_MaxCrossCorrelation.Low > .MeasData.TX0_PA0_MaxCrossCorrelation Then .RecData.TX0_PA0_MaxCrossCorrelation.Low = .MeasData.TX0_PA0_MaxCrossCorrelation

                    If .RecData.TX0_DPD_L1_Table_Max_Gain.High <= .MeasData.Tx0_DPD_L1_Table_Max_Gain Then .RecData.TX0_DPD_L1_Table_Max_Gain.High = .MeasData.Tx0_DPD_L1_Table_Max_Gain
                    If .RecData.TX0_DPD_L1_Table_Max_Gain.Low > .MeasData.Tx0_DPD_L1_Table_Max_Gain Then .RecData.TX0_DPD_L1_Table_Max_Gain.Low = .MeasData.Tx0_DPD_L1_Table_Max_Gain

                    If .RecData.TX0_DPD_L1_Table_Min_Gain.High <= .MeasData.Tx0_DPD_L1_Table_Min_Gain Then .RecData.TX0_DPD_L1_Table_Min_Gain.High = .MeasData.Tx0_DPD_L1_Table_Min_Gain
                    If .RecData.TX0_DPD_L1_Table_Min_Gain.Low > .MeasData.Tx0_DPD_L1_Table_Min_Gain Then .RecData.TX0_DPD_L1_Table_Min_Gain.Low = .MeasData.Tx0_DPD_L1_Table_Min_Gain

                    If .RecData.Tx0_DPD_L2_3rd_sym_am.High <= .MeasData.Tx0_DPD_L2_3rd_sym_am Then .RecData.Tx0_DPD_L2_3rd_sym_am.High = .MeasData.Tx0_DPD_L2_3rd_sym_am
                    If .RecData.Tx0_DPD_L2_3rd_sym_am.Low > .MeasData.Tx0_DPD_L2_3rd_sym_am Then .RecData.Tx0_DPD_L2_3rd_sym_am.Low = .MeasData.Tx0_DPD_L2_3rd_sym_am

                    If .RecData.Tx0_DPD_L2_3rd_sym_ph.High <= .MeasData.Tx0_DPD_L2_3rd_sym_ph Then .RecData.Tx0_DPD_L2_3rd_sym_ph.High = .MeasData.Tx0_DPD_L2_3rd_sym_ph
                    If .RecData.Tx0_DPD_L2_3rd_sym_ph.Low > .MeasData.Tx0_DPD_L2_3rd_sym_ph Then .RecData.Tx0_DPD_L2_3rd_sym_ph.Low = .MeasData.Tx0_DPD_L2_3rd_sym_ph

                    If .RecData.Tx0_DPD_L3_3rd_sym_am.High <= .MeasData.Tx0_DPD_L3_3rd_sym_am Then .RecData.Tx0_DPD_L3_3rd_sym_am.High = .MeasData.Tx0_DPD_L3_3rd_sym_am
                    If .RecData.Tx0_DPD_L3_3rd_sym_am.Low > .MeasData.Tx0_DPD_L3_3rd_sym_am Then .RecData.Tx0_DPD_L3_3rd_sym_am.Low = .MeasData.Tx0_DPD_L3_3rd_sym_am

                    If .RecData.Tx0_DPD_L3_3rd_sym_ph.High <= .MeasData.Tx0_DPD_L3_3rd_sym_ph Then .RecData.Tx0_DPD_L3_3rd_sym_ph.High = .MeasData.Tx0_DPD_L3_3rd_sym_ph
                    If .RecData.Tx0_DPD_L3_3rd_sym_ph.Low > .MeasData.Tx0_DPD_L3_3rd_sym_ph Then .RecData.Tx0_DPD_L3_3rd_sym_ph.Low = .MeasData.Tx0_DPD_L3_3rd_sym_ph

                    If .RecData.Tx0_DPD_L2_5th_sym_am.High <= .MeasData.Tx0_DPD_L2_5th_sym_am Then .RecData.Tx0_DPD_L2_5th_sym_am.High = .MeasData.Tx0_DPD_L2_5th_sym_am
                    If .RecData.Tx0_DPD_L2_5th_sym_am.Low > .MeasData.Tx0_DPD_L2_5th_sym_am Then .RecData.Tx0_DPD_L2_5th_sym_am.Low = .MeasData.Tx0_DPD_L2_5th_sym_am

                    If .RecData.Tx0_DPD_L2_5th_sym_ph.High <= .MeasData.Tx0_DPD_L2_5th_sym_ph Then .RecData.Tx0_DPD_L2_5th_sym_ph.High = .MeasData.Tx0_DPD_L2_5th_sym_ph
                    If .RecData.Tx0_DPD_L2_5th_sym_ph.Low > .MeasData.Tx0_DPD_L2_5th_sym_ph Then .RecData.Tx0_DPD_L2_5th_sym_ph.Low = .MeasData.Tx0_DPD_L2_5th_sym_ph

                    If .RecData.Tx0_DPD_L3_5th_sym_am.High <= .MeasData.Tx0_DPD_L3_5th_sym_am Then .RecData.Tx0_DPD_L3_5th_sym_am.High = .MeasData.Tx0_DPD_L3_5th_sym_am
                    If .RecData.Tx0_DPD_L3_5th_sym_am.Low > .MeasData.Tx0_DPD_L3_5th_sym_am Then .RecData.Tx0_DPD_L3_5th_sym_am.Low = .MeasData.Tx0_DPD_L3_5th_sym_am

                    If .RecData.Tx0_DPD_L3_5th_sym_ph.High <= .MeasData.Tx0_DPD_L3_5th_sym_ph Then .RecData.Tx0_DPD_L3_5th_sym_ph.High = .MeasData.Tx0_DPD_L3_5th_sym_ph
                    If .RecData.Tx0_DPD_L3_5th_sym_ph.Low > .MeasData.Tx0_DPD_L3_5th_sym_ph Then .RecData.Tx0_DPD_L3_5th_sym_ph.Low = .MeasData.Tx0_DPD_L3_5th_sym_ph

                    If .RecData.Tx0_DPD_L2_3rd_asym_am.High <= .MeasData.Tx0_DPD_L2_3rd_asym_am Then .RecData.Tx0_DPD_L2_3rd_asym_am.High = .MeasData.Tx0_DPD_L2_3rd_asym_am
                    If .RecData.Tx0_DPD_L2_3rd_asym_am.Low > .MeasData.Tx0_DPD_L2_3rd_asym_am Then .RecData.Tx0_DPD_L2_3rd_asym_am.Low = .MeasData.Tx0_DPD_L2_3rd_asym_am

                    If .RecData.Tx0_DPD_L2_3rd_asym_ph.High <= .MeasData.Tx0_DPD_L2_3rd_asym_ph Then .RecData.Tx0_DPD_L2_3rd_asym_ph.High = .MeasData.Tx0_DPD_L2_3rd_asym_ph
                    If .RecData.Tx0_DPD_L2_3rd_asym_ph.Low > .MeasData.Tx0_DPD_L2_3rd_asym_ph Then .RecData.Tx0_DPD_L2_3rd_asym_ph.Low = .MeasData.Tx0_DPD_L2_3rd_asym_ph

                    If .RecData.Tx0_DPD_L2_5th_asym_am.High <= .MeasData.Tx0_DPD_L2_5th_asym_am Then .RecData.Tx0_DPD_L2_5th_asym_am.High = .MeasData.Tx0_DPD_L2_5th_asym_am
                    If .RecData.Tx0_DPD_L2_5th_asym_am.Low > .MeasData.Tx0_DPD_L2_5th_asym_am Then .RecData.Tx0_DPD_L2_5th_asym_am.Low = .MeasData.Tx0_DPD_L2_5th_asym_am

                    If .RecData.Tx0_DPD_L2_5th_asym_ph.High <= .MeasData.Tx0_DPD_L2_5th_asym_ph Then .RecData.Tx0_DPD_L2_5th_asym_ph.High = .MeasData.Tx0_DPD_L2_5th_asym_ph
                    If .RecData.Tx0_DPD_L2_5th_asym_ph.Low > .MeasData.Tx0_DPD_L2_5th_asym_ph Then .RecData.Tx0_DPD_L2_5th_asym_ph.Low = .MeasData.Tx0_DPD_L2_5th_asym_ph

                    'Tx1
                    If .RecData.TX1_Output_Pow.High <= .MeasData.TX1_Output_Pow Then .RecData.TX1_Output_Pow.High = .MeasData.TX1_Output_Pow
                    If .RecData.TX1_Output_Pow.Low > .MeasData.TX1_Output_Pow Then .RecData.TX1_Output_Pow.Low = .MeasData.TX1_Output_Pow

                    If .RecData.TX1_VSWR.High <= .MeasData.TX1_VSWR Then .RecData.TX1_VSWR.High = .MeasData.TX1_VSWR
                    If .RecData.TX1_VSWR.Low > .MeasData.TX1_VSWR Then .RecData.TX1_VSWR.Low = .MeasData.TX1_VSWR

                    If .RecData.TX1_Forward_Power_Detector.High <= .MeasData.TX1_Forward_Power_Detector Then .RecData.TX1_Forward_Power_Detector.High = .MeasData.TX1_Forward_Power_Detector
                    If .RecData.TX1_Forward_Power_Detector.Low > .MeasData.TX1_Forward_Power_Detector Then .RecData.TX1_Forward_Power_Detector.Low = .MeasData.TX1_Forward_Power_Detector

                    If .RecData.TX1_Reverse_Power_Detector.High <= .MeasData.TX1_Reverse_Power_Detector Then .RecData.TX1_Reverse_Power_Detector.High = .MeasData.TX1_Reverse_Power_Detector
                    If .RecData.TX1_Reverse_Power_Detector.Low > .MeasData.TX1_Reverse_Power_Detector Then .RecData.TX1_Reverse_Power_Detector.Low = .MeasData.TX1_Reverse_Power_Detector

                    If .RecData.TX1_Gain_VCA.High <= .MeasData.TX1_Gain_VCA Then .RecData.TX1_Gain_VCA.High = .MeasData.TX1_Gain_VCA
                    If .RecData.TX1_Gain_VCA.Low > .MeasData.TX1_Gain_VCA Then .RecData.TX1_Gain_VCA.Low = .MeasData.TX1_Gain_VCA

                    If .RecData.TX1_txDacGain.High <= .MeasData.TX1_txDacGain Then .RecData.TX1_txDacGain.High = .MeasData.TX1_txDacGain
                    If .RecData.TX1_txDacGain.Low > .MeasData.TX1_txDacGain Then .RecData.TX1_txDacGain.Low = .MeasData.TX1_txDacGain

                    If .RecData.TX1_totalTxAttn.High <= .MeasData.TX1_totalTxAttn Then .RecData.TX1_totalTxAttn.High = .MeasData.TX1_totalTxAttn
                    If .RecData.TX1_totalTxAttn.Low > .MeasData.TX1_totalTxAttn Then .RecData.TX1_totalTxAttn.Low = .MeasData.TX1_totalTxAttn

                    If .RecData.TX1_Gain_TxStep.High <= .MeasData.TX1_Gain_TxStep Then .RecData.TX1_Gain_TxStep.High = .MeasData.TX1_Gain_TxStep
                    If .RecData.TX1_Gain_TxStep.Low > .MeasData.TX1_Gain_TxStep Then .RecData.TX1_Gain_TxStep.Low = .MeasData.TX1_Gain_TxStep

                    If .RecData.TX1_Gain_FbStep.High <= .MeasData.TX1_Gain_FbStep Then .RecData.TX1_Gain_FbStep.High = .MeasData.TX1_Gain_FbStep
                    If .RecData.TX1_Gain_FbStep.Low > .MeasData.TX1_Gain_FbStep Then .RecData.TX1_Gain_FbStep.Low = .MeasData.TX1_Gain_FbStep

                    If .RecData.TX1_Gain_FbTxQuo.High <= .MeasData.TX1_Gain_FbTxQuo Then .RecData.TX1_Gain_FbTxQuo.High = .MeasData.TX1_Gain_FbTxQuo
                    If .RecData.TX1_Gain_FbTxQuo.Low > .MeasData.TX1_Gain_FbTxQuo Then .RecData.TX1_Gain_FbTxQuo.Low = .MeasData.TX1_Gain_FbTxQuo

                    If .RecData.TX1_Gain_GainError.High <= .MeasData.TX1_Gain_GainError Then .RecData.TX1_Gain_GainError.High = .MeasData.TX1_Gain_GainError
                    If .RecData.TX1_Gain_GainError.Low > .MeasData.TX1_Gain_GainError Then .RecData.TX1_Gain_GainError.Low = .MeasData.TX1_Gain_GainError

                    If .RecData.TX1_PA1_PsVolt.High <= .MeasData.TX1_PA1_PsVolt Then .RecData.TX1_PA1_PsVolt.High = .MeasData.TX1_PA1_PsVolt
                    If .RecData.TX1_PA1_PsVolt.Low > .MeasData.TX1_PA1_PsVolt Then .RecData.TX1_PA1_PsVolt.Low = .MeasData.TX1_PA1_PsVolt

                    If .RecData.TX1_PA1_Temp.High <= .MeasData.TX1_PA1_Temp Then .RecData.TX1_PA1_Temp.High = .MeasData.TX1_PA1_Temp
                    If .RecData.TX1_PA1_Temp.Low > .MeasData.TX1_PA1_Temp Then .RecData.TX1_PA1_Temp.Low = .MeasData.TX1_PA1_Temp

                    If .RecData.TX1_PA1_BiasTemp.High <= .MeasData.TX1_PA1_BiasTemp Then .RecData.TX1_PA1_BiasTemp.High = .MeasData.TX1_PA1_BiasTemp
                    If .RecData.TX1_PA1_BiasTemp.Low > .MeasData.TX1_PA1_BiasTemp Then .RecData.TX1_PA1_BiasTemp.Low = .MeasData.TX1_PA1_BiasTemp

                    If .RecData.TX1_PA1_Driver1Cur.High <= .MeasData.TX1_PA1_Driver1Cur Then .RecData.TX1_PA1_Driver1Cur.High = .MeasData.TX1_PA1_Driver1Cur
                    If .RecData.TX1_PA1_Driver1Cur.Low > .MeasData.TX1_PA1_Driver1Cur Then .RecData.TX1_PA1_Driver1Cur.Low = .MeasData.TX1_PA1_Driver1Cur

                    If .RecData.TX1_PA1_Driver2Cur.High <= .MeasData.TX1_PA1_Driver2Cur Then .RecData.TX1_PA1_Driver2Cur.High = .MeasData.TX1_PA1_Driver2Cur
                    If .RecData.TX1_PA1_Driver2Cur.Low > .MeasData.TX1_PA1_Driver2Cur Then .RecData.TX1_PA1_Driver2Cur.Low = .MeasData.TX1_PA1_Driver2Cur

                    If .RecData.TX1_PA1_Driver3Cur.High <= .MeasData.TX1_PA1_Driver3Cur Then .RecData.TX1_PA1_Driver3Cur.High = .MeasData.TX1_PA1_Driver3Cur
                    If .RecData.TX1_PA1_Driver3Cur.Low > .MeasData.TX1_PA1_Driver3Cur Then .RecData.TX1_PA1_Driver3Cur.Low = .MeasData.TX1_PA1_Driver3Cur

                    If .RecData.TX1_PA1_Driver4Cur.High <= .MeasData.TX1_PA1_Driver4Cur Then .RecData.TX1_PA1_Driver4Cur.High = .MeasData.TX1_PA1_Driver4Cur
                    If .RecData.TX1_PA1_Driver4Cur.Low > .MeasData.TX1_PA1_Driver4Cur Then .RecData.TX1_PA1_Driver4Cur.Low = .MeasData.TX1_PA1_Driver4Cur

                    If .RecData.TX1_PA1_Final1Cur.High <= .MeasData.TX1_PA1_Final1Cur Then .RecData.TX1_PA1_Final1Cur.High = .MeasData.TX1_PA1_Final1Cur
                    If .RecData.TX1_PA1_Final1Cur.Low > .MeasData.TX1_PA1_Final1Cur Then .RecData.TX1_PA1_Final1Cur.Low = .MeasData.TX1_PA1_Final1Cur

                    If .RecData.TX1_PA1_Final2Cur.High <= .MeasData.TX1_PA1_Final2Cur Then .RecData.TX1_PA1_Final2Cur.High = .MeasData.TX1_PA1_Final2Cur
                    If .RecData.TX1_PA1_Final2Cur.Low > .MeasData.TX1_PA1_Final2Cur Then .RecData.TX1_PA1_Final2Cur.Low = .MeasData.TX1_PA1_Final2Cur

                    If .RecData.TX1_PA1_Driver1Dac.High <= .MeasData.TX1_PA1_Driver1Dac Then .RecData.TX1_PA1_Driver1Dac.High = .MeasData.TX1_PA1_Driver1Dac
                    If .RecData.TX1_PA1_Driver1Dac.Low > .MeasData.TX1_PA1_Driver1Dac Then .RecData.TX1_PA1_Driver1Dac.Low = .MeasData.TX1_PA1_Driver1Dac

                    If .RecData.TX1_PA1_Driver2Dac.High <= .MeasData.TX1_PA1_Driver2Dac Then .RecData.TX1_PA1_Driver2Dac.High = .MeasData.TX1_PA1_Driver2Dac
                    If .RecData.TX1_PA1_Driver2Dac.Low > .MeasData.TX1_PA1_Driver2Dac Then .RecData.TX1_PA1_Driver2Dac.Low = .MeasData.TX1_PA1_Driver2Dac

                    If .RecData.TX1_PA1_Driver3Dac.High <= .MeasData.TX1_PA1_Driver3Dac Then .RecData.TX1_PA1_Driver3Dac.High = .MeasData.TX1_PA1_Driver3Dac
                    If .RecData.TX1_PA1_Driver3Dac.Low > .MeasData.TX1_PA1_Driver3Dac Then .RecData.TX1_PA1_Driver3Dac.Low = .MeasData.TX1_PA1_Driver3Dac

                    If .RecData.TX1_PA1_Driver4Dac.High <= .MeasData.TX1_PA1_Driver4Dac Then .RecData.TX1_PA1_Driver4Dac.High = .MeasData.TX1_PA1_Driver4Dac
                    If .RecData.TX1_PA1_Driver4Dac.Low > .MeasData.TX1_PA1_Driver4Dac Then .RecData.TX1_PA1_Driver4Dac.Low = .MeasData.TX1_PA1_Driver4Dac

                    If .RecData.TX1_PA1_Final1Dac.High <= .MeasData.TX1_PA1_Final1Dac Then .RecData.TX1_PA1_Final1Dac.High = .MeasData.TX1_PA1_Final1Dac
                    If .RecData.TX1_PA1_Final1Dac.Low > .MeasData.TX1_PA1_Final1Dac Then .RecData.TX1_PA1_Final1Dac.Low = .MeasData.TX1_PA1_Final1Dac

                    If .RecData.TX1_PA1_Final2Dac.High <= .MeasData.TX1_PA1_Final2Dac Then .RecData.TX1_PA1_Final2Dac.High = .MeasData.TX1_PA1_Final2Dac
                    If .RecData.TX1_PA1_Final2Dac.Low > .MeasData.TX1_PA1_Final2Dac Then .RecData.TX1_PA1_Final2Dac.Low = .MeasData.TX1_PA1_Final2Dac

                    If .RecData.TX1_PA1_ampDelayInt.High <= .MeasData.TX1_PA1_ampDelayInt Then .RecData.TX1_PA1_ampDelayInt.High = .MeasData.TX1_PA1_ampDelayInt
                    If .RecData.TX1_PA1_ampDelayInt.Low > .MeasData.TX1_PA1_ampDelayInt Then .RecData.TX1_PA1_ampDelayInt.Low = .MeasData.TX1_PA1_ampDelayInt

                    If .RecData.TX1_PA1_ampDelayFrac.High <= .MeasData.TX1_PA1_ampDelayFrac Then .RecData.TX1_PA1_ampDelayFrac.High = .MeasData.TX1_PA1_ampDelayFrac
                    If .RecData.TX1_PA1_ampDelayFrac.Low > .MeasData.TX1_PA1_ampDelayFrac Then .RecData.TX1_PA1_ampDelayFrac.Low = .MeasData.TX1_PA1_ampDelayFrac

                    If .RecData.TX1_PA1_MaxCrossCorrelation.High <= .MeasData.TX1_PA1_MaxCrossCorrelation Then .RecData.TX1_PA1_MaxCrossCorrelation.High = .MeasData.TX1_PA1_MaxCrossCorrelation
                    If .RecData.TX1_PA1_MaxCrossCorrelation.Low > .MeasData.TX1_PA1_MaxCrossCorrelation Then .RecData.TX1_PA1_MaxCrossCorrelation.Low = .MeasData.TX1_PA1_MaxCrossCorrelation

                    If .RecData.TX1_DPD_L1_Table_Max_Gain.High <= .MeasData.Tx1_DPD_L1_Table_Max_Gain Then .RecData.TX1_DPD_L1_Table_Max_Gain.High = .MeasData.Tx1_DPD_L1_Table_Max_Gain
                    If .RecData.TX1_DPD_L1_Table_Max_Gain.Low > .MeasData.Tx1_DPD_L1_Table_Max_Gain Then .RecData.TX1_DPD_L1_Table_Max_Gain.Low = .MeasData.Tx1_DPD_L1_Table_Max_Gain

                    If .RecData.TX1_DPD_L1_Table_Min_Gain.High <= .MeasData.Tx1_DPD_L1_Table_Min_Gain Then .RecData.TX1_DPD_L1_Table_Min_Gain.High = .MeasData.Tx1_DPD_L1_Table_Min_Gain
                    If .RecData.TX1_DPD_L1_Table_Min_Gain.Low > .MeasData.Tx1_DPD_L1_Table_Min_Gain Then .RecData.TX1_DPD_L1_Table_Min_Gain.Low = .MeasData.Tx1_DPD_L1_Table_Min_Gain

                    If .RecData.Tx1_DPD_L2_3rd_sym_am.High <= .MeasData.Tx1_DPD_L2_3rd_sym_am Then .RecData.Tx1_DPD_L2_3rd_sym_am.High = .MeasData.Tx1_DPD_L2_3rd_sym_am
                    If .RecData.Tx1_DPD_L2_3rd_sym_am.Low > .MeasData.Tx1_DPD_L2_3rd_sym_am Then .RecData.Tx1_DPD_L2_3rd_sym_am.Low = .MeasData.Tx1_DPD_L2_3rd_sym_am

                    If .RecData.Tx1_DPD_L2_3rd_sym_ph.High <= .MeasData.Tx1_DPD_L2_3rd_sym_ph Then .RecData.Tx1_DPD_L2_3rd_sym_ph.High = .MeasData.Tx1_DPD_L2_3rd_sym_ph
                    If .RecData.Tx1_DPD_L2_3rd_sym_ph.Low > .MeasData.Tx1_DPD_L2_3rd_sym_ph Then .RecData.Tx1_DPD_L2_3rd_sym_ph.Low = .MeasData.Tx1_DPD_L2_3rd_sym_ph

                    If .RecData.Tx1_DPD_L3_3rd_sym_am.High <= .MeasData.Tx1_DPD_L3_3rd_sym_am Then .RecData.Tx1_DPD_L3_3rd_sym_am.High = .MeasData.Tx1_DPD_L3_3rd_sym_am
                    If .RecData.Tx1_DPD_L3_3rd_sym_am.Low > .MeasData.Tx1_DPD_L3_3rd_sym_am Then .RecData.Tx1_DPD_L3_3rd_sym_am.Low = .MeasData.Tx1_DPD_L3_3rd_sym_am

                    If .RecData.Tx1_DPD_L3_3rd_sym_ph.High <= .MeasData.Tx1_DPD_L3_3rd_sym_ph Then .RecData.Tx1_DPD_L3_3rd_sym_ph.High = .MeasData.Tx1_DPD_L3_3rd_sym_ph
                    If .RecData.Tx1_DPD_L3_3rd_sym_ph.Low > .MeasData.Tx1_DPD_L3_3rd_sym_ph Then .RecData.Tx1_DPD_L3_3rd_sym_ph.Low = .MeasData.Tx1_DPD_L3_3rd_sym_ph

                    If .RecData.Tx1_DPD_L2_5th_sym_am.High <= .MeasData.Tx1_DPD_L2_5th_sym_am Then .RecData.Tx1_DPD_L2_5th_sym_am.High = .MeasData.Tx1_DPD_L2_5th_sym_am
                    If .RecData.Tx1_DPD_L2_5th_sym_am.Low > .MeasData.Tx1_DPD_L2_5th_sym_am Then .RecData.Tx1_DPD_L2_5th_sym_am.Low = .MeasData.Tx1_DPD_L2_5th_sym_am

                    If .RecData.Tx1_DPD_L2_5th_sym_ph.High <= .MeasData.Tx1_DPD_L2_5th_sym_ph Then .RecData.Tx1_DPD_L2_5th_sym_ph.High = .MeasData.Tx1_DPD_L2_5th_sym_ph
                    If .RecData.Tx1_DPD_L2_5th_sym_ph.Low > .MeasData.Tx1_DPD_L2_5th_sym_ph Then .RecData.Tx1_DPD_L2_5th_sym_ph.Low = .MeasData.Tx1_DPD_L2_5th_sym_ph

                    If .RecData.Tx1_DPD_L3_5th_sym_am.High <= .MeasData.Tx1_DPD_L3_5th_sym_am Then .RecData.Tx1_DPD_L3_5th_sym_am.High = .MeasData.Tx1_DPD_L3_5th_sym_am
                    If .RecData.Tx1_DPD_L3_5th_sym_am.Low > .MeasData.Tx1_DPD_L3_5th_sym_am Then .RecData.Tx1_DPD_L3_5th_sym_am.Low = .MeasData.Tx1_DPD_L3_5th_sym_am

                    If .RecData.Tx1_DPD_L3_5th_sym_ph.High <= .MeasData.Tx1_DPD_L3_5th_sym_ph Then .RecData.Tx1_DPD_L3_5th_sym_ph.High = .MeasData.Tx1_DPD_L3_5th_sym_ph
                    If .RecData.Tx1_DPD_L3_5th_sym_ph.Low > .MeasData.Tx1_DPD_L3_5th_sym_ph Then .RecData.Tx1_DPD_L3_5th_sym_ph.Low = .MeasData.Tx1_DPD_L3_5th_sym_ph

                    If .RecData.Tx1_DPD_L2_3rd_asym_am.High <= .MeasData.Tx1_DPD_L2_3rd_asym_am Then .RecData.Tx1_DPD_L2_3rd_asym_am.High = .MeasData.Tx1_DPD_L2_3rd_asym_am
                    If .RecData.Tx1_DPD_L2_3rd_asym_am.Low > .MeasData.Tx1_DPD_L2_3rd_asym_am Then .RecData.Tx1_DPD_L2_3rd_asym_am.Low = .MeasData.Tx1_DPD_L2_3rd_asym_am

                    If .RecData.Tx1_DPD_L2_3rd_asym_ph.High <= .MeasData.Tx1_DPD_L2_3rd_asym_ph Then .RecData.Tx1_DPD_L2_3rd_asym_ph.High = .MeasData.Tx1_DPD_L2_3rd_asym_ph
                    If .RecData.Tx1_DPD_L2_3rd_asym_ph.Low > .MeasData.Tx1_DPD_L2_3rd_asym_ph Then .RecData.Tx1_DPD_L2_3rd_asym_ph.Low = .MeasData.Tx1_DPD_L2_3rd_asym_ph

                    If .RecData.Tx1_DPD_L2_5th_asym_am.High <= .MeasData.Tx1_DPD_L2_5th_asym_am Then .RecData.Tx1_DPD_L2_5th_asym_am.High = .MeasData.Tx1_DPD_L2_5th_asym_am
                    If .RecData.Tx1_DPD_L2_5th_asym_am.Low > .MeasData.Tx1_DPD_L2_5th_asym_am Then .RecData.Tx1_DPD_L2_5th_asym_am.Low = .MeasData.Tx1_DPD_L2_5th_asym_am

                    If .RecData.Tx1_DPD_L2_5th_asym_ph.High <= .MeasData.Tx1_DPD_L2_5th_asym_ph Then .RecData.Tx1_DPD_L2_5th_asym_ph.High = .MeasData.Tx1_DPD_L2_5th_asym_ph
                    If .RecData.Tx1_DPD_L2_5th_asym_ph.Low > .MeasData.Tx1_DPD_L2_5th_asym_ph Then .RecData.Tx1_DPD_L2_5th_asym_ph.Low = .MeasData.Tx1_DPD_L2_5th_asym_ph
                End If

                'V1.1.6
                'RX0
                If .RecData.Rx0_LNA_Atten.High <= .MeasData.Rx0_LNA_Atten Then .RecData.Rx0_LNA_Atten.High = .MeasData.Rx0_LNA_Atten
                If .RecData.Rx0_LNA_Atten.Low > .MeasData.Rx0_LNA_Atten Then .RecData.Rx0_LNA_Atten.Low = .MeasData.Rx0_LNA_Atten

                If .RecData.Rx0_Rx_Atten0.High <= .MeasData.Rx0_Rx_Atten0 Then .RecData.Rx0_Rx_Atten0.High = .MeasData.Rx0_Rx_Atten0
                If .RecData.Rx0_Rx_Atten0.Low > .MeasData.Rx0_Rx_Atten0 Then .RecData.Rx0_Rx_Atten0.Low = .MeasData.Rx0_Rx_Atten0

                If .RecData.Rx0_Rx_Atten1.High <= .MeasData.Rx0_Rx_Atten1 Then .RecData.Rx0_Rx_Atten1.High = .MeasData.Rx0_Rx_Atten1
                If .RecData.Rx0_Rx_Atten1.Low > .MeasData.Rx0_Rx_Atten1 Then .RecData.Rx0_Rx_Atten1.Low = .MeasData.Rx0_Rx_Atten1

                If .RecData.Rx0_RSSI_C0.High <= .MeasData.Rx0_RSSI_C0 Then .RecData.Rx0_RSSI_C0.High = .MeasData.Rx0_RSSI_C0
                If .RecData.Rx0_RSSI_C0.Low > .MeasData.Rx0_RSSI_C0 Then .RecData.Rx0_RSSI_C0.Low = .MeasData.Rx0_RSSI_C0

                If .RecData.Rx0_RSSI_C1.High <= .MeasData.Rx0_RSSI_C1 Then .RecData.Rx0_RSSI_C1.High = .MeasData.Rx0_RSSI_C1
                If .RecData.Rx0_RSSI_C1.Low > .MeasData.Rx0_RSSI_C1 Then .RecData.Rx0_RSSI_C1.Low = .MeasData.Rx0_RSSI_C1

                'Rx1
                If .RecData.Rx1_LNA_Atten.High <= .MeasData.Rx1_LNA_Atten Then .RecData.Rx1_LNA_Atten.High = .MeasData.Rx1_LNA_Atten
                If .RecData.Rx1_LNA_Atten.Low > .MeasData.Rx1_LNA_Atten Then .RecData.Rx1_LNA_Atten.Low = .MeasData.Rx1_LNA_Atten

                If .RecData.Rx1_Rx_Atten0.High <= .MeasData.Rx1_Rx_Atten0 Then .RecData.Rx1_Rx_Atten0.High = .MeasData.Rx1_Rx_Atten0
                If .RecData.Rx1_Rx_Atten0.Low > .MeasData.Rx1_Rx_Atten0 Then .RecData.Rx1_Rx_Atten0.Low = .MeasData.Rx1_Rx_Atten0

                If .RecData.Rx1_Rx_Atten1.High <= .MeasData.Rx1_Rx_Atten1 Then .RecData.Rx1_Rx_Atten1.High = .MeasData.Rx1_Rx_Atten1
                If .RecData.Rx1_Rx_Atten1.Low > .MeasData.Rx1_Rx_Atten1 Then .RecData.Rx1_Rx_Atten1.Low = .MeasData.Rx1_Rx_Atten1

                If .RecData.Rx1_RSSI_C0.High <= .MeasData.Rx1_RSSI_C0 Then .RecData.Rx1_RSSI_C0.High = .MeasData.Rx1_RSSI_C0
                If .RecData.Rx1_RSSI_C0.Low > .MeasData.Rx1_RSSI_C0 Then .RecData.Rx1_RSSI_C0.Low = .MeasData.Rx1_RSSI_C0

                If .RecData.Rx1_RSSI_C1.High <= .MeasData.Rx1_RSSI_C1 Then .RecData.Rx1_RSSI_C1.High = .MeasData.Rx1_RSSI_C1
                If .RecData.Rx1_RSSI_C1.Low > .MeasData.Rx1_RSSI_C1 Then .RecData.Rx1_RSSI_C1.Low = .MeasData.Rx1_RSSI_C1

                'Rx2
                If .RecData.Rx2_LNA_Atten.High <= .MeasData.Rx2_LNA_Atten Then .RecData.Rx2_LNA_Atten.High = .MeasData.Rx2_LNA_Atten
                If .RecData.Rx2_LNA_Atten.Low > .MeasData.Rx2_LNA_Atten Then .RecData.Rx2_LNA_Atten.Low = .MeasData.Rx2_LNA_Atten

                If .RecData.Rx2_Rx_Atten0.High <= .MeasData.Rx2_Rx_Atten0 Then .RecData.Rx2_Rx_Atten0.High = .MeasData.Rx2_Rx_Atten0
                If .RecData.Rx2_Rx_Atten0.Low > .MeasData.Rx2_Rx_Atten0 Then .RecData.Rx2_Rx_Atten0.Low = .MeasData.Rx2_Rx_Atten0

                If .RecData.Rx2_Rx_Atten1.High <= .MeasData.Rx2_Rx_Atten1 Then .RecData.Rx2_Rx_Atten1.High = .MeasData.Rx2_Rx_Atten1
                If .RecData.Rx2_Rx_Atten1.Low > .MeasData.Rx2_Rx_Atten1 Then .RecData.Rx2_Rx_Atten1.Low = .MeasData.Rx2_Rx_Atten1

                If .RecData.Rx2_RSSI_C0.High <= .MeasData.Rx2_RSSI_C0 Then .RecData.Rx2_RSSI_C0.High = .MeasData.Rx2_RSSI_C0
                If .RecData.Rx2_RSSI_C0.Low > .MeasData.Rx2_RSSI_C0 Then .RecData.Rx2_RSSI_C0.Low = .MeasData.Rx2_RSSI_C0

                If .RecData.Rx2_RSSI_C1.High <= .MeasData.Rx2_RSSI_C1 Then .RecData.Rx2_RSSI_C1.High = .MeasData.Rx2_RSSI_C1
                If .RecData.Rx2_RSSI_C1.Low > .MeasData.Rx2_RSSI_C1 Then .RecData.Rx2_RSSI_C1.Low = .MeasData.Rx2_RSSI_C1

                'Rx3
                If .RecData.Rx3_LNA_Atten.High <= .MeasData.Rx3_LNA_Atten Then .RecData.Rx3_LNA_Atten.High = .MeasData.Rx3_LNA_Atten
                If .RecData.Rx3_LNA_Atten.Low > .MeasData.Rx3_LNA_Atten Then .RecData.Rx3_LNA_Atten.Low = .MeasData.Rx3_LNA_Atten

                If .RecData.Rx3_Rx_Atten0.High <= .MeasData.Rx3_Rx_Atten0 Then .RecData.Rx3_Rx_Atten0.High = .MeasData.Rx3_Rx_Atten0
                If .RecData.Rx3_Rx_Atten0.Low > .MeasData.Rx3_Rx_Atten0 Then .RecData.Rx3_Rx_Atten0.Low = .MeasData.Rx3_Rx_Atten0

                If .RecData.Rx3_Rx_Atten1.High <= .MeasData.Rx3_Rx_Atten1 Then .RecData.Rx3_Rx_Atten1.High = .MeasData.Rx3_Rx_Atten1
                If .RecData.Rx3_Rx_Atten1.Low > .MeasData.Rx3_Rx_Atten1 Then .RecData.Rx3_Rx_Atten1.Low = .MeasData.Rx3_Rx_Atten1

                If .RecData.Rx3_RSSI_C0.High <= .MeasData.Rx3_RSSI_C0 Then .RecData.Rx3_RSSI_C0.High = .MeasData.Rx3_RSSI_C0
                If .RecData.Rx3_RSSI_C0.Low > .MeasData.Rx3_RSSI_C0 Then .RecData.Rx3_RSSI_C0.Low = .MeasData.Rx3_RSSI_C0

                If .RecData.Rx3_RSSI_C1.High <= .MeasData.Rx3_RSSI_C1 Then .RecData.Rx3_RSSI_C1.High = .MeasData.Rx3_RSSI_C1
                If .RecData.Rx3_RSSI_C1.Low > .MeasData.Rx3_RSSI_C1 Then .RecData.Rx3_RSSI_C1.Low = .MeasData.Rx3_RSSI_C1

                'PS
                If .RecData.Input_Voltage.High <= .MeasData.Input_Voltage Then .RecData.Input_Voltage.High = .MeasData.Input_Voltage
                If .RecData.Input_Voltage.Low > .MeasData.Input_Voltage Then .RecData.Input_Voltage.Low = .MeasData.Input_Voltage

                'V1.1.6
                If .MeasData.PAEnabled Then
                    If .RecData.Input_Current_HP.High <= .MeasData.Input_Current Then .RecData.Input_Current_HP.High = .MeasData.Input_Current
                    If .RecData.Input_Current_HP.Low > .MeasData.Input_Current Then .RecData.Input_Current_HP.Low = .MeasData.Input_Current

                    If .RecData.Input_Power_HP.High <= .MeasData.Input_Power Then .RecData.Input_Power_HP.High = .MeasData.Input_Power
                    If .RecData.Input_Power_HP.Low > .MeasData.Input_Power Then .RecData.Input_Power_HP.Low = .MeasData.Input_Power
                End If

                'V1.1.6
                If Not .MeasData.PAEnabled Then
                    If .RecData.Input_Current_LP.High <= .MeasData.Input_Current Then .RecData.Input_Current_LP.High = .MeasData.Input_Current
                    If .RecData.Input_Current_LP.Low > .MeasData.Input_Current Then .RecData.Input_Current_LP.Low = .MeasData.Input_Current

                    If .RecData.Input_Power_LP.High <= .MeasData.Input_Power Then .RecData.Input_Power_LP.High = .MeasData.Input_Power
                    If .RecData.Input_Power_LP.Low > .MeasData.Input_Power Then .RecData.Input_Power_LP.Low = .MeasData.Input_Power
                End If

                If .RecData.Output_Voltage.High <= .MeasData.Output_Voltage Then .RecData.Output_Voltage.High = .MeasData.Output_Voltage
                If .RecData.Output_Voltage.Low > .MeasData.Output_Voltage Then .RecData.Output_Voltage.Low = .MeasData.Output_Voltage

                If .RecData.AISG_12V_Voltage.High <= .MeasData.AISG_12V_Voltage Then .RecData.AISG_12V_Voltage.High = .MeasData.AISG_12V_Voltage
                If .RecData.AISG_12V_Voltage.Low > .MeasData.AISG_12V_Voltage Then .RecData.AISG_12V_Voltage.Low = .MeasData.AISG_12V_Voltage

                If .RecData.AISG_12V_Current.High <= .MeasData.AISG_12V_Current Then .RecData.AISG_12V_Current.High = .MeasData.AISG_12V_Current
                If .RecData.AISG_12V_Current.Low > .MeasData.AISG_12V_Current Then .RecData.AISG_12V_Current.Low = .MeasData.AISG_12V_Current

                If .RecData.AISG_24V_Voltage.High <= .MeasData.AISG_24V_Voltage Then .RecData.AISG_24V_Voltage.High = .MeasData.AISG_24V_Voltage
                If .RecData.AISG_24V_Voltage.Low > .MeasData.AISG_24V_Voltage Then .RecData.AISG_24V_Voltage.Low = .MeasData.AISG_24V_Voltage

                If .RecData.AISG_24V_Current.High <= .MeasData.AISG_24V_Current Then .RecData.AISG_24V_Current.High = .MeasData.AISG_24V_Current
                If .RecData.AISG_24V_Current.Low > .MeasData.AISG_24V_Current Then .RecData.AISG_24V_Current.Low = .MeasData.AISG_24V_Current

                If InStr(.RecData.AlarmString, "No Alarm") > 0 Then .RecData.AlarmString = .MeasData.AlarmString

                .RecData.FPGARevision = .MeasData.FPGARevision
                .RecData.DSPRevision = .MeasData.DSPRevision
                .RecData.SWRevision = .MeasData.SWRevision

                .RecData.PowerCycleCount = .MeasData.PowerCycleCount
            End With
            DetectFailures(slot)
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub
End Module

Module modTest

    'Public Delegate Sub MeasurementStepDelegate(ByVal slot As Integer, ByVal ePhase As String)
    'Public MeasurementStep As New MeasurementStepDelegate(AddressOf MeasurementStepThread)
    'Public Delegate Sub AddMessageDelegate(ByVal message As String)
    'Public addmessagethread As New MeasurementStepDelegate(AddressOf AddMessage)

    Public Sub ReadActiveUnits()

        For slot As Integer = 0 To SlotNum
            KanbanRealTimeUpload(slot, "Loading")
        Next

        TurnAllPSOnOff(True)

        If UCase(BI_HW.ConnectionType) = "ROUTER" Then
            For i As Integer = 0 To SlotNum
                TurnPowerOnOff(i, True)
                TurnFanOnOff(i, True)
            Next
            ManualTurnPowerOnOff(True)
            Delay(70000)
        End If

        For slot As Integer = 0 To SlotNum
            Try
                If AbortTest Then Exit Try

                If UCase(BI_HW.ConnectionType) = "SWITCH" Then
                    TurnPowerOnOff(slot, True)
                    TurnFanOnOff(slot, True)
                    DisplayReadUnit(slot)

                    Delay(20000)
                End If

                DisplayReadUnit(slot)

                If UCase(BI_HW.ConnectionType) = "SWITCH" Then
                    Try
                        Transceiver(slot).Address = "192.168.255.1"
                        Transceiver(slot).Open()
                        Transceiver(slot).Close_Timer()
                    Catch ex As Exception
                        Transceiver(slot).Address = BI_HW.IPAddress(slot)
                        Transceiver(slot).Open()
                        Transceiver(slot).Close_Timer()
                    End Try
                Else
                    Transceiver(slot).Address = BI_HW.IPAddress(slot)
                    'Transceiver(slot).Address = "192.168.255.1"
                    Transceiver(slot).Open()
                    Transceiver(slot).Close_Timer()
                End If

                My.Application.DoEvents()

                BI_Data(slot).UnitInfo.UnitActive = True
                Dim tmpTime As String = VB6.Format(Now, "yyyyMMddHHmmss")
                'BI_Data(slot).UnitInfo.UnitSN = Transceiver(slot).Inventory.Serial
                BI_Data(slot).UnitInfo.UnitSN = Transceiver(slot).CatSN   'V1.1.4
                'BI_Data(slot).UnitInfo.UnitSN = "111111"
                BI_Data(slot).UnitInfo.FileName = BI_Data(slot).UnitInfo.UnitSN & "_" & tmpTime

                '*********************
                'Dim tmpHWREF As String = Transceiver(slot).Inventory.FunctionCode
                'Serial: 7890123456789
                'FunctionCode: R2x50-80L1
                'KsNumber: KS24829L1
                'KsVersion: 1.1
                'ComCode: 849144415
                'CLEI: WDMAU00ARA
                'ECI: 462628
                'Misc: CT P0.0
                'GDF: AND001
                'Select Case UCase(Trim(tmpHWREF))
                '    Case "R2X50-80L1"
                BI_Data(slot).UnitInfo.UnitType = "Viper"
                'BI_Data(slot).UnitInfo.JPGFilePath = BI_Param.Matlab_Picture_Save_Path & "Viper_1900\"
                BI_Data(slot).UnitInfo.CSVFilePath = BI_Param.File_Save_Path & "Viper_1900\"
                With My.Computer.FileSystem
                    If Not .DirectoryExists(BI_Data(slot).UnitInfo.CSVFilePath) Then .CreateDirectory(BI_Data(slot).UnitInfo.CSVFilePath)
                End With
                BI_Data(slot).UnitInfo.CSVFileName = BI_Data(slot).UnitInfo.CSVFilePath & BI_Data(slot).UnitInfo.FileName & ".csv"
                BI_Data(slot).UnitInfo.DATFilePath = BI_Param.File_Save_Path & "Viper_1900\" & BI_Data(slot).UnitInfo.FileName & ".dat"
                BI_Data(slot).UnitInfo.TXTFilePath = BI_Param.File_Save_Path & "Viper_1900\" & BI_Data(slot).UnitInfo.UnitSN & "-" & tmpTime & ".txt"
                '    Case Else
                'Throw New Exception("Wrong product type!")
                'End Select
                '*********************

                If UCase(BI_HW.ConnectionType) = "SWITCH" And Not (Transceiver(slot).Address = BI_HW.IPAddress(slot)) Then
                    AddMessage("Change Slot " & (slot + 1).ToString & " IP address to " & BI_HW.IPAddress(slot) & " ...")
                    If Transceiver(slot).ChangeIPAddress(BI_HW.IPAddress(slot)) Then
                        AddMessage("Change Slot " & (slot + 1).ToString & " IP address to " & BI_HW.IPAddress(slot) & " successfully.")
                    Else
                        AddMessage("Change Slot " & (slot + 1).ToString & " IP address to " & BI_HW.IPAddress(slot) & " failed.")
                    End If
                End If

                Transceiver(slot).Close()
                If UCase(BI_HW.ConnectionType) = "SWITCH" Then TurnPowerOnOff(slot, False)
                DisplayUnitActive(slot)

            Catch ex As Exception
                AddMessage("Slot " & (slot + 1).ToString & " is blank ...")
                If UCase(BI_HW.ConnectionType) = "SWITCH" Then TurnPowerOnOff(slot, False)
                TurnFanOnOff(slot, False)
                BI_Data(slot).UnitInfo.UnitActive = False
                DisplayUnitActive(slot)
            End Try
        Next

        For slot As Integer = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                KanbanRealTimeUpload(slot, "Loading")
            Else
                KanbanRealTimeUpload(slot, "Blank")
            End If
        Next

        If UCase(BI_HW.ConnectionType) = "ROUTER" Then
            For i As Integer = 0 To SlotNum
                'TurnPowerOnOff(i, False)
                TurnFanOnOff(i, False)
            Next
        End If
    End Sub

    Public Function CheckStartTemp() As Boolean
        For slot As Integer = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                Try
                    Transceiver(slot).Open()
                    Transceiver(slot).Close_Timer()
                    Dim tTemp As Viper_Transceiver.TempSensorData = Transceiver(slot).TempSensors
                    Dim TempArr(11) As Double
                    Dim tHighTemp As Double
                    TempArr(0) = tTemp.PA0
                    TempArr(1) = tTemp.PA0VSWR
                    TempArr(2) = tTemp.PA1
                    TempArr(3) = tTemp.PA1VSWR
                    TempArr(4) = tTemp.LNA0
                    TempArr(5) = tTemp.LNA1
                    TempArr(6) = tTemp.LNA2
                    TempArr(7) = tTemp.LNA3
                    TempArr(8) = tTemp.PS_CONVERTER
                    TempArr(9) = tTemp.PS_BRICK
                    TempArr(10) = tTemp.FB
                    TempArr(11) = tTemp.RX
                    tHighTemp = 0
                    For t As Integer = 0 To 11
                        If tHighTemp < TempArr(t) Then tHighTemp = TempArr(t)
                    Next
                    BI_Data(slot).UnitInfo.StartTemperature = tHighTemp
                    'Transceiver(slot).Close()
                Catch ex As Exception
                    TurnPowerOnOff(slot, False)
                    TurnFanOnOff(slot, False)
                    BI_Data(slot).UnitInfo.UnitActive = False
                    DisplayUnitActive(slot)

                    KanbanRealTimeUpload(slot, "Lost")
                End Try
            End If
        Next

        For slot As Integer = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                If BI_Data(slot).UnitInfo.StartTemperature > BI_Profile.BI_Start_Temp Then
                    Return False
                    Exit Function
                End If
            End If
        Next
        Return True
    End Function

    Public Function CheckStatus(ByRef VSWR(,) As Double) As Boolean
        ReDim VSWR(SlotNum, 1)

        For slot As Integer = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then TurnPowerOnOff(slot, True)
        Next
        AddMessage("Waiting for units starting up...")
        Delay(20000)

        For slot As Integer = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                Try
                    AddMessage("Turn On RF: " & BI_Data(slot).UnitInfo.UnitSN)
                    'TurnPowerOnOff(slot, True)
                    Transceiver(slot).Address = BI_HW.IPAddress(slot)
                    Transceiver(slot).Open()
                    Transceiver(slot).Close_Timer()
                    'RemoveAlarm4(slot)
                    Transceiver(slot).InitBurnInCommands()
                    Dim tRetry As Integer = 0
                    Do Until Transceiver(slot).CheckSlewRate Or tRetry = 3
                        AddMessage("Check Slew Rate Byte Failed.")
                        Transceiver(slot).WriteSlewRate()
                        tRetry = tRetry + 1
                    Loop
                    TurnRFOnOff(slot, True)
                    BI_Data(slot).MeasData.PAEnabled = True
                    BI_Data(slot).RecData.SerialNumber = BI_Data(slot).UnitInfo.UnitSN

                    Delay(1000)
                    VSWR(slot, 0) = Transceiver(slot).VSWR(0)
                    VSWR(slot, 1) = Transceiver(slot).VSWR(1)

                    Delay(1000)
                    AddMessage("Turn Off RF: " & BI_Data(slot).UnitInfo.UnitSN)
                    TurnRFOnOff(slot, False)
                    'TurnPowerOnOff(slot, False)

                Catch ex As Exception
                    Try
                        AddMessage("Turn On RF: " & BI_Data(slot).UnitInfo.UnitSN)
                        'TurnPowerOnOff(slot, True)
                        Transceiver(slot).Address = BI_HW.IPAddress(slot)
                        Transceiver(slot).Open()
                        Transceiver(slot).Close_Timer()
                        'RemoveAlarm4(slot)
                        Transceiver(slot).InitBurnInCommands()
                        Dim tRetry As Integer = 0
                        Do Until Transceiver(slot).CheckSlewRate Or tRetry = 3
                            AddMessage("Check Slew Rate Byte Failed.")
                            Transceiver(slot).WriteSlewRate()
                            tRetry = tRetry + 1
                        Loop
                        TurnRFOnOff(slot, True)
                        BI_Data(slot).MeasData.PAEnabled = True
                        BI_Data(slot).RecData.SerialNumber = BI_Data(slot).UnitInfo.UnitSN

                        Delay(1000)
                        VSWR(slot, 0) = Transceiver(slot).VSWR(0)
                        VSWR(slot, 1) = Transceiver(slot).VSWR(1)

                        Delay(1000)
                        AddMessage("Turn Off RF: " & BI_Data(slot).UnitInfo.UnitSN)
                        TurnRFOnOff(slot, False)
                        'TurnPowerOnOff(slot, False)
                    Catch ex1 As Exception
                        AddMessage("Unit " & BI_Data(slot).UnitInfo.UnitSN & " Check start status error: " & ex.Message)
                        If UCase(BI_HW.ConnectionType) = "SWITCH" Then TurnPowerOnOff(slot, False)
                        TurnFanOnOff(slot, False)
                        BI_Data(slot).UnitInfo.UnitActive = False
                        DisplayUnitActive(slot)
                    End Try
                End Try
            End If
        Next

        For slot As Integer = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                If VSWR(slot, 0) < BI_Limit.BI_Pre_VSWR Or VSWR(slot, 1) < BI_Limit.BI_Pre_VSWR Then
                    Return False
                    Exit Function
                End If
            End If
        Next

        Return True
    End Function

    Public Function PreBurnIn() As Boolean
        Dim Unitcount As Integer = 0
        Dim slot As Integer

        'Re-start units after CheckStatus
        If UCase(BI_HW.ConnectionType) = "SWITCH" Then
            For slot = 0 To SlotNum
                If BI_Data(slot).UnitInfo.UnitActive Then
                    Transceiver(slot).Close()
                    TurnPowerOnOff(slot, False)
                    DisplayUnitActive(slot)
                End If
            Next
            Delay(3000)

            For slot = 0 To SlotNum
                If BI_Data(slot).UnitInfo.UnitActive Then TurnPowerOnOff(slot, True)
            Next
            AddMessage("Waiting for units starting up...")
            Delay(70000)
        End If

        If UCase(BI_HW.ConnectionType) = "ROUTER" Then
            For i As Integer = 0 To SlotNum
                'TurnPowerOnOff(i, True)
                TurnFanOnOff(i, True)
            Next
            'AddMessage("Waiting for units starting up...")
            'Delay(20000)
        End If

        For slot = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                Try
                    AddMessage("Initializing unit: " & BI_Data(slot).UnitInfo.UnitSN)
                    Transceiver(slot).Address = BI_HW.IPAddress(slot)
                    'Transceiver(slot).Address = "192.168.255.1"
                    Transceiver(slot).Open()
                    Transceiver(slot).Close_Timer()
                    Transceiver(slot).InitBurnInCommands()
                    Dim tRetry As Integer = 0
                    Do Until Transceiver(slot).CheckSlewRate Or tRetry = 3
                        AddMessage("Check Slew Rate Byte Failed.")
                        Transceiver(slot).WriteSlewRate()
                        tRetry = tRetry + 1
                    Loop
                    BI_Data(slot).MeasData.PAEnabled = True
                    BI_Data(slot).RecData.SerialNumber = BI_Data(slot).UnitInfo.UnitSN
                Catch ex As Exception
                    Try
                        Transceiver(slot).Close()
                        If UCase(BI_HW.ConnectionType) = "SWITCH" Then
                            TurnPowerOnOff(slot, False)
                            Delay(3000)
                            TurnPowerOnOff(slot, True)
                            Delay(70000)
                        End If
                        Delay(10000)
                        Transceiver(slot).Address = BI_HW.IPAddress(slot)
                        Transceiver(slot).Open()
                        Transceiver(slot).Close_Timer()
                        AddMessage("Initializing unit: " & BI_Data(slot).UnitInfo.UnitSN)
                        Transceiver(slot).InitBurnInCommands()
                        Dim tRetry As Integer = 0
                        Do Until Transceiver(slot).CheckSlewRate Or tRetry = 3
                            AddMessage("Check Slew Rate Byte Failed.")
                            Transceiver(slot).WriteSlewRate()
                            tRetry = tRetry + 1
                        Loop
                        BI_Data(slot).MeasData.PAEnabled = True
                        BI_Data(slot).RecData.SerialNumber = BI_Data(slot).UnitInfo.UnitSN
                    Catch ex1 As Exception
                        AddMessage(ex1.Message)
                        If UCase(BI_HW.ConnectionType) = "SWITCH" Then TurnPowerOnOff(slot, False)
                        TurnFanOnOff(slot, False)
                        BI_Data(slot).UnitInfo.UnitActive = False
                        DisplayUnitActive(slot)

                        KanbanRealTimeUpload(slot, "Lost")
                    End Try
                End Try
            End If
            Application.DoEvents()
        Next

        For slot = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                Unitcount += 1
            End If
        Next

        If Unitcount = 0 Then
            AddMessage("No active unit")
            Return False
        Else
            Delay(1000)
            Return True
        End If
    End Function

    Public Sub Measurement(ByVal ePhase As String)
        Dim UnitCount As Integer = 0
        Dim tmpStep As String = String.Empty
        For Slot As Integer = 0 To SlotNum

            ''********************
            'For i As Integer = 0 To SlotNum
            '    TurnPowerOnOff(i, True)
            '    TurnFanOnOff(i, True)
            'Next
            'AddMessage("Waiting for units starting up...")
            'Delay(20000)

            'Slot = 0
            'Transceiver(Slot).Address = "192.168.99.50"
            'Transceiver(Slot).Open()

            'If BI_Data(Slot).MeasData.AlarmVSRamLog = False Then
            '    Dim tmpRam As String = Transceiver(Slot).GetRAMLOG
            '    BI_Data(Slot).MeasData.AlarmVSRamLog = WriteRamLogFile("Voyager_800", BI_Data(Slot).UnitInfo.UnitSN, tmpRam)
            'End If

            ''If Transceiver(Slot).CheckAlarm4 Then Transceiver(Slot).DisableAlarm4()
            ''Dim xxx As Boolean = Transceiver(Slot).CheckAlarm4
            'RemoveAlarm4(Slot)
            'Transceiver(Slot).InitBurnInCommands()
            'BI_Data(Slot).MeasData.PAEnabled = True
            'BI_Data(Slot).RecData.SerialNumber = BI_Data(Slot).UnitInfo.UnitSN
            'BI_Data(Slot).UnitInfo.UnitActive = True
            ''********************

            If BI_Data(Slot).UnitInfo.UnitActive Then
                Try
                    tmpStep = "Polling Start"

                    'For retry As Integer = 0 To 3
                    '    Dim tmpopen As Boolean = True
                    '    Try
                    '        Transceiver(Slot).Open()
                    '    Catch ex As Exception
                    '        tmpopen = False
                    '    End Try
                    '    If tmpopen Then Exit For
                    '    Delay(10000)
                    'Next

                    'Transceiver(Slot).Open()

                    DisplayPolling(Slot)
                    Application.DoEvents()

                    UnitCount += 1
                    BI_Data(Slot).MeasData.PollingTime = Now
                    BI_Data(Slot).MeasData.Phase = ePhase

                    'Meas Temperature
                    tmpStep = "Meas Temperature"
                    Dim tTemp As Viper_Transceiver.TempSensorData = Transceiver(Slot).TempSensors     'V2.0.3, add -99999 in Dll.
                    Dim TempArr(11) As Double
                    TempArr(0) = tTemp.PA0
                    TempArr(1) = tTemp.PA0VSWR
                    TempArr(2) = tTemp.PA1
                    TempArr(3) = tTemp.PA1VSWR
                    TempArr(4) = tTemp.LNA0
                    TempArr(5) = tTemp.LNA1
                    TempArr(6) = tTemp.LNA2
                    TempArr(7) = tTemp.LNA3
                    TempArr(8) = tTemp.PS_CONVERTER
                    TempArr(9) = tTemp.PS_BRICK
                    TempArr(10) = tTemp.FB
                    TempArr(11) = tTemp.RX
                    BI_Data(Slot).MeasData.PA0_Temp = TempArr(0)
                    BI_Data(Slot).MeasData.PA0_VSWR_Temp = TempArr(1)
                    BI_Data(Slot).MeasData.PA1_Temp = TempArr(2)
                    BI_Data(Slot).MeasData.PA1_VSWR_Temp = TempArr(3)
                    BI_Data(Slot).MeasData.LNA0_Temp = TempArr(4)
                    BI_Data(Slot).MeasData.LNA1_Temp = TempArr(5)
                    BI_Data(Slot).MeasData.LNA2_Temp = TempArr(6)
                    BI_Data(Slot).MeasData.LNA3_Temp = TempArr(7)
                    BI_Data(Slot).MeasData.PS_Converter_Temp = TempArr(8)
                    BI_Data(Slot).MeasData.PS_Brick_Temp = TempArr(9)
                    BI_Data(Slot).MeasData.FB_Temp = TempArr(10)
                    BI_Data(Slot).MeasData.RX_Temp = TempArr(11)

                    BI_Data(Slot).MeasData.PA_Temp_Delta = Format((TempArr(0) - TempArr(2)), "0.00")
                    BI_Data(Slot).MeasData.PSU_Temp_Delta = Format((TempArr(8) - TempArr(9)), "0.00")
                    Dim tmaxPA As Double, tmaxPS As Double
                    If TempArr(0) > TempArr(2) Then tmaxPA = TempArr(0) Else tmaxPA = TempArr(2)
                    If TempArr(8) > TempArr(9) Then tmaxPS = TempArr(8) Else tmaxPS = TempArr(9)
                    BI_Data(Slot).MeasData.PSU_PA_Temp_Delta = Format((tmaxPA - tmaxPS), "0.00")

                    Dim tmp_Temp_Max As Double = TempArr(0)
                    Dim tmp_Temp_Min As Double = TempArr(0)
                    Dim tmp_PA_Temp_Max As Double = TempArr(0)
                    Dim tmp_PA_Temp_Min As Double = TempArr(0)
                    For i As Integer = 1 To TempArr.Length - 1
                        If tmp_Temp_Max <= TempArr(i) Then tmp_Temp_Max = TempArr(i)
                        If tmp_Temp_Min >= TempArr(i) Then tmp_Temp_Min = TempArr(i)
                    Next
                    For j As Integer = 0 To 3
                        If tmp_PA_Temp_Max <= TempArr(j) Then tmp_PA_Temp_Max = TempArr(j)
                        If tmp_PA_Temp_Min >= TempArr(j) Then tmp_PA_Temp_Min = TempArr(j)
                    Next
                    Application.DoEvents()
                    Sleep(50)

                    'Meas Tx0
                    tmpStep = "Meas Tx0"
                    BI_Data(Slot).MeasData.TX0_Output_Pow = Transceiver(Slot).PowerOut(0)
                    'BI_Data(Slot).MeasData.TX0_VSWR = Transceiver(Slot).VSWR(0)
                    BI_Data(Slot).MeasData.TX0_VSWR = 0   'V1.1.3

                    'BI_Data(Slot).MeasData.TX0_Forward_Power_Detector = Transceiver(Slot).LnaReadAdc(Viper_Transceiver.LnaDetSwitchEnum.Tx0).FwdA2D
                    'BI_Data(Slot).MeasData.TX0_Reverse_Power_Detector = Transceiver(Slot).LnaReadAdc(Viper_Transceiver.LnaDetSwitchEnum.Tx0).RevA2D
                    BI_Data(Slot).MeasData.TX0_Forward_Power_Detector = 0   'V1.1.3
                    BI_Data(Slot).MeasData.TX0_Reverse_Power_Detector = 0   'V1.1.3

                    Dim tGainStatus As Viper_Transceiver.GainStatusData = Transceiver(Slot).GainStatus(0)   'V2.0.3, add -99999 in Dll.
                    'BI_Data(Slot).MeasData.TX0_Gain_VCA = tGainStatus.Vca
                    BI_Data(Slot).MeasData.TX0_Gain_VCA = 0    'V1.1.3
                    BI_Data(Slot).MeasData.TX0_txDacGain = tGainStatus.txDacGain    'V1.1.6
                    BI_Data(Slot).MeasData.TX0_totalTxAttn = tGainStatus.total_tx_attn    'V1.1.6
                    BI_Data(Slot).MeasData.TX0_Gain_TxStep = tGainStatus.TxStep
                    BI_Data(Slot).MeasData.TX0_Gain_FbStep = tGainStatus.FbStep
                    BI_Data(Slot).MeasData.TX0_Gain_FbTxQuo = tGainStatus.FbTxQuo
                    'BI_Data(Slot).MeasData.TX0_Gain_GainError = tGainStatus.GainError
                    'Do not measure GainError when PA Off, V1.1.1
                    If BI_Data(Slot).MeasData.PAEnabled Then
                        BI_Data(Slot).MeasData.TX0_Gain_GainError = tGainStatus.GainError
                    Else
                        BI_Data(Slot).MeasData.TX0_Gain_GainError = 0
                    End If

                    Dim tPaStatus As Viper_Transceiver.PaStatusData = Transceiver(Slot).PaStatus(0)   'V2.0.3, add -99999 in Dll.
                    BI_Data(Slot).MeasData.TX0_PA0_PsVolt = tPaStatus.Vdrain
                    BI_Data(Slot).MeasData.TX0_PA0_Temp = tPaStatus.Temperature
                    BI_Data(Slot).MeasData.TX0_PA0_BiasTemp = tPaStatus.Bias_Point_Temp
                    BI_Data(Slot).MeasData.TX0_PA0_Driver1Cur = tPaStatus.CurrentDriver1
                    BI_Data(Slot).MeasData.TX0_PA0_Driver2Cur = tPaStatus.CurrentDriver2
                    BI_Data(Slot).MeasData.TX0_PA0_Driver3Cur = tPaStatus.CurrentDriver3
                    BI_Data(Slot).MeasData.TX0_PA0_Driver4Cur = tPaStatus.CurrentDriver4
                    BI_Data(Slot).MeasData.TX0_PA0_Final1Cur = tPaStatus.CurrentFinal1
                    BI_Data(Slot).MeasData.TX0_PA0_Final2Cur = tPaStatus.CurrentFinal2
                    BI_Data(Slot).MeasData.TX0_PA0_Driver1Dac = tPaStatus.DacDriver1
                    BI_Data(Slot).MeasData.TX0_PA0_Driver2Dac = tPaStatus.DacDriver2
                    BI_Data(Slot).MeasData.TX0_PA0_Driver3Dac = tPaStatus.DacDriver3
                    BI_Data(Slot).MeasData.TX0_PA0_Driver4Dac = tPaStatus.DacDriver4
                    BI_Data(Slot).MeasData.TX0_PA0_Final1Dac = tPaStatus.DacFinal1
                    BI_Data(Slot).MeasData.TX0_PA0_Final2Dac = tPaStatus.DacFinal2

                    'V1.1.6
                    Dim tDpdDelayData As Viper_Transceiver.dpdDelayData = Transceiver(Slot).GetDpdDelayData(0)    'V2.0.3, add -99999 in Dll.
                    BI_Data(Slot).MeasData.TX0_PA0_ampDelayInt = tDpdDelayData.dpd_ampDelayInt
                    BI_Data(Slot).MeasData.TX0_PA0_ampDelayFrac = tDpdDelayData.dpd_ampDelayFrac
                    BI_Data(Slot).MeasData.TX0_PA0_MaxCrossCorrelation = tDpdDelayData.dpd_Maximum_cross_correlation

                    Dim tTxDPDTable() As Double = Transceiver(Slot).TxDPDTable(0)    'V2.0.3, add -99999 in Dll.
                    'BI_Data(Slot).MeasData.Tx0_DPD_L1_Table_Max_Gain = tTxDPDTable(0)
                    'BI_Data(Slot).MeasData.Tx0_DPD_L1_Table_Min_Gain = tTxDPDTable(1)
                    'V1.1.3
                    If BI_Data(Slot).MeasData.PAEnabled Then
                        BI_Data(Slot).MeasData.Tx0_DPD_L1_Table_Max_Gain = tTxDPDTable(0)
                        BI_Data(Slot).MeasData.Tx0_DPD_L1_Table_Min_Gain = tTxDPDTable(1)
                    Else
                        BI_Data(Slot).MeasData.Tx0_DPD_L1_Table_Max_Gain = 0
                        BI_Data(Slot).MeasData.Tx0_DPD_L1_Table_Min_Gain = 0
                    End If

                    'V1.1.6
                    Dim tDpdl2data As Viper_Transceiver.dpdL2Data = Transceiver(Slot).GetDpdL2Data(0)     'V2.0.3, add -99999 in Dll.
                    BI_Data(Slot).MeasData.Tx0_DPD_L2_3rd_sym_am = tDpdl2data.dpd_L2_3rd_sym_am
                    BI_Data(Slot).MeasData.Tx0_DPD_L2_3rd_sym_ph = tDpdl2data.dpd_L2_3rd_sym_ph
                    BI_Data(Slot).MeasData.Tx0_DPD_L3_3rd_sym_am = tDpdl2data.dpd_L3_3rd_sym_am
                    BI_Data(Slot).MeasData.Tx0_DPD_L3_3rd_sym_ph = tDpdl2data.dpd_L3_3rd_sym_ph
                    BI_Data(Slot).MeasData.Tx0_DPD_L2_5th_sym_am = tDpdl2data.dpd_L2_5th_sym_am
                    BI_Data(Slot).MeasData.Tx0_DPD_L2_5th_sym_ph = tDpdl2data.dpd_L2_5th_sym_ph
                    BI_Data(Slot).MeasData.Tx0_DPD_L3_5th_sym_am = tDpdl2data.dpd_L3_5th_sym_am
                    BI_Data(Slot).MeasData.Tx0_DPD_L3_5th_sym_ph = tDpdl2data.dpd_L3_5th_sym_ph
                    BI_Data(Slot).MeasData.Tx0_DPD_L2_3rd_asym_am = tDpdl2data.dpd_L2_3rd_asym_am
                    BI_Data(Slot).MeasData.Tx0_DPD_L2_3rd_asym_ph = tDpdl2data.dpd_L2_3rd_asym_ph
                    BI_Data(Slot).MeasData.Tx0_DPD_L2_5th_asym_am = tDpdl2data.dpd_L2_5th_asym_am
                    BI_Data(Slot).MeasData.Tx0_DPD_L2_5th_asym_ph = tDpdl2data.dpd_L2_5th_asym_ph

                    Application.DoEvents()
                    Sleep(50)

                    'Meas Tx1
                    tmpStep = "Meas Tx1"
                    BI_Data(Slot).MeasData.TX1_Output_Pow = Transceiver(Slot).PowerOut(1)
                    'BI_Data(Slot).MeasData.TX1_VSWR = Transceiver(Slot).VSWR(1)
                    BI_Data(Slot).MeasData.TX1_VSWR = 0     'V1.1.3

                    'BI_Data(Slot).MeasData.TX1_Forward_Power_Detector = Transceiver(Slot).LnaReadAdc(Viper_Transceiver.LnaDetSwitchEnum.Tx1).FwdA2D
                    'BI_Data(Slot).MeasData.TX1_Reverse_Power_Detector = Transceiver(Slot).LnaReadAdc(Viper_Transceiver.LnaDetSwitchEnum.Tx1).RevA2D
                    BI_Data(Slot).MeasData.TX1_Forward_Power_Detector = 0     'V1.1.3
                    BI_Data(Slot).MeasData.TX1_Reverse_Power_Detector = 0     'V1.1.3

                    Dim tGainStatus_1 As Viper_Transceiver.GainStatusData = Transceiver(Slot).GainStatus(1)
                    'BI_Data(Slot).MeasData.TX1_Gain_VCA = tGainStatus_1.Vca
                    BI_Data(Slot).MeasData.TX1_Gain_VCA = 0     'V1.1.3
                    BI_Data(Slot).MeasData.TX1_txDacGain = tGainStatus_1.txDacGain    'V1.1.6
                    BI_Data(Slot).MeasData.TX1_totalTxAttn = tGainStatus_1.total_tx_attn    'V1.1.6
                    BI_Data(Slot).MeasData.TX1_Gain_TxStep = tGainStatus_1.TxStep
                    BI_Data(Slot).MeasData.TX1_Gain_FbStep = tGainStatus_1.FbStep
                    BI_Data(Slot).MeasData.TX1_Gain_FbTxQuo = tGainStatus_1.FbTxQuo
                    'BI_Data(Slot).MeasData.TX1_Gain_GainError = tGainStatus_1.GainError
                    'Do not measure GainError when PA Off, V1.1.1
                    If BI_Data(Slot).MeasData.PAEnabled Then
                        BI_Data(Slot).MeasData.TX1_Gain_GainError = tGainStatus_1.GainError
                    Else
                        BI_Data(Slot).MeasData.TX1_Gain_GainError = 0
                    End If

                    Dim tPaStatus_1 As Viper_Transceiver.PaStatusData = Transceiver(Slot).PaStatus(1)
                    BI_Data(Slot).MeasData.TX1_PA1_PsVolt = tPaStatus_1.Vdrain
                    BI_Data(Slot).MeasData.TX1_PA1_Temp = tPaStatus_1.Temperature
                    BI_Data(Slot).MeasData.TX1_PA1_BiasTemp = tPaStatus_1.Bias_Point_Temp
                    BI_Data(Slot).MeasData.TX1_PA1_Driver1Cur = tPaStatus_1.CurrentDriver1
                    BI_Data(Slot).MeasData.TX1_PA1_Driver2Cur = tPaStatus_1.CurrentDriver2
                    BI_Data(Slot).MeasData.TX1_PA1_Driver3Cur = tPaStatus_1.CurrentDriver3
                    BI_Data(Slot).MeasData.TX1_PA1_Driver4Cur = tPaStatus_1.CurrentDriver4
                    BI_Data(Slot).MeasData.TX1_PA1_Final1Cur = tPaStatus_1.CurrentFinal1
                    BI_Data(Slot).MeasData.TX1_PA1_Final2Cur = tPaStatus_1.CurrentFinal2
                    BI_Data(Slot).MeasData.TX1_PA1_Driver1Dac = tPaStatus_1.DacDriver1
                    BI_Data(Slot).MeasData.TX1_PA1_Driver2Dac = tPaStatus_1.DacDriver2
                    BI_Data(Slot).MeasData.TX1_PA1_Driver3Dac = tPaStatus_1.DacDriver3
                    BI_Data(Slot).MeasData.TX1_PA1_Driver4Dac = tPaStatus_1.DacDriver4
                    BI_Data(Slot).MeasData.TX1_PA1_Final1Dac = tPaStatus_1.DacFinal1
                    BI_Data(Slot).MeasData.TX1_PA1_Final2Dac = tPaStatus_1.DacFinal2

                    'V1.1.6
                    Dim tDpdDelayData_1 As Viper_Transceiver.dpdDelayData = Transceiver(Slot).GetDpdDelayData(1)
                    BI_Data(Slot).MeasData.TX1_PA1_ampDelayInt = tDpdDelayData_1.dpd_ampDelayInt
                    BI_Data(Slot).MeasData.TX1_PA1_ampDelayFrac = tDpdDelayData_1.dpd_ampDelayFrac
                    BI_Data(Slot).MeasData.TX1_PA1_MaxCrossCorrelation = tDpdDelayData_1.dpd_Maximum_cross_correlation

                    Dim tTxDPDTable_1() As Double = Transceiver(Slot).TxDPDTable(1)
                    'BI_Data(Slot).MeasData.Tx1_DPD_L1_Table_Max_Gain = tTxDPDTable_1(0)
                    'BI_Data(Slot).MeasData.Tx1_DPD_L1_Table_Min_Gain = tTxDPDTable_1(1)
                    'V1.1.3
                    If BI_Data(Slot).MeasData.PAEnabled Then
                        BI_Data(Slot).MeasData.Tx1_DPD_L1_Table_Max_Gain = tTxDPDTable_1(0)
                        BI_Data(Slot).MeasData.Tx1_DPD_L1_Table_Min_Gain = tTxDPDTable_1(1)
                    Else
                        BI_Data(Slot).MeasData.Tx1_DPD_L1_Table_Max_Gain = 0
                        BI_Data(Slot).MeasData.Tx1_DPD_L1_Table_Min_Gain = 0
                    End If

                    'V1.1.6
                    Dim tDpdl2data_1 As Viper_Transceiver.dpdL2Data = Transceiver(Slot).GetDpdL2Data(1)
                    BI_Data(Slot).MeasData.Tx1_DPD_L2_3rd_sym_am = tDpdl2data_1.dpd_L2_3rd_sym_am
                    BI_Data(Slot).MeasData.Tx1_DPD_L2_3rd_sym_ph = tDpdl2data_1.dpd_L2_3rd_sym_ph
                    BI_Data(Slot).MeasData.Tx1_DPD_L3_3rd_sym_am = tDpdl2data_1.dpd_L3_3rd_sym_am
                    BI_Data(Slot).MeasData.Tx1_DPD_L3_3rd_sym_ph = tDpdl2data_1.dpd_L3_3rd_sym_ph
                    BI_Data(Slot).MeasData.Tx1_DPD_L2_5th_sym_am = tDpdl2data_1.dpd_L2_5th_sym_am
                    BI_Data(Slot).MeasData.Tx1_DPD_L2_5th_sym_ph = tDpdl2data_1.dpd_L2_5th_sym_ph
                    BI_Data(Slot).MeasData.Tx1_DPD_L3_5th_sym_am = tDpdl2data_1.dpd_L3_5th_sym_am
                    BI_Data(Slot).MeasData.Tx1_DPD_L3_5th_sym_ph = tDpdl2data_1.dpd_L3_5th_sym_ph
                    BI_Data(Slot).MeasData.Tx1_DPD_L2_3rd_asym_am = tDpdl2data_1.dpd_L2_3rd_asym_am
                    BI_Data(Slot).MeasData.Tx1_DPD_L2_3rd_asym_ph = tDpdl2data_1.dpd_L2_3rd_asym_ph
                    BI_Data(Slot).MeasData.Tx1_DPD_L2_5th_asym_am = tDpdl2data_1.dpd_L2_5th_asym_am
                    BI_Data(Slot).MeasData.Tx1_DPD_L2_5th_asym_ph = tDpdl2data_1.dpd_L2_5th_asym_ph

                    Application.DoEvents()
                    Sleep(50)

                    'Meas Rx0
                    tmpStep = "Meas Rx0"
                    BI_Data(Slot).MeasData.Rx0_LNA_Atten = Transceiver(Slot).LNAAttenuator(0)    'V2.0.3, add -99999 in Dll.

                    Dim tRxAttn_0() As Double = Transceiver(Slot).RxAttenuator
                    BI_Data(Slot).MeasData.Rx0_Rx_Atten0 = tRxAttn_0(0 * 2)
                    BI_Data(Slot).MeasData.Rx0_Rx_Atten1 = tRxAttn_0(0 * 2 + 1)

                    Dim tRssi_0 As Viper_Transceiver.RssiData = Transceiver(Slot).Rssi
                    BI_Data(Slot).MeasData.Rx0_RSSI_C0 = tRssi_0.Rx(0).Carr(0).Pcg_Rssi
                    BI_Data(Slot).MeasData.Rx0_RSSI_C1 = tRssi_0.Rx(0).Carr(1).Pcg_Rssi
                    Application.DoEvents()
                    Sleep(50)

                    'Meas Rx1
                    tmpStep = "Meas Rx1"
                    BI_Data(Slot).MeasData.Rx1_LNA_Atten = Transceiver(Slot).LNAAttenuator(1)

                    Dim tRxAttn_1() As Double = Transceiver(Slot).RxAttenuator
                    BI_Data(Slot).MeasData.Rx1_Rx_Atten0 = tRxAttn_1(1 * 2)
                    BI_Data(Slot).MeasData.Rx1_Rx_Atten1 = tRxAttn_1(1 * 2 + 1)

                    Dim tRssi_1 As Viper_Transceiver.RssiData = Transceiver(Slot).Rssi
                    BI_Data(Slot).MeasData.Rx1_RSSI_C0 = tRssi_1.Rx(1).Carr(0).Pcg_Rssi
                    BI_Data(Slot).MeasData.Rx1_RSSI_C1 = tRssi_1.Rx(1).Carr(1).Pcg_Rssi
                    Application.DoEvents()
                    Sleep(50)

                    'Meas Rx2
                    tmpStep = "Meas Rx2"
                    BI_Data(Slot).MeasData.Rx2_LNA_Atten = Transceiver(Slot).LNAAttenuator(2)

                    Dim tRxAttn_2() As Double = Transceiver(Slot).RxAttenuator
                    BI_Data(Slot).MeasData.Rx2_Rx_Atten0 = tRxAttn_2(2 * 2)
                    BI_Data(Slot).MeasData.Rx2_Rx_Atten1 = tRxAttn_2(2 * 2 + 1)

                    Dim tRssi_2 As Viper_Transceiver.RssiData = Transceiver(Slot).Rssi
                    BI_Data(Slot).MeasData.Rx2_RSSI_C0 = tRssi_2.Rx(2).Carr(0).Pcg_Rssi
                    BI_Data(Slot).MeasData.Rx2_RSSI_C1 = tRssi_2.Rx(2).Carr(1).Pcg_Rssi
                    Application.DoEvents()
                    Sleep(50)

                    'Meas Rx3
                    tmpStep = "Meas Rx3"
                    BI_Data(Slot).MeasData.Rx3_LNA_Atten = Transceiver(Slot).LNAAttenuator(3)

                    Dim tRxAttn_3() As Double = Transceiver(Slot).RxAttenuator
                    BI_Data(Slot).MeasData.Rx3_Rx_Atten0 = tRxAttn_3(3 * 2)
                    BI_Data(Slot).MeasData.Rx3_Rx_Atten1 = tRxAttn_3(3 * 2 + 1)

                    Dim tRssi_3 As Viper_Transceiver.RssiData = Transceiver(Slot).Rssi
                    BI_Data(Slot).MeasData.Rx3_RSSI_C0 = tRssi_0.Rx(3).Carr(0).Pcg_Rssi
                    BI_Data(Slot).MeasData.Rx3_RSSI_C1 = tRssi_0.Rx(3).Carr(1).Pcg_Rssi
                    Application.DoEvents()
                    Sleep(50)

                    'Meas PS
                    tmpStep = "Meas PS"
                    Dim tPsStatus As Viper_Transceiver.PSStatusData = Transceiver(Slot).PSStatus    'V2.0.3, add -99999 in Dll.
                    BI_Data(Slot).MeasData.Input_Voltage = tPsStatus.Input_Voltage
                    BI_Data(Slot).MeasData.Input_Current = tPsStatus.Input_Current
                    BI_Data(Slot).MeasData.Input_Power = tPsStatus.Input_Power
                    BI_Data(Slot).MeasData.Output_Voltage = tPsStatus.Output_Voltage
                    'BI_Data(Slot).MeasData.AISG_12V_Voltage = tPsStatus.AISG_12V_Voltage
                    'BI_Data(Slot).MeasData.AISG_12V_Current = tPsStatus.AISG_12V_Current
                    BI_Data(Slot).MeasData.AISG_12V_Voltage = 0    'V1.1.3
                    BI_Data(Slot).MeasData.AISG_12V_Current = 0
                    BI_Data(Slot).MeasData.AISG_24V_Voltage = tPsStatus.AISG_24V_Voltage
                    BI_Data(Slot).MeasData.AISG_24V_Current = tPsStatus.AISG_24V_Current
                    Application.DoEvents()
                    Sleep(50)

                    'Meas(Version)
                    tmpStep = "Meas Version"
                    'BI_Data(Slot).MeasData.CPRIFPGAVer = Transceiver(Slot).CPRIRev
                    BI_Data(Slot).MeasData.FPGARevision = Transceiver(Slot).fpgaRev
                    BI_Data(Slot).MeasData.DSPRevision = Transceiver(Slot).dspRev
                    BI_Data(Slot).MeasData.SWRevision = Transceiver(Slot).pkgRev

                    'Read Alarms
                    tmpStep = "Meas Alarms"
                    Try
                        Dim AlarmArr() As String = Transceiver(Slot).Alarms
                        BI_Data(Slot).MeasData.AlarmString = String.Empty
                        For i As Integer = 0 To AlarmArr.Length - 1
                            If InStr(UCase(AlarmArr(i)), UCase("Alarm")) Then
                                'If (InStr(UCase(AlarmArr(i)), UCase("Alarm 54")) + InStr(UCase(AlarmArr(i)), UCase("Alarm 12"))) = 0 Then
                                If (InStr(UCase(AlarmArr(i)), UCase("Alarm 54")) + _
                                    InStr(UCase(AlarmArr(i)), UCase("Alarm 12")) + _
                                    InStr(UCase(AlarmArr(i)), UCase("Alarm 13")) + _
                                    InStr(UCase(AlarmArr(i)), UCase("Alarm 32")) + _
                                    InStr(UCase(AlarmArr(i)), UCase("Alarm 41"))) = 0 Then
                                    'Ignore Voltage Alarm before FW updated.
                                    'Ignore Alarm 13, 32, for RF off
                                    BI_Data(Slot).MeasData.AlarmString = BI_Data(Slot).MeasData.AlarmString & "; " & Replace(AlarmArr(i), ",", " -")
                                End If
                            End If
                        Next

                        If BI_Data(Slot).MeasData.AlarmString = String.Empty Then
                            BI_Data(Slot).MeasData.AlarmString = "No Alarm"
                        End If

                        ''V1.1.6
                        'If BI_Data(Slot).MeasData.AlarmString <> "No Alarm" And BI_Data(Slot).MeasData.RamLog = "" Then
                        '    BI_Data(Slot).MeasData.RamLog = Transceiver(Slot).GetRamlog
                        'End If

                    Catch ex As Exception
                        BI_Data(Slot).MeasData.AlarmString = "No Alarm"
                    End Try
                    Application.DoEvents()
                    Sleep(50)

                    'Meas PowerCycleCount
                    'tmpStep = "Meas PowerCycleCount"
                    ''BI_Data(Slot).MeasData.CPRIFPGAVer = Transceiver(Slot).CPRIRev
                    'BI_Data(Slot).MeasData.PowerCycleCount = Transceiver(Slot).PowerCycleCount
                    'Application.DoEvents()
                    'Sleep(50)

                    'If tmp_Temp_Max >= BI_Limit.Thres_Hold_Temp Then
                    If tmp_PA_Temp_Max >= BI_Limit.Thres_Hold_Temp Then
                        BI_Data(Slot).RecData.Thres_Hold_Time += CDbl((DateDiff(DateInterval.Second, BI_Data(Slot).MeasData.Last_Polling_Time, BI_Data(Slot).MeasData.PollingTime) / 60))
                    End If

                    If BI_Data(Slot).MeasData.PAEnabled Then
                        BI_Data(Slot).RecData.RF_On_Time += CDbl(DateDiff(DateInterval.Second, BI_Data(Slot).MeasData.Last_Polling_Time, BI_Data(Slot).MeasData.PollingTime) / 60)
                    End If
                    BI_Data(Slot).MeasData.Last_Polling_Time = BI_Data(Slot).MeasData.PollingTime

                    UpdateDCFData(Slot)
                    UpdateTestReocrd(Slot, ePhase)

                    ''V2.0.3  Split single Ramlog with normal one.
                    'If BI_Data(Slot).MeasData.RamLogSingleFlag Then WriteRamLogFileSingle(Slot)

                    'V2.0.8 Catch ramlog for requirement from Qiaozhi for debug.
                    'If BI_Data(Slot).MeasData.TX0_Output_Pow < 43 Or BI_Data(Slot).MeasData.TX1_Output_Pow < 43 Then WriteRamLogFileSingle(Slot)
                    'V2.0.9
                    If BI_Data(Slot).MeasData.PAEnabled Then
                        If BI_Data(Slot).MeasData.TX0_Output_Pow < 43 Or BI_Data(Slot).MeasData.TX1_Output_Pow < 43 Then BI_Data(Slot).MeasData.RamLogSingleFlag = True
                    End If

                    'V2.0.9
                    If BI_Data(Slot).RecData.AlarmFlag Then BI_Data(Slot).MeasData.RamLogSingleFlag = True

                    If Not BI_Data(Slot).RecData.AlarmFlag Then
                        KanbanRealTimeUpload(Slot, "Testing")
                    Else
                        KanbanRealTimeUpload(Slot, "Fail")
                    End If

                    tmpStep = "Check RF/Fan on/off temperatures"
                    'V1.2.1, Light, 2013.10.18, Modify for ALU new BI profile.
                    'If tmp_Temp_Max > BI_Profile.BI_Target_Temp Then
                    If tmp_PA_Temp_Max > BI_Profile.BI_Target_Temp Then
                        Call TurnRFOnOff(Slot, False)
                        Call TurnFanOnOff(Slot, True)
                    Else
                        If BI_Data(Slot).UnitInfo.Temp_Heating_Up Then
                            'If tmp_Temp_Max < BI_Profile.BI_Target_Temp - 5 And Not BI_Data(Slot).MeasData.PAEnabled Then
                            If tmp_PA_Temp_Max < BI_Profile.BI_Target_Temp - 20 And Not BI_Data(Slot).MeasData.PAEnabled Then
                                Call TurnRFOnOff(Slot, True)
                                Call TurnFanOnOff(Slot, False)
                            End If
                        Else
                            If BI_Data(Slot).MeasData.PAEnabled Then
                                Call TurnRFOnOff(Slot, False)
                                Call TurnFanOnOff(Slot, True)
                            End If
                        End If
                    End If

                    'Try
                    '    Transceiver(Slot).Close()
                    'Catch ex As Exception

                    'End Try

                Catch ex As Exception
                    AddMessage("Slot:" & (Slot + 1) & ": " & tmpStep & ": " & ex.Message)

                    WriteRamLogFileSingle(Slot)

                    If UCase(BI_HW.ConnectionType) = "SWITCH" Then TurnPowerOnOff(Slot, False)
                    TurnFanOnOff(Slot, False)
                    BI_Data(Slot).MeasData.PAEnabled = False
                    BI_Data(Slot).UnitInfo.UnitActive = False
                    DisplayUnitActive(Slot)

                    KanbanRealTimeUpload(Slot, "Lost")
                End Try
            End If
        Next

        If UnitCount = 0 Then
            AddMessage("All Units lost connection!")
            AbortTest = True
        End If
    End Sub

    '' ''New Measurement function for Multithread reading   Light, 2012.4.20, V1.5.0
    ' ''Public Sub Measurement(ByVal ePhase As String)
    ' ''    Dim UnitCount As Integer = 0
    ' ''    Dim tmpStep As String = String.Empty
    ' ''    Dim Thread1 As CustomThread

    ' ''    For Slot As Integer = 0 To 3 '''SlotNum

    ' ''        ''********************
    ' ''        'For i As Integer = 0 To SlotNum
    ' ''        '    TurnPowerOnOff(i, True)
    ' ''        '    TurnFanOnOff(i, True)
    ' ''        'Next
    ' ''        'AddMessage("Waiting for units starting up...")
    ' ''        'Delay(50000)

    ' ''        'Slot = 0
    ' ''        'Transceiver(Slot).Address = "192.168.1.1"
    ' ''        'Transceiver(Slot).Open()

    ' ''        'If BI_Data(Slot).MeasData.AlarmVSRamLog = False Then
    ' ''        '    Dim tmpRam As String = Transceiver(Slot).GetRAMLOG
    ' ''        '    BI_Data(Slot).MeasData.AlarmVSRamLog = WriteRamLogFile("Voyager_800", BI_Data(Slot).UnitInfo.UnitSN, tmpRam)
    ' ''        'End If

    ' ''        ''If Transceiver(Slot).CheckAlarm4 Then Transceiver(Slot).DisableAlarm4()
    ' ''        ''Dim xxx As Boolean = Transceiver(Slot).CheckAlarm4
    ' ''        'RemoveAlarm4(Slot)
    ' ''        'Transceiver(Slot).InitBurnInCommands()
    ' ''        'BI_Data(Slot).MeasData.PAEnabled = True
    ' ''        'BI_Data(Slot).RecData.SerialNumber = BI_Data(Slot).UnitInfo.UnitSN
    ' ''        'BI_Data(Slot).UnitInfo.UnitActive = True
    ' ''        ''********************

    ' ''        'If BI_Data(Slot).UnitInfo.UnitActive Then
    ' ''        UnitCount += 1
    ' ''        Thread1 = MeasurementStep_Task(Slot, ePhase)
    ' ''        Application.DoEvents()
    ' ''        'While Thread1.IsRunning
    ' ''        '    System.Threading.Thread.Sleep(100)
    ' ''        '    System.Windows.Forms.Application.DoEvents()
    ' ''        'End While
    ' ''        'If Thread1.IsRunning Then
    ' ''        '    Application.DoEvents()
    ' ''        '    Delay(1000)
    ' ''        'End If
    ' ''        'End If
    ' ''    Next

    ' ''    Application.DoEvents()

    ' ''    While Thread1.IsRunning
    ' ''        System.Threading.Thread.Sleep(100)
    ' ''        System.Windows.Forms.Application.DoEvents()
    ' ''    End While
    ' ''    Application.DoEvents()

    ' ''    'WAIT BOTH THREADS
    ' ''    'Dim ThreadRunNum As Integer = 0
    ' ''    'Dim ThreadRunning As Boolean = True
    ' ''    'Do Until ThreadRunning = False
    ' ''    '    For slot As Integer = 0 To SlotNum
    ' ''    '        If Thread(slot).IsRunning Then ThreadRunNum += 1
    ' ''    '    Next
    ' ''    '    ThreadRunning = (ThreadRunNum > 0)
    ' ''    '    ThreadRunNum = 0
    ' ''    'Loop
    ' ''    'For slot As Integer = 0 To SlotNum
    ' ''    '    If Thread(slot).ErrorStatus Then Throw New Exception(Thread(slot).ErrorString)
    ' ''    'Next
    ' ''    'While Thread1.IsRunning Or Thread2.IsRunning
    ' ''    '    System.Threading.Thread.Sleep(100)
    ' ''    '    System.Windows.Forms.Application.DoEvents()
    ' ''    'End While
    ' ''    'If Thread1.ErrorStatus Then
    ' ''    '    Throw New Exception(Thread1.ErrorString)
    ' ''    'End If
    ' ''    'If Thread2.ErrorStatus Then
    ' ''    '    Throw New Exception(Thread2.ErrorString)
    ' ''    'End If

    ' ''    If UnitCount = 0 Then
    ' ''        AddMessage("All Units lost connection!")
    ' ''        AbortTest = True
    ' ''    End If
    ' ''End Sub
    ' ''Public Sub MeasurementStepThread(ByVal slot As Integer, ByVal ePhase As String)
    ' ''    Dim tmpStep As String = String.Empty

    ' ''    'MsgBox(ePhase & "try multithread " & slot)
    ' ''    'Application.DoEvents()
    ' ''    'If frmMain.TextBoxMessage.InvokeRequired Then
    ' ''    '    Dim pInvoke As New dl_ShowTextbox(AddressOf ShowTextbox)
    ' ''    '    Me.Invoke(pInvoke, New Object() {iStr})


    ' ''    'End If
    ' ''    AddMessage(ePhase & "try multithread " & slot)
    ' ''    'frmMain.TextBoxMessage.Text = "xxxxxxxxxxxxxx"
    ' ''    Application.DoEvents()
    ' ''    Delay(5000)
    ' ''    'Try
    ' ''    '    tmpStep = "Polling Start"
    ' ''    '    'Transceiver(Slot).Open()

    ' ''    '    DisplayPolling(slot)
    ' ''    '    Application.DoEvents()

    ' ''    '    'UnitCount += 1
    ' ''    '    BI_Data(slot).MeasData.PollingTime = Now
    ' ''    '    BI_Data(slot).MeasData.Phase = ePhase

    ' ''    '    'Meas Temperature
    ' ''    '    tmpStep = "Meas Temperature"
    ' ''    '    Dim TempArr(4) As Double
    ' ''    '    TempArr(0) = Transceiver(slot).Temperature(0)   'PA0
    ' ''    '    TempArr(1) = Transceiver(slot).Temperature(1)   'PA1
    ' ''    '    TempArr(2) = Transceiver(slot).Temperature(2)   'LNA0
    ' ''    '    TempArr(3) = Transceiver(slot).Temperature(3)   'LNA1
    ' ''    '    TempArr(4) = Transceiver(slot).FBTemperature
    ' ''    '    BI_Data(slot).MeasData.PA0_Temp = TempArr(0)
    ' ''    '    BI_Data(slot).MeasData.PA1_Temp = TempArr(1)
    ' ''    '    BI_Data(slot).MeasData.LNA0_Temp = TempArr(2)
    ' ''    '    BI_Data(slot).MeasData.LNA1_Temp = TempArr(3)
    ' ''    '    BI_Data(slot).MeasData.FB_Temp = TempArr(4)
    ' ''    '    Dim tmp_Temp_Max As Double = TempArr(0)
    ' ''    '    Dim tmp_Temp_Min As Double = TempArr(0)
    ' ''    '    Dim tmp_PA_Temp_Max As Double = TempArr(2)
    ' ''    '    Dim tmp_PA_Temp_Min As Double = TempArr(2)
    ' ''    '    For i As Integer = 1 To TempArr.Length - 1
    ' ''    '        If tmp_Temp_Max <= TempArr(i) Then tmp_Temp_Max = TempArr(i)
    ' ''    '        If tmp_Temp_Min >= TempArr(i) Then tmp_Temp_Min = TempArr(i)
    ' ''    '        'If tmp_PA_Temp_Max <= TempArr(2) Then tmp_PA_Temp_Max = TempArr(2)
    ' ''    '        'If tmp_PA_Temp_Min >= TempArr(2) Then tmp_PA_Temp_Min = TempArr(2)
    ' ''    '    Next
    ' ''    '    For j As Integer = 0 To 1
    ' ''    '        If tmp_PA_Temp_Max <= TempArr(j) Then tmp_PA_Temp_Max = TempArr(j)
    ' ''    '        If tmp_PA_Temp_Min >= TempArr(j) Then tmp_PA_Temp_Min = TempArr(j)
    ' ''    '    Next
    ' ''    '    Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'Meas Output Power
    ' ''    '    tmpStep = "Meas Output Power"
    ' ''    '    'BI_Data(Slot).MeasData.Output_Power_0 = Transceiver(Slot).FWPow(0)
    ' ''    '    'BI_Data(Slot).MeasData.Output_Power_1 = Transceiver(Slot).FWPow(1)
    ' ''    '    'Dim tPout As Voyager_Transceiver.Pout = Transceiver(Slot).PowerOut
    ' ''    '    BI_Data(slot).MeasData.Output_Power_0 = Transceiver(slot).PowerOut(0)
    ' ''    '    BI_Data(slot).MeasData.Output_Power_1 = Transceiver(slot).PowerOut(1)
    ' ''    '    Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'Meas ADC_OPD & ADC_RPD
    ' ''    '    'tmpStep = "Meas ADC_OPD & ADC_RPD"
    ' ''    '    'Dim ADC_Arr As Integer() = Transceiver(Slot).LNA_ADC2
    ' ''    '    'BI_Data(Slot).MeasData.OPD = ADC_Arr(1)
    ' ''    '    'BI_Data(Slot).MeasData.RPD = ADC_Arr(2)
    ' ''    '    'Application.DoEvents()
    ' ''    '    'Sleep(50)

    ' ''    '    'Meas VSWR
    ' ''    '    tmpStep = "Meas VSWR"
    ' ''    '    BI_Data(slot).MeasData.VSWR_0 = Transceiver(slot).VSWR(0)
    ' ''    '    BI_Data(slot).MeasData.VSWR_1 = Transceiver(slot).VSWR(1)
    ' ''    '    Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'Meas Attn
    ' ''    '    tmpStep = "Meas Attn"
    ' ''    '    'BI_Data(Slot).MeasData.txStepAttn_0 = 0
    ' ''    '    'BI_Data(Slot).MeasData.txStepAttn_1 = 0
    ' ''    '    BI_Data(slot).MeasData.txStepAttn_0 = Transceiver(slot).txStepAttn(0)
    ' ''    '    BI_Data(slot).MeasData.txStepAttn_1 = Transceiver(slot).txStepAttn(1)
    ' ''    '    BI_Data(slot).MeasData.rxAttn_0 = Transceiver(slot).rxAttn(0)
    ' ''    '    BI_Data(slot).MeasData.rxAttn_1 = Transceiver(slot).rxAttn(1)
    ' ''    '    BI_Data(slot).MeasData.fbAttn = Transceiver(slot).fbAttn
    ' ''    '    Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'Meas Voltage, Current
    ' ''    '    tmpStep = "Meas Input_V"
    ' ''    '    BI_Data(slot).MeasData.Input_V = Transceiver(slot).PSU_Volt
    ' ''    '    Application.DoEvents()
    ' ''    '    Sleep(50)
    ' ''    '    tmpStep = "Meas Input_I"
    ' ''    '    BI_Data(slot).MeasData.Input_I = Transceiver(slot).PSU_Curr
    ' ''    '    Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'Meas PA_BiasStatus
    ' ''    '    tmpStep = "Meas PA_BiasStatus"
    ' ''    '    Dim PA_Status_0 As Voyager_Transceiver.BiasStatus = Transceiver(slot).PA_BiasStatus(0)
    ' ''    '    Dim PA_Status_1 As Voyager_Transceiver.BiasStatus = Transceiver(slot).PA_BiasStatus(1)
    ' ''    '    BI_Data(slot).MeasData.PS_V_0 = PA_Status_0.DrainVoltage
    ' ''    '    BI_Data(slot).MeasData.FINAL1_0 = Format(PA_Status_0.Final1Current, "0.000")
    ' ''    '    BI_Data(slot).MeasData.FINAL2_0 = Format(PA_Status_0.Final2Current, "0.000")
    ' ''    '    'BI_Data(Slot).MeasData.DRIVER1_0 = PA_Status_0.Driver1Current
    ' ''    '    'BI_Data(Slot).MeasData.DRIVER2_0 = PA_Status_0.Driver2Current
    ' ''    '    BI_Data(slot).MeasData.DRIVER3_0 = Format(PA_Status_0.Driver3Current, "0.000")
    ' ''    '    BI_Data(slot).MeasData.DRIVER4_0 = Format(PA_Status_0.Driver4Current, "0.000")
    ' ''    '    BI_Data(slot).MeasData.PS_V_1 = PA_Status_1.DrainVoltage
    ' ''    '    BI_Data(slot).MeasData.FINAL1_1 = Format(PA_Status_1.Final1Current, "0.000")
    ' ''    '    BI_Data(slot).MeasData.FINAL2_1 = Format(PA_Status_1.Final2Current, "0.000")
    ' ''    '    'BI_Data(Slot).MeasData.DRIVER1_1 = PA_Status_1.Driver1Current
    ' ''    '    'BI_Data(Slot).MeasData.DRIVER2_1 = PA_Status_1.Driver2Current
    ' ''    '    BI_Data(slot).MeasData.DRIVER3_1 = Format(PA_Status_1.Driver3Current, "0.000")
    ' ''    '    BI_Data(slot).MeasData.DRIVER4_1 = Format(PA_Status_1.Driver4Current, "0.000")
    ' ''    '    My.Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'Meas RSSI
    ' ''    '    tmpStep = "Meas RSSI"
    ' ''    '    Dim tmpRSSI As Double(,) = Transceiver(slot).GetRSSI
    ' ''    '    BI_Data(slot).MeasData.RSSI0_0 = tmpRSSI(0, 0)
    ' ''    '    BI_Data(slot).MeasData.RSSI1_0 = tmpRSSI(0, 1)
    ' ''    '    BI_Data(slot).MeasData.RSSI2_0 = tmpRSSI(0, 2)
    ' ''    '    BI_Data(slot).MeasData.RSSI3_0 = tmpRSSI(0, 3)
    ' ''    '    BI_Data(slot).MeasData.RSSI4_0 = tmpRSSI(0, 4)
    ' ''    '    BI_Data(slot).MeasData.RSSI0_1 = tmpRSSI(1, 0)
    ' ''    '    BI_Data(slot).MeasData.RSSI1_1 = tmpRSSI(1, 1)
    ' ''    '    BI_Data(slot).MeasData.RSSI2_1 = tmpRSSI(1, 2)
    ' ''    '    BI_Data(slot).MeasData.RSSI3_1 = tmpRSSI(1, 3)
    ' ''    '    BI_Data(slot).MeasData.RSSI4_1 = tmpRSSI(1, 4)
    ' ''    '    My.Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'tmpStep = "Meas Calc_ACPR"
    ' ''    '    'Dim tmpACPR() As Double = Transceiver(Slot).Calc_ACPR
    ' ''    '    'BI_Data(Slot).MeasData.ACPR_Low = tmpACPR(0)
    ' ''    '    'BI_Data(Slot).MeasData.ACPR_High = tmpACPR(1)
    ' ''    '    'Application.DoEvents()
    ' ''    '    'Sleep(50)

    ' ''    '    'Meas LNA_FWD_Power_Ticks
    ' ''    '    tmpStep = "Meas LNA_FWD_Power_Ticks"
    ' ''    '    BI_Data(slot).MeasData.LNA_FWD_Power_Ticks_0 = Transceiver(slot).LNA_FWD_Power_Ticks(0)
    ' ''    '    BI_Data(slot).MeasData.LNA_FWD_Power_Ticks_1 = Transceiver(slot).LNA_FWD_Power_Ticks(1)
    ' ''    '    My.Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'Meas DPD
    ' ''    '    tmpStep = "Meas DPD"
    ' ''    '    Dim pDpdL1 As Voyager_Transceiver.DpdL1Data = Transceiver(slot).DpdL1
    ' ''    '    Dim pDpdL2 As Voyager_Transceiver.DpdL2Data = Transceiver(slot).DpdL2
    ' ''    '    BI_Data(slot).MeasData.DPD_L1_Max_Table_Gain_0 = Format(pDpdL1.DPD_L1_Max_Table_Gain_0, "0.00")
    ' ''    '    BI_Data(slot).MeasData.DPD_L1_Min_Table_Gain_0 = Format(pDpdL1.DPD_L1_Min_Table_Gain_0, "0.00")
    ' ''    '    BI_Data(slot).MeasData.DPD_L1_Max_Table_Gain_1 = Format(pDpdL1.DPD_L1_Max_Table_Gain_1, "0.00")
    ' ''    '    BI_Data(slot).MeasData.DPD_L1_Min_Table_Gain_1 = Format(pDpdL1.DPD_L1_Min_Table_Gain_1, "0.00")
    ' ''    '    BI_Data(slot).MeasData.DPD_L2_3rd_Mag_0 = pDpdL2.DPD_L2_3rd_Mag_0
    ' ''    '    BI_Data(slot).MeasData.DPD_L2_3rd_Phase_0 = pDpdL2.DPD_L2_3rd_Phase_0
    ' ''    '    BI_Data(slot).MeasData.DPD_L2_5th_Mag_0 = pDpdL2.DPD_L2_5th_Mag_0
    ' ''    '    BI_Data(slot).MeasData.DPD_L2_5th_Phase_0 = pDpdL2.DPD_L2_5th_Phase_0
    ' ''    '    BI_Data(slot).MeasData.DPD_L2_3rd_Mag_1 = pDpdL2.DPD_L2_3rd_Mag_1
    ' ''    '    BI_Data(slot).MeasData.DPD_L2_3rd_Phase_1 = pDpdL2.DPD_L2_3rd_Phase_1
    ' ''    '    BI_Data(slot).MeasData.DPD_L2_5th_Mag_1 = pDpdL2.DPD_L2_5th_Mag_1
    ' ''    '    BI_Data(slot).MeasData.DPD_L2_5th_Phase_1 = pDpdL2.DPD_L2_5th_Phase_1
    ' ''    '    My.Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'Meas Version
    ' ''    '    tmpStep = "Meas Version"
    ' ''    '    'BI_Data(Slot).MeasData.CPRIFPGAVer = Transceiver(Slot).CPRIRev
    ' ''    '    BI_Data(slot).MeasData.FPGARevision = Transceiver(slot).fpgaRev
    ' ''    '    BI_Data(slot).MeasData.DSPRevision = Transceiver(slot).dspRev
    ' ''    '    BI_Data(slot).MeasData.SWRevision = Transceiver(slot).pkgRev

    ' ''    '    'Read Alarms
    ' ''    '    tmpStep = "Meas Alarms"
    ' ''    '    'Try
    ' ''    '    '    Dim tActiveAlarms As String = Transceiver(Slot).ActiveAlarms
    ' ''    '    '    'Dim tUnitState As String = Transceiver(Slot).UnitState
    ' ''    '    '    BI_Data(Slot).MeasData.AlarmString = String.Empty

    ' ''    '    '    If tActiveAlarms = "" Then
    ' ''    '    '        BI_Data(Slot).MeasData.AlarmString = "No Alarm"
    ' ''    '    '    Else
    ' ''    '    '        BI_Data(Slot).MeasData.AlarmString = BI_Data(Slot).MeasData.AlarmString & "; " & tActiveAlarms
    ' ''    '    '    End If

    ' ''    '    'Catch ex As Exception
    ' ''    '    '    BI_Data(Slot).MeasData.AlarmString = "No Alarm"
    ' ''    '    'End Try
    ' ''    '    Try
    ' ''    '        Dim AlarmArr() As String = Transceiver(slot).Alarms
    ' ''    '        BI_Data(slot).MeasData.AlarmString = String.Empty
    ' ''    '        For i As Integer = 0 To AlarmArr.Length - 1
    ' ''    '            'Ignore Unit State first
    ' ''    '            'If InStr(UCase(AlarmArr(i)), UCase("Unit State")) Then
    ' ''    '            '    If BI_Data(Slot).MeasData.PAEnabled And InStr(UCase(AlarmArr(i)), UCase("Unit State: Enable")) = 0 Then
    ' ''    '            '        BI_Data(Slot).MeasData.AlarmString = BI_Data(Slot).MeasData.AlarmString & "; " & Replace(AlarmArr(i), ",", " -")
    ' ''    '            '    End If
    ' ''    '            'End If

    ' ''    '            If InStr(UCase(AlarmArr(i)), UCase("Alarm")) Then
    ' ''    '                'If (InStr(UCase(AlarmArr(i)), UCase("Alarm 54")) + InStr(UCase(AlarmArr(i)), UCase("Alarm 12"))) = 0 Then
    ' ''    '                If (InStr(UCase(AlarmArr(i)), UCase("Alarm 54")) + _
    ' ''    '                    InStr(UCase(AlarmArr(i)), UCase("Alarm 12")) + _
    ' ''    '                    InStr(UCase(AlarmArr(i)), UCase("Alarm 41"))) = 0 Then  'Ignore Voltage Alarm before FW updated.
    ' ''    '                    BI_Data(slot).MeasData.AlarmString = BI_Data(slot).MeasData.AlarmString & "; " & Replace(AlarmArr(i), ",", " -")
    ' ''    '                End If
    ' ''    '            End If
    ' ''    '        Next

    ' ''    '        If BI_Data(slot).MeasData.AlarmString = String.Empty Then
    ' ''    '            BI_Data(slot).MeasData.AlarmString = "No Alarm"
    ' ''    '        End If

    ' ''    '        'Add RAMLOG output, Light, V1.4.2
    ' ''    '        If BI_Data(slot).MeasData.AlarmString <> "No Alarm" And BI_Data(slot).MeasData.AlarmVSRamLog = False Then
    ' ''    '            Dim tmpRam As String = Transceiver(slot).GetRAMLOG
    ' ''    '            BI_Data(slot).MeasData.AlarmVSRamLog = _
    ' ''    '                WriteRamLogFile(BI_Data(slot).UnitInfo.UnitType, BI_Data(slot).UnitInfo.UnitSN, BI_Data(slot).MeasData.AlarmString, tmpRam)
    ' ''    '        End If

    ' ''    '    Catch ex As Exception
    ' ''    '        BI_Data(slot).MeasData.AlarmString = "No Alarm"
    ' ''    '    End Try
    ' ''    '    Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'Meas PowerCycleCount
    ' ''    '    tmpStep = "Meas PowerCycleCount"
    ' ''    '    'BI_Data(Slot).MeasData.CPRIFPGAVer = Transceiver(Slot).CPRIRev
    ' ''    '    BI_Data(slot).MeasData.PowerCycleCount = Transceiver(slot).PowerCycleCount
    ' ''    '    Application.DoEvents()
    ' ''    '    Sleep(50)

    ' ''    '    'If tmp_Temp_Max >= BI_Limit.Thres_Hold_Temp Then
    ' ''    '    If tmp_PA_Temp_Max >= BI_Limit.Thres_Hold_Temp Then
    ' ''    '        BI_Data(slot).RecData.Thres_Hold_Time += CDbl((DateDiff(DateInterval.Second, BI_Data(slot).MeasData.Last_Polling_Time, BI_Data(slot).MeasData.PollingTime) / 60))
    ' ''    '    End If

    ' ''    '    If BI_Data(slot).MeasData.PAEnabled Then
    ' ''    '        BI_Data(slot).RecData.RF_On_Time += CDbl(DateDiff(DateInterval.Second, BI_Data(slot).MeasData.Last_Polling_Time, BI_Data(slot).MeasData.PollingTime) / 60)
    ' ''    '    End If
    ' ''    '    BI_Data(slot).MeasData.Last_Polling_Time = BI_Data(slot).MeasData.PollingTime

    ' ''    '    UpdateDCFData(slot)
    ' ''    '    UpdateTestReocrd(slot, ePhase)

    ' ''    '    tmpStep = "Check RF/Fan on/off temperatures"
    ' ''    '    If tmp_Temp_Max > BI_Profile.BI_Target_Temp Then
    ' ''    '        'If tmp_PA_Temp_Max > BI_Profile.BI_Target_Temp Then
    ' ''    '        Call TurnRFOnOff(slot, False)
    ' ''    '        Call TurnFanOnOff(slot, True)
    ' ''    '    Else
    ' ''    '        If BI_Data(slot).UnitInfo.Temp_Heating_Up Then
    ' ''    '            If tmp_Temp_Max < BI_Profile.BI_Target_Temp - 3 And Not BI_Data(slot).MeasData.PAEnabled Then
    ' ''    '                'If tmp_PA_Temp_Max < BI_Profile.BI_Target_Temp - 3 And Not BI_Data(Slot).MeasData.PAEnabled Then
    ' ''    '                Call TurnRFOnOff(slot, True)
    ' ''    '                Call TurnFanOnOff(slot, False)
    ' ''    '            End If
    ' ''    '        Else
    ' ''    '            If BI_Data(slot).MeasData.PAEnabled Then
    ' ''    '                Call TurnRFOnOff(slot, False)
    ' ''    '                Call TurnFanOnOff(slot, True)
    ' ''    '            End If
    ' ''    '        End If
    ' ''    '    End If

    ' ''    '    'Transceiver(Slot).Close()

    ' ''    'Catch ex As Exception
    ' ''    '    AddMessage("Slot:" & (slot + 1) & ": " & tmpStep & ": " & ex.Message)
    ' ''    '    If UCase(BI_HW.ConnectionType) = "SWITCH" Then TurnPowerOnOff(slot, False)
    ' ''    '    TurnFanOnOff(slot, False)
    ' ''    '    BI_Data(slot).MeasData.PAEnabled = False
    ' ''    '    BI_Data(slot).UnitInfo.UnitActive = False
    ' ''    '    DisplayUnitActive(slot)
    ' ''    'End Try
    ' ''End Sub
    ' ''Public Function MeasurementStep_Task(ByVal slot As Integer, ByVal ePhase As String) As CustomThread
    ' ''    Try
    ' ''        Dim MyThread As New CustomThread
    ' ''        Dim Myparams As Object() = New Object(1) {slot, ePhase}
    ' ''        MyThread.InputParameter = Myparams
    ' ''        MyThread.DelegateProcess = MeasurementStep
    ' ''        MyThread.Start()
    ' ''        Return MyThread
    ' ''    Catch ex As Exception
    ' ''        Throw New Exception(ex.Message)
    ' ''    End Try
    ' ''End Function

    Public Sub TakeMeasurement()
        Dim Stt As Date
        Dim ElapSecs As Integer
        Dim PowerOnOffCycle As Integer = 1
        Dim PowerCycle_ElapSecs As Integer = 0
        Dim PowerCycle_Stt As Date = Now

        For slot As Integer = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                KanbanRealTimeUpload(slot, "Testing")
            End If
        Next

        Do Until TimeDone
            For DoCycle As Integer = 1 To BI_Profile.BI_Cycle
                'Temperature warmming up
                AddMessage("Temperature warming up...")

                IniWarmUp()

                ElapSecs = 0
                Stt = Now
                Do Until (ElapSecs >= BI_Profile.BI_Heating_Up_Time * 60) Or TimeDone
                    PowerCycle_ElapSecs = DateDiff(DateInterval.Second, PowerCycle_Stt, Now)
                    ElapSecs = DateDiff(DateInterval.Second, Stt, Now)
                    If AbortTest Then Exit Sub
                    Application.DoEvents()
                    Measurement("Up_" & BI_Profile.BI_Target_Temp)
                    Delay(BI_Profile.BI_Polling_Interval * 1000)
                Loop

                'High Temperature Soaking
                AddMessage("Temperature Soaking...")
                ElapSecs = 0
                Stt = Now
                PowerCycle_ElapSecs = 0
                PowerCycle_Stt = Now
                Do Until (ElapSecs >= BI_Profile.BI_Soak_Time * 60) Or TimeDone
                    PowerCycle_ElapSecs = DateDiff(DateInterval.Second, PowerCycle_Stt, Now)
                    ElapSecs = DateDiff(DateInterval.Second, Stt, Now)
                    If AbortTest Then Exit Sub
                    Application.DoEvents()


                    'Check Power On Off Cycle
                    If PowerOnOffCycle <= BI_Profile.BI_Power_Cycle And BI_Profile.BI_Power_Cycle > 0 Then
                        If PowerCycle_ElapSecs > BI_Profile.BI_Power_Cycle_Interval Then
                            'V2.0.9
                            For slot As Integer = 0 To SlotNum
                                If BI_Data(slot).MeasData.RamLogSingleFlag Then WriteRamLogFileSingle(slot)
                                BI_Data(slot).MeasData.RamLogSingleFlag = False
                            Next

                            Call DoPowerCycle(PowerOnOffCycle)
                            PowerOnOffCycle += 1
                            PowerCycle_Stt = Now
                        End If
                    End If

                    Measurement("SK_" & BI_Profile.BI_Target_Temp)
                    Delay(BI_Profile.BI_Polling_Interval * 1000)

                Loop

                'Temperatre cooling down
                AddMessage("Temperature cooling down...")

                IniCoolDown()

                ElapSecs = 0
                Stt = Now
                Do Until (ElapSecs >= BI_Profile.BI_Cooling_Down_Time * 60) Or TimeDone
                    PowerCycle_ElapSecs = DateDiff(DateInterval.Second, PowerCycle_Stt, Now)
                    ElapSecs = DateDiff(DateInterval.Second, Stt, Now)
                    If AbortTest = True Then Exit Sub
                    Application.DoEvents()
                    ' Temp_Heating_Up = False
                    Measurement("CD")
                    Delay(BI_Profile.BI_Polling_Interval * 1000)
                Loop
            Next
        Loop

    End Sub

    Private Sub IniWarmUp()
        For Slot As Integer = 0 To SlotNum
            If BI_Data(Slot).UnitInfo.UnitActive Then
                Try
                    BI_Data(Slot).UnitInfo.Temp_Heating_Up = True
                    TurnFanOnOff(Slot, False)
                    TurnRFOnOff(Slot, True)
                Catch ex As Exception
                    AddMessage("Unit " & BI_Data(Slot).UnitInfo.UnitSN & " initializing warm up error: " & ex.Message)
                    If UCase(BI_HW.ConnectionType) = "SWITCH" Then TurnPowerOnOff(Slot, False)
                    TurnFanOnOff(Slot, False)
                    BI_Data(Slot).UnitInfo.UnitActive = False
                    DisplayUnitActive(Slot)
                End Try
            End If
        Next
        Delay(5000)
    End Sub

    Private Sub IniCoolDown()
        For Slot As Integer = 0 To SlotNum
            If BI_Data(Slot).UnitInfo.UnitActive Then
                Try
                    BI_Data(Slot).UnitInfo.Temp_Heating_Up = False
                    TurnFanOnOff(Slot, True)
                    TurnRFOnOff(Slot, False)
                Catch ex As Exception
                    AddMessage("Unit " & BI_Data(Slot).UnitInfo.UnitSN & " initializing cool down error: " & ex.Message)
                    If UCase(BI_HW.ConnectionType) = "SWITCH" Then TurnPowerOnOff(Slot, False)
                    TurnFanOnOff(Slot, False)
                    BI_Data(Slot).UnitInfo.UnitActive = False
                    DisplayUnitActive(Slot)
                End Try
            End If
        Next
        Delay(5000)
    End Sub

    Private Sub DoPowerCycle(ByVal Cycle As Integer)
        Dim slot As Integer
        AddMessage("Do power cycle " & Cycle & "...")

        'V2.0.4, Add to avoid AISG damage risk.
        For slot = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                Call TurnFanOnOff(slot, False)
                Call TurnRFOnOff(slot, False)
            End If
        Next

        TurnActiveUnitPowerSwitch(False)
        Delay(20000)     'V2.0.3, increase from 5 to 20 sec.
        TurnActiveUnitPowerSwitch(True)
        Delay(120000)    'V2.0.3, increase from 70 to 120 sec.

        For slot = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                Try
                    AddMessage("Initializing unit " & BI_Data(slot).UnitInfo.UnitSN)
                    Application.DoEvents()
                    Application.DoEvents()
                    Transceiver(slot).Address = BI_HW.IPAddress(slot)
                    Transceiver(slot).Open()
                    Transceiver(slot).Close_Timer()
                    'RemoveAlarm4(slot)
                    Transceiver(slot).InitBurnInCommands()
                    Dim tRetry As Integer = 0
                    Do Until Transceiver(slot).CheckSlewRate Or tRetry = 3
                        AddMessage("Check Slew Rate Byte Failed.")
                        Transceiver(slot).WriteSlewRate()
                        tRetry = tRetry + 1
                    Loop
                    BI_Data(slot).MeasData.PAEnabled = True
                    'DisplayRFOnOff(slot)
                Catch ex As Exception
                    AddMessage("Unit " & BI_Data(slot).UnitInfo.UnitSN & " Power cycle error: " & ex.Message)
                    BI_Data(slot).MeasData.PAEnabled = False
                    'DisplayRFOnOff(slot)
                    If UCase(BI_HW.ConnectionType) = "SWITCH" Then TurnPowerOnOff(slot, False)
                    TurnFanOnOff(slot, False)
                    BI_Data(slot).UnitInfo.UnitActive = False
                    DisplayUnitActive(slot)

                    KanbanRealTimeUpload(slot, "Lost")
                End Try
            End If
        Next

        Delay(5000)

        Measurement("DC_" & Cycle)
        Delay(BI_Profile.BI_Polling_Interval * 1000)
    End Sub


    Public Sub Test_Done()
        Try
            frmMain.totTestTimeOut.Stoppe()

            'TurnActiveUnitPowerSwitch(False)
            'Delay(4000)

            WriteCSVFile()
            WriteDCFFile()
            WriteDatFile()
            WriteTxtFile()
            WriteRamLogFile()

            Try
                If BI_Param.Transfer_Data And Not AbortTest Then
                    Shell("C:\ODC\TestUpdate.exe", AppWinStyle.NormalFocus, True)
                End If
            Catch ex As Exception
                AddMessage("TestUpdate Error: " & ex.Message)
            End Try

            If UCase(BI_HW.ConnectionType) = "SWITCH" Then

                TurnActiveUnitPowerSwitch(True)
                AddMessage("Waiting for change IP address back...")
                Delay(50000)

                For slot As Integer = 0 To SlotNum
                    If BI_Data(slot).UnitInfo.UnitActive Then
                        Try
                            AddMessage("Change Slot " & (slot + 1).ToString & " IP address back to 192.168.255.1 ...")
                            Transceiver(slot).Address = BI_HW.IPAddress(slot)
                            Transceiver(slot).Open()
                            'Transceiver(slot).ChangeIPAddress("192.168.255.1")
                            AddMessage("Change IP back done.")
                        Catch ex As Exception
                            Try
                                TurnPowerOnOff(slot, False)
                                Delay(4000)
                                TurnPowerOnOff(slot, True)
                                Delay(50000)
                                Transceiver(slot).Address = BI_HW.IPAddress(slot)
                                Transceiver(slot).Open()
                                'Transceiver(slot).ChangeIPAddress("192.168.255.1")
                                AddMessage("Change IP back done.")
                            Catch ex1 As Exception
                                AddMessage("Change IP back failed.")
                            End Try
                        End Try
                        Delay(1000)
                        TurnPowerOnOff(slot, False)
                        TurnFanOnOff(slot, True)
                    End If
                Next
            End If

            If BI_HW.Chamber_Control_Enabled Then
                AddMessage("Turn off Heating room...")
                ChamberOnOff(False)
            Else
                AddMessage("Please turn off the chamber!")
            End If

            For slot As Integer = 0 To SlotNum
                If BI_Data(slot).UnitInfo.UnitActive Then
                    Try
                        AddMessage("Turn Off RF: " & BI_Data(slot).UnitInfo.UnitSN)
                        TurnRFOnOff(slot, False)
                    Catch ex As Exception
                        AddMessage(ex.Message)
                    End Try
                End If
            Next

            For slot As Integer = 0 To SlotNum
                TurnFanOnOff(slot, True)
            Next

            ManualTurnPowerOnOff(False)

            TurnActiveUnitPowerSwitch(False)
            Delay(4000)

            TurnAllPSOnOff(False)

            If AbortTest Then
                AddMessage("Test was aborted!")
                TurnActiveUnitAntonSwitch(True)
                For slot As Integer = 0 To SlotNum
                    If BI_Data(slot).UnitInfo.UnitActive Then KanbanRealTimeUpload(slot, "Abort")
                Next
                Delay(100)
                MessageBox.Show("Test was aborted!", "Note", MessageBoxButtons.OK)
                TurnActiveUnitAntonSwitch(False)

                For slot As Integer = 0 To SlotNum
                    KanbanRealTimeUpload(slot, "Idle")
                Next
                Delay(100)
            Else
                AddMessage("Test completed!")
                TurnActiveUnitAntonSwitch(True)
                For slot As Integer = 0 To SlotNum
                    If BI_Data(slot).UnitInfo.UnitActive Then
                        If BI_Data(slot).RecData.AlarmFlag Then
                            KanbanRealTimeUpload(slot, "Fail")
                        Else
                            KanbanRealTimeUpload(slot, "Pass")
                        End If
                    End If
                Next
                Delay(100)
                MessageBox.Show("Test completed!", "Note", MessageBoxButtons.OK)
                TurnActiveUnitAntonSwitch(False)
                For slot As Integer = 0 To SlotNum
                    KanbanRealTimeUpload(slot, "Idle")
                Next
                Delay(100)
            End If

            For slot As Integer = 0 To SlotNum
                TurnFanOnOff(slot, False)
                TurnPowerOnOff(slot, False)
            Next

            EnableControlBtn(True)

        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub TurnActiveUnitPowerSwitch(ByVal OnOff As Boolean)
        If UCase(BI_HW.ConnectionType) = "SWITCH" Then
            For slot As Integer = 0 To SlotNum
                If BI_Data(slot).UnitInfo.UnitActive Then
                    Try
                        If Not OnOff Then Transceiver(slot).Close()
                        TurnPowerOnOff(slot, OnOff)
                    Catch ex As Exception
                        TurnPowerOnOff(slot, OnOff)
                        AddMessage(ex.Message)
                    End Try
                End If
            Next
        End If

        If UCase(BI_HW.ConnectionType) = "ROUTER" Then
            'For slot As Integer = 0 To SlotNum
            '    Try
            '        If Not OnOff Then Transceiver(slot).Close()
            '        TurnPowerOnOff(slot, OnOff)
            '    Catch ex As Exception
            '        TurnPowerOnOff(slot, OnOff)
            '        AddMessage(ex.Message)
            '    End Try
            'Next
            Try
                If Not OnOff Then
                    'V2.0.2 Temp for 23H with DC
                    'For slot As Integer = 0 To SlotNum
                    '    Transceiver(slot).Close()
                    'Next
                End If
                For slot As Integer = 0 To SlotNum
                    TurnPowerOnOff(slot, OnOff)
                Next
                ManualTurnPowerOnOff(OnOff)
            Catch ex As Exception
                AddMessage(ex.Message)
            End Try
        End If
    End Sub

    Private Sub TurnActiveUnitAntonSwitch(ByVal OnOff As Boolean)
        For slot As Integer = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                Try
                    TurnAntonOnOff(slot, OnOff)
                Catch ex As Exception
                    AddMessage(ex.Message)
                End Try
            End If
        Next
    End Sub

    Public Function ChangeGW() As Boolean

        '    For i As Integer = 0 To SlotNum
        '        TurnPowerOnOff(i, True)
        '        TurnFanOnOff(i, True)
        '    Next
        '    AddMessage("Waiting for units starting up...")
        '    Delay(50000)

        '    Dim slot As Integer = 0
        '    Try
        '        AddMessage("Changing GW...")
        '        Transceiver(slot).Address = "192.168.255.1"
        '        Transceiver(slot).Open()
        '        Transceiver(slot).Close_Timer()
        '        'Transceiver(slot).ChangeGW("192.168.255.100")
        '        Transceiver(slot).Reset()

        '        For i As Integer = 0 To SlotNum
        '            TurnPowerOnOff(i, False)
        '            TurnFanOnOff(i, False)
        '        Next

        '        AddMessage("GW Change Successed...")
        '    Catch ex As Exception
        '        AddMessage("ChangeGW : " & ex.Message)
        '    End Try
        '    Application.DoEvents()
    End Function

    Public Sub RemoveAlarm4(ByVal slot As Integer)
        'Try
        '    If Transceiver(slot).CheckAlarm4 Then
        '        Transceiver(slot).DisableAlarm4()
        '    Else
        '        Delay(5000)
        '        If Transceiver(slot).CheckAlarm4 Then Transceiver(slot).DisableAlarm4()
        '    End If
        '    If Not Transceiver(slot).CheckAlarm4 Then Return True
        'Catch ex As Exception
        '    AddMessage("RemoveAlarm4 : " & ex.Message)
        'End Try
    End Sub


    Public Sub KanbanRealTimeUpload(ByVal slot As Integer, ByVal status As String)
        Dim tStr As String
        Dim reply As String
        Dim client As New KanbanSer.BurinInService
        Dim tSlot As String
        'My.Computer.Name
        'Shelf2Slot()
        'Plant
        'frmMain.tsTestTimeTextBox.Text
        'BI_Profile.BI_Duration()
        'BI_Data(slot).RecData.AlarmFlag
        'Status => Idle, Loading, Blank, Testing, Pass, Fail, Abort, Lost
        Try
            If BI_HW.Kanban_System_Upload Then
                If Plant <> "ERROR" Then
                    tSlot = CStr(slot + 1 + Shelf2Slot)
                    Select Case tSlot
                        Case "1"
                            tSlot = "2"
                        Case "2"
                            tSlot = "4"
                        Case "3"
                            tSlot = "6"
                        Case "4"
                            tSlot = "8"
                        Case "5"
                            tSlot = "1"
                        Case "6"
                            tSlot = "3"
                        Case "7"
                            tSlot = "5"
                        Case "8"
                            tSlot = "7"
                    End Select

                    tStr = My.Computer.Name & "," & _
                              Plant & tSlot & "," & _
                              frmMain.tsTestTimeTextBox.Text & "," & _
                              CStr(BI_Profile.BI_Duration) & "," & _
                              status
                    'client.Url = "http://homeappsdev/mts/burninservices/burninservice.asmx"
                    reply = client.SentBurnInData(tStr)     'reply = "Data Sent Success!"
                    Delay(200)
                End If
            End If
        Catch ex As Exception
            'AddMessage("Kanban System Data Upload Error: " & ex.Message)
        End Try
    End Sub

End Module
