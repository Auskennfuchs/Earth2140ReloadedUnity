using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WDViewer.Assets
{
    public class AssetImage : Asset
    {
        public Texture2D Image { get; set; }
        public UInt16 Width { get; set; }
        public UInt16 Height { get; set; }
        public AssetPalette Palette { get; set; }
    }
}
