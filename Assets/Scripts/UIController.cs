using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
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
    }
}
