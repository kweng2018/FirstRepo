Imports System.Drawing.Color
Imports Microsoft.VisualBasic
Imports AndrewIntegratedProducts.DUTDriverFramework

Public Class frmProcessCheck
    Private UnitCount As Integer


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub frmProcessCheck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call ControlIni()
    End Sub

    Private Sub ControlIni()
        UnitCount = 0
        For slot As Integer = 0 To SlotNum
            If BI_Data(slot).UnitInfo.UnitActive Then
                Me.dgvProcess.Rows.Add(New String(1) {"Slot " & slot + 1, BI_Data(slot).UnitInfo.UnitSN})
                If BI_Param.ODC_Check Then PC.AddRecord(BI_Data(slot).UnitInfo.UnitSN, "Slot" & CStr(slot + 1))
                UnitCount += 1
            Else
                Me.dgvProcess.Rows.Add(New String(1) {"Slot " & slot + 1, "Blank"})
            End If
        Next

        If UnitCount > 0 Then
            If BI_Param.ODC_Check Then
                Me.lbPCStatus.Text = "Wait for Checking"
                Me.lbPCStatus.ForeColor = Black
            Else
                Me.lbPCStatus.Text = "ODC check was skipped"
                Me.lbPCStatus.ForeColor = Blue
            End If
        Else
            Me.lbPCStatus.Text = "No unit active!"
            Me.lbPCStatus.ForeColor = OrangeRed
            AbortTest = True
        End If
    End Sub


    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Call CheckODC()
        If Not ODCCheckResult Then
            Select Case MsgBox("ODC Check failed, do you want to continue?", MsgBoxStyle.OkCancel, "Note")
                Case MsgBoxResult.Ok
                    ODCCheckResult = True
                Case MsgBoxResult.Cancel
                    ODCCheckResult = False
            End Select
        End If
        Me.Close()
    End Sub

    Private Sub AllBtn_Disable()
        Me.btnAbort.Enabled = False
        Me.btnContinue.Enabled = False
    End Sub

    Private Sub AllBtn_Enable()
        Me.btnAbort.Enabled = True
        Me.btnContinue.Enabled = True
    End Sub

    Private Sub btnAbort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbort.Click
        AbortTest = True
        ODCCheckResult = False
        Me.Close()
    End Sub

    Private Function CheckODC() As Boolean

        Try
            AllBtn_Disable()
            If UnitCount = 0 Then
                ODCCheckResult = False
                Return False
                Exit Function
            End If

            If BI_Param.ODC_Check Then
                PC.GetProcessCheck(15)
                If Not PC.ValidProcessCheck Then
                    ODCCheckResult = False
                    Me.lbPCStatus.Text = "Process Check Fail!"
                    Me.lbPCStatus.ForeColor = OrangeRed
                Else
                    ODCCheckResult = True
                    Me.lbPCStatus.Text = "Process Check Pass!"
                    Me.lbPCStatus.ForeColor = LimeGreen
                End If
            Else
                ODCCheckResult = True
                Return True
            End If
        Catch ex As Exception
            ODCCheckResult = False
            Me.lbPCStatus.Text = "Process Check Fail!"
            Return False
        Finally
            AllBtn_Enable()
        End Try

    End Function
End Class