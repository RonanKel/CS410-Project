using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextScript : MonoBehaviour
{
    [SerializeField] float fadeDuration = 5f;
    float startTime;
    float startFadeTime;

    [SerializeField] Color textColor = new Color(255f, 255f, 255f, 255f);
    [SerializeField] Color finalColor = new Color(255f, 255f, 255f, 0f);

    [SerializeField] Material fontMaterial;
    TMP_Text text;

    bool isFading = false;
    // Start is called before the first frame update
    void Awake()
    {
        startTime = Time.time;
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime + 2f < Time.time && !isFading) {
            StartFade();
        }

        if (isFading) {
            if (startFadeTime + 5f < Time.time) {
                Reset();
            }
        }
    }

    void StartFade() {
        startFadeTime = Time.time;
        isFading = true;
        Debug.Log("StartFade");
    }

    void Reset() {
        text.color = textColor;
        startTime = 0f;
        startFadeTime = 0f;
        isFading = false;
        Debug.Log("End Fade");
        gameObject.SetActive(false);

    }

}
