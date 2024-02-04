
Public Class Form1


    Private Async Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click

        If PwdPictureLogin.CheckedRectangle < 3 Then
            MessageBox.Show("At least 3 fields must be filled out!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim Ctrl As Control = PwdPictureLogin
        Dim PictureData As New Bitmap(Ctrl.Width, Ctrl.Height)
        Ctrl.DrawToBitmap(PictureData, New Rectangle(0, 0, Ctrl.Width, Ctrl.Height))


        Dim result As Loginstatus = Await CheckLogin.CreateLogin(PictureData)
        Select Case result
            Case Loginstatus.LoginCreated
                MessageBox.Show("Great!, The application will restart ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Application.Restart()
            Case Loginstatus.LoginSuccess
                MessageBox.Show("Your password is correct :) ", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)


            Case Loginstatus.Loginfailed

                MessageBox.Show("Your password is incorrect :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                PwdPictureLogin.ClearPassword()
        End Select

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IO.File.Exists(Login_with_picture.DllSource) = True Then
            BtnLogin.Text = "Log-In"
            BtnNewPwd.Enabled = True
        Else
            BtnLogin.Text = "Sign-Up"
        End If
    End Sub


    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        PwdPictureLogin.ClearPassword()
    End Sub



    Private Sub BtnNewPwd_Click(sender As Object, e As EventArgs) Handles BtnNewPwd.Click



      

        If Newpassword() = True Then
            PwdPictureLogin.ClearPassword()
            BtnLogin.Text = "Sign-Up"
        End If

    End Sub
End Class
