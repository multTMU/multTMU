Public Class Pu240EffectiveConversion
    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim file_location, item_param_file, fiss_param_file, iso_param_file As String


        '    file_location = "c:\multiplicity_TMU\item_info\"
        '    item_param_file = file_location & "current_item.csv"
        '    Call get_item_info_file(item_param_file)                        ' read in item specific information from csv file


        file_location = "c:\multiplicity_TMU\fission_parameters\"
        fiss_param_file = file_location & "current_fisison_parameters.csv"
        Call get_fiss_const_file(fiss_param_file)                       ' read nuclear data constants file

        file_location = "c:\multiplicity_TMU\nuclide_data\"
        iso_param_file = file_location & "Nuclide_decay_data.csv"
        Call get_iso_par_file(iso_param_file)                           ' read isotopics conversion constants file



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

        pu238_eff_box.Text = Int(100000 * m240_conv(1)) / 100000
        pu239_eff_box.Text = Int(100000 * m240_conv(2)) / 100000
        pu240_eff_box.Text = Int(100000 * m240_conv(3)) / 100000
        pu241_eff_box.Text = Int(100000 * m240_conv(4)) / 100000
        pu242_eff_box.Text = Int(100000 * m240_conv(5)) / 100000
        pu244_eff_box.Text = Int(100000 * m240_conv(6)) / 100000
        am241_eff_box.Text = Int(100000 * m240_conv(7)) / 100000

        pu238_eff_box_err.Text = Int(100000 * m240_conv_err(1)) / 100000
        pu239_eff_box_err.Text = Int(100000 * m240_conv_err(2)) / 100000
        pu240_eff_box_err.Text = Int(100000 * m240_conv_err(3)) / 100000
        pu241_eff_box_err.Text = Int(100000 * m240_conv_err(4)) / 100000
        pu242_eff_box_err.Text = Int(100000 * m240_conv_err(5)) / 100000
        pu244_eff_box_err.Text = Int(100000 * m240_conv_err(6)) / 100000
        am241_eff_box_err.Text = Int(100000 * m240_conv_err(7)) / 100000


        dec_date_pu = iso_date(1)
        dec_date_Am = iso_date(2)
        dec_date_cf = iso_date(3)

        Dim num_days As Integer
        Dim new_iso1(12), new_iso_err1(12), iso_err_temp(12), new_iso_err2(12), faux_iso_err(12), faux_half_life_err(12), pu_240_effective_err_decay

        '  num_days = assay_date - dec_date_pu
        Dim days As Double = DateDiff(DateInterval.Day, dec_date_pu, assay_date)
        Dim alpha_val(11)

        '  -------------------------------------------------------------------------------
        ' Decay correction isotopic data

        Dim conv_240(12), conv_240_err(12)

        For i = 1 To 12
            faux_iso_err(i) = 0
            faux_half_life_err(i) = 0
            conv_240_err(i) = 0
        Next i


        For i_out = 1 To 8

            new_iso1(i_out) = iso_decay(dec_date_pu, dec_date_Am, dec_date_cf, assay_date, iso_val, iso_val_err, i_out, alpha_val, half_life, half_life_err)
            new_iso_err1(i_out) = iso_decay(dec_date_pu, dec_date_Am, dec_date_cf, assay_date, iso_val, iso_val_err, i_out + 20, alpha_val, half_life, half_life_err)
            new_iso_err2(i_out) = iso_decay(dec_date_pu, dec_date_Am, dec_date_cf, assay_date, iso_val, faux_iso_err, i_out + 20, alpha_val, half_life, half_life_err)   ' error with declared errors = 0 to calc error due to nuc data

        Next i_out

        If new_iso1(8) > 0 Then new_iso1(8) = 1
        new_iso_err1(8) = 0


        ' calculate Pu240 effective per gram value

        Dim pu_240_effective1, pu_240_effective_err1, pu_240_effective_err_nd, pu_240_effective_err_iso


        pu_240_effective1 = 0
        pu_240_effective_err1 = 0
        pu_240_effective_err_nd = 0
        pu_240_effective_err_iso = 0
        pu_240_effective_err_decay = 0

        For i = 1 To 8
            conv_240(i) = m240_conv(i) * new_iso1(i)
            conv_240_err(i) = (m240_conv(i) * new_iso_err1(i)) ^ 2 + (m240_conv(i) * new_iso_err2(i)) ^ 2
            conv_240_err(i) = conv_240_err(i) + (m240_conv_err(i) * new_iso1(i)) ^ 2 + (m240_conv(i) * new_iso_err1(i)) ^ 2
            conv_240_err(i) = conv_240_err(i) ^ 0.5

            pu_240_effective1 = pu_240_effective1 + m240_conv(i) * new_iso1(i)                                   ' sum weighted pu240effective contributions
            pu_240_effective_err1 = pu_240_effective_err1 + (m240_conv(i) * new_iso_err1(i)) ^ 2

            pu_240_effective_err1 = pu_240_effective_err1 + (m240_conv_err(i) * new_iso1(i)) ^ 2

            pu_240_effective_err_iso = pu_240_effective_err_iso + (m240_conv(i) * new_iso_err1(i)) ^ 2
            pu_240_effective_err_nd = pu_240_effective_err_nd + (m240_conv_err(i) * new_iso1(i)) ^ 2
            pu_240_effective_err_decay = pu_240_effective_err_decay + (m240_conv(i) * new_iso_err2(i)) ^ 2        ' error due to decay correction

        Next i

        pu_240_effective_err_nd = pu_240_effective_err_nd ^ 0.5
        pu_240_effective_err = pu_240_effective_err ^ 0.5
        pu_240_effective_err_iso = pu_240_effective_err_iso ^ 0.5
        pu_240_effective_err_decay = pu_240_effective_err_decay ^ 0.5
        pu_240_effective_err1 = pu_240_effective_err1 ^ 0.5



        If new_iso(8) > 0 Then pu_240_effective1 = 1
        If new_iso(8) > 0 Then pu_240_effective_err1 = 0

        wt_box_238.Text = Int(100000 * conv_240(1)) / 100000
        wt_box_239.Text = Int(100000 * conv_240(2)) / 100000
        wt_box_240.Text = Int(100000 * conv_240(3)) / 100000
        wt_box_241.Text = Int(100000 * conv_240(4)) / 100000
        wt_box_242.Text = Int(100000 * conv_240(5)) / 100000
        wt_box_244.Text = Int(100000 * conv_240(6)) / 100000
        wt_box_am241.Text = Int(100000 * conv_240(7)) / 100000

        wt_box_238_err.Text = Int(100000 * conv_240_err(1)) / 100000
        wt_box_239_err.Text = Int(100000 * conv_240_err(2)) / 100000
        wt_box_240_err.Text = Int(100000 * conv_240_err(3)) / 100000
        wt_box_241_err.Text = Int(100000 * conv_240_err(4)) / 100000
        wt_box_242_err.Text = Int(100000 * conv_240_err(5)) / 100000
        wt_box_244_err.Text = Int(100000 * conv_240_err(6)) / 100000
        wt_box_am241_err.Text = Int(100000 * conv_240_err(7)) / 100000

        MaskedTextBox1.Text = Int(100000 * pu_240_effective1) / 100000              ' pu240effective per gram value
        MaskedTextBox7.Text = Int(100000 * pu_240_effective_err1) / 100000          ' overall error in pu240effective

        MaskedTextBox2.Text = Int(100000 * pu_240_effective_err_iso) / 100000       ' error due to orignal declaration 
        MaskedTextBox3.Text = Int(100000000 * pu_240_effective_err_decay) / 100000000     ' error due to decay correction
        MaskedTextBox4.Text = Int(100000 * pu_240_effective_err_nd) / 100000        ' error due to nuclear data uncertainty
        MaskedTextBox5.Text = Int(100000 * (pu_240_effective_err_iso ^ 2 + pu_240_effective_err_decay ^ 2 + pu_240_effective_err_nd ^ 2) ^ 0.5 + pu_240_effective_err_decay) / 10000

        MaskedTextBox6.Text = Int(100000 * pu_240_effective_err1 / pu_240_effective1) / 1000 & "%"



    End Sub

End Class