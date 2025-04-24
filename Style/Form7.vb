Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class Form7

    ' ✅ Shared variables to store customer details globally
    Public Shared CustomerFullName As String
    Public Shared CustomerContact As String
    Public Shared CustomerEmail As String

    ' ========================== FORM CLOSING EVENT ==========================
    Private Sub Form7_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' ✅ Terminate the entire application when any form is closed
        Application.Exit()
    End Sub

    ' ========================== FORM LOAD ==========================
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearFields()
    End Sub

    ' ========================== CLEAR FIELDS ==========================
    Private Sub ClearFields()
        txtFullName.Clear()
        txtContact.Clear()
        txtEmail.Clear()
    End Sub

    ' ========================== VALIDATE INPUT ==========================
    Private Function ValidateInput() As Boolean
        ' ✅ Ensure all fields are filled and valid
        If String.IsNullOrWhiteSpace(txtFullName.Text) OrElse
               String.IsNullOrWhiteSpace(txtContact.Text) OrElse
               String.IsNullOrWhiteSpace(txtEmail.Text) Then
            MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' ✅ Validate contact number (10 digits)
        If Not IsNumeric(txtContact.Text) OrElse txtContact.Text.Length <> 10 Then
            MessageBox.Show("Contact number must be 10 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' ✅ Validate email format
        If Not txtEmail.Text.Contains("@") OrElse Not txtEmail.Text.Contains(".") Then
            MessageBox.Show("Enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    ' ========================== NAVIGATE TO PAYMENT FORM ==========================
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        ' ✅ Validate the form before proceeding
        If ValidateInput() Then
            ' ✅ Store customer details in Shared Variables
            CustomerFullName = txtFullName.Text
            CustomerContact = txtContact.Text
            CustomerEmail = txtEmail.Text

            ' ✅ Navigate to Form8 (Payment)
            Dim form8 As New Form8()
            Me.Hide()
            form8.ShowDialog()
            Me.Show()
        End If
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' ✅ Navigate back to Form6 (Cart)
        Dim form6 As New Form6()
        Me.Hide()
        form6.ShowDialog()
        Me.Show()
    End Sub

End Class
