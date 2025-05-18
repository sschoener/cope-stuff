#region

using System;
using cope.Extensions;

#endregion

namespace cope.Relic
{
    /// <summary>
    /// Helper class which implements the hashing function used in SpaceMarine.
    /// </summary>
    public static class SpaceMarineHash
    {
        // 512 entries
        private static readonly uint[] s_hashData = {
                                                        0x0, 0x0, 0x5800C, 0x1A1561E, 0x0B0018, 0x342AC3C, 0x0E8014,
                                                        0x2E3FA22,
                                                        0x160030, 0x6855878, 0x13803C, 0x7240E66, 0x1D0028, 0x5C7F444,
                                                        0x188024,
                                                        0x466A25A, 0x2C0060, 0x0D0AB0F0, 0x29806C, 0x0CABE6EE, 0x270078,
                                                        0x0E481CCC, 0x228074, 0x0FE94AD2, 0x3A0050, 0x0B8FE888, 0x3F805C
                                                        ,
                                                        0x0A2EBE96, 0x310048, 0x8CD44B4, 0x348044, 0x96C12AA, 0x5800C0,
                                                        0x1A1561E0, 0x5D80CC, 0x1BB437FE, 0x5300D8, 0x1957CDDC, 0x5680D4
                                                        ,
                                                        0x18F69BC2, 0x4E00F0, 0x1C903998, 0x4B80FC, 0x1D316F86, 0x4500E8
                                                        ,
                                                        0x1FD295A4, 0x4080E4, 0x1E73C3BA, 0x7400A0, 0x171FD110, 0x7180AC
                                                        ,
                                                        0x16BE870E, 0x7F00B8, 0x145D7D2C, 0x7A80B4, 0x15FC2B32, 0x620090
                                                        ,
                                                        0x119A8968, 0x67809C, 0x103BDF76, 0x690088, 0x12D82554, 0x6C8084
                                                        ,
                                                        0x1379734A, 0x0B00180, 0x342AC3C0, 0x0B5818C, 0x358B95DE,
                                                        0x0BB0198,
                                                        0x37686FFC, 0x0BE8194, 0x36C939E2, 0x0A601B0, 0x32AF9BB8,
                                                        0x0A381BC,
                                                        0x330ECDA6, 0x0AD01A8, 0x31ED3784, 0x0A881A4, 0x304C619A,
                                                        0x9C01E0,
                                                        0x39207330, 0x9981EC, 0x3881252E, 0x9701F8, 0x3A62DF0C, 0x9281F4
                                                        ,
                                                        0x3BC38912, 0x8A01D0, 0x3FA52B48, 0x8F81DC, 0x3E047D56, 0x8101C8
                                                        ,
                                                        0x3CE78774, 0x8481C4, 0x3D46D16A, 0x0E80140, 0x2E3FA220,
                                                        0x0ED814C,
                                                        0x2F9EF43E, 0x0E30158, 0x2D7D0E1C, 0x0E68154, 0x2CDC5802,
                                                        0x0FE0170,
                                                        0x28BAFA58, 0x0FB817C, 0x291BAC46, 0x0F50168, 0x2BF85664,
                                                        0x0F08164,
                                                        0x2A59007A, 0x0C40120, 0x233512D0, 0x0C1812C, 0x229444CE,
                                                        0x0CF0138,
                                                        0x2077BEEC, 0x0CA8134, 0x21D6E8F2, 0x0D20110, 0x25B04AA8,
                                                        0x0D7811C,
                                                        0x24111CB6, 0x0D90108, 0x26F2E694, 0x0DC8104, 0x2753B08A,
                                                        0x1600300,
                                                        0x68558780, 0x165830C, 0x69F4D19E, 0x16B0318, 0x6B172BBC,
                                                        0x16E8314,
                                                        0x6AB67DA2, 0x1760330, 0x6ED0DFF8, 0x173833C, 0x6F7189E6,
                                                        0x17D0328,
                                                        0x6D9273C4, 0x1788324, 0x6C3325DA, 0x14C0360, 0x655F3770,
                                                        0x149836C,
                                                        0x64FE616E, 0x1470378, 0x661D9B4C, 0x1428374, 0x67BCCD52,
                                                        0x15A0350,
                                                        0x63DA6F08, 0x15F835C, 0x627B3916, 0x1510348, 0x6098C334,
                                                        0x1548344,
                                                        0x6139952A, 0x13803C0, 0x7240E660, 0x13D83CC, 0x73E1B07E,
                                                        0x13303D8,
                                                        0x71024A5C, 0x13683D4, 0x70A31C42, 0x12E03F0, 0x74C5BE18,
                                                        0x12B83FC,
                                                        0x7564E806, 0x12503E8, 0x77871224, 0x12083E4, 0x7626443A,
                                                        0x11403A0,
                                                        0x7F4A5690, 0x11183AC, 0x7EEB008E, 0x11F03B8, 0x7C08FAAC,
                                                        0x11A83B4,
                                                        0x7DA9ACB2, 0x1020390, 0x79CF0EE8, 0x107839C, 0x786E58F6,
                                                        0x1090388,
                                                        0x7A8DA2D4, 0x10C8384, 0x7B2CF4CA, 0x1D00280, 0x5C7F4440,
                                                        0x1D5828C,
                                                        0x5DDE125E, 0x1DB0298, 0x5F3DE87C, 0x1DE8294, 0x5E9CBE62,
                                                        0x1C602B0,
                                                        0x5AFA1C38, 0x1C382BC, 0x5B5B4A26, 0x1CD02A8, 0x59B8B004,
                                                        0x1C882A4,
                                                        0x5819E61A, 0x1FC02E0, 0x5175F4B0, 0x1F982EC, 0x50D4A2AE,
                                                        0x1F702F8,
                                                        0x5237588C, 0x1F282F4, 0x53960E92, 0x1EA02D0, 0x57F0ACC8,
                                                        0x1EF82DC,
                                                        0x5651FAD6, 0x1E102C8, 0x54B200F4, 0x1E482C4, 0x551356EA,
                                                        0x1880240,
                                                        0x466A25A0, 0x18D824C, 0x47CB73BE, 0x1830258, 0x4528899C,
                                                        0x1868254,
                                                        0x4489DF82, 0x19E0270, 0x40EF7DD8, 0x19B827C, 0x414E2BC6,
                                                        0x1950268,
                                                        0x43ADD1E4, 0x1908264, 0x420C87FA, 0x1A40220, 0x4B609550,
                                                        0x1A1822C,
                                                        0x4AC1C34E, 0x1AF0238, 0x4822396C, 0x1AA8234, 0x49836F72,
                                                        0x1B20210,
                                                        0x4DE5CD28, 0x1B7821C, 0x4C449B36, 0x1B90208, 0x4EA76114,
                                                        0x1BC8204,
                                                        0x4F06370A, 0x2C00600, 0x0D0AB0F00, 0x2C5860C, 0x0D10A591E,
                                                        0x2CB0618,
                                                        0x0D3E9A33C, 0x2CE8614, 0x0D248F522, 0x2D60630, 0x0D62E5778,
                                                        0x2D3863C,
                                                        0x0D78F0166, 0x2DD0628, 0x0D56CFB44, 0x2D88624, 0x0D4CDAD5A,
                                                        0x2EC0660,
                                                        0x0DDA1BFF0, 0x2E9866C, 0x0DC00E9EE, 0x2E70678, 0x0DEE313CC,
                                                        0x2E28674,
                                                        0x0DF4245D2, 0x2FA0650, 0x0DB24E788, 0x2FF865C, 0x0DA85B196,
                                                        0x2F10648,
                                                        0x0D8664BB4, 0x2F48644, 0x0D9C71DAA, 0x29806C0, 0x0CABE6EE0,
                                                        0x29D86CC,
                                                        0x0CB1F38FE, 0x29306D8, 0x0C9FCC2DC, 0x29686D4, 0x0C85D94C2,
                                                        0x28E06F0,
                                                        0x0CC3B3698, 0x28B86FC, 0x0CD9A6086, 0x28506E8, 0x0CF799AA4,
                                                        0x28086E4,
                                                        0x0CED8CCBA, 0x2B406A0, 0x0C7B4DE10, 0x2B186AC, 0x0C615880E,
                                                        0x2BF06B8,
                                                        0x0C4F6722C, 0x2BA86B4, 0x0C5572432, 0x2A20690, 0x0C1318668,
                                                        0x2A7869C,
                                                        0x0C090D076, 0x2A90688, 0x0C2732A54, 0x2AC8684, 0x0C3D27C4A,
                                                        0x2700780,
                                                        0x0E481CCC0, 0x275878C, 0x0E5209ADE, 0x27B0798, 0x0E7C360FC,
                                                        0x27E8794,
                                                        0x0E66236E2, 0x26607B0, 0x0E20494B8, 0x26387BC, 0x0E3A5C2A6,
                                                        0x26D07A8,
                                                        0x0E1463884, 0x26887A4, 0x0E0E76E9A, 0x25C07E0, 0x0E98B7C30,
                                                        0x25987EC,
                                                        0x0E82A2A2E, 0x25707F8, 0x0EAC9D00C, 0x25287F4, 0x0EB688612,
                                                        0x24A07D0,
                                                        0x0EF0E2448, 0x24F87DC, 0x0EEAF7256, 0x24107C8, 0x0EC4C8874,
                                                        0x24487C4,
                                                        0x0EDEDDE6A, 0x2280740, 0x0FE94AD20, 0x22D874C, 0x0FF35FB3E,
                                                        0x2230758,
                                                        0x0FDD6011C, 0x2268754, 0x0FC775702, 0x23E0770, 0x0F811F558,
                                                        0x23B877C,
                                                        0x0F9B0A346, 0x2350768, 0x0FB535964, 0x2308764, 0x0FAF20F7A,
                                                        0x2040720,
                                                        0x0F39E1DD0, 0x201872C, 0x0F23F4BCE, 0x20F0738, 0x0F0DCB1EC,
                                                        0x20A8734,
                                                        0x0F17DE7F2, 0x2120710, 0x0F51B45A8, 0x217871C, 0x0F4BA13B6,
                                                        0x2190708,
                                                        0x0F659E994, 0x21C8704, 0x0F7F8BF8A, 0x3A00500, 0x0B8FE8880,
                                                        0x3A5850C,
                                                        0x0B95FDE9E, 0x3AB0518, 0x0BBBC24BC, 0x3AE8514, 0x0BA1D72A2,
                                                        0x3B60530,
                                                        0x0BE7BD0F8, 0x3B3853C, 0x0BFDA86E6, 0x3BD0528, 0x0BD397CC4,
                                                        0x3B88524,
                                                        0x0BC982ADA, 0x38C0560, 0x0B5F43870, 0x389856C, 0x0B4556E6E,
                                                        0x3870578,
                                                        0x0B6B6944C, 0x3828574, 0x0B717C252, 0x39A0550, 0x0B3716008,
                                                        0x39F855C,
                                                        0x0B2D03616, 0x3910548, 0x0B033CC34, 0x3948544, 0x0B1929A2A,
                                                        0x3F805C0,
                                                        0x0A2EBE960, 0x3FD85CC, 0x0A34ABF7E, 0x3F305D8, 0x0A1A9455C,
                                                        0x3F685D4,
                                                        0x0A0081342, 0x3EE05F0, 0x0A46EB118, 0x3EB85FC, 0x0A5CFE706,
                                                        0x3E505E8,
                                                        0x0A72C1D24, 0x3E085E4, 0x0A68D4B3A, 0x3D405A0, 0x0AFE15990,
                                                        0x3D185AC,
                                                        0x0AE400F8E, 0x3DF05B8, 0x0ACA3F5AC, 0x3DA85B4, 0x0AD02A3B2,
                                                        0x3C20590,
                                                        0x0A96401E8, 0x3C7859C, 0x0A8C557F6, 0x3C90588, 0x0AA26ADD4,
                                                        0x3CC8584,
                                                        0x0AB87FBCA, 0x3100480, 0x8CD44B40, 0x315848C, 0x8D751D5E,
                                                        0x31B0498,
                                                        0x8F96E77C, 0x31E8494, 0x8E37B162, 0x30604B0, 0x8A511338,
                                                        0x30384BC,
                                                        0x8BF04526, 0x30D04A8, 0x8913BF04, 0x30884A4, 0x88B2E91A,
                                                        0x33C04E0,
                                                        0x81DEFBB0, 0x33984EC, 0x807FADAE, 0x33704F8, 0x829C578C,
                                                        0x33284F4,
                                                        0x833D0192, 0x32A04D0, 0x875BA3C8, 0x32F84DC, 0x86FAF5D6,
                                                        0x32104C8,
                                                        0x84190FF4, 0x32484C4, 0x85B859EA, 0x3480440, 0x96C12AA0,
                                                        0x34D844C,
                                                        0x97607CBE, 0x3430458, 0x9583869C, 0x3468454, 0x9422D082,
                                                        0x35E0470,
                                                        0x904472D8, 0x35B847C, 0x91E524C6, 0x3550468, 0x9306DEE4,
                                                        0x3508464,
                                                        0x92A788FA, 0x3640420, 0x9BCB9A50, 0x361842C, 0x9A6ACC4E,
                                                        0x36F0438,
                                                        0x9889366C, 0x36A8434, 0x99286072, 0x3720410, 0x9D4EC228,
                                                        0x377841C,
                                                        0x9CEF9436, 0x3790408, 0x9E0C6E14, 0x37C8404, 0x9FAD380A
                                                    };

