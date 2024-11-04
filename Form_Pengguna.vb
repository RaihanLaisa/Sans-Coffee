Imports System.Data.SqlClient

Public Class Form_Pengguna

    Sub KondisiAwal()
        Call Koneksi()
        Da = New SqlDataAdapter("Select * From TB_Login", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "TB_Login")
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Tbnama.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Form_Pengguna_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Call Koneksi()

            ' Gunakan parameterized query untuk menghindari SQL Injection
            Dim InputData As String = "INSERT INTO TB_Login VALUES (@Username, @Password, @Nama)"
            cmd = New SqlCommand(InputData, Conn)

            ' Tambahkan parameter dan nilainya
            cmd.Parameters.AddWithValue("@Username", Tbuser.Text)
            cmd.Parameters.AddWithValue("@Password", Tbpw.Text)
            cmd.Parameters.AddWithValue("@Nama", Tbnama.Text)

            ' Eksekusi perintah SQL
            cmd.ExecuteNonQuery()

            MsgBox("Data Berhasil Di Input")
            Call KondisiAwal()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            ' Tutup koneksi setelah selesai
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        End Try
    End Sub

End Class
