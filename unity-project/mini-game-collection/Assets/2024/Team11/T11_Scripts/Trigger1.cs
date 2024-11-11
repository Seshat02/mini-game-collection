using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Trigger1 : MonoBehaviour

{
    public int score = 0;
    public TextMeshProUGUI text1;

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
            score++;
        }

    }

           
        

}