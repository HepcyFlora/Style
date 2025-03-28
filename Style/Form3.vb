Imports System.Data.SqlClient
Imports Microsoft.Data.SqlClient ' ✅ Explicitly define the SQL library

Public Class Form3
    ' ✅ Correct Database connection (ensure it points to DEMO)
    Dim con As New Microsoft.Data.SqlClient.SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStaffData() ' ✅ Load staff details into DataGridView
    End Sub

    ' ========================== LOAD STAFF DATA ==========================
    Private Sub LoadStaffData()
        Try
            Dim query As String = "SELECT StaffID, Username FROM Staff" ' ✅ Fetch only ID and Username

            Dim adapter As New Microsoft.Data.SqlClient.SqlDataAdapter(query, con)
            Dim table As New DataTable()

            con.Open()
            adapter.Fill(table)
            con.Close()

            dgvStaff.DataSource = table ' ✅ Display staff data in DataGridView

        Catch ex As Exception
            MessageBox.Show("Error loading staff data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== ADD STAFF ==========================
    Private Sub btnAddStaff_Click(sender As Object, e As EventArgs) Handles btnAddStaff.Click
        Try
            ' ✅ Validate input
            If txtUsername.Text = "" Or txtPassword.Text = "" Then
                MessageBox.Show("Please enter both Username and Password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ✅ Insert new staff into database
            Dim query As String = "INSERT INTO Staff (Username, Password) VALUES (@username, @password)"
            Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim())
                cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim())

                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using

            MessageBox.Show("Staff added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ✅ Refresh DataGridView
            LoadStaffData()

            ' ✅ Clear input fields
            txtUsername.Clear()
            txtPassword.Clear()

        Catch ex As Exception
            MessageBox.Show("Error adding staff: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== DELETE STAFF ==========================
    Private Sub btnDeleteStaff_Click(sender As Object, e As EventArgs) Handles btnDeleteStaff.Click
        Try
            ' ✅ Ensure a staff member is selected
            If dgvStaff.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a staff member to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ✅ Get the selected StaffID
            Dim staffID As Integer = Convert.ToInt32(dgvStaff.SelectedRows(0).Cells("StaffID").Value)

            ' ✅ Confirm deletion
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this staff member?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then Return

            ' ✅ Delete from database
            Dim query As String = "DELETE FROM Staff WHERE StaffID = @staffID"
            Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@staffID", staffID)

                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using

            MessageBox.Show("Staff deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ✅ Refresh DataGridView
            LoadStaffData()

        Catch ex As Exception
            MessageBox.Show("Error deleting staff: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim adminDashboard As New Form2()
        Me.Hide()
        adminDashboard.ShowDialog() ' ✅ Use ShowDialog() for better form handling
    End Sub
End Class


