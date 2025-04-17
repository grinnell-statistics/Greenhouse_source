using System.Collections;
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
            if (Global.croptype[Global.season - 1, i] == 1)
            {
                cornYield += Mathf.RoundToInt(Global.yielddata[Global.season - 1, i]);
            }
            else if (Global.croptype[Global.season - 1, i] == 2)
            {
                beanYield += Mathf.RoundToInt(Global.yielddata[Global.season - 1, i]);
            }
            else
            {
                tomatoYield += Mathf.RoundToInt(Global.yielddata[Global.season - 1, i]);
            }

            Global.planted[i] = false;
            Global.harvest[i] = true;

            // Clear selections
            for (int j = 1; j < 41; j++)
            {
                gameController.GetComponent<Multiselect>().Reset(j);
            }

            Global.grown[i] = false;
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
        calculating.SetActive(false);
        Global.harvested = true;
        this.gameObject.SetActive(false);
    }


}
