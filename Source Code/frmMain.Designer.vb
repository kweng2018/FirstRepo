<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Public Testdata = New TestResults
        'Add any initialization after the InitializeComponent() call
        For i As Integer = 0 To SlotNum
            Transceiver(i) = New AndrewIntegratedProducts.DUTDriverFramework.Viper_Transceiver
            BI_Data(i) = New clsBurnInData
        Next
        'ProcessCheck = New frmProcessCheck()
        'InfomationForm = New frmInfo
    End Sub
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle36 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle37 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle42 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle38 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle39 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle40 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle41 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle43 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle48 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle44 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle45 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle46 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle47 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle49 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle54 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle50 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle51 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle52 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle53 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle55 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle60 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle56 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle57 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle58 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle59 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle61 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle66 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle62 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle63 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle64 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle65 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle67 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle72 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle68 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle69 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle70 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle71 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle73 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle78 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle74 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle75 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle76 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle77 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle79 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle84 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle80 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle81 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle82 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle83 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle85 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle90 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle86 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle87 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle88 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle89 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle91 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle92 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle93 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle94 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle95 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle96 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle97 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle98 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle99 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle100 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle101 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle102 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle103 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle104 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle105 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle106 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle110 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle107 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle108 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle109 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BIToolStrip = New System.Windows.Forms.ToolStrip()
        Me.tsbStart = New System.Windows.Forms.ToolStripButton()
        Me.tsbStop = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbTurnOnAllFans = New System.Windows.Forms.ToolStripButton()
        Me.tsbTurnOffAllFans = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbTurnOnAllPowers = New System.Windows.Forms.ToolStripButton()
        Me.tsbTurnOffAllPowers = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbTurnOnChamber = New System.Windows.Forms.ToolStripButton()
        Me.tsbTurnOffChamber = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbTurnOnPS = New System.Windows.Forms.ToolStripButton()
        Me.tsbTurnOffPS = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbTurnOnAnton = New System.Windows.Forms.ToolStripButton()
        Me.tsbTurnOffAnton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsLabel = New System.Windows.Forms.ToolStripLabel()
        Me.tsTestTimeTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.tslMinutes = New System.Windows.Forms.ToolStripLabel()
        Me.BIStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.tssLab1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssLab2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssLab3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssLab4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TextBoxMessage = New System.Windows.Forms.TextBox()
        Me.grpDetect = New System.Windows.Forms.GroupBox()
        Me.dgvDetect8 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn30 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn31 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn32 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect9 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn33 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn34 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn36 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect10 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn37 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn38 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn39 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn40 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect11 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn41 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn42 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn43 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn44 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect12 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn45 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn46 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn47 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn48 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect13 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn49 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn50 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn51 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn52 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect14 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn53 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn54 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn55 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn56 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect15 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn57 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn58 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn59 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn60 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect4 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect5 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect6 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect7 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect3 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDetect1 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpTestData = New System.Windows.Forms.GroupBox()
        Me.totTestTimeOut = New Viper_Burn_In.EventTimer()
        Me.dgvSlotData15 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData14 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData13 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData12 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData11 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData10 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData9 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData8 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData7 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData6 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData5 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData4 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData3 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData2 = New System.Windows.Forms.DataGridView()
        Me.dgvSlotData1 = New System.Windows.Forms.DataGridView()
        Me.grpSlots = New System.Windows.Forms.GroupBox()
        Me.dgvSlots = New System.Windows.Forms.DataGridView()
        Me.colSlot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tlpInterface = New System.Windows.Forms.TableLayoutPanel()
        Me.OpenParametersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenParametersToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ControlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TurnOnChamberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TurnOffChamberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TurnOnAllPowerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TurnOffAllPowerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TurnOnAllFansToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TurnOffAllFansToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmTurnOnAllPowerSuppliers = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmTurnOffAllPowerSuppliers = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmTurnOnAnton = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmTurnOffAnton = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmChangeGW = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.BIToolStrip.SuspendLayout()
        Me.BIStatusStrip.SuspendLayout()
        Me.grpDetect.SuspendLayout()
        CType(Me.dgvDetect8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetect1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTestData.SuspendLayout()
        CType(Me.dgvSlotData15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSlotData1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSlots.SuspendLayout()
        CType(Me.dgvSlots, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpInterface.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BIToolStrip
        '
        Me.BIToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbStart, Me.tsbStop, Me.ToolStripSplitButton1, Me.tsbTurnOnAllFans, Me.tsbTurnOffAllFans, Me.ToolStripSeparator2, Me.tsbTurnOnAllPowers, Me.tsbTurnOffAllPowers, Me.ToolStripSeparator3, Me.tsbTurnOnChamber, Me.tsbTurnOffChamber, Me.ToolStripSeparator6, Me.tsbTurnOnPS, Me.tsbTurnOffPS, Me.ToolStripSeparator8, Me.tsbTurnOnAnton, Me.tsbTurnOffAnton, Me.ToolStripSeparator4, Me.tsbConfig, Me.ToolStripSeparator5, Me.tsLabel, Me.tsTestTimeTextBox, Me.tslMinutes})
        Me.BIToolStrip.Location = New System.Drawing.Point(0, 24)
        Me.BIToolStrip.Name = "BIToolStrip"
        Me.BIToolStrip.Size = New System.Drawing.Size(904, 25)
        Me.BIToolStrip.TabIndex = 33
        Me.BIToolStrip.Text = "ToolStrip1"
        '
        'tsbStart
        '
        Me.tsbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbStart.Image = CType(resources.GetObject("tsbStart.Image"), System.Drawing.Image)
        Me.tsbStart.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbStart.Name = "tsbStart"
        Me.tsbStart.Size = New System.Drawing.Size(23, 22)
        Me.tsbStart.Text = "Start"
        '
        'tsbStop
        '
        Me.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbStop.Image = CType(resources.GetObject("tsbStop.Image"), System.Drawing.Image)
        Me.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbStop.Name = "tsbStop"
        Me.tsbStop.Size = New System.Drawing.Size(23, 22)
        Me.tsbStop.Text = "Stop"
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(6, 25)
        '
        'tsbTurnOnAllFans
        '
        Me.tsbTurnOnAllFans.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTurnOnAllFans.Image = CType(resources.GetObject("tsbTurnOnAllFans.Image"), System.Drawing.Image)
        Me.tsbTurnOnAllFans.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTurnOnAllFans.Name = "tsbTurnOnAllFans"
        Me.tsbTurnOnAllFans.Size = New System.Drawing.Size(23, 22)
        Me.tsbTurnOnAllFans.Text = "Turn On All Fans"
        '
        'tsbTurnOffAllFans
        '
        Me.tsbTurnOffAllFans.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTurnOffAllFans.Image = CType(resources.GetObject("tsbTurnOffAllFans.Image"), System.Drawing.Image)
        Me.tsbTurnOffAllFans.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTurnOffAllFans.Name = "tsbTurnOffAllFans"
        Me.tsbTurnOffAllFans.Size = New System.Drawing.Size(23, 22)
        Me.tsbTurnOffAllFans.Text = "Turn Off All Fans"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tsbTurnOnAllPowers
        '
        Me.tsbTurnOnAllPowers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTurnOnAllPowers.Image = CType(resources.GetObject("tsbTurnOnAllPowers.Image"), System.Drawing.Image)
        Me.tsbTurnOnAllPowers.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTurnOnAllPowers.Name = "tsbTurnOnAllPowers"
        Me.tsbTurnOnAllPowers.Size = New System.Drawing.Size(23, 22)
        Me.tsbTurnOnAllPowers.Text = "Turn On All Powers"
        '
        'tsbTurnOffAllPowers
        '
        Me.tsbTurnOffAllPowers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTurnOffAllPowers.Image = CType(resources.GetObject("tsbTurnOffAllPowers.Image"), System.Drawing.Image)
        Me.tsbTurnOffAllPowers.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTurnOffAllPowers.Name = "tsbTurnOffAllPowers"
        Me.tsbTurnOffAllPowers.Size = New System.Drawing.Size(23, 22)
        Me.tsbTurnOffAllPowers.Text = "Turn Off All Powers"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'tsbTurnOnChamber
        '
        Me.tsbTurnOnChamber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTurnOnChamber.Image = CType(resources.GetObject("tsbTurnOnChamber.Image"), System.Drawing.Image)
        Me.tsbTurnOnChamber.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTurnOnChamber.Name = "tsbTurnOnChamber"
        Me.tsbTurnOnChamber.Size = New System.Drawing.Size(23, 22)
        Me.tsbTurnOnChamber.Text = "Turn On Chamber"
        '
        'tsbTurnOffChamber
        '
        Me.tsbTurnOffChamber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTurnOffChamber.Image = CType(resources.GetObject("tsbTurnOffChamber.Image"), System.Drawing.Image)
        Me.tsbTurnOffChamber.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTurnOffChamber.Name = "tsbTurnOffChamber"
        Me.tsbTurnOffChamber.Size = New System.Drawing.Size(23, 22)
        Me.tsbTurnOffChamber.Text = "Turn Off Chamber"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'tsbTurnOnPS
        '
        Me.tsbTurnOnPS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTurnOnPS.Image = CType(resources.GetObject("tsbTurnOnPS.Image"), System.Drawing.Image)
        Me.tsbTurnOnPS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTurnOnPS.Name = "tsbTurnOnPS"
        Me.tsbTurnOnPS.Size = New System.Drawing.Size(23, 22)
        Me.tsbTurnOnPS.Text = "Turn On Power Suppliers"
        '
        'tsbTurnOffPS
        '
        Me.tsbTurnOffPS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTurnOffPS.Image = CType(resources.GetObject("tsbTurnOffPS.Image"), System.Drawing.Image)
        Me.tsbTurnOffPS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTurnOffPS.Name = "tsbTurnOffPS"
        Me.tsbTurnOffPS.Size = New System.Drawing.Size(23, 22)
        Me.tsbTurnOffPS.Text = "Turn Off Power Suppliers"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'tsbTurnOnAnton
        '
        Me.tsbTurnOnAnton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTurnOnAnton.Image = CType(resources.GetObject("tsbTurnOnAnton.Image"), System.Drawing.Image)
        Me.tsbTurnOnAnton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTurnOnAnton.Name = "tsbTurnOnAnton"
        Me.tsbTurnOnAnton.Size = New System.Drawing.Size(23, 22)
        Me.tsbTurnOnAnton.Text = "Turn On All Anton"
        '
        'tsbTurnOffAnton
        '
        Me.tsbTurnOffAnton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTurnOffAnton.Image = CType(resources.GetObject("tsbTurnOffAnton.Image"), System.Drawing.Image)
        Me.tsbTurnOffAnton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTurnOffAnton.Name = "tsbTurnOffAnton"
        Me.tsbTurnOffAnton.Size = New System.Drawing.Size(23, 22)
        Me.tsbTurnOffAnton.Text = "Turn Off All Anton"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'tsbConfig
        '
        Me.tsbConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbConfig.Image = CType(resources.GetObject("tsbConfig.Image"), System.Drawing.Image)
        Me.tsbConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConfig.Name = "tsbConfig"
        Me.tsbConfig.Size = New System.Drawing.Size(23, 22)
        Me.tsbConfig.Text = "ToolStripButton9"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'tsLabel
        '
        Me.tsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.tsLabel.ForeColor = System.Drawing.Color.Blue
        Me.tsLabel.Name = "tsLabel"
        Me.tsLabel.Size = New System.Drawing.Size(78, 22)
        Me.tsLabel.Text = "Test Time: "
        '
        'tsTestTimeTextBox
        '
        Me.tsTestTimeTextBox.BackColor = System.Drawing.SystemColors.Info
        Me.tsTestTimeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tsTestTimeTextBox.Name = "tsTestTimeTextBox"
        Me.tsTestTimeTextBox.ReadOnly = True
        Me.tsTestTimeTextBox.Size = New System.Drawing.Size(29, 25)
        Me.tsTestTimeTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tslMinutes
        '
        Me.tslMinutes.ForeColor = System.Drawing.Color.Blue
        Me.tslMinutes.Name = "tslMinutes"
        Me.tslMinutes.Size = New System.Drawing.Size(44, 22)
        Me.tslMinutes.Text = "Minutes"
        '
        'BIStatusStrip
        '
        Me.BIStatusStrip.BackColor = System.Drawing.SystemColors.Control
        Me.BIStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssLab1, Me.tssLab2, Me.tssLab3, Me.tssLab4})
        Me.BIStatusStrip.Location = New System.Drawing.Point(0, 609)
        Me.BIStatusStrip.Name = "BIStatusStrip"
        Me.BIStatusStrip.Padding = New System.Windows.Forms.Padding(1, 0, 8, 0)
        Me.BIStatusStrip.Size = New System.Drawing.Size(904, 22)
        Me.BIStatusStrip.SizingGrip = False
        Me.BIStatusStrip.TabIndex = 34
        Me.BIStatusStrip.Text = "StatusStrip1"
        '
        'tssLab1
        '
        Me.tssLab1.BackColor = System.Drawing.SystemColors.Control
        Me.tssLab1.Name = "tssLab1"
        Me.tssLab1.Size = New System.Drawing.Size(223, 17)
        Me.tssLab1.Spring = True
        Me.tssLab1.Text = "Product:"
        '
        'tssLab2
        '
        Me.tssLab2.BackColor = System.Drawing.SystemColors.Control
        Me.tssLab2.Name = "tssLab2"
        Me.tssLab2.Size = New System.Drawing.Size(223, 17)
        Me.tssLab2.Spring = True
        Me.tssLab2.Text = "Site:"
        '
        'tssLab3
        '
        Me.tssLab3.BackColor = System.Drawing.SystemColors.Control
        Me.tssLab3.Name = "tssLab3"
        Me.tssLab3.Size = New System.Drawing.Size(223, 17)
        Me.tssLab3.Spring = True
        Me.tssLab3.Text = "SW Ver:"
        '
        'tssLab4
        '
        Me.tssLab4.BackColor = System.Drawing.SystemColors.Control
        Me.tssLab4.Name = "tssLab4"
        Me.tssLab4.Size = New System.Drawing.Size(223, 17)
        Me.tssLab4.Spring = True
        Me.tssLab4.Text = "MTR Ver:"
        '
        'TextBoxMessage
        '
        Me.TextBoxMessage.BackColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxMessage.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TextBoxMessage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TextBoxMessage.Location = New System.Drawing.Point(0, 544)
        Me.TextBoxMessage.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxMessage.Multiline = True
        Me.TextBoxMessage.Name = "TextBoxMessage"
        Me.TextBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxMessage.Size = New System.Drawing.Size(904, 65)
        Me.TextBoxMessage.TabIndex = 14
        '
        'grpDetect
        '
        Me.grpDetect.Controls.Add(Me.dgvDetect8)
        Me.grpDetect.Controls.Add(Me.dgvDetect9)
        Me.grpDetect.Controls.Add(Me.dgvDetect10)
        Me.grpDetect.Controls.Add(Me.dgvDetect11)
        Me.grpDetect.Controls.Add(Me.dgvDetect12)
        Me.grpDetect.Controls.Add(Me.dgvDetect13)
        Me.grpDetect.Controls.Add(Me.dgvDetect14)
        Me.grpDetect.Controls.Add(Me.dgvDetect15)
        Me.grpDetect.Controls.Add(Me.dgvDetect4)
        Me.grpDetect.Controls.Add(Me.dgvDetect5)
        Me.grpDetect.Controls.Add(Me.dgvDetect6)
        Me.grpDetect.Controls.Add(Me.dgvDetect7)
        Me.grpDetect.Controls.Add(Me.dgvDetect2)
        Me.grpDetect.Controls.Add(Me.dgvDetect3)
        Me.grpDetect.Controls.Add(Me.dgvDetect1)
        Me.grpDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpDetect.Location = New System.Drawing.Point(2, 370)
        Me.grpDetect.Margin = New System.Windows.Forms.Padding(2)
        Me.grpDetect.Name = "grpDetect"
        Me.grpDetect.Padding = New System.Windows.Forms.Padding(2)
        Me.grpDetect.Size = New System.Drawing.Size(176, 113)
        Me.grpDetect.TabIndex = 43
        Me.grpDetect.TabStop = False
        Me.grpDetect.Text = "Failure Indicator"
        '
        'dgvDetect8
        '
        Me.dgvDetect8.AllowUserToAddRows = False
        Me.dgvDetect8.AllowUserToDeleteRows = False
        Me.dgvDetect8.AllowUserToResizeRows = False
        Me.dgvDetect8.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect8.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDetect8.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect8.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn29, Me.DataGridViewTextBoxColumn30, Me.DataGridViewTextBoxColumn31, Me.DataGridViewTextBoxColumn32})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect8.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvDetect8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect8.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect8.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect8.MultiSelect = False
        Me.dgvDetect8.Name = "dgvDetect8"
        Me.dgvDetect8.ReadOnly = True
        Me.dgvDetect8.RowHeadersVisible = False
        Me.dgvDetect8.RowTemplate.Height = 23
        Me.dgvDetect8.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect8.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect8.TabIndex = 56
        '
        'DataGridViewTextBoxColumn29
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn29.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn29.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.ReadOnly = True
        Me.DataGridViewTextBoxColumn29.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn30
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn30.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn30.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn30.Name = "DataGridViewTextBoxColumn30"
        Me.DataGridViewTextBoxColumn30.ReadOnly = True
        Me.DataGridViewTextBoxColumn30.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn30.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn30.Width = 50
        '
        'DataGridViewTextBoxColumn31
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn31.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn31.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn31.Name = "DataGridViewTextBoxColumn31"
        Me.DataGridViewTextBoxColumn31.ReadOnly = True
        Me.DataGridViewTextBoxColumn31.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn31.Width = 80
        '
        'DataGridViewTextBoxColumn32
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn32.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn32.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn32.Name = "DataGridViewTextBoxColumn32"
        Me.DataGridViewTextBoxColumn32.ReadOnly = True
        Me.DataGridViewTextBoxColumn32.Width = 50
        '
        'dgvDetect9
        '
        Me.dgvDetect9.AllowUserToAddRows = False
        Me.dgvDetect9.AllowUserToDeleteRows = False
        Me.dgvDetect9.AllowUserToResizeRows = False
        Me.dgvDetect9.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect9.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvDetect9.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect9.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn33, Me.DataGridViewTextBoxColumn34, Me.DataGridViewTextBoxColumn35, Me.DataGridViewTextBoxColumn36})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect9.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgvDetect9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect9.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect9.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect9.MultiSelect = False
        Me.dgvDetect9.Name = "dgvDetect9"
        Me.dgvDetect9.ReadOnly = True
        Me.dgvDetect9.RowHeadersVisible = False
        Me.dgvDetect9.RowTemplate.Height = 23
        Me.dgvDetect9.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect9.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect9.TabIndex = 55
        '
        'DataGridViewTextBoxColumn33
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn33.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn33.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn33.Name = "DataGridViewTextBoxColumn33"
        Me.DataGridViewTextBoxColumn33.ReadOnly = True
        Me.DataGridViewTextBoxColumn33.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn34
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn34.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn34.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn34.Name = "DataGridViewTextBoxColumn34"
        Me.DataGridViewTextBoxColumn34.ReadOnly = True
        Me.DataGridViewTextBoxColumn34.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn34.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn34.Width = 50
        '
        'DataGridViewTextBoxColumn35
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn35.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn35.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        Me.DataGridViewTextBoxColumn35.ReadOnly = True
        Me.DataGridViewTextBoxColumn35.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn35.Width = 80
        '
        'DataGridViewTextBoxColumn36
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn36.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn36.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn36.Name = "DataGridViewTextBoxColumn36"
        Me.DataGridViewTextBoxColumn36.ReadOnly = True
        Me.DataGridViewTextBoxColumn36.Width = 50
        '
        'dgvDetect10
        '
        Me.dgvDetect10.AllowUserToAddRows = False
        Me.dgvDetect10.AllowUserToDeleteRows = False
        Me.dgvDetect10.AllowUserToResizeRows = False
        Me.dgvDetect10.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect10.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.dgvDetect10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect10.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn37, Me.DataGridViewTextBoxColumn38, Me.DataGridViewTextBoxColumn39, Me.DataGridViewTextBoxColumn40})
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect10.DefaultCellStyle = DataGridViewCellStyle18
        Me.dgvDetect10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect10.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect10.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect10.MultiSelect = False
        Me.dgvDetect10.Name = "dgvDetect10"
        Me.dgvDetect10.ReadOnly = True
        Me.dgvDetect10.RowHeadersVisible = False
        Me.dgvDetect10.RowTemplate.Height = 23
        Me.dgvDetect10.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect10.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect10.TabIndex = 54
        '
        'DataGridViewTextBoxColumn37
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn37.DefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridViewTextBoxColumn37.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn37.Name = "DataGridViewTextBoxColumn37"
        Me.DataGridViewTextBoxColumn37.ReadOnly = True
        Me.DataGridViewTextBoxColumn37.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn38
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn38.DefaultCellStyle = DataGridViewCellStyle15
        Me.DataGridViewTextBoxColumn38.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn38.Name = "DataGridViewTextBoxColumn38"
        Me.DataGridViewTextBoxColumn38.ReadOnly = True
        Me.DataGridViewTextBoxColumn38.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn38.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn38.Width = 50
        '
        'DataGridViewTextBoxColumn39
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle16.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn39.DefaultCellStyle = DataGridViewCellStyle16
        Me.DataGridViewTextBoxColumn39.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn39.Name = "DataGridViewTextBoxColumn39"
        Me.DataGridViewTextBoxColumn39.ReadOnly = True
        Me.DataGridViewTextBoxColumn39.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn39.Width = 80
        '
        'DataGridViewTextBoxColumn40
        '
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn40.DefaultCellStyle = DataGridViewCellStyle17
        Me.DataGridViewTextBoxColumn40.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn40.Name = "DataGridViewTextBoxColumn40"
        Me.DataGridViewTextBoxColumn40.ReadOnly = True
        Me.DataGridViewTextBoxColumn40.Width = 50
        '
        'dgvDetect11
        '
        Me.dgvDetect11.AllowUserToAddRows = False
        Me.dgvDetect11.AllowUserToDeleteRows = False
        Me.dgvDetect11.AllowUserToResizeRows = False
        Me.dgvDetect11.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect11.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle19
        Me.dgvDetect11.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect11.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn41, Me.DataGridViewTextBoxColumn42, Me.DataGridViewTextBoxColumn43, Me.DataGridViewTextBoxColumn44})
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect11.DefaultCellStyle = DataGridViewCellStyle24
        Me.dgvDetect11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect11.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect11.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect11.MultiSelect = False
        Me.dgvDetect11.Name = "dgvDetect11"
        Me.dgvDetect11.ReadOnly = True
        Me.dgvDetect11.RowHeadersVisible = False
        Me.dgvDetect11.RowTemplate.Height = 23
        Me.dgvDetect11.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect11.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect11.TabIndex = 53
        '
        'DataGridViewTextBoxColumn41
        '
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn41.DefaultCellStyle = DataGridViewCellStyle20
        Me.DataGridViewTextBoxColumn41.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn41.Name = "DataGridViewTextBoxColumn41"
        Me.DataGridViewTextBoxColumn41.ReadOnly = True
        Me.DataGridViewTextBoxColumn41.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn42
        '
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn42.DefaultCellStyle = DataGridViewCellStyle21
        Me.DataGridViewTextBoxColumn42.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn42.Name = "DataGridViewTextBoxColumn42"
        Me.DataGridViewTextBoxColumn42.ReadOnly = True
        Me.DataGridViewTextBoxColumn42.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn42.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn42.Width = 50
        '
        'DataGridViewTextBoxColumn43
        '
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle22.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn43.DefaultCellStyle = DataGridViewCellStyle22
        Me.DataGridViewTextBoxColumn43.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn43.Name = "DataGridViewTextBoxColumn43"
        Me.DataGridViewTextBoxColumn43.ReadOnly = True
        Me.DataGridViewTextBoxColumn43.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn43.Width = 80
        '
        'DataGridViewTextBoxColumn44
        '
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn44.DefaultCellStyle = DataGridViewCellStyle23
        Me.DataGridViewTextBoxColumn44.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn44.Name = "DataGridViewTextBoxColumn44"
        Me.DataGridViewTextBoxColumn44.ReadOnly = True
        Me.DataGridViewTextBoxColumn44.Width = 50
        '
        'dgvDetect12
        '
        Me.dgvDetect12.AllowUserToAddRows = False
        Me.dgvDetect12.AllowUserToDeleteRows = False
        Me.dgvDetect12.AllowUserToResizeRows = False
        Me.dgvDetect12.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect12.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle25
        Me.dgvDetect12.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect12.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn45, Me.DataGridViewTextBoxColumn46, Me.DataGridViewTextBoxColumn47, Me.DataGridViewTextBoxColumn48})
        DataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle30.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect12.DefaultCellStyle = DataGridViewCellStyle30
        Me.dgvDetect12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect12.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect12.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect12.MultiSelect = False
        Me.dgvDetect12.Name = "dgvDetect12"
        Me.dgvDetect12.ReadOnly = True
        Me.dgvDetect12.RowHeadersVisible = False
        Me.dgvDetect12.RowTemplate.Height = 23
        Me.dgvDetect12.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect12.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect12.TabIndex = 52
        '
        'DataGridViewTextBoxColumn45
        '
        DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn45.DefaultCellStyle = DataGridViewCellStyle26
        Me.DataGridViewTextBoxColumn45.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn45.Name = "DataGridViewTextBoxColumn45"
        Me.DataGridViewTextBoxColumn45.ReadOnly = True
        Me.DataGridViewTextBoxColumn45.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn46
        '
        DataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn46.DefaultCellStyle = DataGridViewCellStyle27
        Me.DataGridViewTextBoxColumn46.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn46.Name = "DataGridViewTextBoxColumn46"
        Me.DataGridViewTextBoxColumn46.ReadOnly = True
        Me.DataGridViewTextBoxColumn46.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn46.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn46.Width = 50
        '
        'DataGridViewTextBoxColumn47
        '
        DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle28.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn47.DefaultCellStyle = DataGridViewCellStyle28
        Me.DataGridViewTextBoxColumn47.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn47.Name = "DataGridViewTextBoxColumn47"
        Me.DataGridViewTextBoxColumn47.ReadOnly = True
        Me.DataGridViewTextBoxColumn47.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn47.Width = 80
        '
        'DataGridViewTextBoxColumn48
        '
        DataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn48.DefaultCellStyle = DataGridViewCellStyle29
        Me.DataGridViewTextBoxColumn48.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn48.Name = "DataGridViewTextBoxColumn48"
        Me.DataGridViewTextBoxColumn48.ReadOnly = True
        Me.DataGridViewTextBoxColumn48.Width = 50
        '
        'dgvDetect13
        '
        Me.dgvDetect13.AllowUserToAddRows = False
        Me.dgvDetect13.AllowUserToDeleteRows = False
        Me.dgvDetect13.AllowUserToResizeRows = False
        Me.dgvDetect13.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect13.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle31
        Me.dgvDetect13.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect13.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn49, Me.DataGridViewTextBoxColumn50, Me.DataGridViewTextBoxColumn51, Me.DataGridViewTextBoxColumn52})
        DataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle36.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle36.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle36.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle36.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle36.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect13.DefaultCellStyle = DataGridViewCellStyle36
        Me.dgvDetect13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect13.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect13.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect13.MultiSelect = False
        Me.dgvDetect13.Name = "dgvDetect13"
        Me.dgvDetect13.ReadOnly = True
        Me.dgvDetect13.RowHeadersVisible = False
        Me.dgvDetect13.RowTemplate.Height = 23
        Me.dgvDetect13.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect13.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect13.TabIndex = 51
        '
        'DataGridViewTextBoxColumn49
        '
        DataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn49.DefaultCellStyle = DataGridViewCellStyle32
        Me.DataGridViewTextBoxColumn49.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn49.Name = "DataGridViewTextBoxColumn49"
        Me.DataGridViewTextBoxColumn49.ReadOnly = True
        Me.DataGridViewTextBoxColumn49.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn50
        '
        DataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn50.DefaultCellStyle = DataGridViewCellStyle33
        Me.DataGridViewTextBoxColumn50.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn50.Name = "DataGridViewTextBoxColumn50"
        Me.DataGridViewTextBoxColumn50.ReadOnly = True
        Me.DataGridViewTextBoxColumn50.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn50.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn50.Width = 50
        '
        'DataGridViewTextBoxColumn51
        '
        DataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle34.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn51.DefaultCellStyle = DataGridViewCellStyle34
        Me.DataGridViewTextBoxColumn51.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn51.Name = "DataGridViewTextBoxColumn51"
        Me.DataGridViewTextBoxColumn51.ReadOnly = True
        Me.DataGridViewTextBoxColumn51.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn51.Width = 80
        '
        'DataGridViewTextBoxColumn52
        '
        DataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn52.DefaultCellStyle = DataGridViewCellStyle35
        Me.DataGridViewTextBoxColumn52.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn52.Name = "DataGridViewTextBoxColumn52"
        Me.DataGridViewTextBoxColumn52.ReadOnly = True
        Me.DataGridViewTextBoxColumn52.Width = 50
        '
        'dgvDetect14
        '
        Me.dgvDetect14.AllowUserToAddRows = False
        Me.dgvDetect14.AllowUserToDeleteRows = False
        Me.dgvDetect14.AllowUserToResizeRows = False
        Me.dgvDetect14.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle37.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle37.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle37.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect14.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle37
        Me.dgvDetect14.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect14.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn53, Me.DataGridViewTextBoxColumn54, Me.DataGridViewTextBoxColumn55, Me.DataGridViewTextBoxColumn56})
        DataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle42.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle42.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle42.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle42.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle42.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle42.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect14.DefaultCellStyle = DataGridViewCellStyle42
        Me.dgvDetect14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect14.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect14.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect14.MultiSelect = False
        Me.dgvDetect14.Name = "dgvDetect14"
        Me.dgvDetect14.ReadOnly = True
        Me.dgvDetect14.RowHeadersVisible = False
        Me.dgvDetect14.RowTemplate.Height = 23
        Me.dgvDetect14.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect14.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect14.TabIndex = 50
        '
        'DataGridViewTextBoxColumn53
        '
        DataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn53.DefaultCellStyle = DataGridViewCellStyle38
        Me.DataGridViewTextBoxColumn53.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn53.Name = "DataGridViewTextBoxColumn53"
        Me.DataGridViewTextBoxColumn53.ReadOnly = True
        Me.DataGridViewTextBoxColumn53.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn54
        '
        DataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn54.DefaultCellStyle = DataGridViewCellStyle39
        Me.DataGridViewTextBoxColumn54.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn54.Name = "DataGridViewTextBoxColumn54"
        Me.DataGridViewTextBoxColumn54.ReadOnly = True
        Me.DataGridViewTextBoxColumn54.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn54.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn54.Width = 50
        '
        'DataGridViewTextBoxColumn55
        '
        DataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle40.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn55.DefaultCellStyle = DataGridViewCellStyle40
        Me.DataGridViewTextBoxColumn55.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn55.Name = "DataGridViewTextBoxColumn55"
        Me.DataGridViewTextBoxColumn55.ReadOnly = True
        Me.DataGridViewTextBoxColumn55.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn55.Width = 80
        '
        'DataGridViewTextBoxColumn56
        '
        DataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn56.DefaultCellStyle = DataGridViewCellStyle41
        Me.DataGridViewTextBoxColumn56.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn56.Name = "DataGridViewTextBoxColumn56"
        Me.DataGridViewTextBoxColumn56.ReadOnly = True
        Me.DataGridViewTextBoxColumn56.Width = 50
        '
        'dgvDetect15
        '
        Me.dgvDetect15.AllowUserToAddRows = False
        Me.dgvDetect15.AllowUserToDeleteRows = False
        Me.dgvDetect15.AllowUserToResizeRows = False
        Me.dgvDetect15.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle43.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle43.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle43.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle43.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle43.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle43.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect15.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle43
        Me.dgvDetect15.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect15.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn57, Me.DataGridViewTextBoxColumn58, Me.DataGridViewTextBoxColumn59, Me.DataGridViewTextBoxColumn60})
        DataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle48.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle48.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle48.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle48.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle48.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle48.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect15.DefaultCellStyle = DataGridViewCellStyle48
        Me.dgvDetect15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect15.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect15.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect15.MultiSelect = False
        Me.dgvDetect15.Name = "dgvDetect15"
        Me.dgvDetect15.ReadOnly = True
        Me.dgvDetect15.RowHeadersVisible = False
        Me.dgvDetect15.RowTemplate.Height = 23
        Me.dgvDetect15.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect15.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect15.TabIndex = 49
        '
        'DataGridViewTextBoxColumn57
        '
        DataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn57.DefaultCellStyle = DataGridViewCellStyle44
        Me.DataGridViewTextBoxColumn57.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn57.Name = "DataGridViewTextBoxColumn57"
        Me.DataGridViewTextBoxColumn57.ReadOnly = True
        Me.DataGridViewTextBoxColumn57.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn58
        '
        DataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn58.DefaultCellStyle = DataGridViewCellStyle45
        Me.DataGridViewTextBoxColumn58.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn58.Name = "DataGridViewTextBoxColumn58"
        Me.DataGridViewTextBoxColumn58.ReadOnly = True
        Me.DataGridViewTextBoxColumn58.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn58.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn58.Width = 50
        '
        'DataGridViewTextBoxColumn59
        '
        DataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle46.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn59.DefaultCellStyle = DataGridViewCellStyle46
        Me.DataGridViewTextBoxColumn59.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn59.Name = "DataGridViewTextBoxColumn59"
        Me.DataGridViewTextBoxColumn59.ReadOnly = True
        Me.DataGridViewTextBoxColumn59.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn59.Width = 80
        '
        'DataGridViewTextBoxColumn60
        '
        DataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn60.DefaultCellStyle = DataGridViewCellStyle47
        Me.DataGridViewTextBoxColumn60.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn60.Name = "DataGridViewTextBoxColumn60"
        Me.DataGridViewTextBoxColumn60.ReadOnly = True
        Me.DataGridViewTextBoxColumn60.Width = 50
        '
        'dgvDetect4
        '
        Me.dgvDetect4.AllowUserToAddRows = False
        Me.dgvDetect4.AllowUserToDeleteRows = False
        Me.dgvDetect4.AllowUserToResizeRows = False
        Me.dgvDetect4.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle49.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle49.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle49.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle49.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle49.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle49.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect4.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle49
        Me.dgvDetect4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect4.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn13, Me.DataGridViewTextBoxColumn14, Me.DataGridViewTextBoxColumn15, Me.DataGridViewTextBoxColumn16})
        DataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle54.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle54.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle54.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle54.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle54.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle54.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect4.DefaultCellStyle = DataGridViewCellStyle54
        Me.dgvDetect4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect4.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect4.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect4.MultiSelect = False
        Me.dgvDetect4.Name = "dgvDetect4"
        Me.dgvDetect4.ReadOnly = True
        Me.dgvDetect4.RowHeadersVisible = False
        Me.dgvDetect4.RowTemplate.Height = 23
        Me.dgvDetect4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect4.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect4.TabIndex = 48
        '
        'DataGridViewTextBoxColumn13
        '
        DataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle50
        Me.DataGridViewTextBoxColumn13.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn14
        '
        DataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn14.DefaultCellStyle = DataGridViewCellStyle51
        Me.DataGridViewTextBoxColumn14.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn14.Width = 50
        '
        'DataGridViewTextBoxColumn15
        '
        DataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle52.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn15.DefaultCellStyle = DataGridViewCellStyle52
        Me.DataGridViewTextBoxColumn15.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn15.Width = 80
        '
        'DataGridViewTextBoxColumn16
        '
        DataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn16.DefaultCellStyle = DataGridViewCellStyle53
        Me.DataGridViewTextBoxColumn16.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.Width = 50
        '
        'dgvDetect5
        '
        Me.dgvDetect5.AllowUserToAddRows = False
        Me.dgvDetect5.AllowUserToDeleteRows = False
        Me.dgvDetect5.AllowUserToResizeRows = False
        Me.dgvDetect5.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle55.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle55.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle55.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle55.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle55.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle55.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect5.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle55
        Me.dgvDetect5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect5.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn17, Me.DataGridViewTextBoxColumn18, Me.DataGridViewTextBoxColumn19, Me.DataGridViewTextBoxColumn20})
        DataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle60.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle60.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle60.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle60.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle60.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle60.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect5.DefaultCellStyle = DataGridViewCellStyle60
        Me.dgvDetect5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect5.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect5.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect5.MultiSelect = False
        Me.dgvDetect5.Name = "dgvDetect5"
        Me.dgvDetect5.ReadOnly = True
        Me.dgvDetect5.RowHeadersVisible = False
        Me.dgvDetect5.RowTemplate.Height = 23
        Me.dgvDetect5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect5.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect5.TabIndex = 47
        '
        'DataGridViewTextBoxColumn17
        '
        DataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn17.DefaultCellStyle = DataGridViewCellStyle56
        Me.DataGridViewTextBoxColumn17.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn18
        '
        DataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn18.DefaultCellStyle = DataGridViewCellStyle57
        Me.DataGridViewTextBoxColumn18.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn18.Width = 50
        '
        'DataGridViewTextBoxColumn19
        '
        DataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle58.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn19.DefaultCellStyle = DataGridViewCellStyle58
        Me.DataGridViewTextBoxColumn19.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        Me.DataGridViewTextBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn19.Width = 80
        '
        'DataGridViewTextBoxColumn20
        '
        DataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn20.DefaultCellStyle = DataGridViewCellStyle59
        Me.DataGridViewTextBoxColumn20.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.ReadOnly = True
        Me.DataGridViewTextBoxColumn20.Width = 50
        '
        'dgvDetect6
        '
        Me.dgvDetect6.AllowUserToAddRows = False
        Me.dgvDetect6.AllowUserToDeleteRows = False
        Me.dgvDetect6.AllowUserToResizeRows = False
        Me.dgvDetect6.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle61.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle61.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle61.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle61.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle61.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle61.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect6.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle61
        Me.dgvDetect6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect6.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn21, Me.DataGridViewTextBoxColumn22, Me.DataGridViewTextBoxColumn23, Me.DataGridViewTextBoxColumn24})
        DataGridViewCellStyle66.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle66.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle66.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle66.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle66.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle66.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle66.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect6.DefaultCellStyle = DataGridViewCellStyle66
        Me.dgvDetect6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect6.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect6.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect6.MultiSelect = False
        Me.dgvDetect6.Name = "dgvDetect6"
        Me.dgvDetect6.ReadOnly = True
        Me.dgvDetect6.RowHeadersVisible = False
        Me.dgvDetect6.RowTemplate.Height = 23
        Me.dgvDetect6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect6.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect6.TabIndex = 46
        '
        'DataGridViewTextBoxColumn21
        '
        DataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn21.DefaultCellStyle = DataGridViewCellStyle62
        Me.DataGridViewTextBoxColumn21.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.ReadOnly = True
        Me.DataGridViewTextBoxColumn21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn22
        '
        DataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn22.DefaultCellStyle = DataGridViewCellStyle63
        Me.DataGridViewTextBoxColumn22.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.ReadOnly = True
        Me.DataGridViewTextBoxColumn22.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn22.Width = 50
        '
        'DataGridViewTextBoxColumn23
        '
        DataGridViewCellStyle64.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle64.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn23.DefaultCellStyle = DataGridViewCellStyle64
        Me.DataGridViewTextBoxColumn23.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.ReadOnly = True
        Me.DataGridViewTextBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn23.Width = 80
        '
        'DataGridViewTextBoxColumn24
        '
        DataGridViewCellStyle65.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn24.DefaultCellStyle = DataGridViewCellStyle65
        Me.DataGridViewTextBoxColumn24.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.ReadOnly = True
        Me.DataGridViewTextBoxColumn24.Width = 50
        '
        'dgvDetect7
        '
        Me.dgvDetect7.AllowUserToAddRows = False
        Me.dgvDetect7.AllowUserToDeleteRows = False
        Me.dgvDetect7.AllowUserToResizeRows = False
        Me.dgvDetect7.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle67.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle67.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle67.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle67.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle67.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle67.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle67.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect7.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle67
        Me.dgvDetect7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect7.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn25, Me.DataGridViewTextBoxColumn26, Me.DataGridViewTextBoxColumn27, Me.DataGridViewTextBoxColumn28})
        DataGridViewCellStyle72.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle72.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle72.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle72.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle72.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle72.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle72.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect7.DefaultCellStyle = DataGridViewCellStyle72
        Me.dgvDetect7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect7.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect7.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect7.MultiSelect = False
        Me.dgvDetect7.Name = "dgvDetect7"
        Me.dgvDetect7.ReadOnly = True
        Me.dgvDetect7.RowHeadersVisible = False
        Me.dgvDetect7.RowTemplate.Height = 23
        Me.dgvDetect7.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect7.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect7.TabIndex = 45
        '
        'DataGridViewTextBoxColumn25
        '
        DataGridViewCellStyle68.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn25.DefaultCellStyle = DataGridViewCellStyle68
        Me.DataGridViewTextBoxColumn25.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.ReadOnly = True
        Me.DataGridViewTextBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn26
        '
        DataGridViewCellStyle69.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn26.DefaultCellStyle = DataGridViewCellStyle69
        Me.DataGridViewTextBoxColumn26.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.ReadOnly = True
        Me.DataGridViewTextBoxColumn26.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn26.Width = 50
        '
        'DataGridViewTextBoxColumn27
        '
        DataGridViewCellStyle70.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle70.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn27.DefaultCellStyle = DataGridViewCellStyle70
        Me.DataGridViewTextBoxColumn27.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.ReadOnly = True
        Me.DataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn27.Width = 80
        '
        'DataGridViewTextBoxColumn28
        '
        DataGridViewCellStyle71.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn28.DefaultCellStyle = DataGridViewCellStyle71
        Me.DataGridViewTextBoxColumn28.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.ReadOnly = True
        Me.DataGridViewTextBoxColumn28.Width = 50
        '
        'dgvDetect2
        '
        Me.dgvDetect2.AllowUserToAddRows = False
        Me.dgvDetect2.AllowUserToDeleteRows = False
        Me.dgvDetect2.AllowUserToResizeRows = False
        Me.dgvDetect2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle73.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle73.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle73.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle73.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle73.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle73.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle73.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle73
        Me.dgvDetect2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8})
        DataGridViewCellStyle78.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle78.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle78.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle78.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle78.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle78.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle78.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect2.DefaultCellStyle = DataGridViewCellStyle78
        Me.dgvDetect2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect2.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect2.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect2.MultiSelect = False
        Me.dgvDetect2.Name = "dgvDetect2"
        Me.dgvDetect2.ReadOnly = True
        Me.dgvDetect2.RowHeadersVisible = False
        Me.dgvDetect2.RowTemplate.Height = 23
        Me.dgvDetect2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect2.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect2.TabIndex = 44
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle74.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle74
        Me.DataGridViewTextBoxColumn5.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle75.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle75
        Me.DataGridViewTextBoxColumn6.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 50
        '
        'DataGridViewTextBoxColumn7
        '
        DataGridViewCellStyle76.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle76.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle76
        Me.DataGridViewTextBoxColumn7.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Width = 80
        '
        'DataGridViewTextBoxColumn8
        '
        DataGridViewCellStyle77.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle77
        Me.DataGridViewTextBoxColumn8.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 50
        '
        'dgvDetect3
        '
        Me.dgvDetect3.AllowUserToAddRows = False
        Me.dgvDetect3.AllowUserToDeleteRows = False
        Me.dgvDetect3.AllowUserToResizeRows = False
        Me.dgvDetect3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle79.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle79.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle79.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle79.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle79.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle79.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle79.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect3.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle79
        Me.dgvDetect3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12})
        DataGridViewCellStyle84.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle84.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle84.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle84.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle84.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle84.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle84.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect3.DefaultCellStyle = DataGridViewCellStyle84
        Me.dgvDetect3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect3.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect3.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect3.MultiSelect = False
        Me.dgvDetect3.Name = "dgvDetect3"
        Me.dgvDetect3.ReadOnly = True
        Me.dgvDetect3.RowHeadersVisible = False
        Me.dgvDetect3.RowTemplate.Height = 23
        Me.dgvDetect3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect3.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect3.TabIndex = 43
        '
        'DataGridViewTextBoxColumn9
        '
        DataGridViewCellStyle80.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle80
        Me.DataGridViewTextBoxColumn9.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn10
        '
        DataGridViewCellStyle81.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn10.DefaultCellStyle = DataGridViewCellStyle81
        Me.DataGridViewTextBoxColumn10.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn10.Width = 50
        '
        'DataGridViewTextBoxColumn11
        '
        DataGridViewCellStyle82.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle82.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn11.DefaultCellStyle = DataGridViewCellStyle82
        Me.DataGridViewTextBoxColumn11.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn11.Width = 80
        '
        'DataGridViewTextBoxColumn12
        '
        DataGridViewCellStyle83.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle83
        Me.DataGridViewTextBoxColumn12.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 50
        '
        'dgvDetect1
        '
        Me.dgvDetect1.AllowUserToAddRows = False
        Me.dgvDetect1.AllowUserToDeleteRows = False
        Me.dgvDetect1.AllowUserToResizeRows = False
        Me.dgvDetect1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvDetect1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle85.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle85.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle85.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle85.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle85.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle85.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle85.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetect1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle85
        Me.dgvDetect1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetect1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        DataGridViewCellStyle90.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle90.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle90.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle90.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle90.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle90.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle90.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetect1.DefaultCellStyle = DataGridViewCellStyle90
        Me.dgvDetect1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetect1.Location = New System.Drawing.Point(2, 15)
        Me.dgvDetect1.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetect1.MultiSelect = False
        Me.dgvDetect1.Name = "dgvDetect1"
        Me.dgvDetect1.ReadOnly = True
        Me.dgvDetect1.RowHeadersVisible = False
        Me.dgvDetect1.RowTemplate.Height = 23
        Me.dgvDetect1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetect1.Size = New System.Drawing.Size(172, 96)
        Me.dgvDetect1.TabIndex = 42
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle86.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle86
        Me.DataGridViewTextBoxColumn1.HeaderText = "Test Item"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle87.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle87
        Me.DataGridViewTextBoxColumn2.HeaderText = "LL"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle88.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle88.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle88
        Me.DataGridViewTextBoxColumn3.HeaderText = "Meas"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 80
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle89.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle89
        Me.DataGridViewTextBoxColumn4.HeaderText = "HL"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 50
        '
        'grpTestData
        '
        Me.grpTestData.Controls.Add(Me.totTestTimeOut)
        Me.grpTestData.Controls.Add(Me.dgvSlotData15)
        Me.grpTestData.Controls.Add(Me.dgvSlotData14)
        Me.grpTestData.Controls.Add(Me.dgvSlotData13)
        Me.grpTestData.Controls.Add(Me.dgvSlotData12)
        Me.grpTestData.Controls.Add(Me.dgvSlotData11)
        Me.grpTestData.Controls.Add(Me.dgvSlotData10)
        Me.grpTestData.Controls.Add(Me.dgvSlotData9)
        Me.grpTestData.Controls.Add(Me.dgvSlotData8)
        Me.grpTestData.Controls.Add(Me.dgvSlotData7)
        Me.grpTestData.Controls.Add(Me.dgvSlotData6)
        Me.grpTestData.Controls.Add(Me.dgvSlotData5)
        Me.grpTestData.Controls.Add(Me.dgvSlotData4)
        Me.grpTestData.Controls.Add(Me.dgvSlotData3)
        Me.grpTestData.Controls.Add(Me.dgvSlotData2)
        Me.grpTestData.Controls.Add(Me.dgvSlotData1)
        Me.grpTestData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpTestData.Location = New System.Drawing.Point(182, 2)
        Me.grpTestData.Margin = New System.Windows.Forms.Padding(2)
        Me.grpTestData.Name = "grpTestData"
        Me.grpTestData.Padding = New System.Windows.Forms.Padding(2)
        Me.tlpInterface.SetRowSpan(Me.grpTestData, 2)
        Me.grpTestData.Size = New System.Drawing.Size(704, 481)
        Me.grpTestData.TabIndex = 42
        Me.grpTestData.TabStop = False
        Me.grpTestData.Text = "Test Data"
        '
        'totTestTimeOut
        '
        Me.totTestTimeOut.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.totTestTimeOut.AutoSize = True
        Me.totTestTimeOut.DisplayMinutes = False
        Me.totTestTimeOut.Location = New System.Drawing.Point(198, 17)
        Me.totTestTimeOut.Margin = New System.Windows.Forms.Padding(2)
        Me.totTestTimeOut.MinutesDelay = 1.0R
        Me.totTestTimeOut.Name = "totTestTimeOut"
        Me.totTestTimeOut.SecondsDelay = 60
        Me.totTestTimeOut.Size = New System.Drawing.Size(474, 265)
        Me.totTestTimeOut.TabIndex = 28
        Me.totTestTimeOut.Visible = False
        '
        'dgvSlotData15
        '
        Me.dgvSlotData15.AllowUserToAddRows = False
        Me.dgvSlotData15.AllowUserToDeleteRows = False
        Me.dgvSlotData15.AllowUserToResizeRows = False
        Me.dgvSlotData15.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle91.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle91.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle91.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle91.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle91.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle91.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle91.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData15.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle91
        Me.dgvSlotData15.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData15.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData15.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData15.MultiSelect = False
        Me.dgvSlotData15.Name = "dgvSlotData15"
        Me.dgvSlotData15.ReadOnly = True
        Me.dgvSlotData15.RowHeadersVisible = False
        Me.dgvSlotData15.RowTemplate.Height = 23
        Me.dgvSlotData15.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData15.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData15.TabIndex = 55
        '
        'dgvSlotData14
        '
        Me.dgvSlotData14.AllowUserToAddRows = False
        Me.dgvSlotData14.AllowUserToDeleteRows = False
        Me.dgvSlotData14.AllowUserToResizeRows = False
        Me.dgvSlotData14.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle92.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle92.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle92.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle92.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle92.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle92.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle92.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData14.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle92
        Me.dgvSlotData14.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData14.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData14.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData14.MultiSelect = False
        Me.dgvSlotData14.Name = "dgvSlotData14"
        Me.dgvSlotData14.ReadOnly = True
        Me.dgvSlotData14.RowHeadersVisible = False
        Me.dgvSlotData14.RowTemplate.Height = 23
        Me.dgvSlotData14.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData14.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData14.TabIndex = 54
        '
        'dgvSlotData13
        '
        Me.dgvSlotData13.AllowUserToAddRows = False
        Me.dgvSlotData13.AllowUserToDeleteRows = False
        Me.dgvSlotData13.AllowUserToResizeRows = False
        Me.dgvSlotData13.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle93.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle93.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle93.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle93.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle93.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle93.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle93.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData13.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle93
        Me.dgvSlotData13.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData13.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData13.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData13.MultiSelect = False
        Me.dgvSlotData13.Name = "dgvSlotData13"
        Me.dgvSlotData13.ReadOnly = True
        Me.dgvSlotData13.RowHeadersVisible = False
        Me.dgvSlotData13.RowTemplate.Height = 23
        Me.dgvSlotData13.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData13.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData13.TabIndex = 53
        '
        'dgvSlotData12
        '
        Me.dgvSlotData12.AllowUserToAddRows = False
        Me.dgvSlotData12.AllowUserToDeleteRows = False
        Me.dgvSlotData12.AllowUserToResizeRows = False
        Me.dgvSlotData12.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle94.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle94.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle94.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle94.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle94.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle94.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle94.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData12.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle94
        Me.dgvSlotData12.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData12.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData12.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData12.MultiSelect = False
        Me.dgvSlotData12.Name = "dgvSlotData12"
        Me.dgvSlotData12.ReadOnly = True
        Me.dgvSlotData12.RowHeadersVisible = False
        Me.dgvSlotData12.RowTemplate.Height = 23
        Me.dgvSlotData12.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData12.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData12.TabIndex = 52
        '
        'dgvSlotData11
        '
        Me.dgvSlotData11.AllowUserToAddRows = False
        Me.dgvSlotData11.AllowUserToDeleteRows = False
        Me.dgvSlotData11.AllowUserToResizeRows = False
        Me.dgvSlotData11.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle95.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle95.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle95.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle95.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle95.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle95.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle95.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData11.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle95
        Me.dgvSlotData11.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData11.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData11.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData11.MultiSelect = False
        Me.dgvSlotData11.Name = "dgvSlotData11"
        Me.dgvSlotData11.ReadOnly = True
        Me.dgvSlotData11.RowHeadersVisible = False
        Me.dgvSlotData11.RowTemplate.Height = 23
        Me.dgvSlotData11.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData11.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData11.TabIndex = 51
        '
        'dgvSlotData10
        '
        Me.dgvSlotData10.AllowUserToAddRows = False
        Me.dgvSlotData10.AllowUserToDeleteRows = False
        Me.dgvSlotData10.AllowUserToResizeRows = False
        Me.dgvSlotData10.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle96.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle96.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle96.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle96.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle96.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle96.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle96.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData10.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle96
        Me.dgvSlotData10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData10.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData10.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData10.MultiSelect = False
        Me.dgvSlotData10.Name = "dgvSlotData10"
        Me.dgvSlotData10.ReadOnly = True
        Me.dgvSlotData10.RowHeadersVisible = False
        Me.dgvSlotData10.RowTemplate.Height = 23
        Me.dgvSlotData10.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData10.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData10.TabIndex = 50
        '
        'dgvSlotData9
        '
        Me.dgvSlotData9.AllowUserToAddRows = False
        Me.dgvSlotData9.AllowUserToDeleteRows = False
        Me.dgvSlotData9.AllowUserToResizeRows = False
        Me.dgvSlotData9.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle97.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle97.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle97.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle97.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle97.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle97.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle97.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData9.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle97
        Me.dgvSlotData9.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData9.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData9.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData9.MultiSelect = False
        Me.dgvSlotData9.Name = "dgvSlotData9"
        Me.dgvSlotData9.ReadOnly = True
        Me.dgvSlotData9.RowHeadersVisible = False
        Me.dgvSlotData9.RowTemplate.Height = 23
        Me.dgvSlotData9.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData9.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData9.TabIndex = 49
        '
        'dgvSlotData8
        '
        Me.dgvSlotData8.AllowUserToAddRows = False
        Me.dgvSlotData8.AllowUserToDeleteRows = False
        Me.dgvSlotData8.AllowUserToResizeRows = False
        Me.dgvSlotData8.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle98.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle98.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle98.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle98.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle98.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle98.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle98.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData8.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle98
        Me.dgvSlotData8.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData8.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData8.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData8.MultiSelect = False
        Me.dgvSlotData8.Name = "dgvSlotData8"
        Me.dgvSlotData8.ReadOnly = True
        Me.dgvSlotData8.RowHeadersVisible = False
        Me.dgvSlotData8.RowTemplate.Height = 23
        Me.dgvSlotData8.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData8.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData8.TabIndex = 48
        '
        'dgvSlotData7
        '
        Me.dgvSlotData7.AllowUserToAddRows = False
        Me.dgvSlotData7.AllowUserToDeleteRows = False
        Me.dgvSlotData7.AllowUserToResizeRows = False
        Me.dgvSlotData7.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle99.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle99.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle99.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle99.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle99.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle99.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle99.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData7.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle99
        Me.dgvSlotData7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData7.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData7.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData7.MultiSelect = False
        Me.dgvSlotData7.Name = "dgvSlotData7"
        Me.dgvSlotData7.ReadOnly = True
        Me.dgvSlotData7.RowHeadersVisible = False
        Me.dgvSlotData7.RowTemplate.Height = 23
        Me.dgvSlotData7.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData7.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData7.TabIndex = 47
        '
        'dgvSlotData6
        '
        Me.dgvSlotData6.AllowUserToAddRows = False
        Me.dgvSlotData6.AllowUserToDeleteRows = False
        Me.dgvSlotData6.AllowUserToResizeRows = False
        Me.dgvSlotData6.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle100.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle100.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle100.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle100.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle100.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle100.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle100.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData6.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle100
        Me.dgvSlotData6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData6.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData6.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData6.MultiSelect = False
        Me.dgvSlotData6.Name = "dgvSlotData6"
        Me.dgvSlotData6.ReadOnly = True
        Me.dgvSlotData6.RowHeadersVisible = False
        Me.dgvSlotData6.RowTemplate.Height = 23
        Me.dgvSlotData6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData6.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData6.TabIndex = 46
        '
        'dgvSlotData5
        '
        Me.dgvSlotData5.AllowUserToAddRows = False
        Me.dgvSlotData5.AllowUserToDeleteRows = False
        Me.dgvSlotData5.AllowUserToResizeRows = False
        Me.dgvSlotData5.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle101.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle101.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle101.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle101.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle101.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle101.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle101.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData5.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle101
        Me.dgvSlotData5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData5.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData5.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData5.MultiSelect = False
        Me.dgvSlotData5.Name = "dgvSlotData5"
        Me.dgvSlotData5.ReadOnly = True
        Me.dgvSlotData5.RowHeadersVisible = False
        Me.dgvSlotData5.RowTemplate.Height = 23
        Me.dgvSlotData5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData5.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData5.TabIndex = 45
        '
        'dgvSlotData4
        '
        Me.dgvSlotData4.AllowUserToAddRows = False
        Me.dgvSlotData4.AllowUserToDeleteRows = False
        Me.dgvSlotData4.AllowUserToResizeRows = False
        Me.dgvSlotData4.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle102.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle102.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle102.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle102.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle102.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle102.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle102.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData4.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle102
        Me.dgvSlotData4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData4.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData4.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData4.MultiSelect = False
        Me.dgvSlotData4.Name = "dgvSlotData4"
        Me.dgvSlotData4.ReadOnly = True
        Me.dgvSlotData4.RowHeadersVisible = False
        Me.dgvSlotData4.RowTemplate.Height = 23
        Me.dgvSlotData4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData4.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData4.TabIndex = 44
        '
        'dgvSlotData3
        '
        Me.dgvSlotData3.AllowUserToAddRows = False
        Me.dgvSlotData3.AllowUserToDeleteRows = False
        Me.dgvSlotData3.AllowUserToResizeRows = False
        Me.dgvSlotData3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle103.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle103.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle103.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle103.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle103.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle103.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle103.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData3.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle103
        Me.dgvSlotData3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData3.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData3.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData3.MultiSelect = False
        Me.dgvSlotData3.Name = "dgvSlotData3"
        Me.dgvSlotData3.ReadOnly = True
        Me.dgvSlotData3.RowHeadersVisible = False
        Me.dgvSlotData3.RowTemplate.Height = 23
        Me.dgvSlotData3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData3.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData3.TabIndex = 43
        '
        'dgvSlotData2
        '
        Me.dgvSlotData2.AllowUserToAddRows = False
        Me.dgvSlotData2.AllowUserToDeleteRows = False
        Me.dgvSlotData2.AllowUserToResizeRows = False
        Me.dgvSlotData2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle104.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle104.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle104.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle104.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle104.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle104.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle104.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle104
        Me.dgvSlotData2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData2.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData2.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData2.MultiSelect = False
        Me.dgvSlotData2.Name = "dgvSlotData2"
        Me.dgvSlotData2.ReadOnly = True
        Me.dgvSlotData2.RowHeadersVisible = False
        Me.dgvSlotData2.RowTemplate.Height = 23
        Me.dgvSlotData2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData2.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData2.TabIndex = 42
        '
        'dgvSlotData1
        '
        Me.dgvSlotData1.AllowUserToAddRows = False
        Me.dgvSlotData1.AllowUserToDeleteRows = False
        Me.dgvSlotData1.AllowUserToResizeRows = False
        Me.dgvSlotData1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlotData1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle105.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle105.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle105.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle105.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle105.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle105.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle105.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlotData1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle105
        Me.dgvSlotData1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlotData1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlotData1.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlotData1.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlotData1.MultiSelect = False
        Me.dgvSlotData1.Name = "dgvSlotData1"
        Me.dgvSlotData1.ReadOnly = True
        Me.dgvSlotData1.RowHeadersVisible = False
        Me.dgvSlotData1.RowTemplate.Height = 23
        Me.dgvSlotData1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlotData1.Size = New System.Drawing.Size(700, 464)
        Me.dgvSlotData1.TabIndex = 41
        '
        'grpSlots
        '
        Me.grpSlots.Controls.Add(Me.dgvSlots)
        Me.grpSlots.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpSlots.Location = New System.Drawing.Point(2, 2)
        Me.grpSlots.Margin = New System.Windows.Forms.Padding(2)
        Me.grpSlots.Name = "grpSlots"
        Me.grpSlots.Padding = New System.Windows.Forms.Padding(2)
        Me.grpSlots.Size = New System.Drawing.Size(176, 364)
        Me.grpSlots.TabIndex = 41
        Me.grpSlots.TabStop = False
        Me.grpSlots.Text = "Slots Status"
        '
        'dgvSlots
        '
        Me.dgvSlots.AllowUserToAddRows = False
        Me.dgvSlots.AllowUserToDeleteRows = False
        Me.dgvSlots.AllowUserToResizeRows = False
        Me.dgvSlots.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSlots.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle106.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle106.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle106.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle106.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle106.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle106.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle106.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSlots.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle106
        Me.dgvSlots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSlots.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSlot, Me.colSN, Me.colStatus})
        DataGridViewCellStyle110.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle110.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle110.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle110.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle110.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle110.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle110.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSlots.DefaultCellStyle = DataGridViewCellStyle110
        Me.dgvSlots.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSlots.Location = New System.Drawing.Point(2, 15)
        Me.dgvSlots.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSlots.MultiSelect = False
        Me.dgvSlots.Name = "dgvSlots"
        Me.dgvSlots.ReadOnly = True
        Me.dgvSlots.RowHeadersVisible = False
        Me.dgvSlots.RowTemplate.Height = 23
        Me.dgvSlots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSlots.Size = New System.Drawing.Size(172, 347)
        Me.dgvSlots.TabIndex = 40
        '
        'colSlot
        '
        DataGridViewCellStyle107.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colSlot.DefaultCellStyle = DataGridViewCellStyle107
        Me.colSlot.HeaderText = "Slot"
        Me.colSlot.Name = "colSlot"
        Me.colSlot.ReadOnly = True
        Me.colSlot.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colSlot.Width = 50
        '
        'colSN
        '
        DataGridViewCellStyle108.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colSN.DefaultCellStyle = DataGridViewCellStyle108
        Me.colSN.HeaderText = "Serial Number"
        Me.colSN.Name = "colSN"
        Me.colSN.ReadOnly = True
        Me.colSN.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colSN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colSN.Width = 150
        '
        'colStatus
        '
        DataGridViewCellStyle109.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colStatus.DefaultCellStyle = DataGridViewCellStyle109
        Me.colStatus.HeaderText = "Status"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.ReadOnly = True
        Me.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colStatus.Width = 80
        '
        'tlpInterface
        '
        Me.tlpInterface.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpInterface.ColumnCount = 2
        Me.tlpInterface.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.tlpInterface.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpInterface.Controls.Add(Me.grpSlots, 0, 0)
        Me.tlpInterface.Controls.Add(Me.grpTestData, 1, 0)
        Me.tlpInterface.Controls.Add(Me.grpDetect, 0, 1)
        Me.tlpInterface.Location = New System.Drawing.Point(7, 55)
        Me.tlpInterface.Margin = New System.Windows.Forms.Padding(2)
        Me.tlpInterface.Name = "tlpInterface"
        Me.tlpInterface.RowCount = 2
        Me.tlpInterface.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpInterface.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.tlpInterface.Size = New System.Drawing.Size(888, 485)
        Me.tlpInterface.TabIndex = 44
        '
        'OpenParametersToolStripMenuItem
        '
        Me.OpenParametersToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenParametersToolStripMenuItem1, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.OpenParametersToolStripMenuItem.Name = "OpenParametersToolStripMenuItem"
        Me.OpenParametersToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.OpenParametersToolStripMenuItem.Text = "File"
        '
        'OpenParametersToolStripMenuItem1
        '
        Me.OpenParametersToolStripMenuItem1.Image = CType(resources.GetObject("OpenParametersToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.OpenParametersToolStripMenuItem1.Name = "OpenParametersToolStripMenuItem1"
        Me.OpenParametersToolStripMenuItem1.Size = New System.Drawing.Size(158, 22)
        Me.OpenParametersToolStripMenuItem1.Text = "Open Parameters"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(155, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Image = CType(resources.GetObject("ExitToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ControlToolStripMenuItem
        '
        Me.ControlToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TurnOnChamberToolStripMenuItem, Me.TurnOffChamberToolStripMenuItem, Me.ToolStripMenuItem1, Me.TurnOnAllPowerToolStripMenuItem, Me.TurnOffAllPowerToolStripMenuItem, Me.ToolStripMenuItem2, Me.TurnOnAllFansToolStripMenuItem, Me.TurnOffAllFansToolStripMenuItem, Me.ToolStripMenuItem3, Me.tsmTurnOnAllPowerSuppliers, Me.tsmTurnOffAllPowerSuppliers, Me.ToolStripSeparator7, Me.tsmTurnOnAnton, Me.tsmTurnOffAnton, Me.ToolStripSeparator9, Me.tsmChangeGW})
        Me.ControlToolStripMenuItem.Name = "ControlToolStripMenuItem"
        Me.ControlToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.ControlToolStripMenuItem.Text = "Control"
        '
        'TurnOnChamberToolStripMenuItem
        '
        Me.TurnOnChamberToolStripMenuItem.Image = CType(resources.GetObject("TurnOnChamberToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TurnOnChamberToolStripMenuItem.Name = "TurnOnChamberToolStripMenuItem"
        Me.TurnOnChamberToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.TurnOnChamberToolStripMenuItem.Text = "Turn On Chamber"
        '
        'TurnOffChamberToolStripMenuItem
        '
        Me.TurnOffChamberToolStripMenuItem.Image = CType(resources.GetObject("TurnOffChamberToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TurnOffChamberToolStripMenuItem.Name = "TurnOffChamberToolStripMenuItem"
        Me.TurnOffChamberToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.TurnOffChamberToolStripMenuItem.Text = "Turn Off Chamber"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(205, 6)
        '
        'TurnOnAllPowerToolStripMenuItem
        '
        Me.TurnOnAllPowerToolStripMenuItem.Image = CType(resources.GetObject("TurnOnAllPowerToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TurnOnAllPowerToolStripMenuItem.Name = "TurnOnAllPowerToolStripMenuItem"
        Me.TurnOnAllPowerToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.TurnOnAllPowerToolStripMenuItem.Text = "Turn On All Power Switch"
        '
        'TurnOffAllPowerToolStripMenuItem
        '
        Me.TurnOffAllPowerToolStripMenuItem.Image = CType(resources.GetObject("TurnOffAllPowerToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TurnOffAllPowerToolStripMenuItem.Name = "TurnOffAllPowerToolStripMenuItem"
        Me.TurnOffAllPowerToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.TurnOffAllPowerToolStripMenuItem.Text = "Turn Off All Power Switch"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(205, 6)
        '
        'TurnOnAllFansToolStripMenuItem
        '
        Me.TurnOnAllFansToolStripMenuItem.Image = CType(resources.GetObject("TurnOnAllFansToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TurnOnAllFansToolStripMenuItem.Name = "TurnOnAllFansToolStripMenuItem"
        Me.TurnOnAllFansToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.TurnOnAllFansToolStripMenuItem.Text = "Turn On All Fan Switch"
        '
        'TurnOffAllFansToolStripMenuItem
        '
        Me.TurnOffAllFansToolStripMenuItem.Image = CType(resources.GetObject("TurnOffAllFansToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TurnOffAllFansToolStripMenuItem.Name = "TurnOffAllFansToolStripMenuItem"
        Me.TurnOffAllFansToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.TurnOffAllFansToolStripMenuItem.Text = "Turn Off All Fan Switch"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(205, 6)
        '
        'tsmTurnOnAllPowerSuppliers
        '
        Me.tsmTurnOnAllPowerSuppliers.Image = CType(resources.GetObject("tsmTurnOnAllPowerSuppliers.Image"), System.Drawing.Image)
        Me.tsmTurnOnAllPowerSuppliers.Name = "tsmTurnOnAllPowerSuppliers"
        Me.tsmTurnOnAllPowerSuppliers.Size = New System.Drawing.Size(208, 22)
        Me.tsmTurnOnAllPowerSuppliers.Text = "Turn On All Power Suppliers"
        '
        'tsmTurnOffAllPowerSuppliers
        '
        Me.tsmTurnOffAllPowerSuppliers.Image = CType(resources.GetObject("tsmTurnOffAllPowerSuppliers.Image"), System.Drawing.Image)
        Me.tsmTurnOffAllPowerSuppliers.Name = "tsmTurnOffAllPowerSuppliers"
        Me.tsmTurnOffAllPowerSuppliers.Size = New System.Drawing.Size(208, 22)
        Me.tsmTurnOffAllPowerSuppliers.Text = "Turn Off All Power Suppliers"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(205, 6)
        '
        'tsmTurnOnAnton
        '
        Me.tsmTurnOnAnton.Image = CType(resources.GetObject("tsmTurnOnAnton.Image"), System.Drawing.Image)
        Me.tsmTurnOnAnton.Name = "tsmTurnOnAnton"
        Me.tsmTurnOnAnton.Size = New System.Drawing.Size(208, 22)
        Me.tsmTurnOnAnton.Text = "Turn On All Anton"
        '
        'tsmTurnOffAnton
        '
        Me.tsmTurnOffAnton.Image = CType(resources.GetObject("tsmTurnOffAnton.Image"), System.Drawing.Image)
        Me.tsmTurnOffAnton.Name = "tsmTurnOffAnton"
        Me.tsmTurnOffAnton.Size = New System.Drawing.Size(208, 22)
        Me.tsmTurnOffAnton.Text = "Turn Off All Anton"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(205, 6)
        '
        'tsmChangeGW
        '
        Me.tsmChangeGW.Name = "tsmChangeGW"
        Me.tsmChangeGW.Size = New System.Drawing.Size(208, 22)
        Me.tsmChangeGW.Text = "ChangeGW"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenParametersToolStripMenuItem, Me.ControlToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(904, 24)
        Me.MenuStrip1.TabIndex = 25
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(904, 631)
        Me.Controls.Add(Me.tlpInterface)
        Me.Controls.Add(Me.TextBoxMessage)
        Me.Controls.Add(Me.BIStatusStrip)
        Me.Controls.Add(Me.BIToolStrip)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(454, 365)
        Me.Name = "frmMain"
        Me.Text = "Burn In (AMS)"
        Me.BIToolStrip.ResumeLayout(False)
        Me.BIToolStrip.PerformLayout()
        Me.BIStatusStrip.ResumeLayout(False)
        Me.BIStatusStrip.PerformLayout()
        Me.grpDetect.ResumeLayout(False)
        CType(Me.dgvDetect8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetect1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTestData.ResumeLayout(False)
        Me.grpTestData.PerformLayout()
        CType(Me.dgvSlotData15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSlotData1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSlots.ResumeLayout(False)
        CType(Me.dgvSlots, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpInterface.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Shared Sub Main()
        Application.EnableVisualStyles()
        Application.Run(New frmMain)
    End Sub
    Friend WithEvents totTestTimeOut As Viper_Burn_In.EventTimer
    Friend WithEvents BIToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbStart As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbStop As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbTurnOffAllFans As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbTurnOnAllPowers As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbTurnOffAllPowers As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbTurnOnChamber As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbTurnOffChamber As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tsTestTimeTextBox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BIStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents tsbTurnOnAllFans As System.Windows.Forms.ToolStripButton
    Friend WithEvents tslMinutes As System.Windows.Forms.ToolStripLabel
    Friend WithEvents TextBoxMessage As System.Windows.Forms.TextBox
    Friend WithEvents grpDetect As System.Windows.Forms.GroupBox
    Friend WithEvents dgvDetect8 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect9 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect10 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn37 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn38 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn39 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn40 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect11 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn41 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn42 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn43 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn44 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect12 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn45 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn46 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn47 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn48 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect13 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn49 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn50 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn51 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn52 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect14 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn53 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn54 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn55 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn56 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect15 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn57 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn58 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn59 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn60 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect4 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect5 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect6 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect7 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect3 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDetect1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpTestData As System.Windows.Forms.GroupBox
    Friend WithEvents dgvSlotData15 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData14 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData13 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData12 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData11 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData10 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData9 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData8 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData7 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData6 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData5 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData4 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData3 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData2 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSlotData1 As System.Windows.Forms.DataGridView
    Friend WithEvents tlpInterface As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpSlots As System.Windows.Forms.GroupBox
    Friend WithEvents dgvSlots As System.Windows.Forms.DataGridView
    Friend WithEvents OpenParametersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenParametersToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ControlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TurnOnChamberToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TurnOffChamberToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TurnOnAllPowerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TurnOffAllPowerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TurnOnAllFansToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TurnOffAllFansToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents colSlot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tssLab1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssLab2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssLab3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssLab4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbTurnOnPS As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbTurnOffPS As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmTurnOnAllPowerSuppliers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmTurnOffAllPowerSuppliers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbTurnOnAnton As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbTurnOffAnton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmTurnOnAnton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmTurnOffAnton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmChangeGW As System.Windows.Forms.ToolStripMenuItem
End Class
