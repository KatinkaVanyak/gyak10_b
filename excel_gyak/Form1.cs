using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace excel_gyak
{
    public partial class Form1 : Form
    {
        Excel.Application xlApp;    // maga az excel alkalmaz�s p�ld�nyos�t�sa
        Excel.Workbook xlWB;        // az excelen bel�l egy munkaf�zet p�ld�nyos�t�sa
        Excel.Worksheet xlSheet;    // A munkaf�zeten bel�l egy munkalap p�ld�nyos�t�sa


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                xlApp = new Excel.Application();

                xlWB = xlApp.Workbooks.Add(Missing.Value);

                xlSheet = xlWB.ActiveSheet;

                CreateTable();

                // Control �tad�sa a felhaszn�l�nak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) 
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba eset�n az Excel applik�ci� bez�r�sa automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }


        }
        
        void CreateTable()
        {
            string[] excel_fejlec = new string[]
            {
                "K�rd�s", "1. v�lasz", "2. v�lasz", "3. v�lasz", "Helyes v�lasz", "K�p"
            };

            for (int i = 0; i < excel_fejlec.Length; i++)
            {
                xlSheet.Cells[1, 1] = excel_fejlec[0];
                xlSheet.Cells[1, 2] = excel_fejlec[1];
                xlSheet.Cells[1, 3] = excel_fejlec[2];
                xlSheet.Cells[1, 4] = excel_fejlec[3];
                xlSheet.Cells[1, 5] = excel_fejlec[4];
                xlSheet.Cells[1, 6] = excel_fejlec[5];
            }

           


            Models.HajosContext context_hajos = new Models.HajosContext();
            var kerdes_ossz = context_hajos.Questions.ToList();

            object[,] adatok = new object[kerdes_ossz.Count(), excel_fejlec.Count()];
            // new object[1,2] -->  mennyi sor, mennyi oszlop legyen benne


            for (int i = 0; i < kerdes_ossz.Count(); i++)
            {
                adatok[i,0] = kerdes_ossz[i].Question1;
                adatok[i, 1] = kerdes_ossz[i].Answer1;
                adatok[i, 2] = kerdes_ossz[i].Answer2;
                adatok[i, 3] = kerdes_ossz[i].Answer3;
                adatok[i, 4] = kerdes_ossz[i].CorrectAnswer;
                adatok[i, 5] = kerdes_ossz[i].Image;

            }


            int sorokSz�ma = adatok.GetLength(0);   // az object adatok 2 dimenzi�s. az 1. dimenzi� a sorok, a 2. az oszlopok?
            int oszlopokSz�ma = adatok.GetLength(1);

            Excel.Range adatRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSz�ma, oszlopokSz�ma);
            adatRange.Value2 = adatok;

            //adatRange.Columns.AutoFit();

            Excel.Range fejll�cRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, 6);
            fejll�cRange.Font.Bold = true;
            fejll�cRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejll�cRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //fejll�cRange.EntireColumn.AutoFit();
            fejll�cRange.RowHeight = 20;
            fejll�cRange.Interior.Color = Color.Lavender;
            fejll�cRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range bodyRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(sorokSz�ma, 6);
            bodyRange.Font.Bold = true;
            bodyRange.Interior.Color = Color.LightYellow;
            bodyRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range els�Range = xlSheet.get_Range("A1", Type.Missing).get_Resize(sorokSz�ma, 1);
            els�Range.Interior.Color = Color.LightGray;
            
        }

    }
}