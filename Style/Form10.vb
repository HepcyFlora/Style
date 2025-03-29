Imports System.Data
Imports Microsoft.Data.SqlClient
Imports System.Threading

Public Class Form10

    ' ✅ Database connection
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Hide both panels initially
        pnlScanner.Visible = False
        pnlUPIID.Visible = False
    End Sub

    ' ========================== SHOW SCANNER PANEL ==========================
    Private Sub btnUPIScanner_Click(sender As Object, e As EventArgs) Handles btnUPIScanner.Click
        ' Show Scanner Panel and Hide UPI ID Panel
        pnlScanner.Visible = True
        pnlUPIID.Visible = False
    End Sub

    ' ========================== SHOW UPI ID PANEL ==========================
    Private Sub btnUPIID_Click(sender As Object, e As EventArgs) Handles btnUPIID.Click
        ' Show UPI ID Panel and Hide Scanner Panel
        pnlUPIID.Visible = True
        pnlScanner.Visible = False
    End Sub

    ' ========================== VALIDATE UPI ID ==========================
    Private Sub btnValidateUPI_Click(sender As Object, e As EventArgs) Handles btnValidateUPI.Click
        Dim upiID As String = txtUPIID.Text.Trim()

        ' Simple UPI ID validation
        If String.IsNullOrWhiteSpace(upiID) OrElse Not upiID.Contains("@") Then
            MessageBox.Show("Invalid UPI ID. Please enter a valid UPI ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            MessageBox.Show("UPI ID validated successfully.", "Validation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' ========================== CONFIRM PAYMENT ==========================
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ' Simulate payment processing
        Dim loadingThread As New Thread(AddressOf ShowLoadingAnimation)
        loadingThread.Start()

        ' Simulate 2-second delay for processing
        Thread.Sleep(2000)

        ' ✅ Payment success
        loadingThread.Abort()

        ' Reduce stock only if payment is successful
        If pnlScanner.Visible OrElse pnlUPIID.Visible Then
            ReduceStock()
            MessageBox.Show("Payment successful. Stock reduced.", "Payment Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Navigate to Form13 (Receipt)
            Dim receiptForm As New Form13()
            Me.Hide()
            receiptForm.ShowDialog()
            Me.Close()
        Else
            MessageBox.Show("No payment method selected.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' ========================== DECLINE PAYMENT ==========================
    Private Sub btnDecline_Click(sender As Object, e As EventArgs) Handles btnDecline.Click
        MessageBox.Show("Payment declined. No stock was deducted.", "Payment Declined", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Me.Close()
    End Sub

    ' ========================== REDUCE STOCK ==========================
    Private Sub ReduceStock()
        Try
            ' ✅ Reduce stock for each item in the cart
            For Each row As DataRow In Form6.cart.Rows
                Dim productID As Integer = CInt(row("ProductID"))
                Dim quantity As Integer = CInt(row("Quantity"))

                Dim query As String = "UPDATE Products SET StockQuantity = StockQuantity - @quantity WHERE ProductID = @productID"

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@quantity", quantity)
                    cmd.Parameters.AddWithValue("@productID", productID)

                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            Next

        Catch ex As Exception
            MessageBox.Show("Error reducing stock: " & ex.Message, "Stock Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== SHOW LOADING ANIMATION ==========================
    Private Sub ShowLoadingAnimation()
        Dim loadingForm As New Form()
        Dim loadingLabel As New Label()

        loadingForm.Size = New Size(200, 100)
        loadingForm.StartPosition = FormStartPosition.CenterScreen
        loadingForm.FormBorderStyle = FormBorderStyle.None
        loadingForm.BackColor = Color.White

        loadingLabel.Text = "Processing..."
        loadingLabel.AutoSize = True
        loadingLabel.Font = New Font("Arial", 12, FontStyle.Bold)
        loadingLabel.ForeColor = Color.DarkBlue
        loadingLabel.Location = New Point(50, 30)

        loadingForm.Controls.Add(loadingLabel)
        loadingForm.ShowDialog()
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' Navigate back to Form8 (Payment Options)
        Dim paymentForm As New Form8()
        Me.Hide()
        paymentForm.ShowDialog()
        Me.Close()
    End Sub

End Class

