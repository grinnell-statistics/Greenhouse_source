using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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

        WWWForm numForm = new WWWForm();
        numForm.AddField("PlayerID", Global.username);
        numForm.AddField("GroupID", Global.password);

        //Fetch game number
        using (var www = UnityWebRequest.Post("https://stat2games.sites.grinnell.edu/php/getgreenhousenum.php", numForm))
        {
            Debug.Log("starting fetching game num");
            yield return www.SendWebRequest();
            Debug.Log("fetched");
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Fetching game number failed.  Error message: ");
            }
            else
            {
                gameNum = int.Parse(www.downloadHandler.text);
            }
        }

        //Send individual plot data
        for (int i = 0; i < 40; i++)
        {

            if (Global.croptype[Global.season - 1, i] != 0)
            {
                WWWForm form = new WWWForm();
                form.AddField("GameNum", gameNum);
                form.AddField("PlayerID", Global.username);
                form.AddField("GroupID", Global.password);
                form.AddField("Level", Global.level);
                form.AddField("Season", Global.season);
                form.AddField("Money",
                    Global.gold + Global.crops * Global.cornCropPrice +
                    Global.crops2 * Global.beanCropPrice + Global.crops3 * Global.tomatoCropPrice);
                form.AddField("Plot", i + 1);

                //Adding crop type 
                if (Global.croptype[Global.season - 1, i] == 1)
                {
                    form.AddField("Crop", "Corn");
                }
                else if (Global.croptype[Global.season - 1, i] == 2)
                {
                    form.AddField("Crop", "Beans");
                }
                else
                {
                    form.AddField("Crop", "Tomato");
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
                        form.AddField("PriorHarvest", "Corn");
                    }
                    else if (Global.croptype[Global.season - 2, i] == 2)
                    {
                        form.AddField("PriorHarvest", "Beans");
                    }
                    else if (Global.croptype[Global.season - 2, i] == 3)
                    {
                        form.AddField("PriorHarvest", "Tomato");
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

                using (var www = UnityWebRequest.Post("https://stat2games.sites.grinnell.edu/php/sendgreenhousegameinfo.php", form))
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



