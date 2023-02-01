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
    public TextMeshProUGUI DistanceTxt;
    private float posBeforeLaunch;
    private float posAfterLaunch;
    [SerializeField] GameObject seed;
    [SerializeField] Rigidbody2D seedRb;

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
        posBeforeLaunch = seed.transform.position.x;
        posAfterLaunch = 0;
        seed = GameObject.Find("Seed");
        seedRb = seed.GetComponent<Rigidbody2D>();
        powerBar.value = 0;
        powerBar.maxValue = 100;
        verticalBar.value = 0;
        verticalBar.maxValue = 100;
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Make gravity applied
                hasLaunched = true;
                seedRb.simulated = true;

                // Launch
                float yValue = verticalBar.value/verticalBar.maxValue;
                seedRb.AddForce(new Vector2(powerBar.value * (1 - yValue) * 0.8f, powerBar.value * yValue * 0.5f), ForceMode2D.Impulse);

                // Disable bar UI
                powerBar.gameObject.SetActive(false);
                verticalBar.gameObject.SetActive(false);
            }
        }

        if(hasLaunched && !hasLanded && seedRb.velocity.x <= 0 && seedRb.velocity.y <= 0 )
        {
            posAfterLaunch = seed.transform.position.x;
            Debug.Log(posAfterLaunch);
            int distance = (int) (posAfterLaunch - posBeforeLaunch);
            Debug.Log(distance);
            DistanceTxt.text = "Distance: " + distance.ToString();
            hasLanded = true;
        }
    }
}
