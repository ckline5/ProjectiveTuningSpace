using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static XenObjects;

public static class XenMath
{
    public static readonly int[] prime_numbers = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

    /// <summary>
    /// Gets the patent val for num-et
    /// </summary>
    /// <param name="num">The number of steps to primes.x</param>
    /// <returns>Patent val for all primes</returns>
    public static Val GetPatentVal(float num, PrimeBasis primes)
    {
        float b = primes.X;
        int x = (int)Math.Round(num * Math.Log(primes.X, (float)b));
        int y = (int)Math.Round(num * Math.Log(primes.Y, (float)b));
        int z = (int)Math.Round(num * Math.Log(primes.Z, (float)b));
        return new Val(x, y, z);
    }

    public static bool IsPatentVal(Val inVal, PrimeBasis basis)
    {
        Val pat = GetPatentVal(inVal.X, basis);
        if (inVal.X == pat.X && inVal.Y == pat.Y && inVal.Z == pat.Z)
            return true;
        else
            return false;
    }

    public static Tuple<int,int,int> GetOffsetFromPatentVal(Val inVal, PrimeBasis primes)
    {
        int[] offset = new int[3];
        if (IsPatentVal(inVal, primes))
        {
            offset = new int[3] { 0, 0, 0 };
        }
        else
        {
            Val pat = GetPatentVal(inVal.X, primes);
            for (int i = 0; i < 3; i++)
            {
                if (pat.AsArray[i] == inVal.AsArray[i])
                {
                    //val is patent
                    offset[i] = 0;
                }
                else if (getEtCents(pat.AsArray[i], pat.X) < getCents(primes.AsArray[i], 1))
                {
                    //patent val is flat
                    //therefore 1st offset is above patent val, 2nd is below, 3rd above, 4th below etc
                    int off = ((int)(inVal.AsArray[i] - pat.AsArray[i]));
                    if (off < 0)
                        off = off * -2;
                    else
                        off = off * 2 - 1;
                    offset[i] = off;
                }
                else
                {
                    //patent val is sharp
                    //therefore 1st offset is below patent val, 2nd is above, 3rd below, 4th above etc
                    int off = ((int)(inVal.AsArray[i] - pat.AsArray[i]));
                    if (off < 0)
                        off = off * -2 - 1;
                    else
                        off = off * 2;
                    offset[i] = off;
                }
            }
        }
        return Tuple.Create(offset[0],offset[1],offset[2]);
    }

    /// <summary>
    /// Gets weighted vals for a give set of vals
    /// </summary>
    /// <param name="vals">A set of vals</param>
    /// <returns>Weighted vals according to the primes</returns>
    public static Tuple<float,float,float> GetWeightedVals(Val vals, PrimeBasis primes)
    {
        return Tuple.Create((float)(vals.X / Math.Log(primes.X, primes.X)), 
                            (float)(vals.Y / Math.Log(primes.Y, primes.X)), 
                            (float)(vals.Z / Math.Log(primes.Z, primes.X)));
    }

    public static Monzo GetMonzosFromRatio(float n, float d, PrimeBasis primes)
    {
        if ((n % primes.X == 0 || n % primes.Y == 0 || n % primes.Z == 0)
         && (d % primes.X == 0 || d % primes.Y == 0 || d % primes.Z == 0))
        {
            //valid numbers
            int x = GetPrimeCount(n, primes.X) - GetPrimeCount(d, primes.X);
            int y = GetPrimeCount(n, primes.Y) - GetPrimeCount(d, primes.Y);
            int z = GetPrimeCount(n, primes.Z) - GetPrimeCount(d, primes.Z);
            return new Monzo(x, y, z);
        }
        else
            throw new System.FormatException($"Invalid ratio! {n}/{d} is not within the prime basis {primes.ToString()}.");
    }

    /// <summary>
    /// Prime factorization tool - tells how many times prime factor exists in num
    /// </summary>
    /// <param name="num">number</param>
    /// <param name="prime">prime factor</param>
    /// <returns>how many times the prime exists in num's prime factorization</returns>
    private static int GetPrimeCount(float num, float prime)
    {
        int count = 0;
        while (num % prime == 0)
        {
            num /= prime;
            count++;
        }
        return count;
    }

    /// <summary>
    /// Get cents for a given ratio n/d
    /// </summary>
    /// <param name="n">Numerator</param>
    /// <param name="d">Denominator</param>
    /// <returns>Cent value for n/d</returns>
    public static float getCents(float n, float d = 1)
    {
        if (n == d)
        { 
            return 0;
        }
        if (d > 0 && n > d)
        {
            return 1200 * (float)(Math.Log(n / d, 2));
        }
        else if (n > 0 && n < d)
        {
            return 1200 * (float)(Math.Log(d / n, 2)) * -1;
        }
        else
        {
            throw new DivideByZeroException($"Invalid ratio {n}/{d}");
        }
    }

