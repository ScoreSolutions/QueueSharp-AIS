using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Engine;
using System.Windows.Forms;
using System.IO;

namespace CSITWSWindowService
{
    partial class CSITWSWindowService : ServiceBase
    {
        private const string SOFTWARE_NAME = "CSITWSWindowService";
        
        public CSITWSWindowService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            // TODO: Add code here to start your service.

            Engine.Common.FunctionEng.SaveTransLog("CSITWSWindowService.OnStart", "CSITWSWindowService Start");

            tmCSITWSSurvey = new System.Timers.Timer();
            this.tmCSITWSSurvey.Interval = (60000D); //every 60 secs
            this.tmCSITWSSurvey.Elapsed += new System.Timers.ElapsedEventHandler(this.tmCSITWSSurvey_Tick);
            this.tmCSITWSSurvey.Start();
            tmCSITWSSurvey.Enabled = true;

            tmCSITWSSendATSR = new System.Timers.Timer();
            this.tmCSITWSSendATSR.Interval = (60000D);
            this.tmCSITWSSendATSR.Elapsed += new System.Timers.ElapsedEventHandler(this.tmCSITWSSendATSR_Tick);
            this.tmCSITWSSendATSR.Start();
            tmCSITWSSendATSR.Enabled = true;

            tmCSITWSGetATSRResult = new System.Timers.Timer();
            this.tmCSITWSGetATSRResult.Interval = (60000D);
            this.tmCSITWSGetATSRResult.Elapsed += new System.Timers.ElapsedEventHandler(this.tmCSITWSGetATSRResult_Tick);
            this.tmCSITWSGetATSRResult.Start();
            tmCSITWSGetATSRResult.Enabled = true;

            tmCalTarget = new System.Timers.Timer();
            this.tmCalTarget.Interval = (60000D);
            this.tmCalTarget.Elapsed += new System.Timers.ElapsedEventHandler(this.tmCalTarget_Tick);
            this.tmCalTarget.Start();
            tmCalTarget.Enabled = true;
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            Engine.Common.FunctionEng.SaveTransLog("CSITWSWindowService.OnStop", "CSITWSWindowService Stop");
        }

        private System.Timers.Timer tmCSITWSSurvey;
        private void tmCSITWSSurvey_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmCSITWSSurvey.Enabled = false;
            CreateHeartbeat(SOFTWARE_NAME, "tmCSITWSSurvey");
            CSITWSurveyENG eng = new CSITWSurveyENG();
            try
            {
                if (eng.ReadAndSaveDataFromTextFile() > 0)
                {
                    eng.FilterData();
                }
            }
            catch (Exception ex)
            {
                eng.CreateLogFile(ex.Message + "\n" + ex.StackTrace);
            }
            eng = null;
            tmCSITWSSurvey.Enabled = true;
        }


        private System.Timers.Timer tmCalTarget;
        private void tmCalTarget_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmCalTarget.Enabled = false;
            CreateHeartbeat(SOFTWARE_NAME, "tmCalTarget");
            CSITWSurveyENG eng = new CSITWSurveyENG();
            try
            {
                eng.CalTarget();
            }
            catch (Exception ex)
            {
                eng.CreateLogFile(ex.Message + "\n" + ex.StackTrace);
            }
            eng = null;
            tmCalTarget.Enabled = true;
        }

        private System.Timers.Timer tmCSITWSSendATSR;
        private void tmCSITWSSendATSR_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmCSITWSSendATSR.Enabled = false;
            CreateHeartbeat(SOFTWARE_NAME, "tmCSITWSSendATSR");
            CSITWSurveyENG eng = new CSITWSurveyENG();
            try
            {
                eng.SendATSR();
            }
            catch (Exception ex)
            {
                eng.CreateLogFile( ex.Message + "\n" + ex.StackTrace);
            }
            eng = null;
            tmCSITWSSendATSR.Enabled = true;
        }

        private System.Timers.Timer tmCSITWSGetATSRResult;
        private void tmCSITWSGetATSRResult_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmCSITWSGetATSRResult.Enabled = false;
            CreateHeartbeat(SOFTWARE_NAME, "tmCSITWSGetATSRResult");
            CSITWSurveyENG eng = new CSITWSurveyENG();
            try
            {
                eng.GetATSRResult();
            }
            catch (Exception ex)
            {
                eng.CreateLogFile(ex.Message + "\n" + ex.StackTrace);
            }
            eng = null;
            tmCSITWSGetATSRResult.Enabled = true;
        }

        #region //Log Data
        private object GetSoftwareVersion()
        {
            System.Version v = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            return "AIS CSI TWS Windows Service Version " + v.Major + "." + v.Minor + "." + v.Build + "." + v.Revision;
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

        //public void main(String[] args)
        //{
        //    CSITWSurveyENG eng = new CSITWSurveyENG();
        //    try
        //    {
        //        eng.ReadAndSaveDataFromTextFile();
        //        eng.CalTarget();
        //        eng.FilterData();
        //    }
        //    catch (Exception ex)
        //    {
        //        eng.CreateLogFile("CSITWSWindowService.main Exception : " + ex.Message + "\n" + ex.StackTrace);
        //    }
        //}
    }
}
