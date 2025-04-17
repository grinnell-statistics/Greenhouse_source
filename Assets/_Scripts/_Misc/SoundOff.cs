using UnityEngine;

public class SoundOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Global.muted)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
