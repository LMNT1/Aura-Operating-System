﻿/*
* PROJECT:          Aura Operating System Development
* CONTENT:          VBE VESA Driver
* PROGRAMMERS:      Valentin Charbonnier <valentinbreiz@gmail.com>
*/

using Aura_OS.Core;
using Aura_OS.System.Shell.VESAVBE;
using Cosmos.Core.Memory.Old;

namespace Aura_OS.HAL.Drivers
{
    unsafe class VBE
    {

        private Cosmos.Core.IOGroup.VBE IO = Cosmos.Core.Global.BaseIOGroups.VBE;

        //public static void SetPixel(int x, int y, int RGB)
        //{
        //    int offset = x * (Graphics.depthVESA / 8) + y * (Graphics.widthVESA * (Graphics.depthVESA / 8));
        //
        //    Graphics.vga_mem[offset + 0] = (byte)(RGB & 0xff);
        //     Graphics.vga_mem[offset + 1] = (byte)((RGB >> 8) & 0xff);
        //    Graphics.vga_mem[offset + 2] = (byte)((RGB >> 16) & 0xff);
        //
        //}

        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int len;

        public VBE(int width, int height)
        {
            Width = width;
            Height = height;
            len = Width * Height;
            _buffer = (uint*)Heap.MemAlloc((uint)(len * 4));
        }

        public static uint* _buffer;

        public void SetPixel(int x, int y, uint c, bool background)
        {
            if (background)
            {
                if (c != 0x00)
                {
                    _buffer[x + (y * Width)] = c;

                    int offset = x * (Graphics.depthVESA / 8) + y * (Graphics.widthVESA * (Graphics.depthVESA / 8));
                    //
                    Graphics.vga_mem[offset + 0] = (byte)(c & 0xff);
                    Graphics.vga_mem[offset + 1] = (byte)((c >> 8) & 0xff);
                    Graphics.vga_mem[offset + 2] = (byte)((c >> 16) & 0xff);
                }
            }
            else
            {
                _buffer[x + (y * Width)] = c;

                int offset = x * (Graphics.depthVESA / 8) + y * (Graphics.widthVESA * (Graphics.depthVESA / 8));
                //
                Graphics.vga_mem[offset + 0] = (byte)(c & 0xff);
                Graphics.vga_mem[offset + 1] = (byte)((c >> 8) & 0xff);
                Graphics.vga_mem[offset + 2] = (byte)((c >> 16) & 0xff);
            }
        }

        internal void Clear(uint c)
        {
            //Memory.Memset(_buffer, c, (uint)(len));
            //WriteToScreen();
            ClearVRAM((uint)c);
        }

        public static void WriteToScreen()
        {
            //Memory.Memcpy((uint*)Graphics.vga_mem, _buffer, len);
            Cosmos.Core.MemoryOperations.Copy((uint*)Graphics.vga_mem, _buffer, len);
        }

        public void ScrollUp()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int m = 0; m < Width; m++)
                {
                    _buffer[i * Width + m] = _buffer[(i + 16) * Width + m]; // 16 is the font y size

                }
            }
            for (int i = Height - 16; i < Height; i++)
            {
                for (int m = 0; m < Width; m++)
                {
                    _buffer[i * Width + m] = 0x00;

                }
            }
        }

        public void ClearVRAM(int aStart, int aCount, int value)
        {
            IO.LinearFrameBuffer.Fill(aStart, aCount, value);
        }

        public void CopyVRAM(int aStart, int[] aData, int aIndex, int aCount)
        {
            IO.LinearFrameBuffer.Copy(aStart, aData, aIndex, aCount);
        }

        public void ClearVRAM(uint value)
        {
            IO.LinearFrameBuffer.Fill(value);
        }
    }
}
