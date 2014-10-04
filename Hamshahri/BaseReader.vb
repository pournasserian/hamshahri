Imports System.Net
Imports System.IO

Public Class BaseReader

    Private Shared path As String = "C:\Users\a.pournasserian\Desktop\Hamshahri\Content\"
    Private Shared rawpath As String = path & "raw\"
    Private Shared lastrawfileindex As String = path & "lastrawfileid.txt"
    Private Shared baseurl As String = "http://www.hamshahrionline.ir/details/"

    Public Shared Sub CrawlAll()

        While True

            Dim i As Integer = BaseReader.GetLastRawFileId
            i += 1

            Dim rawcontent As String = ""
            Console.Write(i & ": ")

            Try

                rawcontent = BaseReader.GetRaw(i)
                Console.Write("OK!")

            Catch ex As Exception

                Console.Write(ex.ToString)

            Finally

                SetLastRawFileId(i)
                Write(i, rawcontent)

            End Try

            Console.WriteLine()

        End While

    End Sub

    Private Shared Function GetRaw(id As Integer) As String

        Dim webRequest As HttpWebRequest
        Dim responseReader As StreamReader
        Dim responseData As String = ""

        Try
            webRequest = HttpWebRequest.Create(baseurl & id.ToString)
            responseReader = New StreamReader(webRequest.GetResponse().GetResponseStream())
            responseData = responseReader.ReadToEnd()
            responseReader.Close()
            webRequest.GetResponse().Close()

        Catch ex As Exception

        End Try

        Return responseData

    End Function

    Private Shared Sub Write(fileid As Integer, content As String)
        Dim fullpath As String = rawpath & fileid & ".txt"
        System.IO.File.WriteAllText(fullpath, content)
    End Sub

    Private Shared Function GetLastRawFileId() As Integer

        If Not System.IO.File.Exists(lastrawfileindex) Then
            Return 1
        End If

        Dim id As Integer = CInt(Trim(System.IO.File.ReadAllText(lastrawfileindex)))
        Return id

    End Function

    Private Shared Sub SetLastRawFileId(id As Integer)
        System.IO.File.WriteAllText(lastrawfileindex, id.ToString)
    End Sub

End Class
