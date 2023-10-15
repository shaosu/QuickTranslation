
namespace QuickTranslation
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dialogSelectXml = new System.Windows.Forms.OpenFileDialog();
            this.lstMain = new Himesyo.WinForm.ListLayoutPanel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabIncomplete = new System.Windows.Forms.TabPage();
            this.txtFilter = new Himesyo.WinForm.PromptTextBox();
            this.menuManager = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPause = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemAllAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAllPause = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAllDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemRetryError = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMarkComplete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnStartTran = new System.Windows.Forms.Button();
            this.btnOpenOut = new System.Windows.Forms.Button();
            this.btnConfigApp = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabIncomplete.SuspendLayout();
            this.menuManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(831, 16);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(105, 46);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dialogSelectXml
            // 
            this.dialogSelectXml.DefaultExt = "xml";
            this.dialogSelectXml.Filter = "VS 注释文档|*.xml|所有文件|*.*";
            this.dialogSelectXml.Multiselect = true;
            // 
            // lstMain
            // 
            this.lstMain.AutoScroll = true;
            this.lstMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMain.Location = new System.Drawing.Point(4, 4);
            this.lstMain.Margin = new System.Windows.Forms.Padding(4);
            this.lstMain.Name = "lstMain";
            this.lstMain.Size = new System.Drawing.Size(791, 416);
            this.lstMain.TabIndex = 1;
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabIncomplete);
            this.tabMain.Location = new System.Drawing.Point(16, 16);
            this.tabMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(807, 454);
            this.tabMain.TabIndex = 0;
            // 
            // tabIncomplete
            // 
            this.tabIncomplete.Controls.Add(this.lstMain);
            this.tabIncomplete.Location = new System.Drawing.Point(4, 26);
            this.tabIncomplete.Margin = new System.Windows.Forms.Padding(4);
            this.tabIncomplete.Name = "tabIncomplete";
            this.tabIncomplete.Padding = new System.Windows.Forms.Padding(4);
            this.tabIncomplete.Size = new System.Drawing.Size(799, 424);
            this.tabIncomplete.TabIndex = 0;
            this.tabIncomplete.Text = "未完成";
            this.tabIncomplete.UseVisualStyleBackColor = true;
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.FontPromptText = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFilter.Location = new System.Drawing.Point(249, 11);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.PasswordChar = '\0';
            this.txtFilter.Size = new System.Drawing.Size(566, 26);
            this.txtFilter.TabIndex = 3;
            this.txtFilter.TextAlignPrompt = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtFilter.TextPrompt = "键入以筛选";
            this.txtFilter.Value = "";
            this.txtFilter.ValueChange += new System.EventHandler(this.txtFilter_ValueChange);
            // 
            // menuManager
            // 
            this.menuManager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAdd,
            this.menuItemStart,
            this.menuItemPause,
            this.menuItemDelete,
            this.toolStripMenuItem1,
            this.menuItemAllAdd,
            this.menuItemAllPause,
            this.menuItemAllDelete,
            this.toolStripMenuItem2,
            this.menuItemRetryError,
            this.menuItemMarkComplete});
            this.menuManager.Name = "menuManager";
            this.menuManager.Size = new System.Drawing.Size(173, 214);
            // 
            // menuItemAdd
            // 
            this.menuItemAdd.Name = "menuItemAdd";
            this.menuItemAdd.Size = new System.Drawing.Size(172, 22);
            this.menuItemAdd.Text = "加入等待队列";
            this.menuItemAdd.Click += new System.EventHandler(this.menuItemAdd_Click);
            // 
            // menuItemStart
            // 
            this.menuItemStart.Name = "menuItemStart";
            this.menuItemStart.Size = new System.Drawing.Size(172, 22);
            this.menuItemStart.Text = "立即开始";
            this.menuItemStart.Click += new System.EventHandler(this.menuItemStart_Click);
            // 
            // menuItemPause
            // 
            this.menuItemPause.Name = "menuItemPause";
            this.menuItemPause.Size = new System.Drawing.Size(172, 22);
            this.menuItemPause.Text = "暂停";
            this.menuItemPause.Click += new System.EventHandler(this.menuItemPause_Click);
            // 
            // menuItemDelete
            // 
            this.menuItemDelete.Name = "menuItemDelete";
            this.menuItemDelete.Size = new System.Drawing.Size(172, 22);
            this.menuItemDelete.Text = "删除";
            this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 6);
            // 
            // menuItemAllAdd
            // 
            this.menuItemAllAdd.Name = "menuItemAllAdd";
            this.menuItemAllAdd.Size = new System.Drawing.Size(172, 22);
            this.menuItemAllAdd.Text = "全部准备";
            this.menuItemAllAdd.Click += new System.EventHandler(this.menuItemAllAdd_Click);
            // 
            // menuItemAllPause
            // 
            this.menuItemAllPause.Name = "menuItemAllPause";
            this.menuItemAllPause.Size = new System.Drawing.Size(172, 22);
            this.menuItemAllPause.Text = "全部暂停";
            this.menuItemAllPause.Click += new System.EventHandler(this.menuItemAllPause_Click);
            // 
            // menuItemAllDelete
            // 
            this.menuItemAllDelete.Name = "menuItemAllDelete";
            this.menuItemAllDelete.Size = new System.Drawing.Size(172, 22);
            this.menuItemAllDelete.Text = "全部删除";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 6);
            // 
            // menuItemRetryError
            // 
            this.menuItemRetryError.Name = "menuItemRetryError";
            this.menuItemRetryError.Size = new System.Drawing.Size(172, 22);
            this.menuItemRetryError.Text = "重新翻译错误节点";
            this.menuItemRetryError.Click += new System.EventHandler(this.menuItemRetryError_Click);
            // 
            // menuItemMarkComplete
            // 
            this.menuItemMarkComplete.Name = "menuItemMarkComplete";
            this.menuItemMarkComplete.Size = new System.Drawing.Size(172, 22);
            this.menuItemMarkComplete.Text = "标记为完成";
            this.menuItemMarkComplete.Click += new System.EventHandler(this.menuItemMarkComplete_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(834, 178);
            this.btnTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(105, 46);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfig.Location = new System.Drawing.Point(831, 420);
            this.btnConfig.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(105, 46);
            this.btnConfig.TabIndex = 2;
            this.btnConfig.Text = "配置Key";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnStartTran
            // 
            this.btnStartTran.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartTran.Location = new System.Drawing.Point(834, 124);
            this.btnStartTran.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartTran.Name = "btnStartTran";
            this.btnStartTran.Size = new System.Drawing.Size(105, 46);
            this.btnStartTran.TabIndex = 2;
            this.btnStartTran.Text = "开始翻译";
            this.btnStartTran.UseVisualStyleBackColor = true;
            this.btnStartTran.Visible = false;
            this.btnStartTran.Click += new System.EventHandler(this.btnStartTran_Click);
            // 
            // btnOpenOut
            // 
            this.btnOpenOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenOut.Location = new System.Drawing.Point(831, 70);
            this.btnOpenOut.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenOut.Name = "btnOpenOut";
            this.btnOpenOut.Size = new System.Drawing.Size(105, 46);
            this.btnOpenOut.TabIndex = 2;
            this.btnOpenOut.Text = "输出目录";
            this.btnOpenOut.UseVisualStyleBackColor = true;
            this.btnOpenOut.Click += new System.EventHandler(this.btnOpenOut_Click);
            // 
            // btnConfigApp
            // 
            this.btnConfigApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfigApp.Location = new System.Drawing.Point(831, 366);
            this.btnConfigApp.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfigApp.Name = "btnConfigApp";
            this.btnConfigApp.Size = new System.Drawing.Size(105, 46);
            this.btnConfigApp.TabIndex = 4;
            this.btnConfigApp.Text = "配置App";
            this.btnConfigApp.UseVisualStyleBackColor = true;
            this.btnConfigApp.Click += new System.EventHandler(this.btnConfigApp_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 486);
            this.Controls.Add(this.btnConfigApp);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnOpenOut);
            this.Controls.Add(this.btnStartTran);
            this.Controls.Add(this.btnAdd);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文档翻译";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.tabMain.ResumeLayout(false);
            this.tabIncomplete.ResumeLayout(false);
            this.menuManager.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Himesyo.WinForm.ListLayoutPanel lstMain;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.OpenFileDialog dialogSelectXml;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabIncomplete;
        private Himesyo.WinForm.PromptTextBox txtFilter;
        private System.Windows.Forms.ContextMenuStrip menuManager;
        private System.Windows.Forms.ToolStripMenuItem menuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem menuItemStart;
        private System.Windows.Forms.ToolStripMenuItem menuItemPause;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemAllAdd;
        private System.Windows.Forms.ToolStripMenuItem menuItemAllPause;
        private System.Windows.Forms.ToolStripMenuItem menuItemAllDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuItemRetryError;
        private System.Windows.Forms.ToolStripMenuItem menuItemMarkComplete;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnStartTran;
        private System.Windows.Forms.Button btnOpenOut;
        private System.Windows.Forms.Button btnConfigApp;
    }
}

