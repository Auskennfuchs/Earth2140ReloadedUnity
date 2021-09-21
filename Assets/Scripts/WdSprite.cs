using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WDViewer.Assets;

namespace Assets.Scripts
{
    class WdSprite : MonoBehaviour
    {
        [SerializeField]
        private AssetManagerSO assetManagerAnchor;

        [SerializeField]
        private string mixFile;

        [SerializeField]
        private int index = -1;

        public void Awake()
        {

        }

        public void Start()
        {
            var level = this.GetComponentInParent<LevelMap>();
            var asset = assetManagerAnchor.Get<AssetMix>(mixFile);
            var sprite = ((AssetImage)asset.Content[index]).Image;
            this.gameObject.transform.localScale = new Vector3(sprite.width / (float)LevelMap.TILE_SIZE / level.Width, -sprite.height / (float)LevelMap.TILE_SIZE / level.Height, 1.0f);
            this.gameObject.GetComponent<MeshRenderer>().materials[0].mainTexture = sprite;
        }

        public void Update()
        {

        }
    }
}
