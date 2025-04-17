using UnityEngine;
using UnityEngine.EventSystems;

public class InfoButton : MonoBehaviour
{
    // Plot hover over info canvas
    public int plotNum;
    public GameObject displayer;
    public Sound buttonSound;

    private void OnMouseDown()
    {
        buttonSound.PlayButtonSound();
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            displayer.SetActive(true);
            displayer.GetComponent<InfoDisplayer>().SetText(plotNum);
        }
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            displayer.SetActive(true);
            displayer.GetComponent<InfoDisplayer>().SetText(plotNum);
        }
    }


    private void OnMouseExit()
    {
        displayer.SetActive(false);
    }
}
