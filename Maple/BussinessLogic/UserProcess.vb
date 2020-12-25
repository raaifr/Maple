Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class UserProcess
    'Welcome@maple123
    Private Shared defaultpassword As String = "$2a$09$UQzVN8/4sQ0Xas0sbx.wbOpQLlzYgYeADuCGdr4r33ZmUhhOITtWi"

    Public Shared Function finduser(ByVal term As String) As List(Of UserModel)
        Dim records As New List(Of UserModel)
        Dim query As String

        If isInt(term) Then
            query = <sql>
                       SELECT *
                       FROM [dbo].[User]
                       WHERE id = @term
                   </sql>.Value
        ElseIf isLong(term) Then
            query = <sql>
                       SELECT *
                       FROM [dbo].[User]
                       WHERE contact = @term
                   </sql>.Value
        Else
            term = "%" & term & "%"
            query = <sql>
                       SELECT *
                       FROM [dbo].[User]
                       WHERE first_name LIKE @term OR 
                             last_name LIKE @term OR
                             email LIKE @term OR
                             nic LIKE @term OR 
                             membership LIKE @term
                   </sql>.Value
        End If

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@term", term)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                Dim user As New UserModel
                With user
                    .userid = reader.Item(0)
                    .firstname = reader.Item(1)
                    .lastname = reader.Item(2)
                    .contact = reader.Item(3)
                    .email = reader.Item(4)
                    .country = reader.Item(5)
                    .nid = reader.Item(6)
                    '.Password = reader.Item(7)
                    .membership = reader.Item(8)
                    .dob = reader.Item(9)
                End With
                user.lastpayment = getLastPayment(user.userid)
                records.Add(user)
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

        Return records

    End Function


    ''' <summary>
    ''' Retrives User(s) with the given username from the db and passes the given password to bCrypt for verification.
    ''' If the password is correct that user record is returned. Otherwise nothing is returned
    ''' </summary>
    ''' <param name="username">the nic of the user</param>
    ''' <param name="pass">password they used to register with</param>
    ''' <returns>populated usermodel</returns>
    Public Shared Function AuthenticateUser(ByVal username As String, ByVal pass As String) As UserModel
        Dim records As New UserModel
        Dim query As String
        query = <sql>
                       SELECT *
                       FROM [dbo].[User]
                       WHERE nic = @nid
                   </sql>.Value
        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@nid", username)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                With records
                    .userid = reader.Item(0)
                    .firstname = reader.Item(1)
                    .lastname = reader.Item(2)
                    .contact = reader.Item(3)
                    .email = reader.Item(4)
                    .country = reader.Item(5)
                    .nid = reader.Item(6)
                    .Password = reader.Item(7)
                    .membership = reader.Item(8)
                    .dob = reader.Item(9)
                End With
                records.lastpayment = getLastPayment(records.userid)
                Exit While
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try



        If Not IsNothing(records.nid) Then
            If BCrypt.Net.BCrypt.Verify(pass, records.Password) Then Return records Else : Return Nothing
        Else
            Return Nothing
        End If


    End Function

    Public Shared Function getUser(ByVal userid As String) As UserModel
        Dim records As New UserModel
        Dim query As String
        query = <sql>
                       SELECT *
                       FROM [dbo].[User]
                       WHERE id = @userid
                   </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", userid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                With records
                    .userid = reader.Item(0)
                    .firstname = reader.Item(1)
                    .lastname = reader.Item(2)
                    .contact = reader.Item(3)
                    .email = reader.Item(4)
                    .country = reader.Item(5)
                    .nid = reader.Item(6)
                    .Password = reader.Item(7)
                    .membership = reader.Item(8)
                    .dob = reader.Item(9)
                End With

                records.lastpayment = getLastPayment(records.userid)
                Exit While
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

        If Not IsNothing(records) Then Return records Else : Return Nothing
    End Function

    Public Shared Function refUser(ByVal membercode As String) As UserModel
        Dim records As New UserModel
        Dim query As String
        query = <sql>
                       SELECT *
                       FROM [dbo].[User]
                       WHERE membership = @membercode
                   </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@membercode", membercode)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                With records
                    .userid = reader.Item(0)
                    .firstname = reader.Item(1)
                    .lastname = reader.Item(2)
                    .contact = reader.Item(3)
                    .email = reader.Item(4)
                    .country = reader.Item(5)
                    .nid = reader.Item(6)
                    .Password = reader.Item(7)
                    .membership = reader.Item(8)
                    .dob = reader.Item(9)
                End With
                records.lastpayment = getLastPayment(records.userid)
                Exit While
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

        If Not IsNothing(records) Then Return records Else : Return Nothing
    End Function

    Public Shared Function refUser2(ByVal nic As String) As UserModel
        Dim records As New UserModel
        Dim query As String
        query = <sql>
                       SELECT *
                       FROM [dbo].[User]
                       WHERE nic = @nic
                   </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@nic", nic)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                With records
                    .userid = reader.Item(0)
                    .firstname = reader.Item(1)
                    .lastname = reader.Item(2)
                    .contact = reader.Item(3)
                    .email = reader.Item(4)
                    .country = reader.Item(5)
                    .nid = reader.Item(6)
                    .Password = reader.Item(7)
                    .membership = reader.Item(8)
                    .dob = reader.Item(9)
                End With
                records.lastpayment = getLastPayment(records.userid)
                Exit While
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

        If Not IsNothing(records) Then Return records Else : Return Nothing
    End Function

    ''' <summary>
    ''' Creates a new user in the database table[users]. if the membership id is null one is generated and then filled in before record creation
    ''' </summary>
    ''' <param name="firstname">First name</param>
    ''' <param name="lastname">Last name</param>
    ''' <param name="contact">phone number that can be contacted incase the library needs to call the person</param>
    ''' <param name="email">email address used for library communications such as notifications and password reset</param>
    ''' <param name="country">Country</param>
    ''' <param name="nid">national ID of the person</param>
    ''' <param name="password">chosed password by the user</param>
    ''' <param name="memberid">memberhsip id, this is auto generated by the system at either this function or before calling this function.</param>
    ''' <returns></returns> 
    Public Shared Function InsertUser(ByVal firstname As String,
                                      ByVal lastname As String,
                                      ByVal contact As Long,
                                      ByVal email As String,
                                      ByVal country As String,
                                      ByVal nid As String,
                                      ByVal password As String,
                                      ByVal memberid As String,
                                      ByVal dob As Date) As Boolean


        Dim dataUserModel As UserModel = New UserModel
        With dataUserModel
            .firstname = firstname
            .lastname = lastname
            .contact = contact
            .email = email
            .country = country
            .nid = nid
            .dob = dob
            .Password = BCrypt.Net.BCrypt.HashPassword(password)
        End With
        If memberid = "" Then dataUserModel.membership = generatekey(firstname, lastname, contact, nid) Else dataUserModel.membership = memberid

        Dim sql As String = <sql>
                                INSERT INTO [dbo].[User] (first_name, last_name, contact, email, country, nic, password, membership, dob)
                                VALUES(@firstname,  @lastname, @contact , @email, @country , @nid , @Password, @membership, @dob)
                            </sql>.Value

        Return SqlDataAccess.saveData(sql, dataUserModel)
    End Function

    ''' <summary>
    ''' This is called incase the user has forgotten the password. this function is onyl called after verifying the user via email or phone or member card.
    ''' </summary>
    ''' <param name="id">userid</param>
    ''' <param name="pass">new password</param>
    ''' <returns></returns>
    Public Shared Function UpdateUserPass(ByVal id As String, ByVal pass As String) As Integer
        Dim query As String = <sql>
                                UPDATE [dbo].[User]
                                SET password = @Password
                                WHERE id = @userid
                            </sql>.Value
        pass = BCrypt.Net.BCrypt.HashPassword(pass)

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", id)
        cmd.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(pass))

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

    End Function

    Public Shared Function UpdateUserEmail(ByVal id As String, ByVal email As String) As Integer
        Dim query As String = <sql>
                                UPDATE [dbo].[User]
                                SET email = @email
                                WHERE id = @id
                            </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", id)
        cmd.Parameters.AddWithValue("@email", email)

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

    End Function

    Public Shared Function UpdateUserContact(ByVal id As String, ByVal contact As Long) As Integer
        Dim query As String = <sql>
                                UPDATE [dbo].[User]
                                SET contact = @contact
                                WHERE id = @id
                            </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", id)
        cmd.Parameters.AddWithValue("@contact", contact)

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

    End Function

    ''' <summary>
    ''' Fetch the membership id. for qr code regeneration or member card rewrite.
    ''' </summary>
    ''' <param name="nic">National ID number</param>
    ''' <returns>the membership id stored in the db</returns>
    Public Shared Function getMembershipNumber(ByVal nic As String) As String
        Dim records As List(Of String) = New List(Of String)
        Dim query As String
        query = <sql>
                       SELECT membership
                       FROM [dbo].[User]
                       WHERE nic LIKE @nid
                   </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@nid", nic)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While (reader.Read)
                records.Add(reader(0))
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

        If records.Count > 0 Then Return records(0) Else Return ""
    End Function


    Public Shared Function getPaymenthistory(userid As String) As List(Of PaymentModel)
        Dim records As New List(Of PaymentModel)
        Dim query As String
        query = <sql>
                    SELECT [Payment].[id], [Payment].[user_id], [PaymentType].[payment_name], [Payment].[payment_date], [Payment].[amount], [Payment].[remarks]
                    FROM [Payment] INNER JOIN [PaymentType] 
                    ON [Payment].payment_type = [PaymentType].Id
                    WHERE [Payment].[user_id] = @userid
                </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", userid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            ' [Payment].[id], [Payment].[user_id], [PaymentType].[payment_name], [Payment].[payment_date], [Payment].amount, [Payment].remarks
            While reader.Read()
                Dim inv As New PaymentModel With {
                            .id = reader.Item(0),
                            .userid = reader.Item(1),
                            .paymentType = reader.Item(2),
                            .paymentDate = reader.Item(3),
                            .amount = reader.Item(4)
                            }
                If IsDBNull(reader.Item(5)) Then inv.remarks = "" Else inv.remarks = reader.Item(5)
                records.Add(inv)
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try


        Return records

    End Function

    Public Shared Function getLastPayment(userid As String) As Date
        Dim query As String
        query = <sql>
                    SELECT [Payment].[payment_date] 
                    FROM [Payment]
                    WHERE [Payment].[user_id] = @userid AND [Payment].payment_type = (SELECT [id] FROM [PaymentType] WHERE [payment_name] = 'Membership')
                    ORDER BY [Payment].payment_date DESC
                </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", userid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader

            While reader.Read()
                'return first item since the most recent date is at the top from ORDER BY cmd
                Return reader.Item(0)
            End While
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try
        Return Nothing

    End Function

    Public Shared Function addPayment(ByVal userid As String, ByVal paymentType As String, ByVal paymentdate As Date, ByVal amnt As Double, ByVal remarks As String) As Integer
        Dim query As String
        query = <sql>
                    INSERT INTO [Payment] ([user_id] , payment_type, payment_date, amount, remarks)
                    VALUES (@userid,
                            (SELECT id FROM [PaymentType] WHERE payment_name = @payType),
                            @paymentdate, @amount, @remarks)
                </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", userid)
        cmd.Parameters.AddWithValue("@payType", paymentType)
        cmd.Parameters.AddWithValue("@paymentdate", paymentdate)
        cmd.Parameters.AddWithValue("@amount", amnt)
        If remarks = "" Then
            cmd.Parameters.AddWithValue("@remarks", DBNull.Value)
        Else
            cmd.Parameters.AddWithValue("@remarks", remarks)
        End If

        Try
            conn.Open()
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

    End Function

    ''' <summary>
    ''' Generates a membership id that can be used in qr codes and rfid. 
    ''' </summary>
    ''' <returns>alphanumeric character string spanning 20chars</returns>
    Public Shared Function generatekey(ByVal firstname As String, ByVal lastname As String, ByVal contact As Long, ByVal nic As String) As String
        Dim AllowedChars() As String = New String() {firstname, lastname, contact, nic}
        Dim ret As String = String.Empty
        Dim len = 20
        Dim seed As Integer = Num(contact & nic)
        Dim rnd = New Random(seed)

        'if no len is given then a random is chosen
        While ret.Length < len
            Dim rndSet As Integer = rnd.Next(0, AllowedChars.Length)
            ret &= AllowedChars(rndSet).Substring(rnd.Next(0, AllowedChars(rndSet).Length), 1)
        End While

        Return ret
    End Function

    Public Shared Function Num(ByVal value As String) As Integer
        Dim returnVal As String = String.Empty
        Dim collection As MatchCollection = Regex.Matches(value, "\d+")
        For Each m As Match In collection
            returnVal += m.ToString()
        Next
        'the resulting string is cut down to 8 digits
        'to prevent crossing int32 limit of 2,147,483,647 and still maintain a relatively unique number
        Return Convert.ToInt32(returnVal.Substring(0, 7))
    End Function

    Public Shared Function isInt(s As String) As Boolean
        Try
            Dim g As Integer = Integer.Parse(s)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function isLong(s As String) As Boolean
        Try
            Dim g As Long = Long.Parse(s)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
