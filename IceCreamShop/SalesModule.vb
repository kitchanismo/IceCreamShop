Module SalesModule
    Sub updateStockInDatabase(ByVal newStock As Integer, ByVal prodname As String)
        Connected()
        sql = "UPDATE tblproducts SET prod_stock = " & newStock & " where prod_name = '" & prodname & "'"
        CommandDB()
        cmd.ExecuteNonQuery()
    End Sub

    Public Function getProductStock(ByVal prodname) As Integer
        Connected()
        Dim stock As Integer
        sql = "SELECT prod_stock FROM tblproducts where prod_name = '" & prodname & "'"
        CommandDB()
        dr = cmd.ExecuteReader()
        If dr.HasRows Then
            Do While dr.Read()
                stock = dr.Item(0)
            Loop
        End If
        Return stock
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
End Module
