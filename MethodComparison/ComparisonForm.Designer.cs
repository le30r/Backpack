namespace MethodComparison
{
    partial class ComparisonForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.buttonTest = new System.Windows.Forms.Button();
            this.tableGraphics = new System.Windows.Forms.TableLayoutPanel();
            this.chartError = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTime = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableMain.SuspendLayout();
            this.tableGraphics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTime)).BeginInit();
            this.SuspendLayout();
            // 
            // tableMain
            // 
            this.tableMain.ColumnCount = 1;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.Controls.Add(this.buttonTest, 0, 1);
            this.tableMain.Controls.Add(this.tableGraphics, 0, 0);
            this.tableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableMain.Location = new System.Drawing.Point(0, 0);
            this.tableMain.Name = "tableMain";
            this.tableMain.RowCount = 2;
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableMain.Size = new System.Drawing.Size(661, 340);
            this.tableMain.TabIndex = 0;
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonTest.Location = new System.Drawing.Point(293, 303);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 0;
            this.buttonTest.Text = "Тест";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // tableGraphics
            // 
            this.tableGraphics.ColumnCount = 2;
            this.tableGraphics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableGraphics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableGraphics.Controls.Add(this.chartTime, 0, 0);
            this.tableGraphics.Controls.Add(this.chartError, 1, 0);
            this.tableGraphics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableGraphics.Location = new System.Drawing.Point(3, 3);
            this.tableGraphics.Name = "tableGraphics";
            this.tableGraphics.RowCount = 1;
            this.tableGraphics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableGraphics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableGraphics.Size = new System.Drawing.Size(655, 284);
            this.tableGraphics.TabIndex = 1;
            // 
            // chartError
            // 
            chartArea2.AxisX.Title = "Размер задачи (шт.)";
            chartArea2.AxisY.Title = "Относительная ошибка";
            chartArea2.Name = "ChartArea1";
            this.chartError.ChartAreas.Add(chartArea2);
            this.chartError.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartError.Legends.Add(legend2);
            this.chartError.Location = new System.Drawing.Point(330, 3);
            this.chartError.Name = "chartError";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartError.Series.Add(series2);
            this.chartError.Size = new System.Drawing.Size(322, 278);
            this.chartError.TabIndex = 1;
            this.chartError.Text = "chart1";
            // 
            // chartTime
            // 
            chartArea1.AxisX.Title = "Размер задачи(шт.)";
            chartArea1.AxisY.Title = "Время (мс)";
            chartArea1.Name = "ChartArea1";
            this.chartTime.ChartAreas.Add(chartArea1);
            this.chartTime.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartTime.Legends.Add(legend1);
            this.chartTime.Location = new System.Drawing.Point(3, 3);
            this.chartTime.Name = "chartTime";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTime.Series.Add(series1);
            this.chartTime.Size = new System.Drawing.Size(321, 278);
            this.chartTime.TabIndex = 0;
            this.chartTime.Text = "chart1";
            // 
            // ComparisonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 340);
            this.Controls.Add(this.tableMain);
            this.Name = "ComparisonForm";
            this.Text = "Form1";
            this.tableMain.ResumeLayout(false);
            this.tableGraphics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableMain;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.TableLayoutPanel tableGraphics;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTime;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartError;
    }
}

