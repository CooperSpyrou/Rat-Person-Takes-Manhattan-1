using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamMinigame : MonoBehaviour
{
    int randomButton;
    int randomPressAmount;
    int randomPressAmountMAX;

    private GameObject gm;

    private void Start()
    {
        randomButton = Random.Range(1, 3);
        randomPressAmount = 0;
        randomPressAmountMAX = Random.Range(10, 20);

        gm = GameObject.Find("GameManager");
        if (randomButton == 1)
        {
            print("FUCK1");
            gm.GetComponent<Gameplay>().minigameButton = "\"LEFT CLICK\"";
            print(gm.GetComponent<Gameplay>().minigameButton);
        }
        if (randomButton == 2)
        {
            print("FUCK2");
            gm.GetComponent<Gameplay>().minigameButton = "\"RIGHT CLICK\"";
            print(gm.GetComponent<Gameplay>().minigameButton);
        }
        if (randomButton == 3)
        {
            print("FUCK3");
            gm.GetComponent<Gameplay>().minigameButton = "\"SPACEBAR\"";
            print(gm.GetComponent<Gameplay>().minigameButton);
        }
        //gm.GetComponent<Gameplay>().MinigameText();

        gm.GetComponent<Gameplay>().MinigameTextUpdate();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && randomButton == 1)
        {
            SpamMinigame();
        }

        if (Input.GetMouseButtonDown(1) && randomButton == 2)
        {
            SpamMinigame();
        }


        if (Input.GetKeyDown(KeyCode.Space) && randomButton == 3)
        {
            SpamMinigame();
        }



        void SpamMinigame()
        {
            randomPressAmount += 1;
            if (randomPressAmount >= randomPressAmountMAX)
            {
                GameObject.Find("GameManager").GetComponent<Gameplay>().Next();
                gm.GetComponent<ShakeShit>().shakeDuration = 0;
                Destroy(transform.gameObject);
            }

            gm.GetComponent<ShakeShit>().shakeDuration = 0.25f;

            // If you want to change the random button each time
            //randomButton = Random.Range(1, 3);
        }
    }
}
