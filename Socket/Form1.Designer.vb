<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Connected Clients", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Disconnected Clients", System.Windows.Forms.HorizontalAlignment.Left)
        Me.SendMessageToolStripMenuItem = New System.Windows.Forms.Button()
        Me.tbSend = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lsvClients = New System.Windows.Forms.ListView()
        Me.SessionId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MachineId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SendMessageToolStripMenuItem
        '
        Me.SendMessageToolStripMenuItem.Location = New System.Drawing.Point(6, 39)
        Me.SendMessageToolStripMenuItem.Name = "SendMessageToolStripMenuItem"
        Me.SendMessageToolStripMenuItem.Size = New System.Drawing.Size(259, 23)
        Me.SendMessageToolStripMenuItem.TabIndex = 0
        Me.SendMessageToolStripMenuItem.Text = "Verzenden"
        Me.SendMessageToolStripMenuItem.UseVisualStyleBackColor = True
        '
        'tbSend
        '
        Me.tbSend.Location = New System.Drawing.Point(110, 13)
        Me.tbSend.Name = "tbSend"
        Me.tbSend.Size = New System.Drawing.Size(155, 20)
        Me.tbSend.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Bericht naar client: "
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 268)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(659, 22)
        Me.StatusStrip1.TabIndex = 9
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tStatus
        '
        Me.tStatus.Name = "tStatus"
        Me.tStatus.Size = New System.Drawing.Size(120, 17)
        Me.tStatus.Text = "ToolStripStatusLabel1"
        '
        'Status
        '
        Me.Status.Text = "Status"
        Me.Status.Width = 116
        '
        'lsvClients
        '
        Me.lsvClients.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Status, Me.SessionId, Me.MachineId})
        ListViewGroup1.Header = "Connected Clients"
        ListViewGroup1.Name = "ConnectedClients"
        ListViewGroup2.Header = "Disconnected Clients"
        ListViewGroup2.Name = "DisconnectedClients"
        Me.lsvClients.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lsvClients.Location = New System.Drawing.Point(13, 7)
        Me.lsvClients.Name = "lsvClients"
        Me.lsvClients.Size = New System.Drawing.Size(259, 113)
        Me.lsvClients.TabIndex = 8
        Me.lsvClients.UseCompatibleStateImageBehavior = False
        Me.lsvClients.View = System.Windows.Forms.View.Details
        '
        'SessionId
        '
        Me.SessionId.Text = "SessionId"
        '
        'MachineId
        '
        Me.MachineId.Text = "MachineId"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.SendMessageToolStripMenuItem)
        Me.GroupBox1.Controls.Add(Me.tbSend)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 188)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(287, 71)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Verzend tekst"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(309, 7)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(338, 198)
        Me.RichTextBox1.TabIndex = 11
        Me.RichTextBox1.Text = ""
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(13, 141)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 10
        Me.Button2.Text = "Start Server"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 2000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(659, 290)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lsvClients)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.Button2)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SendMessageToolStripMenuItem As Button
    Friend WithEvents tbSend As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents tStatus As ToolStripStatusLabel
    Friend WithEvents Status As ColumnHeader
    Friend WithEvents lsvClients As ListView
    Friend WithEvents SessionId As ColumnHeader
    Friend WithEvents MachineId As ColumnHeader
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Timer1 As Timer
End Class
