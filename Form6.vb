Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Public Class Form6
    '   Public iso_val(12), iso_val_err(12)
    '   Public iso_date(4) As Date
    '    Public geo_par(19)
    '   Dim item_id As String
    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MaskedTextBox9_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim item_info_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file

        item_info_file_name = "c:\multiplicity_tmu\item_info\current_item.csv"

        Call get_item_info_file(item_info_file_name)
        Call load_item_info_to_screen()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim item_info_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\multiplicity_tmu\item_info\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    item_info_file_name = openFileDialog1.FileName

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
        If item_info_file_name = "" Then Return
        item_file_name_read = item_info_file_name
        Call get_item_info_file(item_info_file_name)
        Call load_item_info_to_screen()
    End Sub


    Sub load_item_info_to_screen()

        ' ***** Load data file contents onto screen *****
        item_id_box.Text = item_id

        iso_par_box1.Text = iso_val(1)
        iso_par_box2.Text = iso_val(2)
        iso_par_box3.Text = iso_val(3)
        iso_par_box4.Text = iso_val(4)
        iso_par_box5.Text = iso_val(5)
        iso_par_box6.Text = iso_val(6)
        iso_par_box7.Text = iso_val(7)
        iso_par_box8.Text = iso_val(8)
        iso_par_box_err_1.Text = iso_val_err(1)
        iso_par_box_err_2.Text = iso_val_err(2)
        iso_par_box_err_3.Text = iso_val_err(3)
        iso_par_box_err_4.Text = iso_val_err(4)
        iso_par_box_err_5.Text = iso_val_err(5)
        iso_par_box_err_6.Text = iso_val_err(6)
        iso_par_box_err_7.Text = iso_val_err(7)
        iso_par_box_err_8.Text = iso_val_err(8)
        iso_date_box1.Text = iso_date(1)
        iso_date_box2.Text = iso_date(2)
        iso_date_box3.Text = iso_date(3)
        item_geo_box1.Text = geo_par(1)
        item_geo_box2.Text = geo_par(2)
        item_geo_box3.Text = geo_par(3)
        item_geo_box4.Text = geo_par(4)
        item_geo_box5.Text = geo_par(5)
        item_geo_box6.Text = geo_par(6)
        item_geo_box7.Text = geo_par(7)
        item_geo_box8.Text = geo_par(8)
        item_geo_box9.Text = geo_par(9)
        item_geo_box10.Text = geo_par(10)
        item_geo_box11.Text = geo_par(11)
        item_geo_box12.Text = geo_par(12)
        item_geo_box13.Text = geo_par(13)
        item_geo_box14.Text = geo_par(14)
        item_geo_box15.Text = geo_par(15)
        item_geo_box16.Text = geo_par(16)
        item_geo_box17.Text = geo_par(17)
        item_geo_box18.Text = geo_par(18)
        item_geo_box19.Text = geo_par(19)

        For i = 1 To 5

            If item_impurity_flag(i) = 1 Then CheckedListBox2.SetItemChecked(i - 1, True) Else CheckedListBox2.SetItemChecked(i - 1, False)

        Next i



        '  If jdex = 3 And idex > 22 And idex < 31 Then item_impurity_conc(idex - 22) = Val(str11)


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '  Save as current item 
        Dim item_info_file_name As String
        Dim outconstants As String
        Dim klineout As String
        Dim iso_label(50)


        'overwrite contstants file for fitting routine

        item_info_file_name = "c:\multiplicity_tmu\item_info\current_item.csv"

        SaveFileDialog1.Filter = "CSV Files (*.csv*)|*.csv"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
       Then
            My.Computer.FileSystem.WriteAllText _
         (SaveFileDialog1.FileName, "", True)


            item_info_file_name = SaveFileDialog1.FileName


            '   save revised parameters to default fission constant file

            item_id = item_id_box.Text
            iso_val(1) = iso_par_box1.Text
            iso_val(2) = iso_par_box2.Text
            iso_val(3) = iso_par_box3.Text
            iso_val(4) = iso_par_box4.Text
            iso_val(5) = iso_par_box5.Text
            iso_val(6) = iso_par_box6.Text
            iso_val(7) = iso_par_box7.Text
            iso_val(8) = iso_par_box8.Text
            iso_val_err(1) = iso_par_box_err_1.Text
            iso_val_err(2) = iso_par_box_err_2.Text
            iso_val_err(3) = iso_par_box_err_3.Text
            iso_val_err(4) = iso_par_box_err_4.Text
            iso_val_err(5) = iso_par_box_err_5.Text
            iso_val_err(6) = iso_par_box_err_6.Text
            iso_val_err(7) = iso_par_box_err_7.Text
            iso_val_err(8) = iso_par_box_err_8.Text
            iso_date(1) = iso_date_box1.Text
            iso_date(2) = iso_date_box2.Text
            iso_date(3) = iso_date_box3.Text
            geo_par(1) = item_geo_box1.Text
            geo_par(2) = item_geo_box2.Text
            geo_par(3) = item_geo_box3.Text
            geo_par(4) = item_geo_box4.Text
            geo_par(5) = item_geo_box5.Text
            geo_par(6) = item_geo_box6.Text
            geo_par(7) = item_geo_box7.Text
            geo_par(8) = item_geo_box8.Text
            geo_par(9) = item_geo_box9.Text
            geo_par(10) = item_geo_box10.Text
            geo_par(11) = item_geo_box11.Text
            geo_par(12) = item_geo_box12.Text
            geo_par(13) = item_geo_box13.Text
            geo_par(14) = item_geo_box14.Text
            geo_par(15) = item_geo_box15.Text
            geo_par(16) = item_geo_box16.Text
            geo_par(17) = item_geo_box17.Text
            geo_par(18) = item_geo_box18.Text
            geo_par(19) = item_geo_box19.Text

            For i = 1 To 5
                If CheckedListBox2.GetItemChecked(i - 1) = True Then item_impurity_flag(i) = 1 Else item_impurity_flag(i) = 0
                item_impurity_flag(i + 5) = 0
            Next i

            '     For i = 1 To 10
            '     item_impurity_conc(i) = 0
            '    Next

            iso_label(1) = "Item_ID"
            iso_label(2) = "Pu238"
            iso_label(3) = "Pu239"
            iso_label(4) = "Pu240"
            iso_label(5) = "Pu241"
            iso_label(6) = "Pu242"
            iso_label(7) = "Pu244"
            iso_label(8) = "Am241"
            iso_label(9) = "Cf252"
            iso_label(10) = "Pu_date"
            iso_label(11) = "Am_date"
            iso_label(12) = "Cf_date"
            iso_label(13) = "Geometry"
            iso_label(14) = "cont_ID"
            iso_label(15) = "cont_H"
            iso_label(16) = "Wall_thick"
            iso_label(17) = "Wall_mat"
            iso_label(18) = "FH_min"
            iso_label(19) = "FH_max"
            iso_label(20) = "off_x"
            iso_label(21) = "off_y"
            iso_label(22) = "Matrix_type"
            iso_label(23) = "UPu_ratio"
            iso_label(24) = "UPu_min"
            iso_label(25) = "UPu_max"
            iso_label(26) = "Ref_Density"
            iso_label(27) = "Min_Density"
            iso_label(28) = "Max_Density"
            iso_label(29) = "Ref_mod"
            iso_label(30) = "Mod_min"
            iso_label(31) = "Mod_max"
            '
            iso_label(32) = "Lithium"
            iso_label(33) = "Fluorine"
            iso_label(34) = "Boron"
            iso_label(35) = "Beryllium"
            iso_label(36) = "Carbon"
            iso_label(37) = "spare"
            iso_label(38) = "spare"
            iso_label(39) = "spare"
            iso_label(40) = "spare"
            iso_label(41) = "spare"


            klineout = "Item_Id , " & item_id & " , " & vbCrLf

            Dim fs As FileStream = File.Create(item_info_file_name)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()

            For i = 1 To 8
                klineout = iso_label(i + 1) & ", " & iso_val(i) & ", " & iso_val_err(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(item_info_file_name, klineout, True)

            Next i

            For i = 1 To 3
                klineout = iso_label(i + 9) & ", " & iso_date(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(item_info_file_name, klineout, True)

            Next i

            For i = 1 To 19
                klineout = iso_label(i + 12) & ", " & geo_par(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(item_info_file_name, klineout, True)

            Next i


            For i = 1 To 10
                klineout = iso_label(i + 31) & ", " & item_impurity_flag(i) & ", " & item_impurity_conc(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(item_info_file_name, klineout, True)

            Next i

        End If

    End Sub

    Private Sub Label32_Click(sender As Object, e As EventArgs) Handles Label32.Click

    End Sub

    Private Sub MaskedTextBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles item_geo_box14.MaskInputRejected

    End Sub

    Private Sub Max_Density_Box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles item_geo_box16.MaskInputRejected

    End Sub

    Private Sub item_geo_box10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles item_geo_box10.SelectedIndexChanged

    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        '  Save as current item 
        Dim item_info_file_name As String
        Dim outconstants As String
        Dim klineout As String
        Dim iso_label(50)

        item_info_file_name = "c:\multiplicity_tmu\item_info\current_item.csv"

        '   save revised parameters to default fission constant file

        item_id = item_id_box.Text
        iso_val(1) = iso_par_box1.Text
        iso_val(2) = iso_par_box2.Text
        iso_val(3) = iso_par_box3.Text
        iso_val(4) = iso_par_box4.Text
        iso_val(5) = iso_par_box5.Text
        iso_val(6) = iso_par_box6.Text
        iso_val(7) = iso_par_box7.Text
        iso_val(8) = iso_par_box8.Text
        iso_val_err(1) = iso_par_box_err_1.Text
        iso_val_err(2) = iso_par_box_err_2.Text
        iso_val_err(3) = iso_par_box_err_3.Text
        iso_val_err(4) = iso_par_box_err_4.Text
        iso_val_err(5) = iso_par_box_err_5.Text
        iso_val_err(6) = iso_par_box_err_6.Text
        iso_val_err(7) = iso_par_box_err_7.Text
        iso_val_err(8) = iso_par_box_err_8.Text
        iso_date(1) = iso_date_box1.Text
        iso_date(2) = iso_date_box2.Text
        iso_date(3) = iso_date_box3.Text
        geo_par(1) = item_geo_box1.Text
        geo_par(2) = item_geo_box2.Text
        geo_par(3) = item_geo_box3.Text
        geo_par(4) = item_geo_box4.Text
        geo_par(5) = item_geo_box5.Text
        geo_par(6) = item_geo_box6.Text
        geo_par(7) = item_geo_box7.Text
        geo_par(8) = item_geo_box8.Text
        geo_par(9) = item_geo_box9.Text
        geo_par(10) = item_geo_box10.Text
        geo_par(11) = item_geo_box11.Text
        geo_par(12) = item_geo_box12.Text
        geo_par(13) = item_geo_box13.Text
        geo_par(14) = item_geo_box14.Text
        geo_par(15) = item_geo_box15.Text
        geo_par(16) = item_geo_box16.Text
        geo_par(17) = item_geo_box17.Text
        geo_par(18) = item_geo_box18.Text
        geo_par(19) = item_geo_box19.Text

        For i = 1 To 5
            If CheckedListBox2.GetItemChecked(i - 1) = True Then item_impurity_flag(i) = 1 Else item_impurity_flag(i) = 0
            item_impurity_flag(i + 5) = 0
        Next i

        '     For i = 1 To 10
        '     item_impurity_conc(i) = 0
        '    Next

        iso_label(1) = "Item_ID"
        iso_label(2) = "Pu238"
        iso_label(3) = "Pu239"
        iso_label(4) = "Pu240"
        iso_label(5) = "Pu241"
        iso_label(6) = "Pu242"
        iso_label(7) = "Pu244"
        iso_label(8) = "Am241"
        iso_label(9) = "Cf252"
        iso_label(10) = "Pu_date"
        iso_label(11) = "Am_date"
        iso_label(12) = "Cf_date"
        iso_label(13) = "Geometry"
        iso_label(14) = "cont_ID"
        iso_label(15) = "cont_H"
        iso_label(16) = "Wall_thick"
        iso_label(17) = "Wall_mat"
        iso_label(18) = "FH_min"
        iso_label(19) = "FH_max"
        iso_label(20) = "off_x"
        iso_label(21) = "off_y"
        iso_label(22) = "Matrix_type"
        iso_label(23) = "UPu_ratio"
        iso_label(24) = "UPu_min"
        iso_label(25) = "UPu_max"
        iso_label(26) = "Ref_Density"
        iso_label(27) = "Min_Density"
        iso_label(28) = "Max_Density"
        iso_label(29) = "Ref_mod"
        iso_label(30) = "Mod_min"
        iso_label(31) = "Mod_max"
        '
        iso_label(32) = "Lithium"
        iso_label(33) = "Fluorine"
        iso_label(34) = "Boron"
        iso_label(35) = "Beryllium"
        iso_label(36) = "Carbon"
        iso_label(37) = "spare"
        iso_label(38) = "spare"
        iso_label(39) = "spare"
        iso_label(40) = "spare"
        iso_label(41) = "spare"

        'overwrite contstants file for fitting routine

        item_info_file_name = "c:\multiplicity_tmu\item_info\current_item.csv"

        klineout = "Item_Id , " & item_id & " , " & vbCrLf

        Dim fs As FileStream = File.Create(item_info_file_name)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        For i = 1 To 8
            klineout = iso_label(i + 1) & ", " & iso_val(i) & ", " & iso_val_err(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(item_info_file_name, klineout, True)

        Next i

        For i = 1 To 3
            klineout = iso_label(i + 9) & ", " & iso_date(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(item_info_file_name, klineout, True)

        Next i

        For i = 1 To 19
            klineout = iso_label(i + 12) & ", " & geo_par(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(item_info_file_name, klineout, True)

        Next i

        For i = 1 To 10
            klineout = iso_label(i + 31) & ", " & item_impurity_flag(i) & ", " & item_impurity_conc(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(item_info_file_name, klineout, True)

        Next i

        Close()

    End Sub


End Class