using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNotif : MonoBehaviour
{
    // Start is called before the first frame update

    public Color BeginColor;
    public Color EndColor;
    public float TweenTime;
    public LeanTweenType Ease;
    public float Yposition;

    public TMP_Text textNotification;

    private Vector3 GameobjectPosition;
    void Start()
    {
        //Copies GameObject's Initial Position
        GameobjectPosition = gameObject.transform.position;
        //Get's jumpNotification
        string localjumpNotif = GameManager.GMInstance.g_jumpNotif;
        //Modified Jump Modification Text
        textNotification.text = localjumpNotif;
        
        StartCoroutine(NotifCoroutine());
    }

    IEnumerator NotifCoroutine()
    {
        Tween();
        yield return new WaitForSeconds(TweenTime + 0.25f);
        Destroy(gameObject);
    }

    public void Tween()
    {
        gameObject.transform.position = GameobjectPosition;

        LeanTween.moveLocalY(gameObject, Yposition, TweenTime)
            .setEase(Ease);

        LeanTween.value(gameObject, 0f, 1f, TweenTime)
            .setEase(Ease)
            .setOnUpdate( (value) => 
            {
                textNotification.color = Color.Lerp(BeginColor, EndColor, value);
            });
    }
}
