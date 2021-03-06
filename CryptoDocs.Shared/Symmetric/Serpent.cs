﻿using System;

namespace CryptoDocs.Shared.Symmetric
{
    class Serpent
    {
        private const int Rounds = 32;
        private const int Phi = unchecked((int) 0x9E3779B9); // (Sqrt(5) - 1) * 2**31
        public const int BlockSize = 16;
        public const int KeySize = 32;

        private readonly int[] _workingKey;

        private int X0, X1, X2, X3;    // registers

        public Serpent(byte[] key)
        {
            var keySize = key.Length * 8;

            if (!(keySize == 128 || keySize == 192 || keySize == 256))
                throw new ArgumentException($"KeySize '{keySize}' is not valid for this algorithm.");

            _workingKey = MakeWorkingKey(key);
        }

        public int EncryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            if (inputCount != BlockSize)
                throw new ArgumentException("inputCount");

            X3 = BytesToWord(inputBuffer, inputOffset);
            X2 = BytesToWord(inputBuffer, inputOffset + 4);
            X1 = BytesToWord(inputBuffer, inputOffset + 8);
            X0 = BytesToWord(inputBuffer, inputOffset + 12);

            Sb0(this._workingKey[0] ^ X0, this._workingKey[1] ^ X1, this._workingKey[2] ^ X2, this._workingKey[3] ^ X3); LT();
            Sb1(this._workingKey[4] ^ X0, this._workingKey[5] ^ X1, this._workingKey[6] ^ X2, this._workingKey[7] ^ X3); LT();
            Sb2(this._workingKey[8] ^ X0, this._workingKey[9] ^ X1, this._workingKey[10] ^ X2, this._workingKey[11] ^ X3); LT();
            Sb3(this._workingKey[12] ^ X0, this._workingKey[13] ^ X1, this._workingKey[14] ^ X2, this._workingKey[15] ^ X3); LT();
            Sb4(this._workingKey[16] ^ X0, this._workingKey[17] ^ X1, this._workingKey[18] ^ X2, this._workingKey[19] ^ X3); LT();
            Sb5(this._workingKey[20] ^ X0, this._workingKey[21] ^ X1, this._workingKey[22] ^ X2, this._workingKey[23] ^ X3); LT();
            Sb6(this._workingKey[24] ^ X0, this._workingKey[25] ^ X1, this._workingKey[26] ^ X2, this._workingKey[27] ^ X3); LT();
            Sb7(this._workingKey[28] ^ X0, this._workingKey[29] ^ X1, this._workingKey[30] ^ X2, this._workingKey[31] ^ X3); LT();
            Sb0(this._workingKey[32] ^ X0, this._workingKey[33] ^ X1, this._workingKey[34] ^ X2, this._workingKey[35] ^ X3); LT();
            Sb1(this._workingKey[36] ^ X0, this._workingKey[37] ^ X1, this._workingKey[38] ^ X2, this._workingKey[39] ^ X3); LT();
            Sb2(this._workingKey[40] ^ X0, this._workingKey[41] ^ X1, this._workingKey[42] ^ X2, this._workingKey[43] ^ X3); LT();
            Sb3(this._workingKey[44] ^ X0, this._workingKey[45] ^ X1, this._workingKey[46] ^ X2, this._workingKey[47] ^ X3); LT();
            Sb4(this._workingKey[48] ^ X0, this._workingKey[49] ^ X1, this._workingKey[50] ^ X2, this._workingKey[51] ^ X3); LT();
            Sb5(this._workingKey[52] ^ X0, this._workingKey[53] ^ X1, this._workingKey[54] ^ X2, this._workingKey[55] ^ X3); LT();
            Sb6(this._workingKey[56] ^ X0, this._workingKey[57] ^ X1, this._workingKey[58] ^ X2, this._workingKey[59] ^ X3); LT();
            Sb7(this._workingKey[60] ^ X0, this._workingKey[61] ^ X1, this._workingKey[62] ^ X2, this._workingKey[63] ^ X3); LT();
            Sb0(this._workingKey[64] ^ X0, this._workingKey[65] ^ X1, this._workingKey[66] ^ X2, this._workingKey[67] ^ X3); LT();
            Sb1(this._workingKey[68] ^ X0, this._workingKey[69] ^ X1, this._workingKey[70] ^ X2, this._workingKey[71] ^ X3); LT();
            Sb2(this._workingKey[72] ^ X0, this._workingKey[73] ^ X1, this._workingKey[74] ^ X2, this._workingKey[75] ^ X3); LT();
            Sb3(this._workingKey[76] ^ X0, this._workingKey[77] ^ X1, this._workingKey[78] ^ X2, this._workingKey[79] ^ X3); LT();
            Sb4(this._workingKey[80] ^ X0, this._workingKey[81] ^ X1, this._workingKey[82] ^ X2, this._workingKey[83] ^ X3); LT();
            Sb5(this._workingKey[84] ^ X0, this._workingKey[85] ^ X1, this._workingKey[86] ^ X2, this._workingKey[87] ^ X3); LT();
            Sb6(this._workingKey[88] ^ X0, this._workingKey[89] ^ X1, this._workingKey[90] ^ X2, this._workingKey[91] ^ X3); LT();
            Sb7(this._workingKey[92] ^ X0, this._workingKey[93] ^ X1, this._workingKey[94] ^ X2, this._workingKey[95] ^ X3); LT();
            Sb0(this._workingKey[96] ^ X0, this._workingKey[97] ^ X1, this._workingKey[98] ^ X2, this._workingKey[99] ^ X3); LT();
            Sb1(this._workingKey[100] ^ X0, this._workingKey[101] ^ X1, this._workingKey[102] ^ X2, this._workingKey[103] ^ X3); LT();
            Sb2(this._workingKey[104] ^ X0, this._workingKey[105] ^ X1, this._workingKey[106] ^ X2, this._workingKey[107] ^ X3); LT();
            Sb3(this._workingKey[108] ^ X0, this._workingKey[109] ^ X1, this._workingKey[110] ^ X2, this._workingKey[111] ^ X3); LT();
            Sb4(this._workingKey[112] ^ X0, this._workingKey[113] ^ X1, this._workingKey[114] ^ X2, this._workingKey[115] ^ X3); LT();
            Sb5(this._workingKey[116] ^ X0, this._workingKey[117] ^ X1, this._workingKey[118] ^ X2, this._workingKey[119] ^ X3); LT();
            Sb6(this._workingKey[120] ^ X0, this._workingKey[121] ^ X1, this._workingKey[122] ^ X2, this._workingKey[123] ^ X3); LT();
            Sb7(this._workingKey[124] ^ X0, this._workingKey[125] ^ X1, this._workingKey[126] ^ X2, this._workingKey[127] ^ X3);

