Imports visa = Viper_Burn_In.visa32

Public Class VisaDevice
	Private Declare Function viWriteString Lib "visa32.dll" Alias "#257" (ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer
	Public Declare Function viReadString Lib "visa32.dll" Alias "#256" (ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer

	Private vh As Integer
	Private pInitialized As Boolean
	Private pInstrName As String
  Public Const ERR_VISA As Long = -5000
  Private Shared VISA_RM As Long = -1

	Public Sub Init(ByVal InstrName As String)
		Dim stat As Integer

		'Open default Resource Manager if not already open
    If VISA_RM <= 0 Then
      stat = visa.viOpenDefaultRM(VISA_RM)
      If stat <> VI_SUCCESS Then
                Call Err.Raise(ErrorMod.ErrorConstants.ERR_VISA, , "Could not open VISA Resource Manager")
      End If
    End If

		'Open Visa session to this instrument
		stat = visa.viOpen(VISA_RM, InstrName, VI_NULL, VI_NULL, vh)
		Call CheckStatus(stat, "Error opening Visa session")

    pInstrName = InstrName
		pInitialized = True
	End Sub

	Public Property Handle() As Integer
		Get
			Handle = vh
		End Get
		Set(ByVal Value As Integer)
      vh = Value
      pInstrName = GetResourceName(vh)
      If pInstrName = "" Then
        Dim ErrMsg As String = "Error setting Visa Handle " & vh.ToString
                Call Err.Raise(ErrorMod.ErrorConstants.ERR_VISA, "VISA Error", ErrMsg)
      End If
      pInitialized = True
		End Set
	End Property

	Public Shared Function GetResourceName(ByVal vh As Integer) As String
    Dim tmp As New System.Text.StringBuilder("", 256), stat As Integer

		stat = visa.viGetAttribute(vh, visa.VI_ATTR_RSRC_NAME, tmp)
    If stat = VI_SUCCESS Then
      GetResourceName = tmp.ToString
    Else
      GetResourceName = ""
    End If
	End Function

	Public Property Timeout() As Integer
		Get
			Dim stat, tmo As Integer
			stat = visa.viGetAttribute(vh, VI_ATTR_TMO_VALUE, tmo)
			Call CheckStatus(stat, "Error querying VISA timeout")
			Timeout = tmo
		End Get
		Set(ByVal Value As Integer)
			Dim stat As Integer
			stat = visa.viSetAttribute(vh, VI_ATTR_TMO_VALUE, Value)
			Call CheckStatus(stat, "Error setting VISA timeout")
		End Set
	End Property

	Private Sub CheckStatus(ByVal stat As Integer, ByVal ErrMsg As String)
		If stat <> VI_SUCCESS Then
            Call Err.Raise(ErrorMod.ErrorConstants.ERR_VISA, "VISA Status Error for " & pInstrName, ErrMsg & vbNewLine & GetError(stat))
		End If
	End Sub

	Private Function GetError(ByVal stat As Integer) As String
		Dim tmp As Integer
		'Dim tmps As New VB6.FixedLengthString(256)
		Dim tmps As New System.Text.StringBuilder("", 256)
		Dim i As Short
		tmp = visa.viStatusDesc(vh, stat, tmps)
		If tmp = VI_SUCCESS Then
      i = InStr(1, tmps.ToString, Chr(0))
      If i = 0 Then i = tmps.ToString.Length
			GetError = Left(tmps.ToString, i - 1)
		Else
			GetError = ""
		End If
	End Function

	Public Sub viWrite(ByVal cmd As String)
		Dim stat, retcnt As Integer

		Call CheckInitialized()
		stat = viWriteString(vh, cmd, Len(cmd), retcnt)
        'Call LogFile.LogMessage("VIWRITE", pInstrName & "," & Left(cmd, 80))
		Call CheckStatus(stat, "Error writing cmd: " & cmd)
	End Sub

	Public Function viQuery(ByVal cmd As String, Optional ByVal count As Integer = 500) As String
		Call viWrite(cmd)
		viQuery = viRead(count)
	End Function

	Public Function viReadESR() As Byte
		viReadESR = CByte(viQuery("*esr?"))
	End Function

	Public Sub viWriteFromFile(ByVal fname As String, Optional ByRef SwapBytes As Boolean = False)
		Dim retcnt, stat, L As Integer
		Dim fh As Short
		Dim bytes() As Byte
		'Dim buf As String
		Dim i As Integer
		Dim tmp As Byte
		Dim old_tmo, stat2 As Integer

		Call CheckInitialized()

		L = FileLen(fname)

		stat2 = visa.viGetAttribute(vh, VI_ATTR_TMO_VALUE, old_tmo)
		stat2 = visa.viSetAttribute(vh, VI_ATTR_TMO_VALUE, 30000)

		If SwapBytes Then
			'Read in file
			ReDim bytes(L - 1)
			fh = FreeFile()
			bytes = My.Computer.FileSystem.ReadAllBytes(fname)
			For i = 1 To L - 1 Step 2
				tmp = bytes(i)
				bytes(i) = bytes(i - 1)
				bytes(i - 1) = tmp
			Next
			'buf = Bytes2String(bytes)
			stat = visa.viWrite(vh, bytes, L, retcnt)
		Else
			stat = visa.viWriteFromFile(vh, fname, L, retcnt)
		End If

		stat2 = visa.viSetAttribute(vh, VI_ATTR_TMO_VALUE, old_tmo)

		If stat <> VI_SUCCESS Then
            Call Err.Raise(ErrorMod.ErrorConstants.ERR_VISA, "ibwrtf", "Write failed")
		End If
	End Sub

	'Public Function ibln() As Boolean
	'  Call CheckInitialized
	'  ibln = pDeviceFound
	'End Function

	Public Function viRead(Optional ByVal count As Integer = 500) As String
		Dim stat, retcnt As Integer
		Dim buf As String
		Call CheckInitialized()
		buf = Space(count)
		stat = viReadString(vh, buf, Len(buf), retcnt)
        'Call LogFile.LogMessage("VIREAD", pInstrName & "," & Left(buf, retcnt))

		If stat = VI_SUCCESS Then
			viRead = Left(buf, retcnt)
		Else
			'Call err.raise(TazErrorConstants.ERR_visa, "viRead", "Read failed")
			viRead = ""
		End If
	End Function

	Public Function viReadToFile(ByVal fname As String, Optional ByVal cnt As Integer = 1500000) As Boolean
		Dim stat, retcnt As Integer

		Call CheckInitialized()
		stat = visa.viReadToFile(vh, fname, 200000, retcnt)

		viReadToFile = True
	End Function

	Public Sub eot(ByVal enable As Boolean)
		Dim stat, Value As Integer
		Call CheckInitialized()
		If enable Then
			Value = VI_TRUE
		Else
			Value = VI_FALSE
		End If
		stat = visa.viSetAttribute(vh, VI_ATTR_SEND_END_EN, Value)
	End Sub

	Public Sub GoToLocal()
		Dim stat As Integer
		Call CheckInitialized()
		stat = visa.viGpibControlREN(vh, VI_GPIB_REN_ADDRESS_GTL)
	End Sub

  Public Function viReadStb() As Byte
    Dim stat As Short
    Call VISA.viReadStb(vh, stat)
    viReadStb = (stat And &HFF)
  End Function

  Private Sub CheckInitialized()
    If vh <= 0 Then
            Call Err.Raise(ErrorMod.ErrorConstants.ERR_VISA, "CheckInitialized", "Device not Initialized")
    End If
  End Sub

  Public Sub viClear()
    Dim stat As Long

    Call CheckInitialized()
        'Call LogFile.LogMessage("VISA_CLEAR", pInstrName)
    stat = VISA.viClear(vh)
    Call CheckStatus(stat, "Error completing device clear")
  End Sub

  Public Sub WaitOpc(Optional ByVal timeout As Single = 10.0#)
    Dim Resp As String, StartTime As Single
    Call viWrite("*opc?")
        StartTime = Microsoft.VisualBasic.DateAndTime.Timer
    Do
      Resp = viRead
      If Resp Like "*1*" Then Exit Do
            If ElapsedTime(StartTime, Microsoft.VisualBasic.DateAndTime.Timer) > timeout Then
                Call Err.Raise(ERR_VISA, "WaitOpc", "Timeout waiting for VISA operation")
            End If
    Loop
  End Sub

  'Public Function ReadIntegerArray(ByRef Values As Short, ByVal Num2Read As Short) As Integer
  '	Dim stat, retcnt As Integer
  '	stat = VISA.viRead(vh, Values, Num2Read * 2, retcnt)
  '	ReadIntegerArray = retcnt / 2
  'End Function

  'Public Function ReadSingleArray(ByRef Values As Single, ByVal Num2Read As Short) As Integer
  '	Dim stat, retcnt As Integer
  '	stat = visa.viRead(vh, Values, Num2Read * 4, retcnt)
  '	ReadSingleArray = retcnt / 4
  'End Function

  Private Sub Class_Terminate_Renamed()
    On Error GoTo ErrorHandler
    If pInitialized Then
      Call visa.viClose(vh)
    End If
ErrorHandler:
  End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class