        /// <summary>
        /// Returns a 64bit hash value.
        /// </summary>
        /// <param name="toHash"></param>
        /// <returns></returns>
        public static byte[] CalculateHash(byte[] toHash)
        {
            /*
             loc_8C9ACE:
                movzx   eax, byte ptr [esi]
                dec     [esp+0Ch+strLen]
                push    eax
                call    ds:tolower
                xor     eax, edi
                shrd    edi, ebx, 8
                and     eax, 0FFh
                xor     edi, ds:dword_12F3438[eax*8]
                shr     ebx, 8
                xor     ebx, ds:dword_12F3438+4[eax*8]
                pop     ecx
                inc     esi
             */

            uint firstPart = 0xFFFFFFFF;
            uint secondPart = 0xFFFFFFFF;

            for (int idx = 0; idx < toHash.Length; idx++)
            {
                byte current = toHash[idx];
                int offset = (int) ((current ^ firstPart) & 0xFF); // XOR

                firstPart = firstPart >> 8;
                uint secondPartLastByte = (secondPart & 0x000000FF) << 24;
                firstPart = firstPart | secondPartLastByte; // SHRD 8

                secondPart = secondPart >> 8; // SHR 8

                firstPart = firstPart ^ s_hashData[offset * 2];
                secondPart = secondPart ^ s_hashData[(offset * 2) + 1];
            }
            // subtracting 1 is _NOT_ part of the original algorithm but had to be added
            // to get correct results
            int first32Bits = (int) (-firstPart - 1); // NOT
            int second32Bits = (int) (-secondPart - 1);

            // put the values into a byte-array and return it
            return BitConverter.GetBytes(first32Bits).Append(BitConverter.GetBytes(second32Bits));
        }
    }
}