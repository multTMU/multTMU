
Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Imports Microsoft.VisualBasic.PowerPacks.Printing


Public Class DetectorUncertainty

    '        Public det_par_val(10)
    '   Public det_par_err(10)
    '  Public det_par_covar(10, 10)
    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim det_par_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file

        det_par_file_name = "c:\multiplicity_tmu\detector_parameters\det_parameters.csv"

        Call get_det_par_file(det_par_file_name)
        load_det_file_contents()


        For i = 1 To 10
            If dt_par_d <> 0 Then  det_comp(i) = mult_anal_err2(new_num_A_R, new_num_Acc, new_max_bin_A_R, new_max_bin_Acc, det_par_val, det_par_err, fiss_const_val, fiss_const_err, grunt_time(2), 3, i, 1)

            If dt_par_d = 0 Then det_comp(i) = mult_anal_err2_LANL(new_num_A_R, new_num_Acc, new_max_bin_A_R, new_max_bin_Acc, det_par_val, det_par_err, fiss_const_val, fiss_const_err, grunt_time(2), 3, i, 1)
        Next i

        det_err_sum = 0
        For i = 1 To 10
            det_err_sum = det_err_sum + (det_comp(i)) ^ 2
            'remember covariance must be done before applying Abs
            det_err_comp(i) = Abs(det_comp(i))
        Next i
        det_err_sum = det_err_sum ^ 0.5

        MaskedTextBox1.Text = Int(1000 * det_err_comp(1)) / 1000
        MaskedTextBox2.Text = Int(1000 * det_err_comp(2)) / 1000
        MaskedTextBox3.Text = Int(1000 * det_err_comp(3)) / 1000
        MaskedTextBox32.Text = Int(1000 * det_err_comp(4)) / 1000
        MaskedTextBox5.Text = Int(1000 * det_err_comp(5)) / 1000
        MaskedTextBox6.Text = Int(1000 * det_err_comp(6)) / 1000
        MaskedTextBox7.Text = Int(1000 * det_err_comp(7)) / 1000
        MaskedTextBox8.Text = Int(1000 * det_err_comp(8)) / 1000
        MaskedTextBox9.Text = Int(1000 * det_err_comp(9)) / 1000
        MaskedTextBox10.Text = Int(1000 * det_err_comp(10)) / 1000

        MaskedTextBox11.Text = Int(1000 * det_err_comp(1) / pu_240_effective) / 1000
        MaskedTextBox12.Text = Int(1000 * det_err_comp(2) / pu_240_effective) / 1000
        MaskedTextBox13.Text = Int(1000 * det_err_comp(3) / pu_240_effective) / 1000
        MaskedTextBox14.Text = Int(1000 * det_err_comp(4) / pu_240_effective) / 1000
        MaskedTextBox15.Text = Int(1000 * det_err_comp(5) / pu_240_effective) / 1000
        MaskedTextBox16.Text = Int(1000 * det_err_comp(6) / pu_240_effective) / 1000
        MaskedTextBox17.Text = Int(1000 * det_err_comp(7) / pu_240_effective) / 1000
        MaskedTextBox18.Text = Int(1000 * det_err_comp(8) / pu_240_effective) / 1000
        MaskedTextBox19.Text = Int(1000 * det_err_comp(9) / pu_240_effective) / 1000
        MaskedTextBox20.Text = Int(1000 * det_err_comp(10) / pu_240_effective) / 1000

        MaskedTextBox21.Text = Int(10000 * det_err_comp(1) / m240_new) / 100 & "%"
        MaskedTextBox22.Text = Int(10000 * det_err_comp(2) / m240_new) / 100 & "%"
        MaskedTextBox23.Text = Int(10000 * det_err_comp(3) / m240_new) / 100 & "%"
        MaskedTextBox24.Text = Int(10000 * det_err_comp(4) / m240_new) / 100 & "%"
        MaskedTextBox25.Text = Int(10000 * det_err_comp(5) / m240_new) / 100 & "%"
        MaskedTextBox26.Text = Int(10000 * det_err_comp(6) / m240_new) / 100 & "%"
        MaskedTextBox27.Text = Int(10000 * det_err_comp(7) / m240_new) / 100 & "%"
        MaskedTextBox28.Text = Int(10000 * det_err_comp(8) / m240_new) / 100 & "%"
        MaskedTextBox29.Text = Int(10000 * det_err_comp(9) / m240_new) / 100 & "%"
        MaskedTextBox30.Text = Int(10000 * det_err_comp(10) / m240_new) / 100 & "%"

        MaskedTextBox51.Text = Int(1000 * det_err_sum) / 1000
        MaskedTextBox52.Text = Int(1000 * det_err_sum / pu_240_effective) / 1000
        MaskedTextBox53.Text = Int(10000 * det_err_sum / m240_new) / 100 & "%"

    End Sub





    Sub load_det_file_contents()

        ' ***** Load data file contents onto screen *****
        ' detparfilenamebox1.Text = det_par_file_name

        detparmBox1.Text = det_par_val(1)
        detparmBox2.Text = det_par_val(2)
        detparmBox3.Text = det_par_val(3)
        detparmBox4.Text = det_par_val(4)
        detparmBox5.Text = det_par_val(5)
        detparmBox6.Text = det_par_val(6)
        detparmBox7.Text = det_par_val(7)
        detparmBox8.Text = det_par_val(8)
        detparmBox9.Text = det_par_val(9)
        detparmBox10.Text = det_par_val(10)

        DetErrTextBox1.Text = det_par_err(1)
        DetErrTextBox2.Text = det_par_err(2)
        DetErrTextBox3.Text = det_par_err(3)
        DetErrTextBox4.Text = det_par_err(4)
        DetErrTextBox5.Text = det_par_err(5)
        DetErrTextBox6.Text = det_par_err(6)
        DetErrTextBox7.Text = det_par_err(7)
        DetErrTextBox8.Text = det_par_err(8)
        DetErrTextBox9.Text = det_par_err(9)
        DetErrTextBox10.Text = det_par_err(10)


        DetCovarBox11.Text = det_par_covar(1, 1)
        DetCovarBox12.Text = det_par_covar(1, 2)
        DetCovarBox13.Text = det_par_covar(1, 3)
        DetCovarBox14.Text = det_par_covar(1, 4)
        DetCovarBox15.Text = det_par_covar(1, 5)
        DetCovarBox16.Text = det_par_covar(1, 6)
        DetCovarBox17.Text = det_par_covar(1, 7)
        DetCovarBox18.Text = det_par_covar(1, 8)
        DetCovarBox19.Text = det_par_covar(1, 9)
        DetCovarBox1a.Text = det_par_covar(1, 10)

        DetCovarBox21.Text = det_par_covar(2, 1)
        DetCovarBox22.Text = det_par_covar(2, 2)
        DetCovarBox23.Text = det_par_covar(2, 3)
        DetCovarBox24.Text = det_par_covar(2, 4)
        DetCovarBox25.Text = det_par_covar(2, 5)
        DetCovarBox26.Text = det_par_covar(2, 6)
        DetCovarBox27.Text = det_par_covar(2, 7)
        DetCovarBox28.Text = det_par_covar(2, 8)
        DetCovarBox29.Text = det_par_covar(2, 9)
        DetCovarBox2a.Text = det_par_covar(2, 10)

        DetCovarBox31.Text = det_par_covar(3, 1)
        DetCovarBox32.Text = det_par_covar(3, 2)
        DetCovarBox33.Text = det_par_covar(3, 3)
        DetCovarBox34.Text = det_par_covar(3, 4)
        DetCovarBox35.Text = det_par_covar(3, 5)
        DetCovarBox36.Text = det_par_covar(3, 6)
        DetCovarBox37.Text = det_par_covar(3, 7)
        DetCovarBox38.Text = det_par_covar(3, 8)
        DetCovarBox39.Text = det_par_covar(3, 9)
        DetCovarBox3a.Text = det_par_covar(3, 10)

        DetCovarBox41.Text = det_par_covar(4, 1)
        DetCovarBox42.Text = det_par_covar(4, 2)
        DetCovarBox43.Text = det_par_covar(4, 3)
        DetCovarBox44.Text = det_par_covar(4, 4)
        DetCovarBox45.Text = det_par_covar(4, 5)
        DetCovarBox46.Text = det_par_covar(4, 6)
        DetCovarBox47.Text = det_par_covar(4, 7)
        DetCovarBox48.Text = det_par_covar(4, 8)
        DetCovarBox49.Text = det_par_covar(4, 9)
        DetCovarBox4a.Text = det_par_covar(4, 10)

        DetCovarBox51.Text = det_par_covar(5, 1)
        DetCovarBox52.Text = det_par_covar(5, 2)
        DetCovarBox53.Text = det_par_covar(5, 3)
        DetCovarBox54.Text = det_par_covar(5, 4)
        DetCovarBox55.Text = det_par_covar(5, 5)
        DetCovarBox56.Text = det_par_covar(5, 6)
        DetCovarBox57.Text = det_par_covar(5, 7)
        DetCovarBox58.Text = det_par_covar(5, 8)
        DetCovarBox59.Text = det_par_covar(5, 9)
        DetCovarBox5a.Text = det_par_covar(5, 10)

        DetCovarBox61.Text = det_par_covar(6, 1)
        DetCovarBox62.Text = det_par_covar(6, 2)
        DetCovarBox63.Text = det_par_covar(6, 3)
        DetCovarBox64.Text = det_par_covar(6, 4)
        DetCovarBox65.Text = det_par_covar(6, 5)
        DetCovarBox66.Text = det_par_covar(6, 6)
        DetCovarBox67.Text = det_par_covar(6, 7)
        DetCovarBox68.Text = det_par_covar(6, 8)
        DetCovarBox69.Text = det_par_covar(6, 9)
        DetCovarBox6a.Text = det_par_covar(6, 10)

        DetCovarBox71.Text = det_par_covar(7, 1)
        DetCovarBox72.Text = det_par_covar(7, 2)
        DetCovarBox73.Text = det_par_covar(7, 3)
        DetCovarBox74.Text = det_par_covar(7, 4)
        DetCovarBox75.Text = det_par_covar(7, 5)
        DetCovarBox76.Text = det_par_covar(7, 6)
        DetCovarBox77.Text = det_par_covar(7, 7)
        DetCovarBox78.Text = det_par_covar(7, 8)
        DetCovarBox79.Text = det_par_covar(7, 9)
        DetCovarBox7a.Text = det_par_covar(7, 10)

        DetCovarBox81.Text = det_par_covar(8, 1)
        DetCovarBox82.Text = det_par_covar(8, 2)
        DetCovarBox83.Text = det_par_covar(8, 3)
        DetCovarBox84.Text = det_par_covar(8, 4)
        DetCovarBox85.Text = det_par_covar(8, 5)
        DetCovarBox86.Text = det_par_covar(8, 6)
        DetCovarBox87.Text = det_par_covar(8, 7)
        DetCovarBox88.Text = det_par_covar(8, 8)
        DetCovarBox89.Text = det_par_covar(8, 9)
        DetCovarBox8a.Text = det_par_covar(8, 10)

        DetCovarBox91.Text = det_par_covar(9, 1)
        DetCovarBox92.Text = det_par_covar(9, 2)
        DetCovarBox93.Text = det_par_covar(9, 3)
        DetCovarBox94.Text = det_par_covar(9, 4)
        DetCovarBox95.Text = det_par_covar(9, 5)
        DetCovarBox96.Text = det_par_covar(9, 6)
        DetCovarBox97.Text = det_par_covar(9, 7)
        DetCovarBox98.Text = det_par_covar(9, 8)
        DetCovarBox99.Text = det_par_covar(9, 9)
        DetCovarBox9a.Text = det_par_covar(9, 10)

        DetCovarBoxa1.Text = det_par_covar(10, 1)
        DetCovarBoxa2.Text = det_par_covar(10, 2)
        DetCovarBoxa3.Text = det_par_covar(10, 3)
        DetCovarBoxa4.Text = det_par_covar(10, 4)
        DetCovarBoxa5.Text = det_par_covar(10, 5)
        DetCovarBoxa6.Text = det_par_covar(10, 6)
        DetCovarBoxa7.Text = det_par_covar(10, 7)
        DetCovarBoxa8.Text = det_par_covar(10, 8)
        DetCovarBoxa9.Text = det_par_covar(10, 9)
        DetCovarBoxaa.Text = det_par_covar(10, 10)




    End Sub

    Private Sub MaskedTextBox10_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles MaskedTextBox10.MaskInputRejected

    End Sub

    Private Sub MaskedTextBox20_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles MaskedTextBox20.MaskInputRejected

    End Sub

    Private Sub MaskedTextBox30_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles MaskedTextBox30.MaskInputRejected

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pf As New PrintForm
        Size = New Size(1200, 920)
        AutoScrollPosition = New Point(1, 1)
        pf.Form = Me
        pf.PrinterSettings.DefaultPageSettings.Landscape = False
        pf.PrinterSettings.Copies = 1
        pf.PrinterSettings.DefaultPageSettings.Margins = New Printing.Margins(60, 30, 90, 30)
        pf.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = Printing.PaperKind.Letter
        pf.PrintAction = Printing.PrintAction.PrintToPreview
        'pf.Print()
        pf.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)
    End Sub
End Class