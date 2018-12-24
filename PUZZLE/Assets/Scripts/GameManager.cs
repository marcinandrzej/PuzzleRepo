using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int CELL_COUNT = 4;
    public Sprite spr;

    private GameObject gamePanel;
    private GameObject menuPanel;
    private GameObject[,] menuButt;
    private GameObject[,] tab;
    private List<GameObject> tex;
    private List<GameObject> menuTex;

    private GuiScript guiScr;
    private ModelScript modelScr;

    public action onClick;
    // Use this for initialization
    void Start()
    {
        guiScr = new GuiScript();
        modelScr = new ModelScript(CELL_COUNT);
        gamePanel = guiScr.CreatePanel(gameObject, "GamePanel", new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector3(1, 1, 1), new Vector3(0, 0, 0),
           new Vector2(400, 400), new Vector2(0, 0), spr, new Color32(0, 150, 120, 255));
        menuPanel = guiScr.CreatePanel(gameObject, "MenuPanel", new Vector2(1, 1), new Vector2(1, 1), new Vector3(1, 1, 1), new Vector3(0, 0, 0),
           new Vector2(100, 200), new Vector2(-60, -110), spr, new Color32(255, 255, 255, 0));
        tab = guiScr.FillWithButtons(gamePanel, 4, 4, 100, 100, spr, new Color32(255, 255, 255, 255));
        menuButt = guiScr.FillWithButtons(menuPanel, 1, 2, 100, 100, spr, new Color32(255, 255, 255, 255));
        tex = guiScr.SetText(modelScr.Table, CELL_COUNT, tab);
        menuTex = guiScr.SetMenuText(menuButt);

        onClick = new action(Execute);

        guiScr.SetAction(tab, onClick);

        menuButt[0, 0].GetComponent<Button>().onClick.AddListener(delegate
        { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
        menuButt[0, 1].GetComponent<Button>().onClick.AddListener(delegate
        { Application.Quit(); });
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Execute(GameObject button)
    {
        int x = button.GetComponent<ButtonScript>().X;
        int y = button.GetComponent<ButtonScript>().Y;
        
        if (modelScr.IfCanMove(x, y))
        {
            button.GetComponent<ButtonScript>().Move(tab[modelScr.EmptyIndexX, modelScr.EmptyIndexY], tab);
            Move(x, y);
            modelScr.Move(x, y);
            if (modelScr.IsEnd())
            {
                foreach (GameObject go in tab)
                {
                    Destroy(go, 0.6f);
                }
                StartCoroutine(guiScr.ShowWinner(gameObject, spr));
            }
        }
    }

    public void Move(int indexX, int indexY)
    {
        GameObject temp = tab[indexX, indexY];
        tab[indexX, indexY] = tab[modelScr.EmptyIndexX, modelScr.EmptyIndexY];
        tab[modelScr.EmptyIndexX, modelScr.EmptyIndexY] = temp;
    }
}
