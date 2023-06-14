using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControlManager : MonoBehaviour
{
    public float swipeThreshold = 50f; // Ngưỡng vuốt để xác định là vuốt thành công

    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;

    public bool IsSwipingUp()
    {
        return IsSwiping() && swipeEndPosition.y - swipeStartPosition.y > swipeThreshold;
    }

    public bool IsSwipingDown()
    {
        return IsSwiping() && swipeStartPosition.y - swipeEndPosition.y > swipeThreshold;
    }

    public bool IsSwipingRight()
    {
        return IsSwiping() && swipeEndPosition.x - swipeStartPosition.x > swipeThreshold;
    }

    public bool IsSwipingLeft()
    {
        return IsSwiping() && swipeStartPosition.x - swipeEndPosition.x > swipeThreshold;
    }

    private bool IsSwiping()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeEndPosition = touch.position;
            }
        }
    }
}