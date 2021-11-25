using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image2maze
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var openDlg = new OpenFileDialog()) 
               using(var saveDlg = new SaveFileDialog())
            {
                openDlg.Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;|BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png";
                openDlg.Title = "Select Source Image File to Load";

                openDlg.RestoreDirectory = true;
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    var mz = new Maze();
                    mz.Generate(openDlg.FileName);

                    saveDlg.Filter = "Jpeg (*.jpg)|*.jpg";
                    saveDlg.Title = "Select File to Save";
                    saveDlg.FilterIndex = 0;
                    saveDlg.RestoreDirectory = true;

                    if (saveDlg.ShowDialog() == DialogResult.OK)
                    {
                        var mr = new MazeRenderer();
                        mr.SaveToJpeg(mz.Grid, saveDlg.FileName, new Models.RenderSettings() { ColourMap = mz.ColourMap, FillBackGround = true });
                    }
                       
                }
            }          

        }
    }
}
