using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobilePlayerInput : PlayerInputBase {

    private Touch theTouch;
    private int currentFingerId = 0;
    private Vector2 touchStartPosition, touchEndPosition, playerPosition, startDistance;
    private Vector2 movementVector = new Vector2(0, 0);

    // Update is called once per frame
    void Update () {

    }

    public override Vector2 ProcessInput()
    {

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began)
            {
                touchStartPosition = Camera.main.ScreenToWorldPoint(theTouch.position);
            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Stationary)
            {
                if(currentFingerId == theTouch.fingerId)
                {
                    touchEndPosition = Camera.main.ScreenToWorldPoint(theTouch.position);
                    movementVector = touchEndPosition - touchStartPosition;
                    touchStartPosition = touchEndPosition;
                }
                else
                {
                    currentFingerId = theTouch.fingerId;
                    touchStartPosition = Camera.main.ScreenToWorldPoint(theTouch.position);
                }

            }

            else if (theTouch.phase == TouchPhase.Ended)
            {
                movementVector.Set(0, 0);
            }
        }

        return movementVector;
    }
}
