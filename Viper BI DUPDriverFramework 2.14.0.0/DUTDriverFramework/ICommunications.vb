Public Interface ICommunications
    Enum BusCommunications
        Serial = 0
        Telnet = 1
    End Enum
    Property Address() As String
    Property Port() As Integer
    Property Prompt() As String
    Property TimeOutSec() As Integer
    Function Open() As Boolean
    Function Close() As Boolean
    Function Write(ByVal Message As String, Optional ByVal WaitForPrompt As Boolean = True) As String
    'Function Write(ByVal Message As String, ByVal prt As String, Optional ByVal CarriageReturnParam As String = vbCrLf) As String
    'Function Write(ByRef txdata As Byte(), Optional ByVal WaitForPrompt As Boolean = True) As String
    'Function WriteAndReadBytes(ByVal TxMessage As String, ByRef rx As Byte()) As String
    'Function SetPrompt() As String
    Event DataReceivedCompleted(ByVal ReceivedData As String)
End Interface
