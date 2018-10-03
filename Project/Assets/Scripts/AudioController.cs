using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public static AudioController instance;

    public AudioSource audioSource;

    [SerializeField]
    private AudioClip clickButtonClip;


  void Awake()
    {
        MakeSingleton();
    }

    void Start () {
     

    }
	
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


     public void playButtonClickSound()
    {
        audioSource.PlayOneShot(clickButtonClip);
    }

	
}
