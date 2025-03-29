Public Class Form8

    ' ✅ Shared variable to hold the payment method for cross-form access
    Public Shared PaymentMethod As String

    ' ========================== NAVIGATE TO PAYMENT FORMS ==========================

    ' Cash Payment Button → Navigates to Form9
    Private Sub btnCash_Click(sender As Object, e As EventArgs) Handles btnCash.Click
        PaymentMethod = "Cash"
        Dim formCash As New Form9()
        Me.Hide()
        formCash.ShowDialog()
        Me.Show()
    End Sub

    ' UPI Payment Button → Navigates to Form10
    Private Sub btnUPI_Click(sender As Object, e As EventArgs) Handles btnUPI.Click
        PaymentMethod = "UPI"
        Dim formUPI As New Form10()
        Me.Hide()
        formUPI.ShowDialog()
        Me.Show()
    End Sub

    ' Bank Transfer Button → Navigates to Form11
    Private Sub btnBankTransfer_Click(sender As Object, e As EventArgs) Handles btnBankTransfer.Click
        PaymentMethod = "Bank Transfer"
        Dim formBank As New Form11()
        Me.Hide()
        formBank.ShowDialog()
        Me.Show()
    End Sub

    ' Credit/Debit Card Button → Navigates to Form12
    Private Sub btnCard_Click(sender As Object, e As EventArgs) Handles btnCard.Click
        PaymentMethod = "Credit/Debit Card"
        Dim formCard As New Form12()
        Me.Hide()
        formCard.ShowDialog()
        Me.Show()
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' Navigate back to Form7 (Customer Details)
        Dim form7 As New Form7()
        Me.Hide()
        form7.ShowDialog()
        Me.Close()
    End Sub

    ' ========================== FORM LOAD EVENT ==========================
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize the form
        PaymentMethod = ""
    End Sub

End Class
