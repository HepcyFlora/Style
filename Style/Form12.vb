Imports System.Data
Imports System.Text.RegularExpressions
Imports Microsoft.Data.SqlClient

Public Class Form12

    ' ✅ Database connection
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ✅ Populate card types in ComboBox
        cmbCardType.Items.Clear()
        cmbCardType.Items.Add("Visa")
        cmbCardType.Items.Add("MasterCard")
        cmbCardType.Items.Add("Rupay")
        cmbCardType.Items.Add("Amex")
        cmbCardType.SelectedIndex = -1  ' No selection initially
    End Sub

    ' ========================== CONFIRM PAYMENT ==========================
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Try
            ' ✅ Validate inputs
            If Not ValidateInputs() Then
                Return
            End If

            ' ✅ Proceed with payment confirmation
            MessageBox.Show("Payment confirmed. Stock will be reduced.", "Payment Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ✅ Reduce stock immediately
            ReduceStock()

            ' ✅ Navigate to Form13 (Receipt)
            Dim receiptForm As New Form13()
            Me.Hide()
            receiptForm.ShowDialog()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error during payment: " & ex.Message, "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' ========================== VALIDATE INPUTS ==========================
    Private Function ValidateInputs() As Boolean
        ' ✅ Validate Cardholder Name
        If String.IsNullOrWhiteSpace(txtCardHolder.Text) Then
            MessageBox.Show("Please enter the cardholder's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' ✅ Validate Card Number (16 digits)
        Dim cardNumber As String = txtCardNumber.Text.Trim()
        If Not Regex.IsMatch(cardNumber, "^\d{16}$") Then
            MessageBox.Show("Card Number must contain exactly 16 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' ✅ Validate Expiry Date (MM/YY format)
        Dim expiryDate As String = txtExpiryDate.Text.Trim()
        If Not Regex.IsMatch(expiryDate, "^(0[1-9]|1[0-2])\/(2[5-9]|3[0-9])$") Then
            MessageBox.Show("Expiry Date must be in MM/YY format and not expired.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' ✅ Validate CVV (3 digits for Visa, MasterCard, Rupay; 4 digits for Amex)
        Dim cvv As String = txtCVV.Text.Trim()
        Dim cardType As String = cmbCardType.SelectedItem?.ToString()

        If String.IsNullOrEmpty(cardType) Then
            MessageBox.Show("Please select a card type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' CVV validation based on card type
        If cardType = "Amex" Then
            If Not Regex.IsMatch(cvv, "^\d{4}$") Then
                MessageBox.Show("Amex CVV must be exactly 4 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
        Else
            If Not Regex.IsMatch(cvv, "^\d{3}$") Then
                MessageBox.Show("CVV must be exactly 3 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
        End If

        Return True
    End Function

    ' ========================== REDUCE STOCK ==========================
    Private Sub ReduceStock()
        Try
            ' ✅ Ensure cart is not empty
            If Form6.cart.Rows.Count = 0 Then
                MessageBox.Show("No items in the cart to reduce stock.", "Cart Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

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

            MessageBox.Show("Stock updated successfully!", "Stock Reduced", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error reducing stock: " & ex.Message, "Stock Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== DECLINE PAYMENT ==========================
    Private Sub btnDecline_Click(sender As Object, e As EventArgs) Handles btnDecline.Click
        MessageBox.Show("Payment declined. No stock was deducted.", "Payment Declined", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Me.Close()
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim paymentForm As New Form8()
        Me.Hide()
        paymentForm.ShowDialog()
        Me.Close()
    End Sub
    ' ========================== FORM CLOSING EVENT ==========================
    Private Sub Form12_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' ✅ Terminate the entire application when any form is closed
        Application.Exit()
    End Sub

End Class
