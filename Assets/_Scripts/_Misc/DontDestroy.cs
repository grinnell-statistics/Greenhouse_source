using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Make sure when loading Farm Scene not as new game, necessary clones are not destroyed
    public GameObject growButton;

    private void Start()
    {
        Global.loadCore = false;
        if (Global.newGame)
        {
            for (int i = 1; i < 41; i++)
            {
                string clone = "Clone" + i;
                Destroy(GameObject.FindGameObjectWithTag(clone));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < 41; i++)
        {
            string clone = "Clone" + i;
            SetObjects(clone);
        }

        SetObjects("Selection");

        SetObjects("Pesticide");
    }

    private void SetObjects(string objTag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(objTag);

        foreach (GameObject obj in objs)
        {
            DontDestroyOnLoad(obj);
        }


    }
}
