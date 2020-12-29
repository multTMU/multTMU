Imports System.IO
Imports System.Text
Imports System.Math
Public Class Form15
    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim file_location, det_param_file, fiss_param_file, iso_param_file, det_dim_file As String
        file_location = "c:\multiplicity_TMU\detector_parameters\"
        det_param_file = file_location & "det_parameters.csv"
        Call get_det_par_file(det_param_file)                           ' read detector parameter file

        Dim item_param_file As String
        file_location = "c:\multiplicity_TMU\item_info\"
        item_param_file = file_location & "current_item.csv"
        Call get_item_info_file(item_param_file)                        ' read in item specific information from csv file

        file_location = "c:\multiplicity_TMU\fission_parameters\"
        fiss_param_file = file_location & "current_fisison_parameters.csv"
        Call get_fiss_const_file(fiss_param_file)                       ' read nuclear data constants file

        file_location = "c:\multiplicity_TMU\nuclide_data\"
        iso_param_file = file_location & "Nuclide_decay_data.csv"
        Call get_iso_par_file(iso_param_file)                           ' read isotopics conversion constants file

        file_location = "c:\multiplicity_TMU\detector_dimensions\"
        det_dim_file = file_location & "current_det_dimensions.csv"
        Call get_det_dimensions_file(det_dim_file)                       ' read detector geometry file


        file_location = "c:\multiplicity_TMU\misc_parameters\"
        empirical_par_file = file_location & "tmu_empirical_parameters.csv"
        Call get_empirical_par_file(empirical_par_file)                     ' read empirical TMU bias parameter file


        ''******************************************************************************************************
        '
        '                        Burn-Up Bias 
        '
        '       Estimate of bias introduced due to difference in plutonium isotopics betweend and calibration assays 
        '
        ''******************************************************************************************************
        Dim delta_burnup

        dev_240_par_box1.Text = dev_pu240_par(1)
        dev_240_par_box2.Text = dev_pu240_par(2)

        cal_240eff_box.Text = Int(100000 * det_cal_pu240_eff) / 1000 & "%"

        Pu_240_eff_g_Box.Text = Int(100000 * pu_240_effective) / 1000 & "%"
        '  Pu_240_eff_g_err_Box.Text = Int(100000 * pu_240_effective_err) / 100000

        delta_burnup = (pu_240_effective - det_cal_pu240_eff)
        rel_burnup_bias = dev_pu240_par(1) + dev_pu240_par(2) * delta_burnup * alpha_new

        burnup_bias_box.Text = Int(100000 * rel_burnup_bias) / 1000 & "%"

        ''******************************************************************************************************
        '
        '                        UPu Bias
        '
        ''******************************************************************************************************
        Dim delta_upu, min_upu_bias, max_upu_bias, mass_dependence, m_ratio
        upu_par_box_1.Text = dev_UPu_par(1)
        upu_par_box_2.Text = dev_UPu_par(2)
        upu_par_box_3.Text = dev_UPu_par(3)
        upu_par_box_4.Text = dev_UPu_par(4)
        upu_par_box_5.Text = dev_UPu_par(5)

        pu_mass_box.Text = Int(1000 * Final_mass_new) / 1000

        item_upu_box.Text = geo_par(11)             '  typical expected moderator content in %
        min_upu_box.Text = geo_par(12)              '  minimum expected moderator content in %
        max_upu_box.Text = geo_par(13)              '  maximum expected moderator content in %
        cal_upu_box.Text = det_cal_UPu_ratio

        rel_upu_random = 0
        rel_UPu_bias = 0



        delta_upu = geo_par(11) - det_cal_UPu_ratio
        If geo_par(11) = 0 Then m_ratio = 0 Else m_ratio = m240_new / pu_240_effective / dev_UPu_par(5)
        rel_UPu_bias = (dev_UPu_par(1) + dev_UPu_par(2) * m_ratio + dev_UPu_par(3) * m_ratio ^ 2) * (delta_upu / dev_UPu_par(4)) ^ 0.75

        delta_upu = geo_par(13) - det_cal_UPu_ratio

        max_upu_bias = (dev_UPu_par(1) + dev_UPu_par(2) * m_ratio + dev_UPu_par(3) * m_ratio ^ 2) * (delta_upu / dev_UPu_par(4)) ^ 0.75

        delta_upu = geo_par(12) - det_cal_UPu_ratio
        min_upu_bias = (dev_UPu_par(1) + dev_UPu_par(2) * m_ratio + dev_UPu_par(3) * m_ratio ^ 2) * (delta_upu / dev_UPu_par(4)) ^ 0.75

        rel_upu_random = (max_upu_bias - min_upu_bias) / 6

        upu_random_box.Text = Int(10000 * rel_upu_random) / 100 & "%"

        upu_bias_box.Text = Int(10000 * rel_UPu_bias) / 100 & "%"
        '
        ''******************************************************************************************************
        '
        '                        Alpha Bias
        '
        ''******************************************************************************************************
        Dim delta_alpha, min_alpha_bias, max_alpha_bias, ref_alpha_bias
        Dim dev_alpha_par1(3)
        rel_alpha_random = 0
        rel_alpha_bias = 0

        ' set the checkboxes to indicate the expected impurities
        For i = 1 To 5
            If item_impurity_flag(i) = 1 Then impuritycheckbox.SetItemChecked(i - 1, True) Else impuritycheckbox.SetItemChecked(i - 1, False)
        Next i

        dev_alpha_index = 4
        For i = 1 To 5
            If item_impurity_flag(i) = 1 Then dev_alpha_index = i                  ' determine which impurity is present - if none use oxygen
            If item_impurity_flag(i) = 1 Then GoTo 50
        Next i
