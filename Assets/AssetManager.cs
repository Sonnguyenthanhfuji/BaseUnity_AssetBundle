using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    private static AssetManager _i;

    public static AssetManager i
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<AssetManager>("AssetManager"));
            }
            return _i;
        }
    }

    public SoundEffectClip[] soundEffectClips;
    
    [System.Serializable]
    public class SoundEffectClip
    {
        public SoundManager.SoundEffect soundEffect;
        public AudioClip audioClip;
    }
}
