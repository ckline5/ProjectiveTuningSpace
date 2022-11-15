using System;
using System.Collections;
using System.Collections.Generic;

public abstract class XenObjects
{
    public class PrimeBasis
    {
        private Tuple<float, float, float> _primes = Tuple.Create(0f, 0f, 0f);
        public Tuple<float, float, float> Primes
        {
            get { return _primes; }
            set
            {
                if (value == _primes) { return; }
                _primes = value;
            }
        }
        public float X { get { return _primes.Item1; } }
        public float Y { get { return _primes.Item2; } }
        public float Z { get { return _primes.Item3; } }

        public float[] AsArray { get { return new float[] { X, Y, Z }; } }

        public PrimeBasis(float x, float y, float z)
        {
            _primes = Tuple.Create(x, y, z);
        }
        public PrimeBasis(Tuple<float, float, float> v)
        {
            _primes = Tuple.Create(v.Item1, v.Item2, v.Item3);
        }

        public static PrimeBasis operator +(PrimeBasis a, PrimeBasis b) { return new PrimeBasis(a.X + b.X, a.Y + b.Y, a.Z + b.Z); }
        public static PrimeBasis operator -(PrimeBasis a, PrimeBasis b) { return new PrimeBasis(a.X - b.X, a.Y - b.Y, a.Z - b.Z); }
        public static PrimeBasis operator *(PrimeBasis a, PrimeBasis b) { return new PrimeBasis(a.X * b.X, a.Y * b.Y, a.Z * b.Z); }
        public static PrimeBasis operator /(PrimeBasis a, PrimeBasis b) { return new PrimeBasis(a.X / b.X, a.Y / b.Y, a.Z / b.Z); }
        public static PrimeBasis operator +(PrimeBasis a, float b) { return new PrimeBasis(a.X + b, a.Y + b, a.Z + b); }
        public static PrimeBasis operator -(PrimeBasis a, float b) { return new PrimeBasis(a.X - b, a.Y - b, a.Z - b); }
        public static PrimeBasis operator *(PrimeBasis a, float b) { return new PrimeBasis(a.X * b, a.Y * b, a.Z * b); }
        public static PrimeBasis operator /(PrimeBasis a, float b) { return new PrimeBasis(a.X / b, a.Y / b, a.Z / b); }
        public static bool operator ==(PrimeBasis a, PrimeBasis b) { return (a.X == b.X && a.Y == b.Y && a.Z == b.Z); }
        public static bool operator !=(PrimeBasis a, PrimeBasis b) { return (a.X != b.X || a.Y != b.Y || a.Z != b.Z); }
        public static bool operator ==(PrimeBasis a, Tuple<float, float, float> b) { return (a.X == b.Item1 && a.Y == b.Item2 && a.Z == b.Item3); }
        public static bool operator !=(PrimeBasis a, Tuple<float, float, float> b) { return (a.X != b.Item1 || a.Y != b.Item2 || a.Z != b.Item3); }
        public static PrimeBasis Zero { get { return new PrimeBasis(0, 0, 0); } }

        public override string ToString()
        {
            return $"{X}.{Y}.{Z}";
        }
    }


    public class Val
    {
        private Tuple<float, float, float> _vals = Tuple.Create(0f, 0f, 0f);
        public Tuple<float, float, float> Vals
        {
            get { return _vals; }
            set
            {
                if (value == _vals) { return; }
                _vals = value;
            }
        }
        public float X { get { return _vals.Item1; } }
        public float Y { get { return _vals.Item2; } }
        public float Z { get { return _vals.Item3; } }

        public float[] AsArray { get { return new float[] { X, Y, Z }; } }

        public Val(float x, float y, float z)
        {
            _vals = Tuple.Create(x, y, z);
        }
        public Val(Tuple<float,float,float> v)
        {
            _vals = Tuple.Create(v.Item1, v.Item2, v.Item3);
        }

