using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BondMath
{
    public static float Approach(float a, float b, float factor, float dt)
    {
        return Mathf.Lerp(a, b, 1 - Mathf.Pow(factor, dt));
    }
}
