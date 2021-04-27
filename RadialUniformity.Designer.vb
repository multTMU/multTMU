<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RadialUniformity
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RadialUniformity))
        Me.container_id_box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_5_radius_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_4_radius_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_3_radius_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_2_radius_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_1_radius_Box = New System.Windows.Forms.MaskedTextBox()
        Me.counter_geo_box = New System.Windows.Forms.ComboBox()
        Me.Item_Stand_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_5_tubes_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_4_tubes_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_3_tubes_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_2_tubes_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Ring_1_tubes_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Number_rows_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Tube_Length_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Tube_ID_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Cd_liner_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Cavity_Length_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Cavity_ID_Box = New System.Windows.Forms.MaskedTextBox()
        Me.cavity_height_Box1 = New System.Windows.Forms.MaskedTextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Counter_Name_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.container_offset_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Radial_Bias_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Radial_Random_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Item_ID_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Pt_source_bias_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Container_Random_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Chart3 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'container_id_box
        '
        Me.container_id_box.Location = New System.Drawing.Point(232, 398)
        Me.container_id_box.Name = "container_id_box"
        Me.container_id_box.Size = New System.Drawing.Size(100, 20)
        Me.container_id_box.TabIndex = 0
        '
        'Ring_5_radius_Box
        '
        Me.Ring_5_radius_Box.Location = New System.Drawing.Point(237, 330)
        Me.Ring_5_radius_Box.Name = "Ring_5_radius_Box"
        Me.Ring_5_radius_Box.Size = New System.Drawing.Size(100, 20)
        Me.Ring_5_radius_Box.TabIndex = 174
        '
        'Ring_4_radius_Box
        '
        Me.Ring_4_radius_Box.Location = New System.Drawing.Point(237, 304)
        Me.Ring_4_radius_Box.Name = "Ring_4_radius_Box"
        Me.Ring_4_radius_Box.Size = New System.Drawing.Size(100, 20)
        Me.Ring_4_radius_Box.TabIndex = 173
        '
        'Ring_3_radius_Box
        '
        Me.Ring_3_radius_Box.Location = New System.Drawing.Point(237, 281)
        Me.Ring_3_radius_Box.Name = "Ring_3_radius_Box"
        Me.Ring_3_radius_Box.Size = New System.Drawing.Size(100, 20)
        Me.Ring_3_radius_Box.TabIndex = 172
        '
        'Ring_2_radius_Box
        '
        Me.Ring_2_radius_Box.Location = New System.Drawing.Point(237, 257)
        Me.Ring_2_radius_Box.Name = "Ring_2_radius_Box"
        Me.Ring_2_radius_Box.Size = New System.Drawing.Size(100, 20)
        Me.Ring_2_radius_Box.TabIndex = 171
        '
        'Ring_1_radius_Box
        '
        Me.Ring_1_radius_Box.Location = New System.Drawing.Point(237, 232)
        Me.Ring_1_radius_Box.Name = "Ring_1_radius_Box"
        Me.Ring_1_radius_Box.Size = New System.Drawing.Size(100, 20)
        Me.Ring_1_radius_Box.TabIndex = 170
        '
        'counter_geo_box
        '
        Me.counter_geo_box.FormattingEnabled = True
        Me.counter_geo_box.Items.AddRange(New Object() {"Cylindrical", "Rectangular"})
        Me.counter_geo_box.Location = New System.Drawing.Point(232, 12)
        Me.counter_geo_box.Name = "counter_geo_box"
        Me.counter_geo_box.Size = New System.Drawing.Size(115, 21)
        Me.counter_geo_box.TabIndex = 169
        '
        'Item_Stand_Box
        '
        Me.Item_Stand_Box.Location = New System.Drawing.Point(237, 356)
        Me.Item_Stand_Box.Name = "Item_Stand_Box"
        Me.Item_Stand_Box.Size = New System.Drawing.Size(100, 20)
        Me.Item_Stand_Box.TabIndex = 168
        '
        'Ring_5_tubes_Box
        '
        Me.Ring_5_tubes_Box.Location = New System.Drawing.Point(384, 330)
        Me.Ring_5_tubes_Box.Name = "Ring_5_tubes_Box"
        Me.Ring_5_tubes_Box.Size = New System.Drawing.Size(61, 20)
        Me.Ring_5_tubes_Box.TabIndex = 167
        '
        'Ring_4_tubes_Box
        '
        Me.Ring_4_tubes_Box.Location = New System.Drawing.Point(384, 304)
        Me.Ring_4_tubes_Box.Name = "Ring_4_tubes_Box"
        Me.Ring_4_tubes_Box.Size = New System.Drawing.Size(61, 20)
        Me.Ring_4_tubes_Box.TabIndex = 166
        '
        'Ring_3_tubes_Box
        '
        Me.Ring_3_tubes_Box.Location = New System.Drawing.Point(384, 281)
        Me.Ring_3_tubes_Box.Name = "Ring_3_tubes_Box"
        Me.Ring_3_tubes_Box.Size = New System.Drawing.Size(61, 20)
        Me.Ring_3_tubes_Box.TabIndex = 165
        '
        'Ring_2_tubes_Box
        '
        Me.Ring_2_tubes_Box.Location = New System.Drawing.Point(384, 257)
        Me.Ring_2_tubes_Box.Name = "Ring_2_tubes_Box"
        Me.Ring_2_tubes_Box.Size = New System.Drawing.Size(61, 20)
        Me.Ring_2_tubes_Box.TabIndex = 164
        '
        'Ring_1_tubes_Box
        '
        Me.Ring_1_tubes_Box.Location = New System.Drawing.Point(384, 232)
        Me.Ring_1_tubes_Box.Name = "Ring_1_tubes_Box"
        Me.Ring_1_tubes_Box.Size = New System.Drawing.Size(61, 20)
        Me.Ring_1_tubes_Box.TabIndex = 163
        '
        'Number_rows_Box
        '
        Me.Number_rows_Box.Location = New System.Drawing.Point(232, 185)
        Me.Number_rows_Box.Name = "Number_rows_Box"
        Me.Number_rows_Box.Size = New System.Drawing.Size(115, 20)
        Me.Number_rows_Box.TabIndex = 162
        '
        'Tube_Length_Box
        '
        Me.Tube_Length_Box.Location = New System.Drawing.Point(232, 159)
        Me.Tube_Length_Box.Name = "Tube_Length_Box"
        Me.Tube_Length_Box.Size = New System.Drawing.Size(115, 20)
        Me.Tube_Length_Box.TabIndex = 161
        '
        'Tube_ID_Box
        '
        Me.Tube_ID_Box.Location = New System.Drawing.Point(232, 136)
        Me.Tube_ID_Box.Name = "Tube_ID_Box"
        Me.Tube_ID_Box.Size = New System.Drawing.Size(115, 20)
        Me.Tube_ID_Box.TabIndex = 160
        '
        'Cd_liner_Box
        '
        Me.Cd_liner_Box.Location = New System.Drawing.Point(232, 110)
        Me.Cd_liner_Box.Name = "Cd_liner_Box"
        Me.Cd_liner_Box.Size = New System.Drawing.Size(115, 20)
        Me.Cd_liner_Box.TabIndex = 159
        '
        'Cavity_Length_Box
        '
        Me.Cavity_Length_Box.Location = New System.Drawing.Point(232, 84)
        Me.Cavity_Length_Box.Name = "Cavity_Length_Box"
        Me.Cavity_Length_Box.Size = New System.Drawing.Size(115, 20)
        Me.Cavity_Length_Box.TabIndex = 158
        '
        'Cavity_ID_Box
        '
        Me.Cavity_ID_Box.Location = New System.Drawing.Point(232, 59)
        Me.Cavity_ID_Box.Name = "Cavity_ID_Box"
        Me.Cavity_ID_Box.Size = New System.Drawing.Size(115, 20)
        Me.Cavity_ID_Box.TabIndex = 157
        '
        'cavity_height_Box1
        '
        Me.cavity_height_Box1.Location = New System.Drawing.Point(232, 35)
        Me.cavity_height_Box1.Name = "cavity_height_Box1"
        Me.cavity_height_Box1.Size = New System.Drawing.Size(115, 20)
        Me.cavity_height_Box1.TabIndex = 156
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.SystemColors.Control
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 24
        Me.ListBox1.Items.AddRange(New Object() {"Cavity Type", "Cavity Height (cm)", "Cavity Width or Dia. (cm)", "Cavity Lenth (cm)", "Cd Liner thickness (cm)", "Tube ID (cm)", "Tube Active Length (cm)", "Number of rows", "                                           Radius (cm)               Tubes (#)", "   Row 1 ", "   Row 2", "   Row 3 ", "   Row 4", "   Row 5", "Item Stand Height (cm)"})
        Me.ListBox1.Location = New System.Drawing.Point(11, 12)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(215, 384)
        Me.ListBox1.TabIndex = 155
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 18)
        Me.Label1.TabIndex = 176
        Me.Label1.Text = "Counter:"
        '
        'Counter_Name_Box
        '
        Me.Counter_Name_Box.Location = New System.Drawing.Point(89, 5)
        Me.Counter_Name_Box.Name = "Counter_Name_Box"
        Me.Counter_Name_Box.Size = New System.Drawing.Size(214, 20)
        Me.Counter_Name_Box.TabIndex = 175
        '
        'Chart1
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Enabled = False
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(33, 556)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.IsVisibleInLegend = False
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(675, 278)
        Me.Chart1.TabIndex = 177
        '
        'Chart2
        '
        ChartArea2.Name = "ChartArea1"
        Me.Chart2.ChartAreas.Add(ChartArea2)
        Legend2.Enabled = False
        Legend2.Name = "Legend1"
        Me.Chart2.Legends.Add(Legend2)
        Me.Chart2.Location = New System.Drawing.Point(33, 853)
        Me.Chart2.Name = "Chart2"
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series2.IsVisibleInLegend = False
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Me.Chart2.Series.Add(Series2)
        Me.Chart2.Size = New System.Drawing.Size(675, 278)
        Me.Chart2.TabIndex = 178
        Me.Chart2.Text = "Chart2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 398)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(203, 18)
        Me.Label2.TabIndex = 179
        Me.Label2.Text = "Container Inner Dia.  (cm)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 427)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(164, 18)
        Me.Label3.TabIndex = 181
        Me.Label3.Text = "Average Offset  (cm)"
        '
        'container_offset_Box
        '
        Me.container_offset_Box.Location = New System.Drawing.Point(232, 428)
        Me.container_offset_Box.Name = "container_offset_Box"
        Me.container_offset_Box.Size = New System.Drawing.Size(100, 20)
        Me.container_offset_Box.TabIndex = 182
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 118)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(188, 18)
        Me.Label6.TabIndex = 185
        Me.Label6.Text = "Systematic Contribution"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 84)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(168, 18)
        Me.Label7.TabIndex = 186
        Me.Label7.Text = "Random Contribution"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 149)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(300, 15)
        Me.Label8.TabIndex = 187
        Me.Label8.Text = "(assuming normal distribution about the typical offset.)"
        '
        'Radial_Bias_Box
        '
        Me.Radial_Bias_Box.Location = New System.Drawing.Point(204, 119)
        Me.Radial_Bias_Box.Name = "Radial_Bias_Box"
        Me.Radial_Bias_Box.Size = New System.Drawing.Size(100, 20)
        Me.Radial_Bias_Box.TabIndex = 188
        '
        'Radial_Random_Box
        '
        Me.Radial_Random_Box.Location = New System.Drawing.Point(203, 85)
        Me.Radial_Random_Box.Name = "Radial_Random_Box"
        Me.Radial_Random_Box.Size = New System.Drawing.Size(100, 20)
        Me.Radial_Random_Box.TabIndex = 189
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Item_ID_Box)
        Me.Panel1.Controls.Add(Me.Radial_Random_Box)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Radial_Bias_Box)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Counter_Name_Box)
        Me.Panel1.Location = New System.Drawing.Point(547, 47)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(343, 169)
        Me.Panel1.TabIndex = 190
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(4, 38)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(63, 18)
        Me.Label17.TabIndex = 261
        Me.Label17.Text = "Item Id:"
        '
        'Item_ID_Box
        '
        Me.Item_ID_Box.Location = New System.Drawing.Point(89, 35)
        Me.Item_ID_Box.Name = "Item_ID_Box"
        Me.Item_ID_Box.Size = New System.Drawing.Size(215, 20)
        Me.Item_ID_Box.TabIndex = 260
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(544, 283)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(223, 16)
        Me.Label9.TabIndex = 191
        Me.Label9.Text = "Assumes volumetric calibration"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(544, 301)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(293, 16)
        Me.Label10.TabIndex = 192
        Me.Label10.Text = "Pt source efficiency calibration adds bias"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(544, 335)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(323, 16)
        Me.Label11.TabIndex = 193
        Me.Label11.Text = "Additional Bias From Point Source Calibration"
        '
        'Pt_source_bias_Box
        '
        Me.Pt_source_bias_Box.Location = New System.Drawing.Point(643, 358)
        Me.Pt_source_bias_Box.Name = "Pt_source_bias_Box"
        Me.Pt_source_bias_Box.Size = New System.Drawing.Size(100, 20)
        Me.Pt_source_bias_Box.TabIndex = 194
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(544, 319)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(251, 15)
        Me.Label12.TabIndex = 195
        Me.Label12.Text = "(over reports due to underestimate efficiency)"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(18, 456)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(167, 18)
        Me.Label14.TabIndex = 197
        Me.Label14.Text = "Random Offset  (cm)"
        '
        'Container_Random_Box
        '
        Me.Container_Random_Box.Location = New System.Drawing.Point(232, 457)
        Me.Container_Random_Box.Name = "Container_Random_Box"
        Me.Container_Random_Box.Size = New System.Drawing.Size(100, 20)
        Me.Container_Random_Box.TabIndex = 198
        '
        'Chart3
        '
        ChartArea3.Name = "ChartArea1"
        Me.Chart3.ChartAreas.Add(ChartArea3)
        Legend3.Enabled = False
        Legend3.Name = "Legend1"
        Me.Chart3.Legends.Add(Legend3)
        Me.Chart3.Location = New System.Drawing.Point(31, 1145)
        Me.Chart3.Name = "Chart3"
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Me.Chart3.Series.Add(Series3)
        Me.Chart3.Size = New System.Drawing.Size(675, 278)
        Me.Chart3.TabIndex = 199
        Me.Chart3.Text = "Chart3"
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(831, 1354)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(41, 197)
        Me.Panel2.TabIndex = 200
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(42, 1426)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(309, 15)
        Me.Label16.TabIndex = 202
        Me.Label16.Text = "Distribution based on " & samp_pts & " random container positions."
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(544, 420)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(451, 16)
        Me.Label22.TabIndex = 256
        Me.Label22.Text = "Note: Only outermost Tube diameter establishes effective radius"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(544, 438)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(367, 16)
        Me.Label21.TabIndex = 255
        Me.Label21.Text = "(Tube number dependence is a future enhancement)"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1001, -1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 257
        Me.Button1.Text = "Print"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.counter_geo_box)
        Me.Panel3.Controls.Add(Me.container_id_box)
        Me.Panel3.Controls.Add(Me.ListBox1)
        Me.Panel3.Controls.Add(Me.cavity_height_Box1)
        Me.Panel3.Controls.Add(Me.Cavity_ID_Box)
        Me.Panel3.Controls.Add(Me.Cavity_Length_Box)
        Me.Panel3.Controls.Add(Me.Container_Random_Box)
        Me.Panel3.Controls.Add(Me.Cd_liner_Box)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.Tube_ID_Box)
        Me.Panel3.Controls.Add(Me.Tube_Length_Box)
        Me.Panel3.Controls.Add(Me.Number_rows_Box)
        Me.Panel3.Controls.Add(Me.Ring_1_tubes_Box)
        Me.Panel3.Controls.Add(Me.Ring_2_tubes_Box)
        Me.Panel3.Controls.Add(Me.Ring_3_tubes_Box)
        Me.Panel3.Controls.Add(Me.Ring_4_tubes_Box)
        Me.Panel3.Controls.Add(Me.Ring_5_tubes_Box)
        Me.Panel3.Controls.Add(Me.container_offset_Box)
        Me.Panel3.Controls.Add(Me.Item_Stand_Box)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Ring_1_radius_Box)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Ring_2_radius_Box)
        Me.Panel3.Controls.Add(Me.Ring_3_radius_Box)
        Me.Panel3.Controls.Add(Me.Ring_4_radius_Box)
        Me.Panel3.Controls.Add(Me.Ring_5_radius_Box)
        Me.Panel3.Location = New System.Drawing.Point(24, 47)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(476, 487)
        Me.Panel3.TabIndex = 258
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(381, 213)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 16)
        Me.Label5.TabIndex = 260
        Me.Label5.Text = "Tubes (#)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(241, 213)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 16)
        Me.Label4.TabIndex = 259
        Me.Label4.Text = "Radius (cm)"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(416, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(292, 18)
        Me.Label15.TabIndex = 259
        Me.Label15.Text = "Radial Offset Contribution to the TMU"
        '
        'ListBox2
        '
        Me.ListBox2.BackColor = System.Drawing.SystemColors.Control
        Me.ListBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 16
        Me.ListBox2.Items.AddRange(New Object() {"Note: Distribution about assay mass result is non-normal.", "(refer to chart - Relative Efficiency with Fill Height)"})
        Me.ListBox2.Location = New System.Drawing.Point(547, 239)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(434, 32)
        Me.ListBox2.TabIndex = 260
        '
        'Form9
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1174, 712)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Chart3)
        Me.Controls.Add(Me.Pt_source_bias_Box)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Chart2)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.Panel3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form9"
        Me.Text = "Radial Uniformity"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents container_id_box As MaskedTextBox
    Friend WithEvents Ring_5_radius_Box As MaskedTextBox
    Friend WithEvents Ring_4_radius_Box As MaskedTextBox
    Friend WithEvents Ring_3_radius_Box As MaskedTextBox
    Friend WithEvents Ring_2_radius_Box As MaskedTextBox
    Friend WithEvents Ring_1_radius_Box As MaskedTextBox
    Friend WithEvents counter_geo_box As ComboBox
    Friend WithEvents Item_Stand_Box As MaskedTextBox
    Friend WithEvents Ring_5_tubes_Box As MaskedTextBox
    Friend WithEvents Ring_4_tubes_Box As MaskedTextBox
    Friend WithEvents Ring_3_tubes_Box As MaskedTextBox
    Friend WithEvents Ring_2_tubes_Box As MaskedTextBox
    Friend WithEvents Ring_1_tubes_Box As MaskedTextBox
    Friend WithEvents Number_rows_Box As MaskedTextBox
    Friend WithEvents Tube_Length_Box As MaskedTextBox
    Friend WithEvents Tube_ID_Box As MaskedTextBox
    Friend WithEvents Cd_liner_Box As MaskedTextBox
    Friend WithEvents Cavity_Length_Box As MaskedTextBox
    Friend WithEvents Cavity_ID_Box As MaskedTextBox
    Friend WithEvents cavity_height_Box1 As MaskedTextBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Counter_Name_Box As MaskedTextBox
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents Chart2 As DataVisualization.Charting.Chart
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents container_offset_Box As MaskedTextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Radial_Bias_Box As MaskedTextBox
    Friend WithEvents Radial_Random_Box As MaskedTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Pt_source_bias_Box As MaskedTextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Container_Random_Box As MaskedTextBox
    Friend WithEvents Chart3 As DataVisualization.Charting.Chart
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Item_ID_Box As MaskedTextBox
    Friend WithEvents ListBox2 As ListBox
End Class
