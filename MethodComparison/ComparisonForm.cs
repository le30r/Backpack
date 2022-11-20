using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backpack;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;
using Cursor = System.Windows.Forms.Cursor;

namespace MethodComparison
{
    public partial class ComparisonForm : Form
    {
        protected List<AbstractBackpack> solvers;
        protected List<string> friendlyNames;
        protected List<bool> display;
        protected List<Color> colors; 
        protected AbortableBackgroundWorker bw;
        protected int minTaskSize = 10;
        protected int maxTaskSize = 80;

        public ComparisonForm()
        {
            InitializeComponent();
            InitializeSolvers();
            InitializeFriendlyNames();
            InitializeColors();
            bw = new AbortableBackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
        }

        private void InitializeColors()
        {
            colors = new List<Color>();
            colors.Add(Color.Blue);
            colors.Add(Color.Red);
        }

        private void InitializeSolvers()
        {
            solvers = new List<AbstractBackpack>();
            solvers.Add(new ExhausiveSearchBackpack()); //полный перебор
            solvers.Add(new BranchAndBoundBackpack()); //метод ветвей и границ
        }

        private void InitializeFriendlyNames()
        {
            friendlyNames = new List<string>();
            Type type = typeof (MethodType);
            foreach (var item in solvers)
            {
                var memberInfo = type.GetMember(item.Method.ToString());
                var attribute = memberInfo[0].GetCustomAttribute<MethodFriendlyName>();
                if (attribute != null)
                    friendlyNames.Add(attribute.Name);
                else friendlyNames.Add("NAME_NOT_FOUND");
            }
        }

        private void GetDisplayed()
        {            
            display = new List<bool>();
            for(int i = 0; i < solvers.Count; i++)
            {
                display.Add(true); //отображать всё
            }
            //TODO: отображаемые методы выбираются чекбоксами
        }

        private void UpdateData()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (bw.IsBusy)
            {
                bw.Abort();
                bw.Dispose();
            }
            while (bw.IsBusy)
                Application.DoEvents();
            ClearCharts();
            InitializeSeries();
            bw.DoWork += CollectData;
            bw.RunWorkerCompleted += HandleWorkerEnd;
            bw.ProgressChanged += UpdateProgress;
            bw.RunWorkerAsync();
        }

        void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ClearCharts()
        {
            if (chartError.InvokeRequired)
            {
                chartError.Invoke(new Action(ClearCharts));
            }
            else
            {
                chartError.Series.Clear();
                chartTime.Series.Clear();
                chartTime.ChartAreas[0].AxisY.IsLogarithmic = false;
            }
        }

        void HandleWorkerEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                chartTime.ChartAreas[0].AxisY.IsLogarithmic = true;
                chartTime.Update();
                chartError.Update();
            }
            else
            {
                ClearCharts();
            }
            Cursor.Current = Cursors.Default;
            bw.DoWork -= CollectData;
            bw.ProgressChanged -= UpdateProgress;
            bw.RunWorkerCompleted -= HandleWorkerEnd;
        }

        void CollectData(object sender, DoWorkEventArgs e)
        {            
            GetDisplayed();
            //по всем задачам
            for (int i = minTaskSize; i <= maxTaskSize; i++)
            {
                var task = TaskGenerator.GetRandomTask(i);
                double[] solutions = new double[solvers.Count]; //решения для каждого метода
                for (int j = 0; j < solvers.Count; j++) //по всем методам
                {
                    Stopwatch sw = new Stopwatch();
                    solvers[j].MaxWeight = task.Item2;
                    sw.Start();
                    var solution = solvers[j].SolveZeroOne(task.Item1);
                    sw.Stop();
                    this.Invoke(new Action(() => { chartTime.Series[j].Points.AddXY(
                        i, sw.ElapsedMilliseconds > 0? sw.ElapsedMilliseconds : 1); }));                    
                    solutions[j] = solvers[j].TotalCost(task.Item1, solution);
                }
                double max = solutions.Max();
                for (int j = 0; j < solvers.Count; j++)
                    this.Invoke(
                        new Action(() => { chartError.Series[j].Points.AddXY(i, (max - solutions[j])/max); }));
            }
        }

        private void InitializeSeries()
        {
            for (int i = 0; i < solvers.Count; i++)
            {
                Series s1 = new Series();
                s1.ChartType = SeriesChartType.Line;
                s1.Color = colors[i];
                s1.BorderWidth = 2;
                s1.Name = friendlyNames[i];
                chartTime.Series.Add(s1);
                Series s2 = new Series();
                s2.ChartType = SeriesChartType.Line;
                s2.Color = colors[i];
                s2.BorderWidth = 2;
                s2.Name = friendlyNames[i];
                chartError.Series.Add(s2);
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
    }
}
