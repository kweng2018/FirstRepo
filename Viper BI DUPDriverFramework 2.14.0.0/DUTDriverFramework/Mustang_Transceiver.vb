Imports System.Text.RegularExpressions

Public Class MUSTANG_Transceiver
  Implements IDeviceUnderTest

  Private _SerialNumber As String
  Private _DeviceAddress As String
  Private _HWRev As String
  Private _Port As Integer = 23
  Private _Model As String = "OTRX_MUSTANG"
  Private _FWRevision As String
  Private _DSPRevision As String
  Private _FPGARevision As String
  Private _CpriFPGARevision As String
  Private _BootloaderRevision As String
  'Private _RxCompTempEnabled As Boolean = True
  'Private _TxCompTempEnabled As Boolean = True
  Private PASSWORD As String = "engin33r"
  Private _SendEvent As Boolean = True
  Private _VSWR_Alarm_Enabled As Boolean = True
  Const NumRx As Integer = 2

  Private _Inventory(5) As String
  Private _EnableDiagnostic As Boolean
  Private _InventoryField As String() = New String(5) {"HWREF", "RUNAME", "PECCODE", "SN", "MANUFDATE", "HWRELEASE"}
  Private _CustomerCarrierConfigured As Boolean

  Private _BoardDataCmdIsNew As Boolean
  Private _BoardDataCmdIsKnown As Boolean = False

  Public Structure BoardData
    Dim BandMHz As Integer
    Dim PowerWatts As Integer
    Dim OTB_SN As String
    Dim OTB_SAP As String
    Dim TRDU_SN As String
    Dim TRDU_SAP As String
    Dim TRDU_SAP100 As String
  End Structure
  Public Structure TempSensorData
    'TRDU700P1.3#RDK>tempsensors
    '  RX     = 23 C
    '  FB     = 24 C
    '  PA     = 22 C
    '  LNA    = 25 C
    '  PA2    = 23 C
    '  FPGA   = 30 C
    '  PS     = 25 C
    '  PACAM  = 26 C
    '  PA2CAM = 26 C
    Dim RX As Integer
    Dim FB As Integer
    Dim PA0 As Integer
    Dim LNA As Integer
    Dim PA1 As Integer
    Dim FPGA As Integer
    Dim PS As Integer
    Dim PA0CAM As Integer
    Dim PA1CAM As Integer
  End Structure

  Public Structure InventoryData
    'inventory unit set SerialNumber LBALLU-FV082512345    // sets the SerialNumber [18 chars]
    'inventory unit set FunctionCode FUNCTIONCO            // sets the FunctionCode [10 chars]
    'inventory unit set KsNumber KS24817L1                 // sets the KsNumber [11 chars]
    'inventory unit set KsVersion 1.1                      // sets the KsVersion [4 chars]
    'inventory unit set ComCode 409102316                  // sets the ComCode [9 chars]
    'inventory unit set CLEI VER_FV_P1.0                   // sets the CLEI [10 chars]
    'inventory unit set ECI ABCDEF                         // sets the ECI [6 chars]
    'inventory unit set Misc unused                        // sets the Misc [13 chars]
    Dim SerialNumber As String
    Dim FunctionCode As String
    Dim KsNumber As String
    Dim KsVersion As String
    Dim ComCode As String
    Dim CLEI As String
    Dim ECI As String
    Dim Misc As String
  End Structure

  Public Structure Invent3GPPData
    Dim UnitFamily As String
    Dim UnitName As String
    Dim UnitNumber As String
    Dim Version As String
    Dim SerialNumber As String
    Dim Firmware As String
    Dim Manufacturer As String
    Dim ManufactureDate As String
    Dim ServDate As String
    Dim CLEI As String
    Dim ECI As String
    Dim ManufactureData As String
  End Structure

  Public Structure InventoryDataPa
    'paeeprom inventory  PA Apparauscode = HV7_CAMS_40_WattsPA1
    'paeeprom inventory  PA SN = 234567
    'paeeprom inventory  Manufacturedate = 11/30/2007
    'paeeprom inventory  Manufacturedcode = 911
    'paeeprom inventory  Frequency = 2140 MHZ
    '5s inventory  Wattage = 0.00
    'paeeprom inventory  Power Supply Part No. = Not Programmed
    'paeeprom inventory  Power Supply Part SN. = Not Programmed
    'paeeprom inventory  CAM DRIVER Part No. = Not Programmed
    'paeeprom inventory  CAM DRIVER Part SN. = Not Programmed
    'paeeprom inventory  CAM FINAL Part No. = Not Programmed
    'paeeprom inventory  CAM FINAL Part SN. = Not Programmed
    Dim ApparatusCode As String
    Dim SerialNumber As String
    Dim ManufactureDate As String
    Dim ManufactureCode As String
    Dim Frequency As String
    Dim CamDriverPartNumber As String
    Dim CamDriverSerialNumber As String
    Dim CamFinalPartNumber As String
    Dim CamFinalSerialNumber As String
  End Structure

  Public Structure InventoryDataLna
    'TRDU700P1.3#RDK>lnaee read inventory
    'Inventory (valid):
    '  serial number     <serialNo TBD....>
    '  apparatus code    <apparatus TBD...>
    '  manufactured date <010109..>
    '  manufacturer name <Andrew..........>
    Dim SerialNumber As String
    Dim ApparatusCode As String
    Dim ManufactureDate As String
    Dim ManufacturerName As String
    Dim LnaSerialNumber As String
    Dim LnaApparatusCode As String
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

  'TRDU700P2.0#309>lnaee read txcal
  'TX path calibration data (valid):
  '  calibration temperature     <280>
  '  LNA_TX0 maxLossDBx100       <68>
  '  LNA_TX0 avgLossDBx100       <68>
  '  LNA_TX0 maxDelayPS          <89238>
  '  LNA_TX0 delayFlatnessPS     <20876>
  '  LNA_TX0 couplerLossDBx100   <4080>
  '  LNA_TX1 maxLossDBx100       <66>
  '  LNA_TX1 avgLossDBx100       <66>
  '  LNA_TX1 maxDelayPS          <86872>
  '  LNA_TX1 delayFlatnessPS     <12269>
  '  LNA_TX1 couplerLossDBx100   <4019>
  Public Structure LnaTxCalPathRecord
    Dim MaxLossDb As Double
    Dim AvgLossDb As Double
    Dim MaxDelayPs As Double
    Dim DelayFlatnessPs As Double
    Dim CouplerLossDb As Double
  End Structure

  Public Structure LnaTxCalRecord
    Dim Temperature As Double
    Dim PathValues() As LnaTxCalPathRecord
  End Structure

  Public Structure PwrDetPair
    Dim count As Integer
    Dim ValueDBm As Double
  End Structure

  Public Structure VswrFreqRecord
    Dim FreqHz As Long
    Dim PwrDet() As PwrDetPair
  End Structure

  Public Structure VswrTxRecord
    Dim TempDegC As Integer
    Dim FreqInfo() As VswrFreqRecord
  End Structure

  Public Structure LnaDetRecord
    Dim Adc As Integer
    Dim PowerDBm As Double
  End Structure

  Public Structure NetCfgData
    'Mac Address: 00:0D:EE:05:80:33 (Read Only)
    '    IP Address : 192.168.99.50 
    '    Gateway    : 192.168.99.1 
    '    Net Mask   : 255.255.255.0 
    '    Hostname   : TRDU700P1.0#yyz 
    '    Logger Address : 255.255.255.255 
    '    FTP Server Address : 255.255.255.255 
    'Cpri Interface:
    'Mac Address CHA: 00:0D:EE:05:80:B3 (Read Only)
    'Mac Address CHB: FF:FF:FF:FF:FF:FF (Read Only)

    Dim MacAddress As String
    Dim IPAddress As String
    Dim Gateway As String
    Dim NetMask As String
    Dim Hostname As String
    Dim LoggerAddress As String
    Dim FtpServerAddress As String
    Dim CpriMacAddressA As String
    'Dim CpriMacAddressB As String
  End Structure

  Public Structure GainStatusData
    'TRDU700P1.3#yyz>gain tx0 v
    '
    '        enabled             =   off,  update count           =     0
    '        attenSetting        =     0,  ocpmGain               =     0
    '        txStepAtten         =   7.0,  fbStepAtten            =  11.0
    '        nomGain             =  0.63,  fbTxQuotient           =   674
    '        nomGainChangeRxStep =  1.00,  nomGainChangeTemp      =  0.94
    '        nomGainChangeOcpm   =  1.00,  nomGainChangeOverdrive =  1.00
    '        gainError           =  1.00,  variableGain           =  0.20
    '        vcaGain             =  1.00,  offsetGain             =  1.00
    '        vcaSetting          =  3000
    '
    'Total Output Power  =   0.0 dBm,
    'C0 Output Power     =   0.0 dBm,  C0 Input Power         =    0.0 dBFS
    'C1 Output Power     =   0.0 dBm,  C1 Input Power         =    0.0 dBFS
    Dim TxStep As Double
    Dim FbStep As Double
    Dim FbTxQuo As Double
    Dim Vca As Integer
    Dim GainError As Double
    Dim Enabled As Boolean
  End Structure

  Public Structure PowerSupplyData
    'Chan  ------Desc------  Counts  EngValue  Units
    '   0  Csense Buck 30V     739       1.50  A
    '   1  Csense I2C  24V       2       0.00  A
    '   2  Temperature PSB    1336       31.6  Deg C
    '   3  Vsense      30V    4079      30.01  V
    '   4  Csense AISG 24V      18       0.02  A
    '   5  Csense AISG 12V       9       0.01  A
    '   6  Vsense     3.3V    4095       3.33  V
    '   7  Vsense       5V    4095       5.00  V
    '   8  Vsense     6.5V    4095       6.52  V
    '   9  Vsense I2C  24V       0       0.00  V
    '  10  Vsense AISG 12V    3437      11.96  V
    '  11  Vsense AISG 24V    4038      23.81  V
    Dim I2C_24V_Curr As Double
    Dim I2C_24V_Volt As Double
    Dim AISG_12V_Curr As Double
    Dim AISG_12V_Volt As Double
    Dim AISG_24V_Curr As Double
    Dim AISG_24V_Volt As Double

    Dim V3p3_Volt As Double
    Dim V5p0_Volt As Double
    Dim V6p5_Volt As Double
    Dim V30p0_Volt As Double
    Dim V30p0_Buck_Curr As Double
  End Structure

  Public Structure RssiData
    'TRDU700P1.3#002>rssi
    'RX0:
    '  C0 input power = -118.3 dBm,    C0 digital power =  -81.2 dBFS
    '  C1 input power =   -Inf dBm,    C1 digital power =   -Inf dBFS
    'RX1:
    '  C0 input power = -119.3 dBm,    C0 digital power =  -82.2 dBFS
    '  C1 input power =   -Inf dBm,    C1 digital power =   -Inf dBFS
    Structure RxData
      Structure CarrData
        Dim InputPower As Double
        Dim DigitalPower As Double
      End Structure
      Dim Carr() As CarrData
    End Structure
    Dim Rx() As RxData
  End Structure

  Public Structure PaStatusData
    'HELLCAT_hc>pa2bias v
    'PA2 Power Supply (28 Volt) = 27.3 Volts
    'PA2 Temperature            = 31 C
    'PA2 CAM Temperature        = -49 C
    'VCA                        = 2400
    'OC set point               = 2500
    'Vtrim                      = 2680
    'BIAS State                 = Normal
    'PA2 FINAL1    cam current = 0.000 amps -- Nom Bias current = 0.000 amps(without RF)
    'PA2 FINAL2    cam current = 0.890 amps -- Nom Bias current = 0.898 amps(without RF)
    'PA2 DRIVER2   cam current = 0.197 amps -- Nom Bias current = 0.200 amps(without RF)
    'PA2 DRIVER1   cam current = 0.058 amps -- Nom Bias current = 0.060 amps(without RF)

    'PA2 FINAL1    cam dac setting =   328 adc =     0 at biasing =     0 (No-RF)
    'PA2 FINAL2    cam dac setting =   796 adc =   437 at biasing =   441 (No-RF)
    'PA2 DRIVER2   cam dac setting =  1448 adc =   725 at biasing =   737 (No-RF)
    'PA2 DRIVER1   cam dac setting =  1471 adc =   215 at biasing =   220 (No-RF)

    Dim PsVolt As Double
    Dim Temperature As Double
    Dim TemperatureCAM As Double
    Dim CurrentFinal1 As Double
    Dim CurrentFinal2 As Double
    Dim CurrentDriver1 As Double
    Dim CurrentDriver2 As Double
    Dim DacFinal1 As Integer
    Dim DacFinal2 As Integer
    Dim DacDriver1 As Integer
    Dim DacDriver2 As Integer
  End Structure

  Public Structure SystemStateData
    'HELLCAT_0000>sys state
    'Unit State: In-Service
    'LED State: Standby
    'Tx1 State: Disabled
    'Tx2 State: Disabled
    'Rx1 State: Pass
    'Rx2 State: Pass
    Dim UnitState As String
    Dim LedState As String
    Dim Tx1State As String
    Dim Tx2State As String
    Dim Rx1State As String
    Dim Rx2State As String
  End Structure

  Public Enum LedStateEnum
    OFF
    GREEN
    FLASHGREEN
    RED
    FLASHRED
    YELLOW
    FLASHYELLOW
  End Enum

  Public Structure BootmodeData
    'TRDU700P1.3#196>bootmode v
    'Bootmode Status:
    '  ImageA name:  TestLoadA
    '  ImageA dsp :  26          valid    ->  LTE700_Mustang.0.3.0.15.1.bin
    '  ImageA fpga:  27          valid    ->  mustang_sp_fpga_v3.20.bin
    '  ImageA cpri:  28          valid    ->  tru_lte_cpri_fpga_v1.01_e.bin
    '  ImageB name:  TestLoadB
    '  ImageB dsp :  26          valid    ->  LTE700_Mustang.0.3.0.15.1.bin
    '  ImageB fpga:  27          valid    ->  mustang_sp_fpga_v3.20.bin
    '  ImageB cpri:  28          valid    ->  tru_lte_cpri_fpga_v1.01_e.bin
    '  boot Select:  A
    '  active image: A

    'Latest images:
    '  Latest dsp :  26          LTE700_Mustang.0.3.0.15.1.bin
    '  Latest fpga:  27          mustang_sp_fpga_v3.20.bin
    '  Latest cpri:  28          tru_lte_cpri_fpga_v1.01_e.bin
    Structure ImageData
      Dim Name As String
      Dim Dsp As String
      Dim DspNum As Integer
      Dim Fpga As String
      Dim FpgaNum As Integer
      Dim Cpri As String
      Dim CpriNum As Integer
    End Structure
    Dim Image() As ImageData
    Dim ActiveImage As Integer
    Dim BootImage As Integer
  End Structure

  Public Structure TRDUFile
    Dim globalSeqNum As Integer
    Dim fileType As Integer
    Dim fileLength As Integer
    Dim fileDate As String
    Dim fileName As String
  End Structure
  Private _TRDUFileSystem As New List(Of TRDUFile)

  Public Enum BootImage
    A = 0
    B = 1
  End Enum

  Private WithEvents _TelnetSession As ClientTelnetInterface = Nothing
  Private WithEvents _TelnetCustomer As CustomerTelnetInterface = Nothing
  Public Event DeviceMessageReceived(ByVal Message As String) Implements IDeviceUnderTest.DeviceMessageReceived

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
      _TRDUFileSystem.Clear()
      If _TelnetSession IsNot Nothing Then
        _TelnetSession.Close()
        _TelnetSession = Nothing
      End If
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
    Try
      If _TelnetCustomer IsNot Nothing Then
        _TelnetCustomer.Close()
        _TelnetCustomer = Nothing
      End If
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public ReadOnly Property FWRev() As String Implements IDeviceUnderTest.FWRev
    Get
      Try
        Return _FWRevision
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
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

  Property Net_MAC() As String Implements IDeviceUnderTest.MAC
    Get
      Dim nc As NetCfgData = NetCfg
      Return nc.MacAddress
    End Get
    Set(ByVal value As String)
      Try
        Dim Answer As String = _TelnetSession.SendCommandAndReceive(String.Format("netcfg EA {0}", value))
        Answer = _TelnetSession.SendCommandAndReceive("netcfg COMMIT")
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
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
      _TelnetSession = New ClientTelnetInterface
      _TelnetSession.IPAddress = _DeviceAddress
      _TelnetSession.Port = _Port
      _TelnetSession.Open()

      ParseBanner(_TelnetSession.GetPrompt())

      'ENTERING IN ANDREW MODE
      _TelnetSession.SendCommandNoWait("access andrew")
      _TelnetSession.SendCommandAndReceive(PASSWORD)
      '_TelnetSession.SendCommandAndReceive("access andrew " & PASSWORD)
      EnableDiagnostic = False
      _BoardDataCmdIsKnown = False
      '_TelnetSession.SendBinary(New Byte() {7, 7})  'send ctrl-g twice to suppress async alarms
      'If _DSPRevision.Contains("1.0.111.0") And _FPGARevision.Contains("3.32") Then
      '  _TelnetSession.SendCommandAndReceive("dac5688 tx0 write 0x1 0x19")
      '  _TelnetSession.SendCommandAndReceive("dac5688 tx1 write 0x1 0x19")
      'End If

    Catch ex As Exception
      Me.Close()
      Throw New Exception(ex.Message)
    End Try

    Try
      _TelnetCustomer = New CustomerTelnetInterface
      _TelnetCustomer.IPAddress = _DeviceAddress
      _TelnetCustomer.Port = 8081
      _TelnetCustomer.Open()
      _CustomerCarrierConfigured = False
    Catch ex As Exception
      Me.Close()
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Property EnableDiagnostic() As Boolean
    Get
      Return _EnableDiagnostic
    End Get
    Set(ByVal value As Boolean)
      Try
        _EnableDiagnostic = value
        If value Then
          'enable
          _TelnetSession.SendBinary(New Byte() {&HB, &HB})
        Else
          'disable
          _TelnetSession.SendBinary(New Byte() {&H7, &H7})
        End If
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Set
  End Property


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

  Public ReadOnly Property DSPRev() As String
    Get
      Try
        Return _DSPRevision
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Get
  End Property
  Public ReadOnly Property FPGARev() As String
    Get
      Try
        Return _FPGARevision
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Get
  End Property
  Public ReadOnly Property CPRIFPGARev() As String
    Get
      Return _CpriFPGARevision
    End Get
  End Property
  Public ReadOnly Property BootloaderRev() As String
    Get
      Return _BootloaderRevision
    End Get
  End Property

  Private Sub ParseBanner(ByVal answ As String)
    Try
      '=========================================================
      '=========================================================
      '=====                  TRD2X40-07                    ====
      '=========================================================
      '=========================================================
      '          Copyright 2009, Andrew Corporation
      '=========================================================
      ' DISPLAY:        TELNET1
      ' BOARD INFO:
      ' - MAC ADDRESS: 00:0D:EE:05:80:33
      ' - IP ADDRESS:  192.168.255.33
      ' S/W INFO:
      ' - VERSION:     0.2.8.0
      ' - BUILD:       103013091
      ' FPGA INFO:
      ' - VERSION:     2.2
      ' CPRI FPGA INFO:
      ' - VERSION:     0.5
      '=========================================================

      Dim VerDsp, VerFpga, VerCpriFpga As String
      Dim mymatch As Match
      mymatch = Regex.Match(answ, "S/W INFO:\s*- VERSION:\s*(\S*)\s*")
      VerDsp = mymatch.Groups(1).ToString
      mymatch = Regex.Match(answ, "FPGA INFO:\s*- VERSION:\s*(\S*)\s*")
      VerFpga = mymatch.Groups(1).ToString
      mymatch = Regex.Match(answ, "CPRI FPGA INFO:\s*- VERSION:\s*(\S*)\s*")
      VerCpriFpga = mymatch.Groups(1).ToString

      'Bootloader info availabe after FW rev 0.3.0.4
      mymatch = Regex.Match(answ, "BOOTLOADER INFO:\s*- VERSION:\s*(\S*)\s*")
      If mymatch.Success Then
        _BootloaderRevision = mymatch.Groups(1).Value
      Else
        _BootloaderRevision = ""
      End If

      'Update FWVersion
      _FWRevision = String.Format("DSP_{0}_FPGA_{1}", VerDsp, VerFpga)
      _DSPRevision = VerDsp
      _FPGARevision = VerFpga
      _CpriFPGARevision = VerCpriFpga

    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Sub FWDownload(ByVal FTPAddress As String, ByVal FWFileName As String) ' As Boolean 'Implements ITransceiver.FWDownload
    Dim oldTimeout As Integer = 0
    Try
      oldTimeout = _TelnetSession.TimeOutSec
      _TelnetSession.TimeOutSec = 60 * 5

      Dim Command As String = String.Format("download {0} {1} anonymous anonymous", FWFileName, FTPAddress)
      Dim Answer As String = _TelnetSession.SendCommandAndReceive(Command)
      If Not Answer Like "*Download Complete - FTP OK, Image(s) OK*" Then
        Throw New Exception("Error downloading file '" & FWFileName & "': " & Answer)
      End If
    Finally
      _TelnetSession.TimeOutSec = oldTimeout
    End Try
  End Sub

  '********************************************************************
  ' FILE SYSTEM FUNCTIONS
  '********************************************************************
  ''' <summary>
  ''' returns an array of two strings, 0 = FW File Name, 1 = FPGA File Name
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetCurrentFWAndFPGAFile() As String()
    Try
      Dim FWVersions As String() = New String(1) {"", ""}
      Dim maxFW As Integer = -1
      Dim maxFPGA As Integer = -1
      'look for the highest globalSeqNum for fileType 1 and 2
      For Each itemfile As TRDUFile In _TRDUFileSystem
        If itemfile.fileType = 1 Then
          If maxFW < itemfile.globalSeqNum Then
            maxFW = itemfile.globalSeqNum
            FWVersions(0) = itemfile.fileName
          End If
        ElseIf itemfile.fileType = 2 Then
          If maxFPGA < itemfile.globalSeqNum Then
            maxFPGA = itemfile.globalSeqNum
            FWVersions(1) = itemfile.fileName
          End If
        End If
      Next
      Return FWVersions
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function

  'Public Sub RefreshFileSystem()
  '    Try
  '        Dim Answer As String = _TelnetSession.SendCommandAndReceive("fs dir")
  '        Dim tmpFile As TRDUFile
  '        Dim token As String = "fileName"
  '        Dim StartPos As Integer = Answer.IndexOf(token) + token.Length
  '        Dim StopPos As Integer = Answer.IndexOf("Number of")
  '        Answer = Answer.Substring(StartPos, StopPos - StartPos).Trim(" ")
  '        StopPos = Answer.IndexOf(" ")
  '        _TRDUFileSystem.Clear()
  '        While StopPos < Answer.Length - 1
  '            tmpFile = New TRDUFile
  '            With tmpFile
  '                .globalSeqNum = Answer.Substring(0, StopPos).Trim(" ")
  '                Answer = Answer.Substring(StopPos).Trim(" ")
  '                StopPos = Answer.IndexOf(" ")
  '                .fileType = Answer.Substring(0, StopPos).Trim(" ")
  '                Answer = Answer.Substring(StopPos).Trim(" ")
  '                StopPos = Answer.IndexOf(" ")
  '                .fileLength = Answer.Substring(0, StopPos).Trim(" ")
  '                Answer = Answer.Substring(StopPos).Trim(" ")
  '                StopPos = Answer.IndexOf(" ")
  '                .fileDate = Answer.Substring(0, StopPos).Trim(" ")
  '                Answer = Answer.Substring(StopPos).Trim(" ")
  '                'Add space because trim remove the space at the end
  '                Answer = Answer & " "
  '                StopPos = Answer.IndexOf(" ")
  '                .fileName = Answer.Substring(0, StopPos).Trim(" ")
  '                Answer = Answer.Substring(StopPos).Trim(" ")
  '                StopPos = Answer.IndexOf(" ")
  '                _TRDUFileSystem.Add(tmpFile)
  '            End With
  '        End While
  '        ''AFTER REFRESH UPDATE FW  VERSION 
  '        '_FWRevision = Me.GetCurrentFWAndFPGAFile(0)
  '    Catch ex As Exception
  '        Throw New Exception(ex.Message)
  '    End Try
  'End Sub

  Public Sub RefreshFileSystem()
    Try
      Dim Answer As String = _TelnetSession.SendCommandAndReceive("fs dir")
      Dim tmpFile As TRDUFile

      Dim m As MatchCollection
      m = Regex.Matches(Answer, "\n\s*(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\S+)")
      For i As Integer = 0 To m.Count - 1
        tmpFile = New TRDUFile
        If m(i).Groups.Count = 6 Then
          With tmpFile
            .globalSeqNum = m(i).Groups(1).Value
            .fileType = m(i).Groups(2).Value
            .fileLength = m(i).Groups(3).Value
            .fileDate = m(i).Groups(4).Value
            .fileName = m(i).Groups(5).Value
          End With
          _TRDUFileSystem.Add(tmpFile)
        End If
      Next
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Sub RemoveFileFromFileSystem(ByVal FileName As String)
    Dim oldTimeout As Integer = _TelnetSession.TimeOutSec
    Try
      Dim Answer As String
      RefreshFileSystem()
      For Each itemfile As TRDUFile In _TRDUFileSystem
        If Not (itemfile.fileName <> FileName) Then
          _TelnetSession.TimeOutSec = 120
          Answer = _TelnetSession.SendCommandAndReceive(String.Format("fs del {0}", itemfile.globalSeqNum))
          _TelnetSession.SendCommandAndReceive("fs clean")
        End If
      Next
    Catch ex As Exception
      Throw New Exception(ex.Message)
    Finally
      _TelnetSession.TimeOutSec = oldTimeout
    End Try
  End Sub
  Public Sub RemoveOldFilesFromFileSystem()
    Dim oldTimeout As Integer = _TelnetSession.TimeOutSec
    Try
      _TelnetSession.TimeOutSec = 30.0#
      _TelnetSession.SendCommandAndReceive("fs del old")
    Catch ex As Exception
      Throw New Exception(ex.Message)
    Finally
      _TelnetSession.TimeOutSec = oldTimeout
    End Try
  End Sub
  Public Sub BootLoader_SetupImageToLastFWAndFPGA(ByVal image As BootImage)
    Try
      Dim maxFW As Integer = -1
      Dim maxFPGA As Integer = -1
      Dim im As String = ""
      Dim header As String = "bootmode set "
      RefreshFileSystem()
      'look for the highest globalSeqNum for fileType 1 and 2
      For Each itemfile As TRDUFile In _TRDUFileSystem
        If itemfile.fileType = 1 Then
          If maxFW < itemfile.globalSeqNum Then
            maxFW = itemfile.globalSeqNum
          End If
        ElseIf itemfile.fileType = 2 Then
          If maxFPGA < itemfile.globalSeqNum Then
            maxFPGA = itemfile.globalSeqNum
          End If
        End If
      Next
      If image = BootImage.A Then
        im = "A"
      ElseIf image = BootImage.B Then
        im = "B"
      End If
      'Setup Boot to the highest number for FPGA and FW file
      _TelnetSession.SendCommandAndReceive(String.Format("{0} dspImage{1} {2}", header, im, maxFW))
      _TelnetSession.SendCommandAndReceive(String.Format("{0} fpgaImage{1} {2}", header, im, maxFPGA))
      _TelnetSession.SendCommandAndReceive(String.Format("{0} bootSelect {1}", header, im))

    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Sub BootLoader_ActivateFile(ByVal image As MUSTANG_Transceiver.BootImage, ByVal file As TRDUFile)
    Try
      Dim im As String = ""
      Dim header As String = "bootmode set "
      If image = BootImage.A Then
        im = "A"
      ElseIf image = BootImage.B Then
        im = "B"
      End If
      'setup file in the image
      If file.fileType = 1 Then
        _TelnetSession.SendCommandAndReceive(String.Format("{0} dspImage{1} {2}", header, im, file.globalSeqNum))
      ElseIf file.fileType = 2 Then
        _TelnetSession.SendCommandAndReceive(String.Format("{0} fpgaImage{1} {2}", header, im, file.globalSeqNum))
      ElseIf file.fileType = 7 Then
        _TelnetSession.SendCommandAndReceive(String.Format("{0} cpriImage{1} {2}", header, im, file.globalSeqNum))
      End If
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Sub BootLoader_ActivateImage(ByVal image As MUSTANG_Transceiver.BootImage)
    Try
      Dim im As String = ""
      Dim header As String = "bootmode set "
      If image = BootImage.A Then
        im = "A"
      ElseIf image = BootImage.B Then
        im = "B"
      End If
      'Setup Boot to the highest number for FPGA and FW file
      _TelnetSession.SendCommandAndReceive(String.Format("{0} bootSelect {1}", header, im))
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub
  Public ReadOnly Property FileSystem() As List(Of TRDUFile)
    Get
      Return _TRDUFileSystem
    End Get
  End Property
  Public Function BootmodeStatus() As BootmodeData
    'TRDU700P1.3#196>bootmode v
    'Bootmode Status:
    '  ImageA name:  TestLoadA
    '  ImageA dsp :  26          valid    ->  LTE700_Mustang.0.3.0.15.1.bin
    '  ImageA fpga:  27          valid    ->  mustang_sp_fpga_v3.20.bin
    '  ImageA cpri:  28          valid    ->  tru_lte_cpri_fpga_v1.01_e.bin
    '  ImageB name:  TestLoadB
    '  ImageB dsp :  26          valid    ->  LTE700_Mustang.0.3.0.15.1.bin
    '  ImageB fpga:  27          valid    ->  mustang_sp_fpga_v3.20.bin
    '  ImageB cpri:  28          valid    ->  tru_lte_cpri_fpga_v1.01_e.bin
    '  boot Select:  A
    '  active image: A

    'Latest images:
    '  Latest dsp :  26          LTE700_Mustang.0.3.0.15.1.bin
    '  Latest fpga:  27          mustang_sp_fpga_v3.20.bin
    '  Latest cpri:  28          tru_lte_cpri_fpga_v1.01_e.bin
    Dim bd As BootmodeData, tmp() As String
    Dim resp As String = _TelnetSession.SendCommandAndReceive("bootmode v")

    Dim tmpstr As String
    tmpstr = GetRegexField(resp, "boot Select:\s*(\S+)")
    If tmpstr = "A" Then bd.BootImage = 0 Else bd.BootImage = 1
    tmpstr = GetRegexField(resp, "active image:\s*(\S+)")
    If tmpstr = "A" Then bd.ActiveImage = 0 Else bd.ActiveImage = 1

    ReDim bd.Image(1)
    Dim ImageStr() As String = {"ImageA", "ImageB"}
    For i As Integer = 0 To 1
      bd.Image(i).Name = GetRegexField(resp, ImageStr(i) & " name:\s*(\S+)")

      tmp = GetRegexFields(resp, ImageStr(i) & " dsp\s*:\s*(\d+)\s*valid\s*->\s*(\S+)", 2)
      bd.Image(i).Dsp = tmp(1)
      bd.Image(i).DspNum = CInt(tmp(0))

      tmp = GetRegexFields(resp, ImageStr(i) & " fpga\s*:\s*(\d+)\s*valid\s*->\s*(\S+)", 2)
      bd.Image(i).Fpga = tmp(1)
      bd.Image(i).FpgaNum = CInt(tmp(0))

      tmp = GetRegexFields(resp, ImageStr(i) & " cpri\s*:\s*(\d+)\s*valid\s*->\s*(\S+)", 2)
      bd.Image(i).Cpri = tmp(1)
      bd.Image(i).CpriNum = CInt(tmp(0))
    Next

    Return bd
  End Function

  Public Sub BootmodeToggle()
    _TelnetSession.SendCommandAndReceive("bootmode activate")
  End Sub

  Public WriteOnly Property BootmodeImageName(ByVal ImageNum As Integer) As String
    Set(ByVal value As String)
      Dim param As String
      If ImageNum = 0 Then param = "ImageAName" Else param = "ImageBName"
      _TelnetSession.SendCommandAndReceive("bootmode set " & param & " " & value)
    End Set
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

  Public Function GetFileInFileSystem(ByVal filename As String) As TRDUFile
    Try
      Dim myFile As TRDUFile = New TRDUFile
      myFile.globalSeqNum = 0
      'look for the highest globalSeqNum for fileType 1 and 2
      Me.RefreshFileSystem()
      For Each itemfile As TRDUFile In _TRDUFileSystem
        'If itemfile.fileName = filename Then
        If filename.StartsWith(itemfile.fileName) Then
          myFile = itemfile
        End If
      Next
      Return myFile
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function

  Public Function LnaReadAdc(ByVal detSW As LnaDetSwitchEnum) As LnaAdcData
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
    If detSW < 0 Or detSW > 2 Then
      Throw New ArgumentException("LnaReadAdc: Invalid parameter 'detSW=" & detSW & "'")
    End If
    Dim resp As String = _TelnetSession.SendCommandAndReceive("lna readadc " & detSW)
    Dim AdcData As LnaAdcData, a2d As Integer, eng As Double
    GetAdcFields(resp, "diff", AdcData.DiffA2D, eng)
    GetAdcFields(resp, "fwd", AdcData.FwdA2D, eng)
    GetAdcFields(resp, "rev", AdcData.RevA2D, eng)
    GetAdcFields(resp, "temp", a2d, AdcData.TempDegC)
    GetAdcFields(resp, "cur0", a2d, AdcData.Cur0mA)
    GetAdcFields(resp, "cur1", a2d, AdcData.Cur1mA)
    Return AdcData
  End Function

  Private Sub GetAdcFields(ByVal Resp As String, ByVal FieldName As String, ByRef A2d As Integer, ByRef EngVal As Double)
    Dim m As Match
    m = Regex.Match(Resp, FieldName & "\s+(\d+)\s+(\S+)")
    If m.Success Then
      A2d = CInt(m.Groups(1).Value)
      EngVal = CDbl(m.Groups(2).Value)
    Else
      Throw New Exception("GetLnaReadAdcField: could not find FieldName '" & FieldName & "' in response '" & Resp & "'")
    End If
  End Sub

  Public Sub LnaVswrWriteEeprom(ByVal vswr() As VswrTxRecord)
    'lnaee write VSWRCAL <0|1> <temp> <freqIdx 0-5> <freq> <npair> <x y...>
    'lnaee commit <section>..............commit buffer to EEPROM
    Dim str As String, resp As String
    For i As Integer = 0 To vswr.Length - 1
      For j As Integer = 0 To vswr(i).FreqInfo.Length - 1
        str = String.Format("lnaee write VSWRCAL {0} {1}", i, CInt(vswr(i).TempDegC * 10))
        str &= String.Format(" {0} {1} {2}", j, vswr(i).FreqInfo(j).FreqHz, vswr(i).FreqInfo(j).PwrDet.Length)
        For k As Integer = 0 To vswr(i).FreqInfo(j).PwrDet.Length - 1
          With vswr(i).FreqInfo(j).PwrDet(k)
            str &= String.Format(" {0} {1}", .count, CInt(.ValueDBm * 100))
          End With
        Next
        resp = _TelnetSession.SendCommandAndReceive(str)
      Next
    Next
    resp = _TelnetSession.SendCommandAndReceive("lnaee commit VSWRCAL")
    If Not resp.Contains("LNA EEPROM section <VSWRCAL> written successfully") Then
      Throw New Exception("LNA EEPROM write failed: " & resp)
    End If
  End Sub

  Public Sub LnaDetWriteEeprom(ByVal rec() As LnaDetRecord, ByVal TxChan As Integer, ByVal IsFwd As Boolean, ByVal TempDegC As Double)
    'lnaee write DETCAL <table 0-6> <temp> <npair> <x y...>
    '  0-TX0 forward, 1-TX1 forward, 2-TX0 reverse, 3-TX1 reverse
    '  4-EF  forward, 5-EF  reverse, 6-EF  difference
    Dim TableIndex As Integer = TxChan
    If Not IsFwd Then TableIndex += 2
    Dim cmd As String = String.Format("lnaee write DETCAL {0} {1} {2}", TableIndex, CInt(TempDegC * 10), rec.Length)
    For i As Integer = 0 To rec.Length - 1
      cmd &= String.Format(" {0} {1}", rec(i).Adc, CInt(rec(i).PowerDBm * 100))
    Next
    Dim resp As String
    resp = _TelnetSession.SendCommandAndReceive(cmd)
    resp = _TelnetSession.SendCommandAndReceive("lnaee commit DETCAL")
  End Sub

  Public Function LnaVswrReadEeprom() As VswrTxRecord()
    'TRDU700P1.3#RDK>lnaee read VSWRCAL
    'VSWR calibration data (valid):
    '  calibration temperature  <25>
    '  LNA_TX0 is calibrated for 1 frequencies (max 6):
    '    frequency <751000000 Hz>, 2 (x,y) pairs (8 max)
    '      (   0,   0)  (4095,4095)
    '  LNA_TX1 is calibrated for 1 frequencies (max 6):
    '    frequency <751000000 Hz>, 2 (x,y) pairs (8 max)
    '      (   0,   0)  (4095,4095)
    Dim resp = _TelnetSession.SendCommandAndReceive("lnaee read VSWRCAL")
    Dim fields() As String = Split(resp, "LNA_TX")
    If fields.Length < 2 Then
      Throw New Exception("Unexpected response to command 'lnaee read VSWRCAL': " & resp)
    End If

    Dim m As Match = Regex.Match(fields(0), "temperature\s+<(\S+)>")
    Dim temp As Integer = CInt(m.Groups(1).Value)

    Dim vswr(fields.Length - 2) As AndrewIntegratedProducts.DUTDriverFramework.MUSTANG_Transceiver.VswrTxRecord
    For i As Integer = 0 To vswr.Length - 1
      m = Regex.Match(fields(i + 1), "is calibrated for (\d+) frequencies(.*)", RegexOptions.Singleline)

      vswr(i).TempDegC = temp
      Dim NumFreqs As Integer = CInt(m.Groups(1).Value)
      Dim FreqStr As String = m.Groups(2).Value

      Dim mc2 As MatchCollection
      mc2 = Regex.Matches(FreqStr, "frequency <(\d+) Hz>, (\d+) \(x,y\) pairs \(\d+ max\)([^f]+)")
      If mc2.Count <> NumFreqs Then
        Throw New Exception("Number of Frequences do not match in resp: " & resp)
      End If

      ReDim vswr(i).FreqInfo(NumFreqs - 1)
      For j As Integer = 0 To NumFreqs - 1
        vswr(i).FreqInfo(j).FreqHz = CInt(mc2(j).Groups(1).Value)
        Dim NumPairs As Integer = CInt(mc2(j).Groups(2).Value)
        Dim PairStr As String = mc2(j).Groups(3).Value

        Dim mc3 As MatchCollection
        mc3 = Regex.Matches(PairStr, "\(\s*(\d+)\s*,\s*(-?\d+)\s*\)")
        If mc3.Count <> NumPairs Then
          Throw New Exception("Number of A/D pairs do not match in resp: " & resp)
        End If

        ReDim vswr(i).FreqInfo(j).PwrDet(NumPairs - 1)
        For k As Integer = 0 To NumPairs - 1
          vswr(i).FreqInfo(j).PwrDet(k).count = CInt(mc3(k).Groups(1).Value)
          vswr(i).FreqInfo(j).PwrDet(k).ValueDBm = CInt(mc3(k).Groups(2).Value) / 100
        Next
      Next
    Next

    Return vswr
  End Function

  Public ReadOnly Property LnaTxCalRead() As LnaTxCalRecord
    'TRDU700P2.0#309>lnaee read txcal
    'TX path calibration data (valid):
    '  calibration temperature     <280>
    '  LNA_TX0 maxLossDBx100       <68>
    '  LNA_TX0 avgLossDBx100       <68>
    '  LNA_TX0 maxDelayPS          <89238>
    '  LNA_TX0 delayFlatnessPS     <20876>
    '  LNA_TX0 couplerLossDBx100   <4080>
    '  LNA_TX1 maxLossDBx100       <66>
    '  LNA_TX1 avgLossDBx100       <66>
    '  LNA_TX1 maxDelayPS          <86872>
    '  LNA_TX1 delayFlatnessPS     <12269>
    '  LNA_TX1 couplerLossDBx100   <4019>
    Get
      Dim resp As String = _TelnetSession.SendCommandAndReceive("lnaee read txcal")
      Dim rec As LnaTxCalRecord
      ReDim rec.PathValues(1)

      Try
        Dim validstr As String = GetRegexField(resp, "TX path calibration data (\S+):")
        If validstr <> "(valid)" Then
          Throw New Exception("LNA EE data invalid for command 'lnaee read txcal': " & resp)
        End If
        rec.Temperature = CDbl(GetRegexField(resp, "calibration temperature +<(\d+)>")) / 10
        For i As Integer = 0 To 1
          Dim prefix As String = "LNA_TX" & i
          rec.PathValues(i).MaxLossDb = CDbl(GetRegexField(resp, prefix & " maxLossDBx100 +<(\d+)>")) / 100
          rec.PathValues(i).AvgLossDb = CDbl(GetRegexField(resp, prefix & " avgLossDBx100 +<(\d+)>")) / 100
          rec.PathValues(i).MaxDelayPs = GetRegexField(resp, prefix & " maxDelayPS +<(\d+)>")
          rec.PathValues(i).DelayFlatnessPs = GetRegexField(resp, prefix & " delayFlatnessPS +<(\d+)>")
          rec.PathValues(i).CouplerLossDb = CDbl(GetRegexField(resp, prefix & " couplerLossDBx100 +<(\d+)>")) / 100
        Next
      Catch ex As Exception
        Throw New Exception("Unexpected response for 'lnaee read txcal': " & resp, ex)
      End Try

      Return rec
    End Get
  End Property

  Public ReadOnly Property InventoryLna() As InventoryDataLna
    'TRDU700P1.3#RDK>lnaee read inventory
    'Inventory (valid):
    '  serial number     <serialNo TBD....>
    '  apparatus code    <apparatus TBD...>
    '  manufactured date <010109..>
    '  manufacturer name <Andrew..........>
    Get
      Dim inv As InventoryDataLna
      Dim resp As String = _TelnetSession.SendCommandAndReceive("lnaee read inventory")
      If Not resp.Contains("Inventory (valid)") Then
        Throw New Exception("InventoryLna: command returned invalid inventory")
      End If
      inv.SerialNumber = GetInventoryLnaField(resp, "serial number")
      inv.ApparatusCode = GetInventoryLnaField(resp, "apparatus code")
      inv.ManufactureDate = GetInventoryLnaField(resp, "manufactured date")
      inv.ManufacturerName = GetInventoryLnaField(resp, "manufacturer name")
      inv.LnaSerialNumber = GetInventoryLnaField(resp, "LNA serial number")
      inv.LnaApparatusCode = GetInventoryLnaField(resp, "LNA apparatus code")
      Return inv
    End Get
  End Property

  Private Function GetInventoryLnaField(ByVal InvResp As String, ByVal Fieldname As String) As String
    Dim m As Match
    m = Regex.Match(InvResp, Fieldname & " +<(.+)>")
    If m.Success Then
      Return m.Groups(1).Value
    Else
      Throw New Exception("GetInventoryField: could not find inventory field '" & Fieldname & "' in response '" & InvResp & "'")
    End If
  End Function

  Public ReadOnly Property InventoryPA(ByVal channel As Integer) As InventoryDataPa
    Get
      'paeeprom inventory  PA Apparauscode = HV7_CAMS_40_WattsPA1
      'paeeprom inventory  PA SN = 234567
      'paeeprom inventory  Manufacturedate = 11/30/2007
      'paeeprom inventory  Manufacturedcode = 911
      'paeeprom inventory  Frequency = 2140 MHZ
      '5s inventory  Wattage = 0.00
      'paeeprom inventory  Power Supply Part No. = Not Programmed
      'paeeprom inventory  Power Supply Part SN. = Not Programmed
      'paeeprom inventory  CAM DRIVER Part No. = Not Programmed
      'paeeprom inventory  CAM DRIVER Part SN. = Not Programmed
      'paeeprom inventory  CAM FINAL Part No. = Not Programmed
      'paeeprom inventory  CAM FINAL Part SN. = Not Programmed
      Dim cmd() As String = {"paeeprom", "pa2eeprom"}
      Dim resp As String = _TelnetSession.SendCommandAndReceive(cmd(channel) & " inventory d")
      Dim inv As InventoryDataPa
      inv.ApparatusCode = GetInventoryPaField(resp, "PA Apparat?us ?[Cc]ode")
      inv.SerialNumber = GetInventoryPaField(resp, "PA SN")
      inv.ManufactureDate = GetInventoryPaField(resp, "Manufacture ?[Dd]ate")
      inv.ManufactureCode = GetInventoryPaField(resp, "Manufactured?code")
      inv.Frequency = GetInventoryPaField(resp, "Frequency")
      inv.CamDriverPartNumber = GetInventoryPaField(resp, "CAM DRIVER Part No.")
      inv.CamDriverSerialNumber = GetInventoryPaField(resp, "CAM DRIVER Part SN.")
      inv.CamFinalPartNumber = GetInventoryPaField(resp, "CAM FINAL Part No.")
      inv.CamFinalSerialNumber = GetInventoryPaField(resp, "CAM FINAL Part SN.")
      Return inv
    End Get
  End Property

  Private Function GetInventoryPaField(ByVal InvResp As String, ByVal Fieldname As String) As String
    Dim m As Match
    m = Regex.Match(InvResp, Fieldname & " = (.+)")
    If m.Success Then
      Return m.Groups(1).Value
    Else
      Throw New Exception("GetInventoryField: could not find inventory field '" & Fieldname & "' in response '" & InvResp & "'")
    End If
  End Function


  Public Property CustomerInventory2() As InventoryData
    'SerialNumber            : <>
    '- IssuingAgencyCode     : <>
    '- EnterpriseIdentifier  : <>
    '- ManufactoringLocation : <>
    '- ManufactoringYear     : <>
    '- ManufactoringWeek     : <>
    '- SerialSequence        : <>
    'FunctionCode            : <>
    'KsNumber                : <>
    'KsVersion               : <>
    'ComCode                 : <>
    'CLEI                    : <>
    'ECI                     : <>
    'Misc                    : <>
    Get
      Dim resp As String = _TelnetSession.SendCommandAndReceive("inventory unit print")
      Dim inv As InventoryData
      inv.SerialNumber = GetInventoryField(resp, "SerialNumber")
      inv.FunctionCode = GetInventoryField(resp, "FunctionCode")
      inv.KsNumber = GetInventoryField(resp, "KsNumber")
      inv.KsVersion = GetInventoryField(resp, "KsVersion")
      inv.ComCode = GetInventoryField(resp, "ComCode")
      inv.CLEI = GetInventoryField(resp, "CLEI")
      inv.ECI = GetInventoryField(resp, "ECI")
      inv.Misc = GetInventoryField(resp, "Misc")
      Return inv
    End Get
    Set(ByVal value As InventoryData)
      SetInventoryField("SerialNumber", value.SerialNumber, 18)
      SetInventoryField("FunctionCode", value.FunctionCode, 10)
      SetInventoryField("KsNumber", value.KsNumber, 11)
      SetInventoryField("KsVersion", value.KsVersion, 4)
      SetInventoryField("Comcode", value.ComCode, 9)
      SetInventoryField("CLEI", value.CLEI, 10)
      SetInventoryField("ECI", value.ECI, 6)
      SetInventoryField("Misc", value.Misc, 13)
      _TelnetSession.SendCommandAndReceive("inventory unit commit")
    End Set
  End Property

  Public Property CustomerInvent3GPP() As Invent3GPPData
    Get
      Dim resp As String = _TelnetSession.SendCommandAndReceive("invent3GPP unit print")
      Dim tmp As Invent3GPPData
      tmp.UnitFamily = GetInventoryField(resp, "UNITFAMILY")
      tmp.UnitName = GetInventoryField(resp, "UNITNAME")
      tmp.UnitNumber = GetInventoryField(resp, "UNITNUMBER")
      tmp.Version = GetInventoryField(resp, "UNITVERSION")
      tmp.SerialNumber = GetInventoryField(resp, "UNITSERIAL")
      tmp.Firmware = GetInventoryField(resp, "FIRMWARE")
      tmp.Manufacturer = GetInventoryField(resp, "MANF")
      tmp.ManufactureDate = GetInventoryField(resp, "MANFDATE")
      tmp.ServDate = GetInventoryField(resp, "SERVDATE")
      tmp.CLEI = GetInventoryField(resp, "CLEI")
      tmp.ECI = GetInventoryField(resp, "ECI")
      tmp.ManufactureData = GetInventoryField(resp, "MANFDATA")
      Return tmp
    End Get
    Set(ByVal value As Invent3GPPData)
      Dim cmd As String = "invent3GPP unit set"
      _TelnetSession.SendCommandAndReceive(cmd & " UNITFAMILY " & value.UnitFamily)
      _TelnetSession.SendCommandAndReceive(cmd & " UNITNAME " & value.UnitName)
      _TelnetSession.SendCommandAndReceive(cmd & " UNITNUMBER " & value.UnitNumber)
      _TelnetSession.SendCommandAndReceive(cmd & " UNITVERSION " & value.Version)
      _TelnetSession.SendCommandAndReceive(cmd & " UNITSERIAL " & value.SerialNumber)
      _TelnetSession.SendCommandAndReceive(cmd & " FIRMWARE " & value.Firmware)
      _TelnetSession.SendCommandAndReceive(cmd & " MANF " & value.Manufacturer)
      _TelnetSession.SendCommandAndReceive(cmd & " MANFDATE " & value.ManufactureDate)
      _TelnetSession.SendCommandAndReceive(cmd & " SERVDATE " & value.ServDate)
      _TelnetSession.SendCommandAndReceive(cmd & " CLEI " & value.CLEI)
      _TelnetSession.SendCommandAndReceive(cmd & " ECI " & value.ECI)
      _TelnetSession.SendCommandAndReceive(cmd & " MANFDATA " & value.ManufactureData)
      _TelnetSession.SendCommandAndReceive("invent3GPP unit commit")
    End Set
  End Property

  Private Function GetInventoryField(ByVal InvResp As String, ByVal Fieldname As String) As String
    Dim m As Match
    m = Regex.Match(InvResp, Fieldname & " +: <(.+)>")
    If m.Success Then
      Return m.Groups(1).Value
    Else
      Throw New Exception("GetInventoryField: could not find inventory field '" & Fieldname & "' in response '" & InvResp & "'")
    End If
  End Function
  Private Sub SetInventoryField(ByVal Fieldname As String, ByVal value As String, ByVal MaxLen As Integer)
    _TelnetSession.SendCommandAndReceive("inventory unit set " & Fieldname & " " & value)
  End Sub

  ''' <summary>
  ''' Inventory structure is a string array of 6 items:  
  '''   HWREF     value = HwRef   (16 bits value => 2 chars number in dec)    (it has different value if the frequency band os sub-modules changes)
  '''   RUNAME    value = RuName  (32 chars)         (ex: Example: MOD: RFHEAD850)
  '''   PECCODE   value = PecCode (16 chars)         (provided by RE manufacturer)
  '''   SN        value = SerialNumber (16 chars)    (provided by RE manufacturer)
  '''   MANUFDATE value = ManufactoryDate (4 chars)  (wwyy:  w = week, y = year)
  '''   HWRELEASE value = HwRelease (32 bits value => 4 chars number in dec)  (provided by RE manufacturer)
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property CustomerInventory() As String()
    Get
      Try
        'Prototype Answer:
        'HWREF     <value> = HwRef   (16 bits value)
        'RUNAME    <value> = RuName  (32 chars)
        'PECCODE   <value> = PecCode (16 chars)
        'SN        <value> = SerialNumber (16 chars)
        'MANUFDATE <value> = ManufactoryDate (4 chars)
        'HWRELEASE <value> = HwRelease  (4 chars)

        Dim answer As String = _TelnetSession.SendCommandAndReceive("inventory")
        Dim m As MatchCollection
        m = Regex.Matches(answer, "[A-Z]+:\s*(\S+)")
        If m.Count = 6 Then
          For i As Integer = 0 To m.Count - 1
            _Inventory(i) = m(i).Groups(1).Value
          Next
        End If
        Return _Inventory
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Get
    Set(ByVal value As String())
      Try
        If value.Length > 6 Then
          Throw New Exception(String.Format("TRDU Error: Data for inventory has too many paramaters ({0})", value.Length))
        End If
        For i As Integer = 0 To _InventoryField.Length - 1
          _TelnetSession.SendCommandAndReceive(String.Format("invdata {0} {1}", _InventoryField(i), value(i)))
        Next
        _TelnetSession.SendCommandAndReceive("invdata COMMIT")
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Set
  End Property

  Public Property AndrewInventory() As BoardData
    'HELLCAT_0000>help boarddata
    ' boarddata Band <frequency>
    ' boarddata Power <power>
    ' boarddata [OTBPtNo|OTBSerNo|UnitPtNo|UnitSerNo] <string>
    ' boarddata COMMIT
    '   where:
    '       Band          sets the frequency band
    '       Power         sets the nominal output power
    '       OTBRfNo       sets the Part (RF) # of the OTB
    '       OTBSerNo      sets the serial # of the OTB
    '       UnitRfNo      sets the Part (RF) # of the Unit
    '       UnitSerNo     sets the serial # of the Unit
    '       <frequency>   is the nominal frquency in MHz of the band
    '       <power>       is the nominal output power in watts
    '       <string>      is a part # or serial # string of 15 characters or less
    'HELLCAT_0000>boarddata
    'Board Data:

    ' Frequency Band:  2600MHz
    ' Power Output:    30W
    ' Board/Unit   Part (RF) #       Serial #
    ' ==========   ===========       ========
    ' Unit
    ' OTB          RF200204-1A      0447852021

    Get
      Dim answer As String = _TelnetSession.SendCommandAndReceive("boarddata")
      Dim tmp As BoardData
      Try
        tmp = BoardDataParseNew(answer)
        _BoardDataCmdIsNew = True
      Catch ex As Exception
        tmp = BoardDataParseOld(answer)
        _BoardDataCmdIsNew = False
      End Try
      _BoardDataCmdIsKnown = True
      Return tmp
    End Get
    Set(ByVal value As BoardData)
      Dim Commit As Boolean = False
      If value.BandMHz > 1 Then
        _TelnetSession.SendCommandAndReceive("boarddata Band " & value.BandMHz)
        Commit = True
      End If
      If value.PowerWatts > 1 Then
        _TelnetSession.SendCommandAndReceive("boarddata Power " & value.PowerWatts)
        Commit = True
      End If
      If value.OTB_SAP.Length > 0 Then
        _TelnetSession.SendCommandAndReceive("boarddata OTBPtNo " & value.OTB_SAP)
        Commit = True
      End If
      If value.OTB_SN.Length > 0 Then
        _TelnetSession.SendCommandAndReceive("boarddata OTBSerNo " & value.OTB_SN)
        Commit = True
      End If
      If value.TRDU_SAP.Length > 0 Then
        _TelnetSession.SendCommandAndReceive("boarddata UnitRfNo " & value.TRDU_SAP)
        Commit = True
      End If
      If value.TRDU_SN.Length > 0 Then
        _TelnetSession.SendCommandAndReceive("boarddata UnitSerNo " & value.TRDU_SN)
        Commit = True
      End If

      If Not _BoardDataCmdIsKnown Then
        'this will query the BoardData command and parse it to determine if the command format is 'old' or 'new'
        Dim tmpbd As BoardData = AndrewInventory
      End If
      If _BoardDataCmdIsNew Then
        If value.TRDU_SAP100.Length > 0 Then
          _TelnetSession.SendCommandAndReceive("boarddata UnitRf100No " & value.TRDU_SAP100)
          Commit = True
        End If
      End If

      If Commit Then
        _TelnetSession.SendCommandAndReceive("boarddata COMMIT")
      End If
    End Set
  End Property

  Private Function BoardDataParseOld(ByVal answer As String) As BoardData
    Dim tmpBoardData As New BoardData

    tmpBoardData.OTB_SAP = ""
    tmpBoardData.OTB_SN = ""
    tmpBoardData.TRDU_SAP = ""
    tmpBoardData.TRDU_SN = ""
    tmpBoardData.TRDU_SAP100 = ""

    Dim m As Match
    m = Regex.Match(answer, "Band: *(\d+)MHz.*Output: *(\d+)W.*===.*(Unit.*)(OTB.*)", RegexOptions.Singleline)
    If m.Success Then
      tmpBoardData.BandMHz = CInt(m.Groups(1).Value)
      tmpBoardData.PowerWatts = CInt(m.Groups(2).Value)
      Dim fields() As String
      fields = m.Groups(3).Value.Trim.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
      If fields.Length >= 3 Then
        tmpBoardData.TRDU_SN = fields(2)
      End If
      If fields.Length >= 2 Then
        tmpBoardData.TRDU_SAP = fields(1)
      End If
      fields = m.Groups(4).Value.Trim.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
      If fields.Length >= 3 Then
        tmpBoardData.OTB_SN = fields(2)
      End If
      If fields.Length >= 2 Then
        tmpBoardData.OTB_SAP = fields(1)
      End If
    Else
      Throw New Exception("Unexpected response for 'boarddata' command: " & answer)
    End If
    Return tmpBoardData
  End Function

  Private Function BoardDataParseNew(ByVal answer As String) As BoardData
    Dim tmpBoardData As New BoardData

    tmpBoardData.OTB_SAP = ""
    tmpBoardData.OTB_SN = ""
    tmpBoardData.TRDU_SAP = ""
    tmpBoardData.TRDU_SN = ""
    tmpBoardData.TRDU_SAP100 = ""

    Dim m As Match
    m = Regex.Match(answer, "Band: *(\d+)MHz.*Output: *(\d+)W.*===.*(Unit RF100#.*)(Unit RF150#.*)(OTB.*)", RegexOptions.Singleline)
    If m.Success Then
      tmpBoardData.BandMHz = CInt(m.Groups(1).Value)
      tmpBoardData.PowerWatts = CInt(m.Groups(2).Value)
      Dim fields() As String
      fields = m.Groups(3).Value.Trim.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
      If fields.Length >= 4 Then
        tmpBoardData.TRDU_SN = fields(3)
      End If
      If fields.Length >= 3 Then
        tmpBoardData.TRDU_SAP100 = fields(2)
      End If
      fields = m.Groups(4).Value.Trim.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
      If fields.Length >= 4 Then
        tmpBoardData.TRDU_SN = fields(3)
      End If
      If fields.Length >= 3 Then
        tmpBoardData.TRDU_SAP = fields(2)
      End If
      fields = m.Groups(5).Value.Trim.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
      If fields.Length >= 3 Then
        tmpBoardData.OTB_SN = fields(2)
      End If
      If fields.Length >= 2 Then
        tmpBoardData.OTB_SAP = fields(1)
      End If
    Else
      Throw New Exception("Unexpected response for 'boarddata' command: " & answer)
    End If
    Return tmpBoardData
  End Function

  'Public Property AndrewInventory2() As BoardData
  '  'TRDU700P2.1#309>boarddata
  '  'Board Data:

  '  ' Frequency Band:  700MHz
  '  ' Power Output:    45W
  '  ' Board/Unit   Part (RF) #       Serial #
  '  ' ==========   ===========       ========
  '  ' Unit RF100#
  '  ' Unit RF150#  RF150747-1A      LBALLU-BG092300309
  '  ' OTB          RF201670-1A      0447735006

  '  'TRDU700P2.1#309>help boarddata
  '  ' boarddata Band <frequency>
  '  ' boarddata Power <power>
  '  ' boarddata [OTBPtNo|OTBSerNo|UnitPtNo|UnitSerNo] <string>
  '  ' boarddata COMMIT
  '  '   where:
  '  '       Band          sets the frequency band
  '  '       Power         sets the nominal output power
  '  '       OTBRfNo       sets the RF200 Part# of the OTB
  '  '       OTBSerNo      sets the serial # of the OTB
  '  '       UnitRfNo      sets the RF150 Part# of the Unit
  '  '       UnitRf100No   sets the RF100 Part# of the Unit
  '  '       UnitSerNo     sets the serial # of the Unit
  '  '       <frequency>   is the nominal frquency in MHz of the band
  '  '       <power>       is the nominal output power in watts
  '  '       <string>      is a part # or serial # string of 15 characters or less
  '  'TRDU700P2.1#309>

  '  Get
  '    Dim answer As String = _TelnetSession.SendCommandAndReceive("boarddata")
  '    Return BoardDataParseNew(answer)
  '  End Get
  '  Set(ByVal value As BoardData)
  '    Dim Commit As Boolean = False
  '    If value.BandMHz > 1 Then
  '      _TelnetSession.SendCommandAndReceive("boarddata Band " & value.BandMHz)
  '      Commit = True
  '    End If
  '    If value.PowerWatts > 1 Then
  '      _TelnetSession.SendCommandAndReceive("boarddata Power " & value.PowerWatts)
  '      Commit = True
  '    End If
  '    If value.OTB_SAP.Length > 0 Then
  '      _TelnetSession.SendCommandAndReceive("boarddata OTBPtNo " & value.OTB_SAP)
  '      Commit = True
  '    End If
  '    If value.OTB_SN.Length > 0 Then
  '      _TelnetSession.SendCommandAndReceive("boarddata OTBSerNo " & value.OTB_SN)
  '      Commit = True
  '    End If
  '    If value.TRDU_SAP.Length > 0 Then
  '      _TelnetSession.SendCommandAndReceive("boarddata UnitRfNo " & value.TRDU_SAP)
  '      Commit = True
  '    End If
  '    If value.TRDU_SAP100.Length > 0 Then
  '      _TelnetSession.SendCommandAndReceive("boarddata UnitRf100No " & value.TRDU_SAP100)
  '      Commit = True
  '    End If
  '    If value.TRDU_SN.Length > 0 Then
  '      _TelnetSession.SendCommandAndReceive("boarddata UnitSerNo " & value.TRDU_SN)
  '      Commit = True
  '    End If
  '    If Commit Then
  '      _TelnetSession.SendCommandAndReceive("boarddata COMMIT")
  '    End If
  '  End Set
  'End Property

  Property NetCfg() As NetCfgData
    Get
      Dim resp As String = _TelnetSession.SendCommandAndReceive("netcfg")
      Dim cfg As NetCfgData

      'If String.Compare(_DSPRevision, "0.3.0.4") < 0 Then
      '    'TRDU700P1.0#yyz>netcfg
      '    'Network configuration:
      '    '    Mac Address: 00:0D:EE:05:80:33 (Read Only)
      '    '    IP Address : 192.168.99.50
      '    '    Gateway    : 192.168.99.1
      '    '    Net Mask   : 255.255.255.0
      '    '    Hostname   : TRDU700P1.0#yyz
      '    '    Logger Address : 255.255.255.255
      '    '    FTP Server Address : 255.255.255.255
      '    'Cpri Interface:
      '    'Mac Address CHA: 00:0D:EE:05:80:B3 (Read Only)
      '    'Mac Address CHB: FF:FF:FF:FF:FF:FF (Read Only)
      '    cfg.MacAddress = GetNetCfgField(resp, "Mac Address")
      '    cfg.IPAddress = GetNetCfgField(resp, "IP Address")
      '    cfg.Gateway = GetNetCfgField(resp, "Gateway")
      '    cfg.NetMask = GetNetCfgField(resp, "Net Mask")
      '    cfg.Hostname = GetNetCfgField(resp, "Hostname")
      '    cfg.LoggerAddress = GetNetCfgField(resp, "Logger Address")
      '    cfg.FtpServerAddress = GetNetCfgField(resp, "FTP Server Address")
      '    cfg.CpriMacAddressA = GetNetCfgField(resp, "Mac Address CHA")
      'Else
      'netcfg 'starting with version 3.04
      'Network configuration:
      '   Default GW    : 192.168.99.1 
      'LAN Interface:
      '   Mac Address   : 00:0D:EE:05:80:33 (Read Only)
      '   IP Address    : 192.168.99.50 
      '   Net Mask      : 255.255.255.0 
      '   Hostname      : TRDU700P1.3#RDK 
      '   Logger        : 255.255.255.255 
      '   FTP Server    : 255.255.255.255 
      'Cpri Interface A:
      '   Mac Address   : 00:0D:EE:05:80:B3 (Read Only)
      Dim m As Match
      m = Regex.Match(resp, "Network configuration:(.*)LAN Interface:(.*)Cpri Interface A:(.*)", RegexOptions.Singleline)
      cfg.Gateway = GetNetCfgField(m.Groups(1).Value, "Default GW")
      cfg.MacAddress = GetNetCfgField(m.Groups(2).Value, "Mac Address")
      cfg.IPAddress = GetNetCfgField(m.Groups(2).Value, "IP Address")
      cfg.NetMask = GetNetCfgField(m.Groups(2).Value, "Net Mask")
      cfg.Hostname = GetNetCfgField(m.Groups(2).Value, "Hostname")
      cfg.LoggerAddress = GetNetCfgField(m.Groups(2).Value, "Logger")
      cfg.FtpServerAddress = GetNetCfgField(m.Groups(2).Value, "FTP Server")
      cfg.CpriMacAddressA = GetNetCfgField(m.Groups(3).Value, "Mac Address")
      'End If
      Return cfg
    End Get
    Set(ByVal value As NetCfgData)
      'TRDU700P1.0#yyz>help netcfg
      ' netcfg
      ' netcfg [operation] [value]
      ' netcfg COMMIT
      '   where operation is one of the following:
      '       EA <value> = Ethernet (MAC) Address of the unit e.g. 00:0D:EE:04:00:01
      '       IP <value> = IP Address of the unit e.g. 10.20.46.238
      '       GW <value> = IP Address of the gateway e.g. 10.20.40.1
      '       NM <value> = Network mask value e.g. 255.255.248.0
      '       HN <value> = Hostname up to 20 characters e.g. ANDREW
      '       LOG <value> = IP Address of Log Server
      '       FTP <value> = IP Address of Default FTP Server
      '       CMA <value> = MAC Address of CPRI Interface Ethernet
      _TelnetSession.SendCommandAndReceive("netcfg EA " & value.MacAddress)
      _TelnetSession.SendCommandAndReceive("netcfg IP " & value.IPAddress)
      _TelnetSession.SendCommandAndReceive("netcfg GW " & value.Gateway)
      _TelnetSession.SendCommandAndReceive("netcfg NM " & value.NetMask)
      _TelnetSession.SendCommandAndReceive("netcfg HN " & value.Hostname)
      _TelnetSession.SendCommandAndReceive("netcfg LOG " & value.LoggerAddress)
      _TelnetSession.SendCommandAndReceive("netcfg FTP " & value.FtpServerAddress)
      _TelnetSession.SendCommandAndReceive("netcfg CMA " & value.CpriMacAddressA)
      _TelnetSession.SendCommandAndReceive("netcfg COMMIT")
    End Set
  End Property

  Private Function GetNetCfgField(ByVal Resp As String, ByVal Fieldname As String) As String
    Dim m As Match
    m = Regex.Match(Resp, Fieldname & " *: (\S+)")
    If m.Success Then
      Return m.Groups(1).Value
    Else
      Throw New Exception("GetNetCfgField: could not find field '" & Fieldname & "' in response '" & Resp & "'")
    End If
  End Function

  Property Net_IPAddress() As String
    Get
      Dim nc As NetCfgData = NetCfg
      Return nc.IPAddress
    End Get
    Set(ByVal value As String)
      Try
        Dim Answer As String = _TelnetSession.SendCommandAndReceive(String.Format("netcfg IP {0}", value))
        Answer = _TelnetSession.SendCommandAndReceive("netcfg COMMIT")
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Set
  End Property
  Property Net_HostName() As String
    Get
      Dim nc As NetCfgData = NetCfg
      Return nc.Hostname
    End Get
    Set(ByVal value As String)
      Try
        Dim Answer As String = _TelnetSession.SendCommandAndReceive(String.Format("netcfg HN {0}", value))
        Answer = _TelnetSession.SendCommandAndReceive("netcfg COMMIT")
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Set
  End Property
  Property Net_Mask() As String
    Get
      Dim nc As NetCfgData = NetCfg
      Return nc.NetMask
    End Get
    Set(ByVal value As String)
      Try
        Dim Answer As String = _TelnetSession.SendCommandAndReceive(String.Format("netcfg NM {0}", value))
        Answer = _TelnetSession.SendCommandAndReceive("netcfg COMMIT")
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Set
  End Property

  Public Sub Reset()
    Try
      _TelnetSession.SendCommandNoWait("reset")
      Threading.Thread.Sleep(8000)
      Me.Close()
      Threading.Thread.Sleep(22000)
      Me.Open()
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Function BiasPA(ByVal channel As Integer) As Boolean 'Implements ITransceiver.BiasPA
    Try
      If channel < 0 Or channel > 1 Then
        Throw New Exception("Tx channel must be 0 or 1: " & channel)
      End If
      Dim cmd As String
      If channel = 0 Then cmd = "pabias" Else cmd = "pa2bias"
      Dim answer As String = _TelnetSession.SendCommandAndReceive(cmd & " e")
      If answer.ToUpper().Contains("BIAS FAILED") Then
        Throw New Exception("Bias Failed")
      End If
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function

  Public Function DeBiasPA(ByVal channel As Integer) As Boolean 'Implements ITransceiver.DeBiasPA
    Try
      If channel < 0 Or channel > 1 Then
        Throw New Exception("Tx channel must be 0 or 1: " & channel)
      End If
      Dim cmd As String
      If channel = 0 Then cmd = "pabias" Else cmd = "pa2bias"
      Dim answer As String = _TelnetSession.SendCommandAndReceive(cmd & " d")
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function


  Public ReadOnly Property PaStatus(ByVal channel As Integer) As PaStatusData
    'HELLCAT_hc>pa2bias v
    'PA2 Power Supply (28 Volt) = 27.3 Volts
    'PA2 Temperature            = 31 C
    'PA2 CAM Temperature        = -49 C
    'VCA                        = 2400
    'OC set point               = 2500
    'Vtrim                      = 2680
    'BIAS State                 = Normal
    'PA2 FINAL1    cam current = 0.000 amps -- Nom Bias current = 0.000 amps(without RF)
    'PA2 FINAL2    cam current = 0.890 amps -- Nom Bias current = 0.898 amps(without RF)
    'PA2 DRIVER2   cam current = 0.197 amps -- Nom Bias current = 0.200 amps(without RF)
    'PA2 DRIVER1   cam current = 0.058 amps -- Nom Bias current = 0.060 amps(without RF)

    'PA2 FINAL1    cam dac setting =   328 adc =     0 at biasing =     0 (No-RF)
    'PA2 FINAL2    cam dac setting =   796 adc =   437 at biasing =   441 (No-RF)
    'PA2 DRIVER2   cam dac setting =  1448 adc =   725 at biasing =   737 (No-RF)
    'PA2 DRIVER1   cam dac setting =  1471 adc =   215 at biasing =   220 (No-RF)
    Get
      Dim cmd As String
      If channel = 0 Then cmd = "pabias v" Else cmd = "pa2bias v"
      Dim resp As String = _TelnetSession.SendCommandAndReceive(cmd)
      Dim data As PaStatusData

      data.PsVolt = GetRegexField(resp, "Power Supply.*= *(\S+) *Volts")
      data.Temperature = GetRegexField(resp, "PA\d? Temperature *= *(\S+) *C")
      data.TemperatureCAM = GetRegexField(resp, "CAM Temperature *= *(\S+) *C")
      data.CurrentDriver1 = GetRegexField(resp, "DRIVER1 *cam current = *(\S+) amps")
      data.CurrentDriver2 = GetRegexField(resp, "DRIVER2 *cam current = *(\S+) amps")
      data.CurrentFinal1 = GetRegexField(resp, "FINAL1 *cam current = *(\S+) amps")
      data.CurrentFinal2 = GetRegexField(resp, "FINAL2 *cam current = *(\S+) amps")
      data.DacDriver1 = GetRegexField(resp, "DRIVER1.*adc = *(\S+)")
      data.DacDriver2 = GetRegexField(resp, "DRIVER2.*adc = *(\S+)")
      data.DacFinal1 = GetRegexField(resp, "FINAL1.*adc = *(\S+)")
      data.DacFinal2 = GetRegexField(resp, "FINAL2.*adc = *(\S+)")
      Return data
    End Get
  End Property

  Public Function PaD2A_All(ByVal chan As Integer) As Integer()
    Dim panames() As String = {"pa", "pa2"}
    Dim NumChannels = 8
    Dim D2A(NumChannels - 1) As Integer
    Dim resp As String = _TelnetSession.SendCommandAndReceive("pa_all " & panames(chan))
    Dim m As MatchCollection
    m = Regex.Matches(resp, "DAC Channel \S+ Data = \S+ *, *(\d+)")
    If m.Count <> NumChannels Then
      Throw New Exception("FpgaRead invalid resp: " & resp)
    End If
    For i As Integer = 0 To NumChannels - 1
      D2A(i) = CInt(m(i).Groups(1).Value)
    Next
    Return D2A
  End Function

  Public WriteOnly Property PaD2A(ByVal chan As Integer, ByVal index As Integer) As Integer
    Set(ByVal value As Integer)
      Dim panames() As String = {"pa", "pa2"}
      Dim resp As String = _TelnetSession.SendCommandAndReceive("d2a " & panames(chan) & " " & index & " " & value)
    End Set
  End Property

  Public Property PaEepromCamSetpoints(ByVal chan As Integer) As Integer()
    Get
      Dim panames() As String = {"paeeprom", "pa2eeprom"}
      Dim resp As String = _TelnetSession.SendCommandAndReceive(panames(chan) & " cam setpoint r 4")
      Dim m As Match = Regex.Match(resp, "dac setpoint *= *(\d+) +(\d+) +(\d+) +(\d+)")
      If Not m.Success Then Throw New Exception("Invalid response in command PaCamSetPoints: " & resp)
      Dim ret(3) As Integer
      For i As Integer = 0 To 3
        ret(i) = CInt(m.Groups(i + 1).Value)
      Next
      Return ret
    End Get
    Set(ByVal value As Integer())
      Dim panames() As String = {"paeeprom", "pa2eeprom"}
      If value.Length <> 4 Then Throw New ArgumentException("Invalid argument for PaCamSetPoints")
      _TelnetSession.SendCommandAndReceive(String.Format(panames(chan) & " cam setpoint w {0} {1} {2} {3}", value(0), value(1), value(2), value(3)))
    End Set
  End Property

  Public Sub PaEepromCommit(ByVal chan As Integer)
    Dim panames() As String = {"paeeprom", "pa2eeprom"}
    _TelnetSession.SendCommandAndReceive(panames(chan) & " commit")
  End Sub

  Public Sub FpgaWrite(ByVal address As Integer, ByVal value As Integer)
    _TelnetSession.SendCommandAndReceive(String.Format("fpga w 0x{0:X} 0x{1:X}", address, value))
  End Sub

  Public Function FpgaRead(ByVal Address As Integer, ByVal NumPoints As Integer) As Integer()
    Dim resp As String
    resp = _TelnetSession.SendCommandAndReceive(String.Format("fpga r 0x{0:X} 0x{1:X}", Address, NumPoints))

    Dim m As MatchCollection
    m = Regex.Matches(resp, "data = 0x([0-9A-Fa-f]+)")
    If m.Count <> NumPoints Then
      Throw New Exception("FpgaRead invalid resp: " & resp)
    End If

    Dim result() As Integer
    ReDim result(m.Count - 1)
    For i As Integer = 0 To m.Count - 1
      result(i) = Int16.Parse(m(i).Groups(1).Value, Globalization.NumberStyles.HexNumber)
    Next
    Return result
  End Function

  Public Function FpgaRead(ByVal Address As Integer) As Integer
    Dim result() As Integer = FpgaRead(Address, 1)
    Return result(0)
  End Function

  Private Property PE4302_Atten(ByVal dev As String) As Double
    Get
      Dim token As String = "pe4302 getattn " & dev
      Dim tmpString As String = _TelnetSession.SendCommandAndReceive(token)

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
        Dim token As String = String.Format("pe4302 setattn {0} {1:F1}", dev, value)
        Dim tmpString As String = _TelnetSession.SendCommandAndReceive(token)
        If tmpString.ToUpper().Contains("ERROR WRITING") Then
          Throw New Exception("Invalid response for command '" & token & "': " & tmpString)
        End If
      Else
        Throw New ArgumentException("InvalidParameter:PE4302_Atten", "value:" & value)
      End If
    End Set
  End Property

  Public Property TxAttenuator(ByVal channel As Integer) As Double
    Get
      If channel < 0 Or channel > 1 Then Throw New ArgumentException("InvalidParameter:TxAttenuator", "channel:" & channel)
      Dim dev() As String = {"TX0", "TX1"}
      Return PE4302_Atten(dev(channel))
    End Get
    Set(ByVal value As Double)
      If channel < 0 Or channel > 1 Then Throw New ArgumentException("InvalidParameter:TxAttenuator", "channel:" & channel)
      Dim dev() As String = {"TX0", "TX1"}
      PE4302_Atten(dev(channel)) = value
    End Set
    End Property

    Public Function TxDPDTable(ByVal channel As Integer) As Double()
        'Blackhawk>dpd tx0 status l1obj

        'Layer 1 Object Parameters
        '        Data(Collection)
        '    Channel                       : 0
        '    trigger Type                  : 0
        '    Number of captures            : 16
        '    Number of transfers / capture : 15
        '    Number of samples / transfer  : 512

        '  Sequence Alignment Parameters
        '    Amplifier Delay               : 126 0/16
        '    Reference Channel Gain        : 32767+j0      (32767.00<0.00)
        '    Feedback Channel Gain         : 32767+j0      (32767.00<0.00)

        '        Constants()
        '    Learning Constant (Mu)        : 16384
        '    Third Order Offset            : 0+j0          (0.00<0.00)

        '        Estimated(Parameters)
        '    Maximum Table Gain (mag2)     : 769937165 (+4.5762 db)
        '    Minimun Table Gain (mag2)     : 250751849 (-0.2960 db)
        '    Maximum Populated Table Index : 167
        Dim tmpString As String = String.Empty
        Dim tmpArr(1) As Double
        Try
            Dim token As String = String.Format("dpd tx{0} status l1obj", channel)
            tmpString = _TelnetSession.SendCommandAndReceive(token)

            token = "Maximum Table Gain (mag2)     : "
            Dim StartPos As Integer = tmpString.IndexOf(token) + token.Length
            Dim StopPos As Integer = tmpString.IndexOf(" db)") + 4
            tmpArr(0) = CDbl(tmpString.Substring(StartPos, StopPos - StartPos).Split("(")(0))
            token = "Minimun Table Gain (mag2)     : "
            StartPos = tmpString.IndexOf(token) + token.Length
            StopPos = tmpString.Substring(StartPos).IndexOf(" db)") + StartPos + 4
            tmpArr(1) = CDbl(tmpString.Substring(StartPos, StopPos - StartPos).Split("(")(0))

            Return tmpArr
        Catch ex As Exception
            Throw New Exception("Unexpected response for dpd command: " & tmpString)
        End Try
    End Function

    Public Property RxAttenuator(ByVal channel As Integer) As Double
        Get
            If channel < 0 Or channel > 3 Then Throw New ArgumentException("InvalidParameter:RxAttenuator", "channel:" & channel)
            Dim dev() As String = {"RX0", "RX1", "RX2", "RX3"}
            Return PE4302_Atten(dev(channel))
        End Get
        Set(ByVal value As Double)
            If channel < 0 Or channel > 3 Then Throw New ArgumentException("InvalidParameter:RxAttenuator", "channel:" & channel)
            Dim dev() As String = {"RX0", "RX1", "RX2", "RX3"}
            PE4302_Atten(dev(channel)) = value
        End Set
    End Property

    Public Property LNAAttenuator(ByVal channel As Integer) As Double
        Get
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("InvalidParameter:RxAttenuator", "channel:" & channel)
            Dim dev() As String = {"LNA0", "LNA1"}
            Return PE4302_Atten(dev(channel))
        End Get
        Set(ByVal value As Double)
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("InvalidParameter:RxAttenuator", "channel:" & channel)
            Dim dev() As String = {"LNA0", "LNA1"}
            PE4302_Atten(dev(channel)) = value
        End Set
    End Property

    Public Function RxCalibrationNoise(ByVal channel As Integer) As Double
        If channel < 0 Or channel > 3 Then Throw New ArgumentException("InvalidParameter:RxCalibrationNoise", "channel:" & channel)
        Dim resp As String = _TelnetSession.SendCommandAndReceive("rxCal rx" & channel)
        Dim m As Match = Regex.Match(resp, "attenuator set to +(\S+)")
        If m.Success Then
            Return CDbl(m.Groups(1).Value)
        Else
            Throw New Exception("Unexpected response for rxCal command: " & resp)
        End If
    End Function

    Public Function PsVin() As Double
        Dim resp As String = _TelnetSession.SendCommandAndReceive("ps vin")
        Dim m As Match = Regex.Match(resp, "voltage\s*=\s*(\d+)\s*mV")
        If m.Success Then
            Return (CDbl(m.Groups(1).Value) / 1000)
        Else
            Throw New Exception("Unexpected response for PsVin command: " & resp)
        End If
    End Function

    Public Function PsPin() As Double()
        'TRDU700P2.1#309>ps pin
        'Input voltage = 24123 mV, current = 1940 mA, power = 47 W
        Dim resp As String = _TelnetSession.SendCommandAndReceive("ps pin")
        Dim m As Match = Regex.Match(resp, "voltage = (\d+) mV, current = (\d+) mA, power = (\d+) W")
        If m.Success Then
            Dim tmp(2) As Double
            tmp(0) = CDbl(m.Groups(1).Value) / 1000
            tmp(1) = CDbl(m.Groups(2).Value) / 1000
            tmp(2) = CDbl(m.Groups(3).Value)
            Return tmp
        Else
            Throw New Exception("Unexpected response for PsVin command: " & resp)
        End If
    End Function
    Public Property TxVca(ByVal channel As Integer) As Integer
        Get
            Dim cmd() As String = {"pabias", "pa2bias"}
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("TxVca invalid channel: " & channel)
            Dim resp As String = _TelnetSession.SendCommandAndReceive(String.Format("{0} vca", cmd(channel)))
            Dim fields() As String = resp.Split("=")
            If fields.Length = 2 Then
                Return CInt(fields(1).Trim)
            Else
                Throw New Exception("Invalid response to command " & cmd(channel))
            End If
        End Get
        Set(ByVal value As Integer)
            Dim cmd() As String = {"pabias", "pa2bias"}
            If channel < 0 Or channel > 1 Then Throw New ArgumentException("TxVca invalid channel: " & channel)
            Dim resp As String = _TelnetSession.SendCommandAndReceive(String.Format("{0} vca {1}", cmd(channel), value))
            Dim fields() As String = resp.Split("=")
            If fields.Length <> 2 Then
                Throw New Exception("Invalid response to command " & cmd(channel))
            End If
        End Set
    End Property
    Public WriteOnly Property TxLOFreqMHz(ByVal channel As Integer) As Double
        Set(ByVal value As Double)
            If channel < 0 Or channel > 1 Then
                Throw New Exception("Tx channel must be 0 or 1: " & channel)
            End If
            Dim token As String = String.Format("stw8110x tx{0} lo ", channel)
            Dim Freq As Integer = CInt(value * 1000000.0)
            Dim answer As String = _TelnetSession.SendCommandAndReceive(token & " " & Freq)
        End Set
    End Property
    Public WriteOnly Property FpgaLoopback() As Boolean
        Set(ByVal value As Boolean)
            Dim cmd As String = "fpgaloop "
            If value Then cmd &= "enable" Else cmd &= "disable"
            _TelnetSession.SendCommandAndReceive(cmd, True)
        End Set
    End Property
    Public Sub TxBlank(ByVal Chan1 As Boolean, ByVal Chan2 As Boolean)
        Dim mask As Integer = 0
        If Chan1 Then mask += 1
        If Chan2 Then mask += 2
        FpgaWrite(&H30020, mask)
        'Dim answer As String = _TelnetSession.SendCommandAndReceive("fpga w 0x30020 " & mask)
    End Sub
    Public Sub TxInit()
        '_TelnetSession.SendCommandAndReceive("fpga w 0x30004 3")  'clears PA0 and PA1 overrange bits in interrupt reg
        FpgaWrite(&H30004, 3)
        _TelnetSession.SendCommandAndReceive("dac5688 tx0 w 1 9") 'tells transmit D/A not to invert data clock
        _TelnetSession.SendCommandAndReceive("dac5688 tx1 w 1 9") 'tells transmit D/A not to invert data clock

        '_TelnetSession.SendCommandAndReceive("fpga write 0x10015 0x0003") 'Carrier enable (in general 3 means C0 & C1)
        '_TelnetSession.SendCommandAndReceive("fpga write 0x10042 0x4000") 'A0C0 gain
        '_TelnetSession.SendCommandAndReceive("fpga write 0x10043 0x4000") 'A0C1 gain
        '_TelnetSession.SendCommandAndReceive("fpga write 0x10044 0x4000") 'A1C0 gain
        '_TelnetSession.SendCommandAndReceive("fpga write 0x10045 0x4000") 'A1C1 gain
        FpgaWrite(&H10015, 3)
        FpgaWrite(&H10042, &H4000)
        FpgaWrite(&H10043, &H4000)
        FpgaWrite(&H10044, &H4000)
        FpgaWrite(&H10045, &H4000)
    End Sub
    Public Sub TxSineWave()
        _TelnetSession.SendCommandAndReceive("dac5688 tx0 write 8 0")
        _TelnetSession.SendCommandAndReceive("dac5688 tx0 write 9 0")
        _TelnetSession.SendCommandAndReceive("dac5688 tx0 write 10 0x00")
        _TelnetSession.SendCommandAndReceive("dac5688 tx0 write 11 0")
        _TelnetSession.SendCommandAndReceive("dac5688 tx0 write 5 0x80")
        _TelnetSession.SendCommandAndReceive("dac5688 tx0 write 5 0xc0")
    End Sub
    Public Sub RxInitAdc()
        _TelnetSession.SendCommandAndReceive("ads6442 quad_adc w 0x00 0x000")
        _TelnetSession.SendCommandAndReceive("ads6442 quad_adc w 0x04 0x000")
        _TelnetSession.SendCommandAndReceive("ads6442 quad_adc w 0x0A 0x000")
        _TelnetSession.SendCommandAndReceive("ads6442 quad_adc w 0x0B 0x000")
        _TelnetSession.SendCommandAndReceive("ads6442 quad_adc w 0x0C 0x200")
        _TelnetSession.SendCommandAndReceive("ads6442 quad_adc w 0x0D 0x417")
        _TelnetSession.SendCommandAndReceive("ads6442 quad_adc w 0x10 0x000")
        _TelnetSession.SendCommandAndReceive("ads6442 quad_adc w 0x11 0x000")
    End Sub
    Public WriteOnly Property RxLoFreqMHz() As Double
        Set(ByVal value As Double)
            Dim resp, tmp As String
            resp = _TelnetSession.SendCommandAndReceive(String.Format("stw8110x rx_0_1 lo {0}", CInt(value * 1000000.0)))
            Dim m As Match
            m = Regex.Match(resp, "Freq Error\s*=\s*(\S*)")
            If Not m.Success Then
                Throw New Exception("RxLoFreqMHz: invalid response: " & resp)
            End If
            Dim FreqError = CDbl(m.Groups(1).Value)
            'phase_inc = ((15.36 * 1000000.0  + freqError) * 268435456.0) / (1.0 * REF_FREQ);
            'Dim phase_inc As Integer
            'Const REF_FREQ As Double = 61440000.0
            'phase_inc = CInt(((15360000.0 + FreqError) * 268435456.0) / (1.0 * REF_FREQ))
            'FpgaWrite(&H2001A, phase_inc And &HFFFF)
            'FpgaWrite(&H2001B, (phase_inc >> 16) And &HFFFF)
            'FpgaWrite(&H2001E, phase_inc And &HFFFF)
            'FpgaWrite(&H2001F, (phase_inc >> 16) And &HFFFF)
        End Set
    End Property

    Public Function GetFIFO(ByVal path As String, ByVal Numpoints As Integer) As Integer()
        Dim Address As String = "", resp As String, Addr As Integer

        'Verify arguments
        Select Case path
            Case "RX0"
                Address = "0xC0000"
            Case "RX1"
                Address = "0xD0000"
            Case Else
                Throw New ArgumentException("GetFIFO invalid path: " & path)
        End Select
        Addr = Integer.Parse(Right(Address, 5), Globalization.NumberStyles.HexNumber)
        If Numpoints < 0 Or Numpoints > 8192 Then
            Throw New ArgumentException("GetFIFO invalid Numpoints: " & Numpoints.ToString)
        End If

        'Send command to capture the FIFOs and wait for command to complete
        '_TelnetSession.SendCommandAndReceive("fpga w 0x3003A 0x0001") 'capture the data
        FpgaWrite(&H3003A, 1)
        For i As Integer = 1 To 5
            If FpgaRead(&H3003A) = 0 Then Exit For
            'resp = _TelnetSession.SendCommandAndReceive("fpga r 0x3003A")
            'If resp Like "*data = 0x0000*" Then Exit For
            Threading.Thread.Sleep(10)
            If i = 5 Then Throw New Exception("GetFIFO timeout waiting for capture complete")
        Next

        'Now, retrieve the data from the FIFO
        'resp = _TelnetSession.SendCommandAndReceive(String.Format("fpga r {0} {1}", Address, Numpoints))
        'resp = _TelnetSession.SendLongAnswerCommand(String.Format("fpga r {0} {1}", Address, Numpoints))

        'Dim m As System.MatchCollection
        'm = Regex.Matches(resp, "data = 0x([0-9A-Fa-f]+)")

        'Dim result() As Integer
        'ReDim result(m.Count - 1)
        'For i As Integer = 0 To m.Count - 1
        '    'result(i) = Val("&H" & m(i).Groups(1).Value)
        '    result(i) = Int16.Parse(m(i).Groups(1).Value, Globalization.NumberStyles.HexNumber)
        Dim result() As Integer = Nothing
        Try
            If Numpoints > 50 Then
                _SendEvent = False  'turn off event reporting because it is time consuming for a big command
            End If
            result = FpgaRead(Addr, Numpoints)
        Finally
            _SendEvent = True
        End Try
        'Next
        Return result
    End Function

    Public Sub GetFifoFtp(ByVal path As String, ByVal Filename As String)
        Dim FifoName As String = ""

        'Verify arguments
        Select Case path
            Case "TX"
                FifoName = "GetFifo"
            Case Else
                Throw New ArgumentException("GetFifoFtp invalid path: " & path)
        End Select
        For i As Integer = 1 To 5
            Try
                My.Computer.Network.DownloadFile("ftp://" & Me.Address & "/" & FifoName, Filename, "lab", "lab", False, 3000, True)
                Exit For
            Catch ex As Net.WebException
                Threading.Thread.Sleep(100 * i)
            End Try
            If i = 5 Then Throw New Exception("GetFifoFtp error: could not download file")
        Next
    End Sub

    Public ReadOnly Property TempSensors() As TempSensorData
        Get
            'TRDU700P1.3#RDK>tempsensors
            '  RX     = 23 C
            '  FB     = 24 C
            '  PA     = 22 C
            '  LNA    = 25 C
            '  PA2    = 23 C
            '  FPGA   = 30 C
            '  PS     = 25 C
            '  PACAM  = 26 C
            '  PA2CAM = 26 C
            Dim ts As TempSensorData
            Dim resp As String = _TelnetSession.SendCommandAndReceive("tempsensors")
            ts.RX = GetTempSensorField(resp, "RX")
            ts.FB = GetTempSensorField(resp, "FB")
            ts.PA0 = GetTempSensorField(resp, "PA")
            ts.LNA = GetTempSensorField(resp, "LNA")
            ts.PA1 = GetTempSensorField(resp, "PA2")
            ts.FPGA = GetTempSensorField(resp, "FPGA")
            ts.PS = GetTempSensorField(resp, "PS")
            ts.PA0CAM = GetTempSensorField(resp, "PACAM")
            ts.PA1CAM = GetTempSensorField(resp, "PA2CAM")
            Return ts
        End Get
    End Property

    Private Shared Function GetTempSensorField(ByVal resp As String, ByVal FieldName As String) As Integer
        Dim m As Match
        m = Regex.Match(resp, FieldName & " *= *(\S+) *C")
        If m.Success = False Then
            Throw New Exception("GetTempSensorField: could not find field '" & FieldName & "' in response '" & resp & "'")
        End If
        Return CInt(m.Groups(1).Value)
    End Function

    Public Sub SetIQOffset(ByVal channel As Integer, ByVal mag As Double, ByVal phas As Double)
        Dim dev() As String = {"tx0", "tx1"}
        If channel < 0 Or channel > 1 Then Throw New ArgumentException("SetIQOffset invalid channel: " & channel)
        If mag < 0.0# Or mag > 4000.0# Then Throw New ArgumentException("SetIQOffset invalid mag: " & mag)
        If phas < 0.0# Or phas > 360.0# Then Throw New ArgumentException("SetIQOffset invalid phas: " & phas)

        'dac5688 tx1 set qmcoffset 744.00<157.00
        _TelnetSession.SendCommandAndReceive(String.Format("dac5688 {0} set qmcoffset {1:F2}<{2:F2}", dev(channel), mag, phas))
    End Sub

    Public Sub SetIQOffsetIQ(ByVal channel As Integer, ByVal ival As Integer, ByVal qval As Integer)
        Dim dev() As String = {"tx0", "tx1"}
        If channel < 0 Or channel > 1 Then Throw New ArgumentException("SetIQOffset invalid channel: " & channel)
        If ival < -4000 Or ival > 4000 Then Throw New ArgumentException("SetIQOffset invalid ival: " & ival)
        If qval < -4000 Or qval > 4000 Then Throw New ArgumentException("SetIQOffset invalid qval: " & qval)

        'dac5688 tx1 set qmcoffset 100.0+j233.0
        _TelnetSession.SendCommandAndReceive(String.Format("dac5688 {0} set qmcoffset {1}+j{2}", dev(channel), ival, qval))
    End Sub

    Public Sub IQOffset_Commit(ByVal channel As Integer)
        Dim dev() As String = {"tx0", "tx1"}
        If channel < 0 Or channel > 1 Then Throw New ArgumentException("IQOffset_Commit invalid channel: " & channel)
        _TelnetSession.SendCommandAndReceive(String.Format("dac5688 {0} commit", dev(channel)))
    End Sub

    Public Sub QmcPower(ByVal channel As Integer, ByVal NumAvg As Integer, ByRef PowerDb As Double, ByRef Ival As Integer, ByRef Qval As Integer)
        'TRDU700P2.1#309>dpd tx0 qmc power 3
        'Tx0: offset = 680 - j134
        '|-------+------------------------------------------+------------------------------------------|
        '|       |                  alpha                   |                   beta                   |
        '|       |-----------------------+------------------+-----------------------+------------------|
        '| Count |      rectangular      |      polar       |      rectangular      |      polar       |
        '|-------+-----------------------+------------------+-----------------------+------------------|
        '| 0     | -36983.38+j44714.25   | 58027.01<129.59  | 0.00+j0.00            | 0.00<0.00        |
        '| 1     | -30881.88+j36307.13   | 47664.43<130.38  | 0.00+j0.00            | 0.00<0.00        |
        '| 2     | -42927.19+j32353.88   | 53754.22<142.99  | 0.00+j0.00            | 0.00<0.00        |
        '|-------+------------------------------------------+------------------------------------------|
        Dim cmd As String = String.Format("dpd tx{0} qmc power {1}", channel, NumAvg)
        Dim resp = _TelnetSession.SendCommandAndReceive(cmd)
        Dim m As MatchCollection = Regex.Matches(resp, "^\| *(\d+) *\| *(\S+)([-+]j\S+) *\| *(\S+)<(\S+) *\|", RegexOptions.Multiline)
        If m.Count = NumAvg Then
            Dim powsum As Double = 0
            For i As Integer = 0 To m.Count - 1
                powsum += CDbl(m(i).Groups(4).Value) ^ 2
            Next
            PowerDb = 10 * Math.Log10(powsum / NumAvg)
        Else
            Throw New Exception("Unexpected response for command: " & cmd & ": " & resp)
        End If
        Dim m2 As Match = Regex.Match(resp, "offset *= *([-+]?\d+) *([-+]) *j(\d+)")
        If m2.Success Then
            Ival = CInt(m2.Groups(1).Value)
            Qval = CInt(m2.Groups(2).Value & m2.Groups(3).Value)
        Else
            Throw New Exception("Unexpected response for command: " & cmd & ": " & resp)
        End If
    End Sub

    Private Sub _TelnetSession_DataReceivedCompleted(ByVal ReceivedData As String) Handles _TelnetSession.DataReceivedCompleted
        If _SendEvent Then
            ReceivedData = ReceivedData.Replace(Chr(0), "")
            RaiseEvent DeviceMessageReceived(ReceivedData)
        End If
    End Sub

    Public Function SendCommand(ByVal cmd As String, ByVal wait As Boolean) As String
        Return _TelnetSession.SendCommandAndReceive(cmd, wait)
    End Function

    Public Function SendScript(ByVal filename As String) As String
        Dim s As String = My.Computer.FileSystem.ReadAllText(filename)
        Dim lines() As String = s.Split(vbCrLf)
        Dim tmp As String = "", index As String, tmp2 As String = ""
        For i As Integer = 0 To lines.Length - 1
            tmp = lines(i).Trim()
            index = tmp.IndexOf("--")
            If index >= 0 Then
                tmp = tmp.Substring(0, index)
            End If
            index = tmp.IndexOf("//")
            If index >= 0 Then
                tmp = tmp.Substring(0, index)
            End If
            tmp = tmp.Trim()

            If tmp <> "" Then
                tmp2 &= tmp & vbCrLf
                SendCommand(tmp, True)
            End If
        Next
        Return tmp2
    End Function

    Public Sub GetRAMFlightRecorder(ByVal filename As String)
        Try
            RaiseEvent DeviceMessageReceived(String.Format("Downloading RAM Flight Recorder in local file {0} started...", filename))
            My.Computer.Network.DownloadFile("ftp://" & _TelnetSession.IPAddress & "/DiagFileRam.txt", filename, "anonymous", "", False, 10000, True)
            RaiseEvent DeviceMessageReceived(String.Format("Downloading RAM Flight Recorder in local file {0} done.", filename))
        Catch ex As Exception
            Throw New Exception("GetRAMFlightRecorder exception", ex)
        End Try
    End Sub

    Public Function LoopGain() As Double
        Dim resp As String = _TelnetSession.SendCommandAndReceive("fpga t")
        Dim m As Match
        m = Regex.Match(resp, "txpower *= *([0-9]+).*fbpower *= *([0-9]+)", RegexOptions.Singleline)
        If Not m.Success Then
            Throw New Exception("invalid response for 'fpga t' command: resp")
        End If
        Dim fbpower As Long = CInt(m.Groups(2).Value)
        Dim txpower As Long = CInt(m.Groups(1).Value)
        Return Math.Sqrt(fbpower / txpower)
    End Function

    Public WriteOnly Property DpdEnable(ByVal Channel As Integer) As Boolean
        Set(ByVal value As Boolean)
            Dim tmp As String
            If value Then tmp = "ON" Else tmp = "OFF"
            _TelnetSession.SendCommandAndReceive(String.Format("dpd tx{0} {1}", Channel, tmp))
        End Set
    End Property

    Public WriteOnly Property GainTxStep(ByVal TxChan As Integer) As Double
        Set(ByVal value As Double)
            If TxChan < 0 Or TxChan > 1 Then Throw New ArgumentException("InvalidParameter:Channel", "channel:" & TxChan)
            If value < -0.1 Or value > 31.6 Then
                Throw New ArgumentException("InvalidParameter:AttenValue", "value:" & value)
            End If
            value = CDbl(CInt(value * 2) / 2)  'make sure value is multiple of 0.5
            GainTxSet(TxChan, "txstep", value.ToString("0.0"))
        End Set
    End Property

    Public WriteOnly Property GainFbStep(ByVal TxChan As Integer) As Double
        Set(ByVal value As Double)
            If TxChan < 0 Or TxChan > 1 Then Throw New ArgumentException("InvalidParameter:Channel", "channel:" & TxChan)
            If value < -0.1 Or value > 31.6 Then
                Throw New ArgumentException("InvalidParameter:AttenValue", "value:" & value)
            End If
            value = CDbl(CInt(value * 2) / 2)  'make sure value is multiple of 0.5
            GainTxSet(TxChan, "fbstep", value.ToString("0.0"))
        End Set
    End Property

    Public WriteOnly Property GainTxQuotient(ByVal TxChan As Integer) As Integer
        Set(ByVal value As Integer)
            If TxChan < 0 Or TxChan > 1 Then Throw New ArgumentException("InvalidParameter:Channel", "channel:" & TxChan)
            GainTxSet(TxChan, "fbtxq", value.ToString("0"))
        End Set
    End Property

    Public WriteOnly Property GainTemp(ByVal TxChan As Integer) As Double
        Set(ByVal value As Double)
            If TxChan < 0 Or TxChan > 1 Then Throw New ArgumentException("InvalidParameter:Channel", "channel:" & TxChan)
            GainTxSet(TxChan, "temp", value.ToString("0.0"))
        End Set
    End Property

    Public WriteOnly Property GainEnable(ByVal TxChan As Integer) As Boolean
        Set(ByVal value As Boolean)
            If TxChan < 0 Or TxChan > 1 Then Throw New ArgumentException("InvalidParameter:Channel", "channel:" & TxChan)
            Dim tmp As String
            If value Then tmp = "ON" Else tmp = "OFF"
            _TelnetSession.SendCommandAndReceive(String.Format("gain tx{0} {1}", TxChan, tmp))
        End Set
    End Property

    Private Sub GainTxSet(ByVal TxChan As Integer, ByVal param As String, ByVal Value As String)
        Dim cmd As String = String.Format("gain tx{0} set {1} {2}", TxChan, param, Value)
        Dim resp As String = _TelnetSession.SendCommandAndReceive(cmd)
    End Sub

    Public Sub GainCommit()
        _TelnetSession.SendCommandAndReceive("gain commit")
    End Sub

    Public ReadOnly Property GainVca(ByVal TxChan As Integer) As Integer
        Get
            Dim gs As GainStatusData = GainStatus(TxChan)
            Return gs.Vca
            'If TxChan < 0 Or TxChan > 1 Then Throw New ArgumentException("InvalidParameter:Channel", "channel:" & TxChan)
            'Dim resp As String = _TelnetSession.SendCommandAndReceive(String.Format("gain tx{0} v", TxChan))
            'Dim m As Match
            'm = Regex.Match(resp, "vcaSetting *= *([0-9]+)")
            'If m.Success = False Then
            '    Throw New Exception("Invalid response for GainVca: " & resp)
            'End If
            'Return CInt(m.Groups(1).Value)
        End Get
    End Property

    Public ReadOnly Property GainStatus(ByVal TxChan As Integer) As GainStatusData
        'TRDU700P1.3#yyz>gain tx0 v
        '
        '        enabled             =   off,  update count           =     0
        '        attenSetting        =     0,  ocpmGain               =     0
        '        txStepAtten         =   7.0,  fbStepAtten            =  11.0
        '        nomGain             =  0.63,  fbTxQuotient           =   674
        '        nomGainChangeRxStep =  1.00,  nomGainChangeTemp      =  0.94
        '        nomGainChangeOcpm   =  1.00,  nomGainChangeOverdrive =  1.00
        '        gainError           =  1.00,  variableGain           =  0.20
        '        vcaGain             =  1.00,  offsetGain             =  1.00
        '        vcaSetting          =  3000
        '
        'Total Output Power  =   0.0 dBm,
        'C0 Output Power     =   0.0 dBm,  C0 Input Power         =    0.0 dBFS
        'C1 Output Power     =   0.0 dBm,  C1 Input Power         =    0.0 dBFS
        Get
            If TxChan < 0 Or TxChan > 1 Then Throw New ArgumentException("InvalidParameter:Channel", "channel:" & TxChan)
            Dim resp As String = _TelnetSession.SendCommandAndReceive(String.Format("gain tx{0} v", TxChan))
            Dim gs As GainStatusData
            gs.Vca = CInt(GetGainStatusField(resp, "vcaSetting"))
            gs.TxStep = CDbl(GetGainStatusField(resp, "txStepAtten"))
            gs.FbStep = CDbl(GetGainStatusField(resp, "fbStepAtten"))
            gs.FbTxQuo = CInt(GetGainStatusField(resp, "fbTxQuotient"))
            gs.GainError = CDbl(GetGainStatusField(resp, "gainError"))
            gs.Enabled = (GetGainStatusField(resp, "enabled").ToLower = "on")
            Return gs
        End Get
    End Property

    Private Shared Function GetGainStatusField(ByVal resp As String, ByVal FieldName As String) As String
        Dim m As Match
        m = Regex.Match(resp, FieldName & " *= *([\w\.]+)")
        If m.Success = False Then
            Throw New Exception("Gain status field '" & FieldName & "' not found in resp: " & resp)
        End If
        Return m.Groups(1).Value
    End Function

    Public ReadOnly Property PowerSupply() As PowerSupplyData
        'Chan  ------Desc------  Counts  EngValue  Units
        '   0  Csense Buck 30V     739       1.50  A
        '   1  Csense I2C  24V       2       0.00  A
        '   2  Temperature PSB    1336       31.6  Deg C
        '   3  Vsense      30V    4079      30.01  V
        '   4  Csense AISG 24V      18       0.02  A
        '   5  Csense AISG 12V       9       0.01  A
        '   6  Vsense     3.3V    4095       3.33  V
        '   7  Vsense       5V    4095       5.00  V
        '   8  Vsense     6.5V    4095       6.52  V
        '   9  Vsense I2C  24V       0       0.00  V
        '  10  Vsense AISG 12V    3437      11.96  V
        '  11  Vsense AISG 24V    4038      23.81  V
        Get
            Dim resp As String = _TelnetSession.SendCommandAndReceive("ps read")
            Dim ps As PowerSupplyData, a2d As Integer
            GetAdcFields(resp, "Csense I2C  24V", a2d, ps.I2C_24V_Curr)
            GetAdcFields(resp, "Vsense I2C  24V", a2d, ps.I2C_24V_Volt)
            GetAdcFields(resp, "Csense AISG 12V", a2d, ps.AISG_12V_Curr)
            GetAdcFields(resp, "Vsense AISG 12V", a2d, ps.AISG_12V_Volt)
            GetAdcFields(resp, "Csense AISG 24V", a2d, ps.AISG_24V_Curr)
            GetAdcFields(resp, "Vsense AISG 24V", a2d, ps.AISG_24V_Volt)
            GetAdcFields(resp, "Vsense     3.3V", a2d, ps.V3p3_Volt)
            GetAdcFields(resp, "Vsense       5V", a2d, ps.V5p0_Volt)
            GetAdcFields(resp, "Vsense     6.5V", a2d, ps.V6p5_Volt)
            GetAdcFields(resp, "Vsense      30V", a2d, ps.V30p0_Volt)
            GetAdcFields(resp, "Csense Buck 30V", a2d, ps.V30p0_Buck_Curr)
            Return ps
        End Get
    End Property

    Public Sub I2C_WriteIoExpander(ByVal b As Byte)
        'ps ALU_IO write <byte_value>...................Write external filter I/O expander
        'Example response if nothing connected: "Unable to access the I2C device"
        Dim resp As String = _TelnetSession.SendCommandAndReceive(String.Format("ps ALU_IO write 0x{0:X2}", b))
        If resp.Contains("Unable to access the I2C device") Then
            Throw New Exception("Unable to access the I2C device")
        End If
    End Sub

    Public Function I2C_ReadIoExpander() As Byte
        'ps ALU_IO read.................................Read external filter I/O expander
        'Example response if nothing connected: "Unable to access the I2C device"
        Dim resp As String = _TelnetSession.SendCommandAndReceive("ps ALU_IO read")
        If resp.Contains("Unable to access the I2C device") Then
            Throw New Exception("Unable to access the I2C device")
        End If

        'ps ALU_IO read
        'I/O Expander value = 0x5A
        Dim m As Match = Regex.Match(resp, "I/O Expander value = 0x(\S+)")
        If m.Success Then
            Return Byte.Parse(m.Groups(1).Value, Globalization.NumberStyles.HexNumber)
        Else
            Throw New Exception("Invalid response from DUT ALU_IO command:" & resp)
        End If
    End Function

    Public WriteOnly Property I2C_24V_Enable() As Boolean
        Set(ByVal value As Boolean)
            PS_Enable("i2c_24v") = value
        End Set
    End Property

    Public WriteOnly Property AISG_24V_Enable() As Boolean
        Set(ByVal value As Boolean)
            PS_Enable("aisg_24v") = value
        End Set
    End Property

    Public WriteOnly Property AISG_12V_Enable() As Boolean
        Set(ByVal value As Boolean)
            PS_Enable("aisg_12v") = value
        End Set
    End Property

    Private WriteOnly Property PS_Enable(ByVal dev As String) As Boolean
        Set(ByVal value As Boolean)
            Dim tmp, cmd As String
            If value Then tmp = "on" Else tmp = "off"
            cmd = String.Format("ps {0} {1}", dev, tmp)
            Dim resp As String = _TelnetSession.SendCommandAndReceive(cmd)
            If Not resp.ToLower.Contains(("supply turned " & tmp).ToLower) Then
                Throw New Exception(String.Format("Unexpected response for command '{0}': {1}", cmd, resp))
            End If
        End Set
    End Property

    Public WriteOnly Property LEDColor() As LedStateEnum
        Set(ByVal value As LedStateEnum)
            _TelnetSession.SendCommandAndReceive("cpld led " & value.ToString)
        End Set
    End Property

    Public ReadOnly Property CpriStatus(ByVal chan As Integer) As Boolean
        'HELLCAT_0000>cpristatus link1
        '----------------
        '- LINK1 STATUS -
        '----------------
        '  LOS                        : OK     (Signal present)
        '  HFN synch                  : KO     (BAD DL Synchronization)
        Get
            Dim resp = _TelnetSession.SendCommandAndReceive("cpristatus link" & chan)
            Dim m As Match = Regex.Match(resp, "LOS *: *([OK]+)")
            If m.Success Then
                If m.Groups(1).Value <> "OK" Then Return False
                m = Regex.Match(resp, "HFN synch *: ([OK]+)")
                If m.Success Then
                    Return (m.Groups(1).Value = "OK")
                Else
                    Return False
                End If
            Else
                Throw New Exception("Unexpected response for 'CpriStatus': " & resp)
            End If
        End Get
    End Property

    Public ReadOnly Property GPIO() As Integer
        'HELLCAT_0000>help mcp23s17
        'MCP23S17 Command
        '  Read registers, Write register, Write a single bit in a register
        '    mcp23s17 READ <sreg> <ereg>
        '    mcp23s17 WRITE <reg> <8-bit data>
        '    mcp23s17 WRITEBIT <reg> <bit 0-7> <value 0-1>

        'HELLCAT_0000>mcp23s17 read 18
        'MCP23S17 read reg (0x12) data = 0x00

        'GPIOA ( mcp23s17 read 18 ) 
        'GPIOB ( mcp23s17 read 19 ) 

        'GPIO   P1 description   P2 Description
        'GPA0   In 1             In 1
        'GPA1   In 2             In 2
        'GPA2   In 3             In 3
        'GPA3   In 4             Unused
        'GPA4   In 5             In 4
        'GPA5   In 6             In 5
        'GPA6   In 7             In 6
        'GPA7   In 8             Unused

        'GPB0   Unused (out 1)   Unused
        'GPB1   Unused (out 2)   Unused
        'GPB2   In (Open door)   In (Open door)
        'GPB3   Unused           Unused
        'GPB4   Unused           Unused
        'GPB5   Unused           Unused
        'GPB6   Unused           Unused
        'GPB7   Unused           Unused
        Get
            Dim result As Integer
            For i As Integer = 0 To 1
                Dim resp As String = _TelnetSession.SendCommandAndReceive("mcp23s17 read " & (i + 18))
                Dim m As Match = Regex.Match(resp, "data = 0x([0-9A-F]+)")
                If m.Success Then
                    Dim tmp As Integer = Integer.Parse(m.Groups(1).Value, Globalization.NumberStyles.HexNumber)
                    If i = 0 Then result = tmp Else result += (tmp << 8)
                Else
                    Throw New Exception("Unexpected response for 'mpc23s17': resp")
                End If
            Next
            Return result
        End Get
    End Property

    Public ReadOnly Property SystemState() As SystemStateData
        Get
            'HELLCAT_0000>sys state
            'Unit State: In-Service
            'LED State: Standby
            'Tx1 State: Disabled
            'Tx2 State: Disabled
            'Rx1 State: Pass
            'Rx2 State: Pass
            Dim ss As SystemStateData
            Dim resp As String = _TelnetSession.SendCommandAndReceive("sys state")
            Dim m As MatchCollection = Regex.Matches(resp, "(\S+) State: (\S+)")
            Dim d As New Dictionary(Of String, String)
            For i As Integer = 0 To m.Count - 1
                d.Add(m(i).Groups(1).Value, m(i).Groups(2).Value)
            Next
            ss.UnitState = d("Unit")
            ss.LedState = d("LED")
            ss.Tx1State = d("Tx1")
            ss.Tx2State = d("Tx2")
            ss.Rx1State = d("Rx1")
            ss.Rx2State = d("Rx2")
            Return ss
        End Get
    End Property

    Public Function Rssi() As RssiData
        'TRDU700P1.3#002>rssi
        'RX0:
        '  C0 input power = -118.3 dBm,    C0 digital power =  -81.2 dBFS
        '  C1 input power =   -Inf dBm,    C1 digital power =   -Inf dBFS
        'RX1:
        '  C0 input power = -119.3 dBm,    C0 digital power =  -82.2 dBFS
        '  C1 input power =   -Inf dBm,    C1 digital power =   -Inf dBFS
        Dim rs As RssiData
        Dim mc As MatchCollection
        Dim resp As String = _TelnetSession.SendCommandAndReceive("rssi")
        Dim fields() As String = Regex.Split(resp, "RX\d:")
        If fields.Length < 2 Then
            Throw New Exception("Unexpected RSSI response: " & resp)
        End If
        ReDim rs.Rx(fields.Length - 2)
        For i As Integer = 0 To rs.Rx.Length - 1
            mc = Regex.Matches(fields(i + 1), "C\d input power = +(\S+) dBm, +C\d digital power = +(\S+) dBFS")
            If mc.Count = 0 Then
                Throw New Exception("Unexpected RSSI response: " & resp)
            End If
            ReDim rs.Rx(i).Carr(mc.Count - 1)
            For j As Integer = 0 To rs.Rx(i).Carr.Length - 1
                Try
                    rs.Rx(i).Carr(j).InputPower = CDbl(mc(j).Groups(1).Value)
                Catch
                    rs.Rx(i).Carr(j).InputPower = -99
                End Try
                Try
                    rs.Rx(i).Carr(j).DigitalPower = CDbl(mc(j).Groups(2).Value)
                Catch
                    rs.Rx(i).Carr(j).DigitalPower = -99
                End Try
            Next
        Next
        Return rs
    End Function

    Public Sub Rs485Send(ByVal SendString As String)
        'TRDU_700_P0#061>rs485 tx this_is_a_test
        '<this_is_a_test> transmitted successfully
        _TelnetSession.SendCommandAndReceive("rs485 tx " & SendString)
    End Sub

    Public Function Rs485Receive() As String
        'TRDU_700_P0#061>rs485 rx
        '0 characters received: <>
        Dim resp As String = _TelnetSession.SendCommandAndReceive("rs485 rx")
        Dim m As Match = Regex.Match(resp, "characters received: <(.*)>")
        If m.Success Then
            Return m.Groups(1).Value
        Else
            Throw New Exception("Unexpected response for Rs485Receive: " & resp)
        End If
    End Function

    Property VSWR_Alarm_Enabled() As Boolean
        Get
            Return _VSWR_Alarm_Enabled
        End Get
        Set(ByVal value As Boolean)
            _VSWR_Alarm_Enabled = value
            If value Then
                _TelnetSession.SendCommandAndReceive("alarm enable vswr")
            Else
                _TelnetSession.SendCommandAndReceive("alarm disable vswr")
            End If
        End Set
    End Property

    Public Function Vswr(ByVal Txpath As Integer) As Double
        If Txpath < 0 Or Txpath > 1 Then
            Throw New ArgumentException("Invalid argument to Vswr function: Txpath: " & Txpath)
        End If
        Dim resp As String = _TelnetSession.SendCommandAndReceive("vswr readvswr " & (Txpath + 1))
        Dim m As Match = Regex.Match(resp, "return loss = (\S+) dB")
        If m.Success Then
            Vswr = CDbl(m.Groups(1).Value)
        Else
            'Throw New Exception("Unexpected response from Vswr function: " & resp)
            Return -999
        End If
    End Function

    Public WriteOnly Property CustomerTxEnable() As Boolean
        '[
        'MESSAGE: TYPE=SET
        'TRANSACTION: ID = 518
        'TRANSMIT: TXENABLE = DISABLE
        ']
        Set(ByVal value As Boolean)
            Dim tmp As String
            If value Then tmp = "ENABLE" Else tmp = "DISABLE"
            _TelnetCustomer.SendCommand("SET", 518, "TRANSMIT: TXENABLE=" & tmp)
        End Set
    End Property
    Public Sub CustomerCarrierCfg(ByVal Power As Double, ByVal TxFreqMHz As Double, ByVal RxFreqMHz As Double)
        '[ 
        ' MESSAGE:TYPE=SET 
        'TRANSACTION: ID = 103
        ' CARRIERCFG: INDEX=1 STATE=ENABLE TXCONTAINER=1 TX2CONTAINER=2 RX1CONTAINER=1 RX2CONTAINER=2 TXFREQ=751000 RXFREQ=782000 SIGTYPE=NONE POWER=465 CARRTYPE=LTE_10
        '] 
        _TelnetCustomer.SendCommand("SET", 103, _
        String.Format("CARRIERCFG: INDEX=1 STATE=ENABLE TXCONTAINER=1 TX2CONTAINER=2 RX1CONTAINER=1 RX2CONTAINER=2 TXFREQ={1} RXFREQ={2} SIGTYPE=NONE POWER={0} CARRTYPE=LTE_10", CInt(Power * 10), CInt(TxFreqMHz * 1000), CInt(RxFreqMHz * 1000)))
        _CustomerCarrierConfigured = True
    End Sub

    Public Sub CustomerCarrierCfgNew(ByVal Power As Double, ByVal TxFreqMHz As Double, ByVal RxFreqMHz As Double)
        '[
        ' MESSAGE:TYPE=SET
        ' TRANSACTION:ID=103
        ' CARRIERCFG: INDEX=1 STATE=ENABLE TXCONTAINER=1 TX2CONTAINER=5 RX1CONTAINER=1 RX2CONTAINER=5 GRPSIZEDOWN=4 GRPSIZEUP=4 TXFREQ=751000 RXFREQ=782000 SIGTYPE=NONE POWER=463 CARRTYPE=LTE_10
        ']
        _TelnetCustomer.SendCommand("SET", 103, _
        String.Format("CARRIERCFG: INDEX=1 STATE=ENABLE TXCONTAINER=1 TX2CONTAINER=5 RX1CONTAINER=1 RX2CONTAINER=5 GRPSIZEDOWN=4 GRPSIZEUP=4 TXFREQ={1} RXFREQ={2} SIGTYPE=NONE POWER={0} CARRTYPE=LTE_10", CInt(Power * 10), CInt(TxFreqMHz * 1000), CInt(RxFreqMHz * 1000)))
        _CustomerCarrierConfigured = True
    End Sub

    Public Sub CustomerCarrierCfg(ByVal Power As Double, ByVal Enable As Boolean)
        Dim StrEnable As String
        If Enable Then StrEnable = "ENABLE" Else StrEnable = "DISABLE"
        _TelnetCustomer.SendCommand("SET", 102, _
          String.Format("CARRIERCFG: INDEX=1 STATE={0} POWER={1}", StrEnable, CInt(Power * 10)))
        _CustomerCarrierConfigured = Enable
    End Sub

    Public Sub CustomerCarrierCfg(ByVal Enable As Boolean)
        Dim StrEnable As String
        If Enable Then StrEnable = "ENABLE" Else StrEnable = "DISABLE"
        _TelnetCustomer.SendCommand("SET", 103, _
          String.Format("CARRIERCFG: INDEX=1 STATE={0}", StrEnable))
        _CustomerCarrierConfigured = Enable
    End Sub

    Public ReadOnly Property CustomerCarrierConfigured() As Boolean
        Get
            Return _CustomerCarrierConfigured
        End Get
    End Property

    Private Sub _TelnetCustomer_DataReceivedCompleted(ByVal ReceivedData As String) Handles _TelnetCustomer.DataReceivedCompleted
        RaiseEvent DeviceMessageReceived(ReceivedData)
    End Sub

End Class

