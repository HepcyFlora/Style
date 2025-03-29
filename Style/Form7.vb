﻿Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class Form7

    ' ✅ Database connection string
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ✅ Clear fields when the form loads
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

    ' ========================== SAVE CUSTOMER DETAILS ==========================
    Private Sub SaveCustomerDetails()
        Try
            ' ✅ Save customer details to the Customers table
            con.Open()
            Dim query As String = "INSERT INTO Customers (FullName, Contact, Email) VALUES (@FullName, @Contact, @Email)"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text)
                cmd.Parameters.AddWithValue("@Contact", txtContact.Text)
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text)
                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("Customer details saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error saving customer details: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    ' ========================== NAVIGATE TO PAYMENT FORM ==========================
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        ' ✅ Validate the form before proceeding
        If ValidateInput() Then
            ' ✅ Save the customer details in Form7 (accessible in Form13)
            SaveCustomerDetails()

            ' ✅ Store the customer details globally (to be accessed in Form13)
            Form13.CustomerFullName = txtFullName.Text
            Form13.CustomerContact = txtContact.Text
            Form13.CustomerEmail = txtEmail.Text

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
