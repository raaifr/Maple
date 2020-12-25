Imports System.Data.SqlClient

Public Class BookProcess
    Public Enum Category
        Any = 0
        Book = 1
        Author = 2
        Publisher = 3
        ISBN = 4
    End Enum

    Public Shared Function findBook(ByVal searchterm As String, Optional ByVal category As Integer = 1, Optional CheckStock As Boolean = False) As List(Of BookModel)
        Dim records As List(Of BookModel) = New List(Of BookModel)

        Dim query As String = Nothing

        Select Case category
            Case 0 'this is a limited auto/any search since some categories for search with wild cards will be only for staff. Use advance search for a more comprehensive search
                query = <sql>
                            SELECT Book.id, ISNULL(Book.series_title,'') AS series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                            FROM [dbo].[Book]
                            INNER JOIN Publisher ON Book.publisher=publisher.id
                            INNER JOIN Shelf ON Book.shelf_id=Shelf.id 
                            WHERE Book.series_title LIKE '%' + @searchterm + '%' OR
                                  Book.book_title LIKE '%' + @searchterm + '%' OR
                                  Book.tags LIKE '%' + @searchterm + '%' OR
                                  Publisher.publisher_name LIKE '%' + @searchterm + '%'
                        </sql>.Value

            Case 1
                query = <sql>
                            SELECT Book.id, ISNULL(Book.series_title,'') AS series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                            FROM [dbo].[Book]
                            INNER JOIN Publisher ON Book.publisher=publisher.id
                            INNER JOIN Shelf ON Book.shelf_id=Shelf.id 
                            WHERE [series_title] LIKE '%' + @searchterm + '%' OR [book_title] LIKE '%' + @searchterm + '%'
                        </sql>.Value

            Case 2
                query = <sql>
                            SELECT Book.id, ISNULL(Book.series_title,'') AS series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                                   FROM [dbo].[Contributer]
                                   INNER JOIN Book ON Contributer.book_id=Book.id 
                                   INNER JOIN Author ON Contributer.author_id=Author.id
                                   INNER JOIN Publisher ON Book.publisher=publisher.id
                                   INNER JOIN Shelf ON Book.shelf_id=Shelf.id
                                   WHERE Author.author_firstname LIKE '%' + @searchterm + '%' OR
	                                     Author.author_middlename LIKE '%' + @searchterm + '%' OR
	                                     Author.author_lastname LIKE '%' + @searchterm + '%'
                        </sql>.Value

            Case 3
                query = <sql>
                            SELECT Book.id, ISNULL(Book.series_title,'') AS series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                            FROM [dbo].[Book]
                            INNER JOIN Publisher ON Book.publisher=publisher.id
                            INNER JOIN Shelf ON Book.shelf_id=Shelf.id 
                            WHERE Publisher.publisher_name LIKE '%' + @searchterm + '%'
                        </sql>.Value

            Case 4
                query = <sql>
                            SELECT Book.id, ISNULL(Book.series_title,'') AS series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                            FROM [dbo].[Book]
                            INNER JOIN Publisher ON Book.publisher=publisher.id
                            INNER JOIN Shelf ON Book.shelf_id=Shelf.id 
                            WHERE Book.isbn=@searchterm
                        </sql>.Value

        End Select
        If query = Nothing Then Return records

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@searchterm", searchterm)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            'Book.id, Book.series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
            While reader.Read()
                Dim book As New BookModel With {
                            .id = reader.Item(0),
                            .series_title = reader.Item(1),
                            .book_title = reader.Item(2),
                            .isbn = reader.Item(3),
                            .ddc = reader.Item(4),
                            .tags = reader.Item(5),
                            .year = CInt(reader.Item(6)),
                            .stock = CInt(reader.Item(7)),
                            .publisher = reader.Item(8),
                            .shelf = reader.Item(9),
                            .authors = getAuthors(CInt(reader.Item(0)))
                            }
                If CheckStock Then
                    book.borrowed = TransactionProcess.getBorrowed(reader.Item(0))
                    book.reserved = TransactionProcess.getRevervations(reader.Item(0))
                End If
                records.Add(book)
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try


        Return records


    End Function

    Public Shared Function getBook(ByVal bookid As Integer, Optional ByVal checkstock As Boolean = False) As BookModel
        Dim res As New BookModel
        Dim query = <sql>
                            SELECT Book.id, ISNULL(Book.series_title,'') AS series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number
                            FROM [dbo].[Book]
                            INNER JOIN Publisher ON Book.publisher=publisher.id
                            INNER JOIN Shelf ON Book.shelf_id=Shelf.id 
                            WHERE [dbo].[Book].id=@bookid
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@bookid", bookid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                With res
                    .id = reader.Item(0)
                    .series_title = reader.Item(1)
                    .book_title = reader.Item(2)
                    .isbn = reader.Item(3)
                    .ddc = reader.Item(4)
                    .tags = reader.Item(5)
                    .year = CInt(reader.Item(6))
                    .stock = CInt(reader.Item(7))
                    .publisher = reader.Item(8)
                    .shelf = reader.Item(9)
                    .authors = getAuthors(CInt(reader.Item(0)))
                End With
                If checkstock Then
                    res.borrowed = TransactionProcess.getBorrowed(reader.Item(0))
                    res.reserved = TransactionProcess.getRevervations(reader.Item(0))
                End If
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

    Public Shared Function getStock(ByVal bookid As Integer) As Integer
        Dim res As Integer = 0
        Dim query = <sql>
                            SELECT stock 
                            FROM [dbo].[Book] 
                            where id = @bookid
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@bookid", bookid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                res = reader.Item(0)
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

    Public Shared Function getAuthors() As List(Of String)
        Dim res As New List(Of String)
        Dim query = <sql>
                            SELECT Author.author_firstname + ' ' + ISNULL(Author.author_middlename,'') + ' ' + Author.author_lastname AS auth
                            FROM [dbo].[Author]
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

    Public Shared Function findAuthor(ByVal input As String) As List(Of AuthorModel)
        Dim res As New List(Of AuthorModel)
        Dim query = <sql>
                            SELECT * FROM [dbo].[Author] 
                            WHERE author_firstname LIKE '%' + @input + '%'  
                            OR author_middlename LIKE '%' + @input + '%' 
                            OR author_lastname LIKE '%' + @input + '%'
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@input", input)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                Dim author As New AuthorModel With
                    {
                    .id = reader.Item(0),
                    .firstname = reader.Item(1),
                    .middlename = reader.Item(2),
                    .lastname = reader.Item(3)
                    }
                res.Add(author)
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

    Public Shared Function getAuthor(ByVal id As Integer) As AuthorModel
        Dim res As New AuthorModel
        Dim query = <sql>
                            SELECT *
                            FROM [dbo].[Author]
                            WHERE id = @aid
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@aid", id)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                With res
                    .id = reader.Item(0)
                    .firstname = reader.Item(1)
                    .middlename = reader.Item(2)
                    .lastname = reader.Item(3)
                    Return res
                End With
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

    Public Shared Function getAuthors(ByVal bookid As Integer) As List(Of String)
        Dim res As New List(Of String)
        Dim query = <sql>
                            SELECT Author.author_firstname + ' ' + ISNULL(Author.author_middlename,'') + ' ' + Author.author_lastname AS auth
                            FROM [dbo].[Author]
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

    Public Shared Function insertAuthor(ByVal firstname As String, ByVal middlename As String, ByVal lastname As String) As Integer
        Dim query As String = <sql>
                                INSERT INTO [Author] (author_firstname ,author_middlename ,author_lastname ) 
                                VALUES (@fname, @midname, @lastname)
                            </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@fname", firstname)
        cmd.Parameters.AddWithValue("@midname", middlename)
        cmd.Parameters.AddWithValue("@lastname", lastname)

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

    End Function
    Public Shared Function getshelves() As List(Of String)
        Dim res As New List(Of String)
        Dim query = <sql>
                            SELECT shelf_number FROM [Shelf]
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

    Public Shared Function getpublisher() As List(Of String)
        Dim res As New List(Of String)
        Dim query = <sql>
                            SELECT DISTINCT publisher_name FROM [Publisher]
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

    Public Shared Function getpublisher(ByVal id As Integer) As PublisherModel
        Dim res As New PublisherModel
        Dim query = <sql>
                            SELECT * FROM Publisher 
                            WHERE id =  @input
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@input", id)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                With res
                    .id = reader.Item(0)
                    .name = reader.Item(1)
                    .address = reader.Item(2)
                End With
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
    End Function

    Public Shared Function findpublisher(ByVal term As String) As List(Of PublisherModel)
        Dim res As New List(Of PublisherModel)
        Dim query = <sql>
                            SELECT * FROM Publisher 
                            WHERE publisher_name LIKE '%' + @input + '%'  
                            OR [address] LIKE '%' + @input + '%'
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@input", term)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                Dim author As New PublisherModel With
                    {
                    .id = reader.Item(0),
                    .name = reader.Item(1),
                    .address = reader.Item(2)
                    }
                res.Add(author)
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

    Public Shared Function insertPublisher(ByVal publishername As String, ByVal address As String) As Integer
        Dim query As String = <sql>
                                INSERT INTO [Publisher] (publisher_name, [address])
                                VALUES (@name, @house)
                            </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@name", publishername)
        cmd.Parameters.AddWithValue("@house", address)

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

    End Function

    Public Shared Function getseriesnames() As List(Of String)
        Dim res As New List(Of String)
        Dim query = <sql>
                            SELECT DISTINCT series_title FROM [Book] WHERE series_title IS NOT NULL
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

    Public Shared Function getreservehistory(ByVal userid As Integer) As List(Of BookModel)
        Dim res As New List(Of BookModel)

        Dim query = <sql>
                        SELECT Book.id, ISNULL(Book.series_title,'') AS series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number, Reservation.reserve_date
                        FROM [Book] 
                        INNER JOIN [Reservation] ON [Reservation].book_id = [Book].id
                        INNER JOIN [Publisher] ON [Publisher].id = [Book].publisher
                        INNER JOIN [Shelf] ON [Shelf].id = [Book].shelf_id
                        WHERE [Reservation].user_id = @userid
                     </sql>.Value
        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", userid)

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
                    .year = CInt(reader.Item(6)),
                    .stock = CInt(reader.Item(7)),
                    .publisher = reader.Item(8),
                    .shelf = reader.Item(9),
                    .reservedate = reader.Item(10),
                    .authors = getAuthors(CInt(reader.Item(0)))
                }
                res.Add(book)
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res

    End Function

    Public Shared Function getborrowhistory(ByVal userid As Integer) As List(Of BookModel)
        Dim res As New List(Of BookModel)

        Dim query = <sql>
                        SELECT Book.id, ISNULL(Book.series_title,'') AS series_title, Book.book_title, Book.isbn, Book.ddc, Book.tags, Book.year, Book.stock, Publisher.publisher_name, Shelf.shelf_number, [Transaction].borrow_date, [Transaction].return_date, [Transaction].due
                        FROM [Book] 
                        INNER JOIN [Transaction] ON [Transaction].book_id = [Book].id
                        INNER JOIN [Publisher] ON [Publisher].id = [Book].publisher
                        INNER JOIN [Shelf] ON [Shelf].id = [Book].shelf_id
                        WHERE [Transaction].user_id = @userid
                     </sql>.Value
        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", userid)

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
                    .year = CInt(reader.Item(6)),
                    .stock = CInt(reader.Item(7)),
                    .publisher = reader.Item(8),
                    .shelf = reader.Item(9),
                    .borrowdate = reader.Item(10),
                    .due = reader.Item(12),
                    .authors = getAuthors(CInt(reader.Item(0)))
                }
                If IsDBNull(reader.Item(11)) Then book.returndate = Nothing Else book.returndate = reader.Item(11)

                res.Add(book)
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res

    End Function

    Public Shared Function checkOverdue(ByVal userid As Integer) As List(Of OverDueModel)
        Dim res As New List(Of OverDueModel)

        Dim query = <sql>
                        SELECT borrow_date, due, book_id
                        FROM [Transaction] 
                        WHERE return_date IS NULL AND [user_id] = @userid
                     </sql>.Value
        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", userid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                Dim ov As New OverDueModel With {
                    .borrowdate = reader.Item(0),
                    .due = reader.Item(1),
                    .bookid = reader.Item(2)
                }
                res.Add(ov)
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

    Public Shared Function decodebookCode(ByVal code As String) As Integer
        'bookcode consists of bk with the book id padded with shitton of zeros
        Return CInt(code.Replace("bk", ""))
    End Function

    Public Shared Function generatebookCode(ByVal bookid As Integer) As Integer
        'bookcode consists of bk with the book id padded with shitton of zeros
        Dim sb As New StringBuilder

        sb.Append("bk")
        For i = 0 To CInt(8 - bookid.ToString.Length)
            sb.Append("0")
        Next
        sb.Append(bookid.ToString)

        Return sb.ToString
    End Function

