Imports System.Security.Cryptography
Imports System.Text
Imports System.CodeDom.Compiler
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Reflection
Imports Microsoft.Win32

Module Login_with_picture

    Public LoginTask As Task(Of Loginstatus)
    Enum Loginstatus As Integer
        LoginSuccess
        Loginfailed
        LoginCreated
    End Enum
    Public DllSource As String = Path.Combine(Environment.CurrentDirectory, "LoginDLL.dll")
    Private DLL_Data As String = My.Resources.DLL_Data
    Public Async Function CreateLogin(ByVal LoginPicture As Bitmap) As Task(Of Loginstatus)
    
        Dim HashTime As String = MD5Hash(DateTime.Now.ToString("F"))
        Dim PictureData As String = ImageToBase64(LoginPicture)
        If File.Exists(DllSource) = False Then
            DLL_Data = DLL_Data.Replace("#PictureData#", PictureData)
            DLL_Data = DLL_Data.Replace("#HashTime#", HashTime)
            Dim codeProvider As New VBCodeProvider()
            Dim compilerParams As New CompilerParameters()
            compilerParams.GenerateExecutable = True
            compilerParams.GenerateInMemory = False
            compilerParams.OutputAssembly = DllSource
            compilerParams.ReferencedAssemblies.Add("System.Windows.Forms.dll")
            compilerParams.ReferencedAssemblies.Add("System.Drawing.dll")
            compilerParams.CompilerOptions = "/t:library"
            Dim results As CompilerResults = Nothing
            Await task.Run(Sub()
                               results = codeProvider.CompileAssemblyFromSource(compilerParams, DLL_Data)
                           End Sub)
            If IsNothing(results) Then Return Loginstatus.Loginfailed
            If results.Errors.Count > 0 Then
                Return Loginstatus.Loginfailed
            Else
                CreateKey("BasiHashData", HashTime, Microsoft.Win32.RegistryValueKind.String)
                Return Loginstatus.LoginCreated
            End If
          
        Else
 
            Dim assembly As Assembly = assembly.LoadFile(DllSource)     
            Dim myClassType As Type = assembly.GetType("BasicLogin")
            If myClassType IsNot Nothing Then
                Dim myClassInstance As Object = Activator.CreateInstance(myClassType)
                If myClassInstance IsNot Nothing Then
                    HashTime = myClassInstance.GetHashTime
                    Dim LocalHash As String = MD5Hash(GetKeyvalue("BasiHashData"))
                    If HashTime.Equals(LocalHash) Then

                        Dim result As Boolean = myClassInstance.Compareimage(LoginPicture)

                        If result = False Then
                            Return Loginstatus.Loginfailed
                        End If
                        Return Loginstatus.LoginSuccess
                    Else
                        Return Loginstatus.Loginfailed
                    End If
                End If
            Else
                Return Loginstatus.Loginfailed
            End If
        End If
        Try
        Catch ex As Exception
            Return Loginstatus.Loginfailed
        End Try
        Return Loginstatus.Loginfailed

    End Function
    Public Function Newpassword() As Boolean
        Try
            If File.Exists(DllSource) Then File.Delete(DllSource)
            Removekey()
            Return True
        Catch ex As Exception
            Return False
        End Try
        
    End Function
    Private Function ImageToBase64(ByVal image_ As Image) As String
        Using m As MemoryStream = New MemoryStream()
            image_.Save(m, ImageFormat.Png)
            Dim imageBytes As Byte() = m.ToArray()
            Dim base64String = Convert.ToBase64String(imageBytes)
            m.Close()
            Return base64String
        End Using

    End Function
    Private Function MD5Hash(theInput As String) As String
        If IsNothing(theInput) Then Return Nothing
        Using hasher As MD5 = MD5.Create()
            Dim dbytes As Byte() =
                 hasher.ComputeHash(Encoding.UTF8.GetBytes(theInput))
            Dim sBuilder As New StringBuilder()
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("X2"))
            Next n
            Return sBuilder.ToString()
        End Using

    End Function

    Sub CreateKey(ByVal valueName As String, valueData As String, ByVal RegistryValueKind_ As RegistryValueKind)
        Try
            Dim subkey As RegistryKey = Registry.CurrentUser.CreateSubKey("Password_Picture")
            subkey.SetValue(valueName, valueData, RegistryValueKind_)
            subkey.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Function GetKeyvalue(ByVal valueName As String) As String
        Dim subkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Password_Picture")
        If IsNothing(subkey) Then Return Nothing
        Dim maildata As String = subkey.GetValue(valueName)
        subkey.Close()
        Return maildata
    End Function
    Sub Removekey()
        Try
            Registry.CurrentUser.DeleteSubKey("Password_Picture")       
        Catch ex As Exception
        End Try
    End Sub
End Module
