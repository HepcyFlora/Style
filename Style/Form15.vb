Imports System.Data
Imports Microsoft.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form15

    ' ✅ Database connection string
    Dim con As New SqlConnection("Data Source=DESKTOP-SJHEKCV\SQLEXPRESS;Initial Catalog=DEMO;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")

    ' ========================== FORM LOAD ==========================
    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ✅ Populate ComboBox with grouping options
        cmbGroupBy.Items.Add("Brand")
        cmbGroupBy.Items.Add("Color")
        cmbGroupBy.Items.Add("Type")
        cmbGroupBy.Items.Add("Month")
        cmbGroupBy.SelectedIndex = 0

        ' ✅ Load the initial charts
        GenerateCharts("Brand")
    End Sub

    ' ========================== GENERATE CHARTS ==========================
    Private Sub GenerateCharts(groupBy As String)
        ' Clear existing series and titles
        Chart1.Series.Clear()
        Chart2.Series.Clear()
        Chart1.Titles.Clear()
        Chart2.Titles.Clear()

        Try
            con.Open()

            ' ✅ Dynamic SQL query based on selection
            Dim query As String = ""

            Select Case groupBy
                Case "Brand"
                    query = "SELECT Brand, SUM(TotalPrice) AS TotalSales FROM Sales GROUP BY Brand"
                Case "Color"
                    query = "SELECT Color, SUM(TotalPrice) AS TotalSales FROM Sales GROUP BY Color"
                Case "Type"
                    query = "SELECT Type, SUM(TotalPrice) AS TotalSales FROM Sales GROUP BY Type"
                Case "Month"
                    query = "SELECT FORMAT(DateTime, 'yyyy-MM') AS Month, SUM(TotalPrice) AS TotalSales FROM Sales GROUP BY FORMAT(DateTime, 'yyyy-MM')"
            End Select

            Dim adapter As New SqlDataAdapter(query, con)
            Dim table As New DataTable()
            adapter.Fill(table)

            ' ✅ Add Titles
            Chart1.Titles.Add($"Sales Report by {groupBy} - Bar Chart")
            Chart2.Titles.Add($"Sales Report by {groupBy} - Pie Chart")

            ' ✅ Create Bar Chart Series
            Dim barSeries As New Series("BarSeries") With {
                .ChartType = SeriesChartType.Bar
            }
            Chart1.Series.Add(barSeries)

            ' ✅ Create Pie Chart Series
            Dim pieSeries As New Series("PieSeries") With {
                .ChartType = SeriesChartType.Pie
            }
            Chart2.Series.Add(pieSeries)

            ' ✅ Populate Chart Data
            For Each row As DataRow In table.Rows
                Dim label As String = row(0).ToString()
                Dim value As Decimal = Convert.ToDecimal(row(1))

                ' Add data to both charts
                barSeries.Points.AddXY(label, value)
                pieSeries.Points.AddXY(label, value)
            Next

        Catch ex As Exception
            MessageBox.Show($"Error loading chart data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    ' ========================== COMBOBOX SELECTION CHANGE ==========================
    Private Sub cmbGroupBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGroupBy.SelectedIndexChanged
        Dim groupBy As String = cmbGroupBy.SelectedItem.ToString()
        GenerateCharts(groupBy)
    End Sub

    ' ========================== EXPORT TO PDF (SIDE-BY-SIDE CHARTS) ==========================
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim saveDialog As New SaveFileDialog()
        saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
        saveDialog.FileName = "StyleSack_Sales_Report.pdf"

        If saveDialog.ShowDialog() = DialogResult.OK Then
            Try
                Dim pdfDoc As New Document(PageSize.A4.Rotate(), 10, 10, 10, 10)
                Dim writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream(saveDialog.FileName, FileMode.Create))
                pdfDoc.Open()

                ' ✅ Add Title with StyleSack Header
                Dim titleFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD)
                Dim title As New Paragraph($"StyleSack - Sales Report by {cmbGroupBy.SelectedItem}", titleFont)
                title.Alignment = Element.ALIGN_CENTER
                pdfDoc.Add(title)
                pdfDoc.Add(New Paragraph(" "))

                ' ✅ Export Bar Chart
                Dim barChartImg As New Bitmap(Chart1.Width, Chart1.Height)
                Chart1.DrawToBitmap(barChartImg, New System.Drawing.Rectangle(0, 0, Chart1.Width, Chart1.Height))

                ' ✅ Export Pie Chart
                Dim pieChartImg As New Bitmap(Chart2.Width, Chart2.Height)
                Chart2.DrawToBitmap(pieChartImg, New System.Drawing.Rectangle(0, 0, Chart2.Width, Chart2.Height))

                ' ✅ Combine Both Images Side by Side
                Dim combinedImgWidth As Integer = 700  ' Combined width for both charts
                Dim combinedImgHeight As Integer = 350

                Dim combinedImg As New Bitmap(combinedImgWidth, combinedImgHeight)
                Using g As Graphics = Graphics.FromImage(combinedImg)
                    g.FillRectangle(Brushes.White, 0, 0, combinedImgWidth, combinedImgHeight)

                    ' Draw both charts side by side
                    g.DrawImage(barChartImg, New System.Drawing.Rectangle(0, 0, 350, 300))
                    g.DrawImage(pieChartImg, New System.Drawing.Rectangle(350, 0, 350, 300))
                End Using

                ' ✅ Add the combined image to PDF
                Dim imgStream As New MemoryStream()
                combinedImg.Save(imgStream, Imaging.ImageFormat.Png)
                Dim imgCombined As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imgStream.ToArray())
                imgCombined.ScaleToFit(750.0F, 350.0F)   ' ✅ Fit both charts on one page
                pdfDoc.Add(imgCombined)

                pdfDoc.Close()

                MessageBox.Show("Charts exported successfully to PDF!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show($"Error exporting to PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' ========================== BACK BUTTON ==========================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' ✅ Go back to Form2
        Dim form2 As New Form2()
        Me.Hide()
        form2.ShowDialog()
        Me.Close()   ' Close Form15 after returning to Form2
    End Sub

    ' ========================== FORM CLOSING EVENT ==========================
    Private Sub Form15_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' ✅ Close the entire project when X button is clicked
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Exit()
        End If
    End Sub

End Class
