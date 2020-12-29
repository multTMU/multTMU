Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Imports Microsoft.VisualBasic.powerpacks.printing
Public Class Form1
    ' Dim Form2 As New Form
    Dim myForm1 As Form1
    Dim myForm2 As Fission_Data_Form    ' fission data entry
    Dim myForm3 As Form3        ' Detector Parameters
    Dim myForm4 As Form4        ' Isotopic Nuclear data entry
    Dim myForm5 As Form5        ' Empirical Bias Parameter Entry Form
    Dim myForm6 As Form6        ' Item Info Entry
    Dim myForm7 As Form7        ' Detector Physical Parameter Entry
    Dim myForm8 As Form8        ' Misc Analysis Parameters
    Dim myForm9 As Form9        ' Radial Contribution Detail
    Dim myForm10 As Form10      ' Vertical Contribution Detail
    Dim myForm11 As Form11      ' Detector Characterization Contribution Detail
    Dim myForm12 As Form12      ' pu_240 mass effecitve conversion detail
    Dim myForm13 As Form13      ' Fission momenet contribution detail
    Dim myForm14 As Form14      ' Measurement precision detail
    Dim myForm15 As Form15      ' Empirical Bias details


    Private Sub FissionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FissionToolStripMenuItem.Click
        ' Launch Form 2
        If myForm2 Is Nothing Then
            myForm2 = New Fission_Data_Form
        End If
        myForm2.Show()
        myForm2 = Nothing

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Lanuch Form 1
        use_file_det_param_box.Checked = True
        use_file_isotopics_box.Checked = True
        use_INCC_deadtime_box.Checked = True
        file_sing_rate_Box.Text = file_pass_sing_rate

    End Sub

    Private Sub BasicParametersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BasicParametersToolStripMenuItem.Click
        'Lanuch Form 3
        If myForm3 Is Nothing Then
            myForm3 = New Form3
        End If
        myForm3.Show()
        myForm3 = Nothing
    End Sub

    Private Sub AlphaNToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlphaNToolStripMenuItem.Click
        ' Launch Form 4
        If myForm4 Is Nothing Then
            myForm4 = New Form4
        End If
        myForm4.Show()
        myForm4 = Nothing
    End Sub


    Private Sub OpenAssayFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenAssayFileToolStripMenuItem.Click
        ' Updates Form 1 following TMU calculation

        If use_file_det_param_box.Checked = True Then use_file_det_flag = True Else use_file_det_flag = False
        If use_file_isotopics_box.Checked = True Then use_file_iso_flag = True Else use_file_iso_flag = False
        If use_INCC_deadtime_box.Checked = True Then use_LANL_DT_flag = True Else use_LANL_DT_flag = False
        If use_file_det_flag Then Label100.Text = "Detector Component Unavailable" Else Label100.Text = "Detector Characterization:"

        Call main(0, use_file_det_flag)

        Call update_form_1()



    End Sub

    Private Sub DetectorParametersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetectorParametersToolStripMenuItem.Click

    End Sub

    Private Sub PhysicalParametersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PhysicalParametersToolStripMenuItem.Click
        ' Launch Form 7
        If myForm7 Is Nothing Then
            myForm7 = New Form7
        End If
        myForm7.Show()
        myForm7 = Nothing
    End Sub

    Private Sub ItemIsotopicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemIsotopicsToolStripMenuItem.Click
        ' Launch Form 6
        If myForm6 Is Nothing Then
            myForm6 = New Form6
        End If
        myForm6.Show()
        myForm6 = Nothing
    End Sub

    Private Sub file_sing_rate_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles file_sing_rate_Box.MaskInputRejected

    End Sub

    Private Sub sum_sing_rate_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub sum_doub_rate_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub sum_trip_rate_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub sum_trip_err_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub sum_doub_err_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub sum_sing_err_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MaskedTextBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub MaskedTextBox2_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' change isotopic composition
        '  Call main(1, use_file_det_flag)

        'Call update_form_1()
    End Sub

    Private Sub Label102_Click(sender As Object, e As EventArgs) Handles Label102.Click

    End Sub

    Private Sub Label83_Click(sender As Object, e As EventArgs) Handles Label83.Click

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        If use_file_det_param_box.Checked = True Then use_file_det_flag = True Else use_file_det_flag = False
        If use_file_isotopics_box.Checked = True Then use_file_iso_flag = True Else use_file_iso_flag = False
        If use_INCC_deadtime_box.Checked = True Then use_LANL_DT_flag = True Else use_LANL_DT_flag = False
        If use_file_det_flag Then Label100.Text = "Detector Component Unavailable" Else Label100.Text = "Detector Characterization:"
        Call main(1, use_file_det_flag)

        Call update_form_1()

    End Sub


    Sub update_form_1()

        sample_id_box.Text = file_item_id
        data_file_id_box.Text = data_file_id

        file_sing_rate_Box.Text = Int(1000 * file_avg_singles) / 1000
        file_doub_rate_Box.Text = Int(1000 * file_avg_doubles) / 1000
        file_trip_rate_Box.Text = Int(1000 * file_avg_triples) / 1000
        file_sing_err_Box.Text = Int(1000 * file_stdev_singles) / 1000
        file_doub_err_Box.Text = Int(1000 * file_stdev_doubles) / 1000
        file_trip_err_Box.Text = Int(1000 * file_stdev_triples) / 1000

        ring_ratio_box.Text = Int(1000 * ring_ratio) / 1000
        ring_ratio_err_box.Text = Int(1000 * ring_ratio_err) / 1000

        file_avg_covar_11_Box.Text = Int(10000 * file_stdev_singles ^ 2) / 10000
        file_avg_covar_12_Box.Text = Int(10000 * file_co_variance_sd) / 10000
        file_avg_covar_13_Box.Text = Int(10000 * file_co_variance_st) / 10000
        file_avg_covar_21_Box.Text = Int(10000 * file_co_variance_sd) / 10000
        file_avg_covar_22_Box.Text = Int(10000 * file_stdev_doubles ^ 2) / 10000
        file_avg_covar_23_Box.Text = Int(10000 * file_co_variance_dt) / 10000
        file_avg_covar_31_Box.Text = Int(10000 * file_co_variance_st) / 10000
        file_avg_covar_32_Box.Text = Int(10000 * file_co_variance_dt) / 10000
        file_avg_covar_33_Box.Text = Int(10000 * file_stdev_triples ^ 2) / 10000

        ' average rates calculated from the revised analysis individual histograms 
        avg_sing_rate_Box.Text = Int(1000 * new_rates(1)) / 1000
        avg_doub_rate_Box.Text = Int(1000 * new_rates(2)) / 1000
        avg_trip_rate_Box.Text = Int(1000 * new_rates(3)) / 1000
        avg_sing_err_Box.Text = Int(1000 * new_rates_err(1)) / 1000
        avg_doub_err_Box.Text = Int(1000 * new_rates_err(2)) / 1000
        avg_trip_err_Box.Text = Int(1000 * new_rates_err(3)) / 1000

        'covariance matrix for the average rates calculated from the revised analysis individual histograms 
        avg_covar_11_Box.Text = Int(10000 * stdev_singles ^ 2) / 10000
        avg_covar_12_Box.Text = Int(10000 * co_variance_sd) / 10000
        avg_covar_13_Box.Text = Int(10000 * co_variance_st) / 10000
        avg_covar_21_Box.Text = Int(10000 * co_variance_sd) / 10000
        avg_covar_22_Box.Text = Int(10000 * stdev_doubles ^ 2) / 10000
        avg_covar_23_Box.Text = Int(10000 * co_variance_dt) / 10000
        avg_covar_31_Box.Text = Int(10000 * co_variance_st) / 10000
        avg_covar_32_Box.Text = Int(10000 * co_variance_dt) / 10000
        avg_covar_33_Box.Text = Int(10000 * stdev_triples ^ 2) / 10000

        '   display rates based on summed multiplicity histograms from RTS file
        sum_sing_rate_Box.Text = Int(1000 * (file_rates_out(1) - file_pass_sing_bkg)) / 1000
        sum_doub_rate_Box.Text = Int(1000 * (file_rates_out(3) - file_pass_doub_bkg)) / 1000
        sum_trip_rate_Box.Text = Int(1000 * (file_rates_out(5) - file_pass_trip_bkg)) / 1000
        sum_sing_err_Box.Text = Int(1000 * (file_rates_out(2) ^ 2 + file_pass_sing_bkg_err ^ 2) ^ 0.5) / 1000
        sum_doub_err_Box.Text = Int(1000 * (file_rates_out(4) ^ 2 + file_pass_doub_bkg_err ^ 2) ^ 0.5) / 1000
        sum_trip_err_Box.Text = Int(1000 * (file_rates_out(6) ^ 2 + file_pass_doub_bkg_err ^ 2) ^ 0.5) / 1000

        '   display rates based on revised summed multiplicity histograms
        New_sum_sing_Box.Text = Int(1000 * new_rates_out(1) - file_pass_sing_bkg) / 1000
        New_sum_doub_Box.Text = Int(1000 * new_rates_out(3) - file_pass_doub_bkg) / 1000
        New_sum_trip_Box.Text = Int(1000 * new_rates_out(5) - file_pass_trip_bkg) / 1000
        New_sum_sing_err_Box.Text = Int(1000 * (new_rates_out(2) ^ 2 + file_pass_sing_bkg_err ^ 2) ^ 0.5) / 1000
        New_sum_doub_err_Box.Text = Int(1000 * (new_rates_out(4) ^ 2 + file_pass_doub_bkg_err ^ 2) ^ 0.5) / 1000
        New_sum_trip_err_Box.Text = Int(1000 * (new_rates_out(6) ^ 2 + file_pass_trip_bkg_err ^ 2) ^ 0.5) / 1000

        '     display dithered uncertainty values
        jitter_sing_err_sum_new_box.Text = Int(1000 * jitter_rates_covar(1, 1) ^ 0.5) / 1000
        jitter_doub_err_sum_new_box.Text = Int(1000 * jitter_rates_covar(2, 2) ^ 0.5) / 1000
        jitter_trip_err_sum_new_box.Text = Int(1000 * jitter_rates_covar(3, 3) ^ 0.5) / 1000
        jitter_covar_sum_new_1_1_box.Text = Int(1000 * jitter_rates_covar(1, 1)) / 1000
        jitter_covar_sum_new_1_2_box.Text = Int(1000 * jitter_rates_covar(1, 2)) / 1000
        jitter_covar_sum_new_1_3_box.Text = Int(1000 * jitter_rates_covar(1, 3)) / 1000
        jitter_covar_sum_new_2_1_box.Text = Int(1000 * jitter_rates_covar(2, 1)) / 1000
        jitter_covar_sum_new_2_2_box.Text = Int(1000 * jitter_rates_covar(2, 2)) / 1000
        jitter_covar_sum_new_2_3_box.Text = Int(1000 * jitter_rates_covar(2, 3)) / 1000
        jitter_covar_sum_new_3_1_box.Text = Int(1000 * jitter_rates_covar(3, 1)) / 1000
        jitter_covar_sum_new_3_2_box.Text = Int(1000 * jitter_rates_covar(3, 2)) / 1000
        jitter_covar_sum_new_3_3_box.Text = Int(1000 * jitter_rates_covar(3, 3)) / 1000
        jitter_iterations_box.Text = n_jitter

        Dim d_s_ratio, d_s_err, d_s_err_wcovar, dith_d_s_err, dith_d_s_err_wcovar
        d_s_ratio = new_rates_out(3) / new_rates_out(1)
        d_s_err = ((new_rates_out(4) / new_rates_out(1)) ^ 2 + (new_rates_out(3) / new_rates_out(1) ^ 2 * new_rates_out(2)) ^ 2)
        d_s_err_wcovar = (d_s_err + 2 * co_variance_sd / new_rates_out(1) * new_rates_out(3) / new_rates_out(1) ^ 2) ^ 0.5
        d_s_err = (d_s_err) ^ 0.5

        dith_d_s_err = ((jitter_rates_covar(2, 2) ^ 0.5 / new_rates_out(1)) ^ 2 + (new_rates_out(3) / new_rates_out(1) ^ 2 * jitter_rates_covar(1, 1) ^ 0.5) ^ 2)
        dith_d_s_err_wcovar = (dith_d_s_err + 2 * jitter_rates_covar(1, 2) / new_rates_out(1) * new_rates_out(3) / new_rates_out(1) ^ 2) ^ 0.5
        dith_d_s_err = (dith_d_s_err) ^ 0.5

        file_good_cycles_Box.Text = file_net_cycles & " / " & max_cycles
        File_assay_time_Box.Text = file_total_count_time
        analysis_max_cycles_box.Text = net_cycles & " / " & max_cycles
        analysis_total_assay_time_Box.Text = net_cycles * cycle_time
        n_sigma_Box.Text = qc_sigma
        ' jitter_iterations_box.Text = net_cycles & " / " & max_cycles
        '    MaskedTextBox6.Text = net_cycles * cycle_time


        If use_file_iso_flag = True Then Item_source_box.Text = "from INCC file" Else Item_source_box.Text = "From Item File:"
        If use_file_iso_flag = True Then Isotopic_filename_box.Text = data_file_id Else Isotopic_filename_box.Text = item_file_name_read


        '   MaskedTextBox40.Text = iso_choice

        ' display initial plutonium isotopic values
        Pu238_initBox.Text = iso_val(1)
        Pu239_initBox.Text = iso_val(2)
        Pu240_initBox.Text = iso_val(3)
        Pu241_initBox.Text = iso_val(4)
        Pu242_initBox.Text = iso_val(5)
        Pu244_initBox.Text = iso_val(6)
        Am241_initBox.Text = iso_val(7)
        Cf252_initBox.Text = iso_val(8)

        Pu238_init_errBox.Text = iso_val_err(1)
        Pu239_init_errBox.Text = iso_val_err(2)
        Pu240_init_errBox.Text = iso_val_err(3)
        Pu241_init_errBox.Text = iso_val_err(4)
        Pu242_init_errBox.Text = iso_val_err(5)
        Pu244_init_errBox.Text = iso_val_err(6)
        Am241_init_errBox.Text = iso_val_err(7)
        Cf252_init_errBox.Text = iso_val_err(8)

        pu_declare_date_Box.Text = iso_date(1)
        Am_declare_date_Box.Text = iso_date(2)
        cf_declare_date_Box.Text = iso_date(3)

        Assay_date_box.Text = assay_date

        Pu238_corrBox.Text = Int(100000 * new_iso(1)) / 100000
        Pu239_corrBox.Text = Int(100000 * new_iso(2)) / 100000
        Pu240_corrBox.Text = Int(100000 * new_iso(3)) / 100000
        Pu241_corrBox.Text = Int(100000 * new_iso(4)) / 100000
        Pu242_corrBox.Text = Int(100000 * new_iso(5)) / 100000
        Pu244_corrBox.Text = Int(100000 * new_iso(6)) / 100000
        Am241_corrBox.Text = Int(100000 * new_iso(7)) / 100000
        Cf252_corrBox.Text = new_iso(8)

        Pu238_corr_errBox.Text = Int(100000 * new_iso_err(1)) / 100000
        Pu239_corr_errBox.Text = Int(100000 * new_iso_err(2)) / 100000
        Pu240_corr_errBox.Text = Int(100000 * new_iso_err(3)) / 100000
        Pu241_corr_errBox.Text = Int(100000 * new_iso_err(4)) / 100000
        Pu242_corr_errBox.Text = Int(100000 * new_iso_err(5)) / 100000
        Pu244_corr_errBox.Text = Int(100000 * new_iso_err(6)) / 100000
        Am241_corr_errBox.Text = Int(100000 * new_iso_err(7)) / 100000
        Cf252_corr_errBox.Text = Int(100000 * new_iso_err(8)) / 100000

        Pu_240_eff_gBox.Text = Int(100000 * pu_240_effective) / 100000
        Pu_240_eff_g_errBox.Text = Int(100000 * pu_240_effective_err) / 100000

        alpha_value_expBox.Text = Int(100000 * alpha_val_cal) / 100000
        alpha_value_exp_errBox.Text = Int(100000 * alpha_val_cal_err) / 100000


        Material_type_box.Text = geo_par(10)
        adjusted_alpha_Box.Text = Int(100000 * adjusted_alpha) / 100000
        adjusted_alpha_err_Box.Text = Int(100000 * adjusted_alpha_err) / 100000

        ' Display revised assay results
        ' naming convention:  parameters with the "new" are based on the revised analysis of the individual histogram data
        '

        Dim m240_out, m240_err_out, m240_new_nocovar_err_out
        Dim final_mass_new_out, final_mass_err_new_out, final_mass_nocovar_new_file_out
        '
        If iso_val(8) = 0 Then m240_out = m240_new Else m240_out = 1000000000.0 * m240_new                  ' display  Pu values in g and Cf values in ng
        If iso_val(8) = 0 Then m240_err_out = m240_new_err Else m240_err_out = 1000000000.0 * m240_new_err
        If iso_val(8) = 0 Then m240_new_nocovar_err_out = m240_new_nocovar_err Else m240_new_nocovar_err_out = 1000000000.0 * m240_new_nocovar_err
        If iso_val(8) = 0 Then final_mass_new_out = Final_mass_new Else final_mass_new_out = 1000000000.0 * Final_mass_new
        If iso_val(8) = 0 Then final_mass_err_new_out = final_mass_err_new Else final_mass_err_new_out = 1000000000.0 * final_mass_err_new
        If iso_val(8) = 0 Then final_mass_nocovar_new_file_out = final_mass_nocovar_err_new Else final_mass_nocovar_new_file_out = 1000000000.0 * final_mass_nocovar_err_new

        Dim final_mass_nocovar_new_out_trad, final_mass_err_new_out_trad
        If iso_val(8) = 0 Then final_mass_err_new_out_trad = final_mass_err_new_trad Else final_mass_err_new_out_trad = 1000000000.0 * final_mass_err_new_trad
        If iso_val(8) = 0 Then final_mass_nocovar_new_out_trad = final_mass_nocovar_err_new_trad Else final_mass_nocovar_new_file_out = 1000000000.0 * final_mass_nocovar_err_new_trad


        mult_new_Box.Text = Int(10000 * mult_new) / 10000
        mult_new_err_Box.Text = Int(10000 * mult_new_err) / 10000
        alpha_new_Box.Text = Int(1000 * alpha_new) / 1000
        alpha_new_err_Box.Text = Int(1000 * alpha_new_err) / 1000
        m240_new_Box.Text = Int(1000 * m240_out) / 1000
        m240_new_err_Box.Text = Int(1000 * m240_err_out) / 1000
        final_mass_new_Box.Text = Int(1000 * final_mass_new_out) / 1000
        Final_mass_err_new_Box.Text = Int(1000 * final_mass_err_new_out_trad) / 1000
        Final_mass_err_nocovar_new_Box.Text = Int(1000 * final_mass_nocovar_err_new_trad) / 1000

        mult_new_nocovar_err_Box.Text = Int(10000 * mult_new_nocovar_err) / 10000
        alpha_new_nocovar_err_Box.Text = Int(1000 * alpha_new_nocovar_err) / 1000
        m240_new_nocovar_err_Box.Text = Int(1000 * m240_new_nocovar_err_out) / 1000

        ' Display assay results from INCC file rates

        Dim m240_file_out, m240_file_err_out, m240_file_nocovar_err_out
        Dim final_mass_file_out, final_mass_err_file_out, final_mass_nocovar_err_file_out
        If iso_val(8) = 0 Then m240_file_out = m240_file Else m240_file_out = 1000000000.0 * m240_file                  ' display  Pu values in g and Cf values in ng
        If iso_val(8) = 0 Then m240_file_err_out = m240_file_err Else m240_file_err_out = 1000000000.0 * m240_file_err
        If iso_val(8) = 0 Then m240_file_nocovar_err_out = m240_file_nocovar_err Else m240_file_nocovar_err_out = 1000000000.0 * m240_file_nocovar_err
        If iso_val(8) = 0 Then final_mass_file_out = Final_mass_file Else final_mass_file_out = 1000000000.0 * Final_mass_file
        If iso_val(8) = 0 Then final_mass_err_file_out = final_mass_err_file Else final_mass_err_file_out = 1000000000.0 * final_mass_err_file
        If iso_val(8) = 0 Then final_mass_nocovar_err_file_out = final_mass_nocovar_err_file Else final_mass_nocovar_err_file_out = 1000000000.0 * final_mass_nocovar_err_file

        Dim final_mass_nocovar_err_file_out_trad, final_mass_err_file_out_trad
        If iso_val(8) = 0 Then final_mass_err_file_out_trad = final_mass_err_file_trad Else final_mass_err_file_out_trad = 1000000000.0 * final_mass_err_file_trad
        If iso_val(8) = 0 Then final_mass_nocovar_err_file_out_trad = final_mass_nocovar_err_file_trad Else final_mass_nocovar_err_file_out_trad = 1000000000.0 * final_mass_nocovar_err_file_trad


        mult_file_Box.Text = Int(10000 * mult_file) / 10000
        mult_file_err_Box.Text = Int(10000 * mult_file_err) / 10000
        alpha_file_Box.Text = Int(1000 * alpha_file) / 1000
        alpha_file_err_Box.Text = Int(1000 * alpha_file_err) / 1000
        m240_file_Box.Text = Int(1000 * m240_file_out) / 1000
        m240_file_err_Box.Text = Int(1000 * m240_file_err_out) / 1000
        mult_file_nocovar_err_Box.Text = Int(10000 * mult_file_nocovar_err) / 10000
        alpha_file_nocovar_err_Box.Text = Int(1000 * alpha_file_nocovar_err) / 1000
        m240_file_nocovar_err_Box.Text = Int(1000 * m240_file_nocovar_err_out) / 1000
        final_mass_file_Box.Text = Int(1000 * Final_mass_file) / 1000
        Final_mass_err_file_Box.Text = Int(1000 * final_mass_err_file_out_trad) / 1000
        Final_mass_err_nocovar_file_Box.Text = Int(1000 * final_mass_nocovar_err_file_trad) / 1000

        If iso_val(8) = 0 Then effective_mass_out_label2.Text = "g Pu240 / g Pu" Else effective_mass_out_label2.Text = "g Cf252/ g Cf"
        If iso_val(8) = 0 Then assay_eff_mass_label1.Text = "Mass (g Pu240 eff)" Else assay_eff_mass_label1.Text = "Mass (ng 252Cf eff)"
        If iso_val(8) = 0 Then assay_tot_mass_label1.Text = "Mass (g Pu)" Else assay_tot_mass_label1.Text = "Mass (ng 252Cf)"

        ' ---------------------- Error Components  ----------------------------------------------------
        '
        ' ----------------- Error components are based on revised assayed results ---------------------
        '

        ' if individual historgrams are not present use the uncertainties from the INCC rates analysis

        Dim no_hist_flag As Boolean
        If stdev_singles > 0 Then no_hist_flag = False Else no_hist_flag = True
        If no_hist_flag Then m240_out = m240_file_out
        If no_hist_flag Then m240_err_out = m240_file_err_out
        If no_hist_flag Then final_mass_new_out = Final_mass_file

        '-----------------------------------------------------------------
        Counting_Stats_Pu240_Box.Text = Int(1000 * m240_err_out) / 1000
        Counting_Stats_Pu_tot_Box.Text = Int(1000 * (m240_err_out / m240_out) * final_mass_new_out) / 1000
        Counting_stats_percent_Box.Text = Int(100000 * m240_err_out / m240_out) / 1000 & "%"

        det_Pu240_contrib_Box.Text = Int(1000 * det_err_sum) / 1000
        det_Pu_tot_contrib_Box.Text = Int(1000 * det_err_sum / pu_240_effective) / 1000
        det_Pu240_percent_Box.Text = Int(100000 * det_err_sum / m240_out) / 1000 & "%"

        If use_file_det_flag Then det_Pu240_contrib_Box.Visible = "False" Else det_Pu240_contrib_Box.Visible = "True"
        If use_file_det_flag Then det_Pu_tot_contrib_Box.Visible = "False" Else det_Pu_tot_contrib_Box.Visible = "True"
        If use_file_det_flag Then det_Pu240_percent_Box.Visible = "False" Else det_Pu240_percent_Box.Visible = "True"

        Pu240conv_contrib_box.Text = "N/A"
        Pu240conv_Pu_tot_contrib_box.Text = Int(1000 * pu_240_effective_err * m240_out / pu_240_effective ^ 2) / 1000
        Pu240conv_percent_Box.Text = Int(100000 * pu_240_effective_err / pu_240_effective) / 1000 & "%"

        Fiss_mom_Pu240_contrib.Text = Int(1000 * fiss_mom_err_sum) / 1000
        Fiss_mom_Pu_tot_contrib.Text = Int(1000 * fiss_mom_err_sum / pu_240_effective) / 1000
        Fiss_mom_Pu_percent_Box.Text = Int(100000 * fiss_mom_err_sum / m240_out) / 1000 & "%"

        Radial_rand_m240_Box.Text = Int(1000 * Radial_Random_out * m240_out) / 1000
        Radial_rand_Pu_tot_Box.Text = Int(1000 * Radial_Random_out * m240_out / pu_240_effective) / 1000
        radial_rand_Pu_percent_Box.Text = Int(10000 * Radial_Random_out) / 100 & "%"

        '  Radial_rand_m240_Box.Text = Int(1000 * radial_random) / 1000
        '  Radial_rand_Pu_tot_Box.Text = Int(1000 * radial_random / pu_240_effective) / 1000
        '  radial_rand_Pu_percent_Box.Text = Int(100000 * radial_random / m240_out) / 1000 & "%"


        Radial_systematic_m240_Box.Text = Int(1000 * Radial_Bias_Out * m240_out) / 1000
        Radial_systematic_Pu_tot_Box.Text = Int(1000 * Radial_Bias_Out * m240_out / pu_240_effective) / 1000
        radial_systematic_Pu_percent_Box.Text = Int(10000 * Radial_Bias_Out) / 100 & "%"

        Dim axial_bias
        If pt_cal_flag Then axial_bias = pt_cal_bias Else axial_bias = distrib_cal_bias

        Fill_height_Pu240_Box.Text = Int(1000 * (axial_bias) * m240_out) / 1000
        Fill_height_Tot_Pu_Box.Text = Int(1000 * (axial_bias) * m240_out / pu_240_effective) / 1000
        Fill_height_Pu_percent_Box.Text = Int(100000 * axial_bias) / 1000 & "%"

        Fill_height_random_m240_Box.Text = Int(1000 * m240_out * st_dev_rnd_mass) / 1000                   '    (Abs(max_fill_mass - min_fill_mass) / avg_fill_mass / 6)
        Fill_height_random_putot_Box.Text = Int(1000 * m240_out * st_dev_rnd_mass / pu_240_effective) / 1000
        Fill_height_random_random_Box.Text = Int(100000 * st_dev_rnd_mass) / 1000 & "%"

        '  --------------------

        Density_Pu240_Box.Text = Int(1000 * m240_out * rel_rho_bias) / 1000
        Density_Pu_tot_Box.Text = Int(1000 * final_mass_new_out * rel_rho_bias) / 1000
        Density_Pu_percent_Box.Text = Int(100000 * rel_rho_bias) / 1000 & "%"

        density_rand_Pu240_box.Text = Int(1000 * m240_out * rel_rho_rand) / 1000
        density_rand_Pu_tot_box.Text = Int(1000 * final_mass_new_out * rel_rho_rand) / 1000
        Density_rand_Pu_percent_Box.Text = Int(100000 * rel_rho_rand) / 1000 & "%"

        '  --------------------

        Alpha_Pu240_Box.Text = Int(1000 * m240_out * rel_alpha_bias) / 1000
        Alpha_Pu_tot_Box.Text = Int(1000 * final_mass_new_out * rel_alpha_bias) / 1000
        Alpha_Pu_percent_Box.Text = Int(100000 * rel_alpha_bias) / 1000 & "%"

        Alpha_rand_Pu240_Box.Text = Int(1000 * m240_out * rel_alpha_random) / 1000
        Alpha_rand_Pu_tot_Box.Text = Int(1000 * final_mass_new_out * rel_alpha_random) / 1000
        Alpha_rand_Pu_percent_Box.Text = Int(100000 * rel_alpha_random) / 1000 & "%"

        '  --------------------

        burnup_Pu240_Box.Text = Int(1000 * m240_out * rel_burnup_bias) / 1000
        burnup_putot_box.Text = Int(1000 * final_mass_new_out * rel_burnup_bias) / 1000
        burnup_putot_percent_box.Text = Int(100000 * rel_burnup_bias) / 1000 & "%"

        '  --------------------

        wall_effect_bias_Pu240_Box.Text = Int(1000 * m240_out * rel_wall_effect_bias) / 1000
        wall_effect_bias_putot_box.Text = Int(1000 * final_mass_new_out * rel_wall_effect_bias) / 1000
        wall_effect_bias_putot_percent_box.Text = Int(100000 * rel_wall_effect_bias) / 1000 & "%"

        '  --------------------

        Chem_Pu240_Box.Text = Int(1000 * m240_out * rel_UPu_bias) / 1000
        Chem_Pu_tot_Box.Text = Int(1000 * final_mass_new_out * rel_UPu_bias) / 1000
        Chem_form_Pu_percent_Box.Text = Int(100000 * rel_UPu_bias) / 1000 & "%"

        Chem_rand_Pu240_Box.Text = Int(1000 * m240_out * rel_upu_random) / 1000
        Chem_rand_Pu_tot_Box.Text = Int(1000 * final_mass_new_out * rel_upu_random) / 1000
        Chem_form_rand_Pu_percent_Box.Text = Int(100000 * rel_upu_random) / 1000 & "%"

        '  --------------------

        Moderator_Pu240_Box.Text = Int(1000 * m240_out * rel_mod_bias) / 1000
        Moderator_Pu_tot_Box.Text = Int(1000 * final_mass_new_out * rel_mod_bias) / 1000
        Moderator_Pu_percent_Box.Text = Int(100000 * rel_mod_bias) / 1000 & "%"

        Moderator_rand_Pu240_Box.Text = Int(1000 * m240_out * rel_mod_random) / 1000
        Moderator_rand_Pu_tot_Box.Text = Int(1000 * final_mass_new_out * rel_mod_random) / 1000
        Moderator_rand_Pu_percent_Box.Text = Int(100000 * rel_mod_random) / 1000 & "%"
        '
        '  ------------------------------------------------------------------
        '  -------------------- final results  ------------------------------
        '  ------------------------------------------------------------------
        '
        Dim systematic_240_temp, rand_temp, systematic_tot_temp
        Dim analysis_date As String


        analysis_date = Today()

        sample_id_box2.Text = file_item_id
        data_file_id_box2.Text = data_file_id
        assay_date_date_box.text = file_assay_date
        analysis_date_box.Text = analysis_date
        ' ***************************************************
        ' ***** Apply Multiplcation Correction Factor *******
        ' ***************************************************
        Dim mult_cf_mass_err
        mult_cf_mass_err = 0
        mult_corr_fact_box.Visible = False
        mult_corr_fact_err_box.Visible = False
        mult_corr_fact_err_box.Visible = False
        RichTextBox46.Visible = False
        Label8.Text = ""

        If mult_corr_flag = False Then GoTo skip_mult_CF
        mult_corr_fact_box.Visible = True
        mult_corr_fact_err_box.Visible = True
        mult_corr_fact_err_box.Visible = True

        Label8.Text = "Multiplication Correction:"
        RichTextBox46.Visible = True
        mult_corr_fact = 1 / multiplication_corr_fact(mult_new, mult_new_err, 1)
        mult_corr_fact_err = multiplication_corr_fact(mult_new, mult_new_err, 2) / mult_corr_fact

        mult_corr_fact_box.Text = Int(10000 * mult_corr_fact) / 10000
        mult_corr_fact_err_box.Text = Int(10000 * mult_corr_fact_err) / 10000
        '
        ' adjust absolute errors for multiplication correction factor
        '
        m240_out = m240_out * mult_corr_fact
        Final_mass_new = Final_mass_new * mult_corr_fact
        m240_err_out = m240_err_out * mult_corr_fact
        st_dev_rnd_mass = st_dev_rnd_mass * mult_corr_fact
        det_err_sum = det_err_sum * mult_corr_fact
        fiss_mom_err_sum = fiss_mom_err_sum * mult_corr_fact
        '
        mult_cf_mass_err = m240_out * mult_corr_fact_err                                 '  error component from multiplication correction factor
        '
        ' ***************************************************
        '
