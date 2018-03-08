Public Class frmLogin

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Connected()
    End Sub

    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        If IsUserAuthentic(cbopos.Text.ToLower, tbpcode.Text.ToLower) = True Then
            MsgBox("Successfully login", MsgBoxStyle.Information)
            frmMain.Show()
            If cbopos.Text = "Employee" Then

                frmMain.InventoryToolStripMenuItem.Visible = False
                frmMain.POSToolStripMenuItem.Visible = False
                frmMain.ReportsToolStripMenuItem.Visible = False
            Else
                frmMain.InventoryToolStripMenuItem.Visible = True
                frmMain.POSToolStripMenuItem.Visible = True
                frmMain.ReportsToolStripMenuItem.Visible = True

            End If
            Me.Hide()
            cbopos.Text = "Select"
            tbpcode.Text = ""
        Else
            MsgBox("Incorrect passcode", MsgBoxStyle.Critical)
        End If
    End Sub

    'check if user is valid
    Function IsUserAuthentic(ByVal u As String, ByVal p As String) As Boolean
        Connected()
        sql = "SELECT * FROM tbluser WHERE [position] = '" & u & "' AND [passcode] = '" & p & "' "
        cmd = New OleDb.OleDbCommand(sql, conn)
        dr = cmd.ExecuteReader()
        If (dr.Read() = True) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        End
    End Sub
End Class
