Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class Form9

    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ✅ Form load logic (if any)
    End Sub

    ' ========================== CASH RECEIVED BUTTON ==========================
    Private Sub btnCashReceived_Click(sender As Object, e As EventArgs) Handles btnCashReceived.Click
        If MessageBox.Show("Confirm payment?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            ReduceStock()
            Dim receiptForm As New Form13()
            Me.Hide()
            receiptForm.ShowDialog()
        End If
    End Sub

    ' ========================== CASH NOT RECEIVED BUTTON ==========================
    Private Sub btnCashNotReceived_Click(sender As Object, e As EventArgs) Handles btnCashNotReceived.Click
        MessageBox.Show("Payment declined.")
        Me.Close()
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' ✅ Navigate to Form8
        Dim form8 As New Form8()
        Me.Hide()                   ' Hide Form9
        form8.ShowDialog()          ' Show Form8 as a dialog
        Me.Close()                   ' Close Form9 after navigation
    End Sub

    ' ========================== REDUCE STOCK ==========================
    Private Sub ReduceStock()
        Try
            con.Open()
            Dim transaction As SqlTransaction = con.BeginTransaction()

            For Each row As DataRow In Form6.cart.Rows
                Dim cmd As New SqlCommand("UPDATE Products SET StockQuantity = StockQuantity - @quantity WHERE ProductID = @productID", con, transaction)
                cmd.Parameters.AddWithValue("@quantity", CInt(row("Quantity")))
                cmd.Parameters.AddWithValue("@productID", CInt(row("ProductID")))
                cmd.ExecuteNonQuery()
            Next

            transaction.Commit()
            con.Close()
        Catch ex As Exception
            MessageBox.Show("Error reducing stock: " & ex.Message)
        End Try
    End Sub

    ' ========================== FORM CLOSING EVENT ==========================
    Private Sub Form9_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' ✅ Only close the entire application when the X button is clicked
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Exit()
        End If
    End Sub

End Class
