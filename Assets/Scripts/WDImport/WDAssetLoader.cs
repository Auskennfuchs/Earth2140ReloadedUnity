using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using WDViewer;
using WDViewer.Assets;
using WDViewer.Reader;

public class WDAssetLoader : MonoBehaviour
{
    [SerializeField]
    private AssetManagerSO assetManagerAnchor;

    [SerializeField]
    private String gameAssetPath = "";
    // Start is called before the first frame update
    public void Start()
    {
        var reader = new WdFileReader();
        var pathInfo = new DirectoryInfo(gameAssetPath/* @"E:\Games\Earth 2140" */);
        var assets = pathInfo.GetFiles()
            .Where(f => ".wd".Equals(f.Extension.ToLower()))
            .Select(file => reader.Read(file.FullName))
                    .Aggregate(new Dictionary<string, Asset>(), (res, a) =>
                    {
                        AddRange(res, a);
                        return res;
                    });

        assetManagerAnchor.Item = new AssetDictionary(assets);
        StartCoroutine(LoadAsynchronously("Playground"));
    }

    IEnumerator LoadAsynchronously(string level)
    {
        var operation = SceneManager.LoadSceneAsync(level);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    private void AddRange<T>(ICollection<T> target, IEnumerable<T> source)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        foreach (var element in source)
            target.Add(element);
    }

}
