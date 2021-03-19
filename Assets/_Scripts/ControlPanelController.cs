using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPanelController : MonoBehaviour
{
    public float speed;
    public float timer;
    public bool isOnScreen = false;
    public TMP_Text pauseText;

    public Vector2 offScreenPos;
    public Vector2 onScreenPos;

    RectTransform rectTransform;

    [Header("Player Settings")]
    public PlayerController player;
    public CameraController playerCam;

    public Pause pause;

    [Header("Scene Data")]
    public SceneDataSO sceneData;

    private bool clicked;

    private void Start()
    {
        pause = FindObjectOfType<Pause>();

        playerCam = FindObjectOfType<CameraController>();

        player = FindObjectOfType<PlayerController>();

        rectTransform = GetComponent<RectTransform>();

        offScreenPos = rectTransform.anchoredPosition;

        rectTransform.anchoredPosition = offScreenPos;

        timer = 0.0f;
    }

    private void Update()
    {
        if (clicked)
        {
            clicked = false;
            playerCam.enabled = isOnScreen;
            isOnScreen = !isOnScreen;
            timer = 0.0f;
        }

        if (isOnScreen)
        {
            playerCam.enabled = false;
            MoveControlPanelDown();
            pauseText.enabled = true;
        }
        else
        {
            playerCam.enabled = true;
            MoveControlPanelUp();
            pauseText.enabled = false;
        }
    }

    public void ClickedI()
    {
        clicked = true;
    }

    public void MoveControlPanelDown()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(offScreenPos, onScreenPos, timer);
        if(timer < 1.0f)
        {
            timer += Time.deltaTime * speed;
        }
        pause.TogglePause();
    }

    public void MoveControlPanelUp()
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

    public void OnLoadButtonPressed()
    {
        isOnScreen = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = sceneData.playerPosition;
        player.transform.rotation = sceneData.playerRotation;
        player.GetComponent<CharacterController>().enabled = true;

        player.SetHealth(sceneData.playerHealth);
    }

    public void OnSaveButtonPressed()
    {
        sceneData.playerPosition = player.transform.position;
        sceneData.playerHealth = player.health;
        sceneData.playerRotation = player.transform.localRotation;
    }
}
