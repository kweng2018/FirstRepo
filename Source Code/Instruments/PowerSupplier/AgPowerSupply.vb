Option Explicit On
Option Strict On

Public Class AgPowerSupply

  Implements iPS
  Private vdev As New VisaDevice
  Private Id As String
  Private Outp As Boolean

  Public ReadOnly Property IdString() As String Implements iPS.IdString
    Get
      Return Id
    End Get
  End Property

  Public Sub Initialize(ByVal Address As String) Implements iPS.Initialize
    Call vdev.Init(Address)
    vdev.Timeout = 1000
    Id = vdev.viQuery("*idn?")
  End Sub

  Public Property OutputOn() As Boolean Implements iPS.OutputOn
    Get
      Return Outp
    End Get
    Set(ByVal value As Boolean)
      Dim tmp As String
      If value Then tmp = "1" Else tmp = "0"
      Call vdev.viQuery("outp " & tmp & ";*opc?")
      Outp = value
    End Set
  End Property

  Public Function ReadCurrent() As Double Implements iPS.ReadCurrent
    Dim tmp As String
    tmp = vdev.viQuery("meas:curr?")
    Return CDbl(tmp)
  End Function

  Public Function ReadVoltage() As Double Implements iPS.ReadVoltage
    Dim tmp As String
    tmp = vdev.viQuery("meas:volt?")
    Return CDbl(tmp)
  End Function

  Public Sub Reset() Implements iPS.Reset
    Call vdev.viQuery("*rst;*opc?")
  End Sub

  Public Sub Setup(ByVal Voltage As Double, ByVal CurrentLim As Double) Implements iPS.Setup
        Call vdev.viQuery("volt " & Voltage & ";curr " & CurrentLim & ";*opc?")    'volt 48;curr 2;*opc?   outp 1;*opc?
  End Sub
End Class
