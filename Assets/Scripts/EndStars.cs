using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndStars : MonoBehaviour
{
    public Image starOne;
    public Image starTwo;
    public Image starThree;
    public float oneTime;
    public float twoTime;
    public float threeTime;
    public Color myColor;
    public GameEnding gameEnding;
    float endTime;

   private void Start()
   {
    myColor.a = 1;
    
    //gameEnding = FindObjectOfType<GameEnding>();
    endTime = gameEnding.currentTime;
    //Debug.Log("Current Time: " + gameEnding.currentTime);

    //One Star
    if(endTime > oneTime)
    {
        starOne.color = myColor;

        //Two Stars
        if(endTime > twoTime)
        {
            starTwo.color = myColor;

            //Three Stars
            if(endTime > threeTime)
            {
                starThree.color = myColor;
            }
        }

    }
   }

  
}
