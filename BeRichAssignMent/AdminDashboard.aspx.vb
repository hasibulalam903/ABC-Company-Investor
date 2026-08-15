Imports System

Partial Class AdminDashboard
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load

        If Session("UserID") Is Nothing Then

            Response.Redirect("Login.aspx")
            Return

        End If


        If Session("UserRole") Is Nothing OrElse
           Session("UserRole").ToString().ToLower() <> "admin" Then

            Response.Redirect("Home.aspx")
            Return

        End If

    End Sub


    ' ==========================================
    ' USER PANEL
    ' ==========================================

    Protected Sub lnkUserPanel_Click(
        sender As Object,
        e As EventArgs
    )

        Response.Redirect("UsersPanel.aspx")

    End Sub


    ' ==========================================
    ' INVESTOR PANEL
    ' ==========================================

    Protected Sub lnkInvestorPanel_Click(
        sender As Object,
        e As EventArgs
    )

        Response.Redirect("Investors.aspx")

    End Sub

End Class