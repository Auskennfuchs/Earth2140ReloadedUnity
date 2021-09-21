using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WDViewer.Assets;

namespace Assets.Scripts.ScriptableObjects
{
    public class LevelAnchor
    {
        private readonly AssetLevel level;

        public AssetLevel Level { get => Level; }

        public LevelAnchor(AssetLevel level)
        {
            this.level = level;
        }
    }

    [CreateAssetMenu(menuName = "Game/Level Manager")]
    public class LevelManagerSO : RuntimeAnchorBaseSO<LevelAnchor>
    {
    }
}