50:

        For i = 1 To 3
            dev_alpha_par1(i) = dev_alpha_par(3 * (dev_alpha_index - 1) + i)
        Next

        alpha_par_box_1.Text = dev_alpha_par1(1)                                                 ' empirical bias parameter
        alpha_par_box_2.Text = dev_alpha_par1(2)                                                 ' empirical bias parameter
        alpha_par_box_3.Text = dev_alpha_par1(3)                                                 ' empirical bias parameter
        '
        Item_alpha_box.Text = Int(1000 * alpha_new) / 1000                                      '  measured alpha value
        Item_alpha_err_box.Text = Int(1000 * alpha_new_err) / 1000                              '  uncertainty in measured alpha value
        ring_ratio_box.Text = Int(1000 * ring_ratio) / 1000                                     '  measured ring ratio
        ring_ratio_err_box.Text = Int(1000 * ring_ratio_err) / 1000                             '  measured ring ratio uncertainty
        '
        ref_ring_ratio = 0
        If outer_ring_eff_240 > 0 Then ref_ring_ratio = inner_ring_eff_240 / outer_ring_eff_240         '  reference 240Pu ring ratio from the dual energy data screen

        ref_ring_ratio_box.Text = Int(1000 * ref_ring_ratio) / 1000                                     '  240Pu reference ring ratio

        cal_alpha_box.Text = det_cal_alpha

        rel_alpha_random = 0
        rel_alpha_bias = 0

        ref_alpha_bias = dev_alpha_par1(1) + dev_alpha_par1(2) * (Final_mass_new) ^ dev_alpha_par1(3) * det_cal_alpha
        rel_alpha_bias = dev_alpha_par1(1) + dev_alpha_par1(2) * (Final_mass_new) ^ dev_alpha_par1(3) * alpha_new
        rel_alpha_bias = rel_alpha_bias - ref_alpha_bias
        rel_alpha_random = dev_alpha_par1(2) * (Final_mass_new) ^ dev_alpha_par1(3) * alpha_new_err


        alpha_random_box.Text = Int(10000 * rel_alpha_random) / 100 & "%"
        alpha_bias_box.Text = Int(10000 * rel_alpha_bias) / 100 & "%"

        Dim bias_neg_flag
        Dim alpha_bias_sign As String
        If (ring_ratio + ring_ratio_err * 3) > ref_ring_ratio And ref_ring_ratio > 0 And ring_ratio > 0 Then bias_neg_flag = 1 Else bias_neg_flag = 0
        If (item_impurity_flag(1) + item_impurity_flag(2) + bias_neg_flag) > 0 Then bias_neg_flag = 1
        If bias_neg_flag = 1 Then alpha_bias_sign = "negative" Else alpha_bias_sign = "positive"

        alpha_bias_sign_box.Text = alpha_bias_sign
        '
        ''******************************************************************************************************
        '
        '                        Moderator Bias
        '
        ''******************************************************************************************************
        Dim delta_mod, min_mod_bias, max_mod_bias
        mod_par_box_1.Text = dev_mod_par(1)
        mod_par_box_2.Text = dev_mod_par(2)
        mod_par_box_3.Text = dev_mod_par(3)
        typical_mod_box.Text = geo_par(17)              '  typical expected moderator content in %
        minimum_mod_box.Text = geo_par(18)              '  minimum expected moderator content in %
        maximum_mod_box.Text = geo_par(19)              '  maximum expected moderator content in %
        Cal_mod_box.Text = det_cal_mod

        rel_mod_random = 0
        rel_mod_bias = 0

        delta_mod = (geo_par(17) - det_cal_mod) / 100
        rel_mod_bias = dev_mod_par(1) + dev_mod_par(2) * delta_mod + dev_mod_par(3) * delta_mod ^ 2


        delta_mod = (geo_par(19) - geo_par(17)) / 100
        max_mod_bias = dev_mod_par(1) + dev_mod_par(2) * delta_mod + dev_mod_par(3) * delta_mod ^ 2


        delta_mod = (geo_par(18) - geo_par(17)) / 100
        min_mod_bias = dev_mod_par(1) + dev_mod_par(2) * delta_mod + dev_mod_par(3) * delta_mod ^ 2

        rel_mod_random = (max_mod_bias - min_mod_bias) / 6
        moderator_rand_box.Text = Int(10000 * rel_mod_random) / 100 & "%"

        moderator_bias_box.Text = Int(10000 * rel_mod_bias) / 100 & "%"

        '
        ''******************************************************************************************************
        '
        '                        Density Bias
        '
        ''******************************************************************************************************


        Dim ref_rho, min_rho, max_rho_bias, ref_bias_mass, ref_rho_dev, rho_sigma, typ_rho_bias
        Dim min_rho_bias, typ_rho, max_rho, delta_rho

        ref_rho = det_cal_ref_density
        typ_rho = geo_par(14)
        min_rho = geo_par(15)
        max_rho = geo_par(16)

        item_rho_box.Text = geo_par(14)
        ref_rho_box.Text = det_cal_ref_density
        min_rho_box.Text = geo_par(15)
        max_rho_box.Text = geo_par(16)


        typ_rho_bias = dev_rho_par(1)
        ref_bias_mass = dev_rho_par(2)
        ref_rho_dev = dev_rho_par(3)
        rho_sigma = dev_rho_par(4)


        rho_par_box1.Text = dev_rho_par(1)
        rho_par_box2.Text = dev_rho_par(2)
        rho_par_box3.Text = dev_rho_par(3)
        rho_par_box4.Text = dev_rho_par(4)
        rho_par_box5.Text = dev_rho_par(5)

        ref_bias_mass = dev_rho_par(5)
        delta_rho = ref_rho - typ_rho

        rel_rho_bias = -dev_rho_par(1) * (Final_mass_new / ref_bias_mass / typ_rho) ^ dev_rho_par(2) * (1 + dev_rho_par(3) * Exp(-dev_rho_par(4) * typ_rho)) * delta_rho
        '
        rel_rho_bias_box.Text = Int(10000 * rel_rho_bias) / 100 & "%"

        delta_rho = ref_rho - min_rho
        min_rho_bias = dev_rho_par(1) * (Final_mass_new / ref_bias_mass / min_rho) ^ dev_rho_par(2) * (1 + dev_rho_par(3) * Exp(-dev_rho_par(4) * typ_rho)) * delta_rho

        delta_rho = ref_rho - max_rho
        max_rho_bias = dev_rho_par(1) * (Final_mass_new / ref_bias_mass / max_rho) ^ dev_rho_par(2) * (1 + dev_rho_par(3) * Exp(-dev_rho_par(4) * typ_rho)) * delta_rho

        rel_rho_rand = Abs(max_rho_bias - min_rho_bias) / 2
        rel_rho_rand_box.Text = Int(10000 * rel_rho_rand) / 100 & "%"

        '  Alternate bias calculation - based on emprical multiplcation bias correction

        Dim total_mass, fill_h

        mult_corr_fact = 1 + mult_corr(1) * (mult_new - 1) + mult_corr(2) * (mult_new - 1) ^ 2

        For i = 1 To 10
            det_val_mult(i) = det_par_val(i)
        Next

        det_val_mult(1) = vol_average_fill_efficiency

        '     total_mass = m240_new / pu_240_effective * geo_par(11)
        '     fill_h = total_mass / (typ_rho * PI * (container_IDiam / 2) ^ 2)

        m_bias_pt = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 1)             ' m240 Using parameter file effieincy (point or otherwise)
        m_bias_fill = mult_anal2(new_rates, new_rates_err, new_covar, det_val_mult, fiss_const_val, 1)          ' m240 using volume average effieincy

        mult_mass_bias = m_bias_fill * mult_corr_fact                                               ' multiplication corrected volume average efficiency mass


        Mult_box1.Text = Int(10000 * mult_new) / 10000                                              ' assay result multiplication
        mult_bias_box1.Text = Int(10000 * mult_corr_fact) / 10000                                   ' Multiplication correction factor
        mult_bias_box2.Text = Int(10000 * vol_average_fill_efficiency / det_par_val(1)) / 10000     ' ratio of volume averge effieincy to point source effieincy
        mult_bias_box3.Text = Int(10000 * (mult_mass_bias - m_bias_pt) / m_bias_pt) / 100 & "%"     ' relative bias due to density difference

        '
        ''******************************************************************************************************
        '
        '                        Wall Effect Bias
        '
        ''******************************************************************************************************
        Dim item_wall, cal_wall, wall_eff, wall_effect_bias
        Dim det_val_wall(10), m_bias_wall, m_bias_standard
        wall_par_Box1.Text = dev_wall_par(1)
        Item_wall_mat_box.Text = geo_par(5)
        Item_wall_thick_box.Text = geo_par(4)
        cal_wall_mat_box.Text = cal_wall_material
        cal_wall_thick_box.Text = cal_wall_thickness

        cal_wall = cal_wall_thickness
        item_wall = geo_par(4)
        If geo_par(5) <> "Stainless Steel" Then item_wall = 0
        If cal_wall_material <> "Stainless Steel" Then cal_wall = 0

        wall_eff = 1 + (item_wall - cal_wall) * dev_wall_par(1)

        For i = 1 To 10
            det_val_wall(i) = det_par_val(i)
        Next

        det_val_wall(1) = wall_eff * det_par_val(1)

        m_bias_standard = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)             ' m240 Using parameter file effieincy 
        m_bias_wall = mult_anal2(new_rates, new_rates_err, new_covar, det_val_wall, fiss_const_val, 5)                ' m240 using wall_corrected effieincy

        wall_effect_bias = (m_bias_wall - m_bias_standard) / m_bias_standard

        Wall_Bias_Box.Text = Int(10000 * wall_effect_bias) / 100 & "%"


    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub
End Class