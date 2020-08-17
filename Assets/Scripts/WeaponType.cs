using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : ScriptableObject {

    public enum FiringShape { Straight, Cone };

    public GameObject projectilePrefab;
    public AudioClip projectileSFX;
    public float projectileSpeed;
    public float fireCooldown;
    public int numberOfProjectiles;
    public float fireRadius;
    public bool useShootersRotation;
    public FiringShape firingShape = FiringShape.Straight;


    public void FireStraight(GameObject shooter)
    {
        for (int numShot = 1; numShot <= numberOfProjectiles; numShot++)
        {
            FireStraightOnce(shooter, numShot);
        }

    }

    public void FireStraightOnce(GameObject shooter, int whichShot)
    {
        float projectileXOffsetStep = (numberOfProjectiles > 1) ? (fireRadius / (numberOfProjectiles - 1)) : 0;
        float projectileXOffset = (whichShot > 1) ? 
            ((-fireRadius / 2) + (whichShot - 1) * projectileXOffsetStep) : -fireRadius / 2 ;
        float direction = projectileSpeed > 0 ? 0 : 180;
        float rotation = 0;

        if (useShootersRotation)
        {
            rotation = -shooter.transform.eulerAngles.z;
        }


        if (numberOfProjectiles == 0)
            projectileXOffset = 0;

        GameObject projectile = Instantiate(projectilePrefab,
            shooter.transform.position + Vector3.right * projectileXOffset, Quaternion.identity);
        projectile.transform.SetParent(GameObject.Find("Projectiles").transform);
        //projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(
            projectileSpeed * Mathf.Sin(Mathf.Deg2Rad * rotation),
            projectileSpeed * Mathf.Cos(Mathf.Deg2Rad * rotation));
        projectile.GetComponent<Rigidbody2D>().MoveRotation(rotation + direction);

    }

    public void FireCone(GameObject shooter)
    {

        for (int numShot = 1; numShot <= numberOfProjectiles; numShot++)
        {
            FireInConeOnce(shooter, numShot);
        }
    }

    public void FireInConeOnce(GameObject shooter, int whichShot)
    {
        float angleStepPerProjectile = (numberOfProjectiles > 1) ? (fireRadius / (numberOfProjectiles - 1)) : 0;
        float angle = (numberOfProjectiles > 1) ? (-fireRadius / 2) : 0;
        float direction = projectileSpeed > 0 ? 0 : 180;

        if (whichShot > 1)
            angle += angleStepPerProjectile * (whichShot - 1);
        if (useShootersRotation)
        {
            var rotation = shooter.transform.eulerAngles.z;
            angle -= rotation;

        }


        GameObject projectile = Instantiate(projectilePrefab, shooter.transform.position, Quaternion.identity);
        projectile.transform.SetParent(GameObject.Find("Projectiles").transform);
        projectile.GetComponent<Rigidbody2D>().MoveRotation(-angle + direction);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(
            projectileSpeed * Mathf.Sin(Mathf.Deg2Rad * angle),
            projectileSpeed * Mathf.Cos(Mathf.Deg2Rad * angle));

    }




    //public void FireStraight(GameObject shooter)
    //{
    //    float projectileXOffset = (numberOfProjectiles > 1) ? (-fireRadius / 2) : 0;
    //    float projectileXOffsetStep = (numberOfProjectiles > 1) ? (fireRadius / (numberOfProjectiles - 1)) : 0;
    //    float direction = projectileSpeed > 0 ? 0 : 180;
    //    for (int numShot = 1; numShot <= numberOfProjectiles; numShot++)
    //    {
    //        FireStraightOnce(shooter, direction, projectileXOffset);
    //        projectileXOffset += projectileXOffsetStep;
    //    }

    //}


    //private void FireStraightOnce(GameObject shooter, float direction, float projectileXOffset = 0)
    //{
    //    GameObject projectile = Instantiate(projectilePrefab,
    //        shooter.transform.position + Vector3.right * projectileXOffset, Quaternion.identity);
    //    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
    //    projectile.GetComponent<Rigidbody2D>().MoveRotation(direction);
    //}

    //public void FireCone(GameObject shooter)
    //{
    //    float angle = (numberOfProjectiles > 1) ? (-fireRadius / 2) : 0;
    //    float angleStepPerProjectile = (numberOfProjectiles > 1) ? (fireRadius / (numberOfProjectiles - 1)) : 0;
    //    float direction = projectileSpeed > 0 ? 0 : 180;
    //    for (int numShot = 1; numShot <= numberOfProjectiles; numShot++)
    //    {
    //        GameObject projectile = Instantiate(projectilePrefab, shooter.transform.position, Quaternion.identity);
    //        projectile.GetComponent<Rigidbody2D>().MoveRotation(-angle + direction);
    //        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(
    //            projectileSpeed * Mathf.Sin(Mathf.Deg2Rad * angle),
    //            projectileSpeed * Mathf.Cos(Mathf.Deg2Rad * angle));
    //        angle += angleStepPerProjectile;
    //    }
    //}
}
