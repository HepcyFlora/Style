<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form9
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
        Me.btnCashNotReceived = New System.Windows.Forms.Button()
        Me.btnCashReceived = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnCashNotReceived
        '
        Me.btnCashNotReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCashNotReceived.Location = New System.Drawing.Point(47, 263)
        Me.btnCashNotReceived.Name = "btnCashNotReceived"
        Me.btnCashNotReceived.Size = New System.Drawing.Size(331, 59)
        Me.btnCashNotReceived.TabIndex = 0
        Me.btnCashNotReceived.Text = "Cash not Collected"
        Me.btnCashNotReceived.UseVisualStyleBackColor = True
        '
        'btnCashReceived
        '
        Me.btnCashReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCashReceived.Location = New System.Drawing.Point(457, 263)
        Me.btnCashReceived.Name = "btnCashReceived"
        Me.btnCashReceived.Size = New System.Drawing.Size(292, 61)
        Me.btnCashReceived.TabIndex = 1
        Me.btnCashReceived.Text = "Cash Collected"
        Me.btnCashReceived.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(625, 44)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(133, 40)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "BACK"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Black", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(287, 45)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "CASH PAYMENT"
        '
        'Form9
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Style.My.Resources.Resources.WhatsApp_Image_2025_03_31_at_23_14_35
        Me.ClientSize = New System.Drawing.Size(823, 421)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnCashReceived)
        Me.Controls.Add(Me.btnCashNotReceived)
        Me.Name = "Form9"
        Me.Text = "Form9"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCashNotReceived As Button
    Friend WithEvents btnCashReceived As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents Label1 As Label
End Class
