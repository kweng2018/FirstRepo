<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EventTimer
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.txtEventTimer = New System.Windows.Forms.TextBox
        Me.timer = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'txtEventTimer
        '
        Me.txtEventTimer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEventTimer.Location = New System.Drawing.Point(0, 0)
        Me.txtEventTimer.Margin = New System.Windows.Forms.Padding(2)
        Me.txtEventTimer.Name = "txtEventTimer"
        Me.txtEventTimer.Size = New System.Drawing.Size(90, 20)
        Me.txtEventTimer.TabIndex = 0
        Me.txtEventTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'timer
        '
        Me.timer.Enabled = True
        Me.timer.Interval = 900
        '
        'EventTimer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtEventTimer)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "EventTimer"
        Me.Size = New System.Drawing.Size(90, 26)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtEventTimer As System.Windows.Forms.TextBox
    Friend WithEvents timer As System.Windows.Forms.Timer
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
