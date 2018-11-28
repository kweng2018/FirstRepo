'V1.1.2   Only update for new DLL
'V1.1.3   Only update for new DLL that update internal waveform
'V1.1.4   Modify measurement method for some items per requirement from Hou Xiaohua.
'V1.1.4   Modify SN reading to CatSN per requirement from Lu Cunyuan, for different SN format
'V1.1.5   Enable Start_Temp checking function
'V1.1.6   
'   Add command "alarm -u" before every alarms reading
'   --Old-- Record the return of "rld" (RamLog) once got alarm
'   Record the return of "rld" (RamLog) for each slot at the end of BI

'   Record RX value both RF On and Off, Modify in Function UpdateDCFData
'   Input Current related 2 items devide to RF On and Off items in DCF report.
'   Add H/L limit for Max/Min value for each test items.

'   Add item Tx0_txDacGain, Tx0_total_tx_attn, Tx1_txDacGain, Tx1_total_tx_attn, 
'     Command "rcmd dsp "txgain -a 0 -p""
'   Add item PA0_ampDelayInt, PA0_ampDelayFrac, PA0_Maximum_cross_correlation
'     PA1_ampDelayInt, PA1_ampDelayFrac, PA1_Maximum_cross_correlation
'     Command "rcmd dsp "dpd tx0 status delay""
'   Add item for dpd L2, L3, 12 items each channel
'     Command "rcmd dsp "dpd tx0 status l2""
'V1.1.7   Modify TxOnOff command
'V1.1.9   Add 3 temp delta items.
'         The 3 items only write in DCF, not display in raw data.
'V1.2.1   Trial run new temp profile per ALU purpose.
'           Target, Add RF Cycle to 24, each cycle ramp up from 60C to 80C in 11 minutes then cooling down back in 11 minutes.
'           Trial, Modify Measurement -> Temp control, Modify Target Temp from 85 to 80, BI Time from 360 min to 690 min, 
'              RF Cycle 22.5-23, each cycle ramp up from 60C to 80C in 14 minutes then cooling down back in 14 minutes.
'           Final, Modify BI Time to 720 min, BI_High_PA_Temp from 80-90 to 75-85, RF_On_Time to 300
'V1.2.2
'           Update test limit per QiaoZhi and Jason instruction
'            TX_PA_DRIVER2DAC_HIGH        LL 1450       UL 1700
'            TX_PA_DRIVER2DAC_LOW         LL 1350       UL 1650
'            TX_PA_DRIVER3DAC_HIGH        LL 1400       UL 1700
'            TX_PA_DRIVER3DAC_LOW         LL 1350       UL 1650
'            TX_PA_DRIVER4DAC_HIGH        LL 1400       UL 1700
'            TX_PA_DRIVER4DAC_LOW         LL 1360       UL 1650
'            BI_Low_TX_Gain_FbStep        LL  5.0         UL 9.0
'            BI_High_TX_Gain_FbStep       LL  5.0         UL 9.0
'V1.2.3 
'          1, Add new test parameter per Jason request.
'              BI_Delta_TX_PA_Driver1Cur      LL=-1000, UL=1000
'              BI_Delta_TX_PA_Driver2Cur      LL=-1000, UL=1000
'              BI_Delta_TX_PA_Driver3Cur      LL=-1000, UL=1000
'              BI_Delta_TX_PA_Driver4Cur      LL=-1000, UL=1000
'              BI_Delta_TX_PA_Final1Cur       LL=-1000, UL=1000
'              BI_Delta_TX_PA_Final2Cur       LL=-1000, UL=1000
'          2,  Update BI_High_Rx_Rx_Atten0 and BI_High_Rx_Rx_Atten1 test limit per Jason instruction
'              BI_High_Rx_Rx_Atten0_UL   from 12 to 31.5
'              BI_High_Rx_Rx_Atten1_UL   from 12 to 31.5
'          3, Update router IP address, support two ethernet network card for different shelf, solvel the connection lost issue while run two test program at sametime on one test computer.
'V1.2.4   Add fan control, contorled by uut itself, read 
'         Update test limit PSU_PA_TEMP_DELTA_HIGH       LL=-15, UL=+25
'                           PSU_PA_TEMP_DELTA_LOW        LL=-30, UL=+10
'                           BI_High_TX_PA_Final2Cur_LL   LL=4000 to LL=3900
'                           BI_Low_TX_PA_Final1Dac_UL    UL=200  to UL=250
'                           BI_High_Input_Current_LL     LL=8.0  to LL=7.8
'V1.2.8   Update ASIG_24V_Current Low and High Limit 
'                            <BI_Low_AISG_24V_Current_LL>0.0</BI_Low_AISG_24V_Current_LL>
'                            <BI_Low_AISG_24V_Current_UL>0.1</BI_Low_AISG_24V_Current_UL>
'                            <BI_High_AISG_24V_Current_LL>0.3</BI_High_AISG_24V_Current_LL>
'                            <BI_High_AISG_24V_Current_UL>0.6</BI_High_AISG_24V_Current_UL>
'                          
'V2.0.0   Big Change for Viper BI setting. Modify test application to met new HW setting.
'           Add Switch_9CH_Eth, Relay slot 0 for power switch control, Relay slot 1 plan for Anton lamp to highlight the slot test finished, then change to replace AISG Fan.
'           Modify each test bench setting from 4 Shelf * 4 Unit to 5 Shelf * 2 Unit.
'           Add real-time status update function for each slot to show all BI plant status in TV Kanban.
'
'V2.0.0-0627(V2.0.1)  Modify different colour for each BI shelf form.  
'           Modify PSU control tool for only Viper BI.
'           Modify limit.
'
'V2.0.4   Combine and implement temp solution in V2.0.2 and V2.0.3.
'           Cut in DC Cycle for mass run, DC each 30 minutes.
'           Modify Power Off/On dalay time for DC from 5/70 to 20/120 sec to eliminate slew rate alarm.
'           Modify dll to add reading -99999 instead of error and exit test.
'           Modify internal carrier from 465/0 to 435/435.
'           Add single ramlog if -99999 reading to record detail failure information for further analyzer.
'           Modify limit due to carrier modification.
'           Add FAN turn off function before DC to avoid AISG damage risk.           
'
'V2.0.9   Continuously update from 2.0.4.
'           Add command for slew rate issue to improve yield.
'           Modify Single Ramlog trigger mode, from -99999 reading, to -99999 reading & P_Out < 43 & All failure.
'           Modify Single Ramlog trigger time, from each polling, to before each DC cycle.
'
'V2.1.0   Enable Kanban function.
'
'V2.1.1   Disable 9CH box for AMS, directly connect each DUT to different PSU.


