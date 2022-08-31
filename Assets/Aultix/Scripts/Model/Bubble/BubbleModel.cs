using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Altux.Model.Bubble
{
    public class BubbleModel
    {
        public List<Items> data { get; set; }
    }

    public class Items
    {
        public string category { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int size { get; set; }
    }
}
