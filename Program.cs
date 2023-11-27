using ImGuiNET;
using ClickableTransparentOverlay;
using System.Numerics;
using SixLabors.ImageSharp.ColorSpaces;
using System.Runtime.InteropServices;
using ClickableTransparentOverlay.Win32;
using SharpDX.DXGI;
using Swed32;
using System.ComponentModel;

namespace mw2mm
{
    public class Program : Overlay
    {

        int ammo1;
        int ammo2;
        float height = 10;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int hideConsole = 0;
        const int showConsole = 1;

        int windowxsize = 429;
        int windowysize = 250;

        bool isAmmoInfinite = false;
        bool isHealthInfinite = false;
        bool isFlyingHacked = false;

        bool about = true;
        bool settings = false;
        bool cheats = false;
        bool cheatSettings = false;

        bool HCM = false;
        bool NA = false;

        Vector4 col = new Vector4(1f, 1f, 1f, 1f);
        Vector4 hcm = new Vector4(1f, 0f, 0f, 1f);
        Vector4 regButton = new Vector4(1f, 1f, 1f, 0.5f);
        Vector4 hcmButton = new Vector4(1f, 1f, 0f, 1f);

        protected override void Render()
        {

            ImGuiStylePtr style = ImGui.GetStyle();
            style.WindowBorderSize = 2.0f;
            style.Colors[(int)ImGuiCol.Border] = col;
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(1.0f, 1.0f, 1.0f, 0.45f);
            style.Colors[(int)ImGuiCol.Button] = regButton;

            // style selection 
            if (HCM == true)
            {
                style.Colors[(int)ImGuiCol.Text] = hcm;
                style.Colors[(int)ImGuiCol.Button] = hcmButton;
            }
            else if (HCM == false)
            {
                style.Colors[(int)ImGuiCol.Text] = col;
                style.Colors[(int)ImGuiCol.Button] = regButton;
            }

            ImGui.Begin("DucksterBoo's Universal Mod Menu", ImGuiWindowFlags.MenuBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoResize);
            ImGui.SetWindowSize(new Vector2(windowxsize, windowysize));

            if (ImGui.BeginMenuBar())
            {
                if (ImGui.BeginMenu("DBUMM"))
                {
                    if (ImGui.MenuItem("About"))
                    {
                        about = true;
                        settings = false;
                        cheats = false;
                        cheatSettings = false;
                    }
                    if (ImGui.MenuItem("Setings"))
                    {
                        about = false;
                        settings = true;
                        cheats = false;
                        cheatSettings = false;
                    }
                    if (ImGui.MenuItem("Exit"))
                    {
                        Environment.Exit(0);
                    }
                    ImGui.EndMenu();
                }
                if (ImGui.BeginMenu("Cheats"))
                {
                    if (ImGui.MenuItem("MW2 (2009)"))
                    {
                        about = false; ;
                        settings = false;
                        cheats = true;
                        cheatSettings = false;
                    }
                    if (ImGui.MenuItem("Cheat Setings"))
                    {
                        about = false;
                        settings = false;
                        cheats = false;
                        cheatSettings = true;
                    }
                    ImGui.EndMenu();
                }
                ImGui.EndMenuBar();
            }

            if (cheats == true)
            {
                ImGui.Text("Modern Warfare 2 (2009)");
                ImGui.NewLine();
                ImGui.Checkbox("Infinite Ammo", ref isAmmoInfinite);
                if (isAmmoInfinite == true)
                {
                    infiniteAmmo();
                }

                ImGui.Checkbox("Infinite Health", ref isHealthInfinite);
                if (isHealthInfinite == true)
                {
                    infiniteHealth();
                }
                ImGui.Checkbox("Fly Hacks", ref isFlyingHacked);
                if (isFlyingHacked == true)
                {
                    ImGui.Text("M - Fly Down");
                    if (ImGui.IsKeyDown(ImGuiKey.M))
                    {
                        height = height - 600;

                        flyHack();
                    }

                    ImGui.Text("N - Fly Up");
                    if (ImGui.IsKeyDown(ImGuiKey.N))
                    {
                        height = height + 1000;

                        flyHack();
                    }
                }
            }

            if (about == true)
            {
                ImGui.SeparatorText("About");
                ImGui.Text("Developer: DucksterBoo123");
                ImGui.Text("Special Thanks To: Fahoom");
                ImGui.Text("Why: idk he was just there init");
                ImGui.Text("My Github: https://github.com/DucksterBoo123");
                ImGui.Text("Fahim's Github: https://github.com/Fahoom");
                ImGui.Text("Dev Start: 26/11/23");
                ImGui.Text("Dev End: ");
                ImGui.Text("License: MIT");
                ImGui.SeparatorText("Current Features");
                ImGui.Text("Game: Modern Warfare 2 (2009)");
                ImGui.Text("Cheats: Infinite Ammo");
                ImGui.Text("Cheats: Infinite Health");
                ImGui.Text("Cheats: Fly Hacks (Limited)");
                ImGui.Text("To-Do: No Clip");
                ImGui.Text("To-Do: Unlock All");
                ImGui.Text("To-Do: FOV Slider");
            }

            if (settings == true)
            {
                Vector2 windowSize = ImGui.GetWindowSize();

                ImGui.BeginChild("1", new System.Numerics.Vector2(windowSize.X - 250, 0));
                // options
                ImGui.SeparatorText("Options");
                ImGui.Checkbox("High Contrast Mode", ref HCM);
                ImGui.Checkbox("Coming Soon...", ref NA);
                ImGui.SeparatorText("Sizing");
                ImGui.PushItemWidth(175);
                ImGui.SliderInt("WindowX", ref windowxsize, 429, 1000);
                ImGui.SliderInt("WindowY", ref windowysize, 250, 1000);
                // window sizing
                if (ImGui.Button("Reset Window Size"))
                {
                    windowxsize = 429;
                    windowysize = 250;
                }

                ImGui.EndChild();

                ImGui.SameLine();

                ImGui.BeginChild("2");
                // colour picker
                ImGui.ColorPicker4("App Colour", ref col);
                ImGui.EndChild();
            }

            if (cheatSettings == true)
            {
                ImGui.Text("Coming Soon...");
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

            ammo1 = swed.ReadInt(ammoAddress1);
            ammo2 = swed.ReadInt(ammoAddress2);

            //Console.WriteLine(ammo3);
            swed.WriteInt(ammoAddress1, 5);
            swed.WriteInt(ammoAddress2, 3);
        }

        public void infiniteHealth()
        {
            Swed swed = new Swed("iw4mp");
            IntPtr moduleBase = swed.GetModuleBase("iw4mp.exe");
            //IntPtr healthAddress = swed.ReadPointer(moduleBase, 0x02EE01D0) + 0x19C;
            IntPtr healthAddress = swed.ReadPointer(moduleBase, 0x00119EB0) + 0x684;

            swed.WriteInt(healthAddress, 100000);
        }

        public void flyHack()
        {
            Swed swed = new Swed("iw4mp");
            IntPtr moduleBase = swed.GetModuleBase("iw4mp.exe");

            IntPtr heightAddress1 = swed.ReadPointer(moduleBase, 0x0154BF90) + 0x24;
            //IntPtr heightAddress2 = swed.ReadPointer(moduleBase, 0x00138928) + 0xA54;

            swed.WriteFloat(heightAddress1, 100 + height);
            //swed.WriteFloat(heightAddress2, 1000);
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