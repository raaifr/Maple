Imports System.Data.SqlClient

Public Class TransactionProcess
    Public Shared Function addReservation(ByVal bookid As Integer, ByVal userid As Integer, ByVal rdate As Date) As Integer
        Dim query As String = <sql>
                                INSERT INTO [dbo].[Reservation] (user_id,book_id,reserve_date)
                                VALUES (@userid, @bookid, @rdate)
                            </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@bookid", bookid)
        cmd.Parameters.AddWithValue("@userid", userid)
        cmd.Parameters.AddWithValue("@rdate", rdate)

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
    End Function

    Public Shared Function addTransaction(ByVal userid As Integer, ByVal bookid As Integer, ByVal borrowdate As Date, returndate As Date, Optional ByVal due As Integer = 14) As Integer
        Dim query As String = <sql>
                                INSERT INTO [dbo].[Transaction] (user_id, book_id, borrow_date, return_date, due)
                                VALUES (@userid, @bookid, @borrow, NULL, @due)
                            </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", userid)
        cmd.Parameters.AddWithValue("@bookid", bookid)
        cmd.Parameters.AddWithValue("@borrow", borrowdate)
        'cmd.Parameters.AddWithValue("@return", returndate)
        cmd.Parameters.AddWithValue("@due", due)

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
    End Function

    Public Shared Function updateTransaction(ByVal userid As Integer, ByVal bookid As Integer, returndate As Date) As Integer
        Dim query As String = <sql>
                                UPDATE [dbo].[Transaction] 
                                SET return_date = @return 
                                WHERE user_id = @userid AND book_id = @bookid
                            </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", userid)
        cmd.Parameters.AddWithValue("@bookid", bookid)
        cmd.Parameters.AddWithValue("@return", returndate)

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
    End Function


    Public Shared Function checkRevervations(ByVal bookid As Integer, ByVal userid As Integer) As Boolean
        Dim res As Integer = 0
        Dim query = <sql>
                            SELECT id 
                            FROM [dbo].[Reservation] 
                            where book_id = @bookid AND user_id = @userid
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@bookid", bookid)
        cmd.Parameters.AddWithValue("@userid", userid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                res += 1
            End While
            If res = 0 Then Return False Else Return True
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

    ''' <summary>
    ''' Check if the user has a book that needs to be returned
    ''' </summary>
    ''' <param name="bookid">db id of the book</param>
    ''' <param name="userid">db id of the user</param>
    ''' <returns>False if theres is no record of any book pending return, True if otherwise</returns>
    Public Shared Function checkReturn(ByVal bookid As Integer, ByVal userid As Integer) As Boolean
        Dim res As Integer = 0
        Dim query = <sql>
                            SELECT id 
                            FROM [dbo].[Transaction] 
                            where book_id = @bookid AND user_id = @userid AND return_date IS NULL
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@bookid", bookid)
        cmd.Parameters.AddWithValue("@userid", userid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                res += 1
            End While
            If res > 0 Then Return True Else Return False
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

    Public Shared Function getRevervations(ByVal bookid As Integer) As Integer
        Dim res As Integer = 0
        Dim query = <sql>
                            SELECT id 
                            FROM [dbo].[Reservation] 
                            where book_id = @bookid
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@bookid", bookid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                res += 1
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

    Public Shared Function getBorrowed(ByVal bookid As Integer) As Integer
        Dim res As Integer = 0
        Dim query = <sql>
                            SELECT id 
                            FROM [dbo].[Transaction] 
                            where book_id = @bookid
                        </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@bookid", bookid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                res += 1
            End While
            Return res
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return res
    End Function

End Class
