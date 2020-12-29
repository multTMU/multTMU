Imports System.IO
Imports System.Text
Imports System.Math

Module Module2
    Public Radial_Bias_Out, Radial_Random_out, Pt_source_bias_out
    Public vol_avg(11), eff_avg(21), vol_inc, vol_delta_max, Rad_0, Vol_center, vol_average_fill_efficiency
    Public mass_rand(20000), eff_rnd(20000), st_dev_rnd_mass, mass_rand_v(20000), eff_rnd_v(20000)
    Public eff_r(101), samp_pts, cavity_ID
    Public average_density, minimum_density, maximum_density, minimum_fill_height, maximum_fill_height, pu_mass_fraction, average_fill_height

    Public min_fill_mass, max_fill_mass, vol_center_mass
    Public avg_rnd_mass, avg_fill_mass, m240_center, m240_most_likely
    Public fd_adjust_cal, ft_adjust_cal
    Public distrib_cal_mass, distrib_cal_bias, pt_cal_bias, rand_fill_height_err
    Public z, cavity_H, eff_z(101), avg_240(21), pt_bias, distrib_bias

    Sub calc_radial_error()
        '       Dim container_IDiam, temp_eff, temp_fd, temp_ft, typical_container_offset, radial_likely_offset, Random_container_offset
        Dim det_dim_file_name, file_location As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        ' Read in seed parameters from generic file

        Dim item_param_file As String
        file_location = "c:\multiplicity_TMU\item_info\"
        item_param_file = file_location & "current_item.csv"

        Call get_item_info_file(item_param_file)                        ' read in item specific information from csv file

        ' **** overwrite csv item file parameters with INCC detector isotopics read in from text file
        If use_file_iso_flag Then iso_date(1) = file_pu_date
        If use_file_iso_flag Then iso_date(2) = file_am_date
        For i = 1 To 7
            If use_file_iso_flag Then iso_val(i) = file_iso(i) / 100
            If use_file_iso_flag Then iso_val_err(i) = file_iso_err(i) / 100
        Next i

        det_dim_file_name = "c:\multiplicity_tmu\detector_dimensions\current_det_dimensions.csv"

        Call get_det_dimensions_file(det_dim_file_name)



        container_IDiam = geo_par(2)
        typical_container_offset = geo_par(8)
        Random_container_offset = geo_par(9)

        ' ******************************************************************************************************
        '
        '  ******* CALC RADIAL RESPONSE  ********
        '
        ''******************************************************************************************************

        Dim cavity_ID, Radial_offset, radial_sensitivity, Avg_radial_offset, max_rad_dev, typical_rad_offset
        Dim cavity_H, z_offset, i_dex


        Dim R_cav, T_L, sing_err1, m240_center, m240_most_likely, del_eff

        cavity_H = det_dim_val(2)
        cavity_ID = det_dim_val(3)

        temp_eff = det_par_val(1)
        temp_fd = det_par_val(4)
        temp_ft = det_par_val(5)
        '                     
        radial_random = 0
        '
        Radial_offset = geo_par(8)
        z_offset = 0
        max_rad_dev = (cavity_ID - container_IDiam) / 2
        '
        i_dex = det_dim_val(8)          ' number of tube rows to identify outer most tube diameter
        R_cav = det_dim_val(i_dex + 9)  ' container_IDiam    effective cavity diameter is the diameter of the outermost row of tubes
        T_L = det_dim_val(2) + R_cav            ' cavity_H + R_cav    effective height is the cavity height plus the effective cavity diameter or the tube length
        '
        ''******************************************************************************************************
        '
        ' calculate mass at center of cavity for reference
        '
        ''******************************************************************************************************
        '
        m240_center = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)   '  assumes calibration and assay items are similar

        ' calculate efficiency change at most likely can off set
        '
        'radial_likely_offset = 1 / ((4 * PI - 2 * (R_cav ^ 2 + 4 * Radial_offset ^ 2) * (T_L ^ 2 + 4 * z_offset ^ 2) / (T_L ^ 2 - 4 * z_offset ^ 2) ^ 2) / (4 * PI - 2 * (R_cav / T_L) ^ 2)) ^ 2
        '
        radial_likely_offset = rad_eff(T_L, R_cav, Radial_offset, z_offset)

        det_par_val(1) = temp_eff * radial_likely_offset        ' adjust efficiency for typical radial offset 
        del_eff = radial_likely_offset - 1                      ' incremental relative change in effieincy relative to cavity center
        det_par_val(4) = (1 + del_eff * 0.325) * temp_fd        ' adjust gate fractions - scaled to change in efficiency
        det_par_val(5) = (1 + del_eff * 0.325) ^ 2 * temp_ft
        '
        ' calculate m240 mass at most likely can location
        '
        m240_most_likely = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)
        '
        '  return detector parameters to original values
        '
        det_par_val(1) = temp_eff
        det_par_val(4) = temp_fd
        det_par_val(5) = temp_ft
        '
        ' Create array of Efficiency as a function of the radial offect for a point source for plot
        '
        For i = 1 To 50
            Radial_offset = cavity_ID / 100 * (i - 1)
            eff_r(i + 50) = rad_eff(T_L, R_cav, Radial_offset, z_offset)
            eff_r(51 - i) = rad_eff(T_L, R_cav, Radial_offset, z_offset)
        Next i
        '
        ''******************************************************************************************************
        '
        ' Estimate efficiency averaged over the can diameter as a function of radial offset (but dont let can move outside of cavity) for plot
        '
        ''******************************************************************************************************
        For i = 1 To 10
            eff_avg(i) = 0
        Next i

        vol_delta_max = (cavity_ID - container_IDiam) / 2         '   difference between can OR and cavity IR - room to rattle
        vol_inc = container_IDiam / 20

        For i = 1 To 10
            Rad_0 = vol_delta_max / 10 * (i - 1)                   '  look at average efficiency accross the can  as a function of distance from cavity center
            For j = -10 To 10
                Radial_offset = Rad_0 + vol_inc * j
                eff_avg(i) = eff_avg(i) + (1 / 21) * rad_eff(T_L, R_cav, Radial_offset, z_offset)
            Next j
        Next i
        Vol_center = eff_avg(1)

        For i = 1 To 10
            eff_avg(i) = (eff_avg(i) / Vol_center)
            vol_avg(i) = eff_avg(i)       ' Can weighted efficiency as a function of offset
        Next i

        det_par_val(1) = temp_eff / Vol_center
        del_eff = Vol_center - 1
        det_par_val(4) = (1 + del_eff * 0.325) * temp_fd
        det_par_val(5) = (1 + del_eff * 0.325) ^ 2 * temp_ft

        vol_center_mass = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)   ' should be the same as m240_center
        '
        '  return detector parameters to original values
        '
        det_par_val(1) = temp_eff
        det_par_val(4) = temp_fd
        det_par_val(5) = temp_ft

        '
        ''******************************************************************************************************
        '
        '      Calculate radial bias for m240 effective 
        '  
        ''******************************************************************************************************
        Dim avg_mass_bias

        avg_mass_bias = 0
        For i = 1 To 10
            det_par_val(1) = temp_eff * eff_avg(i)
            del_eff = eff_avg(i) - 1
            det_par_val(4) = (1 + del_eff * 0.325) * temp_fd
            det_par_val(5) = (1 + del_eff * 0.325) ^ 2 * temp_ft

            avg_240(i) = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)
            avg_mass_bias = avg_mass_bias + avg_240(i) / 10
        Next i

        det_par_val(1) = temp_eff

        'Estimate error from random placement about the typical offset

        Dim Radial_offset_0, Random_offset, n_div

        Dim rand1 As Double
        Radial_offset_0 = geo_par(8)        ' Typical loading radial offset
        Random_offset = geo_par(9)          ' st deviation of offset about the typical point
        n_div = 40                            ' number of divisions
        vol_inc = container_IDiam / n_div

        avg_rnd_mass = 0
        max_rad_dev = (cavity_ID - container_IDiam) / 2

        det_par_val(1) = temp_eff
        det_par_val(4) = temp_fd
        det_par_val(5) = temp_ft

        samp_pts = misc_param(6)
        If samp_pts < 1 Then samp_pts = 1
        If samp_pts > 20000 Then samp_pts = 20000
        Dim rad_avg, radial(20000), n_pts, mass_rand_min, mass_rand_max

        rad_avg = 0
        n_pts = 0
        For i = 1 To samp_pts
            rand1 = Rnd()
            Rad_0 = NormSInv(rand1) * Random_offset + Radial_offset_0   '  can center
            radial(i) = Rad_0
            eff_rnd(i) = 0
            rad_avg = rad_avg + Rad_0
            For j = -n_div / 2 To n_div / 2
                Radial_offset = Rad_0 + vol_inc * j
                eff_rnd(i) = eff_rnd(i) + rad_eff(T_L, R_cav, Radial_offset, z_offset)
            Next j
            eff_rnd(i) = eff_rnd(i) / (n_div + 1)
            det_par_val(1) = temp_eff / eff_rnd(i)
            del_eff = det_par_val(1) / temp_eff - 1
            det_par_val(4) = (1 + del_eff * 0.325) * temp_fd
            det_par_val(5) = (1 + del_eff * 0.325) ^ 2 * temp_ft

            mass_rand(i) = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)

        Next i

        n_pts = 0
        For i = 1 To samp_pts
            If Abs(radial(i)) + container_IDiam / 2 < cavity_ID / 2 Then n_pts = n_pts + 1
            If Abs(radial(i)) + container_IDiam / 2 < cavity_ID / 2 Then avg_rnd_mass = avg_rnd_mass + mass_rand(i)

        Next i

        avg_rnd_mass = avg_rnd_mass / n_pts

        det_par_val(1) = temp_eff
        det_par_val(4) = temp_fd
        det_par_val(5) = temp_ft

        rad_avg = rad_avg / samp_pts

        Dim mass_comp As String
        mass_comp = "m240 center " & m240_center & vbCrLf
        mass_comp = mass_comp & "most likely " & m240_most_likely & vbCrLf
        mass_comp = mass_comp & "vol center " & vol_center_mass & vbCrLf
        mass_comp = mass_comp & "avg rand " & avg_rnd_mass & vbCrLf
        '        MsgBox(mass_comp)

        st_dev_rnd_mass = 0

        For i = 1 To samp_pts
            If Abs(radial(i)) + container_IDiam / 2 < cavity_ID / 2 Then st_dev_rnd_mass = st_dev_rnd_mass + (mass_rand(i) - avg_rnd_mass) ^ 2
        Next i
        st_dev_rnd_mass = (st_dev_rnd_mass / (n_pts - 1)) ^ 0.5


        Radial_Bias_Out = (avg_rnd_mass / vol_center_mass) - 1
        Radial_Random_out = st_dev_rnd_mass / avg_rnd_mass
        Pt_source_bias_out = (m240_center / vol_center_mass) - 1

    End Sub


    Public Sub calc_axial_errors()
        '
        '  ******* CALC AXIAL RESPONSE AND BIASES  ********
        '
        Dim myStream As Stream = Nothing
        Dim det_dimfile_name, matrix_type As String
        Dim idex, jdex As Integer

        Dim cavity_ID, Radial_offset, radial_sensitivity, Avg_radial_offset, max_rad_dev, typical_rad_offset
        Dim z_offset, i_dex

        Dim R_cav, T_L, sing_err1, del_eff, expected_UPU_ratio

        idex = 0
        container_IDiam = geo_par(2)
        container_H = geo_par(3)
        typical_container_offset = geo_par(8)
        Random_container_offset = geo_par(9)
        matrix_type = geo_par(10)
        expected_UPU_ratio = geo_par(11)
        detector_stand = det_dim_val(15)

        pu_mass_fraction = 1
        If matrix_type = "MOX" Then pu_mass_fraction = (239 / (239 + 32) / expected_UPU_ratio)
        If matrix_type = "PuO2" Then pu_mass_fraction = (239 / (239 + 32))
        If matrix_type = "metal" Then pu_mass_fraction = 1


        average_density = geo_par(14)
        minimum_density = geo_par(15)
        maximum_density = geo_par(16)

        If minimum_density <= 0 Then minimum_density = 0.0012
        If maximum_density <= average_density Then maximum_density = average_density + 0.0012

        average_fill_height = Final_mass_new / average_density / pu_mass_fraction / (PI * (container_IDiam / 2) ^ 2)
        minimum_fill_height = Final_mass_new / maximum_density / pu_mass_fraction / (PI * (container_IDiam / 2) ^ 2)
        maximum_fill_height = Final_mass_new / minimum_density / pu_mass_fraction / (PI * (container_IDiam / 2) ^ 2)



        fd_adjust_cal = 0
        ft_adjust_cal = 0

        cavity_H = det_dim_val(2)
        cavity_ID = det_dim_val(3)

        temp_eff = det_par_val(1)                   ' remember original detector efficiency
        temp_fd = det_par_val(4)                    ' remember original detector doubles gate fraction
        temp_ft = det_par_val(5)                    ' remember original detector triples gate fraction
        '                     
        z_offset = 0
        '
        Radial_offset = geo_par(8)                  ' typical radial offset from user input file

        max_rad_dev = (cavity_ID - container_IDiam) / 2
        '
        i_dex = det_dim_val(8)                      ' number of tube rows to identify outer most tube diameter
        R_cav = det_dim_val(i_dex + 9)              ' container_IDiam    effective cavity diameter is the diameter of the outermost row of tubes
        T_L = cavity_H + R_cav                      ' cavity_H + R_cav    effective height is the cavity height plus the effective cavity diameter or the tube length
        '
        '
        ' calculate mass using reference detector parameters (typically from a 240Pu point source at cavity center)
        '
        m240_center = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)   '  assumes calibration and assay items are similar

        '
        ' calculate efficiency change at most likely can off set (e.g. at typical radial offset at can centerline)
        ' use for reference mass for deteriming axial bias contribution - to distinguish from radial contribution
        '
        z_offset = 0
        radial_likely_offset = rad_eff(T_L, R_cav, Radial_offset, z_offset)       ' efficiency at container fill centerline
        '
        det_par_val(1) = temp_eff / radial_likely_offset            ' adjust efficiency for typical radial offset 
        del_eff = radial_likely_offset - 1                          ' incremental relative change in effieincy relative to cavity center
        det_par_val(4) = (1 - del_eff * fd_adjust_cal) * temp_fd    ' adjust gate fractions - scaled to change in efficiency - typical relationship determined from PSMC MCNP runs
        det_par_val(4) = (1 - del_eff * ft_adjust_cal) * temp_fd
        '
        ' calculate m240 mass at most likely can location
        '
        m240_most_likely = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)    ' mass result using efficiec
        '
        '
        '  return detector parameters to original values
        det_par_val(1) = temp_eff
        det_par_val(4) = temp_fd
        det_par_val(5) = temp_ft
        z_offset = 0
        ' 
        ' ********   Efficiency as a function of fill Height for plot (for chart1)*************
        ' Create array of efficiency as a function of the hieght above cavity floor for a point source for plot,  eff_z(i)
        '
        For i = 1 To 101
            Radial_offset = 0                                               '   caluclate efficiency at cavity axial center-line
            z_offset = cavity_H / 100 * (i - 1) - cavity_H / 2              '   point source location - vary from cavity bottom to cavity top
            eff_z(i) = z_eff(T_L, R_cav, Radial_offset, z_offset)           '   efficiency array at cavity center-line
        Next i
        '
        '
        '  ******* Estimate efficiency as a function of fill height for plot (chart2) ***********
        '
        '   Create array, eff_avg(i), of estimated average efficiency as a function of fill hieght for a series of fill heights 
        '   in increments of 1/20th of the container height
        '
        ''***************************************************************************************
        For i = 1 To 21
            eff_avg(i) = 0                                                  ' initialize array
        Next i
        '
        Radial_offset = geo_par(8)                                          ' return radial offset to typical offset value
        '
        Dim z_0, delta_z, z_max, delta_zz
        '
        z_0 = detector_stand - cavity_H / 2         '   sample stand distance from center line
        delta_z = container_H / 20                  '   container fill height
        '
        For i = 1 To 20
            z_max = delta_z * (i - 1)                   ' position of bottom of each efficiency eleement for each fill height
            delta_zz = z_max / 20
            For j = 1 To 21
                z_offset = z_0 + j * delta_zz           ' efficiency of each element average over 20 positions within the element
                eff_avg(i) = eff_avg(i) + z_eff(T_L, R_cav, Radial_offset, z_offset) / 21
            Next j
        Next i
        '
        '
        '  ********************************************************************************
        '
        ' Calculate average efficiency for expected fill height and bounding limits
        '
        Dim avg_fill_eff, min_fill_eff, max_fill_eff, cal_height, cal_eff

        avg_fill_eff = 0
        min_fill_eff = 0
        max_fill_eff = 0
        cal_eff = 0

        z_0 = detector_stand - cavity_H / 2         '   sample stand distance from center line

        delta_zz = average_fill_height / 20

        For j = 1 To 21
            z_offset = z_0 + j * delta_zz
            avg_fill_eff = avg_fill_eff + z_eff(T_L, R_cav, Radial_offset, z_offset) / 21      '  efficiency at average fill height
        Next j

        delta_zz = minimum_fill_height / 20

        For j = 1 To 21
            z_offset = z_0 + j * delta_zz
            min_fill_eff = min_fill_eff + z_eff(T_L, R_cav, Radial_offset, z_offset) / 21     '  efficiency at minimum fill height
        Next j

        delta_zz = maximum_fill_height / 20

        For j = 1 To 21
            z_offset = z_0 + j * delta_zz
            max_fill_eff = max_fill_eff + z_eff(T_L, R_cav, Radial_offset, z_offset) / 21     '  efficiency at maximum fill height
        Next j

        If det_cal_FH <> 0 Then delta_zz = det_cal_FH / 20 Else delta_zz = 0.75 * container_H / 20

        For j = 1 To 21
            z_offset = z_0 + j * delta_zz
            cal_eff = cal_eff + z_eff(T_L, R_cav, Radial_offset, z_offset) / 21     '  efficiency at maximum fill height
        Next j


        det_par_val(1) = temp_eff / avg_fill_eff
        vol_average_fill_efficiency = temp_eff / avg_fill_eff
        del_eff = avg_fill_eff - 1
        det_par_val(4) = (1 - del_eff * fd_adjust_cal) * temp_fd
        det_par_val(5) = (1 - del_eff * ft_adjust_cal) * temp_ft

        avg_fill_mass = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)   '   assay mass result at average fill height

        det_par_val(1) = temp_eff / min_fill_eff
        del_eff = min_fill_eff - 1
        det_par_val(4) = (1 - del_eff * fd_adjust_cal) * temp_fd
        det_par_val(5) = (1 - del_eff * ft_adjust_cal) * temp_ft

        min_fill_mass = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5) '   assay mass result at minimum fill height

        det_par_val(1) = temp_eff / max_fill_eff
        del_eff = max_fill_eff - 1
        det_par_val(4) = (1 - del_eff * fd_adjust_cal) * temp_fd
        det_par_val(5) = (1 - del_eff * ft_adjust_cal) * temp_ft

        max_fill_mass = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5) '   assay mass result at maximum fill height

        '  return detector parameters to original values
        det_par_val(1) = temp_eff
        det_par_val(4) = temp_fd
        det_par_val(5) = temp_ft

        det_par_val(1) = temp_eff / cal_eff
        del_eff = cal_eff - 1
        det_par_val(4) = (1 - del_eff * fd_adjust_cal) * temp_fd
        det_par_val(5) = (1 - del_eff * ft_adjust_cal) * temp_ft

        distrib_cal_mass = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5) '   assay mass result at maximum fill height

        '  return detector parameters to original values
        det_par_val(1) = temp_eff
        det_par_val(4) = temp_fd
        det_par_val(5) = temp_ft

        '
        '   ***************************************************************************
        '
        'Calculate axial bias for m240 effective 
        '  
        Dim avg_mass_bias

        avg_mass_bias = 0
        For i = 1 To 20
            det_par_val(1) = temp_eff * eff_avg(i)
            del_eff = eff_avg(i) - 1
            det_par_val(4) = (1 - del_eff * fd_adjust_cal) * temp_fd
            det_par_val(5) = (1 - del_eff * ft_adjust_cal) * temp_ft

            avg_240(i) = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)
            avg_mass_bias = avg_mass_bias + avg_240(i) / 20
        Next i

        det_par_val(1) = temp_eff

        'Estimate error contribution to efficiency from density about the typical density

        '  Public st_dev_rnd_mass

        Dim Random_fill_H, Density_0, Radial_offset_0
        Dim samp_pts, z
        Dim rand1 As Double

        Radial_offset_0 = geo_par(8)        ' Typical loading radial offset
        z_0 = detector_stand - cavity_H / 2         '   sample stand distance from center line
        vol_inc = container_IDiam / 20

        avg_rnd_mass = 0
        max_rad_dev = (cavity_ID - container_IDiam) / 2

        Random_fill_H = (maximum_fill_height - minimum_fill_height) / 6

        det_par_val(1) = temp_eff
        det_par_val(4) = temp_fd
        det_par_val(5) = temp_ft

        samp_pts = misc_param(6)
        If samp_pts < 1 Then samp_pts = 1
        If samp_pts > 20000 Then samp_pts = 20000

        Dim rad_avg, radial(20000), n_pts, mass_rand_min, mass_rand_max

        rad_avg = 0
        n_pts = 0
        For i = 1 To samp_pts
            rand1 = Rnd()
            Rad_0 = Radial_offset_0   '  can center
            z = z_0 + NormSInv(rand1) * Random_fill_H + average_fill_height
            radial(i) = Rad_0
            eff_rnd_v(i) = 0
            rad_avg = rad_avg + Rad_0
            For j = -10 To 10
                Radial_offset = Rad_0 + vol_inc * j
                eff_rnd_v(i) = eff_rnd_v(i) + rad_eff(T_L, R_cav, Radial_offset, z)         '  <-----------------
            Next j
            eff_rnd_v(i) = eff_rnd_v(i) / 21
            det_par_val(1) = temp_eff / eff_rnd_v(i)
            del_eff = det_par_val(1) / temp_eff - 1
            det_par_val(4) = (1 - del_eff * fd_adjust_cal) * temp_fd
            det_par_val(5) = (1 - del_eff * ft_adjust_cal) * temp_ft
            ' 
            mass_rand_v(i) = mult_anal2(new_rates, new_rates_err, new_covar, det_par_val, fiss_const_val, 5)

        Next i

        n_pts = 0
        For i = 1 To samp_pts
            If Abs(radial(i)) + container_IDiam / 2 < cavity_ID / 2 Then n_pts = n_pts + 1
            If Abs(radial(i)) + container_IDiam / 2 < cavity_ID / 2 Then avg_rnd_mass = avg_rnd_mass + mass_rand_v(i)

        Next i

        avg_rnd_mass = avg_rnd_mass / n_pts

        det_par_val(1) = temp_eff
        det_par_val(4) = temp_fd
        det_par_val(5) = temp_ft

        rad_avg = rad_avg / samp_pts

        Dim mass_comp As String
        mass_comp = "#m240 center " & m240_center & vbCrLf
        mass_comp = mass_comp & "#min mass " & min_fill_mass & vbCrLf
        mass_comp = mass_comp & "#max mass " & max_fill_mass & vbCrLf
        mass_comp = mass_comp & "#cal mass " & distrib_cal_mass & vbCrLf
        '       MsgBox(mass_comp)

        st_dev_rnd_mass = 0

        For i = 1 To samp_pts
            st_dev_rnd_mass = st_dev_rnd_mass + (mass_rand_v(i) - avg_rnd_mass) ^ 2
        Next i
        st_dev_rnd_mass = (st_dev_rnd_mass / (n_pts - 1)) ^ 0.5 / avg_rnd_mass

        pt_cal_bias = avg_fill_mass / m240_center - 1
        distrib_cal_bias = avg_fill_mass / distrib_cal_mass - 1

    End Sub

    Function rad_eff(T_L, R_cav, Radial_offset, z_offset)
        ' radial response profile
        rad_eff = 1 / ((4 * PI - 2 * (R_cav ^ 2 + 4 * Radial_offset ^ 2) * (T_L ^ 2 + 4 * z_offset ^ 2) / (T_L ^ 2 - 4 * z_offset ^ 2) ^ 2) / (4 * PI - 2 * (R_cav / T_L) ^ 2)) ^ 2
    End Function

    Function z_eff(T_L, R_cav, Radial_offset, z_offset)
        ' creates vertical axial response profile
        Dim T_L2
        T_L2 = T_L * 1.1
        z_eff = ((4 * PI - 2 * R_cav ^ 2 * (T_L2 ^ 2 + 4 * z_offset ^ 2) / (T_L2 ^ 2 - 4 * z_offset ^ 2) ^ 2) / (4 * PI - 2 * (R_cav / T_L2) ^ 2))


    End Function

    Function c_rates(i, mass, Multi, alpha0, n_eff, doub_frac, trip_frac, gate_width, nuc_dat, back_rate, assay_time)
        ' Estimates rates and uncertainties given mass and alpha based on an assumed multiplication
        Dim nu_s1, nu_s2, nu_s3, nu_s4, nu_s5
        Dim nu_i1, nu_i2, nu_i3, nu_i4, nu_i5
        Dim f_f, p_c, p_f
        Dim q4, q4_1, q4_2, q4_3, q4_r
        Dim q5, q5_1, q5_2, q5_3, q5_4, q5_r
        Dim rates(5), rates_sig(5), singles, doubles, triples

        '
        nu_s1 = nuc_dat(1)
        nu_s2 = nuc_dat(2)
        nu_s3 = nuc_dat(3)
        '  nu_s4 = nuc_dat(4)
        '  nu_s5 = nuc_dat(5)
        nu_i1 = nuc_dat(4)
        nu_i2 = nuc_dat(5)
        nu_i3 = nuc_dat(6)
        '     nu_i4 = nuc_dat(9)
        '    nu_i5 = nuc_dat(10)
        f_f = nuc_dat(7)
        p_c = 0
        p_f = 1

        rates(1) = mass * f_f * Multi * n_eff * nu_s1 * (1 + alpha0)
        rates(2) = (mass * f_f * doub_frac * n_eff ^ 2 * Multi ^ 2 / 2) * (nu_s2 + (Multi - 1) / (nu_i1 - 1 - p_c / p_f) * nu_s1 * (1 + alpha0) * nu_i2)
        rates(3) = (mass * f_f * n_eff ^ 3 * trip_frac * Multi ^ 3 / 3) * (nu_s3 + (Multi - 1) / (nu_i1 - 1 - p_c / p_f) * (3 * nu_s2 * nu_i2 + nu_s1 * (1 + alpha0) * nu_i3) + 3 * (Multi - 1) ^ 2 / (nu_i1 - 1) ^ 2 * nu_s1 * (alpha0 + 1) * nu_i2 ^ 2) / 2

        singles = rates(1)
        doubles = rates(2)
        triples = rates(3)

        Dim gamma, epsilon
        gamma = 1 - (1 - Exp(-gate_width / die_away_t) / (gate_width / die_away_t))
        epsilon = (1 + doubles / doub_frac / singles) ^ 0.5



        rates_sig(1) = Sqrt((singles + back_rate(1)) / assay_time) * epsilon
        rates_sig(2) = Sqrt((2 * singles ^ 2 * gate_width * 0.000001 + doubles + back_rate(2)) / assay_time) * (1 + 8 * gamma * doubles / doub_frac / singles) ^ 0.5
        rates_sig(3) = Sqrt((singles ^ 3 * (gate_width * 0.000001) ^ 2 + triples + back_rate(3)) / assay_time) * 1.4
        '  rates_sig(3) = Sqrt((3 * singles ^ 3 * (gate_width * 0.000001) ^ 2 + triples + back_rate(3)) / assay_time) * epsilon * (1 + 8 * gamma * doubles / doub_frac / singles) ^ 0.5
        '
        '      rates_sig(3) = Sqrt((2 * singles ^ 3 * (gate_width * 0.000001) ^ 2 + triples) / assay_time) * Sqrt(2 + 0 * gamma * doubles / singles / f_d)


        If i = 1 Then c_rates = rates(1)
        If i = 2 Then c_rates = rates(2)
        If i = 3 Then c_rates = rates(3)

        If i = 11 Then c_rates = rates_sig(1)
        If i = 12 Then c_rates = rates_sig(2)
        If i = 13 Then c_rates = rates_sig(3)

    End Function

    Function multiplication_corr_fact(mult_2, mult_2_err, i_out)
        Dim CF(2), err_temp

        CF(1) = 1 + mult_corr(1) * (mult_2 - 1) + mult_corr(2) * (mult_2 - 1) ^ 2
        err_temp = (mult_corr(4) * (mult_2 - 1)) ^ 2 + (mult_corr(5) * (mult_2 - 1) ^ 2) ^ 2
        err_temp = err_temp + 2 * mult_corr(6) * (mult_2 - 1) ^ 3
        err_temp = err_temp + (mult_corr(1) + 2 * mult_corr(2) * (mult_2 - 1)) ^ 2 * mult_2_err ^ 2
        If err_temp > 0 Then CF(2) = err_temp ^ 0.5 Else CF(2) = 0
        multiplication_corr_fact = CF(i_out)

    End Function

End Module
