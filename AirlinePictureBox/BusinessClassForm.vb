Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Threading

Public Class BusinessClassForm
    Dim BusinessClass(19) As PictureBox
    Dim SeatClientNames(19) As RichTextBox
    Dim myImageLocationPrefix As String = ""
    Dim EmptySeatImg As String = "./AirlineSeatEmpty.png"
    Dim FullSeating As String = "./AirlineSeatFull.png"
    Dim FLNameLabel(1) As Label
    Dim FLNameTextBox(1) As TextBox
    Dim FLNames(1) As String
    Dim AirlineMenu As Button

    Dim ConnectionObj As SqlConnection

    Private Sub BusinessClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Handles MyBase.Load
        ConnectionObj = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\xenig\source\repos\March15_VB_Airline\March15_VB_Airline_Assignment\AirlinePictureBox\AirlinePictureBox\Airline.mdf;Integrated Security=True")
        ConnectionObj.Open()

        Dim xLocation As Integer = 10
        Dim yLocation As Integer = 10
        Dim xRichTextLocation As Integer = 10
        Dim yRichTextLocation As Integer = 160
        FLNames(0) = "First Name"
        FLNames(1) = "Last Name"

        For index = 0 To BusinessClass.GetUpperBound(0)
            BusinessClass(index) = New PictureBox() With {
                .ImageLocation = myImageLocationPrefix + EmptySeatImg,
                .Size = New Size(150, 150),
                .SizeMode = PictureBoxSizeMode.Zoom,
                .Location = New Point(xLocation, yLocation),
                .Name = "PictureBox" + index.ToString(),
                .Visible = True,
                .Enabled = True,
                .Cursor = Cursors.Hand
            }
            AddHandler BusinessClass(index).Click, AddressOf PictureBoxClick
            Me.Controls.Add(BusinessClass(index))

            PrintNextRow(xLocation, yLocation, index)
        Next

        For index = 0 To SeatClientNames.GetUpperBound(0)
            SeatClientNames(index) = New RichTextBox() With {
                .Size = New Size(150, 50),
                .BackColor = Color.LightSlateGray,
                .Location = New Point(xRichTextLocation, yRichTextLocation),
                .Name = "RichText" + index.ToString(),
                .Visible = True,
                .Enabled = False,
                .Cursor = Cursors.Hand,
                .BorderStyle = BorderStyle.None
            }
            Me.Controls.Add(SeatClientNames(index))

            PrintNextRow(xRichTextLocation, yRichTextLocation, index)

            index = PrintNameLabelTextbox(index)
        Next

        PrintAirlineHomeButton()

        ReadTable()

    End Sub
    Private Sub PictureBoxClick(sender As Object, e As EventArgs)

        Dim pictureboxclick As PictureBox = sender
        Dim PicBoxIndex As Integer = Convert.ToInt32(pictureboxclick.Name.Substring(10))
        Dim PicBoxControl = TryCast(sender, Control)

        If (FLNameTextBox(0).Text = "" Or FLNameTextBox(1).Text = "") And SeatClientNames(PicBoxIndex).Text = "" Then
            TextLabel_Y_Location(PicBoxControl)
            MessageBox.Show($"Enter {FLNames(0)} and {FLNames(1)} name")
            Return
        End If

        If pictureboxclick.ImageLocation = myImageLocationPrefix + EmptySeatImg Then

            pictureboxclick.ImageLocation = myImageLocationPrefix + FullSeating
            SeatClientNames(PicBoxIndex).Text = $"{FLNames(0)}: {FLNameTextBox(0).Text}{vbCrLf}{FLNames(1)}: {FLNameTextBox(1).Text}"

            UpdateToTable(PicBoxIndex)

            SubClearTextboxes()

            Return
        ElseIf pictureboxclick.ImageLocation = myImageLocationPrefix + FullSeating Then

            TextLabel_Y_Location(PicBoxControl)

            Dim CancelYesNo As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "y/n?", MessageBoxButtons.YesNo)

            If CancelYesNo = 6 Then

                DeleteFromTable(PicBoxIndex)

                pictureboxclick.ImageLocation = myImageLocationPrefix + EmptySeatImg
                SeatClientNames(PicBoxIndex).Text = ""

                SubClearTextboxes()

            End If
        End If
        'MessageBox.Show(pictureboxclick.ImageLocation)
    End Sub
    Private Sub ReadTable()
        Dim Cmd As New SqlCommand($"SELECT * FROM BusinessClass;", ConnectionObj)
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
            BusinessClass(Seat).ImageLocation = FullSeating
            SeatClientNames(Seat).Text = $"{FLNames(0)}: {FirstName} {vbCrLf}{FLNames(1)}: {LastName} "
        End While
        reader.Close()
    End Sub
    Private Sub DeleteFromTable(PicBoxIndex As Integer)
        Dim Command As New SqlCommand($"DELETE FROM BusinessClass WHERE Seat = '{PicBoxIndex}';", ConnectionObj)
        Command.ExecuteNonQuery()
        'MessageBox.Show("Record Delete Successful")
    End Sub

    Private Sub UpdateToTable(PicBoxIndex As Integer)
        Dim quearyString As String = $"INSERT INTO BusinessClass (Seat, FirstName, LastName) VALUES ({PicBoxIndex}, '{FLNameTextBox(0).Text}', '{FLNameTextBox(1).Text}');"
        Dim Cmd As New SqlCommand(quearyString, ConnectionObj)
        Cmd.ExecuteNonQuery()
        'MessageBox.Show("Record Added")
    End Sub

    Private Sub PrintAirlineHomeButton()
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
                    .Location = New Point(675, 150)
                }
        Me.Controls.Add(AirlineMenu)
        AddHandler AirlineMenu.Click, AddressOf MenuButtonClick
    End Sub

    Private Function PrintNameLabelTextbox(index As Integer) As Integer
        If index < 2 Then
            FLNameTextBox(index) = New TextBox With {
                .Name = "TextBox" + index.ToString(),
                .Location = New Point(800, 225 + (index * 40))
                }
            FLNameLabel(index) = New Label With {
                .Name = "Label" + index.ToString(),
                .Text = FLNames(index),
                .Location = New Point(675, 225 + (index * 40))
                }
            Me.Controls.Add(FLNameTextBox(index))
            Me.Controls.Add(FLNameLabel(index))
        End If

        Return index
    End Function

    Private Shared Sub PrintNextRow(ByRef xLocation As Integer, ByRef yLocation As Integer, index As Integer)
        xLocation += 160
        If (index + 1) Mod 4 = 0 Then
            xLocation = 10
            yLocation += 215
        End If
    End Sub


    Sub TextLabel_Y_Location(SubPicBoxControl)
        For index = 0 To 1
            FLNameTextBox(index).Location = New Point(800, SubPicBoxControl.Location.Y + 75 + (index * 40))
            FLNameLabel(index).Location = New Point(675, SubPicBoxControl.Location.Y + 75 + (index * 40))
        Next
        AirlineMenu.Location = New Point(675, SubPicBoxControl.Location.Y)

        Thread.Sleep(100)

    End Sub
    Sub SubClearTextboxes()
        For index = 0 To 1
            FLNameTextBox(index).Clear()
        Next
    End Sub
    Private Sub MenuButtonClick(sender As Object, e As EventArgs)
        Me.Hide()
    End Sub

End Class
