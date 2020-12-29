Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Imports Microsoft.VisualBasic.PowerPacks.Printing
Public Class Form14
    Public alpha1, alpha2, alpha3
    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        alpha1 = 0.5
        alpha2 = 1
        alpha3 = 3

        Call update_form_14()

    End Sub

    Sub update_form_14()


        Dim alpha0, n_eff, doub_frac, trip_frac, back_rate(3), assay_time, mass, mass_240, multi
        Dim rates(1000, 3, 3), rates_sig(1000, 3, 3), nul_covar(3, 3)
        Dim i, j, k As Integer
        '   Dim alpha1, alpha2, alpha3



        Alpha_plot_Box1.Text = alpha1
        Alpha_plot_Box2.Text = alpha2
        Alpha_plot_Box3.Text = alpha3

        n_eff = det_par_val(1)
        gate_width = det_par_val(3)
        doub_frac = det_par_val(4)
        trip_frac = det_par_val(5)

        back_rate(1) = file_pass_sing_bkg
        back_rate(2) = file_pass_doub_bkg
        back_rate(3) = file_pass_trip_bkg

        assay_time = grunt_time(1)

        MaskedTextBox1.Text = assay_time
        MaskedTextBox2.Text = Int(100000 * mult_new) / 100000
        MaskedTextBox3.Text = Int(10000 * alpha_new) / 10000
        MaskedTextBox4.Text = Int(10000 * m240_new) / 10000
        MaskedTextBox5.Text = Int(1000 * Final_mass_new) / 1000
        MaskedTextBox6.Text = Int(100000 * m240_new_err / m240_new) / 1000 & "%"

        alpha0 = 0.56
        For i = 1 To 3
            For j = 1 To 3
                nul_covar(i, j) = 0
            Next j
        Next i
        For k = 1 To 3
            alpha0 = alpha1
            If k = 2 Then alpha0 = alpha2
            If k = 3 Then alpha0 = alpha3
            For j = 1 To 1000
                mass = 2 * j
                mass_240 = pu_240_effective * mass
                '  multi = 1 + 0.075 * (mass / 722) ^ 1.5
                multi = mult_est(mass)

                For i = 1 To 3
                    rates(j, i, k) = c_rates(i, mass_240, multi, alpha0, n_eff, doub_frac, trip_frac, gate_width, fiss_const_val, back_rate, assay_time)
                    rates_sig(j, i, k) = c_rates(i + 10, mass_240, multi, alpha0, n_eff, doub_frac, trip_frac, gate_width, fiss_const_val, back_rate, assay_time)
                Next i
            Next j
        Next k
        Dim calc_mass(1000), calc_mass_err(1000), rates_temp(3), rates_err_temp(3), rel_rate_err(1000, k)

        For k = 1 To 3
            For i = 1 To 1000
                For j = 1 To 3
                    rates_temp(j) = rates(i, j, k)
                    rates_err_temp(j) = rates_sig(i, j, k)
                    nul_covar(j, j) = rates_err_temp(j) ^ 2
                Next j

                calc_mass(i) = mult_anal2(rates_temp, rates_err_temp, nul_covar, det_par_val, fiss_const_val, 5)
                calc_mass_err(i) = mult_anal2(rates_temp, rates_err_temp, nul_covar, det_par_val, fiss_const_val, 6)
                rel_rate_err(i, k) = 0
                If calc_mass(i) <> 0 Then rel_rate_err(i, k) = 100 * calc_mass_err(i) / calc_mass(i)
            Next i
        Next k

        Dim npt

        '    ****** PLOT RESPONSE     ******

        Dim mass1, ymin, ymax, xmin, xmax

        ymin = 0
        ymax = 0
        For k = 1 To 3
            For i = 1 To 1000
                If rel_rate_err(i, k) > ymax Then ymax = rel_rate_err(i, k)
            Next i
        Next k

        If 100 * m240_new_err / m240_new > ymax Then ymax = 100 * m240_new_err / m240_new

        ymin = ymax
        For k = 1 To 3
            For i = 1 To 1000
                If rel_rate_err(i, k) < ymin Then ymin = rel_rate_err(i, k)
            Next i
        Next k

        Dim series_label1, series_label2, series_label3 As String
        series_label1 = "Alpha=" & alpha1
        series_label2 = "Alpha=" & alpha2
        series_label3 = "Alpha=" & alpha3

        With Chart1.ChartAreas(0)

            xmin = 0
            xmax = 2000
            .AxisX.Minimum = xmin
            .AxisX.Interval = (xmax - xmin) / 10
            .AxisX.Maximum = xmax

            ' .AxisY.IsLogarithmic = True
            .AxisY.Minimum = 0  '  ymin
            .AxisY.Interval = Int(ymax + 1) / 10
            .AxisY.Maximum = Int(ymax + 1)

            .AxisX.Title = "Mass (g Pu)"
            .AxisY.Title = "Relative Error (%)"

        End With

        Chart1.Series.Clear()
        Chart1.Series.Add(series_label1)
        Chart1.Series.Add(series_label2)
        Chart1.Series.Add(series_label3)
        Chart1.Series.Add("This Assay")

        Me.Chart1.Series(series_label1).Points.Clear()
        Me.Chart1.Series(series_label2).Points.Clear()
        Me.Chart1.Series(series_label3).Points.Clear()
        Me.Chart1.Series("This Assay").Points.Clear()

        '   Chart1.Titles.Add("Estimated Measurement Precision")

        With Chart1.Series(0)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            '  .BorderWidth = 2
            '    .Color = Color.Blue
            '  .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            '  .MarkerSize = 8
            '.IsVisibleInLegend = False
            For i = 1 To 1000
                mass1 = 2 * i
                Me.Chart1.Series(series_label1).Points.AddXY(mass1, rel_rate_err(i, 1))
            Next
        End With

        With Chart1.Series(1)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            '  .BorderWidth = 2
            '    .Color = Color.Blue
            '  .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            '  .MarkerSize = 8
            '.IsVisibleInLegend = False
            For i = 1 To 1000
                mass1 = 2 * i
                Me.Chart1.Series(series_label2).Points.AddXY(mass1, rel_rate_err(i, 2))
            Next
        End With
        With Chart1.Series(2)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            '  .BorderWidth = 2
            '    .Color = Color.Blue
            '  .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            '  .MarkerSize = 8
            '.IsVisibleInLegend = False
            For i = 1 To 1000
                mass1 = 2 * i

                Me.Chart1.Series(series_label3).Points.AddXY(mass1, rel_rate_err(i, 3))
            Next
        End With
        With Chart1.Series(3)
            .ChartType = DataVisualization.Charting.SeriesChartType.Point
            .BorderWidth = 2
            .Color = Color.Black
            .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            .MarkerSize = 6
            .IsVisibleInLegend = True
            mass1 = Final_mass_new

            Me.Chart1.Series("This Assay").Points.AddXY(mass1, 100 * m240_new_err / m240_new)

        End With

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        alpha1 = Alpha_plot_Box1.Text
        alpha2 = Alpha_plot_Box2.Text
        alpha3 = Alpha_plot_Box3.Text

        Call update_form_14()

    End Sub

    Function mult_est(mass)
        ' provides an estimate of multiplication as a function of mass for precision estimate
        '  multi = 1 + 0.075 * (mass / 722) ^ 1.5
        mult_est = 1 + mult_par(1) * (mass / mult_par(2)) ^ mult_par(3)
        mult_est = mult_est + mult_par(4) + mult_par(5) * mass + mult_par(6) * mass ^ 2 + mult_par(7) * mass ^ 3
    End Function

    Private Sub Chart1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Chart1_Click_1(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub
End Class