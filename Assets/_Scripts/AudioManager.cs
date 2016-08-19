using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour {

    public AudioMixer mixer;
    public Vector3 ambientSoundPos;
    [Header("Readonly")]
    public List<GameObject> currentSources;

    public void DrugSounds(float duration)
    {
        StartCoroutine(DrugSoundCoroutine(duration));
    }

    public void StopDrugSounds()
    {
        mixer.SetFloat("Drug", 0f);
    }

    IEnumerator DrugSoundCoroutine(float duration)
    {
        mixer.SetFloat("Drug", 1f);
        yield break;
    }

    void Start()
    {
        if (mixer == null)
            Debug.LogError("No audiomixer assigned.");
    }

    public void PlayAudioClip(AudioClip clip, Vector3 pos, bool looping)
    {
        print("Playing audioclip name " + clip.name);

        GameObject audioObj = new GameObject();
        audioObj.transform.position = pos;
        audioObj.name = "AudioObj " + clip.name;

        if (!looping)
        {
            Destroy(audioObj, clip.length);
        }

        AudioSource audioObjSource = audioObj.AddComponent<AudioSource>();
        currentSources.Add(audioObj);
        audioObjSource.loop = looping;
        audioObjSource.clip = clip;
        audioObjSource.maxDistance = 20;
        audioObjSource.minDistance = 3;
        audioObjSource.dopplerLevel = 0f;
        audioObjSource.rolloffMode = AudioRolloffMode.Linear;
        audioObjSource.spatialBlend = 1f;
        audioObjSource.Play();


    }

    public void PlayAmbientSound(AudioClip clip, bool loop)
    {
        print("Playing ambientsound name " + clip.name);

        GameObject audioObj = new GameObject();
        audioObj.transform.position = ambientSoundPos;
        audioObj.name = "AudioAmbient " + clip.name;

        AudioSource audioObjSource = audioObj.AddComponent<AudioSource>();
        currentSources.Add(audioObj);
        audioObjSource.loop = loop;
        audioObjSource.clip = clip;
        audioObjSource.maxDistance = 50;
        audioObjSource.minDistance = 1;
        audioObjSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioObjSource.spatialBlend = 1f;
        audioObjSource.Play();
    }


    public void RemoveAllAudio()
    {
        foreach(GameObject a in currentSources)
        {
            Destroy(a);
        }
    }

    public static AudioManager instance;
    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

    }

   
}
