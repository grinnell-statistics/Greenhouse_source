using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
//using System.Security.Policy;
//using UnityEditorInternal;

public class ScatterplotData : MonoBehaviour
{
    struct point
    {
        public int season;
        public int plot;
        public string crop;
        public int totalWater;
        public float yield;
        public int nitrates;
        public int level;

        public float Get(int i)
        {
            switch(i)
            {
                case 1:
                    return season;
                case 2:
                    return plot;
                case 3:
                    return totalWater;
                case 4:
                    return yield;
                case 5:
                    return nitrates;
                case 6:
                    return level;
            }
            return -1;
        }
    }
    public GraphChart chart;

    //private float[,] waterData;
    //private float[,] seasonData;

    public TMP_Dropdown xAxis;
    public TMP_Dropdown yAxis;
    public TMP_Dropdown cropType;
    public TMP_Dropdown dataSet;

    public TextMeshProUGUI xLabel;
    public TextMeshProUGUI yLabel;

    public Toggle colors;
    public Toggle nitrate;

    //Chart aspects
    private VerticalAxis vert;
    private HorizontalAxis hor;

    private float xMax;
    private float yMax;

    private point[] playerPoints;
    private point[] groupPoints;




    // Start is called before the first frame update
    void Start()
    {
        //Get the graph chart object
        GameObject graphChart = GameObject.Find("GraphChart");
        vert = graphChart.GetComponent<VerticalAxis>();
        hor = graphChart.GetComponent<HorizontalAxis>();
        StartCoroutine(GetDatabaseValues());

        /*
        waterData = new float[Global.raindata.Length, 6];

        //Initialize the water 2D-array
        for(int i = 1; i < Global.season; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                waterData[i - 1, j] = Global.wateradded[i - 1, j] + Global.raindata[i - 1];
            }
        }

        //Automatically do water vs yield first
        RewriteData(this.waterData, Global.yielddata);
        seasonData = new float[Global.raindata.Length, 6];

        //Initialize season as 2D array
        for(int i = 0; i < 6; i++)
        {
            for(int j = 1; j <= Global.raindata.Length; j++)
            {
                seasonData[j-1, i] = j;
            }
        }
        */
        
    }

    public void ChangeAxes()
    {
        int xValues = 3;
        int yValues = 4;

        switch (xAxis.value)
        {
            //x axis is water
            case 0:
                xValues = 3;
                xLabel.SetText("Water");
                break;
            //x axis is yield
            case 1:
                xValues = 4;
                xLabel.SetText("Yield");
                break;
            //x axis is nitrate level
            case 2:
                xValues = 5;
                xLabel.SetText("Nitrate");
                break;
            //x axis is plot
            case 3:
                xValues = 2;
                xLabel.SetText("Plot");
                break;
            //x axis is season
            case 4:
                xValues = 1;
                xLabel.SetText("Season");
                break;
        }

        switch (yAxis.value)
        {
            //y axis is yield
            case 0:
                yValues = 4;
                yLabel.SetText("Yield");
                break;
            //y axis is water
            case 1:
                yValues = 3;
                yLabel.SetText("Water");
                break;
            //y axis is nitrate level
            case 2:
                yValues = 5;
                yLabel.SetText("Nitrate");
                break;
        }

        RewriteData(xValues, yValues);
    }



