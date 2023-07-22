using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private enum Level {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }
    
    PlayerController player;
    public TextMeshProUGUI distanceScored;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isStopping)
        {
            distanceScored.enabled = false;
            return;
        }

        int distance = Mathf.FloorToInt(player.distance);
        distanceScored.text = distance + " m";
        
        if (distance > 1000)
        {
            distanceScored.text = distance / 1000 + " km";
        } 
        
        Time.timeScale = (float) distance/1000 + 1f;
    }
}
