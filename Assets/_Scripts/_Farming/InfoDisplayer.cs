using UnityEngine;
using TMPro;

public class InfoDisplayer : MonoBehaviour
{
    public TextMeshProUGUI cropText;
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI nitrateText;
    public TextMeshProUGUI yieldText;

    public void SetText(int plot)
    {
        if (Global.croptype[Global.season - 1, plot - 1] == 0)
        {
            cropText.SetText("Crop: None");

        }
        else
        {
            if (Global.croptype[Global.season - 1, plot - 1] == 1)
            {

                cropText.SetText("Crop: Corn");
            }
            if (Global.croptype[Global.season - 1, plot - 1] == 2)
            {

                cropText.SetText("Crop: Bean");
            }
            if (Global.croptype[Global.season - 1, plot - 1] == 3)
            {

                cropText.SetText("Crop: Tomato");
            }
        }
        waterText.SetText("Water: " + Global.wateradded[Global.season - 1, plot - 1].ToString());
        nitrateText.SetText("Nitrate: " + (int)Global.nitrate[Global.season - 1, plot - 1]);
        yieldText.SetText("Yield: " + Mathf.RoundToInt(Global.yielddata[Global.season - 1, plot - 1]).ToString());
    }
}
