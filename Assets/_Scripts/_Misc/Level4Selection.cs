using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level4Selection : MonoBehaviour
{
    // Level 4 Selection Scene
    public GameObject plot1;
    public GameObject plot2;
    public GameObject cost1;
    public GameObject cost2;
    public GameObject repetition;
    public GameObject error;
    public GameObject Play;
    public GameObject gold;


    // Calculate estimated needed plots and costs when user is entering inputs
    private void Update()
    {
        gold.GetComponent<TMP_InputField>().SetTextWithoutNotify(Global.gold.ToString());

        int length = 0;
        int totalWater = 0;
        for (int i = 0; i < 6; i++)
        {
            if (Global.waterAmounts[i] >= 0)
            {
                length++;
                totalWater += Global.waterAmounts[i];
            }
        }

        int plotNumber = Global.cropTypes * length;

        plot1.GetComponent<TMP_InputField>().SetTextWithoutNotify(plotNumber.ToString());

        int costNumber = Global.seeds * Global.cornSeedPrice * length + Global.seeds2 * Global.beanSeedPrice * length + Global.seeds3 * Global.tomatoSeedPrice * length + totalWater;
        cost1.GetComponent<TMP_InputField>().SetTextWithoutNotify(costNumber.ToString());


        int times = 0;
        if (repetition.GetComponent<TMP_InputField>().text != "")
        {
            times = int.Parse(repetition.GetComponent<TMP_InputField>().text);
        }

        int plotNumber2 = plotNumber * times;
        plot2.GetComponent<TMP_InputField>().SetTextWithoutNotify(plotNumber2.ToString());

        if (plotNumber2 > 40)
        {
            error.SetActive(true);
        }
        else
        {
            error.SetActive(false);
        }

        if (plotNumber2 <= 0 || plotNumber2 > 40)
        {
            Play.GetComponent<Button>().interactable = false;
        }
        else
        {
            Play.GetComponent<Button>().interactable = true;
        }
        int costNumber2 = costNumber * times;
        cost2.GetComponent<TMP_InputField>().SetTextWithoutNotify(costNumber2.ToString());

        Global.spending = costNumber2;
        Global.plotNeeded = plotNumber2;
        Global.repetition = times;
        Global.waterLength = length;
        length = 0;
    }
}
