using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunManager : MonoBehaviour {

    public GameObject playerPrefab;
    public SpriteRenderer spriteRenderer;
    public Sprite[] guns;
    public GameObject[] bulletPrefabs;

    BulletManager bulletManager;
    Player player;
    public CameraController cameraController;

    public enum Guns { Nothing, Pistol, Revolver, Shotgun, TommyGun, Minigun, RocketLauncher, Mine };

    Guns gun, randomGun;

    int bulletType;

    bool canShot;

    float delayBetweenShots;
    float delay;
    float speed;
    float knockback;
    float dispersion;
    float screenShake;
    float lifetime;

    // Use this for initialization
    void Start () {
        player = playerPrefab.GetComponent<Player>();
        cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update () {
        if (delay <= 0)
        {
            canShot = true;
            delay = delayBetweenShots;
        }
        else
        {
            delay -= Time.deltaTime;
        }

        if (Input.GetKeyDown("u"))
        {
            delayBetweenShots -= 0.1f;
        }
	}

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && canShot && gun != Guns.Nothing)
        {
            canShot = false;

            cameraController.ShakeCamera(screenShake, 0);

            GameObject bullet = Instantiate(bulletPrefabs[bulletType], transform.position, Quaternion.identity);

            /*bulletManager = bullet.GetComponent<BulletManager>();
            bulletManager.SetSpeed(speed);
            
            bulletManager.SetLifeTime(lifetime);
            bulletManager.SetDispersion(getDispersion());
            bulletManager.SetBullet(bulletType);
            bulletManager.Flip(player.facingRight);*/
            //player.Knockback(knockback);

        }
    }

    public float getDispersion()
    {
        dispersion = 0;
        switch (gun)
        {
            case Guns.Shotgun: dispersion = Random.Range(-2f, 2f); break;
            case Guns.TommyGun: dispersion = Random.Range(-2f, 2f); break;
            case Guns.Minigun: dispersion = Random.Range(-6f, 6f); break;
        }

        return dispersion;
    }

    public void SetGun(Guns gun)
    {
        this.gun = gun;

        spriteRenderer.sprite = guns[(int)gun];

        delay = 0;
        knockback = 0;
        speed = 30f;
        delayBetweenShots = 0.3f;
        lifetime = 2f;
        screenShake = 0;

        switch (gun)
        {
            case Guns.Pistol:
                bulletType = 0;
                break;

            case Guns.Revolver:
                speed = 40f;
                bulletType = 1;
                screenShake = 10;
                break;

            case Guns.Shotgun:
                knockback = 0.2f;
                bulletType = 0;
                lifetime = 0.2f;
                screenShake = 10;
                break;

            case Guns.TommyGun:
                speed = 30f;
                delayBetweenShots = 0.05f;
                knockback = 0.5f;
                bulletType = 0;
                screenShake = 8;
                break;

            case Guns.Minigun:
                speed = 30f;
                delayBetweenShots = 0.01f;
                knockback = 0.3f;
                bulletType = 0;
                screenShake = 10;
                break;

            case Guns.RocketLauncher:
                speed = 10f;
                delayBetweenShots = 1f;
                bulletType = 2;
                break;

            case Guns.Mine:
                delayBetweenShots = 4f;
                bulletType = 3;
                break;

        }
    }

    public void SetRandomGun()
    {
        while (randomGun == gun || randomGun == Guns.Shotgun || randomGun == Guns.Mine) { 
            randomGun = (Guns)Random.Range(1, Enum.GetNames(typeof(Guns)).Length);
        }

        SetGun(randomGun);

        gun = randomGun;
    }
}
