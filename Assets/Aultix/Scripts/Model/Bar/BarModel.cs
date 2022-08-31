using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Altux.Model.Bar
{
    public class BarModel
    {
        public List<Items> data { get; set; }
    }
    public class Items
    {
        public string x { get; set; }
        public string z { get; set; }
        public float y { get; set; }
    }
}



