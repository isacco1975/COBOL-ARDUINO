Imports System.Configuration
Imports System.IO.Ports
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports Microsoft.VisualBasic

''
''*******************************************************
''* TCP Serial Driver                                   *
''*                                                     *
''*                         2024 - Isaac Garcia Peveri  *
''*                                                     *
''* --------------------------------------------------  *
''* SMTP SERVER                                         *
''* --------------------------------------------------  *
''*******************************************************
''
Module TCP_SerialDriver

#Region "WORKING-STORAGE"
    Private WithEvents serialPort As SerialPort
    Private comportName As String = String.Empty
    Private comportSpeed As Integer = 9600
    Private comportParity As Parity
    Private comportStopBits As Integer = 1
    Private comportDataBits As Integer = 8
    Private data As String = Nothing
    Private tcpPort As Integer = 64000
#End Region

    Private Sub ReadSettings()
        comportName = ConfigurationManager.AppSettings("ComportName")
        comportSpeed = CInt(ConfigurationManager.AppSettings("ComportSpeed"))
        comportParity = CInt(ConfigurationManager.AppSettings("ComportParity"))
        comportStopBits = CInt(ConfigurationManager.AppSettings("ComportStopBits"))
        comportDataBits = CInt(ConfigurationManager.AppSettings("ComportDataBits"))
        tcpPort = CInt(ConfigurationManager.AppSettings("TcpPort"))
    End Sub

    ''' <summary>
    ''' Sending data incoming via Client to serial port 
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <returns></returns>
    Private Function SendData(msg As String) As Boolean
        Try
            serialPort.Write(msg)
            Return True
        Catch ex As Exception
            serialPort.Close()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Main Logicv
    ''' </summary>
    Sub Main()
        ' Reading Serial Port Settings from Configuration, and creates the object
        ReadSettings()
        serialPort = New SerialPort(comportName, comportSpeed, comportParity, comportDataBits, comportStopBits)

        ' Data buffer for incoming data.
        Dim bytes() As Byte = New [Byte](1024) {}

        Dim ipHostInfo As IPHostEntry = Dns.Resolve(Dns.GetHostName())
        Dim localEndPoint As New IPEndPoint(ipHostInfo.AddressList(0), tcpPort)

        ' Create a TCP/IP socket.
        Dim listener As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

        ' Bind the socket to the local endpoint and listen for incoming connections.
        Try
            listener.Bind(localEndPoint)
            listener.Listen(50)

            ' Start listening for connections.
            While True
                Console.WriteLine(" ")
                Console.WriteLine("Server: Waiting for a connection...")
                Dim handler As Socket = listener.Accept()

                Console.WriteLine("Incoming connection ...")
                'Answering "Ready" (to talk with the client)
                handler.Send(Encoding.ASCII.GetBytes("220 Test SMTP Service ready" & vbCrLf))

                While True
                    bytes = New Byte(1024) {}
                    Dim bytesRec As Integer = handler.Receive(bytes)
                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec)
                    Console.WriteLine("Incoming data from client : {0}", data)

                    If Not data = String.Empty Then
                        'Process Commands
                        Dim command As String = data.Substring(0, 4).ToUpper
                        Select Case command
                            Case "HELO"
                                handler.Send(Encoding.ASCII.GetBytes("250 OK" & vbCrLf))
                            Case "MAIL"
                                handler.Send(Encoding.ASCII.GetBytes("250 OK" & vbCrLf))
                            Case "RCPT"
                                handler.Send(Encoding.ASCII.GetBytes("250 OK" & vbCrLf))
                            Case "DATA"
                                ' Answering the client to send body data
                                handler.Send(Encoding.ASCII.GetBytes("354 Start mail input; end with ." & vbCrLf))
                            Case "QUIT"
                                handler.Send(Encoding.ASCII.GetBytes("221 Service closing transmission channel" & vbCrLf))
                                Exit While
                            Case Else
                                serialPort.Open()

                                Console.WriteLine("Incoming TCP message: " & data.Replace(Convert.ToChar(0), ""))
                                SendData(data.Replace(Convert.ToChar(0), "").Split(vbCrLf)(6).Replace(vbLf, String.Empty))
                                handler.Send(Encoding.ASCII.GetBytes("250 OK" & vbCrLf))

                                serialPort.Close()
                        End Select
                    End If
                End While

                'Close Connection
                handler.Shutdown(SocketShutdown.Both)
                handler.Close()
            End While

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.WriteLine(ControlChars.Cr + "Press ENTER to continue...")
        Console.Read()
    End Sub

End Module
