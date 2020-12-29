<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form16
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form16))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.inner_ring_eff_box = New System.Windows.Forms.MaskedTextBox()
        Me.outer_ring_eff_box = New System.Windows.Forms.MaskedTextBox()
        Me.Grid2 = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.Grid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(68, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(77, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(157, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Inner Ring Detection Efficiency:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(77, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(184, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Outer Ring Ring Detection Efficiency:"
        '
        'inner_ring_eff_box
        '
        Me.inner_ring_eff_box.Location = New System.Drawing.Point(283, 29)
        Me.inner_ring_eff_box.Name = "inner_ring_eff_box"
        Me.inner_ring_eff_box.Size = New System.Drawing.Size(78, 20)
        Me.inner_ring_eff_box.TabIndex = 3
        '
        'outer_ring_eff_box
        '
        Me.outer_ring_eff_box.Location = New System.Drawing.Point(283, 54)
        Me.outer_ring_eff_box.Name = "outer_ring_eff_box"
        Me.outer_ring_eff_box.Size = New System.Drawing.Size(78, 20)
        Me.outer_ring_eff_box.TabIndex = 4
        '
        'Grid2
        '
        Me.Grid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid2.Location = New System.Drawing.Point(84, 108)
        Me.Grid2.Name = "Grid2"
        Me.Grid2.Size = New System.Drawing.Size(596, 448)
        Me.Grid2.TabIndex = 11
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(561, 29)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 36)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Update and Return"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form16
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 582)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Grid2)
        Me.Controls.Add(Me.outer_ring_eff_box)
        Me.Controls.Add(Me.inner_ring_eff_box)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form16"
        Me.Text = "Dual Enegy Multiplicity Parameter Entry"
        CType(Me.Grid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents inner_ring_eff_box As MaskedTextBox
    Friend WithEvents outer_ring_eff_box As MaskedTextBox
    Friend WithEvents Grid2 As DataGridView
    Friend WithEvents Button1 As Button
End Class
