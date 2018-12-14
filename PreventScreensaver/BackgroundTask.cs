using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreventScreensaver
{
    public class BackgroundTask
    {
        private bool isCanceled = false;
        public bool Running { get; private set; } = false;

        public void Start()
        {
            if (Running)
            {
                return;
            }

            Task.Run(() =>
            {
                Running = true;
                int maxCount = 4*60;
                int count = 0;
                int x = 0;
                int y = 0;

                while (!isCanceled)
                {
                    var point = Control.MousePosition;
                    if (point.X == x && point.Y == y)
                    {
                        count++;
                    }
                    else
                    {
                        x = point.X;
                        y = point.Y;
                        count = 0;
                    }

                    if (count == maxCount)
                    {
                        count = 0;
                        SendKeys.SendWait("+");
                        Thread.Sleep(1 * 1000);

                        SendKeys.SendWait("^");
                        Thread.Sleep(1 * 1000);

                        SendKeys.SendWait("%");
                    }
                    Thread.Sleep(1 * 1000);
                }
                Running = false;
            });
        }

        public void Stop()
        {
            isCanceled = true;
        }
    }
}
