using UnityEngine;
using UnityEngine.UI;

public class NitrateColorClick : MonoBehaviour
{
    public void UpdateClicks()
    {
        if (this.gameObject.GetComponent<Toggle>().isOn)
        {
            DataClicks.colorNitrate++;
        }
    }
}
