using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class MyTask
    {
        private ManualResetEvent _pauseSignal = new ManualResetEvent(true);
        private CancellationTokenSource _cts;
        private Task _task;
        private Action _action;
        private volatile int _threadCount;
        private volatile bool _isPaused;

        public event EventHandler Started;

        public event EventHandler Paused;

        public event EventHandler Resumed;

        public event EventHandler Stopped;

        /// <summary>
        /// Task is started or paused
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return _task != null && (_task.Status < TaskStatus.RanToCompletion);
            }
        }

        public virtual void Init(Action action, int threadCount = 1)
        {
            _action = action;
            _threadCount = threadCount;
        }

        public virtual void Start()
        {
            if (!IsRunning)
            {
                _cts = new CancellationTokenSource();
                _pauseSignal.Set();
                _isPaused = false;
                _task = new Task(() =>
                {
                    for (int i = 0; i < _threadCount; i++)
                    {
                        var childTask = new Task(() =>
                        {
                            try
                            {
                                _action();
                            }
                            catch (OperationCanceledException)
                            {
                                // do nothing, this is expected if a task is canceled in running state.
                            }
                        }, _cts.Token, TaskCreationOptions.AttachedToParent);
                        childTask.Start();
                    }

                    Started?.Invoke(this, null);
                }, _cts.Token);
                _task.ContinueWith(t => Stopped?.Invoke(this, null));
                _task.Start();
            }
        }

        public virtual void Pause()
        {
            if (IsRunning && !_isPaused)
            {
                _pauseSignal.Reset();
                _isPaused = true;
                Paused?.Invoke(this, null);
            }
        }

        public virtual void Resume()
        {
            if (IsRunning && _isPaused)
            {
                _pauseSignal.Set();
                _isPaused = false;
                Resumed?.Invoke(this, null);
            }
        }

        public virtual void Stop()
        {
            if (IsRunning)
            {
                _cts.Cancel();
                _pauseSignal.Set();
            }
        }

        public virtual void MonitorSignal()
        {
            _cts.Token.ThrowIfCancellationRequested();
            _pauseSignal.WaitOne();
        }
    }
}