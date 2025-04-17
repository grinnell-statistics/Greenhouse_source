using UnityEngine;

public class Instructions : MonoBehaviour
{
    // Instruction Scene
    public GameObject ins1;
    public GameObject ins2;
    public GameObject ins3;
    public GameObject table1;
    public GameObject table2;

    // Start is called before the first frame update
    void Start()
    {
        if (Global.level == 1)
        {
            ins1.gameObject.SetActive(true);
            ins2.gameObject.SetActive(false);
            ins3.gameObject.SetActive(false);

            table1.gameObject.SetActive(true);
            table2.gameObject.SetActive(false);

        }
        else if (Global.level == 2)
        {
            ins2.gameObject.SetActive(true);
            ins1.gameObject.SetActive(false);
            ins3.gameObject.SetActive(false);

            table2.gameObject.SetActive(true);
            table1.gameObject.SetActive(false);
        }
        else if (Global.level == 3)
        {
            ins3.gameObject.SetActive(true);
            ins2.gameObject.SetActive(false);
            ins1.gameObject.SetActive(false);

            table2.gameObject.SetActive(true);
            table1.gameObject.SetActive(false);
        }
    }
}
