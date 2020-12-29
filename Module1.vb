Imports System.IO
Imports System.Text
Imports System.Math

Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Module Module1
    '   Form 1: primary screen, displays raw data and results
    '   Form 2: Fission Parameter data entry form
    '   Form 3: Detector calibration parameters entry
    '   Form 4: Nuclear data constants entry 
    '   Form 5: Empirical bias corrections parameters 
    '   Form 6: Item Information (container size, material type, etc)
    '   Form 7: Detector physical parameters entry
    '   Form 8: Miscellaneous analysis parameters
    '   Form 9: Evaluation of radial offset error contribution to TMU
    '   Form 10: Evaluation of axial (material fill height) error contribution to TMU
    '   Form 11: Detector Uncertainty Contribution to TMU
    '   Form 12: Pu240 effective conversion error contribution to TMU
    '   Form 13: Fission moments contribution to TMU
    '   Form 14: Comparison of predicted and observed measurement precision
    '   Form 15: Contribution of empirical biases to the TMU
    '   Form 16: Dual energy multipicity analysis parameter entry form (for future use)
    '   Module 1: (this module) Common parameters definition primary analysis and error calculations
    '       includes the following subroutines for extracting data from files
    '           Sub extract_from_data_file(spec_file_name)
    '           Sub Get_summed_histogram()
    '           Sub get_iso_par_file(det_file)
    '           Sub get_fiss_const_file(fiss_const_file)
    '           Sub get_det_par_file(det_file)
    '           Sub get_item_info_file(det_file)
    '           Sub get_det_dimensions_file(det_dim_file)
    '           Sub get_misc_par_file(misc_par_file)
    '           Sub get_empirical_par_file(empirical_par_file)
    '       includes the following rates caluclation subroutines
    '           Sub calc_rates_jitter(A_R_i, ACCID_i, A_R_len, ACC_len, det_parms, assay_time, i_runs)
    '           Sub calc_M_rates(out_rates, A_R, ACCID, A_R_len, ACC_len, det_parms, assay_time)
    '
    '       Module 1 also includes the following functions
    '	        Function calc_rates(A_R, ACCID, A_R_len, ACC_len, det_parms, assay_time, i_out)
    '       	Function calc_alpha(input_iso, input_iso_err, alpha_val, alpha_err, m240val, spont_fiss, idex)
    '       	Function sep_histogram(input_string, i_num)
    '       	Function sep_rates(input_string, i_num)
    '       	Function sep_iso(input_string, i_num
    '       	Function iso_decay(dec_date_Pu, dec_date_Am, dec_date_cf, meas_date, init_iso_temp, init_iso_temp_err, iso_num, alpha_val, half_lives, half_life_err)
    '
    '       	Function mult_anal(rates, rates_err, det_parms, fis_dat, i_out)
    '       	Function mult_anal2(rates, rates_err, covar_m, det_parms, fis_dat, i_out)
    '       	Function mult_anal_err2(A_R, ACCID, A_R_len, ACC_len, det_parms, det_parms_err, fis_dat, fis_dat_err, assay_time, p_out, i_out, j_out)
    '
    '       	Function radial_err(del_Radius, R_Cav, T_L, cav_ID, container_Dia, n_it)
    '
    '       	Function NormSInv(ByVal p As Double) As Double
    '       	Function Factorial(ByVal x As Integer) As Double
    '       	Function Permutation(ByVal n As Long, ByVal r As Long) As Long
    '       	Function Combinatorial(ByVal a As integer, ByVal b As integer) As Double
    '
    '   Module 2: Radial and Axial response profiles and uncertainty contributions
    '       includes the folloing subroutines and functions
    '   		Sub calc_radial_error()
    '	    	Sub calc_axial_errors()
    '	
    '		    Function rad_eff(T_L, R_cav, Radial_offset, z_offset)
    '		    Function z_eff(T_L, R_cav, Radial_offset, z_offset)
    '		    Function c_rates(i, mass, Multi, alpha0, n_eff, doub_frac, trip_frac, gate_width, nuc_dat, back_rate, assay_time)
    '
    '

    Public spec_file_name As String                                                                     ' INCC file name
    Public item_file_name_read As String                                                                ' place older for last item ID file read in
    '
    '
    Public fiss_const_val(12)                                                                           ' fission constants entered in FORM 2
    Public fiss_const_err(12)                                                                           ' fission constants uncertainties in FORM 2
    Public fiss_covar(12, 12)                                                                           ' fission constants covariance terms in FORM 2
    '
    '  Detector parameters entered in FORMS 3 and 16 from detector_parameter.CSV file
    Public current_det_file_name As String                                                              ' detector csv file name
    Public det_par_val(10)                                                                              ' selected detector paraemters in FORM 3
    Public det_par_err(10)                                                                              ' detector paraemter uncertainties in FORM 3
    Public det_par_covar(10, 10), det_cal_FH, det_cal_UPu_ratio, det_cal_pu240_eff, det_cal_alpha       ' detector paraemter covar array and calibration characeteristics in FORM 3
    Public det_cal_mod, det_cal_ref_density                                                             ' calibration reference values in FORM 3
    Public pt_cal_flag As Boolean                                                                       ' point source calibration flag in FORM 3
    Public neut_energy(101), det_eff_E(101), inner_outer(101), rel_fiss_prob(101)                       ' parameters for dual energy multiplicity analysis in FORM 16
    Public inner_ring_eff_240, outer_ring_eff_240, ref_ring_ratio                                       ' reference ring ratios for impurity impact FORM 16 and FORM 15
    '
    ' Nuclide data parameter definitions FORM 4 for Pu-240 effective conversion and to calculate alpha from isotopics
    Public half_life(12), half_life_err(12)                                                            ' Half-lives and uncertainties for decay corrections - FORM 4
    Public spont_fiss_rate(12), spont_fiss_err(12)                                                     ' spontaneous fission rates and uncertainties  - FORM 4
    Public alpha_n_rate(12), alpha_n_err(12)                                                           ' oxygen alpha,n emission rates - FORM 4
    Public f_alpha_n_rate(12), f_alpha_n_err(12)                                                       ' fluorine alpha,n emission rates - FORM 4
    Public m240_conv(12), m240_conv_err(12)                                                            ' spontaneous fission yields
    Public pu240_eff_parms(12), pu240_eff_parms_err(12)                                                ' constants for calcuation of Pu240 effective relativ mass
    '
    '
    Public empirical_par_file As String                                                                ' name empirical parameter file - FORM 5
    Public mult_par(10), dev_pu240_par(10), dev_UPu_par(10), dev_alpha_par(18), dev_alpha_index        ' empirical TMU bias parameters - FORM 5, alpha_dev_index selects impurity parameters 
    Public dev_mod_par(10), dev_rho_par(10)                                                            ' empirical TMU bias parameters - FORM 5
    '
    '   item information from item_information.csv file - FORM 6 
    Public iso_par_file_name As String                                                                 '  name item information file - FORM 6
    Public iso_val(12), iso_val_err(12)                                                                '  isotopics and uncertainty values from item_information.csv file - FORM 6
    Public iso_date(4) As Date                                                                         '  isotopic declaration dates from item_information.csv file - FORM 6
    Public geo_par(19), upu_ratio, item_impurity_flag(10), item_impurity_conc(10)                      '  container an material information for the assay item from item_information.csv file - FORM 6
    Public item_id As String                                                                           '  item ID from item_information.csv file - FORM 6
    '
    '
    Public counter_id As String                                                                        ' detector ID - FORM 7
    Public det_dim_val(20), tube_numbers(10)                                                           ' detector physical parameters - FORM 7
    '
    '
    Public misc_par_file As String                                                                     ' name miscellaneous parameter file - FORM 8
    Public misc_param(12)                                                                              ' misc analysis parameters entered in FORM 8
    '
    '    --------------------------------------------------------------------------
    '
    Public use_file_det_flag, use_file_iso_flag As Boolean                                              ' flags indicate whether to use parameters from INCC or CSV file - FORM 1
    Public use_LANL_DT_flag As Boolean
    Public file_det_parameters(10)                                                                      ' detector calibration parameters extracted from INCC file - Module 1
    Public file_delcared_pu240_m, file_declared_putot_m                                                ' declared 240Pu effective and total Pu mass values - Module 1
    '
    Public cycle_time                                                                                   ' cycle time in seconds from RTS file, number of sigma for outlier rejection
    Public file_assay_date As String                                                                    ' assay date extracted in LANL format from INCC file
    Public assay_date, file_pu_date, file_am_date As Date                                               ' converted assay date, Pu and Am declaration dates from INCC file
    Public file_iso(12), file_iso_err(12)                                                               ' Pu isotopics from INCC file, error array is a hook for CI files to be implemented later

    Public grunt_time(2) As Double                                                                      ' 1 = file assay time, 2 = reanalysis assay time
    Public dt_par_a, dt_par_b, dt_par_c, dt_par_d, dead_time_parm, die_away_t, gate_width, f_d, f_t     ' detector parameters from INCC file

    Public cycle_hist_A_R(9999, 512), cycle_hist_Acc(9999, 512) As Integer                              ' cycle by cycle histograms from INCC file
    Public max_bin, num_A_R(512), num_Acc(512) As Integer                                               ' summed hsiotgram from INCC file  
    Public cycle_hist_A_R_max(9999), cycle_hist_ACC_max(9999) As Integer                                ' maximum bin populated in histograms 
    Public new_max_bin_A_R, new_max_bin_Acc As Integer
    Public file_rates_out(10), sum_ar, sum_acc As Double                                                ' rates and uncertainties from analysis for INCC file summed histograms
    Public file_pass_sing_rate, file_pass_doub_rate, file_pass_trip_rate As Double                      ' average passive rates from INCC file
    Public file_pass_sing_rate_err, file_pass_doub_rate_err, file_pass_trip_rate_err As Double          ' erross in average passive rates from INCC file
    Public file_avg_singles, file_avg_doubles, file_avg_triples As Double                               ' average of recalculated rates from historgrams using new filters
    Public file_stdev_singles, file_stdev_doubles, file_stdev_triples As Double                         ' uncertainties in average of recalculated rates from historgrams using new filters

    Public file_net_cycles, file_total_count_time                                                       ' number good cycles and net measurement time from INCC file
    Public file_co_variance_sd, file_co_variance_dt, file_co_variance_st As Double                      ' covariance terms for average rates of INCC file rates

    Public avg_singles, avg_doubles, avg_triples As Double                                              ' average of recalculated rates from historgrams using new filters
    Public stdev_singles, stdev_doubles, stdev_triples As Double                                        ' uncertainties in average of recalculated rates from historgrams using new filters
    Public co_variance_sd, co_variance_dt, co_variance_st As Double                                     ' covariance terms for average rates

    Public new_num_A_R(512), new_num_Acc(512), qc_sigma, acc_sigma, min_cycles                          ' revised summed histograms using current filter
    Public cycle_hardware_fail(9999), cycle_outlier_fail(9999, 4) As Integer                            ' revised filter results 0 = pass, 1 = fail
    Public max_cycles, net_cycles                                                                       ' total cycles, total good cycles after application of revised filters
    Public new_rates_out(10) As Double                                                                  ' rates and uncertainties from analysis new summed histograms
    Public use_triples_flag As Boolean

    Public new_rates(3), new_rates_err(3), new_covar(3, 3) As Double                                    ' temporary arrays for calculating S, D, and T rates

    Public scaler1, scaler2, bkg_scaler1, bkg_scaler2, ring_ratio, ring_ratio_err                       ' scaler rates extracted from INCC file, ring ratio and error calculated from scaler rates

    Public file_pass_sing_bkg, file_pass_doub_bkg, file_pass_trip_bkg                                      ' passive background rates from RTS file
    Public file_pass_sing_bkg_err, file_pass_doub_bkg_err, file_pass_trip_bkg_err                          ' passive background rate errors from RTS file
    Public file_singles(9999), file_doubles(9999), file_triples(9999), file_rates_flag(9999), good_cycles  ' cycle by cycle rates, rejection flag and number of good cycles from RTS file
    Public file_item_id, data_file_id As String                                                            ' Item ID from INCC file, INCC file name
    '
    Public new_iso(8), new_iso_err(8), dec_date_pu, dec_date_Am, dec_date_cf                            ' decay corrected isotopics array, Pu, Am and Cf declaration dates


    Public pu_240_effective, pu_240_effective_err                                                       ' Pu240 effective mass per gram Pu (g/g) and uncertainty
    Public alpha_val_cal, alpha_val_cal_err, adjusted_alpha, adjusted_alpha_err                         ' alpha value from decay corrected isotopics, and expected alpha weighted for relative Oxy number

    Public m240_new, m240_new_err, alpha_new, alpha_new_err, mult_new, mult_new_err                     ' Pu240 mass (g), alpha, multipication and errors from revised rates analysis
    Public m240_file, m240_file_err, alpha_file, alpha_file_err, mult_file, mult_file_err               ' Pu240 mass (g), alpha, multipication and errors from INCC rates analysis

    Public mult_new_nocovar_err, alpha_new_nocovar_err, m240_new_nocovar_err                            ' pu240 mass result covariance array from revised rates analysis
    Public mult_file_nocovar_err, alpha_file_nocovar_err, m240_file_nocovar_err                         ' pu240 mass result covariance array from INCC rates analysis
    '
    Public Final_mass_new, final_mass_err_new, final_mass_nocovar_err_new                               ' total pu mass result, uncertainty and covariance array from revised rates analysis
    Public Final_mass_file, final_mass_err_file, final_mass_nocovar_err_file                            ' total pu mass result, uncertainty and covariance array from INCC rates analysis
    Public det_comp(10), det_err_comp(10), det_err_sum                                                  ' detector parameter, sensitvity,error contribution from each detector parameter and summed error contribution from the detector parameters
    Public fiss_mom_comp(7), fiss_mom_comp_err(7), fiss_mom_err_sum                                     ' fission moment sensitivity, fission moment error contribution and sum of fission momement errors
    Public radial_random, Radial_bias                                                                   ' radial offset error contributions, random and systematic

    Public rel_rho_bias, rel_rho_rand                                                                   ' Systematic and random error contributions due to item density
    Public rel_alpha_bias, rel_burnup_bias                                                              ' Systematic and random error contributions due to item isotopics
    Public rel_UPu_bias, rel_mod_bias                                                                   ' Systematic and random error contributions due to item moderator content
    Public rel_mod_random, rel_upu_random                                                               ' Systematic and random error contributions due to item UPu ratio
    Public rel_alpha_random                                                                             ' Random error contributions due to item alpha (calculated from assay value)

    Public jitter_out_rates(16) As Double                                                               ' temporary array used to calculate jitter_rates and errors from dithering of the summed histogram
    Public jitter_rates(3), jitter_rates_err(3), jitter_rates_covar(3, 3) As Double                     ' average rates, errors and covariances from dithered histograms
    Public n_jitter                                                                                     ' number of iterations performed to calculate dithered rates

    Public pu_240_effective_err_trad, final_mass_err_file_trad, final_mass_nocovar_err_file_trad        ' errors for pu-240-effective without nuclear data contribution
    Public final_mass_nocovar_err_new_trad, final_mass_err_new_trad                                     ' errors for pu-240-effective without nuclear data contribution

    Public radial_likely_offset
    Public container_IDiam, temp_eff, temp_fd, temp_ft, typical_container_offset, Random_container_offset, detector_stand, container_H
    Public i_rean As Integer

    Public mult_corr(10), mult_corr_err(2), mult_corr_covar_ab                                           ' multiplication correction factor and uncertainty temporay covariance - mult_corr_covar_ab = mult_corr_err(6)
    Public mult_corr_flag As Boolean                                                                     ' apply multiplication correction factor if TRUE   
    Public mult_mass_bias, mult_corr_fact, mult_corr_fact_err, m_bias_pt, m_bias_fill
    Public det_val_mult(10)                                                                              ' temprory det value array for mult corr effect calc

    Public cal_wall_thickness, cal_wall_material, dev_wall_par(10), rel_wall_effect_bias                 ' reference data for wall thickness correction

    Public num_days_pu As Double
    Public num_days_am As Double
    Public num_days_cf As Double


    Sub main(i_rean, use_file_det_flag)
        Dim file_location, det_param_file, fiss_param_file, iso_param_file, det_dim_file As String
        Dim i_out, A_R_len, ACC_len, j, det_flag As Integer
        '  Dim A_R, ACCID, assay_time
        Dim count_time, accidentals(9999), acc_err(9999), A_S_unc(9999), singles(9999), sing_err(9999)
        Dim rates_array(9999, 10), temp_a_r(512), temp_acc(512)

        Dim new_count_time

        '   Dim spec_file_name As String
        Dim reject_cycle
        Dim check_sum(9999) As Integer
        Dim acc_flag As Integer

        If i_rean = 1 Then GoTo 1
        item_file_name_read = "Default"
        '   ********* Get spectrum file name   *******

        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\multiplicity_tmu\"
        openFileDialog1.Filter = "txt files (*.txt)|*.rts|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    spec_file_name = openFileDialog1.FileName

                End If
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            Finally
                ' Check this again, since we need to make sure we didn't throw an exception on open.
                If (myStream IsNot Nothing) Then
                    myStream.Close()
                End If
            End Try
        End If

