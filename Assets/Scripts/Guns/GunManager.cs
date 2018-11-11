using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunManager : MonoBehaviour {

    ABullet aBullet;

    Player player;

    GameObject Object;

    CameraController CameraController;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public Sprite[] GunSprites;
    public GameObject[] BulletPrefabs;

    public enum Guns { PISTOL, REVOLVER, SHOTGUN, TOMMYGUN, MINIGUN, ROCKETLAUNCHER, MINE, DOUBLEPISTOL, GRENADELAUNCHER };

    Guns Gun, RandomGun;

    public enum Bullets { SMALL, LARGE, FRACTION, ROCKET, MINE , GRENADE}

    Bullets Bullet;

    bool CanShot;

    Vector2 Velocity;

    float LifeTime;

    int Damage;
    float KnockBack;
    float ScreenShake;

    float DelayBetweenShots;
    float Delay;

    bool automatic;

    void Start () {
        CameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        SetGun(Guns.GRENADELAUNCHER);
    }

    void Update () {
        if (Delay <= 0)
        {
            CanShot = true;
            Delay = DelayBetweenShots;
        }
        else if(!CanShot)
        {
            Delay -= Time.deltaTime;
        }
	}

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && CanShot && automatic)
        {
            CanShot = false;

            CameraController.ShakeCamera(ScreenShake, 0.1f);

            player.Knockback = KnockBack;

            SetDispersion();

            if (Gun == Guns.MINE)
            {
                Instantiate(BulletPrefabs[(int)Bullet], transform.position, Quaternion.identity);
            }
            else if (Gun == Guns.SHOTGUN)
            {
                for (int i = 0; i < 6; i++)
                {
                    Object = Instantiate(BulletPrefabs[(int)Bullet], new Vector2(transform.position.x + GetSpawnPoint() + i * transform.parent.localScale.x / 2, transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                    aBullet = Object.gameObject.GetComponent<ABullet>();
                    aBullet.LifeTime = Random.Range(LifeTime, LifeTime * 2);
                    aBullet.Velocity = new Vector2(Random.Range(Velocity.x, Velocity.x * 2), Velocity.y);
                    aBullet.Damage = Damage;
                    aBullet.Flip(transform.parent.localScale);
                }
            }
            else if(Gun == Guns.GRENADELAUNCHER)
            {
                Object = Instantiate(BulletPrefabs[(int)Bullet], new Vector2(transform.position.x + GetSpawnPoint(), transform.position.y), Quaternion.identity);
                aBullet = Object.gameObject.GetComponentInChildren<ABullet>();
                aBullet.Velocity = Velocity;
                aBullet.Flip(transform.parent.localScale);
            }
            else
            {
                Object = Instantiate(BulletPrefabs[(int)Bullet], new Vector2(transform.position.x + GetSpawnPoint(), transform.position.y), Quaternion.identity);
                aBullet = Object.gameObject.GetComponent<ABullet>();
                aBullet.LifeTime = LifeTime;
                aBullet.Velocity = Velocity;
                aBullet.Damage = Damage;
                aBullet.Flip(transform.parent.localScale);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space) && !automatic)
        {
            CameraController.ShakeCamera(ScreenShake, 0.1f);

            player.Knockback = KnockBack;

            if (Gun == Guns.DOUBLEPISTOL)
            {
                Object = Instantiate(BulletPrefabs[(int)Bullet], new Vector2(transform.position.x - GetSpawnPoint(), transform.position.y), Quaternion.identity);
                aBullet = Object.gameObject.GetComponent<ABullet>();
                aBullet.LifeTime = LifeTime;
                aBullet.Velocity = Velocity * -1;
                aBullet.Damage = Damage;
                aBullet.Flip(transform.parent.localScale);

                Object = Instantiate(BulletPrefabs[(int)Bullet], new Vector2(transform.position.x + GetSpawnPoint(), transform.position.y), Quaternion.identity);
                aBullet = Object.gameObject.GetComponent<ABullet>();
                aBullet.LifeTime = LifeTime;
                aBullet.Velocity = Velocity;
                aBullet.Damage = Damage;
                aBullet.Flip(transform.parent.localScale);
            }
            else
            {
                Object = Instantiate(BulletPrefabs[(int)Bullet], new Vector2(transform.position.x + GetSpawnPoint(), transform.position.y), Quaternion.identity);
                aBullet = Object.gameObject.GetComponent<ABullet>();
                aBullet.LifeTime = LifeTime;
                aBullet.Velocity = Velocity;
                aBullet.Damage = Damage;
                aBullet.Flip(transform.parent.localScale);
            }
        }
        else
        {
            player.Knockback = 0;
        }
    }

    public void SetGun(Guns Gun)
    {
        this.Gun = Gun;

        spriteRenderer.sprite = GunSprites[(int)Gun];

        Velocity = Vector2.zero;
        Delay = 0;
        DelayBetweenShots = 0.3f;
        KnockBack = 0;
        LifeTime = 2f;
        ScreenShake = 0;
        automatic = true;

        switch (Gun)
        {
            case Guns.PISTOL:
                Bullet = Bullets.SMALL;
                DelayBetweenShots = .02f;
                automatic = false;
                Damage = 1;
                Velocity.x = 30f;
                ScreenShake = 5f;
                break;

            case Guns.DOUBLEPISTOL:
                Bullet = Bullets.SMALL;
                DelayBetweenShots = .02f;
                automatic = false;
                Damage = 1;
                Velocity.x = 30f;
                ScreenShake = 5f;
                break;

            case Guns.REVOLVER:
                Bullet = Bullets.LARGE;
                DelayBetweenShots = .03f;
                automatic = false;
                Damage = 2;
                Velocity.x = 40f;
                KnockBack = 6f;
                ScreenShake = 8f;
                break;

            case Guns.SHOTGUN:
                Bullet = Bullets.FRACTION;
                DelayBetweenShots = 1f;
                Damage = 1;
                Velocity.x = 15f;
                LifeTime = 0.2f;
                KnockBack = 8f;
                ScreenShake = 10f;
                break;

            case Guns.TOMMYGUN:
                Bullet = Bullets.SMALL;
                DelayBetweenShots = 0.05f;
                Damage = 1;
                Velocity.x = 30f;
                KnockBack = 15f;
                ScreenShake = 5f;
                break;

            case Guns.MINIGUN:
                Bullet = Bullets.SMALL;
                DelayBetweenShots = 0.01f;
                Damage = 1;
                Velocity.x = 30f;
                KnockBack = 19.5f;
                ScreenShake = 6f;
                break;

            case Guns.ROCKETLAUNCHER:
                Bullet = Bullets.ROCKET;
                DelayBetweenShots = 2f;
                Velocity.x = 10f;
                KnockBack = 6f;
                break;

            case Guns.GRENADELAUNCHER:
                Bullet = Bullets.GRENADE;
                DelayBetweenShots = 2f;
                Velocity.x = 0.2f;
                Velocity.y = 0.05f;
                KnockBack = 6f;
                break;

            case Guns.MINE:
                Bullet = Bullets.MINE;
                DelayBetweenShots = 1.5f;
                break;

        }
    }

    public void SetRandomGun()
    {
        while (RandomGun == Gun) {
            RandomGun = (Guns)Random.Range(0, Enum.GetNames(typeof(Guns)).Length);
        }

        SetGun(RandomGun);

        Gun = RandomGun;
    }

    public float GetSpawnPoint()
    {
        return Mathf.Sign(transform.parent.localScale.x) * spriteRenderer.bounds.size.x;
    }

    public void SetDispersion()
    {
        switch (Gun)
        {
            case Guns.SHOTGUN: Velocity.y = Random.Range(-1f, 1f); break;
            case Guns.TOMMYGUN: Velocity.y = Random.Range(-2f, 2f); break;
            case Guns.MINIGUN: Velocity.y = Random.Range(-6f, 6f); break;
        }
    }

    public int GetDamage()
    {
        return Damage;
    }
}
