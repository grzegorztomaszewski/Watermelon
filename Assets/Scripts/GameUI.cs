using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField]
    private GameObject restartButton;

    [Header("Knife Count Display")] //header for organization purposes
    [SerializeField]
    private GameObject panelKnives;
    [SerializeField]
    //this will be set to the icon prefab
    private GameObject iconKnife;
    [SerializeField]
    private Color usedKnifeIconColor;

    //enable the restartButton game object
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

    //add a number of iconKnife children to panelKnives
    public void SetInitialDisplayedKnifeCount(int count)
    {
        for (int i = 0; i < count; i++)
            Instantiate(iconKnife, panelKnives.transform);
    }

    //keeping track of the last icon representing an unthrown knife
    private int knifeIconIndexToChange = 0;
    //changing the color of the image to represent a thrown (used) knife
    public void DecrementDisplayedKnifeCount()
    {
        panelKnives.transform.GetChild(knifeIconIndexToChange++)
            .GetComponent<Image>().color = usedKnifeIconColor;
    }
}