skip_mult_CF:
        final_mult_Box.Text = Int(10000 * mult_new) / 10000
        final_mult_random_box.Text = Int(10000 * mult_new_err) / 10000
        Final_alpha_Box.Text = Int(1000 * alpha_new) / 1000
        final_alpha_random_box.Text = Int(1000 * alpha_new_err) / 1000
        m240_new_Box2.Text = Int(1000 * m240_out) / 1000
        mtot_box2.Text = Int(1000 * Final_mass_new) / 1000
        '
        rand_temp = (m240_err_out ^ 2 + st_dev_rnd_mass ^ 2 + (Radial_Random_out * m240_out) ^ 2 + (rel_mod_random * m240_out) ^ 2)
        rand_temp = rand_temp + (rel_alpha_random * m240_out) ^ 2 + (rel_upu_random * m240_out) ^ 2 + (rel_rho_rand * m240_out) ^ 2
        rand_temp = rand_temp ^ 0.5
        '  
        m240_tmu_random_box.Text = Int(1000 * rand_temp) / 1000
        mtot_tmu_random_box.Text = Int(1000 * rand_temp / pu_240_effective) / 1000

        systematic_240_temp = det_err_sum ^ 2 + fiss_mom_err_sum ^ 2 + (Radial_Bias_Out ^ 2 + rel_rho_bias ^ 2 + rel_alpha_bias ^ 2 + axial_bias ^ 2) * m240_out ^ 2
        systematic_240_temp = systematic_240_temp + (rel_burnup_bias ^ 2 + rel_UPu_bias ^ 2 + rel_mod_bias ^ 2 + rel_wall_effect_bias ^ 2) * m240_out ^ 2
        systematic_240_temp = systematic_240_temp + mult_cf_mass_err ^ 2
        systematic_tot_temp = (systematic_240_temp / pu_240_effective ^ 2 + (pu_240_effective_err * m240_out / pu_240_effective ^ 2) ^ 2) ^ 0.5
        systematic_240_temp = systematic_240_temp ^ 0.5

        m240_tmu_systematic_box.Text = Int(1000 * systematic_240_temp) / 1000
        mtot_tmu_systematic_box.Text = Int(1000 * systematic_tot_temp) / 1000

        m240_tmu_box.Text = Int(1000 * (systematic_240_temp ^ 2 + rand_temp ^ 2) ^ 0.5) / 1000
        mtot_tmu_box.Text = Int(1000 * (systematic_tot_temp ^ 2 + (rand_temp / pu_240_effective) ^ 2) ^ 0.5) / 1000

        declared_Pu240_m_Box.Text = file_delcared_pu240_m
        declared_Putot_m_Box.Text = file_declared_putot_m

        percent_tmu_random_box.Text = Int(10000 * (rand_temp) / m240_out) / 100 & "% "
        percent_tmu_systematic_box.Text = Int(10000 * (systematic_tot_temp) / Final_mass_new) / 100 & "% "
        percent_tmu_box.Text = Int(10000 * (systematic_tot_temp ^ 2 + (rand_temp / pu_240_effective) ^ 2) ^ 0.5 / Final_mass_new) / 100 & "%"

        relative_diff_box.Text = " diff = " & Int(10000 * (Final_mass_new - file_declared_putot_m) / file_declared_putot_m) / 100 & "%"
        mtot_diff_box.Text = Int(1000 * (Final_mass_new - file_declared_putot_m)) / 1000 & " g"

    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click


        Dim pf As New PrintForm


        PageSetupDialog1.Document = PrintDocument1
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        PrintDialog1.AllowSomePages = True

        If PrintDialog1.ShowDialog = DialogResult.OK Then
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings

            '  print page 1

            Size = New Size(1000, 900)
            AutoScrollPosition = New Point(1, 20)
            pf.Form = Me


            pf.PrinterSettings.Copies = 1
            pf.PrinterSettings.DefaultPageSettings.Margins = New Printing.Margins(60, 30, 90, 30)
            pf.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = Printing.PaperKind.Letter

            pf.PrintAction = Printing.PrintAction.PrintToPrinter
            'pf.Print()

            pf.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)
            ' pf.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.CompatibleModeFullWindow)
            ' pf.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.Scrollable)

            ' page 2
            Size = New Size(1000, 810)
            AutoScrollPosition = New Point(1, 875)
            pf.Form = Me
            pf.PrinterSettings.Copies = 1
            pf.PrinterSettings.DefaultPageSettings.Margins = New Printing.Margins(60, 30, 90, 30)
            pf.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = Printing.PaperKind.Letter
            pf.PrintAction = Printing.PrintAction.PrintToPrinter
            ' pf.Print()
            pf.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)

            ' page 3
            Size = New Size(1000, 830)
            AutoScrollPosition = New Point(1, 1625)
            pf.Form = Me
            pf.PrinterSettings.Copies = 1
            pf.PrinterSettings.DefaultPageSettings.Margins = New Printing.Margins(60, 30, 90, 30)
            pf.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = Printing.PaperKind.Letter
            pf.PrintAction = Printing.PrintAction.PrintToPrinter
            'pf.Print()
            pf.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)

            ' page 4
            Size = New Size(1000, 500)
            AutoScrollPosition = New Point(1, 2500)
            pf.Form = Me
            pf.PrinterSettings.Copies = 1
            pf.PrinterSettings.DefaultPageSettings.Margins = New Printing.Margins(60, 30, 90, 30)
            pf.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = Printing.PaperKind.Letter
            pf.PrintAction = Printing.PrintAction.PrintToPrinter
            'pf.Print()
            pf.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)

        End If

    End Sub

    Private Sub OnPrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)


        'create a memory bitmap and size to the form
        Using bmp As Bitmap = New Bitmap(Me.Width, Me.Height)


            'draw the form on the memory bitmap
            Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))


            'draw the form image on the printer graphics sized and centered to margins
            Dim ratio As Single = CSng(bmp.Width / bmp.Height)


            If ratio > e.MarginBounds.Width / e.MarginBounds.Height Then


                e.Graphics.DrawImage(bmp,
                e.MarginBounds.Left,
                CInt(e.MarginBounds.Top + (e.MarginBounds.Height / 2) - ((e.MarginBounds.Width / ratio) / 2)),
                e.MarginBounds.Width,
                CInt(e.MarginBounds.Width / ratio))


            Else


                e.Graphics.DrawImage(bmp,
                CInt(e.MarginBounds.Left + (e.MarginBounds.Width / 2) - (e.MarginBounds.Height * ratio / 2)),
                e.MarginBounds.Top,
                CInt(e.MarginBounds.Height * ratio),
                e.MarginBounds.Height)


            End If


        End Using

    End Sub

    Private Sub use_file_isotopics_box_CheckedChanged(sender As Object, e As EventArgs) Handles use_file_isotopics_box.CheckedChanged
        If use_file_isotopics_box.Checked = True Then use_file_iso_flag = True
    End Sub

    Private Sub use_file_det_param_box_CheckedChanged(sender As Object, e As EventArgs) Handles use_file_det_param_box.CheckedChanged
        If use_file_det_param_box.Checked = True Then use_file_det_flag = True

    End Sub
    Private Sub use_INCC_deadtime_box_CheckedChanged(sender As Object, e As EventArgs) Handles use_INCC_deadtime_box.CheckedChanged
        If use_INCC_deadtime_box.Checked = True Then use_LANL_DT_flag = True

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim list_det_par As String
        ' Lists detector parameters used in analysis
        ' message box from info button

        list_det_par = "Detector Parameters Used in Analysis" & vbCrLf & vbCrLf

        list_det_par = list_det_par & "Efficiency = " & det_par_val(1) & vbCrLf
        list_det_par = list_det_par & "Die away time = " & det_par_val(2) & vbCrLf
        list_det_par = list_det_par & "Gate length = " & det_par_val(3) & vbCrLf
        list_det_par = list_det_par & "Doubles gate fraction = " & det_par_val(4) & vbCrLf
        list_det_par = list_det_par & "Triples gate fraction = " & det_par_val(5) & vbCrLf
        list_det_par = list_det_par & "Coefficient A deadtime = " & det_par_val(6) & vbCrLf
        list_det_par = list_det_par & "Coefficient B deadtime = " & det_par_val(7) & vbCrLf
        list_det_par = list_det_par & "Coefficient C deadtime = " & det_par_val(8) & vbCrLf
        list_det_par = list_det_par & "Coefficient D deadtime = " & det_par_val(9) & vbCrLf
        list_det_par = list_det_par & "Multiplicity deadtime = " & det_par_val(10) & vbCrLf & vbCrLf
        If use_file_det_flag = True Then list_det_par = list_det_par & "No Error Terms Found" & vbCrLf

        MessageBox.Show(list_det_par, "Detector Parameters")

    End Sub

    Private Sub Label100_Click(sender As Object, e As EventArgs) Handles Label100.Click

    End Sub

    Private Sub MaskedTextBox1_MaskInputRejected_1(sender As Object, e As MaskInputRejectedEventArgs) Handles declared_Pu240_m_Box.MaskInputRejected

    End Sub

    Private Sub MaskedTextBox2_MaskInputRejected_1(sender As Object, e As MaskInputRejectedEventArgs) Handles declared_Putot_m_Box.MaskInputRejected

    End Sub

    Private Sub AdditionalParametersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdditionalParametersToolStripMenuItem.Click
        If myForm5 Is Nothing Then
            myForm5 = New Form5
        End If
        myForm5.Show()
        myForm5 = Nothing
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        If myForm8 Is Nothing Then
            myForm8 = New Form8
        End If
        myForm8.Show()
        myForm8 = Nothing
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If myForm9 Is Nothing Then
            myForm9 = New Form9
        End If
        myForm9.Show()
        myForm9 = Nothing
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If myForm10 Is Nothing Then
            myForm10 = New Form10
        End If
        myForm10.Show()
        myForm10 = Nothing
    End Sub

    Private Sub det_cont_details_Button1_Click(sender As Object, e As EventArgs) Handles det_cont_details_Button1.Click
        If myForm11 Is Nothing Then
            myForm11 = New Form11
        End If
        myForm11.Show()
        myForm11 = Nothing
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If myForm12 Is Nothing Then
            myForm12 = New Form12
        End If
        myForm12.Show()
        myForm12 = Nothing
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If myForm13 Is Nothing Then
            myForm13 = New Form13
        End If
        myForm13.Show()
        myForm13 = Nothing
    End Sub

    Private Sub Panel8_Paint(sender As Object, e As PaintEventArgs) Handles Panel8.Paint

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If myForm14 Is Nothing Then
            myForm14 = New Form14
        End If
        myForm14.Show()
        myForm14 = Nothing
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

    End Sub

    Private Sub TopicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TopicsToolStripMenuItem.Click
        Dim a111 As String
        a111 = "Wishful Thinking"
        MsgBox(a111)

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim a111 As String

        a111 = a111 & "Multiplicity TMU Estimator, Ver. 1.0" & vbCrLf
        a111 = a111 & "December 21, 2020" & vbCrLf
        a111 = a111 & "" & vbCrLf
        a111 = a111 & "Questions?, contact" & vbCrLf
        a111 = a111 & "" & vbCrLf
        a111 = a111 & "Robert D. McElroy, Jr." & vbCrLf
        a111 = a111 & "Oak Ridge National Laboratory" & vbCrLf
        a111 = a111 & "One Bethel Valley Road" & vbCrLf
        a111 = a111 & "P.O. Box 2008, MS 6166" & vbCrLf
        a111 = a111 & "Oak Ridge, TN 37831-6166" & vbCrLf
        a111 = a111 & "" & vbCrLf
        a111 = a111 & "mcelroyrd@ornl.gov" & "" & vbCrLf
        a111 = a111 & "" & vbCrLf
        a111 = a111 & "Note: This software is intended only as a tool to aide in the  " & vbCrLf
        a111 = a111 & "           evaluation of the Total Measuement Uncertainty in  " & vbCrLf
        a111 = a111 & "           Thermal Neutron Multiplicity Analysis of Pu-oxide and MOX" & vbCrLf
        a111 = a111 & "           materials typically encountered in safeguards applications.  " & vbCrLf
        a111 = a111 & "           Estimated errors presented are only as good as the input data " & vbCrLf
        a111 = a111 & "           and underlying assumptions upon which those estimates are based." & vbCrLf
        a111 = a111 & "           Uncertainty estimates have been primarily based upon simulated " & vbCrLf
        a111 = a111 & "           and measured data for the PSMC and ENMC counting systems. " & vbCrLf
        MsgBox(a111)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If myForm15 Is Nothing Then
            myForm15 = New Form15
        End If
        myForm15.Show()
        myForm15 = Nothing
    End Sub

    Private Sub mult_file_err_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles mult_file_err_Box.MaskInputRejected

    End Sub

    Private Sub alpha_file_err_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles alpha_file_err_Box.MaskInputRejected

    End Sub

    Private Sub m240_file_err_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles m240_file_err_Box.MaskInputRejected

    End Sub

    Private Sub Final_mass_err_file_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles Final_mass_err_file_Box.MaskInputRejected

    End Sub

    Private Sub Label93_Click(sender As Object, e As EventArgs) Handles Label93.Click

    End Sub


End Class
