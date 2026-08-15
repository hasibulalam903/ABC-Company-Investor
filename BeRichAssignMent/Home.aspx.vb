Imports System

Partial Class Home

    Inherits System.Web.UI.Page


    '=========================================================
    ' PAGE LOAD
    '=========================================================

    Protected Sub Page_Load(
        sender As Object,
        e As EventArgs) Handles Me.Load


        If Not IsPostBack Then

            'Home page initialization can be added here later.

            'Possible future features:
            '
            '1. Total Investor count
            '2. Total Investment amount
            '3. Active Investor count
            '4. Department statistics
            '5. Designation statistics
            '6. Recent investor registrations
            '7. Login / Session information

        End If


    End Sub


End Class