using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.ComponentModel;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    public GameObject RestartButton;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private bool GameOn = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        WinTextObject.SetActive(false);
        LoseTextObject.SetActive(false);
        RestartButton.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameOn)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);
                count++;
                SetCountText();
            }
            if (count >= 8)
            {
                WinGame();
            }
        }
    }

    private void WinGame()
    {
        WinTextObject.SetActive(true);
        GameOn = false;
        gameObject.SendMessage("EndGame");
        RestartButton.SetActive(true);
    }
    public void GameOver()
    {
        LoseTextObject.SetActive(true);
        GameOn = false;
        RestartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Minigame");
    }
}