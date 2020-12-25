Imports System.Data.SqlClient
Public Class StaffProcess
    Public Shared DefaultPassword As String = "WelcomeStaff@890"
    Public Shared DefaultPasswordEnc As String = "$2a$09$lIC0zuuz4n/NcoxyuiG1Fe2tfJQbjom4GOeJwzNFJs4By9Hd3mXqu"

    ''' <summary>
    ''' Retrives staff with the given username from the db and passes the given password to bCrypt for verification.
    ''' If the password is correct that staff record is returned. Otherwise nothing is returned
    ''' </summary>
    ''' <param name="username">the email of the user</param>
    ''' <param name="pass">password they were given with</param>
    ''' <returns>populated staffmodel</returns>
    Public Shared Function AuthenticateStaff(ByVal username As String, ByVal pass As String) As StaffModel
        Dim records As New StaffModel
        Dim query As String
        query = <sql>
                       SELECT [Staff].id, [Staff].first_name, [Staff].last_name, [Staff].email, [Staff].dob, [Staff].contact, [Staff].employment_date, [Staff].employee_id, [StaffRole].role_name, [Staff].[password], [StaffRole].accessCode
                       FROM [dbo].[Staff]
		               INNER JOIN [StaffRole] ON [Staff].role = [StaffRole].id
		               WHERE [Staff].email = @email
                   </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@email", username)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                With records
                    .staffid = reader.Item(0)
                    .firstname = reader.Item(1)
                    .lastname = reader.Item(2)
                    .email = reader.Item(3)
                    .dob = reader.Item(4)
                    .contact = reader.Item(5)
                    .employdate = reader.Item(6)
                    .employid = reader.Item(7)
                    .role = reader.Item(8)
                    .password = reader.Item(9)
                    .accessCode = reader.Item(10)
                End With
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try



        If Not IsNothing(records.email) Then
            If BCrypt.Net.BCrypt.Verify(pass, records.password) Then Return records Else : Return Nothing
        Else
            Return Nothing
        End If


    End Function

    Public Shared Function getstaff(ByVal id As Integer) As StaffModel
        Dim query As String
        query = <sql>
                       SELECT [Staff].id, first_name, last_name, email, dob, contact, employment_date , employee_id , [StaffRole].role_name
                       FROM Staff INNER JOIN [StaffRole] ON [Staff].role = [StaffRole].id
                       WHERE [Staff].id = @sid 
                   </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@sid", id)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                Dim staff As New StaffModel With {
                .staffid = reader.Item(0),
                .firstname = reader.Item(1),
                .lastname = reader.Item(2),
                .email = reader.Item(3),
                .dob = reader.Item(4),
                .contact = reader.Item(5),
                .employdate = reader.Item(6),
                .employid = reader.Item(7),
                .role = reader.Item(8)
                }
                Return staff
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return Nothing

    End Function

    Public Shared Function findstaff(term As String) As List(Of StaffModel)
        Dim res As New List(Of StaffModel)
        Dim query As String
        query = <sql>
                       SELECT [Staff].id, first_name, last_name, email, dob, contact, employment_date , employee_id , [StaffRole].role_name
                       FROM Staff INNER JOIN [StaffRole] ON [Staff].role = [StaffRole].id
                       WHERE [Staff].first_name LIKE @term OR
                             [Staff].last_name  LIKE @term OR
                             [Staff].email  LIKE @term OR
                             [Staff].dob  LIKE @term OR
                             [Staff].contact  LIKE @term OR
                             [Staff].employment_date  LIKE @term OR
                             [Staff].employee_id  LIKE @term OR
                             [StaffRole].role_name  LIKE @term 
                   </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@term", term)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                Dim staff As New StaffModel With {
                .staffid = reader.Item(0),
                .firstname = reader.Item(1),
                .lastname = reader.Item(2),
                .email = reader.Item(3),
                .dob = reader.Item(4),
                .contact = reader.Item(5),
                .employdate = reader.Item(6),
                .employid = reader.Item(7),
                .role = reader.Item(8)
                }
                res.Add(staff)
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

        Return res

    End Function

    Public Shared Function getRoles() As List(Of String)
        Dim res As New List(Of String)
        Dim query As String
        query = <sql>
                       SELECT role_name FROM [StaffRole] WHERE accessCode BETWEEN 0 AND 1999
                   </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                res.Add(reader.Item(0))
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

        Return res
    End Function

    Public Shared Function getRole(ByVal staffid As Integer) As Integer
        Dim res As Integer = 10
        Dim query As String
        query = <sql>
                       SELECT [StaffRole].accessCode 
                       FROM [dbo].[Staff]
		               INNER JOIN [StaffRole] ON [Staff].role = [StaffRole].id
		               WHERE [Staff].id = @staffid
                   </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@staffid", staffid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                res = reader.Item(0)
                Exit While
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

        Return res
    End Function


    Public Shared Function InsertStaff(model As StaffModel) As QueryResultObj
        Dim query As String = <sql>
                                  INSERT INTO [STAFF] (first_name, last_name, email, dob, contact, employment_date, employee_id, [role], [password])
                                  VALUES (@firstname, @lastname, @email, @dob, @contact, @employdate, @employeeid, 
                                            (SELECT id FROM [StaffRole] WHERE role_name = @role),
                                            @pass
                                          )   
                              </sql>
        Try
            Using conn As SqlConnection = SqlDataAccess.getConnection
                conn.Open()
                Using cmd As SqlCommand = New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@firstname", model.firstname)
                    cmd.Parameters.AddWithValue("@lastname", model.lastname)
                    cmd.Parameters.AddWithValue("@email", model.email)
                    cmd.Parameters.AddWithValue("@dob", model.dob)
                    cmd.Parameters.AddWithValue("@contact", model.contact)
                    cmd.Parameters.AddWithValue("@employdate", model.employdate)
                    cmd.Parameters.AddWithValue("@employeeid", model.employid)
                    cmd.Parameters.AddWithValue("@role", model.role)
                    cmd.Parameters.AddWithValue("@pass", model.password)

                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return New QueryResultObj(True, "")
        Catch ex As Exception
            Return New QueryResultObj(False, ex.Message)
        End Try
    End Function

    Public Shared Function updateStaff(model As StaffModel) As QueryResultObj
        Dim query As String = <sql>
                                  UPDATE [Staff] SET 
                                                    first_name = @firstname, last_name = @lastname, 
                                                    email = @email, contact = @contact, 
                                                    [role] = (SELECT [StaffRole].id FROM [StaffRole] WHERE role_name = @rolename )
                                                 WHERE [Staff].id = @id
                              </sql>
        Try
            Using conn As SqlConnection = SqlDataAccess.getConnection
                conn.Open()
                Using cmd As SqlCommand = New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@firstname", model.firstname)
                    cmd.Parameters.AddWithValue("@lastname", model.lastname)
                    cmd.Parameters.AddWithValue("@email", model.email)
                    cmd.Parameters.AddWithValue("@contact", model.contact)
                    cmd.Parameters.AddWithValue("@rolename", model.role)
                    cmd.Parameters.AddWithValue("@id", model.staffid)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return New QueryResultObj(True, "")
        Catch ex As Exception
            Return New QueryResultObj(False, ex.Message)
        End Try
    End Function

    Public Shared Function resetpass(staffid As Integer) As Boolean
        Dim query As String = <sql>
                                  UPDATE [Staff] SET [password] = @pass
                                  WHERE [Staff].id = @id
                              </sql>
        Try
            Using conn As SqlConnection = SqlDataAccess.getConnection
                conn.Open()
                Using cmd As SqlCommand = New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@pass", DefaultPasswordEnc)
                    cmd.Parameters.AddWithValue("@id", staffid)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function deletestaff(id As Integer) As QueryResultObj
        Dim query As String = <sql>
                                  DELETE FROM [Staff] WHERE [Staff].id = @id
                              </sql>
        Try
            Using conn As SqlConnection = SqlDataAccess.getConnection
                conn.Open()
                Using cmd As SqlCommand = New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return New QueryResultObj(True, "")
        Catch ex As Exception
            Throw ex 'away
            'Return New QueryResultObj(False, ex.Message)
        End Try
    End Function


    Public Shared Function generateEmployeecard(ByVal contact As Long) As String
        Dim rnd = New Random(contact)
        Return "PCM" & rnd.Next
    End Function
End Class