#Region "           UPDATE QUERIES              "
    Public Shared Function updatebook(ByVal bookid As String,
                                      ByVal seriestitle As String,
                                      ByVal booktitle As String,
                                      ByVal publisher As String,
                                      ByVal authors As String,
                                      ByVal isbn As String,
                                      ByVal ddc As Double,
                                      ByVal tags As String,
                                      ByVal year As Integer,
                                      ByVal stock As Integer,
                                      ByVal shelf As String) As QueryResultObj

        Dim oldbook As BookModel = getBook(bookid)

        If Not oldbook.publisher = publisher Then
            'update publisher
            Try
                Dim query = <sql>
                                UPDATE Book set Book.publisher = (SELECT id FROM [Publisher] WHERE publisher_name = @publisher)
                            </sql>.Value

                Using conn As SqlConnection = SqlDataAccess.getConnection
                    conn.Open()
                    Using cmd As SqlCommand = New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@publisher", publisher)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

            Catch ex As Exception
                Return New QueryResultObj(False, ex.Message)
            End Try
        End If

        If Not oldbook.authorstring = authors Then
            'update authors
            'yeah better not mess with this either
            Try
                Dim multiauthors As Boolean = authors.Contains(",")

                If multiauthors Then
                    Dim authorslist As List(Of String) = authors.Split(",").ToList
                    Dim contriblist As New List(Of String())
                    Dim removelist As New List(Of Integer)

                    'get all contribs for this book
                    Dim query = <sql>
                                SELECT [Contributer].id,
                                [Contributer].author_id,
                                [Author].author_firstname + ' ' + [Author].author_middlename + ' ' + [Author].author_lastname as AuthorFullName,
                                [Author].author_firstname + ' ' + [Author].author_lastname as AuthorPartialName 
                                FROM [Contributer] INNER JOIN [Author] ON [Contributer].author_id = [Author].id 
                                WHERE [Contributer].book_id = @bookid
                            </sql>.Value

                    Using conn As SqlConnection = SqlDataAccess.getConnection
                        conn.Open()
                        Using cmd As SqlCommand = New SqlCommand(query, conn)
                            cmd.Parameters.AddWithValue("@bookid", bookid)
                            Dim reader As SqlDataReader = cmd.ExecuteReader
                            While reader.Read()
                                contriblist.Add(New String() {reader.Item(0), reader.Item(1), reader.Item(2), reader.Item(2)})
                            End While
                        End Using
                    End Using

                    'compare and replace any authors that are not in the update
                    For Each contrib In contriblist
                        For Each auth In authorslist
                            If Not contrib(2) = auth Or contrib(3) = auth Then
                                'replace with updated name
                                query = <sql>
                                                UPDATE [Contributer] SET author_id = @authorid WHERE book_id = @bookid
                                        </sql>.Value

                                Using conn As SqlConnection = SqlDataAccess.getConnection
                                    conn.Open()
                                    Using cmd As SqlCommand = New SqlCommand(query, conn)
                                        cmd.Parameters.AddWithValue("@bookid", bookid)
                                        cmd.Parameters.AddWithValue("@authorid", contrib(1))
                                        cmd.ExecuteNonQuery()
                                    End Using
                                End Using
                            End If
                        Next
                    Next

                Else 'its a single author
                    Dim query = <sql>
                                    UPDATE [Contributer] 
                                    SET author_id = (SELECT subtable.id FROM 
                                                            (SELECT id, 
                                                                    author_firstname + ' ' + author_middlename + ' ' + author_lastname AS authorfullname,
                                                                    author_firstname + ' ' + author_lastname AS authorpartialname 
                                                             FROM [Author]) subtable
                                                    WHERE authorfullname = @authorname OR authorpartialname = @authorname) 
                                    WHERE book_id = @bookid
                                </sql>.Value

                    Using conn As SqlConnection = SqlDataAccess.getConnection
                        conn.Open()
                        Using cmd As SqlCommand = New SqlCommand(Query, conn)
                            cmd.Parameters.AddWithValue("@bookid", bookid)
                            cmd.Parameters.AddWithValue("@authorname", authors)
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using

                End If
            Catch ex As Exception
                Return New QueryResultObj(False, ex.Message)
            End Try
        End If

        If Not oldbook.shelf = shelf Then
            'update shelf
            Try
                Dim query = <sql>
                                UPDATE Book set Book.shelf_id = (SELECT id FROM [Shelf] WHERE shelf_number = @shelf)
                            </sql>.Value

                Using conn As SqlConnection = SqlDataAccess.getConnection
                    conn.Open()
                    Using cmd As SqlCommand = New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@shelf", shelf)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

            Catch ex As Exception
                Return New QueryResultObj(False, ex.Message)
            End Try
        End If

        'update the rest
        Try
            Dim query = <sql>
                             UPDATE [dbo].[Book] 
                             SET 
                                 series_title = @series_title, 
                                 book_title = @book_title, 
                                 isbn = @isbn, 
                                 ddc = @ddc, 
                                 tags = @tags, 
                                 [year] = @year, 
                                 stock = @stock
                             WHERE id = @bookid
                         </sql>.Value


            Using conn As SqlConnection = SqlDataAccess.getConnection
                conn.Open()
                Using cmd As SqlCommand = New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@series_title", seriestitle)
                    cmd.Parameters.AddWithValue("@book_title", booktitle)
                    cmd.Parameters.AddWithValue("@isbn", isbn)
                    cmd.Parameters.AddWithValue("@ddc", ddc)
                    cmd.Parameters.AddWithValue("@tags", tags)
                    cmd.Parameters.AddWithValue("@year", year)
                    cmd.Parameters.AddWithValue("@stock", stock)
                    cmd.Parameters.AddWithValue("@bookid", bookid)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            Return New QueryResultObj(False, ex.Message)
        End Try

        Return New QueryResultObj(True, "")
    End Function

    Public Shared Function updateAuthor(ByVal authorid As Integer, ByVal firstname As String, ByVal middlename As String, ByVal lastname As String) As QueryResultObj
        Try
            Dim query = <sql>
                            UPDATE [Author] 
                            SET author_firstname = @fname, author_middlename = @mname, author_lastname = @lname
                            WHERE id = @aid
                        </sql>.Value

            Using conn As SqlConnection = SqlDataAccess.getConnection
                conn.Open()
                Using cmd As SqlCommand = New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@fname", firstname)
                    cmd.Parameters.AddWithValue("@mname", middlename)
                    cmd.Parameters.AddWithValue("@lname", lastname)
                    cmd.Parameters.AddWithValue("@aid", authorid)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return New QueryResultObj(True, "")
        Catch ex As Exception
            Return New QueryResultObj(False, ex.Message)
        End Try
    End Function

    Public Shared Function updatePublisher(ByVal id As Integer, ByVal name As String, ByVal address As String) As QueryResultObj

    End Function

