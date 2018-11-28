
Imports System.IO
Imports AndrewIntegratedProducts.DUTDriverFramework
Imports System.Threading.Thread


Public Class frmMain
    Inherits System.Windows.Forms.Form
    Private ProcessCheck As frmProcessCheck

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Dim SC_CLOSE As Integer = 61536
        Dim WM_SYSCOMMAND As Integer = 274

        If m.Msg = WM_SYSCOMMAND AndAlso m.WParam.ToInt32 = SC_CLOSE Then

            Exit Sub
        End If
        MyBase.WndProc(m)

    End Sub
    Private Declare Function GetSystemMenu Lib "User32" (ByVal hwnd As Integer, ByVal bRevert As Long) As Integer
    Private Declare Function RemoveMenu Lib "User32" (ByVal hMenu As Integer, ByVal nPosition As Integer, ByVal wFlags As Integer) As Integer
    Private Declare Function DrawMenuBar Lib "User32" (ByVal hwnd As Integer) As Integer
    Private Declare Function GetMenuItemCount Lib "User32" (ByVal hMenu As Integer) As Integer
    Private Const MF_BYPOSITION = &H400&
    Private Const MF_DISABLED = &H2&

    Private Sub closeX(ByVal wnd As Form)
        Dim hMenu As Integer, nCount As Integer
        'hMenu = GetSystemMenu(wnd.Handle.ToInt32, 0)
        nCount = GetMenuItemCount(hMenu)
        Call RemoveMenu(hMenu, nCount - 1, MF_BYPOSITION Or MF_DISABLED)
        DrawMenuBar(Me.Handle.ToInt32)
    End Sub

    Private Sub Start_Test(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbStart.Click
        If frmVoltage.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Try
            EnableControlBtn(False)
            Reset_State()
            Delay(1000)

            '********************
            'Measurement("tmp")
            '********************

            ReadActiveUnits()

            If AbortTest Then Exit Try
            ProcessCheck = New frmProcessCheck
            ProcessCheck.ShowDialog()

            If Not ODCCheckResult Then
                AbortTest = True
                Exit Try
            End If


            If Not CheckStartTemp() Then
                'MsgBox("Units' start temperature is over " & BI_Param.Start_Temp & " Degree C, Please cool down the temperature and try again!", MsgBoxStyle.OkOnly, "Note")
                MsgBox("Units' start temperature is over " & BI_Profile.BI_Start_Temp & " Degree C, Please cool down the temperature and try again!", MsgBoxStyle.OkOnly, "Note")
                AbortTest = True

                Exit Try
            End If

            ''CheckStatus function to check VSWR
            'Dim VSWR(,) As Double = Nothing
            'Dim Loopcount As Integer = 0
            'Dim vswrErrString As String = "Start VSWR too low ( Must > " & BI_Limit.BI_Pre_VSWR & " ): " & vbCr
            'Do Until CheckStatus(VSWR) Or Loopcount = 2
            '    For slot As Integer = 0 To SlotNum
            '        If BI_Data(slot).UnitInfo.UnitActive Then
            '            If VSWR(slot, 0) < BI_Limit.BI_Pre_VSWR Then
            '                vswrErrString = vswrErrString & " Slot " & slot + 1 & ": " & BI_Data(slot).UnitInfo.UnitSN & vbTab & VSWR(slot, 0) & vbCr
            '            End If
            '            If VSWR(slot, 1) < BI_Limit.BI_Pre_VSWR Then
            '                vswrErrString = vswrErrString & " Slot " & slot + 1 & ": " & BI_Data(slot).UnitInfo.UnitSN & vbTab & VSWR(slot, 1) & vbCr
            '            End If
            '        End If
            '    Next
            '    Select Case MsgBox(vswrErrString, MsgBoxStyle.RetryCancel, "Note")
            '        Case MsgBoxResult.Retry
            '        Case MsgBoxResult.Cancel
            '            AbortTest = True
            '            Exit Try
            '    End Select
            '    Loopcount += 1
            '    'If Loopcount = 3 Then
            '    '    AbortTest = True
            '    '    Exit Sub
            '    'End If
            'Loop

            Application.DoEvents()
            AddMessage("Initializing Units...")
            Application.DoEvents()
            If Not PreBurnIn() Then Exit Sub

            ''Shoud initialize chamber here
            'If BI_HW.Chamber_Control_Enabled Then
            '    ChamberOnOff(True)
            'Else
            '    AddMessage("Please turn on the chamber!")
            'End If
            'Delay(5000)

            'Start test polling
            AddMessage("Polling Units...")
            ResetTestTimeIndicator()
            TakeMeasurement()

        Catch ex As Exception
            AddMessage("Error: " & ex.Message)
            AbortTest = True
        Finally
            Test_Done()
            EnableControlBtn(True)
        End Try

    End Sub


    Private Sub Me_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        On Error Resume Next
        ProcessCheck.Close()
        frmParam.Close()
        End
    End Sub


    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        frmParam.frmParam_Load()

        'V2.0.0_0627, Modify different colour for each BI shelf form.
        Select Case ShelfID
            Case "A"
                'Me.BackColor = Color.Violet
            Case "B"
                Me.BackColor = Color.Khaki
            Case "C"
                Me.BackColor = Color.LightGreen
            Case "D"
                Me.BackColor = Color.LightSkyBlue
            Case "E"
                Me.BackColor = Color.Crimson
        End Select

        InitFormControlsArr()
        IniPS()
        Reset_State()
        Me.dgvSlots.Select()
        closeX(Me)
    End Sub


    Private Sub OpenParametersToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenParametersToolStripMenuItem1.Click, tsbConfig.Click
        frmParam.ShowDialog()
        Form_Load(Nothing, Nothing)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        If MessageBox.Show("Do you want to exit program?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.OK Then End
    End Sub


    Private Sub Stop_Test(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbStop.Click
        AddMessage("Stopping Test...")
        AbortTest = True
    End Sub


    Private Sub TurnOnChamberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TurnOnChamberToolStripMenuItem.Click, tsbTurnOnChamber.Click
        Try
            ChamberOnOff(True)
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub TurnOffChamberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TurnOffChamberToolStripMenuItem.Click, tsbTurnOffChamber.Click
        Try
            ChamberOnOff(False)
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub TurnOnAllPowerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TurnOnAllPowerToolStripMenuItem.Click, tsbTurnOnAllPowers.Click
        Try
            For slot As Integer = 0 To SlotNum
                TurnPowerOnOff(slot, True)
                Threading.Thread.Sleep(100)
            Next
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub TurnOffAllPowerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TurnOffAllPowerToolStripMenuItem.Click, tsbTurnOffAllPowers.Click
        Try
            For slot As Integer = 0 To SlotNum
                TurnPowerOnOff(slot, False)
                Threading.Thread.Sleep(100)
            Next
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub TurnOnAllFansToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TurnOnAllFansToolStripMenuItem.Click, tsbTurnOnAllFans.Click
        Try
            For slot As Integer = 0 To SlotNum
                TurnFanOnOff(slot, True)
                Threading.Thread.Sleep(100)
            Next
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub TurnOffAllFansToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TurnOffAllFansToolStripMenuItem.Click, tsbTurnOffAllFans.Click
        Try
            For slot As Integer = 0 To SlotNum
                TurnFanOnOff(slot, False)
                Threading.Thread.Sleep(100)
            Next
        Catch ex As Exception
            AddMessage(ex.Message)
        End Try
    End Sub

    Private Sub InitFormControlsArr()

        'Me.Text = SW_Title & " " & SW_Version & " with MTR_Ver_" & MTR_Version & " Shelf : " & ShelfID
        Me.Text = SW_Title & " " & SW_Version & " with MTR_Ver_" & MTR_Version & " Plant : " & Plant & " Shelf : " & ShelfID
        MessageIndicators = New Collection
        MessageIndicators.Add(Me.TextBoxMessage)

        dgvSlotTestData(0) = Me.dgvSlotData1
        dgvSlotTestData(0).BringToFront()
        dgvSlotTestData(1) = Me.dgvSlotData2
        'dgvSlotTestData(2) = Me.dgvSlotData3
        'dgvSlotTestData(3) = Me.dgvSlotData4
        'dgvSlotTestData(4) = Me.dgvSlotData5
        'dgvSlotTestData(5) = Me.dgvSlotData6
        'dgvSlotTestData(6) = Me.dgvSlotData7
        'dgvSlotTestData(7) = Me.dgvSlotData8
        'dgvSlotTestData(8) = Me.dgvSlotData9
        'dgvSlotTestData(9) = Me.dgvSlotData10
        'dgvSlotTestData(10) = Me.dgvSlotData11
        'dgvSlotTestData(11) = Me.dgvSlotData12
        'dgvSlotTestData(12) = Me.dgvSlotData13
        'dgvSlotTestData(13) = Me.dgvSlotData14
        'dgvSlotTestData(14) = Me.dgvSlotData15

        dgvFailureInd(0) = Me.dgvDetect1
        dgvFailureInd(0).BringToFront()
        dgvFailureInd(1) = Me.dgvDetect2
        'dgvFailureInd(2) = Me.dgvDetect3
        'dgvFailureInd(3) = Me.dgvDetect4
        'dgvFailureInd(4) = Me.dgvDetect5
        'dgvFailureInd(5) = Me.dgvDetect6
        'dgvFailureInd(6) = Me.dgvDetect7
        'dgvFailureInd(7) = Me.dgvDetect8
        'dgvFailureInd(8) = Me.dgvDetect9
        'dgvFailureInd(9) = Me.dgvDetect10
        'dgvFailureInd(10) = Me.dgvDetect11
        'dgvFailureInd(11) = Me.dgvDetect12
        'dgvFailureInd(12) = Me.dgvDetect13
        'dgvFailureInd(13) = Me.dgvDetect14
        'dgvFailureInd(14) = Me.dgvDetect15

        dgvSlotInd = Me.dgvSlots
        'dgvDetectorFailureInd = Me.dgvDetect

        For slot As Integer = 0 To SlotNum
            dgvSlotTestData(slot).Dock = DockStyle.Fill
            dgvFailureInd(slot).Dock = DockStyle.Fill
        Next

        TurnOnAllFansToolStripMenuItem.Enabled = BI_HW.Fan_Chan_Enabled
        tsbTurnOnAllFans.Enabled = BI_HW.Fan_Chan_Enabled
        TurnOffAllFansToolStripMenuItem.Enabled = BI_HW.Fan_Chan_Enabled
        tsbTurnOffAllFans.Enabled = BI_HW.Fan_Chan_Enabled

        TurnOnAllPowerToolStripMenuItem.Enabled = BI_HW.Pwr_Chan_Enabled
        tsbTurnOnAllPowers.Enabled = BI_HW.Pwr_Chan_Enabled
        TurnOffAllPowerToolStripMenuItem.Enabled = BI_HW.Pwr_Chan_Enabled
        tsbTurnOffAllPowers.Enabled = BI_HW.Pwr_Chan_Enabled

        Dim PS_E As Boolean = False
        For PSCount As Integer = 0 To 5
            If BI_HW.PS_Control_Enabled(PSCount) Then
                PS_E = True
                Exit For
            End If
        Next

        Me.tsmTurnOffAllPowerSuppliers.Enabled = PS_E
        Me.tsmTurnOnAllPowerSuppliers.Enabled = PS_E
        Me.tsbTurnOffPS.Enabled = PS_E
        Me.tsbTurnOnPS.Enabled = PS_E


        'Add for Power Switch control function of Net-Switch   
        tsmTurnOnAnton.Enabled = BI_HW.Anton_Chan_Enabled
        tsbTurnOnAnton.Enabled = BI_HW.Anton_Chan_Enabled
        tsmTurnOffAnton.Enabled = BI_HW.Anton_Chan_Enabled
        tsbTurnOffAnton.Enabled = BI_HW.Anton_Chan_Enabled

        TurnOnChamberToolStripMenuItem.Enabled = BI_HW.Chamber_Control_Enabled
        tsbTurnOnChamber.Enabled = BI_HW.Chamber_Control_Enabled
        TurnOffChamberToolStripMenuItem.Enabled = BI_HW.Chamber_Control_Enabled
        tsbTurnOffChamber.Enabled = BI_HW.Chamber_Control_Enabled

        Me.totTestTimeOut.SetTimeIndtsTextBox = Me.tsTestTimeTextBox
        Me.totTestTimeOut.DisplayMinutes = True
        Me.totTestTimeOut.MinutesDelay = BI_Profile.BI_Duration

        Me.tssLab1.Text = "Product: " & Assembly_Type
        Me.tssLab2.Text = "Site: " & Test_Site
        Me.tssLab3.Text = "SW Version: " & SW_Version
        Me.tssLab4.Text = "MTR Ver: " & MTR_Version
    End Sub

    Private Sub dgvSlots_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSlots.CellEnter
        Dim pSlot As Integer = dgvSlots.CurrentRow.Index
        Me.grpTestData.Text = "Test Date: Slot " & pSlot + 1
        Me.grpDetect.Text = "Failure Indicator: Slot " & pSlot + 1
        dgvSlotTestData(pSlot).BringToFront()
        dgvFailureInd(pSlot).BringToFront()
    End Sub

    Private Sub dgvSlotData1_ColumnHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvSlotData1.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData2.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData3.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData4.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData5.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData6.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData7.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData8.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData9.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData10.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData11.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData12.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData13.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData14.ColumnHeaderMouseDoubleClick, _
                                                                                                                                                            dgvSlotData15.ColumnHeaderMouseDoubleClick
        Dim colCount As Integer = sender.Columns.Count
        For i As Integer = 0 To colCount - 1
            sender.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Next

        'sender.Columns.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    Private Sub tsbTurnOnPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbTurnOnPS.Click, tsmTurnOnAllPowerSuppliers.Click
        Try
            For PSCount As Integer = 0 To 5
                PS(PSCount).Setup(BI_HW.PS_Voltage(PSCount), BI_HW.PS_Current(PSCount))
                PS(PSCount).OutputOn = True

            Next
        Catch ex As Exception
            AddMessage("Turn PS on error: " & ex.Message)
        End Try

    End Sub

    Private Sub tsbTurnOffPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbTurnOffPS.Click, tsmTurnOffAllPowerSuppliers.Click
        Try
            For PSCount As Integer = 0 To 5
                PS(PSCount).OutputOn = False
            Next
        Catch ex As Exception
            AddMessage("Turn PS off error: " & ex.Message)
        End Try
    End Sub

    Private Sub tsmTurnOnAnton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbTurnOnAnton.Click, tsmTurnOnAnton.Click
        'TurnSwitchPSOnOff(True)
        For slot As Integer = 0 To SlotNum
            TurnAntonOnOff(slot, True)
        Next
    End Sub

    Private Sub tsmTurnOffAnton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbTurnOffAnton.Click, tsmTurnOffAnton.Click
        'TurnSwitchPSOnOff(False)
        For slot As Integer = 0 To SlotNum
            TurnAntonOnOff(slot, False)
        Next
    End Sub
 
    Private Sub tsmChangeGW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmChangeGW.Click
        ChangeGW()
    End Sub

    Private Sub BIToolStrip_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles BIToolStrip.ItemClicked

    End Sub
End Class