    private void RewriteData(int xData, int yData)
    {
        chart.DataSource.StartBatch();
        chart.DataSource.ClearCategory("Corns");
        chart.DataSource.ClearCategory("Beans");
        chart.DataSource.ClearCategory("Mystery");
        chart.DataSource.ClearCategory("Crop");
        chart.DataSource.ClearCategory("Origin");
        chart.DataSource.ClearCategory("yAxis");
        chart.DataSource.ClearCategory("xAxis");
        chart.DataSource.ClearCategory("Nitrate1");
        chart.DataSource.ClearCategory("Nitrate2");
        chart.DataSource.ClearCategory("Nitrate3");
        chart.DataSource.ClearCategory("Nitrate4");
        chart.DataSource.ClearCategory("Nitrate5");
        chart.DataSource.ClearCategory("Nitrate6");


        point[] points = dataSet.value == 1 ? groupPoints : playerPoints;
        //Find max values for both x and y
        xMax = 0;
        yMax = 0;

        foreach(point p in points)
        {
            //Color by crop
            if (colors.isOn)
            {
                if (p.crop == "Corns" && (cropType.value == 0 || cropType.value == 1))
                {
                    PlotPoint("Corns", p.Get(xData), p.Get(yData));
                    Debug.Log("Plot Corns");
                }
                else if (p.crop == "Beans" && (cropType.value == 0 || cropType.value == 2))
                {
                    PlotPoint("Beans", p.Get(xData), p.Get(yData));
                    Debug.Log("Plot bean");
                }
                else if ((p.crop == "Tomatoes") && (cropType.value == 0 || cropType.value == 3))
                {
                    PlotPoint("Tomatoes", p.Get(xData), p.Get(yData));
                    Debug.Log("Plot tomato");
                }
            }
            //Color by nitrate
            else if (nitrate.isOn)
            {
                if (p.crop == "Corns" && (cropType.value == 0 || cropType.value == 1))
                {
                    PlotNitrate(p.nitrates, p.Get(xData), p.Get(yData));
                }
                else if (p.crop == "Beans" && (cropType.value == 0 || cropType.value == 2))
                {
                    PlotNitrate(p.nitrates, p.Get(xData), p.Get(yData));
                }
                else if ((p.crop == "Tomatoes") && (cropType.value == 0 || cropType.value == 3))
                {
                    PlotNitrate(p.nitrates, p.Get(xData), p.Get(yData));
                }
            }
            else
            {
                if (p.crop == "Corns" && (cropType.value == 0 || cropType.value == 1))
                {
                    PlotPoint("Crop", p.Get(xData), p.Get(yData));
                }
                else if (p.crop == "Beans" && (cropType.value == 0 || cropType.value == 2))
                {
                    PlotPoint("Crop", p.Get(xData), p.Get(yData));
                }
                else if ((p.crop == "Tomatoes") && (cropType.value == 0 || cropType.value == 3))
                {
                    PlotPoint("Crop", p.Get(xData), p.Get(yData));
                }
            }

        } 


        //If there are points to plot, make sure origin is on graph
        if(yMax - 0 > 0.02 || xMax - 0 > 0.02)
        {
            chart.DataSource.AddPointToCategory("Origin", 0, 0);
        }


        //Set the y-axis
        if (yMax < 5)
        {
            vert.MainDivisions.UnitsPerDivision = 1;
        }
        else if (yMax < 10)
        {
            vert.MainDivisions.UnitsPerDivision = 2;
            chart.DataSource.AddPointToCategory("yAxis", 0, yMax + 2);
        }
        else if (yMax < 30)
        {
            vert.MainDivisions.UnitsPerDivision = 5;
            chart.DataSource.AddPointToCategory("yAxis", 0, yMax + 5);
        }
        else if (yMax < 50)
        {
            vert.MainDivisions.UnitsPerDivision = 10;
            chart.DataSource.AddPointToCategory("yAxis", 0, yMax + 10);
        }
        else if (yMax < 75)
        {
            vert.MainDivisions.UnitsPerDivision = 15;
            chart.DataSource.AddPointToCategory("yAxis", 0, yMax + 15);

        }
        else if (yMax < 100)
        {
            vert.MainDivisions.UnitsPerDivision = 20;
            chart.DataSource.AddPointToCategory("yAxis", 0, yMax + 20);

        }
        else if (yMax < 200)
        {
            vert.MainDivisions.UnitsPerDivision = 40;
            chart.DataSource.AddPointToCategory("yAxis", 0, yMax + 40);
        }
        else if (yMax < 400)
        {
            vert.MainDivisions.UnitsPerDivision = 80;
            chart.DataSource.AddPointToCategory("yAxis", 0, yMax + 80);
        }
        else if(yMax < 600)
        {
            vert.MainDivisions.UnitsPerDivision = 100;
            chart.DataSource.AddPointToCategory("yAxis", 0, yMax + 100);
        }
        else
        {
            vert.MainDivisions.UnitsPerDivision = 150;
            chart.DataSource.AddPointToCategory("yAxis", 0, yMax + 150);
        }



        //Set the x-axis
        if (xMax < 10)
        {
            hor.MainDivisions.UnitsPerDivision = 1;
        }
        else if (xMax < 16)
        {
            hor.MainDivisions.UnitsPerDivision = 2;
            chart.DataSource.AddPointToCategory("xAxis", xMax + 2, 0);
        }
        else if (xMax < 35)
        {
            hor.MainDivisions.UnitsPerDivision = 5;
            chart.DataSource.AddPointToCategory("xAxis", xMax + 5, 0);
        }
        else if (xMax < 50)
        {
            hor.MainDivisions.UnitsPerDivision = 10;
            chart.DataSource.AddPointToCategory("xAxis", xMax + 10, 0);
        }
        else if (xMax < 80)
        {
            hor.MainDivisions.UnitsPerDivision = 15;
            chart.DataSource.AddPointToCategory("xAxis", xMax + 15, 0);

        }
        else if (xMax < 100)
        {
            hor.MainDivisions.UnitsPerDivision = 20;
            chart.DataSource.AddPointToCategory("xAxis", xMax + 20, 0);

        }
        else if (xMax < 200)
        {
            hor.MainDivisions.UnitsPerDivision = 40;
            chart.DataSource.AddPointToCategory("xAxis", xMax + 40, 0);
        }
        else if (xMax < 400)
        {
            hor.MainDivisions.UnitsPerDivision = 80;
            chart.DataSource.AddPointToCategory("xAxis", xMax + 80, 0);
        }
        else
        {
            hor.MainDivisions.UnitsPerDivision = 100;
            chart.DataSource.AddPointToCategory("xAxis", xMax + 100, 0);
        }


        chart.DataSource.EndBatch();
    }



