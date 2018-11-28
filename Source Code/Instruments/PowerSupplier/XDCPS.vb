Option Explicit On
Option Strict On

Public Class XDCPS
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
            Call vdev.viQuery("out " & tmp)
            Outp = value
        End Set
    End Property

    Public Function ReadCurrent() As Double Implements iPS.ReadCurrent
        Dim tmp As String
        tmp = Mid(vdev.viQuery("iout? "), 5)
        Return CDbl(tmp)
    End Function

    Public Function ReadVoltage() As Double Implements iPS.ReadVoltage
        Dim tmp As String
        tmp = Mid(vdev.viQuery("vout? "), 5)
        Return CDbl(tmp)
    End Function

    Public Sub Reset() Implements iPS.Reset
        Call vdev.viQuery("*rst;*opc?")
    End Sub

    Public Sub Setup(ByVal Voltage As Double, ByVal CurrentLim As Double) Implements iPS.Setup
        Call vdev.viQuery("iset " & CurrentLim & ";vset " & Voltage)
    End Sub
End Class