    public static float getCents(Interval i)
    {
        return getCents((float)i.Numerator, (float)i.Denominator);
    }

    public static float getCents(Interval i, Val v)
    {
        return getEtCents(getSteps(v, i), v.X, i.PrimeBasis.X);
    }

    public static double getCents(System.Numerics.BigInteger n, System.Numerics.BigInteger d)
    {
        if (d > 0 && n > d)
        {
            return 1200 * (System.Numerics.BigInteger.Log(n / d) / System.Numerics.BigInteger.Log(2));
        }
        else if (n > 0 && n < d)
        {
            return getCents(d, n);
        }
        else
        {
            throw new DivideByZeroException($"Invalid ratio {n}/{d}");
        }
    }

    /// <summary>
    /// Gets cents for a number of steps in a given et
    /// </summary>
    /// <param name="s">Number of steps</param>
    /// <param name="e">Equal Temperament - Steps per divided interval</param>
    /// <param name="o">JI interval being divided</param>
    /// <returns></returns>
    public static float getEtCents(float s, float e, float o = 2)
    {
        if (e != 0)
        {
            return getCents(o) * s / e;
        }
        else
        {
            return 0;
        }
    }

    public static List<float> getScalePitches(Mapping mapping, PrimeBasis primes)
    {
        List<float> pitches = new List<float>();
        for (int i = 1; i <= mapping.vals.X; i++)
        {
            pitches.Add(getEtCents(i, mapping.vals.X, primes.X));
        }
        return pitches;
    }

    public static List<float> getScalePitches(Comma comma, PrimeBasis primes, int mosShell)
    {
        List<float> pitches = new List<float>();
        throw new NotImplementedException();
    }

    public static Tuple<Interval, Interval> GetPerGen(Tuple<float,float,float> wedgie, PrimeBasis primes)
    {
        Tuple<int, int, int> egcd = extendedGcd((int)wedgie.Item1, (int)wedgie.Item2);
        float p = egcd.Item1;
        float gx = 0f;
        float gy = egcd.Item2;
        float gz = egcd.Item3;
        if (p < 0)
        {
            p *= -1;
            gx *= -1;
            gy *= -1;
            gz *= -1;
        }
        Interval equivalenceInt = new Interval(new Monzo(1, 0, 0), primes);
        Interval period = new Interval(equivalenceInt.Monzos, primes, p);
        Interval generator = new Interval(new Monzo(gx, gy, gz), primes);
        
        while (generator.Cents >= equivalenceInt.Cents)
        {
            generator.Monzos -= equivalenceInt.Monzos;
        }
        while (generator.Cents <= 0)
        {
            generator.Monzos += equivalenceInt.Monzos;
        }
        if (equivalenceInt.Cents < (generator.Cents % equivalenceInt.Cents) * 2)
        {
            //invert generator
            generator.Monzos *= -1;
            generator.Monzos += equivalenceInt.Monzos;
        }
        return Tuple.Create(period, generator);
    }

    /// <summary>
    /// Gets Tenney-Optimal value in cents for prime p when comma n/d is tempered out
    /// </summary>
    /// <param name="n"></param>
    /// <param name="d"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static float getTenneyOptimalTuning(System.Numerics.BigInteger n, System.Numerics.BigInteger d, float pr)
    {
        System.Numerics.BigInteger p = (System.Numerics.BigInteger)pr;
        if (n % p != 0 && d % p != 0) //prime does not occur in comma
        {
            return 0;
        }
        float top = getCents((float)n, (float)d) * (float)(System.Numerics.BigInteger.Log10(p) / System.Numerics.BigInteger.Log10(n * d));
        if (n % p == 0) top *= -1;
        return top;
    }

    //for Rank-2
    public static Val getTenneyOptimalTuning(System.Numerics.BigInteger n, System.Numerics.BigInteger d, PrimeBasis primes)
    {
        float top_x = getTenneyOptimalTuning(n, d, primes.X) + getCents(primes.X, 1);
        float top_y = getTenneyOptimalTuning(n, d, primes.Y) + getCents(primes.Y, 1);
        float top_z = getTenneyOptimalTuning(n, d, primes.Z) + getCents(primes.Z, 1);
        return new Val(top_x, top_y, top_z);
    }

