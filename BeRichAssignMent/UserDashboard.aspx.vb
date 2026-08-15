
Imports System

Partial Class UserDashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load

        ' Check login
        If Session("UserID") Is Nothing Then

            Response.Redirect("Login.aspx")
            Return

        End If


        ' Check role
        If Session("Role") Is Nothing OrElse
           Not Session("Role").ToString().Trim().Equals(
               "User",
               StringComparison.OrdinalIgnoreCase) Then

            Response.Redirect("Login.aspx")
            Return

        End If


        If Not IsPostBack Then

            ' Email
            If Session("Email") IsNot Nothing Then

                lblEmail.Text =
                    Session("Email").ToString()

            End If


            ' Role
            If Session("Role") IsNot Nothing Then

                lblRole.Text =
                    Session("Role").ToString()

            End If


            ' Status
            If Session("Status") IsNot Nothing Then

                lblStatus.Text =
                    Session("Status").ToString()

            End If


            ' User name
            If Session("Email") IsNot Nothing Then

                lblUserName.Text =
                    Session("Email").ToString()

            End If

        End If

    End Sub




End Class

