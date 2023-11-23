namespace GKLab2
{
    using System.Numerics;
    using System.Windows.Media.Media3D;

    public partial class Form1 : Form
    {
        TriangleMesh triangleMesh;
        public Form1()
        {
            InitializeComponent();
            InitializeControls();
            LightSource.LightPositon = new Vector3D((float)pictureBox.Size.Width / 2, (float)pictureBox.Size.Height / 2, 100);
            var lcn = new Vector3D(1, 1, 1);
            lcn.Normalize();
            LightSource.LCN = lcn;
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
            trackBarKd.Value = 100;
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
                trackBarKs.Value = 100 - trackBarKd.Value;
                labelKd.Text = "kd: " + Math.Round(TriangleMesh.kd, 2).ToString();
                triangleMesh.FillBitmap();
            }
        }

        private void trackBarKs_ValueChanged(object sender, EventArgs e)
        {
            if (triangleMesh != null)
            {
                TriangleMesh.ks = (float)trackBarKs.Value / 100;
                trackBarKd.Value = 100 - trackBarKs.Value;
                labelKs.Text = "ks: " + Math.Round(TriangleMesh.ks, 2).ToString();
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

        private void objectColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                var color = colorDialog.Color;
                var colorV = new Vector3D(color.R, color.G, color.B);
                colorV.Normalize();
                TriangleMesh.Io = colorV;
                triangleMesh.FillBitmap();
            }
        }

        private void lightColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                var color = colorDialog.Color;
                var colorV = new Vector3D(color.R, color.G, color.B);
                colorV.Normalize();
                LightSource.LCN = colorV;
                triangleMesh.FillBitmap();
            }
        }

        private void checkBoxMesh_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxMesh.Checked)
            {
                triangleMesh.ShowMesh = true;
            }
            else
            {
                triangleMesh.ShowMesh = false;
            }
            triangleMesh.FillBitmap();
        }
    }
}