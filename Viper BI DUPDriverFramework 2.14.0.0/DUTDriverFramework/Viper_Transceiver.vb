Imports System.Net.Sockets
Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading.Thread

Public Class Viper_Transceiver
    Implements IDeviceUnderTest

    Public Structure InvData
        Public Serial As String
        Public FunctionCode As String
        Public KsNumber As String
        Public KsVersion As String
        Public ComCode As String
        Public CLEI As String
        Public ECI As String
        Public Misc As String
        Public GDF As String
    End Structure

    Public Structure TempSensorData
        Dim PA0 As Double
        Dim PA1 As Double
        Dim PA0VSWR As Double
        Dim PA1VSWR As Double
        Dim LNA0 As Double
        Dim LNA1 As Double
        Dim LNA2 As Double
        Dim LNA3 As Double
        Dim PS_CONVERTER As Double
        Dim PS_BRICK As Double
        Dim RX As Double
        Dim FB As Double
    End Structure

    Public Structure PaStatusData
'rtose@voyager> radio -t pa -p
'PA 0 Status Info:
'   Setup Flavor:        Viper1900
'   Machine State:       Biased
'   Status:              OK
'   EEPROM Data Use:     False
'   Bias Compensation:   ON
'   PA turned on:        NO
'   VVA:                 2400 counts
'   Overcurrent Ref:     3100 counts
'   OC Alarm Status:     OFF
'   Current Temp:        33.37 C
'   Bias Point Temp:     28.61 C
'   Vdrain:              29.97 Volt
'   Last Biasing time:   5.79 sec
'   Last Num bias steps: 58 (max 250)
'Transistor Name: Driver1
'   Current Status:                Biased
'   Type:                          Peak
'   Drain Target Current:          0.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          900
'   Instantaneous Drain Current:   0.24 mA
'Transistor Name: Driver2
'   Current Status:                Biased
'   Type:                          Carrier
'   Drain Target Current:          40.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          1464
'   Instantaneous Drain Current:   0.24 mA
'Transistor Name: Driver3
'   Current Status:                Biased
'   Type:                          Carrier
'   Drain Target Current:          260.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          1431
'   Instantaneous Drain Current:   2.44 mA
'Transistor Name: Driver4
'   Current Status:                Biased
'   Type:                          Carrier
'   Drain Target Current:          40.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          1458
'   Instantaneous Drain Current:   0.24 mA
'Transistor Name: Final1
'   Current Status:                Biased
'   Type:                          Peak
'   Drain Target Current:          0.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          295
'   Instantaneous Drain Current:   16.28 mA
'Transistor Name: Final2
'   Current Status:                Biased
'   Type:                          Carrier
'   Drain Target Current:          900.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          962
'   Instantaneous Drain Current:   16.28 mA

        Dim Vdrain As Double
        Dim Temperature As Double
        Dim Bias_Point_Temp As Double
        Dim CurrentFinal1 As Double
        Dim CurrentFinal2 As Double
        Dim CurrentDriver1 As Double
        Dim CurrentDriver2 As Double
        Dim CurrentDriver3 As Double
        Dim CurrentDriver4 As Double
        Dim DacFinal1 As Integer
        Dim DacFinal2 As Integer
        Dim DacDriver1 As Integer
        Dim DacDriver2 As Integer
        Dim DacDriver3 As Integer
        Dim DacDriver4 As Integer
    End Structure

    Public Structure RssiData_ARM
        '         RX1
        '---------------------------------------------------
        '| carrier | container | frequency   | rssi (dBm)  |
        '---------------------------------------------------
        '| 0       | 1         | 1865.00 MHz | -107.09 dBm |
        '| 1       | 9         | 1895.00 MHz | -107.38 dBm |
        '---------------------------------------------------
        ' RX2
        '---------------------------------------------------
        '| carrier | container | frequency   | rssi (dBm)  |
        '---------------------------------------------------
        '| 0       | 3         | 1865.00 MHz | -107.69 dBm |
        '| 1       | 11        | 1895.00 MHz | -107.09 dBm |
        '---------------------------------------------------
        ' RX3
        '---------------------------------------------------
        '| carrier | container | frequency   | rssi (dBm)  |
        '---------------------------------------------------
        '| 0       | 5         | 1865.00 MHz | -107.38 dBm |
        '| 1       | 13        | 1895.00 MHz | -107.09 dBm |
        '---------------------------------------------------
        ' RX4
        '---------------------------------------------------
        '| carrier | container | frequency   | rssi (dBm)  |
        '---------------------------------------------------
        '| 0       | 7         | 1865.00 MHz | -107.09 dBm |
        '| 1       | 15        | 1895.00 MHz | -106.82 dBm |
        '---------------------------------------------------
        Structure RxData
            Structure CarrData
                Dim carrier As Integer
                Dim container As Integer
                Dim frequency As Double
                Dim rssi As Double
            End Structure
            Dim Carr() As CarrData
        End Structure
        Dim Rx() As RxData
    End Structure

    Public Structure RssiData
        '        rtose@voyager> rcmd dsp "rssi -p"
        'Rx path 0 LTE:
        '    Carrier  Raw Rssi(dBm)   Pcg Rssi(dBm)
        '     0        -106.57         -105.78
        '     1        -106.69         -105.88
        'Rx path 1 LTE:
        '    Carrier  Raw Rssi(dBm)   Pcg Rssi(dBm)
        '     0        -106.21         -105.38
        '     1        -106.33         -105.48
        'Rx path 2 LTE:
        '    Carrier  Raw Rssi(dBm)   Pcg Rssi(dBm)
        '     0        -106.96         -106.10
        '     1        -107.09         -106.21
        'Rx path 3 LTE:
        '    Carrier  Raw Rssi(dBm)   Pcg Rssi(dBm)
        '     0        -106.96         -106.10
        '     1        -106.82         -105.99
        Structure RxData
            Structure CarrData
                Dim carrier As Integer
                Dim Raw_Rssi As Double
                Dim Pcg_Rssi As Double
            End Structure
            Dim Carr() As CarrData
        End Structure
        Dim Rx() As RxData
    End Structure

    Public Structure PSStatusData
