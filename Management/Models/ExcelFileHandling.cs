using ClosedXML.Excel;
namespace Management.Models
{
    public class ExcelFileHandling
    {
      //This Method will Create an Excel Sheet and Store it in the Memory Stream Object
            //And return thar Memory Stream Object
            public MemoryStream CreateExcelFile(List<Activity> activity)
            {
                //Create an Instance of Workbook, i.e., Creates a new Excel workbook
                var workbook = new XLWorkbook();

                //Add a Worksheets with the workbook
                //Worksheets name is Employees
                IXLWorksheet worksheet = workbook.Worksheets.Add("activity");

                //Create the Cell
                //First Row is going to be Header Row
                worksheet.Cell(1, 1).Value = "TaskId"; //First Row and First Column
                worksheet.Cell(1, 2).Value = "TaskName"; //First Row and Second Column
                worksheet.Cell(1, 3).Value = "Description"; //First Row and Third Column
                worksheet.Cell(1, 4).Value = "TaskDueDate"; //First Row and Fourth Column
                worksheet.Cell(1, 5).Value = "Created_By"; //First Row and Fifth Column
               

                //Data is going to stored from Row 2
                int row = 2;

                //Loop Through Each Employees and Populate the worksheet
                //For Each Employee increase row by 1
                foreach (var emp in activity)
                {
                    worksheet.Cell(row, 1).Value = emp.TaskId;
                    worksheet.Cell(row, 2).Value = emp.TaskName;
                    worksheet.Cell(row, 3).Value = emp.Description;
                    worksheet.Cell(row, 4).Value = emp.TaskDueDate;
                    worksheet.Cell(row, 5).Value = emp.Created_By;
                    
                    row++; //Increasing the Data Row by 1
                }

                //Create an Memory Stream Object
                var stream = new MemoryStream();

                //Saves the current workbook to the Memory Stream Object.
                workbook.SaveAs(stream);

                //The Position property gets or sets the current position within the stream.
                //This is the next position a read, write, or seek operation will occur from.
                stream.Position = 0;

                return stream;
            }
        }
    }
