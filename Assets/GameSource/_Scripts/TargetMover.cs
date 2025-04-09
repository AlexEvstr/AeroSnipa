using UnityEngine;

public class TargetMover : MonoBehaviour
{
    private float speed = 2f;
    public float xMin = -2.25f;
    public float xMax = 2.25f;

    private float targetX;

    private void Start()
    {
        int targetSpeedLevel = PlayerPrefs.GetInt($"Airplane_{PlayerPrefs.GetInt("ChosenAirplane", 0)}_Skill_2", 1);

        speed -= targetSpeedLevel * 0.1f;

        if (speed < 0.1f)
        {
            speed = 0.1f;
        }

        targetX = xMax;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(targetX, transform.position.y, transform.position.z),
            speed * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.x - targetX) < 0.01f)
        {
            targetX = targetX == xMax ? xMin : xMax;
        }
    }
}