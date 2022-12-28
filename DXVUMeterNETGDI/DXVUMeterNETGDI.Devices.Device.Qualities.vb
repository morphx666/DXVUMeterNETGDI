Partial Public Class DXVUMeterNETGDI
    Partial Public Class DevicesCollection
        Partial Public Class Device
            ''' <summary>Provides a collection to all the device's supported recording parameters</summary>
            Partial Public Class QualitiesCollection
                Implements IList(Of Quality)

                Private mCol As New List(Of Quality)

                Protected Friend Sub New()
                    mCol = New List(Of Quality)
                End Sub

                Protected Friend Sub Add(item As Quality) Implements ICollection(Of Quality).Add
                    mCol.Add(item)
                End Sub

                Protected Friend Sub Clear() Implements ICollection(Of Quality).Clear
                    mCol.Clear()
                End Sub

                Public Function Contains(item As Quality) As Boolean Implements ICollection(Of Quality).Contains
                    Return mCol.Contains(item)
                End Function

                Public Sub CopyTo(array() As Quality, arrayIndex As Integer) Implements ICollection(Of Quality).CopyTo

                End Sub

                ''' <summary>Returns the number of items in the collection</summary>
                Public ReadOnly Property Count() As Integer Implements ICollection(Of Quality).Count
                    Get
                        Return mCol.Count
                    End Get
                End Property

                Public ReadOnly Property IsReadOnly() As Boolean Implements ICollection(Of Quality).IsReadOnly
                    Get
                        Return True
                    End Get
                End Property

                Private Function Remove(item As Quality) As Boolean Implements ICollection(Of Quality).Remove
                    Return mCol.Remove(item)
                End Function

                Public Function GetEnumerator() As IEnumerator(Of Quality) Implements IEnumerable(Of Quality).GetEnumerator
                    Return mCol.GetEnumerator
                End Function

                Public Function IndexOf(item As Quality) As Integer Implements IList(Of Quality).IndexOf
                    Return mCol.IndexOf(item)
                End Function

                Private Sub Insert(index As Integer, item As Quality) Implements IList(Of Quality).Insert
                    mCol.Insert(index, item)
                End Sub

                ''' <summary>Returns a <see ref="Quality"></see> item from the collection</summary>
                Default Public Property Item(index As Integer) As Quality Implements IList(Of Quality).Item
                    Get
                        Return mCol(index)
                    End Get
                    Set(value As Quality)
                        'mCol(index) = value
                    End Set
                End Property

                Private Sub RemoveAt(index As Integer) Implements IList(Of Quality).RemoveAt
                    mCol.RemoveAt(index)
                End Sub

                Private Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
                    Return mCol.GetEnumerator
                End Function
            End Class
        End Class
    End Class
End Class
