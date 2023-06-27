<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PassengerBooking
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(PassengerBooking))
        Business_Class = New Button()
        Economy_Class = New Button()
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' Business_Class
        ' 
        Business_Class.AutoSize = True
        Business_Class.BackColor = Color.Salmon
        Business_Class.Cursor = Cursors.Hand
        Business_Class.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point)
        Business_Class.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Business_Class.Location = New Point(195, 240)
        Business_Class.Name = "Business_Class"
        Business_Class.Padding = New Padding(10)
        Business_Class.Size = New Size(230, 68)
        Business_Class.TabIndex = 0
        Business_Class.Text = "Business Class"
        Business_Class.UseVisualStyleBackColor = False
        ' 
        ' Economy_Class
        ' 
        Economy_Class.AutoSize = True
        Economy_Class.BackColor = Color.Salmon
        Economy_Class.Cursor = Cursors.Hand
        Economy_Class.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point)
        Economy_Class.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Economy_Class.Location = New Point(449, 240)
        Economy_Class.Name = "Economy_Class"
        Economy_Class.Padding = New Padding(10)
        Economy_Class.Size = New Size(243, 68)
        Economy_Class.TabIndex = 1
        Economy_Class.Text = "Economy Class"
        Economy_Class.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Cooper Black", 33F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label1.Location = New Point(167, 106)
        Label1.Name = "Label1"
        Label1.Size = New Size(542, 63)
        Label1.TabIndex = 2
        Label1.Text = "Your Best Airlines"' 
        ' PassengerBooking
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(924, 580)
        Controls.Add(Label1)
        Controls.Add(Economy_Class)
        Controls.Add(Business_Class)
        Name = "PassengerBooking"
        Text = "PassengerBooking"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Public WithEvents Business_Class As Button
    Public WithEvents Economy_Class As Button
    Friend WithEvents Label1 As Label
End Class
