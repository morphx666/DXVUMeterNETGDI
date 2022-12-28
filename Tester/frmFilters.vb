Imports NDXVUMeterNET

Public Class frmFilters
    Private activeFilter As ListViewItem
    Private ignoreEvents As Boolean

    Private Sub frmFilters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbFilterType.Items.Clear()
        For Each k As BiQuadFilter.FiltersTypes In [Enum].GetValues(GetType(BiQuadFilter.FiltersTypes))
            cbFilterType.Items.Add(k)
        Next
        cbFilterType.SelectedIndex = 0

        ListViewFilters.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)

        tbFrequency.Minimum = 0
        tbFrequency.Maximum = dxvuCtrl.Frequency

        AddHandler cbFilterType.SelectedIndexChanged, AddressOf UpdateActiveFilter
        AddHandler tbGain.TextChanged, AddressOf UpdateActiveFilter
        AddHandler tbFrequency.ValueChanged, Sub()
                                                 lblFreqVal.Text = HumanFreq(tbFrequency.Value)
                                                 UpdateActiveFilter()
                                             End Sub
        AddHandler tbBandwidth.TextChanged, AddressOf UpdateActiveFilter

        UpdateFiltersList()
    End Sub

    Private Sub UpdateFiltersList(Optional selectIndex As Integer = -1)
        gbFilterProperties.Enabled = False

        ignoreEvents = True
        ListViewFilters.Items.Clear()
        For Each filter As BiQuadFilter In dxvuCtrl.Filters
            With ListViewFilters.Items.Add([Enum].GetName(GetType(BiQuadFilter.FiltersTypes), filter.FilterType))
                .SubItems.Add(filter.Gain)
                .SubItems.Add(filter.Frequency)
                .SubItems.Add(filter.BandWidth)
                .Checked = filter.Enabled

                .Tag = filter
            End With
        Next
        ignoreEvents = False

        If ListViewFilters.Items.Count = 0 Then
            activeFilter = Nothing
        Else
            If selectIndex = -1 Then
                activeFilter = ListViewFilters.Items(0)
            Else
                activeFilter = ListViewFilters.Items(selectIndex)
            End If
            With activeFilter
                .EnsureVisible()
                .Selected = True
            End With

            gbFilterProperties.Enabled = True
        End If

        Dim hs(ListViewFilters.Columns.Count - 1) As Integer
        ListViewFilters.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        For i As Integer = 0 To hs.Length - 1
            hs(i) = ListViewFilters.Columns(i).Width
        Next
        ListViewFilters.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        For i As Integer = 0 To hs.Length - 1
            ListViewFilters.Columns(i).Width = Math.Max(hs(i), ListViewFilters.Columns(i).Width)
        Next
    End Sub

    Private Sub ListViewFilters_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles ListViewFilters.ItemChecked
        If ignoreEvents Then Exit Sub

        e.Item.Selected = True
        If activeFilter IsNot Nothing Then CType(activeFilter.Tag, BiQuadFilter).Enabled = activeFilter.Checked
    End Sub

    Private Sub ListViewFilters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewFilters.SelectedIndexChanged
        If ListViewFilters.SelectedItems.Count = 1 Then
            activeFilter = ListViewFilters.SelectedItems(0)
        Else
            activeFilter = Nothing
        End If

        If activeFilter Is Nothing Then
            gbFilterProperties.Enabled = False
        Else
            ignoreEvents = True

            Dim filter As BiQuadFilter = CType(activeFilter.Tag, BiQuadFilter)
            cbFilterType.SelectedItem = filter.FilterType
            tbGain.Text = filter.Gain
            tbFrequency.Value = filter.Frequency
            tbBandwidth.Text = filter.BandWidth
            gbFilterProperties.Enabled = True

            UpdateActiveListViewItem(filter)

            ignoreEvents = False
        End If
    End Sub

    Private Sub UpdateActiveListViewItem(filter As BiQuadFilter)
        activeFilter.Text = [Enum].GetName(GetType(BiQuadFilter.FiltersTypes), filter.FilterType)
        activeFilter.SubItems(chGain.Index).Text = filter.Gain
        activeFilter.SubItems(chFrequency.Index).Text = filter.Frequency
        activeFilter.SubItems(chBandwidth.Index).Text = filter.BandWidth
    End Sub

    Private Sub UpdateActiveFilter()
        If ignoreEvents OrElse activeFilter Is Nothing Then Exit Sub

        Dim filter As BiQuadFilter = CType(activeFilter.Tag, BiQuadFilter)
        Try
            filter.FilterType = CType(cbFilterType.SelectedItem, BiQuadFilter.FiltersTypes)
            filter.Gain = Double.Parse(tbGain.Text)
            filter.Frequency = tbFrequency.Value
            filter.BandWidth = Double.Parse(tbBandwidth.Text)
        Catch
        End Try

        UpdateActiveListViewItem(filter)
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        AddFilter(New BiQuadFilter(BiQuadFilter.FiltersTypes.LowPass, 100, dxvuCtrl.Frequency / 4, dxvuCtrl.Frequency, 0.1))
    End Sub

    Private Sub AddFilter(filter As BiQuadFilter)
        dxvuCtrl.Filters.Add(filter)
        UpdateFiltersList(ListViewFilters.Items.Count)
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        If activeFilter IsNot Nothing Then
            dxvuCtrl.Filters.Remove(CType(activeFilter.Tag, BiQuadFilter))
            UpdateFiltersList()
        End If
    End Sub
End Class