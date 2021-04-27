<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CounterPhysicalDescription
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CounterPhysicalDescription))
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.cavity_height_Box1 = New System.Windows.Forms.MaskedTextBox()
        Me.Cavity_ID_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Cavity_Length_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Cd_liner_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Tube_ID_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Tube_Length_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Number_rows_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_1_tubes_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_2_tubes_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_3_tubes_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_4_tubes_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_5_tubes_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Counter_Name_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Counter_file_name_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Item_Stand_Box = New System.Windows.Forms.MaskedTextBox()
        Me.counter_geo_box = New System.Windows.Forms.ComboBox()
        Me.Ring_5_radius_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_4_radius_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_3_radius_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_2_radius_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_1_radius_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.SaveFileDialog2 = New System.Windows.Forms.SaveFileDialog()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 24
        Me.ListBox1.Items.AddRange(New Object() {"Cavity Type", "Cavity Height (cm)", "Cavity Width or Dia. (cm)", "Cavity Length (cm)", "Cd Liner thickness (cm)", "Tube ID (cm)", "Tube Active Length (cm)", "Number of rows", "                                           Radius (cm)               Tubes (#)", "   Row 1 ", "   Row 2", "   Row 3 ", "   Row 4", "   Row 5", "Item Stand Height (cm)"})
        Me.ListBox1.Location = New System.Drawing.Point(52, 111)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(513, 388)
        Me.ListBox1.TabIndex = 0
        '
        'cavity_height_Box1
        '
        Me.cavity_height_Box1.Location = New System.Drawing.Point(273, 134)
        Me.cavity_height_Box1.Name = "cavity_height_Box1"
        Me.cavity_height_Box1.Size = New System.Drawing.Size(115, 20)
        Me.cavity_height_Box1.TabIndex = 3
        '
        'Cavity_ID_Box
        '
        Me.Cavity_ID_Box.Location = New System.Drawing.Point(273, 158)
        Me.Cavity_ID_Box.Name = "Cavity_ID_Box"
        Me.Cavity_ID_Box.Size = New System.Drawing.Size(115, 20)
        Me.Cavity_ID_Box.TabIndex = 4
        '
        'Cavity_Length_Box
        '
        Me.Cavity_Length_Box.Location = New System.Drawing.Point(273, 183)
        Me.Cavity_Length_Box.Name = "Cavity_Length_Box"
        Me.Cavity_Length_Box.Size = New System.Drawing.Size(115, 20)
        Me.Cavity_Length_Box.TabIndex = 5
        '
        'Cd_liner_Box
        '
        Me.Cd_liner_Box.Location = New System.Drawing.Point(273, 209)
        Me.Cd_liner_Box.Name = "Cd_liner_Box"
        Me.Cd_liner_Box.Size = New System.Drawing.Size(115, 20)
        Me.Cd_liner_Box.TabIndex = 6
        '
        'Tube_ID_Box
        '
        Me.Tube_ID_Box.Location = New System.Drawing.Point(273, 235)
        Me.Tube_ID_Box.Name = "Tube_ID_Box"
        Me.Tube_ID_Box.Size = New System.Drawing.Size(115, 20)
        Me.Tube_ID_Box.TabIndex = 7
        '
        'Tube_Length_Box
        '
        Me.Tube_Length_Box.Location = New System.Drawing.Point(273, 258)
        Me.Tube_Length_Box.Name = "Tube_Length_Box"
        Me.Tube_Length_Box.Size = New System.Drawing.Size(115, 20)
        Me.Tube_Length_Box.TabIndex = 8
        '
        'Number_rows_Box
        '
        Me.Number_rows_Box.Location = New System.Drawing.Point(273, 284)
        Me.Number_rows_Box.Name = "Number_rows_Box"
        Me.Number_rows_Box.Size = New System.Drawing.Size(115, 20)
        Me.Number_rows_Box.TabIndex = 9
        '
        'Ring_1_tubes_Box
        '
        Me.Ring_1_tubes_Box.Location = New System.Drawing.Point(450, 331)
        Me.Ring_1_tubes_Box.Name = "Ring_1_tubes_Box"
        Me.Ring_1_tubes_Box.Size = New System.Drawing.Size(61, 20)
        Me.Ring_1_tubes_Box.TabIndex = 10
        '
        'Ring_2_tubes_Box
        '
        Me.Ring_2_tubes_Box.Location = New System.Drawing.Point(450, 356)
        Me.Ring_2_tubes_Box.Name = "Ring_2_tubes_Box"
        Me.Ring_2_tubes_Box.Size = New System.Drawing.Size(61, 20)
        Me.Ring_2_tubes_Box.TabIndex = 11
        '
        'Ring_3_tubes_Box
        '
        Me.Ring_3_tubes_Box.Location = New System.Drawing.Point(450, 380)
        Me.Ring_3_tubes_Box.Name = "Ring_3_tubes_Box"
        Me.Ring_3_tubes_Box.Size = New System.Drawing.Size(61, 20)
        Me.Ring_3_tubes_Box.TabIndex = 12
        '
        'Ring_4_tubes_Box
        '
        Me.Ring_4_tubes_Box.Location = New System.Drawing.Point(450, 403)
        Me.Ring_4_tubes_Box.Name = "Ring_4_tubes_Box"
        Me.Ring_4_tubes_Box.Size = New System.Drawing.Size(61, 20)
        Me.Ring_4_tubes_Box.TabIndex = 13
        '
        'Ring_5_tubes_Box
        '
        Me.Ring_5_tubes_Box.Location = New System.Drawing.Point(450, 429)
        Me.Ring_5_tubes_Box.Name = "Ring_5_tubes_Box"
        Me.Ring_5_tubes_Box.Size = New System.Drawing.Size(61, 20)
        Me.Ring_5_tubes_Box.TabIndex = 14
        '
        'Counter_Name_Box
        '
        Me.Counter_Name_Box.Location = New System.Drawing.Point(134, 26)
        Me.Counter_Name_Box.Name = "Counter_Name_Box"
        Me.Counter_Name_Box.Size = New System.Drawing.Size(158, 20)
        Me.Counter_Name_Box.TabIndex = 15
        '
        'Counter_file_name_Box
        '
        Me.Counter_file_name_Box.Location = New System.Drawing.Point(134, 65)
        Me.Counter_file_name_Box.Name = "Counter_file_name_Box"
        Me.Counter_file_name_Box.Size = New System.Drawing.Size(509, 20)
        Me.Counter_file_name_Box.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(49, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 16)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Counter:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(49, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 16)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "File Name:"
        '
        'Item_Stand_Box
        '
        Me.Item_Stand_Box.Location = New System.Drawing.Point(273, 455)
        Me.Item_Stand_Box.Name = "Item_Stand_Box"
        Me.Item_Stand_Box.Size = New System.Drawing.Size(115, 20)
        Me.Item_Stand_Box.TabIndex = 20
        '
        'counter_geo_box
        '
        Me.counter_geo_box.FormattingEnabled = True
        Me.counter_geo_box.Items.AddRange(New Object() {"Cylindrical", "Rectangular"})
        Me.counter_geo_box.Location = New System.Drawing.Point(273, 111)
        Me.counter_geo_box.Name = "counter_geo_box"
        Me.counter_geo_box.Size = New System.Drawing.Size(115, 21)
        Me.counter_geo_box.TabIndex = 149
        '
        'Ring_5_radius_Box
        '
        Me.Ring_5_radius_Box.Location = New System.Drawing.Point(273, 429)
        Me.Ring_5_radius_Box.Name = "Ring_5_radius_Box"
        Me.Ring_5_radius_Box.Size = New System.Drawing.Size(115, 20)
        Me.Ring_5_radius_Box.TabIndex = 154
        '
        'Ring_4_radius_Box
        '
        Me.Ring_4_radius_Box.Location = New System.Drawing.Point(273, 403)
        Me.Ring_4_radius_Box.Name = "Ring_4_radius_Box"
        Me.Ring_4_radius_Box.Size = New System.Drawing.Size(115, 20)
        Me.Ring_4_radius_Box.TabIndex = 153
        '
        'Ring_3_radius_Box
        '
        Me.Ring_3_radius_Box.Location = New System.Drawing.Point(273, 380)
        Me.Ring_3_radius_Box.Name = "Ring_3_radius_Box"
        Me.Ring_3_radius_Box.Size = New System.Drawing.Size(115, 20)
        Me.Ring_3_radius_Box.TabIndex = 152
        '
        'Ring_2_radius_Box
        '
        Me.Ring_2_radius_Box.Location = New System.Drawing.Point(273, 356)
        Me.Ring_2_radius_Box.Name = "Ring_2_radius_Box"
        Me.Ring_2_radius_Box.Size = New System.Drawing.Size(115, 20)
        Me.Ring_2_radius_Box.TabIndex = 151
        '
        'Ring_1_radius_Box
        '
        Me.Ring_1_radius_Box.Location = New System.Drawing.Point(273, 331)
        Me.Ring_1_radius_Box.Name = "Ring_1_radius_Box"
        Me.Ring_1_radius_Box.Size = New System.Drawing.Size(115, 20)
        Me.Ring_1_radius_Box.TabIndex = 150
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(614, 21)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(109, 27)
        Me.Button3.TabIndex = 157
        Me.Button3.Text = "Make Current Item"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(477, 21)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(109, 27)
        Me.Button2.TabIndex = 156
        Me.Button2.Text = "Save New Item"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(315, 21)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(124, 27)
        Me.Button1.TabIndex = 155
        Me.Button1.Text = "Read from file"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'SaveFileDialog2
        '
        '
        'Form7
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(933, 606)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Ring_5_radius_Box)
        Me.Controls.Add(Me.Ring_4_radius_Box)
        Me.Controls.Add(Me.Ring_3_radius_Box)
        Me.Controls.Add(Me.Ring_2_radius_Box)
        Me.Controls.Add(Me.Ring_1_radius_Box)
        Me.Controls.Add(Me.counter_geo_box)
        Me.Controls.Add(Me.Item_Stand_Box)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Counter_file_name_Box)
        Me.Controls.Add(Me.Counter_Name_Box)
        Me.Controls.Add(Me.Ring_5_tubes_Box)
        Me.Controls.Add(Me.Ring_4_tubes_Box)
        Me.Controls.Add(Me.Ring_3_tubes_Box)
        Me.Controls.Add(Me.Ring_2_tubes_Box)
        Me.Controls.Add(Me.Ring_1_tubes_Box)
        Me.Controls.Add(Me.Number_rows_Box)
        Me.Controls.Add(Me.Tube_Length_Box)
        Me.Controls.Add(Me.Tube_ID_Box)
        Me.Controls.Add(Me.Cd_liner_Box)
        Me.Controls.Add(Me.Cavity_Length_Box)
        Me.Controls.Add(Me.Cavity_ID_Box)
        Me.Controls.Add(Me.cavity_height_Box1)
        Me.Controls.Add(Me.ListBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form7"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Counter Physical Description"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents cavity_height_Box1 As MaskedTextBox
    Friend WithEvents Cavity_ID_Box As MaskedTextBox
    Friend WithEvents Cavity_Length_Box As MaskedTextBox
    Friend WithEvents Cd_liner_Box As MaskedTextBox
    Friend WithEvents Tube_ID_Box As MaskedTextBox
    Friend WithEvents Tube_Length_Box As MaskedTextBox
    Friend WithEvents Number_rows_Box As MaskedTextBox
    Friend WithEvents Ring_1_tubes_Box As MaskedTextBox
    Friend WithEvents Ring_2_tubes_Box As MaskedTextBox
    Friend WithEvents Ring_3_tubes_Box As MaskedTextBox
    Friend WithEvents Ring_4_tubes_Box As MaskedTextBox
    Friend WithEvents Ring_5_tubes_Box As MaskedTextBox
    Friend WithEvents Counter_Name_Box As MaskedTextBox
    Friend WithEvents Counter_file_name_Box As MaskedTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Item_Stand_Box As MaskedTextBox
    Friend WithEvents counter_geo_box As ComboBox
    Friend WithEvents Ring_5_radius_Box As MaskedTextBox
    Friend WithEvents Ring_4_radius_Box As MaskedTextBox
    Friend WithEvents Ring_3_radius_Box As MaskedTextBox
    Friend WithEvents Ring_2_radius_Box As MaskedTextBox
    Friend WithEvents Ring_1_radius_Box As MaskedTextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents SaveFileDialog2 As SaveFileDialog
End Class
