Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Imports Microsoft.VisualBasic.powerpacks.printing
Public Class Form10
    Public radial_likely_offset
    Public container_IDiam, temp_eff, temp_fd, temp_ft, typical_container_offset, Random_container_offset, detector_stand, container_H
    Public average_density, minimum_density, maximum_density, minimum_fill_height, maximum_fill_height, pu_mass_fraction, average_fill_height
    '  Public min_fill_mass, max_fill_mass





    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pf As New PrintForm
        Size = New Size(1200, 700)
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
        AutoScrollPosition = New Point(60, 650)
        pf.Form = Me
        pf.PrinterSettings.DefaultPageSettings.Landscape = False
        pf.PrinterSettings.Copies = 1
        pf.PrinterSettings.DefaultPageSettings.Margins = New Printing.Margins(60, 30, 90, 30)
        pf.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = Printing.PaperKind.Letter
        pf.PrintAction = Printing.PrintAction.PrintToPrinter
        'pf.Print()
        pf.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)
    End Sub




    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim det_dim_file_name, file_location As String
        Dim matrix_type As String

        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file

        Dim item_param_file As String
        file_location = "c:\multiplicity_TMU\item_info\"
        item_param_file = file_location & "current_item.csv"

        Call get_item_info_file(item_param_file)                        ' read in item specific information from csv file

        det_dim_file_name = "c:\multiplicity_tmu\detector_dimensions\current_det_dimensions.csv"

        Call get_det_dimensions_file(det_dim_file_name)
        Dim upu_min, upu_max


        container_IDiam = geo_par(2)
        container_H = geo_par(3)

        typical_container_offset = geo_par(8)
        Random_container_offset = geo_par(9)
        matrix_type = geo_par(10)
        upu_ratio = geo_par(11)
        upu_min = geo_par(12)
        upu_max = geo_par(13)

        detector_stand = det_dim_val(15)

        pu_mass_fraction = 1
        If matrix_type = "MOX" Then pu_mass_fraction = (239 / (239 + 32))
        If matrix_type = "PuO2" Then pu_mass_fraction = (239 / (239 + 32))
        If matrix_type = "metal" Then pu_mass_fraction = 1


        average_density = geo_par(14)
        minimum_density = geo_par(15)
        maximum_density = geo_par(16)

        If minimum_density <= 0 Then minimum_density = 0.0012
        If maximum_density <= average_density Then maximum_density = average_density + 0.0012

        average_fill_height = Final_mass_new / average_density / pu_mass_fraction * (1 + upu_ratio) / (PI * (container_IDiam / 2) ^ 2)
        minimum_fill_height = Final_mass_new / maximum_density / pu_mass_fraction * (1 + upu_ratio) / (PI * (container_IDiam / 2) ^ 2)
        maximum_fill_height = Final_mass_new / minimum_density / pu_mass_fraction * (1 + upu_ratio) / (PI * (container_IDiam / 2) ^ 2)

        '     MsgBox(average_density & " : " & pu_mass_fraction & " : " & upu_ratio & " : " & container_IDiam)

        Call calc_axial_errors()
        Call plot_axial()

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
        Averge_density_Box.Text = Int(1000 * average_fill_height) / 1000
        minimum_density_Box.Text = Int(1000 * minimum_fill_height) / 1000
        maximum_density_Box.Text = Int(1000 * maximum_fill_height) / 1000

        If pt_cal_flag Then CheckBox1.Checked = True Else CheckBox1.Checked = False
        If pt_cal_flag Then CheckBox2.Checked = False Else CheckBox2.Checked = True
        typical_cal_FH_Box.Text = det_cal_FH


        'calculate bias from the mass values caulcated in subroutine calc_axial_errors
        pt_bias = avg_fill_mass / m240_center - 1
        pt_cal_bias = pt_bias
        distrib_cal_bias = avg_fill_mass / distrib_cal_mass - 1
        rand_fill_height_err = st_dev_rnd_mass    '   Abs(max_fill_mass - min_fill_mass) / avg_fill_mass / 6)
        Axial_Random_Box.Text = Int(100000 * rand_fill_height_err) / 1000 & "%"
        If pt_cal_flag Then Pt_source_bias_Box.Text = Int(100000 * pt_bias) / 1000 & "%" Else Pt_source_bias_Box.Text = Int(100000 * distrib_cal_bias) / 1000 & "%"


        lower_density_mass_Box.Text = Int(10000 * (min_fill_mass / avg_fill_mass)) / 100 & "%"
        upper_density_mass_Box.Text = Int(10000 * (max_fill_mass / avg_fill_mass)) / 100 & "%"



    End Sub



    Sub plot_axial()

        '    ****** PLOT  AXIAL RESPONSE     ******

        Dim R, radius, ymin, ymax, xmin, xmax

        ymin = 0
        ymax = 0
        For i = 1 To 101
            If eff_z(i) > ymax Then ymax = eff_z(i)

        Next i

        ymin = ymax

        For i = 1 To 49

            If eff_z(i) < ymin Then ymin = eff_z(i)
        Next i

        ymin = Int(ymin * 100 - 0.5) / 100

        With Chart1.ChartAreas(0)

            xmin = -Int(cavity_H / 2 + 0.5)
            xmax = Int(cavity_H / 2 + 0.5)
            .AxisX.Minimum = xmin
            .AxisX.Interval = (xmax - xmin) / 10
            .AxisX.Maximum = xmax

            ' .AxisY.IsLogarithmic = True
            .AxisY.Minimum = ymin
            .AxisY.Interval = 0.01
            .AxisY.Maximum = ymax

            .AxisX.Title = "Vertical distance from cavity Center (cm)"
            .AxisY.Title = "Relative Efficiency"
        End With

        Chart1.Series.Clear()
        Chart1.Series.Add("Axial_Response")
        Chart1.Titles.Add("Relative Efficiency (Pt Source) at z=0")

        With Chart1.Series(0)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            .BorderWidth = 2
            '    .Color = Color.Blue
            '  .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            '  .MarkerSize = 8
            '.IsVisibleInLegend = False
            For i = 1 To 101
                z = (cavity_H / 100) * (i - 1) - cavity_H / 2
                Me.Chart1.Series("Axial_Response").Points.AddXY(z, eff_z(i))
            Next

            Dim PC As New CalloutAnnotation
            With PC
                Chart1.Annotations.Add(PC)
            End With

        End With

        '    ****** PLOT Avg Efficiency vs Fill Hieght     ******

        Dim Z2, ymin2, ymax2

        ymin2 = 0
        ymax2 = 0
        For i = 1 To 20
            If eff_avg(i) > ymax2 Then ymax2 = eff_avg(i)
        Next
        ymin2 = ymax2
        For i = 1 To 20
            If eff_avg(i) < ymin2 Then ymin2 = eff_avg(i)
        Next
        ymax2 = Max(1, Int(ymax2 * 100 + 1) / 100)
        ymin2 = Int(ymin2 * 100 - 1) / 100

        '  MsgBox(" ymin : " & ymin2 & " ymax " & ymax2)
        '     MsgBox(" cont H " & container_H & vbCrLf & eff_avg(1) & vbCrLf & eff_avg(9) & vbCrLf & container_IDiam)
        ' 

        With Chart2.ChartAreas(0)

            .AxisX.Minimum = 0
            .AxisX.Interval = Int(container_H + 0.5) / 10
            .AxisX.Maximum = Int(container_H + 0.5)

            ' .AxisY.IsLogarithmic = True
            .AxisY.Minimum = ymin2
            '  .AxisY.Interval = 0.005
            .AxisY.Maximum = ymax2

            .AxisX.Title = "Fill Height (cm)"
            .AxisY.Title = "Relative Efficiency"
        End With

        Chart2.Series.Clear()
        Chart2.Series.Add("Axial_Bias")
        Chart2.Series.Add("Fill Range")
        Chart2.Titles.Add("Relative Efficiency with Fill Height")

        With Chart2.Series(0)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            .BorderWidth = 2
            .Color = Color.Black
            For i = 1 To 20
                Z2 = (i) * container_H / 20
                Me.Chart2.Series("Axial_Bias").Points.AddXY(Z2, eff_avg(i))
            Next

        End With

        ' show material fill range on plot
        With Chart2.Series(1)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            .BorderWidth = 2
            .BorderDashStyle = ChartDashStyle.DashDot
            .Color = Color.Green

            Me.Chart2.Series("Fill Range").Points.AddXY(minimum_fill_height, 0)
            Me.Chart2.Series("Fill Range").Points.AddXY(minimum_fill_height, 1.5)
            Me.Chart2.Series("Fill Range").Points.AddXY(maximum_fill_height, 1.5)
            Me.Chart2.Series("Fill Range").Points.AddXY(maximum_fill_height, 0)

        End With

        GoTo 100
        '        '  