    //---------------Helpers-----------------

    private void PlotPoint(string pointName, float x, float y)
    {
        //Plot in correct group
        chart.DataSource.AddPointToCategory(pointName, x, y);
        yMax = Mathf.Max(yMax, y);
        xMax = Mathf.Max(xMax, x);
    }

    private void PlotNitrate(float nitrateLevel, float x, float y)
    {
        if(nitrateLevel < 200)
        {
            PlotPoint("Nitrate1", x, y);
        }
        else if(nitrateLevel < 250)
        {
            PlotPoint("Nitrate2", x, y);
        }
        else if (nitrateLevel < 300)
        {
            PlotPoint("Nitrate3", x, y);
        }
        else if (nitrateLevel < 350)
        {
            PlotPoint("Nitrate4", x, y);
        }
        else if (nitrateLevel < 400)
        {
            PlotPoint("Nitrate5", x, y);
        }
        else
        {
            PlotPoint("Nitrate6", x, y);
        }
    }

    public IEnumerator GetDatabaseValues()
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerID", Global.username);
        form.AddField("GroupID", Global.password);
        string url = "https://stat2games.sites.grinnell.edu/php/getgreenhousescatterplotdata.php";
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        string data = www.downloadHandler.text;
        if (data.Contains("Failed"))
        {
            Debug.Log(data);
            yield break;
        }
        string[] splitData = data.Split(new char[] { ';' }, System.StringSplitOptions.None);
        //Debug.Log("split player and group data");
        string[] playerData = splitData[0].Split(new char[] { ',' }, System.StringSplitOptions.None);
        Debug.Log(playerData.Length);
        //Debug.Log(playerData[1] + playerData[2] + playerData[3] + playerData[4]);
        string[] groupData = splitData[1].Split(new char[] { ',' }, System.StringSplitOptions.None);
        Debug.Log(playerData.Length);
        groupPoints = new point[groupData.Length / 7];
        playerPoints = new point[playerData.Length / 7];
        for (int i = 0; i < playerData.Length; i += 7)
        {
            playerPoints[i / 7].level = int.Parse(playerData[i]);
            playerPoints[i / 7].season = int.Parse(playerData[i + 1]);
            playerPoints[i / 7].plot = int.Parse(playerData[i + 2]);
            playerPoints[i / 7].crop = playerData[i + 3];
            playerPoints[i / 7].totalWater = int.Parse(playerData[i + 4]);
            playerPoints[i / 7].yield = float.Parse(playerData[i + 5]);
            playerPoints[i / 7].nitrates = int.Parse(playerData[i + 6]);
        }
        for (int i = 0; i < groupData.Length; i += 7)
        {
            groupPoints[i / 7].level = int.Parse(groupData[i]);
            groupPoints[i / 7].season = int.Parse(groupData[i + 1]);
            groupPoints[i / 7].plot = int.Parse(groupData[i + 2]);
            groupPoints[i / 7].crop = groupData[i + 3];
            groupPoints[i / 7].totalWater = int.Parse(groupData[i + 4]);
            groupPoints[i / 7].yield = float.Parse(groupData[i + 5]);
            groupPoints[i / 7].nitrates = int.Parse(groupData[i + 6]);

        }
        RewriteData(3, 4);
    }
}