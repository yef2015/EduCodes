using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using NPOI.HSSF.Record.Chart;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.BLL
{
   public class ExportXls 
    {
        #region 导出


        HSSFWorkbook hssfworkbook;

        MemoryStream GetExcelStream()
        {
            //Write the stream data of workbook to the root directory
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            return file;
        }
        /// <summary>
        /// 生成数据
        /// </summary>
        /// <param name="exportDataTable"></param>
        void GenerateData(List<string> fieldsName, DataTable exportDs, string filename)
        {
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

            ICellStyle cellstyleHead = hssfworkbook.CreateCellStyle();
            cellstyleHead.VerticalAlignment = VerticalAlignment.Center;
            //表头样式
            cellstyleHead.BorderBottom = BorderStyle.Thin;
            cellstyleHead.BorderLeft = BorderStyle.Thin;
            cellstyleHead.BorderRight = BorderStyle.Thin;
            cellstyleHead.BorderTop = BorderStyle.Thin; 
            var font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "宋体";
            font.Boldweight = 700;
            cellstyleHead.SetFont(font);

            //表体样式
            ICellStyle cellstyleContent = hssfworkbook.CreateCellStyle();
            cellstyleContent.BorderBottom = BorderStyle.Thin;
            cellstyleContent.BorderLeft = BorderStyle.Thin;
            cellstyleContent.BorderRight = BorderStyle.Thin;
            cellstyleContent.BorderTop = BorderStyle.Thin;
            var font1 = hssfworkbook.CreateFont();
            font1.FontHeightInPoints = 11;
            font1.FontName = "宋体";
           font1.Boldweight = 10;
            cellstyleContent.SetFont(font1);


            IRow title = sheet1.CreateRow(0);
            for (int f = 0; f < fieldsName.Count; f++)
            {
                ICell cell = title.CreateCell(f);
                cell.SetCellValue(fieldsName[f]);
                
                cell.CellStyle = cellstyleHead;

            }
            int i = 1;
            if (exportDs != null && exportDs.Rows.Count > 0)
            {
                foreach (DataRow r in exportDs.Rows)
                {
                    IRow row = sheet1.CreateRow(i);
                    for (var j = 0; j < exportDs.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.CellStyle = cellstyleContent; 
                        sheet1.SetColumnWidth(j,5000); 
                        cell.SetCellValue(Convert.ToString(r[j])); 
                    }
                    i++;
                    #region 合并
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 0, 0);
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 1, 1);
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 2, 2);
                    #endregion
                }
            }
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            NPOI.SS.Util.CellRangeAddress cellRangeAddress = new NPOI.SS.Util.CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }

        void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            //////create a entry of DocumentSummaryInformation
            //DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            //dsi.Company = "NPOI Team";
            //hssfworkbook.DocumentSummaryInformation = dsi;

            //////create a entry of SummaryInformation
            //SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //si.Subject = "NPOI SDK Example";
            //hssfworkbook.SummaryInformation = si;
        }
        #endregion

        public void ExportToXls(System.Web.HttpResponse Response, List<string> fieldsName, DataTable exportDs, string filename)
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8)));
            Response.Clear();

            InitializeWorkbook();
            GenerateData(fieldsName, exportDs, filename);
            GetExcelStream().WriteTo(Response.OutputStream);
            Response.End();
        }




        public void ExportImg2Xls(System.Web.HttpResponse Response, byte[] imgBytes, string filename,int imgHeight,int imgWidth)
        {
            //byte[] bytes = System.IO.File.ReadAllBytes(@"E:\mineown\mine\TrainerEvaluate20140929\untitled.png");
            hssfworkbook=new HSSFWorkbook();
          //  File.WriteAllBytes(@"d:\11.png",imgBytes);
            int pictureIdx = hssfworkbook.AddPicture(imgBytes, PictureType.PNG);  
            //create sheet 
            var sheet = hssfworkbook.CreateSheet("课程满意度分布");  
            // Create the drawing patriarch.  This is the top level container for all shapes.  
            var patriarch = sheet.CreateDrawingPatriarch();  
            //add a picture 
            var anchor = new HSSFClientAnchor(0, 0,1023, 255, 0, 0,10, 18); 
            var pict = patriarch.CreatePicture(anchor, pictureIdx);
            pict.Resize(1);

            Response.Clear(); 
            Response.ContentType = "application/vnd.ms-excel"; 
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.xls",  HttpUtility.UrlPathEncode(filename)));
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            file.WriteTo(Response.OutputStream);
            Response.End();  
        }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="Response"></param>
       /// <param name="fieldsName"></param>
       /// <param name="exportDs"></param>
       /// <param name="filename"></param>
        public void ExportTotalReportToxls(System.Web.HttpResponse Response, List<string> fieldsName, DataTable exportDs, string filename)
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8)));
            Response.Clear();

            InitializeWorkbook();
            GenerateTotalReportData(fieldsName, exportDs, filename);
            GetExcelStream().WriteTo(Response.OutputStream);
            Response.End();

       }



        void GenerateTotalReportData(List<string> fieldsName, DataTable exportDs, string filename)
        {
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

            ICellStyle cellstyleHead = hssfworkbook.CreateCellStyle();
            cellstyleHead.VerticalAlignment = VerticalAlignment.Center;
            //表头样式
            cellstyleHead.BorderBottom = BorderStyle.Thin;
            cellstyleHead.BorderLeft = BorderStyle.Thin;
            cellstyleHead.BorderRight = BorderStyle.Thin;
            cellstyleHead.BorderTop = BorderStyle.Thin;
            cellstyleHead.WrapText = true;
            var font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "宋体";
            font.Boldweight = 700;
            cellstyleHead.SetFont(font);

            //表体样式
            ICellStyle cellstyleContent = hssfworkbook.CreateCellStyle();
            cellstyleContent.BorderBottom = BorderStyle.Thin;
            cellstyleContent.BorderLeft = BorderStyle.Thin;
            cellstyleContent.BorderRight = BorderStyle.Thin;
            cellstyleContent.BorderTop = BorderStyle.Thin; 
            var font1 = hssfworkbook.CreateFont();
            font1.FontHeightInPoints = 11;
            font1.FontName = "宋体";
            font1.Boldweight = 10;
            cellstyleContent.SetFont(font1);
            cellstyleContent.WrapText = true;
          
            IRow title = sheet1.CreateRow(0);
            title.Height = 600;  
            for (int f = 0; f < fieldsName.Count; f++)
            {
                ICell cell = title.CreateCell(f);
                sheet1.SetColumnWidth(f, 13 * 256);
                cell.SetCellValue(fieldsName[f]); 
                cell.CellStyle = cellstyleHead;
                
            }
            int i = 1;
            if (exportDs != null && exportDs.Rows.Count > 0)
            {
                foreach (DataRow r in exportDs.Rows)
                {
                    IRow row = sheet1.CreateRow(i);
                    for (var j = 0; j < exportDs.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.CellStyle = cellstyleContent;
                      //  sheet1.SetColumnWidth(j, 12 * 256);
                        if (exportDs.Columns[j].ColumnName == "TotalAvgScore")
                        { 
                            cell.SetCellValue(string.Format("{0:N2}", r[j]));
                        }
                        else if (exportDs.Columns[j].ColumnName == "TotalSatisfy" || exportDs.Columns[j].ColumnName == "TotalCourse"
                            || exportDs.Columns[j].ColumnName == "TotalTeacher" || exportDs.Columns[j].ColumnName == "TotalOrg")
                        {
                            var totalSatisfy = Convert.ToDouble(r[j]) >= 1.0
                             ? "100%"
                            : string.Format("{0:N2}%", Convert.ToDouble(r[j]) * 100);
                          
                            cell.SetCellValue( totalSatisfy);
                        } 
                        else
                        {
                            cell.SetCellValue(Convert.ToString(r[j]));
                        } 
                    }
                    i++;
                    #region 合并
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 0, 0);
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 1, 1);
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 2, 2);
                    #endregion
                }

                IRow row1 = sheet1.CreateRow(i);
                row1.Height = 600;
                for (var j = 0; j < exportDs.Columns.Count; j++)
                {
                    ICell cell1 = row1.CreateCell(j);
                    cell1.CellStyle = cellstyleContent;
                  //  sheet1.SetColumnWidth(0, 12 * 256);
                    if (j == 0)
                    {
                        cell1.SetCellValue("总平均分=各项得分总和/实评人数；满意度=（很满意+满意）/实评人数； 课程（讲师或者组织）的满意度=每项满意度相加/项数 "); 
                    }
                    else
                    {
                        cell1.SetCellValue("");
                    } 
                }  
                SetCellRangeAddress(sheet1, i, i, 0, 9);
            }
        }


        public void ExportCourseReportToxls(System.Web.HttpResponse Response, List<string> fieldsName, DataTable exportDs, string filename)
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8)));
            Response.Clear();

            InitializeWorkbook();
            GenerateCourseReportData(fieldsName, exportDs, filename);
            GetExcelStream().WriteTo(Response.OutputStream);
            Response.End();

        }



        void GenerateCourseReportData(List<string> fieldsName, DataTable exportDs, string filename)
        {
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

            ICellStyle cellstyleHead = hssfworkbook.CreateCellStyle();
            cellstyleHead.VerticalAlignment = VerticalAlignment.Center;
            //表头样式
            cellstyleHead.BorderBottom = BorderStyle.Thin;
            cellstyleHead.BorderLeft = BorderStyle.Thin;
            cellstyleHead.BorderRight = BorderStyle.Thin;
            cellstyleHead.BorderTop = BorderStyle.Thin;
            cellstyleHead.WrapText = true;
            cellstyleHead.Alignment= HorizontalAlignment.Center;
            var font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "宋体";
            font.Boldweight = 700;
            cellstyleHead.SetFont(font);

            //表体样式
            ICellStyle cellstyleContent = hssfworkbook.CreateCellStyle();
            cellstyleContent.BorderBottom = BorderStyle.Thin;
            cellstyleContent.BorderLeft = BorderStyle.Thin;
            cellstyleContent.BorderRight = BorderStyle.Thin;
            cellstyleContent.BorderTop = BorderStyle.Thin;
            var font1 = hssfworkbook.CreateFont();
            font1.FontHeightInPoints = 11;
            font1.FontName = "宋体";
            font1.Boldweight = 10;
            cellstyleContent.SetFont(font1);
            cellstyleContent.WrapText = true;

            IRow title = sheet1.CreateRow(0);
            title.Height = 600;
            for (int k = 0; k < fieldsName.Count; k++)
            {
                if (k == 0)
                {
                    ICell cell0 = title.CreateCell(k);
                    cell0.CellStyle = cellstyleHead;
                    cell0.SetCellValue("课程名称");
                }
                else
                {
                    ICell cell0 = title.CreateCell(k);  
                    cell0.CellStyle = cellstyleHead;
                    cell0.SetCellValue("课程内容各指标满意度");
                }
            }

            IRow title1 = sheet1.CreateRow(1);
            title1.Height = 600;

            for (int f = 0; f < fieldsName.Count; f++)
            {
             
                ICell cell = title1.CreateCell(f);
                sheet1.SetColumnWidth(f, 18 * 256);
                cell.SetCellValue(fieldsName[f]);
                cell.CellStyle = cellstyleHead;

            }
            SetCellRangeAddress(sheet1, 0, 0, 1, 5);
            SetCellRangeAddress(sheet1, 0, 1, 0, 0); 
            int i = 2;
            if (exportDs != null && exportDs.Rows.Count > 0)
            {
                foreach (DataRow r in exportDs.Rows)
                {
                    IRow row = sheet1.CreateRow(i);
                    for (var j = 0; j < exportDs.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.CellStyle = cellstyleContent;
                        //  sheet1.SetColumnWidth(j, 12 * 256);
                        if (exportDs.Columns[j].ColumnName == "CourseSubjectP" || exportDs.Columns[j].ColumnName == "CourseDevelopP"
                         || exportDs.Columns[j].ColumnName == "CourseKeyP" || exportDs.Columns[j].ColumnName == "CoursePracticalP" || exportDs.Columns[j].ColumnName == "CourseRichP")
                          {
                            var totalSatisfy = Convert.ToDouble(r[j]) >= 1.0
                             ? "100%"
                            : string.Format("{0:N2}%", Convert.ToDouble(r[j]) * 100);

                            cell.SetCellValue(totalSatisfy);
                        }
                        else
                        {
                            cell.SetCellValue(Convert.ToString(r[j]));
                        }
                    }
                    i++;
                    #region 合并
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 0, 0);
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 1, 1);
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 2, 2);
                    #endregion
                } 
            }
        }




        public void ExportTeacherReportToxls(System.Web.HttpResponse Response, List<string> fieldsName, DataTable exportDs, string filename)
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8)));
            Response.Clear();

            InitializeWorkbook();
            GenerateTeacherReportData(fieldsName, exportDs, filename);
            GetExcelStream().WriteTo(Response.OutputStream);
            Response.End();

        }



        void GenerateTeacherReportData(List<string> fieldsName, DataTable exportDs, string filename)
        {
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

            ICellStyle cellstyleHead = hssfworkbook.CreateCellStyle();
            cellstyleHead.VerticalAlignment = VerticalAlignment.Center;
            //表头样式
            cellstyleHead.BorderBottom = BorderStyle.Thin;
            cellstyleHead.BorderLeft = BorderStyle.Thin;
            cellstyleHead.BorderRight = BorderStyle.Thin;
            cellstyleHead.BorderTop = BorderStyle.Thin;
            cellstyleHead.WrapText = true;
            cellstyleHead.Alignment = HorizontalAlignment.Center;
            var font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "宋体";
            font.Boldweight = 700;
            cellstyleHead.SetFont(font);

            //表体样式
            ICellStyle cellstyleContent = hssfworkbook.CreateCellStyle();
            cellstyleContent.BorderBottom = BorderStyle.Thin;
            cellstyleContent.BorderLeft = BorderStyle.Thin;
            cellstyleContent.BorderRight = BorderStyle.Thin;
            cellstyleContent.BorderTop = BorderStyle.Thin;
            var font1 = hssfworkbook.CreateFont();
            font1.FontHeightInPoints = 11;
            font1.FontName = "宋体";
            font1.Boldweight = 10;
            cellstyleContent.SetFont(font1);
            cellstyleContent.WrapText = true;

            IRow title = sheet1.CreateRow(0);
            title.Height = 600;
            for (int k = 0; k < fieldsName.Count; k++)
            {
                if (k == 0)
                {
                    ICell cell0 = title.CreateCell(k);
                    cell0.CellStyle = cellstyleHead;
                    cell0.SetCellValue("课程名称");
                }
                else
                {
                    ICell cell0 = title.CreateCell(k);
                    cell0.CellStyle = cellstyleHead;
                    cell0.SetCellValue("培训讲师各指标满意度");
                }
            }

            IRow title1 = sheet1.CreateRow(1);
            title1.Height = 600;

            for (int f = 0; f < fieldsName.Count; f++)
            {

                ICell cell = title1.CreateCell(f);
                sheet1.SetColumnWidth(f, 18 * 256);
                cell.SetCellValue(fieldsName[f]);
                cell.CellStyle = cellstyleHead;

            }
            SetCellRangeAddress(sheet1, 0, 0, 1, 5);
            SetCellRangeAddress(sheet1, 0, 1, 0, 0);
            int i = 2;
            if (exportDs != null && exportDs.Rows.Count > 0)
            {
                foreach (DataRow r in exportDs.Rows)
                {
                    IRow row = sheet1.CreateRow(i);
                    for (var j = 0; j < exportDs.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.CellStyle = cellstyleContent;
                        //  sheet1.SetColumnWidth(j, 12 * 256); 
                        if (exportDs.Columns[j].ColumnName == "TeacherBearingP" || exportDs.Columns[j].ColumnName == "TeacherCommunicationP"
                            || exportDs.Columns[j].ColumnName == "TeacherLanguageP" || exportDs.Columns[j].ColumnName == "TeacherPrepareP" || exportDs.Columns[j].ColumnName == "TeacherStyleP")
                        {
                            var totalSatisfy = Convert.ToDouble(r[j]) >= 1.0
                             ? "100%"
                            : string.Format("{0:N2}%", Convert.ToDouble(r[j]) * 100);

                            cell.SetCellValue(totalSatisfy);
                        }
                        else
                        {
                            cell.SetCellValue(Convert.ToString(r[j]));
                        }
                    }
                    i++;
                    #region 合并
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 0, 0);
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 1, 1);
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 2, 2);
                    #endregion
                }
            }
        }



        public void ExportOrgReportToxls(System.Web.HttpResponse Response, List<string> fieldsName, DataTable exportDs, string filename)
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8)) );
            Response.Clear();

            InitializeWorkbook();
            GenerateOrgReportData(fieldsName, exportDs, filename);
            GetExcelStream().WriteTo(Response.OutputStream);
            Response.End();

        }



        void GenerateOrgReportData(List<string> fieldsName, DataTable exportDs, string filename)
        {
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

            ICellStyle cellstyleHead = hssfworkbook.CreateCellStyle();
            cellstyleHead.VerticalAlignment = VerticalAlignment.Center;
            //表头样式
            cellstyleHead.BorderBottom = BorderStyle.Thin;
            cellstyleHead.BorderLeft = BorderStyle.Thin;
            cellstyleHead.BorderRight = BorderStyle.Thin;
            cellstyleHead.BorderTop = BorderStyle.Thin;
            cellstyleHead.WrapText = true;
            cellstyleHead.Alignment = HorizontalAlignment.Center;
            var font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "宋体";
            font.Boldweight = 700;
            cellstyleHead.SetFont(font);

            //表体样式
            ICellStyle cellstyleContent = hssfworkbook.CreateCellStyle();
            cellstyleContent.BorderBottom = BorderStyle.Thin;
            cellstyleContent.BorderLeft = BorderStyle.Thin;
            cellstyleContent.BorderRight = BorderStyle.Thin;
            cellstyleContent.BorderTop = BorderStyle.Thin;
            var font1 = hssfworkbook.CreateFont();
            font1.FontHeightInPoints = 11;
            font1.FontName = "宋体";
            font1.Boldweight = 10;
            cellstyleContent.SetFont(font1);
            cellstyleContent.WrapText = true;

            IRow title = sheet1.CreateRow(0);
            title.Height = 600;
            for (int k = 0; k < fieldsName.Count; k++)
            {
                if (k == 0)
                {
                    ICell cell0 = title.CreateCell(k);
                    cell0.CellStyle = cellstyleHead;
                    cell0.SetCellValue("课程名称");
                }
                else
                {
                    ICell cell0 = title.CreateCell(k);
                    cell0.CellStyle = cellstyleHead;
                    cell0.SetCellValue("培训组织和管理各指标满意度");
                }
            }

            IRow title1 = sheet1.CreateRow(1);
            title1.Height = 600;

            for (int f = 0; f < fieldsName.Count; f++)
            {

                ICell cell = title1.CreateCell(f);
                sheet1.SetColumnWidth(f, 18 * 256);
                cell.SetCellValue(fieldsName[f]);
                cell.CellStyle = cellstyleHead;

            }
            SetCellRangeAddress(sheet1, 0, 0, 1, 3);
            SetCellRangeAddress(sheet1, 0, 1, 0, 0);
            int i = 2;
            if (exportDs != null && exportDs.Rows.Count > 0)
            {
                foreach (DataRow r in exportDs.Rows)
                {
                    IRow row = sheet1.CreateRow(i);
                    for (var j = 0; j < exportDs.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.CellStyle = cellstyleContent;
                        //  sheet1.SetColumnWidth(j, 12 * 256); 
                        if (exportDs.Columns[j].ColumnName == "OrgArrangeP" || exportDs.Columns[j].ColumnName == "OrgServiceP"
                            || exportDs.Columns[j].ColumnName == "OrgTimeP" )
                        {
                            var totalSatisfy = Convert.ToDouble(r[j]) >= 1.0
                             ? "100%"
                            : string.Format("{0:N2}%", Convert.ToDouble(r[j]) * 100);

                            cell.SetCellValue(totalSatisfy);
                        }
                        else
                        {
                            cell.SetCellValue(Convert.ToString(r[j]));
                        }
                    }
                    i++;
                    #region 合并
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 0, 0);
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 1, 1);
                    //SetCellRangeAddress(sheet1, rowStart, rowStart + 2, 2, 2);
                    #endregion
                }
            }
        }




        public     void ExportEvReportToxls(System.Web.HttpResponse Response, string filename, Guid courseId)
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8)));
            Response.Clear();

            InitializeWorkbook();
            GenerateEvReportData(courseId);
            GetExcelStream().WriteTo(Response.OutputStream);
            Response.End();

        }



        void GenerateEvReportData(Guid courseId)
        {
            ISheet sheet1 = hssfworkbook.CreateSheet("评估报告单");

            ICellStyle cellstyleHead = hssfworkbook.CreateCellStyle();
            cellstyleHead.VerticalAlignment = VerticalAlignment.Center;
            //表头样式
            cellstyleHead.BorderBottom = BorderStyle.Thin;
            cellstyleHead.BorderLeft = BorderStyle.Thin;
            cellstyleHead.BorderRight = BorderStyle.Thin;
            cellstyleHead.BorderTop = BorderStyle.Thin;
            cellstyleHead.WrapText = true;
            cellstyleHead.Alignment= HorizontalAlignment.Center;
            var font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 15;
            font.FontName = "宋体";
            font.Boldweight = 700;
            cellstyleHead.SetFont(font);

            //表体样式
            ICellStyle cellstyleContent = hssfworkbook.CreateCellStyle();
            cellstyleContent.BorderBottom = BorderStyle.Thin;
            cellstyleContent.BorderLeft = BorderStyle.Thin;
            cellstyleContent.BorderRight = BorderStyle.Thin;
            cellstyleContent.BorderTop = BorderStyle.Thin;
            cellstyleContent.Alignment = HorizontalAlignment.Center;
            var font1 = hssfworkbook.CreateFont();
            font1.FontHeightInPoints = 14;
            font1.FontName = "宋体";
            font1.Boldweight = 10;
            cellstyleContent.SetFont(font1);
            cellstyleContent.WrapText = true;

            ICellStyle cellstyleConTitle = hssfworkbook.CreateCellStyle();
            cellstyleConTitle.BorderBottom = BorderStyle.Thin;
            cellstyleConTitle.BorderLeft = BorderStyle.Thin;
            cellstyleConTitle.BorderRight = BorderStyle.Thin;
            cellstyleConTitle.BorderTop = BorderStyle.Thin;
            cellstyleConTitle.Alignment = HorizontalAlignment.Center;
            cellstyleConTitle.VerticalAlignment = VerticalAlignment.Center;  
            var font11 = hssfworkbook.CreateFont();
            font11.FontHeightInPoints = 14;
            font11.FontName = "宋体";
            font11.Boldweight = 600;
            cellstyleConTitle.SetFont(font11);
            cellstyleConTitle.WrapText = true;



            ICellStyle cellstyleConTitleLeft = hssfworkbook.CreateCellStyle();
            cellstyleConTitleLeft.BorderBottom = BorderStyle.Thin;
            cellstyleConTitleLeft.BorderLeft = BorderStyle.Thin;
            cellstyleConTitleLeft.BorderRight = BorderStyle.Thin;
            cellstyleConTitleLeft.BorderTop = BorderStyle.Thin;
            cellstyleConTitleLeft.Alignment = HorizontalAlignment.Left;
            cellstyleConTitleLeft.VerticalAlignment = VerticalAlignment.Center;
            var fontTitleLeft = hssfworkbook.CreateFont();
            fontTitleLeft.FontHeightInPoints = 14;
            fontTitleLeft.FontName = "宋体";
            fontTitleLeft.Boldweight = 10;
            cellstyleConTitleLeft.SetFont(fontTitleLeft);
            cellstyleConTitleLeft.WrapText = true;


            IRow title = sheet1.CreateRow(0);
            title.Height = 600; 

            for (int f = 0; f < 6; f++)
            {
                ICell cell = title.CreateCell(f);
                sheet1.SetColumnWidth(f, 13 * 256);
                cell.SetCellValue(DateTime.Now.Year+ "年中青年干部教育管理培训班课程评估表");
                cell.CellStyle = cellstyleHead; 
            }
            SetCellRangeAddress(sheet1, 0, 0, 0, 5);
            sheet1.SetColumnWidth(0, 15 * 256); 
            sheet1.SetColumnWidth(1, 25 * 256); 
            sheet1.SetColumnWidth(2, 15 * 256);
            sheet1.SetColumnWidth(3, 15 * 256);
            sheet1.SetColumnWidth(4, 15 * 256);
            sheet1.SetColumnWidth(5, 15 * 256); 
            
            var coubll= new BLL.Course();
            var couModel = coubll.GetModel(courseId);
            var question = new BLL.Questionnaire();
            var exportDs = question.GetReportTile(courseId);  
            if (exportDs != null && exportDs.Tables.Count > 0&&exportDs.Tables[0].Rows.Count>0)
            {
                var datarow = exportDs.Tables[0].Rows[0];
                IRow row1 = sheet1.CreateRow(1);
                ICell cell1 = row1.CreateCell(0);
                cell1.CellStyle = cellstyleConTitle;
                cell1.SetCellValue("课程名称"); 
                ICell cell2 = row1.CreateCell(1);
                cell2.CellStyle = cellstyleContent; 
                cell2.SetCellValue( couModel.CourseName); 
                ICell cell3 = row1.CreateCell(2);
                cell3.CellStyle = cellstyleConTitle;
                cell3.SetCellValue("培训地点"); 
                ICell cell4 = row1.CreateCell(3);
                cell4.CellStyle = cellstyleContent;
                cell4.SetCellValue(couModel.TeachPlace); 
                ICell cell5 = row1.CreateCell(4);
                cell5.CellStyle = cellstyleContent;
                cell5.SetCellValue(couModel.TeachPlace); 
                ICell cell6 = row1.CreateCell(5);
                cell6.CellStyle = cellstyleContent;
                cell6.SetCellValue(couModel.TeachPlace);
                SetCellRangeAddress(sheet1, 1, 1, 3, 5);

                 IRow row2 = sheet1.CreateRow(2);
                ICell cell21 = row2.CreateCell(0);
                cell21.CellStyle = cellstyleConTitle;
                cell21.SetCellValue("培训讲师"); 
                ICell cell22 = row2.CreateCell(1);
                cell22.CellStyle = cellstyleContent; 
                cell22.SetCellValue( couModel.TeacherName); 
                ICell cell23 = row2.CreateCell(2);
                cell23.CellStyle = cellstyleConTitle;
                cell23.SetCellValue("培训时间"); 
                ICell cell24 = row2.CreateCell(3);
                cell24.CellStyle = cellstyleContent;
                cell24.SetCellValue(couModel.TeachTime); 
                ICell cell25 = row2.CreateCell(4);
                cell25.CellStyle = cellstyleContent;
                cell25.SetCellValue(couModel.TeachTime); 
                ICell cell26 = row2.CreateCell(5);
                cell26.CellStyle = cellstyleContent;
                cell26.SetCellValue(couModel.TeachTime);
                SetCellRangeAddress(sheet1, 2, 2, 3, 5);


                 IRow row3 = sheet1.CreateRow(3);
                ICell cell31 = row3.CreateCell(0);
                cell31.CellStyle = cellstyleConTitle;
                cell31.SetCellValue("应评人数"); 
                ICell cell32 = row3.CreateCell(1);
                cell32.CellStyle = cellstyleContent;
                cell32.SetCellValue(datarow["totalNum"].ToString()+"人"); 
                ICell cell33 = row3.CreateCell(2);
                cell33.CellStyle = cellstyleConTitle;
                cell33.SetCellValue("实评人数"); 
                ICell cell34 = row3.CreateCell(3);
                cell34.CellStyle = cellstyleContent;
                cell34.SetCellValue(datarow["totalDone"].ToString() + "人"); 
                ICell cell35 = row3.CreateCell(4);
                cell35.CellStyle = cellstyleContent;
                cell35.SetCellValue(datarow["totalDone"].ToString() + "人"); 
                ICell cell36 = row3.CreateCell(5);
                cell36.CellStyle = cellstyleContent;
                cell36.SetCellValue(datarow["totalDone"].ToString() + "人"); 
                SetCellRangeAddress(sheet1, 3, 3, 3, 5);


                IRow row4 = sheet1.CreateRow(4);
                ICell cell41 = row4.CreateCell(0);
                cell41.CellStyle = cellstyleConTitle;
                cell41.SetCellValue("总体平均分");
                ICell cell42 = row4.CreateCell(1);
                cell42.CellStyle = cellstyleContent;
                cell42.SetCellValue(string.Format("{0:N2}", datarow["totalAvg"]) + " 分（满分52）");
                ICell cell43 = row4.CreateCell(2);
                cell43.CellStyle = cellstyleConTitle;
                cell43.SetCellValue("满意度");
                ICell cell44 = row4.CreateCell(3);
                cell44.CellStyle = cellstyleContent;
                cell44.SetCellValue(Convert.ToDouble(datarow["Satisfy"]) >= 1.0 ? "100%" : string.Format("{0:N2}" + "%", ((Convert.ToDecimal(datarow["Satisfy"])) * 100)));
                ICell cell45 = row4.CreateCell(4);
                cell45.CellStyle = cellstyleConTitle;
                cell45.SetCellValue("等级");
                ICell cell46 = row4.CreateCell(5);
                cell46.CellStyle = cellstyleContent;
                cell46.SetCellValue(question.GetLevel(Convert.ToDouble(datarow["Satisfy"])));
                SetCellRangeAddress(sheet1, 4, 4, 3, 5);

                var resultTotalReport = question.GetTotalReport();
                if (resultTotalReport != null && resultTotalReport.Rows.Count > 0)
                {
                    var cr = resultTotalReport.Select(string.Format(" CourseId='{0}'", courseId));
                    if (cr.Length > 0)
                    {
                        IRow row5 = sheet1.CreateRow(5);
                        ICell cell51 = row5.CreateCell(0);
                        cell51.CellStyle = cellstyleConTitle;
                        cell51.SetCellValue("课程内容");
                        ICell cell52 = row5.CreateCell(1);
                        cell52.CellStyle = cellstyleContent;
                      //  cell52.SetCellValue(cr[0]["TotalCourse"].ToString());
                        cell52.SetCellValue(Convert.ToDouble(cr[0]["TotalCourse"]) >= 1.0 ? "100%" : string.Format("{0:N2}" + "%", ((Convert.ToDecimal(cr[0]["TotalCourse"])) * 100)));
                        ICell cell53 = row5.CreateCell(2);
                        cell53.CellStyle = cellstyleConTitle;
                        cell53.SetCellValue("培训讲师");
                        ICell cell54 = row5.CreateCell(3);
                        cell54.CellStyle = cellstyleContent;
                      //  cell54.SetCellValue(cr[0]["TotalTeacher"].ToString());
                        cell54.SetCellValue(Convert.ToDouble(cr[0]["TotalTeacher"]) >= 1.0 ? "100%" : string.Format("{0:N2}" + "%", ((Convert.ToDecimal(cr[0]["TotalTeacher"])) * 100)));
                   
                        ICell cell55 = row5.CreateCell(4);
                        cell55.CellStyle = cellstyleConTitle;
                        cell55.SetCellValue("培组织管理");
                        ICell cell56 = row5.CreateCell(5);
                        cell56.CellStyle = cellstyleContent;
                       // cell56.SetCellValue(cr[0]["TotalOrg"].ToString()); 
                        cell56.SetCellValue(Convert.ToDouble(cr[0]["TotalOrg"]) >= 1.0 ? "100%" : string.Format("{0:N2}" + "%", ((Convert.ToDecimal(cr[0]["TotalOrg"])) * 100)));
                   
                    } 
                }

                var reportBody = question.GetReport(courseId);
                if (reportBody != null && reportBody.Tables.Count > 0)
                {
                    var result = new Dictionary<int, double[]>();
                    if (reportBody != null && reportBody.Tables.Count > 0)
                    {
                        foreach (DataRow row in reportBody.Tables[0].Rows)
                        {
                            result.Add((int)row["num"], new[] { Convert.ToDouble(row["top1"]), Convert.ToDouble(row["top2"]), Convert.ToDouble(row["top3"]), Convert.ToDouble(row["top4"]), Convert.ToDouble(row["top5"]) });
                        }
                    }

                    var allTop1 = string.Format("{0:N2}" + "%", result[1][0] * 100);
                    var allTop2 = string.Format("{0:N2}" + "%", result[1][1] * 100);
                    var allTop3 = string.Format("{0:N2}" + "%", result[1][2] * 100);
                    var allTop4 = string.Format("{0:N2}" + "%", result[1][3] * 100);
                    var allTop5 = string.Format("{0:N2}" + "%", result[1][4] * 100);   
                     
                   
                    IRow row6 = sheet1.CreateRow(6);
                    ICell cell61 = row6.CreateCell(0);
                    cell61.CellStyle = cellstyleConTitle;
                    cell61.SetCellValue("培训满意度评价项目");
                    ICell cell62 = row6.CreateCell(1);
                    cell62.CellStyle = cellstyleConTitle;
                    cell62.SetCellValue("培训满意度评价项目");
                    ICell cell63 = row6.CreateCell(2);
                    cell63.CellStyle = cellstyleContent;
                    cell63.SetCellValue("很满意");
                    ICell cell64 = row6.CreateCell(3);
                    cell64.CellStyle = cellstyleContent;
                    cell64.SetCellValue("满意");
                    ICell cell65 = row6.CreateCell(4);
                    cell65.CellStyle = cellstyleContent;
                    cell65.SetCellValue("一般");
                    ICell cell66 = row6.CreateCell(5);
                    cell66.CellStyle = cellstyleContent;
                    cell66.SetCellValue("不满意");
                    SetCellRangeAddress(sheet1, 6, 6, 0, 1);

                   
                    IRow row7 = sheet1.CreateRow(7);
                    ICell cell71 = row7.CreateCell(0);
                    cell71.CellStyle = cellstyleConTitle;
                    cell71.SetCellValue("本次课程总体满意度");
                    ICell cell72 = row7.CreateCell(1);
                    cell72.CellStyle = cellstyleConTitle;
                    cell72.SetCellValue("本次课程总体满意度");
                    ICell cell73 = row7.CreateCell(2);
                    cell73.CellStyle = cellstyleContent;
                    cell73.SetCellValue(allTop2);
                    ICell cell74 = row7.CreateCell(3);
                    cell74.CellStyle = cellstyleContent;
                    cell74.SetCellValue(allTop3);
                    ICell cell75 = row7.CreateCell(4);
                    cell75.CellStyle = cellstyleContent;
                    cell75.SetCellValue(allTop4);
                    ICell cell76 = row7.CreateCell(5);
                    cell76.CellStyle = cellstyleContent;
                    cell76.SetCellValue(allTop5);
                    SetCellRangeAddress(sheet1, 7, 7, 0, 1);


                    var startRowNum = 8;
                    var startKey = 2; 
                    SetDetailValue("课程内容",
                        new []{"课程主题清晰明确","课程内容丰富、能吸引人","课程内容切合实际，能指导实践","课程内容重点突出，易于理解","课程内容有助于个人发展"},
                        result, ref startRowNum, sheet1, cellstyleContent, cellstyleConTitle, ref startKey, cellstyleConTitleLeft);

                   
                    SetDetailValue("培训讲师",
                        new[] { "讲师准备比较充分", "语言表达清晰，态度端正", "仪表仪容端庄大方，有亲和力", "培训方式多样，生动有趣", "与学员沟通和互动有效" },
                        result, ref startRowNum, sheet1, cellstyleContent, cellstyleConTitle, ref  startKey, cellstyleConTitleLeft);
                     
                    SetDetailValue("培训组织和管理",
                        new[] { "培训服务周到细致", "培训时间安排和控制合理", "培训场所、设备安排到位" },
                        result, ref startRowNum, sheet1, cellstyleContent, cellstyleConTitle, ref  startKey, cellstyleConTitleLeft);


                    IRow endrow = sheet1.CreateRow(startRowNum);
                    ICell endcell1 = endrow.CreateCell(0);
                    endcell1.CellStyle = cellstyleConTitle;
                    endcell1.SetCellValue("测评单位");
                    ICell endcell12 = endrow.CreateCell(1);
                    endcell12.CellStyle = cellstyleContent;
                    endcell12.SetCellValue("海淀区教育党校");
                    ICell endcell13 = endrow.CreateCell(2);
                    endcell13.CellStyle = cellstyleConTitle;
                    endcell13.SetCellValue("测评时间");
                    ICell endcell14 = endrow.CreateCell(3);
                    endcell14.CellStyle = cellstyleContent;
                    endcell14.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
                    ICell endcell15 = endrow.CreateCell(4);
                    endcell15.CellStyle = cellstyleContent;
                    endcell15.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
                    ICell endcell16 = endrow.CreateCell(5);
                    endcell16.CellStyle = cellstyleContent;
                    endcell16.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
                    SetCellRangeAddress(sheet1, startRowNum, startRowNum, 3, 5);
                }   
               
            }

            GetSuggestion(courseId.ToString());
        }


       private void GetSuggestion(string coid)
       { 
           ISheet sheet2 = hssfworkbook.CreateSheet("学员建议"); 
           ICellStyle cellstyleHead = hssfworkbook.CreateCellStyle();
           cellstyleHead.VerticalAlignment = VerticalAlignment.Center;
           //表头样式
           cellstyleHead.BorderBottom = BorderStyle.Thin;
           cellstyleHead.BorderLeft = BorderStyle.Thin;
           cellstyleHead.BorderRight = BorderStyle.Thin;
           cellstyleHead.BorderTop = BorderStyle.Thin;
           cellstyleHead.Alignment= HorizontalAlignment.Center; 
           cellstyleHead.WrapText = true;
           var font = hssfworkbook.CreateFont();
           font.FontHeightInPoints = 15;
           font.FontName = "宋体";
           font.Boldweight = 700;
           cellstyleHead.SetFont(font);

           //表体样式
           ICellStyle cellstyleContent = hssfworkbook.CreateCellStyle();
           cellstyleContent.BorderBottom = BorderStyle.Thin;
           cellstyleContent.BorderLeft = BorderStyle.Thin;
           cellstyleContent.BorderRight = BorderStyle.Thin;
           cellstyleContent.BorderTop = BorderStyle.Thin;
           var font1 = hssfworkbook.CreateFont();
           font1.FontHeightInPoints = 14;
           font1.FontName = "宋体";
           font1.Boldweight = 10;
           cellstyleContent.SetFont(font1);
           cellstyleContent.WrapText = true;
           sheet2.SetColumnWidth(0, 13 * 256);
           sheet2.SetColumnWidth(1, 78 * 256);

           IRow title0 = sheet2.CreateRow(0);
           title0.Height = 600; 
           for (int f = 0; f < 2; f++)
           {
               ICell cell = title0.CreateCell(f);

               cell.SetCellValue("学员建议");
               cell.CellStyle = cellstyleHead;
           }
           SetCellRangeAddress(sheet2, 0, 0, 0, 1);


           var fieldsName = new List<string>() { "学员姓名", "建议" }; 
           IRow title = sheet2.CreateRow(1);
           for (int f = 0; f < fieldsName.Count; f++)
           {
               ICell cell = title.CreateCell(f);
               cell.SetCellValue(fieldsName[f]);

               cell.CellStyle = cellstyleHead;

           }

           var quesBll = new BLL.Questionnaire();
           var ds = quesBll.GetSuggestion(coid);
           var exportDs = ds.Tables[0];
           int i = 2;
           if (exportDs != null && exportDs.Rows.Count > 0)
           {
               foreach (DataRow r in exportDs.Rows)
               {
                   IRow row = sheet2.CreateRow(i);
                   for (var j = 0; j < exportDs.Columns.Count; j++)
                   {
                       ICell cell = row.CreateCell(j);
                       cell.CellStyle = cellstyleContent; 
                       cell.SetCellValue(Convert.ToString(r[j]));

                   }
                   i++; 
               }
           } 
       }




       private void SetDetailValue(string leftTitle, string[] leftContent, Dictionary<int, double[]> values, ref int startRowNum, ISheet sheet1, ICellStyle cellstyleContent, ICellStyle cellstyleTitle, ref int startKey, ICellStyle cellstyleLeft)
        {
            var oriStartRowNum = startRowNum;
           for (int k = 0;k<leftContent.Length;k++)
           {
               IRow row = sheet1.CreateRow(startRowNum);
               for (int i = 0; i < 6; i++)
               {
                   if (i == 0)
                   {
                       ICell cell = row.CreateCell(i);
                       cell.CellStyle = cellstyleTitle; 
                       cell.SetCellValue(leftTitle); 
                   }
                   else if (i == 1)
                   {
                       ICell cell = row.CreateCell(i);
                       cell.CellStyle = cellstyleLeft; 
                       cell.SetCellValue(leftContent[k]); 
                   }
                   else
                   {
                       ICell cell = row.CreateCell(i);
                       cell.CellStyle = cellstyleContent;
                       sheet1.SetColumnWidth(i, 15 * 256);
                       cell.SetCellValue(values[startKey][i - 1]+"人");
                   }   
               }
               startKey++;
               startRowNum++;
           }
           SetCellRangeAddress(sheet1, oriStartRowNum, startRowNum-1, 0, 0);
       }





       /// <summary>
       /// 将excel中的数据导入到DataTable中
       /// </summary>
       /// <param name="sheetName">excel工作薄sheet的名称</param>
       /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
       /// <returns>返回的DataTable</returns>
       public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn, string fileName)
       {
           ISheet sheet = null;
           DataTable data = new DataTable();
           int startRow = 0; 
           FileStream fs = null;
              IWorkbook workbook = null;
           try
           { 
               fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
               if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                   workbook = new HSSFWorkbook(fs);
                  // workbook = new XSSFWorkbook(fs);
               else if (fileName.IndexOf(".xls") > 0) // 2003版本
                   workbook = new HSSFWorkbook(fs);

               if (sheetName != null)
               {
                   sheet = workbook.GetSheet(sheetName);
                   if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                   {
                       sheet = workbook.GetSheetAt(0);
                   }
               }
               else
               {
                   sheet = workbook.GetSheetAt(0);
               }
               if (sheet != null)
               {
                   IRow firstRow = sheet.GetRow(0);
                   int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                   if (isFirstRowColumn)
                   {
                       for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                       {
                           ICell cell = firstRow.GetCell(i);
                           if (cell != null)
                           {
                               string cellValue = cell.StringCellValue;
                               if (cellValue != null)
                               {
                                   DataColumn column = new DataColumn(cellValue);
                                   data.Columns.Add(column);
                               }
                           }
                       }
                       startRow = sheet.FirstRowNum + 1;
                   }
                   else
                   {
                       startRow = sheet.FirstRowNum;
                   }

                   //最后一列的标号
                   int rowCount = sheet.LastRowNum;
                   for (int i = startRow; i <= rowCount; ++i)
                   {
                       IRow row = sheet.GetRow(i);
                       if (row == null) continue; //没有数据的行默认是null　　　　　　　

                       DataRow dataRow = data.NewRow();
                       for (int j = row.FirstCellNum; j < cellCount; ++j)
                       {
                           //if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                           //    dataRow[j] = row.GetCell(j).ToString();

                           if (row.GetCell(j) != null)
                           {
                               dataRow[j] = row.GetCell(j).ToString();
                               if (row.GetCell(j).CellType == CellType.Numeric)
                               {
                                   short format = row.GetCell(j).CellStyle.DataFormat;
                                   if (format == 14 || format == 31 || format == 57 || format == 58)
                                   {
                                       DateTime date = row.GetCell(j).DateCellValue;
                                       string re = date.ToString("yyyy-MM-dd");
                                       dataRow[j] = re;
                                   }
                               } 
                           }
                           

                       }
                       data.Rows.Add(dataRow);
                   }
               }

               return data;
           }
           catch (Exception ex)
           {
               LogHelper.WriteLogofExceptioin(ex);
               throw ex;
           }
       } 

    }
}
