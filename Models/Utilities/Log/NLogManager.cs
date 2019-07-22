﻿using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Log
{
    public static class NLogManager
    {
        /// <summary>
        /// 
        /// </summary>
        volatile static Logger Log = LogManager.GetLogger("Logging");

        /// <summary>
        /// Writes an Error to the log.
        /// </summary>
        /// <param name="ex"></param>
        public static void PublishException(Exception ex)
        {
            Log.Error(string.Format("\t{0}{1}{2}{3}{4}", GetCalleeString(), Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace));
        }

        public static void LogError(string message)
        {
            Log.Error(":\t" + GetCalleeString() + Environment.NewLine + "\t" + message);
        }

        /// <summary>
        /// Writes an Error to the log.
        /// </summary>
        /// <param name="ex"></param>
        public static void LogMessage(string message)
        {

            Log.Info(":\t" + message);
        }

 
        /// <summary>
        /// Writes an Error to the log.
        /// </summary>
        /// <param name="ex"></param>
        public static void Warning(string message)
        {
            Log.Warn(string.Format(":\t{0}{1}\t{2}", GetCalleeString(), Environment.NewLine, message));
        }

        /// <summary>
        /// Writes an Debug to the log.
        /// </summary>
        /// <param name="ex"></param>
        public static void Debug(string message)
        {
            Log.Error(string.Format(":\t{0}{1}\t{2}", GetCalleeString(), Environment.NewLine, message));
        }

        public static string GetValueOfObject(object ob)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (System.Reflection.PropertyInfo piOrig in ob.GetType().GetProperties())
                {
                    object editedVal = ob.GetType().GetProperty(piOrig.Name).GetValue(ob, null);
                    sb.AppendFormat("{0}:{1}\t ", piOrig.Name, editedVal);
                }
            }
            catch
            {
            }
            return sb.ToString();
        }

        private static string GetCalleeString()
        {
            foreach (StackFrame sf in new StackTrace().GetFrames())
            {
                if (!string.IsNullOrEmpty(sf.GetMethod().ReflectedType.Namespace) && !typeof(NLogManager).FullName.StartsWith(sf.GetMethod().ReflectedType.Namespace))
                {
                    return string.Format("{0}.{1} ", sf.GetMethod().ReflectedType.Name, sf.GetMethod().Name);
                }
            }

            return string.Empty;
        }
    }
}