    //for Rank-1
    public static Val getTenneyOptimalTuning(Val mapping, PrimeBasis primes)
    {
        Tuple<float,float,float> weighted = GetWeightedVals(mapping, primes);
        float avg = (weighted.Item1 + weighted.Item2 + weighted.Item3) / 3;
        float offset = mapping.X / avg;
        Val result = new Val(mapping.X * offset, mapping.Y * offset, mapping.Z * offset);
        return result;
    }

    /// <summary>
    /// Gets tenney optimal damage in cents when comma n/d is tempered out
    /// </summary>
    /// <param name="n">Numerator of tempered comma</param>
    /// <param name="d">Denominator of tempered comma</param>
    /// <returns>Tenney-Optimal damage in cents when comma n/d is tempered out</returns>
    public static double getTenneyOptimalDamage(System.Numerics.BigInteger n, System.Numerics.BigInteger d)
    {
        double nDivD = Math.Exp(System.Numerics.BigInteger.Log(n) - System.Numerics.BigInteger.Log(d)); //same as n/d but works better
        return 1200 * (Math.Log10(nDivD) / System.Numerics.BigInteger.Log10(n * d));
    }

    /// <summary>
    /// Get number of et-steps to traverse Comma c in Mapping m
    /// </summary>
    /// <param name="m">The mapping</param>
    /// <param name="c">The comma being measured</param>
    /// <returns>Number of steps to traverse Comma c in Mapping m</returns>
    public static float getSteps(Val m, Interval c)
    {
        return getSteps(m, c.Monzos);
    }

    public static float getSteps(Val v, Monzo m)
    {
        return v.X * m.X + v.Y * m.Y + v.Z * m.Z;
    }

    public static int gcd(int a, int b)
    {
        while (b != 0)
        {
            int r = a % b;
            a = b;
            b = r;
            //Debug.Log(a);
        }
        //Debug.Log(a + " " + b);
        return a;
    }

    // Recursive function to demonstrate the extended Euclidean algorithm.
    // It returns multiple values using Tuple in C++.
    public static Tuple<int, int, int> extendedGcd(int a, int b)
    {
        if (a == 0)
        {
            return new Tuple<int, int, int>(b, 0, 1);
        }

        int g, x, y;

        // unpack Tuple returned by function into variables
        Tuple<int, int, int> t = extendedGcd(b % a, a);
        g = t.Item1;
        x = t.Item2;
        y = t.Item3;

        return new Tuple<int, int, int>(g, (y - (b / a) * x), x);
    }

    public static Tuple<float,float,float> AntiNullSpaceBasis(Tuple<float,float,float> v1, Tuple<float, float, float> v2)
    {
        List<float> result = new List<float>(3) { 0, 0, 0 };
        int tries = 0;
        float a = v1.Item1;
        float b = v1.Item2;
        float c = v1.Item3;
        float d = v2.Item1;
        float e = v2.Item2;
        float f = v2.Item3;
        do
        {
            tries++;
            float bd_ae = b * d - a * e;
            float cd_af = c * d - a * f;
            result = new List<float>(3) { -f * bd_ae + e * cd_af, -d * cd_af, d * bd_ae };
            if (result.All(x => x == 0))
            {
                switch (tries)
                {
                    case 1:
                        a = v1.Item2;
                        b = v1.Item3;
                        c = v1.Item1;
                        d = v2.Item2;
                        e = v2.Item3;
                        f = v2.Item1;
                        break;
                    case 2:
                        a = v1.Item3;
                        b = v1.Item1;
                        c = v1.Item2;
                        d = v2.Item3;
                        e = v2.Item1;
                        f = v2.Item2;
                        break;
                    default:
                        break;
                }
            }
        } while (result.All(x => x == 0) && tries < 3);
        float g = gcd(gcd((int)result[0], (int)result[1]), (int)result[2]);
        return Tuple.Create(result[0]/g, result[1]/g, result[2]/g);
    }

    public static int GetNumberOfPrime(int p)
    {
        return System.Array.IndexOf(prime_numbers, p);
    }

    public static float GetComplexity(Monzo monzos, PrimeBasis primes)
    {
        //is this the correct method?
        return (float) Math.Log10(Math.Pow(primes.X, Math.Abs(monzos.X)) * Math.Pow(primes.Y, Math.Abs(monzos.Y)) * Math.Pow(primes.Z, Math.Abs(monzos.Z)));
    }

    public static double GetComplexity(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
    {
        //is this the correct method?
        return System.Numerics.BigInteger.Log10(numerator * denominator);
    }

}
