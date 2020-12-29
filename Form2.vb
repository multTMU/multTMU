Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Fission_Data_Form


    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub



    Private Sub Fission_Data_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fiss_const_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file

        fiss_const_file_name = "c:\multiplicity_tmu\fission_parameters\current_fisison_parameters.csv"

        Call get_fiss_const_file(fiss_const_file_name)
        Call load_fiss_constants_to_screen(fiss_const_file_name)


    End Sub
    Public Property FileName As String
    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fiss_const_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\multiplicity_tmu\fission_parameters\"
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
        Call load_fiss_constants_to_screen(fiss_const_file_name)


    End Sub


    Sub load_fiss_constants_to_screen(fiss_const_file_name)
        ' ***** Load data file contents onto screen *****
        constantsfilenamebox1.Text = fiss_const_file_name

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

    End Sub

    Public Property Fiss_const_val1() As String
        Get
            Return Me.FissionTextBox1.Text
        End Get
        Set(ByVal Value As String)
            Me.FissionTextBox1.Text = Value

        End Set
    End Property

    Public Property Fiss_const_val2() As String
        Get
            Return Me.FissiontextBox2.Text
        End Get
        Set(ByVal Value As String)
            Me.FissiontextBox2.Text = Value

        End Set
    End Property
    Public Property Fiss_const_val3() As String
        Get
            Return Me.FissionTextBox3.Text
        End Get
        Set(ByVal Value As String)
            Me.FissionTextBox3.Text = Value

        End Set
    End Property

    Public Property Fiss_const_val4() As String
        Get
            Return Me.FissionTextBox4.Text
        End Get
        Set(ByVal Value As String)
            Me.FissionTextBox4.Text = Value

        End Set
    End Property

    Public Property Fiss_const_val5() As String
        Get
            Return Me.FissionTextBox5.Text
        End Get
        Set(ByVal Value As String)
            Me.FissionTextBox5.Text = Value

        End Set
    End Property

    Public Property Fiss_const_val6() As String
        Get
            Return Me.FissionTextBox6.Text
        End Get
        Set(ByVal Value As String)
            Me.FissionTextBox6.Text = Value

        End Set
    End Property

    Public Property Fiss_const_val7() As String
        Get
            Return Me.FissionTextBox7.Text
        End Get
        Set(ByVal Value As String)
            Me.FissionTextBox7.Text = Value
        End Set
    End Property




    Public Property Fiss_const_err1() As String
        Get
            Return Me.FissErrTextBox1.Text
        End Get
        Set(ByVal Value As String)
            Me.FissErrTextBox1.Text = Value

        End Set
    End Property

    Public Property Fiss_const_err2() As String
        Get
            Return Me.FissErrTextBox2.Text
        End Get
        Set(ByVal Value As String)
            Me.FissErrTextBox2.Text = Value

        End Set
    End Property
    Public Property Fiss_const_err3() As String
        Get
            Return Me.FissErrTextBox3.Text
        End Get
        Set(ByVal Value As String)
            Me.FissErrTextBox3.Text = Value

        End Set
    End Property

    Public Property Fiss_const_err4() As String
        Get
            Return Me.FissErrTextBox4.Text
        End Get
        Set(ByVal Value As String)
            Me.FissErrTextBox4.Text = Value

        End Set
    End Property

    Public Property Fiss_const_err5() As String
        Get
            Return Me.FissErrTextBox5.Text
        End Get
        Set(ByVal Value As String)
            Me.FissErrTextBox5.Text = Value

        End Set
    End Property

    Public Property Fiss_const_err6() As String
        Get
            Return Me.FissErrTextBox6.Text
        End Get
        Set(ByVal Value As String)
            Me.FissErrTextBox6.Text = Value

        End Set
    End Property

    Public Property Fiss_const_err7() As String
        Get
            Return Me.FissErrTextBox7.Text
        End Get
        Set(ByVal Value As String)
            Me.FissErrTextBox7.Text = Value
        End Set
    End Property

    Public Property Fiss_covar_11() As String
        Get
            Return Me.FissCovarBox11.Text
        End Get
        Set(ByVal Value As String)
            Me.FissCovarBox11.Text = Value

        End Set
    End Property

    Public Property Fiss_covar_12() As String
        Get
            Return Me.FissCovarBox12.Text
        End Get
        Set(ByVal Value As String)
            Me.FissCovarBox12.Text = Value

        End Set
    End Property
    Public Property Fiss_covar_13() As String
        Get
            Return Me.FissCovarBox13.Text
        End Get
        Set(ByVal Value As String)
            Me.FissCovarBox13.Text = Value

        End Set
    End Property

    Public Property Fiss_covar_14() As String
        Get
            Return Me.FissCovarBox14.Text
        End Get
        Set(ByVal Value As String)
            Me.FissCovarBox14.Text = Value

        End Set
    End Property

    Public Property Fiss_covar_15() As String
        Get
            Return Me.FissCovarBox15.Text
        End Get
        Set(ByVal Value As String)
            Me.FissCovarBox15.Text = Value

        End Set
    End Property

    Public Property fiss_covar_16() As String
        Get
            Return Me.FissCovarBox16.Text
        End Get
        Set(ByVal Value As String)
            Me.FissCovarBox16.Text = Value

        End Set
    End Property

    Public Property fiss_covar_17() As String
        Get
            Return Me.FissCovarBox17.Text
        End Get
        Set(ByVal Value As String)
            Me.FissCovarBox17.Text = Value
        End Set
    End Property

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim outconstants As String
        Dim klineout As String

        '   save revised parameters to default fission constant file

        fiss_const_val(1) = FissionTextBox1.Text
        fiss_const_val(2) = FissiontextBox2.Text
        fiss_const_val(3) = FissionTextBox3.Text
        fiss_const_val(4) = FissionTextBox4.Text
        fiss_const_val(5) = FissionTextBox5.Text
        fiss_const_val(6) = FissionTextBox6.Text
        fiss_const_val(7) = FissionTextBox7.Text

        fiss_const_err(1) = FissErrTextBox1.Text
        fiss_const_err(2) = FissErrTextBox2.Text
        fiss_const_err(3) = FissErrTextBox3.Text
        fiss_const_err(4) = FissErrTextBox4.Text
        fiss_const_err(5) = FissErrTextBox5.Text
        fiss_const_err(6) = FissErrTextBox6.Text
        fiss_const_err(7) = FissErrTextBox7.Text

        fiss_covar(1, 1) = FissCovarBox11.Text
        fiss_covar(1, 2) = FissCovarBox12.Text
        fiss_covar(1, 3) = FissCovarBox13.Text
        fiss_covar(1, 4) = FissCovarBox14.Text
        fiss_covar(1, 5) = FissCovarBox15.Text
        fiss_covar(1, 6) = FissCovarBox16.Text
        fiss_covar(1, 7) = FissCovarBox17.Text

        fiss_covar(2, 1) = FissCovarBox21.Text
        fiss_covar(2, 2) = FissCovarBox22.Text
        fiss_covar(2, 3) = FissCovarBox23.Text
        fiss_covar(2, 4) = FissCovarBox24.Text
        fiss_covar(2, 5) = FissCovarBox25.Text
        fiss_covar(2, 6) = FissCovarBox26.Text
        fiss_covar(2, 7) = FissCovarBox27.Text

        fiss_covar(3, 1) = FissCovarBox31.Text
        fiss_covar(3, 2) = FissCovarBox32.Text
        fiss_covar(3, 3) = FissCovarBox33.Text
        fiss_covar(3, 4) = FissCovarBox34.Text
        fiss_covar(3, 5) = FissCovarBox35.Text
        fiss_covar(3, 6) = FissCovarBox36.Text
        fiss_covar(3, 7) = FissCovarBox37.Text

        fiss_covar(4, 1) = FissCovarBox41.Text
        fiss_covar(4, 2) = FissCovarBox42.Text
        fiss_covar(4, 3) = FissCovarBox43.Text
        fiss_covar(4, 4) = FissCovarBox44.Text
        fiss_covar(4, 5) = FissCovarBox45.Text
        fiss_covar(4, 6) = FissCovarBox46.Text
        fiss_covar(4, 7) = FissCovarBox47.Text

        fiss_covar(5, 1) = FissCovarBox51.Text
        fiss_covar(5, 2) = FissCovarBox52.Text
        fiss_covar(5, 3) = FissCovarBox53.Text
        fiss_covar(5, 4) = FissCovarBox54.Text
        fiss_covar(5, 5) = FissCovarBox55.Text
        fiss_covar(5, 6) = FissCovarBox56.Text
        fiss_covar(5, 7) = FissCovarBox57.Text

        fiss_covar(6, 1) = FissCovarBox61.Text
        fiss_covar(6, 2) = FissCovarBox62.Text
        fiss_covar(6, 3) = FissCovarBox63.Text
        fiss_covar(6, 4) = FissCovarBox64.Text
        fiss_covar(6, 5) = FissCovarBox65.Text
        fiss_covar(6, 6) = FissCovarBox66.Text
        fiss_covar(6, 7) = FissCovarBox67.Text

        fiss_covar(7, 1) = FissCovarBox71.Text
        fiss_covar(7, 2) = FissCovarBox72.Text
        fiss_covar(7, 3) = FissCovarBox73.Text
        fiss_covar(7, 4) = FissCovarBox74.Text
        fiss_covar(7, 5) = FissCovarBox75.Text
        fiss_covar(7, 6) = FissCovarBox76.Text
        fiss_covar(7, 7) = FissCovarBox77.Text


        '

        outconstants = "c:\multiplicity_tmu\fission_parameters\current_fisison_parameters.csv"

        klineout = "Pu Fisison Constants , " & outconstants & " , " & vbCrLf

        Dim fs As FileStream = File.Create(outconstants)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        For i = 2 To 8
            klineout = fiss_const_val(i - 1) & ", " & fiss_const_err(i - 1) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            '    Console.Write(klineout)
        Next i

        For i = 9 To 15
            klineout = fiss_covar(i - 8, 1)
            For j = 2 To 7
                klineout = klineout & ", " & fiss_covar(i - 8, j)
            Next j
            klineout = klineout & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            '    Console.Write(klineout)
        Next i

        Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim outconstants As String
        Dim klineout As String

        '   save revised parameters to default fission constant file

        fiss_const_val(1) = FissionTextBox1.Text
        fiss_const_val(2) = FissiontextBox2.Text
        fiss_const_val(3) = FissionTextBox3.Text
        fiss_const_val(4) = FissionTextBox4.Text
        fiss_const_val(5) = FissionTextBox5.Text
        fiss_const_val(6) = FissionTextBox6.Text
        fiss_const_val(7) = FissionTextBox7.Text

        fiss_const_err(1) = FissErrTextBox1.Text
        fiss_const_err(2) = FissErrTextBox2.Text
        fiss_const_err(3) = FissErrTextBox3.Text
        fiss_const_err(4) = FissErrTextBox4.Text
        fiss_const_err(5) = FissErrTextBox5.Text
        fiss_const_err(6) = FissErrTextBox6.Text
        fiss_const_err(7) = FissErrTextBox7.Text

        fiss_covar(1, 1) = FissCovarBox11.Text
        fiss_covar(1, 2) = FissCovarBox12.Text
        fiss_covar(1, 3) = FissCovarBox13.Text
        fiss_covar(1, 4) = FissCovarBox14.Text
        fiss_covar(1, 5) = FissCovarBox15.Text
        fiss_covar(1, 6) = FissCovarBox16.Text
        fiss_covar(1, 7) = FissCovarBox17.Text

        fiss_covar(2, 1) = FissCovarBox21.Text
        fiss_covar(2, 2) = FissCovarBox22.Text
        fiss_covar(2, 3) = FissCovarBox23.Text
        fiss_covar(2, 4) = FissCovarBox24.Text
        fiss_covar(2, 5) = FissCovarBox25.Text
        fiss_covar(2, 6) = FissCovarBox26.Text
        fiss_covar(2, 7) = FissCovarBox27.Text

        fiss_covar(3, 1) = FissCovarBox31.Text
        fiss_covar(3, 2) = FissCovarBox32.Text
        fiss_covar(3, 3) = FissCovarBox33.Text
        fiss_covar(3, 4) = FissCovarBox34.Text
        fiss_covar(3, 5) = FissCovarBox35.Text
        fiss_covar(3, 6) = FissCovarBox36.Text
        fiss_covar(3, 7) = FissCovarBox37.Text

        fiss_covar(4, 1) = FissCovarBox41.Text
        fiss_covar(4, 2) = FissCovarBox42.Text
        fiss_covar(4, 3) = FissCovarBox43.Text
        fiss_covar(4, 4) = FissCovarBox44.Text
        fiss_covar(4, 5) = FissCovarBox45.Text
        fiss_covar(4, 6) = FissCovarBox46.Text
        fiss_covar(4, 7) = FissCovarBox47.Text

        fiss_covar(5, 1) = FissCovarBox51.Text
        fiss_covar(5, 2) = FissCovarBox52.Text
        fiss_covar(5, 3) = FissCovarBox53.Text
        fiss_covar(5, 4) = FissCovarBox54.Text
        fiss_covar(5, 5) = FissCovarBox55.Text
        fiss_covar(5, 6) = FissCovarBox56.Text
        fiss_covar(5, 7) = FissCovarBox57.Text

        fiss_covar(6, 1) = FissCovarBox61.Text
        fiss_covar(6, 2) = FissCovarBox62.Text
        fiss_covar(6, 3) = FissCovarBox63.Text
        fiss_covar(6, 4) = FissCovarBox64.Text
        fiss_covar(6, 5) = FissCovarBox65.Text
        fiss_covar(6, 6) = FissCovarBox66.Text
        fiss_covar(6, 7) = FissCovarBox67.Text

        fiss_covar(7, 1) = FissCovarBox71.Text
        fiss_covar(7, 2) = FissCovarBox72.Text
        fiss_covar(7, 3) = FissCovarBox73.Text
        fiss_covar(7, 4) = FissCovarBox74.Text
        fiss_covar(7, 5) = FissCovarBox75.Text
        fiss_covar(7, 6) = FissCovarBox76.Text
        fiss_covar(7, 7) = FissCovarBox77.Text
        '
        '
        outconstants = "c:\multiplicity_tmu\fission_parameters\Pu_fisison_parameters.csv"


        SaveFileDialog1.Filter = "CSV Files (*.csv*)|*.csv"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
       Then
            My.Computer.FileSystem.WriteAllText _
         (SaveFileDialog1.FileName, "", True)
            '
            '
            outconstants = SaveFileDialog1.FileName

            klineout = "Pu Fisison Constants , " & outconstants & " , " & vbCrLf

            Dim fs As FileStream = File.Create(outconstants)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()

            For i = 2 To 8
                klineout = fiss_const_val(i - 1) & ", " & fiss_const_err(i - 1) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

            For i = 9 To 15
                klineout = fiss_covar(i - 8, 1)
                For j = 2 To 7
                    klineout = klineout & ", " & fiss_covar(i - 8, j)
                Next j
                klineout = klineout & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

        End If

    End Sub
End Class