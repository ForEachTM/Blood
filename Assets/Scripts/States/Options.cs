using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    int currentVerticalOption, currentHorizontalOption;

    bool horizontalInputAvaliable;

    public GameObject menu; 

    GameObject selection;

    GameObject[] optionsOptions;

    Text[] optionsText;

    Resolution[] resolutions;

    public GameObject[] arrows;

    void Start()
    {
        resolutions = Screen.resolutions;

        selection = GameObject.FindGameObjectWithTag("Selection");
        optionsOptions = GameObject.FindGameObjectsWithTag("OptionsOptions").OrderBy(obj => -obj.transform.position.y).ToArray();

        optionsText = new Text[optionsOptions.Length];

        for(int i = 0; i < optionsOptions.Length; i++)
        optionsText[i] = optionsOptions[i].GetComponent<Text>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].width == Screen.width)
            {
                currentHorizontalOption = i;
                break;
            }
        }

    }

    void Update()
    {
        ChangeOption();
        SelectOption();
        UpdateText();

        horizontalInputAvaliable = currentVerticalOption == 3 ? true : false;
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            menu.SetActive(true);
        }
    }

    void UpdateText()
    {
        optionsText[2].text = "FULLSCREEN: " + Value(Screen.fullScreen);
        optionsText[3].text = resolutions[currentHorizontalOption].width + ":" + resolutions[currentHorizontalOption].height;

        foreach(GameObject arrow in arrows)
        {
            arrow.SetActive(horizontalInputAvaliable);
        }
    } 

    void ChangeOption()
    {
        selection.transform.position = Vector2.Lerp(selection.transform.position, new Vector2(selection.transform.position.x, optionsOptions[currentVerticalOption].transform.position.y - 0.2f), 0.4f);

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentVerticalOption > 0)
            {
                currentVerticalOption--;
            }
            else
            {
                currentVerticalOption = optionsOptions.Length - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentVerticalOption < optionsOptions.Length - 1)
            {
                currentVerticalOption++;
            }
            else
            {
                currentVerticalOption = 0;
            }
        }

        if (horizontalInputAvaliable)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentHorizontalOption > 0)
                {
                    currentHorizontalOption--;
                }
                else
                {
                    currentHorizontalOption = resolutions.Length - 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentHorizontalOption < resolutions.Length - 1)
                {
                    currentHorizontalOption++;
                }
                else
                {
                    currentHorizontalOption = 0;
                }
            }
        }
    }

    void SelectOption()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentVerticalOption)
            {
                case 0:
                    break;

                case 1: 
                    break;

                case 2:
                    Screen.fullScreen = !Screen.fullScreen;
                    break;

                case 3:
                    Screen.SetResolution(resolutions[currentHorizontalOption].width, resolutions[currentHorizontalOption].height, Screen.fullScreen);
                    break;

                case 4:
                    gameObject.SetActive(false);
                    menu.SetActive(true);
                    break;
            }

        }
    }

    string Value(bool value)
    {
        return value ? "ON" : "OFF";
    }
}
