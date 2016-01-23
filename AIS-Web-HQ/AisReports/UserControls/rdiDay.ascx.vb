Imports System.Data
Partial Class UserControls_rdiDay
    Inherits System.Web.UI.UserControl

    Public Event SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim _isDefaultValue As Boolean = True
    Dim _DefaultValue As String = "0"
    Dim _DefaultDisplay As String = "เลือก"

    Public WriteOnly Property IsNotNull() As Boolean
        Set(ByVal value As Boolean)
            lblValidText.Visible = value
        End Set
    End Property
    Public Overrides ReadOnly Property ClientID() As String
        Get
            Return rdiday.ClientID
        End Get
    End Property
    Public Property IsDefaultValue() As Boolean
        Get
            Return _isDefaultValue
        End Get
        Set(ByVal value As Boolean)
            _isDefaultValue = value
        End Set
    End Property
    Public Property DefaultValue() As String
        Get
            Return _DefaultValue
        End Get
        Set(ByVal value As String)
            _DefaultValue = value
        End Set
    End Property
    Public Property DefaultDisplay() As String
        Get
            Return _DefaultDisplay
        End Get
        Set(ByVal value As String)
            _DefaultDisplay = value
        End Set
    End Property
    Public Property SelectedValue() As String
        Get
            Dim txt As String = ""
            For i = 0 To rdiday.Items.Count - 1
                If rdiday.Items(i).Selected Then
                    If txt = "" Then
                        txt = rdiday.Items(i).Value
                    Else
                        txt = txt & "," & rdiday.Items(i).Value
                    End If
                End If
            Next
            Return txt
        End Get
        Set(ByVal value As String)
            rdiday.SelectedValue = value
        End Set
    End Property
    Public ReadOnly Property SelectedText() As String
        Get
            Return rdiday.SelectedItem.Text
        End Get
    End Property
    Public Property AutoPosBack() As Boolean
        Get
            Return rdiday.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            rdiday.AutoPostBack = value
        End Set
    End Property
    Public Property Width() As Unit
        Get
            Return rdiday.Width
        End Get
        Set(ByVal value As Unit)
            rdiday.Width = value
        End Set
    End Property
    Public Property Enabled() As Boolean
        Get
            Return rdiday.Enabled
        End Get
        Set(ByVal value As Boolean)
            rdiday.Enabled = value
        End Set
    End Property
    Public WriteOnly Property ValidateText() As String
        Set(ByVal value As String)
            lblValidText.Text = value
        End Set
    End Property
    Public Sub ClearCombo()
        rdiday.Items.Clear()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub rdiday_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdiday.SelectedIndexChanged
        RaiseEvent SelectedIndexChanged(sender, e)
    End Sub
End Class
