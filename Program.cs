using ImGuiNET;
using ClickableTransparentOverlay;
using System.Numerics;
using SixLabors.ImageSharp.ColorSpaces;
using System.Runtime.InteropServices;
using ClickableTransparentOverlay.Win32;
using SharpDX.DXGI;
using Swed32;

namespace mw2mm
{
    public class Program : Overlay
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int hideConsole = 0;
        const int showConsole = 1;

        bool isAmmoInfinite = false;

        protected override void Render()
        {
            ImGui.Begin("Mod Menu");
            ImGui.Checkbox("Infinite Ammo", ref isAmmoInfinite);
            if (isAmmoInfinite == true) 
            {
                infiniteAmmo();
            }
            ImGui.End();
        }

        public void infiniteAmmo()
        {
            Swed swed = new Swed("iw4mp");
            IntPtr moduleBase = swed.GetModuleBase("iw4mp.exe");
            //IntPtr ammoAddress = swed.ReadPointer(moduleBase, 0x0154BF90) + 0x36C;
            IntPtr ammoAddress1 = swed.ReadPointer(moduleBase, 0x00138928) + 0xD9C;
            IntPtr ammoAddress2 = swed.ReadPointer(moduleBase, 0x00138928) + 0xD84;

            //int ammo = swed.ReadInt(ammoAddress);
            //Console.WriteLine(ammo);
            swed.WriteInt(ammoAddress1, 5);
            swed.WriteInt(ammoAddress2, 3);
        }

        public static void Main(string[] args)
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, hideConsole);
            Console.WriteLine("Starting GUI...");
            Program program = new Program();
            program.Start().Wait();
        }
    }
}