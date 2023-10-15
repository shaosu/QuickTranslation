namespace QuickTranslation
{
    partial class FormConfigApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkOutputOriginal = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkOutputOriginal
            // 
            this.chkOutputOriginal.AutoSize = true;
            this.chkOutputOriginal.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkOutputOriginal.Location = new System.Drawing.Point(13, 13);
            this.chkOutputOriginal.Name = "chkOutputOriginal";
            this.chkOutputOriginal.Size = new System.Drawing.Size(116, 19);
            this.chkOutputOriginal.TabIndex = 0;
            this.chkOutputOriginal.Text = "是否输出原文";
            this.chkOutputOriginal.UseVisualStyleBackColor = true;
            // 
            // FormConfigApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chkOutputOriginal);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfigApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配置App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConfigApp_FormClosing);
            this.Load += new System.EventHandler(this.FormConfigApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkOutputOriginal;
    }
}