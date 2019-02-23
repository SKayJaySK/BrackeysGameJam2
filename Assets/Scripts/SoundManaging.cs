using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManaging : MonoBehaviour
{
    public static SoundManaging instance;

    public List<AudioClip> sounds = new List<AudioClip>();
    public AudioSource as1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level8" && as1.clip.name != "BGM part 2")
        {
            as1.clip = sounds[0];
            as1.Play();
        }
    }
}
