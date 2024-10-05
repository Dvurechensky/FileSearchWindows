namespace FileSearch
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FileRegexPathTxt = new TextBox();
            SearchBtn = new Button();
            labelNameDir = new Label();
            labelNameFileRegex = new Label();
            DirsTreeView = new TreeView();
            FindFilesTxt = new TextBox();
            AllFilesTxt = new TextBox();
            timerTxt = new TextBox();
            PauseBtn = new Button();
            lstResults = new LargeListViewUserControl();
            statusProgress = new StatusStrip();
            toolStripProgressBar1 = new ToolStripProgressBar();
            statusLabel = new ToolStripStatusLabel();
            statusLabelExceptions = new ToolStripStatusLabel();
            btnBrowse = new Button();
            folderBrowserDialog = new FolderBrowserDialog();
            DirPathTxt = new ComboBox();
            labelInfo1 = new Label();
            labelInfo2 = new Label();
            labelInfo3 = new Label();
            pictureBoxLoading = new PictureBox();
            statusProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLoading).BeginInit();
            SuspendLayout();
            // 
            // FileRegexPathTxt
            // 
            FileRegexPathTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FileRegexPathTxt.Location = new Point(2, 87);
            FileRegexPathTxt.Name = "FileRegexPathTxt";
            FileRegexPathTxt.Size = new Size(329, 27);
            FileRegexPathTxt.TabIndex = 1;
            FileRegexPathTxt.Text = "\\.png";
            // 
            // SearchBtn
            // 
            SearchBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchBtn.BackColor = Color.Red;
            SearchBtn.Cursor = Cursors.Hand;
            SearchBtn.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Bold, GraphicsUnit.Point);
            SearchBtn.ForeColor = SystemColors.ControlLightLight;
            SearchBtn.Location = new Point(545, 19);
            SearchBtn.Margin = new Padding(4);
            SearchBtn.Name = "SearchBtn";
            SearchBtn.Size = new Size(139, 40);
            SearchBtn.TabIndex = 0;
            SearchBtn.Text = "Поиск";
            SearchBtn.UseVisualStyleBackColor = false;
            SearchBtn.Click += SearchBtn_Click;
            // 
            // labelNameDir
            // 
            labelNameDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelNameDir.AutoSize = true;
            labelNameDir.Location = new Point(97, 2);
            labelNameDir.Name = "labelNameDir";
            labelNameDir.Size = new Size(149, 20);
            labelNameDir.TabIndex = 0;
            labelNameDir.Text = "Директория поиска";
            // 
            // labelNameFileRegex
            // 
            labelNameFileRegex.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelNameFileRegex.AutoSize = true;
            labelNameFileRegex.Location = new Point(59, 64);
            labelNameFileRegex.Name = "labelNameFileRegex";
            labelNameFileRegex.Size = new Size(221, 20);
            labelNameFileRegex.TabIndex = 4;
            labelNameFileRegex.Text = "Шаблон имени файла (Regex)";
            // 
            // DirsTreeView
            // 
            DirsTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            DirsTreeView.Location = new Point(0, 124);
            DirsTreeView.Name = "DirsTreeView";
            DirsTreeView.Size = new Size(684, 430);
            DirsTreeView.TabIndex = 9;
            DirsTreeView.NodeMouseDoubleClick += DirsTreeView_NodeMouseDoubleClick;
            // 
            // FindFilesTxt
            // 
            FindFilesTxt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            FindFilesTxt.Enabled = false;
            FindFilesTxt.Location = new Point(374, 26);
            FindFilesTxt.Name = "FindFilesTxt";
            FindFilesTxt.Size = new Size(72, 27);
            FindFilesTxt.TabIndex = 10;
            FindFilesTxt.Text = "0";
            FindFilesTxt.TextAlign = HorizontalAlignment.Center;
            // 
            // AllFilesTxt
            // 
            AllFilesTxt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AllFilesTxt.BackColor = Color.White;
            AllFilesTxt.BorderStyle = BorderStyle.FixedSingle;
            AllFilesTxt.Enabled = false;
            AllFilesTxt.Location = new Point(452, 26);
            AllFilesTxt.Name = "AllFilesTxt";
            AllFilesTxt.Size = new Size(86, 27);
            AllFilesTxt.TabIndex = 11;
            AllFilesTxt.Text = "0";
            AllFilesTxt.TextAlign = HorizontalAlignment.Center;
            // 
            // timerTxt
            // 
            timerTxt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            timerTxt.Enabled = false;
            timerTxt.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            timerTxt.Location = new Point(384, 85);
            timerTxt.Name = "timerTxt";
            timerTxt.Size = new Size(129, 29);
            timerTxt.TabIndex = 12;
            timerTxt.Text = "0";
            timerTxt.TextAlign = HorizontalAlignment.Center;
            // 
            // PauseBtn
            // 
            PauseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PauseBtn.BackColor = Color.Red;
            PauseBtn.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Bold, GraphicsUnit.Point);
            PauseBtn.ForeColor = SystemColors.ControlLightLight;
            PauseBtn.Location = new Point(545, 74);
            PauseBtn.Margin = new Padding(4);
            PauseBtn.Name = "PauseBtn";
            PauseBtn.Size = new Size(139, 40);
            PauseBtn.TabIndex = 13;
            PauseBtn.Text = "Пауза";
            PauseBtn.UseVisualStyleBackColor = false;
            PauseBtn.Click += PauseBtn_Click;
            // 
            // lstResults
            // 
            lstResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstResults.Location = new Point(2, 560);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(684, 376);
            lstResults.TabIndex = 4;
            lstResults.FileOpened += MainForm_FileOpened;
            lstResults.DirectoryOpened += MainForm_DirectoryOpened;
            // 
            // statusProgress
            // 
            statusProgress.Items.AddRange(new ToolStripItem[] { toolStripProgressBar1, statusLabel, statusLabelExceptions });
            statusProgress.Location = new Point(0, 939);
            statusProgress.MinimumSize = new Size(682, 0);
            statusProgress.Name = "statusProgress";
            statusProgress.Size = new Size(684, 22);
            statusProgress.TabIndex = 14;
            statusProgress.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(200, 16);
            // 
            // statusLabel
            // 
            statusLabel.Margin = new Padding(50, 3, 50, 2);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(31, 17);
            statusLabel.Text = "2024";
            // 
            // statusLabelExceptions
            // 
            statusLabelExceptions.AutoToolTip = true;
            statusLabelExceptions.BackColor = Color.DodgerBlue;
            statusLabelExceptions.ForeColor = Color.White;
            statusLabelExceptions.Margin = new Padding(20, 3, 0, 2);
            statusLabelExceptions.Name = "statusLabelExceptions";
            statusLabelExceptions.Size = new Size(54, 17);
            statusLabelExceptions.Text = "Ошибки";
            statusLabelExceptions.ToolTipText = "Открыть панель ошибок";
            statusLabelExceptions.Click += statusLabelExceptions_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowse.BackColor = Color.DodgerBlue;
            btnBrowse.Cursor = Cursors.Hand;
            btnBrowse.FlatStyle = FlatStyle.Popup;
            btnBrowse.ForeColor = Color.White;
            btnBrowse.Location = new Point(337, 24);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(31, 29);
            btnBrowse.TabIndex = 16;
            btnBrowse.Text = "...";
            btnBrowse.TextAlign = ContentAlignment.TopCenter;
            btnBrowse.UseVisualStyleBackColor = false;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // DirPathTxt
            // 
            DirPathTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            DirPathTxt.FormattingEnabled = true;
            DirPathTxt.Location = new Point(0, 25);
            DirPathTxt.Name = "DirPathTxt";
            DirPathTxt.Size = new Size(331, 28);
            DirPathTxt.TabIndex = 17;
            DirPathTxt.TextChanged += DirPathTxt_TextChanged;
            // 
            // labelInfo1
            // 
            labelInfo1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelInfo1.AutoSize = true;
            labelInfo1.Location = new Point(374, 2);
            labelInfo1.Name = "labelInfo1";
            labelInfo1.Size = new Size(72, 20);
            labelInfo1.TabIndex = 18;
            labelInfo1.Text = "Найдено";
            // 
            // labelInfo2
            // 
            labelInfo2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelInfo2.AutoSize = true;
            labelInfo2.Location = new Point(472, 2);
            labelInfo2.Name = "labelInfo2";
            labelInfo2.Size = new Size(48, 20);
            labelInfo2.TabIndex = 19;
            labelInfo2.Text = "Всего";
            // 
            // labelInfo3
            // 
            labelInfo3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelInfo3.AutoSize = true;
            labelInfo3.Location = new Point(421, 62);
            labelInfo3.Name = "labelInfo3";
            labelInfo3.Size = new Size(54, 20);
            labelInfo3.TabIndex = 20;
            labelInfo3.Text = "Время";
            // 
            // pictureBoxLoading
            // 
            pictureBoxLoading.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxLoading.Image = Resource.Loading_icon;
            pictureBoxLoading.Location = new Point(461, 27);
            pictureBoxLoading.Name = "pictureBoxLoading";
            pictureBoxLoading.Size = new Size(68, 25);
            pictureBoxLoading.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLoading.TabIndex = 21;
            pictureBoxLoading.TabStop = false;
            pictureBoxLoading.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(255, 224, 192);
            ClientSize = new Size(684, 961);
            Controls.Add(pictureBoxLoading);
            Controls.Add(labelInfo3);
            Controls.Add(labelInfo2);
            Controls.Add(labelInfo1);
            Controls.Add(DirPathTxt);
            Controls.Add(btnBrowse);
            Controls.Add(statusProgress);
            Controls.Add(PauseBtn);
            Controls.Add(timerTxt);
            Controls.Add(AllFilesTxt);
            Controls.Add(FindFilesTxt);
            Controls.Add(DirsTreeView);
            Controls.Add(labelNameFileRegex);
            Controls.Add(labelNameDir);
            Controls.Add(FileRegexPathTxt);
            Controls.Add(SearchBtn);
            Controls.Add(lstResults);
            Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            Margin = new Padding(4);
            MaximizeBox = false;
            MaximumSize = new Size(1200, 1200);
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            MinimumSize = new Size(700, 1000);
            Name = "MainForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Поиск файлов на диске";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            FileOpened += MainForm_FileOpened;
            DirectoryOpened += MainForm_DirectoryOpened;
            statusProgress.ResumeLayout(false);
            statusProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLoading).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox FileRegexPathTxt;
        private Button SearchBtn;
        private Label labelNameDir;
        private Label labelNameFileRegex;
        private TreeView DirsTreeView;
        private TextBox FindFilesTxt;
        private TextBox AllFilesTxt;
        private TextBox timerTxt;
        private Button PauseBtn;
        private LargeListViewUserControl lstResults;
        private StatusStrip statusProgress;
        private ToolStripProgressBar toolStripProgressBar1;
        private Button btnBrowse;
        private FolderBrowserDialog folderBrowserDialog;
        private ComboBox DirPathTxt;
        private ToolStripStatusLabel statusLabel;
        private Label labelInfo1;
        private Label labelInfo2;
        private Label labelInfo3;
        private ToolStripStatusLabel statusLabelExceptions;
        private PictureBox pictureBoxLoading;
    }
}