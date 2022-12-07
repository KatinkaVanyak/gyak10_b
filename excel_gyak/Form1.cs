using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace excel_gyak
{
    public partial class Form1 : Form
    {
        Excel.Application xlApp;    // maga az excel alkalmazás példányosítása
        Excel.Workbook xlWB;        // az excelen belül egy munkafüzet példányosítása
        Excel.Worksheet xlSheet;    // A munkafüzeten belül egy munkalap példányosítása


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

                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) 
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
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
                "Kérdés", "1. válasz", "2. válasz", "3. válasz", "Helyes válasz", "Kép"
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


            int sorokSzáma = adatok.GetLength(0);   // az object adatok 2 dimenziós. az 1. dimenzió a sorok, a 2. az oszlopok?
            int oszlopokSzáma = adatok.GetLength(1);

            Excel.Range adatRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSzáma, oszlopokSzáma);
            adatRange.Value2 = adatok;

            //adatRange.Columns.AutoFit();

            Excel.Range fejllécRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, 6);
            fejllécRange.Font.Bold = true;
            fejllécRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejllécRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //fejllécRange.EntireColumn.AutoFit();
            fejllécRange.RowHeight = 20;
            fejllécRange.Interior.Color = Color.Lavender;
            fejllécRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range bodyRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(sorokSzáma, 6);
            bodyRange.Font.Bold = true;
            bodyRange.Interior.Color = Color.LightYellow;
            bodyRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range elsõRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(sorokSzáma, 1);
            elsõRange.Interior.Color = Color.LightGray;
            
        }

    }
}