Public Interface IDeviceUnderTest
    Property HWRev() As String
    ReadOnly Property FWRev() As String
    Property SerialNumber() As String
    Property Address() As String
    Property Model() As String
    Property Port() As Integer
    Property MAC() As String
    Sub Open()
    Sub Close()
    Event DeviceMessageReceived(ByVal Message As String)
End Interface

