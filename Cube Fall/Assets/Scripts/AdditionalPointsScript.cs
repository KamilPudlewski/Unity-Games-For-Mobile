using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalPointsScript : MonoBehaviour
{
    private Text text;
    private int points = 100;

    void Start()
    {
        text = GetComponent<Text>();
        text.text = "+" + points.ToString();
    }

    void Update()
    {
        Vector2 tmp = text.transform.position;
        tmp.y += 2;
        text.transform.position = tmp;
        text.fontSize--;

        if (text.fontSize == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPoints(int points)
    {
        this.points = points;
    }
}
