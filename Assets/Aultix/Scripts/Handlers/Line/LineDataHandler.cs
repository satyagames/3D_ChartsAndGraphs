using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using Altux.Model.Line;
using UnityEngine.Networking;
using Altux.Util;
using Newtonsoft.Json;

public class LineDataHandler : MonoBehaviour
{
    GraphChartBase lineGraph;
    public Material[] graphMaterials;
    public BoxPathGenerator LinePreafab;
    public int DepthOffset = 2;

    void Start()
    {
        lineGraph = GetComponent<GraphChartBase>();
        StartCoroutine(GetLineGraphData());
    }

    IEnumerator GetLineGraphData()
    {
        UnityWebRequest request = UnityWebRequest.Get(Constants.URL.LINE_DATA_URL);
        yield return request.SendWebRequest();

        string response = request.downloadHandler.text;
        LineModel root = JsonConvert.DeserializeObject<LineModel>(response);

        CreateLineGraph(root);
    }

    void CreateLineGraph(LineModel root)
    {
        MaterialTiling tiling = new MaterialTiling()
        {
            EnableTiling = true,
            TileFactor = 10
        };
        int mod = 0;
        for (int i = 0; i < root.data.Count; i++)
        {
            if (!lineGraph.DataSource.HasCategory(root.data[i].category))
            {
                if (mod >= graphMaterials.Length)
                    mod = 0;
                int depth = DepthOffset * i;
                lineGraph.DataSource.AddCategory3DGraph(root.data[i].category, LinePreafab, graphMaterials[mod], 0.15f, tiling, null, graphMaterials[mod], false, null, null, 0.75f, depth, false, 10);
                mod++;
            }
            for (int j = 0; j < root.data[i].points.Count; j++)
            {
                lineGraph.DataSource.AddPointToCategory(root.data[i].category, root.data[i].points[j].x, root.data[i].points[j].y);
            }
        }
    }
}
