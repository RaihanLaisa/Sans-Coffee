Imports System.Data.SqlClient

Public Class Form_Supplier

    Sub KondisiAwal()
        Call Koneksi()
        Da = New SqlDataAdapter("Select * From TB_SUPPLIER", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "TB_SUPPLIER")
        DataGridView1.DataSource = (Ds.Tables("TB_SUPPLIER"))
    End Sub


    Private Sub Form_Supplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
        ' Panggil fungsi untuk mendapatkan data dari database


    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles tbtelpon.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Koneksi()
        Dim InputData As String = "insert into TB_SUPPLIER values ('" & tbidsupp.Text & "', '" & tbnama.Text & "', '" & tbalamat.Text & "' , '" & tbtelpon.Text & "' )"
        cmd = New SqlCommand(InputData, Conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Berhasil Di Input")
        Call KondisiAwal()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call Koneksi()
        Dim HapusData As String = "Delete TB_SUPPLIER where IDSupplier = '" & tbidsupp.Text & "'"
        cmd = New SqlCommand(HapusData, Conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Berhasil Di Hapus")
        Call KondisiAwal()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call Koneksi()
        Dim UpdateData As String = "update TB_SUPPLIER set NamaSupplier ='" & tbnama.Text & "',Alamat ='" & tbalamat.Text & "' ,NoTelepon ='" & tbtelpon.Text & "'  where IDSupplier = '" & tbidsupp.Text & "'"
        cmd = New SqlCommand(UpdateData, Conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Berhasil Di Update")
        Call KondisiAwal()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index

        Call Koneksi()
        cmd = New SqlCommand("select * From TB_SUPPLIER where IDSupplier = '" & DataGridView1.Item(0, i).Value & "'", Conn)
        Rd = cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            MsgBox("Id Pelanggan Tidak Ada")
        Else
            On Error Resume Next
            tbidsupp.Text = Rd.Item("IDSupplier")
            tbnama.Text = Rd.Item("NamaSupplier")
            tbalamat.Text = Rd.Item("Alamat")
            tbtelpon.Text = Rd.Item("NoTelepon")
        End If
    End Sub
End Class