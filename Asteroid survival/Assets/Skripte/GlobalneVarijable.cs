using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalneVarijable
{
    private static int bodovi, vrijeme, asteroidi;

    public static int Bodovi
    {
        get { return bodovi; }
        set { bodovi = value; }
    }

    public static int Vrijeme
    {
        get { return vrijeme; }
        set { vrijeme = value; }
    }

    public static int Asteroidi
    {
        get { return asteroidi; }
        set { asteroidi = value; }
    }
}
