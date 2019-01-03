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

    private void Start()
    {
       
        //update the UI as soon as the game starts
        GameUI.SetInitialDisplayedKnifeCount(knifeCount);
        //also spawn the first knife
        SpawnKnife();
        
    }
   
    //this will be called from KnifeScript
    public void OnSuccessfulKnifeHit()
    {

        points++;
        if (knifeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            StartGameOverSequence(true);
        }
    }

    //a pretty self-explanatory method
    private void SpawnKnife()
    {
        pointsText.text = "Points: " + points;
        knifeCount--;
        Instantiate(knifeObject, knifeSpawnPosition, Quaternion.identity);
        if(knifeCount == 7)
        {
            
        }
        else
        {
            sliderProgress.value++;
        }

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
            //make the player realize it's game over and he won
            //you can also add a nice animation of the breaking log
            //but this is outside the scope of this tutorial
            yield return new WaitForSecondsRealtime(0.1f);
            //Feel free to set different values for knife count and log's rotation pattern
            //instead of just restarting. This would make it feel like a new, harder level.
            RestartGame();
        }
        else
        {
            GameUI.ShowRestartButton();
        }
    }

    public void RestartGame()
    {
        //restart the scene by reloading the currently active scene

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
    }

}