Imports System.Data.SqlClient

Public Class BookProcess
    Public Enum Category
        Any = 0
        Book = 1
        Author = 2
        Publisher = 3
        ISBN = 4
    End Enum

    Public Shared Function findBook(Of T)(ByVal searchterm As String, Optional ByVal category As Integer = 1) As List(Of T)
        Dim records As List(Of T) = New List(Of T)

        Dim query As String = <sql>
                                SELECT *
                                FROM dbo.Book
                            </sql>.Value

        Select Case category
            Case 0 'this is a limited auto/any search since some categories for search with wild cards will be only for staff. Use advance search for a more comprehensive search
                query = <sql>
                            SELECT Book.id, Book.series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                            FROM Book
                            INNER JOIN Publisher ON Book.publisher = publisher.id
                            INNER JOIN Shelf ON Book.shelf_id = Shelf.id 
                            WHERE Book.series_title LIKE '%' + @searchterm + '%' OR
                                  Book.book_title LIKE '%' + @searchterm + '%' OR
                                  Book.tags LIKE '%' + @searchterm + '%' OR
                                  Publisher.publisher_name LIKE '%' + @searchterm + '%'
                        </sql>.Value

            Case 1
                query = <sql>
                            SELECT Book.id, Book.series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                            FROM Book
                            INNER JOIN Publisher ON Book.publisher = publisher.id
                            INNER JOIN Shelf ON Book.shelf_id = Shelf.id 
                            WHERE [series_title] LIKE '%' + @searchterm + '%' OR [book_title] LIKE '%' + @searchterm + '%'
                        </sql>.Value

            Case 2
                query = <sql>
                            SELECT Book.id, Book.series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                                   FROM Contributer
                                   INNER JOIN Book ON Contributer.book_id = Book.id 
                                   INNER JOIN Author ON Contributer.author_id = Author.id
                                   INNER JOIN Publisher ON Book.publisher = publisher.id
                                   INNER JOIN Shelf ON Book.shelf_id = Shelf.id
                                   WHERE Author.author_firstname LIKE '%' + @searchterm + '%' OR
	                                     Author.author_middlename LIKE '%' + @searchterm + '%' OR
	                                     Author.author_lastname LIKE '%' + @searchterm + '%'
                        </sql>.Value

            Case 3
                query = <sql>
                            SELECT Book.id, Book.series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                            FROM Book
                            INNER JOIN Publisher ON Book.publisher = publisher.id
                            INNER JOIN Shelf ON Book.shelf_id = Shelf.id 
                            WHERE Publisher.publisher_name LIKE '%' + @searchterm + '%'
                        </sql>.Value

            Case 4
                query = <sql>
                            SELECT Book.id, Book.series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                            FROM Book
                            INNER JOIN Publisher ON Book.publisher = publisher.id
                            INNER JOIN Shelf ON Book.shelf_id = Shelf.id 
                            WHERE Book.isbn = @searchterm
                        </sql>.Value

        End Select

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@searchterm", searchterm)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader

            While reader.Read()
                Dim book As New BookModel With {
                            .id = reader.Item(0),
                            .series_title = reader.Item(1),
                            .book_title = reader.Item(2),
                            .isbn = reader.Item(3),
                            .ddc = reader.Item(4),
                            .tags = reader.Item(5),
                            .Year = CInt(reader.Item(6)),
                            .stock = CInt(reader.Item(7)),
                            .publisher = reader.Item(8),
                            .shelf = reader.Item(9),
                            .authors = getAuthors(CInt(reader.Item(0)))
                            }
                records.Add(book)
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

        If Not records.Count > 0 Then res.Book = records
        Return res


    End Function

    Public Shared Function getAuthors() As List(Of String)
        Dim res As New List(Of String)
        Dim query = <sql>
                            SELECT Author.author_firstname + ' ' + Author.author_lastname AS auth
                            FROM Author
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                res.Add(reader.Item(0))
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

    Public Shared Function getAuthors(bookid As Integer) As List(Of String)
        Dim res As New List(Of String)
        Dim query = <sql>
                            SELECT Author.author_firstname + ' ' + Author.author_lastname AS auth
                            FROM Author
                            INNER JOIN Contributer ON Author.id = Contributer.author_id
                            WHERE Contributer.book_id = @bookid
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@bookid", bookid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                Res.Add(reader.Item(0))
            End While
            Return Res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function


    Public Shared Function InsertBook() As Integer

    End Function


End Class
