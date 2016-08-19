using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
    private CharacterController characterController;
    private CharacterInteractions interactions;

    public void EnableMovement(bool yesno)
    {

        fpsController.enabled = yesno;
        characterController.enabled = yesno;
        interactions.enabled = yesno;
    }

    void Start()
    {
        fpsController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        characterController = GetComponent<CharacterController>();
        interactions = GetComponent<CharacterInteractions>();
    }

    // SINGLETON
    public static Character instance;
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
