using UnityEngine;
using TMPro;

public class UpdateBoxDisplay : MonoBehaviour
{
    public TextMeshProUGUI seasonObj;

    private void Start()
    {
        seasonObj.SetText("Season " + Global.season.ToString());
    }
    public void UpdateSeason(int season)
    {
        seasonObj.SetText("Season " + season.ToString());
    }
}
