using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Launch.cs - Handles Seed Launch and Collision 
// Worked on by Zachary Hubbard and Tessla Muir and contributions are cited as such.
public class Launch : MonoBehaviour
{
    // Variables on lines 10 - 13 implemented by Tessla Muir
    [SerializeField] Slider powerBar;
    [SerializeField] Slider verticalBar;
    bool hasLaunched = false;

    // Variables on lines 16 - 29 implemented by Zachary Hubbard
     bool hasLanded = false;
    private float posBeforeLaunch;
    private float posAfterLaunch;
    private int WindSpeed;
    [SerializeField] TextMeshProUGUI DistanceTxt;
    [SerializeField] TextMeshProUGUI WindSpeedTxt;
    GameObject LaunchTxt;
    public AudioSource SeedLaunch;
    public AudioSource SeedThud;
    [SerializeField] GameObject seed;
    [SerializeField] Rigidbody2D seedRb;
    private WinAndLose winAndLose;
    public GameObject GrownPlant;

    void Awake()
    {
        InitializeSlider();
    }

    private void InitializeSlider() // InitializeSlider Function in awake Implemented by 2023 Game Jam member Tessla Muir (Lines 38-44)
    {
        Slider[] array = GameObject.FindObjectsOfType<Slider>();
        foreach (var slider in array)
        {
            if (slider.name == "Power Slider") powerBar = slider;
            if (slider.name == "Vertical Slider") verticalBar = slider;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Variables on lines 10 - 13 implemented by Tessla Muir
        seed = GameObject.Find("Seed");
        seedRb = seed.GetComponent<Rigidbody2D>();
        powerBar.value = 0;
        powerBar.maxValue = 100;
        verticalBar.value = 0;
        verticalBar.maxValue = 100; 

        // Variables on lines 10 - 13 implemented by Zachary Hubbard
        winAndLose = GameObject.Find("GameManager").GetComponent<WinAndLose>();
        LaunchTxt = GameObject.Find("LaunchTxt");
        WindSpeed = Random.Range(0, 10);
        WindSpeedTxt.text = "Wind Speed: " + WindSpeed.ToString() + " MPH";
        posBeforeLaunch = seed.transform.position.x;
        posAfterLaunch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        LaunchSeed();
        SeedCollision();
    }

    // LaunchSeed function Implemented by 2023 Game Jam member Tessla Muir (Lines 71-121)
    private void LaunchSeed()
    {
        if (powerBar != null && !hasLaunched)
        {
            // More power
            if (Input.GetKeyDown(KeyCode.D) && powerBar.value < powerBar.maxValue)
            {
                powerBar.value += 5;
            }
            // Less power
            else if (Input.GetKeyDown(KeyCode.A) && powerBar.value > 0)
            {
                powerBar.value -= 5;
            }
        }

        if (verticalBar != null && !hasLaunched)
        {
            // More vertical
            if (Input.GetKeyDown(KeyCode.W) && verticalBar.value < verticalBar.maxValue)
            {
                verticalBar.value += 5;
            }
            // Less vertical
            else if (Input.GetKeyDown(KeyCode.S) && verticalBar.value > 0)
            {
                verticalBar.value -= 5;
            }
        }

        if (powerBar.value > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !hasLaunched)
            {
                // Make gravity applied
                hasLaunched = true;
                seedRb.simulated = true;

                // Launch
                float yValue = verticalBar.value / verticalBar.maxValue;
                seedRb.AddForce(new Vector2(powerBar.value * (1 - yValue) * 1.1f, powerBar.value * yValue * 0.8f), ForceMode2D.Impulse);
                SeedLaunch.Play();

                // Disable bar UI
                LaunchTxt.gameObject.SetActive(false);
                powerBar.gameObject.SetActive(false);
                verticalBar.gameObject.SetActive(false);
            }
        }
    }

    // SeedCollision Function created by Zachary Hubbard as part of collision implementation (Lines 124-138)
    public void SeedCollision()
    {
        // If seed has landed
        if (hasLaunched && !hasLanded && seedRb.velocity.x <= 0.3 && seedRb.velocity.y <= 0.3)
        {
            // play sound
            SeedThud.Play();
            GetGroundType();
            hasLanded = true;
        }

        posAfterLaunch = seed.transform.position.x;
        int distance = (int)(posAfterLaunch - posBeforeLaunch);
        DistanceTxt.text = "Distance: " + distance.ToString() + " ft";
    }

    // Get GroundType Function Created by Zachary Hubbard (Lines 140 - 162)
    // Determines wether seed has landed on fertile or infertile Ground
    public void GetGroundType()
    {   // Gets a list of all colliders in radius of seed
        Collider2D[] landsOn = Physics2D.OverlapCircleAll(seed.transform.position, (float)0.75);

        foreach (Collider2D collider in landsOn) // Go through each collider
        {
            if (collider.tag == "FertileGround") // if an object with a collider (the ground object) has the fertile tag
            {
                // Grow the plant and trigger a level win
                Debug.Log("You Landed on Fertile Ground!!!");
                GrowPlant();
                winAndLose.Win();
            }
            if (collider.tag == "SparseGround") // if an object with a collider (the ground object) has the sparse tag
            {
                // Do not grow the plant and trigger a level loss
                Debug.Log("You Landed on Sparse Ground!!!");
                winAndLose.Lose();
            }
        }
    }

    /* GrowPlant Function Created by Zachary Hubbard (Lines 167-173)
    Sets the seed sprite to that of the grown plant if landing on fertile ground
    Grows plant if on fertile ground */
    public void GrowPlant()
    {
        seed.gameObject.SetActive(false);
        GrownPlant.transform.position = seed.transform.position;
        GrownPlant.SetActive(true);
    }
}