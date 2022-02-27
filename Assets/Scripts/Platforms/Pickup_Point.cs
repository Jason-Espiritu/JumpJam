using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Point : MonoBehaviour
{
    [SerializeField] private Platform_Identity _platform_Identity;
    [SerializeField] private bool _isFinish;

    private void Start()
    {
        _isFinish = _platform_Identity.g_isFinalPlatform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_isFinish)
            {
                //Execute Result Screen Function
                //Debug.Log("Game Finished");
                GameManager.GMInstance.g_isGameEnded = true;
            }
            Debug.Log("+1");
            Point_System.PSinsttance.AddPoint();    
            Destroy(gameObject); //uncomment if done debugging
        }
    }
}
