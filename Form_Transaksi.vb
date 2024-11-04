Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Reflection.Emit


Public Class Form_Transaksi

    Sub KondisiAwal()
        Cb_NamaProduk.Text = ""
        TextBox3.Text = ""
        Txt_Qtt.Text = ""
        TextBox2.Text = ""
        TextBox1.Text = ""
        Label7.Text = ""
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub KodeTransaksi()
        Dim random As New Random()

        ' Menghasilkan angka acak antara 1 dan 1000
        Dim randomNumber As Integer = random.Next(1, 1001)

        ' Menetapkan nilai angka acak ke TextBox atau kontrol lainnya
        TextBox3.Text = $"TR{Format(Now, "ddMMyy")}{randomNumber:D3}"
    End Sub


    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Form_Transaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Panggil fungsi untuk mendapatkan data dari database
        PopulateComboBox()
        Call KodeTransaksi()
    End Sub

    Private Sub PopulateComboBox()
        Try
            ' Buat objek koneksi
            Using connection As New SqlConnection("Data Source=LAPTOP-9IKRK684;Initial Catalog=FP_SansCoffee;Integrated Security=True;")
                Dim query As String = "SELECT NamaProduk FROM TB_RESTOK"
                Using cmd As New SqlCommand(query, connection)
                    ' Buka koneksi
                    connection.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        ' Bersihkan item ComboBox sebelum menambahkan item baru
                        Cb_NamaProduk.Items.Clear()

                        While reader.Read()
                            Cb_NamaProduk.Items.Add(reader("NamaProduk").ToString())
                        End While

                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Memeriksa apakah semua TextBox telah diisi
        If String.IsNullOrEmpty(Cb_NamaProduk.Text) OrElse
       String.IsNullOrEmpty(Txt_Kode.Text) OrElse
       String.IsNullOrEmpty(Txt_Harga.Text) OrElse
       String.IsNullOrEmpty(Txt_Qtt.Text) Then
            MessageBox.Show("Harap isi semua kolom sebelum menambahkan data.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            ' Menambahkan data ke DataGridView
            DataGridView1.Rows.Add(Cb_NamaProduk.Text, Txt_Kode.Text, Txt_Harga.Text, Txt_Qtt.Text)
            ' Membersihkan TextBox setelah menambahkan data
            Txt_Kode.Clear()
            Txt_Harga.Clear()
            Txt_Qtt.Clear()

            ' Menghitung total dan menetapkan nilai Label7
            Dim sum As Decimal = 0
            For i = 0 To DataGridView1.Rows.Count - 1
                Dim HasilKolom3 As Double = Convert.ToDouble(DataGridView1.Rows(i).Cells(2).Value)
                Dim HasilKolom4 As Double = Convert.ToDouble(DataGridView1.Rows(i).Cells(3).Value)
                Dim result As Double = HasilKolom3 * HasilKolom4
                DataGridView1.Rows(i).Cells(4).Value = result
                sum += result
            Next
            Label7.Text = sum.ToString()

        End If

    End Sub


    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Dim sum As Decimal = 0

        If DataGridView1.SelectedRows.Count > 0 Then
            For Each selectedRow As DataGridViewRow In DataGridView1.SelectedRows
                If Not selectedRow.IsNewRow Then
                    DataGridView1.Rows.Remove(selectedRow) ' Hapus baris
                End If
            Next

            For i = 0 To DataGridView1.Rows.Count - 1  ' Perbarui nilai sum setelah menghapus baris
                Dim HasilKolom3 As Double = Convert.ToDouble(DataGridView1.Rows(i).Cells(2).Value)
                Dim HasilKolom4 As Double = Convert.ToDouble(DataGridView1.Rows(i).Cells(3).Value)
                Dim result As Double = HasilKolom3 * HasilKolom4
                DataGridView1.Rows(i).Cells(4).Value = result
                sum += result
            Next
        Else
            MessageBox.Show("Pilih baris yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        Label7.Text = sum.ToString()


    End Sub

    Private Sub Cb_NamaProduk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_NamaProduk.SelectedIndexChanged
        Try
            ' Peroleh Kode Produk dan Harga dari database berdasarkan NamaProduk yang dipilih
            Using connection As New SqlConnection("Data Source=LAPTOP-9IKRK684;Initial Catalog=FP_SansCoffee;Integrated Security=True;")
                Dim query As String = "SELECT KodeProduk, HargaProduk FROM TB_RESTOK WHERE NamaProduk =@NamaProduk"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@NamaProduk", Cb_NamaProduk.Text)
                    ' Buka koneksi
                    connection.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Txt_Kode.Text = reader("KodeProduk").ToString()
                            Txt_Harga.Text = reader("HargaProduk").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception

            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text = "" Then
            MsgBox("Tidak Ada Transaksi, Lakukan Transaksi Terlebih Dahulu")
        Else
            Call Koneksi()
            Dim SimpanTransaksi As String = "insert into TB_Transaksi values ('" & TextBox3.Text & "' ,  '" & Cb_NamaProduk.Text & "', '" & Label7.Text & "')"
            cmd = New SqlCommand(SimpanTransaksi, Conn)
            cmd.ExecuteNonQuery()
            Call KondisiAwal()

        End If
    End Sub


    Private Sub Button2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Button2.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            cmd = New SqlCommand("Select * From TB_RESTOK Where KodeProduk ='" & Txt_Kode.Text & "'", Conn)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                Txt_Kode.Text = Rd.Item("KodeProduk")
                Cb_NamaProduk.Text = Rd.Item("NamaProduk")
                Txt_Harga.Text = Rd.Item("HargaProduk")

            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub



    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(TextBox2.Text) < Val(Label7.Text) Then
                MsgBox("Pembayaran kurang !!")
            ElseIf Val(TextBox2.Text) = Val(Label7.Text) Then
                TextBox1.Text = 0
            ElseIf Val(TextBox2.Text) > Val(Label7.Text) Then
                TextBox1.Text = Val(TextBox2.Text) - Val(Label7.Text)

            End If
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class