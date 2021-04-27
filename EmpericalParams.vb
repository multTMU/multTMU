Imports System.IO
Imports System.Text
Public Class EmpericalParams

    Private Sub SumHistoTable1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        empirical_par_file = "c:\multiplicity_TMU\misc_parameters\tmu_empirical_parameters.csv"
        Call get_empirical_par_file(empirical_par_file)

        mult_box1.Text = mult_par(1)
        mult_box2.Text = mult_par(2)
        mult_box3.Text = mult_par(3)
        mult_box4.Text = mult_par(4)
        mult_box5.Text = mult_par(5)
        mult_box6.Text = mult_par(6)
        mult_box7.Text = mult_par(7)
        '    mult_box8.text = mult_par(8)
        '    mult_box9.text = mult_par(9)
        '    mult_box10.text = mult_par(10)
        '
        Dev_Pu_Box1.Text = dev_pu240_par(1)
        Dev_Pu_Box2.Text = dev_pu240_par(2)
        '    Dev_Pu_Box3.text = dev_pu240_par(3)
        '    Dev_Pu_Box4.text = dev_pu240_par(4)
        '    Dev_Pu_Box5.text = dev_pu240_par(5)
        '    Dev_Pu_Box6.text = dev_pu240_par(6)
        '    Dev_Pu_Box7.text = dev_pu240_par(7)
        '    Dev_Pu_Box8.text = dev_pu240_par(8)
        '    Dev_Pu_Box9.text = dev_pu240_par(9)
        '    Dev_Pu_Box10.text = dev_pu240_par(10)
        '
        UPu_par_box1.Text = dev_UPu_par(1)
        UPu_par_box2.Text = dev_UPu_par(2)
        UPu_par_box3.Text = dev_UPu_par(3)
        UPu_par_box4.Text = dev_UPu_par(4)
        UPu_par_box5.Text = dev_UPu_par(5)
        '    Upu_par_Box6.text = Dev_UPu_par(6)
        '    Upu_par_Box7.text = Dev_UPu_par(7)
        '    Upu_par_Box8.text = Dev_UPu_par(8)
        '    Upu_par_Box9.text = Dev_UPu_par(9)
        '    Upu_par_Box10.text = Dev_UPu_par(10)
        '
        alpha_par_box1.Text = dev_alpha_par(1)
        alpha_par_box2.Text = dev_alpha_par(2)
        alpha_par_box3.Text = dev_alpha_par(3)
        alpha_par_box4.text = dev_alpha_par(4)
        alpha_par_box5.text = dev_alpha_par(5)
        alpha_par_box6.text = dev_alpha_par(6)
        alpha_par_box7.text = dev_alpha_par(7)
        alpha_par_box8.text = dev_alpha_par(8)
        alpha_par_box9.text = dev_alpha_par(9)
        alpha_par_box10.text = dev_alpha_par(10)
        alpha_par_box11.Text = dev_alpha_par(11)
        alpha_par_box12.Text = dev_alpha_par(12)
        alpha_par_box13.Text = dev_alpha_par(13)
        alpha_par_box14.text = dev_alpha_par(14)
        alpha_par_box15.text = dev_alpha_par(15)
        '     alpha_par_box16.text = dev_alpha_par(16)
        '     alpha_par_box17.text = dev_alpha_par(17)
        '     alpha_par_box18.text = dev_alpha_par(18)

        mod_par_box1.Text = dev_mod_par(1)
        mod_par_box2.Text = dev_mod_par(2)
        mod_par_box3.Text = dev_mod_par(3)
        '    mod_par_box4.text = dev_mod_par(4)
        '    mod_par_box5.text = dev_mod_par(5)
        '    mod_par_box6.text = dev_mod_par(6)
        '    mod_par_box7.text = dev_mod_par(7)
        '    mod_par_box8.text = dev_mod_par(8)
        '    mod_par_box9.text = dev_mod_par(9)
        '    mod_par_box10.text = dev_mod_par(10)
        rho_par_box1.Text = dev_rho_par(1)
        rho_par_box2.Text = dev_rho_par(2)
        rho_par_box3.Text = dev_rho_par(3)
        rho_par_box4.Text = dev_rho_par(4)
        rho_par_box5.Text = dev_rho_par(5)

        wall_par_Box1.Text = dev_wall_par(1)

    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '  Save as current item 
        Dim misc_par_file_name As String
        Dim outconstants As String
        Dim klineout As String
        Dim emp_param(78)
        Dim par_label(78) As String

        misc_par_file_name = "c:\multiplicity_tmu\misc_parameters\tmu_empirical_parameters.csv"

        '   save revised parameters to default fission constant file

        For i = 1 To 78
            emp_param(i) = 0
        Next i

        emp_param(1) = mult_box1.Text
        emp_param(2) = mult_box2.Text
        emp_param(3) = mult_box3.Text
        emp_param(4) = mult_box4.Text
        emp_param(5) = mult_box5.Text
        emp_param(6) = mult_box6.Text
        emp_param(7) = mult_box7.Text

        emp_param(11) = Dev_Pu_Box1.Text
        emp_param(12) = Dev_Pu_Box2.Text

        emp_param(21) = UPu_par_box1.Text
        emp_param(22) = UPu_par_box2.Text
        emp_param(23) = UPu_par_box3.Text
        emp_param(24) = UPu_par_box4.Text
        emp_param(25) = UPu_par_box5.Text

        emp_param(31) = alpha_par_box1.Text
        emp_param(32) = alpha_par_box2.Text
        emp_param(33) = alpha_par_box3.Text
        emp_param(34) = alpha_par_box4.Text
        emp_param(35) = alpha_par_box5.Text
        emp_param(36) = alpha_par_box6.Text
        emp_param(37) = alpha_par_box7.Text
        emp_param(38) = alpha_par_box8.Text
        emp_param(39) = alpha_par_box9.Text
        emp_param(40) = alpha_par_box10.Text
        emp_param(41) = alpha_par_box11.Text
        emp_param(42) = alpha_par_box12.Text
        emp_param(43) = alpha_par_box13.Text
        emp_param(44) = alpha_par_box14.Text
        emp_param(45) = alpha_par_box15.Text

        emp_param(49) = mod_par_box1.Text
        emp_param(50) = mod_par_box2.Text
        emp_param(51) = mod_par_box3.Text

        emp_param(59) = rho_par_box1.Text
        emp_param(60) = rho_par_box2.Text
        emp_param(61) = rho_par_box3.Text
        emp_param(62) = rho_par_box4.Text
        emp_param(63) = rho_par_box5.Text

        emp_param(69) = wall_par_Box1.Text

        par_label(1) = "mult_par_1"
        par_label(2) = "mult_par_2"
        par_label(3) = "mult_par_3"
        par_label(4) = "mult_par_4"
        par_label(5) = "mult_par_5"
        par_label(6) = "mult_par_6"
        par_label(7) = "mult_par_7"
        par_label(8) = "mult_par_8"
        par_label(9) = "mult_par_9"
        par_label(10) = "mult_par_10"
        par_label(11) = "dev_Pu240_1"
        par_label(12) = "dev_Pu240_2"
        par_label(13) = "dev_Pu240_3"
        par_label(14) = "dev_Pu240_4"
        par_label(15) = "dev_Pu240_5"
        par_label(16) = "dev_Pu240_6"
        par_label(17) = "dev_Pu240_7"
        par_label(18) = "dev_Pu240_8"
        par_label(19) = "dev_Pu240_9"
        par_label(20) = "dev_Pu240_10"
        par_label(21) = "UPu_par_1"
        par_label(22) = "UPu_par_2"
        par_label(23) = "UPu_par_3"
        par_label(24) = "UPu_par_4"
        par_label(25) = "UPu_par_5"
        par_label(26) = "UPu_par_6"
        par_label(27) = "UPu_par_7"
        par_label(28) = "UPu_par_8"
        par_label(29) = "UPu_par_9"
        par_label(30) = "UPu_par_10"
        par_label(31) = "alpha_par_1"
        par_label(32) = "alpha_par_2"
        par_label(33) = "alpha_par_3"
        par_label(34) = "alpha_par_4"
        par_label(35) = "alpha_par_5"
        par_label(36) = "alpha_par_6"
        par_label(37) = "alpha_par_7"
        par_label(38) = "alpha_par_8"
        par_label(39) = "alpha_par_9"
        par_label(40) = "alpha_par_10"
        par_label(41) = "alpha_par_11"
        par_label(42) = "alpha_par_12"
        par_label(43) = "alpha_par_13"
        par_label(44) = "alpha_par_14"
        par_label(45) = "alpha_par_15"
        par_label(46) = "alpha_par_16"
        par_label(47) = "alpha_par_17"
        par_label(48) = "alpha_par_18"
        par_label(49) = "mod_par_1"
        par_label(50) = "mod_par_2"
        par_label(51) = "mod_par_3"
        par_label(52) = "mod_par_4"
        par_label(53) = "mod_par_5"
        par_label(54) = "mod_par_6"
        par_label(55) = "mod_par_7"
        par_label(56) = "mod_par_8"
        par_label(57) = "mod_par_9"
        par_label(58) = "mod_par_10"

        par_label(59) = "rho_par_1"
        par_label(60) = "rho_par_2"
        par_label(61) = "rho_par_3"
        par_label(62) = "rho_par_4"
        par_label(63) = "rho_par_5"
        par_label(64) = "rho_par_6"
        par_label(65) = "rho_par_7"
        par_label(66) = "rho_par_8"
        par_label(67) = "rho_par_9"
        par_label(68) = "rho_par_10"

        par_label(69) = "wall_par_1"
        par_label(70) = "wall_par_2"
        par_label(71) = "wall_par_3"
        par_label(72) = "rho_par_4"
        par_label(73) = "wall_par_5"
        par_label(74) = "wall_par_6"
        par_label(75) = "wall_par_7"
        par_label(76) = "wall_par_8"
        par_label(77) = "wall_par_9"
        par_label(78) = "wall_par_10"


        'overwrite contstants file for fitting routine

        klineout = par_label(1) & ", " & emp_param(1) & " , " & vbCrLf

        Dim fs As FileStream = File.Create(misc_par_file_name)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        For i = 2 To 78
            klineout = par_label(i) & ", " & emp_param(i) & " , " & vbCrLf
            My.Computer.FileSystem.WriteAllText(misc_par_file_name, klineout, True)

        Next i


        Close()
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub
End Class