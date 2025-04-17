using UnityEngine;

public class Multiselect : MonoBehaviour
{
    public GameObject[] selected;
    public GameObject[] prevSelected;
    public GameObject waterSelector;

    public bool[] plotsSelected;

    private void Start()
    {
        GameObject[] select = GameObject.FindGameObjectsWithTag("Selected");
        foreach (GameObject obj in select)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        bool someSelected = false;
        foreach (bool plot in plotsSelected)
        {
            if (plot)
            {
                someSelected = true;
            }
        }

        if (!someSelected)
        {
            waterSelector.SetActive(false);
        }
    }

    public void Select(int plot)
    {
        selected[plot - 1].SetActive(true);
        plotsSelected[plot - 1] = true;
    }

    public void PrevSelect(int plot)
    {
        selected[plot - 1].SetActive(false);
        plotsSelected[plot - 1] = false;
        prevSelected[plot - 1].SetActive(true);
    }

    public void Undo(int plot)
    {
        selected[plot - 1].SetActive(false);
        plotsSelected[plot - 1] = false;
    }

    public void Remove(int plot)
    {
        selected[plot - 1].SetActive(false);
        prevSelected[plot - 1].SetActive(false);
        plotsSelected[plot - 1] = false;
    }

    public void ResetSelected(int plot)
    {
        selected[plot - 1].SetActive(false);
        GameObject[] select = GameObject.FindGameObjectsWithTag("Selected");
        foreach (GameObject obj in select)
        {
            obj.SetActive(false);
        }
    }

    public void Reset(int plot)
    {
        prevSelected[plot - 1].SetActive(false);
        selected[plot - 1].SetActive(false);


        GameObject[] select = GameObject.FindGameObjectsWithTag("Selected");
        foreach (GameObject obj in select)
        {
            obj.SetActive(false);
        }

        GameObject[] prevSelect = GameObject.FindGameObjectsWithTag("PrevSelected");
        foreach (GameObject obj in prevSelect)
        {
            obj.SetActive(false);
        }

    }
}
