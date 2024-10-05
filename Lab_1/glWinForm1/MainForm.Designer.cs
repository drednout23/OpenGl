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
            this.renderControl = new RenderControl(); // Инициализация RenderControl
            this.SuspendLayout();

            // 
            // renderControl
            // 
            this.renderControl.Dock = System.Windows.Forms.DockStyle.Fill; // Заставить RenderControl занимать всю форму

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 324);
            this.Controls.Add(this.renderControl); // Добавление RenderControl в Controls формы
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
