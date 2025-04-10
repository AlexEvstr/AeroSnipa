using System.Collections;
using UnityEngine;

public class AirplaneCollision : MonoBehaviour
{
    public AirplaneManager airplaneManager;
    public GameManager gameManager;
    public GameObject _failText;
    [SerializeField] private GameAudioCotroller _gameAudioCotroller;
    [SerializeField] private MissionManager _missionManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("target"))
        {
            airplaneManager.OnAirplaneCollision(collision.tag);
            gameManager.AddCoins();
            _gameAudioCotroller.PlayCashSound();

            _missionManager.RegisterHit();
        }
        else if (collision.CompareTag("border"))
        {
            airplaneManager.OnAirplaneCollision(collision.tag);
        }
        else if (collision.CompareTag("failBorder"))
        {
            StartCoroutine(ShowFail());
            gameManager.LoseLife();
            _gameAudioCotroller.PlayDeclibeSound();
        }
    }

    private IEnumerator ShowFail()
    {
        _failText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _failText.SetActive(false);
    }
}