Imports System.Data.SqlClient

Public Class Form_Master
    Sub KondisiAwal()
        Call Koneksi()
        Da = New SqlDataAdapter("Select * From TB_RESTOK", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "TB_RESTOK")
        DataGridView1.DataSource = (Ds.Tables("TB_RESTOK"))
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
        ' Panggil fungsi untuk mendapatkan data dari database
        PopulateComboBox()
    End Sub

    Private Sub PopulateComboBox()
        Try
            ' Buat objek koneksi
            Using connection As New SqlConnection("Data Source=LAPTOP-9IKRK684;Initial Catalog=FP_SansCoffee;Integrated Security=True;")
                ' Buat perintah SQL
                Dim query As String = "SELECT NamaSupplier FROM TB_Supplier"
                Using cmd As New SqlCommand(query, connection)
                    ' Buka koneksi
                    connection.Open()

                    ' Buat pembaca data
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        ' Bersihkan item ComboBox sebelum menambahkan item baru
                        CB_Suppllier.Items.Clear()

                        ' Loop melalui hasil kueri dan tambahkan nilai ke ComboBox
                        While reader.Read()
                            CB_Suppllier.Items.Add(reader("NamaSupplier").ToString())
                        End While

                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Tangani kesalahan jika terjadi
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Koneksi()
        Dim InputData As String = "insert into TB_RESTOK values ('" & tbKode.Text & "', '" & tbNama.Text & "', '" & tbharga.Text & "' , '" & tbjuumlahStok.Text & "' , '" & CB_Suppllier.Text & "' )"
        cmd = New SqlCommand(InputData, Conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Berhasil Di Input")
        Call KondisiAwal()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call Koneksi()
        Dim HapusData As String = "Delete TB_RESTOK where KodeProduk = '" & tbKode.Text & "'"
        cmd = New SqlCommand(HapusData, Conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Berhasil Di Hapus")
        Call KondisiAwal()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call Koneksi()
        Dim UpdateData As String = "update TB_RESTOK set NamaProduk ='" & tbNama.Text & "',JmlStokProduk ='" & tbjuumlahStok.Text & "' where KodeProduk = '" & tbKode.Text & "'"
        cmd = New SqlCommand(UpdateData, Conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Berhasil Di Update")
        Call KondisiAwal()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index

        Call Koneksi()
        cmd = New SqlCommand("select * From TB_RESTOK where KodeProduk = '" & DataGridView1.Item(0, i).Value & "'", Conn)
        Rd = cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            MsgBox("Id Pelanggan Tidak Ada")
        Else
            On Error Resume Next
            tbKode.Text = Rd.Item("KodeProduk")
            tbNama.Text = Rd.Item("NamaProduk")
            tbharga.Text = Rd.Item("HargaProduk")
            tbjuumlahStok.Text = Rd.Item("JmlStokProduk")
            CB_Suppllier.Text = Rd.Item("NamaSupplier")
        End If
    End Sub

    Private Sub tbNama_TextChanged(sender As Object, e As EventArgs) Handles tbNama.TextChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
