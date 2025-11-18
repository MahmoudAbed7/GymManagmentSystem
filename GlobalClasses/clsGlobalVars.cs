using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMS_Business;
using Microsoft.Win32;

namespace GMS.GlobalClasses
{
    public class clsGlobalVars
    {
        public static clsUser CurrentUser;
        public static string EventLogging()
        {
            string sourceName = "GMS_App";
            return sourceName;
        }
        public static bool RememberUserNameAndPassword(string UserName, string Password)
        {
            string KeyPath = @"HKEY_Current_User\SOFTWARE\GMS";
            string ValueName = "GMS_LoginInfo";
            string ValueData = UserName + "/##/" + Password;
            try
            {
                Registry.SetValue(KeyPath, ValueName, ValueData, RegistryValueKind.String);
                EventLog.WriteEntry(EventLogging(), "UserName & Password Saved On Registry", EventLogEntryType.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                EventLog.WriteEntry(EventLogging(), ex.Message, EventLogEntryType.Error);
                return false;
            }
        }
        public static bool GetStoredCredentialFromRegistry(ref string UserName, ref string Password)
        {
            string KeyPath = @"HKEY_Current_User\SOFTWARE\GMS";
            string ValueName = "GMS_LoginInfo";
            try
            {
                string StoredData = Registry.GetValue(KeyPath, ValueName, null) as string;
                if (StoredData != null)
                {
                    string[] Line = StoredData.Split(new string[] { "/##/" }, StringSplitOptions.None);
                    UserName = Line[0];
                    Password = Line[1];
                }

                EventLog.WriteEntry(EventLogging(), "UserName & Password Restored From Registry", EventLogEntryType.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                EventLog.WriteEntry(EventLogging(), ex.Message, EventLogEntryType.Error);
                return false;
            }
        }
    }
}
