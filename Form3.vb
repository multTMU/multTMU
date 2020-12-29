Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Public Class Form3

    Dim myForm16 As Form16      ' dual energy multiplicity parameter entry screen

    '    Public pt_cal_flag As Boolean
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim det_par_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file
        pt_cal_flag = True

        det_par_file_name = "c:\multiplicity_tmu\detector_parameters\det_parameters.csv"

        Call get_det_par_file(det_par_file_name)
        Call load_det_pars_to_screen(det_par_file_name)

    End Sub

    Public Property FileName As String
    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim det_par_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\multiplicity_tmu\detector_parameters\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    det_par_file_name = openFileDialog1.FileName

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

        Call get_det_par_file(det_par_file_name)
        Call load_det_pars_to_screen(det_par_file_name)

    End Sub



    Sub load_det_pars_to_screen(det_par_file_name)

        ' ***** Load data file contents onto screen *****
        detparfilenamebox1.Text = det_par_file_name

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
        cal_typical_FH_Box.Text = det_cal_FH
        Cal_UPu_Box1.Text = det_cal_UPu_ratio
        Cal_Pu240eff_Box.Text = det_cal_pu240_eff
        Cal_Alpha_Box.Text = det_cal_alpha
        Cal_mod_box.Text = det_cal_mod
        cal_standard_density_box.Text = det_cal_ref_density
        Cal_wall_Box1.Text = cal_wall_thickness
        cal_mat_box1.Text = cal_wall_material

        mult_corr_box1.Text = mult_corr(1)
        mult_corr_box2.Text = mult_corr(2)
        mult_corr_err_box1.Text = mult_corr(4)
        mult_corr_err_box2.Text = mult_corr(5)
        mult_corr_covar_box.Text = mult_corr(6)

        If pt_cal_flag = True Then CheckBox1.Checked = True
        If pt_cal_flag = False Then CheckBox1.Checked = False
        If pt_cal_flag = True Then CheckBox2.Checked = False
        If pt_cal_flag = False Then CheckBox2.Checked = True
        If mult_corr_flag = True Then Mult_corr_box3.Checked = True

    End Sub



    Public Property Det_par_val1() As String
        Get
            Return Me.detparmBox1.Text
        End Get
        Set(ByVal Value As String)
            Me.detparmBox1.Text = Value

        End Set
    End Property

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim outconstants As String
        Dim klineout As String

        '   save revised parameters to default fission constant file

        det_par_val(1) = detparmBox1.Text
        det_par_val(2) = detparmBox2.Text
        det_par_val(3) = detparmBox3.Text
        det_par_val(4) = detparmBox4.Text
        det_par_val(5) = detparmBox5.Text
        det_par_val(6) = detparmBox6.Text
        det_par_val(7) = detparmBox7.Text
        det_par_val(8) = detparmBox8.Text
        det_par_val(9) = detparmBox9.Text
        det_par_val(10) = detparmBox10.Text

        det_par_err(1) = DetErrTextBox1.Text
        det_par_err(2) = DetErrTextBox2.Text
        det_par_err(3) = DetErrTextBox3.Text
        det_par_err(4) = DetErrTextBox4.Text
        det_par_err(5) = DetErrTextBox5.Text
        det_par_err(6) = DetErrTextBox6.Text
        det_par_err(7) = DetErrTextBox7.Text
        det_par_err(8) = DetErrTextBox8.Text
        det_par_err(9) = DetErrTextBox9.Text
        det_par_err(10) = DetErrTextBox10.Text

        det_par_covar(1, 1) = DetCovarBox11.Text
        det_par_covar(1, 2) = DetCovarBox12.Text
        det_par_covar(1, 3) = DetCovarBox13.Text
        det_par_covar(1, 4) = DetCovarBox14.Text
        det_par_covar(1, 5) = DetCovarBox15.Text
        det_par_covar(1, 6) = DetCovarBox16.Text
        det_par_covar(1, 7) = DetCovarBox17.Text
        det_par_covar(1, 8) = DetCovarBox18.Text
        det_par_covar(1, 9) = DetCovarBox19.Text
        det_par_covar(1, 10) = DetCovarBox1a.Text

        det_par_covar(2, 1) = DetCovarBox21.Text
        det_par_covar(2, 2) = DetCovarBox22.Text
        det_par_covar(2, 3) = DetCovarBox23.Text
        det_par_covar(2, 4) = DetCovarBox24.Text
        det_par_covar(2, 5) = DetCovarBox25.Text
        det_par_covar(2, 6) = DetCovarBox26.Text
        det_par_covar(2, 7) = DetCovarBox27.Text
        det_par_covar(2, 8) = DetCovarBox28.Text
        det_par_covar(2, 9) = DetCovarBox29.Text
        det_par_covar(2, 10) = DetCovarBox2a.Text

        det_par_covar(3, 1) = DetCovarBox31.Text
        det_par_covar(3, 2) = DetCovarBox32.Text
        det_par_covar(3, 3) = DetCovarBox33.Text
        det_par_covar(3, 4) = DetCovarBox34.Text
        det_par_covar(3, 5) = DetCovarBox35.Text
        det_par_covar(3, 6) = DetCovarBox36.Text
        det_par_covar(3, 7) = DetCovarBox37.Text
        det_par_covar(3, 8) = DetCovarBox38.Text
        det_par_covar(3, 9) = DetCovarBox39.Text
        det_par_covar(3, 10) = DetCovarBox3a.Text

        det_par_covar(4, 1) = DetCovarBox41.Text
        det_par_covar(4, 2) = DetCovarBox42.Text
        det_par_covar(4, 3) = DetCovarBox43.Text
        det_par_covar(4, 4) = DetCovarBox44.Text
        det_par_covar(4, 5) = DetCovarBox45.Text
        det_par_covar(4, 6) = DetCovarBox46.Text
        det_par_covar(4, 7) = DetCovarBox47.Text
        det_par_covar(4, 8) = DetCovarBox48.Text
        det_par_covar(4, 9) = DetCovarBox49.Text
        det_par_covar(4, 10) = DetCovarBox4a.Text

        det_par_covar(5, 1) = DetCovarBox51.Text
        det_par_covar(5, 2) = DetCovarBox52.Text
        det_par_covar(5, 3) = DetCovarBox53.Text
        det_par_covar(5, 4) = DetCovarBox54.Text
        det_par_covar(5, 5) = DetCovarBox55.Text
        det_par_covar(5, 6) = DetCovarBox56.Text
        det_par_covar(5, 7) = DetCovarBox57.Text
        det_par_covar(5, 8) = DetCovarBox58.Text
        det_par_covar(5, 9) = DetCovarBox59.Text
        det_par_covar(5, 10) = DetCovarBox5a.Text

        det_par_covar(6, 1) = DetCovarBox61.Text
        det_par_covar(6, 2) = DetCovarBox62.Text
        det_par_covar(6, 3) = DetCovarBox63.Text
        det_par_covar(6, 4) = DetCovarBox64.Text
        det_par_covar(6, 5) = DetCovarBox65.Text
        det_par_covar(6, 6) = DetCovarBox66.Text
        det_par_covar(6, 7) = DetCovarBox67.Text
        det_par_covar(6, 8) = DetCovarBox68.Text
        det_par_covar(6, 9) = DetCovarBox69.Text
        det_par_covar(6, 10) = DetCovarBox6a.Text

        det_par_covar(7, 1) = DetCovarBox71.Text
        det_par_covar(7, 2) = DetCovarBox72.Text
        det_par_covar(7, 3) = DetCovarBox73.Text
        det_par_covar(7, 4) = DetCovarBox74.Text
        det_par_covar(7, 5) = DetCovarBox75.Text
        det_par_covar(7, 6) = DetCovarBox76.Text
        det_par_covar(7, 7) = DetCovarBox77.Text
        det_par_covar(7, 8) = DetCovarBox78.Text
        det_par_covar(7, 9) = DetCovarBox79.Text
        det_par_covar(7, 10) = DetCovarBox7a.Text

        det_par_covar(8, 1) = DetCovarBox81.Text
        det_par_covar(8, 2) = DetCovarBox82.Text
        det_par_covar(8, 3) = DetCovarBox83.Text
        det_par_covar(8, 4) = DetCovarBox84.Text
        det_par_covar(8, 5) = DetCovarBox85.Text
        det_par_covar(8, 6) = DetCovarBox86.Text
        det_par_covar(8, 7) = DetCovarBox87.Text
        det_par_covar(8, 8) = DetCovarBox88.Text
        det_par_covar(8, 9) = DetCovarBox89.Text
        det_par_covar(8, 10) = DetCovarBox8a.Text

        det_par_covar(9, 1) = DetCovarBox91.Text
        det_par_covar(9, 2) = DetCovarBox92.Text
        det_par_covar(9, 3) = DetCovarBox93.Text
        det_par_covar(9, 4) = DetCovarBox94.Text
        det_par_covar(9, 5) = DetCovarBox95.Text
        det_par_covar(9, 6) = DetCovarBox96.Text
        det_par_covar(9, 7) = DetCovarBox97.Text
        det_par_covar(9, 8) = DetCovarBox98.Text
        det_par_covar(9, 9) = DetCovarBox99.Text
        det_par_covar(9, 10) = DetCovarBox9a.Text

        det_par_covar(10, 1) = DetCovarBoxa1.Text
        det_par_covar(10, 2) = DetCovarBoxa2.Text
        det_par_covar(10, 3) = DetCovarBoxa3.Text
        det_par_covar(10, 4) = DetCovarBoxa4.Text
        det_par_covar(10, 5) = DetCovarBoxa5.Text
        det_par_covar(10, 6) = DetCovarBoxa6.Text
        det_par_covar(10, 7) = DetCovarBoxa7.Text
        det_par_covar(10, 8) = DetCovarBoxa8.Text
        det_par_covar(10, 9) = DetCovarBoxa9.Text
        det_par_covar(10, 10) = DetCovarBoxaa.Text

        If CheckBox1.Checked = True Then pt_cal_flag = True
        If CheckBox2.Checked = True Then pt_cal_flag = False


        det_cal_FH = cal_typical_FH_Box.Text
        det_cal_UPu_ratio = Cal_UPu_Box1.Text
        det_cal_pu240_eff = Cal_Pu240eff_Box.Text
        det_cal_alpha = Cal_Alpha_Box.Text
        det_cal_mod = Cal_mod_box.Text
        det_cal_ref_density = cal_standard_density_box.Text

        cal_wall_thickness = Cal_wall_Box1.Text
        cal_wall_material = cal_mat_box1.Text

        mult_corr(1) = mult_corr_box1.Text
        mult_corr(2) = mult_corr_box2.Text
        mult_corr(4) = mult_corr_err_box1.Text
        mult_corr(5) = mult_corr_err_box2.Text
        mult_corr(6) = mult_corr_covar_box.Text

        If Mult_corr_box3.Checked = True Then mult_corr_flag = True Else mult_corr_flag = False
        If mult_corr_flag Then mult_corr(3) = "mult_corr_on" Else mult_corr(3) = "mult_corr_off"
        For i = 7 To 10
            mult_corr(i) = 0
        Next i

        'overwrite contstants file for fitting routine

        outconstants = "c:\multiplicity_tmu\detector_parameters\det_parameters.csv"

        klineout = "Detector Parameters , " & outconstants & " , " & vbCrLf

        Dim fs As FileStream = File.Create(outconstants)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()
        '  write detector parameters to file
        For i = 2 To 11
            klineout = det_par_val(i - 1) & ", " & det_par_err(i - 1) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            '    Console.Write(klineout)
        Next i
        '  write detector parameter cavariance array to file
        For i = 12 To 21
            klineout = det_par_covar(i - 11, 1)
            For j = 2 To 10
                klineout = klineout & ", " & det_par_covar(i - 11, j)
            Next j
            klineout = klineout & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            '    Console.Write(klineout)
        Next i

        '  write calibration information to file
        If pt_cal_flag Then klineout = "Pt_source_cal" & vbCrLf Else klineout = "distributed_cal" & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (22)

        klineout = det_cal_FH & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (23)
        klineout = det_cal_UPu_ratio & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (24)
        klineout = det_cal_pu240_eff & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (25)
        klineout = det_cal_alpha & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (26)
        klineout = det_cal_mod & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (27)
        klineout = det_cal_ref_density & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (28)
        klineout = inner_ring_eff_240 & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (29)
        klineout = outer_ring_eff_240 & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (30)
        klineout = cal_wall_thickness & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (31)
        klineout = cal_wall_material & vbCrLf
        My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (32)

        '  write multiplicatoin correction parameters to file
        For i = 1 To 10
            klineout = mult_corr(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
        Next i

        For i = 1 To 100
            klineout = i & ", " & neut_energy(i) & ", " & det_eff_E(i) & ", " & inner_outer(i) & ", " & rel_fiss_prob(i) & ", " & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
        Next i

        Close()

    End Sub

    Private Sub Label36_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then CheckBox2.Checked = False
        If CheckBox1.Checked = False Then CheckBox2.Checked = True

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then CheckBox1.Checked = False
        If CheckBox2.Checked = False Then CheckBox1.Checked = True
    End Sub

    Private Sub DetCovarBoxa7_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles DetCovarBoxa7.MaskInputRejected

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim outconstants As String
        Dim klineout As String

        '   save revised parameters to default fission constant file

        det_par_val(1) = detparmBox1.Text
        det_par_val(2) = detparmBox2.Text
        det_par_val(3) = detparmBox3.Text
        det_par_val(4) = detparmBox4.Text
        det_par_val(5) = detparmBox5.Text
        det_par_val(6) = detparmBox6.Text
        det_par_val(7) = detparmBox7.Text
        det_par_val(8) = detparmBox8.Text
        det_par_val(9) = detparmBox9.Text
        det_par_val(10) = detparmBox10.Text

        det_par_err(1) = DetErrTextBox1.Text
        det_par_err(2) = DetErrTextBox2.Text
        det_par_err(3) = DetErrTextBox3.Text
        det_par_err(4) = DetErrTextBox4.Text
        det_par_err(5) = DetErrTextBox5.Text
        det_par_err(6) = DetErrTextBox6.Text
        det_par_err(7) = DetErrTextBox7.Text
        det_par_err(8) = DetErrTextBox8.Text
        det_par_err(9) = DetErrTextBox9.Text
        det_par_err(10) = DetErrTextBox10.Text



        det_par_covar(1, 1) = DetCovarBox11.Text
        det_par_covar(1, 2) = DetCovarBox12.Text
        det_par_covar(1, 3) = DetCovarBox13.Text
        det_par_covar(1, 4) = DetCovarBox14.Text
        det_par_covar(1, 5) = DetCovarBox15.Text
        det_par_covar(1, 6) = DetCovarBox16.Text
        det_par_covar(1, 7) = DetCovarBox17.Text
        det_par_covar(1, 8) = DetCovarBox18.Text
        det_par_covar(1, 9) = DetCovarBox19.Text
        det_par_covar(1, 10) = DetCovarBox1a.Text

        det_par_covar(2, 1) = DetCovarBox21.Text
        det_par_covar(2, 2) = DetCovarBox22.Text
        det_par_covar(2, 3) = DetCovarBox23.Text
        det_par_covar(2, 4) = DetCovarBox24.Text
        det_par_covar(2, 5) = DetCovarBox25.Text
        det_par_covar(2, 6) = DetCovarBox26.Text
        det_par_covar(2, 7) = DetCovarBox27.Text
        det_par_covar(2, 8) = DetCovarBox28.Text
        det_par_covar(2, 9) = DetCovarBox29.Text
        det_par_covar(2, 10) = DetCovarBox2a.Text

        det_par_covar(3, 1) = DetCovarBox31.Text
        det_par_covar(3, 2) = DetCovarBox32.Text
        det_par_covar(3, 3) = DetCovarBox33.Text
        det_par_covar(3, 4) = DetCovarBox34.Text
        det_par_covar(3, 5) = DetCovarBox35.Text
        det_par_covar(3, 6) = DetCovarBox36.Text
        det_par_covar(3, 7) = DetCovarBox37.Text
        det_par_covar(3, 8) = DetCovarBox38.Text
        det_par_covar(3, 9) = DetCovarBox39.Text
        det_par_covar(3, 10) = DetCovarBox3a.Text

        det_par_covar(4, 1) = DetCovarBox41.Text
        det_par_covar(4, 2) = DetCovarBox42.Text
        det_par_covar(4, 3) = DetCovarBox43.Text
        det_par_covar(4, 4) = DetCovarBox44.Text
        det_par_covar(4, 5) = DetCovarBox45.Text
        det_par_covar(4, 6) = DetCovarBox46.Text
        det_par_covar(4, 7) = DetCovarBox47.Text
        det_par_covar(4, 8) = DetCovarBox48.Text
        det_par_covar(4, 9) = DetCovarBox49.Text
        det_par_covar(4, 10) = DetCovarBox4a.Text

        det_par_covar(5, 1) = DetCovarBox51.Text
        det_par_covar(5, 2) = DetCovarBox52.Text
        det_par_covar(5, 3) = DetCovarBox53.Text
        det_par_covar(5, 4) = DetCovarBox54.Text
        det_par_covar(5, 5) = DetCovarBox55.Text
        det_par_covar(5, 6) = DetCovarBox56.Text
        det_par_covar(5, 7) = DetCovarBox57.Text
        det_par_covar(5, 8) = DetCovarBox58.Text
        det_par_covar(5, 9) = DetCovarBox59.Text
        det_par_covar(5, 10) = DetCovarBox5a.Text

        det_par_covar(6, 1) = DetCovarBox61.Text
        det_par_covar(6, 2) = DetCovarBox62.Text
        det_par_covar(6, 3) = DetCovarBox63.Text
        det_par_covar(6, 4) = DetCovarBox64.Text
        det_par_covar(6, 5) = DetCovarBox65.Text
        det_par_covar(6, 6) = DetCovarBox66.Text
        det_par_covar(6, 7) = DetCovarBox67.Text
        det_par_covar(6, 8) = DetCovarBox68.Text
        det_par_covar(6, 9) = DetCovarBox69.Text
        det_par_covar(6, 10) = DetCovarBox6a.Text

        det_par_covar(7, 1) = DetCovarBox71.Text
        det_par_covar(7, 2) = DetCovarBox72.Text
        det_par_covar(7, 3) = DetCovarBox73.Text
        det_par_covar(7, 4) = DetCovarBox74.Text
        det_par_covar(7, 5) = DetCovarBox75.Text
        det_par_covar(7, 6) = DetCovarBox76.Text
        det_par_covar(7, 7) = DetCovarBox77.Text
        det_par_covar(7, 8) = DetCovarBox78.Text
        det_par_covar(7, 9) = DetCovarBox79.Text
        det_par_covar(7, 10) = DetCovarBox7a.Text

        det_par_covar(8, 1) = DetCovarBox81.Text
        det_par_covar(8, 2) = DetCovarBox82.Text
        det_par_covar(8, 3) = DetCovarBox83.Text
        det_par_covar(8, 4) = DetCovarBox84.Text
        det_par_covar(8, 5) = DetCovarBox85.Text
        det_par_covar(8, 6) = DetCovarBox86.Text
        det_par_covar(8, 7) = DetCovarBox87.Text
        det_par_covar(8, 8) = DetCovarBox88.Text
        det_par_covar(8, 9) = DetCovarBox89.Text
        det_par_covar(8, 10) = DetCovarBox8a.Text

        det_par_covar(9, 1) = DetCovarBox91.Text
        det_par_covar(9, 2) = DetCovarBox92.Text
        det_par_covar(9, 3) = DetCovarBox93.Text
        det_par_covar(9, 4) = DetCovarBox94.Text
        det_par_covar(9, 5) = DetCovarBox95.Text
        det_par_covar(9, 6) = DetCovarBox96.Text
        det_par_covar(9, 7) = DetCovarBox97.Text
        det_par_covar(9, 8) = DetCovarBox98.Text
        det_par_covar(9, 9) = DetCovarBox99.Text
        det_par_covar(9, 10) = DetCovarBox9a.Text

        det_par_covar(10, 1) = DetCovarBoxa1.Text
        det_par_covar(10, 2) = DetCovarBoxa2.Text
        det_par_covar(10, 3) = DetCovarBoxa3.Text
        det_par_covar(10, 4) = DetCovarBoxa4.Text
        det_par_covar(10, 5) = DetCovarBoxa5.Text
        det_par_covar(10, 6) = DetCovarBoxa6.Text
        det_par_covar(10, 7) = DetCovarBoxa7.Text
        det_par_covar(10, 8) = DetCovarBoxa8.Text
        det_par_covar(10, 9) = DetCovarBoxa9.Text
        det_par_covar(10, 10) = DetCovarBoxaa.Text

        If CheckBox1.Checked = True Then pt_cal_flag = True
        If CheckBox2.Checked = True Then pt_cal_flag = False
        det_cal_FH = cal_typical_FH_Box.Text
        det_cal_UPu_ratio = Cal_UPu_Box1.Text
        det_cal_pu240_eff = Cal_Pu240eff_Box.Text
        det_cal_alpha = Cal_Alpha_Box.Text
        det_cal_mod = Cal_mod_box.Text
        det_cal_ref_density = cal_standard_density_box.Text
        cal_wall_thickness = Cal_wall_Box1.Text
        cal_wall_material = cal_mat_box1.Text

        mult_corr(1) = mult_corr_box1.Text
        mult_corr(2) = mult_corr_box2.Text
        mult_corr(4) = mult_corr_err_box1.Text
        mult_corr(5) = mult_corr_err_box2.Text
        mult_corr(6) = mult_corr_covar_box.Text
        If Mult_corr_box3.Checked = True Then mult_corr_flag = True Else mult_corr_flag = False
        If mult_corr_flag Then mult_corr(3) = "mult_corr_on" Else mult_corr(3) = "mult_corr_off"

        For i = 7 To 10
            mult_corr(i) = 0
        Next i

        'overwrite contstants file for fitting routine

        outconstants = "c:\multiplicity_tmu\detector_parameters\det_parameters.csv"


        SaveFileDialog1.Filter = "CSV Files (*.csv*)|*.csv"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
       Then
            My.Computer.FileSystem.WriteAllText _
         (SaveFileDialog1.FileName, "", True)


            outconstants = SaveFileDialog1.FileName


            klineout = "Detector Parameters , " & outconstants & " , " & vbCrLf

            Dim fs As FileStream = File.Create(outconstants)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()

            For i = 2 To 11
                klineout = det_par_val(i - 1) & ", " & det_par_err(i - 1) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

            For i = 12 To 21
                klineout = det_par_covar(i - 11, 1)
                For j = 2 To 10
                    klineout = klineout & ", " & det_par_covar(i - 11, j)
                Next j
                klineout = klineout & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

            If pt_cal_flag Then klineout = "Pt_source_cal" & vbCrLf Else klineout = "distributed_cal" & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)

            klineout = det_cal_FH & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            klineout = det_cal_UPu_ratio & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            klineout = det_cal_pu240_eff & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            klineout = det_cal_alpha & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            klineout = det_cal_mod & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            klineout = det_cal_ref_density & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)

            klineout = inner_ring_eff_240 & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (29)
            klineout = outer_ring_eff_240 & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (30)
            klineout = cal_wall_thickness & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (31)
            klineout = cal_wall_material & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)                             ' (32)
            '
            For i = 1 To 10
                klineout = mult_corr(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next i

            For i = 1 To 100
                klineout = i & ", " & neut_energy(i) & ", " & det_eff_E(i) & ", " & inner_outer(i) & ", " & rel_fiss_prob(i) & ", " & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next i


        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If myForm16 Is Nothing Then
            myForm16 = New Form16
        End If
        myForm16.Show()
        myForm16 = Nothing
    End Sub

    Private Sub Mult_corr_box3_CheckedChanged(sender As Object, e As EventArgs) Handles Mult_corr_box3.CheckedChanged

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class