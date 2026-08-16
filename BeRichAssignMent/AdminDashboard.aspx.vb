Imports System

Partial Class AdminDashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles Me.Load

        ' ==========================================
        ' CHECK USER LOGIN
        ' ==========================================

        If Session("UserID") Is Nothing Then

            Response.Redirect("~/Login.aspx")
            Return

        End If


        ' ==========================================
        ' CHECK ADMIN ROLE
        ' ==========================================

        If Session("Role") Is Nothing OrElse
           Not String.Equals(
               Session("Role").ToString().Trim(),
               "admin",
               StringComparison.OrdinalIgnoreCase
           ) Then

            Response.Redirect("~/Home.aspx")
            Return

        End If

    End Sub


    ' ==========================================
    ' USER PANEL
    ' ==========================================

    Protected Sub lnkUserPanel_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    )

        Response.Redirect("~/UsersPanel.aspx")

    End Sub


    ' ==========================================
    ' INVESTOR PANEL
    ' ==========================================

    Protected Sub lnkInvestorPanel_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    )

        Response.Redirect("~/investors.aspx")

    End Sub

End Class