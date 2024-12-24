using System.Windows.Forms;
using static Transform3D.OpenGL;


namespace Transform3D
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnDepthChecked(object sender, System.EventArgs e)
        {
            rc.DepthTest = cbDepth.Checked;
            rc.Invalidate();
        }
    }
}
