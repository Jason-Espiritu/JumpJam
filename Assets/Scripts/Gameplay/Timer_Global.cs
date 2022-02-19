using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Timer_Global : MonoBehaviour
{
    public Text UI_TEXT;

    public static Timer_Global Instance;

    [SerializeField] private GameManager GM;

    private float g_timer;

    public bool g_openPlatform;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.g_isGameStarted)
        {
            g_timer += Time.deltaTime;
            if (g_timer >= GM.g_BPS)
            {
                g_openPlatform = true; //Open
                g_timer = 0f;

            }else if (g_timer >= (GM.g_BPS / 2f))
            {
                g_openPlatform = false; //Close
            }

            UI_TEXT.text = Instance.g_timer.ToString();
        }
    }


}