Public Class clsBurnInLimit
    'Common Items
    Public Thres_Hold_Time As Integer
    Public Thres_Hold_Temp As Integer
    Public RF_On_Time As Integer

    Public BI_Pre_VSWR As Double

    'General Information, Up Limit
    Public BI_High_PA_Temp_UL As Double
    Public BI_Low_PA_Temp_UL As Double
    Public BI_High_PA_VSWR_Temp_UL As Double
    Public BI_Low_PA_VSWR_Temp_UL As Double

    Public BI_PA_Temp_Delta_UL As Double     'V1.1.9

    Public BI_High_LNA01_Temp_UL As Double
    Public BI_Low_LNA01_Temp_UL As Double
    Public BI_High_LNA23_Temp_UL As Double
    Public BI_Low_LNA23_Temp_UL As Double
    Public BI_High_PS_Converter_Temp_UL As Double
    Public BI_Low_PS_Converter_Temp_UL As Double
    Public BI_High_PS_Brick_Temp_UL As Double
    Public BI_Low_PS_Brick_Temp_UL As Double

    Public BI_PSU_Temp_Delta_UL As Double     'V1.1.9
    ' Public BI_PSU_PA_Temp_Delta_UL As Double     'V1.1.9

    Public BI_High_FB_Temp_UL As Double
    Public BI_Low_FB_Temp_UL As Double
    Public BI_High_RX_Temp_UL As Double
    Public BI_Low_RX_Temp_UL As Double

    'General Information, Low Limit
    Public BI_High_PA_Temp_LL As Double
    Public BI_Low_PA_Temp_LL As Double
    Public BI_High_PA_VSWR_Temp_LL As Double
    Public BI_Low_PA_VSWR_Temp_LL As Double

    Public BI_PA_Temp_Delta_LL As Double     'V1.1.9

    Public BI_High_LNA01_Temp_LL As Double
    Public BI_Low_LNA01_Temp_LL As Double
    Public BI_High_LNA23_Temp_LL As Double
    Public BI_Low_LNA23_Temp_LL As Double
    Public BI_High_PS_Converter_Temp_LL As Double
    Public BI_Low_PS_Converter_Temp_LL As Double
    Public BI_High_PS_Brick_Temp_LL As Double
    Public BI_Low_PS_Brick_Temp_LL As Double

    Public BI_PSU_Temp_Delta_LL As Double     'V1.1.9

    Public BI_High_PSU_PA_Temp_Delta_LL As Double     'V1.2.4
    Public BI_High_PSU_PA_Temp_Delta_UL As Double     'V1.2.4
    Public BI_Low_PSU_PA_Temp_Delta_LL As Double      'V1.2.4
    Public BI_Low_PSU_PA_Temp_Delta_UL As Double      'V1.2.4

    Public BI_High_FB_Temp_LL As Double
    Public BI_Low_FB_Temp_LL As Double
    Public BI_High_RX_Temp_LL As Double
    Public BI_Low_RX_Temp_LL As Double

    'TX_High, Up Limit
    Public BI_High_TX_Output_Pow_UL As Double
    Public BI_High_TX_VSWR_UL As Double
    Public BI_High_TX_Forward_Power_Detector_UL As Double
    Public BI_High_TX_Reverse_Power_Detector_UL As Double
    Public BI_High_TX_Gain_VCA_UL As Double
    Public BI_High_TX_txDacGain_UL As Double      'V1.1.6
    Public BI_High_TX_totalTxAttn_UL As Double    'V1.1.6
    Public BI_High_TX_Gain_TxStep_UL As Double
    Public BI_High_TX_Gain_FbStep_UL As Double
    Public BI_High_TX_Gain_FbTxQuo_UL As Double
    'Public BI_High_TX_Gain_GainError As Double
    Public BI_High_TX_Gain_GainError_UL As Integer
    Public BI_High_TX_PA_PsVolt_UL As Double
    Public BI_High_TX_PA_Temp_UL As Double
    Public BI_High_TX_PA_BiasTemp_UL As Double
    Public BI_High_TX_PA_Driver1Cur_UL As Double
    Public BI_High_TX_PA_Driver2Cur_UL As Double
    Public BI_High_TX_PA_Driver3Cur_UL As Double
    Public BI_High_TX_PA_Driver4Cur_UL As Double
    Public BI_High_TX_PA_Final1Cur_UL As Double
    Public BI_High_TX_PA_Final2Cur_UL As Double
    Public BI_High_TX_PA_Driver1Dac_UL As Double
    Public BI_High_TX_PA_Driver2Dac_UL As Double
    Public BI_High_TX_PA_Driver3Dac_UL As Double
    Public BI_High_TX_PA_Driver4Dac_UL As Double
    Public BI_High_TX_PA_Final1Dac_UL As Double
    Public BI_High_TX_PA_Final2Dac_UL As Double
    Public BI_High_TX_PA_ampDelayInt_UL As Double          'V1.1.6
    Public BI_High_TX_PA_ampDelayFrac_UL As Double         'V1.1.6
    Public BI_High_TX_PA_MaxCrossCorrelation_UL As Double  'V1.1.6
    Public BI_High_Tx_DPD_L1_Table_Max_Gain_UL As Double
    Public BI_High_Tx_DPD_L1_Table_Min_Gain_UL As Double
    Public BI_High_Tx_DPD_L2_3rd_sym_am_UL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L2_3rd_sym_ph_UL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L3_3rd_sym_am_UL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L3_3rd_sym_ph_UL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L2_5th_sym_am_UL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L2_5th_sym_ph_UL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L3_5th_sym_am_UL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L3_5th_sym_ph_UL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L2_3rd_asym_am_UL As Double        'V1.1.6
    Public BI_High_Tx_DPD_L2_3rd_asym_ph_UL As Double        'V1.1.6
    Public BI_High_Tx_DPD_L2_5th_asym_am_UL As Double        'V1.1.6
    Public BI_High_Tx_DPD_L2_5th_asym_ph_UL As Double        'V1.1.6

    'TX_High, Low Limit
    Public BI_High_TX_Output_Pow_LL As Double
    Public BI_High_TX_VSWR_LL As Double
    Public BI_High_TX_Forward_Power_Detector_LL As Double
    Public BI_High_TX_Reverse_Power_Detector_LL As Double
    Public BI_High_TX_Gain_VCA_LL As Double
    Public BI_High_TX_txDacGain_LL As Double      'V1.1.6
    Public BI_High_TX_totalTxAttn_LL As Double    'V1.1.6
    Public BI_High_TX_Gain_TxStep_LL As Double
    Public BI_High_TX_Gain_FbStep_LL As Double
    Public BI_High_TX_Gain_FbTxQuo_LL As Double
    'Public BI_High_TX_Gain_GainError As Double
    Public BI_High_TX_Gain_GainError_LL As Integer
    Public BI_High_TX_PA_PsVolt_LL As Double
    Public BI_High_TX_PA_Temp_LL As Double
    Public BI_High_TX_PA_BiasTemp_LL As Double
    Public BI_High_TX_PA_Driver1Cur_LL As Double
    Public BI_High_TX_PA_Driver2Cur_LL As Double
    Public BI_High_TX_PA_Driver3Cur_LL As Double
    Public BI_High_TX_PA_Driver4Cur_LL As Double
    Public BI_High_TX_PA_Final1Cur_LL As Double
    Public BI_High_TX_PA_Final2Cur_LL As Double
    Public BI_High_TX_PA_Driver1Dac_LL As Double
    Public BI_High_TX_PA_Driver2Dac_LL As Double
    Public BI_High_TX_PA_Driver3Dac_LL As Double
    Public BI_High_TX_PA_Driver4Dac_LL As Double
    Public BI_High_TX_PA_Final1Dac_LL As Double
    Public BI_High_TX_PA_Final2Dac_LL As Double
    Public BI_High_TX_PA_ampDelayInt_LL As Double          'V1.1.6
    Public BI_High_TX_PA_ampDelayFrac_LL As Double         'V1.1.6
    Public BI_High_TX_PA_MaxCrossCorrelation_LL As Double  'V1.1.6
    Public BI_High_Tx_DPD_L1_Table_Max_Gain_LL As Double
    Public BI_High_Tx_DPD_L1_Table_Min_Gain_LL As Double
    Public BI_High_Tx_DPD_L2_3rd_sym_am_LL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L2_3rd_sym_ph_LL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L3_3rd_sym_am_LL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L3_3rd_sym_ph_LL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L2_5th_sym_am_LL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L2_5th_sym_ph_LL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L3_5th_sym_am_LL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L3_5th_sym_ph_LL As Double         'V1.1.6
    Public BI_High_Tx_DPD_L2_3rd_asym_am_LL As Double        'V1.1.6
    Public BI_High_Tx_DPD_L2_3rd_asym_ph_LL As Double        'V1.1.6
    Public BI_High_Tx_DPD_L2_5th_asym_am_LL As Double        'V1.1.6
    Public BI_High_Tx_DPD_L2_5th_asym_ph_LL As Double        'V1.1.6

    'TX_Low, UL
    Public BI_Low_TX_Output_Pow_UL As Double
    Public BI_Low_TX_VSWR_UL As Double
    Public BI_Low_TX_Forward_Power_Detector_UL As Double
    Public BI_Low_TX_Reverse_Power_Detector_UL As Double
    Public BI_Low_TX_Gain_VCA_UL As Double
    Public BI_Low_TX_txDacGain_UL As Double      'V1.1.6
    Public BI_Low_TX_totalTxAttn_UL As Double    'V1.1.6
    Public BI_Low_TX_Gain_TxStep_UL As Double
    Public BI_Low_TX_Gain_FbStep_UL As Double
    Public BI_Low_TX_Gain_FbTxQuo_UL As Double
    'Public BI_Low_TX_Gain_GainError As Double
    Public BI_Low_TX_Gain_GainError_UL As Integer
    Public BI_Low_TX_PA_PsVolt_UL As Double
    Public BI_Low_TX_PA_Temp_UL As Double
    Public BI_Low_TX_PA_BiasTemp_UL As Double
    Public BI_Low_TX_PA_Driver1Cur_UL As Double
    Public BI_Low_TX_PA_Driver2Cur_UL As Double
    Public BI_Low_TX_PA_Driver3Cur_UL As Double
    Public BI_Low_TX_PA_Driver4Cur_UL As Double
    Public BI_Low_TX_PA_Final1Cur_UL As Double
    Public BI_Low_TX_PA_Final2Cur_UL As Double
    Public BI_Low_TX_PA_Driver1Dac_UL As Double
    Public BI_Low_TX_PA_Driver2Dac_UL As Double
    Public BI_Low_TX_PA_Driver3Dac_UL As Double
    Public BI_Low_TX_PA_Driver4Dac_UL As Double
    Public BI_Low_TX_PA_Final1Dac_UL As Double
    Public BI_Low_TX_PA_Final2Dac_UL As Double
    Public BI_Low_TX_PA_ampDelayInt_UL As Double          'V1.1.6
    Public BI_Low_TX_PA_ampDelayFrac_UL As Double         'V1.1.6
    Public BI_Low_TX_PA_MaxCrossCorrelation_UL As Double  'V1.1.6
    Public BI_Low_Tx_DPD_L1_Table_Max_Gain_UL As Double
    Public BI_Low_Tx_DPD_L1_Table_Min_Gain_UL As Double
    Public BI_Low_Tx_DPD_L2_3rd_sym_am_UL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L2_3rd_sym_ph_UL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L3_3rd_sym_am_UL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L3_3rd_sym_ph_UL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L2_5th_sym_am_UL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L2_5th_sym_ph_UL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L3_5th_sym_am_UL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L3_5th_sym_ph_UL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L2_3rd_asym_am_UL As Double        'V1.1.6
    Public BI_Low_Tx_DPD_L2_3rd_asym_ph_UL As Double        'V1.1.6
    Public BI_Low_Tx_DPD_L2_5th_asym_am_UL As Double        'V1.1.6
    Public BI_Low_Tx_DPD_L2_5th_asym_ph_UL As Double        'V1.1.6

    'TX_Low, LL
    Public BI_Low_TX_Output_Pow_LL As Double
    Public BI_Low_TX_VSWR_LL As Double
    Public BI_Low_TX_Forward_Power_Detector_LL As Double
    Public BI_Low_TX_Reverse_Power_Detector_LL As Double
    Public BI_Low_TX_Gain_VCA_LL As Double
    Public BI_Low_TX_txDacGain_LL As Double      'V1.1.6
    Public BI_Low_TX_totalTxAttn_LL As Double    'V1.1.6
    Public BI_Low_TX_Gain_TxStep_LL As Double
    Public BI_Low_TX_Gain_FbStep_LL As Double
    Public BI_Low_TX_Gain_FbTxQuo_LL As Double
    'Public BI_Low_TX_Gain_GainError As Double
    Public BI_Low_TX_Gain_GainError_LL As Integer
    Public BI_Low_TX_PA_PsVolt_LL As Double
    Public BI_Low_TX_PA_Temp_LL As Double
    Public BI_Low_TX_PA_BiasTemp_LL As Double
    Public BI_Low_TX_PA_Driver1Cur_LL As Double
    Public BI_Low_TX_PA_Driver2Cur_LL As Double
    Public BI_Low_TX_PA_Driver3Cur_LL As Double
    Public BI_Low_TX_PA_Driver4Cur_LL As Double
    Public BI_Low_TX_PA_Final1Cur_LL As Double
    Public BI_Low_TX_PA_Final2Cur_LL As Double
    Public BI_Low_TX_PA_Driver1Dac_LL As Double
    Public BI_Low_TX_PA_Driver2Dac_LL As Double
    Public BI_Low_TX_PA_Driver3Dac_LL As Double
    Public BI_Low_TX_PA_Driver4Dac_LL As Double
    Public BI_Low_TX_PA_Final1Dac_LL As Double
    Public BI_Low_TX_PA_Final2Dac_LL As Double
    Public BI_Low_TX_PA_ampDelayInt_LL As Double          'V1.1.6
    Public BI_Low_TX_PA_ampDelayFrac_LL As Double         'V1.1.6
    Public BI_Low_TX_PA_MaxCrossCorrelation_LL As Double  'V1.1.6
    Public BI_Low_Tx_DPD_L1_Table_Max_Gain_LL As Double
    Public BI_Low_Tx_DPD_L1_Table_Min_Gain_LL As Double
    Public BI_Low_Tx_DPD_L2_3rd_sym_am_LL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L2_3rd_sym_ph_LL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L3_3rd_sym_am_LL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L3_3rd_sym_ph_LL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L2_5th_sym_am_LL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L2_5th_sym_ph_LL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L3_5th_sym_am_LL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L3_5th_sym_ph_LL As Double         'V1.1.6
    Public BI_Low_Tx_DPD_L2_3rd_asym_am_LL As Double        'V1.1.6
    Public BI_Low_Tx_DPD_L2_3rd_asym_ph_LL As Double        'V1.1.6
    Public BI_Low_Tx_DPD_L2_5th_asym_am_LL As Double        'V1.1.6
    Public BI_Low_Tx_DPD_L2_5th_asym_ph_LL As Double        'V1.1.6

    'RX_High, UL
    Public BI_High_Rx_LNA_Atten_UL As Double
    Public BI_High_Rx_Rx_Atten0_UL As Double
    Public BI_High_Rx_Rx_Atten1_UL As Double
    Public BI_High_Rx_RSSI_C0_UL As Double
    Public BI_High_Rx_RSSI_C1_UL As Double

    'RX_High, LL
    Public BI_High_Rx_LNA_Atten_LL As Double
    Public BI_High_Rx_Rx_Atten0_LL As Double
    Public BI_High_Rx_Rx_Atten1_LL As Double
    Public BI_High_Rx_RSSI_C0_LL As Double
    Public BI_High_Rx_RSSI_C1_LL As Double

    'RX_Low, UL
    Public BI_Low_Rx_LNA_Atten_UL As Double
    Public BI_Low_Rx_Rx_Atten0_UL As Double
    Public BI_Low_Rx_Rx_Atten1_UL As Double
    Public BI_Low_Rx_RSSI_C0_UL As Double
    Public BI_Low_Rx_RSSI_C1_UL As Double

    'RX_Low, LL
    Public BI_Low_Rx_LNA_Atten_LL As Double
    Public BI_Low_Rx_Rx_Atten0_LL As Double
    Public BI_Low_Rx_Rx_Atten1_LL As Double
    Public BI_Low_Rx_RSSI_C0_LL As Double
    Public BI_Low_Rx_RSSI_C1_LL As Double

    'PS_High, UL
    Public BI_High_Input_Voltage_UL As Double
    Public BI_High_Input_Current_UL As Double
    Public BI_High_Input_Power_UL As Double
    Public BI_High_Output_Voltage_UL As Double
    Public BI_High_AISG_12V_Voltage_UL As Double
    Public BI_High_AISG_12V_Current_UL As Double
    Public BI_High_AISG_24V_Voltage_UL As Double
    Public BI_High_AISG_24V_Current_UL As Double

    'PS_High, LL
    Public BI_High_Input_Voltage_LL As Double
    Public BI_High_Input_Current_LL As Double
    Public BI_High_Input_Power_LL As Double
    Public BI_High_Output_Voltage_LL As Double
    Public BI_High_AISG_12V_Voltage_LL As Double
    Public BI_High_AISG_12V_Current_LL As Double
    Public BI_High_AISG_24V_Voltage_LL As Double
    Public BI_High_AISG_24V_Current_LL As Double

    'PS_Low, UL
    Public BI_Low_Input_Voltage_UL As Double
    Public BI_Low_Input_Current_UL As Double
    Public BI_Low_Input_Power_UL As Double
    Public BI_Low_Output_Voltage_UL As Double
    Public BI_Low_AISG_12V_Voltage_UL As Double
    Public BI_Low_AISG_12V_Current_UL As Double
    Public BI_Low_AISG_24V_Voltage_UL As Double
    Public BI_Low_AISG_24V_Current_UL As Double

    'PS_Low, LL
    Public BI_Low_Input_Voltage_LL As Double
    Public BI_Low_Input_Current_LL As Double
    Public BI_Low_Input_Power_LL As Double
    Public BI_Low_Output_Voltage_LL As Double
    Public BI_Low_AISG_12V_Voltage_LL As Double
    Public BI_Low_AISG_12V_Current_LL As Double
    Public BI_Low_AISG_24V_Voltage_LL As Double
    Public BI_Low_AISG_24V_Current_LL As Double

    'Delta CAM current

    Public BI_Delta_TX_PA_Driver1Cur_UL As Double
    Public BI_Delta_TX_PA_Driver1Cur_LL As Double
    Public BI_Delta_TX_PA_Driver2Cur_UL As Double
    Public BI_Delta_TX_PA_Driver2Cur_LL As Double
    Public BI_Delta_TX_PA_Driver3Cur_UL As Double
    Public BI_Delta_TX_PA_Driver3Cur_LL As Double
    Public BI_Delta_TX_PA_Driver4Cur_UL As Double
    Public BI_Delta_TX_PA_Driver4Cur_LL As Double
    Public BI_Delta_TX_PA_Final1Cur_UL As Double
    Public BI_Delta_TX_PA_Final1Cur_LL As Double
    Public BI_Delta_TX_PA_Final2Cur_UL As Double
    Public BI_Delta_TX_PA_Final2Cur_LL As Double

