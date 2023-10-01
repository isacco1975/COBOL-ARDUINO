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
    Private httpDataOnly As Int16 = 0
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
        Console.WriteLine(" NAME: " & serialPort.PortName)
        Console.WriteLine(" RATE: " & serialPort.BaudRate)
        Console.WriteLine(" DB:   " & serialPort.DataBits)

        While True
            Continue While
        End While
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
        httpDataOnly = CInt(ConfigurationManager.AppSettings("httpDataOnly"))
        tcpPort = CInt(ConfigurationManager.AppSettings("TcpPort"))
    End Sub

    ''' <summary>
    ''' Main Thread
    ''' </summary>
    ''' <param name="arg"></param>
    Private Sub Main_Thread(arg As Object)
        serialPort = New SerialPort(ComportName, ComportSpeed, ComportParity, ComportDataBits, ComportStopBits)
    End Sub

    ''' <summary>
    ''' Sending data incoming via Client to serial port 
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <returns></returns>
    Private Function SendData(msg As String) As Boolean
        Dim httpData As String = msg

        Try
            If httpDataOnly = "1" Then
                Dim split As String() = msg.Split(vbCrLf)
                httpData = split(split.Length - 1)
            End If

            serialPort.Write(httpData)
            serialPort.Close()
        Catch ex As Exception
            serialPort.Close()
        End Try
    End Function

    ''' <summary>
    ''' Thread Event DataReceived
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles serialPort.DataReceived
        inSerialData = serialPort.ReadExisting()

        If tcpServer IsNot Nothing Then
            SendMessage(tcpServer.listClient, inSerialData)
        End If
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
            serialPort.Open()
            Console.WriteLine("Incoming TCP message: " & str.Replace(Convert.ToChar(0), ""))
            SendData(str.Replace(Convert.ToChar(0), ""))
            Thread.Sleep(500)

            If httpDataOnly = "1" Then
                rClient.Close()
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
