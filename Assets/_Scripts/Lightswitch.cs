using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
[ExecuteInEditMode]
public class Lightswitch : MonoBehaviour {

    public float offRot = 60f;
    public float onRot = 130f;
    public bool state = false;
    private bool previousState = false;

    public GameObject[] lights;

    public GameObject switchObject;

    void Update()
    {
     if(state != previousState)
        {
            previousState = state;
            UseSwitch();
        }   
    }

    public void ChangeState()
    {
        state = !state;
    }

    public void UseSwitch()
    {
        print("Using switch");

        ActivateLights(state);

        if (switchObject == null)
            return;

        Vector3 s = switchObject.transform.localEulerAngles;
        float targetRot = 0;
        

        if (state)
            targetRot = onRot;

        if (!state)
            targetRot = offRot;

        

        switchObject.transform.localRotation = Quaternion.Euler(new Vector3(s.x, s.y, targetRot));
    }

    void ActivateLights(bool yesno)
    {
        foreach(GameObject l in lights)
        {
            l.SetActive(yesno);
        }

        if (GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Play();
    }
}