'rtose@voyager> power_sup -r
' Input Voltage     ->    49.03 V
' Input Current     ->     1.31 A
' Input Power       ->    64.28 W
' Output Voltage    ->    29.95 V
' AISG 12V Voltage  ->     6.47 V
' AISG 12V Current  ->     0.74 A
' AISG 24V Voltage  ->    14.31 V
' AISG 24V Current  ->     0.97 A

        Dim Input_Voltage As Double
        Dim Input_Current As Double
        Dim Input_Power As Double
        Dim Output_Voltage As Double
        Dim AISG_12V_Voltage As Double
        Dim AISG_12V_Current As Double
        Dim AISG_24V_Voltage As Double
        Dim AISG_24V_Current As Double
    End Structure

    Public Structure GainStatusData
        'dsp> txgain -a 0 -p
        'Tx Gain Values for path 0

        '        enabled             =   off,  update count           =     0
        '        txStepAttn          = 31.50,  ocpmGain               =     0
        '        txDacGain           =  0.00,  fbTxQuotient           = 1.620
        '        total tx attn       = 31.50,  nomGain                = 1.594
        '        fbStepAtten         =  7.00,  nomGainChangeTemp      = 0.984
        '        gainError           =  1.00,  nomGainChangeOverdrive = 1.000
        '        fbAdcGain           =  0.00,  nomGainChangeOcpm      = 1.000
        '                                      nomGainChangeDcV       = 1.000
        '                                      nomGainChangeFreq      = 1.000

        Dim TxStep As Double
        Dim FbStep As Double
        Dim FbTxQuo As Double
        Dim Vca As Integer
        'Dim GainError As Double
        Dim GainError As Integer
        Dim Enabled As Boolean
        Dim txDacGain As Double
        Dim total_tx_attn As Double
    End Structure

    Public Structure dpdDelayData
        Dim dpd_ampDelayInt As Integer
        Dim dpd_ampDelayFrac As Integer
        Dim dpd_Maximum_cross_correlation As Double
    End Structure

    Public Structure dpdL2Data
        Dim dpd_L2_3rd_sym_am As Double
        Dim dpd_L2_3rd_sym_ph As Double
        Dim dpd_L3_3rd_sym_am As Double
        Dim dpd_L3_3rd_sym_ph As Double
        Dim dpd_L2_5th_sym_am As Double
        Dim dpd_L2_5th_sym_ph As Double
        Dim dpd_L3_5th_sym_am As Double
        Dim dpd_L3_5th_sym_ph As Double
        Dim dpd_L2_3rd_asym_am As Double
        Dim dpd_L2_3rd_asym_ph As Double
        Dim dpd_L2_5th_asym_am As Double
        Dim dpd_L2_5th_asym_ph As Double
    End Structure

    Public Structure LnaAdcData
        'TRDU700P1.3#RDK>lna readadc 2
        'Chan  --Desc--  Counts  EngValue  Units
        '   0  xxx diff      91         0
        '   1  tx0 fwd      143         0
        '   2  tx1 rev      144         0
        '   3      temp    1232        25  degC
        '   4      cur0      63         9  mA
        '   5      cur1      62         8  mA
        Dim DiffA2D As Integer
        Dim FwdA2D As Integer
        Dim RevA2D As Integer
        Dim TempDegC As Double
        Dim Cur0mA As Double
        Dim Cur1mA As Double
    End Structure

    Public Enum LnaDetSwitchEnum
        EF = 0
        Tx1 = 1
        Tx0 = 2
    End Enum

    Public Enum TempDev
        PA0 = 0
        PA1 = 1
        LNA0 = 2
        LNA1 = 3
        LNA2 = 3
        LNA3 = 3
    End Enum
    Public Enum SigPath
        TX0 = 0
        TX1 = 1
        RX0 = 2
        RX1 = 3
        RX2 = 4
        RX3 = 5
    End Enum
    Public Enum LNACfg
        Nom = 0
        Aux = 1
        Tma = 2
    End Enum
    Public Enum CarrTypeEnum
        LTE_5
        LTE_10
        LTE_20
    End Enum
    Public Structure BiasStatus
        Dim DrainVoltage As Double
        Dim PATemperature As Double
        Dim Final1Current As Double
        Dim Final2Current As Double
        Dim Driver1Current As Double
        Dim Driver2Current As Double
        Dim Driver3Current As Double
        Dim Driver4Current As Double
    End Structure
    Private Structure TxGainValues
        Dim TxStepAttn As Double
        Dim FBStepAtten As Double
        Dim FBTxQuotient As Double
    End Structure
    Public Structure AISGData
        Dim AISG_12V_Curr As Double
        Dim AISG_12V_Volt As Double
        Dim AISG_24V_Curr As Double
        Dim AISG_24V_Volt As Double
    End Structure
    Public Structure CaseInfo
        Dim NumberofCarrier As Integer
        Dim MaxFreq As Double
        Dim MinFreq As Double
        Dim MidFreq As Double
        Dim ActiveFreq() As Double
        Dim CaseStr As String
    End Structure
    Public Structure CalDataMap
        Public Structure TX
            Public Structure TG
                Public Structure altCal
                    Public freq As Double
                    Public correction As Double
                End Structure
                Public txAtten As Double
                Public fbAtten As Double
                Public fbtxquotient As Double
                Public txTotalAtten As Double
                Public tempAtCal As Double
                Public freqAtCal As Double
                Public powerAtCal As Double
                Public altCals() As altCal
            End Structure
            Public Structure VW
                Public tempAtCal As Double
                Public Structure freqCal
                    Public freq As Double
                    Public Structure vswrCal
                        Public adc As UInt32
                        Public retloss As Double
                    End Structure
                    Public vswrCals() As vswrCal
                End Structure
                Public freqCals() As freqCal
            End Structure
            Public txgain As TG
            Public vswr As VW
        End Structure
        Public Structure RX
            Public Structure RG
                Public rxAtten As Double
                Public tempAtCal As Double
                Public freqAtCal As Double
                Public Structure altCal
                    Public freq As Double
                    Public correction As Double
                End Structure
                Public altCals() As altCal
            End Structure
            Public rxgain As RG
        End Structure
        Public transmitter() As TX
        Public receiver() As RX
    End Structure
    Private WithEvents _CommunicationSession As ICommunications
    Private WithEvents _TelnetCustomer As CustomerTelnetInterface = Nothing
    Private Sub CallBackTelnetReceivedMessage(ByVal Message As String) Handles _CommunicationSession.DataReceivedCompleted
        RaiseEvent DeviceMessageReceived(Message)
    End Sub
    Private _SerialNumber As String
    Private _UUTType As String
    Private _DeviceAddress As String
    Private _HWRev As String
    Private _FWRevision As String
    Private _Port As Integer = 23
    Private _Model As String = "Voyager"
    'Private _ComInterface As ICommunications.BusCommunications = ICommunications.BusCommunications.Serial

    Private _Usename As String = "root"
    Private _Password As String = "cvrh1tb!"
    Private _TxGain(1) As TxGainValues
    Private _TxGainChanged As Boolean = True
    Private _DPDStatus() As Boolean = {True, True}
    Private _CustomerCarrierConfigured As Boolean


    '****************************************************************
    '*   IDeviceUnderTest Interface Implementation
    '****************************************************************
    Public Property Address() As String Implements IDeviceUnderTest.Address
        Get
            Return _DeviceAddress
        End Get
        Set(ByVal value As String)
            _DeviceAddress = value
        End Set
    End Property

    Public Sub Close() Implements IDeviceUnderTest.Close
        Try
            If _TelnetCustomer IsNot Nothing Then
                _TelnetCustomer.Close()
                _TelnetCustomer = Nothing
            End If
            If _CommunicationSession IsNot Nothing Then
                _CommunicationSession.Write("exit", False)
                Threading.Thread.Sleep(1000)
                _CommunicationSession.Close()
                _CommunicationSession = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Event DeviceMessageReceived(ByVal Message As String) Implements IDeviceUnderTest.DeviceMessageReceived

    Public ReadOnly Property FWRev() As String Implements IDeviceUnderTest.FWRev
        Get
            Return _FWRevision
        End Get
    End Property



    Public Property HWRev() As String Implements IDeviceUnderTest.HWRev
        Get
            Return _HWRev
        End Get
        Set(ByVal value As String)
            _HWRev = value
        End Set
    End Property

    Public Property Model() As String Implements IDeviceUnderTest.Model
        Get
            Return _Model
        End Get
        Set(ByVal value As String)
            _Model = value
        End Set
    End Property

    Public Sub Open() Implements IDeviceUnderTest.Open
        Try
            'If _ComInterface = ICommunications.BusCommunications.Telnet Then
            '    _CommunicationSession = New ClientTelnetInterface
            'ElseIf _ComInterface = ICommunications.BusCommunications.Serial Then
            '    _CommunicationSession = New SerialInterface
            'End If
            If _DeviceAddress.ToUpper.Contains("COM") Then
                _CommunicationSession = New SerialInterface
            Else
                _CommunicationSession = New ClientTelnetInterface
            End If
            _CommunicationSession.Address = _DeviceAddress
            _CommunicationSession.Port = _Port
            _CommunicationSession.Open()

            _CommunicationSession.Write(String.Format("login {0}", Me._Usename), False)
            _CommunicationSession.Write(Me._Password, False)

            _TelnetCustomer = New CustomerTelnetInterface
            _TelnetCustomer.IPAddress = _DeviceAddress
            _TelnetCustomer.Port = 1307
            _TelnetCustomer.Open()
            HeartBeatEnable = False
        Catch ex As Exception
            Me.Close()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Property Port() As Integer Implements IDeviceUnderTest.Port
        Get
            Return _Port
        End Get
        Set(ByVal value As Integer)
            _Port = value
        End Set
    End Property

    Public Property SerialNumber() As String Implements IDeviceUnderTest.SerialNumber
        Get
            Return _SerialNumber
        End Get
        Set(ByVal value As String)
            _SerialNumber = value
        End Set
    End Property
    Property Net_MAC() As String Implements IDeviceUnderTest.MAC
        Get
            Return "No Command"
        End Get
        Set(ByVal value As String)
            Try
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Set
    End Property
    WriteOnly Property BiasPA(ByVal channel As Integer) As Boolean
        Set(ByVal value As Boolean)
            Try
                If value Then
                    _CommunicationSession.Write(String.Format("radio -t pa -a {0} -w on", channel))
                Else
                    _CommunicationSession.Write(String.Format("radio -t pa -a {0} -w off", channel))
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Set
    End Property

    Public Property TxAttenuator(ByVal channel As Integer) As Double
        Get
            'Dim res As Double
            'Dim tmp As String = ""
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("InvalidParameter:TxAttenuator", "channel:" & channel)
            'Try
            '    tmp = _CommunicationSession.Write(String.Format("rcmd dsp ""txattn -{0} -r""", channel))
            '    Dim Token As String = "is"
            '    Dim StartPos As Integer = tmp.IndexOf(Token) + Token.Length
            '    Dim StopPos As Integer = tmp.IndexOf("dB")
            '    res = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))
            'Catch ex As Exception
            '    Throw New Exception(String.Format("Get TxAttenuator:{0}", tmp))
            'End Try
            'Return res
            If _TxGainChanged Then
                UpdateTxGainValues()
                _TxGainChanged = False
            End If
            Return _TxGain(channel).TxStepAttn
        End Get
        Set(ByVal value As Double)
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("InvalidParameter:TxAttenuator", "channel:" & channel)
            If value < 0 Or value > 31.5 Then Throw New ArgumentException("TxAttenuator invalid value: " & value)
            _CommunicationSession.Write(String.Format("rcmd dsp ""txattn -{1} -w {0:.00}""", value, channel))
            _TxGainChanged = True
        End Set
    End Property
    Public Property FBAttenuator() As Double
        Get
            If _TxGainChanged Then
                UpdateTxGainValues()
                _TxGainChanged = False
            End If
            Return _TxGain(0).FBStepAtten
        End Get
        Set(ByVal value As Double)
            If value < 0 Or value > 31.5 Then Throw New ArgumentException("FBAttenuator invalid value: " & value)
            _CommunicationSession.Write(String.Format("rcmd dsp ""fbattn -w {0:.00}""", value))
            _TxGainChanged = True
        End Set
    End Property
    Public Property FbTxQ(ByVal channel As Integer) As Double
        Get
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("InvalidParameter:FBTxQuotient", "channel:" & channel)
            If _TxGainChanged Then
                UpdateTxGainValues()
                _TxGainChanged = False
            End If
            Return _TxGain(channel).FBTxQuotient
        End Get
        Set(ByVal value As Double)
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("InvalidParameter:FBTxQuotient", "channel:" & channel)
            If value < 0 Or value > 1.7 Then Throw New ArgumentException("FBTxQuotient invalid value: " & value)
            _CommunicationSession.Write(String.Format("rcmd dsp ""txgain -a {1} -s fbtxq {0:.00}""", value, channel))
            _TxGainChanged = True
        End Set
    End Property

    Public Property TxVVA(ByVal channel As Integer) As Integer
        Get
            Dim res As Integer
            Dim tmp As String = ""
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("TxVVA invalid channel: " & channel)
            Try
                tmp = _CommunicationSession.Write(String.Format("amc7823 -r pa{0} 1 7", channel))
                Dim Token As String = "0x07 data = 0x7"
                Dim StartPos As Integer = tmp.IndexOf(Token) + Token.Length
                Dim StopPos As Integer = tmp.IndexOf(vbCrLf, StartPos)
                res = Convert.ToInt16(tmp.Substring(StartPos, StopPos - StartPos), 16)
                Return res
            Catch ex As Exception
                Throw New Exception(String.Format("Get TxVVA:{0}", tmp))
            End Try
        End Get
        Set(ByVal value As Integer)
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("TxVVA invalid channel: " & channel)
            If value < 0 Or value > 4095 Then Throw New ArgumentException("TxVVA invalid value: " & value)
            _CommunicationSession.Write(String.Format("amc7823 -w pa{0} 1 7 {1}", channel, value))
        End Set
    End Property
    ReadOnly Property Temperature(ByVal Dev As TempDev) As Double
        Get
            Dim res As Double
            Dim tmp As String = ""
            Dim devname As String = ""
            Select Case Dev
                Case TempDev.PA0
                    devname = "pa0"
                Case TempDev.PA1
                    devname = "pa1"
                Case TempDev.LNA0
                    devname = "lna0"
                Case TempDev.LNA1
                    devname = "lna1"
            End Select
            Try
                tmp = _CommunicationSession.Write(String.Format("amc7823 -t {0}", devname))
                Dim Token As String = "read"
                Dim StartPos As Integer = tmp.IndexOf(Token) + Token.Length
                Dim StopPos As Integer = tmp.IndexOf("C", StartPos)
                res = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))
                Return res
            Catch ex As Exception
                Throw New Exception(String.Format("Get Temperature:{0}", tmp))
            End Try
        End Get
    End Property
    Public ReadOnly Property PA_BiasStatus(ByVal channel As Integer) As BiasStatus
        Get
            Try
                'AMC7823 device PA0 read reg 0x00 data = 0x0005
                'AMC7823 device PA0 read reg 0x01 data = 0x1006
                'AMC7823 device PA0 read reg 0x02 data = 0x2006
                'AMC7823 device PA0 read reg 0x03 data = 0x3005
                'AMC7823 device PA0 read reg 0x04 data = 0x4006
                'AMC7823 device PA0 read reg 0x05 data = 0x528e
                'AMC7823 device PA0 read reg 0x06 data = 0x6ac5
                'AMC7823 device PA0 read reg 0x07 data = 0x7006


                Dim results As BiasStatus
                Dim StartPos As Integer, StopPos As Integer
                Dim token As String
                Dim ref = 5
                Dim res(7) As Double
                If channel < 0 Or channel > 1 Then Throw New ArgumentException("TxVVA invalid channel: " & channel)
                Dim tmpString As String = _CommunicationSession.Write(String.Format("amc7823 -r pa{0} 0 0 8", channel))

                StartPos = 0
                StopPos = 0
                For i As Integer = 0 To res.Length - 1
                    token = String.Format("0x0{0} data = 0x{0}", i)
                    StartPos = tmpString.IndexOf(token, StopPos) + token.Length
                    StopPos = tmpString.IndexOf(vbCrLf, StartPos)
                    res(i) = Convert.ToInt16(tmpString.Substring(StartPos, StopPos - StartPos), 16) * ref / 4096
                Next
                results.Final1Current = res(0) / 0.6
                results.Final2Current = res(1) / 0.6
                results.Driver1Current = res(3) / 4.5
                results.Driver2Current = res(2) / 4.5
                results.Driver3Current = res(7) / 4.5
                results.Driver4Current = res(4) / 4.5
                results.DrainVoltage = Math.Round(res(6) * (6.81 + 49.9) / 6.81, 2)
                results.PATemperature = Math.Round((res(5) * 1000 - 500) / 10, 1)
                Return results
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
    End Property
    Public Sub Reset()
        Try
            If _TelnetCustomer IsNot Nothing Then
                _TelnetCustomer.Close()
                _TelnetCustomer = Nothing
            End If
            _CommunicationSession.Write("reset", False)
            Threading.Thread.Sleep(8000)
            If _CommunicationSession IsNot Nothing Then
                _CommunicationSession.Close()
                _CommunicationSession = Nothing
            End If
            Threading.Thread.Sleep(60000)
            Me.Open()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub UpdateTxGainValues()
        'Tx Gain Values for path 0

        '        enabled             =   off,  update count           =     0
        '        txStepAttn          = 31.50,  ocpmGain               =     0
        '        txDacGain           =  6.00,  fbTxQuotient           = 0.000
        '        total tx attn       = 37.50,  nomGain                = 0.000
        '        fbStepAtten         =  2.00,  nomGainChangeTemp      = 1.000
        '        gainError           =  1.00,  nomGainChangeOverdrive = 1.000
        '        fbAdcGain           =  0.00,  nomGainChangeOcpm      = 1.000
        '        nomGainChangeDcV = 1.0
        '        nomGainChangeFreq = 1.0



        'Tx Gain Values for path 1

        '        enabled             =   off,  update count           =     0
        '        txStepAttn          = 31.50,  ocpmGain               =     0
        '        txDacGain           =  6.00,  fbTxQuotient           = 0.000
        '        total tx attn       = 37.50,  nomGain                = 0.000
        '        fbStepAtten         =  2.00,  nomGainChangeTemp      = 1.000
        '        gainError           =  1.00,  nomGainChangeOverdrive = 1.000
        '        fbAdcGain           =  0.00,  nomGainChangeOcpm      = 1.000
        '        nomGainChangeDcV = 1.0
        '        nomGainChangeFreq = 1.0
        Try
            Dim tmpString As String = _CommunicationSession.Write("rcmd dsp ""txgain -p""")
            Dim StartPos As Integer, StopPos As Integer
            Dim token As String
            For i As Integer = 0 To 1
                token = "txStepAttn          ="
                StartPos = tmpString.IndexOf(token, StopPos) + token.Length
                StopPos = tmpString.IndexOf(",", StartPos)
                _TxGain(i).TxStepAttn = Convert.ToDouble(tmpString.Substring(StartPos, StopPos - StartPos))

                token = "fbTxQuotient           ="
                StartPos = tmpString.IndexOf(token, StopPos) + token.Length
                StopPos = tmpString.IndexOf(vbCrLf, StartPos)
                _TxGain(i).FBTxQuotient = Convert.ToDouble(tmpString.Substring(StartPos, StopPos - StartPos))

                token = "fbStepAtten         ="
                StartPos = tmpString.IndexOf(token, StopPos) + token.Length
                StopPos = tmpString.IndexOf(",", StartPos)
                _TxGain(i).FBStepAtten = Convert.ToDouble(tmpString.Substring(StartPos, StopPos - StartPos))
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Property DPDStatus(ByVal channel As Integer) As Boolean
        Get
            Return _DPDStatus(channel)
        End Get
        Set(ByVal value As Boolean)
            Try
                _DPDStatus(channel) = value
                If value Then
                    _CommunicationSession.Write(String.Format("rcmd dsp ""dsp tx{0} on""", channel))
                Else
                    _CommunicationSession.Write(String.Format("rcmd dsp ""dsp tx{0} off""", channel))
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Set
    End Property
    Public Function GetRSSI() As Double(,)
        Try
            'Answer
            'Carrier  agc_rssi agc_rtwp  rtwp
            '  0      -60.94    -60.94     -83.96
            '  1      -89.31    -89.31     -111.53
            '  2      -89.63    -89.63     -111.67
            '  3      -89.56    -89.56     -111.89
            '  4      -89.63    -89.56     -111.89

            'Carrier  agc_rssi agc_rtwp  rtwp
            '  0      -89.19    -89.06     -111.26
            '  1      -89.25    -89.19     -111.39
            '  2      -89.50    -89.50     -111.60
            '  3      -89.75    -89.75     -111.82
            '  4      -89.81    -89.75     -111.89
            Dim RSSI(1, 4) As Double
            Dim token As String
            Dim StartPos As Integer = 0, StopPos As Integer = 0
            Dim tmpString As String = _CommunicationSession.Write("rcmd dsp ""rssi -p""")
            For I As Integer = 0 To 1
                For J As Integer = 0 To 4
                    token = String.Format("  {0}      ", J)
                    StartPos = tmpString.IndexOf(token, StopPos) + token.Length
                    StopPos = tmpString.IndexOf(vbCrLf, StartPos)
                    Dim tmpStr As String = tmpString.Substring(StartPos, StopPos - StartPos)
                    Dim m As Match = Regex.Match(tmpStr, " *(\S*) *(\S*) *(\S*) *(\S*) *(\S*)")
                    RSSI(I, J) = Convert.ToDouble(m.Groups(3).Value)
                Next
            Next
            Return RSSI
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetRSSIRaw() As Double(,,)
        Try
            'Answer
            'Carrier  agc_rssi agc_rtwp  rtwp
            '  0      -60.94    -60.94     -83.96
            '  1      -89.31    -89.31     -111.53
            '  2      -89.63    -89.63     -111.67
            '  3      -89.56    -89.56     -111.89
            '  4      -89.63    -89.56     -111.89

            'Carrier  agc_rssi agc_rtwp  rtwp
            '  0      -89.19    -89.06     -111.26
            '  1      -89.25    -89.19     -111.39
            '  2      -89.50    -89.50     -111.60
            '  3      -89.75    -89.75     -111.82
            '  4      -89.81    -89.75     -111.89
            Dim RSSI(1, 4, 4) As Double
            Dim token As String
            Dim StartPos As Integer = 0, StopPos As Integer = 0
            Dim tmpString As String = _CommunicationSession.Write("rcmd dsp ""rssi -p""")
            For I As Integer = 0 To 1
                For J As Integer = 0 To 4
                    token = String.Format("  {0}      ", J)
                    StartPos = tmpString.IndexOf(token, StopPos) + token.Length
                    StopPos = tmpString.IndexOf(vbCrLf, StartPos)
                    Dim tmpStr As String = tmpString.Substring(StartPos, StopPos - StartPos)
                    Dim m As Match = Regex.Match(tmpStr, " *(\S*) *(\S*) *(\S*) *(\S*) *(\S*)")
                    For K As Integer = 0 To 4
                        RSSI(I, J, K) = Convert.ToDouble(m.Groups(K + 1).Value)
                    Next
                Next
            Next
            Return RSSI
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub _TelnetCustomer_DataReceivedCompleted(ByVal ReceivedData As String) Handles _TelnetCustomer.DataReceivedCompleted
        RaiseEvent DeviceMessageReceived(ReceivedData)
    End Sub

    Public ReadOnly Property VSWR(ByVal channel As Integer) As Double
        Get
            Dim res As Double
            Dim tmp As String = ""
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("VSWR invalid channel: " & channel)
            Try
                tmp = _CommunicationSession.Write(String.Format("radio -t vswr -a {0} -r retloss", channel))
                Dim Token As String = "Return Loss: "
                Dim StartPos As Integer = tmp.IndexOf(Token) + Token.Length
                Dim StopPos As Integer = tmp.IndexOf("dB", StartPos)
                res = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))
                Return res
            Catch ex As Exception
                Throw New Exception(String.Format("Get VSWR:{0}", tmp))
            End Try
        End Get
    End Property
    Public ReadOnly Property FWPow(ByVal channel As Integer) As Double
        Get
            Dim res As Double
            Dim tmp As String = ""
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("VSWR invalid channel: " & channel)
            Try
                tmp = _CommunicationSession.Write(String.Format("radio -t vswr -a {0} -r fwPower", channel))
                Dim Token As String = "Forward Power: "
                Dim StartPos As Integer = tmp.IndexOf(Token) + Token.Length
                Dim StopPos As Integer = tmp.IndexOf("dBm", StartPos)
                res = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))
                Return res
            Catch ex As Exception
                Throw New Exception(String.Format("Get Forward Power:{0}", tmp))
            End Try
        End Get
    End Property
    Public Function SendCommand(ByVal cmd As String, Optional ByVal wait As Boolean = True) As String
        Return __CommunicationSession.Write(cmd, wait)
    End Function

    Public Property AISG_24V_Enable() As Boolean
        Get
            Dim resp As String = ""
            Try
                resp = SendCommand("hal -r sysdev 0 OUTPUT_GPIO_AISG24_OFF")
                Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
                If m.Success And m.Groups.Count >= 2 Then
                    Select Case UCase(m.Groups(1).Value)
                        Case "ON"
                            Return True
                        Case "OFF"
                            Return False
                        Case Else
                            Throw New Exception("Unexpected response: " & resp)
                    End Select
                Else
                    Throw New Exception("Unexpected response: " & resp)
                End If
            Catch ex As Exception
                Throw New Exception(resp)
            End Try
        End Get
        Set(ByVal value As Boolean)
            If value Then
                SendCommand("hal -w sysdev 0 OUTPUT_GPIO_AISG24_OFF ON")
            Else
                SendCommand("hal -w sysdev 0 OUTPUT_GPIO_AISG24_OFF OFF")
            End If
        End Set
    End Property

    Public Property AISG_12V_Enable() As Boolean
        Get
            Dim resp As String = ""
            Try
                resp = SendCommand("hal -r sysdev 0 OUTPUT_GPIO_AISG12_OFF")
                Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
                If m.Success And m.Groups.Count >= 2 Then
                    Select Case UCase(m.Groups(1).Value)
                        Case "ON"
                            Return True
                        Case "OFF"
                            Return False
                        Case Else
                            Throw New Exception("Unexpected response: " & resp)
                    End Select
                Else
                    Throw New Exception("Unexpected response: " & resp)
                End If
            Catch ex As Exception
                Throw New Exception(resp)
            End Try
        End Get
        Set(ByVal value As Boolean)
            If value Then
                SendCommand("hal -w sysdev 0 OUTPUT_GPIO_AISG12_OFF ON")
            Else
                SendCommand("hal -w sysdev 0 OUTPUT_GPIO_AISG12_OFF OFF")
            End If
        End Set
    End Property

    Public ReadOnly Property AISG() As AISGData
        Get
            Dim Temp As AISGData
            Dim resp As String = ""
            Try
                resp = SendCommand("hal -r 4 0 5")
                Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
                Temp.AISG_12V_Curr = CDbl(m.Groups(1).Value)

                resp = SendCommand("hal -r 4 0 6")
                m = Regex.Match(resp, "-> *(\S*) *")
                Temp.AISG_24V_Curr = CDbl(m.Groups(1).Value)

                resp = SendCommand("hal -r 4 0 7")
                m = Regex.Match(resp, "-> *(\S*) *")
                Temp.AISG_12V_Volt = CDbl(m.Groups(1).Value)

                resp = SendCommand("hal -r 4 0 8")
                m = Regex.Match(resp, "-> *(\S*) *")
                Temp.AISG_24V_Volt = CDbl(m.Groups(1).Value)
                Return Temp
            Catch ex As Exception
                Throw New Exception(resp)
            End Try
        End Get
    End Property

    Public ReadOnly Property AISG_12V_Curr() As Double
        Get
            Dim resp As String = SendCommand("hal -r sysdev 0 SENSOR_CURRENT_AISG_12V")
            Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            If m.Success And m.Groups.Count >= 2 Then
                Return CDbl(m.Groups(1).Value)
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property AISG_24V_Curr() As Double
        'token = "hal -r sysdev 0 SENSOR_CURRENT_AISG_24V"
        Get
            Dim resp As String = SendCommand("hal -r sysdev 0 SENSOR_CURRENT_AISG_24V")
            Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            If m.Success And m.Groups.Count >= 2 Then
                Return CDbl(m.Groups(1).Value)
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property AISG_12V_Volt() As Double
        Get
            Dim resp As String = SendCommand("hal -r sysdev 0 SENSOR_VOLTAGE_AISG_12V")
            Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            If m.Success And m.Groups.Count >= 2 Then
                Return CDbl(m.Groups(1).Value)
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property AISG_24V_Volt() As Double
        Get
            Dim resp As String = SendCommand("hal -r sysdev 0 SENSOR_VOLTAGE_AISG_24V")
            Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            If m.Success And m.Groups.Count >= 2 Then
                Return CDbl(m.Groups(1).Value)
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property PSU_Volt() As Double
        Get
            Dim resp As String = SendCommand("hal -r sysdev 0 SENSOR_VOLTAGE_PSU_VIN")
            Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            If m.Success And m.Groups.Count >= 2 Then
                Return CDbl(m.Groups(1).Value)
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property PSU_Curr() As Double
        Get
            Dim resp As String = SendCommand("hal -r sysdev 0 SENSOR_CURRENT_PSU_CIN")
            Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            If m.Success And m.Groups.Count >= 2 Then
                Return CDbl(m.Groups(1).Value)
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property GPIO_USER_AL1() As String
        Get
            Dim resp As String = SendCommand("hal -r sysdev 0 INPUT_GPIO_USER_AL1")
            'Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            Dim m As Match = Regex.Match(resp, "\(*(\S*)\)")
            If m.Success And m.Groups.Count >= 2 Then
                '  Return UCase(m.Groups(1).Value)
                Select Case UCase(m.Groups(1).Value)
                    Case "0X0000"
                        Return "CLOSE"
                    Case "0X0001"
                        Return "OPEN"
                    Case Else
                        Throw New Exception("Unexpected response: " & resp)
                End Select
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property GPIO_USER_AL2() As String
        Get
            Dim resp As String = SendCommand("hal -r sysdev 0 INPUT_GPIO_USER_AL2")
            'Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            Dim m As Match = Regex.Match(resp, "\(*(\S*)\)")
            If m.Success And m.Groups.Count >= 2 Then
                '  Return UCase(m.Groups(1).Value)
                Select Case UCase(m.Groups(1).Value)
                    Case "0X0000"
                        Return "CLOSE"
                    Case "0X0001"
                        Return "OPEN"
                    Case Else
                        Throw New Exception("Unexpected response: " & resp)
                End Select
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property GPIO_USER_AL3() As String
        Get
            Dim resp As String = SendCommand("hal -r sysdev 0 INPUT_GPIO_USER_AL3")
            'Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            Dim m As Match = Regex.Match(resp, "\(*(\S*)\)")
            If m.Success And m.Groups.Count >= 2 Then
                '  Return UCase(m.Groups(1).Value)
                Select Case UCase(m.Groups(1).Value)
                    Case "0X0000"
                        Return "CLOSE"
                    Case "0X0001"
                        Return "OPEN"
                    Case Else
                        Throw New Exception("Unexpected response: " & resp)
                End Select
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property GPIO_USER_AL4() As String
        Get
            Dim resp As String = SendCommand("hal -r sysdev 0 INPUT_GPIO_USER_AL4")
            'Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            Dim m As Match = Regex.Match(resp, "\(*(\S*)\)")
            If m.Success And m.Groups.Count >= 2 Then
                '  Return UCase(m.Groups(1).Value)
                Select Case UCase(m.Groups(1).Value)
                    Case "0X0000"
                        Return "CLOSE"
                    Case "0X0001"
                        Return "OPEN"
                    Case Else
                        Throw New Exception("Unexpected response: " & resp)
                End Select
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property GPIO_USER_AL(ByVal channel As Integer) As String
        Get
            Dim resp As String = SendCommand(String.Format("hal -r sysdev 0 INPUT_GPIO_USER_AL{0}", channel))
            'Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            Dim m As Match = Regex.Match(resp, "\(*(\S*)\)")
            If m.Success And m.Groups.Count >= 2 Then
                '  Return UCase(m.Groups(1).Value)
                Select Case UCase(m.Groups(1).Value)
                    Case "0X0000"
                        Return "CLOSE"
                    Case "0X0001"
                        Return "OPEN"
                    Case Else
                        Throw New Exception("Unexpected response: " & resp)
                End Select
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property


    Public Property ANT1_24V_DisEnable() As Boolean
        Get
            Dim resp As String = ""
            Try
                resp = SendCommand("hal -r rxdev 0 OUTPUT_GPIO_LNA_PSANT")
                Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
                If m.Success And m.Groups.Count >= 2 Then
                    Select Case UCase(m.Groups(1).Value)
                        Case "ON"
                            Return True
                        Case "OFF"
                            Return False
                        Case Else
                            Throw New Exception("Unexpected response: " & resp)
                    End Select
                Else
                    Throw New Exception("Unexpected response: " & resp)
                End If
            Catch ex As Exception
                Throw New Exception(resp)
            End Try
        End Get
        Set(ByVal value As Boolean)
            If value Then
                SendCommand("hal -w rxdev 0 OUTPUT_GPIO_LNA_PSANT ON")
            Else
                SendCommand("hal -w rxdev 0 OUTPUT_GPIO_LNA_PSANT OFF")
            End If
        End Set
    End Property
    Public ReadOnly Property ANT1_24V_Volt() As Double
        Get
            Dim resp As String = SendCommand("hal -r rxdev 0 SENSOR_VOLTAGE_PS_ANT")
            Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            m = Regex.Match(resp, "= +(\S+) +")
            If m.Success And m.Groups.Count >= 2 Then
                Return CDbl(m.Groups(1).Value)
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property ANT1_24V_Curr() As Double
        Get
            Dim resp As String = SendCommand("hal -r rxdev 0 SENSOR_CURRENT_PS_ANT")
            Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
            If m.Success And m.Groups.Count >= 2 Then
                Return CDbl(m.Groups(1).Value)
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property

    Public Property ANT2_24V_DisEnable() As Boolean
        Get
            Dim resp As String = ""
            Try
                resp = SendCommand("hal -r rxdev 1 OUTPUT_GPIO_LNA_PSANT")
                Dim m As Match = Regex.Match(resp, "-> *(\S*) *")
                If m.Success And m.Groups.Count >= 2 Then
                    Select Case UCase(m.Groups(1).Value)
                        Case "ON"
                            Return True
                        Case "OFF"
                            Return False
                        Case Else
                            Throw New Exception("Unexpected response: " & resp)
                    End Select
                Else
                    Throw New Exception("Unexpected response: " & resp)
                End If
            Catch ex As Exception
                Throw New Exception(resp)
            End Try
        End Get
        Set(ByVal value As Boolean)
            If value Then
                SendCommand("hal -w rxdev 1 OUTPUT_GPIO_LNA_PSANT ON")
            Else
                SendCommand("hal -w rxdev 1 OUTPUT_GPIO_LNA_PSANT OFF")
            End If
        End Set
    End Property

    Public ReadOnly Property Ethernet_IP() As String
        Get
            Dim resp As String = SendCommand("ifconfig -a")
            Dim m As Match = Regex.Match(resp.Substring(resp.IndexOf("eth0")), "inet *(\S*) *netmask")
            If m.Success And m.Groups.Count >= 2 Then
                Return m.Groups(1).Value
            Else
                Throw New Exception("Unexpected response: " & resp)
            End If
        End Get
    End Property
    Public ReadOnly Property Last_Reset_Reason() As String
        Get
            Dim resp As String = SendCommand("lastreset")
            Return Trim(resp.Substring(resp.IndexOf(":") + 1))
        End Get
    End Property

    Public Property CM_Timer() As Boolean
        Get
            Dim resp As String = ""
            Try
                resp = SendCommand("cm_timer")
                If resp.Contains("Enabled") Then Return True
                Return False
            Catch ex As Exception
                Throw New Exception("CM_Timer Unexpected response: " & resp)
            End Try
        End Get
        Set(ByVal value As Boolean)
            If value Then
                SendCommand("cm_timer enable")
            Else
                SendCommand("cm_timer disable")
            End If
        End Set
    End Property
    Public ReadOnly Property TemperaturesStr() As String
        Get
            Dim resp As String = ""
            Try
                resp = SendCommand("temperatures")
                Dim StartPos As Integer = resp.IndexOf("Temperature")
                Return resp.Substring(StartPos)
            Catch ex As Exception
                Throw New Exception("Temperatures Unexpected response: " & resp)
            End Try
        End Get
    End Property

    Public Sub GetCalfile(ByVal filename As String)
        Try
            RaiseEvent DeviceMessageReceived("Downloading 'calibration.xml' started...")
            For i As Integer = 1 To 5
                Try
                    My.Computer.Network.DownloadFile(String.Format("ftp://{0}/flash/conf/calibration.xml", Me.Address), filename, Me._Usename, Me._Password, False, 5000, True)
                    Exit For
                Catch ex As Net.WebException
                    Threading.Thread.Sleep(100 * i)
                End Try
            Next
            RaiseEvent DeviceMessageReceived("Downloading 'calibration.xml' done")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub SetCalfile(ByVal filename As String)
        Try
            RaiseEvent DeviceMessageReceived("Uploading 'calibration.xml' started...")
            For i As Integer = 1 To 5
                Try
                    My.Computer.Network.UploadFile(filename, String.Format("ftp://{0}/flash/conf/calibration.xml", Me.Address), Me._Usename, Me._Password, False, 5000)
                    Exit For
                Catch ex As Net.WebException
                    Threading.Thread.Sleep(100 * i)
                End Try
            Next
            RaiseEvent DeviceMessageReceived("Uploading 'calibration.xml' done")
            SendCommand("pkg -m /flash/conf/calibration.xml")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    WriteOnly Property GainCalMode(ByVal channel As Integer) As Boolean
        Set(ByVal value As Boolean)
            Try
                If value Then
                    _CommunicationSession.Write(String.Format("rcmd dsp ""txgain –a {0} –s gainCalMode on""", channel))
                Else
                    _CommunicationSession.Write(String.Format("rcmd dsp ""txgain –a {0} –s gainCalMode off""", channel))
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Set
    End Property

    ReadOnly Property GetFBPower(ByVal channel As Integer) As Double
        Get
            Try
                Dim resp As String
                Select Case channel
                    Case 0
                        resp = SendCommand("rcmd dsp ""dpd tx0 sigstat""")
                    Case 1
                        resp = SendCommand("rcmd dsp ""dpd tx1 sigstat""")
                    Case Else
                        Throw (New ArgumentException("InvalidParameter:GetFBPower", "channel:" & channel))
                End Select
                If resp.Contains("error") Then Return -999
                Dim StartPos As Integer = resp.LastIndexOf("Mean")
                resp = resp.Substring(StartPos)
                Dim StopPos As Integer = resp.LastIndexOf("dB")
                StopPos = resp.LastIndexOf("dB", StopPos - 1)
                StartPos = resp.LastIndexOf("|", StopPos - 1)
                resp = resp.Substring(StartPos + 1, StopPos - StartPos - 1)
                Return Convert.ToDouble(resp)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
    End Property
    Public Function ParseXmlDoc(ByVal xmlfile As System.Xml.XmlDocument) As CalDataMap
        Dim ResData As CalDataMap
        ReDim ResData.transmitter(1)
        ReDim ResData.receiver(1)
        For Each Node As System.Xml.XmlNode In xmlfile.Item("product")("system")
            Select Case Node.Name
                Case "transmitter"
                    Dim chan As Integer = Node.Attributes.GetNamedItem("id").Value
                    For Each Node1 As System.Xml.XmlNode In Node
                        Select Case Node1.Name
                            Case "txgain"
                                ResData.transmitter(chan).txgain.txAtten = Node1.Item("txAtten").InnerText
                                ResData.transmitter(chan).txgain.fbAtten = Node1.Item("fbAtten").InnerText
                                ResData.transmitter(chan).txgain.fbtxquotient = Node1.Item("fbtxquotient").InnerText
                                ResData.transmitter(chan).txgain.txTotalAtten = Node1.Item("txTotalAtten").InnerText
                                ResData.transmitter(chan).txgain.tempAtCal = Node1.Item("tempAtCal").InnerText
                                ResData.transmitter(chan).txgain.freqAtCal = Node1.Item("freqAtCal").InnerText
                                ResData.transmitter(chan).txgain.powerAtCal = Node1.Item("powerAtCal").InnerText
                                Dim count As Integer = Node1.Item("altCals").Attributes.GetNamedItem("count").Value
                                ReDim ResData.transmitter(chan).txgain.altCals(count - 1)
                                For Each Node2 As System.Xml.XmlNode In Node1.Item("altCals")
                                    Dim id As Integer = Node2.Attributes.GetNamedItem("id").Value
                                    ResData.transmitter(chan).txgain.altCals(id).freq = Node2.Item("freq").InnerText
                                    ResData.transmitter(chan).txgain.altCals(id).correction = Node2.Item("correction").InnerText
                                Next
                            Case "vswr"
                                ResData.transmitter(chan).vswr.tempAtCal = Node1.Item("tempAtCal").InnerText
                                Dim count As Integer = Node1.Item("freqCals").Attributes.GetNamedItem("count").Value
                                ReDim ResData.transmitter(chan).vswr.freqCals(count - 1)
                                For Each Node2 As System.Xml.XmlNode In Node1.Item("freqCals")
                                    Dim id As Integer = Node2.Attributes.GetNamedItem("id").Value
                                    ResData.transmitter(chan).vswr.freqCals(id).freq = Node2.Item("freq").InnerText
                                    Dim count1 As Integer = Node2.Item("vswrCals").Attributes.GetNamedItem("count").Value
                                    ReDim ResData.transmitter(chan).vswr.freqCals(id).vswrCals(count1 - 1)
                                    For Each Node3 As System.Xml.XmlNode In Node2.Item("vswrCals")
                                        Dim id1 As Integer = Node3.Attributes.GetNamedItem("id").Value
                                        ResData.transmitter(chan).vswr.freqCals(id).vswrCals(id1).adc = Node3.Item("adc").InnerText
                                        ResData.transmitter(chan).vswr.freqCals(id).vswrCals(id1).retloss = Node3.Item("retloss").InnerText
                                    Next
                                Next
                            Case Else
                                Throw New Exception(String.Format("Unexcept Node Name:{0}", Node1.Name))
                        End Select
                    Next
                Case "receiver"
                    Dim chan As Integer = Node.Attributes.GetNamedItem("id").Value
                    For Each Node1 As System.Xml.XmlNode In Node
                        Select Case Node1.Name
                            Case "rxgain"
                                ResData.receiver(chan).rxgain.rxAtten = Node1.Item("rxAtten").InnerText
                                ResData.receiver(chan).rxgain.tempAtCal = Node1.Item("tempAtCal").InnerText
                                ResData.receiver(chan).rxgain.freqAtCal = Node1.Item("freqAtCal").InnerText
                                Dim count As Integer = Node1.Item("altCals").Attributes.GetNamedItem("count").Value
                                ReDim ResData.receiver(chan).rxgain.altCals(count - 1)
                                For Each Node2 As System.Xml.XmlNode In Node1.Item("altCals")
                                    Dim id As Integer = Node2.Attributes.GetNamedItem("id").Value
                                    ResData.receiver(chan).rxgain.altCals(id).freq = Node2.Item("freq").InnerText
                                    ResData.receiver(chan).rxgain.altCals(id).correction = Node2.Item("correction").InnerText
                                Next
                            Case Else
                                Throw New Exception(String.Format("Unexcept Node Name:{0}", Node1.Name))
                        End Select
                    Next
                Case Else
                    Throw New Exception(String.Format("Unexcept Node Name:{0}", Node.Name))
            End Select
        Next Node
        Return ResData
    End Function
    Public Sub Convert2XmlDoc(ByVal InData As CalDataMap, ByVal xmlfile As System.Xml.XmlDocument)
        For Each Node As System.Xml.XmlNode In xmlfile.Item("product")("system")
            Select Case Node.Name
                Case "transmitter"
                    Dim chan As Integer = Node.Attributes.GetNamedItem("id").Value
                    For Each Node1 As System.Xml.XmlNode In Node
                        Select Case Node1.Name
                            Case "txgain"
                                Node1.Item("txAtten").InnerText = InData.transmitter(chan).txgain.txAtten
                                Node1.Item("fbAtten").InnerText = InData.transmitter(chan).txgain.fbAtten
                                Node1.Item("fbtxquotient").InnerText = InData.transmitter(chan).txgain.fbtxquotient
                                Node1.Item("txTotalAtten").InnerText = InData.transmitter(chan).txgain.txTotalAtten
                                Node1.Item("tempAtCal").InnerText = InData.transmitter(chan).txgain.tempAtCal
                                Node1.Item("freqAtCal").InnerText = InData.transmitter(chan).txgain.freqAtCal
                                Node1.Item("powerAtCal").InnerText = InData.transmitter(chan).txgain.powerAtCal
                                Node1.Item("altCals").Attributes.GetNamedItem("count").Value = InData.transmitter(chan).txgain.altCals.Length
                                For Each Node2 As System.Xml.XmlNode In Node1.Item("altCals")
                                    Dim id As Integer = Node2.Attributes.GetNamedItem("id").Value
                                    Node2.Item("freq").InnerText = InData.transmitter(chan).txgain.altCals(id).freq
                                    Node2.Item("correction").InnerText = InData.transmitter(chan).txgain.altCals(id).correction
                                Next
                            Case "vswr"
                                Node1.Item("tempAtCal").InnerText = InData.transmitter(chan).vswr.tempAtCal
                                Node1.Item("freqCals").Attributes.GetNamedItem("count").Value = InData.transmitter(chan).vswr.freqCals.Length
                                For Each Node2 As System.Xml.XmlNode In Node1.Item("freqCals")
                                    Dim id As Integer = Node2.Attributes.GetNamedItem("id").Value
                                    Node2.Item("freq").InnerText = InData.transmitter(chan).vswr.freqCals(id).freq
                                    Node2.Item("vswrCals").Attributes.GetNamedItem("count").Value = InData.transmitter(chan).vswr.freqCals(id).vswrCals.Length
                                    For Each Node3 As System.Xml.XmlNode In Node2.Item("vswrCals")
                                        Dim id1 As Integer = Node3.Attributes.GetNamedItem("id").Value
                                        Node3.Item("adc").InnerText = InData.transmitter(chan).vswr.freqCals(id).vswrCals(id1).adc
                                        Node3.Item("retloss").InnerText = InData.transmitter(chan).vswr.freqCals(id).vswrCals(id1).retloss
                                    Next
                                Next
                            Case Else
                                Throw New Exception(String.Format("Unexcept Node Name:{0}", Node1.Name))
                        End Select
                    Next
                Case "receiver"
                    Dim chan As Integer = Node.Attributes.GetNamedItem("id").Value
                    For Each Node1 As System.Xml.XmlNode In Node
                        Select Case Node1.Name
                            Case "rxgain"
                                Node1.Item("rxAtten").InnerText = InData.receiver(chan).rxgain.rxAtten
                                Node1.Item("tempAtCal").InnerText = InData.receiver(chan).rxgain.tempAtCal
                                Node1.Item("freqAtCal").InnerText = InData.receiver(chan).rxgain.freqAtCal
                                Node1.Item("altCals").Attributes.GetNamedItem("count").Value = InData.receiver(chan).rxgain.altCals.Length
                                For Each Node2 As System.Xml.XmlNode In Node1.Item("altCals")
                                    Dim id As Integer = Node2.Attributes.GetNamedItem("id").Value
                                    Node2.Item("freq").InnerText = InData.receiver(chan).rxgain.altCals(id).freq
                                    Node2.Item("correction").InnerText = InData.receiver(chan).rxgain.altCals(id).correction
                                Next
                            Case Else
                                Throw New Exception(String.Format("Unexcept Node Name:{0}", Node1.Name))
                        End Select
                    Next
                Case Else
                    Throw New Exception(String.Format("Unexcept Node Name:{0}", Node.Name))
            End Select
        Next Node
    End Sub
    Public Sub WriteXmlDoc2File(ByVal xmlfile As System.Xml.XmlDocument, ByVal filename As String)
        Dim settings As New System.Xml.XmlWriterSettings
        settings.Indent = True
        settings.IndentChars = vbTab
        Dim XmlWrt As System.Xml.XmlWriter = System.Xml.XmlWriter.Create(filename, settings)
        xmlfile.Save(XmlWrt)
    End Sub
    ReadOnly Property FBTemperature() As Double
        Get
            Dim res As Double
            Dim tmp As String = ""
            Try
                tmp = _CommunicationSession.Write("tmp123 -r FB")
                Dim Token As String = "Read"
                Dim StartPos As Integer = tmp.IndexOf(Token) + Token.Length
                Dim StopPos As Integer = tmp.IndexOf("C", StartPos)
                res = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))
                Return res
            Catch ex As Exception
                Throw New Exception(String.Format("Get FB Temperature:{0}", tmp))
            End Try
        End Get
    End Property
    ReadOnly Property GetUnitStatus(ByVal channel As Integer) As Double()
        Get
            Dim res(9) As Double
            Try
                If channel < 0 Or channel > 1 Then Throw New ArgumentException("Get Unit Status invalid channel: " & channel)
                Dim tmp As String = _CommunicationSession.Write(String.Format("radio -t vswr -a {0} -p", channel))
                Dim Token As String = "Forward Power:"
                Dim StartPos As Integer = tmp.IndexOf(Token) + Token.Length
                Dim StopPos As Integer = tmp.IndexOf("dBm", StartPos)
                res(0) = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))

                Token = "Return Loss:"
                StartPos = tmp.IndexOf(Token, StopPos) + Token.Length
                StopPos = tmp.IndexOf("dB", StartPos)
                res(1) = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))

                tmp = _CommunicationSession.Write(String.Format("amc7823 -t pa{0}", channel))
                Token = "read"
                StartPos = tmp.IndexOf(Token) + Token.Length
                StopPos = tmp.IndexOf("C", StartPos)
                res(2) = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))

                tmp = _CommunicationSession.Write(String.Format("amc7823 -t lna{0}", channel))
                Token = "read"
                StartPos = tmp.IndexOf(Token) + Token.Length
                StopPos = tmp.IndexOf("C", StartPos)
                res(3) = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))

                tmp = _CommunicationSession.Write(String.Format("radio -t pa -a {0} -p", channel))
                Token = "Instantaneous Drain Current:"
                StartPos = 0
                StopPos = 0
                For i As Integer = 0 To 3
                    StartPos = tmp.IndexOf(Token, StopPos) + Token.Length
                    StopPos = tmp.IndexOf("mA", StartPos)
                    res(4 + i) = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))
                Next

                tmp = _CommunicationSession.Write(String.Format("rcmd dsp ""txgain -a {0} -p""", channel))
                Token = "txStepAttn          ="
                StartPos = tmp.IndexOf(Token) + Token.Length
                StopPos = tmp.IndexOf(",", StartPos)
                res(8) = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))

                Token = "txDacGain           ="
                StartPos = tmp.IndexOf(Token, StopPos) + Token.Length
                StopPos = tmp.IndexOf(",", StartPos)
                res(9) = Convert.ToDouble(tmp.Substring(StartPos, StopPos - StartPos))

                Return res
            Catch ex As Exception
                Throw New Exception(String.Format("Get Unit Status"))
            End Try
        End Get
    End Property
    Public WriteOnly Property HeartBeatEnable() As Boolean
        '[
        'MESSAGE: TYPE=SET
        'TRANSACTION: ID=196
        'HEARTBEAT: STATE= DISABLE
        ']
        Set(ByVal value As Boolean)
            Dim tmp As String
            If value Then tmp = "ENABLE" Else tmp = "DISABLE"
            _TelnetCustomer.SendCommand("SET", 196, "HEARTBEAT: STATE=" & tmp)
        End Set
    End Property
    Public WriteOnly Property LNAConfig(ByVal channel As Integer) As LNACfg
        Set(ByVal value As LNACfg)
            Try
                Select Case value
                    Case LNACfg.Aux
                        _CommunicationSession.Write(String.Format("radio -t lna -a {0} -w conf aux", channel))
                    Case LNACfg.Nom
                        _CommunicationSession.Write(String.Format("radio -t lna -a {0} -w conf nom", channel))
                    Case LNACfg.Tma
                        _CommunicationSession.Write(String.Format("radio -t lna -a {0} -w conf tma", channel))
                End Select
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Set
    End Property
    Public Sub GetTimestamp()
        Try
            _TelnetCustomer.SendCommand("GET", 175, "TIMESTAMP:")
        Catch ex As Exception
            Throw New Exception("GetTimestamp Unexpected response")
        End Try
    End Sub
    Public ReadOnly Property HatchDoor_Open() As Boolean
        'rtose@voyager> hal -e
        '|--------------------------------------------------------------------------------------|
        '| Event | Name                             |   Client | Edge | Value | Count | Pending |
        '|--------------------------------------------------------------------------------------|
        '| #   0 | DOOR_OPEN                        |   None   | Both | 1     | 61    | 1       |
        '| #   1 | PA1_OVERRANGE                    | 0x1009b  |   Up | 0     | 2     | 0       |
        '| #   2 | PA0_OVERRANGE                    | 0x10097  |   Up | 0     | 2     | 0       |
        '|--------------------------------------------------------------------------------------|
        Get
            Dim resp As String = ""
            Try
                resp = SendCommand("hal -e")
                Dim m As Match = Regex.Match(resp, "\| *DOOR_OPEN *\| *(\S*) *\| *(\S*) *\| *(\d*) *\| *(\d*) *\| *(\d*) *\|")
                If m.Success And m.Groups.Count >= 6 Then
                    Select Case UCase(m.Groups(3).Value)
                        Case "1"
                            Return True
                        Case "0"
                            Return False
                        Case Else
                            Throw New Exception("Unexpected response: " & resp)
                    End Select
                Else
                    Throw New Exception("Unexpected response: " & resp)
                End If
            Catch ex As Exception
                Throw New Exception(resp)
            End Try
        End Get
    End Property


    Public Sub CustomerCarrierCfg(ByVal Power As Double, ByVal TxFreqMHz As Double, ByVal RxFreqMHz As Double, Optional ByVal CarrType As CarrTypeEnum = CarrTypeEnum.LTE_10, Optional ByVal CarrNum As Integer = 1, Optional ByVal CarrGap As Double = 10)
        Dim CarrPower As Double = Power - 10 * Math.Log10(CarrNum)
        Dim CPRISize As Integer = 2 ^ (CInt(CarrType) + 1)

        If CarrNum = 1 Then
            If CPRISize < 8 Then
                _TelnetCustomer.SendCommand("SET", 196, _
                            String.Format("CARRIERCFG: INDEX=1 STATE=ENABLE TXCONTAINER={0} TX2CONTAINER={1} RX1CONTAINER={2} RX2CONTAINER={3} RX3CONTAINER={4} RX4CONTAINER={5} GRPSIZEDOWN={6} GRPSIZEUP={7} TXFREQ={8} RXFREQ={9} SIGTYPE=NONE POWER={10} CARRTYPE={11}", _
                            1, (1 + CPRISize), 1, (1 + CPRISize), (1 + CPRISize * 2), (1 + CPRISize * 3), CPRISize, CPRISize, CInt(TxFreqMHz * 1000), CInt(RxFreqMHz * 1000), CInt(CarrPower * 10), CarrType.ToString))
            Else
                _TelnetCustomer.SendCommand("SET", 196, _
                            String.Format("CARRIERCFG: INDEX=1 STATE=ENABLE TXCONTAINER={0} TX2CONTAINER={1} RX1CONTAINER={2} RX2CONTAINER={3} RX3CONTAINER={4} RX4CONTAINER={5} GRPSIZEDOWN={6} GRPSIZEUP={7} TXFREQ={8} RXFREQ={9} SIGTYPE=NONE POWER={10} CARRTYPE={11}", _
                            1, (1 + CPRISize), 1, (1 + CPRISize), 0, 0, CPRISize, CPRISize, CInt(TxFreqMHz * 1000), CInt(RxFreqMHz * 1000), CInt(CarrPower * 10), CarrType.ToString))
            End If
        ElseIf CarrNum = 2 Then
            If CPRISize < 4 Then
                _TelnetCustomer.SendCommand("SET", 196, _
                            String.Format("CARRIERCFG: INDEX=1 STATE=ENABLE TXCONTAINER={0} TX2CONTAINER={1} RX1CONTAINER={2} RX2CONTAINER={3} RX3CONTAINER={4} RX4CONTAINER={5} GRPSIZEDOWN={6} GRPSIZEUP={7} TXFREQ={8} RXFREQ={9} SIGTYPE=NONE POWER={10} CARRTYPE={11}", _
                            1, (1 + CPRISize), 1, (1 + CPRISize), (1 + CPRISize * 2), (1 + CPRISize * 3), CPRISize, CPRISize, CInt(TxFreqMHz * 1000), CInt(RxFreqMHz * 1000), CInt(CarrPower * 10), CarrType.ToString))
                _TelnetCustomer.SendCommand("SET", 196, _
                      String.Format("CARRIERCFG: INDEX=2 STATE=ENABLE TXCONTAINER={0} TX2CONTAINER={1} RX1CONTAINER={2} RX2CONTAINER={3} RX3CONTAINER={4} RX4CONTAINER={5} GRPSIZEDOWN={6} GRPSIZEUP={7} TXFREQ={8} RXFREQ={9} SIGTYPE=NONE POWER={10} CARRTYPE={11}", _
                    1, (1 + CPRISize), (1 + CPRISize * 4), (1 + CPRISize * 5), (1 + CPRISize * 6), (1 + CPRISize * 7), CPRISize, CPRISize, CInt((TxFreqMHz + CarrGap) * 1000), CInt((RxFreqMHz + CarrGap) * 1000), CInt(CarrPower * 10), CarrType.ToString))
            Else
                _TelnetCustomer.SendCommand("SET", 196, _
                            String.Format("CARRIERCFG: INDEX=1 STATE=ENABLE TXCONTAINER={0} TX2CONTAINER={1} RX1CONTAINER={2} RX2CONTAINER={3} RX3CONTAINER={4} RX4CONTAINER={5} GRPSIZEDOWN={6} GRPSIZEUP={7} TXFREQ={8} RXFREQ={9} SIGTYPE=NONE POWER={10} CARRTYPE={11}", _
                            1, (1 + CPRISize), 1, (1 + CPRISize), 0, 0, CPRISize, CPRISize, CInt(TxFreqMHz * 1000), CInt(RxFreqMHz * 1000), CInt(CarrPower * 10), CarrType.ToString))
                _TelnetCustomer.SendCommand("SET", 196, _
                            String.Format("CARRIERCFG: INDEX=2 STATE=ENABLE TXCONTAINER={0} TX2CONTAINER={1} RX1CONTAINER={2} RX2CONTAINER={3} RX3CONTAINER={4} RX4CONTAINER={5} GRPSIZEDOWN={6} GRPSIZEUP={7} TXFREQ={8} RXFREQ={9} SIGTYPE=NONE POWER={10} CARRTYPE={11}", _
                            1, (1 + CPRISize), (1 + CPRISize * 2), (1 + CPRISize * 3), 0, 0, CPRISize, CPRISize, CInt((TxFreqMHz + CarrGap) * 1000), CInt((RxFreqMHz + CarrGap) * 1000), CInt(CarrPower * 10), CarrType.ToString))
            End If
        End If
        _CustomerCarrierConfigured = True
    End Sub

    Public Sub CustomerCarrierCfg(ByVal index As Integer, ByVal Enable As Boolean)
        Dim StrEnable As String
        If Enable Then StrEnable = "ENABLE" Else StrEnable = "DISABLE"
        _TelnetCustomer.SendCommand("SET", 196, _
              String.Format("CARRIERCFG: INDEX={0} STATE={1}", index, StrEnable))
        _CustomerCarrierConfigured = Enable
    End Sub

    Public Sub CustomerCarrierCfg(ByVal Enable As Boolean, Optional ByVal CarrNum As Integer = 1)
        Dim StrEnable As String
        If Enable Then StrEnable = "ENABLE" Else StrEnable = "DISABLE"
        For index As Integer = 1 To CarrNum
            _TelnetCustomer.SendCommand("SET", 196, _
                String.Format("CARRIERCFG: INDEX={0} STATE={1}", index, StrEnable))
        Next
        _CustomerCarrierConfigured = Enable
    End Sub

    Public WriteOnly Property CustomerTxEnable(Optional ByVal CarrNum As Integer = 1) As Boolean
        Set(ByVal value As Boolean)
            Dim tmp As String
            If value Then tmp = "ENABLE" Else tmp = "DISABLE"
            For index As Integer = 1 To CarrNum
                _TelnetCustomer.SendCommand("SET", 196, _
              String.Format("CARRIERCFG: INDEX={0} STATE={1}", index, tmp))
            Next
            _CustomerCarrierConfigured = value
        End Set
    End Property

    Public ReadOnly Property CustomerCarrierConfigured() As Boolean
        Get
            Return _CustomerCarrierConfigured
        End Get
    End Property


    Public Function DeBiasPA(ByVal channel As Integer) As Boolean 'Implements ITransceiver.DeBiasPA
        Try
            If channel < 0 Or channel > 1 Then
                Throw New Exception("Tx channel must be 0 or 1: " & channel)
            End If
            Dim cmd As String = String.Format("radio -t pa -a {0} -w off", channel)
            Dim answer As String = SendCommand(cmd)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Rssi_ARM() As RssiData_ARM

        '        RX1
        '---------------------------------------------------
        '| carrier | container | frequency   | rssi (dBm)  |
        '---------------------------------------------------
        '| 0       | 1         | 1865.00 MHz | -107.09 dBm |
        '| 1       | 9         | 1895.00 MHz | -107.38 dBm |
        '---------------------------------------------------
        '        RX2
        '---------------------------------------------------
        '| carrier | container | frequency   | rssi (dBm)  |
        '---------------------------------------------------
        '| 0       | 3         | 1865.00 MHz | -107.69 dBm |
        '| 1       | 11        | 1895.00 MHz | -107.09 dBm |
        '---------------------------------------------------
        '        RX3
        '---------------------------------------------------
        '| carrier | container | frequency   | rssi (dBm)  |
        '---------------------------------------------------
        '| 0       | 5         | 1865.00 MHz | -107.38 dBm |
        '| 1       | 13        | 1895.00 MHz | -107.09 dBm |
        '---------------------------------------------------
        '        RX4
        '---------------------------------------------------
        '| carrier | container | frequency   | rssi (dBm)  |
        '---------------------------------------------------
        '| 0       | 7         | 1865.00 MHz | -107.09 dBm |
        '| 1       | 15        | 1895.00 MHz | -106.82 dBm |
        '---------------------------------------------------
        Dim rs As RssiData_ARM
        Dim mc As MatchCollection
        Dim resp As String = SendCommand("rssi")
        Dim fields() As String = Regex.Split(resp, "RX\d")
        If fields.Length < 2 Then
            Throw New Exception("Unexpected RSSI response: " & resp)
        End If
        ReDim rs.Rx(fields.Length - 2)
        For i As Integer = 0 To rs.Rx.Length - 1
            mc = Regex.Matches(fields(i + 1), "\| *(\d+) *\| *(\d+) *\| *(\S+) *\S+ *\| *(\S+) *\S+ *\|")
            If mc.Count = 0 Then
                Throw New Exception("Unexpected RSSI response: " & resp)
            End If
            ReDim rs.Rx(i).Carr(mc.Count - 1)
            For j As Integer = 0 To rs.Rx(i).Carr.Length - 1
                Try
                    rs.Rx(i).Carr(j).carrier = CDbl(mc(j).Groups(1).Value)
                Catch
                    rs.Rx(i).Carr(j).carrier = -999
                End Try
                Try
                    rs.Rx(i).Carr(j).container = CDbl(mc(j).Groups(2).Value)
                Catch
                    rs.Rx(i).Carr(j).container = -999
                End Try
                Try
                    rs.Rx(i).Carr(j).frequency = CDbl(mc(j).Groups(3).Value)
                Catch
                    rs.Rx(i).Carr(j).frequency = -999
                End Try
                Try
                    rs.Rx(i).Carr(j).rssi = CDbl(mc(j).Groups(4).Value)
                Catch
                    rs.Rx(i).Carr(j).rssi = -999
                End Try
            Next
        Next
        Return rs
    End Function

    Public Function Rssi() As RssiData
        '        rtose@voyager> rcmd dsp "rssi -p"
        'Rx path 0 LTE:
        '    Carrier  Raw Rssi(dBm)   Pcg Rssi(dBm)
        '     0        -106.57         -105.78
        '     1        -106.69         -105.88
        'Rx path 1 LTE:
        '    Carrier  Raw Rssi(dBm)   Pcg Rssi(dBm)
        '     0        -106.21         -105.38
        '     1        -106.33         -105.48
        'Rx path 2 LTE:
        '    Carrier  Raw Rssi(dBm)   Pcg Rssi(dBm)
        '     0        -106.96         -106.10
        '     1        -107.09         -106.21
        'Rx path 3 LTE:
        '    Carrier  Raw Rssi(dBm)   Pcg Rssi(dBm)
        '     0        -106.96         -106.10
        '     1        -106.82         -105.99
        Dim rs As RssiData
        Dim mc As MatchCollection
        Dim resp As String = SendCommand("rcmd dsp ""rssi -p""")
        Dim fields() As String = Regex.Split(resp, "Rx path \d")
        If fields.Length < 2 Then
            Throw New Exception("Unexpected RSSI response: " & resp)
        End If
        ReDim rs.Rx(fields.Length - 2)
        For i As Integer = 0 To rs.Rx.Length - 1
            mc = Regex.Matches(fields(i + 1), "(\d) +(\S+) +(\S+)")
            If mc.Count = 0 Then
                Throw New Exception("Unexpected RSSI response: " & resp)
            End If
            ReDim rs.Rx(i).Carr(mc.Count - 1)
            For j As Integer = 0 To rs.Rx(i).Carr.Length - 1
                Try
                    rs.Rx(i).Carr(j).carrier = CDbl(mc(j).Groups(1).Value)
                Catch
                    rs.Rx(i).Carr(j).carrier = -999
                End Try
                Try
                    rs.Rx(i).Carr(j).Raw_Rssi = CDbl(mc(j).Groups(2).Value)
                Catch
                    rs.Rx(i).Carr(j).Raw_Rssi = -999
                End Try
                Try
                    rs.Rx(i).Carr(j).Pcg_Rssi = CDbl(mc(j).Groups(3).Value)
                Catch
                    rs.Rx(i).Carr(j).Pcg_Rssi = -999
                End Try
            Next
        Next
        Return rs
    End Function

    Public Function LnaReadAdc(ByVal TxPath As LnaDetSwitchEnum) As LnaAdcData
        'lna readadc <detSW>..............read all A/D channels (no avg)
        '    0-EF  fwd, EF  rev (gpio 0x0X)
        '    1-TX1 fwd, TX0 rev (gpio 0x1X)
        '    2-TX0 fwd, TX1 rev (gpio 0x2X)
        '    3-none   , none    (gpio 0x3X)
        'TRDU700P1.3#RDK>lna readadc 2
        'Chan  --Desc--  Counts  EngValue  Units
        '   0  xxx diff      91         0
        '   1  tx0 fwd      143         0
        '   2  tx1 rev      144         0
        '   3      temp    1232        25  degC
        '   4      cur0      63         9  mA
        '   5      cur1      62         8  mA
        'If detSW < 0 Or detSW > 2 Then
        '    Throw New ArgumentException("LnaReadAdc: Invalid parameter 'detSW=" & detSW & "'")
        'End If
        'Dim resp As String = _TelnetSession.SendCommandAndReceive("lna readadc " & detSW)
        'Dim AdcData As LnaAdcData, a2d As Integer, eng As Double
        'GetAdcFields(resp, "diff", AdcData.DiffA2D, eng)
        'GetAdcFields(resp, "fwd", AdcData.FwdA2D, eng)
        'GetAdcFields(resp, "rev", AdcData.RevA2D, eng)
        'GetAdcFields(resp, "temp", a2d, AdcData.TempDegC)
        'GetAdcFields(resp, "cur0", a2d, AdcData.Cur0mA)
        'GetAdcFields(resp, "cur1", a2d, AdcData.Cur1mA)
        'Return AdcData

        Dim AdcData As LnaAdcData
        If TxPath < 1 Or TxPath > 2 Then

            Throw New ArgumentException("Invalid argument to Vswr function: Txpath: " & TxPath)
        End If
        'old line:
        'Dim resp As String = _TelnetSession.SendCommandAndReceive("vswr readvswr " & (Txpath + 1))
        '--------------------------------Fwd vswr ---------------------------------------------------------------------
        Dim resp As String = SendCommand("hal -r txdev " & (2 - TxPath) & " SENSOR_POWER_FWD")  'new command
        Dim token As String
        ' Dim m As Match
        Dim startpos, stoppos As Integer
        If resp.Contains("(") Then
            token = "("
            startpos = resp.IndexOf(token) + token.Length
            stoppos = resp.IndexOf(")", startpos)
            resp = resp.Substring(startpos, stoppos - startpos).Trim()
            ' m = CInt(resp)
        End If
        If Not resp.Contains("0x") Then
            Throw New Exception("Unexpected response from Vswr function: " & resp)
        End If

        Dim tmpResponse, fwdAgc As Integer
        tmpResponse = Convert.ToInt16(resp, 16)
        fwdAgc = tmpResponse


        ' ---------------------------Diff vswr-----------------------------------------------------------
        resp = SendCommand("hal -r txdev " & (2 - TxPath) & " SENSOR_POWER_VSWR")  'new command
        ' Dim m As Match
        If resp.Contains("(") Then
            token = "("
            startpos = resp.IndexOf(token) + token.Length
            stoppos = resp.IndexOf(")", startpos)
            resp = resp.Substring(startpos, stoppos - startpos).Trim()
            ' m = CInt(resp)
        End If
        If Not resp.Contains("0x") Then
            Throw New Exception("Unexpected response from Vswr function: " & resp)
        End If

        Dim diffAgc As Integer
        tmpResponse = Convert.ToInt16(resp, 16)
        diffAgc = tmpResponse
        '-------------------------------REV vswr-----------------------------------------------------------------
        resp = SendCommand("hal -r txdev " & (2 - TxPath) & " SENSOR_POWER_REV")  'new command
        ' Dim m As Match
        If resp.Contains("(") Then
            token = "("
            startpos = resp.IndexOf(token) + token.Length
            stoppos = resp.IndexOf(")", startpos)
            resp = resp.Substring(startpos, stoppos - startpos).Trim()
            ' m = CInt(resp)
        End If
        If Not resp.Contains("0x") Then
            Throw New Exception("Unexpected response from Vswr function: " & resp)
        End If

        Dim revAgc As Integer
        tmpResponse = Convert.ToInt16(resp, 16)
        revAgc = tmpResponse

        '-------------------------------Temperature-----------------------------------------------------------------
        resp = SendCommand("hal -r txdev " & (2 - TxPath) & " SENSOR_TEMP_VSWR")  'new command
        ' Dim m As Match
        If Not resp.Contains("0x") Then
            Throw New Exception("Unexpected response from Vswr function: " & resp)
        End If
        If resp.Contains("->") Then
            token = "->"
            startpos = resp.IndexOf(token) + token.Length
            stoppos = resp.IndexOf("DegC", startpos)
            resp = resp.Substring(startpos, stoppos - startpos).Trim()
            ' m = CInt(resp)
        End If

        Dim TempC As Double
        TempC = CDbl(resp)


        AdcData.DiffA2D = diffAgc
        AdcData.FwdA2D = fwdAgc
        AdcData.RevA2D = revAgc
        AdcData.TempDegC = TempC
        AdcData.Cur0mA = 0
        AdcData.Cur1mA = 0
        Return AdcData
    End Function

    Public ReadOnly Property GainStatus(ByVal TxChan As Integer) As GainStatusData
