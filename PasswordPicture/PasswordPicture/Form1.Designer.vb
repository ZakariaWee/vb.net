<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.PwdPictureLogin = New PasswordPicture.PasswordLogin()
        Me.BtnClear = New PasswordPicture.LogicButton()
        Me.BtnLogin = New PasswordPicture.LogicButton()
        Me.BtnNewPwd = New PasswordPicture.LogicButton()
        Me.SuspendLayout()
        '
        'PwdPictureLogin
        '
        Me.PwdPictureLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.PwdPictureLogin.Dock = System.Windows.Forms.DockStyle.Top
        Me.PwdPictureLogin.Location = New System.Drawing.Point(0, 0)
        Me.PwdPictureLogin.Name = "PwdPictureLogin"
        Me.PwdPictureLogin.Size = New System.Drawing.Size(348, 156)
        Me.PwdPictureLogin.Success = False
        Me.PwdPictureLogin.TabIndex = 3
        Me.PwdPictureLogin.Text = "PasswordLogin1"
        '
        'BtnClear
        '
        Me.BtnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.BtnClear.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold)
        Me.BtnClear.Location = New System.Drawing.Point(128, 162)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(82, 31)
        Me.BtnClear.TabIndex = 2
        Me.BtnClear.Text = "Clear"
        '
        'BtnLogin
        '
        Me.BtnLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.BtnLogin.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold)
        Me.BtnLogin.Location = New System.Drawing.Point(3, 162)
        Me.BtnLogin.Name = "BtnLogin"
        Me.BtnLogin.Size = New System.Drawing.Size(82, 31)
        Me.BtnLogin.TabIndex = 1
        Me.BtnLogin.Text = "Log-in"
        '
        'BtnNewPwd
        '
        Me.BtnNewPwd.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.BtnNewPwd.Enabled = False
        Me.BtnNewPwd.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold)
        Me.BtnNewPwd.Location = New System.Drawing.Point(245, 162)
        Me.BtnNewPwd.Name = "BtnNewPwd"
        Me.BtnNewPwd.Size = New System.Drawing.Size(91, 31)
        Me.BtnNewPwd.TabIndex = 4
        Me.BtnNewPwd.Text = "New Password"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(348, 205)
        Me.Controls.Add(Me.BtnNewPwd)
        Me.Controls.Add(Me.PwdPictureLogin)
        Me.Controls.Add(Me.BtnClear)
        Me.Controls.Add(Me.BtnLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login using picture"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnLogin As PasswordPicture.LogicButton
    Friend WithEvents BtnClear As PasswordPicture.LogicButton
    Friend WithEvents PwdPictureLogin As PasswordPicture.PasswordLogin
    Friend WithEvents BtnNewPwd As PasswordPicture.LogicButton

End Class
