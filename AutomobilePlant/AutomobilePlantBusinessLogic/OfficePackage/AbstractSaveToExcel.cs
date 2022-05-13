using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantBusinessLogic.OfficePackage.HelperEnums;
using AutomobilePlantBusinessLogic.OfficePackage.HelperModels;


namespace AutomobilePlantBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        /// <summary>
        /// Создание отчета
        /// </summary>
        /// <param name="info"></param>
        public void CreateReport(ExcelInfo info)
        {
            CreateExcel(info.FileName);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 2;
            foreach (var cd in info.CarDetails)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = cd.CarName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
                foreach (var detail in cd.Details)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = detail.Item1,
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = detail.Item2.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    rowIndex++;
                }
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "C",
                    RowIndex = rowIndex,
                    Text = cd.TotalCount.ToString(),
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
            }
            SaveExcel(info.FileName);
        }
        public void CreateReportWarehouse(ExcelInfoWarehouse info)
        {
            CreateExcel(info.FileName);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 2;
            foreach (var wd in info.WarehouseDetails)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = wd.WarehouseName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
                foreach (var detail in wd.Details)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = detail.Item1,
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = detail.Item2.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    rowIndex++;
                }
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "C",
                    RowIndex = rowIndex,
                    Text = wd.TotalCount.ToString(),
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
            }
            SaveExcel(info.FileName);
        }
        /// <summary>
        /// Создание excel-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateExcel(string info);
        /// <summary>
        /// Добавляем новую ячейку в лист
        /// </summary>
        /// <param name="cellParameters"></param>
        protected abstract void InsertCellInWorksheet(ExcelCellParameters
        excelParams);
        /// <summary>
        /// Объединение ячеек
        /// </summary>
        /// <param name="mergeParameters"></param>
        protected abstract void MergeCells(ExcelMergeParameters excelParams);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveExcel(string info);
    }
}
