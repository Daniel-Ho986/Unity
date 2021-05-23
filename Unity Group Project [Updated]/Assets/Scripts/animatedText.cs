using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class animatedText : MonoBehaviour
{
    //Time taken for each letter to appear 
    //(The lower it is, the faster each letter appear)
    public float letterPaused = 0.01f;

    //Message that will display till the end that will come out letter by letter
    public string message;

    //Text for the message to display
    public Text messageDisplay;
    

    public AudioSource typingAudio;

    //Start is called before the first frame update
    //Use this for initialization 
    void Start(){
        //Get text component
        messageDisplay = GetComponent<Text>();

        //Message will display will be at Text
        message = messageDisplay.text;

        //Set the text to be blank first
        messageDisplay.text = "";

        //Call the function and expect yield to return
        StartCoroutine (TypeText ());

        if (typingAudio == null)
            typingAudio = GetComponent<AudioSource>();

        PlayForTime(6.0f);
    }

    public void PlayForTime(float time){
        GetComponent<AudioSource>().Play();
        Invoke("StopAudio", time);
    }

    private void StopAudio(){
        GetComponent<AudioSource>().Stop();
    }

   
    


    IEnumerator TypeText(){
        //Split each char into a char array
        foreach (char letter in message.ToCharArray()){
          
            //Add 1 letter each
            messageDisplay.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPaused);
        }
    }
}