'dsp> txgain -a 0 -p
'Tx Gain Values for path 0

'        enabled             =   off,  update count           =     0
'        txStepAttn          = 31.50,  ocpmGain               =     0
'        txDacGain           =  0.00,  fbTxQuotient           = 1.620
'        total tx attn       = 31.50,  nomGain                = 1.594
'        fbStepAtten         =  7.00,  nomGainChangeTemp      = 0.984
'        gainError           =  1.00,  nomGainChangeOverdrive = 1.000
'        fbAdcGain           =  0.00,  nomGainChangeOcpm      = 1.000
'                                      nomGainChangeDcV       = 1.000
'                                      nomGainChangeFreq      = 1.000
        Get
            Try
                If TxChan < 0 Or TxChan > 1 Then Throw New ArgumentException("InvalidParameter:Channel", "channel:" & TxChan)
                Dim resp As String = SendCommand(String.Format("rcmd dsp ""txgain -a {0} -p""", TxChan))
                Dim gs As GainStatusData
                gs.Vca = 0
                gs.TxStep = CDbl(GetGainStatusField(resp, "txStepAttn"))
                gs.FbStep = CDbl(GetGainStatusField(resp, "fbStepAtten"))
                'gs.FbTxQuo = CInt(GetGainStatusField(resp, "fbTxQuotient"))
                gs.FbTxQuo = CDbl(GetGainStatusField(resp, "fbTxQuotient"))
                gs.GainError = CDbl(GetGainStatusField(resp, "gainError"))
                gs.Enabled = (GetGainStatusField(resp, "enabled").ToLower = "on")
                gs.txDacGain = CDbl(GetGainStatusField(resp, "txDacGain"))
                gs.total_tx_attn = CDbl(GetGainStatusField(resp, "total tx attn"))
                Return gs
            Catch ex As Exception
                Dim gs As GainStatusData
                gs.Vca = -99999
                gs.TxStep = -99999
                gs.FbStep = -99999
                gs.FbTxQuo = -99999
                gs.GainError = -99999
                gs.Enabled = -99999
                gs.txDacGain = -99999
                gs.total_tx_attn = -99999
                Return gs
            End Try

        End Get
    End Property

    Private Shared Function GetGainStatusField(ByVal resp As String, ByVal FieldName As String) As String
        Dim m As Match
        m = Regex.Match(resp, FieldName & " *= *([\w\.]+)")
        If m.Success = False Then
            'Add for "gainError           =  +Inf" issue
            If FieldName = "gainError" Then
                Return 99999
            Else
                Throw New Exception("Gain status field '" & FieldName & "' not found in resp: " & resp)
            End If
        End If
        Return m.Groups(1).Value
    End Function

    Public Function TxDPDTable(ByVal channel As Integer) As Double()
