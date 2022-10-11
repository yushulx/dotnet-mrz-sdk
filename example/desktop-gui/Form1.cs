using System.Drawing.Imaging;
using Dynamsoft;
using Result = Dynamsoft.MrzScanner.Result;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Point = OpenCvSharp.Point;
using static Dynamsoft.MrzScanner;
using System.Text.Json.Nodes;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace Test
{
    public partial class Form1 : Form
    {
        private MrzScanner scanner;
        private VideoCapture capture;
        private bool isCapturing;
        private Thread? thread;
        private Mat _mat = new Mat();
        private Result[]? _results;

        public Form1()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Form1_Closing);
            string license = "DLS2eyJoYW5kc2hha2VDb2RlIjoiMjAwMDAxLTE2NDk4Mjk3OTI2MzUiLCJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSIsInNlc3Npb25QYXNzd29yZCI6IndTcGR6Vm05WDJrcEQ5YUoifQ==";

            int ret = MrzScanner.InitLicense(license); // Get a license key from https://www.dynamsoft.com/customer/license/trialLicense?product=ddn
            if (ret != 0) MessageBox.Show("License is invalid!");
            scanner = MrzScanner.Create();
            capture = new VideoCapture(0);
            isCapturing = false;
            scanner.LoadModel();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Code
            scanner.Destroy();
        }

        private void ShowResults(Result[] results)
        {
            if (results == null)
                return;
        }
        private Bitmap DecodeMat(Mat mat)
        {
            _results = scanner.DetectBuffer(mat.Data, mat.Cols, mat.Rows, (int)mat.Step(), MrzScanner.ImagePixelFormat.IPF_RGB_888);
            if (_results != null)
            {
                string[] lines = new string[_results.Length];
                var index = 0;
                foreach (Result result in _results)
                {
                    lines[index++] = result.Text;
                    richTextBox1.Text += result.Text + Environment.NewLine;
                    if (result.Points != null)
                    {
                        Point[] points = new Point[4];
                        for (int i = 0; i < 4; i++)
                        {
                            points[i] = new Point(result.Points[i * 2], result.Points[i * 2 + 1]);
                        }
                        Cv2.DrawContours(mat, new Point[][] { points }, 0, Scalar.Red, 2);
                    }
                }

                JsonNode? info = Parse(lines);
                if (info != null) richTextBox1.Text = info.ToString();
            }

            Bitmap bitmap = BitmapConverter.ToBitmap(mat);
            return bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopScan();
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image files (*.bmp, *.jpg, *.png) | *.bmp; *.jpg; *.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _mat = Cv2.ImRead(dlg.FileName, ImreadModes.Color);
                        Mat copy = new Mat(_mat.Rows, _mat.Cols, MatType.CV_8UC3);
                        _mat.CopyTo(copy);
                        pictureBox1.Image = BitmapConverter.ToBitmap(copy);
                        pictureBox2.Image = DecodeMat(copy);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!capture.IsOpened())
            {
                MessageBox.Show("Failed to open video stream or file");
                return;
            }

            if (button2.Text == "Camera Scan")
            {
                StartScan();
            }
            else
            {
                StopScan();
            }
        }

        private void StartScan()
        {
            button2.Text = "Stop";
            isCapturing = true;
            thread = new Thread(new ThreadStart(FrameCallback));
            thread.Start();
        }

        private void StopScan()
        {
            button2.Text = "Camera Scan";
            isCapturing = false;
            if (thread != null) thread.Join();
        }

        private void FrameCallback()
        {
            while (isCapturing)
            {
                capture.Read(_mat);
                Mat copy = new Mat(_mat.Rows, _mat.Cols, MatType.CV_8UC3);
                _mat.CopyTo(copy);
                pictureBox1.Image = DecodeMat(copy);
            }
        }

        private void Form1_Closing(object? sender, FormClosingEventArgs e)
        {
            StopScan();
        }

        private void enterLicenseKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string license = InputBox("Enter License Key", "", "");
            if (license != null && license != "")
            {
                int ret = MrzScanner.InitLicense(license);
            }
        }

        public static string InputBox(string title, string promptText, string value)
        {
            Form form = new Form();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(60, 72, 80, 30);
            buttonCancel.SetBounds(260, 72, 80, 30);

            form.ClientSize = new System.Drawing.Size(400, 120);
            form.Controls.AddRange(new Control[] { textBox, buttonOk, buttonCancel });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            return textBox.Text;
        }
    }
}
