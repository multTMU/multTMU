Public Class Form16
    Private Sub Form16_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        '   Called from Calibration parameter entry screen, Form 3
        '

        Dim dual_energy_cal_table As New DataTable
        Dim bin_num(100)
        Dim dual_label(5)



        Dim table_flag_3, table_flag_4 As Boolean
        inner_ring_eff_box.Text = inner_ring_eff_240
        outer_ring_eff_box.Text = outer_ring_eff_240

        dual_label(0) = "index"
        dual_label(1) = "Neutron Energy (MeV)"
        dual_label(2) = "Detector Efficiency (fraction)"
        dual_label(3) = "Inner/Outer Ring Ratio"
        dual_label(4) = "Relative Fission Fraction"


        table_flag_3 = False
        table_flag_4 = True

        '  Create table for calibration table

        '      bin_num(0) = "index"
        '      For i = 1 To 100
        '           bin_num(i) = "(" & i & ")"
        '       Next

        If table_flag_3 Then GoTo 15
        table_flag_3 = True
        With dual_energy_cal_table
            For i = 0 To 4
                .Columns.Add(dual_label(i), System.Type.GetType("System.Double"))
            Next i

        End With

15:     If table_flag_4 Then dual_energy_cal_table.Rows.Clear()
        table_flag_4 = True
        For k = 1 To 100
            Dim newrow As DataRow = dual_energy_cal_table.NewRow
            newrow(dual_label(0)) = k
            newrow(dual_label(1)) = neut_energy(k)
            newrow(dual_label(2)) = det_eff_E(k)
            newrow(dual_label(3)) = inner_outer(k)
            newrow(dual_label(4)) = rel_fiss_prob(k)

            dual_energy_cal_table.Rows.Add(newrow)
        Next k

        Grid2.DataSource = dual_energy_cal_table




    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        inner_ring_eff_240 = inner_ring_eff_box.Text
        outer_ring_eff_240 = outer_ring_eff_box.Text

        ' format is columns then rows

        For i = 1 To 100
            neut_energy(i) = Grid2(1, i - 1).Value
            det_eff_E(i) = Grid2(2, i - 1).Value
            inner_outer(100) = Grid2(3, i - 1).Value
            rel_fiss_prob(100) = Grid2(4, i - 1).Value
        Next i

        Close()

    End Sub
End Class