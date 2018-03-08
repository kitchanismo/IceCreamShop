Public Class frmPOS

    Private Sub frmPOS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpfrom.Value = Date.Now
        dtpto.Value = Date.Now
        viewComputedsales()
    End Sub

    Function ComputeCollumn(ByVal col As Integer) As Integer
        Dim sum = (From lvi In lvsales.Items _
        Let value = CInt(DirectCast(lvi, ListViewItem).SubItems(col).Text) _
        Select value).Sum
        Return sum
    End Function

    Sub viewComputedsales()
        Dim datefrom As Date = dtpfrom.Value.ToShortDateString
        Dim dateto As Date = dtpto.Value.ToShortDateString
        PopulateComputedSales(lvsales, datefrom.AddDays(-1), dateto.AddDays(1))
        tbtotal.Text = "Php. " & FormatNumber(ComputeCollumn(6))
    End Sub
    ' Private Sub dtpfrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpfrom.ValueChanged, dtpto.ValueChanged

    ' End Sub

    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        PopulateTotalSales(lvsales)
        tbtotal.Text = "Php. " & FormatNumber(getTotalSales())
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        viewComputedsales()
    End Sub
End Class
