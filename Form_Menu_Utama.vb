Public Class Form_Menu_Utama

    Sub Terkunci()
        'MasukToolStripMenuItem.Enabled = True
        'LogoutToolStripMenuItem.Enabled = False
        'MasterToolStripMenuItem.Enabled = False
        'TransaksiToolStripMenuItem.Enabled = False
        'LaporanToolStripMenuItem.Enabled = False
        MasukToolStripMenuItem.Visible = True
        LogoutToolStripMenuItem.Visible = False
        MasterToolStripMenuItem.Visible = False
        TransaksiToolStripMenuItem.Visible = False
        LaporanToolStripMenuItem.Visible = False
    End Sub


    Private Sub Form_Menu_Utama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Terkunci()
    End Sub

    Private Sub MasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasukToolStripMenuItem.Click
        Form_Login.ShowDialog()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        MsgBox("Anda Telah Keluar")
        MsgBox("Silahkan Login Kembali")
        Call Terkunci()
    End Sub

    Private Sub TransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransaksiToolStripMenuItem.Click
        Form_Transaksi.ShowDialog()

    End Sub

    Private Sub LaporanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanToolStripMenuItem.Click
        Form_Laporan.ShowDialog()
    End Sub

    Private Sub MasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterToolStripMenuItem.Click

    End Sub

    Private Sub SupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierToolStripMenuItem.Click
        Form_Supplier.ShowDialog()

    End Sub

    Private Sub AdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StokToolStripMenuItem.Click
        Form_Master.ShowDialog()
    End Sub

    Private Sub PenggunaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenggunaToolStripMenuItem.Click
        Form_Pengguna.ShowDialog()
    End Sub
End Class