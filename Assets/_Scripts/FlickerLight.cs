using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Light))]
public class FlickerLight : MonoBehaviour {

    public float min = 0.5f;
    public float max = 1.5f;
    private Light l;
    public float changeInterval = 3;

    private float timer = -1f;

	void Start () {
        l = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if(timer < 0)
        {
            timer = changeInterval / 10;
        }

        timer -= Time.deltaTime;

        l.intensity = Random.Range(min, max);
	}
}
