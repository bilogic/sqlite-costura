Imports System.Data.SQLite
Imports System.IO

Module Module1

    Private Const CREATE_TEST_TABLE_QUERY As String = "CREATE TABLE IF NOT EXISTS [TestTable] (
               [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
               [FirstName] NVARCHAR(64) NULL,
               [LastName] NVARCHAR(64) NULL)"

    Private Const DATABASE_FILE As String = "EmbeddedSQLiteDemo.db"
    Public Sub Main()
        If File.Exists(DATABASE_FILE) Then
            File.Delete(DATABASE_FILE)
            SQLiteConnection.CreateFile(DATABASE_FILE)
        End If

        Using connection = New SQLiteConnection($"data source={DATABASE_FILE};Version=3;New=True;Compress=True;")

            Using command = New SQLiteCommand(connection)
                connection.Open()
                command.CommandText = CREATE_TEST_TABLE_QUERY
                command.ExecuteNonQuery()
                command.CommandText = "INSERT INTO [TestTable] (FirstName, LastName) VALUES ('John', 'Snow')"
                command.ExecuteNonQuery()
                command.CommandText = "INSERT INTO [TestTable] (FirstName, LastName) VALUES ('Alice', 'Cooper')"
                command.ExecuteNonQuery()
                command.CommandText = "INSERT INTO [TestTable] (FirstName, LastName) VALUES ('Suzane', 'Thompson')"
                command.ExecuteNonQuery()
                command.CommandText = "SELECT * FROM [TestTable]"

                Using reader = command.ExecuteReader()

                    While reader.Read()
                        Console.WriteLine(reader("Id") & " : " + reader("FirstName") & " " + reader("LastName"))
                    End While
                End Using

                connection.Close()
            End Using
        End Using

        Console.WriteLine()
        Console.WriteLine("Press <Enter> key to exit.")
        Console.ReadLine()
    End Sub

End Module
