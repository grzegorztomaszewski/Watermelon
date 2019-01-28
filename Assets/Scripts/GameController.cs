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
    public int lvl_nr = 2;

    [SerializeField]
    public Camera cam;

    //we can get this instance from other scripts very easily
    public static GameController Instance { get; private set; }

    [SerializeField]
    private int knifeCount;

    [Header("Knife Spawning")]
    [SerializeField]
    private Vector2 knifeSpawnPosition;
    [SerializeField]

    private GameObject knifeObject;


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
        else
        {
            lvl_default();
            lvl_nr++;
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
    public void lvl_default()
    {
        SceneManager.CreateScene($"lvl_{lvl_nr}");
        SceneManager.GetActiveScene();
        knifeCount = 8;
        cam.backgroundColor = Color.green;
        Destroy(knifeObject);
        SpawnKnife();
        //obiekty które się tworzą są w Log'u!! je trzeba usunąć
    }
}