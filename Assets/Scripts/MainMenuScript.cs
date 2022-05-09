using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    // some code inspired and used from https://www.youtube.com/watch?v=tCr_i5CVv_w

    [SerializeField] GameObject Player;
    [SerializeField] float xDistance = 40;
    [SerializeField] string[] levelNames;
    [SerializeField] GameObject levelScreen;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Credits;
    public GameObject levelIconPrefab;
    public GameObject thisCanvas;
    public Vector2 iconSpacing;
    private Rect iconDimensions;

    // Start is called before the first frame update
    void Start()
    {
        iconDimensions = levelIconPrefab.GetComponent<RectTransform>().rect;
    }
    public void menuOn()
    {
        MainMenu.SetActive(true);
        Credits.SetActive(false);
    }
    public void menuOff()
    {
        MainMenu.SetActive(false);
        Credits.SetActive(true);
    }
    public void LoadPanels()
    {
        //Enable the level canvas and set it's position relative to the existing canvas
        levelScreen.SetActive(true);
        levelScreen.transform.SetParent(thisCanvas.transform, false);
        levelScreen.transform.SetParent(levelScreen.transform);
        levelScreen.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        SetUpGrid(levelScreen);
        LoadIcons(levelScreen);
    }
    void SetUpGrid(GameObject panel)
    {
        //Create the automatic layout for the level buttons
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = iconSpacing;
    }
    void LoadIcons(GameObject parentObject)
    {
        //Create and set the names for level buttons
        for (int i = 1; i <= levelNames.Length; i++)
        {
            GameObject icon = Instantiate(levelIconPrefab) as GameObject;
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = levelNames[i-1];
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText(levelNames[i-1]);
            //icon.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadSceneAsync(levelNames[i - 1].ToString()); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        // allow the player to wrap around the level screen
        if(Player.transform.position.x >= xDistance)
        {
            Vector3 wrap = Player.transform.position;
            wrap.x = -1 * xDistance;
            Player.transform.position = wrap;
        }
    }
}
