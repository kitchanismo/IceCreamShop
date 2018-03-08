'Imports System.Data.SqlClient

Module modconn
    'database
    Public Sub Connected()
        With conn
            Try
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\icecream.mdb;"
                .Open()
            Catch
                MsgBox("Error in Database! Contact Administrator.")
                End
            End Try
        End With
    End Sub
    'cmd use
    Sub CommandDB()
        cmd = New OleDb.OleDbCommand(sql, conn)
    End Sub

End Module
