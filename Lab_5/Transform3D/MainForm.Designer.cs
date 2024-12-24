
namespace Transform3D
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            rc = new RenderControl();
            cbDepth = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // rc
            // 
            rc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rc.BackColor = System.Drawing.Color.SlateGray;
            rc.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            rc.ForeColor = System.Drawing.Color.White;
            rc.Location = new System.Drawing.Point(12, 12);
            rc.Name = "rc";
            rc.Size = new System.Drawing.Size(480, 300);
            rc.TabIndex = 0;
            rc.TextCodePage = 1251;
            // 
            // cbDepth
            // 
            cbDepth.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            cbDepth.AutoSize = true;
            cbDepth.Checked = true;
            cbDepth.CheckState = System.Windows.Forms.CheckState.Checked;
            cbDepth.Location = new System.Drawing.Point(513, 12);
            cbDepth.Name = "cbDepth";
            cbDepth.Size = new System.Drawing.Size(80, 19);
            cbDepth.TabIndex = 1;
            cbDepth.Text = "Depth test";
            cbDepth.UseVisualStyleBackColor = true;
            cbDepth.CheckedChanged += OnDepthChecked;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(624, 324);
            Controls.Add(cbDepth);
            Controls.Add(rc);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Main Form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RenderControl rc;
        private System.Windows.Forms.CheckBox cbDepth;
    }
}

