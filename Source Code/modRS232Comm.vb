Imports System
Imports System.IO
Imports System.Threading

Module modRS232Comm
    Public Sub RS232_Open(ByVal chkDTR As Boolean, ByVal chkRTS As Boolean, Optional ByRef IsOpen As Boolean = False)
        SyncLock moRS232
            Try
                '// Setup parameters
                With moRS232
                    .Port = BI_HW.Chamber_Port_Number
                    .BaudRate = 9600
                    .DataBit = 8
                    .StopBit = 1
                    .Parity = 0
                    .Timeout = 1000
                End With
                '// Initializes port
                moRS232.Open()
                '// Set state of RTS / DTS
                moRS232.Dtr = chkDTR
                moRS232.Rts = chkRTS
            Catch Ex As Exception
                AddMessage(Ex.Message)
            Finally
                IsOpen = moRS232.IsOpen
            End Try
        End SyncLock
    End Sub

    Public Sub RS232_Close()
        SyncLock moRS232
            Try
                moRS232.Close()
            Catch Ex As Exception
                AddMessage(Ex.Message)
            End Try
        End SyncLock
    End Sub

    Public Delegate Sub Task_TX_RX(ByVal sTx As String, ByVal Index As Integer)

    Public Function RS232_TX_RX(ByVal sTx As String) As String
        SyncLock moRS232
            Try
                moRS232.PurgeBuffer(Rs232.PurgeBuffers.TxClear Or Rs232.PurgeBuffers.RXClear)
                moRS232.Write(sTx)

                System.Threading.Thread.Sleep(100)

                moRS232.Read(moRS232.InBufferCount)
                Return sTx & SplitChar & moRS232.InputStreamString
            Catch Ex As Exception
                Return sTx & SplitChar & "Error occurred: " & Ex.Message ' & "  data fetched: " & moRS232.InputStreamString
            End Try
        End SyncLock
    End Function
End Module
