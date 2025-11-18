using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMS.clsGlobal
{
    public class clsUtil
    {
        public static string GenerateGuid() 
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static bool CreateFolderIfNotExist(string FolderDirectory) 
        {
            
            if (!Directory.Exists(FolderDirectory)) 
            {
                try
                {
                    Directory.CreateDirectory(FolderDirectory);
                    return true;
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;

                }

            }
            return true;
        }

        public static string ReplcaeFileNameWithGuid(string FileName) 
        {
            FileInfo FileInfo = new FileInfo(FileName);
            return GenerateGuid() + FileInfo.Extension;

        }

        public static bool CopyImageToProjectImageFolder(ref string SourceFile) 
        {
            string FolderPath = "C:/ GymImageFolder/";
            if (!CreateFolderIfNotExist(FolderPath)) return false;
            string DestinationFile = FolderPath + ReplcaeFileNameWithGuid(SourceFile);
            try
            {
                File.Copy(SourceFile, DestinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SourceFile = DestinationFile;
            return true;
        }
    }
}
