using UnityEngine;
using System.Collections;
[RequireComponent(typeof(ReflectionProbe))]
public class ReflectionProbeLoader : MonoBehaviour {

    ReflectionProbe probe;
    public float refreshRate;
	void Start () {
        probe = GetComponent<ReflectionProbe>();
        print("Refreshing " + gameObject.name +" at rate "+refreshRate);
        InvokeRepeating("LoadProbe", 1.0f, refreshRate);
	}
	
	// Update is called once per frame
	void LoadProbe () {
        
        probe.RenderProbe();
	}
}
