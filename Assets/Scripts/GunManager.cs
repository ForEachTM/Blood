using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunManager : MonoBehaviour {

    Rocket Rocket;
    Bullet Bullet;

    GameObject Object;

    CameraController CameraController;

    SpriteRenderer SpriteRenderer;
    public Sprite[] GunSprites;
    public GameObject[] BulletPrefabs;

    public enum Guns { Pistol, Revolver, Shotgun, TommyGun, Minigun, RocketLauncher, Mine };

    Guns Gun, RandomGun;

    int BulletType;

    bool CanShot;

    Vector2 Velocity;

    float LifeTime;

    float KnockBack;
    float ScreenShake;

    float DelayBetweenShots;
    float Delay;

    void Start () {
        CameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        SpriteRenderer = GetComponent<SpriteRenderer>();

        CanShot = true;
    }

    void Update () {
        if (Delay <= 0)
        {
            CanShot = true;
            Delay = DelayBetweenShots;
        }
        else
        {
            Delay -= Time.deltaTime;
        }

        if (Input.GetKeyDown("u"))
        {
            DelayBetweenShots -= 0.1f;
        }
	}

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && CanShot)
        {
            CanShot = false;

            CameraController.ShakeCamera(ScreenShake, 0.01f);

            Object = Instantiate(BulletPrefabs[BulletType], transform.position, Quaternion.identity);

            switch (BulletType)
            {
                case 0:
                    Velocity.y = GetDispersion();

                    Bullet = Object.gameObject.GetComponent<Bullet>();
                    Bullet.SetLifeTime(LifeTime);
                    Bullet.SetVelocity(Velocity);
                    Bullet.Flip(transform.parent.localScale);
                    break;

                case 1:
                    Bullet = Object.gameObject.GetComponent<Bullet>();
                    Bullet.SetLifeTime(LifeTime);
                    Bullet.SetVelocity(Velocity);
                    Bullet.Flip(transform.parent.localScale);
                    break;

                case 2:
                    Rocket = Object.gameObject.GetComponent<Rocket>();
                    Rocket.SetLifeTime(LifeTime);
                    Rocket.SetVelocity(Velocity);
                    Rocket.Flip(transform.parent.localScale);
                    break;
            }
        }
    }

    public float GetDispersion()
    {
        switch (Gun)
        {
            case Guns.Shotgun: Velocity.y = Random.Range(-2f, 2f); break;
            case Guns.TommyGun: Velocity.y = Random.Range(-2f, 2f); break;
            case Guns.Minigun: Velocity.y = Random.Range(-6f, 6f); break;
        }

        return Velocity.y;
    }

    public void SetGun(Guns Gun)
    {
        this.Gun = Gun;

        SpriteRenderer.sprite = GunSprites[(int)Gun];

        Velocity = Vector2.zero;
        Delay = 0;
        DelayBetweenShots = 0.3f;
        LifeTime = 2f;
        KnockBack = 0;
        ScreenShake = 0;

        switch (Gun)
        {
            case Guns.Pistol:
                BulletType = 0;
                Velocity.x = 30f;
                break;

            case Guns.Revolver:
                BulletType = 1;
                Velocity.x = 40f;
                ScreenShake = 10f;
                break;

            case Guns.Shotgun:
                BulletType = 0;
                Velocity.x = 20f;
                LifeTime = 0.2f;
                KnockBack = 0.2f;
                ScreenShake = 10f;
                break;

            case Guns.TommyGun:
                BulletType = 0;
                DelayBetweenShots = 0.05f;
                Velocity.x = 30f;
                KnockBack = 0.5f;
                ScreenShake = 8f;
                break;

            case Guns.Minigun:
                BulletType = 0;
                DelayBetweenShots = 0.01f;
                Velocity.x = 30f;
                KnockBack = 0.3f;
                ScreenShake = 10f;
                break;

            case Guns.RocketLauncher:
                BulletType = 2;
                DelayBetweenShots = 1f;
                Velocity.x = 10f;
                break;

            case Guns.Mine:
                BulletType = 3;
                DelayBetweenShots = 4f;
                break;

        }
    }

    public void SetRandomGun()
    {
        while (RandomGun == Gun || RandomGun == Guns.Shotgun || RandomGun == Guns.Mine) {
            RandomGun = (Guns)Random.Range(0, Enum.GetNames(typeof(Guns)).Length);
        }

        SetGun(RandomGun);

        Gun = RandomGun;
    }
}
