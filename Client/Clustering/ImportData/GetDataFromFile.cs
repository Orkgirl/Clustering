using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clustering.DataBase;
using Microsoft.Win32;

namespace Clustering.ImportData
{
    public static class GetDataFromFile
    {
        public static List<DocumentData> GetData()
        {
            var result = new List<DocumentData>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var Rawlines = File.ReadAllLines(openFileDialog.FileName);

                foreach (var rawline in Rawlines)
                {
                    rawline.Split(' ');
                }
            }


            return result;
        }
        

    }
}
