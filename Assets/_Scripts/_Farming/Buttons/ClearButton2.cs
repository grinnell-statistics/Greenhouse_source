using UnityEngine;

public class ClearButton2 : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject bar;
    public GameObject clear;
    public GameObject waterSelector;


    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void NotRemove()
    {
        Global.remove = false;
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        waterSelector.SetActive(true);
        bar.SetActive(true);
        clear.SetActive(true);
        this.gameObject.SetActive(false);
    }
}