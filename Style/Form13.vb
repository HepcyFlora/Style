Imports System.Data
Imports Microsoft.Data.SqlClient
Imports System.Drawing.Printing

Public Class Form13

    ' ✅ Database connection string
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ✅ PrintDocument object for receipt printing
    Private WithEvents printDoc As New PrintDocument()

    ' ========================== FORM LOAD ==========================
    Private Sub Form13_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' ✅ Retrieve customer details from Form7 shared variables
            txtFullName.Text = Form7.CustomerFullName
            txtContact.Text = Form7.CustomerContact
            txtEmail.Text = Form7.CustomerEmail

            ' ✅ Retrieve payment method from Form8
            txtPaymentMethod.Text = Form8.PaymentMethod

            ' ✅ Display current date and time
            txtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

            ' ✅ Load cart items into DataGridView
            dgvReceipt.DataSource = Form6.cart

            ' ✅ Display the total amount
            Dim totalAmount As Decimal = 0
            For Each row As DataRow In Form6.cart.Rows
                If Not IsDBNull(row("Total")) Then
                    totalAmount += CDec(row("Total"))
                End If
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

            ' ✅ Insert customer details into the database
            Dim customerQuery As String = "INSERT INTO Customers (FullName, Contact, Email) VALUES (@FullName, @Contact, @Email)"
            Using customerCmd As New SqlCommand(customerQuery, con)
                customerCmd.Parameters.AddWithValue("@FullName", Form7.CustomerFullName)
                customerCmd.Parameters.AddWithValue("@Contact", Form7.CustomerContact)
                customerCmd.Parameters.AddWithValue("@Email", Form7.CustomerEmail)
                customerCmd.ExecuteNonQuery()
            End Using

            ' ✅ Insert sales data into Sales table
            For Each row As DataRow In Form6.cart.Rows
                Dim brand As String = If(IsDBNull(row("Brand")), "", row("Brand").ToString())
                Dim type As String = If(IsDBNull(row("Type")), "", row("Type").ToString())
                Dim color As String = If(IsDBNull(row("Color")), "", row("Color").ToString())
                Dim quantity As Integer = If(IsDBNull(row("Quantity")), 0, CInt(row("Quantity")))
                Dim totalPrice As Decimal = If(IsDBNull(row("Total")), 0D, CDec(row("Total")))

                Dim salesQuery As String = "INSERT INTO Sales (CustomerName, Contact, Email, Brand, Type, Color, Quantity, TotalPrice, PaymentMethod, DateTime) " &
                                           "VALUES (@CustomerName, @Contact, @Email, @Brand, @Type, @Color, @Quantity, @TotalPrice, @PaymentMethod, @DateTime)"

                Using cmd As New SqlCommand(salesQuery, con)
                    cmd.Parameters.AddWithValue("@CustomerName", Form7.CustomerFullName)
                    cmd.Parameters.AddWithValue("@Contact", Form7.CustomerContact)
                    cmd.Parameters.AddWithValue("@Email", Form7.CustomerEmail)
                    cmd.Parameters.AddWithValue("@Brand", brand)
                    cmd.Parameters.AddWithValue("@Type", type)
                    cmd.Parameters.AddWithValue("@Color", color)
                    cmd.Parameters.AddWithValue("@Quantity", quantity)
                    cmd.Parameters.AddWithValue("@TotalPrice", totalPrice)
                    cmd.Parameters.AddWithValue("@PaymentMethod", txtPaymentMethod.Text)
                    cmd.Parameters.AddWithValue("@DateTime", DateTime.Now)
                    cmd.ExecuteNonQuery()
                End Using
            Next

            MessageBox.Show("Customer and Sales data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Error saving to database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        e.Graphics.DrawString($"Name: {Form7.CustomerFullName}", fontBody, Brushes.Black, startX, startY)
        startY += lineHeight
        e.Graphics.DrawString($"Contact: {Form7.CustomerContact}", fontBody, Brushes.Black, startX, startY)
        startY += lineHeight
        e.Graphics.DrawString($"Email: {Form7.CustomerEmail}", fontBody, Brushes.Black, startX, startY)
        startY += lineHeight
        e.Graphics.DrawString($"Payment: {txtPaymentMethod.Text}", fontBody, Brushes.Black, startX, startY)
        startY += lineHeight * 2

        ' ✅ Print cart items
        Dim totalAmount As Decimal = 0
        For Each row As DataRow In Form6.cart.Rows
            Dim brand As String = row("Brand").ToString()
            Dim type As String = row("Type").ToString()
            Dim color As String = row("Color").ToString()
            Dim quantity As Integer = CInt(row("Quantity"))
            Dim price As Decimal = CDec(row("Price"))
            Dim total As Decimal = CDec(row("Total"))

            ' ✅ Print product line
            Dim productLine As String = $"{brand} | {type} | {color} x {quantity} @ ₹{price} = ₹{total}"
            e.Graphics.DrawString(productLine, fontBody, Brushes.Black, startX, startY)

            totalAmount += total
            startY += lineHeight
        Next

        ' ✅ Print the total amount at the bottom
        e.Graphics.DrawString($"Total: ₹{totalAmount:F2}", fontTitle, Brushes.Black, startX, startY + lineHeight)
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

End Class