1:       '    ******** re-enter here for reanalysis     ********
        If spec_file_name = "" Then MsgBox("No data file selected")
        If spec_file_name = "" Then Return
        ' initial set for QC flags
        For i = 1 To 1000
            cycle_hardware_fail(i) = 0      ' hardware filters ; 0 = pass ; 1 = fail  read from data file
            cycle_outlier_fail(i, 1) = 0    ' singles filter ; 0 = pass ; 1 = fail
            cycle_outlier_fail(i, 2) = 0    ' doubles filter ; 0 = pass ; 1 = fail
            cycle_outlier_fail(i, 3) = 0    ' triples filter ; 0 = pass ; 1 = fail
            cycle_outlier_fail(i, 4) = 0    ' A/S test ; 0 = pass ; 1 = fail
            accidentals(i) = 0              ' accidentals rates array
        Next i

        file_delcared_pu240_m = 0
        file_declared_putot_m = 0

        ' load parameters and constants from CSV format data files
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
        misc_par_file = file_location & "TMU_general_parameters.csv"
        Call get_misc_par_file(misc_par_file)                               ' read miscellaneous TMU parameter file

        file_location = "c:\multiplicity_TMU\misc_parameters\"
        empirical_par_file = file_location & "tmu_empirical_parameters.csv"
        Call get_empirical_par_file(empirical_par_file)                     ' read empirical TMU bias parameter file

        ' **** Retrive information from acquistion rts file ****

        If misc_param(5) = 1 Then use_triples_flag = True Else use_triples_flag = False

        Call extract_from_data_file(spec_file_name)                       ' read RTS or VER file

        ' verify detector parameters for analysis are the same as listed in the file
        det_flag = 0
        For i = 1 To 10
            If file_det_parameters(i) <> det_par_val(i) Then det_flag = 1
        Next i
        If det_flag <> 0 Then MsgBox(" Parameters in data file differ from current parameters")


        ' **** if use_file_det_param_box is checked overwrite csv detector file parameters with INCC detector parameters read in from text file
        Dim det_vals As String
        det_vals = ""
        For i = 1 To 10
            If use_file_det_flag Then det_par_val(i) = file_det_parameters(i)
            If use_file_det_flag Then det_par_err(i) = 0
            For j = 1 To 10
                If use_file_det_flag Then det_par_covar(i, j) = 0
            Next j

        Next i


        ' **** overwrite csv item file parameters with INCC detector isotopics read in from text file
        If use_file_iso_flag Then iso_date(1) = file_pu_date
        If use_file_iso_flag Then iso_date(2) = file_am_date
        For i = 1 To 7
            If use_file_iso_flag Then iso_val(i) = file_iso(i) / 100
            If use_file_iso_flag Then iso_val_err(i) = file_iso_err(i) / 100
        Next i



        count_time = grunt_time(1)  ' total assay time after statsitical filters applied during original analysis

        ' calculate S, D, T from summed histogram listed in file
        For i_out = 1 To 6
            file_rates_out(i_out) = calc_rates(num_A_R, num_Acc, max_bin, max_bin, det_par_val, count_time, i_out)
        Next i_out

        'caluclate S, D, and T rates from i-th individual histograms cycle_hist_A_R and cycle_hist_Acc and place into array rates_array(i,j)
        'rates will be stored in temp_rates.csv
        Dim max_bin_temp As Integer


        For i = 1 To max_cycles
            singles(i) = 0
            max_bin_temp = cycle_hist_A_R_max(i)
            For j = 1 To max_bin_temp
                temp_a_r(j) = cycle_hist_A_R(i, j)
                temp_acc(j) = cycle_hist_Acc(i, j)
                singles(i) = singles(i) + cycle_hist_Acc(i, j)                                  ' non dead time corrected singles counts per cycle for A/S filter
                accidentals(i) = accidentals(i) + (j - 1) * cycle_hist_Acc(i, j)                '  calc accidentals rate from histogram for A/S filter
            Next j

            ' Calculate dead-time corrected S, D and T rates for cycle by cycle histograms

            For j = 1 To 10
                rates_array(i, j) = calc_rates(temp_a_r, temp_acc, max_bin_temp, max_bin_temp, det_par_val, cycle_time, j)
            Next j

            sing_err(i) = singles(i) ^ 0.5 / cycle_time         ' non dead-time corrected singles rate error for A/S test
            singles(i) = singles(i) / cycle_time                ' convert non dead-time corrected singles counts to rates for A/S test
            accidentals(i) = accidentals(i) / cycle_time
        Next i

        Dim avg_accidentals, stdev_accidentals
        '  accidentals/singles test
        Dim num_hdwr_pass
        avg_singles = 0
        avg_doubles = 0
        avg_triples = 0
        avg_accidentals = 0
        stdev_singles = 0
        stdev_doubles = 0
        stdev_triples = 0
        stdev_accidentals = 0
        num_hdwr_pass = 0
        '
        ' calculate singles, doubles, and triples rates exclucing cycles rejected by check sum failure
        '
        For i = 1 To max_cycles
            If cycle_hardware_fail(i) = 0 Then avg_singles = avg_singles + rates_array(i, 1)
            If cycle_hardware_fail(i) = 0 Then avg_doubles = avg_doubles + rates_array(i, 3)
            If cycle_hardware_fail(i) = 0 Then avg_triples = avg_triples + rates_array(i, 5)
            If cycle_hardware_fail(i) = 0 Then avg_accidentals = avg_accidentals + accidentals(i)
            acc_err(i) = accidentals(i) ^ 0.5 / cycle_time ^ 0.5
            A_S_unc(i) = Sqrt(acc_err(i) ^ 2) ' + (2 * singles(i) * sing_err(i) * det_par_val(3) * 0.000001) ^ 2)
            If cycle_hardware_fail(i) = 0 Then num_hdwr_pass = num_hdwr_pass + 1
        Next i


        avg_singles = avg_singles / num_hdwr_pass
        avg_doubles = avg_doubles / num_hdwr_pass
        avg_triples = avg_triples / num_hdwr_pass
        avg_accidentals = avg_accidentals / num_hdwr_pass

        ' calculate standard deviations for singles, doubles, and triples rates exclucing cycles rejected by check sum failure

        For i = 1 To max_cycles
            If cycle_hardware_fail(i) = 0 Then stdev_singles = stdev_singles + (avg_singles - rates_array(i, 1)) ^ 2
            If cycle_hardware_fail(i) = 0 Then stdev_doubles = stdev_doubles + (avg_doubles - rates_array(i, 3)) ^ 2
            If cycle_hardware_fail(i) = 0 Then stdev_triples = stdev_triples + (avg_triples - rates_array(i, 5)) ^ 2
            ' If cycle_hardware_fail(i) = 0 Then stdev_accidentals = stdev_accidentals + (avg_accidentals - accidentals(i)) ^ 2
        Next i

        stdev_singles = stdev_singles ^ 0.5 / (num_hdwr_pass - 1) ^ 0.5
        stdev_doubles = stdev_doubles ^ 0.5 / (num_hdwr_pass - 1) ^ 0.5
        stdev_triples = stdev_triples ^ 0.5 / (num_hdwr_pass - 1) ^ 0.5
        stdev_accidentals = stdev_accidentals ^ 0.5 / Sqrt(num_hdwr_pass - 1)

        '  **** Identify Cycles Rejected by S, D and T Statisical Filters and A/S Filter ***
        '  **** rejected cycle sets value to 1

        qc_sigma = misc_param(1)
        acc_sigma = misc_param(2)
        min_cycles = misc_param(3)
        acc_flag = 0
        Dim loopback_counter, max_loop_back As Integer
        loopback_counter = 0
        max_loop_back = 3


5:      ' Loop back for statistical filters

        For i = 1 To max_cycles
            If max_cycles > min_cycles Then
                If cycle_hardware_fail(i) = 0 And Abs(rates_array(i, 1) - avg_singles) > qc_sigma * stdev_singles Then cycle_outlier_fail(i, 1) = 1
                If cycle_hardware_fail(i) = 0 And Abs(rates_array(i, 3) - avg_doubles) > qc_sigma * stdev_doubles Then cycle_outlier_fail(i, 2) = 1
                If use_triples_flag And cycle_hardware_fail(i) = 0 And Abs(rates_array(i, 5) - avg_triples) > qc_sigma * stdev_triples Then cycle_outlier_fail(i, 3) = 1
            End If
            If Abs(accidentals(i) - det_par_val(3) * 0.000001 * singles(i) ^ 2) > (acc_sigma * acc_err(i)) Then acc_flag = 1 Else acc_flag = 0
            If avg_accidentals > 1000 And acc_flag = 1 Then cycle_outlier_fail(i, 4) = 1
            '  MsgBox("singles: " & singles(i) & " acc: " & accidentals(i) & " gate: " & det_par_val(3))
            '  MsgBox("dev: " & accidentals(i) - det_par_val(3) * 0.000001 * singles(i) ^ 2 & "  stdev: " & 4 * acc_err(i) & " flag= " & acc_flag)
        Next i

        avg_singles = 0
        avg_doubles = 0
        avg_triples = 0
        net_cycles = 0
        reject_cycle = 0

        For i = 1 To max_cycles
            reject_cycle = cycle_hardware_fail(i) + cycle_outlier_fail(i, 1) + cycle_outlier_fail(i, 2) + cycle_outlier_fail(i, 3) + cycle_outlier_fail(i, 4)

            If reject_cycle <> 0 Then GoTo 10
            avg_singles = avg_singles + rates_array(i, 1)
            avg_doubles = avg_doubles + rates_array(i, 3)
            avg_triples = avg_triples + rates_array(i, 5)


            net_cycles = net_cycles + 1
10:     Next i

        avg_singles = avg_singles / net_cycles
        avg_doubles = avg_doubles / net_cycles
        avg_triples = avg_triples / net_cycles

        stdev_singles = 0
        stdev_doubles = 0
        stdev_triples = 0
        co_variance_sd = 0
        co_variance_dt = 0
        co_variance_st = 0


        For i = 1 To max_cycles
            reject_cycle = cycle_hardware_fail(i) + cycle_outlier_fail(i, 1) + cycle_outlier_fail(i, 2) + cycle_outlier_fail(i, 3) + cycle_outlier_fail(i, 4)
            If reject_cycle <> 0 Then GoTo 15
            stdev_singles = stdev_singles + (avg_singles - rates_array(i, 1)) ^ 2
            stdev_doubles = stdev_doubles + (avg_doubles - rates_array(i, 3)) ^ 2
            stdev_triples = stdev_triples + (avg_triples - rates_array(i, 5)) ^ 2

            '          ' calculate covariance matrix for cycle by cycle rates
            '          co_variance_sd = co_variance_sd + (avg_singles - rates_array(i, 1)) * (avg_doubles - rates_array(i, 3))
            '         co_variance_dt = co_variance_dt + (avg_triples - rates_array(i, 5)) * (avg_doubles - rates_array(i, 3))
            '         co_variance_st = co_variance_st + (avg_singles - rates_array(i, 1)) * (avg_triples - rates_array(i, 5))
15:     Next i

        stdev_singles = stdev_singles ^ 0.5 / (net_cycles - 1) ^ 0.5
        stdev_doubles = stdev_doubles ^ 0.5 / (net_cycles - 1) ^ 0.5
        stdev_triples = stdev_triples ^ 0.5 / (net_cycles - 1) ^ 0.5


        '  ------------------- loop through filter ---------------
        loopback_counter = loopback_counter + 1
        If loopback_counter <= max_loop_back Then GoTo 5

        stdev_singles = 0
        stdev_doubles = 0
        stdev_triples = 0
        co_variance_sd = 0
        co_variance_dt = 0
        co_variance_st = 0

        For i = 1 To max_cycles
            reject_cycle = cycle_hardware_fail(i) + cycle_outlier_fail(i, 1) + cycle_outlier_fail(i, 2) + cycle_outlier_fail(i, 3) + cycle_outlier_fail(i, 4)
            If reject_cycle <> 0 Then GoTo 16
            stdev_singles = stdev_singles + (avg_singles - rates_array(i, 1)) ^ 2
            stdev_doubles = stdev_doubles + (avg_doubles - rates_array(i, 3)) ^ 2
            stdev_triples = stdev_triples + (avg_triples - rates_array(i, 5)) ^ 2

            ' calculate covariance matrix for cycle by cycle rates
            co_variance_sd = co_variance_sd + (avg_singles - rates_array(i, 1)) * (avg_doubles - rates_array(i, 3))
            co_variance_dt = co_variance_dt + (avg_triples - rates_array(i, 5)) * (avg_doubles - rates_array(i, 3))
            co_variance_st = co_variance_st + (avg_singles - rates_array(i, 1)) * (avg_triples - rates_array(i, 5))
16:     Next i

        stdev_singles = stdev_singles ^ 0.5 / ((net_cycles - 1) * net_cycles) ^ 0.5
        stdev_doubles = stdev_doubles ^ 0.5 / ((net_cycles - 1) * net_cycles) ^ 0.5
        stdev_triples = stdev_triples ^ 0.5 / ((net_cycles - 1) * net_cycles) ^ 0.5

        co_variance_sd = co_variance_sd / ((net_cycles - 1) * net_cycles)
        co_variance_dt = co_variance_dt / ((net_cycles - 1) * net_cycles)
        co_variance_st = co_variance_st / ((net_cycles - 1) * net_cycles)

        'calculate new summed historgram   *******************************************************************************

        For i = 1 To 512
            new_num_A_R(i) = 0
            new_num_Acc(i) = 0
        Next i
        new_max_bin_Acc = 0
        new_max_bin_A_R = 0

        Dim bin_string As String

        For i = 1 To max_cycles
            reject_cycle = cycle_hardware_fail(i) + cycle_outlier_fail(i, 1) + cycle_outlier_fail(i, 2) + cycle_outlier_fail(i, 3) + cycle_outlier_fail(i, 4)
            If reject_cycle <> 0 Then GoTo 17
            max_bin_temp = cycle_hist_A_R_max(i)
            For j = 1 To max_bin_temp
                new_num_A_R(j) = new_num_A_R(j) + cycle_hist_A_R(i, j)
                new_num_Acc(j) = new_num_Acc(j) + cycle_hist_Acc(i, j)
                If cycle_hist_A_R(i, j) > 0 Then new_max_bin_A_R = j
                If cycle_hist_Acc(i, j) > 0 Then new_max_bin_Acc = j
            Next j
