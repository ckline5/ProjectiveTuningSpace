using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using static XenObjects;
using static XenMath;

public class NamedTemperaments : MonoBehaviour
{
    public TextAsset file;

    public class Temperament
    {
        public string name;
        public PrimeBasis primes;
        public Monzo monzos;
    }
    static List<Temperament> temperaments;

    // Start is called before the first frame update
    void Start()
    {
        temperaments = new List<Temperament>();
        ReadTemperamentsFile();
    }

    void ReadTemperamentsFile()
    {
        string text = file.text;
        foreach (string line in Regex.Split(text, "\n|\r|\r\n"))
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                Temperament t = new Temperament();
                string[] l = line.Split(',');
                t.name = l[0];
                t.primes = new PrimeBasis(StringToFloat(l[1]), StringToFloat(l[2]), StringToFloat(l[3]));
                t.monzos = new Monzo(StringToFloat(l[4]), StringToFloat(l[5]), StringToFloat(l[6]));
                temperaments.Add(t);
            }
        }
    }

    public float StringToFloat(string s)
    {
        float num;
        if (float.TryParse(s, out num))
            return num;
        string[] nums = s.Split('/');
        float num1, num2;
        if (float.TryParse(nums[0], out num1) && float.TryParse(nums[1], out num2))
            return num1 / num2;
        return 0;
    }

    public static string WhatTemperament(PrimeBasis primes, Monzo monzos)
    {
        if (temperaments.Count > 0)
        {
            Temperament t = temperaments.Where(x => x.primes == primes && (x.monzos == monzos || x.monzos == monzos * -1)).FirstOrDefault();
            if (t == null)
                return null;
            else
                return t.name;
        }
        else
            throw new FileNotFoundException("No temperaments read from temperaments.csv!");
    }

    public static Monzo WhatMonzos(PrimeBasis primes, string name)
    {
        if (string.Equals(name, "wurschmidt", System.StringComparison.InvariantCultureIgnoreCase))
        {
            //handle umlaut in Würschmidt
            name = "Würschmidt";
        }

        Temperament t = temperaments.Where(x => string.Equals(x.name, name, System.StringComparison.InvariantCultureIgnoreCase) && x.primes == primes).FirstOrDefault();
        if (t == null)
            return null;
        else
            return t.monzos;
    }
}
