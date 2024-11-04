Imports System.Data.SqlClient

Public Class Form_Login

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Silahkan isi Username dan Passoword anda !")
        Else
            Call Koneksi()
            cmd = New SqlCommand("SELECT * FROM TB_Login where username = '" & TextBox1.Text & "' AND password = '" & TextBox2.Text & "'", Conn)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Username atau Password anda Salah !")
            Else
                Me.Close()
                Call Terbuka()
            End If
        End If
    End Sub
    Sub Terbuka()
        Form_Menu_Utama.MasukToolStripMenuItem.Visible = False
        Form_Menu_Utama.LogoutToolStripMenuItem.Visible = True
        Form_Menu_Utama.MasterToolStripMenuItem.Visible = True
        Form_Menu_Utama.TransaksiToolStripMenuItem.Visible = True
        Form_Menu_Utama.LaporanToolStripMenuItem.Visible = True
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Form_Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.PasswordChar = "*"
    End Sub


    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If TextBox1.Text = "" Or TextBox2.Text = "" Then
                MsgBox("Silahkan isi Username dan Passoword anda !")
            Else
                Call Koneksi()
                cmd = New SqlCommand("SELECT * FROM TB_Login where username = '" & TextBox1.Text & "' AND password = '" & TextBox2.Text & "'", Conn)
                Rd = cmd.ExecuteReader
                Rd.Read()
                If Not Rd.HasRows Then
                    MsgBox("Username atau Password anda Salah !")
                Else
                    Me.Close()
                    Call Terbuka()
                End If
            End If
        End If
    End Sub
End Class