            WordToBytes(this._workingKey[131] ^ X3, outputBuffer, outputOffset);
            WordToBytes(this._workingKey[130] ^ X2, outputBuffer, outputOffset + 4);
            WordToBytes(this._workingKey[129] ^ X1, outputBuffer, outputOffset + 8);
            WordToBytes(this._workingKey[128] ^ X0, outputBuffer, outputOffset + 12);

            return BlockSize;
        }

        public int DecryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            if (inputCount != BlockSize)
                throw new ArgumentException("inputCount");

            X3 = this._workingKey[131] ^ BytesToWord(inputBuffer, inputOffset);
            X2 = this._workingKey[130] ^ BytesToWord(inputBuffer, inputOffset + 4);
            X1 = this._workingKey[129] ^ BytesToWord(inputBuffer, inputOffset + 8);
            X0 = this._workingKey[128] ^ BytesToWord(inputBuffer, inputOffset + 12);

            Ib7(X0, X1, X2, X3);
            X0 ^= this._workingKey[124]; X1 ^= this._workingKey[125]; X2 ^= this._workingKey[126]; X3 ^= this._workingKey[127];
            InverseLT(); Ib6(X0, X1, X2, X3);
            X0 ^= this._workingKey[120]; X1 ^= this._workingKey[121]; X2 ^= this._workingKey[122]; X3 ^= this._workingKey[123];
            InverseLT(); Ib5(X0, X1, X2, X3);
            X0 ^= this._workingKey[116]; X1 ^= this._workingKey[117]; X2 ^= this._workingKey[118]; X3 ^= this._workingKey[119];
            InverseLT(); Ib4(X0, X1, X2, X3);
            X0 ^= this._workingKey[112]; X1 ^= this._workingKey[113]; X2 ^= this._workingKey[114]; X3 ^= this._workingKey[115];
            InverseLT(); Ib3(X0, X1, X2, X3);
            X0 ^= this._workingKey[108]; X1 ^= this._workingKey[109]; X2 ^= this._workingKey[110]; X3 ^= this._workingKey[111];
            InverseLT(); Ib2(X0, X1, X2, X3);
            X0 ^= this._workingKey[104]; X1 ^= this._workingKey[105]; X2 ^= this._workingKey[106]; X3 ^= this._workingKey[107];
            InverseLT(); Ib1(X0, X1, X2, X3);
            X0 ^= this._workingKey[100]; X1 ^= this._workingKey[101]; X2 ^= this._workingKey[102]; X3 ^= this._workingKey[103];
            InverseLT(); Ib0(X0, X1, X2, X3);
            X0 ^= this._workingKey[96]; X1 ^= this._workingKey[97]; X2 ^= this._workingKey[98]; X3 ^= this._workingKey[99];
            InverseLT(); Ib7(X0, X1, X2, X3);
            X0 ^= this._workingKey[92]; X1 ^= this._workingKey[93]; X2 ^= this._workingKey[94]; X3 ^= this._workingKey[95];
            InverseLT(); Ib6(X0, X1, X2, X3);
            X0 ^= this._workingKey[88]; X1 ^= this._workingKey[89]; X2 ^= this._workingKey[90]; X3 ^= this._workingKey[91];
            InverseLT(); Ib5(X0, X1, X2, X3);
            X0 ^= this._workingKey[84]; X1 ^= this._workingKey[85]; X2 ^= this._workingKey[86]; X3 ^= this._workingKey[87];
            InverseLT(); Ib4(X0, X1, X2, X3);
            X0 ^= this._workingKey[80]; X1 ^= this._workingKey[81]; X2 ^= this._workingKey[82]; X3 ^= this._workingKey[83];
            InverseLT(); Ib3(X0, X1, X2, X3);
            X0 ^= this._workingKey[76]; X1 ^= this._workingKey[77]; X2 ^= this._workingKey[78]; X3 ^= this._workingKey[79];
            InverseLT(); Ib2(X0, X1, X2, X3);
            X0 ^= this._workingKey[72]; X1 ^= this._workingKey[73]; X2 ^= this._workingKey[74]; X3 ^= this._workingKey[75];
            InverseLT(); Ib1(X0, X1, X2, X3);
            X0 ^= this._workingKey[68]; X1 ^= this._workingKey[69]; X2 ^= this._workingKey[70]; X3 ^= this._workingKey[71];
            InverseLT(); Ib0(X0, X1, X2, X3);
            X0 ^= this._workingKey[64]; X1 ^= this._workingKey[65]; X2 ^= this._workingKey[66]; X3 ^= this._workingKey[67];
            InverseLT(); Ib7(X0, X1, X2, X3);
            X0 ^= this._workingKey[60]; X1 ^= this._workingKey[61]; X2 ^= this._workingKey[62]; X3 ^= this._workingKey[63];
            InverseLT(); Ib6(X0, X1, X2, X3);
            X0 ^= this._workingKey[56]; X1 ^= this._workingKey[57]; X2 ^= this._workingKey[58]; X3 ^= this._workingKey[59];
            InverseLT(); Ib5(X0, X1, X2, X3);
            X0 ^= this._workingKey[52]; X1 ^= this._workingKey[53]; X2 ^= this._workingKey[54]; X3 ^= this._workingKey[55];
            InverseLT(); Ib4(X0, X1, X2, X3);
            X0 ^= this._workingKey[48]; X1 ^= this._workingKey[49]; X2 ^= this._workingKey[50]; X3 ^= this._workingKey[51];
            InverseLT(); Ib3(X0, X1, X2, X3);
            X0 ^= this._workingKey[44]; X1 ^= this._workingKey[45]; X2 ^= this._workingKey[46]; X3 ^= this._workingKey[47];
            InverseLT(); Ib2(X0, X1, X2, X3);
            X0 ^= this._workingKey[40]; X1 ^= this._workingKey[41]; X2 ^= this._workingKey[42]; X3 ^= this._workingKey[43];
            InverseLT(); Ib1(X0, X1, X2, X3);
            X0 ^= this._workingKey[36]; X1 ^= this._workingKey[37]; X2 ^= this._workingKey[38]; X3 ^= this._workingKey[39];
            InverseLT(); Ib0(X0, X1, X2, X3);
            X0 ^= this._workingKey[32]; X1 ^= this._workingKey[33]; X2 ^= this._workingKey[34]; X3 ^= this._workingKey[35];
            InverseLT(); Ib7(X0, X1, X2, X3);
            X0 ^= this._workingKey[28]; X1 ^= this._workingKey[29]; X2 ^= this._workingKey[30]; X3 ^= this._workingKey[31];
            InverseLT(); Ib6(X0, X1, X2, X3);
            X0 ^= this._workingKey[24]; X1 ^= this._workingKey[25]; X2 ^= this._workingKey[26]; X3 ^= this._workingKey[27];
            InverseLT(); Ib5(X0, X1, X2, X3);
            X0 ^= this._workingKey[20]; X1 ^= this._workingKey[21]; X2 ^= this._workingKey[22]; X3 ^= this._workingKey[23];
            InverseLT(); Ib4(X0, X1, X2, X3);
            X0 ^= this._workingKey[16]; X1 ^= this._workingKey[17]; X2 ^= this._workingKey[18]; X3 ^= this._workingKey[19];
            InverseLT(); Ib3(X0, X1, X2, X3);
            X0 ^= this._workingKey[12]; X1 ^= this._workingKey[13]; X2 ^= this._workingKey[14]; X3 ^= this._workingKey[15];
            InverseLT(); Ib2(X0, X1, X2, X3);
            X0 ^= this._workingKey[8]; X1 ^= this._workingKey[9]; X2 ^= this._workingKey[10]; X3 ^= this._workingKey[11];
            InverseLT(); Ib1(X0, X1, X2, X3);
            X0 ^= this._workingKey[4]; X1 ^= this._workingKey[5]; X2 ^= this._workingKey[6]; X3 ^= this._workingKey[7];
            InverseLT(); Ib0(X0, X1, X2, X3);

