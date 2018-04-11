
namespace LetC
{
    using UnityEngine;

    public class Carousel : MonoBehaviour
    {
        class Side
        {
            public static readonly string RIGHT = "right";
            public static readonly string LEFT = "left";
        }

        RectTransform[] data;

        float wide, mousePositionStartX, mousePositionEndX, dragAmount, screenPosition, lastScreenPosition, lerpTimer, lerpPage;

        int pageCount = 1;
        string side = Side.RIGHT;

        public int swipeThrustHold = 30;
        public int space = 30;
        public float selectSize = 1.2f;
        public float unselectSize = .7f;
        public float speed = 5;
        public float margin = 50;
        bool canSwipe;

        public GameObject content, selected;

        void Update()
        {
            UpdateElements();
            UpdateSwipeAction();
            UpdateSelectValue();
        }

        void OnSwipeComplete()
        {
            lastScreenPosition = screenPosition;
            if (dragAmount > 0)
            {
                if (Mathf.Abs(dragAmount) > (swipeThrustHold))
                {
                    if (pageCount == 0) lerpPage = 0;
                    else
                    {
                        if (side == Side.RIGHT) pageCount--;
                        side = Side.LEFT;
                        pageCount--;
                        if (pageCount < 0) pageCount = 0;
                        lerpPage = (wide + space) * pageCount;
                    }
                }
                lerpTimer = 0;
            }
            else if (dragAmount < 0)
            {
                if (Mathf.Abs(dragAmount) > (swipeThrustHold))
                {

                    if (pageCount == data.Length) lerpPage = (wide + space) * data.Length - 1;
                    else
                    {
                        if (side == Side.LEFT) pageCount++;
                        side = Side.RIGHT;
                        lerpPage = (wide + space) * pageCount;
                        pageCount++;
                    }
                }
                lerpTimer = 0;
            }
        }

        void UpdateSwipeAction()
        {
            lerpTimer = lerpTimer + Time.deltaTime;
            if (lerpTimer < .333)
            {
                screenPosition = Mathf.Lerp(lastScreenPosition, lerpPage * -1, lerpTimer * 3);
                lastScreenPosition = screenPosition;
            }

            float contentPositionY = content.GetComponent<RectTransform>().position.y;
            float contentHeight = content.GetComponent<RectTransform>().rect.height;
            if (Input.GetMouseButtonDown(0) && Input.mousePosition.y > (contentPositionY - contentHeight / 2f - margin) && Input.mousePosition.y < (contentPositionY + contentHeight / 2f + margin))
            {
                canSwipe = true;
                mousePositionStartX = Input.mousePosition.x;
            }

            if (Input.GetMouseButton(0))
            {
                if (canSwipe)
                {
                    mousePositionEndX = Input.mousePosition.x;
                    dragAmount = mousePositionEndX - mousePositionStartX;
                    screenPosition = lastScreenPosition + dragAmount;
                }
            }

            if (Mathf.Abs(dragAmount) > swipeThrustHold && canSwipe)
            {
                canSwipe = false;
                lastScreenPosition = screenPosition;
                if (pageCount < data.Length) OnSwipeComplete();
                else if (pageCount == data.Length && dragAmount < 0) lerpTimer = 0;
                else if (pageCount == data.Length && dragAmount > 0) OnSwipeComplete();
            }

            if (Input.GetMouseButtonUp(0)) if (Mathf.Abs(dragAmount) < swipeThrustHold) lerpTimer = 0;
        }

        void UpdateSelectValue()
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i].anchoredPosition = new Vector2(screenPosition + ((wide + space) * i), 0);
                if (side == Side.RIGHT)
                {
                    if (i == pageCount - 1)
                    {
                        selected = data[i].gameObject;
                        data[i].localScale = Vector3.Lerp(data[i].localScale, new Vector3(selectSize, selectSize, selectSize), Time.deltaTime * speed);
                    }
                    else data[i].localScale = Vector3.Lerp(data[i].localScale, new Vector3(unselectSize, unselectSize, unselectSize), Time.deltaTime * speed);
                }
                else
                {
                    if (i == pageCount)
                    {
                        selected = data[i].gameObject;
                        data[i].localScale = Vector3.Lerp(data[i].localScale, new Vector3(selectSize, selectSize, selectSize), Time.deltaTime * speed);
                    }
                    else data[i].localScale = Vector3.Lerp(data[i].localScale, new Vector3(unselectSize, unselectSize, unselectSize), Time.deltaTime * speed);
                }
            }
        }

        void UpdateElements()
        {
            RectTransform[] rects = content.GetComponentsInChildren<RectTransform>();
            data = new RectTransform[rects.Length - 1];
            for (int i = 1; i < rects.Length; i++) data[i - 1] = rects[i];
            wide = content.GetComponent<RectTransform>().rect.width;
            for (int i = 1; i < data.Length; i++) data[i].anchoredPosition = new Vector2(((wide + space) * i), 0);
        }
    }
}
