Imports System.Data.SqlClient
Imports Microsoft.Data.SqlClient ' ✅ Explicitly using Microsoft SQL Client

Public Class Form4
    ' ✅ Database connection string
    Dim con As New Microsoft.Data.SqlClient.SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ✅ Mask the password with dots
        txtPassword.PasswordChar = "*"c
    End Sub

    ' ========================== LOGIN BUTTON ==========================
    Private Sub btnStaffLogin_Click(sender As Object, e As EventArgs) Handles btnStaffLogin.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        ' ✅ Validate input
        If username = "" Or password = "" Then
            MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' ✅ Check if staff exists in the database
            Dim query As String = "SELECT COUNT(*) FROM Staff WHERE Username = @username AND Password = @password"

            ' ✅ Use explicit SQL namespace
            Dim cmd As New Microsoft.Data.SqlClient.SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@username", username)
            cmd.Parameters.AddWithValue("@password", password)

            con.Open()
            Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            con.Close()

            If result > 0 Then
                ' ✅ Successful login - Show dialog box
                MessageBox.Show("Staff login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' ✅ Navigate to Staff Dashboard (Form6)
                Dim staffDashboard As New Form6()
                Me.Hide()
                staffDashboard.ShowDialog()

            Else
                ' ❌ Invalid credentials - Show dialog box
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim adminLogin As New Form1() ' Navigate back to Admin Login
        Me.Hide()
        adminLogin.ShowDialog()
    End Sub
End Class

