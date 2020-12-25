﻿Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class UserProcess

    ''' <summary>
    ''' Retrives User(s) with the given username from the db and passes the given password to bCrypt for verification.
    ''' If the password is correct that user record is returned. Otherwise nothing is returned
    ''' </summary>
    ''' <param name="username">the nic of the user</paraam>
    ''' <param name="pass">password they used to register with</param>
    ''' <returns>populated usermodel</returns>
    Public Shared Function AuthenticateUser(ByVal username As String, ByVal pass As String) As dil_UserModel
        Dim records As List(Of dil_UserModel) = New List(Of dil_UserModel)
        Dim query As String
        query = <sql>
                       SELECT *
                       FROM dbo.User
                       WHERE nic = @nid
                   </sql>.Value
        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@nid", username)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            records = SqlDataAccess.DataReaderMapToList(Of dil_UserModel)(reader)
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try



        If records.Count > 0 Then
            For Each rc As dil_UserModel In records
                If BCrypt.Net.BCrypt.Verify(pass, rc.Password) Then Return rc
            Next
        End If
        Return Nothing


    End Function

    Public Shared Function getUser(ByVal userid As String) As dil_UserModel
        Dim records As List(Of dil_UserModel) = New List(Of dil_UserModel)
        Dim query As String
        query = <sql>
                       SELECT *
                       FROM dbo.User 
                       WHERE id = @userid
                   </sql>.Value

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", userid)

        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            records = SqlDataAccess.DataReaderMapToList(Of dil_UserModel)(reader)
        Catch ex As Exception
            Throw ex 'away
        Finally
            conn.Close()
        End Try

        If records.Count > 0 Then Return records(0) Else Return Nothing
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
                                      ByVal contact As Integer,
                                      ByVal email As String,
                                      ByVal country As String,
                                      ByVal nid As String,
                                      ByVal password As String,
                                      ByVal memberid As String,
                                      ByVal dob As Date) As Boolean


        Dim dataUserModel As dil_UserModel = New dil_UserModel
        With dataUserModel
            .firstname = firstname
            .lastname = lastname
            .contact = contact
            .email = email
            .country = country
            .nid = nid
            .Password = BCrypt.Net.BCrypt.HashPassword(password)
        End With
        If memberid = "" Then dataUserModel.membership = generatekey(firstname, lastname, contact, nid) Else dataUserModel.membership = memberid

        Dim sql As String = <sql>
                                INSERT INTO dbo.User (first_name, last_name, contact, email, country, nic, password, membership, dob)
                                VALUES(@firstname,  @lastname, @contact , @email, @country , @nid , @Password, @membership @dob)
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
                                UPDATE dbo.User
                                SET password = @userid
                                WHERE id = @Password
                            </sql>.Value
        pass = BCrypt.Net.BCrypt.HashPassword(pass)

        Dim conn As SqlConnection = SqlDataAccess.getConnection
        Dim cmd As SqlCommand = New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@userid", id)
        cmd.Parameters.AddWithValue("@Password", pass)

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
                       FROM dbo.User 
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

    ''' <summary>
    ''' Generates a membership id that can be used in qr codes and rfid. 
    ''' </summary>
    ''' <returns>alphanumeric character string spanning 20chars</returns>
    Public Shared Function generatekey(ByVal firstname As String, ByVal lastname As String, ByVal contact As String, ByVal nic As String) As String
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

    Private Shared Function Num(ByVal value As String) As Integer
        Dim returnVal As String = String.Empty
        Dim collection As MatchCollection = Regex.Matches(value, "\d+")
        For Each m As Match In collection
            returnVal += m.ToString()
        Next
        'the resulting string is cut down to 8 digits
        'to prevent crossing int32 limit of 2,147,483,647 and still maintain a relatively unique number
        Return Convert.ToInt32(returnVal.Substring(0, 7))
    End Function

End Class