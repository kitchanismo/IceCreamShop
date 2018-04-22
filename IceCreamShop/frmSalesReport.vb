Imports kitchanismo

Public Class frmSalesReport

    Dim moveObject As New MoveObject
    Dim pos As Point

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        pos = moveObject.GetLocation(Me)
    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = moveObject.SetLocation(pos, MousePosition)
        End If
    End Sub

    Private Sub frmPOS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpfrom.Value = getMinDate()
        dtpto.Value = getMaxDate()
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

    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        dtpfrom.Value = getMinDate()
        dtpto.Value = getMaxDate()
        viewComputedsales()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        viewComputedsales()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Function getMinDate() As DateTime
        Connected()
        sql = "SELECT min(date_order) FROM tblsales"
        CommandDB()
        dr = cmd.ExecuteReader()
        While (dr.Read())
            Return dr(0)
        End While
    End Function

    Function getMaxDate() As DateTime
        Connected()
        sql = "SELECT max(date_order) FROM tblsales"
        CommandDB()
        dr = cmd.ExecuteReader()
        While (dr.Read())
            Return dr(0)
        End While
    End Function

    Public Sub PopulateComputedSales(ByVal lv As ListView, ByVal fromdate As Date, ByVal todate As Date)
        Connected()
        With lv
            .Items.Clear()
            sql = " SELECT * FROM tblsales where date_order >  # " & fromdate & " #   AND date_order < # " & todate & " #  order by ORno"
            CommandDB()
            dr = cmd.ExecuteReader()
            lv.Items.Clear()
            While (dr.Read())
                With lv.Items.Add(dr("id"))
                    .SubItems.Add(dr("ORno"))
                    .SubItems.Add(dr("customer_name"))
                    .SubItems.Add(dr("prod_name"))
                    .SubItems.Add(dr("prod_price"))
                    .SubItems.Add(dr("prod_qty"))
                    .SubItems.Add(dr("prod_subtotal"))
                    .SubItems.Add(dr("date_order"))
                End With
            End While
        End With
    End Sub
End Class
