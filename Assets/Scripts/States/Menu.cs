using System.Linq;
using UnityEngine;

public class Menu : MonoBehaviour
{
    int currentOption;

    public GameObject options;

    GameObject selection;

    GameObject[] menuOptions;

    private void OnEnable()
    {
        if(options != null)
            options.SetActive(false);
    }

    void Start()
    {
        selection = GameObject.FindGameObjectWithTag("Selection");
        menuOptions = GameObject.FindGameObjectsWithTag("MenuOptions").OrderBy(obj => -obj.transform.position.y).ToArray();
    }

    void Update()
    {

        ChangeOption();
        SelectOption();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        
    }

    void ChangeOption()
    {
        selection.transform.position = Vector2.Lerp(selection.transform.position, new Vector2(selection.transform.position.x, menuOptions[currentOption].transform.position.y - 0.2f), 0.4f);

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentOption > 0)
            {
                currentOption--;
            }
            else
            {
                currentOption = menuOptions.Length - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentOption < menuOptions.Length - 1)
            {
                currentOption++;
            }
            else
            {
                currentOption = 0;
            }
        }
    }

    void SelectOption()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentOption)
            {
                case 0: Manager.LoadLevel(Manager.GAME); break;

                case 1:
                    break;

                case 2:
                    gameObject.SetActive(false);
                    options.SetActive(true);
                    break;

                case 3:
                    break;

                case 4:
                    Application.Quit();
                    break;
            }

        }
    }
}
