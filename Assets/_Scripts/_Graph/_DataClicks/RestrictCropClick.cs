using UnityEngine;
using TMPro;

public class RestrictCropClick : MonoBehaviour
{
    public void UpdateClicks()
    {
        TMP_Dropdown dropdown = gameObject.GetComponent<TMP_Dropdown>();
        switch (dropdown.value)
        {
            case 0:
                DataClicks.restrictAll++;
                break;
            case 1:
                DataClicks.restrictCorn++;
                break;
            case 2:
                DataClicks.restrictBean++;
                break;
            case 3:
                DataClicks.restrictTomato++;
                break;
        }
    }
}
