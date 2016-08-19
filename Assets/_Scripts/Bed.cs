using UnityEngine;
using System.Collections;

public class Bed : MonoBehaviour {

    [TextArea(2,4)]
    public string question = "Do you really want to end your day?";

    private float timer = -0.1f;
    public float cooldown = 1f;
    private bool justpressed = false;

	public void UseBed()
    {
        if (justpressed)
            GameMaster.instance.EndDay();


        if (timer > 0)
            return;

        timer = cooldown;
        justpressed = true;         
        MonologueManager.instance.ShowMonologue(question, 5f, 0);

    }

    void FixedUpdate()
    {
        if (timer > 0f)
            timer -= Time.deltaTime;

        if (justpressed && timer < 0)
            justpressed = false;

    }
}
