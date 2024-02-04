Imports System.Drawing.Drawing2D
Enum MouseState As Byte
    None = 0
    Over = 1
    Down = 2
    Block = 3
End Enum
Public Class LogicButton : Inherits Control

#Region " Control Help - MouseState & Flicker Control"

   

    
 
   

    Private State As MouseState = MouseState.None
    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over
        Invalidate()
    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
    End Sub
    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub
#End Region

    Sub New()
        MyBase.New()
        BackColor = Color.FromArgb(38, 38, 38)
        Font = New Font("Times New Roman", 8, FontStyle.Bold)
        DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        MyBase.OnPaint(e)

        G.Clear(Color.FromArgb(36, 34, 30))

        Select Case State
            Case MouseState.None 'Mouse None
                G.DrawLine(New Pen(New SolidBrush(Color.Black)), 2, Height - 1, Width - 4, Height - 1)
                Dim backGrad As New LinearGradientBrush(New Rectangle(1, 1, Width - 1, Height - 2), Color.Black, Color.Black, 90S)
                G.FillRectangle(backGrad, New Rectangle(1, 1, Width - 1, Height - 2))
                G.DrawRectangle(New Pen(New SolidBrush(Color.Black)), New Rectangle(1, 1, Width - 3, Height - 4))
                G.DrawString(Text, New Font("Times new roman", 9, FontStyle.Regular), Brushes.White, New Rectangle(0, -2, Width - 1, Height - 1), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})

            Case MouseState.Over 'Mouse Hover
                G.DrawLine(New Pen(New SolidBrush(Color.Black)), 2, Height - 1, Width - 4, Height - 1)
                Dim backGrad As New LinearGradientBrush(New Rectangle(1, 1, Width - 1, Height - 2), Color.Black, Color.WhiteSmoke, 5S)
                G.FillRectangle(backGrad, New Rectangle(1, 1, Width - 1, Height - 2))
                G.DrawRectangle(New Pen(New SolidBrush(Color.Black)), New Rectangle(1, 1, Width - 3, Height - 4))
                G.DrawString(Text, New Font("Times new roman", 9, FontStyle.Regular), Brushes.White, New Rectangle(0, -2, Width - 1, Height - 1), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})

            Case MouseState.Down 'Mouse Down
                G.DrawLine(New Pen(New SolidBrush(Color.Black)), 2, Height - 1, Width - 4, Height - 1)
                Dim backGrad As New LinearGradientBrush(New Rectangle(1, 1, Width - 1, Height - 2), Color.Black, Color.WhiteSmoke, 50S)
                G.FillRectangle(backGrad, New Rectangle(1, 1, Width - 1, Height - 2))
                G.DrawRectangle(New Pen(New SolidBrush(Color.DarkBlue)), New Rectangle(1, 1, Width - 3, Height - 4))
                G.DrawString(Text, New Font("Times new roman", 9, FontStyle.Regular), Brushes.White, New Rectangle(0, -2, Width - 1, Height - 1), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})

        End Select

        Dim glossGradient As New LinearGradientBrush(New Rectangle(1, 1, Width - 2, Height / 2 - 2), Color.FromArgb(80, Color.White), Color.FromArgb(50, Color.White), 5S)
        G.FillRectangle(glossGradient, New Rectangle(1, 1, Width - 2, Height / 2 - 2))
        G.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(21, 20, 18))), New Rectangle(0, 0, Width - 1, Height - 2))

        

    End Sub
End Class


