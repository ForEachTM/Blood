using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : Singleton<Manager>
{

    public enum Scenes{ MENU, OPTIONS, STATS, GAME, PAUSE }

    Scenes currentScene;

    int score;

    public static string MENU = "Menu";
    public static string GAME = "Game";

    private void Awake()
    {
        SetSingleton();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            OnApplicationStart();
        }
    }

    private void OnApplicationStart()
    {
        Cursor.visible = false;
    }

    public static void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public int GetScore()
    {
        return score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
