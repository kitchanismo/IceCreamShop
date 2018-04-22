Module modquery

    Public Sub PopulateProducts(ByVal lv As ListView, ByVal orderBy As String)
        Connected()
        With lv
            .Items.Clear()
            sql = " SELECT * FROM tblproducts order by " & orderBy.ToLower & ""
            CommandDB()
            dr = cmd.ExecuteReader()
            lv.Items.Clear()
            While (dr.Read())
                With lv.Items.Add(dr("id"))
                    .SubItems.Add(dr("prod_category"))
                    .SubItems.Add(dr("prod_name"))
                    .SubItems.Add(dr("prod_price"))
                    .SubItems.Add(dr("prod_stock"))
                    .SubItems.Add(dr("prod_purchasedate"))
                    .SubItems.Add(dr("prod_passdate"))
                End With

            End While
        End With
    End Sub

    Public Sub PopulateTotalSales(ByVal lv As ListView)
        Connected()
        With lv
            .Items.Clear()
            sql = " SELECT * FROM tblsales"
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

    Function getTotalSales() As Double
        Connected()
        sql = " SELECT sum(prod_subtotal) FROM tblsales"
        CommandDB()
        dr = cmd.ExecuteReader()
        While (dr.Read())
            Return dr(0)
        End While
    End Function

 
    Public Sub PopulateCategory(ByVal cbo As ComboBox)
        Connected()
        cbo.Items.Clear()
        sql = " SELECT distinct prod_category FROM tblproducts"
        CommandDB()
        dr = cmd.ExecuteReader()
        While (dr.Read())
            cbo.Items.Add(dr(0))
        End While
    End Sub

    Public Sub SearchProduct(ByVal lv As ListView, ByVal search As String, Optional ByVal orderBy As String = "id")
        Connected()
        Dim result As String = ""
        Dim key As String = "'%" & search & "%'"
        sql = " SELECT * FROM tblproducts WHERE prod_name LIKE " & key & " or prod_category LIKE " & key & " order by " & orderBy & ""
        CommandDB()
        dr = cmd.ExecuteReader()
        lv.Items.Clear()
        While (dr.Read())
            With lv.Items.Add(dr("id"))
                .SubItems.Add(dr("prod_category"))
                .SubItems.Add(dr("prod_name"))
                .SubItems.Add(dr("prod_price"))
                .SubItems.Add(dr("prod_stock"))
                .SubItems.Add(dr("prod_purchasedate"))
                .SubItems.Add(dr("prod_passdate"))
            End With

        End While
    End Sub


    Public Function IsProductExist(ByVal tbl As String, ByVal field As String, ByVal str As String) As Boolean
        Connected()
        sql = "Select COUNT(*) FROM " & tbl & " WHERE " & field & " = '" & str & "'"
        CommandDB()
        Dim rows As Integer = cmd.ExecuteScalar()
        If rows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Module
