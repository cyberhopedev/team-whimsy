using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InfectionMeter : MonoBehaviour
{
    [Header("Meter Stuff")]
    // Meter 
    [SerializeField] Slider meter;
    private float currAmt;
    [Range(1, 20)]
    [SerializeField] float timeLimitMins = 10f;
    private float timeLimitSeconds;
    // Player effects
    private float currPlayerSpeed;
    private float maxPlayerSpeed;
    [Header("Interference Settings")]
    [SerializeField] float minJitterInterval = 15f;
    [SerializeField] float maxJitterInterval = 3f;

    [SerializeField] float jitterIntensity = 50f; // pixels

    [SerializeField] float driftIntensity = 70f; // pixels per second

    [SerializeField] float minSpeedMultiplier = 0.4f; // player ends at 40% speed
    [Header("Cursor Lag")]
    [SerializeField] private float minLag = 0f;   // no lag at start
    [SerializeField] private float maxLag = 0.85f; // heavy lag at high infection

    private Vector2 smoothedCursorPos;

    private float nextJitterTime;
    private bool drifting = false;
    private Vector2 driftDirection;

    [SerializeField] private float driftJitterCutoff = 0.2f; // 20%
    private bool pause = true;

    public static InfectionMeter Instance;

    private void Awake()
    {
        // Singleton instance
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set up meter
        currAmt = 0;
        timeLimitSeconds = timeLimitMins * 60;
        // Set up player speed
        maxPlayerSpeed = PlayerController.Instance.GetPlayerSpeed();
    }

    // Public setter for pause
    public void PauseInfection(bool yesOrNo)
    {
        pause = yesOrNo;
    }

    // Gets worse rogressively (in a log like fashion, fast change at first and then levels off)
    float InfectionCurve(float t)
    {
        // t = currAmt (0 → 1)
        float logValue = Mathf.Log(1 + t * 9f); 
        return logValue / Mathf.Log(10f); // normalize to 0–1
    }

    void Update()
    {

        if (!pause)
        {
            // Infection meter
            currAmt += Time.deltaTime / timeLimitSeconds;
            currAmt = Mathf.Clamp01(currAmt);
            meter.value = currAmt;

            // Check for lose condition
            if (currAmt >= 1)
            {
                PauseInfection(true);
                DialogueManager.Instance.gameOver = true;
                DialogueManager.Instance.TriggerWhim("Lose");
            }

            // Player slowdown
            ApplyPlayerSlowdown();

            // Cursor effects
            if (currAmt < driftJitterCutoff)
            {
                HandleCursorJitter();
                HandleCursorDrift();
            }
            else
            {
                ApplyCursorLag();
            }   
        }
    }


    void ApplyPlayerSlowdown()
    {
        float curve = InfectionCurve(currAmt);
        float multiplier = Mathf.Lerp(1f, 0.3f, curve);
        PlayerController.Instance.SetPlayerSpeed(maxPlayerSpeed * multiplier);
    }


    void HandleCursorJitter()
    {
        float intensity = jitterIntensity;
        float curve = InfectionCurve(currAmt);

        // Frequency increases fast at first, then levels out
        float interval = Mathf.Lerp(minJitterInterval, maxJitterInterval, curve);

        if (Time.time < nextJitterTime) return;
        nextJitterTime = Time.time + interval;

        Vector2 currentPos = Mouse.current.position.ReadValue();
        Vector2 offset = Random.insideUnitCircle * intensity;
        Mouse.current.WarpCursorPosition(currentPos + offset);
    }


    void HandleCursorDrift()
    {
        float curve = InfectionCurve(currAmt);

        // Chance increases sharply early, then flattens
        float chance = Mathf.Lerp(0.01f, 0.8f, curve);

        if (!drifting && Random.value < chance * Time.deltaTime)
        {
            drifting = true;
            driftDirection = Random.insideUnitCircle.normalized;
            Invoke(nameof(StopDrift), 2f);
        }

        if (drifting)
        {
            Vector2 currentPos = Mouse.current.position.ReadValue();
            Vector2 newPos = currentPos + driftDirection * driftIntensity * Time.deltaTime;
            Mouse.current.WarpCursorPosition(newPos);
        }
    }


    void StopDrift()
    {
        drifting = false;
    }

    void ApplyCursorLag()
    {
        float curve = InfectionCurve(currAmt); // your log-shaped curve

        // How much lag to apply (0 = instant, 1 = extremely slow)
        float lagAmount = Mathf.Lerp(minLag, maxLag, curve);

        Vector2 rawPos = Mouse.current.position.ReadValue();

        // First frame initialization
        if (smoothedCursorPos == Vector2.zero)
            smoothedCursorPos = rawPos;

        // Smooth toward the real cursor position
        smoothedCursorPos = Vector2.Lerp(rawPos, smoothedCursorPos, lagAmount);

        // Apply the lagged position
        Mouse.current.WarpCursorPosition(smoothedCursorPos);
    }
}
