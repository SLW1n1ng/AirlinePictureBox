Public Class PassengerBooking
    Private Sub Business_Class_Click(sender As Object, e As EventArgs) Handles Business_Class.Click
        BusinessClassForm.Show()                                                    ' Handles Business_Class.Click
    End Sub

    Private Sub Economy_Class_Click(sender As Object, e As EventArgs) Handles Economy_Class.Click
        EconomyClass.Show()                                         '  Handles Economy_Class.Click
    End Sub

End Class