using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private int x;
    private int y;

    public int X
    {
        get
        {
            return x;
        }

        set
        {
            x = value;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }

        set
        {
            y = value;
        }
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetIndexes(int _x, int _y)
    {
        X = _x;
        Y = _y;
    }

    public void Move(GameObject button, GameObject[,] buttons)
    {
        int tempX = button.GetComponent<ButtonScript>().X;
        int tempY = button.GetComponent<ButtonScript>().Y;
        button.GetComponent<ButtonScript>().SetIndexes(X, Y);
        SetIndexes(tempX, tempY);
        Vector2 dest = button.GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(button.GetComponent<ButtonScript>().MoveCoroutine(
            gameObject.GetComponent<RectTransform>().anchoredPosition, buttons));
        StartCoroutine(MoveCoroutine(dest, buttons));
    }

    public IEnumerator MoveCoroutine(Vector2 destination, GameObject[,] buttons)
    {
        BlockGrid(buttons, false);
        while (gameObject.GetComponent<RectTransform>().anchoredPosition != destination)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(
                gameObject.GetComponent<RectTransform>().anchoredPosition, destination, 200.0f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        BlockGrid(buttons, true);
    }

    public void BlockGrid(GameObject[,] buttons, bool block)
    {
        foreach (GameObject but in buttons)
        {
            but.GetComponent<Button>().enabled = block;
        }
    }
}
