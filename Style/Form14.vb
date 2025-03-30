Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class Form14

    ' ✅ Database connection string
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomers()
    End Sub

    ' ========================== LOAD ALL CUSTOMERS ==========================
    Private Sub LoadCustomers()
        Try
            con.Open()
            Dim query As String = "SELECT * FROM Customers"
            Dim adapter As New SqlDataAdapter(query, con)
            Dim table As New DataTable()
            adapter.Fill(table)

            ' ✅ Bind the result to DataGridView
            dgvCustomers.DataSource = table

        Catch ex As Exception
            MessageBox.Show("Error loading customers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    ' ========================== SEARCH CUSTOMER ==========================
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim searchName As String = txtSearch.Text.Trim()

        Try
            con.Open()
            Dim query As String = "SELECT * FROM Customers WHERE FullName LIKE @FullName"
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
            con.Close()
        End Try
    End Sub

    ' ========================== UPDATE CUSTOMER ==========================
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If dgvCustomers.SelectedRows.Count > 0 Then
            Dim selectedID As Integer = CInt(dgvCustomers.SelectedRows(0).Cells("CustomerID").Value)

            If ValidateInput() Then
                Try
                    con.Open()
                    Dim query As String = "UPDATE Customers SET FullName = @FullName, Contact = @Contact, Email = @Email WHERE CustomerID = @CustomerID"
                    Dim cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text)
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text)
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text)
                    cmd.Parameters.AddWithValue("@CustomerID", selectedID)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' ✅ Reload the customer list
                    LoadCustomers()
                    ClearFields()

                Catch ex As Exception
                    MessageBox.Show("Error updating customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    con.Close()
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
                    con.Open()
                    Dim query As String = "DELETE FROM Customers WHERE CustomerID = @CustomerID"
                    Dim cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@CustomerID", selectedID)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' ✅ Reload the customer list
                    LoadCustomers()
                    ClearFields()

                Catch ex As Exception
                    MessageBox.Show("Error deleting customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    con.Close()
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
        Me.Close()
    End Sub

End Class
