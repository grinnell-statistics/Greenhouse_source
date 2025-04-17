using UnityEngine;
using TMPro;

public class PriceTable : MonoBehaviour
{
    public TextMeshProUGUI cornseedText;
    public TextMeshProUGUI corncropText;
    public TextMeshProUGUI beanseedText;
    public TextMeshProUGUI beancropText;
    public TextMeshProUGUI tomatoseedText;
    public TextMeshProUGUI tomatocropText;
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI nitrateText;

    private void Start()
    {
        cornseedText.SetText(Global.cornSeedPrice.ToString());
        corncropText.SetText(Global.cornCropPrice.ToString());
        beanseedText.SetText(Global.beanSeedPrice.ToString());
        beancropText.SetText(Global.beanCropPrice.ToString());
        tomatoseedText.SetText(Global.tomatoSeedPrice.ToString());
        tomatocropText.SetText(Global.tomatoCropPrice.ToString());
        waterText.SetText(Global.waterPrice.ToString());
        if (Global.level == 1)
        {
            nitrateText.SetText("-");
        }
        else if (Global.level > 1)
        {
            nitrateText.SetText("10");
        }
    }

}


