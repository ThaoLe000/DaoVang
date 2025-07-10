using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [Header("Lắc")]
    [SerializeField] private float swaySpeed;
    [SerializeField] private float minZ, maxZ;
    private bool isSwayRight = true;
    public bool isSwaying;
    [SerializeField] private float currentZ;

    [Header("Thả")]
    [SerializeField] private float dropSpeed;
    [SerializeField] private float maxX, minX, minY;
    public bool isDropping = false;

    [Header("Kéo")]
    public float pullSpeed;
    public bool isPulling = false;
    public float initPullSpeed;

    [Header("Thuốc nổ")]
    public bool isThrowExplosive = false;

    private Vector3 initialPosition;
    private GoldMinerAnimator goldMinerAnimator;
    
    void Start()
    {
        isSwaying = true;
        currentZ = transform.eulerAngles.z;
        initialPosition = transform.position;
        initPullSpeed = pullSpeed;
        goldMinerAnimator = FindObjectOfType<GoldMinerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSwaying)
        {
            RotationZ();

            if (Input.GetMouseButtonDown(0))
            {
                isSwaying = false;
                isDropping = true;
            }
        }
        else
        {
            MoveHook();
        }
        gameObject.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, transform.position);
    }
    private void RotationZ()
    {
        if (isSwayRight)
        {
            currentZ += swaySpeed * Time.deltaTime;
        }
        else
        {
            currentZ -= swaySpeed * Time.deltaTime;
        }

        if(currentZ >= maxZ) isSwayRight = false;
        if(currentZ <= minZ) isSwayRight = true;
        
        transform.rotation = Quaternion.Euler(0,0,currentZ);
    }
    private void MoveHook()
    {
        goldMinerAnimator.ThrowHook();
        if (isDropping)
        {
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
            if (transform.position.x >= maxX || transform.position.x <= minX
                || transform.position.y <= minY )
            {
                isDropping = false;
                isPulling = true;
            }
        }
        if (isPulling)
        {
            goldMinerAnimator.PullHook();
            if (Input.GetMouseButtonDown(1)) 
            {
                if(GameManager.instance.explosive > 0)
                {
                    goldMinerAnimator.ThrowExplosive();
                    isThrowExplosive = true;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, initialPosition, pullSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, initialPosition) < 0.1f)
            {
                goldMinerAnimator.Stop();
                isPulling = false;
                pullSpeed = initPullSpeed;
                transform.position = initialPosition;
                isSwaying = true;
                isThrowExplosive = false;
            }
        }
    }
}
