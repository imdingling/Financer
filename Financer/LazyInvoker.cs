using System;
using MonoTouch.Foundation;

namespace Financer
{
    public class LazyInvoker
    {
        private NSTimer timer;
        private double seconds;
        private Action action;

        public LazyInvoker (double seconds, Action action)
        {
            this.action = action;
            this.seconds = seconds;
        }

        public void Run ()
        {
            this.Stop ();
            this.Start ();
        }

        private void Fire ()
        {
            this.timer.Invalidate ();
            this.timer = null;
            this.action ();
        }

        public void Stop()
        {
            if (this.timer != null) {
                this.timer.Invalidate ();
                this.timer = null;
            }
        }

        private void Start()
        {
            this.timer = NSTimer.CreateScheduledTimer (seconds, () => this.Fire ());
        }
    }
}

