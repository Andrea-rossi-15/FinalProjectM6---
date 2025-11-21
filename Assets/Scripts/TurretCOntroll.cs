using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretCOntroll : MonoBehaviour
{
    [SerializeField] float FireRange;
    [SerializeField] float FireRate;
    [SerializeField] GameObject Target;
    [SerializeField] float NextShootTimer;

    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        NextShootTimer = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        RotateToTarget();

        NextShootTimer -= Time.deltaTime;
        if (NextShootTimer <= 0f)
        {
            NextShootTimer = 1f / FireRate;
            if (Vector3.Distance(transform.position, Target.transform.position) <= FireRange)
            {
                Fire();
            }
        }
    }
    protected void RotateToTarget()
    {
        Vector3 direction = Target.transform.position - transform.position;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f);
        Gizmos.DrawWireSphere(transform.position, FireRange);
    }
    public void Fire()
    {
        if (Bullet != null && FirePoint != null)
        {
            Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
        }
    }
}
