
Public Class frmParam

    Private Sub TCPIPconnection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        frmParam_Load()
    End Sub
    Public Sub frmParam_Load()
        'ShelfID = Trim(UCase(Command))
        'ShelfID = Microsoft.VisualBasic.Mid(ShelfID, 1, 1)
        'If Asc(ShelfID) < Asc("A") Or Asc(ShelfID) > Asc("E") Then
        '    MsgBox("Unable to locate ShelfID in Command Line." & NL & "Expected first character of Command Line: 'A', 'B', 'C', 'D', 'E'.", vbOKOnly + vbCritical)
        '    Me.Close()
        '    Exit Sub
        'End If
        'Select Case ShelfID
        '    Case "A"
        '        Shelf2Slot = 0
        '    Case "B"
        '        Shelf2Slot = SlotNum + 1
        '    Case "C"
        '        Shelf2Slot = 2 * (SlotNum + 1)
        '    Case "D"
        '        Shelf2Slot = 3 * (SlotNum + 1)
        '    Case "E"
        '        Shelf2Slot = 4 * (SlotNum + 1)
        'End Select
        'SlotNum = Microsoft.VisualBasic.Mid(Command, 2, 1) - 1

        PathsOK()
        LoadConfigXML()
        IniParam()

        If BI_HW.PlantPC = UCase(My.Computer.Name) Then
            Plant = UCase(BI_HW.Plant)
        Else
            Plant = "ERROR"
            MsgBox("Plant Mark is ERROR." & vbCr & "Please Contact TE.", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn.Click
        UpdateParam()
        SaveConfigXML()
        frmMain.Show()
        Me.Hide()
    End Sub

    Private Sub SetConnectionTypeCombo(ByVal Combo As DataGridViewComboBoxCell)
        Try
            Combo.Items.Clear()
            Combo.Items.Add("Router")
            'Combo.Items.Add("Switch")
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub SetPSTypeCombo(ByVal cell As DataGridViewComboBoxCell)
        Try
            cell.Items.Clear()
            cell.Items.Add("COM_Port_PS")
            cell.Items.Add("Agilent_PS")
            cell.Items.Add("XDC_PS")
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Function SetChamberPatternCombo(ByVal Combo As DataGridViewComboBoxCell) As Boolean
        Try
            Combo.Items.Clear()
            For i As Integer = 1 To 4
                Combo.Items.Add(i.ToString)
            Next
            Return True
        Catch ex As Exception
            AddMessage(ex.Message)
            Return False
        End Try
    End Function

    Private Sub SetSwitchCombo(ByVal cell As DataGridViewComboBoxCell)
        Try
            cell.Items.Clear()
            'cell.Items.Add("SWITCH_USBIO_24")
            'cell.Items.Add("SWITCH_RS232")
            'cell.Items.Add("SWITCH_RS422")
            cell.Items.Add("Switch_9CH_Eth")
            cell.Items.Add("Agilent_PSU")
            'cell.Items.Add("UUT")
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub SetFanSwitchCombo(ByVal cell As DataGridViewComboBoxCell)
        Try
            cell.Items.Clear()
            cell.Items.Add("Switch_9CH_Eth")
            cell.Items.Add("UUT")
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub SetAntonSwitchCombo(ByVal cell As DataGridViewComboBoxCell)
        Try
            cell.Items.Clear()
            cell.Items.Add("Switch_9CH_Eth")
            cell.Items.Add("USB")
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub SetCOMPortCombo(ByVal cell As DataGridViewComboBoxCell)
        Try
            cell.Items.Clear()
            For i As Integer = 1 To 30
                cell.Items.Add(i.ToString)
            Next
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub SetEthRelaySlotCombo(ByVal cell As DataGridViewComboBoxCell)
        Try
            cell.Items.Clear()
            For i As Integer = 0 To 1
                cell.Items.Add(i.ToString)
            Next
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub


    Private Sub IniInformation()
        Me.labSWTitileInd.Text = SW_Title
        Me.labSWVerInd.Text = SW_Version
        Me.labMTRVerInd.Text = MTR_Version
        Me.labProdTypeInd.Text = Assembly_Type
        Me.labSiteInd.Text = Test_Site
        Me.labTestGroupInd.Text = Test_Group
    End Sub

    Private Sub IniParam()
        IniInformation()
        IniHWParam()
        IniSWParam()
    End Sub

    Private Sub UpdateParam()
        UpdateHWParam()
        UpdateSWParam()
    End Sub

    Private Sub SetDGVRowStyle(ByVal dgv As System.Windows.Forms.DataGridView, ByVal DisplayString As String)
        Dim rowNum As Integer
        dgv.Rows.Add()
        rowNum = dgv.RowCount - 1

        dgv.Rows(rowNum).Cells(1).Value = DisplayString
        For i As Integer = 0 To 2
            dgv.Rows(rowNum).Cells(i).Style.BackColor = Color.Blue
            dgv.Rows(rowNum).Cells(i).Style.SelectionBackColor = Color.Blue
            dgv.Rows(rowNum).Cells(i).Style.ForeColor = Color.White
            dgv.Rows(rowNum).Cells(i).Style.SelectionForeColor = Color.White
            dgv.Rows(rowNum).Cells(i).Style.Font = New Font("Arial", 10, FontStyle.Bold)
            dgv.Rows(rowNum).Cells(i).ReadOnly = True
        Next
    End Sub

    Private Sub SetReadOnlyRow(ByVal dgv As System.Windows.Forms.DataGridView, ByVal string1 As String, ByVal string2 As String, ByVal string3 As String)
        dgv.Rows.Add(New String(2) {string1, string2, string3})
        Dim tmpRowNum As Integer = dgv.Rows.Count - 1
        dgv.Rows(tmpRowNum).ReadOnly = True
        For i As Integer = 0 To 2
            dgv.Rows(tmpRowNum).Cells(i).Style.BackColor = Color.Violet
            dgv.Rows(tmpRowNum).Cells(i).Style.ForeColor = Color.Black
            dgv.Rows(tmpRowNum).Cells(i).Style.SelectionBackColor = Color.Violet
            dgv.Rows(tmpRowNum).Cells(i).Style.SelectionForeColor = Color.Black
        Next
    End Sub

    Private Sub IniHWParam()
        Try
            Me.dgvHW.Rows.Clear()
            Dim tmpRowCount As Integer = 0

            'Set Unit Connection
            SetDGVRowStyle(Me.dgvHW, "Unit Connection Settings")
            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            Dim cellCombo As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            SetConnectionTypeCombo(cellCombo)
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Communication_Type"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.ConnectionType
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "String"
            For slot As Integer = 0 To SlotNum
                Me.dgvHW.Rows.Add(New String(2) {"Address_" & slot + 1, BI_HW.IPAddress(slot), "String"})
            Next

            Me.dgvHW.Rows.Add()

            ' ''Set Net Switch
            ''For Power Switch control function of Net-Switch 
            'SetDGVRowStyle(Me.dgvHW, "Net Switch Settings")

            'Me.dgvHW.Rows.Add()
            'tmpRowCount = Me.dgvHW.Rows.Count - 1
            'Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            'Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Switch_PS_Enabled"
            'Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Switch_PS_Enabled
            'Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Boolean"

            'Me.dgvHW.Rows.Add()
            'tmpRowCount = Me.dgvHW.Rows.Count - 1
            'Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            'Dim cellSwitchPS As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            'SetSwitchCombo(cellSwitchPS)
            'Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Switch_PS_Type"
            'Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Switch_PS_Type
            'Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "String"

            'Me.dgvHW.Rows.Add()
            'tmpRowCount = Me.dgvHW.Rows.Count - 1
            'Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            'Dim cellSwitchPSComPort As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            'SetCOMPortCombo(cellSwitchPSComPort)
            'Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Switch_PS_COM_Port"
            'Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Switch_PS_COM_Port.ToString
            'Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Integer"

            'Me.dgvHW.Rows.Add(New String(2) {"Switch_PS_Channel", BI_HW.Switch_PS_Channel, "Integer"})

            'Me.dgvHW.Rows.Add()

            'Set Power Switch
            SetDGVRowStyle(Me.dgvHW, "Power Switch Settings")
            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Power_Switch_Enabled"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Pwr_Chan_Enabled
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Boolean"

            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            Dim cellPowerSwitch As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            SetSwitchCombo(cellPowerSwitch)
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Power_Switch_Type"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Pwr_Switch_Type
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "String"

            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            Dim cellPowerSwitchComPort As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            'SetCOMPortCombo(cellPowerSwitchComPort)
            SetEthRelaySlotCombo(cellPowerSwitchComPort)
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Power_Switch_Relay_Port"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Pwr_Port_Number.ToString
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Integer"

            'For slot As Integer = 0 To SlotNum
            '    Me.dgvHW.Rows.Add(New String(2) {"Power_Channel_" & slot + 1, BI_HW.Pwr_Chan(slot), "Integer"})
            'Next
            For slot As Integer = 0 To SlotNum
                Me.dgvHW.Rows.Add(New String(2) {"Power_Channel_" & slot + 1, BI_HW.Pwr_Chan(slot), "String"})
            Next

            Me.dgvHW.Rows.Add()

            'Set Fan Switch
            SetDGVRowStyle(Me.dgvHW, "Fan Switch Settings")

            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Fan_Switch_Enabled"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Fan_Chan_Enabled
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Boolean"

            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            Dim cellFanSwitch As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            SetFanSwitchCombo(cellFanSwitch)
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Fan_Switch_Type"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Fan_Switch_Type
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "String"


            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            Dim cellFanSwitchComPort As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            SetCOMPortCombo(cellFanSwitchComPort)
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Fan_Switch_Relay_Port"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Fan_Port_Number.ToString
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Integer"

            For slot As Integer = 0 To SlotNum
                Me.dgvHW.Rows.Add(New String(2) {"Fan_Channel_" & slot + 1, BI_HW.Fan_Chan(slot), "Integer"})
            Next

            Me.dgvHW.Rows.Add()

            'Set Kanban System data Upload
            SetDGVRowStyle(Me.dgvHW, "Kanban System Control")
            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Kanban_System_Upload"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Kanban_System_Upload
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Boolean"

            Me.dgvHW.Rows.Add()

            'Set Manual Power Control
            SetDGVRowStyle(Me.dgvHW, "Manual Power Supplier Control")
            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "PS_Manual_Control"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.PS_Manual_Control
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Boolean"

            Me.dgvHW.Rows.Add()

            'Set Anton Lamp Switch
            SetDGVRowStyle(Me.dgvHW, "Anton Switch Settings")
            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Anton_Switch_Enabled"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Anton_Chan_Enabled
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Boolean"

            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            Dim cellAntonSwitch As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            SetAntonSwitchCombo(cellAntonSwitch)
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Anton_Switch_Type"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Anton_Switch_Type
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "String"

            Me.dgvHW.Rows.Add()
            tmpRowCount = Me.dgvHW.Rows.Count - 1
            Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            Dim cellAntonSwitchComPort As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            SetEthRelaySlotCombo(cellAntonSwitchComPort)
            Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Anton_Switch_Relay_Port"
            Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Anton_Port_Number.ToString
            Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Integer"

            For slot As Integer = 0 To SlotNum
                Me.dgvHW.Rows.Add(New String(2) {"Anton_Channel_" & slot + 1, BI_HW.Anton_Chan(slot), "String"})
            Next

            Me.dgvHW.Rows.Add()

            ''Set Power Supplier
            'SetDGVRowStyle(Me.dgvHW, "Power Supplier Settings")
            'For PSCount As Integer = 0 To 5
            '    Me.dgvHW.Rows.Add()
            '    tmpRowCount = Me.dgvHW.Rows.Count - 1
            '    Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            '    Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "PS_" & PSCount + 1 & "_Control_Enabled"
            '    Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.PS_Control_Enabled(PSCount)
            '    Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Boolean"
            '    'Me.dgvHW.Rows(tmpRowCount).

            '    Me.dgvHW.Rows.Add()
            '    tmpRowCount = Me.dgvHW.Rows.Count - 1
            '    Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            '    Dim cellPS As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            '    SetPSTypeCombo(cellPS)
            '    Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "PS_" & PSCount + 1 & "_Control_Type"
            '    Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.PS_Control_Type(PSCount)
            '    Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "String"

            '    Me.dgvHW.Rows.Add(New String(2) {"PS_" & PSCount + 1 & "_Control_Address", BI_HW.PS_Control_Address(PSCount), "String"})
            '    Me.dgvHW.Rows.Add(New String(2) {"PS_" & PSCount + 1 & "_Voltage", BI_HW.PS_Voltage(PSCount), "Integer"})
            '    Me.dgvHW.Rows.Add(New String(2) {"PS_" & PSCount + 1 & "_Current", BI_HW.PS_Current(PSCount), "Integer"})
            'Next

            'Me.dgvHW.Rows.Add()

            ''Set Chamber
            'SetDGVRowStyle(Me.dgvHW, "Chamber Settings")
            'Me.dgvHW.Rows.Add()
            'tmpRowCount = Me.dgvHW.Rows.Count - 1
            'Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            'Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Chamber_Enabled"
            'Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Chamber_Control_Enabled
            'Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Boolean"

            'Me.dgvHW.Rows.Add()
            'tmpRowCount = Me.dgvHW.Rows.Count - 1
            'Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            'Dim cellCCOMCombo As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            'SetCOMPortCombo(cellCCOMCombo)
            'Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Chamber_COM_Port"
            'Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Chamber_Port_Number.ToString
            'Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Integer"


            'Me.dgvHW.Rows.Add()
            'tmpRowCount = Me.dgvHW.Rows.Count - 1
            'Me.dgvHW.Rows(tmpRowCount).Cells(1) = New DataGridViewComboBoxCell
            'Dim cellChamberPattern As DataGridViewComboBoxCell = Me.dgvHW.Rows(tmpRowCount).Cells(1)
            'SetChamberPatternCombo(cellChamberPattern)
            'Me.dgvHW.Rows(tmpRowCount).Cells(0).Value = "Chamber_Run_Pattern"
            'Me.dgvHW.Rows(tmpRowCount).Cells(1).Value = BI_HW.Chamber_Run_Pattern.ToString
            'Me.dgvHW.Rows(tmpRowCount).Cells(2).Value = "Integer"

            'For i As Integer = 0 To 9
            '    Me.dgvHW.Rows.Add(New String(2) {"Step" & (i + 1).ToString & "_" & "Temp", BI_HW.Chamber_Step(i).Temp, "Degree C"})
            '    Me.dgvHW.Rows.Add(New String(2) {"Step" & (i + 1).ToString & "_" & "Time", BI_HW.Chamber_Step(i).Time, "HH:mm"})
            '    Me.dgvHW.Rows.Add(New String(2) {"Step" & (i + 1).ToString & "_" & "PID", BI_HW.Chamber_Step(i).PID, "Integer"})
            'Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub UpdateHWParam()
        Try
            With Me.dgvHW
                For RowCount As Integer = 0 To .Rows.Count - 1
                    Select Case .Rows(RowCount).Cells(0).Value
                        Case "Communication_Type"
                            BI_HW.ConnectionType = .Rows(RowCount).Cells(1).Value
                        Case "Address_1"
                            BI_HW.IPAddress(0) = .Rows(RowCount).Cells(1).Value
                        Case "Address_2"
                            BI_HW.IPAddress(1) = .Rows(RowCount).Cells(1).Value
                        Case "Address_3"
                            BI_HW.IPAddress(2) = .Rows(RowCount).Cells(1).Value
                        Case "Address_4"
                            BI_HW.IPAddress(3) = .Rows(RowCount).Cells(1).Value
                        Case "Address_5"
                            BI_HW.IPAddress(4) = .Rows(RowCount).Cells(1).Value
                        Case "Address_6"
                            BI_HW.IPAddress(5) = .Rows(RowCount).Cells(1).Value
                        Case "Address_7"
                            BI_HW.IPAddress(6) = .Rows(RowCount).Cells(1).Value
                        Case "Address_8"
                            BI_HW.IPAddress(7) = .Rows(RowCount).Cells(1).Value
                        Case "Address_9"
                            BI_HW.IPAddress(8) = .Rows(RowCount).Cells(1).Value
                        Case "Address_10"
                            BI_HW.IPAddress(9) = .Rows(RowCount).Cells(1).Value
                        Case "Address_11"
                            BI_HW.IPAddress(10) = .Rows(RowCount).Cells(1).Value
                        Case "Address_12"
                            BI_HW.IPAddress(11) = .Rows(RowCount).Cells(1).Value
                        Case "Address_13"
                            BI_HW.IPAddress(12) = .Rows(RowCount).Cells(1).Value
                        Case "Address_14"
                            BI_HW.IPAddress(13) = .Rows(RowCount).Cells(1).Value
                        Case "Address_15"
                            BI_HW.IPAddress(14) = .Rows(RowCount).Cells(1).Value

                            'Case "Switch_PS_Enabled"
                            '    BI_HW.Switch_PS_Enabled = .Rows(RowCount).Cells(1).Value
                            'Case "Switch_PS_Type"
                            '    BI_HW.Switch_PS_Type = .Rows(RowCount).Cells(1).Value
                            'Case "Switch_PS_COM_Port"
                            '    BI_HW.Switch_PS_COM_Port = .Rows(RowCount).Cells(1).Value
                            'Case "Switch_PS_Channel"
                            '    BI_HW.Switch_PS_Channel = .Rows(RowCount).Cells(1).Value

                        Case "Fan_Switch_Enabled"
                            BI_HW.Fan_Chan_Enabled = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Switch_Type"
                            BI_HW.Fan_Switch_Type = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Switch_Relay_Port"
                            BI_HW.Fan_Port_Number = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_1"
                            BI_HW.Fan_Chan(0) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_2"
                            BI_HW.Fan_Chan(1) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_3"
                            BI_HW.Fan_Chan(2) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_4"
                            BI_HW.Fan_Chan(3) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_5"
                            BI_HW.Fan_Chan(4) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_6"
                            BI_HW.Fan_Chan(5) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_7"
                            BI_HW.Fan_Chan(6) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_8"
                            BI_HW.Fan_Chan(7) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_9"
                            BI_HW.Fan_Chan(8) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_10"
                            BI_HW.Fan_Chan(9) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_11"
                            BI_HW.Fan_Chan(10) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_12"
                            BI_HW.Fan_Chan(11) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_13"
                            BI_HW.Fan_Chan(12) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_14"
                            BI_HW.Fan_Chan(13) = .Rows(RowCount).Cells(1).Value
                        Case "Fan_Channel_15"
                            BI_HW.Fan_Chan(14) = .Rows(RowCount).Cells(1).Value

                        Case "Power_Switch_Enabled"
                            BI_HW.Pwr_Chan_Enabled = .Rows(RowCount).Cells(1).Value
                        Case "Power_Switch_Type"
                            BI_HW.Pwr_Switch_Type = .Rows(RowCount).Cells(1).Value
                        Case "Power_Switch_Relay_Port"
                            BI_HW.Pwr_Port_Number = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_1"
                            BI_HW.Pwr_Chan(0) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_2"
                            BI_HW.Pwr_Chan(1) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_3"
                            BI_HW.Pwr_Chan(2) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_4"
                            BI_HW.Pwr_Chan(3) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_5"
                            BI_HW.Pwr_Chan(4) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_6"
                            BI_HW.Pwr_Chan(5) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_7"
                            BI_HW.Pwr_Chan(6) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_8"
                            BI_HW.Pwr_Chan(7) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_9"
                            BI_HW.Pwr_Chan(8) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_10"
                            BI_HW.Pwr_Chan(9) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_11"
                            BI_HW.Pwr_Chan(10) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_12"
                            BI_HW.Pwr_Chan(11) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_13"
                            BI_HW.Pwr_Chan(12) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_14"
                            BI_HW.Pwr_Chan(13) = .Rows(RowCount).Cells(1).Value
                        Case "Power_Channel_15"
                            BI_HW.Pwr_Chan(14) = .Rows(RowCount).Cells(1).Value

                        Case "Anton_Switch_Enabled"
                            BI_HW.Anton_Chan_Enabled = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Switch_Type"
                            BI_HW.Anton_Switch_Type = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Switch_Relay_Port"
                            BI_HW.Anton_Port_Number = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_1"
                            BI_HW.Anton_Chan(0) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_2"
                            BI_HW.Anton_Chan(1) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_3"
                            BI_HW.Anton_Chan(2) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_4"
                            BI_HW.Anton_Chan(3) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_5"
                            BI_HW.Anton_Chan(4) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_6"
                            BI_HW.Anton_Chan(5) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_7"
                            BI_HW.Anton_Chan(6) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_8"
                            BI_HW.Anton_Chan(7) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_9"
                            BI_HW.Anton_Chan(8) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_10"
                            BI_HW.Anton_Chan(9) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_11"
                            BI_HW.Anton_Chan(10) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_12"
                            BI_HW.Anton_Chan(11) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_13"
                            BI_HW.Anton_Chan(12) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_14"
                            BI_HW.Anton_Chan(13) = .Rows(RowCount).Cells(1).Value
                        Case "Anton_Channel_15"
                            BI_HW.Anton_Chan(14) = .Rows(RowCount).Cells(1).Value

                        Case "Kanban_System_Upload"
                            BI_HW.Kanban_System_Upload = .Rows(RowCount).Cells(1).Value

                        Case "PS_Manual_Control"
                            BI_HW.PS_Manual_Control = .Rows(RowCount).Cells(1).Value

                            'Case "PS_1_Control_Enabled"
                            '    BI_HW.PS_Control_Enabled(0) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_2_Control_Enabled"
                            '    BI_HW.PS_Control_Enabled(1) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_3_Control_Enabled"
                            '    BI_HW.PS_Control_Enabled(2) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_4_Control_Enabled"
                            '    BI_HW.PS_Control_Enabled(3) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_5_Control_Enabled"
                            '    BI_HW.PS_Control_Enabled(4) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_6_Control_Enabled"
                            '    BI_HW.PS_Control_Enabled(5) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_1_Control_Type"
                            '    BI_HW.PS_Control_Type(0) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_2_Control_Type"
                            '    BI_HW.PS_Control_Type(1) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_3_Control_Type"
                            '    BI_HW.PS_Control_Type(2) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_4_Control_Type"
                            '    BI_HW.PS_Control_Type(3) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_5_Control_Type"
                            '    BI_HW.PS_Control_Type(4) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_6_Control_Type"
                            '    BI_HW.PS_Control_Type(5) = .Rows(RowCount).Cells(1).Value

                            'Case "PS_1_Control_Address"
                            '    BI_HW.PS_Control_Address(0) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_2_Control_Address"
                            '    BI_HW.PS_Control_Address(1) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_3_Control_Address"
                            '    BI_HW.PS_Control_Address(2) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_4_Control_Address"
                            '    BI_HW.PS_Control_Address(3) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_5_Control_Address"
                            '    BI_HW.PS_Control_Address(4) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_6_Control_Address"
                            '    BI_HW.PS_Control_Address(5) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_1_Voltage"
                            '    BI_HW.PS_Voltage(0) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_2_Voltage"
                            '    BI_HW.PS_Voltage(1) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_3_Voltage"
                            '    BI_HW.PS_Voltage(2) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_4_Voltage"
                            '    BI_HW.PS_Voltage(3) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_5_Voltage"
                            '    BI_HW.PS_Voltage(4) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_6_Voltage"
                            '    BI_HW.PS_Voltage(5) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_1_Current"
                            '    BI_HW.PS_Current(0) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_2_Current"
                            '    BI_HW.PS_Current(1) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_3_Current"
                            '    BI_HW.PS_Current(2) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_4_Current"
                            '    BI_HW.PS_Current(3) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_5_Current"
                            '    BI_HW.PS_Current(4) = .Rows(RowCount).Cells(1).Value
                            'Case "PS_6_Current"
                            '    BI_HW.PS_Current(5) = .Rows(RowCount).Cells(1).Value

                            'Case "Chamber_Enabled"
                            '    BI_HW.Chamber_Control_Enabled = .Rows(RowCount).Cells(1).Value
                            'Case "Chamber_COM_Port"
                            '    BI_HW.Chamber_Port_Number = .Rows(RowCount).Cells(1).Value
                            'Case "Chamber_Run_Pattern"
                            '    BI_HW.Chamber_Run_Pattern = .Rows(RowCount).Cells(1).Value
                            'Case "Step1_Temp"
                            '    BI_HW.Chamber_Step(0).Temp = .Rows(RowCount).Cells(1).Value
                            'Case "Step2_Temp"
                            '    BI_HW.Chamber_Step(1).Temp = .Rows(RowCount).Cells(1).Value
                            'Case "Step3_Temp"
                            '    BI_HW.Chamber_Step(2).Temp = .Rows(RowCount).Cells(1).Value
                            'Case "Step4_Temp"
                            '    BI_HW.Chamber_Step(3).Temp = .Rows(RowCount).Cells(1).Value
                            'Case "Step5_Temp"
                            '    BI_HW.Chamber_Step(4).Temp = .Rows(RowCount).Cells(1).Value
                            'Case "Step6_Temp"
                            '    BI_HW.Chamber_Step(5).Temp = .Rows(RowCount).Cells(1).Value
                            'Case "Step7_Temp"
                            '    BI_HW.Chamber_Step(6).Temp = .Rows(RowCount).Cells(1).Value
                            'Case "Step8_Temp"
                            '    BI_HW.Chamber_Step(7).Temp = .Rows(RowCount).Cells(1).Value
                            'Case "Step9_Temp"
                            '    BI_HW.Chamber_Step(8).Temp = .Rows(RowCount).Cells(1).Value
                            'Case "Step10_Temp"
                            '    BI_HW.Chamber_Step(9).Temp = .Rows(RowCount).Cells(1).Value
                            'Case "Step1_Time"
                            '    BI_HW.Chamber_Step(0).Time = .Rows(RowCount).Cells(1).Value
                            'Case "Step2_Time"
                            '    BI_HW.Chamber_Step(1).Time = .Rows(RowCount).Cells(1).Value
                            'Case "Step3_Time"
                            '    BI_HW.Chamber_Step(2).Time = .Rows(RowCount).Cells(1).Value
                            'Case "Step4_Time"
                            '    BI_HW.Chamber_Step(3).Time = .Rows(RowCount).Cells(1).Value
                            'Case "Step5_Time"
                            '    BI_HW.Chamber_Step(4).Time = .Rows(RowCount).Cells(1).Value
                            'Case "Step6_Time"
                            '    BI_HW.Chamber_Step(5).Time = .Rows(RowCount).Cells(1).Value
                            'Case "Step7_Time"
                            '    BI_HW.Chamber_Step(6).Time = .Rows(RowCount).Cells(1).Value
                            'Case "Step8_Time"
                            '    BI_HW.Chamber_Step(7).Time = .Rows(RowCount).Cells(1).Value
                            'Case "Step9_Time"
                            '    BI_HW.Chamber_Step(8).Time = .Rows(RowCount).Cells(1).Value
                            'Case "Step10_Time"
                            '    BI_HW.Chamber_Step(9).Time = .Rows(RowCount).Cells(1).Value
                            'Case "Step1_PID"
                            '    BI_HW.Chamber_Step(0).PID = .Rows(RowCount).Cells(1).Value
                            'Case "Step2_PID"
                            '    BI_HW.Chamber_Step(1).PID = .Rows(RowCount).Cells(1).Value
                            'Case "Step3_PID"
                            '    BI_HW.Chamber_Step(2).PID = .Rows(RowCount).Cells(1).Value
                            'Case "Step4_PID"
                            '    BI_HW.Chamber_Step(3).PID = .Rows(RowCount).Cells(1).Value
                            'Case "Step5_PID"
                            '    BI_HW.Chamber_Step(4).PID = .Rows(RowCount).Cells(1).Value
                            'Case "Step6_PID"
                            '    BI_HW.Chamber_Step(5).PID = .Rows(RowCount).Cells(1).Value
                            'Case "Step7_PID"
                            '    BI_HW.Chamber_Step(6).PID = .Rows(RowCount).Cells(1).Value
                            'Case "Step8_PID"
                            '    BI_HW.Chamber_Step(7).PID = .Rows(RowCount).Cells(1).Value
                            'Case "Step9_PID"
                            '    BI_HW.Chamber_Step(8).PID = .Rows(RowCount).Cells(1).Value
                            'Case "Step10_PID"
                            '    BI_HW.Chamber_Step(9).PID = .Rows(RowCount).Cells(1).Value
                    End Select
                Next
            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub IniSWParam()
        Try
            Me.dgvSW.Rows.Clear()
            Dim tmpRowCount As Integer = 0

            SetDGVRowStyle(Me.dgvSW, "Test Settings")
            Me.dgvSW.Rows.Add()
            tmpRowCount = Me.dgvSW.Rows.Count - 1
            Me.dgvSW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            Me.dgvSW.Rows(tmpRowCount).Cells(0).Value = "Performance_ODC_Check"
            Me.dgvSW.Rows(tmpRowCount).Cells(1).Value = BI_Param.ODC_Check
            Me.dgvSW.Rows(tmpRowCount).Cells(2).Value = "Boolean"

            Me.dgvSW.Rows.Add()
            tmpRowCount = Me.dgvSW.Rows.Count - 1
            Me.dgvSW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            Me.dgvSW.Rows(tmpRowCount).Cells(0).Value = "Transfer_Data"
            Me.dgvSW.Rows(tmpRowCount).Cells(1).Value = BI_Param.Transfer_Data
            Me.dgvSW.Rows(tmpRowCount).Cells(2).Value = "Boolean"

            'Me.dgvSW.Rows.Add()
            'tmpRowCount = Me.dgvSW.Rows.Count - 1
            'Me.dgvSW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            'Me.dgvSW.Rows(tmpRowCount).Cells(0).Value = "Matlab_Check_Enabled"
            'Me.dgvSW.Rows(tmpRowCount).Cells(1).Value = BI_Param.Matlab_Check_Enabled
            'Me.dgvSW.Rows(tmpRowCount).Cells(2).Value = "Boolean"

            'Me.dgvSW.Rows.Add()
            'tmpRowCount = Me.dgvSW.Rows.Count - 1
            'Me.dgvSW.Rows(tmpRowCount).Cells(1) = New DataGridViewCheckBoxCell
            'Me.dgvSW.Rows(tmpRowCount).Cells(0).Value = "Matlab_Picture_Enabled"
            'Me.dgvSW.Rows(tmpRowCount).Cells(1).Value = BI_Param.Matlab_Picture_Enabled
            'Me.dgvSW.Rows(tmpRowCount).Cells(2).Value = "Boolean"


            'Me.dgvSW.Rows.Add(New String(2) {"File_Save_Path", BI_Param.File_Save_Path, "String"})
            ''Me.dgvSW.Rows.Add(New String(2) {"Matlab_Program_Path", BI_Param.Matlab_Program_Path, "String"})
            'Me.dgvSW.Rows.Add(New String(2) {"Matlab_Picture_Comments", BI_Param.Matlab_Picture_Comments, "String"})
            'Me.dgvSW.Rows.Add(New String(2) {"Matlab_Picture_Save_Path", BI_Param.Matlab_Picture_Save_Path, "String"})


            Me.dgvSW.Rows.Add()
            SetDGVRowStyle(Me.dgvSW, "Test Profile")
            SetReadOnlyRow(Me.dgvSW, "BI_Start_Temp", BI_Profile.BI_Start_Temp, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Duration", BI_Profile.BI_Duration, "Minutes")
            SetReadOnlyRow(Me.dgvSW, "BI_Cycle", BI_Profile.BI_Cycle, "Counts")
            SetReadOnlyRow(Me.dgvSW, "BI_Polling_Interval", BI_Profile.BI_Polling_Interval, "Seconds")
            SetReadOnlyRow(Me.dgvSW, "BI_Power_Cycle", BI_Profile.BI_Power_Cycle, "Counts")
            SetReadOnlyRow(Me.dgvSW, "BI_Power_Cycle_Interval", BI_Profile.BI_Power_Cycle_Interval, "Seconds")
            SetReadOnlyRow(Me.dgvSW, "BI_Target_Temp", BI_Profile.BI_Target_Temp, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Heating_Up_Time", BI_Profile.BI_Heating_Up_Time, "Minutes")
            SetReadOnlyRow(Me.dgvSW, "BI_Soak_Time", BI_Profile.BI_Soak_Time, "Minutes")
            SetReadOnlyRow(Me.dgvSW, "BI_Cooling_Down_Time", BI_Profile.BI_Cooling_Down_Time, "Minutes")

            Me.dgvSW.Rows.Add()
            SetDGVRowStyle(Me.dgvSW, "Test Limits")
            'Common Items
            SetReadOnlyRow(Me.dgvSW, "Thres_Hold_Time", BI_Limit.Thres_Hold_Time, "Minutes")
            SetReadOnlyRow(Me.dgvSW, "Thres_Hold_Temp", BI_Limit.Thres_Hold_Temp, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "RF_On_Time", BI_Limit.RF_On_Time, "Minutes")

            SetReadOnlyRow(Me.dgvSW, "BI_Pre_VSWR", BI_Limit.BI_Pre_VSWR, "dB")

            'General Information
            SetReadOnlyRow(Me.dgvSW, "BI_High_PA_Temp_UL", BI_Limit.BI_High_PA_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_PA_Temp_LL", BI_Limit.BI_High_PA_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_PA_Temp_UL", BI_Limit.BI_Low_PA_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_PA_Temp_LL", BI_Limit.BI_Low_PA_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_PA_VSWR_Temp_UL", BI_Limit.BI_High_PA_VSWR_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_PA_VSWR_Temp_LL", BI_Limit.BI_High_PA_VSWR_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_PA_VSWR_Temp_UL", BI_Limit.BI_Low_PA_VSWR_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_PA_VSWR_Temp_LL", BI_Limit.BI_Low_PA_VSWR_Temp_LL, "Degree C")

            SetReadOnlyRow(Me.dgvSW, "BI_PA_Temp_Delta_UL", BI_Limit.BI_PA_Temp_Delta_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_PA_Temp_Delta_LL", BI_Limit.BI_PA_Temp_Delta_LL, "Degree C")

            SetReadOnlyRow(Me.dgvSW, "BI_High_LNA01_Temp_UL", BI_Limit.BI_High_LNA01_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_LNA01_Temp_LL", BI_Limit.BI_High_LNA01_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_LNA01_Temp_UL", BI_Limit.BI_Low_LNA01_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_LNA01_Temp_LL", BI_Limit.BI_Low_LNA01_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_LNA23_Temp_UL", BI_Limit.BI_High_LNA23_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_LNA23_Temp_LL", BI_Limit.BI_High_LNA23_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_LNA23_Temp_UL", BI_Limit.BI_Low_LNA23_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_LNA23_Temp_LL", BI_Limit.BI_Low_LNA23_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_PS_Converter_Temp_UL", BI_Limit.BI_High_PS_Converter_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_PS_Converter_Temp_LL", BI_Limit.BI_High_PS_Converter_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_PS_Converter_Temp_UL", BI_Limit.BI_Low_PS_Converter_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_PS_Converter_Temp_LL", BI_Limit.BI_Low_PS_Converter_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_PS_Brick_Temp_UL", BI_Limit.BI_High_PS_Brick_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_PS_Brick_Temp_LL", BI_Limit.BI_High_PS_Brick_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_PS_Brick_Temp_UL", BI_Limit.BI_Low_PS_Brick_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_PS_Brick_Temp_LL", BI_Limit.BI_Low_PS_Brick_Temp_LL, "Degree C")

            SetReadOnlyRow(Me.dgvSW, "BI_PSU_Temp_Delta_UL", BI_Limit.BI_PSU_Temp_Delta_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_PSU_Temp_Delta_LL", BI_Limit.BI_PSU_Temp_Delta_LL, "Degree C")
            'SetReadOnlyRow(Me.dgvSW, "BI_PSU_PA_Temp_Delta_UL", BI_Limit.BI_PSU_PA_Temp_Delta_UL, "Degree C")
            'SetReadOnlyRow(Me.dgvSW, "BI_PSU_PA_Temp_Delta_LL", BI_Limit.BI_PSU_PA_Temp_Delta_LL, "Degree C")

            SetReadOnlyRow(Me.dgvSW, "BI_High_PSU_PA_Temp_Delta_UL", BI_Limit.BI_High_PSU_PA_Temp_Delta_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_PSU_PA_Temp_Delta_LL", BI_Limit.BI_High_PSU_PA_Temp_Delta_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_PSU_PA_Temp_Delta_UL", BI_Limit.BI_Low_PSU_PA_Temp_Delta_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_PSU_PA_Temp_Delta_LL", BI_Limit.BI_Low_PSU_PA_Temp_Delta_LL, "Degree C")

            SetReadOnlyRow(Me.dgvSW, "BI_High_FB_Temp_UL", BI_Limit.BI_High_FB_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_FB_Temp_LL", BI_Limit.BI_High_FB_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_FB_Temp_UL", BI_Limit.BI_Low_FB_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_FB_Temp_LL", BI_Limit.BI_Low_FB_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_RX_Temp_UL", BI_Limit.BI_High_RX_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_RX_Temp_LL", BI_Limit.BI_High_RX_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_RX_Temp_UL", BI_Limit.BI_Low_RX_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_RX_Temp_LL", BI_Limit.BI_Low_RX_Temp_LL, "Degree C")

            'TX_High, UL
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Output_Pow_UL", BI_Limit.BI_High_TX_Output_Pow_UL, "dBm")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_VSWR_UL", BI_Limit.BI_High_TX_VSWR_UL, "dB")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Forward_Power_Detector_UL", BI_Limit.BI_High_TX_Forward_Power_Detector_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Reverse_Power_Detector_UL", BI_Limit.BI_High_TX_Reverse_Power_Detector_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Gain_VCA_UL", BI_Limit.BI_High_TX_Gain_VCA_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_txDacGain_UL", BI_Limit.BI_High_TX_txDacGain_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_totalTxAttn_UL", BI_Limit.BI_High_TX_totalTxAttn_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Gain_TxStep_UL", BI_Limit.BI_High_TX_Gain_TxStep_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Gain_FbStep_UL", BI_Limit.BI_High_TX_Gain_FbStep_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Gain_FbTxQuo_UL", BI_Limit.BI_High_TX_Gain_FbTxQuo_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Gain_GainError_UL", BI_Limit.BI_High_TX_Gain_GainError_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_PsVolt_UL", BI_Limit.BI_High_TX_PA_PsVolt_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Temp_UL", BI_Limit.BI_High_TX_PA_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_BiasTemp_UL", BI_Limit.BI_High_TX_PA_BiasTemp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver1Cur_UL", BI_Limit.BI_High_TX_PA_Driver1Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver2Cur_UL", BI_Limit.BI_High_TX_PA_Driver2Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver3Cur_UL", BI_Limit.BI_High_TX_PA_Driver3Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver4Cur_UL", BI_Limit.BI_High_TX_PA_Driver4Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Final1Cur_UL", BI_Limit.BI_High_TX_PA_Final1Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Final2Cur_UL", BI_Limit.BI_High_TX_PA_Final2Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver1Dac_UL", BI_Limit.BI_High_TX_PA_Driver1Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver2Dac_UL", BI_Limit.BI_High_TX_PA_Driver2Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver3Dac_UL", BI_Limit.BI_High_TX_PA_Driver3Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver4Dac_UL", BI_Limit.BI_High_TX_PA_Driver4Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Final1Dac_UL", BI_Limit.BI_High_TX_PA_Final1Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Final2Dac_UL", BI_Limit.BI_High_TX_PA_Final2Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_ampDelayInt_UL", BI_Limit.BI_High_TX_PA_ampDelayInt_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_ampDelayFrac_UL", BI_Limit.BI_High_TX_PA_ampDelayFrac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_MaxCrossCorrelation_UL", BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L1_Table_Max_Gain_UL", BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L1_Table_Min_Gain_UL", BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_3rd_sym_am_UL", BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_3rd_sym_ph_UL", BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L3_3rd_sym_am_UL", BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L3_3rd_sym_ph_UL", BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_5th_sym_am_UL", BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_5th_sym_ph_UL", BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L3_5th_sym_am_UL", BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L3_5th_sym_ph_UL", BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_3rd_asym_am_UL", BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_3rd_asym_ph_UL", BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_5th_asym_am_UL", BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_5th_asym_ph_UL", BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_UL, "Integer")

            'TX_High, LL
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Output_Pow_LL", BI_Limit.BI_High_TX_Output_Pow_LL, "dBm")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_VSWR_LL", BI_Limit.BI_High_TX_VSWR_LL, "dB")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Forward_Power_Detector_LL", BI_Limit.BI_High_TX_Forward_Power_Detector_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Reverse_Power_Detector_LL", BI_Limit.BI_High_TX_Reverse_Power_Detector_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Gain_VCA_LL", BI_Limit.BI_High_TX_Gain_VCA_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_txDacGain_LL", BI_Limit.BI_High_TX_txDacGain_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_totalTxAttn_LL", BI_Limit.BI_High_TX_totalTxAttn_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Gain_TxStep_LL", BI_Limit.BI_High_TX_Gain_TxStep_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Gain_FbStep_LL", BI_Limit.BI_High_TX_Gain_FbStep_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Gain_FbTxQuo_LL", BI_Limit.BI_High_TX_Gain_FbTxQuo_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_Gain_GainError_LL", BI_Limit.BI_High_TX_Gain_GainError_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_PsVolt_LL", BI_Limit.BI_High_TX_PA_PsVolt_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Temp_LL", BI_Limit.BI_High_TX_PA_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_BiasTemp_LL", BI_Limit.BI_High_TX_PA_BiasTemp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver1Cur_LL", BI_Limit.BI_High_TX_PA_Driver1Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver2Cur_LL", BI_Limit.BI_High_TX_PA_Driver2Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver3Cur_LL", BI_Limit.BI_High_TX_PA_Driver3Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver4Cur_LL", BI_Limit.BI_High_TX_PA_Driver4Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Final1Cur_LL", BI_Limit.BI_High_TX_PA_Final1Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Final2Cur_LL", BI_Limit.BI_High_TX_PA_Final2Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver1Dac_LL", BI_Limit.BI_High_TX_PA_Driver1Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver2Dac_LL", BI_Limit.BI_High_TX_PA_Driver2Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver3Dac_LL", BI_Limit.BI_High_TX_PA_Driver3Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Driver4Dac_LL", BI_Limit.BI_High_TX_PA_Driver4Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Final1Dac_LL", BI_Limit.BI_High_TX_PA_Final1Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_Final2Dac_LL", BI_Limit.BI_High_TX_PA_Final2Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_ampDelayInt_LL", BI_Limit.BI_High_TX_PA_ampDelayInt_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_ampDelayFrac_LL", BI_Limit.BI_High_TX_PA_ampDelayFrac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_TX_PA_MaxCrossCorrelation_LL", BI_Limit.BI_High_TX_PA_MaxCrossCorrelation_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L1_Table_Max_Gain_LL", BI_Limit.BI_High_Tx_DPD_L1_Table_Max_Gain_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L1_Table_Min_Gain_LL", BI_Limit.BI_High_Tx_DPD_L1_Table_Min_Gain_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_3rd_sym_am_LL", BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_3rd_sym_ph_LL", BI_Limit.BI_High_Tx_DPD_L2_3rd_sym_ph_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L3_3rd_sym_am_LL", BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L3_3rd_sym_ph_LL", BI_Limit.BI_High_Tx_DPD_L3_3rd_sym_ph_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_5th_sym_am_LL", BI_Limit.BI_High_Tx_DPD_L2_5th_sym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_5th_sym_ph_LL", BI_Limit.BI_High_Tx_DPD_L2_5th_sym_ph_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L3_5th_sym_am_LL", BI_Limit.BI_High_Tx_DPD_L3_5th_sym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L3_5th_sym_ph_LL", BI_Limit.BI_High_Tx_DPD_L3_5th_sym_ph_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_3rd_asym_am_LL", BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_3rd_asym_ph_LL", BI_Limit.BI_High_Tx_DPD_L2_3rd_asym_ph_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_5th_asym_am_LL", BI_Limit.BI_High_Tx_DPD_L2_5th_asym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Tx_DPD_L2_5th_asym_ph_LL", BI_Limit.BI_High_Tx_DPD_L2_5th_asym_ph_LL, "Integer")

            'TX_Low, UL
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Output_Pow_UL", BI_Limit.BI_Low_TX_Output_Pow_UL, "dBm")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_VSWR_UL", BI_Limit.BI_Low_TX_VSWR_UL, "dB")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Forward_Power_Detector_UL", BI_Limit.BI_Low_TX_Forward_Power_Detector_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Reverse_Power_Detector_UL", BI_Limit.BI_Low_TX_Reverse_Power_Detector_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Gain_VCA_UL", BI_Limit.BI_Low_TX_Gain_VCA_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_txDacGain_UL", BI_Limit.BI_Low_TX_txDacGain_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_totalTxAttn_UL", BI_Limit.BI_Low_TX_totalTxAttn_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Gain_TxStep_UL", BI_Limit.BI_Low_TX_Gain_TxStep_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Gain_FbStep_UL", BI_Limit.BI_Low_TX_Gain_FbStep_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Gain_FbTxQuo_UL", BI_Limit.BI_Low_TX_Gain_FbTxQuo_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Gain_GainError_UL", BI_Limit.BI_Low_TX_Gain_GainError_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_PsVolt_UL", BI_Limit.BI_Low_TX_PA_PsVolt_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Temp_UL", BI_Limit.BI_Low_TX_PA_Temp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_BiasTemp_UL", BI_Limit.BI_Low_TX_PA_BiasTemp_UL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver1Cur_UL", BI_Limit.BI_Low_TX_PA_Driver1Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver2Cur_UL", BI_Limit.BI_Low_TX_PA_Driver2Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver3Cur_UL", BI_Limit.BI_Low_TX_PA_Driver3Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver4Cur_UL", BI_Limit.BI_Low_TX_PA_Driver4Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Final1Cur_UL", BI_Limit.BI_Low_TX_PA_Final1Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Final2Cur_UL", BI_Limit.BI_Low_TX_PA_Final2Cur_UL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver1Dac_UL", BI_Limit.BI_Low_TX_PA_Driver1Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver2Dac_UL", BI_Limit.BI_Low_TX_PA_Driver2Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver3Dac_UL", BI_Limit.BI_Low_TX_PA_Driver3Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver4Dac_UL", BI_Limit.BI_Low_TX_PA_Driver4Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Final1Dac_UL", BI_Limit.BI_Low_TX_PA_Final1Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Final2Dac_UL", BI_Limit.BI_Low_TX_PA_Final2Dac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_ampDelayInt_UL", BI_Limit.BI_Low_TX_PA_ampDelayInt_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_ampDelayFrac_UL", BI_Limit.BI_Low_TX_PA_ampDelayFrac_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_MaxCrossCorrelation_UL", BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L1_Table_Max_Gain_UL", BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L1_Table_Min_Gain_UL", BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_3rd_sym_am_UL", BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_3rd_sym_ph_UL", BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L3_3rd_sym_am_UL", BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L3_3rd_sym_ph_UL", BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_5th_sym_am_UL", BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_5th_sym_ph_UL", BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L3_5th_sym_am_UL", BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L3_5th_sym_ph_UL", BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_3rd_asym_am_UL", BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_3rd_asym_ph_UL", BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_5th_asym_am_UL", BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_5th_asym_ph_UL", BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_UL, "Integer")


            'TX_Low, LL
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Output_Pow_LL", BI_Limit.BI_Low_TX_Output_Pow_LL, "dBm")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_VSWR_LL", BI_Limit.BI_Low_TX_VSWR_LL, "dB")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Forward_Power_Detector_LL", BI_Limit.BI_Low_TX_Forward_Power_Detector_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Reverse_Power_Detector_LL", BI_Limit.BI_Low_TX_Reverse_Power_Detector_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Gain_VCA_LL", BI_Limit.BI_Low_TX_Gain_VCA_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_txDacGain_LL", BI_Limit.BI_Low_TX_txDacGain_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_totalTxAttn_LL", BI_Limit.BI_Low_TX_totalTxAttn_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Gain_TxStep_LL", BI_Limit.BI_Low_TX_Gain_TxStep_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Gain_FbStep_LL", BI_Limit.BI_Low_TX_Gain_FbStep_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Gain_FbTxQuo_LL", BI_Limit.BI_Low_TX_Gain_FbTxQuo_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_Gain_GainError_LL", BI_Limit.BI_Low_TX_Gain_GainError_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_PsVolt_LL", BI_Limit.BI_Low_TX_PA_PsVolt_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Temp_LL", BI_Limit.BI_Low_TX_PA_Temp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_BiasTemp_LL", BI_Limit.BI_Low_TX_PA_BiasTemp_LL, "Degree C")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver1Cur_LL", BI_Limit.BI_Low_TX_PA_Driver1Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver2Cur_LL", BI_Limit.BI_Low_TX_PA_Driver2Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver3Cur_LL", BI_Limit.BI_Low_TX_PA_Driver3Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver4Cur_LL", BI_Limit.BI_Low_TX_PA_Driver4Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Final1Cur_LL", BI_Limit.BI_Low_TX_PA_Final1Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Final2Cur_LL", BI_Limit.BI_Low_TX_PA_Final2Cur_LL, "mA")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver1Dac_LL", BI_Limit.BI_Low_TX_PA_Driver1Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver2Dac_LL", BI_Limit.BI_Low_TX_PA_Driver2Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver3Dac_LL", BI_Limit.BI_Low_TX_PA_Driver3Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Driver4Dac_LL", BI_Limit.BI_Low_TX_PA_Driver4Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Final1Dac_LL", BI_Limit.BI_Low_TX_PA_Final1Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_Final2Dac_LL", BI_Limit.BI_Low_TX_PA_Final2Dac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_ampDelayInt_LL", BI_Limit.BI_Low_TX_PA_ampDelayInt_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_ampDelayFrac_LL", BI_Limit.BI_Low_TX_PA_ampDelayFrac_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_TX_PA_MaxCrossCorrelation_LL", BI_Limit.BI_Low_TX_PA_MaxCrossCorrelation_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L1_Table_Max_Gain_LL", BI_Limit.BI_Low_Tx_DPD_L1_Table_Max_Gain_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L1_Table_Min_Gain_LL", BI_Limit.BI_Low_Tx_DPD_L1_Table_Min_Gain_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_3rd_sym_am_LL", BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_3rd_sym_ph_LL", BI_Limit.BI_Low_Tx_DPD_L2_3rd_sym_ph_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L3_3rd_sym_am_LL", BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L3_3rd_sym_ph_LL", BI_Limit.BI_Low_Tx_DPD_L3_3rd_sym_ph_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_5th_sym_am_LL", BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_5th_sym_ph_LL", BI_Limit.BI_Low_Tx_DPD_L2_5th_sym_ph_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L3_5th_sym_am_LL", BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L3_5th_sym_ph_LL", BI_Limit.BI_Low_Tx_DPD_L3_5th_sym_ph_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_3rd_asym_am_LL", BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_3rd_asym_ph_LL", BI_Limit.BI_Low_Tx_DPD_L2_3rd_asym_ph_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_5th_asym_am_LL", BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_am_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Tx_DPD_L2_5th_asym_ph_LL", BI_Limit.BI_Low_Tx_DPD_L2_5th_asym_ph_LL, "Integer")

            'RX_High, UL
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_LNA_Atten_UL", BI_Limit.BI_High_Rx_LNA_Atten_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_Rx_Atten0_UL", BI_Limit.BI_High_Rx_Rx_Atten0_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_Rx_Atten1_UL", BI_Limit.BI_High_Rx_Rx_Atten1_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_RSSI_C0_UL", BI_Limit.BI_High_Rx_RSSI_C0_UL, "dB")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_RSSI_C1_UL", BI_Limit.BI_High_Rx_RSSI_C1_UL, "dB")

            'RX_High, LL
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_LNA_Atten_LL", BI_Limit.BI_High_Rx_LNA_Atten_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_Rx_Atten0_LL", BI_Limit.BI_High_Rx_Rx_Atten0_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_Rx_Atten1_LL", BI_Limit.BI_High_Rx_Rx_Atten1_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_RSSI_C0_LL", BI_Limit.BI_High_Rx_RSSI_C0_LL, "dB")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_RSSI_C1_LL", BI_Limit.BI_High_Rx_RSSI_C1_LL, "dB")

            'RX_Low, UL
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_LNA_Atten_UL", BI_Limit.BI_High_Rx_LNA_Atten_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_Rx_Atten0_UL", BI_Limit.BI_High_Rx_Rx_Atten0_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_Rx_Atten1_UL", BI_Limit.BI_High_Rx_Rx_Atten1_UL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_RSSI_C0_UL", BI_Limit.BI_High_Rx_RSSI_C0_UL, "dB")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_RSSI_C1_UL", BI_Limit.BI_High_Rx_RSSI_C1_UL, "dB")

            'RX_Low, LL
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_LNA_Atten_LL", BI_Limit.BI_High_Rx_LNA_Atten_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_Rx_Atten0_LL", BI_Limit.BI_High_Rx_Rx_Atten0_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_Rx_Atten1_LL", BI_Limit.BI_High_Rx_Rx_Atten1_LL, "Integer")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_RSSI_C0_LL", BI_Limit.BI_High_Rx_RSSI_C0_LL, "dB")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Rx_RSSI_C1_LL", BI_Limit.BI_High_Rx_RSSI_C1_LL, "dB")

            'PS
            SetReadOnlyRow(Me.dgvSW, "BI_High_Input_Voltage", BI_Limit.BI_High_Input_Voltage_UL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Input_Current_HP", BI_Limit.BI_High_Input_Current_UL, "A")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Input_Power_HP", BI_Limit.BI_High_Input_Power_UL, "W")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Output_Voltage", BI_Limit.BI_High_Output_Voltage_UL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_High_AISG_12V_Voltage", BI_Limit.BI_High_AISG_12V_Voltage_UL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_High_AISG_12V_Current", BI_Limit.BI_High_AISG_12V_Current_UL, "A")
            SetReadOnlyRow(Me.dgvSW, "BI_High_AISG_24V_Voltage", BI_Limit.BI_High_AISG_24V_Voltage_UL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_High_AISG_24V_Current", BI_Limit.BI_High_AISG_24V_Current_UL, "A")

            SetReadOnlyRow(Me.dgvSW, "BI_High_Input_Voltage", BI_Limit.BI_High_Input_Voltage_LL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Input_Current_HP", BI_Limit.BI_High_Input_Current_LL, "A")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Input_Power_HP", BI_Limit.BI_High_Input_Power_LL, "W")
            SetReadOnlyRow(Me.dgvSW, "BI_High_Output_Voltage", BI_Limit.BI_High_Output_Voltage_LL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_High_AISG_12V_Voltage", BI_Limit.BI_High_AISG_12V_Voltage_LL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_High_AISG_12V_Current", BI_Limit.BI_High_AISG_12V_Current_LL, "A")
            SetReadOnlyRow(Me.dgvSW, "BI_High_AISG_24V_Voltage", BI_Limit.BI_High_AISG_24V_Voltage_LL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_High_AISG_24V_Current", BI_Limit.BI_High_AISG_24V_Current_LL, "A")

            SetReadOnlyRow(Me.dgvSW, "BI_Low_Input_Voltage", BI_Limit.BI_Low_Input_Voltage_UL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Input_Current_HP", BI_Limit.BI_Low_Input_Current_UL, "A")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Input_Power_HP", BI_Limit.BI_Low_Input_Power_UL, "W")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Output_Voltage", BI_Limit.BI_Low_Output_Voltage_UL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_AISG_12V_Voltage", BI_Limit.BI_Low_AISG_12V_Voltage_UL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_AISG_12V_Current", BI_Limit.BI_Low_AISG_12V_Current_UL, "A")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_AISG_24V_Voltage", BI_Limit.BI_Low_AISG_24V_Voltage_UL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_AISG_24V_Current", BI_Limit.BI_Low_AISG_24V_Current_UL, "A")

            SetReadOnlyRow(Me.dgvSW, "BI_Low_Input_Voltage", BI_Limit.BI_Low_Input_Voltage_LL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Input_Current_HP", BI_Limit.BI_Low_Input_Current_LL, "A")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Input_Power_LP", BI_Limit.BI_Low_Input_Power_LL, "W")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_Output_Voltage", BI_Limit.BI_Low_Output_Voltage_LL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_AISG_12V_Voltage", BI_Limit.BI_Low_AISG_12V_Voltage_LL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_AISG_12V_Current", BI_Limit.BI_Low_AISG_12V_Current_LL, "A")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_AISG_24V_Voltage", BI_Limit.BI_Low_AISG_24V_Voltage_LL, "V")
            SetReadOnlyRow(Me.dgvSW, "BI_Low_AISG_24V_Current", BI_Limit.BI_Low_AISG_24V_Current_LL, "A")

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub UpdateSWParam()
        Try
            With Me.dgvSW
                For RowCount As Integer = 0 To .Rows.Count - 1
                    Select Case .Rows(RowCount).Cells(0).Value
                        Case "Performance_ODC_Check"
                            BI_Param.ODC_Check = .Rows(RowCount).Cells(1).Value
                        Case "Transfer_Data"
                            BI_Param.Transfer_Data = .Rows(RowCount).Cells(1).Value
                        Case "File_Save_Path"
                            BI_Param.File_Save_Path = .Rows(RowCount).Cells(1).Value
                            'Case "Matlab_Check_Enabled"
                            '    BI_Param.Matlab_Check_Enabled = .Rows(RowCount).Cells(1).Value
                            'Case "Matlab_Picture_Enabled"
                            '    BI_Param.Matlab_Picture_Enabled = .Rows(RowCount).Cells(1).Value
                            '    'Case "Matlab_Program_Path"
                            '    '    BI_Param.Matlab_Program_Path = .Rows(RowCount).Cells(1).Value
                            'Case "Matlab_Picture_Comments"
                            '    BI_Param.Matlab_Picture_Comments = .Rows(RowCount).Cells(1).Value
                            'Case "Matlab_Picture_Save_Path"
                            '    BI_Param.Matlab_Picture_Save_Path = .Rows(RowCount).Cells(1).Value
                    End Select
                Next
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class