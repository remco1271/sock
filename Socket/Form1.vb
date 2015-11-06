Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1
    Dim serverSocket As Socket      'voor luisteren
    Dim clientSocket As Socket      'voor praten naar clients
    Dim byteData(1023) As Byte      'text die door client is verzonden
    Public Class StateObject
        ' Client  socket.
        Public workSocket As Socket = Nothing
        ' Size of receive buffer.
        Public Const BufferSize As Integer = 1024
        ' Receive buffer.
        Public buffer(BufferSize) As Byte
        ' Received data string.
        Public sb As New StringBuilder
    End Class 'StateObject
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tStatus.Text = "Please start server..."
        Dim cultureInfo As System.Globalization.CultureInfo
        cultureInfo = New System.Globalization.CultureInfo("en-US")
        Thread.CurrentThread.CurrentUICulture = cultureInfo 'zet hoofdtaal van main tread naar en-US
    End Sub
    Private Sub OnAccept(ByVal ar As IAsyncResult)
        tStatus.Text = "Client connect aanvraag komt binnen..."
        clientSocket = serverSocket.EndAccept(ar)
        serverSocket.BeginAccept(New AsyncCallback(AddressOf OnAccept), Nothing)
        AddClient(clientSocket)
        Dim listener As Socket = CType(ar.AsyncState, Socket)
        'Dim handler As Socket = listener.EndAccept(ar)

        ' Create the state object for the async receive.
        Dim state As New StateObject
        state.workSocket = clientSocket
        'clientSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReadCallback), state)
        clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                                  New AsyncCallback(AddressOf OnRecieve), clientSocket)

    End Sub

    Delegate Sub _AddClient(ByVal client As Socket) 'delege used to invoke AddCLient()
    Private Sub AddClient(ByVal client As Socket)
        'the listview was not made on the listening
        'thread so we need to invoke this method
        'before using it
        If InvokeRequired Then
            Invoke(New _AddClient(AddressOf AddClient), client)
            Exit Sub
        End If
        tStatus.Text = "Client verbonden met ip: " + client.LocalEndPoint.ToString + " remote adress: " + client.RemoteEndPoint.ToString
        'create a new listview item and add the IP to the list
        Dim lvi As New ListViewItem(client.LocalEndPoint.ToString)
        '.tag is an object that can be set to anything
        'here we are placeing our socket there for use later
        lvi.Tag = client
        'add to the list
        lsvClients.Items.Add(lvi)
    End Sub

    Private Sub OnRecieve(ByVal ar As IAsyncResult)
        Dim client As Socket = ar.AsyncState
        If (clientSocket.Connected = False) AndAlso (clientSocket.Available = 0) Then
            Exit Sub 'sub eindigt hier en wordt niet verder meer uitgevoerd.
        End If
        If (client.Connected = False) AndAlso (client.Available = 0) Then
            Exit Sub 'sub eindigt hier en wordt niet verder meer uitgevoerd.
        End If
        Try
            client.EndReceive(ar)
            Dim bytesRec As Byte() = byteData
            Dim Part As String
            Dim Alles As String = String.Empty
            For Each b As Byte In bytesRec 'data bij elkaar voegen om te kunnen uitlezen of de data leeg is of niet
                Part = Conversion.Hex(b)
                If Part.Length = 1 Then Part = "0" & Part
                Alles = Alles + Part
            Next
            Debug.WriteLine(Alles)
            Debug.WriteLine("")
            'controleer of alle 1023 byte allemaal 0 zijn dan is de client er niet meer.
            'vookomt laatse waarde loop die dan oneindig is totdat er een client weer verbind.
            If (Alles = "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000") Then
                Debug.WriteLine("client is niet meer verbonden ontvangt alles 0")
                Exit Sub 'sub eindigt hier en wordt niet verder meer uitgevoerd en client is nu helemaal verbroken aan beide kanten.
            End If
            Dim message As String = System.Text.ASCIIEncoding.ASCII.GetString(bytesRec) 'tekst van byte naar string omzetten
            Read(message, clientSocket.LocalEndPoint.ToString) 'naar sub voor textbox sturen
            message = String.Empty 'maak bericht leeg om mogenlijke data loop te voorkomen
            ReDim byteData(1023) ' zeer belangrijk anders kan je client disconnect niet detecteren en heb je data loop in ontvangen als client disconnect
            clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                                      New AsyncCallback(AddressOf OnRecieve), clientSocket) 'wacht op volgend bericht
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub Send(ByVal msg As String, ByVal client As Socket)
        'get bytes to send
        Dim sendBytes As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(msg)
        'begin sending (notice the client is sent as an AsyncState)
        client.BeginSend(sendBytes, 0, sendBytes.Length, SocketFlags.None, New AsyncCallback(AddressOf OnSend), client)
    End Sub
    Private Sub OnSend(ByVal ar As IAsyncResult)
        'create a temp socket to use for our client
        Dim client As Socket = ar.AsyncState
        client.EndSend(ar)
    End Sub
    Private Sub SendMessageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMessageToolStripMenuItem.Click
        'send message to the selected client
        For i = 0 To lsvClients.SelectedItems.Count - 1
            Try
                tStatus.Text = "Server verstuurt tekst naar:  " + lsvClients.SelectedItems(i).Text + " met text: " + tbSend.Text
                Send(tbSend.Text + vbCrLf, lsvClients.SelectedItems(i).Tag) 'naar client verzenden met behult van tag
            Catch ex As Exception

                If ex.HResult = -2147467259 Then
                    MsgBox("Client is not more connected and removed from list", , "Client not found")
                    lsvClients.SelectedItems(i).Remove()
                Else
                    MessageBox.Show(ex.Message)
                End If
            End Try
        Next

    End Sub

    Public Shared allDone As New ManualResetEvent(False)
    Private Function Start2() 'word uitgevoerd door de start knop.
        'maak socket
        serverSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        'waar moet server luisteren
        Dim IpEndPoint As IPEndPoint = New IPEndPoint(IPAddress.Any, 8800)
        Dim listener = serverSocket
        'bind the endpoint to the serversocket before listening
        serverSocket.Bind(IpEndPoint)
        'start listening
        serverSocket.Listen(5)
        'begin accepting clients voor toevoegen
        'serverSocket.BeginAccept(New AsyncCallback(AddressOf OnAccept), serverSocket)
        serverSocket.BeginAccept(New AsyncCallback(AddressOf OnAccept), serverSocket)
        Return 0

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Start Server" Then
            Start2()
            tStatus.Text = "Server gestart"
            Button2.Text = "Stop Server" 'disable start knop
            Timer1.Enabled = True
            Button2.Enabled = False
        Else
            Button2.Text = "Start Server"
            clientSocket.Shutdown(SocketShutdown.Both) 'server kan niet herstarten
            clientSocket.Close()
            Try
                tStatus.Text = "Server gestopt"
                clientSocket.Shutdown(SocketShutdown.Both)
                clientSocket.Close()
                clientSocket.Dispose()
                serverSocket.Shutdown(SocketShutdown.Both)
                serverSocket.Close()
                serverSocket.Dispose()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "error")
            End Try
        End If

    End Sub
    Dim lezen As New _Read(AddressOf Read)
    Private Delegate Sub SetTextCallback(ByVal text As String)
    Delegate Sub _Read(ByVal msg As String, ByVal ip As String) 'delege used to invoke
    Private Sub Read(ByVal msg As String, ByVal ip As String)
        Dim text As String = ip.Trim() + " " + msg.Trim()
        If Me.RichTextBox1.InvokeRequired Then 'als invoke moet om de ontvangen data in de textbox te zetten dan voert het de invoke uit.
            Dim d As New _Read(AddressOf Read) '
            Debug.WriteLine("Invoke!")
            Me.Invoke(d, New Object() {msg, ip}) 'invoke eigen sub Read om data naar textbox te zetten en gaat dan else uivoeren.
            'Exit Sub 'gebruiken?
        Else 'als de invoke gedaan is voert het dit uit.
            If text = "" Then 'alleen data invoeren als er text is.
            Else
                RichTextBox1.AppendText(text)
                RichTextBox1.Text = Regex.Replace(RichTextBox1.Text, "\r\n\r\n", vbNewLine) 'replace om de rare spaties die ontstaan uit het niet!
                'RichTextBox1.Text = Regex.Replace(RichTextBox1.Text, "\r\r", vbNewLine)     'deze optie komt niet voor
                'RichTextBox1.Text = Regex.Replace(RichTextBox1.Text, "\r\n\n", vbNewLine) 'deze optie nog niet getest!
                RichTextBox1.Text = Regex.Replace(RichTextBox1.Text, "\n\n", vbNewLine) 'replace om de rare spaties die ontstaan uit het niet!
                RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                RichTextBox1.ScrollToCaret() 'scrolen naar beneden waar de tekst is toegevoegt
            End If
        End If
        Debug.WriteLine("tekst: " + msg + ", ip: " + ip)
    End Sub

    Delegate Sub _remove(ByVal client As Socket)

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'timer controlleerd elke 2 seconden of de client er nog is
        Dim i As Integer = 0
        For Each item In lsvClients.Items
            Try
                Send("a:", lsvClients.Items.Item(i).Tag)
            Catch ex As Exception
                tStatus.Text = "Client " + lsvClients.Items.Item(i).Text + " is disconnected"
                lsvClients.Items.Item(i).Remove()
                If Not ex.HResult = -2147467259 Then
                    Debug.WriteLine("Connection error: " + ex.Message + " HResult: " + ex.HResult.ToString)
                    MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "Error with connection to a client")
                Else
                    Debug.WriteLine("client has been disconnected")
                End If

            End Try
            i = i + 1
        Next
    End Sub
End Class

