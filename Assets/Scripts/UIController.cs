using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private PlayerController _player;
    public TextMeshProUGUI distanceScored;
    [SerializeField] private FrameRate _frameRate;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.isStopping)
        {
            distanceScored.enabled = false;
            return;
        }

        int distance = Mathf.FloorToInt(_player.distance);
        distanceScored.text = distance + " m";
        
        if (distance > 1000)
        {
            distanceScored.text = distance / 1000 + " km";
        }

        if (!_frameRate.isGamePaused) {
            Time.timeScale = (float) distance/1000 + 1f;
        }
    }
}
