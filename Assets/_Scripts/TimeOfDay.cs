using UnityEngine;
using System.Collections;

public class TimeOfDay : MonoBehaviour
{

    [Header("Rotates sun, handles dust")]

    

    [Range(.001f, .02f)]
    public float speed = 2f;
    public static TimeOfDay instance;
    public bool aktiv = false;
    public bool lookForDust = false;

    [Range(.0f, 0.9f)]
    public float turnOnDust;

    [Range(.3f, 0.9f)]
    public float turnOffDust;

    [Range(.3f, 0.9f)]
    public float darkenLight = 0.5f;
    public Light ambientLight;

    private float ambientLightStartIntensity = 0f;

    [Header("ReadOnly")]
    public float timeOfDay;
    public float angle = 0f;
    private Quaternion startRot;
    private GameObject[] dust;
    public bool dustActive = false;
    private float directionalStartIntensity;

    void Start()
    {
        if (lookForDust)
            LookForDust();

        startRot = transform.rotation;
        ambientLightStartIntensity = ambientLight.intensity;
        directionalStartIntensity = GetComponent<Light>().intensity;
    }


    void LookForDust()
    {
        
            dust = GameObject.FindGameObjectsWithTag("Dust");

            if (dust.Length == 0)
                Debug.LogWarning("No dust found.. Set tags?");
    
    }
    public void ResetRot()
    {
        transform.rotation = startRot;
        ambientLight.intensity = ambientLightStartIntensity;
        GetComponent<Light>().intensity = directionalStartIntensity;
    }

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

    void FixedUpdate()
    {

        transform.localEulerAngles -= new Vector3(angle, 0, 0)*Time.deltaTime*speed;

        // TURN ON / OFF DUST
        if (dust == null)
            return;


        timeOfDay = GameMaster.instance.timer / GameMaster.instance.dayDuration;    

        if (timeOfDay > turnOnDust)
            ActivateDust(true);

        if(timeOfDay > turnOffDust)
            ActivateDust(false);

        if(timeOfDay > darkenLight)
        {
            ambientLight.intensity = Mathf.Lerp(ambientLight.intensity, 0, Time.deltaTime * 0.1f);
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, 0, Time.deltaTime * 0.3f);
        }
        
    }

    void ActivateDust(bool yesno)
    {
        if (yesno && dustActive)
            return;

        if (!yesno && !dustActive)
            return;

        

        foreach (GameObject g in dust)
        {

            if (g == null)
                return;

            if (yesno)
                g.GetComponent<ParticleSystem>().Play();

            if (!yesno)
                g.GetComponent<ParticleSystem>().Stop();
        }

        dustActive = yesno;
    }
}
