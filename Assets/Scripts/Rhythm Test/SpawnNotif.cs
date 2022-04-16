using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNotif : MonoBehaviour
{

    [SerializeField] private GameObject _parentGameObject;
    [SerializeField] private GameObject _notifText;

    public void SpawnNotification()
    {
        Instantiate(_notifText, _parentGameObject.transform);
    }
}
