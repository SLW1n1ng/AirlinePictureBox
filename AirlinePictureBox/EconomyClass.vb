Imports System.Data.SqlClient
Imports System.Threading

Public Class EconomyClass
    Dim EconomyClass(99) As PictureBox
    Dim SeatsClientNames(99) As RichTextBox
    Dim myImageLocationPrefix As String = ""
    Dim EmptySeatImg As String = "./AirlineSeatEmpty.png"
    Dim FullSeating As String = "./AirlineSeatFull.png"
    Dim FLNameLabel(1) As Label
    Dim FLNameTextBox(1) As TextBox
    Dim FLNames(1) As String
    Dim AirlineMenu As Button

    Dim ConnectionObj As SqlConnection
    Private Sub EconomyClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Handles MyBase.Load
        ConnectSQL()

        Dim xLocation As Integer = 10
        Dim yLocation As Integer = 10
        Dim xRichTextLocation As Integer = 10
        Dim yRichTextLocation As Integer = 135
        FLNames(0) = "First Name"
        FLNames(1) = "Last Name"

        For index = 0 To EconomyClass.GetUpperBound(0)
            EconomyClass(index) = New PictureBox() With {
            .ImageLocation = myImageLocationPrefix + EmptySeatImg,
            .SizeMode = PictureBoxSizeMode.Zoom,
            .Size = New Size(125, 125),
            .Location = New Point(xLocation, yLocation),
            .Name = "PictureBox" + index.ToString(),
            .Visible = True,
            .Enabled = True,
            .Cursor = Cursors.Hand
            }
            AddHandler EconomyClass(index).Click, AddressOf PictureBoxClick
            Me.Controls.Add(EconomyClass(index))

            PrintNewRow(xLocation, yLocation, index)
        Next

        For index = 0 To SeatsClientNames.GetUpperBound(0)
            SeatsClientNames(index) = New RichTextBox() With {
            .Size = New Size(125, 40),
            .BackColor = Color.LightSlateGray,
            .Location = New Point(xRichTextLocation, yRichTextLocation),
            .Name = "Richtext" + index.ToString(),
            .Visible = True,
            .Enabled = False,
            .Cursor = Cursors.Hand
            }
            'AddHandler SeatsClientNames(index, innerindex).Click, AddressOf RichTextClick
            Me.Controls.Add(SeatsClientNames(index))

            PrintNewRow(xRichTextLocation, yRichTextLocation, index)

            index = PrintNameTextboxLabels(index)
        Next

        PrintAirlineHome()

        ReadUpdateTable()

    End Sub

    Private Sub ConnectSQL()
        ConnectionObj = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\xenig\source\repos\March15_VB_Airline\March15_VB_Airline_Assignment\AirlinePictureBox\AirlinePictureBox\Airline.mdf;Integrated Security=True")
        ConnectionObj.Open()
    End Sub

    Private Sub PictureBoxClick(sender As Object, e As EventArgs)
        Dim pictureboxclick As PictureBox = sender
        Dim PicBoxIndex As Integer = Convert.ToInt32(pictureboxclick.Name.Substring(10))
        Dim PicBoxControl = TryCast(sender, Control)

        If (FLNameTextBox(0).Text = "" Or FLNameTextBox(1).Text = "") And SeatsClientNames(PicBoxIndex).Text = "" Then
            TextLabel_Y_Location(PicBoxControl)
            MessageBox.Show($"Enter {FLNames(0)} and {FLNames(1)} name")
            Return
        End If

        If pictureboxclick.ImageLocation = myImageLocationPrefix + EmptySeatImg Then

            pictureboxclick.ImageLocation = myImageLocationPrefix + FullSeating
            SeatsClientNames(PicBoxIndex).Text = $"{FLNames(0)}: {FLNameTextBox(0).Text}{vbCrLf}{FLNames(1)}: {FLNameTextBox(1).Text}"

            UpdateTable(PicBoxIndex)
            SubClearTextboxes()

            Return
        ElseIf pictureboxclick.ImageLocation = myImageLocationPrefix + FullSeating Then

            TextLabel_Y_Location(PicBoxControl)

            Dim CancelYesNo As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "y/n?", MessageBoxButtons.YesNo)

            If CancelYesNo = 6 Then

                pictureboxclick.ImageLocation = myImageLocationPrefix + EmptySeatImg
                SeatsClientNames(PicBoxIndex).Text = ""

                DeleteFromTable(PicBoxIndex)
                SubClearTextboxes()

            End If
        End If

    End Sub

    Private Sub PrintAirlineHome()
        AirlineMenu = New Button With {
                   .Name = "Airline_Menu",
                   .Text = "Airline Menu",
                   .Padding = New Padding(10),
                   .BackColor = Color.Salmon,
                   .AutoSize = True,
                   .Visible = True,
                   .Enabled = True,
                   .Cursor = Cursors.Hand,
                   .Font = New Font("Tahoma", 16.2),
                   .Location = New Point(775, 150)
               }
        Me.Controls.Add(AirlineMenu)
        AddHandler AirlineMenu.Click, AddressOf AirlineMenuClick
    End Sub

    Private Function PrintNameTextboxLabels(index As Integer) As Integer
        If index < 2 Then
            FLNameTextBox(index) = New TextBox With {
                .Name = "TextBox" + index.ToString(),
                .Location = New Point(900, 225 + (index * 40))
                }
            FLNameLabel(index) = New Label With {
                .Name = "Label" + index.ToString(),
                .Text = FLNames(index),
                .Location = New Point(775, 225 + (index * 40))
                }

            Me.Controls.Add(FLNameTextBox(index))
            Me.Controls.Add(FLNameLabel(index))
        End If

        Return index
    End Function

    Private Shared Sub PrintNewRow(ByRef SubXLocation As Integer, ByRef SubYLocation As Integer, subindex As Integer)
        SubXLocation += 135
        If (subindex + 1) Mod 5 = 0 Then
            SubXLocation = 10
            SubYLocation += 190
        End If
    End Sub

    Private Sub ReadUpdateTable()
        Dim Cmd As New SqlCommand($"SELECT * FROM EconomyClass;", ConnectionObj)
        Dim reader As SqlDataReader
        reader = Cmd.ExecuteReader
        If reader.HasRows = False Then
            reader.Close()
            Return
        End If
        While reader.Read
            Dim Seat As Integer = Convert.ToInt32(reader.GetValue(0))
            Dim FirstName As String = reader.GetValue(1).ToString
            Dim LastName As String = reader.GetValue(2).ToString
            EconomyClass(Seat).ImageLocation = FullSeating
            SeatsClientNames(Seat).Text = $"{FLNames(0)}: {FirstName} {vbCrLf}{FLNames(1)}: {LastName} "
        End While
        reader.Close()
    End Sub

    Private Sub DeleteFromTable(PicBoxIndex As Integer)
        Dim Command As New SqlCommand($"DELETE FROM EconomyClass WHERE Seat = '{PicBoxIndex}';", ConnectionObj)
        Command.ExecuteNonQuery()
        'MessageBox.Show("Record Delete Successful")
    End Sub

    Private Sub UpdateTable(PicBoxIndex As Integer)
        Dim quearyString As String = $"INSERT INTO EconomyClass (Seat, FirstName, LastName) VALUES ({PicBoxIndex}, '{FLNameTextBox(0).Text}', '{FLNameTextBox(1).Text}');"
        Dim Cmd As New SqlCommand(quearyString, ConnectionObj)
        Cmd.ExecuteNonQuery()
        'MessageBox.Show("Record Added")
    End Sub

    Sub TextLabel_Y_Location(SubPicBoxControl)
        For index = 0 To 1
            FLNameTextBox(index).Location = New Point(900, SubPicBoxControl.Location.Y + 70 + (index * 40))
            FLNameLabel(index).Location = New Point(775, SubPicBoxControl.Location.Y + 70 + (index * 40))
        Next
        AirlineMenu.Location = New Point(775, SubPicBoxControl.Location.Y)
        Thread.Sleep(100)
    End Sub
    Sub SubClearTextboxes()
        For index = 0 To 1
            FLNameTextBox(index).Clear()
        Next
    End Sub
    Private Sub AirlineMenuClick(sender As Object, e As EventArgs)
        Me.Hide()
    End Sub
End Class