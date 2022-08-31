using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Altux.Model.Line
{
    public class LineModel : MonoBehaviour
    {
        public List<Item> data { get; set; }
    }
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Item
    {
        public string category { get; set; }
        public List<Point> points { get; set; }
    }
}
