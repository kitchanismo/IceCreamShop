Module modStock
    Sub doChangeListViewColor(ByVal lView As ListView)
        'instantiate
        Dim listViewColor As New kitchanismo.ListViewForeColor

        'initialize 
        listViewColor.ListView = lView
        listViewColor.Column = 5 'stock column in listview products
        listViewColor.Critical = 5 'if stock is less than equal to 5 then change color to orange
        listViewColor.ColorStable = Color.Teal
        listViewColor.ColorWarning = Color.Orange
        listViewColor.ColorDanger = Color.Crimson

        'do color change
        listViewColor.changeListViewForeColor()
    End Sub
End Module
