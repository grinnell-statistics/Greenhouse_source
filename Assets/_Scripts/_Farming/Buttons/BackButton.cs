using UnityEngine;

public class BackButton : MonoBehaviour
{
    // link to instruction button and price table button
    // control appearance of all the buttons
    public GameObject harvestButton;
    public GameObject growButton;
    public GameObject sellallButton;
    public GameObject instruction;
    public GameObject data;
    public GameObject calculating;
    public GameObject loading;
    public GameObject clear;

    private void Start()
    {
        calculating.SetActive(false);
        loading.SetActive(false);

        if (Global.level == 4)
        {
            instruction.SetActive(false);
            data.SetActive(false);
        }
        if(Global.growClicked)
        {
            clear.SetActive(false);
        }
    
        if (Global.growed)
        {
            growButton.SetActive(false);
            harvestButton.SetActive(true);
            clear.SetActive(false);
        }
        else
        {
            harvestButton.SetActive(false);
        }
        if (Global.harvested)
        {
            data.SetActive(false);
            growButton.SetActive(false);
            clear.SetActive(false);
            harvestButton.SetActive(false);
            sellallButton.SetActive(true);
        }
    }
    private void Update()
    {
        if (Global.finishGrow)
        {
            harvestButton.SetActive(true);
            Global.finishGrow = false;
        }
        if (sellallButton.activeSelf)
        {
            Global.finishCurrSeason = true;
        }
        if (Global.harvested)
        {
            sellallButton.SetActive(true);
        }

        int planted = 0;

        for (int i = 0; i < Global.planted.Length; i++)
        {
            if (Global.planted[i])
            {
                planted++;
            }
        }
        if (planted != 0 && Global.level < 4)
        {
            if (!Global.remove)
            {
                growButton.SetActive(true);
            }
            if(Global.growClicked)
            {
                clear.SetActive(false);
            }
            else if (!Global.growed)
            {
                clear.SetActive(true);
            }
        }
        else
        {
            growButton.SetActive(false);
            clear.SetActive(false);
        }

    }

}