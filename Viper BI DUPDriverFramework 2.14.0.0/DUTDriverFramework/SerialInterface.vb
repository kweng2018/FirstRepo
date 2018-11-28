Imports System.IO.Ports
Public Class SerialInterface
    Implements ICommunications
    Private SerialCom As New System.IO.Ports.SerialPort
    Private Const _ERR_HEAD As String = "SERIAL_COM_ERR:"
    Private Const _ERR_TIMEOUT As String = "SERIAL_COM_ERR: Timeout expired waiting for data"
    Private Const _ERR_COM_CLOSED As String = "SERIAL_COM_ERR: the port is not open"

    Private MyStream As System.IO.Stream
    Private _Address As String
    Private _Port As Integer = 1
    Private _Prompt As String = ">"
    Private _TimeOutSec As Integer = 7
    Private TX_LENGTH As Integer = 8192
    Public Event DataReceivedCompleted(ByVal ReceivedData As String) Implements ICommunications.DataReceivedCompleted
    Property Address() As String Implements ICommunications.Address
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
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
        SyncLock SerialCom
            Try
                If Not SerialCom.IsOpen Then
                    SerialCom.Parity = Parity.None
                    SerialCom.DataBits = 8
                    SerialCom.StopBits = StopBits.One
                    SerialCom.PortName = String.Format("{0}{1}", _Address, _Port)
                    SerialCom.BaudRate = 115200
                    SerialCom.WriteBufferSize = TX_LENGTH
                    SerialCom.ReadBufferSize = 8192 * 10 + 100
                    SerialCom.Open()
                    MyStream = SerialCom.BaseStream
                    SetPrompt()
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End SyncLock
    End Function

    Public Function Open(ByVal MyPrompt As String, ByVal tx_buff_len As Integer, ByVal rx_buff_len As Integer) As Boolean
        SyncLock SerialCom
            Try
                If Not SerialCom.IsOpen Then
                    SerialCom.Parity = Parity.None
                    SerialCom.DataBits = 8
                    SerialCom.StopBits = StopBits.One
                    SerialCom.PortName = String.Format("{0}{1}", _Address, _Port)
                    SerialCom.BaudRate = 115200
                    SerialCom.WriteBufferSize = tx_buff_len
                    SerialCom.ReadBufferSize = rx_buff_len
                    SerialCom.Open()
                    MyStream = SerialCom.BaseStream
                    _Prompt = MyPrompt
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End SyncLock
    End Function

    Public Function Close() As Boolean Implements ICommunications.Close
        SyncLock SerialCom
            Try
                If SerialCom.IsOpen Then
                    SerialCom.Close()
                End If
            Catch ex As Exception
                Throw New Exception("ERROR DURING CLOSING SERIAL PORT: " & ex.Message)
            End Try
        End SyncLock
    End Function

    Public Function SetPrompt() As String 'Implements ICommunications.SetPrompt
        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim ResponseData As String = "", tmpBuffer As String
                    Dim StartPoint As Integer, StopPoint As Integer, tic As Integer
                    Dim rxdata As Byte(), txdata As Byte()
                    Me._Prompt = ">"
                    txdata = System.Text.Encoding.ASCII.GetBytes(vbCr)
                    MyStream.Write(txdata, 0, txdata.Length)
                    tic = Environment.TickCount
                    Do
                        Threading.Thread.Sleep(100)
                        If SerialCom.BytesToRead > 0 Then
                            ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                            MyStream.Read(rxdata, 0, rxdata.Length)
                            tmpBuffer = System.Text.Encoding.ASCII.GetString(rxdata, 0, rxdata.Length) 'ParseSerialString(rxdata)
                            RaiseEvent DataReceivedCompleted(tmpBuffer)
                            ResponseData += tmpBuffer
                            tic = Environment.TickCount
                        End If
                        If tic + Me.TimeOutSec * 1000 < Environment.TickCount Then
                            Throw New Exception(_ERR_TIMEOUT)
                        End If
                    Loop Until ResponseData.Contains(_Prompt)

                    StopPoint = ResponseData.LastIndexOf(">")
                    ResponseData = ResponseData.Remove(StopPoint + 1, ResponseData.Length - StopPoint - 1)
                    StartPoint = ResponseData.LastIndexOf(vbLf)
                    If (StopPoint - StartPoint - 1) > 0 Then
                        Me._Prompt = ResponseData.Substring(StartPoint + 1, StopPoint - StartPoint)
                    Else
                        Throw New Exception(String.Format("Telnet Prompt Not Found (Start Point = {0}, Stop Point = {1}}!", _
                        StartPoint, StopPoint))
                    End If
                    RaiseEvent DataReceivedCompleted("Obtained Prompt: " & Me._Prompt)
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception(_ERR_COM_CLOSED)
        End If
    End Function

    Private Function ParseSerialString(ByVal data As Byte()) As String
        Try
            Dim str As String
            'Swap 10,13 to 13,10
            'For i As Integer = 0 To data.Length - 2
            '    If data(i) = 10 And data(i + 1) = 13 Then
            '        'SWAP
            '        data(i) = 13
            '        data(i + 1) = 10
            '        i += 1
            '    ElseIf data(i) = 13 Then
            '        data(i) = &H20
            '    End If
            'Next
            str = System.Text.Encoding.ASCII.GetString(data, 0, data.Length)
            str = str.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {10}), "")
            str = str.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {0}), "")
            Dim RespStringArray As String() = str.Split(vbCrLf)
            str = ""
            If RespStringArray.Length > 0 Then
                For i As Integer = 0 To RespStringArray.Length - 2
                    If RespStringArray(i).Trim().Length > 0 Then
                        str = str & RespStringArray(i).Trim() & vbCrLf
                    End If
                Next
            End If
            str = Me.Prompt & str
            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Write(ByRef txdata() As Byte, Optional ByVal WaitForPrompt As Boolean = True) As String _
        'Implements ICommunications.Write
        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim PACKET_LENGTH As Integer = SerialCom.WriteBufferSize
                    'PACKET_LENGTH = 1
                    Dim ResponseData As String = "", tmpBuffer As String
                    Dim rxdata As Byte()
                    Dim tic As Integer
                    'Split in smaller packets and tx
                    Dim txbuff As Byte() = New Byte(PACKET_LENGTH - 1) {}
                    Dim num_packets As Integer, last_bytes As Integer
                    num_packets = Math.DivRem(txdata.Length, PACKET_LENGTH, last_bytes)
                    'SerialCom.Encoding = System.Text.Encoding.UTF8
                    For i As Integer = 0 To num_packets - 1
                        'For k As Integer = 0 To PACKET_LENGTH - 1
                        '    txbuff(k) = txdata(k + i * PACKET_LENGTH)
                        'Next
                        SerialCom.Write(txdata, i * PACKET_LENGTH, PACKET_LENGTH)
                        'Check RX buffer
                        If SerialCom.BytesToRead > 0 Then
                            ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                            MyStream.Read(rxdata, 0, rxdata.Length)
                            tmpBuffer = ParseSerialString(rxdata)
                            RaiseEvent DataReceivedCompleted(tmpBuffer)
                            ResponseData += tmpBuffer
                        End If
                    Next
                    'Send LAST BYTES
                    'For k As Integer = 0 To last_bytes - 1
                    '    txbuff(k) = txdata(k + num_packets * PACKET_LENGTH)
                    'Next
                    MyStream.Write(txdata, num_packets * PACKET_LENGTH, last_bytes)
                    'Wait for Prompt on RX

                    'Dim txbuff As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(New Char(0) {"5"})

                    'SerialCom.Write(txbuff, 0, txbuff.Length)

                    If WaitForPrompt Then
                        tic = Environment.TickCount
                        Do
                            Threading.Thread.Sleep(100)
                            If SerialCom.BytesToRead > 0 Then
                                ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                                SerialCom.Read(rxdata, 0, rxdata.Length)
                                tmpBuffer = ParseSerialString(rxdata)
                                RaiseEvent DataReceivedCompleted(tmpBuffer)
                                ResponseData += tmpBuffer
                                tic = Environment.TickCount
                            End If
                            If tic + Me.TimeOutSec * 1000 < Environment.TickCount Then
                                Throw New Exception(_ERR_TIMEOUT)
                            End If
                        Loop Until ResponseData.Contains(_Prompt)
                    End If
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception(_ERR_COM_CLOSED)
        End If
    End Function

    Public Function Write(ByVal Message As String, Optional ByVal WaitForPrompt As Boolean = True) As String _
    Implements ICommunications.Write

        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim ResponseData As String = "", tmpBuffer As String
                    Dim txdata As Byte(), rxdata As Byte()
                    Dim tic As Integer
                    txdata = System.Text.Encoding.ASCII.GetBytes(Message & vbCr)
                    MyStream.Write(txdata, 0, txdata.Length)
                    tic = Environment.TickCount
                    If WaitForPrompt Then
                        Do
                            Threading.Thread.Sleep(100)
                            If SerialCom.BytesToRead > 0 Then
                                ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                                MyStream.Read(rxdata, 0, rxdata.Length)
                                tmpBuffer = ParseSerialString(rxdata)
                                RaiseEvent DataReceivedCompleted(tmpBuffer)
                                ResponseData += tmpBuffer
                                tic = Environment.TickCount
                            End If
                            If tic + Me.TimeOutSec * 1000 < Environment.TickCount Then
                                Throw New Exception(_ERR_TIMEOUT)
                            End If
                        Loop Until ResponseData.Contains(_Prompt)
                    End If
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception(_ERR_COM_CLOSED)
        End If
    End Function

    Public Function Write(ByVal Message As String, ByVal prt As String, Optional ByVal CarriageReturnParam As String = vbCrLf) As String _
        'Implements ICommunications.Write
        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim ResponseData As String = "", tmpBuffer As String
                    Dim txdata As Byte(), rxdata As Byte()
                    Dim tic As Integer
                    If CarriageReturnParam <> "" Then
                        txdata = System.Text.Encoding.ASCII.GetBytes(Message & CarriageReturnParam)
                    Else
                        txdata = System.Text.Encoding.ASCII.GetBytes(Message)
                    End If
                    MyStream.Write(txdata, 0, txdata.Length)
                    tic = Environment.TickCount
                    Do
                        Threading.Thread.Sleep(100)
                        If SerialCom.BytesToRead Then
                            ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                            MyStream.Read(rxdata, 0, rxdata.Length)
                            tmpBuffer = ParseSerialString(rxdata)
                            RaiseEvent DataReceivedCompleted(tmpBuffer)
                            ResponseData += tmpBuffer
                            tic = Environment.TickCount
                        End If
                        If tic + Me.TimeOutSec * 1000 < Environment.TickCount Then
                            Throw New Exception(_ERR_TIMEOUT)
                        End If
                    Loop Until ResponseData.Contains(prt) Or ResponseData.Contains(Me._Prompt)
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception(_ERR_COM_CLOSED)
        End If
    End Function


    Public Function WriteAndReadBytes(ByVal TxMessage As String, ByRef rx As Byte()) As String _
           'Implements ICommunications.WriteAndReadBytes
        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim ResponseData As String = "", tmpBuffer As String
                    Dim txdata As Byte(), rxdata As Byte()
                    Dim tic As Integer, count As Integer = 0
                    txdata = System.Text.Encoding.ASCII.GetBytes(TxMessage & vbCr)
                    MyStream.Write(txdata, 0, txdata.Length)
                    Threading.Thread.Sleep(500)
                    tic = Environment.TickCount
                    Do
                        Threading.Thread.Sleep(1000)
                        If SerialCom.BytesToRead > 0 Then
                            ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                            MyStream.Read(rxdata, 0, rxdata.Length)
                            rxdata.CopyTo(rx, count)
                            count += rxdata.Length
                            'Convertion just to wait for prompt
                            tmpBuffer = System.Text.Encoding.ASCII.GetString(rxdata, 0, rxdata.Length)
                            'RaiseEvent DataReceivedCompleted(tmpBuffer)
                            ResponseData += tmpBuffer
                            tic = Environment.TickCount
                        End If
                        If tic + Me.TimeOutSec * 1000 < Environment.TickCount Then
                            Throw New Exception(_ERR_TIMEOUT)
                        End If
                    Loop Until ResponseData.Contains(_Prompt)
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception(_ERR_COM_CLOSED)
        End If
    End Function

