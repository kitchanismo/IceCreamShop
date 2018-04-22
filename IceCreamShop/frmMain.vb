Public Class frmMain

    Private Sub InventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InventoryToolStripMenuItem.Click
        frmInventory.ShowDialog()
    End Sub

    Private Sub orderingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles orderingToolStripMenuItem.Click
        frmPOS.ShowDialog()
    End Sub

    Private Sub POSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POSToolStripMenuItem.Click
        frmSalesReport.ShowDialog()
    End Sub

    Private Sub tmrDate_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDate.Tick
        lbldate.Text = Format(Now, "long date")
        lblTime.Text = TimeOfDay
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub lbllog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TransactionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransactionToolStripMenuItem.Click

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreport.Click
        frmSalesReport.ShowDialog()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninventory.Click
        frmInventory.ShowDialog()
    End Sub

    Private Sub btnpay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpos.Click
        frmPOS.ShowDialog()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        frmLogin.Show()
        Me.Hide()
    End Sub
End Class