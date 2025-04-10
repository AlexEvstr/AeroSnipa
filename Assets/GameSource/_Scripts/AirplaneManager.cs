using UnityEngine;

public class AirplaneManager : MonoBehaviour
{
    public GameObject airplane;
    private float horizontalSpeed = 1f;
    private float verticalSpeed = 3f;
    public Vector2 resetPosition = new Vector2(0, -3);

    private bool moveHorizontally = true;
    private float targetX;
    private float leftBoundary;
    private float rightBoundary;
    [SerializeField] private Sprite[] _planeSprites;
    [SerializeField] private GameObject _shotBtn;

    private void Start()
    {
        airplane.GetComponent<SpriteRenderer>().sprite = _planeSprites[PlayerPrefs.GetInt("ChosenAirplane", 0)];

        int horizontalSpeedLevel = PlayerPrefs.GetInt($"Airplane_{PlayerPrefs.GetInt("ChosenAirplane", 0)}_Skill_0", 1);
        int verticalSpeedLevel = PlayerPrefs.GetInt($"Airplane_{PlayerPrefs.GetInt("ChosenAirplane", 0)}_Skill_1", 1);

        horizontalSpeed += horizontalSpeedLevel * 0.1f;
        verticalSpeed += verticalSpeedLevel * 0.1f;

        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        float airplaneWidth = airplane.GetComponent<SpriteRenderer>().bounds.extents.x;

        leftBoundary = -screenBounds.x + airplaneWidth;
        rightBoundary = screenBounds.x - airplaneWidth;

        targetX = rightBoundary;
    }

    private void Update()
    {
        if (airplane == null) return;

        if (moveHorizontally)
        {
            MoveHorizontally();
        }
        else
        {
            MoveVertically();
        }
    }

    private void MoveHorizontally()
    {
        airplane.transform.position = Vector3.MoveTowards(
            airplane.transform.position,
            new Vector3(targetX, airplane.transform.position.y, airplane.transform.position.z),
            horizontalSpeed * Time.deltaTime
        );

        if (Mathf.Abs(airplane.transform.position.x - targetX) < 0.01f)
        {
            targetX = targetX == rightBoundary ? leftBoundary : rightBoundary;
        }
    }

    private void MoveVertically()
    {
        airplane.transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
    }

    public void SwitchToVerticalMovement()
    {
        moveHorizontally = false;
        _shotBtn.SetActive(false);
    }

    public void OnAirplaneCollision(string tag)
    {
        if (tag == "target" || tag == "border")
        {
            airplane.transform.position = resetPosition;

            moveHorizontally = true;

            targetX = rightBoundary;
            _shotBtn.SetActive(true);
        }
    }
}