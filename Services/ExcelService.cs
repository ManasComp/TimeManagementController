using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TimeManagementController.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace TimeManagementController.Services
{
    class ExcelService
    {
        Excel.Application xlApp;
        Excel.Workbook xlWorkbook;
        Excel._Worksheet xlWorksheet;
        Excel.Range xlRange;
        List<DayProgram> activities;
        FirebaseService firebaseService;

        private string Id;
        private string Url;
        public ExcelService(string id, string url)
        {
            Url = url;
            Id = id;
            firebaseService = new FirebaseService();
        }
        private void CellParse(int x, int y, int activityId)
        {
            Trace.WriteLine("CellParse");
            if (xlRange.Cells[x, y].Value != null && xlRange.Cells[x, y + 2].Value != null && xlRange.Cells[x, y + 3].Value != null)
            {
                double StartHelper;
                double EndHelper;
                activities[activityId].Add(new Activity
                {
                    Start = TimeSpan.FromDays(double.Parse(xlRange.Cells[x, y].Value.ToString())),
                    End = TimeSpan.FromDays(double.Parse(xlRange.Cells[x, y + 2].Value.ToString())),
                    Name = xlRange.Cells[x, y + 3].Value.ToString()
                });
                Trace.WriteLine(activities[activityId][activities[activityId].Count - 1].Name);
            }
        }
        private void Table(int i)
        {
            Trace.WriteLine("Table");
            for (int j = 0; j < 7; j++)
            {
                CellParse(i, 1 + j * 6, j);
            }
        }

        private void Settings()
        {
            Trace.WriteLine("Settings");
            xlApp = new Excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(Url);
            xlWorksheet = xlWorkbook.Sheets[1];
            xlRange = xlWorksheet.UsedRange;
            activities = new List<DayProgram>();

            activities.Add(new DayProgram { Name = "Monday", Id = 0 });
            activities.Add(new DayProgram { Name = "Tuesday", Id = 1 });
            activities.Add(new DayProgram { Name = "Wednesday", Id = 2 });
            activities.Add(new DayProgram { Name = "Thursday", Id = 3 });
            activities.Add(new DayProgram { Name = "Friday", Id = 4 });
            activities.Add(new DayProgram { Name = "Saturday", Id = 5 });
            activities.Add(new DayProgram { Name = "Sunday", Id = 6 });
        }

        private void Cleaning()
        {
            Trace.WriteLine("Cleaning");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private void Loading()
        {
            Trace.WriteLine("Loading");
            Settings();
            for (int i = 2; i < xlRange.Rows.Count; i++)
            {
                Table(i);
            }
            Cleaning();
        }

        public async Task AddData()
        {
            Trace.WriteLine("AddData");
            Loading();

            //string _url = "https://timemanegment-74160.firebaseio.com/";

            await firebaseService.DeleteAsync(Id);
            await firebaseService.PostAsync(Id, activities);
            //FirebaseClient firebaseClient = new FirebaseClient(_url);
            //await firebaseClient.Child(Id).DeleteAsync();
            //await firebaseClient.Child(Id).PostAsync(activities);
            Trace.WriteLine("**************************************************************************************************End");
        }
    }
}
