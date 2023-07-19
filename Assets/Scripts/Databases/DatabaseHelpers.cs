using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class DatabaseHelpers<TOut> where TOut : class
{
    public static List<TOut> GetAssets(string path)
    {
        var objects = Resources.LoadAll(path,typeof(TOut)).ToList();
        var assets = objects.Select(o => o as TOut).ToList();
        return assets;
    }
}