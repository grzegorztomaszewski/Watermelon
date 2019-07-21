using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this script cannot function properly without GameUI
//let Unity know about it with this attribute
[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    public Slider sliderProgress;
    public Text pointsText;
    public int points;


    //we can get this instance from other scripts very easily
    public static GameController Instance { get; private set; }

    [SerializeField]
    private int knifeCount;

    [Header("Knife Spawning")]
    [SerializeField]
    private Vector2 knifeSpawnPosition;
    [SerializeField]
    //this will be a prefab of the knife. You will create the prefab later.
    private GameObject knifeObject;

    //reference to the GameUI on GameController's game object
    public GameUI GameUI { get; private set; }

    private void Awake()
    {
        //simple kind of a singleton instance (we're only in 1 scene)
        Instance = this;
        GameUI = GetComponent<GameUI>();
    }

    void Update()
    {

    }

    private void Start()
    {

        pointsText.text = "Points: " + points;
        //update the UI as soon as the game starts
        GameUI.SetInitialDisplayedKnifeCount(knifeCount);
        //also spawn the first knife
        SpawnKnife();
        
    }

    //this will be called from KnifeScript
    public void OnSuccessfulKnifeHit()
    {
        points++;
        pointsText.text = "Points: " + points;
        sliderProgress.value++;

        if (knifeCount > 0)
        {
            SpawnKnife();
        }
        else if (points == 8)
        {
            SceneManager.LoadScene("Level2");

        }
        else if (points == 16)
        {
            SceneManager.LoadScene("Level3");
        }
        else if (points == 24)
        {
            SceneManager.LoadScene("Level4");
        }
        else if (points == 32)
        {
            SceneManager.LoadScene("Level5");
        }
        else if (points == 40)
        {
            SceneManager.LoadScene("Level6");
        }
        else if (points == 48)
        {
            SceneManager.LoadScene("Level7");
        }
        else if (points == 56)
        {
            SceneManager.LoadScene("Level8");
        }
        else if (points == 64)
        {
            SceneManager.LoadScene("Level9");
        }
        else if (points == 72)
        {
            SceneManager.LoadScene("Level10");
        }
        else if (points == 80)
        {
            SceneManager.LoadScene("Level11");
        }
        else if (points == 88)
        {
            SceneManager.LoadScene("Level12");
        }
        else if (points == 96)
        {
            SceneManager.LoadScene("Level13");
        }
        else if (points == 104)
        {
            SceneManager.LoadScene("Level14");
        }
        else if (points == 112)
        {
            SceneManager.LoadScene("Level15");
        }
        else if (points == 120)
        {
            SceneManager.LoadScene("Level16");
        }
        else if (points == 128)
        {
            SceneManager.LoadScene("Level17");
        }
        else if (points == 136)
        {
            SceneManager.LoadScene("Level18");
        }
        else if (points == 144)
        {
            SceneManager.LoadScene("Level19");
        }
        else if (points == 152)
        {
            SceneManager.LoadScene("Level20");
        }
        else if (points == 160)
        {
            GameUI.ShowWinButton();
        }
    }

    private void SpawnKnife()
    {
        knifeCount--;
        Instantiate(knifeObject, knifeSpawnPosition, Quaternion.identity);
    }

    //the public method for starting game over
    public void StartGameOverSequence(bool win)
    {

        StartCoroutine("GameOverSequenceCoroutine", win);
    }

    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if (win)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        else
        {
            GameUI.ShowRestartButton();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        SceneManager.LoadScene("basic");
    }
    //public void PauseGame(bool On)
    //{
    //    if (On)
    //    {
    //        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
    //       // KnifeScript.instance.isActive = false;
    //    }
    //}

}