100: ' exit point

        '
        '-----------------------------------------------------------------------------------------
        ''  
        '    ****** PLOT AXIAL Distribution  - rand_dist_bin    ******
        '
        '-----------------------------------------------------------------------------------------
        '
        Dim rand_mass_range, rand_step, step1, rand_dist_bin(50), bin_start, bin_low, max_rand_prob, temp_mass
        Dim mass_rand_min, mass_rand_max
        Dim ymin3, ymax3, m_1

        cavity_H = det_dim_val(2)
        cavity_ID = det_dim_val(3)

        mass_rand_min = 0
        mass_rand_max = 0
        max_rand_prob = 0

        For i = 1 To 50
            rand_dist_bin(i) = 0
        Next

        temp_mass = 0
        For i = 1 To samp_pts
            mass_rand_v(i) = mass_rand_v(i) * (m240_center / avg_rnd_mass)
            temp_mass = temp_mass + mass_rand_v(i)
        Next i

        ' MsgBox("normalized  mass = " & temp_mass / samp_pts)

        For i = 1 To samp_pts
            If mass_rand_v(i) > mass_rand_max Then mass_rand_max = mass_rand_v(i)      ' find largest mass result
        Next

        mass_rand_min = mass_rand_max

        For i = 1 To samp_pts
            If mass_rand_v(i) < mass_rand_min Then mass_rand_min = mass_rand_v(i)       ' find smallest mass result
        Next
        rand_mass_range = mass_rand_max - mass_rand_min

        rand_step = Max(0.05, Int(rand_mass_range + 0.5) / 40)                          ' divide the mass range into 40 bins
        bin_start = Int(40 * mass_rand_min) / 40
        bin_low = bin_start - 3 / 2 * rand_step


        For i = 1 To 45
            rand_dist_bin(i) = 0
            bin_low = bin_low + rand_step
            For j = 1 To samp_pts
                If mass_rand_v(j) > bin_low And mass_rand_v(j) <= (bin_low + rand_step) Then rand_dist_bin(i) = rand_dist_bin(i) + 1
            Next j
            If rand_dist_bin(i) > max_rand_prob Then max_rand_prob = rand_dist_bin(i)           ' find max probability
        Next i

        For i = 1 To 50
            rand_dist_bin(i) = rand_dist_bin(i) / samp_pts                                      ' normalize by number of samples
        Next i

        max_rand_prob = max_rand_prob / samp_pts


        ymin3 = 0
        ymax3 = max_rand_prob * 1.1                                                             '  vertical plot extent

        ' MsgBox("  distr plot limits " & ymin3 & vbCrLf & ymax3 & vbCrLf & max_rand_prob & vbCrLf & rand_step & vbCrLf & rand_mass_range)

        With Chart3.ChartAreas(0)

            .AxisX.Minimum = bin_start
            .AxisX.Interval = rand_step * 5
            .AxisX.Maximum = bin_start + 49 * rand_step

            ' .AxisY.IsLogarithmic = True
            .AxisY.Minimum = 0
            .AxisY.Interval = Int(100 * ymax3 + 0.5) / 100 / 10
            .AxisY.Maximum = Int(100 * ymax3 + 0.5) / 100

            .AxisX.Title = "240Pu Mass (g)"
            .AxisY.Title = "Probability"
        End With

        Chart3.Series.Clear()
        Chart3.Series.Add("Probability")
        Chart3.Titles.Add("Fill Height Mass Distribution")

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
    Function z_eff(T_L, R_cav, Radial_offset, z_offset)
        ' creates vertical axial response profile
        Dim T_L2
        T_L2 = T_L * 1.1
        z_eff = ((4 * PI - 2 * R_cav ^ 2 * (T_L2 ^ 2 + 4 * z_offset ^ 2) / (T_L2 ^ 2 - 4 * z_offset ^ 2) ^ 2) / (4 * PI - 2 * (R_cav / T_L2) ^ 2))


    End Function

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Chart2_Click(sender As Object, e As EventArgs) Handles Chart2.Click

    End Sub


End Class