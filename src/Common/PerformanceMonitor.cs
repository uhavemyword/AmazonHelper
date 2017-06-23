// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/16/2016 1:35:50 PM
// ------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class PerformanceMonitor
    {
        private int _processId;
        private int? _cpuUsage;
        private decimal? _memoryUsage;
        private Task _task;
        private CancellationTokenSource _tokenSource;

        public PerformanceMonitor() : this(Process.GetCurrentProcess().Id)
        {
        }

        public PerformanceMonitor(int processId)
        {
            this._processId = processId;
        }

        public int ProcessId
        {
            get { return _processId; }
            private set { _processId = value; }
        }

        public int CpuUsage
        {
            get
            {
                return _cpuUsage ?? 0;
            }
            private set
            {
                if (value != _cpuUsage)
                {
                    _cpuUsage = value;
                    OnCpuUsageChanged(value);
                    OnOverallUsageChanged(this.CpuUsage, this.MemoryUsage);
                }
            }
        }

        public decimal MemoryUsage
        {
            get
            {
                return _memoryUsage ?? 0;
            }
            private set
            {
                if (value != _memoryUsage)
                {
                    _memoryUsage = value;
                    OnMemoryUsageChanged(value);
                    OnOverallUsageChanged(this.CpuUsage, this.MemoryUsage);
                }
            }
        }

        public delegate void CpuUsageChangedEventHandler(int percentage);

        public delegate void MemoryUsageChangedEventHandler(decimal usage);

        public delegate void OverallUsageChangedEventHandler(int cpuPercentage, decimal memoryUsage);

        public event CpuUsageChangedEventHandler CpuUsageChanged;

        public event MemoryUsageChangedEventHandler MemoryUsageChanged;

        public event OverallUsageChangedEventHandler OverallUsageChanged;

        public void Monitor(int interval = 1000)
        {
            _tokenSource = new CancellationTokenSource();
            _task = new Task(() => Calculate(interval, _tokenSource.Token), TaskCreationOptions.LongRunning);
            _task.Start();
        }

        public void Stop()
        {
            if (_task != null)
            {
                _tokenSource.Cancel();
                _task.Wait(1000, _tokenSource.Token);
                _task = null;
            }
        }

        private void Calculate(int interval, CancellationToken token)
        {
            var instanceName = GetProcessInstanceName(this._processId);
            if (string.IsNullOrEmpty(instanceName))
            {
                return;
            }
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", instanceName);
            var memoryCounter = new PerformanceCounter("Process", "Working Set - Private", instanceName);

            while (true)
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    this.CpuUsage = (int)(cpuCounter.NextValue() / Environment.ProcessorCount);
                    this.MemoryUsage = (decimal)memoryCounter.NextValue() / 1024;
                    System.Threading.Thread.Sleep(interval);
                }
                catch (InvalidOperationException)
                {
                    // If we get here, probably the instance name changed,
                    // e.g. "CoinSniffer#1" will change to "ConSniffer" after stop "ConSniffer" intance.
                    var newName = GetProcessInstanceName(this._processId);
                    if (string.IsNullOrEmpty(newName))
                    {
                        throw new Exception("Process instance name doesn't exist!");
                    }
                    else
                    {
                        cpuCounter.InstanceName = newName;
                    }
                }
            }
        }

        private string GetProcessInstanceName(int processId)
        {
            try
            {
                var performanceCounterCategory = new PerformanceCounterCategory("Process");
                string[] instances = performanceCounterCategory.GetInstanceNames();
                foreach (string instance in instances)
                {
                    using (var counter = new PerformanceCounter("Process", "ID Process", instance, true))
                    {
                        int id = (int)counter.RawValue;
                        if (id == processId)
                        {
                            return instance;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private void OnOverallUsageChanged(int cpuPercentage, decimal memoryUsage)
        {
            var handler = this.OverallUsageChanged;
            if (handler != null)
            {
                handler(cpuPercentage, memoryUsage);
            }
        }

        private void OnCpuUsageChanged(int percentage)
        {
            var handler = this.CpuUsageChanged;
            if (handler != null)
            {
                handler(percentage);
            }
        }

        private void OnMemoryUsageChanged(decimal usage)
        {
            var handler = this.MemoryUsageChanged;
            if (handler != null)
            {
                handler(usage);
            }
        }
    }
}