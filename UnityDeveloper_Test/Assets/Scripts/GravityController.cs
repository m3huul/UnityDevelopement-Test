using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Header("Gravity Settings")]
    public float gravityStrength;
    private Vector3 currentGravity;

    [Header("References")]
    public Transform rotateGO;
    public Transform head;

    private void Start()
    {
        SetInitialGravity();
    }

    private void Update()
    {
        if (GamePlayManager.instance.gameState == GamePlayManager.State.Gameplay)
        {
            HandleGravityManipulation();
        }
    }

    private void SetInitialGravity()
    {
        Physics.gravity = new Vector3(0, -gravityStrength, 0);
        currentGravity = Physics.gravity;
    }

    private void HandleGravityManipulation()
    {
        if (Input.anyKeyDown)
        {
            rotateGO.gameObject.SetActive(false);
        }

        UpdateRotateGOPosition();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeGravityDirection(transform.forward);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeGravityDirection(-transform.forward);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeGravityDirection(-transform.right);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeGravityDirection(transform.right);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ApplyGravity();
        }
    }

    private void UpdateRotateGOPosition()
    {
        rotateGO.position = head.position + currentGravity.normalized * 0.5f;
    }

    private void ChangeGravityDirection(Vector3 direction)
    {
        currentGravity = direction * gravityStrength;
        rotateGO.gameObject.SetActive(true);

        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -currentGravity.normalized) * transform.rotation;
        rotateGO.rotation = targetRotation;
    }

    private void ApplyGravity()
    {
        Physics.gravity = currentGravity;
        rotateGO.gameObject.SetActive(false);

        transform.position = rotateGO.GetChild(0).position;
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -currentGravity.normalized) * transform.rotation;
        transform.rotation = targetRotation;
    }
}
