Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class Form14

    ' ✅ Database connection string
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomers()  ' ✅ Load customers only when form loads
    End Sub

    ' ========================== LOAD ALL CUSTOMERS ==========================
    Private Sub LoadCustomers()
        Try
            ' ✅ Open the connection only if closed
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            ' ✅ Display customers in ascending order (oldest first)
            Dim query As String = "SELECT * FROM Customers ORDER BY DateTime ASC"
            Dim adapter As New SqlDataAdapter(query, con)
            Dim table As New DataTable()
            adapter.Fill(table)

            ' ✅ Bind the result to DataGridView
            dgvCustomers.DataSource = table

        Catch ex As Exception
            MessageBox.Show("Error loading customers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ' ✅ Ensure the connection is closed
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    ' ========================== SEARCH CUSTOMER ==========================
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim searchName As String = txtSearch.Text.Trim()

        Try
            ' ✅ Open connection only if closed
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            Dim query As String = "SELECT * FROM Customers WHERE FullName LIKE @FullName OR Contact LIKE @FullName OR Email LIKE @FullName ORDER BY DateTime ASC"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@FullName", "%" & searchName & "%")

            Dim adapter As New SqlDataAdapter(cmd)
            Dim table As New DataTable()
            adapter.Fill(table)

            ' ✅ Bind the result to DataGridView
            dgvCustomers.DataSource = table

        Catch ex As Exception
            MessageBox.Show("Error searching customers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ' ✅ Ensure the connection is closed
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    ' ========================== UPDATE CUSTOMER ==========================
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If dgvCustomers.SelectedRows.Count > 0 Then
            Dim selectedID As Integer = CInt(dgvCustomers.SelectedRows(0).Cells("CustomerID").Value)

            If ValidateInput() Then
                Try
                    ' ✅ Open connection only if closed
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    Dim query As String = "UPDATE Customers SET FullName = @FullName, Contact = @Contact, Email = @Email WHERE CustomerID = @CustomerID"
                    Dim cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text)
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text)
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text)
                    cmd.Parameters.AddWithValue("@CustomerID", selectedID)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show("Error updating customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Finally
                    ' ✅ Ensure the connection is closed
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                End Try
            End If
        Else
            MessageBox.Show("Please select a customer to update.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' ========================== DELETE CUSTOMER ==========================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvCustomers.SelectedRows.Count > 0 Then
            Dim selectedID As Integer = CInt(dgvCustomers.SelectedRows(0).Cells("CustomerID").Value)

            If MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    ' ✅ Open connection only if closed
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    Dim query As String = "DELETE FROM Customers WHERE CustomerID = @CustomerID"
                    Dim cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@CustomerID", selectedID)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show("Error deleting customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Finally
                    ' ✅ Ensure the connection is closed
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                End Try
            End If
        Else
            MessageBox.Show("Please select a customer to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
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

    ' ========================== REFRESH BUTTON ==========================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        ' ✅ Reload customers only when the Refresh button is clicked
        LoadCustomers()
        ClearFields()
    End Sub

    ' ========================== CLEAR FIELDS ==========================
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtFullName.Clear()
        txtContact.Clear()
        txtEmail.Clear()
        txtSearch.Clear()
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' ✅ Go back to Form2
        Dim form2 As New Form2()
        Me.Hide()
        form2.ShowDialog()
        Me.Close()   ' Close Form14 after returning to Form2
    End Sub

    ' ========================== FORM CLOSING EVENT ==========================
    Private Sub Form14_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' ✅ Close the entire project when X button is clicked
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Exit()
        End If
    End Sub

End Class
