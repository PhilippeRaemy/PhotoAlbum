namespace DialogConsoleStarter
{
    using System;
    using System.Windows.Forms;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var form=new AlbumWordAddin.FormImportPictures();
            Application.Run(form);
        }
    }
}
