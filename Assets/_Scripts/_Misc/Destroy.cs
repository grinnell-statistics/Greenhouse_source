using UnityEngine;

public class Destroy : MonoBehaviour
{
    //Make sure when load Farm Scene as new game, unnecessary clones are destroyed
    private void Start()
    {
        if (Global.newGame)
        {
            for (int i = 1; i < 41; i++)
            {
                string clone = "Clone" + i;
                GameObject[] objs = GameObject.FindGameObjectsWithTag(clone);
                for (int j = 0; j < objs.Length; j++)
                {
                    Destroy(objs[j]);
                }
            }
            GameObject[] objs2 = GameObject.FindGameObjectsWithTag("PrevSelected");
            for (int j = 0; j < objs2.Length; j++)
            {
                objs2[j].SetActive(false);
            }
            GameObject[] objs3 = GameObject.FindGameObjectsWithTag("Selected");
            for (int j = 0; j < objs3.Length; j++)
            {
                objs3[j].SetActive(false);
            }


        }
        Global.newGame = false;
    }

}
