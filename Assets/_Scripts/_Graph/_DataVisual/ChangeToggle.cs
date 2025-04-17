using UnityEngine;
using UnityEngine.UI;

public class ChangeToggle : MonoBehaviour
{
    public Toggle other;
    public GameObject legend;

    public void SwitchToggle()
    {
        if (this.gameObject.GetComponent<Toggle>().isOn)
        {
            other.isOn = false;
            legend.SetActive(true);
        }
        else
        {
            legend.SetActive(false);
        }
    }
}
