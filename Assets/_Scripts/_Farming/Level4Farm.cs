using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level4Farm : MonoBehaviour
{
    public GameObject gameController;
    //Keep track of plot
    private int plot;
    public Farm[] plots;

    // Start is called before the first frame update
    public void Start()
    {
        if (Global.level == 4 && !Global.harvested && !Global.growed)
        {
            Global.gold -= Global.spending;

            // create plot number list
            List<int> plotNumbers = new List<int>();
            for (int i = 1; i <= 40; i++)
            {
                plotNumbers.Add(i);
            }
            // Randomly grow corns
            if (Global.seeds > 0)
            {
                List<int> waterNumbers = new List<int>();
                foreach (int i in Global.waterAmounts)
                {
                    if (i >= 0)
                    {
                        for (int k = 0; k < Global.repetition; k++)
                        {
                            waterNumbers.Add(i);
                        }
                    }
                }
                int targetTimes = Global.plotNeeded / Global.cropTypes;

                for (int r = 0; r < targetTimes; r++)
                {
                    // get random plot number
                    plot = plotNumbers[Random.Range(0, plotNumbers.Count)];
                    plotNumbers.Remove(plot);
                    Global.croptype[Global.season - 1, plot - 1] = 1;
                    // get random water amounts
                    int waterLevel = waterNumbers[Random.Range(0, waterNumbers.Count)];
                    waterNumbers.Remove(waterLevel);
                    Global.models[plot - 1].AddWater(waterLevel);
                    Global.wateradded[Global.season - 1, plot - 1] = waterLevel;
                    gameController.GetComponent<PlantGrowth>().GrowPlant(plots[plot - 1], plot);

                    Global.grown[plot - 1] = true;
                    Global.planted[plot - 1] = true;

                }

            }
            // Randomly grow beans
            if (Global.seeds2 > 0)
            {
                List<int> waterNumbers = new List<int>();
                foreach (int i in Global.waterAmounts)
                {
                    if (i >= 0)
                    {

                        for (int k = 0; k < Global.repetition; k++)
                        {
                            waterNumbers.Add(i);
                        }
                    }
                }
                int targetTimes = Global.plotNeeded / Global.cropTypes;

                for (int r = 0; r < targetTimes; r++)
                {
                    // get random plot numbers
                    plot = plotNumbers[Random.Range(0, plotNumbers.Count)];
                    plotNumbers.Remove(plot);
                    Global.croptype[Global.season - 1, plot - 1] = 2;
                    // get random water amounts
                    int waterLevel = waterNumbers[Random.Range(0, waterNumbers.Count)];
                    waterNumbers.Remove(waterLevel);
                    Global.models[plot - 1].AddWater(waterLevel);
                    Global.wateradded[Global.season - 1, plot - 1] = waterLevel;
                    gameController.GetComponent<PlantGrowth>().GrowPlant(plots[plot - 1], plot);
                    Global.grown[plot - 1] = true;
                    Global.planted[plot - 1] = true;
                }
            }
            // Randomly grow tomatoes
            if (Global.seeds3 > 0)
            {
                List<int> waterNumbers = new List<int>();
                foreach (int i in Global.waterAmounts)
                {
                    if (i >= 0)
                    {
                        for (int k = 0; k < Global.repetition; k++)
                        {
                            waterNumbers.Add(i);
                        }
                    }
                }
                int targetTimes = Global.plotNeeded / Global.cropTypes;

                for (int r = 0; r < targetTimes; r++)
                {
                    // get random plot number
                    plot = plotNumbers[Random.Range(0, plotNumbers.Count)];
                    plotNumbers.Remove(plot);
                    Global.croptype[Global.season - 1, plot - 1] = 3;
                    // get random water amounts
                    int waterLevel = waterNumbers[Random.Range(0, waterNumbers.Count)];
                    waterNumbers.Remove(waterLevel);
                    Global.models[plot - 1].AddWater(waterLevel);
                    Global.wateradded[Global.season - 1, plot - 1] = waterLevel;
                    gameController.GetComponent<PlantGrowth>().GrowPlant(plots[plot - 1], plot);
                    Global.grown[plot - 1] = true;
                    Global.planted[plot - 1] = true;
                }
            }
        }
    }
}
