using UnityEngine;
using System.Collections;


public class Drug : MonoBehaviour {

    public float incubationDuration = 20f;
    private UnityStandardAssets.ImageEffects.Vortex vortex;
    private UnityStandardAssets.ImageEffects.Bloom bloom;
    private UnityStandardAssets.ImageEffects.ColorCorrectionCurves curves;
    
    AudioClip drugSound;
    private float timer = -1f;
	void Start()
    {
        if (Camera.main == null)
            Debug.LogError("No main cam found!");

        if(Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Vortex>() == null)
            Debug.LogError("No Vortex effect on cam found!");

        if (Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Bloom>() == null)
            Debug.LogError("No bloom effect on cam found!");

        if (Camera.main.GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionCurves>() == null)
            Debug.LogError("No color curves effect on cam found!");

        vortex = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Vortex>();
        bloom = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Bloom>();
        curves = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionCurves>();
    }

	public void Use () {
        StartCoroutine(DrugEffects());
	}

    IEnumerator DrugEffects()
    {
        timer = incubationDuration;
        curves.enabled = true;
        AudioManager.instance.DrugSounds(incubationDuration);
        while(timer > 0)
        {
            timer -= Time.deltaTime;

            vortex.angle += Time.deltaTime;
            //bloom.bloomIntensity += Time.deltaTime * .3f;
            yield return 0;
        }

        vortex.angle = 0f;
        AudioManager.instance.StopDrugSounds();

        yield break;
    }
}
