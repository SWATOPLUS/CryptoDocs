using System;
using System.Numerics;

namespace CryptoDocs.DigitalSignature.Cli.EllipticCurve
{
    public struct EcPoint
    {
        public static EcPoint DefaultInstance { get; } = new EcPoint
        {
            A = BigInteger.Parse("-3"),
            B = BigInteger.Parse("2455155546008943817740293915197451784769108058161191238065"),
            M = BigInteger.Parse("6277101735386680763835789423207666416083908700390324961279"),
            X = BigInteger.Parse("602046282375688656758213480587526111916698976636884684818"),
            Y = BigInteger.Parse("174050332293622031404857552280219410364023488927386650641")
        };

        public static EcPoint Parse(string s)
        {
            var num = s.Split(" ");

            return new EcPoint
            {
                A = DefaultInstance.A,
                B = DefaultInstance.B,
                M = DefaultInstance.M,
                X = BigInteger.Parse(num[0]),
                Y = BigInteger.Parse(num[1])
            };
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }

        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }
        public BigInteger A { get; set; }
        public BigInteger B { get; set; }
        public BigInteger M { get; set; }


        public bool IsSameCurve(EcPoint other)
        {
            return A == other.A && B == other.B && M == other.M;
        }

        public static EcPoint operator +(EcPoint p1, EcPoint p2)
        {
            if (!p1.IsSameCurve(p2))
            {
                throw new InvalidOperationException();
            }

            var mod = p1.M;
            var (dx, dy) = GetDeltas(p1, p2);
            var lambda = (dy * dx.ModInverse(mod)).PositiveMod(mod);

            var p3 = p1;
            p3.X = (lambda * lambda - p1.X - p2.X).PositiveMod(mod);
            p3.Y = (lambda * (p1.X - p3.X) - p1.Y).PositiveMod(mod);

            return p3;
        }

        private static (BigInteger, BigInteger) GetDeltas(EcPoint p1, EcPoint p2)
        {
            if (p1 == p2)
            {
                var dy = 3 * p1.X * p1.X + p1.A;
                var dx = 2 * p1.Y;

                return (dx, dy);
            }
            else
            {
                var mod = p1.M;
                var dy = (p2.Y - p1.Y).PositiveMod(mod);
                var dx = (p2.X - p1.X).PositiveMod(mod);

                return (dx, dy);
            }
        }

        public static bool operator ==(EcPoint p1, EcPoint p2)
        {
            return p1.IsSameCurve(p2) && p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(EcPoint p1, EcPoint p2)
        {
            return !(p1 == p2);
        }

        private static EcPoint Double(EcPoint p)
        {
            return p + p;
        }

        public EcPoint Multiply(BigInteger x)
        {
            var p = this;
            var result = this;

            x = x - 1;

            while (x != 0)
            {
                if (x % 2 != 0)
                {
                    if (result.X == p.X || result.Y == p.Y)
                    {
                        result = Double(result);
                    }
                    else
                    {
                        result = result + p;
                    }

                    x = x - 1;
                }

                x = x / 2;
                p = Double(p);
            }

            return result;
        }
    }
}