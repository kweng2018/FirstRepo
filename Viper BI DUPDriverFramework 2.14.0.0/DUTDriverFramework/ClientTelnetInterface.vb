Imports System.Net.Sockets
'Imports System.Windows.Forms
Public Class ClientTelnetInterface
    Implements ICommunications
    Private MyTelnetClient As TcpClient = Nothing
    Private MyNetworkStream As NetworkStream = Nothing

    Public IPAddress As String
    Public _Port As Integer = 8081
    Public _TimeOutSec As Integer = 15
    Public _Prompt As String
    Public IsOpen As Boolean = False

    Event DataReceivedCompleted(ByVal ReceivedData As String) Implements ICommunications.DataReceivedCompleted

    Property Address() As String Implements ICommunications.Address
        Get
            Return IPAddress
        End Get
        Set(ByVal value As String)
            IPAddress = value
        End Set
    End Property
    Property Port() As Integer Implements ICommunications.Port
        Get
            Return _Port
        End Get
        Set(ByVal value As Integer)
            _Port = value
        End Set
    End Property
    Property Prompt() As String Implements ICommunications.Prompt
        Get
            Return _Prompt
        End Get
        Set(ByVal value As String)
            _Prompt = value
        End Set
    End Property
    Property TimeOutSec() As Integer Implements ICommunications.TimeOutSec
        Get
            Return _TimeOutSec
        End Get
        Set(ByVal value As Integer)
            _TimeOutSec = value
        End Set
    End Property
    Public Function Open() As Boolean Implements ICommunications.Open
        Try
            If MyTelnetClient Is Nothing Then
                MyTelnetClient = New TcpClient()
            End If
            'Open TCP Client Connection
            MyTelnetClient.Connect(IPAddress, _Port)
            'Create a network stream
            If MyNetworkStream Is Nothing Then
                MyNetworkStream = MyTelnetClient.GetStream
            End If
            GetPrompt()
            IsOpen = True
        Catch ex As SocketException
            Throw New Exception("SocketException: " & ex.Message)
        End Try
    End Function
    Public Function Close() As Boolean Implements ICommunications.Close
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

    Public Function SendCommandAndReceive(ByVal Message As String, Optional ByVal WaitForPrompt As Boolean = True) As String Implements ICommunications.Write
        If MyNetworkStream IsNot Nothing Then
            Try
                Dim data As Byte() = System.Text.Encoding.ASCII.GetBytes(Message & vbCrLf)
                MyNetworkStream.Write(data, 0, data.Length)
                'Reset data to be used as buffer to store the response bytes.
                data = New Byte(256) {}
                ' String to store the response ASCII representation.
                Dim ResponseData As String = String.Empty
                ' Read the first batch of the TcpServer response bytes.
                Dim bytes As Int32
                Dim tic As Integer = Environment.TickCount
                Dim toc As Integer = 0
                ' The following keeps on receiving data until the buffer is empty
                If WaitForPrompt = False Then
                    'Do While (1)
                    Threading.Thread.Sleep(2000)
                    If MyTelnetClient.Available Then
                        ReDim data(0 To MyTelnetClient.Available - 1)
                        MyNetworkStream.Read(data, 0, data.Length)
                        'End If
                        'If bytes > 0 Then
                        ResponseData += System.Text.Encoding.ASCII.GetString(data, 0, data.Length)
                        'Clean zero and other chars
                        ResponseData = ResponseData.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {0}), "")
                        'ResponseData = ResponseData.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {10}), "")
                    End If
                    '    Threading.Thread.Sleep(100)
                    '    If ResponseData.Contains(">") Then
                    '        RaiseEvent DataReceivedCompleted(ResponseData)
                    '        Exit Do
                    '    End If
                    '    toc = Environment.TickCount - tic
                    '    If toc / 1000 > Me.TimeOutSec Then
                    '        RaiseEvent DataReceivedCompleted(ResponseData)
                    '        Exit Do
                    '    End If
                    'Loop
                Else
                    Do While (1)
                        If MyTelnetClient.Available Then
                            ReDim data(0 To MyTelnetClient.Available - 1)
                            bytes = MyNetworkStream.Read(data, 0, data.Length)
                        End If
                        If bytes > 0 Then
                            ResponseData = ResponseData + System.Text.Encoding.ASCII.GetString(data, 0, bytes)
                            bytes = 0
                        End If

                        Threading.Thread.Sleep(50)
                        'Application.DoEvents()

                        If ResponseData.Contains(Me.Prompt) Then
                            'Format the string
                            Dim RespStringArray() As String
                            ResponseData = ResponseData.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {10}), "")
                            ResponseData = ResponseData.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {0}), "")
                            RespStringArray = ResponseData.Split(vbCrLf)
                            ResponseData = ""
                            If RespStringArray.Length > 0 Then
                                For i As Integer = 0 To RespStringArray.Length - 2
                                    If RespStringArray(i).Trim().Length > 0 Then
                                        ResponseData = ResponseData & RespStringArray(i) & vbCrLf
                                    End If
                                Next
                            End If
                            ResponseData = Me.Prompt & ResponseData
                            Exit Do
                        End If
                        toc = Environment.TickCount - tic
                        If toc / 1000 > Me.TimeOutSec Then
                            Throw New Exception("Timeout expired waiting for receving TCP data, cmd:" & Message & " rsp:" & ResponseData)
                        End If
                    Loop
                    RaiseEvent DataReceivedCompleted(ResponseData)
                End If
                Return ResponseData

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        Else
            Throw New Exception("TCP Connection not open")
        End If
    End Function
    Public Sub SendBinary(ByVal data As Byte())
        Try
            If MyNetworkStream IsNot Nothing Then
                MyNetworkStream.Write(data, 0, data.Length)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Function SendLongAnswerCommand(ByVal Message As String) As String
        If MyNetworkStream IsNot Nothing Then
            Dim OldBuffer As Integer
            Try
                Dim data As Byte() = System.Text.Encoding.ASCII.GetBytes(Message & vbCrLf)
                MyNetworkStream.Write(data, 0, data.Length)
                'Reset data to be used as buffer to store the response bytes.
                data = New Byte(256) {}
                ' String to store the response ASCII representation.
                Dim ResponseData As String = String.Empty
                ' Read the first batch of the TcpServer response bytes.
                Dim tic As Integer = Environment.TickCount
                Dim toc As Integer = 0
                Dim RespStringArray() As String
                Dim Answer As String
                Dim CompleteAnswer As String = ""
                OldBuffer = MyTelnetClient.ReceiveBufferSize
                MyTelnetClient.ReceiveBufferSize = 20000
                ' The following keeps on receiving data until the buffer is empty
                Do While (1)
                    Threading.Thread.Sleep(100)
                    If MyTelnetClient.Available Then
                        ReDim data(0 To MyTelnetClient.Available - 1)
                        MyNetworkStream.Read(data, 0, data.Length)
                        'End If
                        'If bytes > 0 Then
                        ResponseData = System.Text.Encoding.ASCII.GetString(data, 0, data.Length)
                        'Clean zero and other chars
                        ResponseData = ResponseData.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {0}), "")
                        ResponseData = ResponseData.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {10}), "")
                        RespStringArray = ResponseData.Split(vbCrLf)
                        Answer = ""
                        For i As Integer = 0 To RespStringArray.Length - 2
                            Answer = Answer & vbCrLf & RespStringArray(i)
                        Next
                        CompleteAnswer = CompleteAnswer & " " & Answer
                        RaiseEvent DataReceivedCompleted(Answer)
                    End If
                    Threading.Thread.Sleep(100)
                    If ResponseData.Contains(Me.Prompt) Then
                        'RaiseEvent DataReceivedCompleted(ResponseData)
                        Exit Do
                    End If
                    toc = Environment.TickCount - tic
                    If toc / 1000 > Me.TimeOutSec Then
                        'RaiseEvent DataReceivedCompleted(ResponseData)
                        Exit Do
                    End If
                Loop
                Return CompleteAnswer
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                MyTelnetClient.ReceiveBufferSize = OldBuffer
            End Try
        Else
            Throw New Exception("TCP Connection not open")
        End If
    End Function

    Public Sub SendQuit()
        If MyNetworkStream IsNot Nothing Then
            Try
                If IsOpen = True Then
                    SendCommandNoWait("quit")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                Me.IsOpen = False
            End Try
        End If
    End Sub

    Public Sub SendCommandNoWait(ByVal Message As String)
        Try
            If MyNetworkStream IsNot Nothing Then
                Dim data As Byte() = System.Text.Encoding.ASCII.GetBytes(Message & vbCrLf)
                MyNetworkStream.Write(data, 0, data.Length)
                'Reset data to be used as buffer to store the response bytes.
                data = New Byte(1024) {}
                ' String to store the response ASCII representation.
                Dim ResponseData As String = String.Empty

                Threading.Thread.Sleep(500)
                'Application.DoEvents()

                If MyTelnetClient.Available Then
                    ReDim data(0 To MyTelnetClient.Available - 1)
                    MyNetworkStream.Read(data, 0, data.Length)
                    ResponseData = System.Text.Encoding.ASCII.GetString(data, 0, data.Length)
                End If

                'Format the string
                Dim RespStringArray() As String
                ResponseData = ResponseData.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {10}), "")
                ResponseData = ResponseData.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {0}), "")
                RespStringArray = ResponseData.Split(vbCrLf)
                ResponseData = Me.Prompt
                For i As Integer = 0 To RespStringArray.Length - 2
                    ResponseData = ResponseData & " " & RespStringArray(i)
                Next
                RaiseEvent DataReceivedCompleted(ResponseData)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Function GetPrompt() As String
        Dim Response As String
        'Dim Banner As String
        Dim StartPoint As Integer
        Dim StopPoint As Integer

        Try


            Dim ResponseData As String = "", tmpBuffer As String
            Dim rxdata As Byte(), txdata As Byte()
            Me._Prompt = ">"
            txdata = System.Text.Encoding.ASCII.GetBytes(vbCrLf)
            MyNetworkStream.Write(txdata, 0, txdata.Length)
            Do
                Threading.Thread.Sleep(100)
                If MyTelnetClient.Available > 0 Then
                    ReDim rxdata(0 To MyTelnetClient.Available - 1)
                    MyNetworkStream.Read(rxdata, 0, rxdata.Length)
                    tmpBuffer = System.Text.Encoding.ASCII.GetString(rxdata, 0, rxdata.Length) 'ParseSerialString(rxdata)
                    RaiseEvent DataReceivedCompleted(tmpBuffer)
                    ResponseData += tmpBuffer
                End If
            Loop Until ResponseData.Contains(_Prompt)




            'Me.Prompt = ""
            'Response = Me.SendCommandAndReceive("", False)
            'Banner = Response
            'Get FW Version from Banner
            StopPoint = ResponseData.LastIndexOf(">")
            Response = ResponseData.Remove(StopPoint + 1, ResponseData.Length - StopPoint - 1)
            StartPoint = Response.LastIndexOf(vbCrLf)
            If (StopPoint - StartPoint - 1) > 0 Then
                Response = Response.Substring(StartPoint + 2, StopPoint - StartPoint - 1)
                If Response.IndexOf(">") < Response.Length - 1 Then
                    StartPoint = Response.IndexOf(">")
                    Me.Prompt = Response.Substring(StartPoint + 1).Trim()
                End If

            Else
                Throw New Exception(String.Format("Telnet Prompt Not Found (Start Point = {0}, Stop Point = {1}}!", _
                StartPoint, StopPoint))
            End If
            RaiseEvent DataReceivedCompleted("Obtained Prompt: " & Me.Prompt)
            Return ResponseData
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
