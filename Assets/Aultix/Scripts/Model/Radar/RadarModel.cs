using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Altux.Model.Radar
{
    public class RadarModel : MonoBehaviour
    {

        public List<Item> data { get; set; }
    }
    public class Item
    {
        public string Key { get; set; }
        public int group { get; set; }
        public int Value { get; set; }
    }
}
