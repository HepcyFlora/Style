<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.btnStaffCreation = New System.Windows.Forms.Button()
        Me.btnCustomerMgmt = New System.Windows.Forms.Button()
        Me.btnInventoryMgmt = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Black", 19.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(248, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(370, 45)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ADMIN DASHBOARD"
        '
        'btnStaffCreation
        '
        Me.btnStaffCreation.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStaffCreation.Location = New System.Drawing.Point(104, 182)
        Me.btnStaffCreation.Name = "btnStaffCreation"
        Me.btnStaffCreation.Size = New System.Drawing.Size(272, 67)
        Me.btnStaffCreation.TabIndex = 6
        Me.btnStaffCreation.Text = "Create Staff"
        Me.btnStaffCreation.UseVisualStyleBackColor = True
        '
        'btnCustomerMgmt
        '
        Me.btnCustomerMgmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerMgmt.Location = New System.Drawing.Point(456, 182)
        Me.btnCustomerMgmt.Name = "btnCustomerMgmt"
        Me.btnCustomerMgmt.Size = New System.Drawing.Size(293, 67)
        Me.btnCustomerMgmt.TabIndex = 7
        Me.btnCustomerMgmt.Text = "Customer Mgmt"
        Me.btnCustomerMgmt.UseVisualStyleBackColor = True
        '
        'btnInventoryMgmt
        '
        Me.btnInventoryMgmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventoryMgmt.Location = New System.Drawing.Point(104, 304)
        Me.btnInventoryMgmt.Name = "btnInventoryMgmt"
        Me.btnInventoryMgmt.Size = New System.Drawing.Size(272, 65)
        Me.btnInventoryMgmt.TabIndex = 8
        Me.btnInventoryMgmt.Text = "Inventory Mgmt"
        Me.btnInventoryMgmt.UseVisualStyleBackColor = True
        '
        'btnReport
        '
        Me.btnReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.Location = New System.Drawing.Point(456, 304)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(293, 65)
        Me.btnReport.TabIndex = 9
        Me.btnReport.Text = "Report"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'btnLogout
        '
        Me.btnLogout.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogout.Location = New System.Drawing.Point(668, 423)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(163, 60)
        Me.btnLogout.TabIndex = 10
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(898, 537)
        Me.Controls.Add(Me.btnLogout)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.btnInventoryMgmt)
        Me.Controls.Add(Me.btnCustomerMgmt)
        Me.Controls.Add(Me.btnStaffCreation)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btnStaffCreation As Button
    Friend WithEvents btnCustomerMgmt As Button
    Friend WithEvents btnInventoryMgmt As Button
    Friend WithEvents btnReport As Button
    Friend WithEvents btnLogout As Button
End Class
