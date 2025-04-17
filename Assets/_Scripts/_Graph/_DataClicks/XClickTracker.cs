using UnityEngine;
using TMPro;

public class XClickTracker : MonoBehaviour
{
    public void UpdateClicks()
    {
        TMP_Dropdown dropdown = gameObject.GetComponent<TMP_Dropdown>();
        switch (dropdown.value)
        {
            case 0:
                DataClicks.xSeason++;
                break;
            case 1:
                DataClicks.xYield++;
                break;
            case 2:
                DataClicks.xNitrate++;
                break;
            case 3:
                DataClicks.xPlot++;
                break;
            case 4:
                DataClicks.xWater++;
                break;
        }
    }
}
