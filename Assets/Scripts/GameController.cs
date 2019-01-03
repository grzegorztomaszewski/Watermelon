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

            // StartGameOverSequence(true);
        }
        else if (points == 16)
        {
            SceneManager.LoadScene("Level3");
            // StartGameOverSequence(true);
        }
        //else if (points == 24)
        //{
        //    SceneManager.LoadScene("Level4");
        //    // StartGameOverSequence(true);
        //}
    }

    //a pretty self-explanatory method
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

    //this is a coroutine because we want to wait for a while when the player wins
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
        //restart the scene by reloading the currently active scene

        //SceneManager.LoadScene(SceneManager.GetActiveScene()., LoadSceneMode.Single);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }



    
}