using System;
using cope.Extensions;

namespace cope.Debug
{
    public static class OpcodeHelper
    {
        public static byte[] InitStdCall(UInt32 stackSize)
        {
            byte[] code;
            if (stackSize > 0)
            {
                code = new byte[9];
                code[0] = Push(R32.EBP);
                Sub(R32.ESP, stackSize).CopyTo(code, 1);
                Move(R32.EBP, R32.ESP).CopyTo(code, 7);
                return code;
            }
            code = new byte[3];
            code[0] = Push(R32.EBP);
            Move(R32.EBP, R32.ESP).CopyTo(code, 1);
            return code;
        }

        public static byte[] EndStdCall(UInt16 paramSize, UInt32 stackSize)
        {
            byte[] code1;
            byte[] code2;
            if (stackSize > 0)
            {
                code1 = new byte[6];
                Add(R32.ESP, stackSize);
            }
            else
            {
                code1 = new byte[0];
            }

            if (paramSize > 0)
            {
                code2 = new byte[4];
                Move(R32.EBP, R32.ESP).CopyTo(code2, 0);
                code2[2] = Pop(R32.EBP);
                code2[3] = Retn(0)[0];
            }
            else
            {
                code2 = new byte[6];
                Move(R32.EBP, R32.ESP).CopyTo(code2, 0);
                code2[2] = Pop(R32.EBP);
                Retn(paramSize).CopyTo(code2, 3);
            }
            return code1.Append(code2);
        }

        public static byte[] Sub(R32 reg, byte value)
        {
            var code = new byte[3];
            code[0] = 0x83;
            code[1] = (byte)(0xE8 + (byte)reg);
            code[2] = value;
            return code;
        }

        public static byte[] Sub(R32 reg, UInt32 value)
        {
            var code = new byte[6];
            code[0] = 0x81;
            code[1] = (byte)(0xE8 + (byte)reg);
            BitConverter.GetBytes(value).CopyTo(code, 3);
            return code;
        }

        public static byte[] Add(R32 reg, UInt32 value)
        {
            var code = new byte[6];
            code[0] = 0x81;
            code[1] = (byte)(0xC0 + (byte)reg);
            BitConverter.GetBytes(value).CopyTo(code, 3);
            return code;
        }

        public static byte[] Add(R32 reg, byte value)
        {
            var code = new byte[3];
            code[0] = 0x83;
            code[1] = (byte)(0xC0 + (byte)reg);
            code[2] = value;
            return code;
        }

        public static byte[] AbsoluteCall(UInt32 pAddress)
        {
            var code = new byte[6];
            code[0] = 0xFF;
            code[1] = 0x15;
            BitConverter.GetBytes(pAddress).CopyTo(code, 2);
            return code;
        }

        public static byte[] RelativeCall(UInt32 pOffset)
        {
            var code = new byte[5];
            code[0] = 0xE8;
            BitConverter.GetBytes(pOffset).CopyTo(code, 1);
            return code;
        }

        public static byte[] AbsoluteJump(UInt32 pAddress)
        {
            var code = new byte[6];
            code[0] = 0xFF;
            code[1] = 0x25;
            BitConverter.GetBytes(pAddress).CopyTo(code, 2);
            return code;
        }

        public static byte[] RelativeJump(UInt32 pOffset)
        {
            var code = new byte[5];
            code[0] = 0xE9;
            BitConverter.GetBytes(pOffset).CopyTo(code, 1);
            return code;
        }

        public static byte[] Call(R32 reg)
        {
            var code = new byte[2];
            code[0] = 0xFF;
            code[1] = (byte)(0xD0 + (byte)reg);
            return code;
        }

        public static byte[] Move(R8 to, byte value)
        {
            var code = new byte[2];
            code[0] = (byte)(0xB0 + (byte)to);
            code[1] = value;
            return code;
        }

        public static byte[] Move(R32 to, UInt32 value)
        {
            var code = new byte[5];
            BitConverter.GetBytes(value).CopyTo(code, 1);
            code[0] = (byte)(to + 0xB8);
            return code;
        }

        public static byte[] Move(R32 to, R32 from)
        {
            var code = new byte[2];
            code[0] = 0x8B;
            code[1] = (byte)(0xC0 + (byte)from + 8 * (byte)to);
            return code;
        }

        public static byte[] Retn(UInt16 value)
        {
            if (value == 0)
                return new byte[] { 0xC3 };
            var code = new byte[3];
            code[0] = 0xC2;
            BitConverter.GetBytes(value).CopyTo(code, 1);
            return code;
        }

        public static byte[] Retf(UInt16 value)
        {
            if (value == 0)
                return new byte[] { 0xCB };
            var code = new byte[3];
            code[0] = 0xCA;
            BitConverter.GetBytes(value).CopyTo(code, 1);
            return code;
        }

        public static byte[] Nop(int count)
        {
            var nops = new byte[count];
            for (int i = 0; i < count; i++)
                nops[i] = 0x90;
            return nops;
        }

        public static byte Breakpoint
        {
            get { return 0xCC; }
        }

        public static byte PushAll
        {
            get { return 0x60; }
        }

        public static byte PopAll
        {
            get { return 0x61; }
        }

        public static byte PushFlags
        {
            get { return 0x9C; }
        }

        public static byte PopFlags
        {
            get { return 0x9D; }
        }

        public static byte Dec(R32 reg)
        {
            return (byte)(0x48 + (byte)reg);
        }

        public static byte Inc(R32 reg)
        {
            return (byte)(0x40 + (byte)reg);
        }

        public static byte XChg(R32 reg)
        {
            return (byte)(0x90 + (byte)reg);
        }

        public static byte Push(R32 reg)
        {
            return (byte)(0x50 + (byte)reg);
        }

        public static byte Pop(R32 reg)
        {
            return (byte)(0x58 + (byte)reg);
        }
    
    }
}