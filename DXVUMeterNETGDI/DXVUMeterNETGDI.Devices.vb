Imports System.Threading
Imports System.Math
Imports System.ComponentModel
Imports SlimDX.DirectSound

Partial Public Class DXVUMeterNETGDI
    ''' <summary>Provides access to a collection of devices (sound cards) that can record audio</summary>
    Partial Public Class DevicesCollection
        Implements IList(Of Device)

        Private mCol As New List(Of Device)

        ''' <summary>Sets or returns the currently selected device</summary>
        Public Property SelectedDevice() As Device
            Get
                For Each d As Device In mCol
                    If d.Selected Then Return d
                Next
                Return Nothing
            End Get
            Set(Value As Device)
                Value.Selected = True
            End Set
        End Property

        Protected Friend Sub New()
            Try
                For Each dinfo As DeviceInformation In DirectSoundCapture.GetDevices()
                    Add(New Device(dinfo.ModuleName, dinfo.Description, dinfo.DriverGuid, Me))
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub

        ''' <summary>Returns the system's default device</summary>
        Public ReadOnly Property DefaultDevice() As Device
            Get
                For Each d As Device In mCol
                    If d.IsDefault Then Return d
                Next
                Return Nothing
            End Get
        End Property

        Private Sub Add(item As Device) Implements ICollection(Of Device).Add
            mCol.Add(item)
        End Sub

        Protected Friend Sub Clear() Implements ICollection(Of Device).Clear
            mCol.Clear()
        End Sub

        Public Function Contains(item As Device) As Boolean Implements ICollection(Of Device).Contains
            Return mCol.Contains(item)
        End Function

        Private Sub CopyTo(array() As Device, arrayIndex As Integer) Implements ICollection(Of Device).CopyTo

        End Sub

        ''' <summary>Returns the number of devices</summary>
        Public ReadOnly Property Count() As Integer Implements ICollection(Of Device).Count
            Get
                Return mCol.Count
            End Get
        End Property

        Public ReadOnly Property IsReadOnly() As Boolean Implements ICollection(Of Device).IsReadOnly
            Get
                Return True
            End Get
        End Property

        Private Function Remove(item As Device) As Boolean Implements ICollection(Of Device).Remove
            Return mCol.Remove(item)
        End Function

        Public Function GetEnumerator() As IEnumerator(Of Device) Implements IEnumerable(Of Device).GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Function IndexOf(item As Device) As Integer Implements IList(Of Device).IndexOf
            Return mCol.IndexOf(item)
        End Function

        Public Sub Insert(index As Integer, item As Device) Implements IList(Of Device).Insert
            mCol.Insert(index, item)
        End Sub

        ''' <summary>Returns a <see cref="Device"></see> from the collection</summary>
        Default Public Property Item(index As Integer) As Device Implements IList(Of Device).Item
            Get
                Return mCol(index)
            End Get
            Set(value As Device)
                'mCol(index)=value
            End Set
        End Property

        Private Sub RemoveAt(index As Integer) Implements IList(Of Device).RemoveAt
            mCol.RemoveAt(index)
        End Sub

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function
    End Class
End Class