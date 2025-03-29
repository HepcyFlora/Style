Imports System.Data
Imports Microsoft.Data.SqlClient
Imports System.Drawing.Printing

Public Class Form13
    ' ✅ Global variables for customer details
    Public Shared CustomerFullName As String
    Public Shared CustomerContact As String
    Public Shared CustomerEmail As String

    ' ✅ Database connection
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ✅ PrintDocument object for receipt printing
    Private WithEvents printDoc As New PrintDocument()

    ' ========================== FORM LOAD ==========================
    Private Sub Form13_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' ✅ Retrieve customer details from shared variables in Form7
            txtFullName.Text = CustomerFullName
            txtContact.Text = CustomerContact
            txtEmail.Text = CustomerEmail

            ' ✅ Retrieve payment method from Form8
            txtPaymentMethod.Text = Form8.PaymentMethod

            ' ✅ Display current date and time
            txtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

            ' ✅ Load cart items into the DataGridView
            dgvReceipt.DataSource = Form6.cart

            ' ✅ Display the total amount
            Dim totalAmount As Decimal = 0
            For Each row As DataRow In Form6.cart.Rows
                totalAmount += CDec(row("Total"))
            Next
            lblTotalAmount.Text = $"Total Amount: ₹{totalAmount:F2}"

        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== PRINT RECEIPT & SAVE TO DATABASE ==========================
    Private Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
        Try
            ' ✅ Save the data to the database
            SaveToDatabase()

            ' ✅ Print the receipt
            printDoc.Print()

            ' ✅ Close the entire project after printing
            Application.Exit()

        Catch ex As Exception
            MessageBox.Show("Error during receipt printing: " & ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== SAVE TO DATABASE ==========================
    Private Sub SaveToDatabase()
        Try
            con.Open()

            ' ✅ Insert customer details into Customers table
            Dim customerQuery As String = "INSERT INTO Customers (FullName, Contact, Email) " &
                                          "VALUES (@FullName, @Contact, @Email)"

            Using cmd As New SqlCommand(customerQuery, con)
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text)
                cmd.Parameters.AddWithValue("@Contact", txtContact.Text)
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text)
                cmd.ExecuteNonQuery()
            End Using

            ' ✅ Get the CustomerID of the newly inserted customer
            Dim customerID As Integer
            Using cmd As New SqlCommand("SELECT SCOPE_IDENTITY()", con)
                customerID = Convert.ToInt32(cmd.ExecuteScalar())
            End Using

            ' ✅ Insert sales data into Sales table
            For Each row As DataRow In Form6.cart.Rows
                Dim productID As Integer = CInt(row("ProductID"))
                Dim brand As String = row("Brand").ToString()
                Dim type As String = row("Type").ToString()
                Dim color As String = row("Color").ToString()
                Dim price As Decimal = CDec(row("Price"))
                Dim quantity As Integer = CInt(row("Quantity"))
                Dim total As Decimal = CDec(row("Total"))

                Dim salesQuery As String = "INSERT INTO Sales (CustomerID, CustomerName, Contact, ProductID, Brand, Type, Color, Quantity, TotalPrice, PaymentMethod, DateTime) " &
                                          "VALUES (@CustomerID, @CustomerName, @Contact, @ProductID, @Brand, @Type, @Color, @Quantity, @TotalPrice, @PaymentMethod, @DateTime)"

                Using cmd As New SqlCommand(salesQuery, con)
                    cmd.Parameters.AddWithValue("@CustomerID", customerID)
                    cmd.Parameters.AddWithValue("@CustomerName", txtFullName.Text)
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text)
                    cmd.Parameters.AddWithValue("@ProductID", productID)
                    cmd.Parameters.AddWithValue("@Brand", brand)
                    cmd.Parameters.AddWithValue("@Type", type)
                    cmd.Parameters.AddWithValue("@Color", color)
                    cmd.Parameters.AddWithValue("@Quantity", quantity)
                    cmd.Parameters.AddWithValue("@TotalPrice", total)
                    cmd.Parameters.AddWithValue("@PaymentMethod", txtPaymentMethod.Text)
                    cmd.Parameters.AddWithValue("@DateTime", DateTime.Now)
                    cmd.ExecuteNonQuery()
                End Using
            Next

        Catch ex As Exception
            MessageBox.Show("Error saving to database: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    ' ========================== PRINT DOCUMENT EVENT ==========================
    Private Sub printDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        Dim fontTitle As New Font("Segoe UI", 14, FontStyle.Bold)
        Dim fontBody As New Font("Segoe UI", 11, FontStyle.Regular)
        Dim startX As Integer = 50
        Dim startY As Integer = 50
        Dim lineHeight As Integer = 30

        ' ✅ Header
        e.Graphics.DrawString("STYLE SACK - RECEIPT", fontTitle, Brushes.Black, startX, startY)
        startY += lineHeight * 2

        ' ✅ Customer Details
        e.Graphics.DrawString($"Name: {txtFullName.Text}", fontBody, Brushes.Black, startX, startY)
        startY += lineHeight
        e.Graphics.DrawString($"Contact: {txtContact.Text}", fontBody, Brushes.Black, startX, startY)
        startY += lineHeight
        e.Graphics.DrawString($"Email: {txtEmail.Text}", fontBody, Brushes.Black, startX, startY)
        startY += lineHeight
        e.Graphics.DrawString($"Payment: {txtPaymentMethod.Text}", fontBody, Brushes.Black, startX, startY)
        startY += lineHeight * 2

        ' ✅ Print product details
        For Each row As DataGridViewRow In dgvReceipt.Rows
            If Not row.IsNewRow Then
                Dim productInfo As String = $"{row.Cells("Brand").Value} - {row.Cells("Type").Value} - {row.Cells("Color").Value} x {row.Cells("Quantity").Value} @ ₹{row.Cells("Price").Value}"
                e.Graphics.DrawString(productInfo, fontBody, Brushes.Black, startX, startY)
                startY += lineHeight
            End If
        Next

        ' ✅ Print total amount
        e.Graphics.DrawString($"Total: ₹{lblTotalAmount.Text}", fontTitle, Brushes.Black, startX, startY + lineHeight)
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

End Class
