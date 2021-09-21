using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WDViewer.Assets;

namespace Assets.Scripts.ScriptableObjects
{
    public class AssetDictionary
    {
        private readonly Dictionary<string, Asset> assets;

        public Dictionary<string, Asset> Assets
        {
            get => assets;
        }

        public AssetDictionary(Dictionary<string, Asset> assets)
        {
            this.assets = assets;
        }
    }

    [CreateAssetMenu(menuName = "Game/Asset Manager")]
    public class AssetManagerSO : RuntimeAnchorBaseSO<AssetDictionary>
    {
        public T Get<T>(string name) where T : Asset
        {
            if (!IsSet)
            {
                Debug.Log($"assets accessed before loaded: {name}");
                return null;
            }
            if (!Item.Assets.ContainsKey(name))
            {
                Debug.Log($"asset not found {name}");
                return null;
            }
            return (T)Item.Assets[name];
        }

    }
}
