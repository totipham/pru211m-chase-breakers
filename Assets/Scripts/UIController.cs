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

    // Start is called before the first frame update
    void Start()
    {

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

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        UnityEngine.UIElements.Button button = root.Q<UnityEngine.UIElements.Button>("ButtonStart");
        button.clicked += () =>
        {
            Debug.Log("Button clicked");
        };
    }
}
