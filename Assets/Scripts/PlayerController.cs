using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.ComponentModel;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float JumpForce = 0;
    public TextMeshProUGUI countText;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    public GameObject RestartButton;
    public bool isOnGround = true;

    private Rigidbody rb;
    private PlayerActionControls playerActionControls;
    private int count;
    private float movementX;
    private float movementY;
    private bool GameOn = true;

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        WinTextObject.SetActive(false);
        LoseTextObject.SetActive(false);
        RestartButton.SetActive(false);
        playerActionControls.Player.Jump.performed += _ => Jump();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Jump()
    {
        if (isOnGround)
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode.Impulse);
            isOnGround = false;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void FixedUpdate()
    {
        if(isOnGround)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
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