Imports System.Net.Sockets

Public Class CustomerTelnetInterface

    Private MyTelnetClient As TcpClient = Nothing
    Private MyNetworkStream As NetworkStream = Nothing

    Public IPAddress As String
    Public Port As Integer = 1307
    Public TimeOutSec As Integer = 5
    Public IsOpen As Boolean = False

    Event DataReceivedCompleted(ByVal ReceivedData As String)

    Public Function Open() As Boolean
        Try
            If MyTelnetClient Is Nothing Then
                MyTelnetClient = New TcpClient()
            End If
            'Open TCP Client Connection & create a network stream
            MyTelnetClient.Connect(IPAddress, Port)
            If MyNetworkStream Is Nothing Then
                MyNetworkStream = MyTelnetClient.GetStream
            End If
            IsOpen = True

            'Dim r As New Text.RegularExpressions.Regex("Ctrl-D to Escape", Text.RegularExpressions.RegexOptions.Singleline)
            'WaitForResponse(r)
        Catch ex As SocketException
            Throw New Exception("SocketException: " & ex.Message)
        End Try
    End Function

    Public Sub SendCommand(ByVal MsgType As String, ByVal TransactionId As Integer, ByVal cmd As String)
        Dim FullCmd As String, resp As String = ""
        FullCmd = "[" & vbNewLine & _
        "MESSAGE: TYPE=" & MsgType & vbNewLine & _
        "TRANSACTION: ID=" & TransactionId.ToString & vbNewLine & _
        cmd & vbNewLine & "]" & vbNewLine

        'Send the command
        Send(FullCmd)
        Threading.Thread.Sleep(1000)
        resp &= Receive()
        RaiseEvent DataReceivedCompleted(resp)
        'wait for the response
        'Dim r As New Text.RegularExpressions.Regex("\[.*]\s*\[.*TRANSACTION: ID=([0-9]+).*]", Text.RegularExpressions.RegexOptions.Singleline)
        '    resp = WaitForResponse(r)

        'verify that the Transaction ID in the response matches the ID in the original command
        '  note: an error response has a different transaction ID so it won't match the original command
        'If TransactionId <> CInt(r.Match(resp).Groups(1).Value) Or resp.Contains("ERRORIND") Then
        '  Throw New Exception("Bad response for cmd: " & FullCmd & ", resp: " & resp)
        'End If
    End Sub

    Private Function WaitForResponse(ByVal r As Text.RegularExpressions.Regex) As String
        Dim resp As String = ""
        Dim StartTime As Date = Now
        Do
            Threading.Thread.Sleep(10)
            resp &= Receive()
            If r.Match(resp).Success Then Exit Do
            If (Now - StartTime).TotalSeconds > TimeOutSec Then
                If resp <> "" Then RaiseEvent DataReceivedCompleted(resp)
                Throw New Exception("Timeout waiting for telnet response")
            End If
        Loop
        RaiseEvent DataReceivedCompleted(resp)
        Return resp
    End Function

    Private Function Receive() As String
        Dim resp As String = "", N As Integer

        N = MyTelnetClient.Available
        If N > 0 Then
            Dim buffer(N - 1) As Byte
            MyNetworkStream.Read(buffer, 0, buffer.Length)
            resp = System.Text.Encoding.ASCII.GetString(buffer, 0, buffer.Length)
            resp = resp.Replace(Chr(0), "")  'get rid of NULL characters
        End If
        Return resp
    End Function

    Private Sub Send(ByVal cmd As String)
        If MyNetworkStream IsNot Nothing Then
            Dim data As Byte() = System.Text.Encoding.ASCII.GetBytes(cmd)
            MyNetworkStream.Write(data, 0, data.Length)
        Else
            Throw New Exception("Network stream unassigned")
        End If
    End Sub

    Public Function Close() As Boolean
        Try
            Me.SendQuit()

        Catch ex As Exception
            Throw New Exception("ERROR DURING CLOSING THE TELNET SESSION: " & ex.Message)
        End Try
        If MyNetworkStream IsNot Nothing Then
            MyNetworkStream.Close()
            MyNetworkStream = Nothing
        End If
        If MyTelnetClient IsNot Nothing Then
            MyTelnetClient.Close()
            MyTelnetClient = Nothing
        End If
    End Function

    Public Sub SendQuit()
        If IsOpen Then
            Me.IsOpen = False
            Send(Chr(4))
        End If
    End Sub
End Class

