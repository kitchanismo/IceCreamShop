Public Class frmMain

    Private Sub InventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InventoryToolStripMenuItem.Click
        frmInventory.ShowDialog()
    End Sub

    Private Sub orderingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles orderingToolStripMenuItem.Click
        frmOrdering.ShowDialog()
    End Sub

    Private Sub POSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POSToolStripMenuItem.Click
        frmPOS.ShowDialog()
    End Sub



    Private Sub tmrDate_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDate.Tick
        lbldate.Text = Format(Now, "long date")
        lblTime.Text = TimeOfDay
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblUser.Text = frmLogin.cbopos.Text
    End Sub

    Private Sub lbllog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbllog.Click
        frmLogin.Show()
        Me.Close()
    End Sub
End Class