using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Platform_Spawner_Tool : MonoBehaviour
{
    [SerializeField] int _spawnPlatforms;
    [SerializeField] GameObject leftPlatform, rightPlatform, finishPlatform;
    [SerializeField] Vector2 _nextPlatformSpawnsAt = new Vector2(0, 0);
    [ContextMenu("Generate Platforms")]
    void CreatePlatforms()
    {
        if (_spawnPlatforms > 0)
        {
            for (int i = 0; i < _spawnPlatforms - 1; i++)
            {
                switch (i % 2)
                {
                    case 1: // Spawn Right Platform
                        //Debug.Log("Right Platform Spawned At: " + _nextPlatformSpawnsAt.ToString());
                        GameObject RightPlatform = PrefabUtility.InstantiatePrefab(rightPlatform) as GameObject;
                        RightPlatform.transform.parent = gameObject.transform;
                        RightPlatform.transform.position = _nextPlatformSpawnsAt;
                        break;
                    default: // Spawn Left Platform
                        //Debug.Log("Left Platform Spawned At: " + _nextPlatformSpawnsAt.ToString());
                        GameObject LeftPlatform = PrefabUtility.InstantiatePrefab(leftPlatform) as GameObject;
                        LeftPlatform.transform.parent = gameObject.transform;
                        LeftPlatform.transform.position = _nextPlatformSpawnsAt;
                        break;
                }
                _nextPlatformSpawnsAt += new Vector2(0, 5f);
            }
        }

        GameObject LastPlatform = PrefabUtility.InstantiatePrefab(finishPlatform) as GameObject;
        //LastPlatform.transform.parent = gameObject.transform;
        LastPlatform.transform.position = _nextPlatformSpawnsAt;
        //Debug.Log("Finish Platform Spawned At: " + _nextPlatformSpawnsAt.ToString() + "Spawned as Child in Parent Object: " + gameObject.name);
        
    }

}
