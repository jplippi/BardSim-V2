using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class LogData
    {
        private DataTable log;

        public DataTable Log { get => log; }

        public LogData()
        {
            log = new DataTable();
            log.BeginInit();
            log.Columns.Add("Type", typeof(LogActionType));
            log.Columns.Add("Timestamp", typeof(decimal));
            log.Columns.Add("Name", typeof(string));
            log.Columns.Add("Damage", typeof(string));
            log.Columns.Add("IsCrit", typeof(bool));
            log.Columns.Add("IsDhit", typeof(bool));
            log.Columns.Add("IsBarrage", typeof(bool));
            log.EndInit();
        }

        public List<string> GetLog()
        {
            List<string> logList = new List<string>();

            foreach(DataRow row in log.Select())
            {
                string substring = "";

                string crit = "";
                
                if((bool)row["IsCrit"] && (bool)row["IsDhit"])
                {
                    crit = "Critical direct hit!! ";
                }
                else if ((bool)row["IsCrit"])
                {
                    crit = "Critical hit! ";
                }
                else if ((bool)row["IsDhit"])
                {
                    crit = "Direct hit! ";
                }

                if ((LogActionType)row["Type"] == LogActionType.DoT)
                {
                    substring = String.Format("      [{0:000.00}] {3}{1} ticks for {2} damage.", ((decimal)row["Timestamp"]).ToString(), (string)row["Name"], (string)row["Damage"], crit);
                }
                else if ((LogActionType)row["Type"] == LogActionType.AutoAttack)
                {
                    substring = String.Format("      [{0:000.00}] {3}{1} ticks for {2} damage.", ((decimal)row["Timestamp"]).ToString(), (string)row["Name"], (string)row["Damage"], crit);
                }
                else if ((LogActionType)row["Type"] == LogActionType.oGCD)
                {
                    substring = String.Format("   [{0:000.00}] {3}{1} hits for {2} damage.", ((decimal)row["Timestamp"]).ToString(), (string)row["Name"], (string)row["Damage"], crit);
                }
                else
                {
                    if ((bool)row["IsBarrage"])
                    {
                        substring = String.Format("[{0:000.00}] {3}{1} hits for {2} damage.", ((decimal)row["Timestamp"]).ToString(), (string)row["Name"], (string)row["Damage"], crit);
                    }
                    else
                    {
                        substring = String.Format("\n[{0:000.00}] {3}{1} hits for {2} damage.", ((decimal)row["Timestamp"]).ToString(), (string)row["Name"], (string)row["Damage"], crit);
                    }
                }

                logList.Add(substring);
                logList.Add("\n");

            }

            return logList;
        }
    }
}
