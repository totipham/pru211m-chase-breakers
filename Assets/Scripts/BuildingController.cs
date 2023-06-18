using UnityEngine;

public class BuildingController : MonoBehaviour
{
    PlayerController player;

    public float buildingHeight;
    public float buildingRigth;
    public float screenRight;
    BoxCollider2D collider;

    bool didGenerated = false;

    public void Awake()
    {
        player = GameObject.Find("PLayer").GetComponent<PlayerController>();

        collider = GetComponent<BoxCollider2D>();
        buildingHeight = transform.position.y + (collider.size.y / 2);
        screenRight = Camera.main.transform.position.x * 2;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x + Time.fixedDeltaTime;

        buildingRigth = transform.position.x + (collider.size.x / 2);

        if (buildingRigth < 0)
        {
            Destroy(gameObject);
        }

        if (!didGenerated)
        {
            if (buildingRigth < screenRight)
            {
                didGenerated = true;
                generateBuidling();
            }
        }
    }

    private void generateBuidling()
    {
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;
        pos.x = screenRight + 30;
        pos.y = transform.position.y;
        go.transform.position = pos;
    }
}