        public static Val operator +(Val a, Val b) { return new Val(a.X + b.X, a.Y + b.Y, a.Z + b.Z); }
        public static Val operator -(Val a, Val b) { return new Val(a.X - b.X, a.Y - b.Y, a.Z - b.Z); }
        public static Val operator *(Val a, Val b) { return new Val(a.X * b.X, a.Y * b.Y, a.Z * b.Z); }
        public static Val operator /(Val a, Val b) { return new Val(a.X / b.X, a.Y / b.Y, a.Z / b.Z); }
        public static Val operator +(Val a, float b) { return new Val(a.X + b, a.Y + b, a.Z + b); }
        public static Val operator -(Val a, float b) { return new Val(a.X - b, a.Y - b, a.Z - b); }
        public static Val operator *(Val a, float b) { return new Val(a.X * b, a.Y * b, a.Z * b); }
        public static Val operator /(Val a, float b) { return new Val(a.X / b, a.Y / b, a.Z / b); }
        public static bool operator ==(Val a, Val b) { return (a.X == b.X && a.Y == b.Y && a.Z == b.Z); }
        public static bool operator !=(Val a, Val b) { return (a.X != b.X || a.Y != b.Y || a.Z != b.Z); }
        public static bool operator ==(Val a, Tuple<float, float, float> b) { return (a.X == b.Item1 && a.Y == b.Item2 && a.Z == b.Item3); }
        public static bool operator !=(Val a, Tuple<float, float, float> b) { return (a.X != b.Item1 || a.Y != b.Item2 || a.Z != b.Item3); }
        public static Val Zero { get { return new Val(0, 0, 0); } }

        public override string ToString()
        {
            return $"< {X} {Y} {Z} ]";
        }

        public static float operator *(Val v, Interval i)
        {
            return XenMath.getSteps(v, i);
        }
        public static float operator *(Val v, Monzo m)
        {
            return XenMath.getSteps(v, m);
        }
    }


    public class Monzo
    {
        private Tuple<float, float, float> _monzos = Tuple.Create(0f,0f,0f);
        public Tuple<float, float, float> Monzos
        {
            get { return _monzos; }
            set
            {
                if (value == _monzos) { return; }
                _monzos = value;
            }
        }
        public float X { get { return _monzos.Item1; } }
        public float Y { get { return _monzos.Item2; } }
        public float Z { get { return _monzos.Item3; } }

        public float[] AsArray { get { return new float[] { X, Y, Z }; } }

        public Monzo(float x, float y, float z)
        {
            _monzos = Tuple.Create(x, y, z);
        }
        public Monzo(Tuple<float, float, float> v)
        {
            _monzos = Tuple.Create(v.Item1, v.Item2, v.Item3);
        }

        public static Monzo operator +(Monzo a, Monzo b) { return new Monzo(a.X + b.X, a.Y + b.Y, a.Z + b.Z); }
        public static Monzo operator -(Monzo a, Monzo b) { return new Monzo(a.X - b.X, a.Y - b.Y, a.Z - b.Z); }
        public static Monzo operator *(Monzo a, Monzo b) { return new Monzo(a.X * b.X, a.Y * b.Y, a.Z * b.Z); }
        public static Monzo operator /(Monzo a, Monzo b) { return new Monzo(a.X / b.X, a.Y / b.Y, a.Z / b.Z); }
        public static Monzo operator +(Monzo a, float b) { return new Monzo(a.X + b, a.Y + b, a.Z + b); }
        public static Monzo operator -(Monzo a, float b) { return new Monzo(a.X - b, a.Y - b, a.Z - b); }
        public static Monzo operator *(Monzo a, float b) { return new Monzo(a.X * b, a.Y * b, a.Z * b); }
        public static Monzo operator /(Monzo a, float b) { return new Monzo(a.X / b, a.Y / b, a.Z / b); }
        public static bool operator ==(Monzo a, Monzo b) { return (a.X == b.X && a.Y == b.Y && a.Z == b.Z); }
        public static bool operator !=(Monzo a, Monzo b) { return (a.X != b.X || a.Y != b.Y || a.Z != b.Z); }
        public static bool operator ==(Monzo a, Tuple<float, float, float> b) { return (a.X == b.Item1 && a.Y == b.Item2 && a.Z == b.Item3); }
        public static bool operator !=(Monzo a, Tuple<float, float, float> b) { return (a.X != b.Item1 || a.Y != b.Item2 || a.Z != b.Item3); }
        public static Monzo Zero { get { return new Monzo(0, 0, 0); } }

        public override string ToString()
        {
            return $"[ {X} {Y} {Z} >";
        }

        public static float operator *(Monzo m, Val v)
        {
            return XenMath.getSteps(v, m);
        }
    }
}
