Partial Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As System.EventArgs
    ) Handles Me.Load

        CheckLoginStatus()

    End Sub


    '==================================================
    ' CHECK LOGIN STATUS
    '==================================================

    Private Sub CheckLoginStatus()

        '==================================================
        ' PUBLIC NAVIGATION
        '
        ' These are ALWAYS visible:
        '
        ' Home
        ' About Us
        ' Contact Us
        '
        '==================================================

        lnkHome.Visible = True
        lnkAbout.Visible = True
        lnkContact.Visible = True


        '==================================================
        ' DEFAULT PRIVATE NAVIGATION
        '==================================================

        lnkLogin.Visible = True

        lnkUserDashboard.Visible = False

        lnkAdminDashboard.Visible = False

        lnkInvestors.Visible = False

        lnkLogout.Visible = False


        '==================================================
        ' CHECK LOGIN
        '==================================================

        If Session("UserID") Is Nothing Then

            '----------------------------------------------
            ' USER IS NOT LOGGED IN
            '----------------------------------------------

            Return

        End If


        '==================================================
        ' USER IS LOGGED IN
        '==================================================

        lnkLogin.Visible = False

        lnkLogout.Visible = True


        '==================================================
        ' CHECK USER ROLE
        '==================================================

        If Session("UserRole") Is Nothing Then

            Return

        End If


        Dim role As String =
            Session("UserRole").ToString().Trim().ToLower()


        '==================================================
        ' ADMIN
        '==================================================

        If role = "admin" Then

            lnkAdminDashboard.Visible = True

            lnkInvestors.Visible = True

            lnkUserDashboard.Visible = False


            '==================================================
            ' NORMAL USER
            '==================================================

        ElseIf role = "user" Then

            lnkUserDashboard.Visible = True

            lnkAdminDashboard.Visible = False

            lnkInvestors.Visible = False

        End If

    End Sub

End Class