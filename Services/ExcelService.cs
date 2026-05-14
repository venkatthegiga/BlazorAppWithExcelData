using BlazorExcelApp.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlazorExcelApp.Services
{
    public class ExcelService
    {
        private readonly string _filePath;
        private const int HeaderRow = 6;
        private const int DataStartRow = 7;

        public ExcelService(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "ProjectData.xlsx");
        }

        public List<ProjectTask> GetTasks()
        {
            var tasks = new List<ProjectTask>();
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RowsUsed().Where(r => r.RowNumber() >= DataStartRow).ToList();
                Console.WriteLine($"Found {rows.Count} rows in Excel starting from row {DataStartRow}");

                foreach (var row in rows)
                {
                    try
                    {
                        var task = new ProjectTask
                        {
                            Id = row.RowNumber(),
                            ProjectName = row.Cell(2).GetValue<string>(),
                            TaskName = row.Cell(3).GetValue<string>(),
                            AssignedTo = row.Cell(4).GetValue<string>(),
                            StartDate = row.Cell(5).IsEmpty() ? (DateTime?)null : row.Cell(5).GetDateTime(),
                            DaysRequired = row.Cell(6).IsEmpty() ? 0 : row.Cell(6).GetValue<int>(),
                            EndDate = row.Cell(7).IsEmpty() ? (DateTime?)null : row.Cell(7).GetDateTime(),
                            Progress = row.Cell(8).IsEmpty() ? 0 : row.Cell(8).GetValue<double>()
                        };
                        tasks.Add(task);
                        Console.WriteLine($"Loaded task: {task.Id} - {task.TaskName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading row {row.RowNumber()}: {ex.Message}");
                    }
                }
            }
            return tasks;
        }

        public void AddTask(ProjectTask task)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? HeaderRow;
                var newRow = worksheet.Row(lastRow + 1);

                newRow.Cell(2).Value = task.ProjectName;
                newRow.Cell(3).Value = task.TaskName;
                newRow.Cell(4).Value = task.AssignedTo;
                newRow.Cell(5).Value = task.StartDate;
                newRow.Cell(6).Value = task.DaysRequired;
                newRow.Cell(7).Value = task.EndDate;
                newRow.Cell(8).Value = task.Progress;

                workbook.Save();
            }
        }

        public void UpdateTask(ProjectTask task)
        {
            Console.WriteLine($"Updating task with Id (Row): {task.Id}");
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                
                worksheet.Cell(task.Id, 2).Value = task.ProjectName;
                worksheet.Cell(task.Id, 3).Value = task.TaskName;
                worksheet.Cell(task.Id, 4).Value = task.AssignedTo;
                worksheet.Cell(task.Id, 5).Value = task.StartDate;
                worksheet.Cell(task.Id, 6).Value = task.DaysRequired;
                worksheet.Cell(task.Id, 7).Value = task.EndDate;
                worksheet.Cell(task.Id, 8).Value = task.Progress;

                workbook.Save();
                Console.WriteLine($"Task with Id {task.Id} saved successfully.");
            }
        }

        public void DeleteTask(int id)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                worksheet.Row(id).Delete();
                workbook.Save();
            }
        }
    }
}
