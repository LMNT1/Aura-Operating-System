﻿/*
* PROJECT:          Aura Operating System Development
* CONTENT:          Debugger using UDP!
* PROGRAMMERS:      Valentin Charbonnier <valentinbreiz@gmail.com>
*/

using Aura_OS.System.Network.IPV4;
using Aura_OS.System.Network.IPV4.UDP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aura_OS.Apps.System
{
    public class Debugger
    {

        UdpClient xClient;

        public bool enabled = false;

        int port;
        public Address ip;

        public Debugger(Address IP, int Port)
        {
            ip = IP;
            port = Port;
        }

        public void Start()
        {
            xClient = new UdpClient(port);
            xClient.Connect(ip, port);
            if (enabled)
            {
                Send("--- Aura Debugger v0.1 ---");
                Send("Connected!");
            }
        }

        public void Send(string message)
        {
            if (enabled)
            {
                xClient.Send(Encoding.ASCII.GetBytes("[" + Aura_OS.System.Time.TimeString(true, true, true) + "] - " + message));
            }
        }
    }

    public class DebuggerSettings
    {

        /// <summary>
        /// Settings of the debugger
        /// </summary>
        public static void RegisterSetting()
        {
            string result = DispSettingsDialog();

            if (result.Equals("on"))
            {
                Console.Clear();
                Console.WriteLine("Starting debugger at: " + Kernel.debugger.ip.ToString() + ":4224");
                Kernel.debugger.enabled = true;
                Kernel.debugger.Start();
                Console.WriteLine("Debugger started!");
            }
            else if (result.Equals("off"))
            {
                Kernel.debugger.enabled = false;
                Console.WriteLine("Debugger disabled!");
            }
        }

        public static int x_;
        public static int y_;

        /// <summary>
        /// Display settings dialog
        /// </summary>
        public static string DispSettingsDialog()
        {
            int x = (Kernel.AConsole.Width / 2) - (64 / 2);
            int y = (Kernel.AConsole.Height / 2) - (10 / 2);
            x_ = x;
            y_ = y;
            SettingMenu(x, y);
            string[] item = { "on", "off" };
            int language = Aura_OS.System.Drawable.Menu.GenericMenu(item, Settings, x, y);
            if (language == 0)
            {
                return "on";
            }
            else if (language == 1)
            {
                return "off";
            }
            else
            {
                return "off";
            }
        }

        static int x_lang = Console.CursorLeft;
        static int y_lang = Console.CursorTop;

        static void SettingMenu(int x, int y)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Console.SetCursorPosition(x, y);
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.SetCursorPosition(x_lang, y_lang);
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("║ Enable or disable UDP debugger:                              ║");
            Console.SetCursorPosition(x_lang, y_lang);
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");

            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("║                                                              ║");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("║                                                              ║");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.SetCursorPosition(x, y + 5);
            Console.WriteLine("║                                                              ║");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.SetCursorPosition(x, y + 6);
            Console.WriteLine("║                                                              ║");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.SetCursorPosition(x, y + 7);
            Console.WriteLine("║                                                              ║");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.SetCursorPosition(x, y + 8);
            Console.WriteLine("║                                                              ║");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.SetCursorPosition(x, y + 9);
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.BackgroundColor = ConsoleColor.Black;
        }

        static void Settings()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Console.SetCursorPosition(x_ + 2, y_ + 3);
            Console.WriteLine(" ");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.SetCursorPosition(x_ + 2, y_ + 4);
            Console.WriteLine(" ");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.SetCursorPosition(x_ + 2, y_ + 5);
            Console.WriteLine(" ");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.SetCursorPosition(x_ + 2, y_ + 6);
            Console.WriteLine(" ");
            Console.SetCursorPosition(x_lang, y_lang);

            Console.SetCursorPosition(x_ + 2, y_ + 7);
            Console.WriteLine(" ");
            Console.SetCursorPosition(x_lang, y_lang);
        }
    }
}
