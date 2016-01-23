<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKiosCapture
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKiosCapture))
        Me.pcCapture = New System.Windows.Forms.PictureBox
        Me.pbReCapture = New System.Windows.Forms.PictureBox
        Me.pbCapture = New System.Windows.Forms.PictureBox
        Me.pbSave = New System.Windows.Forms.PictureBox
        Me.pbClose = New System.Windows.Forms.PictureBox
        Me.pnlCusInfo = New System.Windows.Forms.Panel
        Me.DisplayMobileStatus = New System.Windows.Forms.Label
        Me.lblMobileStatus = New System.Windows.Forms.Label
        Me.DisplayCategory = New System.Windows.Forms.Label
        Me.lblCategory = New System.Windows.Forms.Label
        Me.DisplayContactID = New System.Windows.Forms.Label
        Me.lblContactID = New System.Windows.Forms.Label
        Me.DisplayBillingSystem = New System.Windows.Forms.Label
        Me.lblBiilingSystem = New System.Windows.Forms.Label
        Me.DisplayNetworkType = New System.Windows.Forms.Label
        Me.lblNetworkType = New System.Windows.Forms.Label
        Me.DisplayMobileNo = New System.Windows.Forms.Label
        Me.DisplayLastDate = New System.Windows.Forms.Label
        Me.DisplayImage = New System.Windows.Forms.Label
        Me.DisplaySegment = New System.Windows.Forms.Label
        Me.DisplayPAGroup = New System.Windows.Forms.Label
        Me.lblMobileNo = New System.Windows.Forms.Label
        Me.lblLastDate = New System.Windows.Forms.Label
        Me.lblImage = New System.Windows.Forms.Label
        Me.lblSegment = New System.Windows.Forms.Label
        Me.lblPAGroup = New System.Windows.Forms.Label
        Me.btnDiaplayImage = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        CType(Me.pcCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbReCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCusInfo.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pcCapture
        '
        Me.pcCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pcCapture.Location = New System.Drawing.Point(21, 109)
        Me.pcCapture.Name = "pcCapture"
        Me.pcCapture.Size = New System.Drawing.Size(640, 480)
        Me.pcCapture.TabIndex = 13
        Me.pcCapture.TabStop = False
        '
        'pbReCapture
        '
        Me.pbReCapture.BackgroundImage = CType(resources.GetObject("pbReCapture.BackgroundImage"), System.Drawing.Image)
        Me.pbReCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbReCapture.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbReCapture.Location = New System.Drawing.Point(5, 53)
        Me.pbReCapture.Name = "pbReCapture"
        Me.pbReCapture.Size = New System.Drawing.Size(140, 50)
        Me.pbReCapture.TabIndex = 16
        Me.pbReCapture.TabStop = False
        Me.pbReCapture.Visible = False
        '
        'pbCapture
        '
        Me.pbCapture.BackgroundImage = CType(resources.GetObject("pbCapture.BackgroundImage"), System.Drawing.Image)
        Me.pbCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbCapture.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbCapture.Location = New System.Drawing.Point(151, 53)
        Me.pbCapture.Name = "pbCapture"
        Me.pbCapture.Size = New System.Drawing.Size(140, 50)
        Me.pbCapture.TabIndex = 17
        Me.pbCapture.TabStop = False
        '
        'pbSave
        '
        Me.pbSave.BackgroundImage = CType(resources.GetObject("pbSave.BackgroundImage"), System.Drawing.Image)
        Me.pbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbSave.Location = New System.Drawing.Point(297, 53)
        Me.pbSave.Name = "pbSave"
        Me.pbSave.Size = New System.Drawing.Size(140, 50)
        Me.pbSave.TabIndex = 18
        Me.pbSave.TabStop = False
        Me.pbSave.Visible = False
        '
        'pbClose
        '
        Me.pbClose.BackgroundImage = CType(resources.GetObject("pbClose.BackgroundImage"), System.Drawing.Image)
        Me.pbClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbClose.Location = New System.Drawing.Point(574, 44)
        Me.pbClose.Name = "pbClose"
        Me.pbClose.Size = New System.Drawing.Size(40, 35)
        Me.pbClose.TabIndex = 24
        Me.pbClose.TabStop = False
        '
        'pnlCusInfo
        '
        Me.pnlCusInfo.Controls.Add(Me.DisplayMobileStatus)
        Me.pnlCusInfo.Controls.Add(Me.lblMobileStatus)
        Me.pnlCusInfo.Controls.Add(Me.DisplayCategory)
        Me.pnlCusInfo.Controls.Add(Me.lblCategory)
        Me.pnlCusInfo.Controls.Add(Me.DisplayContactID)
        Me.pnlCusInfo.Controls.Add(Me.lblContactID)
        Me.pnlCusInfo.Controls.Add(Me.DisplayBillingSystem)
        Me.pnlCusInfo.Controls.Add(Me.lblBiilingSystem)
        Me.pnlCusInfo.Controls.Add(Me.DisplayNetworkType)
        Me.pnlCusInfo.Controls.Add(Me.lblNetworkType)
        Me.pnlCusInfo.Controls.Add(Me.DisplayMobileNo)
        Me.pnlCusInfo.Controls.Add(Me.DisplayLastDate)
        Me.pnlCusInfo.Controls.Add(Me.DisplayImage)
        Me.pnlCusInfo.Controls.Add(Me.DisplaySegment)
        Me.pnlCusInfo.Controls.Add(Me.DisplayPAGroup)
        Me.pnlCusInfo.Controls.Add(Me.lblMobileNo)
        Me.pnlCusInfo.Controls.Add(Me.lblLastDate)
        Me.pnlCusInfo.Controls.Add(Me.lblImage)
        Me.pnlCusInfo.Controls.Add(Me.lblSegment)
        Me.pnlCusInfo.Controls.Add(Me.lblPAGroup)
        Me.pnlCusInfo.Location = New System.Drawing.Point(667, 109)
        Me.pnlCusInfo.Name = "pnlCusInfo"
        Me.pnlCusInfo.Size = New System.Drawing.Size(518, 480)
        Me.pnlCusInfo.TabIndex = 25
        '
        'DisplayMobileStatus
        '
        Me.DisplayMobileStatus.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplayMobileStatus.BackColor = System.Drawing.Color.Transparent
        Me.DisplayMobileStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.DisplayMobileStatus.Location = New System.Drawing.Point(189, 330)
        Me.DisplayMobileStatus.Name = "DisplayMobileStatus"
        Me.DisplayMobileStatus.Size = New System.Drawing.Size(299, 26)
        Me.DisplayMobileStatus.TabIndex = 26
        Me.DisplayMobileStatus.Text = "-"
        Me.DisplayMobileStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMobileStatus
        '
        Me.lblMobileStatus.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMobileStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblMobileStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblMobileStatus.Location = New System.Drawing.Point(3, 330)
        Me.lblMobileStatus.Name = "lblMobileStatus"
        Me.lblMobileStatus.Size = New System.Drawing.Size(180, 26)
        Me.lblMobileStatus.TabIndex = 25
        Me.lblMobileStatus.Text = "Mobile Status :"
        Me.lblMobileStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DisplayCategory
        '
        Me.DisplayCategory.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplayCategory.BackColor = System.Drawing.Color.Transparent
        Me.DisplayCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.DisplayCategory.Location = New System.Drawing.Point(189, 366)
        Me.DisplayCategory.Name = "DisplayCategory"
        Me.DisplayCategory.Size = New System.Drawing.Size(299, 26)
        Me.DisplayCategory.TabIndex = 22
        Me.DisplayCategory.Text = "-"
        Me.DisplayCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCategory
        '
        Me.lblCategory.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblCategory.BackColor = System.Drawing.Color.Transparent
        Me.lblCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblCategory.Location = New System.Drawing.Point(3, 366)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(180, 26)
        Me.lblCategory.TabIndex = 18
        Me.lblCategory.Text = "Category :"
        Me.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DisplayContactID
        '
        Me.DisplayContactID.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplayContactID.BackColor = System.Drawing.Color.Transparent
        Me.DisplayContactID.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.DisplayContactID.Location = New System.Drawing.Point(189, 291)
        Me.DisplayContactID.Name = "DisplayContactID"
        Me.DisplayContactID.Size = New System.Drawing.Size(299, 26)
        Me.DisplayContactID.TabIndex = 17
        Me.DisplayContactID.Text = "-"
        Me.DisplayContactID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContactID
        '
        Me.lblContactID.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblContactID.BackColor = System.Drawing.Color.Transparent
        Me.lblContactID.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblContactID.Location = New System.Drawing.Point(3, 291)
        Me.lblContactID.Name = "lblContactID"
        Me.lblContactID.Size = New System.Drawing.Size(180, 26)
        Me.lblContactID.TabIndex = 16
        Me.lblContactID.Text = "Contact ID :"
        Me.lblContactID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DisplayBillingSystem
        '
        Me.DisplayBillingSystem.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplayBillingSystem.BackColor = System.Drawing.Color.Transparent
        Me.DisplayBillingSystem.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.DisplayBillingSystem.Location = New System.Drawing.Point(189, 255)
        Me.DisplayBillingSystem.Name = "DisplayBillingSystem"
        Me.DisplayBillingSystem.Size = New System.Drawing.Size(299, 26)
        Me.DisplayBillingSystem.TabIndex = 15
        Me.DisplayBillingSystem.Text = "-"
        Me.DisplayBillingSystem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBiilingSystem
        '
        Me.lblBiilingSystem.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblBiilingSystem.BackColor = System.Drawing.Color.Transparent
        Me.lblBiilingSystem.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblBiilingSystem.Location = New System.Drawing.Point(2, 255)
        Me.lblBiilingSystem.Name = "lblBiilingSystem"
        Me.lblBiilingSystem.Size = New System.Drawing.Size(180, 26)
        Me.lblBiilingSystem.TabIndex = 14
        Me.lblBiilingSystem.Text = "BOS :"
        Me.lblBiilingSystem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DisplayNetworkType
        '
        Me.DisplayNetworkType.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplayNetworkType.BackColor = System.Drawing.Color.Transparent
        Me.DisplayNetworkType.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.DisplayNetworkType.Location = New System.Drawing.Point(189, 214)
        Me.DisplayNetworkType.Name = "DisplayNetworkType"
        Me.DisplayNetworkType.Size = New System.Drawing.Size(299, 26)
        Me.DisplayNetworkType.TabIndex = 13
        Me.DisplayNetworkType.Text = "-"
        Me.DisplayNetworkType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNetworkType
        '
        Me.lblNetworkType.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNetworkType.BackColor = System.Drawing.Color.Transparent
        Me.lblNetworkType.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblNetworkType.Location = New System.Drawing.Point(3, 214)
        Me.lblNetworkType.Name = "lblNetworkType"
        Me.lblNetworkType.Size = New System.Drawing.Size(180, 26)
        Me.lblNetworkType.TabIndex = 12
        Me.lblNetworkType.Text = "Network Type :"
        Me.lblNetworkType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DisplayMobileNo
        '
        Me.DisplayMobileNo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplayMobileNo.BackColor = System.Drawing.Color.Transparent
        Me.DisplayMobileNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.DisplayMobileNo.Location = New System.Drawing.Point(189, 177)
        Me.DisplayMobileNo.Name = "DisplayMobileNo"
        Me.DisplayMobileNo.Size = New System.Drawing.Size(299, 26)
        Me.DisplayMobileNo.TabIndex = 11
        Me.DisplayMobileNo.Text = "-"
        Me.DisplayMobileNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DisplayLastDate
        '
        Me.DisplayLastDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplayLastDate.BackColor = System.Drawing.Color.Transparent
        Me.DisplayLastDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.DisplayLastDate.Location = New System.Drawing.Point(192, 138)
        Me.DisplayLastDate.Name = "DisplayLastDate"
        Me.DisplayLastDate.Size = New System.Drawing.Size(296, 26)
        Me.DisplayLastDate.TabIndex = 10
        Me.DisplayLastDate.Text = "-"
        Me.DisplayLastDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DisplayImage
        '
        Me.DisplayImage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplayImage.BackColor = System.Drawing.Color.Transparent
        Me.DisplayImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.DisplayImage.Location = New System.Drawing.Point(189, 96)
        Me.DisplayImage.Name = "DisplayImage"
        Me.DisplayImage.Size = New System.Drawing.Size(299, 26)
        Me.DisplayImage.TabIndex = 9
        Me.DisplayImage.Text = "-"
        Me.DisplayImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DisplaySegment
        '
        Me.DisplaySegment.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplaySegment.BackColor = System.Drawing.Color.Transparent
        Me.DisplaySegment.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.DisplaySegment.Location = New System.Drawing.Point(189, 52)
        Me.DisplaySegment.Name = "DisplaySegment"
        Me.DisplaySegment.Size = New System.Drawing.Size(299, 26)
        Me.DisplaySegment.TabIndex = 8
        Me.DisplaySegment.Text = "-"
        Me.DisplaySegment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DisplayPAGroup
        '
        Me.DisplayPAGroup.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplayPAGroup.BackColor = System.Drawing.Color.Transparent
        Me.DisplayPAGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.DisplayPAGroup.Location = New System.Drawing.Point(189, 9)
        Me.DisplayPAGroup.Name = "DisplayPAGroup"
        Me.DisplayPAGroup.Size = New System.Drawing.Size(299, 26)
        Me.DisplayPAGroup.TabIndex = 7
        Me.DisplayPAGroup.Text = "-"
        Me.DisplayPAGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMobileNo
        '
        Me.lblMobileNo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMobileNo.BackColor = System.Drawing.Color.Transparent
        Me.lblMobileNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblMobileNo.Location = New System.Drawing.Point(3, 177)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(180, 26)
        Me.lblMobileNo.TabIndex = 6
        Me.lblMobileNo.Text = "Mobile No :"
        Me.lblMobileNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLastDate
        '
        Me.lblLastDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblLastDate.BackColor = System.Drawing.Color.Transparent
        Me.lblLastDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblLastDate.Location = New System.Drawing.Point(3, 138)
        Me.lblLastDate.Name = "lblLastDate"
        Me.lblLastDate.Size = New System.Drawing.Size(180, 26)
        Me.lblLastDate.TabIndex = 5
        Me.lblLastDate.Text = "Last Capture Date :"
        Me.lblLastDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblImage
        '
        Me.lblImage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblImage.BackColor = System.Drawing.Color.Transparent
        Me.lblImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblImage.Location = New System.Drawing.Point(3, 96)
        Me.lblImage.Name = "lblImage"
        Me.lblImage.Size = New System.Drawing.Size(180, 26)
        Me.lblImage.TabIndex = 4
        Me.lblImage.Text = "Picture  :"
        Me.lblImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSegment
        '
        Me.lblSegment.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblSegment.BackColor = System.Drawing.Color.Transparent
        Me.lblSegment.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblSegment.Location = New System.Drawing.Point(3, 52)
        Me.lblSegment.Name = "lblSegment"
        Me.lblSegment.Size = New System.Drawing.Size(180, 26)
        Me.lblSegment.TabIndex = 3
        Me.lblSegment.Text = "Segment :"
        Me.lblSegment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPAGroup
        '
        Me.lblPAGroup.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblPAGroup.BackColor = System.Drawing.Color.Transparent
        Me.lblPAGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblPAGroup.Location = New System.Drawing.Point(3, 9)
        Me.lblPAGroup.Name = "lblPAGroup"
        Me.lblPAGroup.Size = New System.Drawing.Size(180, 26)
        Me.lblPAGroup.TabIndex = 2
        Me.lblPAGroup.Text = "PAGroup :"
        Me.lblPAGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnDiaplayImage
        '
        Me.btnDiaplayImage.Location = New System.Drawing.Point(21, 109)
        Me.btnDiaplayImage.Name = "btnDiaplayImage"
        Me.btnDiaplayImage.Size = New System.Drawing.Size(274, 201)
        Me.btnDiaplayImage.TabIndex = 28
        Me.btnDiaplayImage.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(948, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(191, 94)
        Me.PictureBox1.TabIndex = 63
        Me.PictureBox1.TabStop = False
        '
        'frmKiosCapture
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.KioskElo.My.Resources.Resources.TH_template_capture
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1151, 666)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnDiaplayImage)
        Me.Controls.Add(Me.pnlCusInfo)
        Me.Controls.Add(Me.pbClose)
        Me.Controls.Add(Me.pbSave)
        Me.Controls.Add(Me.pbCapture)
        Me.Controls.Add(Me.pbReCapture)
        Me.Controls.Add(Me.pcCapture)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmKiosCapture"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Capture"
        CType(Me.pcCapture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbReCapture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCapture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCusInfo.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pcCapture As System.Windows.Forms.PictureBox
    Friend WithEvents pbReCapture As System.Windows.Forms.PictureBox
    Friend WithEvents pbCapture As System.Windows.Forms.PictureBox
    Friend WithEvents pbSave As System.Windows.Forms.PictureBox
    Friend WithEvents pbClose As System.Windows.Forms.PictureBox
    Friend WithEvents pnlCusInfo As System.Windows.Forms.Panel
    Friend WithEvents lblPAGroup As System.Windows.Forms.Label
    Friend WithEvents lblSegment As System.Windows.Forms.Label
    Friend WithEvents lblImage As System.Windows.Forms.Label
    Friend WithEvents lblLastDate As System.Windows.Forms.Label
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
    Friend WithEvents DisplayPAGroup As System.Windows.Forms.Label
    Friend WithEvents DisplayMobileNo As System.Windows.Forms.Label
    Friend WithEvents DisplayLastDate As System.Windows.Forms.Label
    Friend WithEvents DisplayImage As System.Windows.Forms.Label
    Friend WithEvents DisplaySegment As System.Windows.Forms.Label
    Friend WithEvents lblNetworkType As System.Windows.Forms.Label
    Friend WithEvents DisplayNetworkType As System.Windows.Forms.Label
    Friend WithEvents lblBiilingSystem As System.Windows.Forms.Label
    Friend WithEvents DisplayBillingSystem As System.Windows.Forms.Label
    Friend WithEvents DisplayContactID As System.Windows.Forms.Label
    Friend WithEvents lblContactID As System.Windows.Forms.Label
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents DisplayCategory As System.Windows.Forms.Label
    Friend WithEvents lblMobileStatus As System.Windows.Forms.Label
    Friend WithEvents DisplayMobileStatus As System.Windows.Forms.Label
    Friend WithEvents btnDiaplayImage As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class
