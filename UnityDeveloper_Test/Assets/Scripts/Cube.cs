using UnityEngine;

public class Cube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GamePlayManager.instance.UpdateBoxes();
            gameObject.SetActive(false);
        }
    }
}