17:     Next i
        new_count_time = net_cycles * cycle_time
        bin_string = ""
        '    For i = 1 To 10
        '    bin_string = bin_string & i & "  " & cycle_hist_A_R(1, i) & "  " & cycle_hist_Acc(1, i) & vbCrLf
        '   Next i
        '   MsgBox(bin_string)

        For i_out = 1 To 6
            new_rates_out(i_out) = calc_rates(new_num_A_R, new_num_Acc, new_max_bin_A_R, new_max_bin_Acc, det_par_val, new_count_time, i_out)
        Next i_out

        'Calculate covariance for filtered singles doubles triples from RTS file rates
        Dim num_rts
        file_avg_singles = 0
        file_avg_doubles = 0
        file_avg_triples = 0
        file_stdev_singles = 0
        file_stdev_doubles = 0
        file_stdev_triples = 0
        file_co_variance_sd = 0
        file_co_variance_st = 0
        file_co_variance_dt = 0
        num_rts = 0
        '  file_net_cycles

        For i = 1 To Math.Max(max_cycles, file_net_cycles)
            If file_rates_flag(i) = 0 Then file_avg_singles = file_avg_singles + file_singles(i)
            If file_rates_flag(i) = 0 Then file_avg_doubles = file_avg_doubles + file_doubles(i)
            If file_rates_flag(i) = 0 Then file_avg_triples = file_avg_triples + file_triples(i)
            If file_rates_flag(i) = 0 Then num_rts = num_rts + 1
        Next i

        'If num_rts < 2 Then num_rts = file_net_cycles

        file_avg_singles = file_avg_singles / num_rts
        file_avg_doubles = file_avg_doubles / num_rts
        file_avg_triples = file_avg_triples / num_rts

        For i = 1 To max_cycles
            If file_rates_flag(i) = 0 Then file_stdev_singles = file_stdev_singles + (file_singles(i) - file_avg_singles) ^ 2
            If file_rates_flag(i) = 0 Then file_stdev_doubles = file_stdev_doubles + (file_doubles(i) - file_avg_doubles) ^ 2
            If file_rates_flag(i) = 0 Then file_stdev_triples = file_stdev_triples + (file_triples(i) - file_avg_triples) ^ 2

            ' calculate covariance matrix for cycle by cycle rates
            file_co_variance_sd = file_co_variance_sd + (file_singles(i) - file_avg_singles) * (file_doubles(i) - file_avg_doubles)
            file_co_variance_dt = file_co_variance_dt + (file_triples(i) - file_avg_triples) * (file_doubles(i) - file_avg_doubles)
            file_co_variance_st = file_co_variance_st + (file_singles(i) - file_avg_singles) * (file_triples(i) - file_avg_triples)
        Next i

        file_stdev_singles = file_stdev_singles ^ 0.5 / (num_rts * (num_rts - 1)) ^ 0.5
        file_stdev_doubles = file_stdev_doubles ^ 0.5 / (num_rts * (num_rts - 1)) ^ 0.5
        file_stdev_triples = file_stdev_triples ^ 0.5 / (num_rts * (num_rts - 1)) ^ 0.5

        file_co_variance_sd = file_co_variance_sd / (num_rts * (num_rts - 1))
        file_co_variance_dt = file_co_variance_dt / (num_rts * (num_rts - 1))
        file_co_variance_st = file_co_variance_st / (num_rts * (num_rts - 1))

        ' *******  message boxes for debugging rates calcs, uncomment the msgbox to display rates
        Dim text_head, text1, text2, text3 As String

        text_head = "Average of cycle by cycle Rates " & vbCrLf
        text1 = "Singles:     " & Int(1000 * avg_singles) / 1000 & " +/- " & Int(1000 * stdev_singles) / 1000 & vbCrLf
        text2 = "Doubles:    " & Int(1000 * avg_doubles) / 1000 & " +/- " & Int(1000 * stdev_doubles) / 1000 & vbCrLf
        text3 = "Triples:     " & Int(1000 * avg_triples) / 1000 & " +/- " & Int(1000 * stdev_triples) / 1000
        'MsgBox(text_head & text1 & text2 & text3)

        text_head = " Summed Histogram rates" & vbCrLf
        text1 = "Singles:     " & Int(1000 * file_rates_out(1)) / 1000 & " +/- " & Int(1000 * file_rates_out(2)) / 1000 & vbCrLf
        text2 = "Doubles:    " & Int(1000 * file_rates_out(3)) / 1000 & " +/- " & Int(1000 * file_rates_out(4)) / 1000 & vbCrLf
        text3 = "Triples:     " & Int(1000 * file_rates_out(5)) / 1000 & " +/- " & Int(1000 * file_rates_out(6)) / 1000
        ' MsgBox(text_head & text1 & text2 & text3)


        text_head = " New Summed Histogram rates" & vbCrLf
        text1 = "Singles:     " & Int(1000 * new_rates_out(1)) / 1000 & " +/- " & Int(1000 * new_rates_out(2)) / 1000 & vbCrLf
        text2 = "Doubles:    " & Int(1000 * new_rates_out(3)) / 1000 & " +/- " & Int(1000 * new_rates_out(4)) / 1000 & vbCrLf
        text3 = "Triples:     " & Int(1000 * new_rates_out(5)) / 1000 & " +/- " & Int(1000 * new_rates_out(6)) / 1000
        ' MsgBox(text_head & text1 & text2 & text3)



        ' ****  save cycle by cycle rates to temporary file - temp_rates.csv ****

        Dim out_rates_file, klineout As String
        out_rates_file = "c:\multiplicity_tmu\temp_rates.csv"

        klineout = "Cycle by cycle rates , " & out_rates_file & " , " & vbCrLf

        Dim fs As FileStream = File.Create(out_rates_file)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        For i = 1 To max_cycles
            klineout = i & ", " & rates_array(i, 1) & ", " & rates_array(i, 2)
            klineout = klineout & ", " & rates_array(i, 3) & ", " & rates_array(i, 4)
            klineout = klineout & ", " & rates_array(i, 5) & ", " & rates_array(i, 6)
            klineout = klineout & ", " & cycle_hardware_fail(i)
            klineout = klineout & ", " & cycle_outlier_fail(i, 1) & ", " & cycle_outlier_fail(i, 2) & ", " & cycle_outlier_fail(i, 3) & ", " & cycle_outlier_fail(i, 4)
            klineout = klineout & vbCrLf

            My.Computer.FileSystem.WriteAllText(out_rates_file, klineout, True)

        Next i
        klineout = "******************" & vbCrLf

        My.Computer.FileSystem.WriteAllText(out_rates_file, klineout, True)

        For i = 1 To max_cycles
            klineout = i & ", " & file_singles(i)
            klineout = klineout & ", " & file_doubles(i)
            klineout = klineout & ", " & file_triples(i)
            klineout = klineout & ", " & file_rates_flag(i)

            klineout = klineout & vbCrLf

            My.Computer.FileSystem.WriteAllText(out_rates_file, klineout, True)

        Next i


        dec_date_pu = iso_date(1)
        dec_date_Am = iso_date(2)
        dec_date_cf = iso_date(3)

        Dim num_days As Integer
        '  num_days = assay_date - dec_date_pu
        Dim days As Double = DateDiff(DateInterval.Day, dec_date_pu, assay_date)
        Dim alpha_val(11)

        '  -------------------------------------------------------------------------------
        ' Decay correction isotopic data

        For i_out = 1 To 8
            new_iso(i_out) = iso_decay(dec_date_pu, dec_date_Am, dec_date_cf, assay_date, iso_val, iso_val_err, i_out, alpha_val, half_life, half_life_err)
            new_iso_err(i_out) = iso_decay(dec_date_pu, dec_date_Am, dec_date_cf, assay_date, iso_val, iso_val_err, i_out + 20, alpha_val, half_life, half_life_err)
        Next i_out

        If new_iso(8) > 0 Then new_iso(8) = 1
        new_iso_err(8) = 0

        ' calculate Pu240 effective per gram value

        pu_240_effective = 0
        pu_240_effective_err = 0
        pu_240_effective_err_trad = 0

        For i = 1 To 8
            pu_240_effective = pu_240_effective + m240_conv(i) * new_iso(i)
            pu_240_effective_err = pu_240_effective_err + (m240_conv(i) * new_iso_err(i)) ^ 2
            pu_240_effective_err = pu_240_effective_err + (m240_conv_err(i) * new_iso(i)) ^ 2
            pu_240_effective_err_trad = pu_240_effective_err_trad + (m240_conv(i) * new_iso_err(i)) ^ 2
        Next i

        pu_240_effective_err = pu_240_effective_err ^ 0.5
        pu_240_effective_err_trad = pu_240_effective_err_trad ^ 0.5

        If new_iso(8) > 0 Then pu_240_effective = 1
        If new_iso(8) > 0 Then pu_240_effective_err = 0
        If new_iso(8) > 0 Then pu_240_effective_err_trad = 0

        Dim alpha_adjust
        alpha_adjust = 1
        alpha_val_cal = calc_alpha(new_iso, new_iso_err, alpha_n_rate, alpha_n_err, m240_conv, spont_fiss_rate, 1)
        alpha_val_cal_err = calc_alpha(new_iso, new_iso_err, alpha_n_rate, alpha_n_err, m240_conv, spont_fiss_rate, 2)
        If geo_par(10) = "PuF4" Then alpha_val_cal = calc_alpha(new_iso, new_iso_err, f_alpha_n_rate, f_alpha_n_err, m240_conv, spont_fiss_rate, 1)
        If geo_par(10) = "PuF4" Then alpha_val_cal_err = calc_alpha(new_iso, new_iso_err, f_alpha_n_rate, f_alpha_n_err, m240_conv, spont_fiss_rate, 2)
        If geo_par(10) = "PuO2F2" Then alpha_val_cal = calc_alpha(new_iso, new_iso_err, f_alpha_n_rate, f_alpha_n_err, m240_conv, spont_fiss_rate, 1) / 2
        If geo_par(10) = "PuO2F2" Then alpha_val_cal_err = calc_alpha(new_iso, new_iso_err, f_alpha_n_rate, f_alpha_n_err, m240_conv, spont_fiss_rate, 2) / 2

        If new_iso(8) > 0 Then alpha_val_cal = 0
        If new_iso(8) > 0 Then alpha_val_cal_err = 0

        alpha_adjust = 1
        If geo_par(10) = "MOX Powder" Then alpha_adjust = 1.17
        If geo_par(10) = "MOX Pellets" Then alpha_adjust = 1
        If geo_par(10) = "PuF4" Then alpha_adjust = 1
        If geo_par(10) = "PuO2F2" Then alpha_adjust = 1
        If geo_par(10) = "metal" Then alpha_adjust = 0

        adjusted_alpha = alpha_adjust * alpha_val_cal
        adjusted_alpha_err = alpha_adjust * alpha_val_cal_err


        Dim file_rates(3), file_rates_err(3), file_covar(3, 3)


        new_rates(1) = avg_singles - file_pass_sing_bkg
        new_rates(2) = avg_doubles - file_pass_doub_bkg
        new_rates(3) = avg_triples - file_pass_trip_bkg
        new_rates_err(1) = stdev_singles
        new_rates_err(2) = stdev_doubles
        new_rates_err(3) = stdev_triples
        new_covar(1, 1) = stdev_singles ^ 2
        new_covar(1, 2) = co_variance_sd
        new_covar(1, 3) = co_variance_st
        new_covar(2, 1) = co_variance_sd
        new_covar(2, 2) = stdev_doubles ^ 2
        new_covar(2, 3) = co_variance_dt
        new_covar(3, 1) = co_variance_st
        new_covar(3, 2) = co_variance_dt
        new_covar(3, 3) = stdev_triples ^ 2

        mult_new = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 1)
        mult_new_err = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 2)
        alpha_new = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 3)
        alpha_new_err = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 4)
        m240_new = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)
        m240_new_err = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 6)

        ' calculate new rates uncertainties with rates uncertainties for covariances (fully correlated)
        new_covar(1, 2) = new_rates_err(1) * new_rates_err(2)
        new_covar(1, 3) = new_rates_err(1) * new_rates_err(3)
        new_covar(2, 1) = new_rates_err(2) * new_rates_err(1)
        new_covar(2, 3) = new_rates_err(2) * new_rates_err(3)
        new_covar(3, 1) = new_rates_err(3) * new_rates_err(1)
        new_covar(3, 2) = new_rates_err(3) * new_rates_err(2)
        mult_new_nocovar_err = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 2)
        alpha_new_nocovar_err = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 4)
        m240_new_nocovar_err = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 6)

        file_rates(1) = file_avg_singles - file_pass_sing_bkg
        file_rates(2) = file_avg_doubles - file_pass_doub_bkg
        file_rates(3) = file_avg_triples - file_pass_trip_bkg
        file_rates_err(1) = file_stdev_singles
        file_rates_err(2) = file_stdev_doubles
        file_rates_err(3) = file_stdev_triples
        file_covar(1, 1) = file_stdev_singles ^ 2
        file_covar(1, 2) = file_co_variance_sd
        file_covar(1, 3) = file_co_variance_st
        file_covar(2, 1) = file_co_variance_sd
        file_covar(2, 2) = file_stdev_doubles ^ 2
        file_covar(2, 3) = file_co_variance_dt
        file_covar(3, 1) = file_co_variance_st
        file_covar(3, 2) = file_co_variance_dt
        file_covar(3, 3) = file_stdev_triples ^ 2

        mult_file = mult_anal2(file_rates, file_rates_err, file_covar, det_par_val, fiss_const_val, 1)
        mult_file_err = mult_anal2(file_rates, file_rates_err, file_covar, det_par_val, fiss_const_val, 2)
        alpha_file = mult_anal2(file_rates, file_rates_err, file_covar, det_par_val, fiss_const_val, 3)
        alpha_file_err = mult_anal2(file_rates, file_rates_err, file_covar, det_par_val, fiss_const_val, 4)
        m240_file = mult_anal2(file_rates, file_rates_err, file_covar, det_par_val, fiss_const_val, 5)
        m240_file_err = mult_anal2(file_rates, file_rates_err, file_covar, det_par_val, fiss_const_val, 6)

        ' calculate file rates uncertainties with fully correlated covariance
        file_covar(1, 2) = file_rates_err(1) * file_rates_err(2)
        file_covar(1, 3) = file_rates_err(1) * file_rates_err(3)
        file_covar(2, 1) = file_rates_err(2) * file_rates_err(1)
        file_covar(2, 3) = file_rates_err(2) * file_rates_err(3)
        file_covar(3, 1) = file_rates_err(3) * file_rates_err(1)
        file_covar(3, 2) = file_rates_err(3) * file_rates_err(2)
        mult_file_nocovar_err = mult_anal2(file_rates, file_rates_err, file_covar, det_par_val, fiss_const_val, 2)
        alpha_file_nocovar_err = mult_anal2(file_rates, file_rates_err, file_covar, det_par_val, fiss_const_val, 4)
        m240_file_nocovar_err = mult_anal2(file_rates, file_rates_err, file_covar, det_par_val, fiss_const_val, 6)

        ' Cacluate total Pu mass

        Final_mass_new = m240_new / pu_240_effective
        final_mass_err_new = ((m240_new / pu_240_effective ^ 2 * pu_240_effective_err) ^ 2 + (m240_new_err / pu_240_effective) ^ 2) ^ 0.5
        final_mass_nocovar_err_new = ((m240_new / pu_240_effective ^ 2 * pu_240_effective_err) ^ 2 + (m240_new_nocovar_err / pu_240_effective) ^ 2) ^ 0.5

        final_mass_err_new_trad = ((m240_new / pu_240_effective ^ 2 * pu_240_effective_err_trad) ^ 2 + (m240_new_err / pu_240_effective) ^ 2) ^ 0.5                      ' traditional error no error contribution from nuclear data
        final_mass_nocovar_err_new_trad = ((m240_new / pu_240_effective ^ 2 * pu_240_effective_err_trad) ^ 2 + (m240_new_nocovar_err / pu_240_effective) ^ 2) ^ 0.5      ' traditional error no error contribution from nuclear data


        Final_mass_file = m240_file / pu_240_effective

        final_mass_err_file = ((m240_file / pu_240_effective ^ 2 * pu_240_effective_err) ^ 2 + (m240_file_err / pu_240_effective) ^ 2) ^ 0.5
        final_mass_nocovar_err_file = ((m240_file / pu_240_effective ^ 2 * pu_240_effective_err) ^ 2 + (m240_file_nocovar_err / pu_240_effective) ^ 2) ^ 0.5

        final_mass_err_file_trad = ((m240_file / pu_240_effective ^ 2 * pu_240_effective_err_trad) ^ 2 + (m240_file_err / pu_240_effective) ^ 2) ^ 0.5                      ' traditional error no error contribution from nuclear data
        final_mass_nocovar_err_file_trad = ((m240_file / pu_240_effective ^ 2 * pu_240_effective_err_trad) ^ 2 + (m240_file_nocovar_err / pu_240_effective) ^ 2) ^ 0.5      ' traditional error no error contribution from nuclear data

        '
        ' *************  Examine covariances by jittering the revised summed histograms     ****************


        Dim i_runs As Integer
        n_jitter = misc_param(4)
        Call calc_rates_jitter(new_num_A_R, new_num_Acc, new_max_bin_A_R, new_max_bin_Acc, det_par_val, grunt_time(2), n_jitter)

        jitter_rates(1) = jitter_out_rates(1)
        jitter_rates(2) = jitter_out_rates(3)
        jitter_rates(3) = jitter_out_rates(5)
        jitter_rates_err(1) = jitter_out_rates(2)
        jitter_rates_err(2) = jitter_out_rates(4)
        jitter_rates_err(3) = jitter_out_rates(6)
        jitter_rates_covar(1, 1) = jitter_out_rates(2) ^ 2
        jitter_rates_covar(1, 2) = jitter_out_rates(11)
        jitter_rates_covar(1, 3) = jitter_out_rates(13)

        jitter_rates_covar(2, 1) = jitter_out_rates(11)
        jitter_rates_covar(2, 2) = jitter_out_rates(4) ^ 2
        jitter_rates_covar(2, 3) = jitter_out_rates(12)

        jitter_rates_covar(3, 1) = jitter_out_rates(13)
        jitter_rates_covar(3, 2) = jitter_out_rates(12)
        jitter_rates_covar(3, 3) = jitter_out_rates(6) ^ 2


        'MsgBox(jitter_rates_err(1) & " : " & jitter_rates_err(2) & " : " & jitter_rates_err(3))
        'MsgBox(jitter_rates_covar(1, 1) & " : " & jitter_rates_covar(1, 2) & " : " & jitter_rates_covar(1, 3))

        '
        '  *************  Calculate Error Contributions from detector parameters    ***********************

        For i = 1 To 10
            det_comp(i) = mult_anal_err2(new_num_A_R, new_num_Acc, new_max_bin_A_R, new_max_bin_Acc, det_par_val, det_par_err, fiss_const_val, fiss_const_err, grunt_time(2), 3, i, 1)
        Next i

        det_err_sum = 0
        For i = 1 To 10
            det_err_sum = det_err_sum + (det_comp(i)) ^ 2

            det_err_comp(i) = Abs(det_comp(i))
        Next i
        det_err_sum = det_err_sum ^ 0.5

        '
        '  *************  Calculate Fission Moment Contributions to Error     ***********************
        '
        Dim covar_fiss

        For i = 1 To 7
            fiss_mom_comp(i) = mult_anal_err2(new_num_A_R, new_num_Acc, new_max_bin_A_R, new_max_bin_Acc, det_par_val, det_par_err, fiss_const_val, fiss_const_err, grunt_time(2), 3, i, 2)
        Next i

        covar_fiss = 0
        For i = 1 To 7

            For j = 1 To 7
                If (fiss_covar(i, i) * fiss_covar(j, j)) <> 0 Then covar_fiss = covar_fiss + (fiss_mom_comp(i) * fiss_mom_comp(j)) * fiss_covar(i, j) / (fiss_covar(i, i) * fiss_covar(j, j)) ^ 0.5
            Next j

        Next i

        covar_fiss = covar_fiss ^ 0.5

        fiss_mom_err_sum = covar_fiss

        '
        '  *************  Positioning Contributions to Error  - Radial Offset   ***********************
        '
        '  Note this approach is only applicable to uniformly distributed materials within the container
        '  
        ' 
        Dim cavity_ID, container_IDiam, Radial_offset, radial_sensitivity, Avg_radial_offset, max_rad_tol, typical_rad_offset
        Dim cavity_H
        '
        '
        Call calc_radial_error()
        '
        '  ************* Fill Height Contributions to Error     ***********************
        '
        Call calc_axial_errors()
        '
        '  ************* Density Contributions to Error     ***********************
        '
        ' bias is a function of mass and density
        ' 
        Dim ref_rho, min_rho, max_rho_bias, ref_bias_mass, ref_rho_dev, rho_sigma, typ_rho_bias
        Dim min_rho_bias, typ_rho, max_rho, delta_rho

        ref_rho = det_cal_ref_density
        typ_rho = geo_par(14)
        min_rho = geo_par(15)
        max_rho = geo_par(16)

        typ_rho_bias = dev_rho_par(1)
        ref_bias_mass = dev_rho_par(2)
        '  ref_rho_dev = dev_rho_par(3)
        ' rho_sigma = dev_rho_par(4)


        ref_bias_mass = dev_rho_par(5)
        delta_rho = ref_rho - typ_rho

        rel_rho_bias = dev_rho_par(1) * (Final_mass_new / ref_bias_mass / typ_rho) ^ dev_rho_par(2) * (1 + dev_rho_par(3) * Exp(-dev_rho_par(4) * typ_rho)) * delta_rho

        delta_rho = ref_rho - min_rho
        min_rho_bias = dev_rho_par(1) * (Final_mass_new / ref_bias_mass / min_rho) ^ dev_rho_par(2) * (1 + dev_rho_par(3) * Exp(-dev_rho_par(4) * typ_rho)) * delta_rho

        delta_rho = ref_rho - max_rho
        max_rho_bias = dev_rho_par(1) * (Final_mass_new / ref_bias_mass / max_rho) ^ dev_rho_par(2) * (1 + dev_rho_par(3) * Exp(-dev_rho_par(4) * typ_rho)) * delta_rho

        rel_rho_rand = Abs(max_rho_bias - min_rho_bias) / 2

        mult_corr_fact = 1 + mult_corr(1) * (mult_new - 1) + mult_corr(2) * (mult_new - 1) ^ 2

        For i = 1 To 10
            det_val_mult(i) = det_par_val(i)
        Next

        det_val_mult(1) = vol_average_fill_efficiency


        m_bias_pt = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 1)
        m_bias_fill = mult_anal2(new_rates, new_rates_err, new_covar, det_val_mult, fiss_const_val, 1)

        mult_mass_bias = m_bias_fill * mult_corr_fact



        ''******************************************************************************************************
        '
        '  ************* Alpha,n  Contribution to Error     ***********************
        '
        ''******************************************************************************************************
        Dim delta_alpha, min_alpha_bias, max_alpha_bias
        '  The contribution is estimated relative to the calibration item characteristic alpha
        '  need to factor in uncertainty in alpha for large alpha/large uncertainty - alpha_new_err
        Dim dev_alpha_par1(3), ref_alpha_bias
        rel_alpha_random = 0
        rel_alpha_bias = 0
        dev_alpha_index = 4
        For i = 1 To 5
            If item_impurity_flag(i) = 1 Then dev_alpha_index = i                  ' determine which impurity is present - if none use oxygen
            If item_impurity_flag(i) = 1 Then GoTo 50
        Next i
