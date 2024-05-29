using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public static class BuilderExtensions
{
    public static T With<T>(this T self, Action<T> set)
    {
        set.Invoke(self);
        return self;
    }

    public static T With<T>(this T self, Action<T> apply, Func<bool> when)
    {
        if (when())
        {
            apply(self);
        }

        return self;
    }

    public static T With<T>(this T self, Action<T> apply, bool when)
    {
        if (when)
        {
            apply(self);
        }

        return self;
    }
}
