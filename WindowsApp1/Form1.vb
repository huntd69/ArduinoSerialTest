Imports System.Threading

Imports System.IO

Imports System.Text
'Imports System.String


Imports System.IO.Ports




Public Class Form1


    Public Shared buffer As String = ""


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim returnStr As String = "Serial Port Opened"


        TextBox1.Text = ""


        Try
            SerialPort1.Open()
        Catch ex As Exception
            returnStr = "Error Opening Serial Port"
        End Try



        Timer1.Start()


        TextBox1.Text = returnStr & vbCrLf




    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' This is the old code
        Dim returnStr As String = ""
        Try
            returnStr = SerialPort1.ReadExisting
        Catch ex As Exception
            'returnStr = "ERROR"
        End Try
        If returnStr IsNot "" Then
            readBuffer(returnStr)
        End If




    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim returnStr As String = "Serial Port Closed"

        Timer1.Stop()
        Try
            SerialPort1.Close()
        Catch ex As Exception
            returnStr = "Error Closing Serial Port"
        End Try
        TextBox1.Text = TextBox1.Text & vbCrLf & returnStr



    End Sub





    Private Sub readBuffer(newString)

        For counter As Int32 = 1 To Strings.Len(newString)
            If Strings.Left(newString, 1) = vbLf Then
                processBuffer()
            Else
                buffer = buffer & Strings.Left(newString, 1)
                newString = Strings.Right(newString, (Strings.Len(newString) - 1))
            End If

        Next counter

    End Sub

    Private Sub processBuffer()
        'TextBox1.Text = TextBox1.Text & buffer & vbCrLf
        TextBox1.AppendText(buffer & vbCrLf)
        updateProgressBar(buffer)
        buffer = ""

    End Sub

    Private Sub updateProgressBar(incomingMessage As String)
        If Strings.Left(incomingMessage, 2) = "A0" Then
            Try
                'ProgressBar1.Value = Strings.Mid(incomingMessage, 3)
                If Strings.Mid(incomingMessage, 3) > 500 Then
                    PictureBox1.BackColor = Color.Green
                Else
                    PictureBox1.BackColor = Color.Red
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

End Class
