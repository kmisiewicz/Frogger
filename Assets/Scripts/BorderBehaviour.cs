using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderBehaviour : MonoBehaviour
{
    public enum BorderPlace
    {
        Left,
        Right,
        Top,
        Bottom
    }
    public BorderPlace borderPlace;

    // Start is called before the first frame update
    void Start()
    {
        // Zmiana rozmiaru granic, by wypełniały okno.
        Vector3 lowerLeft, lowerRight, upperLeft, upperRight;
        lowerLeft = lowerRight = upperLeft = upperRight = Vector3.zero;
        switch (borderPlace)
        {
            case BorderPlace.Left:
                lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
                lowerRight = new Vector3(-10, lowerLeft.y, 0);
                upperLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
                upperRight = new Vector3(-10, upperLeft.y, 0);
                break;
            case BorderPlace.Right:
                lowerRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
                lowerLeft = new Vector3(10, lowerRight.y, 0);
                upperRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
                upperLeft = new Vector3(10, upperRight.y, 0);
                break;
            case BorderPlace.Top:
                upperLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
                upperLeft.x = -10;
                lowerLeft = new Vector3(-10, 4.5f, 0);
                upperRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
                upperRight.x = 10;
                lowerRight = new Vector3(10, 4.5f, 0);
                break;
            case BorderPlace.Bottom:
                lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
                lowerLeft.x = -10;
                upperLeft = new Vector3(-10, -5.5f, 0);
                lowerRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
                lowerRight.x = 10;
                upperRight = new Vector3(10, -5.5f, 0);
                break;
        }

        transform.position = new Vector3((lowerLeft.x + lowerRight.x) / 2.0f, (lowerLeft.y + upperLeft.y) / 2.0f);
        transform.localScale = new Vector3(lowerRight.x - lowerLeft.x, upperLeft.y - lowerLeft.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
