Public Class EventTimer
    Private SW As New Stopwatch
    Private pSecondsDelay As Integer = 60
    Private pDisplayMinutes As Boolean
    Private ptsTextBox As System.Windows.Forms.ToolStripTextBox
    Public Event Timeout()

    Public Property DisplayMinutes() As Boolean
        Get
            Return pDisplayMinutes
        End Get
        Set(ByVal value As Boolean)
            pDisplayMinutes = value
        End Set
    End Property

    Public WriteOnly Property SetTimeIndtsTextBox() As System.Windows.Forms.ToolStripTextBox
        Set(ByVal value As System.Windows.Forms.ToolStripTextBox)
            ptsTextBox = value
        End Set
    End Property
    'Public Overrides Property Text() As String
    '    Get
    '        Return gbxEventTime.Text
    '    End Get
    '    Set(ByVal value As String)
    '        gbxEventTime.Text = value
    '    End Set
    'End Property
    Public Overrides Property Font() As System.Drawing.Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As System.Drawing.Font)
            MyBase.Font = value
            'gbxEventTime.Font = value
            txtEventTimer.Font = value
        End Set
    End Property
    Public ReadOnly Property IsTimedOut() As Boolean
        Get
            Return Val(txtEventTimer.Text) <= 0
        End Get
    End Property
    Public Sub Start()
        SW.Start()
        timer.Start()
    End Sub
    Public Sub Reset()
        Dim IsRunningNow As Boolean = SW.IsRunning
        SW.Reset()
    End Sub
    Public Sub Stoppe()
        SW.Stop()
        timer.Stop()
    End Sub
    Public Property SecondsDelay() As Integer
        Get
            Return pSecondsDelay
        End Get
        Set(ByVal value As Integer)
            pSecondsDelay = value
        End Set
    End Property
    Public Property MinutesDelay() As Double
        Get
            Return Me.SecondsDelay / 60.0
        End Get
        Set(ByVal value As Double)
            If value > 0 Then
                Me.SecondsDelay = value * 60
            End If
        End Set
    End Property



    Private Sub tmrEventTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer.Tick
        Dim SecsRemaining As Integer
        SecsRemaining = pSecondsDelay - SW.Elapsed.TotalSeconds
        If AbortTest = True Then Exit Sub
        If SecsRemaining < 0 Then SecsRemaining = 0
        If pDisplayMinutes Then
            txtEventTimer.Text = VB6.Format(SecsRemaining / 60.0, "0.0")
        Else
            txtEventTimer.Text = SecsRemaining
        End If
        If Not ptsTextBox Is Nothing Then ptsTextBox.Text = txtEventTimer.Text
        If SecsRemaining = 0 Then
            Me.Stoppe()
            TimeDone = True
            RaiseEvent Timeout()
        End If
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
