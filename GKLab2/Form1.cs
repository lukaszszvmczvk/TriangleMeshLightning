namespace GKLab2
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Numerics;
    using System.Windows.Media.Media3D;

    public partial class Form1 : Form
    {
        TriangleMesh triangleMesh;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int centerX;
        private int centerY;
        private int R;
        private double angle;
        public Form1()
        {
            InitializeComponent();
            InitializeControls();
            centerX = pictureBox.Size.Width / 2;
            centerY = pictureBox.Size.Height / 2;
            angle = 0;
            R = (pictureBox.Size.Height - 100) / 2;
            LightSource.LightPositon = new Vector3D(centerX, centerY, 100);
            var lcn = new Vector3D(1, 1, 1);
            lcn.Normalize();
            LightSource.LCN = lcn;
            pictureBox.Image = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            TriangleMesh.pictureBox = this.pictureBox;
            TriangleMesh.Width = pictureBox.Width; TriangleMesh.Height = pictureBox.Height;
            triangleMesh = new TriangleMesh();
            timer.Interval = 25;
            timer.Tick += Timer_Tick;
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
        private void Timer_Tick(object? sender, EventArgs e)
        {
            int x = centerX + (int)(R * Math.Cos(angle));
            int y = centerY + (int)(R * Math.Sin(angle));
            angle += 0.1;
            LightSource.LightPositon = new Vector3D(x, y, 100);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            triangleMesh.FillBitmap();
            stopWatch.Stop();
            double ts = stopWatch.Elapsed.TotalMilliseconds;
            int fps = (int)(1000 / ts);
            this.Text = "Light simulation. FPS: " + fps;
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
            if (checkBoxMesh.Checked)
            {
                triangleMesh.ShowMesh = true;
            }
            else
            {
                triangleMesh.ShowMesh = false;
            }
            triangleMesh.FillBitmap();
        }

        private void animationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (animationCheckBox.Checked)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image Files |*.jpg;*.jpeg;*.png;";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.FileName))
                    LoadTexture(dialog.FileName);
            }
            dialog.Dispose();
        }

        private void LoadNormalMap(string filePath)
        {
            var bitmap = new Bitmap(filePath);
            bitmap = new Bitmap(bitmap, pictureBox.Width + 1, pictureBox.Height + 1);
            var normals = new Vector3D[bitmap.Width, bitmap.Height];
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var pixelColor = bitmap.GetPixel(i, j);
                    normals[i, j].X = (pixelColor.R - 127) / 128.0;
                    normals[i, j].Y = (pixelColor.G - 127) / 128.0;
                    normals[i, j].Z = pixelColor.B / 255.0;
                    normals[i, j].Normalize();
                }
            }
            TriangleMesh.INV = normals;
            triangleMesh.InitializeTriangles();
        }
        private void LoadTexture(string filePath)
        {
            var textureBitmap = new Bitmap(filePath);
            textureBitmap = new Bitmap(textureBitmap, pictureBox.Width + 1, pictureBox.Height + 1);
            var colors = new Vector3D[textureBitmap.Width, textureBitmap.Height];
            for (int i = 0; i < textureBitmap.Width; i++)
            {
                for (int j = 0; j < textureBitmap.Height; j++)
                {
                    var pixelColor = textureBitmap.GetPixel(i, j);
                    colors[i, j].X = pixelColor.R;
                    colors[i, j].Y = pixelColor.G;
                    colors[i, j].Z = pixelColor.B;
                    colors[i, j].Normalize();
                    if (double.IsNaN(colors[i, j].X))
                    {
                        colors[i, j].X = colors[i, j].Y = colors[i, j].Z = 0;
                    }
                }
            }
            TriangleMesh.LT = colors;
            triangleMesh.InitializeTriangles();
        }
        private void useImageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TriangleMesh.LT == null)
            {
                useImageCheckBox.Checked = false;
                return;
            }
            if(useImageCheckBox.Checked)
                TriangleMesh.UseImage = true;
            else
                TriangleMesh.UseImage = false;
            triangleMesh.FillBitmap();
        }
        private void loadMapButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image Files |*.jpg;*.jpeg;*.png;";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.FileName))
                    LoadNormalMap(dialog.FileName);
            }
            dialog.Dispose();
        }
        private void useNormalMapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TriangleMesh.INV == null)
            {
                useNormalMapCheckBox.Checked = false;
                return;
            }
            if(useNormalMapCheckBox.Checked)
                TriangleMesh.UseNormalMap = true;
            else
                TriangleMesh.UseNormalMap = false;
            triangleMesh.FillBitmap();

        }
    }
}