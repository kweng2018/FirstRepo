<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParam
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParam))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.btn = New System.Windows.Forms.Button
        Me.tabSettings = New System.Windows.Forms.TabControl
        Me.tabInfo = New System.Windows.Forms.TabPage
        Me.labTestGroupInd = New System.Windows.Forms.Label
        Me.labSiteInd = New System.Windows.Forms.Label
        Me.labProdTypeInd = New System.Windows.Forms.Label
        Me.labMTRVerInd = New System.Windows.Forms.Label
        Me.labSWVerInd = New System.Windows.Forms.Label
        Me.labSWTitileInd = New System.Windows.Forms.Label
        Me.labTestGroup = New System.Windows.Forms.Label
        Me.labSite = New System.Windows.Forms.Label
        Me.labProdType = New System.Windows.Forms.Label
        Me.labMRVer = New System.Windows.Forms.Label
        Me.labSWVer = New System.Windows.Forms.Label
        Me.labSWTitile = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.tabHWParam = New System.Windows.Forms.TabPage
        Me.dgvHW = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tabSWParam = New System.Windows.Forms.TabPage
        Me.dgvSW = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tabSettings.SuspendLayout()
        Me.tabInfo.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabHWParam.SuspendLayout()
        CType(Me.dgvHW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSWParam.SuspendLayout()
        CType(Me.dgvSW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn
        '
        Me.btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btn.Location = New System.Drawing.Point(395, 345)
        Me.btn.Margin = New System.Windows.Forms.Padding(2)
        Me.btn.Name = "btn"
        Me.btn.Size = New System.Drawing.Size(100, 30)
        Me.btn.TabIndex = 4
        Me.btn.Text = "OK"
        Me.btn.UseVisualStyleBackColor = True
        '
        'tabSettings
        '
        Me.tabSettings.Controls.Add(Me.tabInfo)
        Me.tabSettings.Controls.Add(Me.tabHWParam)
        Me.tabSettings.Controls.Add(Me.tabSWParam)
        Me.tabSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tabSettings.HotTrack = True
        Me.tabSettings.Location = New System.Drawing.Point(6, 6)
        Me.tabSettings.Name = "tabSettings"
        Me.tabSettings.SelectedIndex = 0
        Me.tabSettings.Size = New System.Drawing.Size(509, 334)
        Me.tabSettings.TabIndex = 24
        '
        'tabInfo
        '
        Me.tabInfo.Controls.Add(Me.labTestGroupInd)
        Me.tabInfo.Controls.Add(Me.labSiteInd)
        Me.tabInfo.Controls.Add(Me.labProdTypeInd)
        Me.tabInfo.Controls.Add(Me.labMTRVerInd)
        Me.tabInfo.Controls.Add(Me.labSWVerInd)
        Me.tabInfo.Controls.Add(Me.labSWTitileInd)
        Me.tabInfo.Controls.Add(Me.labTestGroup)
        Me.tabInfo.Controls.Add(Me.labSite)
        Me.tabInfo.Controls.Add(Me.labProdType)
        Me.tabInfo.Controls.Add(Me.labMRVer)
        Me.tabInfo.Controls.Add(Me.labSWVer)
        Me.tabInfo.Controls.Add(Me.labSWTitile)
        Me.tabInfo.Controls.Add(Me.PictureBox1)
        Me.tabInfo.Location = New System.Drawing.Point(4, 22)
        Me.tabInfo.Name = "tabInfo"
        Me.tabInfo.Size = New System.Drawing.Size(501, 308)
        Me.tabInfo.TabIndex = 7
        Me.tabInfo.Text = "Information"
        Me.tabInfo.UseVisualStyleBackColor = True
        '
        'labTestGroupInd
        '
        Me.labTestGroupInd.AutoSize = True
        Me.labTestGroupInd.Location = New System.Drawing.Point(398, 202)
        Me.labTestGroupInd.Name = "labTestGroupInd"
        Me.labTestGroupInd.Size = New System.Drawing.Size(22, 13)
        Me.labTestGroupInd.TabIndex = 11
        Me.labTestGroupInd.Text = "NA"
        '
        'labSiteInd
        '
        Me.labSiteInd.AutoSize = True
        Me.labSiteInd.Location = New System.Drawing.Point(398, 179)
        Me.labSiteInd.Name = "labSiteInd"
        Me.labSiteInd.Size = New System.Drawing.Size(22, 13)
        Me.labSiteInd.TabIndex = 10
        Me.labSiteInd.Text = "NA"
        '
        'labProdTypeInd
        '
        Me.labProdTypeInd.AutoSize = True
        Me.labProdTypeInd.Location = New System.Drawing.Point(398, 156)
        Me.labProdTypeInd.Name = "labProdTypeInd"
        Me.labProdTypeInd.Size = New System.Drawing.Size(22, 13)
        Me.labProdTypeInd.TabIndex = 9
        Me.labProdTypeInd.Text = "NA"
        '
        'labMTRVerInd
        '
        Me.labMTRVerInd.AutoSize = True
        Me.labMTRVerInd.Location = New System.Drawing.Point(398, 133)
        Me.labMTRVerInd.Name = "labMTRVerInd"
        Me.labMTRVerInd.Size = New System.Drawing.Size(22, 13)
        Me.labMTRVerInd.TabIndex = 8
        Me.labMTRVerInd.Text = "NA"
        '
        'labSWVerInd
        '
        Me.labSWVerInd.AutoSize = True
        Me.labSWVerInd.Location = New System.Drawing.Point(398, 110)
        Me.labSWVerInd.Name = "labSWVerInd"
        Me.labSWVerInd.Size = New System.Drawing.Size(22, 13)
        Me.labSWVerInd.TabIndex = 7
        Me.labSWVerInd.Text = "NA"
        '
        'labSWTitileInd
        '
        Me.labSWTitileInd.AutoSize = True
        Me.labSWTitileInd.Location = New System.Drawing.Point(398, 87)
        Me.labSWTitileInd.Name = "labSWTitileInd"
        Me.labSWTitileInd.Size = New System.Drawing.Size(22, 13)
        Me.labSWTitileInd.TabIndex = 6
        Me.labSWTitileInd.Text = "NA"
        '
        'labTestGroup
        '
        Me.labTestGroup.AutoSize = True
        Me.labTestGroup.Location = New System.Drawing.Point(323, 202)
        Me.labTestGroup.Name = "labTestGroup"
        Me.labTestGroup.Size = New System.Drawing.Size(63, 13)
        Me.labTestGroup.TabIndex = 5
        Me.labTestGroup.Text = "Test Group:"
        '
        'labSite
        '
        Me.labSite.AutoSize = True
        Me.labSite.Location = New System.Drawing.Point(323, 179)
        Me.labSite.Name = "labSite"
        Me.labSite.Size = New System.Drawing.Size(28, 13)
        Me.labSite.TabIndex = 4
        Me.labSite.Text = "Site:"
        '
        'labProdType
        '
        Me.labProdType.AutoSize = True
        Me.labProdType.Location = New System.Drawing.Point(323, 156)
        Me.labProdType.Name = "labProdType"
        Me.labProdType.Size = New System.Drawing.Size(74, 13)
        Me.labProdType.TabIndex = 3
        Me.labProdType.Text = "Product Type:"
        '
        'labMRVer
        '
        Me.labMRVer.AutoSize = True
        Me.labMRVer.Location = New System.Drawing.Point(323, 133)
        Me.labMRVer.Name = "labMRVer"
        Me.labMRVer.Size = New System.Drawing.Size(72, 13)
        Me.labMRVer.TabIndex = 2
        Me.labMRVer.Text = "MTR Version:"
        '
        'labSWVer
        '
        Me.labSWVer.AutoSize = True
        Me.labSWVer.Location = New System.Drawing.Point(323, 110)
        Me.labSWVer.Name = "labSWVer"
        Me.labSWVer.Size = New System.Drawing.Size(66, 13)
        Me.labSWVer.TabIndex = 1
        Me.labSWVer.Text = "SW Version:"
        '
        'labSWTitile
        '
        Me.labSWTitile.AutoSize = True
        Me.labSWTitile.Location = New System.Drawing.Point(323, 87)
        Me.labSWTitile.Name = "labSWTitile"
        Me.labSWTitile.Size = New System.Drawing.Size(53, 13)
        Me.labSWTitile.TabIndex = 0
        Me.labSWTitile.Text = "SW Titile:"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(493, 303)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'tabHWParam
        '
        Me.tabHWParam.Controls.Add(Me.dgvHW)
        Me.tabHWParam.Location = New System.Drawing.Point(4, 22)
        Me.tabHWParam.Name = "tabHWParam"
        Me.tabHWParam.Size = New System.Drawing.Size(501, 308)
        Me.tabHWParam.TabIndex = 1
        Me.tabHWParam.Text = "HW Param"
        Me.tabHWParam.UseVisualStyleBackColor = True
        '
        'dgvHW
        '
        Me.dgvHW.AllowUserToAddRows = False
        Me.dgvHW.AllowUserToDeleteRows = False
        Me.dgvHW.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvHW.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvHW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHW.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn13})
        Me.dgvHW.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvHW.Location = New System.Drawing.Point(0, 0)
        Me.dgvHW.Name = "dgvHW"
        Me.dgvHW.RowHeadersVisible = False
        Me.dgvHW.RowTemplate.Height = 23
        Me.dgvHW.Size = New System.Drawing.Size(501, 308)
        Me.dgvHW.TabIndex = 3
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Item"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn9.Width = 150
        '
        'DataGridViewTextBoxColumn10
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn10.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn10.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn10.Width = 230
        '
        'DataGridViewTextBoxColumn13
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn13.HeaderText = "Unit"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'tabSWParam
        '
        Me.tabSWParam.Controls.Add(Me.dgvSW)
        Me.tabSWParam.Location = New System.Drawing.Point(4, 22)
        Me.tabSWParam.Name = "tabSWParam"
        Me.tabSWParam.Size = New System.Drawing.Size(501, 308)
        Me.tabSWParam.TabIndex = 8
        Me.tabSWParam.Text = "SW Param"
        Me.tabSWParam.UseVisualStyleBackColor = True
        '
        'dgvSW
        '
        Me.dgvSW.AllowUserToAddRows = False
        Me.dgvSW.AllowUserToDeleteRows = False
        Me.dgvSW.AllowUserToResizeRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSW.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvSW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSW.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn14})
        Me.dgvSW.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSW.Location = New System.Drawing.Point(0, 0)
        Me.dgvSW.Name = "dgvSW"
        Me.dgvSW.RowHeadersVisible = False
        Me.dgvSW.RowTemplate.Height = 23
        Me.dgvSW.Size = New System.Drawing.Size(501, 308)
        Me.dgvSW.TabIndex = 4
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Item"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn11.Width = 150
        '
        'DataGridViewTextBoxColumn12
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn12.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn12.Width = 230
        '
        'DataGridViewTextBoxColumn14
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn14.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn14.HeaderText = "Unit"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'frmParam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(518, 384)
        Me.Controls.Add(Me.tabSettings)
        Me.Controls.Add(Me.btn)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Name = "frmParam"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Parameters Setting"
        Me.tabSettings.ResumeLayout(False)
        Me.tabInfo.ResumeLayout(False)
        Me.tabInfo.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabHWParam.ResumeLayout(False)
        CType(Me.dgvHW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSWParam.ResumeLayout(False)
        CType(Me.dgvSW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn As System.Windows.Forms.Button
    Friend WithEvents tabSettings As System.Windows.Forms.TabControl
    Friend WithEvents tabHWParam As System.Windows.Forms.TabPage
    Friend WithEvents tabInfo As System.Windows.Forms.TabPage
    Friend WithEvents labTestGroup As System.Windows.Forms.Label
    Friend WithEvents labSite As System.Windows.Forms.Label
    Friend WithEvents labProdType As System.Windows.Forms.Label
    Friend WithEvents labMRVer As System.Windows.Forms.Label
    Friend WithEvents labSWVer As System.Windows.Forms.Label
    Friend WithEvents labSWTitile As System.Windows.Forms.Label
    Friend WithEvents labTestGroupInd As System.Windows.Forms.Label
    Friend WithEvents labSiteInd As System.Windows.Forms.Label
    Friend WithEvents labProdTypeInd As System.Windows.Forms.Label
    Friend WithEvents labMTRVerInd As System.Windows.Forms.Label
    Friend WithEvents labSWVerInd As System.Windows.Forms.Label
    Friend WithEvents labSWTitileInd As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents tabSWParam As System.Windows.Forms.TabPage
    Friend WithEvents dgvHW As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSW As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
