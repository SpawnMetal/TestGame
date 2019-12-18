using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    private void Awake()
    {
        if (GlobalModule.DontDestroyAudioCheck)
        {
            DontDestroyOnLoad(transform.gameObject);
            GlobalModule.DontDestroyAudioCheck = false;
        }
    }
}
