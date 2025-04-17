using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestButton : MonoBehaviour
{
    
    public Sound sound;
    public GameObject gameController;
    public GameObject yieldDisplay;
    public GameObject SellAll;
    public GameObject calculating;
    public GameObject data;
    public GameObject instruction;
    

    private void Start()
    {
        this.gameObject.SetActive(false);

    }

    private void Update()
    {
        bool harvested = true;
        for (int i = 0; i < 40; i++)
        {
            if(Global.croptype[Global.season-1, i] != 0)
            {
                harvested = false;
                break;
            }
        }
        if(harvested)
        {
            this.gameObject.SetActive(false);
            Global.harvested = true;
            SellAll.SetActive(true);
        }
    }

    public void HarvestCrops()
    {
        int cornYield = 0;
        int beanYield = 0;
        int tomatoYield = 0;
        sound.PlayHarvestSound();
        data.SetActive(false);
        instruction.SetActive(false);
        for (int i = 0; i < 40; i++)
        {
            float yield = 0;
            if (Global.grown[i] && Global.planted[i])
            {
                
                if (Global.croptype[Global.season - 1, i] == 1)
                {
                    yield = Global.models[i].GetYield1();
                }
                else if (Global.croptype[Global.season - 1, i] == 2)
                {
                    yield = Global.models[i].GetYield2();
                }
                else
                {
                    yield = Global.models[i].GetYield3();
                }

                if (Global.level == 3)
                {
                    if (i < 8)
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
                    else if (i > 7 && i < 16)
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
                    else if (i > 23 && i < 32)
                    {
                        yield += 1;
                    }
                    else if (i > 31)
                    {
                        yield += 2;
                    }

                }

                if (Global.croptype[Global.season - 1, i] == 1)
                {
                    cornYield += Mathf.RoundToInt(yield);
                }
                else if (Global.croptype[Global.season - 1, i] == 2)
                {
                    beanYield += Mathf.RoundToInt(yield);
                }
                else
                {
                    tomatoYield += Mathf.RoundToInt(yield);
                }
            }
            Global.planted[i] = false;
            Global.harvest[i] = true;

            for (int j = 1; j < 41; j++)
            {
                gameController.GetComponent<Multiselect>().Reset(j);
            }

            Global.grown[i] = false;

            //Global.wateradded[Global.season - 1, i] = 0;

            //yield
            Global.yielddata[Global.season - 1, i] = yield;
            Global.plots[Global.season - 1, i] = true;
            if (Global.level == 3)
            {
                Global.models[i].UpdateNitrate(i + 1);
                Global.nitrateUpdated[i] = true;
            }
        }

        Global.crops += cornYield;
        Global.crops2 += beanYield;
        Global.crops3 += tomatoYield;

        DestroyAllClones();
        StartCoroutine(Wait());
        
        
        
        
        
        


        GameObject.Find("DatabaseSubmit").GetComponent<SubmitData>().SubmitUpload();

    }

    public void DestroyAllClones()
    {
        for (int i = 1; i < 41; i++)
        {
            GameObject[] crops = GameObject.FindGameObjectsWithTag("Clone" + i);
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
        
    }

    private IEnumerator Wait()
    {
        calculating.SetActive(true);
        yield return new WaitForSeconds(3);
        Debug.Log("here");
        calculating.SetActive(false);
        Global.harvested = true;
        this.gameObject.SetActive(false);
        
    }


}
