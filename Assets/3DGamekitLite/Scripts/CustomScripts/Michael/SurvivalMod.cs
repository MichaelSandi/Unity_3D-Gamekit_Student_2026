using UnityEngine;
using Gamekit3D;

public class SurvivalMod : MonoBehaviour
{
    [SerializeField] private float thirstLevel = 100f;
    [SerializeField] private float thirstSpeed = 10f;
    [SerializeField] private float timer = 10f;

    [SerializeField] private bool thirsty;
    [SerializeField] private bool parched;

    public int waterBottles = 0;

    [SerializeField] private GameObject playerObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*Subtracting the current value of thirstLevel with thirstSpeed.
         thirstSpeed is normalizes by using Time.deltaTIme, so that it's frame rate independent.
         Math.Clamp resrticts the thirstLevel between its current may value and 0.
        */
        
        
        thirstLevel -= Mathf.Clamp(thirstSpeed*Time.deltaTime,0, thirstLevel);


        if (thirstLevel <= 50f && !thirsty)
        {
            Debug.Log("I'm getting thirsty...");
            thirsty = true;
        }

        if (thirstLevel <= 0 && thirsty)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ThirstDamage();
                timer = 10f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && waterBottles > 0)
        {
            DrinkWaterBottle();
        }
    }

    private void ThirstDamage()
    {
        Damageable.DamageMessage msg = new Damageable.DamageMessage()
        {
            amount = 1,
            damager = this,
            damageSource = new Vector3(0, 0, 0),
            direction = new Vector3(0, 0, 0),
            throwing = false,
            stopCamera = false,
        };
        playerObject.GetComponent<Damageable>().ApplyDamage(msg);
        Debug.Log("Damage by Thirst!");
    }

    private void ResetThirst()
    {
        thirstLevel = 100f;
        thirsty = false;
        parched = false;
        Debug.Log("I'm not thirsty anymore!");
    }

    public void AddWaterBottleOnDestroyedBox()
    {
        int amountOfWaterBottles = Random.Range(0, 3);
        waterBottles += amountOfWaterBottles;
        Debug.Log("You found "+ amountOfWaterBottles + " water bottles! Total water bottles: " + waterBottles);
    }

    private void DrinkWaterBottle()
    {
        waterBottles--;
        ResetThirst();
        Debug.Log("You drank a water bottle! Total water bottles: " + waterBottles);
    }
}