#End Region

#Region "           DELETE QUERIES              "
    Public Shared Function deletebook(id As Integer) As Integer
        Dim query = <sql>
                        DELETE
                        FROM [Book] 
                        WHERE id = @bkid
                     </sql>.Value
        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@bkid", id)

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
    End Function

    Public Shared Function deleteauthor(id As Integer) As Integer
        Dim query = <sql>
                        DELETE
                        FROM [Author] 
                        WHERE id = @pid
                     </sql>.Value
        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@pid", id)

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
    End Function

    Public Shared Function deletepublisher(id As Integer) As Integer
        Dim query = <sql>
                        DELETE
                        FROM [Publisher] 
                        WHERE id = @pid
                     </sql>.Value
        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@pid", id)

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
    End Function

#End Region

    Public Shared Function InsertBook(ByVal seriestitle As String,
                                      ByVal booktitle As String,
                                      ByVal publisher As String,
                                      ByVal authors As String,
                                      ByVal isbn As String,
                                      ByVal ddc As Double,
                                      ByVal tags As String,
                                      ByVal year As Integer,
                                      ByVal stock As Integer,
                                      ByVal shelf As String) As QueryResultObj

        Dim author_id As Integer = -1
        Dim author_idCol As New List(Of Integer)
        Dim multipleAuthors As Boolean = False
        Dim query As String

        ' ## this part determines if there is multiple authors or not and fetches all the author id to store in the contributors table ##
        ' dont f**k with this block
        If authors.Contains(",") Then
            multipleAuthors = True
            For Each author As String In authors.ToString.Split(",")
                author = author.Trim.Replace(",", "")
                'get author id
                query = <sql>
                        SELECT subtable.id FROM 
                                            (SELECT id, 
                                                    author_firstname + ' ' + author_middlename + ' ' + author_lastname AS authorfullname,
                                                    author_firstname + ' ' + author_lastname AS authorpartialname 
                                             FROM [Author]) subtable
                        WHERE authorfullname = @authorin OR authorpartialname = @authorin
                </sql>.Value

                Try
                    Using conn As SqlConnection = SqlDataAccess.getConnection
                        conn.Open()
                        Using cmd As SqlCommand = New SqlCommand(query, conn)
                            cmd.Parameters.AddWithValue("@authorin", author)

                            Dim reader As SqlDataReader = cmd.ExecuteReader
                            While reader.Read()
                                author_idCol.Add(reader.Item(0))
                            End While
                        End Using
                    End Using
                Catch ex As Exception
                    'Throw ex 'away
                    Return New QueryResultObj(False, ex.Message)
                End Try
            Next
        Else
            multipleAuthors = False

            query = <sql>
                        SELECT subtable.id FROM 
                                            (SELECT id, 
                                                    author_firstname + ' ' + author_middlename + ' ' + author_lastname AS authorfullname,
                                                    author_firstname + ' ' + author_lastname AS authorpartialname 
                                             FROM [Author]) subtable
                        WHERE authorfullname = @authorin OR authorpartialname = @authorin
                </sql>.Value
            Try
                Using conn As SqlConnection = SqlDataAccess.getConnection
                    conn.Open()
                    Using cmd As SqlCommand = New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@authorin", authors.Trim)

                        Dim reader As SqlDataReader = cmd.ExecuteReader
                        While reader.Read()
                            author_id = reader.Item(0)
                            Exit While
                        End While
                    End Using
                End Using
            Catch ex As Exception
                'Throw ex 'away
                Return New QueryResultObj(False, ex.Message)
            End Try
        End If


        If multipleAuthors And IsNothing(author_idCol) Then
            Return New QueryResultObj(False, "Invalid Authors format. Separate each Author by a comma and separate each name with a space(i.e first middle and last name) and ABB with a period. Example #ENID G. BLYTON, COLIN MACKERAL FITZBURG")
        ElseIf Not multipleAuthors And author_id < 0 Then
            Return New QueryResultObj(False, "Invalid Author: " & authors)
        End If
        ' END OF shaky shakerson block
        'dont f**k with the block above

        '$$ NO INSERTIONS OCCUR BEFORE THIS POINT

        '## insert the book record
        query = <sql>
                                INSERT INTO [dbo].[Book] (series_title, book_title, publisher, isbn, ddc, tags, [year], stock, shelf_id)
                                VALUES(@series_title, @book_title, 
                                       (SELECT id FROM [Publisher] WHERE publisher_name = @publisher),
                                       @isbn, @ddc, @tags, @year, @stock, 
                                       (SELECT id FROM [Shelf] WHERE shelf_number = @shelf)
                                       )
                            </sql>.Value

        Try
            Using conn As SqlConnection = SqlDataAccess.getConnection
                conn.Open()
                Using cmd As SqlCommand = New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@series_title", seriestitle)
                    cmd.Parameters.AddWithValue("@book_title", booktitle)
                    cmd.Parameters.AddWithValue("@publisher", publisher)
                    cmd.Parameters.AddWithValue("@isbn", isbn)
                    cmd.Parameters.AddWithValue("@ddc", ddc)
                    cmd.Parameters.AddWithValue("@tags", tags)
                    cmd.Parameters.AddWithValue("@year", year)
                    cmd.Parameters.AddWithValue("@stock", stock)
                    cmd.Parameters.AddWithValue("@shelf", shelf)

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            'Throw ex 'away
            Return New QueryResultObj(False, ex.Message)
        End Try

        'insert contributers (author)
        'dont mess with this either, s*ts nested to the backside its complex to decode
        query = <sql>
                     INSERT INTO [Contributer] (author_id, book_id) 
                     VALUES (
                                @authorid, 
                                (
                                   SELECT id FROM [Book] 
                                   WHERE book_title = @booktitle 
                                   AND publisher = (SELECT id FROM [Publisher] WHERE publisher_name = @publisher) 
                                   AND shelf_id = (SELECT id FROM [Shelf] WHERE shelf_number = @shelf) 
                                )
                            )
                </sql>.Value

        If multipleAuthors And Not IsNothing(author_idCol) Then
            Try
                For Each auth As Integer In author_idCol
                    Using conn As SqlConnection = SqlDataAccess.getConnection
                        conn.Open()
                        Using cmd As SqlCommand = New SqlCommand(query, conn)
                            cmd.Parameters.AddWithValue("@authorid", auth)
                            cmd.Parameters.AddWithValue("@booktitle", booktitle)
                            cmd.Parameters.AddWithValue("@publisher", publisher)
                            cmd.Parameters.AddWithValue("@shelf", shelf)

                            cmd.ExecuteNonQuery()
                        End Using
                    End Using
                Next
            Catch ex As Exception
                'Throw ex 'away
                Return New QueryResultObj(False, ex.Message)
            End Try
            Return New QueryResultObj(True, "")

        ElseIf Not multipleAuthors And author_id >= 0 Then
            Try
                Using conn As SqlConnection = SqlDataAccess.getConnection
                    conn.Open()
                    Using cmd As SqlCommand = New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@authorid", author_id)
                        cmd.Parameters.AddWithValue("@booktitle", booktitle)
                        cmd.Parameters.AddWithValue("@publisher", publisher)
                        cmd.Parameters.AddWithValue("@shelf", shelf)

                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                Return New QueryResultObj(True, "")
            Catch ex As Exception
                'Throw ex 'away
                Return New QueryResultObj(False, ex.Message)
            End Try
        Else
            'Throw New Exception("Error Inserting Author for Book")
            Return New QueryResultObj(False, "Error Inserting Author for Book")
        End If

        'basically just dont mess with this function at all ;)
    End Function



End Class
