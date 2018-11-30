using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using CryptoDocs.DigitalSignature.Cli.EllipticCurve;

namespace CryptoDocs.DigitalSignature.Cli
{
    class CurveUtils
    {
        public static (int a, int b)[] generate_params(int p)
        {
            return Enumerable.Range(1, p)
                .SelectMany(x => Enumerable.Range(1, p).Select(y => (a: x, b: y)))
                .Where(x => 4 * x.a * x.a * x.a + 27 * x.b * x.b != 0)
                .ToArray();
        }

        public static (int x, int y)[] generate_points(int a, int b, int p)
        {
            return Enumerable.Range(1, p - 1)
                .Select(x => (x: x, square: (x * x * x + a * x + b) % p))
                .Select(x => (x.x, (int)Math.Sqrt(x.square)))
                .ToArray();
        }

        //=====================================================================================================
        //5
        public static int inv(BigInteger x, int p)
        {
            return (int) x.ModInverse(p);
        }

        //public static object neg(object point)
        //{
        //    if (point == null)
        //    {
        //        return true;
        //    }
        //    var _tup_1 = point;
        //    var x = _tup_1.Item1;
        //    var y = _tup_1.Item2;
        //    var result = Tuple.Create(x, -y % p);
        //    return result;
        //}

        public static (int x, int y) summ((int x, int y) point1, (int x, int y) point2, int a, int b, int p)
        {
            if (point1.x == 0 && point1.y == 0)
            {
                return point1;
            }

            if (point2.x == 0 && point2.y == 0)
            {
                return point2;
            }

            int l;

            var (x1, y1) = point1;
            var (x2, y2) = point2;

            if (x1 == x2 && y1 != y2)
            {
                return (0 , 0);
            }

            if (x1 == x2 && y1 == y2)
            {
                l = (3 * x1 * x1 + a) * inv(2 * y1, p);
            }
            else
            {
                l = (y1 - y2) * inv(x1 - x2, p);
            }
            var x3 = l * l - x1 - x2;
            var y3 = y1 + l * (x3 - x1);

            return (x3 % p, -y3 % p);
        }

        public static (int x, int y) mul(int k, (int x, int y) point, int a, int b, int p)
        {
            var res = point;
            foreach (var i in Enumerable.Range(0, k))
            {
                res = summ(res, point, a, b, p);
            }
            return res;
        }

        public static bool is_prime(int n)
        {
            foreach (var i in Enumerable.Range(2, Convert.ToInt32(Math.Sqrt(n)) + 1))
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static ((int x, int y),int k) choose_G((int x, int y)[] points, int a, int b, int p)
        {
            var maxk = 10;
            foreach (var pt in points)
            {
                var k = 1;
                var s = pt;
                while (s.x != 0 && s.y != 0)
                {
                    s = summ(s, pt, a, b, p);
                    k += 1;
                }
                if (k > maxk && is_prime(k))
                {
                    return (pt, k);
                }
            }

            throw new InvalidOperationException();
        }

        public static Random Random = new Random();


        public static ((int x, int y) K_A, int n_A, (int x, int y)? p_A, int n_B, (int x, int y)? p_B) interchange_keys((int x, int y) G, int a, int b, int p)
        {

            var p_A = null as (int x, int y)?;
            var n_A = 0;
            while (p_A == null)
            {
                n_A = Random.Next(p - 1) + 1;
                p_A = mul(n_A, G, a, b, p);
            }

            var p_B = null as (int x, int y)?;
            var n_B = 0;
            while (p_B == null)
            {
                n_B = Random.Next(p - 1) + 1;
                p_B = mul(n_B, G, a, b, p);
            }

            var K_A = mul(n_A, p_B.Value, a, b, p);
            var K_B = mul(n_B, p_A.Value, a, b, p);
            Debug.Assert(K_A == K_B);
            return (K_A, n_A, p_A, n_B, p_B);
        }

        //=====================================================================================================
        // 6
        public static BigInteger h(byte[] m)
        {
            return new BigInteger(new Sha1Hash().Append(m).GetDigest(), true);

        }

        public static (int r, int s) sign(byte[] m, (int,int) G, int a, int b, int p, int c, int n_A)
        {
            var r = 0;
            var s = BigInteger.Zero;
            var k = 0;
            while (s == 0)
            {
                while (r == 0)
                {
                    k = Random.Next(c - 3) + 2;
                    var kG = mul(k, G, a, b, p);
                    var (x1, y1) = kG;
                    r = x1 % c;
                }
                s = inv(k, c) * (h(m) + n_A * r) % c;
            }

            return (r, (int) s);
        }

        public static object validate_sign(int r, int s, byte[] m, (int, int) G, int a, int b, int p, int c, (int, int) p_A)
        {
            if (!(r >= 1 && r <= c - 1 && s >= 1 && s <= c - 1))
            {
                return false;
            }
            var w = inv(s, c);
            var u1 = (int)(h(m) * w % c);
            var u2 = r * w % c;
            var pt_ = summ(mul(u1, G, a, b, p), mul(u2, p_A, a, b, p), a, b, p);
            var (x1_, y1_) = pt_;
            var r_ = x1_ % c;
            return r_ == r;
        }
    }
}
