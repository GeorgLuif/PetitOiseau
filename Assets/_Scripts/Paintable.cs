using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Paintable : MonoBehaviour {

    public Image painting;
    public float paintingSpeed =1f;
    public float paintCooldown = 0.8f;
    private float timer = -1f;
    public Transform finishedPos;

    public AudioClip[] paintSounds;
    public AudioSource finishSound;

    public void Start()
    {
        painting.fillAmount = 0f;
    }

	public void Paint()
    {
        if (timer > 0)
            return;

        print("painting");
        timer = paintCooldown;

        painting.fillAmount += paintingSpeed * Time.deltaTime;
        CheckIfFinished();

        if(paintSounds.Length > 0)
        {
            int i = Random.Range(0, paintSounds.Length);
            GetComponent<AudioSource>().clip = paintSounds[i];
            GetComponent<AudioSource>().Play();
        }
        

    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    void CheckIfFinished()
    {
        if (painting.fillAmount == 1f)
        {
            if(finishSound!=null)
            finishSound.Play();

            painting.transform.position = finishedPos.position;
            painting.transform.rotation = finishedPos.rotation;
            print(transform.parent.parent.name + "is now indestructible");
            
            transform.parent.parent.parent = null;
            DontDestroyOnLoad(transform.parent.root);

            GetComponent<Renderer>().enabled = false;

            Destroy(this);
        }
    }
}
