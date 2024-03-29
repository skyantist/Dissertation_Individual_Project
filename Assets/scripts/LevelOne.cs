﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class LevelOne : MonoBehaviour
{
    //Tutorial references
    levelOneTutorial tutorial;

    //user input
    private InputField input;
    private string inputCopy;

    //correct answer
    public GameObject correctAnswerBox;
    public Text correctAnswerText;
    public Button correctAnswerDismissButton;
    public Text correctAnswerDismissButtonText;
    private float correctAnswerTimer = 2f;
    
    //objects in the scene
    public Button run;
    public Button reset;
    public GameObject player;

    //error prompting objects
    public GameObject errorBox;
    public Text errorTitle;
    public Text errorTitleUnderline;
    public Text errorMessage;
    public Button dismissErrorButton;
    public Text dissmissErrorButtonText;

    //store coordinatees
    private Vector3 movingPlatformPos;
    private Vector3 playerPos;

    //seperating the code into variables
    private string objectName;
    private string loopLength;

    //levelComplete
    private bool partOneDone = false;
    private bool partTwoDone = false;
    //private bool movingPlayerLeft = false;
    private bool movingPlayerRight = false;
    private bool movingPlayerLeftPartTwo = false;
    private bool movingPlayerRightPartTwo = false;

    // Use this for initialization
    void Start()
    {
        tutorial = GameObject.FindObjectOfType(typeof(levelOneTutorial)) as levelOneTutorial;
        input = GetComponent<InputField>();
        input.Select();
        input.ActivateInputField();
        run.onClick.AddListener(onRunClick);
        reset.onClick.AddListener(onResetClick);
        dismissErrorButton.onClick.AddListener(onDismissClick);
        correctAnswerDismissButton.onClick.AddListener(onCorrectAnswerDismiss);

        //Store original object coordinates
        playerPos = player.transform.position;

        //hide error elements
        errorBox.GetComponent<MeshRenderer>().enabled = false;
        errorTitle.GetComponent<Text>().enabled = false;
        errorTitleUnderline.GetComponent<Text>().enabled = false;
        errorMessage.GetComponent<Text>().enabled = false;
        dismissErrorButton.GetComponent<Button>().enabled = false;
        dissmissErrorButtonText.GetComponent<Text>().enabled = false;

        //correct answer
        correctAnswerBox.GetComponent<MeshRenderer>().enabled = false;
        correctAnswerText.GetComponent<Text>().enabled = false;
        correctAnswerDismissButton.GetComponent<Image>().enabled = false;
        correctAnswerDismissButtonText.GetComponent<Text>().enabled = false;

        //tutorial pop-ups
        tutorial.taskOne();
    }

    void Update()
    {
        if(partOneDone)
        {
            if (correctAnswerTimer >= 0f)
            {
                correctAnswerTimer -= Time.deltaTime;

                //show correct answer
                correctAnswerBox.GetComponent<MeshRenderer>().enabled = true;
                correctAnswerText.GetComponent<Text>().enabled = true;
                correctAnswerDismissButton.GetComponent<Image>().enabled = true;
                correctAnswerDismissButtonText.GetComponent<Text>().enabled = true;
            }
            else
            {
                //hide correct answer
                correctAnswerBox.GetComponent<MeshRenderer>().enabled = false;
                correctAnswerText.GetComponent<Text>().enabled = false;
                correctAnswerDismissButton.GetComponent<Image>().enabled = false;
                correctAnswerDismissButtonText.GetComponent<Text>().enabled = false;
            }
        }

        if(partTwoDone)
        {
            if (correctAnswerTimer >= 0f)
            {
                correctAnswerTimer -= Time.deltaTime;

                //show correct answer
                correctAnswerBox.GetComponent<MeshRenderer>().enabled = true;
                correctAnswerText.GetComponent<Text>().enabled = true;
                correctAnswerDismissButton.GetComponent<Image>().enabled = true;
                correctAnswerDismissButtonText.GetComponent<Text>().enabled = true;
            }
            else
            {
                //hide correct answer
                correctAnswerBox.GetComponent<MeshRenderer>().enabled = false;
                correctAnswerText.GetComponent<Text>().enabled = false;
                correctAnswerDismissButton.GetComponent<Image>().enabled = false;
                correctAnswerDismissButtonText.GetComponent<Text>().enabled = false;
            }
        }

        //Moving the player left/right: Part Two
        //right
        if (movingPlayerRight)
        {
            //Move up until for loop ends
            if (player.transform.position.x < playerPos.x + 2f)
            {
                //player.transform.position += Vector3.right * 1f * Time.deltaTime;
                player.transform.position = new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z);
                correctAnswerTimer = 2f;
            }
            //When coordinate is met, set it to that coordinate (ensuring it's an int)
            Debug.Log(player.transform.position.x >= playerPos.x + 2f);
            if (player.transform.position.x >= playerPos.x + 2f)
            {
                input.GetComponent<InputField>().interactable = true;
                player.transform.position = new Vector3(playerPos.x + 2f, player.transform.position.y, player.transform.position.z);
                Debug.Log(player.transform.position);
                movingPlayerRight = false;
                playerPos.x = playerPos.x + 2f;
                input.text = "";

                //Check if the player is in the correct position
                if (player.transform.position.x == -4f)
                {
                    Debug.Log("Part Two done! :D");

                    //tutorial pop-ups                                      
                    //input.GetComponent<InputField>().interactable = false;
                    partOneDone = true;
                    tutorial.hideTutorial();
                    tutorial.taskTwo();
                }
            }
        }

        //Moving the player left/right: Part Two
        //left
        if (movingPlayerLeftPartTwo)
        {
            input.GetComponent<InputField>().interactable = false;

            //Move down until for loop ends
            if (player.transform.position.x > playerPos.x - (Convert.ToInt32(loopLength) + 1))
            {
                player.transform.position += Vector3.left * 1f * Time.deltaTime;
                correctAnswerTimer = 2f;

                //Check if the player is in the exit position
                if (player.transform.position.x >= 2f)
                {
                    input.GetComponent<InputField>().interactable = true;
                    partTwoDone = true;                    
                    Application.LoadLevel("LevelTwo");
                    Debug.Log("Finished! :D");
                }
            }
            //When coordinate is met, set it to that coordinate (ensuring it's an int)
            Debug.Log(player.transform.position.x <= playerPos.x + (Convert.ToInt32(loopLength) + 1));
            if (player.transform.position.x <= playerPos.x - (Convert.ToInt32(loopLength) + 1))
            {
                input.GetComponent<InputField>().interactable = true;
                player.transform.position = new Vector3(playerPos.x - (Convert.ToInt32(loopLength) + 1), player.transform.position.y, player.transform.position.z);
                movingPlayerLeftPartTwo = false;
                playerPos.x = playerPos.x - (Convert.ToInt32(loopLength) + 1);
                Debug.Log(player.transform.position);
                loopLength = "0";
                //input.text = "";                
            }
        }

        //right
        if (movingPlayerRightPartTwo)
        {
            input.GetComponent<InputField>().interactable = false;

            //Move up until for loop ends
            if (player.transform.position.x < playerPos.x + (Convert.ToInt32(loopLength) + 1))
            {
                player.transform.position += Vector3.right * 1f * Time.deltaTime;
                correctAnswerTimer = 2f;

                //Check if the player is in the correct position
                if (player.transform.position.x >= 2f)
                {
                    partTwoDone = true;
                    Application.LoadLevel("LevelTwo");
                    Debug.Log("Finished! :D");
                }
                /*else
                {
                    movingPlayerRightPartTwo
                }*/
            }
            //When coordinate is met, set it to that coordinate (ensuring it's an int)
            Debug.Log(player.transform.position.x >= playerPos.x + (Convert.ToInt32(loopLength) + 1));
            if (player.transform.position.x >= playerPos.x + (Convert.ToInt32(loopLength) + 1))
            {
                input.GetComponent<InputField>().interactable = true;
                player.transform.position = new Vector3(playerPos.x + (Convert.ToInt32(loopLength) + 1), player.transform.position.y, player.transform.position.z);
                Debug.Log(player.transform.position);
                movingPlayerRightPartTwo = false;
                playerPos.x = playerPos.x + (Convert.ToInt32(loopLength) + 1);
                loopLength = "0";
                //input.text = "";             
            }
        }
    }

    void onRunClick()
    {
        //hide error/hint box
        errorBox.GetComponent<MeshRenderer>().enabled = false;
        errorTitle.GetComponent<Text>().enabled = false;
        errorTitleUnderline.GetComponent<Text>().enabled = false;
        errorMessage.GetComponent<Text>().enabled = false;
        dismissErrorButton.GetComponent<Button>().enabled = false;
        dissmissErrorButtonText.GetComponent<Text>().enabled = false;

        Debug.Log("Button was clicked!");

        if (movingPlayerRight == false && partOneDone == false)
        {
            movePlayer();
        }
        if(partOneDone)
        {
            Debug.Log("part two");
            movePlayerPartTwo();
        }
    }

    void onResetClick()
    {
        Application.LoadLevel("LevelOne");
    }

    void onDismissClick()
    {
        //hide error/hint box
        errorBox.GetComponent<MeshRenderer>().enabled = false;
        errorTitle.GetComponent<Text>().enabled = false;
        errorTitleUnderline.GetComponent<Text>().enabled = false;
        errorMessage.GetComponent<Text>().enabled = false;
        dismissErrorButton.GetComponent<Button>().enabled = false;
        dissmissErrorButtonText.GetComponent<Text>().enabled = false;
    }

    public void dismissError()
    {
        //hide error/hint box
        errorBox.GetComponent<MeshRenderer>().enabled = false;
        errorTitle.GetComponent<Text>().enabled = false;
        errorTitleUnderline.GetComponent<Text>().enabled = false;
        errorMessage.GetComponent<Text>().enabled = false;
        dismissErrorButton.GetComponent<Button>().enabled = false;
        dissmissErrorButtonText.GetComponent<Text>().enabled = false;
    }

    void onCorrectAnswerDismiss()
    {
        correctAnswerTimer = 0f;
    }

    void movePlayer()
    {
        inputCopy = input.text;
        Debug.Log("input copy inside player: " + inputCopy);
        Debug.Log("Moving player! :D");
        inputCopy = Regex.Replace(inputCopy, @"\s", string.Empty);  //remove spaces
        //Debug.Log(objectName);
        if (inputCopy == "")
        {
            Debug.Log("Input field is empty");

            //show error/hint box
            errorBox.GetComponent<MeshRenderer>().enabled = true;
            errorTitle.GetComponent<Text>().enabled = true;
            errorTitleUnderline.GetComponent<Text>().enabled = true;
            errorMessage.GetComponent<Text>().enabled = true;
            dismissErrorButton.GetComponent<Button>().enabled = true;
            dissmissErrorButtonText.GetComponent<Text>().enabled = true;

            errorMessage.text = "Input field is empty.";

        }
        else if (!inputCopy.Contains("box"))
        {
            Debug.Log("variable name does not exist");

            //show error/hint box
            errorBox.GetComponent<MeshRenderer>().enabled = true;
            errorTitle.GetComponent<Text>().enabled = true;
            errorTitleUnderline.GetComponent<Text>().enabled = true;
            errorMessage.GetComponent<Text>().enabled = true;
            dismissErrorButton.GetComponent<Button>().enabled = true;
            dissmissErrorButtonText.GetComponent<Text>().enabled = true;

            errorMessage.text = "The 'box' variable is missing.";
        }
        else if (inputCopy.Substring(inputCopy.Length - 1, 1) != ";")
        {
            //show error/hint box
            errorBox.GetComponent<MeshRenderer>().enabled = true;
            errorTitle.GetComponent<Text>().enabled = true;
            errorTitleUnderline.GetComponent<Text>().enabled = true;
            errorMessage.GetComponent<Text>().enabled = true;
            dismissErrorButton.GetComponent<Button>().enabled = true;
            dissmissErrorButtonText.GetComponent<Text>().enabled = true;

            errorMessage.text = "Are you missing a semicolon?";
        }
        else if (Regex.IsMatch(inputCopy, @"(box)+([.])+([x])+([+][=]||[=])+([2])+([;])") == false)
        {
            //show error/hint box
            errorBox.GetComponent<MeshRenderer>().enabled = true;
            errorTitle.GetComponent<Text>().enabled = true;
            errorTitleUnderline.GetComponent<Text>().enabled = true;
            errorMessage.GetComponent<Text>().enabled = true;
            dismissErrorButton.GetComponent<Button>().enabled = true;
            dissmissErrorButtonText.GetComponent<Text>().enabled = true;

            errorMessage.text = "Expression does not match.";
        }

        //Check if for loop if statement matches: moving player to the right
        if (Regex.IsMatch(inputCopy, @"(box)+([.])+([x])+([+][=]||[=])+([2])+([;])"))    //match regex box.x+=2;
        {
            Debug.Log("moving right");
            //Find the object name in the string
            int objectNamePos = inputCopy.IndexOf("{");
            objectName = inputCopy.Substring(objectNamePos + 1, inputCopy.Length - 7 - objectNamePos);

            Debug.Log("object name: " + objectName);

            //Check if correct variable name is used
            if (inputCopy.Contains("box"))
            {
                Debug.Log("variable name exists! :D");
                movingPlayerRight = true;
            }
        }
    }

    void movePlayerPartTwo()
    {
        inputCopy = input.text;
        Debug.Log("Part Two! :D");
        inputCopy = Regex.Replace(inputCopy, @"\s", string.Empty);  //remove spaces

        if (inputCopy == "")
        {
            Debug.Log("Input field is empty");

            //show error/hint box
            errorBox.GetComponent<MeshRenderer>().enabled = true;
            errorTitle.GetComponent<Text>().enabled = true;
            errorTitleUnderline.GetComponent<Text>().enabled = true;
            errorMessage.GetComponent<Text>().enabled = true;
            dismissErrorButton.GetComponent<Button>().enabled = true;
            dissmissErrorButtonText.GetComponent<Text>().enabled = true;

            errorMessage.text = "Input field is empty.";
        }
        else if (inputCopy.Substring(inputCopy.Length - 1, 1) != "}")
        {
            //show error/hint box
            errorBox.GetComponent<MeshRenderer>().enabled = true;
            errorTitle.GetComponent<Text>().enabled = true;
            errorTitleUnderline.GetComponent<Text>().enabled = true;
            errorMessage.GetComponent<Text>().enabled = true;
            dismissErrorButton.GetComponent<Button>().enabled = true;
            dissmissErrorButtonText.GetComponent<Text>().enabled = true;

            errorMessage.text = "Are you missing a curly bracket?";
        }
        else if ((Regex.IsMatch(inputCopy, @"for\(int(\w*)\s?=\s?[0]\s?\;\s*\1\s*[<]?=?\s*[1-9]\s*\;((\s*\1([++])\4)|(\s*\1\s*=\s*\1\s*[+/*-]\s*\d{1,15}))\s*\)\s*{(box\.([x])\-\-\;)*\s*}") == false)    //match regex player.y+=100;
        && (Regex.IsMatch(inputCopy, @"for\(int(\w*)\s?=\s?[0]\s?\;\s*\1\s*[<]?=?\s*[1-9]\s*\;((\s*\1([++])\4)|(\s*\1\s*=\s*\1\s*[+/*-]\s*\d{1,15}))\s*\)\s*{(box\.([x])\+\+\;)*\s*}") == false))    //match regex player.y+=100;
        {
            //show error/hint box
            errorBox.GetComponent<MeshRenderer>().enabled = true;
            errorTitle.GetComponent<Text>().enabled = true;
            errorTitleUnderline.GetComponent<Text>().enabled = true;
            errorMessage.GetComponent<Text>().enabled = true;
            dismissErrorButton.GetComponent<Button>().enabled = true;
            dissmissErrorButtonText.GetComponent<Text>().enabled = true;

            errorMessage.text = "Expression does not match.";
        }

        //Check if for loop if statement matches: moving platform going down
        if (Regex.IsMatch(inputCopy, @"for\(int(\w*)\s?=\s?[0]\s?\;\s*\1\s*[<]?=?\s*[1-9]\s*\;((\s*\1([++])\4)|(\s*\1\s*=\s*\1\s*[+/*-]\s*\d{1,15}))\s*\)\s*{(box\.([x])\-\-\;)*\s*}"))    //match regex player.y+=100;
        {
            //Find the object name in the string
            int objectNamePos = inputCopy.IndexOf("{");
            objectName = inputCopy.Substring(objectNamePos + 1, inputCopy.Length - 7 - objectNamePos);
            Debug.Log("object name: " + objectName);

            //Find how long the loop will run for in the string
            int loopLengthPos = inputCopy.IndexOf("<");
            loopLength = inputCopy.Substring(loopLengthPos + 1, 1);

            Debug.Log("loop length: " + loopLength);

            //Check if correct variable name is used
            if (inputCopy.Contains("box"))
            {
                Debug.Log("variable name exists! :D");
                movingPlayerLeftPartTwo = true;
            }
        }
        //Check if for loop if statement matches: moving platform going up
        else if (Regex.IsMatch(inputCopy, @"for\(int(\w*)\s?=\s?[0]\s?\;\s*\1\s*[<]?=?\s*[1-9]\s*\;((\s*\1([++])\4)|(\s*\1\s*=\s*\1\s*[+/*-]\s*\d{1,15}))\s*\)\s*{(box\.([x])\+\+\;)*\s*}"))    //match regex player.y=100;
        {
            //Find the object name in the string
            int objectNamePos = inputCopy.IndexOf("{");
            objectName = inputCopy.Substring(objectNamePos + 1, inputCopy.Length - 7 - objectNamePos);

            //Find how long the loop will run for in the string
            int loopLengthPos = inputCopy.IndexOf("<");
            loopLength = inputCopy.Substring(loopLengthPos + 1, 1);

            Debug.Log("object name: " + objectName);
            Debug.Log("loop length: " + loopLength);

            //Check if correct variable name is used
            if (inputCopy.Contains("box"))
            {
                Debug.Log("variable name exists! :D");
                movingPlayerRightPartTwo = true;                
            }
        }
    }  
}
