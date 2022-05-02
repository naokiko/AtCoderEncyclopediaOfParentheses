using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class Resolver
{
    protected Dictionary<string, bool> bases;

    public Resolver()
    {
        bases = makeBases();
    }

    public List<string> Run()
    {
        var src = bases;
        int count = 0;

        for (; ; )
        {
            var dst = Combinations(src);
            src = dst;

            if (src.Count == count) break;
            count = src.Count;
        }

        var list = src.Where(x => x.Key.Length == 20).OrderBy(selector => { return selector.Key; }).Select(k => k.Key).ToList();

        return list;
    }

    protected Dictionary<string, bool> Combinations(Dictionary<string, bool> src)
    {
        var dst = new Dictionary<string, bool>();
        foreach (var b in src)
        {
            dst.TryAdd(b.Key, true);

            var r = "(" + b.Key + ")";
            if (r.Length < 21)
            {
                dst.TryAdd(r, true);
            }

            var base_append = new List<string>();
            foreach (var a in src)
            {
                var r2 = b.Key + a.Key;
                if (r2.Length < 21)
                {
                    dst.TryAdd(r2, true);
         
                    var r3 = "(" + r2 + ")";
                    if (r3.Length < 21)
                    {
                        dst.TryAdd(r3, true);
                    }
                }
            }
        }
        foreach (var s in dst)
        {
            src.TryAdd(s.Key, true);
        }
        return src;
    }

    public Dictionary<string, bool> makeBases()
    {
        var bases = new Dictionary<string, bool>();
        var s = "";
        for (int i = 0; i < 10; i++)
        {
            s = "(" + s + ")";
            bases.Add(s, true);
        }

        return bases;
    }

}

public class AtCoderEncyclopediaOfParentheses
{
    public static void Main()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        var resolver = new Resolver();
        var list = resolver.Run();

        sw.Stop();

        Debug.WriteLine($"{sw.Elapsed}");
        Debug.WriteLine(list.Count());
        list.ForEach(item => {
            Debug.WriteLine(item);
        });
    }
}
