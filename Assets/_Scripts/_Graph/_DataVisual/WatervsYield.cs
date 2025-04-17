using UnityEngine;
using ChartAndGraph;

public class WatervsYield : MonoBehaviour
{

    public GraphChart chart;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < Global.season; i++)
        {
            //Plant for each plot
            for (int j = 0; j < 40; j++)
            {
                if (Global.croptype[i - 1, j] == 1)
                {
                    chart.DataSource.AddPointToCategory("Crop 1",
                                                        Global.wateradded[i - 1, j],
                                                        Global.yielddata[i - 1, j]);
                }
                else if (Global.croptype[i - 1, j] == 2)
                {
                    chart.DataSource.AddPointToCategory("Crop 2",
                                                       Global.wateradded[i - 1, j],
                                                       Global.yielddata[i - 1, j]);
                }
                else if (Global.croptype[i - 1, j] == 3)
                {
                    chart.DataSource.AddPointToCategory("Crop 3",
                                                       Global.wateradded[i - 1, j],
                                                       Global.yielddata[i - 1, j]);
                }
            } //for plot
        } // for season
    }

}
