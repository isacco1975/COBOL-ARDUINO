Imports System.Configuration
Imports System.IO.Ports
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
'
'*******************************************************
'* TCP Serial Driver                                   *
'*                                                     *
'*                         2023 - Isaac Garcia Peveri  *
'*                                                     *
'* --------------------------------------------------  *
'* A Multi R=Thread Serial Driver using TCP connections*
'* --------------------------------------------------  *
'*******************************************************
'
Module TCP_SerialDriver

#Region "WORKING-STORAGE"
    Private mainThread As Thread = Nothing
    Private tcpThread As Thread = Nothing
    Private listener As TcpListener
    Private clientList As New List(Of TCP_Server)
    Private tcpServer As TCP_Server
    Private inSerialData As String = String.Empty
    Private tcpPort As Integer = 64000
    Private WithEvents serialPort As SerialPort
    Private comportName As String = String.Empty
    Private comportSpeed As Integer = 9600
    Private comportParity As Parity
    Private comportStopBits As Integer = 1
    Private comportDataBits As Integer = 8
#End Region

    ''' <summary>
    ''' MAIN ROUTINE
    ''' </summary>
    Sub Main()
        ReadSettings()

        mainThread = New Thread(AddressOf Main_Thread)
        mainThread.Start()

        listener = New TcpListener(IPAddress.Any, tcpPort)
        listener.Start()

        tcpThread = New Thread(AddressOf TCP_Thread)
        tcpThread.Start()

        Console.WriteLine(" TCP SERIAL DRIVER STARTED: WAITING CONNECTIONS ")
        Console.ReadLine()
    End Sub

    ''' <summary>
    ''' Initialize Application Settings
    ''' </summary>
    Private Sub ReadSettings()
        comportName = ConfigurationManager.AppSettings("ComportName")
        comportSpeed = CInt(ConfigurationManager.AppSettings("ComportSpeed"))
        comportParity = CInt(ConfigurationManager.AppSettings("ComportParity"))
        comportStopBits = CInt(ConfigurationManager.AppSettings("ComportStopBits"))
        comportDataBits = CInt(ConfigurationManager.AppSettings("ComportDataBits"))
        tcpPort = CInt(ConfigurationManager.AppSettings("TcpPort"))
    End Sub

    ''' <summary>
    ''' Main Thread
    ''' </summary>
    ''' <param name="arg"></param>
    Private Sub Main_Thread(arg As Object)
        serialPort = New SerialPort(comportName, comportSpeed, comportParity, comportDataBits, comportStopBits)

        Try
            serialPort.Open()

        Catch Ex As Exception
            Throw Ex
        End Try
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
    ''' Routine to Read Data from Serial Port
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ForwardSerialPortIncomingData()
        If tcpServer IsNot Nothing Then
            SendMessage(tcpServer.listClient, inSerialData)
        End If
    End Sub

    ''' <summary>
    ''' On Data Received
    ''' </summary>
    Private Sub OnDataReceived(Sender As Object, e As SerialDataReceivedEventArgs) Handles serialPort.DataReceived
        inSerialData = serialPort.ReadExisting()
    End Sub

    ''' <summary>
    ''' TCP thread handling tcp events, connections and so on
    ''' </summary>
    ''' <param name="arg"></param>
    Private Sub TCP_Thread(arg As Object)
        Try
            listener.BeginAcceptTcpClient(New AsyncCallback(AddressOf AcceptClient), listener)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' A new Client connects to the server
    ''' </summary>
    ''' <param name="ar"></param>
    Public Sub AcceptClient(ByVal ar As IAsyncResult)
        Try
            If listener.Server.IsBound Then
                tcpServer = New TCP_Server(listener.EndAcceptTcpClient(ar))

                AddHandler(tcpServer.getMessage), AddressOf MessageReceived
                AddHandler(tcpServer.clientLogout), AddressOf ClientExited
                clientList.Add(tcpServer)
                Console.WriteLine("... A client connected")

                listener.BeginAcceptTcpClient(New AsyncCallback(AddressOf AcceptClient), listener)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Event handler for incoming messages from Client
    ''' </summary>
    ''' <param name="rClient"></param>
    ''' <param name="str"></param>
    Private Sub MessageReceived(ByRef rClient As TcpClient, str As String)
        Try
            Console.WriteLine("Incoming TCP message: " & str.Replace(Convert.ToChar(0), ""))

            If str.Replace(Convert.ToChar(0), "").ToLower = "getdata" Then
                ForwardSerialPortIncomingData()
            End If

            If str.Replace(Convert.ToChar(0), "").ToLower.Contains("senddata") Then
                SendData(str.Replace(Convert.ToChar(0), "").Split(":")(1))
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Sending data to the client
    ''' </summary>
    ''' <param name="rClient"></param>
    ''' <param name="str"></param>
    Private Sub SendMessage(ByRef rClient As TcpClient, str As String)
        Try
            Dim myBytes As Byte() = New Byte() {}
            myBytes = System.Text.Encoding.ASCII.GetBytes(str)
            rClient.GetStream().Write(myBytes, 0, myBytes.Length)
            rClient.GetStream().Flush()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Disconnection
    ''' </summary>
    ''' <param name="client"></param>
    Sub ClientExited(ByVal client As TCP_Server)
        clientList.Remove(client)
        Console.WriteLine("... A client disconnected")
    End Sub

End Module
