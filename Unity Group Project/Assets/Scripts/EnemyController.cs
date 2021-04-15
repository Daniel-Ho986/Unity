using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private string phrase1;
    private string phrase2;
    private string phrase3;
    private string phrase4;
    private string phrase5;
    private string phrase6;
    private string phrase7;

    // Start is called before the first frame update
    void Start()
    {
        phrase1 = "Don't you go touching my data!!!";
        phrase2 = "You're messing up my data >:(";
        phrase3 = "No, no, NO!!! Hands off I say!!!";
        phrase4 = "My stuff!!! I can't believe it >:(";
        phrase5 = "Everything was fine 'till YOU showed up!";
        phrase6 = "I'm gonna get scrapped for this...*sad boop beep*";
        phrase7 = "That somehow looks better... *beep boop*";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //GetPhrase():
    //  - returns the enemy phrase depending on the number entered
    public string GetPhrase(int phraseNum)
    {
        if (phraseNum == 1) { return phrase1; }
        else if (phraseNum == 2) { return phrase2; }
        else if (phraseNum == 3) { return phrase3; }
        else if (phraseNum == 4) { return phrase4; }
        else if (phraseNum == 5) { return phrase5; }
        else if (phraseNum == 6) { return phrase6; }
        else if (phraseNum == 7) { return phrase7; }
        else { return "I'm speechless (literally)!"; }
    }


}