'Layer 1 Object Parameters
'  Data(Collection)
'    Channel                       : 0
'    trigger Type                  : 0
'    Number of captures            : 16
'    Number of transfers / capture : 15
'    Number of samples / transfer  : 512

'  Sequence Alignment Parameters
'    Amplifier Delay               : 29271 29801/16
'    Reference Channel Gain        : 32767+j0      (32767.00<0.00)
'    Feedback Channel Gain         : 32767+j0      (32767.00<0.00)

'  Constants()
'    Learning Constant (Mu)        : 16384
'    Third Order Offset            : 0+j0          (0.00<0.00)

'  Estimated(Parameters)
'    Maximum Table Gain (mag2)     : 1701017669 (+8.0187 db)
'    Minimun Table Gain (mag2)     : 7827276 (-15.3523 db)
'    Maximum Populated Table Index : 0
        Dim tmpString As String = String.Empty
        Dim tmpArr(1) As Double
        Try
            Dim token As String = String.Format("rcmd dsp ""dpd tx{0} status l1obj""", channel)
            tmpString = SendCommand(token)

            token = "Maximum Table Gain (mag2)     : "
            Dim StartPos As Integer = tmpString.IndexOf(token) + token.Length
            'Dim StopPos As Integer = tmpString.IndexOf(" db)") + 4   
            Dim StopPos As Integer = tmpString.IndexOf(" db)")
            tmpArr(0) = CDbl(tmpString.Substring(StartPos, StopPos - StartPos).Split("(")(1))
            token = "Minimun Table Gain (mag2)     : "
            StartPos = tmpString.IndexOf(token) + token.Length
            'StopPos = tmpString.Substring(StartPos).IndexOf(" db)") + StartPos + 4
            StopPos = tmpString.Substring(StartPos).IndexOf(" db)") + StartPos
            tmpArr(1) = CDbl(tmpString.Substring(StartPos, StopPos - StartPos).Split("(")(1))

            Return tmpArr
        Catch ex As Exception
            'Throw New Exception("Unexpected response for dpd command: " & tmpString)
            tmpArr(0) = -99999
            tmpArr(1) = -99999
            Return tmpArr
        End Try
    End Function

    Public ReadOnly Property PSStatus() As PSStatusData
