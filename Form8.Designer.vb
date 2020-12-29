<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form8
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form8))
        Me.filters_n_sigma_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.filters_acc_sigma_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.filters_min_cycles_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.jitter_iteration_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TriplesCheckBox = New System.Windows.Forms.CheckBox()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.random_pos_box = New System.Windows.Forms.MaskedTextBox()
        Me.SuspendLayout()
        '
        'filters_n_sigma_Box
        '
        Me.HelpProvider1.SetHelpString(Me.filters_n_sigma_Box, "Number of sigma used in statistical filters to reject outlier rates events")
        Me.filters_n_sigma_Box.Location = New System.Drawing.Point(171, 62)
        Me.filters_n_sigma_Box.Name = "filters_n_sigma_Box"
        Me.HelpProvider1.SetShowHelp(Me.filters_n_sigma_Box, True)
        Me.filters_n_sigma_Box.Size = New System.Drawing.Size(100, 20)
        Me.filters_n_sigma_Box.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(66, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Filters n sigma"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(66, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Accidentals n sigma"
        '
        'filters_acc_sigma_Box
        '
        Me.HelpProvider1.SetHelpString(Me.filters_acc_sigma_Box, "Number of sigma deviations difference between T^G  and Accidentals rate to reject" &
        " a cycle.")
        Me.filters_acc_sigma_Box.Location = New System.Drawing.Point(171, 92)
        Me.filters_acc_sigma_Box.Name = "filters_acc_sigma_Box"
        Me.HelpProvider1.SetShowHelp(Me.filters_acc_sigma_Box, True)
        Me.filters_acc_sigma_Box.Size = New System.Drawing.Size(100, 20)
        Me.filters_acc_sigma_Box.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(66, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Minimum cycles"
        '
        'filters_min_cycles_Box
        '
        Me.HelpProvider1.SetHelpString(Me.filters_min_cycles_Box, "MInimum number of cycles required to implement statistical filters.")
        Me.filters_min_cycles_Box.Location = New System.Drawing.Point(171, 121)
        Me.filters_min_cycles_Box.Name = "filters_min_cycles_Box"
        Me.HelpProvider1.SetShowHelp(Me.filters_min_cycles_Box, True)
        Me.filters_min_cycles_Box.Size = New System.Drawing.Size(100, 20)
        Me.filters_min_cycles_Box.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(338, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Iterations"
        Me.ToolTip1.SetToolTip(Me.Label4, "Number of iterations to be used in dithering cacluations (max =9999)")
        '
        'jitter_iteration_Box
        '
        Me.HelpProvider1.SetHelpString(Me.jitter_iteration_Box, "Number of iterations used for determination of rates covariances from only the su" &
        "mmed histogram.")
        Me.jitter_iteration_Box.Location = New System.Drawing.Point(409, 62)
        Me.jitter_iteration_Box.Name = "jitter_iteration_Box"
        Me.HelpProvider1.SetShowHelp(Me.jitter_iteration_Box, True)
        Me.jitter_iteration_Box.Size = New System.Drawing.Size(100, 20)
        Me.jitter_iteration_Box.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(338, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(131, 18)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Sum Historgram"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(66, 29)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(129, 18)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Statisical Filters"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(83, 246)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Save Changes"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TriplesCheckBox
        '
        Me.TriplesCheckBox.AutoSize = True
        Me.TriplesCheckBox.Location = New System.Drawing.Point(115, 159)
        Me.TriplesCheckBox.Name = "TriplesCheckBox"
        Me.TriplesCheckBox.Size = New System.Drawing.Size(104, 17)
        Me.TriplesCheckBox.TabIndex = 11
        Me.TriplesCheckBox.Text = "Use Triples Filter"
        Me.TriplesCheckBox.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(338, 141)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(225, 18)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Random Container Positions"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(338, 179)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Positions"
        Me.ToolTip1.SetToolTip(Me.Label8, "Number of iterations to be used in dithering cacluations (max =9999)")
        '
        'random_pos_box
        '
        Me.HelpProvider1.SetHelpString(Me.random_pos_box, "Number of iterations used for determination of rates covariances from only the su" &
        "mmed histogram.")
        Me.random_pos_box.Location = New System.Drawing.Point(409, 176)
        Me.random_pos_box.Name = "random_pos_box"
        Me.HelpProvider1.SetShowHelp(Me.random_pos_box, True)
        Me.random_pos_box.Size = New System.Drawing.Size(100, 20)
        Me.random_pos_box.TabIndex = 13
        '
        'Form8
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 324)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.random_pos_box)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TriplesCheckBox)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.jitter_iteration_Box)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.filters_min_cycles_Box)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.filters_acc_sigma_Box)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.filters_n_sigma_Box)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form8"
        Me.Text = "Miscellaneous Parameters"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents filters_n_sigma_Box As MaskedTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents filters_acc_sigma_Box As MaskedTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents filters_min_cycles_Box As MaskedTextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents jitter_iteration_Box As MaskedTextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents TriplesCheckBox As CheckBox
    Friend WithEvents HelpProvider1 As HelpProvider
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents random_pos_box As MaskedTextBox
End Class
