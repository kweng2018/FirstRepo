Option Explicit On
Option Strict On

Public Interface iPS
  Sub Initialize(ByVal Address As String)
  Sub Reset()

  ReadOnly Property IdString() As String

  Sub Setup(ByVal Voltage As Double, ByVal CurrentLim As Double)
  Property OutputOn() As Boolean

  Function ReadCurrent() As Double
  Function ReadVoltage() As Double
End Interface
