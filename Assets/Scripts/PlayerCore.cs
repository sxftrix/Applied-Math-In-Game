using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [Header("Move Settings")]
    public float laneSpeed;
    public float jumpPower;
    public float gravity;
    public float groundY;
    public float spawnZ;

    public float leftX;
    public float midX;
    public float rightX;

    [Header("Stats")]
    public int hp;
    public int maxHP;
    public float regenRate;

    /* [Header("Anim")]
    public Animator anim; */

    private int lane = 1; // 0 left, 1 mid, 2 right
    private float yVel;
    private Vector3 target;

    private void Start()
    {
        target = new Vector3(midX, groundY, spawnZ);
        transform.position = target;
        UpdateHP();
    }

    private void Update()
    {
        HandleInput();
        Move(Time.deltaTime);
        RegenHP(Time.deltaTime);
        // UpdateAnim();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lane = Mathf.Clamp(lane - 1, 0, 2);
            // anim?.SetTrigger("MoveLeft");
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            lane = Mathf.Clamp(lane + 1, 0, 2);
            // anim?.SetTrigger("MoveRight");
        }
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(transform.position.y, groundY))
        {
            yVel = jumpPower;
            // anim?.SetTrigger("Jump");
        }
    }

    private void Move(float dt)
    {
        float x = lane == 0 ? leftX : (lane == 2 ? rightX : midX);
        target = new Vector3(x, transform.position.y, spawnZ);

        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, x, laneSpeed * dt);

        yVel += gravity * dt;
        float y = pos.y + yVel * dt;
        if (y <= groundY)
        {
            y = groundY;
            yVel = 0f;
        }

        transform.position = new Vector3(pos.x, y, spawnZ);
    }

    private void RegenHP(float dt)
    {
        if (hp < maxHP)
        {
            hp += Mathf.RoundToInt(regenRate * dt);
            if (hp > maxHP) hp = maxHP;
            UpdateHP();
        }
    }

    private void UpdateHP()
    {
        if (GameCore.main != null)
            GameCore.main.SetHP(hp, maxHP);
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            GameCore.main.GameOver();
        }
        UpdateHP();
    }

    /* private void UpdateAnim()
    {
        if (anim == null) return;
        bool grounded = Mathf.Approximately(transform.position.y, groundY);
        bool moving = Mathf.Abs(transform.position.x - target.x) > 0.01f;
        anim.SetBool("Idle", grounded && !moving);
    } */
}
