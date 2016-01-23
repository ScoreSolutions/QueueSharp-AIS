using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Engine;

namespace CSIWindowService
{
    partial class CSIWindowsService : ServiceBase
    {
        private const string SOFTWARE_NAME = "CSIWindowService";

        public CSIWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.

            Engine.Common.FunctionEng.SaveTransLog("CSIWindowService.OnStart", "CSIWindowService Start");

            tmCSISurvey = new System.Timers.Timer();
            this.tmCSISurvey.Interval = (60000D); //every 60 secs
            this.tmCSISurvey.Elapsed += new System.Timers.ElapsedEventHandler(this.tmCSISurvey_Tick);
            this.tmCSISurvey.Start();
            tmCSISurvey.Enabled = true;

            tmCSISendATSR = new System.Timers.Timer();
            this.tmCSISendATSR.Interval = (60000D);
            this.tmCSISendATSR.Elapsed += new System.Timers.ElapsedEventHandler(this.tmCSISendATSR_Tick);
            this.tmCSISendATSR.Start();
            tmCSISendATSR.Enabled = true;

            tmCSIGetATSRResult = new System.Timers.Timer();
            this.tmCSIGetATSRResult.Interval = (60000D);
            this.tmCSIGetATSRResult.Elapsed += new System.Timers.ElapsedEventHandler(this.tmCSIGetATSRResult_Tick);
            this.tmCSIGetATSRResult.Start();
            tmCSIGetATSRResult.Enabled = true;

            tmSendResultToShop = new System.Timers.Timer();
            this.tmSendResultToShop.Interval = (60000D);
            this.tmSendResultToShop.Elapsed += new System.Timers.ElapsedEventHandler(tmSendResultToShop_Tick);
            this.tmSendResultToShop.Start();
            tmSendResultToShop.Enabled = true;

            tmCalTarget = new System.Timers.Timer();
            this.tmCalTarget.Interval = (60000D);
            this.tmCalTarget.Elapsed += new System.Timers.ElapsedEventHandler(this.tmCalTarget_Tick);
            this.tmCalTarget.Start();
            tmCalTarget.Enabled = true;
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            Engine.Common.FunctionEng.SaveTransLog("CSIWindowService.OnStop", "CSIWindowService Stop");
        }

        private System.Timers.Timer tmCSISurvey;
        private void tmCSISurvey_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmCSISurvey.Enabled = false;
            CreateHeartbeat(SOFTWARE_NAME, "tmCSISurvey");
            CSISurveyENG eng = new CSISurveyENG();
            try {
                eng.FilterData();
            }
            catch (Exception ex) {
                eng.CreateLogFile("tmCSISurvey_Tick", ex.Message + "\n" +  ex.StackTrace);
            }
            eng = null;
            tmCSISurvey.Enabled = true;
        }


        private System.Timers.Timer tmCalTarget;
        private void tmCalTarget_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmCalTarget.Enabled = false;
            CreateHeartbeat(SOFTWARE_NAME, "tmCalTarget");
            CSISurveyENG eng = new CSISurveyENG();
            try
            {
                eng.CalTarget();
            }
            catch (Exception ex)
            {
                eng.CreateLogFile("tmCalTarget_Tick", ex.Message + "\n" + ex.StackTrace);
            }
            eng = null;
            tmCalTarget.Enabled = true;
        }

        private System.Timers.Timer tmCSISendATSR;
        private void tmCSISendATSR_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmCSISendATSR.Enabled = false;
            CreateHeartbeat(SOFTWARE_NAME, "tmCSISendATSR");
            CSISurveyENG eng = new CSISurveyENG();
            try {
                eng.SendATSR();
            }
            catch (Exception ex) {
                eng.CreateLogFile("tmCSISendATSR_Tick", ex.Message + "\n" + ex.StackTrace);
            }
            eng = null;
            tmCSISendATSR.Enabled = true;
        }

        private System.Timers.Timer tmCSIGetATSRResult;
        private void tmCSIGetATSRResult_Tick(object sender, System.Timers.ElapsedEventArgs e){
            tmCSIGetATSRResult.Enabled = false;
            CreateHeartbeat(SOFTWARE_NAME, "tmCSIGetATSRResult");
            CSISurveyENG eng = new CSISurveyENG();
            try {
                eng.GetATSRResult();
            }
            catch (Exception ex)
            {
                eng.CreateLogFile("tmCSIGetATSRResult_Tick", ex.Message + "\n" + ex.StackTrace);
            }
            eng = null;

            tmCSIGetATSRResult.Enabled = true;
        }

        private System.Timers.Timer tmSendResultToShop;
        private void tmSendResultToShop_Tick(object sender, System.Timers.ElapsedEventArgs e) {
            tmSendResultToShop.Enabled = false;
            CreateHeartbeat(SOFTWARE_NAME, "tmSendResultToShop");
            CSISurveyENG eng = new CSISurveyENG();
            try
            {
                eng.SendResultToShop();
            }
            catch (Exception ex)
            {
                eng.CreateLogFile("tmSendResultToShop_Tick", ex.Message + "\n" + ex.StackTrace);
            }
            eng = null;


            tmSendResultToShop.Enabled = true;
        }


        #region //Log Data
        private object GetSoftwareVersion()
        {
            System.Version v = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            return "AIS CSI Windows Service Version " + v.Major + "." + v.Minor + "." + v.Build + "." + v.Revision;
        }


        private void CreateHeartbeat(string SoftwareName, string TimerName)
        {
            string LogPath = Application.StartupPath + "\\HeartBeat\\";
            if (Directory.Exists(LogPath) == false)
            {
                Directory.CreateDirectory(LogPath);
            }

            string FileName = LogPath + "HeartBeat_" + SoftwareName + "_Timer_" + TimerName + ".txt";
            StreamWriter obj = new StreamWriter(FileName, false);
            obj.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
            obj.Flush();
            obj.Close();
        }


        private void CreateErrorLog(string SoftwareName, string LogMessage)
        {
            string LogPath = Application.StartupPath + "\\ErrorLog\\";
            if (Directory.Exists(LogPath) == false)
            {
                Directory.CreateDirectory(LogPath);
            }

            string FileName = LogPath + SoftwareName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            StreamWriter obj = new StreamWriter(FileName, true);
            obj.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") + LogMessage + "\n\n");
            obj.Flush();
            obj.Close();
        }
        #endregion

        //public  void main(String[] args)
        //{
        //    CSISurveyENG eng = new CSISurveyENG();
        //    try
        //    {
        //        eng.FilterData();
        //    }
        //    catch (Exception ex)
        //    {
        //        eng.CreateLogFile("tmCSISurvey_Tick", ex.Message + "\n" + ex.StackTrace);
        //    }
        //}
    }
}
