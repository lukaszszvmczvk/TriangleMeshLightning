namespace GKLab2
{
    using System.Numerics;
    public partial class Form1 : Form
    {
        TriangleMesh triangleMesh;
        public Form1()
        {
            InitializeComponent();
            InitializeControls();
            LightSource.LightPositon = new Vector3((float)pictureBox.Size.Width/2, (float)pictureBox.Size.Height/2, 100);
            LightSource.LightColorNormalized = Vector3.Normalize(new Vector3(1, 1, 1));
            pictureBox.Image = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            TriangleMesh.pictureBox = this.pictureBox;
            triangleMesh = new TriangleMesh();
        }
        private void InitializeControls()
        {
            trackBarX.Minimum = trackBarY.Minimum = 4;
            trackBarX.Maximum = trackBarY.Maximum = 100;
            trackBarKd.Minimum = trackBarKs.Minimum = 0;
            trackBarKd.Maximum = trackBarKs.Maximum = trackBarM.Maximum = 100;
            trackBarM.Minimum = 1;
        }

        private void trackBarX_ValueChanged(object sender, EventArgs e)
        {
            if (triangleMesh != null)
            {
                triangleMesh.X = trackBarX.Value;
                labelX.Text = "X: " + trackBarX.Value.ToString();
                triangleMesh.InitializeTriangles();
            }
        }

        private void trackBarY_ValueChanged(object sender, EventArgs e)
        {
            if (triangleMesh != null)
            {
                triangleMesh.Y = trackBarY.Value;
                labelY.Text = "Y: " + trackBarY.Value.ToString();
                triangleMesh.InitializeTriangles();
            }
        }

        private void trackBarKd_ValueChanged(object sender, EventArgs e)
        {
            if (triangleMesh != null)
            {
                TriangleMesh.kd = (float)trackBarKd.Value / 100;
                labelKd.Text = "kd: " + TriangleMesh.kd.ToString();
                triangleMesh.FillBitmap();
            }
        }

        private void trackBarKs_ValueChanged(object sender, EventArgs e)
        {
            if (triangleMesh != null)
            {
                TriangleMesh.ks = (float)trackBarKs.Value / 100;
                labelKs.Text = "ks: " + TriangleMesh.ks.ToString();
                triangleMesh.FillBitmap();
            }
        }

        private void trackBarM_ValueChanged(object sender, EventArgs e)
        {
            if (triangleMesh != null)
            {
                TriangleMesh.m = trackBarM.Value;
                labelM.Text = "m: " + TriangleMesh.m.ToString();
                triangleMesh.FillBitmap();
            }
        }
    }
}