using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Text.RegularExpressions;

public class Farm : MonoBehaviour
{
    public GameObject gameController;
    public GameObject notifications;
    public UpdateBoxDisplay rainBox;

    //Sprites and animations for corn crop
    public GameObject seed_corn;
    public GameObject corn1;
    public GameObject corn2;
    public GameObject corn3;
    public GameObject corn1B;
    public GameObject corn2B;
    public GameObject corn3B;

    //Sprites and animations for bean crop
    public GameObject seed_bean;
    public GameObject bean1;
    public GameObject bean2;
    public GameObject bean3;
    public GameObject bean1B;
    public GameObject bean2B;
    public GameObject bean3B;

    //Sprites and animations for tomato crop
    public GameObject seed_tomato;
    public GameObject tomato1;
    public GameObject tomato2;
    public GameObject tomato3;
    public GameObject tomato1B;
    public GameObject tomato2B;
    public GameObject tomato3B;

    // Bad crops
    private bool[] bad = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

    //Pesticides
    public GameObject pests;
    public GameObject pesticide;

    //Start position of seeds
    public float xStart;
    public float yStart;


    //Keep track of growth of crop
    private bool growing;
    public string cloneName;

    //Model
    public Models model;
    public SubmitWaterButton submit;

    //Keep track of slots
    private GameObject waterSelector;

    //Keep track of various displays
    public GameObject yieldDisplay;
    public GameObject fertDisplay;
    public GameObject data;
    public GameObject info;

    //Keep track of plot and type of plant
    private int plantType;
    private int plot;

    //Keep track of buttons
    public GameObject growButton;

