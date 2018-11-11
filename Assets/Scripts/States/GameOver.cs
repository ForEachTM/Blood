using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    private float[] textPositions;

    public Text crates, best;

    int position = 1, prevPosition = 2;

    float timer, time;

    void Start()
    {
        timer = time = 2f;

        textPositions = new float[3];

        textPositions[0] = -best.transform.position.x;
        textPositions[1] = crates.transform.position.x;
        textPositions[2] = best.transform.position.x;

    }

    void Update()
    {
        
        crates.transform.position = Vector2.Lerp(crates.transform.position, new Vector2(textPositions[position], crates.transform.position.y), 0.5f);
        best.transform.position = Vector2.Lerp(best.transform.position, new Vector2(textPositions[prevPosition], best.transform.position.y), 0.5f);

        if (time <= 0) {

            time = timer;
            prevPosition = position;

            if (crates.transform.position.x <= textPositions[0] + 1)
            {
                crates.transform.position = new Vector2(textPositions[2], crates.transform.position.y);
            } else if (best.transform.position.x <= textPositions[0] + 1)
            {
                best.transform.position = new Vector2(textPositions[2], best.transform.position.y);
            }

            if (position > 0)
            {
                position--;
            }
            else
            {
                position = 1;
            }
        }
        else
        {
            time -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Manager.LoadLevel(Manager.GAME);
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Manager.LoadLevel(Manager.MENU);
        }
    }

    void OnEnable()
    {
        crates.text = "CRATES: " + Manager.Instance.GetScore();
    }

}
