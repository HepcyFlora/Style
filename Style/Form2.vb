﻿Imports System.Data.SqlClient
Imports Microsoft.Data.SqlClient ' ✅ Added Microsoft.Data.SqlClient

Public Class Form2
    ' ========================== FORM CLOSING EVENT ==========================
    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' ✅ Terminate the entire application when any form is closed
        Application.Exit()
    End Sub


    ' ================== Staff Creation Button ==================
    Private Sub btnStaffCreation_Click(sender As Object, e As EventArgs) Handles btnStaffCreation.Click
        ' Navigate to Staff Creation Form as Modal Dialog
        Dim staffForm As New Form3() ' Form3 → Staff Creation
        Me.Hide()
        staffForm.ShowDialog()  ' ✅ Modal Dialog
        Me.Show()               ' Show Form2 again after closing Form3
    End Sub

    ' ================== Customer Management Button ==================
    Private Sub btnCustomerMgmt_Click(sender As Object, e As EventArgs) Handles btnCustomerMgmt.Click
        ' Navigate to Customer Management Form as Modal Dialog
        Dim customerForm As New Form14() ' Form14 → Customer Management
        Me.Hide()
        customerForm.ShowDialog()   ' ✅ Modal Dialog
        Me.Show()                   ' Show Form2 again after closing Form14
    End Sub

    ' ================== Inventory Management Button ==================
    Private Sub btnInventoryMgmt_Click(sender As Object, e As EventArgs) Handles btnInventoryMgmt.Click
        ' Navigate to Inventory Management Form as Modal Dialog
        Dim inventoryForm As New Form5() ' Form5 → Inventory Management
        Me.Hide()
        inventoryForm.ShowDialog()   ' ✅ Modal Dialog
        Me.Show()                    ' Show Form2 again after closing Form5
    End Sub

    ' ================== Report Button ==================
    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        ' Navigate to Report Form as Modal Dialog
        Dim reportForm As New Form15() ' Form15 → Report
        Me.Hide()
        reportForm.ShowDialog()    ' ✅ Modal Dialog
        Me.Show()                  ' Show Form2 again after closing Form15
    End Sub

    ' ================== Sales Button (NEW) ==================
    Private Sub btnSales_Click(sender As Object, e As EventArgs) Handles btnSales.Click
        ' Navigate to Sales Form (Form16) as Modal Dialog
        Dim salesForm As New Form16() ' Form16 → Sales Management
        Me.Hide()
        salesForm.ShowDialog()    ' ✅ Modal Dialog
        Me.Show()                  ' Show Form2 again after closing Form16
    End Sub

    ' ================== Logout Button ==================
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' Navigate back to Login Page as Modal Dialog
        Dim loginForm As New Form1()
        Me.Hide()
        loginForm.ShowDialog()     ' ✅ Modal Dialog
        Me.Close()                  ' Close Form2
    End Sub

    ' ================== Form Load ==================
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Optional: Initialize settings here
    End Sub

End Class




