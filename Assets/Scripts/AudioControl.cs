using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        audioSource.Play();
        // AudioSource.PlayClipAtPoint(audioSource.clip, this.gameObject.transform.position);
        Destroy(this.gameObject, audioSource.clip.length);
    }
}
