Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting
Public Class Form8
    Public misc_param(12)
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim misc_par_file As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file

        misc_par_file = "c:\multiplicity_tmu\misc_parameters\TMU_general_parameters.csv"

        Call get_misc_par_file(misc_par_file)



    End Sub

    Private Sub get_misc_par_file(misc_par_file)
        Dim myStream As Stream = Nothing


        Dim str11, str12, str13, str14 As String
        Dim idex, jdex As Integer
        Dim misc_par_file_name As String
        Dim sep_val1, sep_val2 As String
        Dim use_triples_flag As Boolean
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
                        ' 

                        If jdex = 2 And idex > 0 And idex < 12 Then temp_pars(idex) = Val(str11)

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

        ' ***** Load data file contents onto screen *****
        filters_n_sigma_Box.Text = misc_param(1)
        filters_acc_sigma_Box.Text = misc_param(2)
        filters_min_cycles_Box.Text = misc_param(3)
        jitter_iteration_Box.Text = misc_param(4)
        If misc_param(5) = 1 Then TriplesCheckBox.Checked = True
        If TriplesCheckBox.Checked = True Then use_triples_flag = True
        random_pos_box.Text = misc_param(6)


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '  Save as current item 
        Dim misc_par_file_name As String
        Dim outconstants As String
        Dim klineout As String
        Dim par_label(12)

        misc_par_file_name = "c:\multiplicity_tmu\misc_parameters\TMU_general_parameters.csv"

        '   save revised parameters to default file

        misc_param(1) = filters_n_sigma_Box.Text
        misc_param(2) = filters_acc_sigma_Box.Text
        misc_param(3) = filters_min_cycles_Box.Text
        misc_param(4) = jitter_iteration_Box.Text
        If TriplesCheckBox.Checked = True Then misc_param(5) = 1 Else misc_param(5) = 0
        misc_param(6) = random_pos_box.Text


        par_label(1) = "n_sigma_rates"
        par_label(2) = "n_sigma_accid"
        par_label(3) = "filter_min_cycles"
        par_label(4) = "jitter_iterations"
        par_label(5) = "Use_triples"
        par_label(6) = "rand_positions"
        par_label(7) = "blank"
        par_label(8) = "blank"
        par_label(9) = "blank"
        par_label(10) = "blank"

        'overwrite contstants file for fitting routine

        klineout = par_label(1) & ", " & misc_param(1) & " , " & vbCrLf

        Dim fs As FileStream = File.Create(misc_par_file_name)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        For i = 2 To 10
            klineout = par_label(i) & ", " & misc_param(i) & " , " & vbCrLf
            My.Computer.FileSystem.WriteAllText(misc_par_file_name, klineout, True)

        Next i


        Close()
    End Sub
End Class