using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class TexturesCache
{
    private static Dictionary<string, Texture2D> cache = new Dictionary<string, Texture2D>();

    public static bool Contains(string url)
    {
        return cache.ContainsKey(url);
    }

    public static Texture2D Get(string url)
    {
        return cache.ContainsKey(url) ? cache[url] : null;
    }

    public static void Add(string url, Texture2D texture)
    {
        if (!cache.ContainsKey(url))
        {
            cache[url] = texture;
        }
    }
}
