Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class Form6

    ' ✅ Shared cart across forms
    Public Shared cart As New DataTable()

    ' ✅ Database connection string
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' ✅ Initialize cart table if not already initialized
            If cart.Columns.Count = 0 Then
                cart.Columns.Add("ProductID", GetType(Integer))
                cart.Columns.Add("Brand", GetType(String))
                cart.Columns.Add("Type", GetType(String))
                cart.Columns.Add("Color", GetType(String))
                cart.Columns.Add("Price", GetType(Decimal))
                cart.Columns.Add("Quantity", GetType(Integer))
                cart.Columns.Add("Total", GetType(Decimal))
            End If

            ' ✅ Bind cart to DataGridView
            dgvCart.DataSource = cart

            ' ✅ Load products into the Product DataGridView
            LoadProducts()

            ' ✅ Update total amount initially
            UpdateTotalAmount()

        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message)
        End Try
    End Sub

    ' ========================== LOAD PRODUCTS ==========================
    Private Sub LoadProducts()
        Try
            Dim query As String = "SELECT ProductID, Brand, Type, Color, Price, StockQuantity FROM Products"
            Dim adapter As New SqlDataAdapter(query, con)
            Dim table As New DataTable()

            con.Open()
            adapter.Fill(table)
            con.Close()

            dgvProducts.DataSource = table

        Catch ex As Exception
            MessageBox.Show("Error loading products: " & ex.Message)
        End Try
    End Sub

    ' ========================== ADD TO CART ==========================
    Private Sub btnAddToCart_Click(sender As Object, e As EventArgs) Handles btnAddToCart.Click
        Try
            If dgvProducts.SelectedRows.Count = 0 Then
                MessageBox.Show("Select a product to add.")
                Return
            End If

            Dim row As DataGridViewRow = dgvProducts.SelectedRows(0)
            Dim productID As Integer = CInt(row.Cells("ProductID").Value)
            Dim brand As String = row.Cells("Brand").Value.ToString()
            Dim type As String = row.Cells("Type").Value.ToString()
            Dim color As String = row.Cells("Color").Value.ToString()
            Dim price As Decimal = CDec(row.Cells("Price").Value)
            Dim stock As Integer = CInt(row.Cells("StockQuantity").Value)

            Dim quantity As Integer
            If Not Integer.TryParse(numQuantity.Value.ToString(), quantity) OrElse quantity <= 0 Then
                MessageBox.Show("Enter a valid quantity.")
                Return
            End If

            If quantity > stock Then
                MessageBox.Show($"Out of stock! Only {stock} available.")
                Return
            End If

            Dim total As Decimal = price * quantity
            cart.Rows.Add(productID, brand, type, color, price, quantity, total)
            UpdateTotalAmount()

        Catch ex As Exception
            MessageBox.Show("Error adding product: " & ex.Message)
        End Try
    End Sub

    ' ========================== REMOVE ITEM ==========================
    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If dgvCart.SelectedRows.Count > 0 Then
            dgvCart.Rows.RemoveAt(dgvCart.SelectedRows(0).Index)
            UpdateTotalAmount()
        Else
            MessageBox.Show("Select a product to remove.")
        End If
    End Sub

    ' ========================== CLEAR CART ==========================
    Private Sub btnClearCart_Click(sender As Object, e As EventArgs) Handles btnClearCart.Click
        cart.Clear()
        UpdateTotalAmount()
    End Sub

    ' ========================== UPDATE TOTAL AMOUNT ==========================
    Private Sub UpdateTotalAmount()
        Dim totalAmount As Decimal = 0
        For Each row As DataRow In cart.Rows
            totalAmount += CDec(row("Total"))
        Next
        lblTotalAmount.Text = $"Total Amount: ₹{totalAmount:F2}"
    End Sub

    ' ========================== PROCEED TO CUSTOMER DETAILS ==========================
    Private Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click
        If cart.Rows.Count = 0 Then
            MessageBox.Show("Cart is empty!")
            Return
        End If
        Dim customerForm As New Form7()
        Me.Hide()
        customerForm.ShowDialog()
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' ✅ Navigate back to Form4
        Dim form4 As New Form4()
        Me.Hide()                  ' Hide Form6
        form4.ShowDialog()         ' Show Form4
        Me.Close()                  ' Close Form6 after navigating
    End Sub

    ' ========================== FORM CLOSING EVENT ==========================
    Private Sub Form6_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' ✅ Only close the entire application when the X button is clicked
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Exit()
        End If
    End Sub

End Class