'rtose@voyager> power_sup -r
' Input Voltage     ->    49.03 V
' Input Current     ->     1.31 A
' Input Power       ->    64.28 W
' Output Voltage    ->    29.95 V
' AISG 12V Voltage  ->     6.47 V
' AISG 12V Current  ->     0.74 A
' AISG 24V Voltage  ->    14.31 V
' AISG 24V Current  ->     0.97 A
        Get
            Dim cmd As String = "power_sup -r"
            Dim resp As String = SendCommand(cmd)
            Dim m As MatchCollection = Regex.Matches(resp, "\-> +(\S+) +\S+")
            Dim data As PSStatusData
            If m.Count <> 8 Then
                Throw New Exception("FpgaRead invalid resp: " & resp)
            End If
            Try
                data.Input_Voltage = m(0).Groups(1).Value
                data.Input_Current = m(1).Groups(1).Value
                data.Input_Power = m(2).Groups(1).Value
                data.Output_Voltage = m(3).Groups(1).Value
                data.AISG_12V_Voltage = m(4).Groups(1).Value
                data.AISG_12V_Current = m(5).Groups(1).Value
                data.AISG_24V_Voltage = m(6).Groups(1).Value

                data.AISG_24V_Current = AISG_24V_Curr
                Return data
            Catch ex As Exception
                data.Input_Voltage = -99999
                data.Input_Current = -99999
                data.Input_Power = -99999
                data.Output_Voltage = -99999
                data.AISG_12V_Voltage = -99999
                data.AISG_12V_Current = -99999
                data.AISG_24V_Voltage = -99999
                data.AISG_24V_Current = -99999
                Return data
            End Try

        End Get
    End Property

    Public Property LNAAttenuator(ByVal channel As Integer) As Double
        Get
            If channel < 0 Or channel > 3 Then Throw New ArgumentException("InvalidParameter:RxAttenuator", "channel:" & channel)
            Dim dev() As String = {"LNA0", "LNA1", "LNA2", "LNA3"}
            Return PE4302_Atten(dev(channel))
        End Get
        Set(ByVal value As Double)
            If channel < 0 Or channel > 3 Then Throw New ArgumentException("InvalidParameter:RxAttenuator", "channel:" & channel)
            Dim dev() As String = {"LNA0", "LNA1", "LNA2", "LNA3"}
            PE4302_Atten(dev(channel)) = value
        End Set
    End Property

    Private Property PE4302_Atten(ByVal dev As String) As Double
        Get
            Dim token As String = "pe4302 -r " & dev
            Dim tmpString As String = SendCommand(token)

            Dim mymatch As Match
            mymatch = Regex.Match(tmpString, "PE4302 device \S* value = (\S*) db")
            If mymatch.Success Then
                Dim value As String = mymatch.Groups(1).ToString
                Return CDbl(value)
            Else
                Throw New Exception("Invalid response for command '" & token & "': " & tmpString)
            End If
        End Get
        Set(ByVal value As Double)
            If value >= -0.1 And value <= 31.6 Then
                value = CDbl(CInt(value * 2) / 2)  'make sure value is multiple of 0.5
                Dim token As String = String.Format("pe4302 -w {0} {1:F1}", dev, value)
                Dim tmpString As String = SendCommand(token)
                If tmpString.ToUpper().Contains("ERROR WRITING") Then
                    Throw New Exception("Invalid response for command '" & token & "': " & tmpString)
                End If
            Else
                Throw New ArgumentException("InvalidParameter:PE4302_Atten", "value:" & value)
            End If
        End Set
    End Property

    Public ReadOnly Property RxAttenuator() As Double()
