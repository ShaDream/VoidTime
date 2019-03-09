using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VoidTime.Generator
{
    public class BackgroundFormHelper
    {
        #region Default Constructor

        public BackgroundFormHelper(Panel panel)
        {
            generator = new BackgroundGenerater { BackColor = Color.Black };

            GridInitialization(panel);

            ImageViewerInitialization();

            RightPanelInitialization();

            SizeInitialization();

            StarCountInitialization();

            PatternBoxInitialization();

            ColorInitialization();

            GenerateButtonInitialization();

            SaveButtonInitialization();

            AddToControls();
        }

        #endregion

        #region Private Fields

        private readonly BackgroundGenerater generator;
        private TableLayoutPanel grid;
        private PictureBox ImageViewer;
        private FlowLayoutPanel rightPanel;
        private ListView patternBox;
        private Button deletePatternButton;
        private Button generateButton;
        private Button saveButton;
        private FlowLayoutPanel spawnRate;
        private Label spawnRateLabel;
        private NumericUpDown spawnRateNumber;
        private FlowLayoutPanel size;
        private Label sizeLabel;
        private NumericUpDown sizeX;
        private NumericUpDown sizeY;
        private ListView colorBox;
        private Button deleteColor;
        private Button addColor;
        private ColorDialog colorDialog;
        private SaveFileDialog saveDialog;
        private NumericUpDown starCount;
        private Label starCountLabel;

        #endregion

        #region Initializations Helper Methods

        private void StarCountInitialization()
        {
            starCountLabel = new Label { Text = "Star Count", Width = (rightPanel.Width - 15) / 2 };
            starCount = new NumericUpDown { Maximum = decimal.MaxValue, Value = generator.StarCount, Width = (rightPanel.Width - 15) / 2 };
            starCount.ValueChanged += (s, a) =>
            {
                if (starCount.Value >= 0)
                    generator.StarCount = (int)starCount.Value;
            };
        }

        private void GenerateButtonInitialization()
        {
            generateButton = new Button { Text = "Generate", Width = rightPanel.Width - 5 };
            generateButton.Click += (s, a) =>
            {
                if (ImageViewer.Image != null)
                {
                    ImageViewer.Image.Dispose();
                    ImageViewer.Image = null;
                }
                ImageViewer.Image = generator.GetImage();
            };
        }

        private void SaveButtonInitialization()
        {
            saveButton = new Button { Text = "Save", Width = rightPanel.Width - 5 };
            saveDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = ".png"
            };
            saveButton.Click += (s, a) =>
            {
                if (ImageViewer.Image == null)
                    return;

                if (saveDialog.ShowDialog() == DialogResult.OK) ImageViewer.Image.Save(saveDialog.FileName);
            };
        }

        private void ColorInitialization()
        {
            colorBox = new ListView
            {
                AllowDrop = true,
                Width = rightPanel.Width - 5,
                LargeImageList = new ImageList(),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Height = 200
            };

            deleteColor = new Button { Text = "Delete", Width = (rightPanel.Width - 12) / 2, Enabled = false };
            addColor = new Button { Text = "Add", Width = (rightPanel.Width - 12) / 2 };
            colorDialog = new ColorDialog { AnyColor = true };

            deleteColor.Click += DeleteColor_Click;

            colorBox.SelectedIndexChanged += (s, a) => { deleteColor.Enabled = colorBox.SelectedItems.Count != 0; };
            addColor.Click += AddColor_Click;
        }

        private void SizeInitialization()
        {
            size = new FlowLayoutPanel { Width = rightPanel.Width - 5, Height = 60 };
            sizeLabel = new Label { Text = "Size", Width = rightPanel.Width - 5 };
            sizeX = new NumericUpDown
            {
                Width = (rightPanel.Width - 20) / 2,
                Maximum = decimal.MaxValue,
                Value = generator.Resolution.Width
            };
            sizeY = new NumericUpDown
            {
                Width = (rightPanel.Width - 20) / 2,
                Maximum = decimal.MaxValue,
                Value = generator.Resolution.Height
            };

            sizeX.ValueChanged += (s, a) => { generator.Resolution.Width = (int)sizeX.Value; };
            sizeY.ValueChanged += (s, a) => { generator.Resolution.Height = (int)sizeY.Value; };

            size.Controls.Add(sizeLabel);
            size.Controls.Add(sizeX);
            size.Controls.Add(sizeY);
        }

        private void PatternBoxInitialization()
        {
            patternBox = new ListView
            {
                AllowDrop = true,
                Width = rightPanel.Width - 5,
                LargeImageList = new ImageList(),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Height = 200
            };

            patternBox.DragEnter += (s, a) => a.Effect = DragDropEffects.All;
            patternBox.DragDrop += PatternBox_DragDrop;
            patternBox.SelectedIndexChanged += PatternBox_SelectedIndexChanged;

            deletePatternButton = new Button { Text = "Delete", Width = rightPanel.Width - 5, Enabled = false };

            spawnRate = new FlowLayoutPanel { Width = rightPanel.Width - 5, Visible = false, Height = 30 };
            spawnRateLabel = new Label { Text = "Spawn rate: " };
            spawnRateNumber = new NumericUpDown { DecimalPlaces = 4 };

            spawnRate.Controls.Add(spawnRateLabel);
            spawnRate.Controls.Add(spawnRateNumber);

            spawnRateNumber.ValueChanged += SpawnRateNumber_ValueChanged;
            deletePatternButton.Click += DeletePatternButton_Click;
        }

        private void RightPanelInitialization()
        {
            rightPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill
            };
            grid.Controls.Add(rightPanel, 1, 0);

            rightPanel.Resize += RightPanel_Resize;
        }

        private void ImageViewerInitialization()
        {
            ImageViewer = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            grid.Controls.Add(ImageViewer, 0, 0);
        }

        private void GridInitialization(Panel panel)
        {
            grid = new TableLayoutPanel { Dock = DockStyle.Fill };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.Controls.Add(grid);
        }

        private void PatternBox_DragDrop(object sender, DragEventArgs e)

        {
            var fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (var s1 in fileList)
            {
                if (Path.GetExtension(s1) != ".png")
                    continue;

                patternBox.LargeImageList.Images.Add(new Bitmap(s1));
                patternBox.Items.Add(Path.GetFileName(s1).Replace(Path.GetExtension(s1), ""),
                    patternBox.LargeImageList.Images.Count - 1);
                generator.PathPatterns.Add(s1);
                generator.PatternsSpawnRate.Add(1);
            }
        }

        private void SpawnRateNumber_ValueChanged(object sender, EventArgs e)
        {
            var index = generator.PathPatterns.FindIndex(str =>
                Path.GetFileName(str).Replace(Path.GetExtension(str), "") == patternBox.SelectedItems[0].Text);
            generator.PatternsSpawnRate[index] = (float)spawnRateNumber.Value;
        }

        private void DeletePatternButton_Click(object sender, EventArgs e)
        {
            var items = patternBox.SelectedItems;

            while (items.Count != 0)
            {
                var item = items[0];
                var index = generator.PathPatterns.FindIndex(str =>
                    Path.GetFileName(str).Replace(Path.GetExtension(str), "") == item.Text);
                generator.PatternsSpawnRate.RemoveAt(index);
                generator.PathPatterns.RemoveAt(index);
                patternBox.LargeImageList.Images[item.Index].Dispose();
                patternBox.LargeImageList.Images.RemoveAt(item.Index);
                patternBox.Items.Remove(item);
            }
        }

        private void PatternBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = patternBox.SelectedItems;
            if (items.Count == 1)
            {
                var index = generator.PathPatterns.FindIndex(str =>
                    Path.GetFileName(str).Replace(Path.GetExtension(str), "") == items[0].Text);
                spawnRateNumber.Value = (decimal)generator.PatternsSpawnRate[index];

                spawnRate.Visible = true;
            }
            else
            {
                spawnRate.Visible = false;
            }

            deletePatternButton.Enabled = items.Count > 0;
        }

        private void DeleteColor_Click(object sender, EventArgs e)
        {
            var items = colorBox.SelectedItems;

            while (items.Count != 0)
            {
                var item = items[0];
                generator.ColorPatterns.RemoveAt(item.Index);
                colorBox.LargeImageList.Images[item.Index].Dispose();
                colorBox.LargeImageList.Images.RemoveAt(item.Index);
                colorBox.Items.Remove(item);
            }
        }

        private void AddColor_Click(object sender, EventArgs e)
        {
            var result = colorDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            var preview = new Bitmap(50, 50);
            for (var x = 0; x < 50; x++)
                for (var y = 0; y < 50; y++)
                    preview.SetPixel(x, y, colorDialog.Color);

            generator.ColorPatterns.Add(colorDialog.Color);
            colorBox.LargeImageList.Images.Add(preview);
            colorBox.Items.Add("Color", colorBox.Items.Count);
        }

        private void RightPanel_Resize(object sender, EventArgs e)
        {
            generateButton.Width = rightPanel.Width - 5;
            saveButton.Width = rightPanel.Width - 5;
            patternBox.Width = rightPanel.Width - 5;
            deletePatternButton.Width = rightPanel.Width - 5;
            spawnRate.Width = rightPanel.Width - 5;
            size.Width = rightPanel.Width - 5;
            sizeLabel.Width = rightPanel.Width - 5;
            sizeX.Width = (rightPanel.Width - 20) / 2;
            sizeY.Width = (rightPanel.Width - 20) / 2;
            deleteColor.Width = (rightPanel.Width - 12) / 2;
            addColor.Width = (rightPanel.Width - 12) / 2;
            colorBox.Width = rightPanel.Width - 5;
            starCountLabel.Width = (rightPanel.Width - 15) / 2;
            starCount.Width = (rightPanel.Width - 15) / 2;
        }

        private void AddToControls()
        {
            rightPanel.Controls.Add(size);
            rightPanel.Controls.Add(starCountLabel);
            rightPanel.Controls.Add(starCount);
            rightPanel.Controls.Add(patternBox);
            rightPanel.Controls.Add(deletePatternButton);
            rightPanel.Controls.Add(spawnRate);
            rightPanel.Controls.Add(colorBox);
            rightPanel.Controls.Add(deleteColor);
            rightPanel.Controls.Add(addColor);
            rightPanel.Controls.Add(generateButton);
            rightPanel.Controls.Add(saveButton);
        }

        #endregion
    }
}