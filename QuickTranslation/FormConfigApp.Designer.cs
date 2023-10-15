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
            this.chkShowOriginal = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkShowOriginal
            // 
            this.chkShowOriginal.AutoSize = true;
            this.chkShowOriginal.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkShowOriginal.Location = new System.Drawing.Point(13, 13);
            this.chkShowOriginal.Name = "chkShowOriginal";
            this.chkShowOriginal.Size = new System.Drawing.Size(116, 19);
            this.chkShowOriginal.TabIndex = 0;
            this.chkShowOriginal.Text = "是否显示原文";
            this.chkShowOriginal.UseVisualStyleBackColor = true;
            // 
            // FormConfigApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chkShowOriginal);
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

        private System.Windows.Forms.CheckBox chkShowOriginal;
    }
}