Public Class Form_Laporan
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form_Laporan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ctkLaporanTransaksi.CrystalReportViewer3.ReportSource = ctkLaporanTransaksi.CrystalReport41
        ctkLaporanTransaksi.Show()
    End Sub

    Private Sub cetak_Click(sender As Object, e As EventArgs) Handles cetak.Click
        ctkLaporanRestock.CrystalReportViewer1.ReportSource = ctkLaporanRestock.CrystalReport21
        ctkLaporanRestock.Show()
    End Sub
End Class