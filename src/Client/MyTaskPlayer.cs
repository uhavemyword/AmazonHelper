using Common;
using System;
using System.Threading;
using Telerik.WinControls;

namespace Client
{
    public class MyTaskPlayer : MyTask
    {
        private SynchronizationContext _syncContext;
        private RadElement _startControl;
        private RadElement _pauseControl;
        private RadElement _stopControl;

        public MyTaskPlayer(SynchronizationContext syncContext, RadElement startControl, RadElement pauseControl, RadElement stopControl)
        {
            _syncContext = syncContext;
            _startControl = startControl;
            _pauseControl = pauseControl;
            _stopControl = stopControl;

            if (_startControl != null)
            {
                _startControl.Click += StartControl_Click;
            }
            if (_pauseControl != null)
            {
                _pauseControl.Click += PauseControl_Click;
            }
            if (_startControl != null)
            {
                _stopControl.Click += StopControl_Click;
            }
            EnableControls(canStart: true, canPause: false, canStop: false);

            base.Started += MyTaskPlayer_Started;
            base.Paused += MyTaskPlayer_Paused;
            base.Resumed += MyTaskPlayer_Resumed;
            base.Stopped += MyTaskPlayer_Stopped;
        }

        private void EnableControls(bool canStart, bool canPause, bool canStop)
        {
            _syncContext.Send(new SendOrPostCallback(delegate (object state)
            {
                if (_startControl != null)
                {
                    _startControl.Enabled = canStart;
                }
                if (_pauseControl != null)
                {
                    _pauseControl.Enabled = canPause;
                }
                if (_stopControl != null)
                {
                    _stopControl.Enabled = canStop;
                }
            }), null);
        }

        private void MyTaskPlayer_Stopped(object sender, EventArgs e)
        {
            EnableControls(canStart: true, canPause: false, canStop: false);
        }

        private void MyTaskPlayer_Resumed(object sender, EventArgs e)
        {
            EnableControls(canStart: false, canPause: true, canStop: true);
        }

        private void MyTaskPlayer_Paused(object sender, EventArgs e)
        {
            EnableControls(canStart: true, canPause: false, canStop: true);
        }

        private void MyTaskPlayer_Started(object sender, EventArgs e)
        {
            EnableControls(canStart: false, canPause: true, canStop: true);
        }

        private void StartControl_Click(object sender, EventArgs e)
        {
            base.Resume();
            base.Start();
        }

        private void PauseControl_Click(object sender, EventArgs e)
        {
            base.Pause();
        }

        private void StopControl_Click(object sender, EventArgs e)
        {
            base.Stop();
        }
    }
}