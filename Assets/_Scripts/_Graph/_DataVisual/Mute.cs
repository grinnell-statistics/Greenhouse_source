using UnityEngine;

public class Mute : MonoBehaviour
{
    void Update()
    {
        if (Global.muted)
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().mute = false;
        }
    }

    public void MuteSound()
    {
        Global.muted = !gameObject.GetComponent<AudioSource>().mute;
    }

}