End Class

Public Class clsParamters
    Public ODC_Check As Boolean
    Public Transfer_Data As Boolean
    Public File_Save_Path As String
    'Public Start_Temp As Integer
End Class

Public Class clsBurnInHWConfig
    Public Plant As String
    Public PlantPC As String

    Public Switch_9CH_EthPort As String = "20108"

    Public Pwr_Chan_Enabled As Boolean
    Public Pwr_Switch_Type As String
    Public Pwr_Port_Number As Integer
    'Public Pwr_Chan(SlotNum) As Integer
    Public Pwr_Chan(SlotNum) As String

    Public Anton_Chan_Enabled As Boolean
    Public Anton_Switch_Type As String
    Public Anton_Port_Number As Integer
    Public Anton_Chan(SlotNum) As String

    Public Fan_Chan_Enabled As Boolean
    Public Fan_Switch_Type As String
    Public Fan_Port_Number As Integer
    'Public Fan_Chan(SlotNum) As Integer
    Public Fan_Chan(SlotNum) As String

    Public PS_Control_Enabled(5) As Boolean
    Public PS_Control_Type(5) As String
    Public PS_Control_Address(5) As String
    Public PS_Voltage(5) As Integer
    Public PS_Current(5) As Integer

    'Power Switch control function of Net-Switch
    Public Switch_PS_Enabled As Boolean
    Public Switch_PS_Type As String
    Public Switch_PS_COM_Port As Integer
    Public Switch_PS_Channel As Integer

    Public PS_Manual_Control As Boolean

    Public Kanban_System_Upload As Boolean

    Public Chamber_Control_Enabled As Boolean
    Public Chamber_Run_Pattern As Integer
    Public Chamber_Port_Number As Integer
    Public Chamber_Step(9) As ChamberStep
    Public Structure ChamberStep
        Public Temp As Integer
        Public Time As String
        Public PID As Integer
    End Structure

    Public ConnectionType As String
    Public IPAddress(SlotNum) As String
