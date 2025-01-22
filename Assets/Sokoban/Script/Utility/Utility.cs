using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utility
{
    public static Collider2D OverlapPoint(Vector2 point, string tag)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(point);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == tag)
            {
                return colliders[i];
            }
        }

        return null;
    }

    public static Collider2D[] OverlapPointAll(Vector2 point, string tag)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(point);

        List<Collider2D> result = new List<Collider2D>();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == tag)
            {
                result.Add(colliders[i]);
            }
        }

        return result.ToArray();
    }
}
