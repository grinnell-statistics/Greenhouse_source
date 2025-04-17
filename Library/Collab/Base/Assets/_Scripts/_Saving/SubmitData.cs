using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

//this script sends game data to the server
public class SubmitData : MonoBehaviour
{

    public void SubmitUpload()
    {
        StartCoroutine(Upload());
    }

    public IEnumerator Upload()
    {

        int gameNum = -1;

       /* Debug.Log("start submitting");
        WWWForm numForm = new WWWForm();
        numForm.AddField("PlayerID", Global.username);
        numForm.AddField("GroupID", Global.password);

        //Fetch game number
        using (UnityWebRequest www = UnityWebRequest.Post("https://stat2games.sites.grinnell.edu/php/getgreenhousenum.php", numForm))
        {
            yield return www.SendWebRequest();
            try
            {
                gameNum = int.Parse(www.downloadHandler.text);
            }
            catch (Exception e)
            {
                Debug.Log("Fetching game number failed.  Error message: " + e.ToString());
            }

        }*/


        //Send individual plot data
        for (int i = 0; i < 40; i++)
        {

            if (Global.croptype[Global.season - 1, i] != 0)
            {
                WWWForm form = new WWWForm();
                //form.AddField("GameNum", gameNum);
                form.AddField("PlayerID", Global.username);
                form.AddField("GroupID", Global.password);
                form.AddField("Level", Global.level);
                form.AddField("Season", Global.season);
                form.AddField("Money", Global.gold);
                form.AddField("Plot", i + 1);


                //Adding crop type 
                if (Global.croptype[Global.season - 1, i] == 1)
                {
                    form.AddField("Crop", "Corns");
                }
                else if (Global.croptype[Global.season - 1, i] == 2)
                {
                    form.AddField("Crop", "Beans");
                }
                else
                {
                    form.AddField("Crop", "Tomatoes");
                }


                //Adding prior crop
                if (Global.season - 1 == 0)
                {
                    form.AddField("PriorHarvest", "None");
                }
                else
                {
                    if (Global.croptype[Global.season - 2, i] == 1)
                    {
                        form.AddField("PriorHarvest", "Corns");
                    }
                    else if (Global.croptype[Global.season - 2, i] == 2)
                    {
                        form.AddField("PriorHarvest", "Beans");
                    }
                    else if (Global.croptype[Global.season - 2, i] == 3)
                    {
                        form.AddField("PriorHarvest", "Tomatoes");
                    }
                    else
                    {
                        form.AddField("PriorHarvest", "None");
                    }
                }

                form.AddField("WaterAdded", Global.wateradded[Global.season - 1, i]);
                form.AddField("Insects", BoolToInt(Global.pests[Global.season - 1, i]));
                int nitrate = Global.models[i].GetNitrateLevel();


                if (Global.croptype[Global.season - 1, i] == 1 && Global.season >= 8)
                {
                    nitrate += 25;
                }
                else if (Global.croptype[Global.season - 1, i] == 2 && Global.season >= 8)
                {
                    nitrate -= 10;
                }
                else if (Global.croptype[Global.season - 1, i] == 3 && Global.season >= 8)
                {
                    nitrate += 15;
                }

                form.AddField("NitrateLevel", nitrate);
                form.AddField("PesticidesAdded", BoolToInt(Global.pestUsed[Global.season - 1, i]));
                form.AddField("Yield", Global.yielddata[Global.season - 1, i].ToString("F2"));
                if (Global.croptype[Global.season - 1, i] == 1)
                {
                    form.AddField("BuyPrice", Global.currCornSeed);
                    form.AddField("SellPrice", Global.currCornCrop);
                }
                else if (Global.croptype[Global.season - 1, i] == 2)
                {
                    form.AddField("BuyPrice", Global.currBeanSeed);
                    form.AddField("SellPrice", Global.currBeanCrop);
                }
                else
                {
                    form.AddField("BuyPrice", Global.currCarrotSeed);
                    form.AddField("SellPrice", Global.currCarrotCrop);
                }

                Global.croptype[Global.season - 1, i] = 0;

                /*string url = "https://stat2games.sites.grinnell.edu/php/sendgreenhousegameinfo.php";
                UnityWebRequest www = UnityWebRequest.Post(url, form);
                yield return www.SendWebRequest();*/



                using (UnityWebRequest www = UnityWebRequest.Post("https://stat2games.sites.grinnell.edu/php/sendgreenhousegameinfo.php", form))
                {

                    yield return www.SendWebRequest();

                    if (www.downloadHandler.text == "0")
                    {
                        Debug.Log("Player data created successfully.");
                    }
                    else
                    {
                        Debug.Log("Player data creation failed. Error # " + www.downloadHandler.text);
                    }
                }
            }
        }
    }




    private int BoolToInt(bool boolean)
    {
        if (boolean)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

}



