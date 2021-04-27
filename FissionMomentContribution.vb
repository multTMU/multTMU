Imports System.IO
Imports System.Text
Imports System.Math

Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Public Class FissionMomentContribution
    Private Sub Form13_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' ***** Load data file contents onto screen *****

        FissionTextBox1.Text = fiss_const_val(1)
        FissiontextBox2.Text = fiss_const_val(2)
        FissionTextBox3.Text = fiss_const_val(3)
        FissionTextBox4.Text = fiss_const_val(4)
        FissionTextBox5.Text = fiss_const_val(5)
        FissionTextBox6.Text = fiss_const_val(6)
        FissionTextBox7.Text = fiss_const_val(7)

        FissErrTextBox1.Text = fiss_const_err(1)
        FissErrTextBox2.Text = fiss_const_err(2)
        FissErrTextBox3.Text = fiss_const_err(3)
        FissErrTextBox4.Text = fiss_const_err(4)
        FissErrTextBox5.Text = fiss_const_err(5)
        FissErrTextBox6.Text = fiss_const_err(6)
        FissErrTextBox7.Text = fiss_const_err(7)

        FissCovarBox11.Text = fiss_covar(1, 1)
        FissCovarBox12.Text = fiss_covar(1, 2)
        FissCovarBox13.Text = fiss_covar(1, 3)
        FissCovarBox14.Text = fiss_covar(1, 4)
        FissCovarBox15.Text = fiss_covar(1, 5)
        FissCovarBox16.Text = fiss_covar(1, 6)
        FissCovarBox17.Text = fiss_covar(1, 7)

        FissCovarBox21.Text = fiss_covar(2, 1)
        FissCovarBox22.Text = fiss_covar(2, 2)
        FissCovarBox23.Text = fiss_covar(2, 3)
        FissCovarBox24.Text = fiss_covar(2, 4)
        FissCovarBox25.Text = fiss_covar(2, 5)
        FissCovarBox26.Text = fiss_covar(2, 6)
        FissCovarBox27.Text = fiss_covar(2, 7)

        FissCovarBox31.Text = fiss_covar(3, 1)
        FissCovarBox32.Text = fiss_covar(3, 2)
        FissCovarBox33.Text = fiss_covar(3, 3)
        FissCovarBox34.Text = fiss_covar(3, 4)
        FissCovarBox35.Text = fiss_covar(3, 5)
        FissCovarBox36.Text = fiss_covar(3, 6)
        FissCovarBox37.Text = fiss_covar(3, 7)

        FissCovarBox41.Text = fiss_covar(4, 1)
        FissCovarBox42.Text = fiss_covar(4, 2)
        FissCovarBox43.Text = fiss_covar(4, 3)
        FissCovarBox44.Text = fiss_covar(4, 4)
        FissCovarBox45.Text = fiss_covar(4, 5)
        FissCovarBox46.Text = fiss_covar(4, 6)
        FissCovarBox47.Text = fiss_covar(4, 7)

        FissCovarBox51.Text = fiss_covar(5, 1)
        FissCovarBox52.Text = fiss_covar(5, 2)
        FissCovarBox53.Text = fiss_covar(5, 3)
        FissCovarBox54.Text = fiss_covar(5, 4)
        FissCovarBox55.Text = fiss_covar(5, 5)
        FissCovarBox56.Text = fiss_covar(5, 6)
        FissCovarBox57.Text = fiss_covar(5, 7)

        FissCovarBox61.Text = fiss_covar(6, 1)
        FissCovarBox62.Text = fiss_covar(6, 2)
        FissCovarBox63.Text = fiss_covar(6, 3)
        FissCovarBox64.Text = fiss_covar(6, 4)
        FissCovarBox65.Text = fiss_covar(6, 5)
        FissCovarBox66.Text = fiss_covar(6, 6)
        FissCovarBox67.Text = fiss_covar(6, 7)

        FissCovarBox71.Text = fiss_covar(7, 1)
        FissCovarBox72.Text = fiss_covar(7, 2)
        FissCovarBox73.Text = fiss_covar(7, 3)
        FissCovarBox74.Text = fiss_covar(7, 4)
        FissCovarBox75.Text = fiss_covar(7, 5)
        FissCovarBox76.Text = fiss_covar(7, 6)
        FissCovarBox77.Text = fiss_covar(7, 7)

        '
        '  *************  Calculate Fission Moment Contributions to Error     ***********************
        '
        For i = 1 To 7
            fiss_mom_comp(i) = mult_anal_err2(new_num_A_R, new_num_Acc, new_max_bin_A_R, new_max_bin_Acc, det_par_val, det_par_err, fiss_const_val, fiss_const_err, grunt_time(2), 3, i, 2)
        Next i


        MaskedTextBox1.Text = Int(1000 * fiss_mom_comp(1)) / 1000
        MaskedTextBox2.Text = Int(1000 * fiss_mom_comp(2)) / 1000
        MaskedTextBox3.Text = Int(1000 * fiss_mom_comp(3)) / 1000
        MaskedTextBox4.Text = Int(1000 * fiss_mom_comp(4)) / 1000
        MaskedTextBox5.Text = Int(1000 * fiss_mom_comp(5)) / 1000
        MaskedTextBox6.Text = Int(1000 * fiss_mom_comp(6)) / 1000
        MaskedTextBox7.Text = Int(1000 * fiss_mom_comp(7)) / 1000

        MaskedTextBox8.Text = Int(1000 * fiss_mom_comp(1) / pu_240_effective) / 1000
        MaskedTextBox9.Text = Int(1000 * fiss_mom_comp(2) / pu_240_effective) / 1000
        MaskedTextBox10.Text = Int(1000 * fiss_mom_comp(3) / pu_240_effective) / 1000
        MaskedTextBox11.Text = Int(1000 * fiss_mom_comp(4) / pu_240_effective) / 1000
        MaskedTextBox12.Text = Int(1000 * fiss_mom_comp(5) / pu_240_effective) / 1000
        MaskedTextBox13.Text = Int(1000 * fiss_mom_comp(6) / pu_240_effective) / 1000
        MaskedTextBox14.Text = Int(1000 * fiss_mom_comp(7) / pu_240_effective) / 1000

        MaskedTextBox15.Text = Int(10000 * fiss_mom_comp(1) / m240_new) / 100 & "%"
        MaskedTextBox16.Text = Int(10000 * fiss_mom_comp(2) / m240_new) / 100 & "%"
        MaskedTextBox17.Text = Int(10000 * fiss_mom_comp(3) / m240_new) / 100 & "%"
        MaskedTextBox18.Text = Int(10000 * fiss_mom_comp(4) / m240_new) / 100 & "%"
        MaskedTextBox19.Text = Int(10000 * fiss_mom_comp(5) / m240_new) / 100 & "%"
        MaskedTextBox20.Text = Int(10000 * fiss_mom_comp(6) / m240_new) / 100 & "%"
        MaskedTextBox21.Text = Int(10000 * fiss_mom_comp(7) / m240_new) / 100 & "%"




        Dim covar_fiss, j
        covar_fiss = 0
        For i = 1 To 7


            For j = 1 To 7
                If (fiss_covar(i, i) * fiss_covar(j, j)) <> 0 Then covar_fiss = covar_fiss + (fiss_mom_comp(i) * fiss_mom_comp(j)) * fiss_covar(i, j) / (fiss_covar(i, i) * fiss_covar(j, j)) ^ 0.5
            Next j

        Next i

        covar_fiss = covar_fiss ^ 0.5

        MaskedTextBox22.Text = Int(1000 * covar_fiss) / 1000
        MaskedTextBox23.Text = Int(1000 * covar_fiss / pu_240_effective) / 1000
        MaskedTextBox24.Text = Int(100000 * covar_fiss / m240_new) / 1000 & "%"

    End Sub
End Class