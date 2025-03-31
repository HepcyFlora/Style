<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form10
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUPIID = New System.Windows.Forms.TextBox()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.btnDecline = New System.Windows.Forms.Button()
        Me.btnValidateUPI = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.pnlScanner = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlUPIID = New System.Windows.Forms.Panel()
        Me.btnUPIScanner = New System.Windows.Forms.Button()
        Me.btnUPIID = New System.Windows.Forms.Button()
        Me.picQRCode = New System.Windows.Forms.PictureBox()
        Me.pnlScanner.SuspendLayout()
        Me.pnlUPIID.SuspendLayout()
        CType(Me.picQRCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Black", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(291, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(231, 38)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "UPI PAYMENTS"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.Label2.Location = New System.Drawing.Point(30, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(167, 25)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Enter the UPI ID"
        '
        'txtUPIID
        '
        Me.txtUPIID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUPIID.Location = New System.Drawing.Point(251, 11)
        Me.txtUPIID.Name = "txtUPIID"
        Me.txtUPIID.Size = New System.Drawing.Size(178, 30)
        Me.txtUPIID.TabIndex = 2
        '
        'lblLoading
        '
        Me.lblLoading.AutoSize = True
        Me.lblLoading.Location = New System.Drawing.Point(363, 578)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(0, 16)
        Me.lblLoading.TabIndex = 3
        '
        'btnConfirm
        '
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirm.Location = New System.Drawing.Point(522, 604)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(274, 45)
        Me.btnConfirm.TabIndex = 4
        Me.btnConfirm.Text = "Confirm Payment"
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'btnDecline
        '
        Me.btnDecline.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDecline.Location = New System.Drawing.Point(45, 604)
        Me.btnDecline.Name = "btnDecline"
        Me.btnDecline.Size = New System.Drawing.Size(280, 45)
        Me.btnDecline.TabIndex = 5
        Me.btnDecline.Text = "Decline Payment"
        Me.btnDecline.UseVisualStyleBackColor = True
        '
        'btnValidateUPI
        '
        Me.btnValidateUPI.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidateUPI.Location = New System.Drawing.Point(24, 77)
        Me.btnValidateUPI.Name = "btnValidateUPI"
        Me.btnValidateUPI.Size = New System.Drawing.Size(226, 37)
        Me.btnValidateUPI.TabIndex = 6
        Me.btnValidateUPI.Text = "Validate UPI"
        Me.btnValidateUPI.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI Black", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(686, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(129, 44)
        Me.btnBack.TabIndex = 7
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'pnlScanner
        '
        Me.pnlScanner.Controls.Add(Me.Label3)
        Me.pnlScanner.Controls.Add(Me.picQRCode)
        Me.pnlScanner.Location = New System.Drawing.Point(35, 171)
        Me.pnlScanner.Name = "pnlScanner"
        Me.pnlScanner.Size = New System.Drawing.Size(803, 267)
        Me.pnlScanner.TabIndex = 9
        Me.pnlScanner.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(40, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(311, 25)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Scan Here for UPI PAYMENTS"
        '
        'pnlUPIID
        '
        Me.pnlUPIID.Controls.Add(Me.Label2)
        Me.pnlUPIID.Controls.Add(Me.txtUPIID)
        Me.pnlUPIID.Controls.Add(Me.btnValidateUPI)
        Me.pnlUPIID.Location = New System.Drawing.Point(89, 444)
        Me.pnlUPIID.Name = "pnlUPIID"
        Me.pnlUPIID.Size = New System.Drawing.Size(697, 131)
        Me.pnlUPIID.TabIndex = 9
        Me.pnlUPIID.Visible = False
        '
        'btnUPIScanner
        '
        Me.btnUPIScanner.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUPIScanner.Location = New System.Drawing.Point(145, 116)
        Me.btnUPIScanner.Name = "btnUPIScanner"
        Me.btnUPIScanner.Size = New System.Drawing.Size(241, 40)
        Me.btnUPIScanner.TabIndex = 10
        Me.btnUPIScanner.Text = "UPI SCANNER"
        Me.btnUPIScanner.UseVisualStyleBackColor = True
        '
        'btnUPIID
        '
        Me.btnUPIID.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUPIID.Location = New System.Drawing.Point(505, 116)
        Me.btnUPIID.Name = "btnUPIID"
        Me.btnUPIID.Size = New System.Drawing.Size(224, 40)
        Me.btnUPIID.TabIndex = 11
        Me.btnUPIID.Text = "UPI ID"
        Me.btnUPIID.UseVisualStyleBackColor = True
        '
        'picQRCode
        '
        Me.picQRCode.Image = Global.Style.My.Resources.Resources.WhatsApp_Image_2025_03_29_at_16_10_46__1_
        Me.picQRCode.Location = New System.Drawing.Point(470, 6)
        Me.picQRCode.Name = "picQRCode"
        Me.picQRCode.Size = New System.Drawing.Size(315, 261)
        Me.picQRCode.TabIndex = 8
        Me.picQRCode.TabStop = False
        '
        'Form10
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Style.My.Resources.Resources.WhatsApp_Image_2025_03_31_at_23_09_04
        Me.ClientSize = New System.Drawing.Size(931, 676)
        Me.Controls.Add(Me.pnlUPIID)
        Me.Controls.Add(Me.btnUPIID)
        Me.Controls.Add(Me.btnUPIScanner)
        Me.Controls.Add(Me.lblLoading)
        Me.Controls.Add(Me.pnlScanner)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnDecline)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form10"
        Me.Text = "Form10"
        Me.pnlScanner.ResumeLayout(False)
        Me.pnlScanner.PerformLayout()
        Me.pnlUPIID.ResumeLayout(False)
        Me.pnlUPIID.PerformLayout()
        CType(Me.picQRCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtUPIID As TextBox
    Friend WithEvents lblLoading As Label
    Friend WithEvents btnConfirm As Button
    Friend WithEvents btnDecline As Button
    Friend WithEvents btnValidateUPI As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents picQRCode As PictureBox
    Friend WithEvents pnlScanner As Panel
    Friend WithEvents pnlUPIID As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents btnUPIScanner As Button
    Friend WithEvents btnUPIID As Button
End Class
