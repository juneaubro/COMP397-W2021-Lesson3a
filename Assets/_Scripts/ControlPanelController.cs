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
    public GameObject cam;

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

        GetSceneData();
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

        cam.transform.localRotation = sceneData.cameraRotation;

        player.SetHealth(sceneData.playerHealth);
    }

    public void OnSaveButtonPressed()
    {
        sceneData.playerPosition = player.transform.position;
        sceneData.playerHealth = player.health;
        sceneData.playerRotation = player.transform.rotation;
        sceneData.cameraRotation = cam.transform.localRotation;

        SetSceneData();
    }

    public void GetSceneData()
    {
        sceneData.playerPosition.x = PlayerPrefs.GetFloat("ptx");
        sceneData.playerPosition.y = PlayerPrefs.GetFloat("pty");
        sceneData.playerPosition.z = PlayerPrefs.GetFloat("ptz");

        sceneData.playerRotation.x = PlayerPrefs.GetFloat("prx");
        sceneData.playerRotation.y = PlayerPrefs.GetFloat("pry");
        sceneData.playerRotation.z = PlayerPrefs.GetFloat("prz");
        sceneData.playerRotation.w = PlayerPrefs.GetFloat("prw");

        sceneData.playerHealth = PlayerPrefs.GetInt("php");
    }

    public void SetSceneData()
    {
        PlayerPrefs.SetFloat("ptx", sceneData.playerPosition.x);
        PlayerPrefs.SetFloat("pty", sceneData.playerPosition.y);
        PlayerPrefs.SetFloat("ptz", sceneData.playerPosition.z);

        PlayerPrefs.SetFloat("prx", sceneData.playerRotation.x);
        PlayerPrefs.SetFloat("pry", sceneData.playerRotation.y);
        PlayerPrefs.SetFloat("prz", sceneData.playerRotation.z);
        PlayerPrefs.SetFloat("prw", sceneData.playerRotation.w);

        PlayerPrefs.SetInt("php", sceneData.playerHealth);
    }
}
