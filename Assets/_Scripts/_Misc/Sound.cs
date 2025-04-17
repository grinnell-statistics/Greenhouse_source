using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource audioSource;
    public SoundCollection buttonSound;
    public SoundCollection inventorySound;
    public SoundCollection rainSound;
    public SoundCollection seedingSound;
    public SoundCollection harvestSound;
    public SoundCollection coinSound;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void Mute()
    {
        Global.muted = !audioSource.mute;
    }

    public void PlayButtonSound()
    {
        buttonSound.Play(audioSource);
    }

    public void PlayInventorySound()
    {
        inventorySound.Play(audioSource);
    }

    public void PlayRainSound()
    {
        rainSound.Play(audioSource);
    }

    public void PlaySeedingSound()
    {
        seedingSound.Play(audioSource);
    }

    public void PlayHarvestSound()
    {
        harvestSound.Play(audioSource);
    }

    public void PlayCoinSound()
    {
        coinSound.Play(audioSource);
    }
}
