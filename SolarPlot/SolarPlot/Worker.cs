using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolarPlot
{
    internal class Worker
    {
        public volatile bool workerShouldClose;
        public volatile bool workerHasClosed;


        private Thread workerThread;
        private List<string> commandQueue;
        private MainForm form;

        public Worker(MainForm form)
        {
            this.form = form;
            this.commandQueue = new List<string>();

            this.workerShouldClose = false;
            this.workerHasClosed = false;
            this.workerThread = new Thread(WorkerThread);
            this.workerThread.IsBackground = true;
            this.workerThread.Start();
        }

        public void Command(string command)
        {
            this.commandQueue.Add(command);
        }
        private void WorkerThread()
        {
            while (workerShouldClose == false)
            {
                while (commandQueue.Count > 0)
                {
                    Parse(commandQueue[0]);
                    commandQueue.RemoveAt(0);
                }
                Thread.Sleep(1);

            }
            workerHasClosed = true;

        }

        private void Parse(string command)
        {
            var commandElements = command.Split(' ');
            commandElements[0].ToUpper();
            SetStatus(commandElements[0]);
            switch (commandElements[0])
            {
                case "LOADCSV":
                    SetStatus("Loading CSV");
                    SetProgressBar(100);
                    break;
                default:

                    break;
            }
        }

        private void SetStatus(string status)
        {
            this.form.SetStatus(status);
        }

        private void SetProgressBar(int progress)
        {
            this.form.SetProgress(progress);
        }
    }
}
