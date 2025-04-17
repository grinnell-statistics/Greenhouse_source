using UnityEngine;
using TMPro;

public class YClickTracker : MonoBehaviour
{
    public void UpdateClicks()
    {
        TMP_Dropdown dropdown = gameObject.GetComponent<TMP_Dropdown>();
        switch (dropdown.value)
        {
            case 0:
                DataClicks.yYield++;
                break;
            case 1:
                DataClicks.yWater++;
                break;
            case 2:
                DataClicks.yNitrate++;
                break;
        }
    }
}
