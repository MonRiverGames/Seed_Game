using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Launch : MonoBehaviour
{
    [SerializeField] Slider powerBar;
    [SerializeField] Slider verticalBar;
    bool hasLaunched = false;
    bool hasLanded = false;
    [SerializeField] TextMeshProUGUI DistanceTxt;
    [SerializeField] TextMeshProUGUI WindSpeedTxt;
    private float posBeforeLaunch;
    private float posAfterLaunch;
    private int WindSpeed;
    public AudioSource SeedLaunch;
    public AudioSource SeedThud;
    [SerializeField] GameObject seed;
    [SerializeField] Rigidbody2D seedRb;
    private WinAndLose winAndLose;

    void Awake()
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
        seed = GameObject.Find("Seed");
        seedRb = seed.GetComponent<Rigidbody2D>();
        winAndLose = GameObject.Find("GameManager").GetComponent<WinAndLose>();
        posBeforeLaunch = seed.transform.position.x;
        posAfterLaunch = 0;
        powerBar.value = 0;
        powerBar.maxValue = 100;
        verticalBar.value = 0;
        verticalBar.maxValue = 100;
        WindSpeed = Random.Range(0, 10);
        WindSpeedTxt.text = "Wind Speed: " + WindSpeed.ToString() + " MPH";
    }

    // Update is called once per frame
    void Update()
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
                powerBar.gameObject.SetActive(false);
                verticalBar.gameObject.SetActive(false);
            }
        }

        if (hasLaunched && !hasLanded && seedRb.velocity.x <= 0.3 && seedRb.velocity.y <= 0.3)
        {
            SeedThud.Play();
            posAfterLaunch = seed.transform.position.x;
            int distance = (int)(posAfterLaunch - posBeforeLaunch);
            DistanceTxt.text = "Distance: " + distance.ToString();
            GetGroundType();
            hasLanded = true;
            
        }
    }
    public void GetGroundType()
    {
        Collider2D[] landsOn = Physics2D.OverlapCircleAll(seed.transform.position, (float) 0.75);

        foreach(Collider2D collider in landsOn)
        {
            if(collider.tag == "FertileGround")
            {
                Debug.Log("You Landed on Fertile Ground!!!");
                winAndLose.Win();
            }
            if (collider.tag == "SparseGround")
            {
                Debug.Log("You Landed on Sparse Ground!!!");
                winAndLose.Lose();
            }

        }

    }
}
