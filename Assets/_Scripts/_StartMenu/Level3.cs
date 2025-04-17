using UnityEngine;

public class Level3 : MonoBehaviour
{
    //Level 3 has fences and sunshine blocks
    private void Start()
    {
        if (Global.level == 3)
        {
            this.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("level3fence").SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("level3fence").SetActive(false);

        }
    }

}