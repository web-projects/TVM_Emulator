using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace TVMEmulator.forms
{
    public partial class ucTimings : UserControl
    {
        private long minLatency;
        private long maxLatency;

        private readonly List<long> latencies = new List<long>(10);
        private Stopwatch watch = new Stopwatch();

        public ucTimings()
        {
            InitializeComponent();
        }

        private void ucTimings_Load(object sender, EventArgs e)
        {
        }

        private void ucTimings_Resize(object sender, EventArgs e)
            => HandleResizing();

        private void ucTimings_RegionChanged(object sender, EventArgs e)
            => HandleResizing();

        private void HandleResizing()
        {
            lblMinResponseTimeValue.Left = lblMinResponseTime.Left + lblMinResponseTime.Width + 5;
            lblMaxResponseTimeValue.Left = lblMinResponseTimeValue.Left;

            lblAverageResponseTime.Left = lblMinResponseTimeValue.Left + lblMinResponseTimeValue.Width + 40;
            lblAverageResponseTimeValue.Left = lblAverageResponseTime.Left + lblAverageResponseTime.Width + 5;

            lblLastResponseTime.Left = lblAverageResponseTime.Left;
            lblLastResponseTimeValue.Left = lblAverageResponseTimeValue.Left;
        }

        public void StartTracking()
        {
            if (watch.IsRunning)
            {
                watch.Stop();
            }

            watch = Stopwatch.StartNew();
        }

        public void StopTracking()
        {
            if (watch.IsRunning)
            {
                watch.Stop();
                
                SetLastResponseTime(watch.ElapsedMilliseconds);
            }
        }

        private void SetLastResponseTime(long responseTime)
        {
            if (lblLastResponseTimeValue.InvokeRequired)
            {
                lblLastResponseTimeValue.Invoke(new MethodInvoker(delegate { SetLastResponseTime(responseTime); }));
                return;
            }

            lblLastResponseTimeValue.Text = $"{responseTime}ms";

            UpdateMinLatency(responseTime);
            UpdateMaxLatency(responseTime);
            UpdateAverageLatency(responseTime);
        }

        private void UpdateMinLatency(long responseTime)
        {
            if (minLatency == 0)
            {
                minLatency = responseTime;
            }
            else if (responseTime < minLatency)
            {
                minLatency = responseTime;
            }

            lblMinResponseTimeValue.Text = $"{minLatency}ms";
        }

        private void UpdateMaxLatency(long responseTime)
        {
            if (responseTime > maxLatency)
            {
                maxLatency = responseTime;
            }

            lblMaxResponseTimeValue.Text = $"{maxLatency}ms";
        }

        private void UpdateAverageLatency(long responseTime)
        {
            if (latencies.Count < 2)
            {
                lblAverageResponseTimeValue.Text = "N/A";
            }

            if (latencies.Count == 10)
            {
                latencies.RemoveAt(0);
            }

            latencies.Add(responseTime);

            long average = 0;

            foreach (long latency in latencies)
            {
                average += latency;
            }

            lblAverageResponseTimeValue.Text = $"{average / latencies.Count}ms";
        }
    }
}
