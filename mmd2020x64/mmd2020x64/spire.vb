Imports System
Imports Spire.Pdf
Imports Spire.Pdf.Security

Namespace EncryptPDF
    Friend Class Encryption
        Shared Sub Main(ByVal args() As String)
            'Load File
            Dim doc As New PdfDocument()
            doc.LoadFromFile("D:\work\My Documents\Shelley.pdf")
            'encrypt
            doc.Security.Encrypt("654321", "123456", PdfPermissionsFlags.Print Or PdfPermissionsFlags.FillFields, PdfEncryptionKeySize.Key128Bit)
            'Save and Launch File
            doc.SaveToFile("Encryption.pdf")
            doc.Close()
            System.Diagnostics.Process.Start("Encryption.pdf")
        End Sub
    End Class
End Namespace
