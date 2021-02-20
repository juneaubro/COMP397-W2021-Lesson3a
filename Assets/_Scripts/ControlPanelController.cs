using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelController : MonoBehaviour
{
    public float speed;
    public float timer;
    public bool isOnScreen = false;

    private Vector2 offScreenPos;
    private Vector2 onScreenPos;

    RectTransform rectTransform;
    public CameraController playerCam;

    public Pause pause;

    private void Start()
    {
        pause = FindObjectOfType<Pause>();

        playerCam = FindObjectOfType<CameraController>();

        rectTransform = GetComponent<RectTransform>();

        offScreenPos = rectTransform.anchoredPosition;

        rectTransform.anchoredPosition = offScreenPos;

        onScreenPos = new Vector2(220.0f, -160.0f);

        timer = 0.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            playerCam.enabled = isOnScreen;
            isOnScreen = !isOnScreen;
            timer = 0.0f;
        }

        if (isOnScreen)
        {
            Cursor.lockState = CursorLockMode.None;
            playerCam.enabled = false;
            MoveControlPanelDown();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerCam.enabled = true;
            MoveControlPanelUp();
        }
    }

    private void MoveControlPanelDown()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(offScreenPos, onScreenPos, timer);
        if(timer < 1.0f)
        {
            timer += Time.deltaTime * speed;
        }
    }

    private void MoveControlPanelUp()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(onScreenPos, offScreenPos, timer);
        if (timer < 1.0f)
        {
            timer += Time.deltaTime * speed;
        }
        if (pause.isGamePaused)
        {
            pause.TogglePause();
        }
    }
}
