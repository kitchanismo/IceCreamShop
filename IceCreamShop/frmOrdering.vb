Public Class frmOrdering
    Dim id As Integer
    Dim prodcategory As String
    Dim prodname As String
    Dim prodprice As Integer
    Dim prodstock As Integer
    Dim enterQty As Integer
    Dim newStock As Integer
    Dim subtotal As Double
    Dim ctr As Integer = 0

    Private Sub frmOrdering_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If lvorder.Items.Count <> 0 Then
            Dim i As Integer = MsgBox("Cancel Transaction?", MsgBoxStyle.YesNo)
            If i = MsgBoxResult.Yes Then
                CancelTransaction()
            Else
                e.Cancel = True
            End If
        End If
    End Sub
    Private Sub frmOrdering_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'load all products in listview
        btncancel.Enabled = False
        PopulateProducts(lvProducts)
        lblORno.Text = GenerateOrderNo()
    End Sub

    Private Sub lvProducts_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProducts.MouseDoubleClick
        clickeData()

        'enter qty order
        enterQty = Val(InputBox("Product name: " & prodname & vbNewLine & "Product price: " & prodprice, "Enter Quantity".ToUpper, 1))
        If enterQty = Nothing Then
            Exit Sub
        End If

        'item qty ordered  > item qty stock 
        If enterQty > prodstock Then
            MsgBox("There is no enough number of Stock!", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If

        'enable button cancel transaction 
        btncancel.Enabled = True

        'subtract qty stock to qty ordered
        newStock = Val(prodstock) - Val(enterQty)

        'update stock with new stock
        updateStock(newStock, prodname)

        'compute subtotal
        subtotal = Val(enterQty) * Val(prodprice)

        'update qty and subtotal if an item is existed in order list
        While (ctr <> lvorder.Items.Count)
            If lvorder.Items(ctr).SubItems(0).Text.ToLower = prodname.ToLower Then
                lvorder.Items(ctr).SubItems(2).Text += enterQty
                lvorder.Items(ctr).SubItems(3).Text += subtotal
                lbltotal.Text = ComputeCollumn(3)
                lblitems.Text = ComputeCollumn(2)
                lblchange.Text = ComputeChange()
                ctr = 0
                Exit Sub
            End If
            ctr += 1
        End While

        'add to Order list
        With lvorder.Items.Add(prodname)
            .SubItems.Add(prodprice)
            .SubItems.Add(enterQty)
            .SubItems.Add(subtotal)
        End With
      
        'compute no of item purchased
        lbltotal.Text = ComputeCollumn(3)
        lblitems.Text = ComputeCollumn(2)
        lblchange.Text = ComputeChange()
        ctr = 0
    End Sub

    Private Sub lvorder_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvorder.MouseDoubleClick
        'get require data in lvorder
        Dim qtyorder As Integer
        Dim enterOrder As Integer
        Dim newsubtotal As Integer

        With lvorder.SelectedItems(0)
            prodname = .SubItems(0).Text
            prodprice = .SubItems(1).Text
            qtyorder = .SubItems(2).Text
        End With

a:      enterOrder = Val(InputBox("Product name: " & prodname & vbNewLine & "Product price: " & prodprice, "Enter Quantity Return".ToUpper, 1))
        If enterOrder = Nothing Then
            Exit Sub
        End If

        If Val(qtyorder) < Val(enterOrder) Then
            MsgBox("Quantity return is more than quantity ordered!", MsgBoxStyle.Critical, "Error")
            GoTo a
        End If

        newsubtotal = enterOrder * prodprice
        'add qty ordered to qty stock
        newStock = Val(enterOrder) + Val(getProductStock(prodname))
        'update stock in products
        updateStock(newStock, prodname)

        'update qty and subtotal if an item is existed in order list
        While (ctr <> lvorder.Items.Count)
            If lvorder.Items(ctr).SubItems(0).Text.ToLower = prodname.ToLower Then

                'assign
                lvorder.Items(ctr).SubItems(2).Text -= enterOrder
                lvorder.Items(ctr).SubItems(3).Text -= newsubtotal
                lbltotal.Text = ComputeCollumn(3)
                lblitems.Text = ComputeCollumn(2)
                lblchange.Text = ComputeChange()

                'if qty = 0 then remove to list
                If lvorder.Items(ctr).SubItems(2).Text = 0 Then
                    lvorder.Items.RemoveAt(lvorder.SelectedIndices(0))
                End If

                If lvorder.Items.Count = 0 Then
                    btncancel.Enabled = False
                End If

                'reset
                ctr = 0
                Exit Sub
            End If

            'increment
            ctr += 1
        End While

    End Sub

    Private Sub tbcash_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbcash.TextChanged
        lblchange.Text = ComputeChange()
    End Sub

    Private Sub btnpay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpay.Click
        If Val(tbcash.Text) < Val(lbltotal.Text) Then
            MsgBox("Insufficient Cash!", MsgBoxStyle.Critical)
            Exit Sub
        End If
        SaveData()
        resetOrder()
    End Sub

    Private Sub tbsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbsearch.TextChanged
        SearchProduct(lvProducts, tbsearch.Text)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        If lvorder.Items.Count() = 0 Then
            Exit Sub
        End If
        Dim i As Integer = MsgBox("Cancel Transaction?", MsgBoxStyle.YesNo)
        If i = MsgBoxResult.Yes Then
            CancelTransaction()
            btncancel.Enabled = False
        End If
    End Sub

#Region "functions and methods"

    Sub updateStock(ByVal newStock As Integer, ByVal prodname As String)
        Connected()
        sql = "UPDATE tblproducts SET prod_stock = " & newStock & " where prod_name = '" & prodname & "'"
        CommandDB()
        cmd.ExecuteNonQuery()
        PopulateProducts(lvProducts)
    End Sub

    Public Function getProductStock(ByVal prodname) As Integer
        Connected()
        Dim str As String = ""
        sql = "SELECT prod_stock FROM tblproducts where prod_name = '" & prodname & "'"
        CommandDB()
        dr = cmd.ExecuteReader()
        If dr.HasRows Then
            Do While dr.Read()
                Return dr.Item(0).ToString
            Loop
        End If
    End Function

    Public Function GenerateOrderNo() As String
        Connected()
        Dim str As String = ""
        sql = "SELECT Max(ORno) FROM tblsales"
        CommandDB()
        dr = cmd.ExecuteReader()
        If dr.HasRows Then
            Do While dr.Read()
                str = dr.Item(0).ToString.Substring(2) + 1
            Loop
        End If
        Return "OR" & str
    End Function

    Sub clickeData()
        With lvProducts.SelectedItems(0)
            id = .SubItems(0).Text
            prodcategory = .SubItems(1).Text
            prodname = .SubItems(2).Text
            prodprice = .SubItems(3).Text
            prodstock = .SubItems(4).Text
        End With
    End Sub

    Function ComputeChange() As Double
        Dim cash As Double = Val(tbcash.Text)
        Dim total As Double = Val(lbltotal.Text)
        If Val(cash) < Val(total) Then
            tbcash.ForeColor = Color.Crimson
            Return 0
        End If
        If Val(total) = 0 Then
            tbcash.ForeColor = Color.Green
            Return 0
        End If
        tbcash.ForeColor = Color.Green
        Return Val(cash) - Val(total)
    End Function

    Function ComputeCollumn(ByVal col As Integer) As Integer
        Dim sum = (From lvi In lvorder.Items _
        Let value = CInt(DirectCast(lvi, ListViewItem).SubItems(col).Text) _
        Select value).Sum
        Return sum
    End Function


    Sub SaveData()
        Dim odate As String = Format(Now, "d")
        Dim lv
        Dim ctr As Integer
        Dim iloop As Integer
        ctr = lvorder.Items.Count()
        If Not lvorder.Items.Count = 0 Then
            Do Until iloop = lvorder.Items.Count
                lv = lvorder.Items.Item(iloop)
                With lv
                    Connected()
                    sql = "INSERT into tblsales (ORno,customer_name,prod_name,prod_price,prod_qty,prod_subtotal,date_order) VALUES ('" & lblORno.Text & "','" & tbcustomer.Text & "','" & .subitems(0).Text.ToUpper & "'," & .subitems(1).Text.ToUpper & "," & .subitems(2).Text.ToUpper & "," & .subitems(3).Text.ToUpper & ",'" & odate.ToString & "')"
                    CommandDB()
                    cmd.ExecuteNonQuery()
                End With
                iloop = iloop + 1
                lv = Nothing
            Loop
            MsgBox("Successfully Ordered!", MsgBoxStyle.Information)
        End If
    End Sub

    Sub resetOrder()
        lvorder.Items.Clear()
        lblORno.Text = GenerateOrderNo()
        PopulateProducts(lvProducts)
        lbltotal.Text = 0
        lblitems.Text = 0
        tbcash.Text = 0
        lblchange.Text = 0
        tbcash.ForeColor = Color.Crimson
    End Sub

    Sub CancelTransaction()

        Dim ctr As Integer = 0
        Dim newstock As Integer
        Dim prodname As String
        Dim qtyorder As Integer

        'mag loloop tayo sa order list kung meron syang laman
        'mag loloop yan kung ilan ang row ng order list
        While (ctr <> lvorder.Items.Count)

            'ilalagay ntn ung data na nsa order list each row
            'ctr = 0 ay ung unang row sa order list
            prodname = lvorder.Items(ctr).SubItems(0).Text
            qtyorder = lvorder.Items(ctr).SubItems(2).Text

            'getProductStock(prodname) eto ung ginawa ntn function para mkuha ang stock ng prodname na ilalagay ntn
            newstock = Val(getProductStock(prodname)) + Val(qtyorder)

            'i uupdate na ung stock sa table ng tblproducts gamit ang nakuha nating newstock na kinumpyut
            updateStock(newstock, prodname)

            'increment ntn sya pra nman sa susunod na row
            ctr += 1
        End While

        resetOrder()
        ctr = 0
        lvorder.Items.Clear()
    End Sub

#End Region



End Class
