Imports System.Threading

Public Class CustomThread
    Private _ErrorString As String = ""
    Private _ErrorStatus As Boolean = False
    Private _InternalThread As Thread = Nothing
    Private _ReturnObject As Object
    Private _InputParams As Object()
    Private _Process As System.Delegate   'like void (*functionpointer)(void) in C
    ''' <summary>
    ''' Indicates the process that will be runned by the custom thread
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property DelegateProcess() As System.Delegate
        Set(ByVal value As System.Delegate)
            _Process = value
        End Set
    End Property
    Public WriteOnly Property InputParameter() As Object()
        Set(ByVal value As Object())
            _InputParams = value
        End Set
    End Property


    Public ReadOnly Property ReturnParameter() As Object
        Get
            Return _ReturnObject
        End Get
    End Property
    ''' <summary>
    ''' return the status of the process run by the thread
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ErrorStatus()
        Get
            Return _ErrorStatus
        End Get
    End Property
    ''' <summary>
    ''' return the last error message
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ErrorString()
        Get
            Return _ErrorString
        End Get
    End Property
    ''' <summary>
    ''' Clear error string and status 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearError()
        _ErrorString = ""
        _ErrorStatus = False
    End Sub
    Public Overridable Sub Start()
        _InternalThread = New Thread(AddressOf Me.CallProcess)
        _InternalThread.Start()
    End Sub
    Private Sub CallProcess()
        Try
            If _InputParams IsNot Nothing Then
                _ReturnObject = _Process.DynamicInvoke(_InputParams)
            Else
                _ReturnObject = _Process.DynamicInvoke(_InputParams)
            End If
        Catch ex As Exception
            _ErrorString = ex.Message & vbCrLf & ex.InnerException.Message
            _ErrorStatus = True
        End Try
    End Sub
    ''' <summary>
    ''' Return true if the process of the thread is running
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsRunning() As Boolean
        Get
            If _InternalThread IsNot Nothing Then
                Return _InternalThread.IsAlive
            Else
                Return False
            End If
        End Get
    End Property
End Class
