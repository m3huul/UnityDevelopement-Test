using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GamePlayManager.instance.gameState = GamePlayManager.State.End;
            GamePlayManager.instance.ConclusionText.gameObject.SetActive(true);
            GamePlayManager.instance.ConclusionText.text = "LOSE";
            GamePlayManager.instance.GameplayEnded();
        }
    }
}
