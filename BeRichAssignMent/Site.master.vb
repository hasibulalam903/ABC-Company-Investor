Partial Class Site
    Inherits System.Web.UI.MasterPage


    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As System.EventArgs
    ) Handles Me.Load

        CheckLoginStatus()

    End Sub


    '=========================================================
    ' CHECK LOGIN STATUS
    '=========================================================

    Private Sub CheckLoginStatus()


        '=========================================================
        ' PUBLIC LINKS
        '=========================================================

        lnkHome.Visible = True
        lnkAbout.Visible = True


        '=========================================================
        ' DEFAULT STATE
        '=========================================================

        lnkLogin.Visible = False
        lnkUserDashboard.Visible = False
        lnkMyProfile.Visible = False
        lnkAdminDashboard.Visible = False
        lnkLogout.Visible = False


        '=========================================================
        ' CHECK USER LOGIN
        '=========================================================

        If Session("UserID") Is Nothing Then

            lnkLogin.Visible = True

            Return

        End If


        '=========================================================
        ' LOGGED IN
        '=========================================================

        lnkLogout.Visible = True


        '=========================================================
        ' GET ROLE
        '
        ' First check Session("Role")
        ' If it does not exist, check Session("UserRole")
        '=========================================================

        Dim role As String = ""


        If Session("Role") IsNot Nothing Then

            role =
                Session("Role").ToString().Trim().ToLower()

        ElseIf Session("UserRole") IsNot Nothing Then

            role =
                Session("UserRole").ToString().Trim().ToLower()

        End If


        '=========================================================
        ' ROLE NOT FOUND
        '=========================================================

        If role = "" Then

            lnkLogin.Visible = True

            Return

        End If


        '=========================================================
        ' NORMAL USER
        '=========================================================

        If role = "user" Then

            lnkUserDashboard.Visible = True

            lnkMyProfile.Visible = False

            lnkAdminDashboard.Visible = False

            lnkLogin.Visible = False

            Return

        End If


        '=========================================================
        ' INVESTOR
        '=========================================================

        If role = "investor" Then

            lnkUserDashboard.Visible = False

            lnkMyProfile.Visible = True

            lnkAdminDashboard.Visible = False

            lnkLogin.Visible = False

            Return

        End If


        '=========================================================
        ' ADMIN
        '=========================================================

        If role = "admin" Then

            lnkUserDashboard.Visible = False

            lnkMyProfile.Visible = False

            lnkAdminDashboard.Visible = True

            lnkLogin.Visible = False

            Return

        End If


        '=========================================================
        ' UNKNOWN ROLE
        '=========================================================

        lnkUserDashboard.Visible = False

        lnkMyProfile.Visible = False

        lnkAdminDashboard.Visible = False

        lnkLogin.Visible = True

    End Sub


End Class