    //Sound
    public Sound sound;



    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
        this.plot = Int32.Parse(Regex.Match(this.tag, @"\d+").Value);
        this.growing = false;
        model = Global.models[plot - 1];
        plantType = Global.croptype[Global.season - 1, this.plot - 1];
        waterSelector = GameObject.Find("WaterSelector").transform.GetChild(0).gameObject;
        // initialize nitrate for level 1
        if (Global.level == 1)
        {
            Global.nitrate[Global.season - 1, plot - 1] = Global.nitrateStart;
            Global.models[plot - 1].SetNitrateLevel(Global.nitrateStart);
        }
    }

    private void Update()
    {
        Global.models[plot - 1] = this.model;

        for (int i = 0; i < 40; i++)
        {
            float waterAmount = Global.wateradded[Global.season - 1, i];
            float nitrateAmount = Global.models[i].GetNitrateLevel();

            if (Global.croptype[Global.season - 1, i] == 1)
            {
                if (waterAmount > 50)
                {
                    bad[i] = true;
                }
                else if (waterAmount < 15)
                {
                    bad[i] = true;
                }
                else if (nitrateAmount < 250)
                {
                    bad[i] = true;
                }
                else
                {
                    bad[i] = false;
                }
            }
            else if (Global.croptype[Global.season - 1, i] == 2)
            {
                if (waterAmount > 30)
                {
                    bad[i] = true;
                }
                else if (waterAmount < 7)
                {
                    bad[i] = true;
                }
                else
                {
                    bad[i] = false;
                }
            }
            else if (Global.croptype[Global.season - 1, i] == 3)
            {
                if (waterAmount > 57)
                {
                    bad[i] = true;
                }
                else if (waterAmount < 30)
                {
                    bad[i] = true;
                }
                else if (nitrateAmount < 200)
                {
                    bad[i] = true;
                }
                else
                {
                    bad[i] = false;
                }
            }
        }

    }

    //---------------Farming/Harvesting Animations------------------------//

    //Changes from seedling to srpout to sapling to full crop
    public IEnumerator Grow(int plant)
    {
        this.growing = true;

        //Destroy all seeds and plant sprout
        yield return new WaitForSeconds(1);
        DestroyClones();

        if (plant == 1)
        {
            if (bad[this.plot - 1])
            {
                Plant(corn1B);
            }
            else
            {
                Plant(corn1);
            }
        }
        else if (plant == 2)
        {
            if (bad[this.plot - 1])
            {
                Plant(bean1B);
            }
            else
            {
                Plant(bean1);
            }
        }
        else
        {
            if (bad[this.plot - 1])
            {
                Plant(tomato1B);
            }
            else
            {
                Plant(tomato1);
            }
        }

        yield return new WaitForSeconds(0.9f);
        DestroyClones();
        StartCoroutine(Wait());

        if (plant == 1)
        {
            if (bad[this.plot - 1])
            {
                Plant(corn2B);
            }
            else
            {
                Plant(corn2);
            }

        }
        else if (plant == 2)
        {
            if (bad[this.plot - 1])
            {
                Plant(bean2B);
            }
            else
            {
                Plant(bean2);
            }
        }
        else
        {
            if (bad[this.plot - 1])
            {
                Plant(tomato2B);
            }
            else
            {
                Plant(tomato2);
            }
        }

        //Destroy all saplings and plant full crops
        yield return new WaitForSeconds(1);
        DestroyClones();
        StartCoroutine(Wait());
        if (plant == 1)
        {
            if (bad[this.plot - 1])
            {
                Plant(corn3B);
            }
            else
            {
                Plant(corn3);
            }

        }
        else if (plant == 2)
        {
            if (bad[this.plot - 1])
            {
                Plant(bean3B);
            }
            else
            {
                Plant(bean3);
            }
        }
        else
        {
            if (bad[this.plot - 1])
            {
                Plant(tomato3B);
            }
            else
            {
                Plant(tomato3);
            }
        }
        Global.finishGrow = true;
        Global.growed = true;
        Global.grown[plot - 1] = true;
        this.growing = false;
        CalculateYield();
    }

    private void DestroyClones()
    {
        GameObject[] crops = GameObject.FindGameObjectsWithTag(cloneName);
        foreach (GameObject crop in crops)
        {
            Destroy(crop);
        }

        GameObject[] pesticides = GameObject.FindGameObjectsWithTag("Pesticide");
        foreach (GameObject pesti in pesticides)
        {
            Destroy(pesti);
        }
    }

    //Makes the given gameobject appear in the plot
    public void Plant(GameObject plant)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject clone = Instantiate(plant, new Vector3(xStart + (0.15f * i), yStart - (.15f * j), -0.1f), Quaternion.identity);
                clone.tag = cloneName;
            }
        }
    }

    private void CalculateYield()
    {
        if (Global.grown[plot - 1] && Global.planted[plot - 1])
        {
            float yield = 0;
            if (Global.croptype[Global.season - 1, this.plot - 1] == 1)
            {
                yield = model.GetYield1();
            }
            else if (Global.croptype[Global.season - 1, this.plot - 1] == 2)
            {
                yield = model.GetYield2();
            }
            else
            {
                yield = model.GetYield3();
            }

            if (Global.level == 3)
            {
                if (this.plot < 9)
                {
                    if (yield >= 2)
                    {
                        yield -= 2;
                    }
                    else
                    {
                        yield = 0;
                    }
                }
                else if (this.plot > 8 && this.plot < 17)
                {
                    if (yield >= 1)
                    {
                        yield -= 1;
                    }
                    else
                    {
                        yield = 0;
                    }
                }
                else if (this.plot > 24 && this.plot < 33)
                {
                    yield += 1;
                }
                else if (this.plot > 32)
                {
                    yield += 2;
                }
            }
            Global.yielddata[Global.season - 1, this.plot - 1] = yield;
        }
    }


    //Makes the given gameobject appear in the plot
    //public void Pesticide(GameObject pesticide)
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        for (int j = 0; j < 4; j++)
    //        {
    //            GameObject clone = Instantiate(pesticide, new Vector3(xStart + (0.15f * i), yStart - (.15f * j), -0.1f), Quaternion.identity);
    //            clone.tag = "Pesticide";
    //        }
    //    }
    //}

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }


    //------------- User Interaction and Model Implementation ----------------------//
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
        }
        else
        {
            if (Global.finishCurrSeason)
            {

            }
            // Undo
            else if (Global.level != 4 && Global.remove && Global.planted[plot - 1])
            {
                // Undo crops
                if (Global.croptype[Global.season - 1, this.plot - 1] == 1)
                {
                    Global.gold += Global.cornSeedPrice;
                    Global.spending -= Global.cornSeedPrice;
                }
                else if (Global.croptype[Global.season - 1, this.plot - 1] == 2)
                {
                    Global.gold += Global.beanSeedPrice;
                    Global.spending -= Global.beanSeedPrice;
                }
                else if (Global.croptype[Global.season - 1, this.plot - 1] == 3)
                {
                    Global.gold += Global.tomatoSeedPrice;
                    Global.spending -= Global.tomatoSeedPrice;
                }
                Global.croptype[Global.season - 1, this.plot - 1] = 0;

                // Undo water
                Global.gold += Global.wateradded[Global.season - 1, this.plot - 1];
                Global.spending -= Global.wateradded[Global.season - 1, this.plot - 1];
                model.RemoveWater();
                Global.wateradded[Global.season - 1, this.plot - 1] = 0;
                gameController.GetComponent<Multiselect>().Remove(plot);
                Global.planted[this.plot - 1] = false;

                // Undo fertilizer
                if (Global.level == 2 || Global.level == 3)
                {

                    Global.nitrate[Global.season - 1, this.plot - 1] -= Global.nitrateadded[Global.season - 1, this.plot - 1];
                    Global.models[plot - 1].SetNitrateLevel((int)Global.nitrate[Global.season - 1, this.plot - 1]);
                    Global.gold += (Global.nitrateadded[Global.season - 1, this.plot - 1] / 50) * 10;
                    Global.spending -= (Global.nitrateadded[Global.season - 1, this.plot - 1] / 50) * 10;
                    Global.nitrateadded[Global.season - 1, this.plot - 1] = 0;

                }
                DestroyClones();
            }
            //Planting seeds on mouse click
            else if (Global.level != 4 && !Global.planted[plot - 1] && (Global.slotSelected == 1 || Global.slotSelected == 4 || Global.slotSelected == 6))
            {
                sound.PlaySeedingSound();
                growButton.SetActive(true);

                GameObject seedObj = this.seed_corn;

                //First type of seed selected
                if (Global.slotSelected == 1)
                {
                    seedObj = this.seed_corn;
                    plantType = 1;
                }
                //Second type of seed selected
                else if (Global.slotSelected == 4)
                {
                    seedObj = this.seed_bean;
                    plantType = 2;
                }
                //Third type of seed selected
                else if (Global.slotSelected == 6)
                {
                    seedObj = this.seed_tomato;
                    plantType = 3;
                }

                //Double size of all arrays if we run out of space
                if (Global.season - 1 >= 30)
                {
                    int newLength = 30 * 2;
                    float[,] newYield = new float[newLength, 40];
                    int[,] newWaterAdded = new int[newLength, 40];
                    int[,] newNitrateAdded = new int[newLength, 40];
                    int[,] newCrop = new int[newLength, 40];
                    float[,] newNitrate = new float[newLength, 40];
                    bool[,] newPlots = new bool[newLength, 40];

                    for (int i = 0; i < newLength / 2; i++)
                    {
                        for (int j = 0; j < 40; j++)
                        {
                            newYield[i, j] = Global.yielddata[i, j];
                            newWaterAdded[i, j] = Global.wateradded[i, j];
                            newNitrateAdded[i, j] = Global.nitrateadded[i, j];
                            newCrop[i, j] = Global.croptype[i, j];
                            newNitrate[i, j] = Global.nitrate[i, j];
                            newPlots[i, j] = Global.plots[i, j];

                        }
                    }
                    Global.yielddata = newYield;
                    Global.wateradded = newWaterAdded;
                    Global.nitrateadded = newNitrateAdded;
                    Global.croptype = newCrop;
                    Global.nitrate = newNitrate;
                    Global.plots = newPlots;
                }

                Global.croptype[Global.season - 1, this.plot - 1] = plantType;

                if (Global.slotSelected == 1)
                {
                    Global.gold -= Global.cornSeedPrice;
                    Global.spending += Global.cornSeedPrice;
                    if (Global.seeds == 0)
                    {
                        Global.slotSelected = -1;
                    }
                }
                else if (Global.slotSelected == 4)
                {
                    Global.gold -= Global.beanSeedPrice;
                    Global.spending += Global.beanSeedPrice;
                    if (Global.seeds2 == 0)
                    {
                        Global.slotSelected = -1;
                    }
                }
                else
                {
                    Global.gold -= Global.tomatoSeedPrice;
                    Global.spending += Global.tomatoSeedPrice;
                    if (Global.seeds3 == 0)
                    {
                        Global.slotSelected = -1;
                    }
                }

                //Plant the seed
                Plant(seedObj);
                Global.planted[this.plot - 1] = true;
                Global.harvest[this.plot - 1] = false;
            }
            //Add water 
            else if (Global.level != 4 && !this.growing && !Global.grown[plot - 1] && Global.slotSelected == 3 && Global.planted[plot - 1])
            {
                sound.PlayInventorySound();
                //Reset plot if deselecting
                if (gameController.GetComponent<Multiselect>().plotsSelected[plot - 1])
                {
                    gameController.GetComponent<Multiselect>().Undo(plot);
                    gameController.GetComponent<Multiselect>().plotsSelected[this.plot - 1] = false;

                }
                //Select plot
                else
                {
                    gameController.GetComponent<Multiselect>().Select(plot);
                    waterSelector.SetActive(true);
                }
            }
            //Add fertilizer
            else if (Global.level != 4 && !Global.grown[plot - 1] && Global.slotSelected == 8 && Global.level > 1 && Global.fertilizer > 0)
            {
                model.AddFertilizer(plot);
                Global.gold -= 10;
                Global.spending += 10;
                Global.nitrateadded[Global.season - 1, plot - 1] += 50;
                Global.fertUsed++;
                fertDisplay.GetComponent<Display>().DisplayResource("50");

            }
            //Add pesticides
            //else if (Global.level != 4 && !Global.grown[plot - 1] && Global.slotSelected == 9 && Global.level > 3)
            //{
            //   // model.AddPesticides(plot);
            //    Global.pesticides--;
            //    Pesticide(pesticide);
            //}
        }
    }

    //-------------Miscellaneous---------
    public int GetPlotNum()
    {
        return this.plot;
    }

    public int GetPlantType()
    {
        Debug.Log("aim: " + this.plot);
        return Global.croptype[Global.season - 1, this.plot - 1];
    }


    public bool AllPlanted()
    {
        foreach (bool plant in Global.planted)
        {
            if (!plant)
            {
                return false;
            }
        }
        return true;
    }

    public bool AllHarvested()
    {
        foreach (bool harvest in Global.harvest)
        {
            if (!harvest)
            {
                return false;
            }
        }
        return true;
    }

}
