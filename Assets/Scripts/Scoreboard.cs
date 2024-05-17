using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] int level = 1;


    [SerializeField] List<TMP_Text> scoreCards;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    int Count() {
        int i = 0;
        while (PlayerPrefs.HasKey(ToStringFormat(level, i))) {
            i++;
        }
        return i;
    }

    List<float> Read() {
        int i = 0;
        List<float> times = new List<float>();
        while (PlayerPrefs.HasKey(ToStringFormat(level, i))) {
            times.Add(PlayerPrefs.GetFloat(ToStringFormat(level, i)));
            i++;
        }
        return times;
    }

    string ToStringFormat(int level, int place) {
        return (level.ToString() + "_" + place.ToString());
    }

    public void SaveScore(float score) {
        List<float> times;
        times = Read();
        times.Add(score);
        times = BubbleSortIncreasing(times);

        

        for (int i = 0; i < times.Count; i++) {
            PlayerPrefs.SetFloat(ToStringFormat(level, i), times[i]);
            Debug.Log(ToStringFormat(level, i));
            Debug.Log(times[i]);
        }
        PlayerPrefs.Save();
    }

    [ContextMenu("DeleteScores")]
    public void DeleteScores() {
        PlayerPrefs.DeleteAll();
    }

    List<float> BubbleSortIncreasing(List<float> times) {
        int i;
        int j;
        bool swapped;
        int len = times.Count;

        for (i = 0; i < len - 1; i++) {
            swapped = false;
            for (j = 0; j < len - i - 1; j++) {
                if (times[j] > times[j+1]) {
                    float tmp = times[j];
                    times[j] = times[j+1];
                    times[j+1] = tmp;
                    swapped = true;
                }
            }
            if (!swapped) {
                break;
            }
        }

        return times;
    }

    void OnEnable() {
        Display();
        //InvokeNextFrame(Display);
    }
    /*void OnDisable() {
        //!!! MUST DELETE TO SAVE SCORES BETWEEN GAMES
        DeleteScores();
    }*/

    void Display() {
        List<float> times = Read();
        if (times.Count > 0) {
            for (int i = 0; i < scoreCards.Count; i++) {
                if (i < times.Count) {
                    TimeSpan time = TimeSpan.FromSeconds(times[i]);
                    scoreCards[i].text = "#" + (i+1) + " ....... " + time.ToString(@"mm\:ss\:fff");
                }
                else {
                    scoreCards[i].text = "";
                }
            }
        }
        else {
            Debug.Log("Nothing to display");
        }
    }

    /*public void InvokeNextFrame(Action function)
	{
		try
		{
			StartCoroutine(_InvokeNextFrame(function));	
		}
		catch
		{
			Debug.Log ("Trying to invoke " + function.ToString() + " but it doesnt seem to exist");	
		}			
	}
	
	private IEnumerator _InvokeNextFrame(Action function)
	{
		yield return null;
		function();
	}*/
}
