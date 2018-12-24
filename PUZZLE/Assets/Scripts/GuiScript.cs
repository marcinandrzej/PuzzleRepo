using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void action(GameObject button);

public class GuiScript
{
    public GameObject CreatePanel(GameObject parent, string name, Vector2 anchorMin, Vector2 anchorMax, Vector3 localScale,
        Vector3 localPosition, Vector2 sizeDelta, Vector2 anchoredPosition, Sprite image, Color32 color)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent.transform);
        panel.AddComponent<RectTransform>();
        panel.AddComponent<Image>();

        panel.GetComponent<Image>().sprite = image;
        panel.GetComponent<Image>().type = Image.Type.Sliced;
        panel.GetComponent<Image>().color = color;

        panel.GetComponent<RectTransform>().anchorMin = anchorMin;
        panel.GetComponent<RectTransform>().anchorMax = anchorMax;
        panel.GetComponent<RectTransform>().localScale = localScale;
        panel.GetComponent<RectTransform>().localPosition = localPosition;
        panel.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        panel.GetComponent<RectTransform>().sizeDelta = sizeDelta;
        return panel;
    }

    public GameObject CreateButton(GameObject parent, string name, Vector2 anchorMin, Vector2 anchorMax,
       Vector3 localScale, Vector3 localPosition, Vector2 sizeDelta, Vector2 anchoredPosition, Sprite image, Color32 color)
    {
        GameObject button = new GameObject(name);
        button.transform.SetParent(parent.transform);
        button.AddComponent<RectTransform>();
        button.AddComponent<Image>();
        button.AddComponent<Button>();

        button.GetComponent<Image>().sprite = image;
        button.GetComponent<Image>().type = Image.Type.Sliced;
        button.GetComponent<Image>().color = color;

        button.GetComponent<RectTransform>().anchorMin = anchorMin;
        button.GetComponent<RectTransform>().anchorMax = anchorMax;
        button.GetComponent<RectTransform>().localScale = localScale;
        button.GetComponent<RectTransform>().localPosition = localPosition;
        button.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        button.GetComponent<RectTransform>().sizeDelta = sizeDelta;

        return button;
    }

    public GameObject[,] FillWithButtons(GameObject panel, int buttonCount, int rowsCount, float buttonWidth, 
       float buttonHeight, Sprite s, Color32 color)
    {
        GameObject[,] buttons = new GameObject[buttonCount,rowsCount];
        float offsetx = buttonWidth / 2.0f;
        float offsety = buttonHeight / 2.0f;
        for (int j = 0; j < rowsCount; j++)
        {
            for (int i = 0; i < buttonCount; i++)
            {
                GameObject but = CreateButton(panel, ("Button" + (j * rowsCount + i).ToString()), new Vector2(0, 1), new Vector2(0, 1),
                    new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(buttonWidth, buttonHeight),
                    new Vector2((offsetx + i * buttonWidth), (-offsety - j * buttonHeight)), s, color);
                but.AddComponent<ButtonScript>();
                but.GetComponent<ButtonScript>().SetIndexes(i, j);
                buttons[i,j] = but;
            }
        }
        return buttons;
    }

    public List<GameObject> SetMenuText(GameObject[,] but)
    {
        List<GameObject> l = new List<GameObject>();
        for (int i = 0; i < but.GetLength(0); i++)
        {
            for (int j = 0; j < but.GetLength(1); j++)
            {
                GameObject text = new GameObject("MenuButton");
                text.transform.SetParent(but[i, j].transform);
                text.AddComponent<Text>();
                text.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.0f);
                text.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1.0f);
                text.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                text.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                text.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
                text.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, 0.0f);
                text.GetComponent<Text>().resizeTextForBestFit = false;
                text.GetComponent<Text>().fontSize = 20;
                text.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
                text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                text.GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                if (j==0)
                {
                    text.GetComponent<Text>().text = "RESTART";
                }
                else
                {
                    text.GetComponent<Text>().text = "EXIT";
                }               
            }
        }
        return l;
    }

    public List<GameObject> SetText(int[,] tab, int dimension, GameObject[,] but)
    {
        List<GameObject> l = new List<GameObject>();
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                if (tab[i,j] != (dimension * dimension - 1))
                {
                    GameObject text = new GameObject("Number");
                    text.transform.SetParent(but[i,j].transform);
                    text.AddComponent<Text>();
                    text.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.0f);
                    text.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1.0f);
                    text.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                    text.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    text.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
                    text.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, 0.0f);
                    text.GetComponent<Text>().resizeTextForBestFit = true;
                    text.GetComponent<Text>().resizeTextMaxSize = 40;
                    text.GetComponent<Text>().resizeTextMinSize = 10;
                    text.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
                    text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                    text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                    text.GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                    text.GetComponent<Text>().text = (tab[i,j] + 1).ToString();
                }
                else
                {
                    but[i,j].GetComponent<Image>().color = new Color32(255,255,255, 60);
                }
            }
        }
        return l;
    }

    public void SetAction(GameObject[,] buttons, action act)
    {
        foreach (GameObject but in buttons)
        {
            but.GetComponent<Button>().onClick.AddListener(delegate { act.Invoke(but); });
        }
    }

    public IEnumerator ShowWinner(GameObject parent, Sprite spr)
    {
        yield return new WaitForSeconds(0.6f);
        GameObject textPanel = CreatePanel(parent, "TextPanel", new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector3(1, 1, 1), new Vector3(0, 0, 0),
           new Vector2(400, 100), new Vector3(0, 0, 0), spr, new Color32(255, 255, 255, 255));
        GameObject textt = new GameObject("Text");
        textt.transform.SetParent(textPanel.transform);
        textt.AddComponent<RectTransform>();
        textt.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.0f);
        textt.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1.0f);
        textt.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        textt.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        textt.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        textt.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
        textt.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, 0.0f);
        textt.AddComponent<Text>();
        textt.GetComponent<Text>().text = "CONGRATULATIONS";
        textt.GetComponent<Text>().resizeTextForBestFit = true;
        textt.GetComponent<Text>().resizeTextMaxSize = 40;
        textt.GetComponent<Text>().resizeTextMinSize = 10;
        textt.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
        textt.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        textt.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        textt.GetComponent<Text>().color = new Color32(0, 0, 0, 255);

        for (float i = 0.1f; i < 1.0f; i += 0.05f)
        {
            textPanel.GetComponent<RectTransform>().localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.025f);
        }
    }
}
