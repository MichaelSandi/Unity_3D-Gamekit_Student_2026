using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu(fileName = "InteractablesAudio", menuName = "Scriptable Objects/InteractablesAudio")]
public class InteractablesAudio : ScriptableObject
{
    //Declares a EventReference that holds a reference to our specific event in FMOD Studio. This allows us to easily reference and play the event in our code.
    [SerializeField] private EventReference pickupHealth, boxDestroy, activateLever, openDoor, drinkWater, pickupWaterBottle;

    //Declares a method that plays the pickupHealth event sound without any 3D-positioning
    public void PlayPickupHealth()
    {
        RuntimeManager.PlayOneShot(pickupHealth);
    }

    //Declares a method that plays the boxDestroy event sound at the position of the object that was destroyed.
    //The Transform parameter allows us to get the position of the object in the game world.
    public void PlayBoxDestroy(Transform objectTransform)
    {
        RuntimeManager.PlayOneShot(boxDestroy, objectTransform.position);
    }
    
    public void PlayActivateLever(Transform objectTransform)
    {
        RuntimeManager.PlayOneShot(activateLever, objectTransform.position);
    }
    
}