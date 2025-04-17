using System.Collections;
using UnityEngine;
using TMPro;

public class Display : MonoBehaviour
{
    public string resourceName;

    public void DisplayResource(string amount)
    {
        this.gameObject.SetActive(true);
        this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(resourceName);
        this.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(amount);
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }

}