End Class

Public Class ClientSerialInterface

    Private SerialCom As New System.IO.Ports.SerialPort
    Private Const _ERR_HEAD As String = "SERIAL_COM_ERR:"
    Private Const _ERR_TIMEOUT As String = "SERIAL_COM_ERR: Timeout expired waiting for data"
    Private Const _ERR_COM_CLOSED As String = "SERIAL_COM_ERR: the port is not open"

    Private MyStream As System.IO.Stream
    Private _Address As String
    Private _Port As Integer = 1
    Private _Prompt As String = ">"
    Private _TimeOutSec As Integer = 7
    Private TX_LENGTH As Integer = 8192
    Public Event DataReceivedCompleted(ByVal ReceivedData As String)
    Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property

    Property Port() As Integer
        Get
            Return _Port
        End Get
        Set(ByVal value As Integer)
            _Port = value
        End Set
    End Property

    Property Prompt() As String
        Get
            Return _Prompt
        End Get
        Set(ByVal value As String)
            _Prompt = value
        End Set
    End Property

    Property TimeOutSec() As Integer
        Get
            Return _TimeOutSec
        End Get
        Set(ByVal value As Integer)
            _TimeOutSec = value
        End Set
    End Property

    Public Function Open() As Boolean
        SyncLock SerialCom
            Try
                If Not SerialCom.IsOpen Then
                    SerialCom.Parity = Parity.None
                    SerialCom.DataBits = 8
                    SerialCom.StopBits = StopBits.One
                    SerialCom.PortName = String.Format("{0}{1}", _Address, _Port)
                    SerialCom.BaudRate = 115200
                    SerialCom.WriteBufferSize = TX_LENGTH
                    SerialCom.ReadBufferSize = 8192 * 10 + 100
                    SerialCom.Open()
                    MyStream = SerialCom.BaseStream
                    SetPrompt()
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End SyncLock
    End Function

    Public Function Open(ByVal MyPrompt As String, ByVal tx_buff_len As Integer, ByVal rx_buff_len As Integer) As Boolean
        SyncLock SerialCom
            Try
                If Not SerialCom.IsOpen Then
                    SerialCom.Parity = Parity.None
                    SerialCom.DataBits = 8
                    SerialCom.StopBits = StopBits.One
                    SerialCom.PortName = String.Format("{0}{1}", _Address, _Port)
                    SerialCom.BaudRate = 115200
                    SerialCom.WriteBufferSize = tx_buff_len
                    SerialCom.ReadBufferSize = rx_buff_len
                    SerialCom.Open()
                    MyStream = SerialCom.BaseStream
                    _Prompt = MyPrompt
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End SyncLock
    End Function

    Public Function Close() As Boolean
        SyncLock SerialCom
            Try
                If SerialCom.IsOpen Then
                    SerialCom.Close()
                End If
            Catch ex As Exception
                Throw New Exception("ERROR DURING CLOSING SERIAL PORT: " & ex.Message)
            End Try
        End SyncLock
    End Function

    Public Function SetPrompt() As String 'Implements ICommunications.SetPrompt
        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim ResponseData As String = "", tmpBuffer As String
                    Dim StartPoint As Integer, StopPoint As Integer, tic As Integer
                    Dim rxdata As Byte(), txdata As Byte()
                    Me._Prompt = ">"
                    txdata = System.Text.Encoding.ASCII.GetBytes(vbCr)
                    MyStream.Write(txdata, 0, txdata.Length)
                    tic = Environment.TickCount
                    Do
                        Threading.Thread.Sleep(100)
                        If SerialCom.BytesToRead > 0 Then
                            ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                            MyStream.Read(rxdata, 0, rxdata.Length)
                            tmpBuffer = System.Text.Encoding.ASCII.GetString(rxdata, 0, rxdata.Length) 'ParseSerialString(rxdata)
                            RaiseEvent DataReceivedCompleted(tmpBuffer)
                            ResponseData += tmpBuffer
                            tic = Environment.TickCount
                        End If
                        If tic + Me.TimeOutSec * 1000 < Environment.TickCount Then
                            Throw New Exception(_ERR_TIMEOUT)
                        End If
                    Loop Until ResponseData.Contains(_Prompt)

                    StopPoint = ResponseData.LastIndexOf(">")
                    ResponseData = ResponseData.Remove(StopPoint + 1, ResponseData.Length - StopPoint - 1)
                    StartPoint = ResponseData.LastIndexOf(vbLf)
                    If (StopPoint - StartPoint - 1) > 0 Then
                        Me._Prompt = ResponseData.Substring(StartPoint + 1, StopPoint - StartPoint)
                    Else
                        Throw New Exception(String.Format("Telnet Prompt Not Found (Start Point = {0}, Stop Point = {1}}!", _
                        StartPoint, StopPoint))
                    End If
                    RaiseEvent DataReceivedCompleted("Obtained Prompt: " & Me._Prompt)
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception(_ERR_COM_CLOSED)
        End If
    End Function

    Private Function ParseSerialString(ByVal data As Byte()) As String
        Try
            Dim str As String
            'Swap 10,13 to 13,10
            'For i As Integer = 0 To data.Length - 2
            '    If data(i) = 10 And data(i + 1) = 13 Then
            '        'SWAP
            '        data(i) = 13
            '        data(i + 1) = 10
            '        i += 1
            '    ElseIf data(i) = 13 Then
            '        data(i) = &H20
            '    End If
            'Next
            str = System.Text.Encoding.ASCII.GetString(data, 0, data.Length)
            str = str.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {10}), "")
            str = str.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {0}), "")
            Dim RespStringArray As String() = str.Split(vbCrLf)
            str = ""
            If RespStringArray.Length > 0 Then
                For i As Integer = 0 To RespStringArray.Length - 2
                    If RespStringArray(i).Trim().Length > 0 Then
                        str = str & RespStringArray(i).Trim() & vbCrLf
                    End If
                Next
            End If
            str = Me.Prompt & str
            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Write(ByRef txdata() As Byte, Optional ByVal WaitForPrompt As Boolean = True) As String _
        'Implements ICommunications.Write
        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim PACKET_LENGTH As Integer = SerialCom.WriteBufferSize
                    'PACKET_LENGTH = 1
                    Dim ResponseData As String = "", tmpBuffer As String
                    Dim rxdata As Byte()
                    Dim tic As Integer
                    'Split in smaller packets and tx
                    Dim txbuff As Byte() = New Byte(PACKET_LENGTH - 1) {}
                    Dim num_packets As Integer, last_bytes As Integer
                    num_packets = Math.DivRem(txdata.Length, PACKET_LENGTH, last_bytes)
                    'SerialCom.Encoding = System.Text.Encoding.UTF8
                    For i As Integer = 0 To num_packets - 1
                        'For k As Integer = 0 To PACKET_LENGTH - 1
                        '    txbuff(k) = txdata(k + i * PACKET_LENGTH)
                        'Next
                        SerialCom.Write(txdata, i * PACKET_LENGTH, PACKET_LENGTH)
                        'Check RX buffer
                        If SerialCom.BytesToRead > 0 Then
                            ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                            MyStream.Read(rxdata, 0, rxdata.Length)
                            tmpBuffer = ParseSerialString(rxdata)
                            RaiseEvent DataReceivedCompleted(tmpBuffer)
                            ResponseData += tmpBuffer
                        End If
                    Next
                    'Send LAST BYTES
                    'For k As Integer = 0 To last_bytes - 1
                    '    txbuff(k) = txdata(k + num_packets * PACKET_LENGTH)
                    'Next
                    MyStream.Write(txdata, num_packets * PACKET_LENGTH, last_bytes)
                    'Wait for Prompt on RX

                    'Dim txbuff As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(New Char(0) {"5"})

                    'SerialCom.Write(txbuff, 0, txbuff.Length)

                    If WaitForPrompt Then
                        tic = Environment.TickCount
                        Do
                            Threading.Thread.Sleep(100)
                            If SerialCom.BytesToRead > 0 Then
                                ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                                SerialCom.Read(rxdata, 0, rxdata.Length)
                                tmpBuffer = ParseSerialString(rxdata)
                                RaiseEvent DataReceivedCompleted(tmpBuffer)
                                ResponseData += tmpBuffer
                                tic = Environment.TickCount
                            End If
                            If tic + Me.TimeOutSec * 1000 < Environment.TickCount Then
                                Throw New Exception(_ERR_TIMEOUT)
                            End If
                        Loop Until ResponseData.Contains(_Prompt)
                    End If
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception(_ERR_COM_CLOSED)
        End If
    End Function

    Public Function Write(ByVal Message As String, Optional ByVal WaitForPrompt As Boolean = True) As String

        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim ResponseData As String = "", tmpBuffer As String
                    Dim txdata As Byte(), rxdata As Byte()
                    Dim tic As Integer
                    txdata = System.Text.Encoding.ASCII.GetBytes(Message & vbCr)
                    MyStream.Write(txdata, 0, txdata.Length)
                    tic = Environment.TickCount
                    If WaitForPrompt Then
                        Do
                            Threading.Thread.Sleep(100)
                            If SerialCom.BytesToRead > 0 Then
                                ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                                MyStream.Read(rxdata, 0, rxdata.Length)
                                tmpBuffer = ParseSerialString(rxdata)
                                RaiseEvent DataReceivedCompleted(tmpBuffer)
                                ResponseData += tmpBuffer
                                tic = Environment.TickCount
                            End If
                            If tic + Me.TimeOutSec * 1000 < Environment.TickCount Then
                                Throw New Exception(_ERR_TIMEOUT)
                            End If
                        Loop Until ResponseData.Contains(_Prompt)
                    End If
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception(_ERR_COM_CLOSED)
        End If
    End Function

    Public Function Write(ByVal Message As String, ByVal prt As String, Optional ByVal CarriageReturnParam As String = vbCrLf) As String _
        'Implements ICommunications.Write
        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim ResponseData As String = "", tmpBuffer As String
                    Dim txdata As Byte(), rxdata As Byte()
                    Dim tic As Integer
                    If CarriageReturnParam <> "" Then
                        txdata = System.Text.Encoding.ASCII.GetBytes(Message & CarriageReturnParam)
                    Else
                        txdata = System.Text.Encoding.ASCII.GetBytes(Message)
                    End If
                    MyStream.Write(txdata, 0, txdata.Length)
                    tic = Environment.TickCount
                    Do
                        Threading.Thread.Sleep(100)
                        If SerialCom.BytesToRead Then
                            ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                            MyStream.Read(rxdata, 0, rxdata.Length)
                            tmpBuffer = ParseSerialString(rxdata)
                            RaiseEvent DataReceivedCompleted(tmpBuffer)
                            ResponseData += tmpBuffer
                            tic = Environment.TickCount
                        End If
                        If tic + Me.TimeOutSec * 1000 < Environment.TickCount Then
                            Throw New Exception(_ERR_TIMEOUT)
                        End If
                    Loop Until ResponseData.Contains(prt) Or ResponseData.Contains(Me._Prompt)
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception(_ERR_COM_CLOSED)
        End If
    End Function


    Public Function WriteAndReadBytes(ByVal TxMessage As String, ByRef rx As Byte()) As String _
           'Implements ICommunications.WriteAndReadBytes
        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim ResponseData As String = "", tmpBuffer As String
                    Dim txdata As Byte(), rxdata As Byte()
                    Dim tic As Integer, count As Integer = 0
                    txdata = System.Text.Encoding.ASCII.GetBytes(TxMessage & vbCr)
                    MyStream.Write(txdata, 0, txdata.Length)
                    Threading.Thread.Sleep(500)
                    tic = Environment.TickCount
                    Do
                        Threading.Thread.Sleep(1000)
                        If SerialCom.BytesToRead > 0 Then
                            ReDim rxdata(0 To SerialCom.BytesToRead - 1)
                            MyStream.Read(rxdata, 0, rxdata.Length)
                            rxdata.CopyTo(rx, count)
                            count += rxdata.Length
                            'Convertion just to wait for prompt
                            tmpBuffer = System.Text.Encoding.ASCII.GetString(rxdata, 0, rxdata.Length)
                            'RaiseEvent DataReceivedCompleted(tmpBuffer)
                            ResponseData += tmpBuffer
                            tic = Environment.TickCount
                        End If
                        If tic + Me.TimeOutSec * 1000 < Environment.TickCount Then
                            Throw New Exception(_ERR_TIMEOUT)
                        End If
                    Loop Until ResponseData.Contains(_Prompt)
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception(_ERR_COM_CLOSED)
        End If
    End Function

End Class
