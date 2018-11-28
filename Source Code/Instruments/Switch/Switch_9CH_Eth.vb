Imports System.Text
Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Sockets

Public Class Switch_9CH_Eth
    Private Buf(30) As Byte

    Public Function TurnRelayOnOff(ByVal ip As String, ByVal port As Integer, ByVal RelaySlot As Integer, ByVal OnOff As Boolean) As Boolean
        Dim s As Socket = Nothing
        Dim address As IPAddress = IPAddress.Parse(ip)
        Dim endPoint As New IPEndPoint(address, port)
        Dim tempSocket As New Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
        Dim tReceive(10) As Byte, tStr As String

        Try
            If My.Computer.Network.Ping(ip) Then
                tempSocket.Connect(endPoint)
                If tempSocket.Connected Then
                    s = tempSocket
                End If

                GetBuf("RELAY", RelaySlot, OnOff)
                s.Send(Buf)
                s.Receive(tReceive)
                tStr = Encoding.ASCII.GetString(tReceive)
                If Not InStr(tStr, "OK") > 1 Then Return False : Exit Function Else Return True
            Else
                'Throw New Exception("Ping " & ip & " Failed.")
                MsgBox("Ping " & ip & " Failed. Please Contact TE.", MsgBoxStyle.OkOnly)
                Return False
            End If
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            Return False
            Exit Function
        End Try
    End Function

    Public Function CheckACK(ByVal ip As String, ByVal port As Integer) As Boolean
        Dim s As Socket = Nothing
        Dim address As IPAddress = IPAddress.Parse(ip)
        Dim endPoint As New IPEndPoint(address, port)
        Dim tempSocket As New Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
        Dim tReceive(10) As Byte, tStr As String

        Try
            If My.Computer.Network.Ping(ip) Then
                tempSocket.Connect(endPoint)
                If tempSocket.Connected Then
                    s = tempSocket
                End If

                GetBuf("ACK")
                s.Send(Buf)
                s.Receive(tReceive)
                tStr = Encoding.ASCII.GetString(tReceive)
                If Not InStr(tStr, "OK") > 1 Then Return False : Exit Function Else Return True
            Else
                'Throw New Exception(ex.Message)
                Return False
            End If
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            Return False
            Exit Function
        End Try
    End Function

    Public Function GetRelayStatus(ByVal ip As String, ByVal port As Integer) As String
        Dim s As Socket = Nothing
        Dim address As IPAddress = IPAddress.Parse(ip)
        Dim endPoint As New IPEndPoint(address, port)
        Dim tempSocket As New Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
        Dim tReceive(20) As Byte, tStr As String

        Try
            If My.Computer.Network.Ping(ip) Then
                tempSocket.Connect(endPoint)
                If tempSocket.Connected Then
                    s = tempSocket
                End If

                GetBuf("STATE")
                s.Send(Buf)
                s.Receive(tReceive)
                tStr = Encoding.ASCII.GetString(tReceive)
                If InStr(tStr, "OK") > 1 Then Return tStr
            Else
                'Throw New Exception(ex.Message)
                Return False
            End If
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            Exit Function
        End Try
    End Function

    Private Sub GetBuf(ByVal command As String, Optional relay As Integer = 0, Optional OnOff As Boolean = False)
        '//02 41 4E 44 52 45 57 20 20 20 20 30 30 30 30 4F 4E 31 30 03 30 32    'Relay 0 , ON
        'ANDREW    0000ON1002
        'OK    
        '//02 41 4E 44 52 45 57 20 20 20 20 30 30 30 30 4F 46 46 31 30 03 34 30 'Relay 0 , OFF
        'ANDREW    0000OFF1040
        'OK    
        '//02 41 4E 44 52 45 57 20 20 20 20 30 30 30 30 4F 4E 31 31 03 30 33    'Relay 1 , ON
        'ANDREW    0000ON11030
        'OK    
        '//02 41 4E 44 52 45 57 20 20 20 20 30 30 30 30 4F 46 46 31 31 03 34 31 'Relay 1 , OFF
        'ANDREW    0000OFF1141
        'OK    
        '//02 41 4E 44 52 45 57 20 20 20 20 30 30 30 30 41 43 4B 31 31 03 33 35 'ACK
        'ANDREW    0000ACK1135
        'ACKOK  
        '//02 41 4E 44 52 45 57 20 20 20 20 30 30 30 30 53 54 41 31 31 03 34 45 'STATE
        'ANDREW    0000STA114E
        'RELAY0=0 RELAY1=0 OK

        'Dim Buf(30) As Byte
        Try
            Buf(0) = &H2
            Buf(1) = &H41
            Buf(2) = &H4E
            Buf(3) = &H44
            Buf(4) = &H52
            Buf(5) = &H45
            Buf(6) = &H57
            Buf(7) = &H20
            Buf(8) = &H20
            Buf(9) = &H20
            Buf(10) = &H20
            Buf(11) = &H30
            Buf(12) = &H30
            Buf(13) = &H30
            Buf(14) = &H30
            Select Case command
                Case "ACK"
                    Buf(15) = &H41
                    Buf(16) = &H43
                    Buf(17) = &H4B
                    Buf(18) = &H31
                    Buf(19) = &H31
                    Buf(20) = &H3
                    Buf(21) = &H33
                    Buf(22) = &H35
                Case "STATE"
                    Buf(15) = &H53
                    Buf(16) = &H54
                    Buf(17) = &H41
                    Buf(18) = &H31
                    Buf(19) = &H31
                    Buf(20) = &H3
                    Buf(21) = &H34
                    Buf(22) = &H45
                Case "RELAY"
                    If relay = 0 And OnOff = True Then
                        Buf(15) = &H4F
                        Buf(16) = &H4E
                        Buf(17) = &H31
                        Buf(18) = &H30
                        Buf(19) = &H3
                        Buf(20) = &H30
                        Buf(21) = &H32
                    End If
                    If relay = 0 And OnOff = False Then
                        Buf(15) = &H4F
                        Buf(16) = &H46
                        Buf(17) = &H46
                        Buf(18) = &H31
                        Buf(19) = &H30
                        Buf(20) = &H3
                        Buf(21) = &H34
                        Buf(22) = &H30
                    End If
                    If relay = 1 And OnOff = True Then
                        Buf(15) = &H4F
                        Buf(16) = &H4E
                        Buf(17) = &H31
                        Buf(18) = &H31
                        Buf(19) = &H3
                        Buf(20) = &H30
                        Buf(21) = &H33
                    End If
                    If relay = 1 And OnOff = False Then
                        Buf(15) = &H4F
                        Buf(16) = &H46
                        Buf(17) = &H46
                        Buf(18) = &H31
                        Buf(19) = &H31
                        Buf(20) = &H3
                        Buf(21) = &H34
                        Buf(22) = &H31
                    End If
            End Select

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
End Class