Class PasswordLogin : Inherits Control
    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
                 ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Dock = DockStyle.Top
        BackColor = Color.FromArgb(56, 54, 53)

    End Sub
    Private mouseX As Integer
    Private mouseY As Integer
    Private IsDown As Boolean = False
    ReadOnly Property CheckedRectangle As Integer
        Get
            Return MyPoints.Count
        End Get
    End Property

    Sub ClearPassword()
        patternPoints.Clear()
        MyPoints.Clear()
        Me.Invalidate()
    End Sub

    Private MyLocation As New Point

    Sub deletelocation()
        If MyPoints.Count = 0 Then Return
        For Each Loca_ As Point In MyPoints
            If MyLocation.X - Loca_.X <= 50 AndAlso MyLocation.Y - Loca_.Y <= 50 Then
                MyPoints.Remove(Loca_)
                Exit For
            End If
        Next
    End Sub
    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)
        If e.Button = MouseButtons.Left Then
            IsDown = True
            MyLocation = e.Location
        Else
            MyLocation = e.Location
            deletelocation()
        End If
        Me.Invalidate()
    End Sub

   
    Private Success_ As Boolean = False
    Property Success As Boolean
        Get
            Return Success_
        End Get
        Set(value As Boolean)
            Success_ = value
            Invalidate()
        End Set
    End Property


    Private patternPoints As New List(Of Point)
    Private MyPoints As New List(Of Point)

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics

        With G
            .SmoothingMode = SmoothingMode.HighQuality
            .Clear(BackColor)


            .FillRectangle(New SolidBrush(Parent.BackColor), ClientRectangle)

        End With
        patternPoints.Clear()
        Dim size As Integer = 50


        For row As Integer = 0 To Height / size - 1
            For col As Integer = 0 To Width / size - 1

                Dim color_ As Color
                If (row + col) Mod 2 = 0 Then
                    color_ = Color.Black
                Else
                    color_ = Color.White
                End If


                Dim x As Integer = col * size
                Dim y As Integer = row * size
                e.Graphics.FillRectangle(New SolidBrush(color_), x, y, size, size)
                e.Graphics.DrawRectangle(Pens.White, x, y, size, size)
                patternPoints.Add(New Point(x, y))
            Next

        Next

        Dim kp As New Point

        If MyLocation.X > 0 AndAlso IsDown = True Then

            For i = 0 To patternPoints.Count - 1

                If MyLocation.X - patternPoints(i).X <= 0 Then
                    Try
                        kp.X = patternPoints(i - 1).X
                    Catch ex As Exception

                    End Try

                    Exit For
                ElseIf MyLocation.X > patternPoints(patternPoints.Count - 1).X Then
                    kp.X = patternPoints(patternPoints.Count - 1).X

                    Exit For
                End If

            Next

            For i = 0 To patternPoints.Count - 1

                If MyLocation.Y - patternPoints(i).Y <= 0 Then
                    Try
                        kp.Y = patternPoints(i - 1).Y
                    Catch ex As Exception

                    End Try

                    Exit For
                ElseIf MyLocation.Y > patternPoints(patternPoints.Count - 1).Y Then
                    kp.Y = patternPoints(patternPoints.Count - 1).Y

                    Exit For
                End If

            Next

            IsDown = False
            MyPoints.Add(kp)
            If MyPoints.GroupBy(Function(x) x).Any(Function(c) c.Count() > 1) Then
                MyPoints.Remove(kp)
            End If

        End If
        For i = 0 To MyPoints.Count - 1
            If patternPoints.Contains(MyPoints(i)) Then
                Dim Rec_ As New Rectangle(MyPoints(i).X, MyPoints(i).Y, size, size)
                e.Graphics.FillRectangle(New SolidBrush(Color.Red), Rec_)
                e.Graphics.DrawRectangle(Pens.Black, Rec_)

                'Dim bmp_ As New Bitmap("\icon\0.png", False)
                'bmp_ = ResizeImage(bmp_, New Size(45, 45))
                'e.Graphics.DrawImage(bmp_, New Point(Rec_.Location.X + 4, Rec_.Location.Y + 4))
                Dim Location As New Point(Rec_.Location.X + 18, Rec_.Location.Y + 16)
                Select Case i
                    Case Is < 10
                        DrawDigit(e.Graphics, i, Location)
                    Case Is > 9
                        For ic = 0 To 1
                            Location = New Point(Location.X + (8 * ic), Location.Y)
                            DrawDigit(e.Graphics, i.ToString.Substring(ic, 1), Location)
                        Next
                End Select

            End If
        Next


        MyBase.OnPaint(e)


    End Sub
    Public Shared Function ResizeImage(ByVal InputBitmap As Bitmap, picsize As Size) As Bitmap
        Return New Bitmap(InputBitmap, New Size(picsize.Width, picsize.Height))
    End Function
    Private Sub DrawDigit(g As Graphics, theDigitA As String, ByVal Location_ As Point)

        With g
            Dim Xp As Integer = Location_.X
            Dim Yp As Integer = Location_.Y
            Using p As New Pen(Color.Black, 1.6)
                p.StartCap = Drawing2D.LineCap.Triangle
                p.EndCap = Drawing2D.LineCap.Triangle
                p.Alignment = PenAlignment.Center
                If Not IsNumeric(theDigitA) Then Return

                If "045689".Contains(theDigitA) Then .DrawLine(p, New PointF(Xp + 1, Yp + 2), New PointF(Xp + 1, Yp + 5))
                If "0268 ".Contains(theDigitA) Then .DrawLine(p, New PointF(Xp + 1, Yp + 7), New PointF(Xp + 1, Yp + 10))
                If "01234789".Contains(theDigitA) Then .DrawLine(p, New PointF(Xp + 6, Yp + 2), New PointF(Xp + 6, Yp + 5))
                If "013456789".Contains(theDigitA) Then .DrawLine(p, New PointF(Xp + 6, Yp + 7), New PointF(Xp + 6, Yp + 10))
                If "02356789".Contains(theDigitA) Then .DrawLine(p, New PointF(Xp + 2, Yp + 1), New PointF(Xp + 5, Yp + 1))
                If "2345689".Contains(theDigitA) Then .DrawLine(p, New PointF(Xp + 2, Yp + 6), New PointF(Xp + 5, Yp + 6))
                If "023568".Contains(theDigitA) Then .DrawLine(p, New PointF(Xp + 2, Yp + 11), New PointF(Xp + 5, Yp + 11))

            End Using
        End With
    End Sub
End Class


