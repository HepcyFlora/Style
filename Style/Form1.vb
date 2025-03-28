Imports Microsoft.Data.SqlClient  ' ✅ Use the correct library

Public Class Form1
    ' ✅ Database connection string
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ✅ Mask the password with dots
        txtPassword.PasswordChar = "*"c
    End Sub

    ' ========================== LOGIN BUTTON ==========================
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        ' ✅ Hardcoded Admin Login
        If username = "admin" AndAlso password = "admin123" Then
            MessageBox.Show("Admin login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ✅ Correct Navigation to Form2
            Me.Hide()                  ' Hide Form1
            Dim adminForm As New Form2()  ' Create a new instance of Form2
            adminForm.ShowDialog()        ' Show Form2 properly with ShowDialog()

        Else
            ' ✅ Invalid credentials message
            MessageBox.Show("Invalid credentials. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' ========================== LINKED LABEL TO STAFF LOGIN ==========================
    Private Sub lnkStaffLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkStaffLogin.LinkClicked
        ' ✅ Navigate to Staff Login (Form4)
        Me.Hide()                      ' Hide Form1
        Dim staffLoginForm As New Form4()
        staffLoginForm.ShowDialog()   ' Show Form4 properly
    End Sub

    ' ========================== EXIT BUTTON ==========================
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Application.Exit()
    End Sub
End Class



