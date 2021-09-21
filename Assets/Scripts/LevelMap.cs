using Assets.Scripts.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WDViewer.Assets;

public class LevelMap : MonoBehaviour
{
    [SerializeField]
    private AssetManagerSO assetManagerAnchor;

    [SerializeField]
    private LevelManagerSO levelManagerAnchor;

    public static readonly int TILE_SIZE = 64;

    public int Width { get; private set; }

    public int Height { get; private set; }

    [SerializeField]
    private string levelName;

    private void Awake()
    {
        // Start is called before the first frame update
        var level = assetManagerAnchor.Get<AssetLevel>(levelName);
        var tileSet = assetManagerAnchor.Get<AssetMix>(level.TileSetPath);
        gameObject.transform.localScale = new Vector3(level.Width, level.Height, 1.0f);
        this.gameObject.GetComponent<MeshRenderer>().materials[0].mainTexture = CreateMap(level, tileSet);

        Width = (int)level.Width;
        Height = (int)level.Height;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Texture2D CreateMap(AssetLevel level, AssetMix tileSet)
    {
        var width = (int)level.Width;
        var height = (int)level.Height;
        var targetWidth = width * 64;
        var targetHeight = height * 64;
        var renderTexture = RenderTexture.GetTemporary(targetWidth, targetHeight);
        var curActive = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(false, true, Color.magenta);
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, targetWidth, targetHeight, 0);
        for (var y = 0; y < level.Height; ++y)
        {
            for (var x = 0; x < level.Width; ++x)
            {
                var tile = level.Tiles[x, y];
                var tileSprite = tileSet.Content[tile.tileIndex] as AssetImage;
                Graphics.DrawTexture(new Rect(x * 64, (y + 1) * 64, 64, -64), tileSprite.Image);
            }
        }
        GL.End();
        GL.PopMatrix();
        var newTexture = new Texture2D(targetWidth, targetHeight, TextureFormat.RGBA32, false);
        newTexture.ReadPixels(new Rect(0, 0, targetWidth, targetHeight), 0, 0);
        RenderTexture.active = curActive;
        RenderTexture.ReleaseTemporary(renderTexture);
        newTexture.Apply();
        return newTexture;
    }
}
