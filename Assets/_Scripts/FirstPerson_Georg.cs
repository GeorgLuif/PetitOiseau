using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]

public class FirstPerson_Georg : MonoBehaviour {

    public Vector2 mousePos;
    public Vector2 oldMousePos;
    public float differenceX;
    public float differenceY;

    public Vector3 rot;
    public Vector3 oldRot;

    public float rotSpeed = 0.5f;

    [Range(0.1f, 2f)]
    public float rotHorizontalPower = 0.5f;
    [Range(0.1f, 2f)]
    public float rotVerticalPower = 0.5f;


    //private CharacterController character;
    public Camera cam;
    [Range(0.1f, 5f)]
    public float speed = 1f;


    void Start()
    {
        //character = GetComponent<CharacterController>();
        rot = cam.transform.rotation.eulerAngles;
    }
	void LateUpdate () {
        HeadMovement();

	}

    void HeadMovement()
    {
        mousePos = Input.mousePosition;
        differenceX = mousePos.x - oldMousePos.x;
        differenceY = mousePos.y - oldMousePos.y;
        oldMousePos = mousePos;

        rot += new Vector3(-differenceY*rotHorizontalPower, differenceX* rotVerticalPower, 0);
        rot = new Vector3(Mathf.Clamp(rot.x, -25, 25), rot.y, rot.z);

        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(rot), Time.deltaTime* rotSpeed);
    }
}
