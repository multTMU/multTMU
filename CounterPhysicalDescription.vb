Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Public Class CounterPhysicalDescription


    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim det_dim_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file

        det_dim_file_name = "c:\multiplicity_tmu\detector_dimensions\current_det_dimensions.csv"

        If det_dim_file_name <> "" Then Call get_det_dimensions_file(det_dim_file_name)
        Call load_det_to_screen()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim det_dim_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\multiplicity_tmu\detector_dimensions\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    det_dim_file_name = openFileDialog1.FileName

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

        Call get_det_dimensions_file(det_dim_file_name)
        Call load_det_to_screen()

    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '  Save as current item 
        Dim det_dimfile_name, det_dim_label(17) As String
        Dim outconstants, tube_num As String
        Dim klineout As String
        Dim iso_label(25)

        det_dimfile_name = "c:\multiplicity_tmu\detector_dimensions\current_det_dimensions.csv"

        counter_id = Counter_Name_Box.Text
        current_det_file_name = det_dimfile_name
        det_dim_val(1) = counter_geo_box.Text
        det_dim_val(2) = cavity_height_Box1.Text
        det_dim_val(3) = Cavity_ID_Box.Text
        det_dim_val(4) = Cavity_Length_Box.Text
        det_dim_val(5) = Cd_liner_Box.Text
        det_dim_val(6) = Tube_ID_Box.Text
        det_dim_val(7) = Tube_Length_Box.Text
        det_dim_val(8) = Number_rows_Box.Text
        det_dim_val(9) = 0
        det_dim_val(10) = Ring_1_radius_Box.Text
        det_dim_val(11) = Ring_2_radius_Box.Text
        det_dim_val(12) = Ring_3_radius_Box.Text
        det_dim_val(13) = Ring_4_radius_Box.Text
        det_dim_val(14) = Ring_5_radius_Box.Text
        det_dim_val(15) = Item_Stand_Box.Text
        tube_numbers(1) = Ring_1_tubes_Box.Text
        tube_numbers(2) = Ring_2_tubes_Box.Text
        tube_numbers(3) = Ring_3_tubes_Box.Text
        tube_numbers(4) = Ring_4_tubes_Box.Text
        tube_numbers(5) = Ring_5_tubes_Box.Text


        '   save revised parameters to current detector dimensions file



        det_dim_label(1) = "cavity_type"
        det_dim_label(2) = "Cav_height"
        det_dim_label(3) = "Cav_width"
        det_dim_label(4) = "Cav_length"
        det_dim_label(5) = "Cd_thickness"
        det_dim_label(6) = "Tube_ID"
        det_dim_label(7) = "Tube_length"
        det_dim_label(8) = "num_rows"
        det_dim_label(9) = "Radius_quantity"
        det_dim_label(10) = "Row1"
        det_dim_label(11) = "Row2"
        det_dim_label(12) = "Row3"
        det_dim_label(13) = "Row4"
        det_dim_label(14) = "Row5"
        det_dim_label(15) = "Stand_height"


        klineout = "Counter , " & counter_id & " , " & vbCrLf

        Dim fs As FileStream = File.Create(current_det_file_name)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        klineout = "file_ID , " & current_det_file_name & " , " & vbCrLf
        My.Computer.FileSystem.WriteAllText(current_det_file_name, klineout, True)

        For i = 1 To 15
            If i > 9 And i < 15 Then tube_num = tube_numbers(i - 9) Else tube_num = ""
            klineout = det_dim_label(i) & ", " & det_dim_val(i) & ", " & tube_num & vbCrLf
            My.Computer.FileSystem.WriteAllText(current_det_file_name, klineout, True)

        Next i

        Close()

    End Sub



    Sub load_det_to_screen()


        ' ***** Load data file contents onto screen *****
        Counter_Name_Box.Text = counter_id

        Counter_file_name_Box.Text = current_det_file_name
        counter_geo_box.Text = det_dim_val(1)
        cavity_height_Box1.Text = det_dim_val(2)
        Cavity_ID_Box.Text = det_dim_val(3)
        Cavity_Length_Box.Text = det_dim_val(4)
        Cd_liner_Box.Text = det_dim_val(5)
        Tube_ID_Box.Text = det_dim_val(6)
        Tube_Length_Box.Text = det_dim_val(7)
        Number_rows_Box.Text = det_dim_val(8)
        Ring_1_radius_Box.Text = det_dim_val(10)
        Ring_2_radius_Box.Text = det_dim_val(11)
        Ring_3_radius_Box.Text = det_dim_val(12)
        Ring_4_radius_Box.Text = det_dim_val(13)
        Ring_5_radius_Box.Text = det_dim_val(14)
        Item_Stand_Box.Text = det_dim_val(15)
        Ring_1_tubes_Box.Text = tube_numbers(1)
        Ring_2_tubes_Box.Text = tube_numbers(2)
        Ring_3_tubes_Box.Text = tube_numbers(3)
        Ring_4_tubes_Box.Text = tube_numbers(4)
        Ring_5_tubes_Box.Text = tube_numbers(5)

    End Sub

    Private Sub SaveFileDialog2_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog2.FileOk

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        '  Save as current item in new file

        Dim det_dimfile_name, det_dim_label(17) As String
        Dim outconstants, tube_num As String
        Dim klineout As String
        Dim iso_label(25)

        det_dimfile_name = "c:\multiplicity_tmu\detector_dimensions\current_det_dimensions.csv"

        counter_id = Counter_Name_Box.Text
        current_det_file_name = det_dimfile_name
        det_dim_val(1) = counter_geo_box.Text
        det_dim_val(2) = cavity_height_Box1.Text
        det_dim_val(3) = Cavity_ID_Box.Text
        det_dim_val(4) = Cavity_Length_Box.Text
        det_dim_val(5) = Cd_liner_Box.Text
        det_dim_val(6) = Tube_ID_Box.Text
        det_dim_val(7) = Tube_Length_Box.Text
        det_dim_val(8) = Number_rows_Box.Text
        det_dim_val(9) = 0
        det_dim_val(10) = Ring_1_radius_Box.Text
        det_dim_val(11) = Ring_2_radius_Box.Text
        det_dim_val(12) = Ring_3_radius_Box.Text
        det_dim_val(13) = Ring_4_radius_Box.Text
        det_dim_val(14) = Ring_5_radius_Box.Text
        det_dim_val(15) = Item_Stand_Box.Text
        tube_numbers(1) = Ring_1_tubes_Box.Text
        tube_numbers(2) = Ring_2_tubes_Box.Text
        tube_numbers(3) = Ring_3_tubes_Box.Text
        tube_numbers(4) = Ring_4_tubes_Box.Text
        tube_numbers(5) = Ring_5_tubes_Box.Text


        '   save revised parameters to current detector dimensions file



        det_dim_label(1) = "cavity_type"
        det_dim_label(2) = "Cav_height"
        det_dim_label(3) = "Cav_width"
        det_dim_label(4) = "Cav_length"
        det_dim_label(5) = "Cd_thickness"
        det_dim_label(6) = "Tube_ID"
        det_dim_label(7) = "Tube_length"
        det_dim_label(8) = "num_rows"
        det_dim_label(9) = "Radius_quantity"
        det_dim_label(10) = "Row1"
        det_dim_label(11) = "Row2"
        det_dim_label(12) = "Row3"
        det_dim_label(13) = "Row4"
        det_dim_label(14) = "Row5"
        det_dim_label(15) = "Stand_height"


        SaveFileDialog1.Filter = "CSV Files (*.csv*)|*.csv"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
       Then
            My.Computer.FileSystem.WriteAllText _
         (SaveFileDialog1.FileName, "", True)


            det_dimfile_name = SaveFileDialog1.FileName

            klineout = "Counter , " & counter_id & " , " & vbCrLf

            Dim fs As FileStream = File.Create(current_det_file_name)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()

            klineout = "file_ID , " & current_det_file_name & " , " & vbCrLf
            My.Computer.FileSystem.WriteAllText(current_det_file_name, klineout, True)

            For i = 1 To 15
                If i > 9 And i < 15 Then tube_num = tube_numbers(i - 9) Else tube_num = ""
                klineout = det_dim_label(i) & ", " & det_dim_val(i) & ", " & tube_num & vbCrLf
                My.Computer.FileSystem.WriteAllText(current_det_file_name, klineout, True)

            Next i

        End If
    End Sub

End Class