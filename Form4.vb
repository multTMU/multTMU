
Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Public Class Form4
    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    '   Public iso_par_file_name As String
    '  Public half_life(8), half_life_err(8)
    ' Public spont_fiss_rate(8), spont_fiss_err(8)
    ' Public alpha_n_rate(8), alpha_n_err(8)
    ' Public m240_conv(8), m240_conv_err(8)
    ' Public f_alpha_n_rate(8), f_alpha_n_err(8)

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim iso_par_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file

        iso_par_file_name = "c:\multiplicity_tmu\nuclide_data\nuclide_decay_data.csv"

        Call get_iso_par_file(iso_par_file_name)
        Call load_nuc_data_to_screen(iso_par_file_name)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub



    Private Sub RichTextBox40_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox40.TextChanged

    End Sub

    Private Sub SaveFileDialog2_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog2.FileOk

    End Sub

    Public Property FileName As String
    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim det_par_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\multiplicity_tmu\nuclide_data\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    iso_par_file_name = openFileDialog1.FileName

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

        Call get_iso_par_file(iso_par_file_name)
        Call load_nuc_data_to_screen(iso_par_file_name)

    End Sub



    Sub load_nuc_data_to_screen(iso_par_file_name)

        ' ***** Load data file contents onto screen *****
        constantsfilenamebox1.Text = iso_par_file_name

        HallifeBox1.Text = half_life(1)
        HallifeBox2.Text = half_life(2)
        HallifeBox3.Text = half_life(3)
        HallifeBox4.Text = half_life(4)
        HallifeBox5.Text = half_life(5)
        HallifeBox6.Text = half_life(6)
        HallifeBox7.Text = half_life(7)
        HallifeBox8.Text = half_life(8)

        HallifeErrBox1.Text = half_life_err(1)
        HallifeErrBox2.Text = half_life_err(2)
        HallifeErrBox3.Text = half_life_err(3)
        HallifeErrBox4.Text = half_life_err(4)
        HallifeErrBox5.Text = half_life_err(5)
        HallifeErrBox6.Text = half_life_err(6)
        HallifeErrBox7.Text = half_life_err(7)
        HallifeErrBox8.Text = half_life_err(8)

        spntfissBox1.Text = spont_fiss_rate(1)
        spntfissBox2.Text = spont_fiss_rate(2)
        spntfissBox3.Text = spont_fiss_rate(3)
        spntfissBox4.Text = spont_fiss_rate(4)
        spntfissBox5.Text = spont_fiss_rate(5)
        spntfissBox6.Text = spont_fiss_rate(6)
        spntfissBox7.Text = spont_fiss_rate(7)
        spntfissBox8.Text = spont_fiss_rate(8)

        SpnterrBox1.Text = spont_fiss_err(1)
        SpnterrBox2.Text = spont_fiss_err(2)
        SpnterrBox3.Text = spont_fiss_err(3)
        SpnterrBox4.Text = spont_fiss_err(4)
        SpnterrBox5.Text = spont_fiss_err(5)
        SpnterrBox6.Text = spont_fiss_err(6)
        SpnterrBox7.Text = spont_fiss_err(7)
        SpnterrBox8.Text = spont_fiss_err(8)

        Alp_nBox1.Text = alpha_n_rate(1)
        Alp_nBox2.Text = alpha_n_rate(2)
        Alp_nBox3.Text = alpha_n_rate(3)
        Alp_nBox4.Text = alpha_n_rate(4)
        Alp_nBox5.Text = alpha_n_rate(5)
        Alp_nBox6.Text = alpha_n_rate(6)
        Alp_nBox7.Text = alpha_n_rate(7)
        Alp_nBox8.Text = alpha_n_rate(8)

        Alp_nerrBox1.Text = alpha_n_err(1)
        Alp_nerrBox2.Text = alpha_n_err(2)
        Alp_nerrBox3.Text = alpha_n_err(3)
        Alp_nerrBox4.Text = alpha_n_err(4)
        Alp_nerrBox5.Text = alpha_n_err(5)
        Alp_nerrBox6.Text = alpha_n_err(6)
        Alp_nerrBox7.Text = alpha_n_err(7)
        Alp_nerrBox8.Text = alpha_n_err(8)


        F4_alpha_Box1.Text = f_alpha_n_rate(1)
        F4_alpha_Box2.Text = f_alpha_n_rate(2)
        F4_alpha_Box3.Text = f_alpha_n_rate(3)
        F4_alpha_Box4.Text = f_alpha_n_rate(4)
        F4_alpha_Box5.Text = f_alpha_n_rate(5)
        F4_alpha_Box6.Text = f_alpha_n_rate(6)
        F4_alpha_Box7.Text = f_alpha_n_rate(7)
        F4_alpha_Box8.Text = f_alpha_n_rate(8)

        F4_alpha_err_Box1.Text = f_alpha_n_err(1)
        F4_alpha_err_Box2.Text = f_alpha_n_err(2)
        F4_alpha_err_Box3.Text = f_alpha_n_err(3)
        F4_alpha_err_Box4.Text = f_alpha_n_err(4)
        F4_alpha_err_Box5.Text = f_alpha_n_err(5)
        F4_alpha_err_Box6.Text = f_alpha_n_err(6)
        F4_alpha_err_Box7.Text = f_alpha_n_err(7)
        F4_alpha_err_Box8.Text = f_alpha_n_err(8)



        m240_Box1.Text = m240_conv(1)
        m240_Box2.Text = m240_conv(2)
        m240_Box3.Text = m240_conv(3)
        m240_Box4.Text = m240_conv(4)
        m240_Box5.Text = m240_conv(5)
        m240_Box6.Text = m240_conv(6)
        m240_Box7.Text = m240_conv(7)
        m240_Box8.Text = m240_conv(8)

        m240_errBox1.Text = m240_conv_err(1)
        m240_errBox2.Text = m240_conv_err(2)
        m240_errBox3.Text = m240_conv_err(3)
        m240_errBox4.Text = m240_conv_err(4)
        m240_errBox5.Text = m240_conv_err(5)
        m240_errBox6.Text = m240_conv_err(6)
        m240_errBox7.Text = m240_conv_err(7)
        m240_errBox8.Text = m240_conv_err(8)


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim outconstants As String
        Dim klineout As String

        '   save revised parameters to default fission constant file

        half_life(1) = HallifeBox1.Text
        half_life(2) = HallifeBox2.Text
        half_life(3) = HallifeBox3.Text
        half_life(4) = HallifeBox4.Text
        half_life(5) = HallifeBox5.Text
        half_life(6) = HallifeBox6.Text
        half_life(7) = HallifeBox7.Text
        half_life(8) = HallifeBox8.Text

        half_life_err(1) = HallifeErrBox1.Text
        half_life_err(2) = HallifeErrBox2.Text
        half_life_err(3) = HallifeErrBox3.Text
        half_life_err(4) = HallifeErrBox4.Text
        half_life_err(5) = HallifeErrBox5.Text
        half_life_err(6) = HallifeErrBox6.Text
        half_life_err(7) = HallifeErrBox7.Text
        half_life_err(8) = HallifeErrBox8.Text

        spont_fiss_rate(1) = spntfissBox1.Text
        spont_fiss_rate(2) = spntfissBox2.Text
        spont_fiss_rate(3) = spntfissBox3.Text
        spont_fiss_rate(4) = spntfissBox4.Text
        spont_fiss_rate(5) = spntfissBox5.Text
        spont_fiss_rate(6) = spntfissBox6.Text
        spont_fiss_rate(7) = spntfissBox7.Text
        spont_fiss_rate(8) = spntfissBox8.Text

        spont_fiss_err(1) = SpnterrBox1.Text
        spont_fiss_err(2) = SpnterrBox2.Text
        spont_fiss_err(3) = SpnterrBox3.Text
        spont_fiss_err(4) = SpnterrBox4.Text
        spont_fiss_err(5) = SpnterrBox5.Text
        spont_fiss_err(6) = SpnterrBox6.Text
        spont_fiss_err(7) = SpnterrBox7.Text
        spont_fiss_err(8) = SpnterrBox8.Text

        alpha_n_rate(1) = Alp_nBox1.Text
        alpha_n_rate(2) = Alp_nBox2.Text
        alpha_n_rate(3) = Alp_nBox3.Text
        alpha_n_rate(4) = Alp_nBox4.Text
        alpha_n_rate(5) = Alp_nBox5.Text
        alpha_n_rate(6) = Alp_nBox6.Text
        alpha_n_rate(7) = Alp_nBox7.Text
        alpha_n_rate(8) = Alp_nBox8.Text

        alpha_n_err(1) = Alp_nerrBox1.Text
        alpha_n_err(2) = Alp_nerrBox2.Text
        alpha_n_err(3) = Alp_nerrBox3.Text
        alpha_n_err(4) = Alp_nerrBox4.Text
        alpha_n_err(5) = Alp_nerrBox5.Text
        alpha_n_err(6) = Alp_nerrBox6.Text
        alpha_n_err(7) = Alp_nerrBox7.Text
        alpha_n_err(8) = Alp_nerrBox8.Text


        f_alpha_n_rate(1) = F4_alpha_Box1.Text
        f_alpha_n_rate(2) = F4_alpha_Box2.Text
        f_alpha_n_rate(3) = F4_alpha_Box3.Text
        f_alpha_n_rate(4) = F4_alpha_Box4.Text
        f_alpha_n_rate(5) = F4_alpha_Box5.Text
        f_alpha_n_rate(6) = F4_alpha_Box6.Text
        f_alpha_n_rate(7) = F4_alpha_Box7.Text
        f_alpha_n_rate(8) = F4_alpha_Box8.Text

        f_alpha_n_err(1) = F4_alpha_err_Box1.Text
        f_alpha_n_err(2) = F4_alpha_err_Box2.Text
        f_alpha_n_err(3) = F4_alpha_err_Box3.Text
        f_alpha_n_err(4) = F4_alpha_err_Box4.Text
        f_alpha_n_err(5) = F4_alpha_err_Box5.Text
        f_alpha_n_err(6) = F4_alpha_err_Box6.Text
        f_alpha_n_err(7) = F4_alpha_err_Box7.Text
        f_alpha_n_err(8) = F4_alpha_err_Box8.Text

        m240_conv(1) = m240_Box1.Text
        m240_conv(2) = m240_Box2.Text
        m240_conv(3) = m240_Box3.Text
        m240_conv(4) = m240_Box4.Text
        m240_conv(5) = m240_Box5.Text
        m240_conv(6) = m240_Box6.Text
        m240_conv(7) = m240_Box7.Text
        m240_conv(8) = m240_Box8.Text

        m240_conv_err(1) = m240_errBox1.Text
        m240_conv_err(2) = m240_errBox2.Text
        m240_conv_err(3) = m240_errBox3.Text
        m240_conv_err(4) = m240_errBox4.Text
        m240_conv_err(5) = m240_errBox5.Text
        m240_conv_err(6) = m240_errBox6.Text
        m240_conv_err(7) = m240_errBox7.Text
        m240_conv_err(8) = m240_errBox8.Text


        'overwrite default isotopics parameter file for fitting routine

        outconstants = "c:\multiplicity_tmu\nuclide_data\nuclide_decay_data.csv"

        klineout = "Nuclide data , " & outconstants & " , " & vbCrLf

        Dim fs As FileStream = File.Create(outconstants)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        For i = 2 To 9
            klineout = half_life(i - 1) & ", " & half_life_err(i - 1)
            klineout = klineout & ", " & spont_fiss_rate(i - 1) & ", " & spont_fiss_err(i - 1)
            klineout = klineout & ", " & alpha_n_rate(i - 1) & ", " & alpha_n_err(i - 1)
            klineout = klineout & ", " & f_alpha_n_rate(i - 1) & ", " & f_alpha_n_err(i - 1)
            klineout = klineout & ", " & m240_conv(i - 1) & ", " & m240_conv_err(i - 1) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)

        Next i



        Close()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim outconstants As String
        Dim klineout As String

        '   save revised parameters to default fission constant file

        half_life(1) = HallifeBox1.Text
        half_life(2) = HallifeBox2.Text
        half_life(3) = HallifeBox3.Text
        half_life(4) = HallifeBox4.Text
        half_life(5) = HallifeBox5.Text
        half_life(6) = HallifeBox6.Text
        half_life(7) = HallifeBox7.Text
        half_life(8) = HallifeBox8.Text

        half_life_err(1) = HallifeErrBox1.Text
        half_life_err(2) = HallifeErrBox2.Text
        half_life_err(3) = HallifeErrBox3.Text
        half_life_err(4) = HallifeErrBox4.Text
        half_life_err(5) = HallifeErrBox5.Text
        half_life_err(6) = HallifeErrBox6.Text
        half_life_err(7) = HallifeErrBox7.Text
        half_life_err(8) = HallifeErrBox8.Text

        spont_fiss_rate(1) = spntfissBox1.Text
        spont_fiss_rate(2) = spntfissBox2.Text
        spont_fiss_rate(3) = spntfissBox3.Text
        spont_fiss_rate(4) = spntfissBox4.Text
        spont_fiss_rate(5) = spntfissBox5.Text
        spont_fiss_rate(6) = spntfissBox6.Text
        spont_fiss_rate(7) = spntfissBox7.Text
        spont_fiss_rate(8) = spntfissBox8.Text

        spont_fiss_err(1) = SpnterrBox1.Text
        spont_fiss_err(2) = SpnterrBox2.Text
        spont_fiss_err(3) = SpnterrBox3.Text
        spont_fiss_err(4) = SpnterrBox4.Text
        spont_fiss_err(5) = SpnterrBox5.Text
        spont_fiss_err(6) = SpnterrBox6.Text
        spont_fiss_err(7) = SpnterrBox7.Text
        spont_fiss_err(8) = SpnterrBox8.Text

        alpha_n_rate(1) = Alp_nBox1.Text
        alpha_n_rate(2) = Alp_nBox2.Text
        alpha_n_rate(3) = Alp_nBox3.Text
        alpha_n_rate(4) = Alp_nBox4.Text
        alpha_n_rate(5) = Alp_nBox5.Text
        alpha_n_rate(6) = Alp_nBox6.Text
        alpha_n_rate(7) = Alp_nBox7.Text
        alpha_n_rate(8) = Alp_nBox8.Text

        alpha_n_err(1) = Alp_nerrBox1.Text
        alpha_n_err(2) = Alp_nerrBox2.Text
        alpha_n_err(3) = Alp_nerrBox3.Text
        alpha_n_err(4) = Alp_nerrBox4.Text
        alpha_n_err(5) = Alp_nerrBox5.Text
        alpha_n_err(6) = Alp_nerrBox6.Text
        alpha_n_err(7) = Alp_nerrBox7.Text
        alpha_n_err(8) = Alp_nerrBox8.Text


        f_alpha_n_rate(1) = F4_alpha_Box1.Text
        f_alpha_n_rate(2) = F4_alpha_Box2.Text
        f_alpha_n_rate(3) = F4_alpha_Box3.Text
        f_alpha_n_rate(4) = F4_alpha_Box4.Text
        f_alpha_n_rate(5) = F4_alpha_Box5.Text
        f_alpha_n_rate(6) = F4_alpha_Box6.Text
        f_alpha_n_rate(7) = F4_alpha_Box7.Text
        f_alpha_n_rate(8) = F4_alpha_Box8.Text

        f_alpha_n_err(1) = F4_alpha_err_Box1.Text
        f_alpha_n_err(2) = F4_alpha_err_Box2.Text
        f_alpha_n_err(3) = F4_alpha_err_Box3.Text
        f_alpha_n_err(4) = F4_alpha_err_Box4.Text
        f_alpha_n_err(5) = F4_alpha_err_Box5.Text
        f_alpha_n_err(6) = F4_alpha_err_Box6.Text
        f_alpha_n_err(7) = F4_alpha_err_Box7.Text
        f_alpha_n_err(8) = F4_alpha_err_Box8.Text

        m240_conv(1) = m240_Box1.Text
        m240_conv(2) = m240_Box2.Text
        m240_conv(3) = m240_Box3.Text
        m240_conv(4) = m240_Box4.Text
        m240_conv(5) = m240_Box5.Text
        m240_conv(6) = m240_Box6.Text
        m240_conv(7) = m240_Box7.Text
        m240_conv(8) = m240_Box8.Text

        m240_conv_err(1) = m240_errBox1.Text
        m240_conv_err(2) = m240_errBox2.Text
        m240_conv_err(3) = m240_errBox3.Text
        m240_conv_err(4) = m240_errBox4.Text
        m240_conv_err(5) = m240_errBox5.Text
        m240_conv_err(6) = m240_errBox6.Text
        m240_conv_err(7) = m240_errBox7.Text
        m240_conv_err(8) = m240_errBox8.Text


        'overwrite default isotopics parameter file for fitting routine

        outconstants = "c:\multiplicity_tmu\nuclide_data\nuclide_decay_data.csv"

        SaveFileDialog1.Filter = "CSV Files (*.csv*)|*.csv"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
       Then
            My.Computer.FileSystem.WriteAllText _
         (SaveFileDialog1.FileName, "", True)


            outconstants = SaveFileDialog1.FileName

            klineout = "Nuclide data , " & outconstants & " , " & vbCrLf

            Dim fs As FileStream = File.Create(outconstants)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()

            For i = 2 To 9
                klineout = half_life(i - 1) & ", " & half_life_err(i - 1)
                klineout = klineout & ", " & spont_fiss_rate(i - 1) & ", " & spont_fiss_err(i - 1)
                klineout = klineout & ", " & alpha_n_rate(i - 1) & ", " & alpha_n_err(i - 1)
                klineout = klineout & ", " & f_alpha_n_rate(i - 1) & ", " & f_alpha_n_err(i - 1)
                klineout = klineout & ", " & m240_conv(i - 1) & ", " & m240_conv_err(i - 1) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)

            Next i

        End If

    End Sub
End Class