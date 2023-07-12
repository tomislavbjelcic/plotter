using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //TestAsync();

        }

        static void TestAsync()
        {

            
            Console.WriteLine("Press c to cancel, q to quit.");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;
            Action ac = () =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (ct.IsCancellationRequested) return;
                    Thread.Sleep(100);
                    if ((i+1)%10 == 0)
                        Console.WriteLine($"Completed {(i+1)/10} seconds");
                }
            };
            Task t = Task.Run(ac);




            while (true)
            {
                var crk = Console.ReadKey(true);
                char pressed = crk.KeyChar;
                if (pressed == 'q') break;
                if (pressed == 'c')
                {
                    cts.Cancel();
                    t.Wait();
                    Console.WriteLine("Restarting task");
                    cts.Dispose();
                    cts = new CancellationTokenSource();
                    ct = cts.Token;

                    t = Task.Run(ac);
                }
            }

            Console.WriteLine("end");


        }
    }
}
