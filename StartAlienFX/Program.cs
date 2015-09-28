using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace StartAlienFX
{

    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            var enableGPU = false;
            var enableBattery = false;
            if (args.Length > 0)
            {
                if (args.Length > 0 && args[0].ToString().ToUpper() == "enablegpu".ToUpper()) enableGPU = true;
                if (args.Length > 0 && args[0].ToString().ToUpper() == "enablebattery".ToUpper()) enableBattery = true;

                if (args.Length > 1 && args[1].ToString().ToUpper() == "enablegpu".ToUpper()) enableGPU = true;
                if (args.Length > 1 && args[1].ToString().ToUpper() == "enablebattery".ToUpper()) enableBattery = true;
                Process.Start(new ProcessStartInfo("sc", string.Format(" start Service1{0}{1}",(enableBattery) ? " enablebattery ": string.Empty,(enableGPU) ? " enablegpu " : string.Empty)) { CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden }).WaitForExit();
            }
            
        }
    }
}
