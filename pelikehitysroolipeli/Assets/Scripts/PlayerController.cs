using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector2 lastMovement;
    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed;

    DoorController activeDoor = null;
    public GameObject Buttons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        lastMovement = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();

        Buttons = GameObject.Find("DoorButtons");

        Button openbutton = GameObject.Find("OpenButton").GetComponent<Button>();
        openbutton.onClick.AddListener(OnOpenButton);

        Button closebutton = GameObject.Find("CloseButton").GetComponent<Button>();
        closebutton.onClick.AddListener(OnCloseButton);

        Button lockbutton = GameObject.Find("LockButton").GetComponent<Button>();
        lockbutton.onClick.AddListener(OnLockButton);

        Button unlockbutton = GameObject.Find("UnlockButton").GetComponent<Button>();
        unlockbutton.onClick.AddListener(OnUnlockButton);

        Buttons.SetActive(false);
    }

    void OnOpenButton()
    {
        Debug.Log("Open button was pressed");
        if (activeDoor)
        {
            activeDoor.ReceiveAction(DoorController.Action.Open);
        }
    }
    void OnCloseButton()
    {
        if (activeDoor)
        {
            activeDoor.ReceiveAction(DoorController.Action.Close);
        }
    }
    void OnLockButton()
    {
        if (activeDoor)
        {
            activeDoor.ReceiveAction(DoorController.Action.Lock);
        }
    }
    void OnUnlockButton()
    {
        if (activeDoor)
        {
            activeDoor.ReceiveAction(DoorController.Action.Unlock);
        }
    }


    // Update is called once per frame


    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + lastMovement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Huomaa mitä pelaaja löytää
        if (collision.CompareTag("Door"))
        {
            Debug.Log("Found Door");

            activeDoor = collision.gameObject.GetComponent<DoorController>();
            Buttons.SetActive(true);

            Vector3 doorposition = collision.gameObject.transform.position;
            Vector3 doorScreenPosition = Camera.main.WorldToScreenPoint(doorposition);
           // doorScreenPosition.y = Screen.height - doorScreenPosition.y;
            Buttons.transform.position = doorScreenPosition;
        }
        else if (collision.CompareTag("Merchant"))
        {
            Debug.Log("Found Merchant");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            Debug.Log("Exited from door area");

            Buttons.SetActive(false);
        }
    }

    void OnMoveAction(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        lastMovement = v;
    }
    private void ConnectButtonToFunction(string buttonName, UnityAction action)
    {
        GameObject buttonObj = GameObject.Find(buttonName);
        Button buttonComp = buttonObj.GetComponent<Button>();
        buttonComp.onClick.AddListener(action);
    }
    public void OnOpenButtonPress()
    {
        activeDoor.ReceiveAction(DoorController.Action.Open);
    }
    public void OnCloseButtonPress()
    {
        activeDoor.ReceiveAction(DoorController.Action.Close);
    }
    public void OnLockButtonPress()
    {
        activeDoor.ReceiveAction(DoorController.Action.Lock);
    }
    public void OnUnlockButtonPress()
    {
        activeDoor.ReceiveAction(DoorController.Action.Unlock);
    }
}