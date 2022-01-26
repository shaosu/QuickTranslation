
using System.ComponentModel;

namespace QuickTranslation
{
    partial class TranBox
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.probarMain = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblPro = new System.Windows.Forms.Label();
            this.thread = new QuickTranslation.FixedThreadShowItem();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoEllipsis = true;
            this.lblName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.lblName.Location = new System.Drawing.Point(16, 11);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(452, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "正在加载...";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblName_MouseClick);
            // 
            // lblState
            // 
            this.lblState.Location = new System.Drawing.Point(17, 38);
            this.lblState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(126, 20);
            this.lblState.TabIndex = 1;
            this.lblState.Text = "初始化";
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.AutoEllipsis = true;
            this.lblMessage.Location = new System.Drawing.Point(151, 37);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(370, 20);
            this.lblMessage.TabIndex = 1;
            // 
            // probarMain
            // 
            this.probarMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.probarMain.Location = new System.Drawing.Point(20, 63);
            this.probarMain.Name = "probarMain";
            this.probarMain.Size = new System.Drawing.Size(489, 23);
            this.probarMain.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(500, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(24, 16);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "×";
            this.btnCancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCancel_MouseClick);
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(17, 92);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(72, 16);
            this.lblLength.TabIndex = 4;
            this.lblLength.Text = "文件大小";
            this.lblLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPro
            // 
            this.lblPro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPro.Location = new System.Drawing.Point(384, 89);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(125, 23);
            this.lblPro.TabIndex = 4;
            this.lblPro.Text = "0/0";
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TranBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblPro);
            this.Controls.Add(this.lblLength);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.probarMain);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblName);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TranBox";
            this.Size = new System.Drawing.Size(525, 118);
            this.Load += new System.EventHandler(this.TranBox_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TranBox_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ProgressBar probarMain;
        private System.Windows.Forms.Label btnCancel;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblPro;
        private FixedThreadShowItem thread;
    }
}
