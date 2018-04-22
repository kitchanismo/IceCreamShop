Imports kitchanismo

Public Class frmInventory
    Dim id As Integer

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

    Private Sub frmInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'load all products in listview
        PopulateProducts(lvProducts, "")
        'load all category in combobox
        PopulateCategory(cbocategory)
        doChangeListViewColor(lvProducts)
        getDate()
    End Sub



    Private Sub tbsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbsearch.TextChanged
        'search products category or name
        SearchProduct(lvProducts, tbsearch.Text)
        doChangeListViewColor(lvProducts)
    End Sub

    Private Sub lvProducts_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProducts.MouseClick
        ' Dim d As Date
        With lvProducts.SelectedItems(0)
            id = .SubItems(0).Text
            cbocategory.Text = .SubItems(1).Text
            tbname.Text = .SubItems(2).Text
            tbprice.Text = .SubItems(3).Text
            tbstock.Text = .SubItems(4).Text
            tbpurchase.Text = .SubItems(5).Text
            tbpass.Text = .SubItems(6).Text
        End With
        pndetails.Enabled = True
    End Sub


    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        If btnadd.Text = "Add" Then
            btnadd.Text = "Save"
            btnedit.Text = "Cancel"
            btndelete.Visible = False
            btnclear.Visible = False
            pndetails.Enabled = True
            getDate()
            ClearUserInput()
        Else
            If IsUserInputValid() = True Then
                SaveData()
                ClearUserInput()
                doChangeListViewColor(lvProducts)
                MsgBox("Successfully added", MsgBoxStyle.Information)

            End If
        End If
    End Sub


    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        If btnedit.Text = "Update" Then
            If id <> 0 Then
                EditData()
                doChangeListViewColor(lvProducts)
                MsgBox("Successfully updated", MsgBoxStyle.Information)
            Else
                MsgBox("Select on product list")
            End If
        Else
            btnadd.Text = "Add"
            btnedit.Text = "Update"
            btndelete.Visible = True
            btnclear.Visible = True
            pndetails.Enabled = False
        End If

    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
        ClearUserInput()
    End Sub


    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        ClearUserInput()
        PopulateProducts(lvProducts, "prod_category")
        pndetails.Enabled = False
        doChangeListViewColor(lvProducts)
    End Sub

#Region "Functions and Methods"



    Sub SaveData()
        Connected()
        sql = "INSERT INTO tblproducts (prod_category, prod_name, prod_price, prod_stock, prod_purchasedate, prod_passdate) VALUES ('" & cbocategory.Text.ToUpper & "','" & tbname.Text.ToUpper & "'," & tbprice.Text & "," & tbstock.Text & ",'" & tbpurchase.Text.ToString & "','" & tbpass.Text.ToString & "')"
        CommandDB()
        dr = cmd.ExecuteReader()
        PopulateProducts(lvProducts, "prod_category")
    End Sub

    Sub EditData()
        Connected()
        sql = "UPDATE tblproducts SET prod_category = '" & cbocategory.Text.ToUpper & "', prod_name = '" & tbname.Text.ToUpper & "', prod_price = " & tbprice.Text & " , prod_stock = " & tbstock.Text & ", prod_purchasedate = '" & tbpurchase.Text.ToString & "', prod_passdate = '" & tbpass.Text.ToString & "' where id = " & id & ""
        CommandDB()
        cmd.ExecuteNonQuery()
        PopulateProducts(lvProducts, "prod_category")
    End Sub

    Sub DeleteData()
        Connected()
        Dim i As Integer
        sql = "DELETE FROM tblproducts WHERE id = " & id & ""
        'validattion
        If id = "0" Then
            MsgBox("Select on product list", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        CommandDB()
        i = MsgBox("Are you sure you want to delete " & tbname.Text.ToUpper & "?", MsgBoxStyle.YesNo)
        If i = MsgBoxResult.Yes Then
            'execute delete query
            cmd.ExecuteNonQuery()
            MsgBox("Succesfully deleted", MsgBoxStyle.Information)
            PopulateProducts(lvProducts, "prod_category")
        End If
    End Sub

    Function IsUserInputValid() As Boolean
        If cbocategory.Text = "" Or cbocategory.Text.ToLower = "select" Or tbname.Text = "" Or tbstock.Text = "" Or tbprice.Text = "" Or tbpurchase.Text = "" Or tbpass.Text = "" Then
            MsgBox("Missing input", MsgBoxStyle.Critical)
            Return False
        ElseIf IsProductExist("tblproducts", "prod_name", tbname.Text) = True Then
            MsgBox("Product " & tbname.Text.ToUpper & " Exist", MsgBoxStyle.Critical)
            Return False
        End If
        Return True
    End Function

    Sub ClearUserInput()
        id = 0
        cbocategory.Text = "Select"
        tbname.Text = ""
        tbprice.Text = ""
        tbstock.Text = ""
        tbsearch.Text = ""
        getDate()
    End Sub


    Sub getDate()
        tbpurchase.Text = Format(Now, "short date")
        tbpass.Text = Format(Now, "short date")
    End Sub

#End Region



    'validation for enter number only in price
    Private Sub tbprice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbstock.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

End Class