End Class

Public Class clsBurnInProfile
    Public BI_Start_Temp As Integer
    Public BI_Duration As Integer
    Public BI_Cycle As Integer
    Public BI_Polling_Interval As Integer
    Public BI_Power_Cycle As Integer
    Public BI_Power_Cycle_Interval As Integer
    Public BI_Target_Temp As Integer
    Public BI_Heating_Up_Time As Integer
    Public BI_Soak_Time As Integer
    Public BI_Cooling_Down_Time As Integer
End Class


Public Class clsBurnInData

    Public UnitInfo As UnitInformation
    Public MeasData As MeasurementData
    Public RecData As RecordData

    Public Structure UnitInformation
        Public UnitActive As Boolean
        Public UnitSN As String
        Public CSVFilePath As String
        Public CSVFileName As String
        Public FileName As String
        Public DATFilePath As String
        Public TXTFilePath As String
        Public JPGFilePath As String
        Public PowerOnOff As Boolean
        Public FanOnOff As Boolean
        Public Temp_Heating_Up As Boolean
        Public UnitType As String
        Public StartTemperature As Integer
    End Structure

    Public Structure MeasurementData
        Public PollingTime As Date
        Public Phase As String

        'General Information
        Public PA0_Temp As Double
        Public PA0_VSWR_Temp As Double
        Public PA1_Temp As Double
        Public PA1_VSWR_Temp As Double
        Public PA_Temp_Delta As Double     'V1.1.9
        Public LNA0_Temp As Double
        Public LNA1_Temp As Double
        Public LNA2_Temp As Double
        Public LNA3_Temp As Double
        Public PS_Converter_Temp As Double
        Public PS_Brick_Temp As Double
        Public PSU_Temp_Delta As Double     'V1.1.9
        Public PSU_PA_Temp_Delta As Double     'V1.1.9
        Public FB_Temp As Double
        Public RX_Temp As Double

        'TX0
        Public TX0_Output_Pow As Double
        Public TX0_VSWR As Double
        Public TX0_Forward_Power_Detector As Double
        Public TX0_Reverse_Power_Detector As Double
        Public TX0_Gain_VCA As Double
        Public TX0_txDacGain As Double      'V1.1.6
        Public TX0_totalTxAttn As Double    'V1.1.6
        Public TX0_Gain_TxStep As Double
        Public TX0_Gain_FbStep As Double
        Public TX0_Gain_FbTxQuo As Double
        'Public TX0_Gain_GainError As Double
        Public TX0_Gain_GainError As Integer
        Public TX0_PA0_PsVolt As Double
        Public TX0_PA0_Temp As Double
        Public TX0_PA0_BiasTemp As Double      'Rename from CamTemp to BiasTemp, V1.1.1
        Public TX0_PA0_Driver1Cur As Double
        Public TX0_PA0_Driver2Cur As Double
        Public TX0_PA0_Driver3Cur As Double
        Public TX0_PA0_Driver4Cur As Double
        Public TX0_PA0_Final1Cur As Double
        Public TX0_PA0_Final2Cur As Double
        Public TX0_PA0_Driver1Dac As Double
        Public TX0_PA0_Driver2Dac As Double
        Public TX0_PA0_Driver3Dac As Double
        Public TX0_PA0_Driver4Dac As Double
        Public TX0_PA0_Final1Dac As Double
        Public TX0_PA0_Final2Dac As Double
        Public TX0_PA0_ampDelayInt As Double          'V1.1.6
        Public TX0_PA0_ampDelayFrac As Double         'V1.1.6
        Public TX0_PA0_MaxCrossCorrelation As Double  'V1.1.6
        Public Tx0_DPD_L1_Table_Max_Gain As Double
        Public Tx0_DPD_L1_Table_Min_Gain As Double
        Public Tx0_DPD_L2_3rd_sym_am As Double         'V1.1.6
        Public Tx0_DPD_L2_3rd_sym_ph As Double         'V1.1.6
        Public Tx0_DPD_L3_3rd_sym_am As Double         'V1.1.6
        Public Tx0_DPD_L3_3rd_sym_ph As Double         'V1.1.6
        Public Tx0_DPD_L2_5th_sym_am As Double         'V1.1.6
        Public Tx0_DPD_L2_5th_sym_ph As Double         'V1.1.6
        Public Tx0_DPD_L3_5th_sym_am As Double         'V1.1.6
        Public Tx0_DPD_L3_5th_sym_ph As Double         'V1.1.6
        Public Tx0_DPD_L2_3rd_asym_am As Double        'V1.1.6
        Public Tx0_DPD_L2_3rd_asym_ph As Double        'V1.1.6
        Public Tx0_DPD_L2_5th_asym_am As Double        'V1.1.6
        Public Tx0_DPD_L2_5th_asym_ph As Double        'V1.1.6

        'TX1
        Public TX1_Output_Pow As Double
        Public TX1_VSWR As Double
        Public TX1_Forward_Power_Detector As Double
        Public TX1_Reverse_Power_Detector As Double
        Public TX1_Gain_VCA As Double
        Public TX1_txDacGain As Double      'V1.1.6
        Public TX1_totalTxAttn As Double    'V1.1.6
        Public TX1_Gain_TxStep As Double
        Public TX1_Gain_FbStep As Double
        Public TX1_Gain_FbTxQuo As Double
        'Public TX1_Gain_GainError As Double
        Public TX1_Gain_GainError As Integer
        Public TX1_PA1_PsVolt As Double
        Public TX1_PA1_Temp As Double
        Public TX1_PA1_BiasTemp As Double
        Public TX1_PA1_Driver1Cur As Double
        Public TX1_PA1_Driver2Cur As Double
        Public TX1_PA1_Driver3Cur As Double
        Public TX1_PA1_Driver4Cur As Double
        Public TX1_PA1_Final1Cur As Double
        Public TX1_PA1_Final2Cur As Double
        Public TX1_PA1_Driver1Dac As Double
        Public TX1_PA1_Driver2Dac As Double
        Public TX1_PA1_Driver3Dac As Double
        Public TX1_PA1_Driver4Dac As Double
        Public TX1_PA1_Final1Dac As Double
        Public TX1_PA1_Final2Dac As Double
        Public TX1_PA1_ampDelayInt As Double          'V1.1.6
        Public TX1_PA1_ampDelayFrac As Double         'V1.1.6
        Public TX1_PA1_MaxCrossCorrelation As Double  'V1.1.6
        Public Tx1_DPD_L1_Table_Max_Gain As Double
        Public Tx1_DPD_L1_Table_Min_Gain As Double
        Public Tx1_DPD_L2_3rd_sym_am As Double         'V1.1.6
        Public Tx1_DPD_L2_3rd_sym_ph As Double         'V1.1.6
        Public Tx1_DPD_L3_3rd_sym_am As Double         'V1.1.6
        Public Tx1_DPD_L3_3rd_sym_ph As Double         'V1.1.6
        Public Tx1_DPD_L2_5th_sym_am As Double         'V1.1.6
        Public Tx1_DPD_L2_5th_sym_ph As Double         'V1.1.6
        Public Tx1_DPD_L3_5th_sym_am As Double         'V1.1.6
        Public Tx1_DPD_L3_5th_sym_ph As Double         'V1.1.6
        Public Tx1_DPD_L2_3rd_asym_am As Double        'V1.1.6
        Public Tx1_DPD_L2_3rd_asym_ph As Double        'V1.1.6
        Public Tx1_DPD_L2_5th_asym_am As Double        'V1.1.6
        Public Tx1_DPD_L2_5th_asym_ph As Double        'V1.1.6

        'RX0
        Public Rx0_LNA_Atten As Double
        Public Rx0_Rx_Atten0 As Double
        Public Rx0_Rx_Atten1 As Double
        Public Rx0_RSSI_C0 As Double
        Public Rx0_RSSI_C1 As Double

        'RX1
        Public Rx1_LNA_Atten As Double
        Public Rx1_Rx_Atten0 As Double
        Public Rx1_Rx_Atten1 As Double
        Public Rx1_RSSI_C0 As Double
        Public Rx1_RSSI_C1 As Double

        'RX2
        Public Rx2_LNA_Atten As Double
        Public Rx2_Rx_Atten0 As Double
        Public Rx2_Rx_Atten1 As Double
        Public Rx2_RSSI_C0 As Double
        Public Rx2_RSSI_C1 As Double

        'RX3
        Public Rx3_LNA_Atten As Double
        Public Rx3_Rx_Atten0 As Double
        Public Rx3_Rx_Atten1 As Double
        Public Rx3_RSSI_C0 As Double
        Public Rx3_RSSI_C1 As Double

        'PS
        Public Input_Voltage As Double
        Public Input_Current As Double
        Public Input_Power As Double
        Public Output_Voltage As Double
        Public AISG_12V_Voltage As Double
        Public AISG_12V_Current As Double
        Public AISG_24V_Voltage As Double
        Public AISG_24V_Current As Double

        'Other
        Public RF_Status As Boolean
        Public Last_Polling_Time As Date

        Public DSPRevision As String
        Public FPGARevision As String
        Public SWRevision As String
        'Public CPRIFPGAVer As String

        Public AlarmString As String
        Public RamLog As String     'V1.1.6
        Public RamLogSingle As String
        Public RamLogSingleFlag As Boolean

        Public PowerCycleCount As Integer

        Public PAEnabled As Boolean

        'Public RetryConnectCount As Integer
        'Public ACPR_Low As Double
        'Public ACPR_High As Double
    End Structure

    Public Structure RecordData
        'Public First_Polling As Boolean
        'Public First_Polling_Final2_Current As Double
        'Public High_Temp_Final2_Current As Double
        Public StartTime As Date
        Public SerialNumber As String
        Public AlarmFlag As Boolean

        'General Information
        Public PA0_Temp As High_Low
        Public PA0_VSWR_Temp As High_Low
        Public PA1_Temp As High_Low
        Public PA1_VSWR_Temp As High_Low
        Public PA_Temp_Delta As High_Low     'V1.1.9
        Public LNA0_Temp As High_Low
        Public LNA1_Temp As High_Low
        Public LNA2_Temp As High_Low
        Public LNA3_Temp As High_Low
        Public PS_Converter_Temp As High_Low
        Public PS_Brick_Temp As High_Low
        Public PSU_Temp_Delta As High_Low     'V1.1.9
        Public PSU_PA_Temp_Delta As High_Low     'V1.1.9
        Public FB_Temp As High_Low
        Public RX_Temp As High_Low

        'TX0
        Public TX0_Output_Pow As High_Low
        Public TX0_VSWR As High_Low
        Public TX0_Forward_Power_Detector As High_Low
        Public TX0_Reverse_Power_Detector As High_Low
        Public TX0_Gain_VCA As High_Low
        Public TX0_txDacGain As High_Low      'V1.1.6
        Public TX0_totalTxAttn As High_Low    'V1.1.6
        Public TX0_Gain_TxStep As High_Low
        Public TX0_Gain_FbStep As High_Low
        Public TX0_Gain_FbTxQuo As High_Low
        Public TX0_Gain_GainError As High_Low
        Public TX0_PA0_PsVolt As High_Low
        Public TX0_PA0_Temp As High_Low
        Public TX0_PA0_BiasTemp As High_Low
        Public TX0_PA0_Driver1Cur As High_Low
        Public TX0_PA0_Driver2Cur As High_Low
        Public TX0_PA0_Driver3Cur As High_Low
        Public TX0_PA0_Driver4Cur As High_Low
        Public TX0_PA0_Final1Cur As High_Low
        Public TX0_PA0_Final2Cur As High_Low
        Public TX0_PA0_Driver1Dac As High_Low
        Public TX0_PA0_Driver2Dac As High_Low
        Public TX0_PA0_Driver3Dac As High_Low
        Public TX0_PA0_Driver4Dac As High_Low
        Public TX0_PA0_Final1Dac As High_Low
        Public TX0_PA0_Final2Dac As High_Low
        Public TX0_PA0_ampDelayInt As High_Low          'V1.1.6
        Public TX0_PA0_ampDelayFrac As High_Low         'V1.1.6
        Public TX0_PA0_MaxCrossCorrelation As High_Low  'V1.1.6
        Public TX0_DPD_L1_Table_Max_Gain As High_Low
        Public TX0_DPD_L1_Table_Min_Gain As High_Low
        Public Tx0_DPD_L2_3rd_sym_am As High_Low         'V1.1.6
        Public Tx0_DPD_L2_3rd_sym_ph As High_Low         'V1.1.6
        Public Tx0_DPD_L3_3rd_sym_am As High_Low         'V1.1.6
        Public Tx0_DPD_L3_3rd_sym_ph As High_Low         'V1.1.6
        Public Tx0_DPD_L2_5th_sym_am As High_Low         'V1.1.6
        Public Tx0_DPD_L2_5th_sym_ph As High_Low         'V1.1.6
        Public Tx0_DPD_L3_5th_sym_am As High_Low         'V1.1.6
        Public Tx0_DPD_L3_5th_sym_ph As High_Low         'V1.1.6
        Public Tx0_DPD_L2_3rd_asym_am As High_Low        'V1.1.6
        Public Tx0_DPD_L2_3rd_asym_ph As High_Low        'V1.1.6
        Public Tx0_DPD_L2_5th_asym_am As High_Low        'V1.1.6
        Public Tx0_DPD_L2_5th_asym_ph As High_Low        'V1.1.6

        'TX1
        Public TX1_Output_Pow As High_Low
        Public TX1_VSWR As High_Low
        Public TX1_Forward_Power_Detector As High_Low
        Public TX1_Reverse_Power_Detector As High_Low
        Public TX1_Gain_VCA As High_Low
        Public TX1_txDacGain As High_Low      'V1.1.6
        Public TX1_totalTxAttn As High_Low    'V1.1.6
        Public TX1_Gain_TxStep As High_Low
        Public TX1_Gain_FbStep As High_Low
        Public TX1_Gain_FbTxQuo As High_Low
        Public TX1_Gain_GainError As High_Low
        Public TX1_PA1_PsVolt As High_Low
        Public TX1_PA1_Temp As High_Low
        Public TX1_PA1_BiasTemp As High_Low
        Public TX1_PA1_Driver1Cur As High_Low
        Public TX1_PA1_Driver2Cur As High_Low
        Public TX1_PA1_Driver3Cur As High_Low
        Public TX1_PA1_Driver4Cur As High_Low
        Public TX1_PA1_Final1Cur As High_Low
        Public TX1_PA1_Final2Cur As High_Low
        Public TX1_PA1_Driver1Dac As High_Low
        Public TX1_PA1_Driver2Dac As High_Low
        Public TX1_PA1_Driver3Dac As High_Low
        Public TX1_PA1_Driver4Dac As High_Low
        Public TX1_PA1_Final1Dac As High_Low
        Public TX1_PA1_Final2Dac As High_Low
        Public TX1_PA1_ampDelayInt As High_Low          'V1.1.6
        Public TX1_PA1_ampDelayFrac As High_Low         'V1.1.6
        Public TX1_PA1_MaxCrossCorrelation As High_Low  'V1.1.6
        Public TX1_DPD_L1_Table_Max_Gain As High_Low
        Public TX1_DPD_L1_Table_Min_Gain As High_Low
        Public Tx1_DPD_L2_3rd_sym_am As High_Low         'V1.1.6
        Public Tx1_DPD_L2_3rd_sym_ph As High_Low         'V1.1.6
        Public Tx1_DPD_L3_3rd_sym_am As High_Low         'V1.1.6
        Public Tx1_DPD_L3_3rd_sym_ph As High_Low         'V1.1.6
        Public Tx1_DPD_L2_5th_sym_am As High_Low         'V1.1.6
        Public Tx1_DPD_L2_5th_sym_ph As High_Low         'V1.1.6
        Public Tx1_DPD_L3_5th_sym_am As High_Low         'V1.1.6
        Public Tx1_DPD_L3_5th_sym_ph As High_Low         'V1.1.6
        Public Tx1_DPD_L2_3rd_asym_am As High_Low        'V1.1.6
        Public Tx1_DPD_L2_3rd_asym_ph As High_Low        'V1.1.6
        Public Tx1_DPD_L2_5th_asym_am As High_Low        'V1.1.6
        Public Tx1_DPD_L2_5th_asym_ph As High_Low        'V1.1.6

        'RX0
        Public Rx0_LNA_Atten As High_Low
        Public Rx0_Rx_Atten0 As High_Low
        Public Rx0_Rx_Atten1 As High_Low
        Public Rx0_RSSI_C0 As High_Low
        Public Rx0_RSSI_C1 As High_Low

        'RX1
        Public Rx1_LNA_Atten As High_Low
        Public Rx1_Rx_Atten0 As High_Low
        Public Rx1_Rx_Atten1 As High_Low
        Public Rx1_RSSI_C0 As High_Low
        Public Rx1_RSSI_C1 As High_Low

        'RX2
        Public Rx2_LNA_Atten As High_Low
        Public Rx2_Rx_Atten0 As High_Low
        Public Rx2_Rx_Atten1 As High_Low
        Public Rx2_RSSI_C0 As High_Low
        Public Rx2_RSSI_C1 As High_Low

        'RX3
        Public Rx3_LNA_Atten As High_Low
        Public Rx3_Rx_Atten0 As High_Low
        Public Rx3_Rx_Atten1 As High_Low
        Public Rx3_RSSI_C0 As High_Low
        Public Rx3_RSSI_C1 As High_Low

        'PS
        Public Input_Voltage As High_Low
        Public Input_Current_HP As High_Low
        Public Input_Current_LP As High_Low
        Public Input_Power_HP As High_Low
        Public Input_Power_LP As High_Low
        Public Output_Voltage As High_Low
        Public AISG_12V_Voltage As High_Low
        Public AISG_12V_Current As High_Low
        Public AISG_24V_Voltage As High_Low
        Public AISG_24V_Current As High_Low

        Public RF_On_Time As Double
        Public Thres_Hold_Time As Double

        Public Last_DSPRevision As String
        Public Last_FPGARevision As String
        Public Last_SWRevision As String
        'Public Last_CPRIFPGAVer As String
        Public DSPRevision As String
        Public FPGARevision As String
        Public SWRevision As String
        'Public CPRIFPGAVer As String

        Public FailureReason As String
        Public AlarmString As String
        Public PowerCycleCount As Integer

        'Public ACPR As High_Low

        'Public RetryCount As Integer
    End Structure
    Public Structure High_Low
        Public High As Double
        Public Low As Double
        Public Delta As Double
    End Structure
End Class