50:

        For i = 1 To 3
            dev_alpha_par1(i) = dev_alpha_par(3 * (dev_alpha_index - 1) + i)
        Next

        ref_alpha_bias = dev_alpha_par1(1) + dev_alpha_par1(2) * (Final_mass_new) ^ dev_alpha_par1(3) * det_cal_alpha
        rel_alpha_bias = dev_alpha_par1(1) + dev_alpha_par1(2) * (Final_mass_new) ^ dev_alpha_par1(3) * alpha_new
        rel_alpha_bias = rel_alpha_bias - ref_alpha_bias

        rel_alpha_random = dev_alpha_par1(2) * (Final_mass_new) ^ dev_alpha_par1(3) * alpha_new_err
        '
        ''******************************************************************************************************
        '
        '  ************* Burn-Up Contribution to Error     ***********************
        '
        ''******************************************************************************************************
        Dim delta_burnup

        delta_burnup = (pu_240_effective - det_cal_pu240_eff)
        rel_burnup_bias = dev_pu240_par(1) + dev_pu240_par(2) * delta_burnup * alpha_new
        '
        ''******************************************************************************************************
        '
        '  ************* UPu Ratio Contribution to Error     ***********************
        '
        ''******************************************************************************************************

        Dim delta_upu, min_upu_bias, max_upu_bias, mass_dependence, m_ratio


        rel_upu_random = 0
        rel_UPu_bias = 0

        delta_upu = geo_par(11) - det_cal_UPu_ratio
        If geo_par(11) = 0 Then m_ratio = 0 Else m_ratio = m240_new / pu_240_effective / dev_UPu_par(5)
        rel_UPu_bias = (dev_UPu_par(1) + dev_UPu_par(2) * m_ratio + dev_UPu_par(3) * m_ratio ^ 2) * (delta_upu / dev_UPu_par(4)) ^ 0.75

        delta_upu = geo_par(13) - det_cal_UPu_ratio

        max_upu_bias = (dev_UPu_par(1) + dev_UPu_par(2) * m_ratio + dev_UPu_par(3) * m_ratio ^ 2) * (delta_upu / dev_UPu_par(4)) ^ 0.75

        delta_upu = geo_par(12) - det_cal_UPu_ratio
        min_upu_bias = (dev_UPu_par(1) + dev_UPu_par(2) * m_ratio + dev_UPu_par(3) * m_ratio ^ 2) * (delta_upu / dev_UPu_par(4)) ^ 0.75

        rel_upu_random = (max_upu_bias - min_upu_bias) / 4

        '
        ''******************************************************************************************************
        '
        '  ************* Moderator Contribution to Error     ***********************
        '
        ''******************************************************************************************************
        Dim delta_mod, max_mod_bias, min_mod_bias

        rel_mod_random = 0
        rel_mod_bias = 0

        delta_mod = (geo_par(17) - det_cal_mod) / 100
        rel_mod_bias = dev_mod_par(1) + dev_mod_par(2) * delta_mod + dev_mod_par(3) * delta_mod ^ 2


        delta_mod = (geo_par(19) - geo_par(17)) / 100
        max_mod_bias = dev_mod_par(1) + dev_mod_par(2) * delta_mod + dev_mod_par(3) * delta_mod ^ 2


        delta_mod = (geo_par(18) - geo_par(17)) / 100
        min_mod_bias = dev_mod_par(1) + dev_mod_par(2) * delta_mod + dev_mod_par(3) * delta_mod ^ 2

        rel_mod_random = (max_mod_bias - min_mod_bias) / 4
        '

        '
        ''******************************************************************************************************
        '
        '                        Wall Effect Bias
        '
        ''******************************************************************************************************
        Dim item_wall, cal_wall, wall_eff
        Dim det_val_wall(10), m_bias_wall, m_bias_standard


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

        rel_wall_effect_bias = (m_bias_wall - m_bias_standard) / m_bias_standard                                          ' relative bias due to wall effect (thicker wall -> greater bias)
        '
        '
    End Sub


    Public Property FileName As String
    Public Sub Get_summed_histogram()
        Dim fiss_const_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\multiplicity_tmu\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    fiss_const_file_name = openFileDialog1.FileName

                End If
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            Finally
                ' Check this again, since we need to make sure we didn't throw an exception on open.
                If (myStream IsNot Nothing) Then
                    myStream.Close()
                End If
            End Try
        End If

        Call get_fiss_const_file(fiss_const_file_name)


    End Sub
    Sub calc_rates_jitter(A_R_i, ACCID_i, A_R_len, ACC_len, det_parms, assay_time, i_runs)
        Dim alph(512), bet(512), mr(512)
        Dim p_i(512), q_i(256), r_1(512), r_2(256), A_R(512), ACCID(512)
        Dim r_3(512), gam(512), g_2(512)
        Dim sing(8000), doub(8000), trip(8000), quad(8000)
        Dim m_rates(10)


        Dim a_r_sum, Phi, doub_err_1, sum_ar, sum_acc, r_norm
        Dim s_err, d_err, t_err
        Dim singles, doubles, triples, quads
        Dim sing_stdev, doub_stdev, trip_stdev, quad_stdev
        Dim co_var_sd, co_var_dt, co_var_st

        Dim num_bins As Integer

        dt_par_a = det_parms(6) / 1000000.0#
        dt_par_b = det_parms(7) / 1000000000000.0#
        dt_par_c = det_parms(8) / 1000000000.0#
        dt_par_d = det_parms(9) / 1000000000.0#
        dead_time_parm = det_parms(10)

        die_away_t = det_parms(2)
        gate_width = det_parms(3)

        a_r_sum = 0
        For i = 1 To 256
            A_R(i) = 0
            ACCID(i) = 0
            a_r_sum = a_r_sum + A_R(i)
        Next i

        num_bins = ACC_len
        If A_R_len > ACC_len Then num_bins = A_R_len

        Phi = dead_time_parm / gate_width / 1000

        Call calc_M_rates(m_rates, A_R_i, ACCID_i, A_R_len, ACC_len, det_parms, assay_time)

        doub_err_1 = m_rates(2) / m_rates(1)

        For ir = 1 To i_runs

            sum_ar = 0
            sum_acc = 0

            For i = 1 To num_bins

                A_R(i) = A_R_i(i) + (A_R_i(i) ^ 0.5) * NormSInv(Rnd())
                ACCID(i) = ACCID_i(i) + (ACCID_i(i) ^ 0.5) * NormSInv(Rnd())
                sum_ar = sum_ar + A_R_i(i)
                sum_acc = sum_acc + ACCID_i(i)

            Next i
            Dim rand1, norm1
            rand1 = Rnd()
            norm1 = NormSInv(rand1)
            r_norm = 1 + norm1 * doub_err_1

            For i = 1 To num_bins
                A_R(i) = A_R(i) * r_norm
                ACCID(i) = ACCID(i) * r_norm
            Next i

            Call calc_M_rates(m_rates, A_R, ACCID, A_R_len, ACC_len, det_parms, assay_time)

            sing(ir) = m_rates(1) * r_norm
            doub(ir) = m_rates(3) * r_norm
            trip(ir) = m_rates(5) * r_norm
            quad(ir) = m_rates(7) * r_norm

            '
            s_err = m_rates(2)
            d_err = m_rates(4)
            t_err = m_rates(6)

            '  Singles and doubles error from INCC software manual
            '  Triples error from Croft et.al. A' Priori Precision estimates for Neutron Triples Counting, ANIMMA 6-9 June 2011 Ghent Belgium
            '
            '       s_err = (1 + 2 * doubles / f_d / singles) ^ 0.5 * sum_acc ^ 0.5 / assay_time
            '       d_err = s_d_mult * ((singles ^ 2 * gate_width * 0.000001 * 2 + doubles) / assay_time) ^ 0.5
            '       t_err = (1 + 8 * f_t * doubles / f_d / singles) ^ 0.5 * ((triples + 2 * A_T) / assay_time) ^ 0.5

        Next ir

        singles = 0
        doubles = 0
        triples = 0
        quads = 0
        sing_stdev = 0
        doub_stdev = 0
        trip_stdev = 0
        quad_stdev = 0
        co_var_sd = 0
        co_var_dt = 0
        co_var_st = 0

        For i = 1 To i_runs
            singles = singles + sing(i)
            doubles = doubles + doub(i)
            triples = triples + trip(i)
            quads = quads + quad(i)
        Next i

        singles = singles / i_runs
        doubles = doubles / i_runs
        triples = triples / i_runs
        quads = quads / i_runs

        '    Gamma = 1 - (1 - Exp(-gate_width / die_away_t)) / (gate_width / die_away_t)
        '    theo_fd = (1 - Exp(-gate_width / die_away_t)) / Exp(-4.5 / die_away_t)
        '    sing_err_mult = (1 + 2 * doubles / singles / theo_fd) ^ 0.5
        '    doubl_err_mult = (1 + doubles / singles * 8 * Gamma / theo_fd) ^ 0.5

        For i = 1 To i_runs
            sing_stdev = sing_stdev + (singles - sing(i)) ^ 2
            doub_stdev = doub_stdev + (4 / 3 * (doubles - doub(i))) ^ 2
            trip_stdev = trip_stdev + (triples - trip(i)) ^ 2
            quad_stdev = quad_stdev + (quads - quad(i)) ^ 2
            co_var_sd = co_var_sd + (sing(i) - singles) * 4 / 3 * (doub(i) - doubles)
            co_var_dt = co_var_dt + (trip(i) - triples) * 4 / 3 * (doub(i) - doubles)
            co_var_st = co_var_st + (sing(i) - singles) * (trip(i) - triples)
        Next i

        jitter_out_rates(1) = singles
        jitter_out_rates(2) = (sing_stdev / (i_runs - 1)) ^ 0.5
        jitter_out_rates(3) = doubles
        jitter_out_rates(4) = (doub_stdev / (i_runs - 1)) ^ 0.5
        jitter_out_rates(5) = triples
        jitter_out_rates(6) = (trip_stdev / (i_runs - 1)) ^ 0.5
        jitter_out_rates(7) = quads
        jitter_out_rates(8) = (quad_stdev / (i_runs - 1)) ^ 0.5
        jitter_out_rates(11) = co_var_sd / (i_runs - 1)
        jitter_out_rates(12) = co_var_dt / (i_runs - 1)
        jitter_out_rates(13) = co_var_st / (i_runs - 1)


    End Sub

    Function calc_rates(A_R, ACCID, A_R_len, ACC_len, det_parms, assay_time, i_out)
        Dim alph(512), bet(512), mr(512)
        Dim p_i(512), q_i(512), r_1(512), r_2(512), r_3(512), gam(512), g_2(512)
        Dim out_rates(10), count_time

        Dim a_r_sum, Phi, doub_err_1, r_norm
        Dim C_S, C_RA, C_A
        Dim s_err, d_err, t_err, sum_ar, sum_acc
        Dim singles, doubles, triples, quads
        Dim comb_q, g_temp, sum_r_1, sum_r_2, sum_r_3, sum_r_4
        Dim mn0, mn1, mn2, mn3, mn4, mn5
        Dim mb0, mb1, mb2, mb3, mb4, mb5
        Dim mr0, mr1, mr2, mr3, mr4, mr5
        Dim mlt_totals, dead_time_factor, doubles_dt_factor, triples_dt_factor, quads_dt_factor
        Dim g_w, Gamma, s_d_mult, A_T

        Dim num_bins, i As Integer

        dt_par_a = det_parms(6) / 1000000.0#              ' us to seconds
        dt_par_b = det_parms(7) / 1000000000000.0#        ' uus to 1e-12 seconds
        dt_par_c = det_parms(8) / 1000000000.0#           ' ns
        dt_par_d = det_parms(9) / 1000000000.0#           ' ns
        dead_time_parm = det_parms(10)

        die_away_t = det_parms(2)
        gate_width = det_parms(3)
        f_d = det_parms(4)
        f_t = det_parms(5)

        sum_ar = 0
        sum_acc = 0

        num_bins = ACC_len
        If A_R_len > ACC_len Then num_bins = A_R_len

        C_S = 0
        C_RA = 0
        C_A = 0

        For i = 1 To num_bins
            sum_ar = sum_ar + A_R(i)
            sum_acc = sum_acc + ACCID(i)
            C_S = C_S + ACCID(i)                           ' for INCC style rates
            C_RA = C_RA + (i - 1) * A_R(i)                       ' for INCC style reals rates
            C_A = C_A + (i - 1) * ACCID(i)                       ' for INCC style reals rates
        Next i

        Phi = dead_time_parm / gate_width / 1000

        For i = 1 To num_bins
            p_i(i - 1) = A_R(i) / sum_ar
            q_i(i - 1) = ACCID(i) / sum_acc
            alph(i) = (1 + (i - 1) * Phi + 3 / 2 * (i) ^ 2 * Phi ^ 2 + 5 / 2 * (i - 1) ^ 3 * Phi ^ 3 + 7 / 2 * (i - 1) ^ 4 * Phi ^ 4) * i
            bet(i) = (alph(i) / i) ^ 2 * (i * (i - 1) / 2)
        Next i
        gam(1) = 0
        gam(2) = 0
        gam(3) = bet(3) - (alph(3) - 1)
        '
        For i = 4 To num_bins
            gam(i) = bet(i) - (alph(i) - 1)

            For k = 0 To i - 4
                comb_q = Combinatorial(i - 1, k + 3)
                g_temp = (k + 1) * (k + 2) * (k + 3) ^ k * Phi ^ k / (1 - (k + 3) * Phi) ^ (k + 4)
                gam(i) = gam(i) + comb_q / 2 * g_temp
            Next k

        Next i


        sum_r_1 = 0
        sum_r_2 = 0
        sum_r_3 = 0
        sum_r_4 = 0
        '
        For i = 1 To num_bins
            r_1(i) = alph(i) * (p_i(i) - q_i(i))
            sum_r_1 = sum_r_1 + r_1(i)
        Next i
        '
        For i = 1 To num_bins
            r_2(i) = ((bet(i) * (p_i(i) - q_i(i))) - sum_r_1 * (alph(i) * q_i(i)))
            sum_r_2 = sum_r_2 + r_2(i)
        Next i

        mn0 = 0
        mn1 = 0
        mn2 = 0
        mn3 = 0
        mn4 = 0
        mn5 = 0
        mb0 = 0
        mb1 = 0
        mb2 = 0
        mb3 = 0
        mb4 = 0
        mb5 = 0
        mr0 = 0
        mr1 = 0
        mr2 = 0
        mr3 = 0
        mr4 = 0
        mr5 = 0


        For i = 1 To num_bins
            mr(i) = p_i(i) - q_i(i)
            mn0 = mn0 + p_i(i)
            mn1 = mn1 + p_i(i) * alph(i)
            mn2 = mn2 + p_i(i) * bet(i)
            mn3 = mn3 + p_i(i) * gam(i)
            mn4 = mn4 + p_i(i) * 0
            mn5 = mn5 + p_i(i) * 0
            mb0 = mb0 + q_i(i)
            mb1 = mb1 + q_i(i) * alph(i)
            mb2 = mb2 + q_i(i) * bet(i)
            mb3 = mb3 + q_i(i) * gam(i)
            mb4 = mb4 + q_i(i) * 0
            mb5 = mb5 + q_i(i) * 0
            mr0 = mr0 + mr(i)
            mr1 = mr1 + mr(i) * alph(i)
            mr2 = mr2 + mr(i) * bet(i)
            mr3 = mr3 + mr(i) * gam(i)
            mr4 = mr4 + mr(i) * 0
            mr5 = mr5 + mr(i) * 0
        Next i

        g_w = gate_width * 0.000001
        mlt_totals = sum_acc / assay_time
        count_time = sum_acc / mlt_totals
        dead_time_factor = Exp(dead_time_parm * 10 ^ -9 * mlt_totals)
        doubles_dt_factor = Exp(mlt_totals * dt_par_c)
        triples_dt_factor = Exp(mlt_totals * dt_par_d)
        quads_dt_factor = Exp(mlt_totals * dt_par_d)

        'gate_width
        Dim LANL_S_DT, LANL_D_DT, LANL_T_DT
        Dim proper_S_DT, proper_D_DT, proper_T_DT

        proper_S_DT = dead_time_factor                                          ' calc singles deadtime 
        proper_D_DT = dead_time_factor * doubles_dt_factor                      ' calc doubles deadtime
        proper_T_DT = dead_time_factor * triples_dt_factor                      ' calc triples deadtime


        LANL_S_DT = Exp(mlt_totals * (dt_par_a + dt_par_b * mlt_totals) / 4)    ' calc totals rate deadtime 
        LANL_D_DT = Exp(mlt_totals * (dt_par_a + dt_par_b * mlt_totals))        ' calc reals rate deadtime 
        LANL_T_DT = dead_time_factor * (1 + mlt_totals * dt_par_c)              ' calc additinal deadtime for triples

        If use_LANL_DT_flag Then singles = C_S / assay_time * LANL_S_DT Else singles = mlt_totals * proper_S_DT
        If use_LANL_DT_flag Then doubles = (C_RA - C_A) / assay_time * LANL_D_DT Else doubles = sum_r_1 * mlt_totals * proper_D_DT
        If use_LANL_DT_flag Then triples = sum_r_2 * mlt_totals * LANL_T_DT Else triples = sum_r_2 * mlt_totals * proper_T_DT
        '    doubles = sum_r_1 * mlt_totals * dead_time_factor * doubles_dt_factor
        '    triples = sum_r_2 * mlt_totals * dead_time_factor * triples_dt_factor 
        g_w = gate_width * 0.000001

        'singles = mb1 / g_w * dead_time_factor
        'doubles = mb1 / g_w * mr1 * doubles_dt_factor * dead_time_factor
        'triples = mb1 / g_w * (mr2 - mb1 * mr1) * triples_dt_factor * dead_time_factor
        quads = mb1 / g_w * (mr3 - mb2 * mr1 - mb1 * (mr2 - mb1 * mr1)) * quads_dt_factor * dead_time_factor

        Gamma = 1 - (1 - Exp(-gate_width / die_away_t)) / (gate_width / die_away_t)
        s_d_mult = (1 + 8 * Gamma * doubles / f_d / singles) ^ 0.5
        A_T = (1 + f_t / f_d) * singles * doubles * gate_width * 0.000001 + 0.5 * singles * (singles * gate_width * 0.000001) ^ 2
        '
        '  Singles and doubles error from INCC software manual
        '  Triples error from Croft et.al. A' Priori Precision estimates for Neutron Triples Counting, ANIMMA 6-9 June 2011 Ghent Belgium
        '

        s_err = (1 + 2 * doubles / f_d / singles) ^ 0.5 * sum_acc ^ 0.5 / count_time
        d_err = s_d_mult * ((singles ^ 2 * gate_width * 0.000001 * 2 + doubles) / count_time) ^ 0.5
        t_err = Sqrt(2 * (singles ^ 3 * (gate_width * 0.000001) ^ 2 + triples) / assay_time)
        '' t_err = 2 * ((singles ^ 3 * gate_width ^ 2 * 0.000001 ^ 2 + 2.5 * triples) / assay_time) ^ 0.5
        ' t_err = (1 + 8 * f_t * doubles / f_d / singles) ^ 0.5 * ((triples + 2 * A_T) / count_time) ^ 0.5
        ' Gamma = 1 - (1 - Exp(-gate_width / die_away_t)) / (gate_width / die_away_t)
        '  t_err = Sqrt((2 * singles ^ 3 * (gate_width * 0.000001) ^ 2 + triples) / assay_time) * Sqrt(2 + 0 * Gamma * doubles / singles / f_d)

        out_rates(1) = singles
        out_rates(2) = s_err
        out_rates(3) = doubles
        out_rates(4) = d_err
        out_rates(5) = triples
        out_rates(6) = t_err
        out_rates(7) = quads
        out_rates(9) = sum_ar
        out_rates(10) = sum_acc

        calc_rates = out_rates(i_out)

    End Function


    Sub calc_M_rates(out_rates, A_R, ACCID, A_R_len, ACC_len, det_parms, assay_time)
        ' used in jittered analysis
        Dim alph(256), bet(256), mr(256)
        Dim p_i(256), q_i(256), r_1(256), r_2(256), r_3(256), gam(256), g_2(256)

        Dim a_r_sum, Phi, doub_err_1, sum_ar, sum_acc, r_norm
        Dim s_err, d_err, t_err
        Dim singles, doubles, triples, quads
        Dim comb_q, g_temp, sum_r_1, sum_r_2, sum_r_3, sum_r_4
        Dim mn0, mn1, mn2, mn3, mn4, mn5
        Dim mb0, mb1, mb2, mb3, mb4, mb5
        Dim mr0, mr1, mr2, mr3, mr4, mr5
        Dim mlt_totals, dead_time_factor, doubles_dt_factor, triples_dt_factor, quads_dt_factor
        Dim g_w, Gamma, s_d_mult, A_T

        Dim num_bins As Integer

        dt_par_a = det_parms(6) / 1000000.0#
        dt_par_b = det_parms(7) / 1000000000000.0#
        dt_par_c = det_parms(8) / 1000000000.0#
        dt_par_d = det_parms(9) / 1000000000.0#
        dead_time_parm = det_parms(10)

        die_away_t = det_parms(2)
        gate_width = det_parms(3)
        f_d = det_parms(4)
        f_t = det_parms(5)

        sum_ar = 0
        sum_acc = 0

        num_bins = ACC_len
        If A_R_len > ACC_len Then num_bins = A_R_len

        For i = 1 To num_bins
            sum_ar = sum_ar + A_R(i)
            sum_acc = sum_acc + ACCID(i)
        Next i

        Phi = dead_time_parm / gate_width / 1000

        For i = 1 To num_bins
            p_i(i - 1) = A_R(i) / sum_ar
            q_i(i - 1) = ACCID(i) / sum_acc
            alph(i) = (1 + (i - 1) * Phi + 3 / 2 * (i) ^ 2 * Phi ^ 2 + 5 / 2 * (i - 1) ^ 3 * Phi ^ 3 + 7 / 2 * (i - 1) ^ 4 * Phi ^ 4) * i
            bet(i) = (alph(i) / i) ^ 2 * (i * (i - 1) / 2)
        Next i
        gam(1) = 0
        gam(2) = 0
        gam(3) = bet(3) - (alph(3) - 1)
        '
        For i = 4 To num_bins
            gam(i) = bet(i) - (alph(i) - 1)

            For k = 0 To i - 4
                comb_q = Combinatorial(i - 1, k + 3)
                g_temp = (k + 1) * (k + 2) * (k + 3) ^ k * Phi ^ k / (1 - (k + 3) * Phi) ^ (k + 4)
                gam(i) = gam(i) + comb_q / 2 * g_temp
            Next k

        Next i


        sum_r_1 = 0
        sum_r_2 = 0
        sum_r_3 = 0
        sum_r_4 = 0
        '
        For i = 1 To num_bins
            r_1(i) = alph(i) * (p_i(i) - q_i(i))
            sum_r_1 = sum_r_1 + r_1(i)
        Next i
        '
        For i = 1 To num_bins
            r_2(i) = 2 * ((bet(i) * (p_i(i) - q_i(i))) - sum_r_1 * (alph(i) * q_i(i)))
            sum_r_2 = sum_r_2 + r_2(i)
        Next i

        mn0 = 0
        mn1 = 0
        mn2 = 0
        mn3 = 0
        mn4 = 0
        mn5 = 0
        mb0 = 0
        mb1 = 0
        mb2 = 0
        mb3 = 0
        mb4 = 0
        mb5 = 0
        mr0 = 0
        mr1 = 0
        mr2 = 0
        mr3 = 0
        mr4 = 0
        mr5 = 0


        For i = 1 To num_bins
            mr(i) = p_i(i) - q_i(i)
            mn0 = mn0 + p_i(i)
            mn1 = mn1 + p_i(i) * alph(i)
            mn2 = mn2 + p_i(i) * bet(i)
            mn3 = mn3 + p_i(i) * gam(i)
            mn4 = mn4 + p_i(i) * 0
            mn5 = mn5 + p_i(i) * 0
            mb0 = mb0 + q_i(i)
            mb1 = mb1 + q_i(i) * alph(i)
            mb2 = mb2 + q_i(i) * bet(i)
            mb3 = mb3 + q_i(i) * gam(i)
            mb4 = mb4 + q_i(i) * 0
            mb5 = mb5 + q_i(i) * 0
            mr0 = mr0 + mr(i)
            mr1 = mr1 + mr(i) * alph(i)
            mr2 = mr2 + mr(i) * bet(i)
            mr3 = mr3 + mr(i) * gam(i)
            mr4 = mr4 + mr(i) * 0
            mr5 = mr5 + mr(i) * 0
        Next i


        mlt_totals = sum_acc / assay_time
        dead_time_factor = Exp(dead_time_parm * 10 ^ -9 * mlt_totals)
        doubles_dt_factor = Exp(mlt_totals * dt_par_c)
        triples_dt_factor = Exp(mlt_totals * dt_par_d)
        quads_dt_factor = Exp(mlt_totals * dt_par_d)

        'gate_width
        'singles = mlt_totals / gate_width * dead_time_factor
        'doubles = sum_r_1 * mlt_totals * dead_time_factor * doubles_dt_factor
        'triples = sum_r_2 * mlt_totals * dead_time_factor * triples_dt_factor / 2
        g_w = gate_width * 0.000001
        singles = mb1 / g_w     ' * dead_time_factor
        doubles = mb1 / g_w * mr1 * doubles_dt_factor   '* dead_time_factor
        triples = mb1 / g_w * (mr2 - mb1 * mr1) * triples_dt_factor     ' * dead_time_factor
        quads = mb1 / g_w * (mr3 - mb2 * mr1 - mb1 * (mr2 - mb1 * mr1)) * quads_dt_factor '* dead_time_factor

        Gamma = 1 - (1 - Exp(-gate_width / die_away_t)) / (gate_width / die_away_t)
        s_d_mult = (1 + 8 * Gamma * doubles / f_d / singles) ^ 0.5
        A_T = (1 + f_t / f_d) * singles * doubles * gate_width * 0.000001 + 0.5 * singles * (singles * gate_width * 0.000001) ^ 2
        '
        '  Singles and doubles error from INCC software manual
        '  Triples error from Croft et.al. A' Priori Precision estimates for Neutron Triples Counting, ANIMMA 6-9 June 2011 Ghent Belgium
        '
        s_err = (1 + 2 * doubles / f_d / singles) ^ 0.5 * sum_acc ^ 0.5 / assay_time
        d_err = s_d_mult * ((singles ^ 2 * gate_width * 0.000001 * 2 + doubles) / assay_time) ^ 0.5
        't_err = 2 * ((singles ^ 3 * gate_width ^ 2 * 0.000001 ^ 2 + 2.5 * triples) / assay_time) ^ 0.5
        t_err = (1 + 8 * f_t * doubles / f_d / singles) ^ 0.5 * ((triples + 2 * A_T) / assay_time) ^ 0.5

        out_rates(1) = singles
        out_rates(2) = s_err
        out_rates(3) = doubles
        out_rates(4) = d_err
        out_rates(5) = triples
        out_rates(6) = t_err
        out_rates(7) = quads
        out_rates(8) = 0

    End Sub

    Public Sub get_iso_par_file(det_file)
        Dim myStream As Stream = Nothing

        Dim str11, str12, str13, str14 As String
        Dim idex, jdex As Integer
        Dim det_par_file_name As String
        If det_file = "" Then Return
        det_par_file_name = det_file

        idex = 0
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(det_par_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    idex = idex + 1

                    jdex = 0
                    Dim currentField As String
                    For Each currentField In currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        ' 
                        If jdex = 1 And idex > 1 And idex < 10 Then half_life(idex - 1) = Val(str11)
                        If jdex = 2 And idex > 1 And idex < 10 Then half_life_err(idex - 1) = Val(str11)

                        If jdex = 3 And idex > 1 And idex < 10 Then spont_fiss_rate(idex - 1) = Val(str11)
                        If jdex = 4 And idex > 1 And idex < 10 Then spont_fiss_err(idex - 1) = Val(str11)

                        If jdex = 5 And idex > 1 And idex < 10 Then alpha_n_rate(idex - 1) = Val(str11)
                        If jdex = 6 And idex > 1 And idex < 10 Then alpha_n_err(idex - 1) = Val(str11)

                        If jdex = 7 And idex > 1 And idex < 10 Then f_alpha_n_rate(idex - 1) = Val(str11)
                        If jdex = 8 And idex > 1 And idex < 10 Then f_alpha_n_err(idex - 1) = Val(str11)

                        If jdex = 9 And idex > 1 And idex < 10 Then m240_conv(idex - 1) = Val(str11)
                        If jdex = 10 And idex > 1 And idex < 10 Then m240_conv_err(idex - 1) = Val(str11)


                    Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using
    End Sub

    Public Sub get_fiss_const_file(fiss_const_file)
        Dim myStream As Stream = Nothing

        Dim str11, str12, str13, str14 As String
        Dim idex, jdex As Integer
        Dim fiss_const_file_name As String

        If fiss_const_file = "" Then Return
        fiss_const_file_name = fiss_const_file

        idex = 0
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(fiss_const_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    idex = idex + 1

                    jdex = 0
                    Dim currentField As String
                    For Each currentField In currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        ' 
                        If jdex = 1 And idex > 1 And idex < 9 Then fiss_const_val(idex - 1) = Val(str11)
                        If jdex = 2 And idex > 1 And idex < 9 Then fiss_const_err(idex - 1) = Val(str11)

                        If jdex = 1 And idex > 8 And idex < 16 Then fiss_covar(idex - 8, jdex) = Val(str11)
                        If jdex = 2 And idex > 8 And idex < 16 Then fiss_covar(idex - 8, jdex) = Val(str11)
                        If jdex = 3 And idex > 8 And idex < 16 Then fiss_covar(idex - 8, jdex) = Val(str11)
                        If jdex = 4 And idex > 8 And idex < 16 Then fiss_covar(idex - 8, jdex) = Val(str11)
                        If jdex = 5 And idex > 8 And idex < 16 Then fiss_covar(idex - 8, jdex) = Val(str11)
                        If jdex = 6 And idex > 8 And idex < 16 Then fiss_covar(idex - 8, jdex) = Val(str11)
                        If jdex = 7 And idex > 8 And idex < 16 Then fiss_covar(idex - 8, jdex) = Val(str11)

                    Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using
    End Sub

    Public Sub get_det_par_file(det_file)
        Dim myStream As Stream = Nothing

        Dim str11, str12, str13, str14 As String
        Dim idex, jdex As Integer
        Dim det_par_file_name As String

        If det_file = "" Then Return
        det_par_file_name = det_file

        For i = 1 To 100
            neut_energy(i) = 0
            det_eff_E(i) = 0
            inner_outer(i) = 0
            rel_fiss_prob(i) = 0
        Next i

        pt_cal_flag = False

        idex = 0
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(det_par_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    idex = idex + 1

                    jdex = 0
                    Dim currentField As String
                    For Each currentField In currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        If str11 = "Pt_source_cal" Then pt_cal_flag = True
                        If str11 = "Pt_source_cal" Then GoTo 55
                        ' 
                        If jdex = 1 And idex > 1 And idex < 12 Then det_par_val(idex - 1) = Val(str11)
                        If jdex = 2 And idex > 1 And idex < 12 Then det_par_err(idex - 1) = Val(str11)

                        If jdex = 1 And idex > 11 And idex < 22 Then det_par_covar(idex - 11, jdex) = Val(str11)
                        If jdex = 2 And idex > 11 And idex < 22 Then det_par_covar(idex - 11, jdex) = Val(str11)
                        If jdex = 3 And idex > 11 And idex < 22 Then det_par_covar(idex - 11, jdex) = Val(str11)
                        If jdex = 4 And idex > 11 And idex < 22 Then det_par_covar(idex - 11, jdex) = Val(str11)
                        If jdex = 5 And idex > 11 And idex < 22 Then det_par_covar(idex - 11, jdex) = Val(str11)
                        If jdex = 6 And idex > 11 And idex < 22 Then det_par_covar(idex - 11, jdex) = Val(str11)
                        If jdex = 7 And idex > 11 And idex < 22 Then det_par_covar(idex - 11, jdex) = Val(str11)
                        If jdex = 8 And idex > 11 And idex < 22 Then det_par_covar(idex - 11, jdex) = Val(str11)
                        If jdex = 9 And idex > 11 And idex < 22 Then det_par_covar(idex - 11, jdex) = Val(str11)
                        If jdex = 10 And idex > 11 And idex < 22 Then det_par_covar(idex - 11, jdex) = Val(str11)
                        If jdex = 1 And idex = 23 Then det_cal_FH = Val(str11)
                        If jdex = 1 And idex = 24 Then det_cal_UPu_ratio = Val(str11)
                        If jdex = 1 And idex = 25 Then det_cal_pu240_eff = Val(str11)
                        If jdex = 1 And idex = 26 Then det_cal_alpha = Val(str11)
                        If jdex = 1 And idex = 27 Then det_cal_mod = Val(str11)
                        If jdex = 1 And idex = 28 Then det_cal_ref_density = Val(str11)

                        If jdex = 1 And idex = 29 Then inner_ring_eff_240 = Val(str11)
                        If jdex = 1 And idex = 30 Then outer_ring_eff_240 = Val(str11)

                        If jdex = 1 And idex = 31 Then cal_wall_thickness = Val(str11)
                        If jdex = 1 And idex = 32 Then cal_wall_material = str11

                        If jdex = 1 And idex > 32 And idex < 43 Then mult_corr(idex - 32) = Val(str11)

                        If jdex = 2 And idex > 42 And idex < 142 Then neut_energy(idex - 42) = Val(str11)
                        If jdex = 3 And idex > 42 And idex < 142 Then det_eff_E(idex - 42) = Val(str11)
                        If jdex = 4 And idex > 42 And idex < 142 Then inner_outer(idex - 42) = Val(str11)
                        If jdex = 5 And idex > 42 And idex < 142 Then rel_fiss_prob(idex - 42) = Val(str11)

55:                 Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Private Function Factorial(ByVal x As Integer) As Double
        Factorial = 1

        If x <= 1 Then
            Return 1
        Else
            For i = 1 To x
                Factorial = Factorial * i
            Next i
        End If
    End Function

    Private Function Permutation(ByVal n As Long, ByVal r As Long) As Long
        If r = 0 Then Permutation = 0
        If n = 0 Then Permutation = 0
        If (r >= 0) And (r <= n) Then
            Permutation = Factorial(n) / Factorial(n - r)
        Else
            Permutation = 0
        End If
    End Function

    Private Function Combinatorial(ByVal a As integer, ByVal b As integer) As Double
        If a <= 1 Then Combinatorial = 1

        Combinatorial = Factorial(a) / (Factorial(b) * Factorial(a - b))
    End Function


    Private Sub extract_from_data_file(spec_file_name)
        Dim myStream As Stream = Nothing

        Dim str11, str12, str13, str14 As String
        Dim idex, jdex, hist_flag As Integer
        Dim spec_working_file_name As String

        Dim file_predelay, file_gate_width, file_2nd_gate_width, file_HV_setting, file_die_away, file_efficiency
        Dim file_multiplicity_deadtime_par, file_dt_a, file_dt_b, file_dt_c, file_f_d, file_f_t
        Dim Passive_singles_bkgd, Passive_doubles_bkgd, Passive_Triples_Bkgd As String
        Dim passive_singles_str, passive_doubles_str, passive_triples_str As String

        Dim file_shift_reg_sum, file_shift_reg_R_A_sum
        Dim file_shift_reg_A_sum, file_shift_reg_sc1_sum, file_shift_reg_SC2_sum

        Dim file_iso_str(12)

        Dim hist_num, i_len, line_num, i_fail, j_bin, num_rates As Integer
        Dim individ_cycles, new_cycle, rates_cycle As Boolean

        Dim Pu_date_str, Am_date_str

        Dim key_w11 As String

        Dim i_bin     ', num_A_R(512), num_Acc(512) As Integer

        Dim assay_det_paramters(10)

        For i = 0 To 512
            num_A_R(idex) = 0
            num_Acc(idex) = 0
        Next i

        individ_cycles = False
        hist_num = 0
        max_cycles = 1
        line_num = 0
        num_rates = 0
        new_cycle = False
        rates_cycle = False

        Dim s_len, colon_loc As Integer
        Dim key_w, key_w1, temp_val As String
        spec_working_file_name = spec_file_name
        hist_flag = 3
        idex = 0
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(spec_working_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()

                    line_num = line_num + 1
                    jdex = 0
                    Dim currentField As String
                    For Each currentField In currentRow

                        jdex = jdex + 1
                        str11 = currentField
                        key_w1 = Strings.LTrim(str11)
                        If str11 = "Passive multiplicity distributions" Then individ_cycles = True
                        If Strings.Left(str11, 46) = "Cycle      Singles       Doubles       Triples" Then rates_cycle = True
                        If individ_cycles = True Then rates_cycle = False
                        If individ_cycles Then GoTo 70
                        If rates_cycle = True Then GoTo 80

                        key_w1 = Strings.LTrim(str11)
                        If Strings.Right(key_w1, 11) = "checksum #2" Then i_fail = Val(Strings.Left(key_w1, 6))
                        If Strings.Right(key_w1, 11) = "checksum #2" Then cycle_hardware_fail(i_fail) = 1
                        If Strings.Right(key_w1, 11) = "checksum #1" Then i_fail = Val(Strings.Left(key_w1, 6))
                        If Strings.Right(key_w1, 11) = "checksum #1" Then cycle_hardware_fail(i_fail) = 1

                        s_len = Strings.Len(Strings.LTrim(str11))
                        colon_loc = Strings.InStr(Strings.LTrim(str11), ":")
                        If colon_loc < 1 Then GoTo 50
                        key_w = Strings.Left(Strings.LTrim(str11), colon_loc - 1)

                        temp_val = Strings.Right(Strings.LTrim(str11), s_len - colon_loc)

                        If key_w = "Item id" Then file_item_id = (temp_val)
                        If key_w = "Results file name" Then data_file_id = (temp_val)

                        If key_w = "Predelay" Then file_predelay = Val(temp_val)
                        If key_w = "Gate length" Then file_gate_width = Val(temp_val)
                        If key_w = "2nd gate length" Then file_2nd_gate_width = Val(temp_val)
                        If key_w = "High voltage" Then file_HV_setting = Val(temp_val)
                        If key_w = "Die away time" Then file_die_away = Val(temp_val)
                        If key_w = "Efficiency" Then file_efficiency = Val(temp_val)
                        If key_w = "Multiplicity deadtime" Then file_multiplicity_deadtime_par = Val(temp_val)
                        If key_w = "Coefficient A deadtime" Then file_dt_a = Val(temp_val)
                        If key_w = "Coefficient B deadtime" Then file_dt_b = Val(temp_val)
                        If key_w = "Coefficient C deadtime" Then file_dt_c = Val(temp_val)
                        If key_w = "Doubles gate fraction" Then file_f_d = Val(temp_val)
                        If key_w = "Triples gate fraction" Then file_f_t = Val(temp_val)
                        If key_w = "Measurement date" Then file_assay_date = temp_val
                        If key_w = "Passive singles background" Then Passive_singles_bkgd = (temp_val)
                        If key_w = "Passive doubles background" Then Passive_doubles_bkgd = (temp_val)
                        If key_w = "Passive triples background" Then Passive_Triples_Bkgd = (temp_val)

                        If key_w = "Number of good cycles" Then file_net_cycles = Val(temp_val)
                        If key_w = "Total count time" Then file_total_count_time = Val(temp_val)
                        If key_w = "Shift register singles sum" Then file_shift_reg_sum = Val(temp_val)
                        If key_w = "Shift register reals + accidentals sum" Then file_shift_reg_R_A_sum = Val(temp_val)

                        If key_w = "Shift register accidentals sum" Then file_shift_reg_A_sum = Val(temp_val)
                        If key_w = "Shift register 1st scaler sum" Then file_shift_reg_sc1_sum = Val(temp_val)
                        If key_w = "Shift register 2nd scaler sum" Then file_shift_reg_SC2_sum = Val(temp_val)

                        If key_w = "Singles" Then passive_singles_str = (temp_val)
                        If key_w = "Doubles" Then passive_doubles_str = (temp_val)
                        If key_w = "Triples" Then passive_triples_str = (temp_val)
                        If key_w = "Count time (sec)" Then cycle_time = Val(temp_val)

                        If key_w = "Passive scaler1 background" Then bkg_scaler1 = Val(temp_val)
                        If key_w = "Passive scaler2 background" Then bkg_scaler2 = Val(temp_val)
                        If key_w = "Scaler 1" Then scaler1 = Val(temp_val)
                        If key_w = "Scaler 2" Then scaler2 = Val(temp_val)

                        If key_w = "Pu238" Then file_iso_str(1) = temp_val
                        If key_w = "Pu239" Then file_iso_str(2) = temp_val
                        If key_w = "Pu240" Then file_iso_str(3) = temp_val
                        If key_w = "Pu241" Then file_iso_str(4) = temp_val
                        If key_w = "Pu242" Then file_iso_str(5) = temp_val
                        If key_w = "Am241" Then file_iso_str(7) = temp_val
                        If key_w = "Pu date" Then Pu_date_str = temp_val
                        If key_w = "Am date" Then Am_date_str = temp_val

                        If key_w = "Declared Pu240e mass (g)" Then file_delcared_pu240_m = Val(temp_val)
                        If key_w = "Declared Pu mass (g)" Then file_declared_putot_m = Val(temp_val)

                        '           If key_w = "Shift register 2nd scaler sum" Then GoTo 100
50: ' no colon
                        If key_w1 = "R+A sums       A sums" Then hist_flag = 1

                        If hist_flag <> 1 Then GoTo 60
                        If key_w1 = "Passive results" Then hist_flag = 3
                        If key_w1 = "Passive results" Then GoTo 60

                        If key_w1 = "Results" Then hist_flag = 3
                        If key_w1 = "Results" Then GoTo 60

                        idex = idex + 1

                        i_bin = sep_histogram(str11, 1)
                        num_A_R(i_bin + 1) = sep_histogram(str11, 2)
                        num_Acc(i_bin + 1) = sep_histogram(str11, 3)
                        max_bin = i_bin + 1
                        GoTo 90
60:             ' finished reading summed histogram 
                        GoTo 90
70:             ' start reading individual cycle histograms
                        '     MsgBox("line number : " & line_num & " : " & key_w1)
                        i_len = Strings.Len(key_w1)
                        If i_len < 1 Then GoTo 90
                        If Strings.Left(key_w1, 5) = "Cycle" Then new_cycle = True
                        If new_cycle Then key_w11 = Strings.LTrim(Strings.Right(key_w1, i_len - 5))
                        If new_cycle Then hist_num = Val(Strings.Left(key_w11, 5))
                        If new_cycle Then i_bin = 0
                        '  If new_cycle Then hist_num = hist_num + 1
                        new_cycle = False

                        i_bin = sep_histogram(str11, 1)

                        cycle_hist_A_R(hist_num, i_bin + 1) = sep_histogram(str11, 2)
                        cycle_hist_Acc(hist_num, i_bin + 1) = sep_histogram(str11, 3)
                        max_bin = i_bin + 1

                        'MsgBox(hist_num & " ibin= " & i_bin & ": " & cycle_hist_A_R(hist_num, i_bin + 1) & ", " & cycle_hist_Acc(hist_num, i_bin + 1))

                        cycle_hist_A_R_max(hist_num) = i_bin + 1
                        cycle_hist_ACC_max(hist_num) = i_bin + 1
                        max_cycles = hist_num
                        GoTo 90

80:                 ' start reading cycle by cycle rates data

                        i_len = Strings.Len(key_w1)
                        If i_len < 1 Then GoTo 90

                        j_bin = sep_rates(str11, 1)
                        file_singles(j_bin) = sep_rates(str11, 2)
                        file_doubles(j_bin) = sep_rates(str11, 3)
                        file_triples(j_bin) = sep_rates(str11, 4)
                        file_rates_flag(j_bin) = sep_rates(str11, 5)
                        If file_rates_flag(j_bin) = 0 Then good_cycles = good_cycles + 1
                        num_rates = j_bin + 1

                        'MsgBox(j_bin & ": " & file_singles(j_bin) & ", " & file_doubles(j_bin) & ", " & file_triples(j_bin) & ", " & file_rates_flag(j_bin))

90:                 Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
100:    ' finished getting basic data

        End Using

        Dim scaler1_err, scaler2_err
        ring_ratio = 0
        ring_ratio_err = 0
        If file_total_count_time > 0 Then scaler1_err = scaler1 / file_total_count_time ^ 0.5
        If file_total_count_time > 0 Then scaler2_err = scaler2 / file_total_count_time ^ 0.5
        If (scaler2 - bkg_scaler2) > 0 Then ring_ratio = (scaler1 - bkg_scaler1) / (scaler2 - bkg_scaler2)
        If file_total_count_time > 0 Then ring_ratio_err = ((scaler1_err / scaler2) ^ 2 + (scaler1 * scaler2_err / scaler2 ^ 2) ^ 2) ^ 0.5

        For i = 1 To 5
            file_iso(i) = sep_iso(file_iso_str(i), 1)
            file_iso_err(i) = sep_iso(file_iso_str(i), 2)
            ' file_iso(i) = Strings.Left(file_iso_str(i), 7)
        Next i
        file_iso(6) = 0
        file_iso_err(6) = 0
        file_iso(7) = sep_iso(file_iso_str(7), 1)
        file_iso_err(7) = sep_iso(file_iso_str(7), 2)

        Dim assay_year, assay_month, assay_day As String

        file_pu_date = "01/01/1941"
        file_am_date = "01/01/1941"
        If Pu_date_str = "" Then GoTo 999

        assay_year = (Strings.Left(Strings.Trim(Pu_date_str), 2))
        assay_month = (Strings.Mid(Strings.Trim(Pu_date_str), 4, 2))
        assay_day = (Strings.Right(Strings.Left(Pu_date_str, 10), 2))

        file_pu_date = assay_month & "/" & assay_day & "/" & assay_year

        assay_year = (Strings.Left(Strings.Trim(Am_date_str), 2))
        assay_month = (Strings.Mid(Strings.Trim(Am_date_str), 4, 2))
        assay_day = (Strings.Right(Strings.Left(Am_date_str, 10), 2))

        file_am_date = assay_month & "/" & assay_day & "/" & assay_year


999:    assay_year = (Strings.Left(Strings.Trim(file_assay_date), 2))
        assay_month = (Strings.Mid(Strings.Trim(file_assay_date), 4, 2))
        assay_day = (Strings.Right(Strings.Left(file_assay_date, 10), 2))

        assay_date = assay_month & "/" & assay_day & "/" & assay_year


        file_pass_sing_bkg = Val(Passive_singles_bkgd)
        file_pass_doub_bkg = Val(Passive_doubles_bkgd)
        file_pass_trip_bkg = Val(Passive_Triples_Bkgd)
        file_pass_sing_bkg_err = Val(Strings.Right(Passive_singles_bkgd, 9))
        file_pass_doub_bkg_err = Val(Strings.Right(Passive_doubles_bkgd, 9))
        file_pass_trip_bkg_err = Val(Strings.Right(Passive_Triples_Bkgd, 9))


        file_pass_sing_rate = Val(passive_singles_str)
        file_pass_doub_rate = Val(passive_doubles_str)
        file_pass_trip_rate = Val(passive_triples_str)
        file_pass_sing_rate_err = Val(Strings.Right(passive_singles_str, 9))
        file_pass_doub_rate_err = Val(Strings.Right(passive_doubles_str, 9))
        file_pass_trip_rate_err = Val(Strings.Right(passive_triples_str, 9))

        Dim text_head, text1, text2, text3 As String
        text_head = " Reported Rates from file" & vbCrLf
        text1 = "Singles:      " & Int(1000 * file_pass_sing_rate) / 1000 & " +/- " & Int(1000 * file_pass_sing_rate_err) / 1000 & vbCrLf
        text2 = "Doubles:    " & Int(1000 * file_pass_doub_rate) / 1000 & " +/- " & Int(1000 * file_pass_doub_rate_err) / 1000 & vbCrLf
        text3 = "Triples:      " & Int(1000 * file_pass_trip_rate) / 1000 & " +/- " & Int(1000 * file_pass_trip_rate_err) / 1000
        ' MsgBox(text_head & text1 & text2 & text3)


        file_det_parameters(1) = file_efficiency
        file_det_parameters(2) = file_die_away
        file_det_parameters(3) = file_gate_width
        file_det_parameters(4) = file_f_d
        file_det_parameters(5) = file_f_t

        file_det_parameters(6) = file_dt_a
        file_det_parameters(7) = file_dt_b
        file_det_parameters(8) = file_dt_c
        file_det_parameters(9) = file_dt_c
        file_det_parameters(10) = file_multiplicity_deadtime_par


        grunt_time(1) = file_total_count_time    ' total assay time in INCC analysis
        grunt_time(2) = max_cycles * cycle_time   ' total assay time in this anlaysis

    End Sub

    Function sep_histogram(input_string, i_num)

        Dim str_1, str_2, str_3 As String
        Dim ilen, ispc_1, outval(3) As Integer

        str_1 = Strings.LTrim(input_string)
        ilen = Strings.Len(str_1)
        ispc_1 = Strings.InStr(str_1, " ")
        outval(1) = Val(Strings.Left(str_1, ispc_1))

        str_2 = Strings.LTrim(Strings.Right(str_1, ilen - ispc_1))
        ilen = Strings.Len(str_2)
        ispc_1 = Strings.InStr(str_2, " ")
        outval(2) = Val(Strings.Left(str_2, ispc_1))

        str_3 = Strings.LTrim(Strings.Right(str_2, ilen - ispc_1))
        '     ilen = Strings.Len(str_3)
        '    ispc_1 = Strings.InStr(str_3, " ")
        ' outval(2) = Val(Strings.Left(str_2, ispc_1))
        outval(3) = Val(str_3)

        sep_histogram = outval(i_num)

    End Function

    Function sep_rates(input_string, i_num)

        Dim str_1, str_2, str_3, str_4 As String
        Dim ilen, ispc_1 As Integer
        Dim outval(5)

        str_1 = Strings.LTrim(input_string)
        ilen = Strings.Len(str_1)
        ispc_1 = Strings.InStr(str_1, " ")
        outval(1) = Val(Strings.Left(input_string, 5))

        str_2 = Strings.LTrim(Strings.Right(str_1, ilen - ispc_1))
        ilen = Strings.Len(str_2)
        ispc_1 = Strings.InStr(str_2, " ")
        outval(2) = Val(Strings.Left(str_2, ispc_1))

        str_3 = Strings.LTrim(Strings.Right(str_2, ilen - ispc_1))
        ilen = Strings.Len(str_3)
        ispc_1 = Strings.InStr(str_3, " ")
        outval(3) = Val(Strings.Left(str_3, ispc_1))

        str_4 = Strings.LTrim(Strings.Right(str_3, ilen - ispc_1))
        ilen = Strings.Len(str_4)
        ispc_1 = Strings.InStr(str_4, " ")
        outval(4) = Val(Strings.Left(str_4, ispc_1))

        If Strings.Right(input_string, 4) = "Pass" Then outval(5) = 0 Else outval(5) = 1

        sep_rates = outval(i_num)

    End Function

    Function sep_iso(input_string, i_num)

        Dim str_1, str_2, str_3, str_4 As String
        Dim ilen, ispc_1 As Integer
        Dim outval(5)
        outval(i_num) = 0

        ilen = Strings.Len(input_string)
        If ilen < 10 Then GoTo 10
        outval(1) = Val(Strings.Left(input_string, 10))

        str_1 = Strings.Right(input_string, ilen - 13)
        outval(2) = Val(Strings.Left(str_1, 10))

10:     sep_iso = outval(i_num)

    End Function

    Public Sub get_item_info_file(det_file)
        Dim myStream As Stream = Nothing


        Dim str11, str12, str13, str14 As String
        Dim idex, jdex As Integer
        Dim det_par_file_name As String
        Dim sep_val1, sep_val2 As String
        det_par_file_name = det_file
        idex = 0
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(det_par_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    idex = idex + 1

                    jdex = 0
                    Dim currentField As String
                    For Each currentField In currentRow
                        jdex = jdex + 1

                        str11 = currentField
                        ' 
                        If jdex = 2 And idex = 1 Then item_id = str11
                        If jdex = 2 And idex > 1 And idex < 10 Then iso_val(idex - 1) = Val(str11)
                        If jdex = 3 And idex > 1 And idex < 10 Then iso_val_err(idex - 1) = Val(str11)
                        If jdex = 2 And idex = 10 Then iso_date(1) = (str11)
                        If jdex = 2 And idex = 11 Then iso_date(2) = (str11)
                        If jdex = 2 And idex = 12 Then iso_date(3) = (str11)

                        If jdex = 2 And idex > 12 And idex < 32 Then geo_par(idex - 12) = Val(str11)
                        If jdex = 2 And idex = 13 Then geo_par(1) = (str11)
                        If jdex = 2 And idex = 17 Then geo_par(5) = (str11)
                        If jdex = 2 And idex = 22 Then geo_par(10) = (str11)
                        If jdex = 2 And idex > 31 And idex < 42 Then item_impurity_flag(idex - 31) = Val(str11)
                        If jdex = 3 And idex > 31 And idex < 42 Then item_impurity_conc(idex - 31) = Val(str11)

                    Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Public Sub get_det_dimensions_file(det_dim_file)
        Dim myStream As Stream = Nothing
        Dim det_dimfile_name As String

        Dim str11, str12, str13, str14 As String
        Dim idex, jdex As Integer
        Dim det_par_file_name As String
        Dim sep_val1, sep_val2 As String
        '    Dim counter_id, current_det_file_name As String
        If det_dim_file = "" Then Return
        det_dimfile_name = det_dim_file

        idex = 0
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(det_dimfile_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    idex = idex + 1

                    jdex = 0
                    Dim currentField As String
                    For Each currentField In currentRow
                        jdex = jdex + 1

                        str11 = currentField
                        ' 
                        If jdex = 2 And idex = 1 Then counter_id = str11
                        If jdex = 2 And idex = 2 Then current_det_file_name = str11
                        If jdex = 2 And idex = 3 And idex < 18 Then det_dim_val(1) = (str11)
                        If jdex = 2 And idex > 3 And idex < 18 Then det_dim_val(idex - 2) = Val(str11)
                        If jdex = 3 And idex > 11 And idex < 17 Then tube_numbers(idex - 11) = (str11)

                    Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Function iso_decay(dec_date_Pu, dec_date_Am, dec_date_cf, meas_date, init_iso_temp, init_iso_temp_err, iso_num, alpha_val, half_lives, half_life_err)

        Dim temp_iso(12), final_iso(12), final_iso_err(12), err_temp(12)
        Dim ln2, sum_iso, temp, sum_iso_err
        Dim i As Integer

        Dim lam(12), lam_err(12)

        num_days_pu = DateDiff(DateInterval.Day, dec_date_Pu, meas_date)
        num_days_am = DateDiff(DateInterval.Day, dec_date_Am, meas_date)
        num_days_cf = DateDiff(DateInterval.Day, dec_date_cf, meas_date)

        ln2 = Log(2)

        sum_iso = 0
        For i = 1 To 12
            lam(i) = ln2 / (365.25 * half_lives(i))
            lam_err(i) = lam(i) * half_life_err(i) / half_lives(i)
            temp_iso(i) = 0
            final_iso(i) = 0
            final_iso_err(i) = 0
        Next i
        For i = 1 To 6
            sum_iso = sum_iso + init_iso_temp(i)
        Next i

        If sum_iso = 0 Then GoTo 10
        ' Pu decay
        sum_iso = 0
        For i = 1 To 6
            temp_iso(i) = init_iso_temp(i) * Exp(-lam(i) * num_days_pu)
            err_temp(i) = ((-num_days_pu * temp_iso(i) * lam_err(i)) ^ 2 + (Exp(-lam(i) * num_days_pu) * (init_iso_temp_err(i))) ^ 2) ^ 0.5
        Next i

        temp_iso(7) = init_iso_temp(7) * Exp(-lam(7) * num_days_am)
        temp_iso(7) = temp_iso(7) + (lam(4) / (lam(7) - lam(4))) * temp_iso(4) * (Exp(-lam(4) * num_days_am) - Exp(-lam(7) * num_days_pu))

        sum_iso_err = 0
        For i = 1 To 6
            sum_iso = sum_iso + temp_iso(i)
            sum_iso_err = sum_iso_err + err_temp(i) ^ 2
        Next i

        For i = 1 To 7
            final_iso(i) = temp_iso(i) / sum_iso
            final_iso_err(i) = ((err_temp(i) / sum_iso) ^ 2 + (temp_iso(i) / sum_iso ^ 2 * sum_iso_err ^ 2) ^ 2) ^ 0.5
        Next i

10:     final_iso(8) = init_iso_temp(8) * Exp(-ln2 * (num_days_cf) / (365.25 * half_lives(8)))
        final_iso_err(7) = (final_iso_err(7) ^ 2 + final_iso_err(4) ^ 2) ^ 0.5

        For i = 1 To 8
            If init_iso_temp(i) <> 0 Then final_iso_err(i) = ((init_iso_temp_err(i) * final_iso(i) / init_iso_temp(i)) ^ 2 + final_iso_err(i) ^ 2) ^ 0.5
        Next i

        temp = 0
        If iso_num < 9 Then temp = final_iso(iso_num)
        If iso_num > 20 Then temp = final_iso_err(iso_num - 20)

        iso_decay = temp
    End Function


    Function calc_alpha(input_iso, input_iso_err, alpha_val, alpha_err, m240val, spont_fiss, idex)
        Dim alpha_temp, alpha_temp_err

        alpha_temp = 0
        alpha_temp_err = 0
        For i = 1 To 8
            alpha_temp = alpha_temp + alpha_val(i) * input_iso(i)
        Next i
        alpha_temp = alpha_temp / spont_fiss(3)
        alpha_temp = alpha_temp / (m240val(1) * input_iso(1) + input_iso(3) + input_iso(5) * m240val(5) + 0 * m240val(6) * input_iso(6))

        calc_alpha = alpha_temp
        For i = 1 To 8
            If alpha_val(i) <> 0 Then alpha_temp_err = alpha_temp_err + (alpha_temp / alpha_val(i) * alpha_err(i)) ^ 2
            If input_iso(i) <> 0 Then alpha_temp_err = alpha_temp_err + (alpha_temp / input_iso(i) * input_iso_err(i)) ^ 2
        Next i

        alpha_temp_err = alpha_temp_err ^ 0.5
        If idex = 2 Then calc_alpha = alpha_temp_err

    End Function

    Public Function mult_anal2(rates, rates_err, covar_m, det_parms, fis_dat, i_out)
        ' calculate the mass, alpha and multiplication values, using the measured rates covariances
        Dim out_val(6)
        Dim nu_s1, nu_s2, nu_s3, nu_i1, nu_i2, nu_i3, f_f
        Dim n_eff, die, gate, doub_frac, trip_frac
        Dim singles, doubles, triples, sig_s, sig_d, sig_t
        Dim k_1, k_2, k_3, k_4, mult
        Dim dm_ds, dm_dd, dm_dt
        Dim dm_covar, sig_mult, alpha1, d_alpha_dm, d_alpha_ds, d_alpha_dd, d_alpha_dt
        Dim dalpha_covar, sig_alpha1, m_240, dm240_ds, dm240_dd, dm240_dt, dm240_dm, dm240_covar, sig_m240
        Dim covar_sd, covar_st, covar_dt

        nu_s1 = fis_dat(1)
        nu_s2 = fis_dat(2)
        nu_s3 = fis_dat(3)
        nu_i1 = fis_dat(4)
        nu_i2 = fis_dat(5)
        nu_i3 = fis_dat(6)
        f_f = fis_dat(7)

        n_eff = det_parms(1)
        die = det_parms(2)
        gate = det_parms(3)
        doub_frac = det_parms(4)
        trip_frac = det_parms(5)

        singles = rates(1)
        doubles = rates(2)
        triples = rates(3)
        '
        sig_s = rates_err(1)
        sig_d = rates_err(2)
        sig_t = rates_err(3)

        covar_sd = covar_m(1, 2)
        covar_st = covar_m(1, 3)
        covar_dt = covar_m(2, 3)

        ' calc multiplication

        k_4 = nu_s2 * nu_i3 - nu_s3 * nu_i2
        k_1 = -6 * triples * nu_s2 * (nu_i1 - 1) / (n_eff ^ 2 * trip_frac * singles * k_4)
        k_2 = 2 * doubles * (nu_s3 * (nu_i1 - 1) - 3 * nu_s2 * nu_i2) / (n_eff * doub_frac * singles * k_4)
        k_3 = (6 * doubles * nu_s2 * nu_i2 / (n_eff * doub_frac * singles * k_4)) - 1

        mult = (k_3 + 2 - k_1) / (k_2 + 2 * k_3 + 3)

        For i = 1 To 5
            mult = (k_3 * mult ^ 2 + 2 * mult ^ 3 - k_1) / (k_2 + 2 * k_3 * mult + 3 * mult ^ 2)
        Next i

        ' calc error in multiplication

        dm_ds = (k_1 + k_2 * mult + (k_3 + 1) * mult ^ 2) / singles / (k_2 + 2 * mult * k_3 + 3 * mult ^ 2)
        dm_dd = -(k_2 * mult + (k_3 + 1) * mult ^ 2) / doubles / (k_2 + 2 * mult * k_3 + 3 * mult ^ 2)
        dm_dt = -k_1 / triples / (k_2 + 2 * mult * k_3 + 3 * mult ^ 2)

        dm_covar = dm_ds * dm_dt * covar_st + dm_ds * dm_dd * covar_sd + dm_dd * dm_dt * covar_dt

        sig_mult = (dm_ds * sig_s) ^ 2 + (dm_dd * sig_d) ^ 2 + (dm_dt * sig_t) ^ 2
        sig_mult = sig_mult + 2 * (dm_ds * dm_dd * sig_s * sig_d + dm_ds * dm_dt * sig_s * sig_t + dm_dd * dm_dt * sig_d * sig_t)
        sig_mult = (sig_mult + 2 * dm_covar) ^ 0.5

        out_val(1) = mult
        out_val(2) = sig_mult



        m_240 = ((2 * doubles / n_eff / doub_frac) - (mult * (mult - 1) * nu_i2 * singles) / (nu_i1 - 1)) / (n_eff * mult ^ 2 * nu_s2) / f_f        ' Pu240 effective mass

        'calculate the partial derivative of m_240
        Dim k01, k02
        k01 = 2 / doub_frac / n_eff ^ 2 / nu_s2 / f_f
        k02 = nu_i2 / (nu_i1 - 1) / nu_s2 / n_eff / f_f

        dm240_dm = -(2 * k01 * doubles / mult ^ 3 + k02 * singles / mult ^ 2)

        dm240_ds = dm240_dm * dm_ds - (1 - 1 / mult) * k02
        dm240_dd = k01 / mult ^ 2 + dm240_dm * dm_dd
        dm240_dt = dm240_dm * dm_dt
        '
        dm240_covar = 0
        ' 

        sig_m240 = (dm240_ds * sig_s) ^ 2 + (dm240_dd * sig_d) ^ 2 + (dm240_dt * sig_t) ^ 2
        dm240_covar = dm240_ds * dm240_dd * covar_sd + dm240_dd * dm240_dt * covar_dt + dm240_ds * dm240_dt * covar_st                                          '  ****** using measured covariances and S,D,T error propogation



        sig_m240 = (sig_m240 + 2 * dm240_covar) ^ 0.5

        out_val(5) = m_240
        out_val(6) = sig_m240


        ' calc alpha, n value

        ' alpha1 = (mult * nu_s2 * singles / nu_s1) / ((2 * doubles / doub_frac / n_eff) - (mult * (mult - 1) * nu_i2 * singles / (nu_i1 - 1))) - 1
        alpha1 = singles / (m_240 * f_f * n_eff * nu_s1 * mult) - 1

        d_alpha_dm = ((alpha1 + 1) / mult) * (1 + (alpha1 + 1) * nu_s1 / nu_s2 * nu_i2 * (2 * mult - 1) / (nu_i1 - 1))

        d_alpha_ds = (alpha1 + 1) * (1 / singles - dm_ds / mult - dm240_ds / m_240)
        d_alpha_dd = -(alpha1 + 1) * (dm_dd / mult + dm240_dd / m_240)
        d_alpha_dt = -(alpha1 + 1) * (dm_dt / mult + dm240_dt / m_240)

        '    d_alpha_dt = d_alpha_dm * dm_dt     '  ******
        '    d_alpha_ds = ((alpha1 + 1) / singles) * (1 + (alpha1 + 1) * nu_s1 / nu_s2 * nu_i2 * (mult - 1) / (nu_i1 - 1))
        '    d_alpha_dd = (alpha1 + 1) ^ 2 * nu_s1 / nu_s2 / mult / singles * (2 / doub_frac / n_eff)

        ' dalpha_covar = (d_alpha_dt * d_alpha_ds * sig_t * sig_s + d_alpha_dt * d_alpha_dd * sig_t * sig_d + d_alpha_ds * d_alpha_dd * sig_s * sig_d)
        dalpha_covar = (d_alpha_dt * d_alpha_ds * covar_st + d_alpha_dt * d_alpha_dd * covar_dt + d_alpha_ds * d_alpha_dd * covar_sd)

        sig_alpha1 = (d_alpha_ds * sig_s) ^ 2 + (d_alpha_dd * sig_d) ^ 2 + (d_alpha_dt * sig_t) ^ 2
        sig_alpha1 = (sig_alpha1 + 2 * dalpha_covar) ^ 0.5

        'dalpha_covar = 2 * (d_alpha_dt * d_alpha_ds * covar_st + d_alpha_dt * d_alpha_dd * covar_dt + d_alpha_ds * d_alpha_dd * covar_sd)
        'dalpha_covar = 2 * (d_alpha_dm * d_alpha_ds * sig_mult * sig_s + d_alpha_dm * d_alpha_dd * sig_mult * sig_d + d_alpha_ds * d_alpha_dd * sig_s * sig_d)  


        ' error in value of alpha

        out_val(3) = alpha1
        out_val(4) = sig_alpha1


        mult_anal2 = out_val(i_out)

    End Function


    Function mult_anal_err2(A_R, ACCID, A_R_len, ACC_len, det_parms, det_parms_err, fis_dat, fis_dat_err, assay_time, p_out, i_out, j_out)
        Dim err_arr(3, 11, 2), det_parms_l(11), det_parms_h(11), rates(3), rates_err(3)
        Dim fis_parms_1(7), fis_parms_h(7), m_l, m_h
        Dim i_dex, ij, ik As Integer
        '   p_out = 1 <===== contributions to multiplication error
        '   p_out = 3 <===== contributions to alpha error
        '   p_out = 3 <===== contributions to mass error
        '   j_out = 1 <====== errors from detector parameters
        '   j_out = 2 <====== errors from nuclear data

        ' detector parameter contributions

        If p_out = 1 Then i_dex = 1
        If p_out = 2 Then i_dex = 3
        If p_out = 3 Then i_dex = 5

        For j = 1 To 10
            For i = 1 To 10
                det_parms_l(i) = det_parms(i)
                det_parms_h(i) = det_parms(i)
            Next i

            det_parms_l(j) = det_parms(j) - det_parms_err(j) / 2

            For ij = 1 To 3
                ik = 2 * ij - 1
                rates(ij) = calc_rates(A_R, ACCID, A_R_len, ACC_len, det_parms_l, assay_time, ik)
                rates_err(ij) = calc_rates(A_R, ACCID, A_R_len, ACC_len, det_parms_l, assay_time, ik + 1)
            Next ij

            m_l = mult_anal(rates, rates_err, det_parms_l, fis_dat, i_dex)
            '        mult_anal2(rates, rates_err, covar_m, det_parms, fis_dat, i_out)
            det_parms_h(j) = det_parms(j) + det_parms_err(j) / 2

            For ij = 1 To 3
                ik = 2 * ij - 1
                rates(ij) = calc_rates(A_R, ACCID, A_R_len, ACC_len, det_parms_h, assay_time, ik)
                rates_err(ij) = calc_rates(A_R, ACCID, A_R_len, ACC_len, det_parms_h, assay_time, ik + 1)
            Next ij

            m_h = mult_anal(rates, rates_err, det_parms_h, fis_dat, i_dex)
            err_arr(3, j, 1) = m_h - m_l

        Next j

        For ij = 1 To 3
            ik = 2 * ij - 1
            rates(ij) = calc_rates(A_R, ACCID, A_R_len, ACC_len, det_parms, assay_time, ik)
            rates_err(ij) = calc_rates(A_R, ACCID, A_R_len, ACC_len, det_parms, assay_time, ik + 1)
        Next ij

        '   nuclear data contributions to error


        For j = 1 To 7
            For i = 1 To 7
                fis_parms_1(i) = fis_dat(i)
                fis_parms_h(i) = fis_dat(i)
            Next i


            fis_parms_1(j) = fis_dat(j) - fis_dat_err(j) / 2
            m_l = mult_anal(rates, rates_err, det_parms, fis_parms_1, i_dex)

            fis_parms_h(j) = fis_dat(j) + fis_dat_err(j) / 2
            m_h = mult_anal(rates, rates_err, det_parms, fis_parms_h, i_dex)

            err_arr(3, j, 2) = m_h - m_l
        Next j

10:     mult_anal_err2 = err_arr(3, i_out, j_out)


    End Function


    Function mult_anal(rates, rates_err, det_parms, fis_dat, i_out)
        ' calculate the mass, alpha and multiplication values, using fully correlated covariances
        Dim out_val(6)
        Dim nu_s1, nu_s2, nu_s3, nu_i1, nu_i2, nu_i3, f_f
        Dim n_eff, die, gate, doub_frac, trip_frac
        Dim singles, doubles, triples, sig_s, sig_d, sig_t
        Dim k_1, k_2, k_3, k_4, mult
        Dim dm_ds, dm_dd, dm_dt
        Dim dm_covar, sig_mult, alpha1, d_alpha_dm, d_alpha_ds, d_alpha_dd, d_alpha_dt
        Dim dalpha_covar, sig_alpha1, m_240, dm240_ds, dm240_dd, dm240_dt, dm240_dm, dm240_covar, sig_m240
        Dim covar_sd, covar_st, covar_dt

        nu_s1 = fis_dat(1)
        nu_s2 = fis_dat(2)
        nu_s3 = fis_dat(3)
        nu_i1 = fis_dat(4)
        nu_i2 = fis_dat(5)
        nu_i3 = fis_dat(6)
        f_f = fis_dat(7)

        n_eff = det_parms(1)
        die = det_parms(2)
        gate = det_parms(3)
        doub_frac = det_parms(4)
        trip_frac = det_parms(5)

        singles = rates(1)
        doubles = rates(2)
        triples = rates(3)
        '
        sig_s = rates_err(1)
        sig_d = rates_err(2)
        sig_t = rates_err(3)

        '  covar_sd = covar_m(1, 2)
        '  covar_st = covar_m(1, 3)
        '  covar_dt = covar_m(2, 3)

        covar_sd = sig_s * sig_d
        covar_st = sig_s * sig_t
        covar_dt = sig_d * sig_t

        ' calc multiplication

        k_4 = nu_s2 * nu_i3 - nu_s3 * nu_i2
        k_1 = -6 * triples * nu_s2 * (nu_i1 - 1) / (n_eff ^ 2 * trip_frac * singles * k_4)
        k_2 = 2 * doubles * (nu_s3 * (nu_i1 - 1) - 3 * nu_s2 * nu_i2) / (n_eff * doub_frac * singles * k_4)
        k_3 = (6 * doubles * nu_s2 * nu_i2 / (n_eff * doub_frac * singles * k_4)) - 1

        mult = (k_3 + 2 - k_1) / (k_2 + 2 * k_3 + 3)

        For i = 1 To 5
            mult = (k_3 * mult ^ 2 + 2 * mult ^ 3 - k_1) / (k_2 + 2 * k_3 * mult + 3 * mult ^ 2)
        Next i

        ' calc error in multiplication

        dm_ds = (k_1 + k_2 * mult + (k_3 + 1) * mult ^ 2) / singles / (k_2 + 2 * mult * k_3 + 3 * mult ^ 2)
        dm_dd = -(k_2 * mult + (k_3 + 1) * mult ^ 2) / doubles / (k_2 + 2 * mult * k_3 + 3 * mult ^ 2)
        dm_dt = -k_1 / triples / (k_2 + 2 * mult * k_3 + 3 * mult ^ 2)

        dm_covar = dm_ds * dm_dt * covar_st + dm_ds * dm_dd * covar_sd + dm_dd * dm_dt * covar_dt

        sig_mult = (dm_ds * sig_s) ^ 2 + (dm_dd * sig_d) ^ 2 + (dm_dt * sig_t) ^ 2
        sig_mult = sig_mult + 2 * (dm_ds * dm_dd * sig_s * sig_d + dm_ds * dm_dt * sig_s * sig_t + dm_dd * dm_dt * sig_d * sig_t)
        sig_mult = (sig_mult + 2 * dm_covar) ^ 0.5

        out_val(1) = mult
        out_val(2) = sig_mult


        m_240 = ((2 * doubles / n_eff / doub_frac) - (mult * (mult - 1) * nu_i2 * singles) / (nu_i1 - 1)) / (n_eff * mult ^ 2 * nu_s2) / f_f        ' Pu240 effective mass

        'calculate the partial derivative of m_240
        Dim k01, k02
        k01 = 2 / doub_frac / n_eff ^ 2 / nu_s2 / f_f
        k02 = nu_i2 / (nu_i1 - 1) / nu_s2 / n_eff / f_f

        dm240_dm = -(2 * k01 * doubles / mult ^ 3 + k02 * singles / mult ^ 2)

        dm240_ds = dm240_dm * dm_ds - (1 - 1 / mult) * k02
        dm240_dd = k01 / mult ^ 2 + dm240_dm * dm_dd
        dm240_dt = dm240_dm * dm_dt
        '
        dm240_covar = 0
        ' 

        sig_m240 = (dm240_ds * sig_s) ^ 2 + (dm240_dd * sig_d) ^ 2 + (dm240_dt * sig_t) ^ 2
        dm240_covar = dm240_ds * dm240_dd * covar_sd + dm240_dd * dm240_dt * covar_dt + dm240_ds * dm240_dt * covar_st                                          '  ****** using measured covariances and S,D,T error propogation


        sig_m240 = (sig_m240 + 2 * dm240_covar) ^ 0.5

        out_val(5) = m_240
        out_val(6) = sig_m240

        ' calc alpha, n value

        alpha1 = singles / (m_240 * f_f * n_eff * nu_s1 * mult) - 1

        d_alpha_dm = ((alpha1 + 1) / mult) * (1 + (alpha1 + 1) * nu_s1 / nu_s2 * nu_i2 * (2 * mult - 1) / (nu_i1 - 1))

        d_alpha_ds = (alpha1 + 1) * (1 / singles - dm_ds / mult - dm240_ds / m_240)
        d_alpha_dd = -(alpha1 + 1) * (dm_dd / mult + dm240_dd / m_240)
        d_alpha_dt = -(alpha1 + 1) * (dm_dt / mult + dm240_dt / m_240)


        dalpha_covar = (d_alpha_dt * d_alpha_ds * covar_st + d_alpha_dt * d_alpha_dd * covar_dt + d_alpha_ds * d_alpha_dd * covar_sd)

        sig_alpha1 = (d_alpha_ds * sig_s) ^ 2 + (d_alpha_dd * sig_d) ^ 2 + (d_alpha_dt * sig_t) ^ 2
        sig_alpha1 = (sig_alpha1 + 2 * dalpha_covar) ^ 0.5

        out_val(3) = alpha1
        out_val(4) = sig_alpha1


        mult_anal = out_val(i_out)

    End Function


    Function mult_anal_old(rates, rates_err, det_parms, fis_dat, i_out)
        '
        ' calculate the mass, alpha and multiplication values, using the rates uncertainties for the covariances
        '
        Dim out_val(6)
        Dim nu_s1, nu_s2, nu_s3, nu_i1, nu_i2, nu_i3, f_f
        Dim n_eff, die, gate, doub_frac, trip_frac
        Dim singles, doubles, triples, sig_s, sig_d, sig_t
        Dim k_1, k_2, k_3, k_4, mult
        Dim dm_ds, dm_dd, dm_dt
        Dim dm_covar, sig_mult, alpha1, d_alpha_dm, d_alpha_ds, d_alpha_dd
        Dim dalpha_covar, sig_alpha1, m_240, dm240_ds, dm240_dd, dm240_dt, dm240_dm, dm240_covar, sig_m240
        Dim covar_sd, covar_st, covar_dt

        nu_s1 = fis_dat(1)
        nu_s2 = fis_dat(2)
        nu_s3 = fis_dat(3)
        nu_i1 = fis_dat(4)
        nu_i2 = fis_dat(5)
        nu_i3 = fis_dat(6)
        f_f = fis_dat(7)

        n_eff = det_parms(1)
        die = det_parms(2)
        gate = det_parms(3)
        doub_frac = det_parms(4)
        trip_frac = det_parms(5)

        singles = rates(1)
        doubles = rates(2)
        triples = rates(3)
        '
        sig_s = rates_err(1)
        sig_d = rates_err(2)
        sig_t = rates_err(3)

        '  covar_sd = covar_m(1, 2)
        '  covar_st = covar_m(1, 3)
        '  covar_dt = covar_m(2, 3)

        ' calc multiplication

        k_4 = nu_s2 * nu_i3 - nu_s3 * nu_i2
        k_1 = -6 * triples * nu_s2 * (nu_i1 - 1) / (n_eff ^ 2 * trip_frac * singles * k_4)
        k_2 = 2 * doubles * (nu_s3 * (nu_i1 - 1) - 3 * nu_s2 * nu_i2) / (n_eff * doub_frac * singles * k_4)
        k_3 = (6 * doubles * nu_s2 * nu_i2 / (n_eff * doub_frac * singles * k_4)) - 1

        mult = (k_3 + 2 - k_1) / (k_2 + 2 * k_3 + 3)

        For i = 1 To 5
            mult = (k_3 * mult ^ 2 + 2 * mult ^ 3 - k_1) / (k_2 + 2 * k_3 * mult + 3 * mult ^ 2)
        Next i

        ' calc error in multiplication

        dm_ds = (k_1 + k_2 * mult + (k_3 + 1) * mult ^ 2) / singles / (k_2 + 2 * mult * k_3 + 3 * mult ^ 2)
        dm_dd = -(k_2 * mult + (k_3 + 1) * mult ^ 2) / doubles / (k_2 + 2 * mult * k_3 + 3 * mult ^ 2)
        dm_dt = -k_1 / triples / (k_2 + 2 * mult * k_3 + 3 * mult ^ 2)

        dm_covar = dm_ds * dm_dt * sig_s * sig_t + dm_ds * dm_dd * sig_s * sig_d + dm_dd * dm_dt * sig_d * sig_t


        sig_mult = (dm_ds * sig_s) ^ 2 + (dm_dd * sig_d) ^ 2 + (dm_dt * sig_t) ^ 2
        sig_mult = sig_mult + 2 * (dm_ds * dm_dd * sig_s * sig_d + dm_ds * dm_dt * sig_s * sig_t + dm_dd * dm_dt * sig_d * sig_t)
        sig_mult = (sig_mult + 2 * dm_covar) ^ 0.5

        out_val(1) = mult
        out_val(2) = sig_mult

        ' calc alpha, n value

        alpha1 = (mult * nu_s2 * singles / nu_s1) / ((2 * doubles / doub_frac / n_eff) - (mult * (mult - 1) * nu_i2 * singles / (nu_i1 - 1))) - 1

        d_alpha_dm = ((alpha1 + 1) / mult) * (1 + (alpha1 + 1) * nu_s1 / nu_s2 * nu_i2 * (2 * mult - 1) / (nu_i1 - 1))
        Dim d_alpha_dt    '  ******  
        d_alpha_dt = d_alpha_dm * dm_dt     '  ******
        d_alpha_ds = ((alpha1 + 1) / singles) * (1 + (alpha1 + 1) * nu_s1 / nu_s2 * nu_i2 * (mult - 1) / (nu_i1 - 1))
        d_alpha_dd = (alpha1 + 1) ^ 2 * nu_s1 / nu_s2 / mult / singles * (2 / doub_frac / n_eff)

        dalpha_covar = 2 * (d_alpha_dt * d_alpha_ds * sig_t * sig_s + d_alpha_dt * d_alpha_dd * sig_t * sig_d + d_alpha_ds * d_alpha_dd * sig_s * sig_d)            '  ******usng cheesy covariances and S,D,T error propogation
        '   dalpha_covar = 2 * (d_alpha_dt * d_alpha_ds * covar_st + d_alpha_dt * d_alpha_dd * covar_dt + d_alpha_ds * d_alpha_dd * covar_sd)                       '  ******using measured covariancesand S,D,T error propogation
        '   dalpha_covar = 2 * (d_alpha_dm * d_alpha_ds * sig_mult * sig_s + d_alpha_dm * d_alpha_dd * sig_mult * sig_d + d_alpha_ds * d_alpha_dd * sig_s * sig_d)  '  ******usng cheesy covariances but proper error propogation


        sig_alpha1 = (d_alpha_dm * sig_mult) ^ 2 + (d_alpha_ds * sig_s) ^ 2 + (d_alpha_dd * sig_d) ^ 2
        sig_alpha1 = (sig_alpha1 + 2 * dalpha_covar) ^ 0.5                                                                                           ' error in value of alpha

        out_val(3) = alpha1
        out_val(4) = sig_alpha1

        m_240 = ((2 * doubles / n_eff / doub_frac) - (mult * (mult - 1) * nu_i2 * singles) / (nu_i1 - 1)) / (n_eff * mult ^ 2 * nu_s2) / f_f        ' Pu240 effective mass

        'calculate the partial derivative of m_240
        Dim k01, k02
        k01 = 2 / doub_frac / n_eff ^ 2 / nu_s2 / f_f
        k02 = nu_i2 / (nu_i1 - 1) / nu_s2 / n_eff / f_f

        dm240_dm = -(2 * k01 * doubles / mult ^ 3 + k02 * singles / mult ^ 2)

        dm240_ds = dm240_dm * dm_ds - (1 - 1 / mult) * k02
        dm240_dd = k01 / mult ^ 2 + dm240_dm * dm_dd
        dm240_dt = dm240_dm * dm_dt
        '
        dm240_covar = 0
        ' 

        sig_m240 = (dm240_ds * sig_s) ^ 2 + (dm240_dd * sig_d) ^ 2 + (dm240_dt * sig_t) ^ 2
        dm240_covar = dm240_ds * dm240_dd * sig_s * sig_d + dm240_dd * dm240_dt * sig_d * sig_t + dm240_ds * dm240_dt * sig_s * sig_t
        ' dm240_covar = dm240_ds * dm240_dd * covar_sd + dm240_dd * dm240_dt * covar_dt + dm240_ds * dm240_dt * covar_st                                          '  ****** using measured covariances and S,D,T error propogation

        '   sig_m240 = (dm240_ds * sig_s) ^ 2 + (dm240_dd * sig_d) ^ 2 + (dm240_dm * sig_mult) ^ 2
        '   dm240_covar = (dm240_ds * dm240_dd * sig_t) * (sig_d + dm240_dd * dm240_dm * sig_d) * (sig_mult + dm240_ds * dm240_dm * sig_s * sig_mult)
        '   dm240_covar = dm240_ds * dm240_dd * covar_sd + dm240_dd * dm240_dm * covar_dm + dm240_ds * dm240_dm * covar_sm                                       '  ****** calcuated covariance between singles, doubles and multiplication

        sig_m240 = (sig_m240 + 2 * dm240_covar) ^ 0.5

        out_val(5) = m_240
        out_val(6) = sig_m240


        mult_anal_old = out_val(i_out)

    End Function


    Function mult_anal3(rates, rates_err, det_parms, fis_dat, i_out)

        Dim out_val(6)
        Dim nu_s1, nu_s2, nu_s3, nu_i1, nu_i2, nu_i3, f_f
        Dim n_eff, die, gate, doub_frac, trip_frac
        Dim singles, doubles, triples, sig_s, sig_d, sig_t
        Dim par_a, par_b, par_c, p93, mult
        Dim dm_ds, dm_dd, dm_dt
        Dim dm_covar, sig_mult, alpha1, d_alpha_dm, d_alpha_ds, d_alpha_dd
        Dim dalpha_covar, sig_alpha1, m_240, dm240_ds, dm240_dd, dm240_dm, dm240_covar, sig_m240, dm240_dt
        Dim covar_sd, covar_st, covar_dt

        nu_s1 = fis_dat(1)
        nu_s2 = fis_dat(2)
        nu_s3 = fis_dat(3)
        nu_i1 = fis_dat(4)
        nu_i2 = fis_dat(5)
        nu_i3 = fis_dat(6)
        f_f = fis_dat(7)

        n_eff = det_parms(1)
        die = det_parms(2)
        gate = det_parms(3)
        doub_frac = det_parms(4)
        trip_frac = det_parms(5)

        singles = rates(1)
        doubles = rates(2)
        triples = rates(3)
        '
        sig_s = rates_err(1)
        sig_d = rates_err(2)
        sig_t = rates_err(3)

        '  covar_sd = covar_m(1, 2)
        '  covar_st = covar_m(1, 3)
        '  covar_dt = covar_m(2, 3)
        covar_sd = 0
        covar_st = 0
        covar_dt = 0

        ' calc multiplication

        p93 = nu_s2 * nu_i3 - nu_s3 * nu_i2
        par_a = -3 * 2 * triples * nu_s2 * (nu_i1 - 1) / (n_eff ^ 2 * trip_frac * singles * p93)
        par_b = 2 * doubles * (nu_s3 * (nu_i1 - 1) - 3 * nu_s2 * nu_i2) / (n_eff * doub_frac * singles * p93)
        par_c = (6 * doubles * nu_s2 * nu_i2 / (n_eff * doub_frac * singles * p93)) - 1

        mult = (par_c + 2 - par_a) / (par_b + 2 * par_c + 3)

        For i = 1 To 5
            mult = (par_c * mult ^ 2 + 2 * mult ^ 3 - par_a) / (par_b + 2 * par_c * mult + 3 * mult ^ 2)
        Next i

        ' calc error in multiplication

        'calculate the partial derivative of m_240
        dm240_ds = -(1 - 1 / mult) * nu_i2 / ((nu_i1 - 1) * n_eff * nu_s2 * f_f)
        dm240_ds = dm240_ds - (4 * doubles / doub_frac / n_eff ^ 2 / nu_s2 / mult ^ 3 + nu_s2 / (nu_i1 - 1) / n_eff / nu_s2 / mult ^ 2) * dm_ds
        '
        dm240_dd = 2 / (n_eff * doub_frac * f_f * n_eff ^ 2 * mult ^ 2)
        dm240_dd = dm240_dd - (nu_i2 * singles / (nu_i1 - 1) / (n_eff * nu_s2 * f_f * mult ^ 2) + 4 * doubles / (nu_s2 * f_f * n_eff ^ 2 * mult ^ 3)) * dm_dd

        dm240_dt = -1 / (nu_i1 - 1) / nu_s2 / n_eff / f_f * (4 * doubles / (n_eff * f_d * mult ^ 3) + nu_i2 * singles / mult ^ 2) * dm_dt

        dm_covar = dm_ds * dm_dt * covar_st + dm_ds * dm_dd * covar_sd + dm_dd * dm_dt * covar_dt
        dm_covar = 0

        sig_mult = (dm_ds * sig_s) ^ 2 + (dm_dd * sig_d) ^ 2 + (dm_dt * sig_t) ^ 2
        '    sig_mult = (sig_mult + 2 * (dm_ds * dm_dd * sig_s * sig_d + dm_ds * dm_dt * sig_s * sig_t + dm_dd * dm_dt * sig_d * sig_t) + 2 * dm_covar) ^ 0.5
        sig_mult = sig_mult ^ 2

        out_val(1) = mult
        out_val(2) = sig_mult

        ' calc alpha, n value

        alpha1 = (mult * nu_s2 * singles / nu_s1) / ((2 * doubles / doub_frac / n_eff) - (mult * (mult - 1) * nu_i2 * singles / (nu_i1 - 1))) - 1

        d_alpha_dm = ((alpha1 + 1) / mult) * (1 + (alpha1 + 1) * nu_s1 / nu_s2 * nu_i2 * (2 * mult - 1) / (nu_i1 - 1))
        Dim d_alpha_dT    '   *******
        d_alpha_dT = d_alpha_dm * dm_dt       '   *******
        d_alpha_ds = ((alpha1 + 1) / singles) * (1 + (alpha1 + 1) * nu_s1 / nu_s2 * nu_i2 * (mult - 1) / (nu_i1 - 1))
        d_alpha_dd = (alpha1 + 1) ^ 2 * nu_s1 / nu_s2 / mult / singles * (2 / doub_frac / n_eff)
        dalpha_covar = d_alpha_ds * d_alpha_dd * covar_sd
        dalpha_covar = (d_alpha_dT * d_alpha_ds * sig_t * sig_s + d_alpha_dT * d_alpha_dd * sig_t * sig_d + d_alpha_ds * d_alpha_dd * sig_s * sig_d)
        sig_alpha1 = (d_alpha_dm * sig_mult) ^ 2 + (d_alpha_ds * sig_s) ^ 2 + (d_alpha_dd * sig_d) ^ 2
        ' sig_alpha1 = sig_alpha1 + 2 * (d_alpha_dm * d_alpha_ds * sig_mult * sig_s + d_alpha_dm * d_alpha_dd * sig_mult * sig_d + d_alpha_ds * d_alpha_dd * sig_s * sig_d)
        sig_alpha1 = (sig_alpha1 + 2 * dalpha_covar) ^ 0.5

        out_val(3) = alpha1
        out_val(4) = sig_alpha1

        m_240 = ((2 * doubles / n_eff / doub_frac) - (mult * (mult - 1) * nu_i2 * singles) / (nu_i1 - 1)) / (n_eff * mult ^ 2 * nu_s2) / f_f

        'calculate the partial derivative of m_240
        Dim par_00
        par_00 = (nu_i1 - 1) * n_eff * nu_s2 * f_f

        dm240_ds = -(1 - 1 / mult) * nu_i2 / par_00 - (4 * doubles / (doub_frac * n_eff ^ 2 * nu_s2 * f_f * mult ^ 3) + nu_s2 / par_00 / mult ^ 2) * dm_ds
        dm240_dd = 2 / (nu_s2 * f_f * n_eff ^ 2 * doub_frac * mult ^ 2) - (nu_i2 * singles / (par_00 * mult ^ 2) + 4 * doubles / (nu_s2 * f_f * n_eff ^ 2 * f_d * mult ^ 3)) * dm_dd
        dm240_dt = -1 / par_00 * (4 * doubles / (n_eff * f_d * mult ^ 3) + nu_i2 * singles / mult ^ 2) * dm_dt

        dm240_dm = -((4 * doubles / (n_eff ^ 2 * mult ^ 3 * doub_frac * f_f * nu_s2)) + (nu_i2 * singles / ((nu_i1 - 1) * n_eff * nu_s2 * f_f) / mult ^ 2))
        dm240_covar = dm240_ds * dm240_dd * covar_sd + dm240_dd * dm240_dt * covar_dt + dm240_ds * dm240_dt * covar_st
        ' dm240_covar = dm240_ds * dm240_dd * covar_sd + dm240_dd * dm240_dm * covar_dm + dm240_ds * dm240_dm * covar_sm

        sig_m240 = (dm240_ds * sig_s) ^ 2 + (dm240_dd * sig_d) ^ 2 + (dm240_dt * sig_t) ^ 2 + 0 * (dm240_dm * sig_mult) ^ 2
        sig_m240 = (sig_m240 + 2 * dm240_covar) ^ 0.5

        out_val(5) = m_240
        out_val(6) = sig_m240

        mult_anal3 = out_val(i_out)

    End Function

    ' This function is a replacement for the Microsoft Excel Worksheet function NORMSINV.
    ' It uses the algorithm of Peter J. Acklam to compute the inverse normal cumulative
    ' distribution. 
    ' Adapted to VB by Christian d'Heureuse
    Public Function NormSInv(ByVal p As Double) As Double
        Const a1 = -39.6968302866538, a2 = 220.946098424521, a3 = -275.928510446969
        Const a4 = 138.357751867269, a5 = -30.6647980661472, a6 = 2.50662827745924
        Const b1 = -54.4760987982241, b2 = 161.585836858041, b3 = -155.698979859887
        Const b4 = 66.8013118877197, b5 = -13.2806815528857, c1 = -0.00778489400243029
        Const c2 = -0.322396458041136, c3 = -2.40075827716184, c4 = -2.54973253934373
        Const c5 = 4.37466414146497, c6 = 2.93816398269878, d1 = 0.00778469570904146
        Const d2 = 0.32246712907004, d3 = 2.445134137143, d4 = 3.75440866190742
        Const p_low = 0.02425, p_high = 1 - p_low
        Dim q As Double, r As Double
        If p < 0 Or p > 1 Then
            'Err.Raise vbObjectError, , "NormSInv: Argument out of range."
        ElseIf p < p_low Then
            q = Sqrt(-2 * Log(p))
            NormSInv = (((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) /
         ((((d1 * q + d2) * q + d3) * q + d4) * q + 1)
        ElseIf p <= p_high Then
            q = p - 0.5 : r = q * q
            NormSInv = (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q /
         (((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1)
        Else
            q = Sqrt(-2 * Log(1 - p))
            NormSInv = -(((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) /
         ((((d1 * q + d2) * q + d3) * q + d4) * q + 1)
        End If

    End Function


    Function radial_err(del_Radius, R_Cav, T_L, cav_ID, container_Dia, n_it)
        Dim z1, radial_response, del_r, temp
        ' z1 --> vertical distance
        ' del_R --> avgerage_radial offset
        ' R_cav --> effective cavity IR (generally radius of outer most tube ring)
        ' T_L --> effective cavity height (generally the active tube length)
        ' cav_ID --> true cavity inner diameter
        ' container_Dia --> container diameter
        ' normal distribution for off_set distance between 0 and del_Radius cm
        ' z1=0 ; determined for deviation at cavity vertical center line
        z1 = 0
        radial_response = 0
        For i = 1 To n_it
            del_r = del_Radius * (1 + NormSInv(Rnd()))
            If del_r > (cav_ID - container_Dia) / 2 Then del_r = (cav_ID - container_Dia) / 2
            temp = 1 / ((4 * PI - 2 * (R_Cav ^ 2 + 4 * del_r ^ 2) * (T_L ^ 2 + 4 * z1 ^ 2) / (T_L ^ 2 - 4 * z1 ^ 2) ^ 2) / (4 * PI - 2 * (R_Cav / T_L) ^ 2)) ^ 2
            radial_response = radial_response + temp ^ 1.5
        Next i
        radial_err = radial_response / n_it

    End Function
    Private Sub get_misc_par_file(misc_par_file)
        Dim myStream As Stream = Nothing


        Dim str11, str12, str13, str14 As String
        Dim idex, jdex As Integer
        Dim misc_par_file_name As String
        Dim sep_val1, sep_val2 As String
        Dim temp_pars(12)

        misc_par_file_name = misc_par_file

        idex = 0
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(misc_par_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    idex = idex + 1

                    jdex = 0
                    Dim currentField As String
                    For Each currentField In currentRow
                        jdex = jdex + 1

                        str11 = currentField


                        If jdex = 2 And idex > 0 And idex < 10 Then temp_pars(idex) = Val(str11)

                    Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

        For i = 1 To 10
            misc_param(i) = temp_pars(i)

        Next i
        ' 


    End Sub

    Public Sub get_empirical_par_file(empirical_par_file)
        Dim myStream As Stream = Nothing


        Dim str11, str12, str13, str14 As String
        Dim idex, jdex As Integer
        Dim empirical_par_file_name As String
        Dim sep_val1, sep_val2 As String
        Dim temp_pars(78), max_items
        max_items = 78

        empirical_par_file_name = empirical_par_file

        For i = 1 To 10
            mult_par(i) = 0
            dev_pu240_par(i) = 0
            dev_UPu_par(i) = 0
            dev_mod_par(i) = 0
            dev_rho_par(i) = 0
            dev_wall_par(i) = 0
        Next i

        For i = 1 To 18
            dev_alpha_par(i) = 0
        Next i

        idex = 0
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(empirical_par_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    idex = idex + 1

                    jdex = 0
                    Dim currentField As String
                    For Each currentField In currentRow
                        jdex = jdex + 1

                        str11 = currentField


                        If jdex = 2 And idex > 0 And idex < (max_items + 1) Then temp_pars(idex) = Val(str11)

                    Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

        For i = 1 To 10
            mult_par(i) = temp_pars(i)
            dev_pu240_par(i) = temp_pars(i + 10)
            dev_UPu_par(i) = temp_pars(i + 20)
            dev_mod_par(i) = temp_pars(i + 48)
            dev_rho_par(i) = temp_pars(i + 58)
            dev_wall_par(i) = temp_pars(i + 68)
        Next i
        For i = 1 To 18
            dev_alpha_par(i) = temp_pars(i + 30)
        Next i

    End Sub

    Function calc_rates_LANL(A_R, ACCID, A_R_len, ACC_len, det_parms, assay_time, i_out)
        Dim alph(512), bet(512), mr(512)
        Dim p_i(512), q_i(512), r_1(512), r_2(512), r_3(512), gam(512), g_2(512)
        Dim out_rates(10), count_time

        Dim a_r_sum, Phi, doub_err_1, r_norm
        Dim s_err, d_err, t_err, sum_ar, sum_acc
        Dim singles, doubles, triples, quads
        Dim comb_q, g_temp, sum_r_1, sum_r_2, sum_r_3, sum_r_4
        Dim mn0, mn1, mn2, mn3, mn4, mn5
        Dim mb0, mb1, mb2, mb3, mb4, mb5
        Dim mr0, mr1, mr2, mr3, mr4, mr5
        Dim mlt_totals, dead_time_factor, doubles_dt_factor, triples_dt_factor, quads_dt_factor
        Dim g_w, Gamma, s_d_mult, A_T
        Dim C_S, C_R_A, C_A

        Dim num_bins, i As Integer

        dt_par_a = det_parms(6) / 1000000.0#              ' us to seconds
        dt_par_b = det_parms(7) / 1000000000000.0#        ' uus (1E-12) to seconds
        dt_par_c = det_parms(8) / 1000000000.0#           ' ns
        dt_par_d = det_parms(9) / 1000000000.0#           ' ns
        dead_time_parm = det_parms(10)

        die_away_t = det_parms(2)
        gate_width = det_parms(3)
        f_d = det_parms(4)
        f_t = det_parms(5)

        sum_ar = 0
        sum_acc = 0
        C_S = 0
        C_R_A = 0
        C_A = 0

        num_bins = ACC_len
        If A_R_len > ACC_len Then num_bins = A_R_len

        For i = 1 To num_bins
            sum_ar = sum_ar + A_R(i)
            sum_acc = sum_acc + ACCID(i)
        Next i

        Phi = dead_time_parm / gate_width / 1000

        For i = 1 To num_bins
            p_i(i - 1) = A_R(i) / sum_ar
            q_i(i - 1) = ACCID(i) / sum_acc
            alph(i) = (1 + (i - 1) * Phi + 3 / 2 * (i) ^ 2 * Phi ^ 2 + 5 / 2 * (i - 1) ^ 3 * Phi ^ 3 + 7 / 2 * (i - 1) ^ 4 * Phi ^ 4) * i
            bet(i) = (alph(i) / i) ^ 2 * (i * (i - 1) / 2)
            C_S = C_S + ACCID(i)
            C_A = C_A + ACCID(i) * i
            C_R_A = C_R_A + A_R(i) * i
        Next i
        gam(1) = 0
        gam(2) = 0
        gam(3) = bet(3) - (alph(3) - 1)
        '
        For i = 4 To num_bins
            gam(i) = bet(i) - (alph(i) - 1)

            For k = 0 To i - 4
                comb_q = Combinatorial(i - 1, k + 3)
                g_temp = (k + 1) * (k + 2) * (k + 3) ^ k * Phi ^ k / (1 - (k + 3) * Phi) ^ (k + 4)
                gam(i) = gam(i) + comb_q / 2 * g_temp
            Next k

        Next i


        sum_r_1 = 0
        sum_r_2 = 0
        sum_r_3 = 0
        sum_r_4 = 0
        '
        For i = 1 To num_bins
            r_1(i) = alph(i) * (p_i(i) - q_i(i))
            sum_r_1 = sum_r_1 + r_1(i)
        Next i
        '
        For i = 1 To num_bins
            r_2(i) = 2 * ((bet(i) * (p_i(i) - q_i(i))) - sum_r_1 * (alph(i) * q_i(i)))
            sum_r_2 = sum_r_2 + r_2(i)
        Next i

        mn0 = 0
        mn1 = 0
        mn2 = 0
        mn3 = 0
        mn4 = 0
        mn5 = 0
        mb0 = 0
        mb1 = 0
        mb2 = 0
        mb3 = 0
        mb4 = 0
        mb5 = 0
        mr0 = 0
        mr1 = 0
        mr2 = 0
        mr3 = 0
        mr4 = 0
        mr5 = 0


        For i = 1 To num_bins
            mr(i) = p_i(i) - q_i(i)
            mn0 = mn0 + p_i(i)
            mn1 = mn1 + p_i(i) * alph(i)
            mn2 = mn2 + p_i(i) * bet(i)
            mn3 = mn3 + p_i(i) * gam(i)
            mn4 = mn4 + p_i(i) * 0
            mn5 = mn5 + p_i(i) * 0
            mb0 = mb0 + q_i(i)
            mb1 = mb1 + q_i(i) * alph(i)
            mb2 = mb2 + q_i(i) * bet(i)
            mb3 = mb3 + q_i(i) * gam(i)
            mb4 = mb4 + q_i(i) * 0
            mb5 = mb5 + q_i(i) * 0
            mr0 = mr0 + mr(i)
            mr1 = mr1 + mr(i) * alph(i)
            mr2 = mr2 + mr(i) * bet(i)
            mr3 = mr3 + mr(i) * gam(i)
            mr4 = mr4 + mr(i) * 0
            mr5 = mr5 + mr(i) * 0
        Next i

        Dim dead_time_totals, dead_time_reals, totals, reals

        g_w = gate_width * 0.000001
        mlt_totals = sum_acc / assay_time
        count_time = sum_acc / mlt_totals
        dead_time_factor = Exp(dead_time_parm * 10 ^ -9 * mlt_totals)
        dead_time_totals = Exp((dt_par_a + dt_par_b * mlt_totals) * mlt_totals / 4)
        dead_time_reals = Exp((dt_par_a + dt_par_b * mlt_totals) * mlt_totals)
        doubles_dt_factor = Exp(mlt_totals * dt_par_c)
        '   triples_dt_factor = Exp(mlt_totals * dt_par_d)

        triples_dt_factor = Exp(mlt_totals * dt_par_c)

        '
        quads_dt_factor = Exp(mlt_totals * dt_par_d)

        'gate_width
        totals = mlt_totals * dead_time_totals
        reals = (C_R_A - C_A) / assay_time * dead_time_reals
        '      singles = mlt_totals * * dead_time_factor
        '      doubles = sum_r_1 * mlt_totals * dead_time_factor * doubles_dt_factor
        singles = totals
        doubles = reals
        triples = sum_r_2 * mlt_totals * dead_time_factor * triples_dt_factor / 2
        g_w = gate_width * 0.000001
        'singles = mb1 / g_w * dead_time_factor
        'doubles = mb1 / g_w * mr1 * doubles_dt_factor * dead_time_factor
        'triples = mb1 / g_w * (mr2 - mb1 * mr1) * triples_dt_factor * dead_time_factor
        quads = mb1 / g_w * (mr3 - mb2 * mr1 - mb1 * (mr2 - mb1 * mr1)) * quads_dt_factor * dead_time_factor

        Gamma = 1 - (1 - Exp(-gate_width / die_away_t)) / (gate_width / die_away_t)
        s_d_mult = (1 + 8 * Gamma * doubles / f_d / singles) ^ 0.5
        A_T = (1 + f_t / f_d) * singles * doubles * gate_width * 0.000001 + 0.5 * singles * (singles * gate_width * 0.000001) ^ 2
        '
        '  Singles and doubles error from INCC software manual
        '  Triples error from Croft et.al. A' Priori Precision estimates for Neutron Triples Counting, ANIMMA 6-9 June 2011 Ghent Belgium
        '

        s_err = (1 + 2 * doubles / f_d / singles) ^ 0.5 * sum_acc ^ 0.5 / count_time
        d_err = s_d_mult * ((singles ^ 2 * gate_width * 0.000001 * 2 + doubles) / count_time) ^ 0.5
        t_err = Sqrt(2 * (singles ^ 3 * (gate_width * 0.000001) ^ 2 + triples) / assay_time)
        '' t_err = 2 * ((singles ^ 3 * gate_width ^ 2 * 0.000001 ^ 2 + 2.5 * triples) / assay_time) ^ 0.5
        ' t_err = (1 + 8 * f_t * doubles / f_d / singles) ^ 0.5 * ((triples + 2 * A_T) / count_time) ^ 0.5
        ' Gamma = 1 - (1 - Exp(-gate_width / die_away_t)) / (gate_width / die_away_t)
        '  t_err = Sqrt((2 * singles ^ 3 * (gate_width * 0.000001) ^ 2 + triples) / assay_time) * Sqrt(2 + 0 * Gamma * doubles / singles / f_d)

        out_rates(1) = singles
        out_rates(2) = s_err
        out_rates(3) = doubles
        out_rates(4) = d_err
        out_rates(5) = triples
        out_rates(6) = t_err
        out_rates(7) = quads
        out_rates(9) = sum_ar
        out_rates(10) = sum_acc

        calc_rates_LANL = out_rates(i_out)

    End Function

    Function mult_anal_err2_LANL(A_R, ACCID, A_R_len, ACC_len, det_parms, det_parms_err, fis_dat, fis_dat_err, assay_time, p_out, i_out, j_out)
        Dim err_arr(3, 11, 2), det_parms_l(11), det_parms_h(11), rates(3), rates_err(3)
        Dim fis_parms_1(7), fis_parms_h(7), m_l, m_h
        Dim i_dex, ij, ik As Integer
        '   p_out = 1 <===== contributions to multiplication error
        '   p_out = 3 <===== contributions to alpha error
        '   p_out = 3 <===== contributions to mass error
        '   j_out = 1 <====== errors from detector parameters
        '   j_out = 2 <====== errors from nuclear data

        ' detector parameter contributions

        If p_out = 1 Then i_dex = 1
        If p_out = 2 Then i_dex = 3
        If p_out = 3 Then i_dex = 5

        For j = 1 To 10
            For i = 1 To 10
                det_parms_l(i) = det_parms(i)
                det_parms_h(i) = det_parms(i)
            Next i

            det_parms_l(j) = det_parms(j) - det_parms_err(j) / 2

            For ij = 1 To 3
                ik = 2 * ij - 1
                rates(ij) = calc_rates_LANL(A_R, ACCID, A_R_len, ACC_len, det_parms_l, assay_time, ik)
                rates_err(ij) = calc_rates_LANL(A_R, ACCID, A_R_len, ACC_len, det_parms_l, assay_time, ik + 1)
            Next ij

            m_l = mult_anal(rates, rates_err, det_parms_l, fis_dat, i_dex)
            '        mult_anal2(rates, rates_err, covar_m, det_parms, fis_dat, i_out)
            det_parms_h(j) = det_parms(j) + det_parms_err(j) / 2

            For ij = 1 To 3
                ik = 2 * ij - 1
                rates(ij) = calc_rates_LANL(A_R, ACCID, A_R_len, ACC_len, det_parms_h, assay_time, ik)
                rates_err(ij) = calc_rates_LANL(A_R, ACCID, A_R_len, ACC_len, det_parms_h, assay_time, ik + 1)
            Next ij

            m_h = mult_anal(rates, rates_err, det_parms_h, fis_dat, i_dex)
            err_arr(3, j, 1) = m_h - m_l

        Next j

        For ij = 1 To 3
            ik = 2 * ij - 1
            rates(ij) = calc_rates(A_R, ACCID, A_R_len, ACC_len, det_parms, assay_time, ik)
            rates_err(ij) = calc_rates(A_R, ACCID, A_R_len, ACC_len, det_parms, assay_time, ik + 1)
        Next ij

        '   nuclear data contributions to error


        For j = 1 To 7
            For i = 1 To 7
                fis_parms_1(i) = fis_dat(i)
                fis_parms_h(i) = fis_dat(i)
            Next i


            fis_parms_1(j) = fis_dat(j) - fis_dat_err(j) / 2
            m_l = mult_anal(rates, rates_err, det_parms, fis_parms_1, i_dex)

            fis_parms_h(j) = fis_dat(j) + fis_dat_err(j) / 2
            m_h = mult_anal(rates, rates_err, det_parms, fis_parms_h, i_dex)

            err_arr(3, j, 2) = m_h - m_l
        Next j

10:     mult_anal_err2_LANL = err_arr(3, i_out, j_out)


    End Function

End Module


