using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Classes
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {

            // Generate a new GUID
            Guid newGuid = Guid.NewGuid();

            // convert the GUID to a string
            return newGuid.ToString();
            
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
         
            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
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
     
        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            // Full file name. Change your file name   
            string fileName = sourceFile;
            FileInfo fi = new FileInfo(fileName);
            string extn = fi.Extension;
            return GenerateGUID() + extn;

        }
       
        public static  bool CopyImageToProjectImagesFolder(ref string  sourceFile)
        {
            // this funciton will copy the image to the
            // project images foldr after renaming it
            // with GUID with the same extention, then it will update the sourceFileName with the new name.

            string DestinationFolder = @"C:\DVLD-People-Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, destinationFile, true);

            }
            catch (IOException iox)
            {
                MessageBox.Show (iox.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFile= destinationFile;
            return true;
        }

        public static void LoadDataPage(DataTable sourceTable, DataGridView dataGridView,
                                int currentPage, int pageSize,
                                Label labelInfo, Button btnPrev, Button btnNext)
        {
            try
            {
                int startIndex = (currentPage - 1) * pageSize;
                DataTable pageTable = sourceTable.AsEnumerable().Skip(startIndex).Take(pageSize).CopyToDataTable();
                dataGridView.DataSource = pageTable;
                int totalPages = (int)Math.Ceiling((double)sourceTable.Rows.Count / pageSize);
                labelInfo.Text = $"{currentPage} \\ {totalPages}";
                btnPrev.Enabled = (currentPage > 1);
                btnNext.Enabled = (currentPage < totalPages);
            }
            catch (Exception)
            {
                dataGridView.DataSource = sourceTable.Clone();
                labelInfo.Text = "0 \\ 0";
                btnPrev.Enabled = btnNext.Enabled = false;
            }
        }
    }
}
