Partial Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As System.EventArgs
    ) Handles Me.Load

        CheckLoginStatus()

    End Sub


    Private Sub CheckLoginStatus()

        ' PUBLIC

        lnkHome.Visible = True
        lnkAbout.Visible = True
        lnkContact.Visible = True


        ' DEFAULT

        lnkLogin.Visible = True
        lnkUserDashboard.Visible = False
        lnkAdminDashboard.Visible = False
        lnkLogout.Visible = False


        ' NOT LOGGED IN

        If Session("UserID") Is Nothing Then
            Return
        End If


        ' LOGGED IN

        lnkLogin.Visible = False
        lnkLogout.Visible = True


        ' ROLE NOT FOUND

        If Session("UserRole") Is Nothing Then
            Return
        End If


        Dim role As String =
            Session("UserRole").ToString().Trim().ToLower()


        ' ADMIN

        If role = "admin" Then

            lnkAdminDashboard.Visible = True
            lnkUserDashboard.Visible = False


            ' NORMAL USER

        ElseIf role = "user" Then

            lnkUserDashboard.Visible = True
            lnkAdminDashboard.Visible = False

        End If

    End Sub

End Class