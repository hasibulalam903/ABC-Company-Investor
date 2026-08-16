Imports System

Partial Class UserDashboard
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles Me.Load


        '==================================================
        ' CHECK LOGIN
        '==================================================

        If Session("UserID") Is Nothing Then

            Response.Redirect("~/Login.aspx")
            Return

        End If


        '==================================================
        ' CHECK ROLE
        ' ONLY NORMAL USER CAN ACCESS THIS PAGE
        '==================================================

        If Session("Role") Is Nothing OrElse
           Not Session("Role").ToString().Trim().Equals(
               "User",
               StringComparison.OrdinalIgnoreCase
           ) Then

            Response.Redirect("~/Login.aspx")
            Return

        End If


        '==================================================
        ' LOAD USER INFORMATION
        '==================================================

        If Not IsPostBack Then


            '==================================================
            ' USER NAME
            '==================================================

            If Session("UserName") IsNot Nothing Then

                lblUserName.Text =
                    Session("UserName").ToString()

            ElseIf Session("Name") IsNot Nothing Then

                lblUserName.Text =
                    Session("Name").ToString()

            ElseIf Session("Email") IsNot Nothing Then

                lblUserName.Text =
                    Session("Email").ToString()

            Else

                lblUserName.Text = "User"

            End If


            '==================================================
            ' EMAIL
            '==================================================

            If Session("Email") IsNot Nothing Then

                lblEmail.Text =
                    Session("Email").ToString()

            Else

                lblEmail.Text = "-"

            End If


            '==================================================
            ' PHONE NUMBER
            ' DATABASE COLUMN = Mobile
            '==================================================

            If Session("Phone") IsNot Nothing Then

                lblPhone.Text =
                    Session("Phone").ToString()

            ElseIf Session("Mobile") IsNot Nothing Then

                lblPhone.Text =
                    Session("Mobile").ToString()

            Else

                lblPhone.Text = "-"

            End If


            '==================================================
            ' ROLE
            '==================================================

            If Session("Role") IsNot Nothing Then

                lblRole.Text =
                    Session("Role").ToString()

            Else

                lblRole.Text = "User"

            End If


            '==================================================
            ' ACCOUNT STATUS
            '==================================================

            If Session("Status") IsNot Nothing Then

                lblStatus.Text =
                    Session("Status").ToString()

            Else

                lblStatus.Text = "Active"

            End If


        End If


    End Sub


    '==================================================
    ' MY PROFILE BUTTON
    '==================================================

    Protected Sub btnProfile_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnProfile.Click

        Response.Redirect("~/MyProfile.aspx")

    End Sub


End Class