            WordToBytes(X3 ^ this._workingKey[3], outputBuffer, outputOffset);
            WordToBytes(X2 ^ this._workingKey[2], outputBuffer, outputOffset + 4);
            WordToBytes(X1 ^ this._workingKey[1], outputBuffer, outputOffset + 8);
            WordToBytes(X0 ^ this._workingKey[0], outputBuffer, outputOffset + 12);

            return BlockSize;
        }

        private int[] MakeWorkingKey(byte[] key)
        {
            //
            // pad key to 256 bits
            //
            int[] kPad = new int[16];
            int off = 0;
            int length = 0;

            for (off = key.Length - 4; off > 0; off -= 4)
            {
                kPad[length++] = BytesToWord(key, off);
            }

            if (off == 0)
            {
                kPad[length++] = BytesToWord(key, 0);
                if (length < 8)
                {
                    kPad[length] = 1;
                }
            }
            else
            {
                throw new ArgumentException("key must be a multiple of 4 bytes");
            }

            //
            // expand the padded key up to 33 x 128 bits of key material
            //
            int amount = (Rounds + 1) * 4;
            int[] w = new int[amount];

            //
            // compute w0 to w7 from w-8 to w-1
            //
            for (int i = 8; i < 16; i++)
            {
                kPad[i] = RotateLeft(kPad[i - 8] ^ kPad[i - 5] ^ kPad[i - 3] ^ kPad[i - 1] ^ Phi ^ (i - 8), 11);
            }

            Buffer.BlockCopy(kPad, 8, w, 0, 8);

            //
            // compute w8 to w136
            //
            for (int i = 8; i < amount; i++)
            {
                w[i] = RotateLeft(w[i - 8] ^ w[i - 5] ^ w[i - 3] ^ w[i - 1] ^ Phi ^ i, 11);
            }

            //
            // create the working keys by processing w with the Sbox and IP
            //
            Sb3(w[0], w[1], w[2], w[3]);
            w[0] = X0; w[1] = X1; w[2] = X2; w[3] = X3;
            Sb2(w[4], w[5], w[6], w[7]);
            w[4] = X0; w[5] = X1; w[6] = X2; w[7] = X3;
            Sb1(w[8], w[9], w[10], w[11]);
            w[8] = X0; w[9] = X1; w[10] = X2; w[11] = X3;
            Sb0(w[12], w[13], w[14], w[15]);
            w[12] = X0; w[13] = X1; w[14] = X2; w[15] = X3;
            Sb7(w[16], w[17], w[18], w[19]);
            w[16] = X0; w[17] = X1; w[18] = X2; w[19] = X3;
            Sb6(w[20], w[21], w[22], w[23]);
            w[20] = X0; w[21] = X1; w[22] = X2; w[23] = X3;
            Sb5(w[24], w[25], w[26], w[27]);
            w[24] = X0; w[25] = X1; w[26] = X2; w[27] = X3;
            Sb4(w[28], w[29], w[30], w[31]);
            w[28] = X0; w[29] = X1; w[30] = X2; w[31] = X3;
            Sb3(w[32], w[33], w[34], w[35]);
            w[32] = X0; w[33] = X1; w[34] = X2; w[35] = X3;
            Sb2(w[36], w[37], w[38], w[39]);
            w[36] = X0; w[37] = X1; w[38] = X2; w[39] = X3;
            Sb1(w[40], w[41], w[42], w[43]);
            w[40] = X0; w[41] = X1; w[42] = X2; w[43] = X3;
            Sb0(w[44], w[45], w[46], w[47]);
            w[44] = X0; w[45] = X1; w[46] = X2; w[47] = X3;
            Sb7(w[48], w[49], w[50], w[51]);
            w[48] = X0; w[49] = X1; w[50] = X2; w[51] = X3;
            Sb6(w[52], w[53], w[54], w[55]);
            w[52] = X0; w[53] = X1; w[54] = X2; w[55] = X3;
            Sb5(w[56], w[57], w[58], w[59]);
            w[56] = X0; w[57] = X1; w[58] = X2; w[59] = X3;
            Sb4(w[60], w[61], w[62], w[63]);
            w[60] = X0; w[61] = X1; w[62] = X2; w[63] = X3;
            Sb3(w[64], w[65], w[66], w[67]);
            w[64] = X0; w[65] = X1; w[66] = X2; w[67] = X3;
            Sb2(w[68], w[69], w[70], w[71]);
            w[68] = X0; w[69] = X1; w[70] = X2; w[71] = X3;
            Sb1(w[72], w[73], w[74], w[75]);
            w[72] = X0; w[73] = X1; w[74] = X2; w[75] = X3;
            Sb0(w[76], w[77], w[78], w[79]);
            w[76] = X0; w[77] = X1; w[78] = X2; w[79] = X3;
            Sb7(w[80], w[81], w[82], w[83]);
            w[80] = X0; w[81] = X1; w[82] = X2; w[83] = X3;
            Sb6(w[84], w[85], w[86], w[87]);
            w[84] = X0; w[85] = X1; w[86] = X2; w[87] = X3;
            Sb5(w[88], w[89], w[90], w[91]);
            w[88] = X0; w[89] = X1; w[90] = X2; w[91] = X3;
            Sb4(w[92], w[93], w[94], w[95]);
            w[92] = X0; w[93] = X1; w[94] = X2; w[95] = X3;
            Sb3(w[96], w[97], w[98], w[99]);
            w[96] = X0; w[97] = X1; w[98] = X2; w[99] = X3;
            Sb2(w[100], w[101], w[102], w[103]);
            w[100] = X0; w[101] = X1; w[102] = X2; w[103] = X3;
            Sb1(w[104], w[105], w[106], w[107]);
            w[104] = X0; w[105] = X1; w[106] = X2; w[107] = X3;
            Sb0(w[108], w[109], w[110], w[111]);
            w[108] = X0; w[109] = X1; w[110] = X2; w[111] = X3;
            Sb7(w[112], w[113], w[114], w[115]);
            w[112] = X0; w[113] = X1; w[114] = X2; w[115] = X3;
            Sb6(w[116], w[117], w[118], w[119]);
            w[116] = X0; w[117] = X1; w[118] = X2; w[119] = X3;
            Sb5(w[120], w[121], w[122], w[123]);
            w[120] = X0; w[121] = X1; w[122] = X2; w[123] = X3;
            Sb4(w[124], w[125], w[126], w[127]);
            w[124] = X0; w[125] = X1; w[126] = X2; w[127] = X3;
            Sb3(w[128], w[129], w[130], w[131]);
            w[128] = X0; w[129] = X1; w[130] = X2; w[131] = X3;

            return w;
        }

        private int RotateLeft(int x, int bits)
        {
            return ((x << bits) | (int)((uint)x >> (32 - bits)));
        }

        private int RotateRight(int x, int bits)
        {
            return ((int)((uint)x >> bits) | (x << (32 - bits)));
        }

        private int BytesToWord(byte[] src, int srcOff)
        {
            return (((src[srcOff] & 0xff) << 24) | ((src[srcOff + 1] & 0xff) << 16) |
            ((src[srcOff + 2] & 0xff) << 8) | ((src[srcOff + 3] & 0xff)));
        }

        private void WordToBytes(int word, byte[] dst, int dstOff)
        {
            dst[dstOff + 3] = (byte)(word);
            dst[dstOff + 2] = (byte)((uint)word >> 8);
            dst[dstOff + 1] = (byte)((uint)word >> 16);
            dst[dstOff] = (byte)((uint)word >> 24);
        }

        private void Sb0(int a, int b, int c, int d)
        {
            int t1 = a ^ d;
            int t3 = c ^ t1;
            int t4 = b ^ t3;
            X3 = (a & d) ^ t4;
            int t7 = a ^ (b & t1);
            X2 = t4 ^ (c | t7);
            int t12 = X3 & (t3 ^ t7);
            X1 = (~t3) ^ t12;
            X0 = t12 ^ (~t7);
        }

        private void Ib0(int a, int b, int c, int d)
        {
            int t1 = ~a;
            int t2 = a ^ b;
            int t4 = d ^ (t1 | t2);
            int t5 = c ^ t4;
            X2 = t2 ^ t5;
            int t8 = t1 ^ (d & t2);
            X1 = t4 ^ (X2 & t8);
            X3 = (a & t4) ^ (t5 | X1);
            X0 = X3 ^ (t5 ^ t8);
        }
        private void Sb1(int a, int b, int c, int d)
        {
            int t2 = b ^ (~a);
            int t5 = c ^ (a | t2);
            X2 = d ^ t5;
            int t7 = b ^ (d | t2);
            int t8 = t2 ^ X2;
            X3 = t8 ^ (t5 & t7);
            int t11 = t5 ^ t7;
            X1 = X3 ^ t11;
            X0 = t5 ^ (t8 & t11);
        }

        private void Ib1(int a, int b, int c, int d)
        {
            int t1 = b ^ d;
            int t3 = a ^ (b & t1);
            int t4 = t1 ^ t3;
            X3 = c ^ t4;
            int t7 = b ^ (t1 & t3);
            int t8 = X3 | t7;
            X1 = t3 ^ t8;
            int t10 = ~X1;
            int t11 = X3 ^ t7;
            X0 = t10 ^ t11;
            X2 = t4 ^ (t10 | t11);
        }

        private void Sb2(int a, int b, int c, int d)
        {
            int t1 = ~a;
            int t2 = b ^ d;
            int t3 = c & t1;
            X0 = t2 ^ t3;
            int t5 = c ^ t1;
            int t6 = c ^ X0;
            int t7 = b & t6;
            X3 = t5 ^ t7;
            X2 = a ^ ((d | t7) & (X0 | t5));
            X1 = (t2 ^ X3) ^ (X2 ^ (d | t1));
        }

        private void Ib2(int a, int b, int c, int d)
        {
            int t1 = b ^ d;
            int t2 = ~t1;
            int t3 = a ^ c;
            int t4 = c ^ t1;
            int t5 = b & t4;
            X0 = t3 ^ t5;
            int t7 = a | t2;
            int t8 = d ^ t7;
            int t9 = t3 | t8;
            X3 = t1 ^ t9;
            int t11 = ~t4;
            int t12 = X0 | X3;
            X1 = t11 ^ t12;
            X2 = (d & t11) ^ (t3 ^ t12);
        }

        private void Sb3(int a, int b, int c, int d)
        {
            int t1 = a ^ b;
            int t2 = a & c;
            int t3 = a | d;
            int t4 = c ^ d;
            int t5 = t1 & t3;
            int t6 = t2 | t5;
            X2 = t4 ^ t6;
            int t8 = b ^ t3;
            int t9 = t6 ^ t8;
            int t10 = t4 & t9;
            X0 = t1 ^ t10;
            int t12 = X2 & X0;
            X1 = t9 ^ t12;
            X3 = (b | d) ^ (t4 ^ t12);
        }

        private void Ib3(int a, int b, int c, int d)
        {
            int t1 = a | b;
            int t2 = b ^ c;
            int t3 = b & t2;
            int t4 = a ^ t3;
            int t5 = c ^ t4;
            int t6 = d | t4;
            X0 = t2 ^ t6;
            int t8 = t2 | t6;
            int t9 = d ^ t8;
            X2 = t5 ^ t9;
            int t11 = t1 ^ t9;
            int t12 = X0 & t11;
            X3 = t4 ^ t12;
            X1 = X3 ^ (X0 ^ t11);
        }

        private void Sb4(int a, int b, int c, int d)
        {
            int t1 = a ^ d;
            int t2 = d & t1;
            int t3 = c ^ t2;
            int t4 = b | t3;
            X3 = t1 ^ t4;
            int t6 = ~b;
            int t7 = t1 | t6;
            X0 = t3 ^ t7;
            int t9 = a & X0;
            int t10 = t1 ^ t6;
            int t11 = t4 & t10;
            X2 = t9 ^ t11;
            X1 = (a ^ t3) ^ (t10 & X2);
        }

        private void Ib4(int a, int b, int c, int d)
        {
            int t1 = c | d;
            int t2 = a & t1;
            int t3 = b ^ t2;
            int t4 = a & t3;
            int t5 = c ^ t4;
            X1 = d ^ t5;
            int t7 = ~a;
            int t8 = t5 & X1;
            X3 = t3 ^ t8;
            int t10 = X1 | t7;
            int t11 = d ^ t10;
            X0 = X3 ^ t11;
            X2 = (t3 & t11) ^ (X1 ^ t7);
        }

        private void Sb5(int a, int b, int c, int d)
        {
            int t1 = ~a;
            int t2 = a ^ b;
            int t3 = a ^ d;
            int t4 = c ^ t1;
            int t5 = t2 | t3;
            X0 = t4 ^ t5;
            int t7 = d & X0;
            int t8 = t2 ^ X0;
            X1 = t7 ^ t8;
            int t10 = t1 | X0;
            int t11 = t2 | t7;
            int t12 = t3 ^ t10;
            X2 = t11 ^ t12;
            X3 = (b ^ t7) ^ (X1 & t12);
        }

        private void Ib5(int a, int b, int c, int d)
        {
            int t1 = ~c;
            int t2 = b & t1;
            int t3 = d ^ t2;
            int t4 = a & t3;
            int t5 = b ^ t1;
            X3 = t4 ^ t5;
            int t7 = b | X3;
            int t8 = a & t7;
            X1 = t3 ^ t8;
            int t10 = a | d;
            int t11 = t1 ^ t7;
            X0 = t10 ^ t11;
            X2 = (b & t10) ^ (t4 | (a ^ c));
        }

        private void Sb6(int a, int b, int c, int d)
        {
            int t1 = ~a;
            int t2 = a ^ d;
            int t3 = b ^ t2;
            int t4 = t1 | t2;
            int t5 = c ^ t4;
            X1 = b ^ t5;
            int t7 = t2 | X1;
            int t8 = d ^ t7;
            int t9 = t5 & t8;
            X2 = t3 ^ t9;
            int t11 = t5 ^ t8;
            X0 = X2 ^ t11;
            X3 = (~t5) ^ (t3 & t11);
        }

        private void Ib6(int a, int b, int c, int d)
        {
            int t1 = ~a;
            int t2 = a ^ b;
            int t3 = c ^ t2;
            int t4 = c | t1;
            int t5 = d ^ t4;
            X1 = t3 ^ t5;
            int t7 = t3 & t5;
            int t8 = t2 ^ t7;
            int t9 = b | t8;
            X3 = t5 ^ t9;
            int t11 = b | X3;
            X0 = t8 ^ t11;
            X2 = (d & t1) ^ (t3 ^ t11);
        }

        private void Sb7(int a, int b, int c, int d)
        {
            int t1 = b ^ c;
            int t2 = c & t1;
            int t3 = d ^ t2;
            int t4 = a ^ t3;
            int t5 = d | t1;
            int t6 = t4 & t5;
            X1 = b ^ t6;
            int t8 = t3 | X1;
            int t9 = a & t4;
            X3 = t1 ^ t9;
            int t11 = t4 ^ t8;
            int t12 = X3 & t11;
            X2 = t3 ^ t12;
            X0 = (~t11) ^ (X3 & X2);
        }

        private void Ib7(int a, int b, int c, int d)
        {
            int t3 = c | (a & b);
            int t4 = d & (a | b);
            X3 = t3 ^ t4;
            int t6 = ~d;
            int t7 = b ^ t4;
            int t9 = t7 | (X3 ^ t6);
            X1 = a ^ t9;
            X0 = (c ^ t7) ^ (d | X1);
            X2 = (t3 ^ X1) ^ (X0 ^ (a & X3));
        }

        private void LT()
        {
            int x0 = RotateLeft(X0, 13);
            int x2 = RotateLeft(X2, 3);
            int x1 = X1 ^ x0 ^ x2;
            int x3 = X3 ^ x2 ^ x0 << 3;

            X1 = RotateLeft(x1, 1);
            X3 = RotateLeft(x3, 7);
            X0 = RotateLeft(x0 ^ X1 ^ X3, 5);
            X2 = RotateLeft(x2 ^ X3 ^ (X1 << 7), 22);
        }

        private void InverseLT()
        {
            int x2 = RotateRight(X2, 22) ^ X3 ^ (X1 << 7);
            int x0 = RotateRight(X0, 5) ^ X1 ^ X3;
            int x3 = RotateRight(X3, 7);
            int x1 = RotateRight(X1, 1);
            X3 = x3 ^ x2 ^ x0 << 3;
            X1 = x1 ^ x0 ^ x2;
            X2 = RotateRight(x2, 3);
            X0 = RotateRight(x0, 13);
        }
    }
}
