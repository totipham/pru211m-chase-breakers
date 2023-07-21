using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource shootSound;
    private AudioSource gameOverSound;
    private AudioSource jumpSound;
    private AudioSource caughtSound;
    private AudioSource menuSound;
    private AudioSource inGameSound;

    // Start is called before the first frame update
    void Start()
    {
        menuSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StopAllSounds()
    {
        AudioSource[] allSounds = FindObjectsOfType<AudioSource>();
        foreach (AudioSource sound in allSounds)
        {
            sound.Stop();
        }
    }

}
