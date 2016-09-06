using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD1
{
    static class dateStart
    {
        //add personal add date
        public static DateTime Value { get; set; }
    }
    static class changeId
    {//change position
        public static int Value{ get; set; }
    }
    static class delId
    {//to fire person
        public static int Value { get; set; }
    }
    static class salaryVal
    {
        public static int Value { get; set; }
    }
    static class conn_string
    {
        public static string Value { get; set; }
    }
    static class idPo
    {
        public static int Value { get; set; }
    }
    static class indexPosition
    {
        public static int Value { get; set; }
    }
    static class Program1
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FIRE());
         //   Application.Run(new dovid());
           // Application.Run(new ADD_WORKER());
          // Application.Run(new Reporte());
            Application.Run(new USER());
          //  Application.Run(new changep());
        }
        
    }
}
