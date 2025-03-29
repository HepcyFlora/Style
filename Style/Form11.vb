Imports System.Data
Imports System.Text.RegularExpressions
Imports Microsoft.Data.SqlClient

Public Class Form11

    ' ✅ Database connection
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ✅ Populate ComboBoxes
        PopulateBankNames()
        cmbBankBranch.Enabled = False  ' Disable branch selection until bank is selected
    End Sub

    ' ========================== POPULATE BANK NAMES ==========================
    Private Sub PopulateBankNames()
        Try
            cmbBankName.Items.Clear()

            ' ✅ Add sample bank names
            cmbBankName.Items.Add("State Bank of India")
            cmbBankName.Items.Add("HDFC Bank")
            cmbBankName.Items.Add("ICICI Bank")
            cmbBankName.Items.Add("Axis Bank")
            cmbBankName.Items.Add("Punjab National Bank")

        Catch ex As Exception
            MessageBox.Show("Error loading bank names: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================== POPULATE BRANCHES BASED ON BANK NAME ==========================
    Private Sub cmbBankName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBankName.SelectedIndexChanged
        cmbBankBranch.Items.Clear()

        ' ✅ Enable branch selection only when bank is selected
        cmbBankBranch.Enabled = True

        Select Case cmbBankName.SelectedItem.ToString()
            Case "State Bank of India"
                cmbBankBranch.Items.Add("MG Road Branch")
                cmbBankBranch.Items.Add("BTM Layout Branch")
                cmbBankBranch.Items.Add("Whitefield Branch")

            Case "HDFC Bank"
                cmbBankBranch.Items.Add("Koramangala Branch")
                cmbBankBranch.Items.Add("Indiranagar Branch")
                cmbBankBranch.Items.Add("JP Nagar Branch")

            Case "ICICI Bank"
                cmbBankBranch.Items.Add("Electronic City Branch")
                cmbBankBranch.Items.Add("HSR Layout Branch")
                cmbBankBranch.Items.Add("Marathahalli Branch")

            Case "Axis Bank"
                cmbBankBranch.Items.Add("Rajajinagar Branch")
                cmbBankBranch.Items.Add("Yelahanka Branch")
                cmbBankBranch.Items.Add("Hebbal Branch")

            Case "Punjab National Bank"
                cmbBankBranch.Items.Add("Banashankari Branch")
                cmbBankBranch.Items.Add("Jayanagar Branch")
                cmbBankBranch.Items.Add("Malleshwaram Branch")
        End Select
    End Sub

    ' ========================== CONFIRM PAYMENT ==========================
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Try
            ' ✅ Validate inputs
            If Not ValidateInputs() Then
                Return
            End If

            ' ✅ Proceed with payment confirmation
            MessageBox.Show("Payment confirmed. Stock will be automatically reduced.", "Payment Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
        ' ✅ Validate Bank Holder Name
        If String.IsNullOrWhiteSpace(txtHolderName.Text) Then
            MessageBox.Show("Please enter the bank holder's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' ✅ Validate Bank Account Number (9-15 digits, only numbers)
        Dim accountNumber As String = txtAccountNumber.Text.Trim()
        If Not Regex.IsMatch(accountNumber, "^\d{9,15}$") Then
            MessageBox.Show("Bank Account Number must contain only digits and be 9-15 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' ✅ Validate IFSC Code (11 alphanumeric characters)
        Dim ifscCode As String = txtIFSC.Text.Trim()
        If Not Regex.IsMatch(ifscCode, "^[A-Za-z0-9]{11}$") Then
            MessageBox.Show("IFSC Code must be exactly 11 alphanumeric characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' ✅ Validate Bank and Branch Selection
        If cmbBankName.SelectedIndex = -1 OrElse cmbBankBranch.SelectedIndex = -1 Then
            MessageBox.Show("Please select both bank and branch.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
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

End Class
