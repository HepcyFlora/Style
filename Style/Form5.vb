Imports System.Data
Imports Microsoft.Data.SqlClient  ' ✅ Use only Microsoft.Data.SqlClient

Public Class Form5
    ' ✅ Database connection string
    Dim con As New Microsoft.Data.SqlClient.SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM CLOSING EVENT ==========================
    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' ✅ Terminate the entire application when any form is closed
        Application.Exit()
    End Sub

    ' ========================== FORM LOAD ==========================
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateComboBoxes()
        LoadProducts()
    End Sub

    ' ========================== POPULATE COMBOBOXES ==========================
    Private Sub PopulateComboBoxes()
        ' ✅ Pre-fill combo boxes with hardcoded values
        cmbBrand.Items.AddRange({"Gucci", "Prada", "Louis Vuitton", "Chanel", "Hermès", "Burberry", "Versace", "Dior", "Fendi", "Bottega Veneta",
                                 "Balenciaga", "Givenchy", "Coach", "Armani", "Tom Ford", "Zara", "H&M", "Puma", "Nike", "Adidas"})

        cmbType.Items.AddRange({"Tote", "Clutch", "Backpack", "Crossbody", "Satchel", "Shoulder Bag", "Bucket Bag", "Hobo", "Duffel", "Messenger"})

        cmbColor.Items.AddRange({"Black", "White", "Red", "Blue", "Green", "Yellow", "Pink", "Purple", "Orange", "Brown",
                                 "Grey", "Beige", "Gold", "Silver", "Turquoise"})
    End Sub

    ' ========================== LOAD PRODUCTS ==========================
    Private Sub LoadProducts()
        Try
            Dim query As String = "SELECT * FROM Products"
            Dim adapter As New Microsoft.Data.SqlClient.SqlDataAdapter(query, con)
            Dim table As New DataTable()

            con.Open()
            adapter.Fill(table)
            con.Close()

            dgvProducts.DataSource = table

        Catch ex As Exception
            MessageBox.Show("Error loading products: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== ADD PRODUCT ==========================
    Private Sub btnAddProduct_Click(sender As Object, e As EventArgs) Handles btnAddProduct.Click
        Try
            ' ✅ Validate input fields
            If cmbBrand.SelectedItem Is Nothing Or cmbType.SelectedItem Is Nothing Or cmbColor.SelectedItem Is Nothing OrElse
               txtPrice.Text = "" OrElse txtStockQuantity.Text = "" Then

                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ✅ Prepare SQL query for inserting product
            Dim query As String = "INSERT INTO Products (Brand, Type, Color, Price, StockQuantity) VALUES (@brand, @type, @color, @price, @stock)"
            Dim cmd As New Microsoft.Data.SqlClient.SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@brand", cmbBrand.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@type", cmbType.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@color", cmbColor.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text))
            cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStockQuantity.Text))

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ✅ Refresh DataGridView
            LoadProducts()

        Catch ex As Exception
            MessageBox.Show("Error adding product: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== DELETE PRODUCT ==========================
    Private Sub btnDeleteProduct_Click(sender As Object, e As EventArgs) Handles btnDeleteProduct.Click
        Try
            ' ✅ Ensure a product is selected
            If dgvProducts.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a product to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim productID As Integer = Convert.ToInt32(dgvProducts.SelectedRows(0).Cells("ProductID").Value)

            Dim query As String = "DELETE FROM Products WHERE ProductID = @productID"
            Dim cmd As New Microsoft.Data.SqlClient.SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@productID", productID)

            ' ✅ Confirm deletion
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then Return

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ✅ Refresh DataGridView
            LoadProducts()

        Catch ex As Exception
            MessageBox.Show("Error deleting product: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== UPDATE PRODUCT ==========================
    Private Sub btnUpdateProduct_Click(sender As Object, e As EventArgs) Handles btnUpdateProduct.Click
        Try
            ' ✅ Ensure a product is selected
            If dgvProducts.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a product to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim productID As Integer = Convert.ToInt32(dgvProducts.SelectedRows(0).Cells("ProductID").Value)

            ' ✅ Validate input fields
            If cmbBrand.SelectedItem Is Nothing Or cmbType.SelectedItem Is Nothing Or cmbColor.SelectedItem Is Nothing OrElse
               txtPrice.Text = "" OrElse txtStockQuantity.Text = "" Then

                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim query As String = "UPDATE Products SET Brand = @brand, Type = @type, Color = @color, Price = @price, StockQuantity = @stock WHERE ProductID = @productID"
            Dim cmd As New Microsoft.Data.SqlClient.SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@brand", cmbBrand.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@type", cmbType.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@color", cmbColor.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text))
            cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStockQuantity.Text))
            cmd.Parameters.AddWithValue("@productID", productID)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ✅ Refresh DataGridView
            LoadProducts()

        Catch ex As Exception
            MessageBox.Show("Error updating product: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim adminDashboard As New Form2()
        Me.Hide()
        adminDashboard.ShowDialog()
    End Sub
End Class
