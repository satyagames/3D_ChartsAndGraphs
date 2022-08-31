using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using Altux.Model.Radar;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Altux.Util;

namespace Altux.Radar.Handler
{
    public class RadarDataHandler : MonoBehaviour
    {
        RadarChart radarChart;
        public BoxPathGenerator LinePrefab;
        public GameObject pointPrefab;
        public Material radarMaterial;

        void Start()
        {
            radarChart = GetComponent<RadarChart>();
            StartCoroutine(GetRadarGraphData());
        }

        IEnumerator GetRadarGraphData()
        {
            UnityWebRequest request = UnityWebRequest.Get(Constants.URL.RADAR_DATA_URL);
            yield return request.SendWebRequest();

            string response = request.downloadHandler.text;
            RadarModel root = JsonConvert.DeserializeObject<RadarModel>(response);

            CreateRadarGraph(root);
        }
        void CreateRadarGraph(RadarModel root)
        {
            for (int i = 0; i < root.data.Count; i++)
            {
                if (!radarChart.DataSource.HasCategory(root.data[i].Key))
                    radarChart.DataSource.Add3DCategory(root.data[i].Key, LinePrefab, radarMaterial, 0.16f, pointPrefab, radarMaterial, 0.5f, radarMaterial, 10, 0.62f, -1.41f);
                if (!radarChart.DataSource.HasGroup(root.data[i].group.ToString()))
                    radarChart.DataSource.AddGroup(root.data[i].group.ToString());
                radarChart.DataSource.SetValue(root.data[i].Key, root.data[i].group.ToString(), root.data[i].Value);
            }
        }

    }
}
