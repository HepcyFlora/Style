Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class Form9

    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    Private Sub btnCashReceived_Click(sender As Object, e As EventArgs) Handles btnCashReceived.Click
        If MessageBox.Show("Confirm payment?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            ReduceStock()
            Dim receiptForm As New Form13()
            Me.Hide()
            receiptForm.ShowDialog()
        End If
    End Sub

    Private Sub btnCashNotReceived_Click(sender As Object, e As EventArgs) Handles btnCashNotReceived.Click
        MessageBox.Show("Payment declined.")
        Me.Close()
    End Sub

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

End Class
