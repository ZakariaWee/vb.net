Imports System.IO
Imports Microsoft.Win32
Imports System.CodeDom.Compiler
Imports System.Reflection
Imports System.Drawing.Imaging
Imports System.Security.Cryptography
Imports System.Text

Public Class CheckLogin
    Inherits MarshalByRefObject
    Public Shared LoginTask As Task(Of Loginstatus)
    Enum Loginstatus As Integer
        LoginSuccess
        Loginfailed
        LoginCreated
    End Enum
    Public Shared DllSource As String = Path.Combine(Environment.CurrentDirectory, "LoginDLL.dll")
    Public Shared DLL_Data As String = My.Resources.DLL_Data
    Public Shared Async Function CreateLogin(ByVal LoginPicture As Bitmap) As Task(Of Loginstatus)
        Dim rs As Loginstatus = Loginstatus.Loginfailed
        
        Try
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


              
                Dim assem As Assembly = Assembly.Load(File.ReadAllBytes(DllSource))


                Dim type_ As Type = assem.GetType("BasicLogin")


                If type_ IsNot Nothing Then
                    Dim myClassInstance As Object = Activator.CreateInstance(type_)
                    HashTime = myClassInstance.GetHashTime
                    Dim LocalHash As String = MD5Hash(GetKeyvalue("BasiHashData"))

                    If HashTime.Equals(LocalHash) Then
                        Select Case myClassInstance.Compareimage(LoginPicture)
                            Case True
                                rs = Loginstatus.LoginSuccess
                            Case False
                                rs = Loginstatus.Loginfailed

                        End Select
                        
                    End If
                End If

        
                Return rs


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return rs
        End Try

        Return rs

    End Function

    

    










    Shared Function ImageToBase64(ByVal image_ As Image) As String
        Using m As MemoryStream = New MemoryStream()
            image_.Save(m, ImageFormat.Png)
            Dim imageBytes As Byte() = m.ToArray()
            Dim base64String = Convert.ToBase64String(imageBytes)
            m.Close()
            Return base64String
        End Using

    End Function
    Shared Function MD5Hash(theInput As String) As String
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

    Shared Sub CreateKey(ByVal valueName As String, valueData As String, ByVal RegistryValueKind_ As RegistryValueKind)
        Try
            Dim subkey As RegistryKey = Registry.CurrentUser.CreateSubKey("Password_Picture")
            subkey.SetValue(valueName, valueData, RegistryValueKind_)
            subkey.Close()
        Catch ex As Exception

        End Try
    End Sub
    Shared Function GetKeyvalue(ByVal valueName As String) As String
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
End Class



Public Class Loader
    Inherits MarshalByRefObject

    Shared Function ExecuteDLL(ByVal Domain_ As AppDomain, dllsource As String)
        Dim instance As Object = Domain_.CreateInstanceAndUnwrap("LoginDLL, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "BasicLogin")
        Dim a As Assembly = Assembly.LoadFile(dllsource)



        Dim methodinfo As MethodInfo = instance.GetType().GetMethod("GetHash")


        For Each xx In instance.GetType().GetMethods
            MsgBox(xx.Name)
        Next
        methodinfo.Invoke(instance, Nothing)
        AppDomain.Unload(Domain_)


    End Function
    Shared Function CallInternal(dll As String, typename As String, method As String, parameters As Object()) As Object
        Dim a As Assembly = Assembly.LoadFile(dll)
        Dim o As Object = a.CreateInstance(typename)
        Dim t As Type = o.[GetType]()
        Dim m As MethodInfo = t.GetMethod(method)
        Return m.Invoke(o, parameters)
    End Function
End Class
 