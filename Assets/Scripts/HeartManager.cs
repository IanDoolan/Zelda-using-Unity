using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart; 
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;


    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue; i ++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    // Reflect damage on hearts
    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        for (int i = 0; i < heartContainers.initialValue; i ++)
        {
            //-1
           if (i <= tempHealth-1)
           {
               //Full Heart
               hearts[i].sprite = fullHeart;
           } else if (i >= tempHealth)
           {
               //Empty Heart
               hearts[i].sprite = emptyHeart;
           } else
           {
               //half heart
               hearts[i].sprite = halfFullHeart;
           }
        }
    }
}


//
//public void updateHearts() {
//        float tempHealth = playerCurrentHealth.RuntimeValue / 4;
//        for (int i = 0; i < heartContainers.initialValue; i++) {
//            float currHeart = Mathf.Ceil(tempHealth - 1);
//            if (i <= tempHealth - 1) {
//                //FullHeart
//                hearts[i].sprite = fullHeart;
//            } else if (i >= tempHealth) {
//                //emptyHeart
//                hearts[i].sprite = emptyHeart;
//            } else if(i == currHeart &&  (tempHealth % 1) == .50){
//                //Half full heart
//                hearts[i].sprite = halfHeart;
//            } else if (i == currHeart && (tempHealth % 1) == .25) {
//                //1/4 heart
//                hearts[i].sprite = quarterHeart;
//            } else /*(i == currHeart && (tempHealth % 1) == .75)*/ {
//                //3/4 heart
//                hearts[i].sprite = threeFourthHeart;
//            }
//        }
//    }