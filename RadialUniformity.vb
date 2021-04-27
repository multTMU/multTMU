Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Imports Microsoft.VisualBasic.powerpacks.printing
Public Class RadialUniformity
    Public radial_likely_offset
    '  Public counter_id, current_det_file_name As String
    Public container_IDiam, temp_eff, temp_fd, temp_ft, typical_container_offset, Random_container_offset
    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim det_dim_file_name, file_location As String

        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file

        Dim item_param_file As String
        file_location = "c:\multiplicity_TMU\item_info\"
        item_param_file = file_location & "current_item.csv"

        Call get_item_info_file(item_param_file)                        ' read in item specific information from csv file

        det_dim_file_name = "c:\multiplicity_tmu\detector_dimensions\current_det_dimensions.csv"

        container_IDiam = geo_par(2)
        typical_container_offset = geo_par(8)
        Random_container_offset = geo_par(9)

        Call get_det_dimensions_file(det_dim_file_name)

        Call calc_radial_errors()

        container_IDiam = geo_par(2)
        typical_container_offset = geo_par(8)
        Random_container_offset = geo_par(9)

        ' ***** Load data file contents onto screen *****
        Counter_Name_Box.Text = counter_id
        Item_ID_Box.Text = item_id

        '     Counter_file_name_Box.Text = current_det_file_name
        counter_geo_box.Text = det_dim_val(1)
        cavity_height_Box1.Text = det_dim_val(2)
        Cavity_ID_Box.Text = det_dim_val(3)
        Cavity_Length_Box.Text = det_dim_val(4)
        Cd_liner_Box.Text = det_dim_val(5)
        Tube_ID_Box.Text = det_dim_val(6)
        Tube_Length_Box.Text = det_dim_val(7)
        Number_rows_Box.Text = det_dim_val(8)
        Ring_1_radius_Box.Text = det_dim_val(10)
        Ring_2_radius_Box.Text = det_dim_val(11)
        Ring_3_radius_Box.Text = det_dim_val(12)
        Ring_4_radius_Box.Text = det_dim_val(13)
        Ring_5_radius_Box.Text = det_dim_val(14)
        Item_Stand_Box.Text = det_dim_val(15)
        Ring_1_tubes_Box.Text = tube_numbers(1)
        Ring_2_tubes_Box.Text = tube_numbers(2)
        Ring_3_tubes_Box.Text = tube_numbers(3)
        Ring_4_tubes_Box.Text = tube_numbers(4)
        Ring_5_tubes_Box.Text = tube_numbers(5)
        container_id_box.Text = container_IDiam
        container_offset_Box.Text = typical_container_offset
        Container_Random_Box.Text = Random_container_offset


    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pf As New PrintForm
        Size = New Size(1200, 600)
        AutoScrollPosition = New Point(1, 1)
        pf.Form = Me
        pf.PrinterSettings.DefaultPageSettings.Landscape = True
        pf.PrinterSettings.Copies = 1
        pf.PrinterSettings.DefaultPageSettings.Margins = New Printing.Margins(60, 30, 90, 30)
        pf.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = Printing.PaperKind.Letter
        pf.PrintAction = Printing.PrintAction.PrintToPrinter
        'pf.Print()
        pf.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)

        Size = New Size(1200, 920)
        AutoScrollPosition = New Point(60, 550)
        pf.Form = Me
        pf.PrinterSettings.DefaultPageSettings.Landscape = False
        pf.PrinterSettings.Copies = 1
        pf.PrinterSettings.DefaultPageSettings.Margins = New Printing.Margins(60, 30, 90, 30)
        pf.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = Printing.PaperKind.Letter
        pf.PrintAction = Printing.PrintAction.PrintToPrinter
        'pf.Print()
        pf.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)


    End Sub


    Private Sub Counter_Name_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles Counter_Name_Box.MaskInputRejected

    End Sub

    Private Sub calc_radial_errors()
        '
        Dim myStream As Stream = Nothing
        Dim det_dimfile_name As String

        Dim str11, str12, str13, str14 As String
        Dim jdex As Integer
        Dim det_par_file_name As String
        Dim sep_val1, sep_val2 As String

        container_IDiam = geo_par(2)
        typical_container_offset = geo_par(8)
        Random_container_offset = geo_par(9)

        Call calc_radial_error()

        Radial_Bias_Box.Text = Int(10000 * (avg_rnd_mass / vol_center_mass - 1)) / 100 & "%"
        Radial_Random_Box.Text = Int(10000 * st_dev_rnd_mass / avg_rnd_mass) / 100 & "%"
        Pt_source_bias_Box.Text = Int(10000 * (m240_center / vol_center_mass - 1)) / 100 & "%"
        '
        '   ----------------calculate mass probability distribution  ---------------------------
        '
        Dim rand_mass_range, rand_step, step1, rand_dist_bin(50), bin_start, bin_low, max_rand_prob, temp_mass
        Dim mass_rand_min, mass_rand_max

        cavity_H = det_dim_val(2)
        cavity_ID = det_dim_val(3)

        mass_rand_min = 0
        mass_rand_max = 0
        max_rand_prob = 0


        temp_mass = 0
        For i = 1 To samp_pts
            mass_rand(i) = mass_rand(i) * (m240_center / avg_rnd_mass)
            temp_mass = temp_mass + mass_rand(i)
        Next i

        ' MsgBox("normalized  mass = " & temp_mass / samp_pts)

        For i = 1 To samp_pts
            If mass_rand(i) > mass_rand_max Then mass_rand_max = mass_rand(i)
        Next

        mass_rand_min = mass_rand_max

        For i = 1 To samp_pts
            If mass_rand(i) < mass_rand_min Then mass_rand_min = mass_rand(i)
        Next
        rand_mass_range = mass_rand_max - mass_rand_min

        rand_step = Max(0.025, Int(rand_mass_range + 0.5) / 40)
        bin_start = Int(40 * mass_rand_min) / 40
        bin_low = bin_start - 3 / 2 * rand_step



        For i = 1 To 44
            rand_dist_bin(i) = 0
            bin_low = bin_low + rand_step
            For j = 1 To samp_pts
                If mass_rand(j) > bin_low And mass_rand(j) <= bin_low + rand_step Then rand_dist_bin(i) = rand_dist_bin(i) + 1
            Next j
            If rand_dist_bin(i) > max_rand_prob Then max_rand_prob = rand_dist_bin(i)
        Next i

        For i = 1 To 50
            rand_dist_bin(i) = rand_dist_bin(i) / samp_pts
        Next i
        max_rand_prob = max_rand_prob / samp_pts

        ' ********      save rand data to temp file for diagnostics or fun  *******************


        ' ****  save cycle by cycle rates to temporary file - temp_rates.csv ****

        '  Dim out_rates_file, klineout As String
        '  out_rates_file = "c:\multiplicity_tmu\rand_rates.csv"

        '   klineout = "Bias calcualtion intermediate results , " & out_rates_file & " , " & vbCrLf

        ' Dim fs As FileStream = File.Create(out_rates_file)
        '  Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        '  fs.Write(info, 0, info.Length)

        '  fs.Close()

        '  For i = 1 To samp_pts
        '  klineout = i & ", " & eff_rnd(i) & ", " & mass_rand(i) & ", " & radial(i)
        '  klineout = klineout & vbCrLf

        ' My.Computer.FileSystem.WriteAllText(out_rates_file, klineout, True)

        '  Next i
        '  klineout = "******************" & vbCrLf
        '
        '-----------------------------------------------------------------------------------------
        '
        '    ****** PLOT  RADIAL RESPONSE  -  eff_r   ******
        '
        '-----------------------------------------------------------------------------------------
        '
        Dim R, radius, ymin, ymax, xmin, xmax

        ymin = 0
        ymax = 0
        For i = 1 To 101
            If eff_r(i) > ymax Then ymax = eff_r(i)
        Next
        ymin = ymax
        For i = 1 To 101

            If eff_r(i) < ymin Then ymin = eff_r(i)

        Next

        With Chart1.ChartAreas(0)

            xmin = -Int(cavity_ID / 2 + 0.5)
            xmax = Int(cavity_ID / 2 + 0.5)
            .AxisX.Minimum = xmin
            .AxisX.Interval = (xmax - xmin) / 10
            .AxisX.Maximum = xmax

            ' .AxisY.IsLogarithmic = True
            .AxisY.Minimum = 1  '  ymin
            .AxisY.Interval = 0.01
            .AxisY.Maximum = ymax

            .AxisX.Title = "Radial Distance (cm)"
            .AxisY.Title = "Relative Efficiency"

        End With

        Chart1.Series.Clear()
        Chart1.Series.Add("Radial_Response")
        Chart1.Titles.Add("Relative Efficiency (Pt. Source)")

        With Chart1.Series(0)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            .BorderWidth = 2
            '    .Color = Color.Blue
            '  .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            '  .MarkerSize = 8
            '.IsVisibleInLegend = False
            For i = 1 To 101

                R = (cavity_ID / 100) * (i - 1) - cavity_ID / 2
                Me.Chart1.Series("Radial_Response").Points.AddXY(R, eff_r(i))

            Next

            Dim PC As New CalloutAnnotation
            With PC
                Chart1.Annotations.Add(PC)
            End With

        End With
        '
        '-----------------------------------------------------------------------------------------
        '
        '    ****** PLOT  RADIAL BIAS - eff_avg    ******
        '
        '-----------------------------------------------------------------------------------------
        '
        Dim R2, ymin2, ymax2

        vol_delta_max = (cavity_ID - container_IDiam) / 2

        ymin2 = 0
        ymax2 = 0
        For i = 1 To 10
            If vol_avg(i) > ymax2 Then ymax2 = vol_avg(i)
        Next
        ymin2 = ymax2
        For i = 1 To 10
            If vol_avg(i) < ymin2 Then ymin2 = vol_avg(i)
        Next
        ymax2 = Int(ymax2 * 100 + 0.5) / 100
        ymin2 = Int(ymin2 * 100 - 0.5) / 100

        '   MsgBox(vol_delta_max & vbCrLf & vol_avg(1) & vbCrLf & vol_avg(9) & vbCrLf & container_IDiam)

        With Chart2.ChartAreas(0)

            .AxisX.Minimum = 0
            .AxisX.Interval = 1
            .AxisX.Maximum = Int(vol_delta_max + 0.5)

            ' .AxisY.IsLogarithmic = True
            .AxisY.Minimum = ymin2
            .AxisY.Interval = 0.005
            .AxisY.Maximum = ymax2

            .AxisX.Title = "Container Offset (cm)"
            .AxisY.Title = "Relative Efficiency"
        End With

        Chart2.Series.Clear()
        Chart2.Series.Add("Radial_Bias")
        Chart2.Titles.Add("Relative Efficiency (Can Average)")

        With Chart2.Series(0)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            .BorderWidth = 2
            '    .Color = Color.Blue
            '  .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            '  .MarkerSize = 8
            '.IsVisibleInLegend = False
            For i = 1 To 10
                R2 = vol_delta_max / 10 * (i - 1)
                Me.Chart2.Series("Radial_Bias").Points.AddXY(R2, eff_avg(i))
            Next

            Dim PC As New CalloutAnnotation
            With PC
                Chart2.Annotations.Add(PC)
            End With

        End With
        '
        '-----------------------------------------------------------------------------------------
        ''  
        '    ****** PLOT  RADIAL Distribution  - rand_dist_bin    ******
        '
        '-----------------------------------------------------------------------------------------
        '
        Dim ymin3, ymax3, m_1

        ymin3 = 0
        ymax3 = max_rand_prob * 1.1

        ' MsgBox("  distr plot limits " & ymin3 & vbCrLf & ymax3 & vbCrLf & max_rand_prob & vbCrLf & rand_step & vbCrLf & rand_mass_range)

        With Chart3.ChartAreas(0)

            .AxisX.Minimum = bin_start
            .AxisX.Interval = rand_step * 5
            .AxisX.Maximum = bin_start + 50 * rand_step

            ' .AxisY.IsLogarithmic = True
            .AxisY.Minimum = 0
            .AxisY.Interval = Int(100 * ymax3 + 0.5) / 100 / 10
            .AxisY.Maximum = Int(100 * ymax3 + 0.5) / 100

            .AxisX.Title = "240Pu Mass (g)"
            .AxisY.Title = "Probability"
        End With

        Chart3.Series.Clear()
        Chart3.Series.Add("Probability")
        Chart3.Titles.Add("Radial Offset Mass Distribution")

        With Chart3.Series(0)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            .BorderWidth = 2
            '    .Color = Color.Blue
            '  .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            '  .MarkerSize = 8
            '.IsVisibleInLegend = False
            Me.Chart3.Series("Probability").Points.AddXY(bin_start, 0)

            For i = 1 To 48
                m_1 = bin_start + rand_step * i
                Me.Chart3.Series("Probability").Points.AddXY(m_1, rand_dist_bin(i))
            Next

            Dim PC As New CalloutAnnotation
            With PC
                Chart3.Annotations.Add(PC)
            End With

        End With


    End Sub





    Function rad_eff(T_L, R_cav, Radial_offset, z_offset)
        rad_eff = 1 / ((4 * PI - 2 * (R_cav ^ 2 + 4 * Radial_offset ^ 2) * (T_L ^ 2 + 4 * z_offset ^ 2) / (T_L ^ 2 - 4 * z_offset ^ 2) ^ 2) / (4 * PI - 2 * (R_cav / T_L) ^ 2)) ^ 2
    End Function

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Chart2_Click(sender As Object, e As EventArgs) Handles Chart2.Click

    End Sub


End Class