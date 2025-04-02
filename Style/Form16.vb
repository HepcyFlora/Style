Imports System.Data
Imports Microsoft.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Drawing.Printing

Public Class Form16

    ' ✅ Database connection string
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form16_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSales()
    End Sub

    ' ========================== LOAD SALES DATA ==========================
    Private Sub LoadSales()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If

        Try
            con.Open()
            Dim query As String = "SELECT SalesID, CustomerName, Contact, Email, Brand, Type, Color, Quantity, TotalPrice, PaymentMethod, DateTime FROM Sales"
            Dim adapter As New SqlDataAdapter(query, con)
            Dim table As New DataTable()
            adapter.Fill(table)

            ' ✅ Bind data to DataGridView
            dgvSales.DataSource = table

        Catch ex As Exception
            Debug.WriteLine("Error loading sales data: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    ' ========================== REFRESH BUTTON ==========================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadSales()
    End Sub

    ' ========================== DELETE SELECTED SALES RECORD ==========================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvSales.SelectedRows.Count > 0 Then
            Dim selectedID As Integer = CInt(dgvSales.SelectedRows(0).Cells("SalesID").Value)

            ' ✅ Confirm deletion
            If MessageBox.Show($"Are you sure you want to delete Sales ID {selectedID}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    con.Open()
                    Dim query As String = "DELETE FROM Sales WHERE SalesID = @SalesID"
                    Using cmd As New SqlCommand(query, con)
                        cmd.Parameters.AddWithValue("@SalesID", selectedID)
                        cmd.ExecuteNonQuery()
                    End Using

                    MessageBox.Show($"Sales record {selectedID} deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    Debug.WriteLine("Error deleting sales record: " & ex.Message)
                Finally
                    con.Close()
                End Try
            End If
        Else
            MessageBox.Show("Please select a sales record to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' ========================== EXPORT TO PDF (LANDSCAPE) WITH SMALLER FONT ==========================
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If dgvSales.Rows.Count = 0 Then
            MessageBox.Show("No sales data available to export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim saveDialog As New SaveFileDialog()
        saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
        saveDialog.FileName = "Sales_Report.pdf"

        If saveDialog.ShowDialog() = DialogResult.OK Then
            Try
                ' ✅ Export in landscape mode
                Dim pdfDoc As New iTextSharp.text.Document(PageSize.A4.Rotate(), 10, 10, 10, 10)
                Dim writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream(saveDialog.FileName, FileMode.Create))

                pdfDoc.Open()

                ' ✅ Add StyleSack Title with smaller font
                Dim headerFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD, BaseColor.BLUE)
                Dim header As New Paragraph("StyleSack", headerFont)
                header.Alignment = Element.ALIGN_CENTER
                pdfDoc.Add(header)

                ' ✅ Add Sales Report Title with smaller font
                Dim titleFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD)
                Dim title As New Paragraph("Sales", titleFont)
                title.Alignment = Element.ALIGN_CENTER
                pdfDoc.Add(title)
                pdfDoc.Add(New Paragraph(" "))

                ' ✅ Create PDF table
                Dim pdfTable As New PdfPTable(dgvSales.Columns.Count)
                pdfTable.WidthPercentage = 100

                ' ✅ Add headers with smaller font
                For Each column As DataGridViewColumn In dgvSales.Columns
                    Dim cellFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD)
                    Dim cell As New PdfPCell(New Phrase(column.HeaderText, cellFont))
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY
                    cell.Padding = 5  ' Adjust padding for readability
                    pdfTable.AddCell(cell)
                Next

                ' ✅ Add data rows with smaller font
                For Each row As DataGridViewRow In dgvSales.Rows
                    If Not row.IsNewRow Then
                        For Each cell As DataGridViewCell In row.Cells
                            Dim dataFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL)
                            Dim dataCell As New PdfPCell(New Phrase(If(cell.Value IsNot Nothing, cell.Value.ToString(), ""), dataFont))
                            dataCell.Padding = 4
                            pdfTable.AddCell(dataCell)
                        Next
                    End If
                Next

                pdfDoc.Add(pdfTable)
                pdfDoc.Close()

                MessageBox.Show("Sales report exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                Debug.WriteLine("Error exporting to PDF: " & ex.Message)
            End Try
        End If
    End Sub

    ' ========================== SEARCH SALES ==========================
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim searchText As String = txtSearch.Text.Trim()

        Try
            ' ✅ Open connection only if closed
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            ' ✅ Search query (filters by multiple columns)
            Dim query As String = "SELECT * FROM Sales 
                               WHERE CustomerName LIKE @Search 
                               OR Contact LIKE @Search 
                               OR Email LIKE @Search 
                               OR Brand LIKE @Search 
                               OR Type LIKE @Search 
                               OR Color LIKE @Search 
                               ORDER BY DateTime ASC"

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@Search", "%" & searchText & "%")

            Dim adapter As New SqlDataAdapter(cmd)
            Dim table As New DataTable()
            adapter.Fill(table)

            ' ✅ Bind the result to DataGridView
            dgvSales.DataSource = table

        Catch ex As Exception
            MessageBox.Show("Error searching sales: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ' ✅ Ensure the connection is closed
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub


    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' ✅ Go back to Form2
        Me.Hide()

        ' We assume Form2 has already been instantiated or is created when needed
        Dim form2 As New Form2()
        form2.ShowDialog()

        ' Close Form16 after navigating to Form2
        Me.Close()
    End Sub

    ' ========================== FORM CLOSING EVENT ==========================
    Private Sub Form16_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' ✅ Close the entire project when X button is clicked
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Exit()
        End If
    End Sub

End Class