'dsp> rxattn -p
'Rx attenuator setting for channel 0 is 16.5 dB
'Rx attenuator setting for channel 1 is 15.0 dB
'Rx attenuator setting for channel 2 is 16.0 dB
'Rx attenuator setting for channel 3 is 15.5 dB
'Rx attenuator setting for channel 4 is 17.5 dB
'Rx attenuator setting for channel 5 is 16.0 dB
'Rx attenuator setting for channel 6 is 17.0 dB
'Rx attenuator setting for channel 7 is 16.0 dB
        Get
            Dim token As String = "rcmd dsp ""rxattn -p"""
            Dim tmpString As String = SendCommand(token)
            Dim mymatch As MatchCollection
            mymatch = Regex.Matches(tmpString, "Rx attenuator setting for channel \d is +(\S+) dB")
            If mymatch.Count = 8 Then
                Dim value(7) As Double
                For i As Integer = 0 To 7
                    value(i) = mymatch(i).Groups(1).ToString
                Next
                Return value
            Else
                Throw (New Exception("Invalid response for command '" & token & "': " & tmpString))
            End If
        End Get
    End Property

    Public ReadOnly Property PaStatus(ByVal channel As Integer) As PaStatusData
    'rtose@voyager> radio -t pa -p
'PA 0 Status Info:
'   Setup Flavor:        Viper1900
'   Machine State:       Biased
'   Status:              OK
'   EEPROM Data Use:     False
'   Bias Compensation:   ON
'   PA turned on:        NO
'   VVA:                 2400 counts
'   Overcurrent Ref:     3100 counts
'   OC Alarm Status:     OFF
'   Current Temp:        33.37 C
'   Bias Point Temp:     28.61 C
'   Vdrain:              29.97 Volt
'   Last Biasing time:   5.79 sec
'   Last Num bias steps: 58 (max 250)
'Transistor Name: Driver1
'   Current Status:                Biased
'   Type:                          Peak
'   Drain Target Current:          0.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          900
'   Instantaneous Drain Current:   0.24 mA
'Transistor Name: Driver2
'   Current Status:                Biased
'   Type:                          Carrier
'   Drain Target Current:          40.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          1464
'   Instantaneous Drain Current:   0.24 mA
'Transistor Name: Driver3
'   Current Status:                Biased
'   Type:                          Carrier
'   Drain Target Current:          260.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          1431
'   Instantaneous Drain Current:   2.44 mA
'Transistor Name: Driver4
'   Current Status:                Biased
'   Type:                          Carrier
'   Drain Target Current:          40.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          1458
'   Instantaneous Drain Current:   0.24 mA
'Transistor Name: Final1
'   Current Status:                Biased
'   Type:                          Peak
'   Drain Target Current:          0.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          295
'   Instantaneous Drain Current:   16.28 mA
'Transistor Name: Final2
'   Current Status:                Biased
'   Type:                          Carrier
'   Drain Target Current:          900.00 mA
'   Dac Loaded Value:              0
'   Dac Bias Point Value:          962
'   Instantaneous Drain Current:   16.28 mA

        Get
            Dim cmd As String = String.Format("radio -t pa -a {0} -p", channel)
            Dim resp As String = SendCommand(cmd)
            Dim data As PaStatusData

            Try
                data.Vdrain = GetRegexField(resp, "Vdrain: *(\S+) *Volt")
                data.Temperature = GetRegexField(resp, "Current Temp: *(\S+) *C")
                data.Bias_Point_Temp = GetRegexField(resp, "Bias Point Temp: *(\S+) *C")

                Dim fields() As String = Regex.Split(resp, "Transistor Name:")

                If fields.Length < 2 Then
                    Throw New Exception("Unexpected response: " & resp)
                End If

                data.CurrentDriver1 = GetRegexField(fields(1), "Instantaneous Drain Current: *(\S+) *mA")
                data.CurrentDriver2 = GetRegexField(fields(2), "Instantaneous Drain Current: *(\S+) *mA")
                data.CurrentDriver3 = GetRegexField(fields(3), "Instantaneous Drain Current: *(\S+) *mA")
                data.CurrentDriver4 = GetRegexField(fields(4), "Instantaneous Drain Current: *(\S+) *mA")
                data.CurrentFinal1 = GetRegexField(fields(5), "Instantaneous Drain Current: *(\S+) *mA")
                data.CurrentFinal2 = GetRegexField(fields(6), "Instantaneous Drain Current: *(\S+) *mA")
                data.DacDriver1 = GetRegexField(fields(1), "Dac Loaded Value: *(\S+)")
                data.DacDriver2 = GetRegexField(fields(2), "Dac Loaded Value: *(\S+)")
                data.DacDriver3 = GetRegexField(fields(3), "Dac Loaded Value: *(\S+)")
                data.DacDriver4 = GetRegexField(fields(4), "Dac Loaded Value: *(\S+)")
                data.DacFinal1 = GetRegexField(fields(5), "Dac Loaded Value: *(\S+)")
                data.DacFinal2 = GetRegexField(fields(6), "Dac Loaded Value: *(\S+)")
                Return data
            Catch ex As Exception
                data.Vdrain = -99999
                data.Temperature = -99999
                data.Bias_Point_Temp = -99999
                data.CurrentDriver1 = -99999
                data.CurrentDriver2 = -99999
                data.CurrentDriver3 = -99999
                data.CurrentDriver4 = -99999
                data.CurrentFinal1 = -99999
                data.CurrentFinal2 = -99999
                data.DacDriver1 = -99999
                data.DacDriver2 = -99999
                data.DacDriver3 = -99999
                data.DacDriver4 = -99999
                data.DacFinal1 = -99999
                data.DacFinal2 = -99999
                Return data
            End Try

        End Get
    End Property

    Private Shared Function GetRegexField(ByVal input As String, ByVal pattern As String) As String
        Dim m As Match = Regex.Match(input, pattern)
        If m.Success Then
            Return m.Groups(1).Value
        Else
            Throw New Exception("Regular expression '" & pattern & "'not found in string: " & input)
        End If
    End Function

    Private Shared Function GetRegexFields(ByVal input As String, ByVal pattern As String, ByVal NumFields As Integer) As String()
        Dim m As Match = Regex.Match(input, pattern)
        If m.Success Then
            Dim ret(NumFields - 1) As String
            For i As Integer = 0 To NumFields - 1
                ret(i) = m.Groups(i + 1).Value
            Next
            Return ret
        Else
            Throw New Exception("Regular expression '" & pattern & "'not found in string: " & input)
        End If
    End Function

    Public ReadOnly Property TempSensors() As TempSensorData
        Get
            Dim ts As TempSensorData
            Dim resp As String = SendCommand("hal -r txdev 0")
            Try
                ts.PA0 = GetRegexField(resp, "SENSOR_TEMP_PA *-> *(\S+) *DegC *\(\S+\)")
                ts.PA0VSWR = GetRegexField(resp, "SENSOR_TEMP_VSWR *-> *(\S+) *DegC *\(\S+\)")

                resp = SendCommand("hal -r txdev 1")
                ts.PA1 = GetRegexField(resp, "SENSOR_TEMP_PA *-> *(\S+) *DegC *\(\S+\)")
                ts.PA1VSWR = GetRegexField(resp, "SENSOR_TEMP_VSWR *-> *(\S+) *DegC *\(\S+\)")

                resp = SendCommand("hal -r rxdev 0")
                ts.LNA0 = GetRegexField(resp, "SENSOR_TEMP_LNA *-> *(\S+) *DegC *\(\S+\)")
                resp = SendCommand("hal -r rxdev 1")
                ts.LNA1 = GetRegexField(resp, "SENSOR_TEMP_LNA *-> *(\S+) *DegC *\(\S+\)")
                resp = SendCommand("hal -r rxdev 2")
                ts.LNA2 = GetRegexField(resp, "SENSOR_TEMP_LNA *-> *(\S+) *DegC *\(\S+\)")
                resp = SendCommand("hal -r rxdev 3")
                ts.LNA3 = GetRegexField(resp, "SENSOR_TEMP_LNA *-> *(\S+) *DegC *\(\S+\)")

                resp = SendCommand("hal -r sysdev")
                ts.PS_CONVERTER = GetRegexField(resp, "SENSOR_TEMP_PS_CONVERTER *-> *(\S+) *DegC *\(\S+\)")
                ts.PS_BRICK = GetRegexField(resp, "SENSOR_TEMP_PS_BRICK *-> *(\S+) *DegC *\(\S+\)")
                ts.RX = GetRegexField(resp, "SENSOR_TEMP_RX0 *-> *(\S+) *DegC *\(\S+\)")
                ts.FB = GetRegexField(resp, "SENSOR_TEMP_FB0 *-> *(\S+) *DegC *\(\S+\)")
                Return ts
            Catch ex As Exception
                ts.PA0 = -99999
                ts.PA0VSWR = -99999
                ts.PA1 = -99999
                ts.PA1VSWR = -99999
                ts.LNA0 = -99999
                ts.LNA1 = -99999
                ts.LNA2 = -99999
                ts.LNA3 = -99999
                ts.PS_CONVERTER = -99999
                ts.PS_BRICK = -99999
                ts.RX = -99999
                ts.FB = -99999
                Return ts
            End Try
        End Get
  End Property

  Public Function GetFIFOValue(ByVal chan As Double) As Integer()
    Dim res(1) As Integer
    Dim resp As String = ""
    Try
      resp = SendCommand("rcmd dsp ""fifo -g tx" & chan & " -l 8192 -f /null""")
      Dim m As MatchCollection = Regex.Matches(resp, "\s=\s(\d+)")
      If m.Count <> 2 Then Throw New Exception("Unexpected response: " & resp)
      Dim I As Integer = 0
      For Each ma As Match In m
        res(I) = ma.Groups(1).Value
        I = I + 1
      Next
      Return res
    Catch ex As Exception
      Throw New Exception("Unexpected response: " & resp)
    End Try
  End Function

  Public Sub GetFIFOData(ByVal path As SigPath, ByVal Numpoints As Integer, ByVal filename As String)
    Try
      RaiseEvent DeviceMessageReceived(String.Format("Downloading Fifo Data to file {0} started...", filename))
      Dim pathstr As String = ""
      Select Case path
        Case SigPath.TX0
          pathstr = "tx0"
        Case SigPath.TX1
          pathstr = "tx1"
        Case SigPath.RX0
          pathstr = "rx0"
        Case SigPath.RX1
          pathstr = "rx1"
        Case SigPath.RX2
          pathstr = "rx2"
        Case SigPath.RX3
          pathstr = "rx3"
      End Select
      Dim tmpString As String = SendCommand(String.Format("rcmd dsp ""fifo -g {0} -l {1} -f /ram/fifo_{0}.dat""", pathstr, Numpoints))
      If tmpString.Contains("capture complete") = False Then
        Throw New Exception(String.Format("Get FIFO:{0}", tmpString))
      End If
      For i As Integer = 1 To 5
        Try
          My.Computer.Network.DownloadFile(String.Format("ftp://{0}/ram/fifo_{1}.dat", Me.Address, pathstr), filename, Me._Usename, Me._Password, False, 5000, True)
          Exit For
        Catch ex As Net.WebException
          Threading.Thread.Sleep(100 * i)
        End Try
      Next
      RaiseEvent DeviceMessageReceived(String.Format("Downloading Fifo Data to file {0} done.", filename))
      SendCommand(String.Format("rm /ram/fifo_{0}.dat", pathstr))
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Sub GetFIFOFtp(ByVal path As String, ByVal Filename As String)
    Try
      RaiseEvent DeviceMessageReceived(String.Format("Downloading Fifo Data to file {0} started...", filename))
      Dim pathstr As String = path.ToLower
      Dim tmpString As String = SendCommand(String.Format("rcmd dsp ""fifo -g {0} -l {1} -f /ram/fifo_{0}.dat""", pathstr, 8192))
      If tmpString.Contains("capture complete") = False Then
        Throw New Exception(String.Format("Get FIFO:{0}", tmpString))
      End If
      For i As Integer = 1 To 5
        Try
          My.Computer.Network.DownloadFile(String.Format("ftp://{0}/ram/fifo_{1}.dat", Me.Address, pathstr), filename, Me._Usename, Me._Password, False, 5000, True)
          Exit For
        Catch ex As Net.WebException
          Threading.Thread.Sleep(100 * i)
        End Try
      Next
      RaiseEvent DeviceMessageReceived(String.Format("Downloading Fifo Data to file {0} done.", filename))
      SendCommand(String.Format("rm /ram/fifo_{0}.dat", pathstr))
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  WriteOnly Property RxCal_Enable() As Boolean
    Set(ByVal value As Boolean)
      Try
        If value Then
          SendCommand("rcmd dsp ""rxcal -e""")
        Else
          SendCommand("rcmd dsp ""rxcal -d""")
        End If
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Set
  End Property

  Public Sub RxCalSetRxPhysicalChannelFrequency(ByVal Channel As Integer, ByVal CenterFrequency As Double, ByVal CarrierFrequency As Double)
    If Channel > -1 And Channel < 8 Then SendCommand(String.Format("rcmd dsp ""rxcal –a {0} –s freq {1} {2}""", Channel, CenterFrequency, CarrierFrequency))
  End Sub

  Public Sub RxCalSetRxPhysicalChannelAttn(ByVal Channel As Integer, ByVal Attn As Double)
    If Channel > -1 And Channel < 8 Then SendCommand(String.Format("rcmd dsp ""rxcal –a {0} –s att {1}""", Channel, Attn))
  End Sub

  Public Sub RxCalSetRxPhysicalChannelGain(ByVal Channel As Integer, ByVal Gain As Double)
    If Channel > -1 And Channel < 8 Then SendCommand(String.Format("rcmd dsp ""rxcal –a {0} –s gain {1}""", Channel, Gain))
  End Sub

  Public Function RxCalReadRSSI(ByVal Channel As Integer) As Double
    If Channel > -1 And Channel < 8 Then
      Dim tmpstr As String = SendCommand(String.Format("rcmd dsp ""rxcal –a {0} –r""", Channel))
      Return CDbl(GetRegexField(tmpstr, "Rx physical channel after pcg \d *= *(\S+) *dBm"))
    End If
  End Function

  Public Sub RxCalSaveRxPhysicalChannelAttn(ByVal Channel As Integer, ByVal Band As Integer, ByVal Attn As Double)
    If Channel > -1 And Channel < 8 Then SendCommand(String.Format("rxcal -a {0} -b {1} -s att {2}""", Channel, Band, Attn))
  End Sub

  Public Sub RxCalSaveRxPhysicalChannelFrequencyRef(ByVal Channel As Integer, ByVal Band As Integer, ByVal CarrierFrequency As Double)
    If Channel > -1 And Channel < 8 Then SendCommand(String.Format("rxcal -a {0} -b {1} -s freq {2}""", Channel, Band, CarrierFrequency))
  End Sub

  Public Sub RxCalSaveRxPhysicalChannelFrequency(ByVal Channel As Integer, ByVal Band As Integer, ByVal Position As Integer, ByVal CarrierFrequency As Double)
    If Channel > -1 And Channel < 8 Then SendCommand(String.Format("rxcal –a {0} –b {1} –q {2} –s freq {3}""", Channel, Band, Position, CarrierFrequency))
  End Sub

  Public Function RxCalGetTemperature(ByVal Channel As Integer) As Double
    If Channel > -1 And Channel < 8 Then
      Dim tmpstr As String = SendCommand(String.Format("rxgain –a {0} –g temp""", Channel))
      Return CDbl(GetRegexField(tmpstr, "temperature = 54.200 C"))
    End If
  End Function

  Public Sub RxCalSaveTemperature(ByVal Channel As Integer, ByVal Band As Integer, ByVal Temp As Double)
    If Channel > -1 And Channel < 8 Then SendCommand(String.Format("rxcal –a {0} –b {1} –s temp {2}""", Channel, Band, Temp))
  End Sub

  Public Sub RxCalSaveRxPhysicalChannelGain(ByVal Channel As Integer, ByVal Band As Integer, ByVal Position As Integer, ByVal Gain As Double)
    If Channel > -1 And Channel < 8 Then SendCommand(String.Format("rxcal –a {0} –b {1} –q {2} –s att {3}""", Channel, Band, Position, Gain))
    End Sub

    Public Function InitBurnInCommands() As Boolean
        Try
            'set Internal Waveform
            _CommunicationSession.Write("alarm -m 0 0 0 8")
            _CommunicationSession.Write("cm_timer disable")
            _CommunicationSession.Write("alarm -u")

            _CommunicationSession.Write("rcmd dsp ""fpga -a 0 -c 0 -t lte -w LTE_PN18_SCALE_FACTOR_RA 0x16a0""")
            _CommunicationSession.Write("rcmd dsp ""fpga -a 1 -c 0 -t lte -w LTE_PN18_SCALE_FACTOR_RA 0x16a0""")
            _CommunicationSession.Write("rcmd dsp ""fpga -a 0 -c 1 -t lte -w LTE_PN18_SCALE_FACTOR_RA 0x16a0""")
            _CommunicationSession.Write("rcmd dsp ""fpga -a 1 -c 1 -t lte -w LTE_PN18_SCALE_FACTOR_RA 0x16a0""")
            _CommunicationSession.Write("rcmd dsp ""fpga -a 0 -w LTE_BB_INPUT_SELECT_RA 2""")
            _CommunicationSession.Write("rcmd dsp ""fpga -a 1 -w LTE_BB_INPUT_SELECT_RA 2""")
            _CommunicationSession.Write("rcmd dsp ""fpga -a 0 -w IF_GAIN_RA 0x5147""")
            _CommunicationSession.Write("rcmd dsp ""fpga -a 1 -w IF_GAIN_RA 0x5147""")

            _TelnetCustomer.SendCommand("SET", 196, "HEARTBEAT:STATE=DISABLE")
            _TelnetCustomer.SendCommand("SET", 196, "CARRIERCFG:INDEX=1 STATE=DISABLE")
            _TelnetCustomer.SendCommand("SET", 196, "CARRIERCFG:INDEX=2 STATE=DISABLE")
            '_TelnetCustomer.SendCommand("SET", 196, "CARRIERCFG:INDEX=1 TXFREQ=1960000 RXFREQ=1880000 POWER=435 CARRTYPE=LTE_5 GRPSIZEDOWN=2 GRPSIZEUP=2 TXCONTAINER=1 TX2CONTAINER=3 RX1CONTAINER=1 RX2CONTAINER=3 RX3CONTAINER=5 RX4CONTAINER=7 STATE=ENABLE")
            '_TelnetCustomer.SendCommand("SET", 196, "CARRIERCFG:INDEX=2 TXFREQ=1990000 RXFREQ=1910000 POWER=435 CARRTYPE=LTE_5 GRPSIZEDOWN=2 GRPSIZEUP=2 TXCONTAINER=5 TX2CONTAINER=7 RX1CONTAINER=9 RX2CONTAINER=11 RX3CONTAINER=13 RX4CONTAINER=15 STATE=ENABLE")
            ''_TelnetCustomer.SendCommand("SET", 196, "CARRIERCFG:INDEX=1 TXFREQ=1990000 RXFREQ=1852500 POWER=465 CARRTYPE=LTE_5 GRPSIZEDOWN=2 GRPSIZEUP=2 TXCONTAINER=1 TX2CONTAINER=3 RX1CONTAINER=1 RX2CONTAINER=3 RX3CONTAINER=5 RX4CONTAINER=7 STATE=ENABLE")
            ''_TelnetCustomer.SendCommand("SET", 196, "CARRIERCFG:INDEX=2 TXFREQ=1992500 RXFREQ=1900000 POWER=0 CARRTYPE=LTE_5 GRPSIZEDOWN=2 GRPSIZEUP=2 TXCONTAINER=0 TX2CONTAINER=0 RX1CONTAINER=9 RX2CONTAINER=11 RX3CONTAINER=13 RX4CONTAINER=15 STATE=ENABLE")
            'Resume 2 carrier for slew-rate alarm issue after DC cycle.  V2.0.4
            _TelnetCustomer.SendCommand("SET", 196, "CARRIERCFG:INDEX=1 TXFREQ=1987500 RXFREQ=1852500 POWER=435 CARRTYPE=LTE_5 GRPSIZEDOWN=2 GRPSIZEUP=2 TXCONTAINER=1 TX2CONTAINER=3 RX1CONTAINER=1 RX2CONTAINER=3 RX3CONTAINER=5 RX4CONTAINER=7 STATE=ENABLE")
            _TelnetCustomer.SendCommand("SET", 196, "CARRIERCFG:INDEX=2 TXFREQ=1992500 RXFREQ=1900000 POWER=435 CARRTYPE=LTE_5 GRPSIZEDOWN=2 GRPSIZEUP=2 TXCONTAINER=5 TX2CONTAINER=7 RX1CONTAINER=9 RX2CONTAINER=11 RX3CONTAINER=13 RX4CONTAINER=15 STATE=ENABLE")

            'Add below DSP commands then read TX/PA per requirement from Qiaozhi.     Light, 2014.10.28     
            Sleep(6000)

            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_LSW_RA 0x0000""")
            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_MSW_RA 0x1800""")
            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_MSB_RA 0x0001""")

            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_BEFORE_CFR_LSW_RA 0x0000""")
            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_BEFORE_CFR_MSW_RA 0x3600""")
            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_BEFORE_CFR_MSB_RA 0x0001""")

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Add for TX1 DPD failure, Light, 2014.10.30
    Public Sub WriteSlewRate()
        Try
            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_LSW_RA 0x0000""")
            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_MSW_RA 0x1800""")
            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_MSB_RA 0x0001""")

            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_BEFORE_CFR_LSW_RA 0x0000""")
            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_BEFORE_CFR_MSW_RA 0x3600""")
            _CommunicationSession.Write("rcmd dsp ""fpga -f -w SLEW_RATE_THRESHOLD_BEFORE_CFR_MSB_RA 0x0001""")
        Catch ex As Exception
            'Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Function CheckSlewRate() As String
        Try
            Dim answer As String = ""
            Dim StartPos As Integer, StopPos As Integer
            Dim token As String, t1 As String, t2 As String, t3 As String

            answer = _CommunicationSession.Write("rcmd dsp ""fpga -f -r SLEW_RATE_THRESHOLD_LSW_RA""")
            token = "fpga read reg 0x10034 data = "
            StartPos = answer.IndexOf(token, StopPos) + token.Length
            StopPos = answer.IndexOf(vbCrLf, StartPos)
            t1 = answer.Substring(StartPos, StopPos - StartPos)
            'If t <> "0x0000" Then Return False

            answer = _CommunicationSession.Write("rcmd dsp ""fpga -f -r SLEW_RATE_THRESHOLD_MSW_RA""")
            token = "fpga read reg 0x10035 data = "
            StartPos = 0 : StopPos = 0
            StartPos = answer.IndexOf(token, StopPos) + token.Length
            StopPos = answer.IndexOf(vbCrLf, StartPos)
            t2 = answer.Substring(StartPos, StopPos - StartPos)
            'If t <> "0x1800" Then Return False

            answer = _CommunicationSession.Write("rcmd dsp ""fpga -f -r SLEW_RATE_THRESHOLD_MSB_RA""")
            token = "fpga read reg 0x10036 data = "
            StartPos = 0 : StopPos = 0
            StartPos = answer.IndexOf(token, StopPos) + token.Length
            StopPos = answer.IndexOf(vbCrLf, StartPos)
            t3 = answer.Substring(StartPos, StopPos - StartPos)
            'If t <> "0x0001" Then Return False
            If t1 = "0x0000" And t2 = "0x1800" And t3 = "0x0001" Then Return True Else Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub Close_Timer()
        Try
            _CommunicationSession.Write("cm_timer disable")
        Catch ex As Exception
            Me.Close()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    ReadOnly Property pkgRev() As String
        Get
            Dim answer As String = ""
            Dim m As Match
            Try
                answer = _CommunicationSession.Write(String.Format("pkg -p"))
                m = Regex.Match(answer, "pkg_file *. *([\w\.]+) *. *\d+-")
                Return m.Groups(1).Value
            Catch ex As Exception
                Return "999"
            End Try
        End Get
    End Property
    ReadOnly Property dspRev() As String
        Get
            Dim answer As String = ""
            Dim m As Match
            Try
                answer = _CommunicationSession.Write(String.Format("pkg -p"))
                m = Regex.Match(answer, "dsp_appl *. *([\w\.]+) *. *\d+-")
                Return m.Groups(1).Value
            Catch ex As Exception
                Return "999"
            End Try
        End Get
    End Property
    ReadOnly Property fpgaRev() As String
        Get
            Dim answer As String = ""
            Dim m As Match
            Try
                answer = _CommunicationSession.Write(String.Format("pkg -p"))
                m = Regex.Match(answer, "fpga_zip *. *([\w\.]+) *. *\d+-")
                Return m.Groups(1).Value
            Catch ex As Exception
                Return "999"
            End Try
        End Get
    End Property

    Public ReadOnly Property Alarms() As String()
        Get
            Try
                'Dim answer As String = _CommunicationSession.Write(String.Format("alarm -p"))

                'Dim token As String = "Active Alarms:"
                'Dim StartPos As Integer = answer.IndexOf(token) + token.Length
                'Dim StopPos As Integer = answer.IndexOf("LED State:", StartPos)
                'Dim t As String = answer.Substring(StartPos, StopPos - StartPos)
                'Return t

                'Read <Acrive Alarms> and <Unit State> item in alarm -p.
                Dim i As Integer
                _CommunicationSession.Write(String.Format("alarm -u"))
                Dim answer As String = _CommunicationSession.Write(String.Format("alarm -p"))
                Dim token As String = "Active Alarms:"
                Dim StartPos As Integer = answer.IndexOf(token) + token.Length
                Dim StopPos As Integer = answer.IndexOf("LED State:", StartPos)
                Dim t As String = answer.Substring(StartPos, StopPos - StartPos)
                t = t.Trim()
                Dim AlarmList() As String = t.Split(vbCrLf)
                For i = 0 To AlarmList.Length - 1
                    AlarmList(i) = AlarmList(i).Trim()
                Next
                Return AlarmList
            Catch ex As Exception
                'Dim tmp As String = ""   'New String(0) {ex.Message}
                'Return tmp
                Dim AlarmList() As String = New String(0) {ex.Message}
                Return AlarmList
            End Try
        End Get
    End Property

    Public ReadOnly Property Inventory() As InvData
        'Serial: 7890123456789
        'FunctionCode: R2x50-80L1
        'KsNumber: KS24829L1
        'KsVersion: 1.1
        'ComCode: 849144415
        'CLEI: WDMAU00ARA
        'ECI: 462628
        'Misc: CT P0.0
        'GDF: AND001
        Get
            Try
                Dim answer As String = _CommunicationSession.Write("invdata")
                Dim inv As InvData
                inv.Serial = GetInventory(answer, "Serial:").Trim()
                inv.FunctionCode = GetInventory(answer, "FunctionCode:").Trim()
                inv.KsNumber = GetInventory(answer, "KsNumber:").Trim()
                inv.KsVersion = GetInventory(answer, "KsVersion:").Trim()
                inv.ComCode = GetInventory(answer, "ComCode:").Trim()
                inv.CLEI = GetInventory(answer, "CLEI:").Trim()
                inv.ECI = GetInventory(answer, "ECI:").Trim()
                inv.Misc = GetInventory(answer, "Misc:").Trim()
                inv.GDF = GetInventory(answer, "GDF:").Trim()
                Return inv
            Catch ex As Exception
                Throw New Exception(ex.Message)
                'Dim inv As InvData
                'inv.Serial = ""
                'inv.FunctionCode = ""
                'inv.KsNumber = ""
                'inv.KsVersion = ""
                'inv.ComCode = ""
                'inv.CLEI = ""
                'inv.ECI = ""
                'inv.Misc = ""
                'inv.GDF = ""
                'Return inv
            End Try
        End Get
    End Property
    Private Shared Function GetInventory(ByVal resp As String, ByVal FieldName As String) As String
        Dim StartPos As Integer, StopPos As Integer
        Dim token As String, t As String
        token = FieldName
        StartPos = resp.IndexOf(token, StopPos) + token.Length
        StopPos = resp.IndexOf(vbCrLf, StartPos)
        t = resp.Substring(StartPos, StopPos - StartPos)
        Return t
    End Function

    Public ReadOnly Property PowerOut(ByVal channel As Integer) As Double
        'rtose@voyager> rcmd dsp "tx -q"
        'Tx path 0 transmit power values:
        '   Carrier mode              = CDMA2k
        '   Number of active carriers = 5
        '   Tx enable                 = ON
        '   Tx output power (dBm)     = 47.0
        '   Tx output power (W)       = 49.7
        'Tx path 0 per carrier power values:
        '   Carrier  Type    CenterFreq(MHz)  inputPwr(dBFS) outputPwr(dBm) outputPwr(W)
        '      0    CDMA2K      862.900         -22.0            40.0            9.9
        '      1    CDMA2K      864.150         -22.0            40.0            9.9
        '      2    CDMA2K      865.400         -22.0            40.0            9.9
        '      3    CDMA2K      866.650         -22.0            40.0            9.9
        '      4    CDMA2K      867.900         -22.0            40.0            9.9

        'Tx path 1 transmit power values:
        '   Carrier mode              = CDMA2k
        '   Number of active carriers = 5
        '   Tx enable                 = ON
        '   Tx output power (dBm)     = 47.0
        '   Tx output power (W)       = 49.7
        'Tx path 1 per carrier power values:
        '   Carrier  Type    CenterFreq(MHz)  inputPwr(dBFS) outputPwr(dBm) outputPwr(W)
        '      0    CDMA2K      862.900         -22.0            40.0            9.9
        '      1    CDMA2K      864.150         -22.0            40.0            9.9
        '      2    CDMA2K      865.400         -22.0            40.0            9.9
        '      3    CDMA2K      866.650         -22.0            40.0            9.9
        '      4    CDMA2K      867.900         -22.0            40.0            9.9

        'dsp> tx -q
        'Tx path 0 transmit power values:
        '   Number of active CDMA carriers = 0
        '   Number of active LTE carriers  = 0
        '   Tx enable                      = OFF
        '   Tx output power (dBm)          =  0.0
        '   Tx output power (W)            =  0.0
        'Tx path 0 per carrier power values:
        '   Carrier  Type    CenterFreq(MHz)  inputPwr(dBFS) outputPwr(dBm) outputPwr(W)

        'Tx path 1 transmit power values:
        '   Number of active CDMA carriers = 0
        '   Number of active LTE carriers  = 0
        '   Tx enable                      = OFF
        '   Tx output power (dBm)          =  0.0
        '   Tx output power (W)            =  0.0
        'Tx path 1 per carrier power values:
        '   Carrier  Type    CenterFreq(MHz)  inputPwr(dBFS) outputPwr(dBm) outputPwr(W)
        Get
            Try
                Dim resp As String = _CommunicationSession.Write("rcmd dsp ""tx -q""")
                Dim m As Match
                'm = Regex.Match(resp, "Tx path 0 transmit power values:(.*)Tx path 0 per carrier power values:(.*)Tx path 1 transmit power values:(.*)Tx path 1 per carrier power values:(.*)")
                m = Regex.Match(resp, String.Format("Tx path {0} transmit power values:(.*)", channel), RegexOptions.Singleline)
                'Dim tPout As Pout
                If m.Success Then
                    'tPout.Pout_0 = GetPout(m.Groups(1).Value, "Tx output power (dBm)")
                    'tPout.Pout_1 = GetPout(m.Groups(2).Value, "Tx output power (dBm)")
                    Dim tt As String = m.Groups(1).Value
                    Return GetPout(m.Groups(1).Value, "Tx output power \(dBm\)")
                End If
                'Return tPout


                '    'rtose@voyager > output_power
                '    '    TX1()
                '    '-------------------------------------------------------------
                '    '| carrier | container | frequency   | cfg power | out power |
                '    '-------------------------------------------------------------
                '    '| 0       | 1         | 1932.50 MHz | 46.50 dBm | 46.44 dBm |
                '    '-------------------------------------------------------------
                '    '    TX2()
                '    '-------------------------------------------------------------
                '    '| carrier | container | frequency   | cfg power | out power |
                '    '-------------------------------------------------------------
                '    '| 0       | 3         | 1932.50 MHz | 46.50 dBm | 46.44 dBm |
                '    '-------------------------------------------------------------
                'Dim resp As String = SendCommand("output_power")
                'Dim m As Match
                'm = Regex.Match(resp, String.Format("TX{0}(.*)", channel + 1), RegexOptions.Singleline)
                'If m.Success Then
                '    'Return GetPout(m.Groups(1).Value, "Tx output power \(dBm\)")
                '    Dim m1 As Match
                '    m1 = Regex.Match(m.Groups(1).Value, "\| *(\d+) *\| *(\d+) *\| *(\S+) *\S+ *\| *(\S+) *\S+ *\| *(\S+) *\S+ *\|")
                '    Return m1.Groups(1).Value
                'End If
            Catch ex As Exception
                'Dim tPout As Pout
                'tPout.Pout_0 = -999.0
                'tPout.Pout_1 = -999.0
                'Return tPout
                Return -999.0
            End Try
        End Get
    End Property
    Private Function GetPout(ByVal resp As String, ByVal Field As String) As Double
        Dim m As Match
        m = Regex.Match(resp, Field & " *= *(\S+)")
        If m.Success Then
            Return m.Groups(1).Value
        Else
            Throw New Exception("GetPout: Could not fine field '" & Field & "' in response '" & resp & "'")
        End If
    End Function

    Public Function ChangeIPAddress(ByVal TargetAdd As String) As Boolean
        'Try
        '    _CommunicationSession.Write("netcfg IP " & TargetAdd)
        '    _CommunicationSession.Write("netcfg COMMIT")
        '    Return True
        'Catch ex As Exception
        '    Return False
        'End Try
    End Function

    Public Function ChangeGW(ByVal TargetAdd As String) As Boolean
        'Try
        '    _CommunicationSession.Write("netcfg GW " & TargetAdd)
        '    _CommunicationSession.Write("netcfg COMMIT")
        '    Return True
        'Catch ex As Exception
        '    Return False
        'End Try
    End Function

    Public ReadOnly Property CatSN() As String
        'rtose@voyager> cat /flash/conf/inventory.xml
        'n++<?xml version="1.0" encoding="UTF-8"?>
        '<!-- Inventory Stored on TRX board in Filesystem -->
        '<inv fmtver="1">
        '  <rf100>RF101018-1</rf100>
        '  <rf150>RF151032-2C</rf150>
        '  <serial>SZP2013460003</serial>
        '  <trx-mod>
        '    <!-- TRX board RF part number and serianl number -->
        '    <rf200>RF202197-1B</rf200>
        '    <serial>RV00113440001</serial>
        '  </trx-mod>
        '  <psb-mod>
        '    <!-- PSB board RF part number and serianl number -->
        '    <rf200>RF202196-1A</rf200>
        '    <serial>CSUIAQM0111410005</serial>
        '  </psb-mod>
        '  <!-- A space followed by linefeed "\n" is required at the end of the "what" st
        'ring -->
        '  <what> @(#)~[T] conf_xml [V] 01.00 [P] voyager [D] 2011-01-21 12:00:23 [S] 000
        '0000752 [M] D2DBA5473A45A8F39C4274CBCADE8BAF
        '  </what>
        '</inv>rtose@voyager>
        Get
            Try
                Dim answer As String = _CommunicationSession.Write("cat /flash/conf/inventory.xml")
                Dim StartPos As Integer, StopPos As Integer
                Dim token As String, t As String
                token = "<serial>"
                StartPos = answer.IndexOf(token, StopPos) + token.Length
                StopPos = answer.IndexOf("</serial>", StartPos)
                t = answer.Substring(StartPos, StopPos - StartPos)
                Return t
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
    End Property

    Public ReadOnly Property GetRamlog() As String
        'rtose@voyager> rld
        '...............................
        'rtose@voyager>
        Get
            Try
                Dim answer As String = _CommunicationSession.Write("rld")
                'Dim StartPos As Integer, StopPos As Integer
                'Dim token As String, t As String
                'token = "rld"
                'StartPos = answer.IndexOf(token, StopPos) + token.Length
                'StopPos = answer.IndexOf("rtose@voyager", StartPos)
                't = answer.Substring(StartPos, StopPos - StartPos)
                Return answer
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
    End Property

    Public ReadOnly Property GetDpdDelayData(ByVal TxChan As Integer) As dpdDelayData
        'rtose@voyager> rcmd dsp "dpd tx1 status delay"

        'dsp> dpd status delay

        'Delay Object Parameters
        '  Data Collection
        '    Channel                       : 0
        '    trigger Type                  : 0
        '    Number of captures            : 1
        '    Number of transfers / capture : 16
        '    Number of samples / transfer  : 512

        '  Autocorrelation lag support
        '    Search Start                  : 167
        '    Search Length                 : 50

        '  Estimated Parameters
        '    Amplifier Delay               : 190 4/16
        '    Maximum cross-correlation     : 0.9984
        Get
            Try
                If TxChan < 0 Or TxChan > 1 Then Throw New ArgumentException("InvalidParameter:Channel", "channel:" & TxChan)
                Dim resp As String = SendCommand(String.Format("rcmd dsp ""dpd tx{0} status delay""", TxChan))
                Dim dd As dpdDelayData
                Dim StartPos As Integer, StopPos As Integer
                Dim token As String, t As String
                token = "Amplifier Delay               : "
                StartPos = resp.IndexOf(token, StopPos) + token.Length
                StopPos = resp.IndexOf("Maximum cross-correlation     : ", StartPos)
                t = resp.Substring(StartPos, StopPos - StartPos)
                dd.dpd_ampDelayInt = t.Split(" ")(0)
                t = t.Split(" ")(1)
                dd.dpd_ampDelayFrac = t.Split("/")(0)
                token = "Maximum cross-correlation     : "
                StartPos = resp.IndexOf(token, StopPos) + token.Length
                StopPos = resp.Length
                t = resp.Substring(StartPos, StopPos - StartPos)
                dd.dpd_Maximum_cross_correlation = t
                Return dd
            Catch ex As Exception
                'Throw New Exception(ex.Message)
                Dim dd As dpdDelayData
                dd.dpd_ampDelayInt = -99999
                dd.dpd_ampDelayFrac = -99999
                dd.dpd_Maximum_cross_correlation = -99999
                Return dd
            End Try
        End Get
    End Property

    Public ReadOnly Property GetDpdL2Data(ByVal TxChan As Integer) As dpdL2Data
        'dsp> dpd tx0 status l2

        'Layer 2 Object Parameters
        '  Data Collection

        ' Estimated least squares parameters
        '   Beta L2 3rd symmetric         : 0.1078+j0.0355 (0.1135<18.2365)
        '   Beta L3 3rd symmetric         : -0.0512-j0.0235 (0.0563<-155.3100)
        '   Beta L2 5th symmetric         : -0.2281+j0.1382 (0.2668<148.7853)
        '   Beta L3 5th symmetric         : -0.0564+j0.0503 (0.0756<138.2782)
        '   Beta L2 3rd asymmetric        : -0.1732+j0.1665 (0.2402<136.1246)
        '   Beta L3 3rd asymmetric        : 0.0000+j0.0000 (0.0000<0.0000)
        '   Beta L2 5th asymmetric        : -0.0917-j0.2572 (0.2730<-109.6235)
        '   Beta L3 5th asymmetric        : 0.0000+j0.0000 (0.0000<0.0000)
        Get
            Try
                If TxChan < 0 Or TxChan > 1 Then Throw New ArgumentException("InvalidParameter:Channel", "channel:" & TxChan)
                Dim resp As String = SendCommand(String.Format("rcmd dsp ""dpd tx{0} status l2""", TxChan))
                Dim dl2 As dpdL2Data
                dl2.dpd_L2_3rd_sym_am = CDbl(GetDpdL2DataField(resp, "Beta L2 3rd symmetric").Split("<")(0))
                dl2.dpd_L2_3rd_sym_ph = CDbl(GetDpdL2DataField(resp, "Beta L2 3rd symmetric").Split("<")(1))
                dl2.dpd_L3_3rd_sym_am = CDbl(GetDpdL2DataField(resp, "Beta L3 3rd symmetric").Split("<")(0))
                dl2.dpd_L3_3rd_sym_ph = CDbl(GetDpdL2DataField(resp, "Beta L3 3rd symmetric").Split("<")(1))
                dl2.dpd_L2_5th_sym_am = CDbl(GetDpdL2DataField(resp, "Beta L2 5th symmetric").Split("<")(0))
                dl2.dpd_L2_5th_sym_ph = CDbl(GetDpdL2DataField(resp, "Beta L2 5th symmetric").Split("<")(1))
                dl2.dpd_L3_5th_sym_am = CDbl(GetDpdL2DataField(resp, "Beta L3 5th symmetric").Split("<")(0))
                dl2.dpd_L3_5th_sym_ph = CDbl(GetDpdL2DataField(resp, "Beta L3 5th symmetric").Split("<")(1))
                dl2.dpd_L2_3rd_asym_am = CDbl(GetDpdL2DataField(resp, "Beta L2 3rd asymmetric").Split("<")(0))
                dl2.dpd_L2_3rd_asym_ph = CDbl(GetDpdL2DataField(resp, "Beta L2 3rd asymmetric").Split("<")(1))
                dl2.dpd_L2_5th_asym_am = CDbl(GetDpdL2DataField(resp, "Beta L2 5th asymmetric").Split("<")(0))
                dl2.dpd_L2_5th_asym_ph = CDbl(GetDpdL2DataField(resp, "Beta L2 5th asymmetric").Split("<")(1))
                Return dl2
            Catch ex As Exception
                'Throw New Exception(ex.Message)
                Dim dl2 As dpdL2Data
                dl2.dpd_L2_3rd_sym_am = -99999
                dl2.dpd_L2_3rd_sym_ph = -99999
                dl2.dpd_L3_3rd_sym_am = -99999
                dl2.dpd_L3_3rd_sym_ph = -99999
                dl2.dpd_L2_5th_sym_am = -99999
                dl2.dpd_L2_5th_sym_ph = -99999
                dl2.dpd_L3_5th_sym_am = -99999
                dl2.dpd_L3_5th_sym_ph = -99999
                dl2.dpd_L2_3rd_asym_am = -99999
                dl2.dpd_L2_3rd_asym_ph = -99999
                dl2.dpd_L2_5th_asym_am = -99999
                dl2.dpd_L2_5th_asym_ph = -99999
                Return dl2
            End Try
        End Get
    End Property
    Private Shared Function GetDpdL2DataField(ByVal resp As String, ByVal FieldName As String) As String
        Dim m As Match
        m = Regex.Match(resp, FieldName & " *: *(\S*) *\((\S*)\)")
        If m.Success = False Then
            Throw New Exception("Gain status field '" & FieldName & "' not found in resp: " & resp)
        End If
        Return m.Groups(2).Value
    End Function

    WriteOnly Property TxOnOff(ByVal channel As Integer) As Boolean
        'rtose@voyager> tx off 1
        'Transmitter 1 turned off
        'rtose@voyager> tx off 2
        'Transmitter 2 turned off
        'rtose@voyager> tx on 1
        'Transmitter 1 turned on.
        'rtose@voyager> tx on 2
        'Transmitter 2 turned on.
        Set(ByVal value As Boolean)
            Try
                If value Then
                    _CommunicationSession.Write(String.Format("tx on {0}", channel))
                Else
                    _CommunicationSession.Write(String.Format("tx off {0}", channel))
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Set
    End Property
End Class
