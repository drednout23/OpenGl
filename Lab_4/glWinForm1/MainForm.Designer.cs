namespace glWinForm1
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private RenderControl renderControl; // Добавьте поле для RenderControl

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            renderControl = new RenderControl();
            SuspendLayout();
            // 
            // renderControl
            // 
            renderControl.BackColor = System.Drawing.Color.SlateGray;
            renderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            renderControl.ForeColor = System.Drawing.Color.White;
            renderControl.Location = new System.Drawing.Point(0, 0);
            renderControl.Name = "renderControl";
            renderControl.Size = new System.Drawing.Size(651, 355);
            renderControl.TabIndex = 0;
            renderControl.TextCodePage = 65001;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(651, 355);
            Controls.Add(renderControl);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Main Form";
            ResumeLayout(false);
        }

        #endregion
    }
}
