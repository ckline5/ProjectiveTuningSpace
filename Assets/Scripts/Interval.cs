using System;
using System.Collections;
using System.Collections.Generic;
using static XenMath;
using static XenObjects;

public class Interval
{
    //interval
    private PrimeBasis _primeBasis = new PrimeBasis(0,0,0);
    public PrimeBasis PrimeBasis
    {
        get { return _primeBasis; }
        set
        {
            if (value == _primeBasis) return;
            _primeBasis = value;
        }
    }

    private Monzo _monzos = new Monzo(0, 0, 0);
    public Monzo Monzos
    {
        get { return _monzos; }
        set
        {
            if (value == _monzos) return;
            _monzos = value;
        }
    }

    private float _divisions = 1;
    public float Divisions
    {
        get { return _divisions; }
        set
        {
            if (value == _divisions) return;
            _divisions = value;
        }
    }

    public float Cents
    {
        get { return (float)getCents((float)Numerator, (float)Denominator) / _divisions; }
    }

    public double Numerator
    {
        get
        {
            double n =  1 * (Monzos.X > 0 ? Math.Pow(_primeBasis.X, Monzos.X) : 1)
                          * (Monzos.Y > 0 ? Math.Pow(_primeBasis.Y, Monzos.Y) : 1)
                          * (Monzos.Z > 0 ? Math.Pow(_primeBasis.Z, Monzos.Z) : 1);
            //n = Math.Pow(n, 1 / _divisions);
            return n;
        }
    }
    public double Denominator
    {
        get
        {
            double d = 1 * (Monzos.X < 0 ? Math.Pow(_primeBasis.X, -Monzos.X) : 1)
                         * (Monzos.Y < 0 ? Math.Pow(_primeBasis.Y, -Monzos.Y) : 1)
                         * (Monzos.Z < 0 ? Math.Pow(_primeBasis.Z, -Monzos.Z) : 1);
            //d = Math.Pow(d, 1 / _divisions);
            return d;
        }
    }
    public System.Numerics.BigInteger NumeratorBI
    {
        get
        {
            return 1 * (Monzos.X > 0 ? System.Numerics.BigInteger.Pow((System.Numerics.BigInteger)_primeBasis.X, (int)Monzos.X) : 1)
                     * (Monzos.Y > 0 ? System.Numerics.BigInteger.Pow((System.Numerics.BigInteger)_primeBasis.Y, (int)Monzos.Y) : 1)
                     * (Monzos.Z > 0 ? System.Numerics.BigInteger.Pow((System.Numerics.BigInteger)_primeBasis.Z, (int)Monzos.Z) : 1);
        }
    }
    public System.Numerics.BigInteger DenominatorBI
    {
        get
        {
            return 1 * (Monzos.X < 0 ? System.Numerics.BigInteger.Pow((System.Numerics.BigInteger)_primeBasis.X, -(int)Monzos.X) : 1)
                     * (Monzos.Y < 0 ? System.Numerics.BigInteger.Pow((System.Numerics.BigInteger)_primeBasis.Y, -(int)Monzos.Y) : 1)
                     * (Monzos.Z < 0 ? System.Numerics.BigInteger.Pow((System.Numerics.BigInteger)_primeBasis.Z, -(int)Monzos.Z) : 1);
        }
    }

    public Interval(float a, float b, float c, float x, float y, float z, float div = 1)
    {
        _primeBasis = new PrimeBasis(a, b, c);
        _monzos = new Monzo(x, y, z);
        _divisions = div;
    }
    public Interval(Monzo m, PrimeBasis b, float div = 1)
    {
        _primeBasis = b;
        _monzos = m;
        _divisions = div;
    }
}
