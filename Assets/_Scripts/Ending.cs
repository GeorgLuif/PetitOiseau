using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ending : MonoBehaviour {
    private bool once = true;
    public Text endText;
    void Start()
    {
        endText.gameObject.SetActive(false);
    }
	// Use this for initialization
	void OnTriggerEnter (Collider c) {
	    if(c.gameObject.tag == "Animal" && once)
        {
            once = false;
            EndGame();
        }
	}
	
	// Update is called once per frame
	void EndGame () {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Animal");

        foreach (GameObject a in g)
            Destroy(a);

        endText.gameObject.SetActive(true);
    }
}
