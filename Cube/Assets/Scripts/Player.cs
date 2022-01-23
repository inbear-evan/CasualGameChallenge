using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //    // 좌우로 이동
    //// 앞에 있는 장애물을 판별하기
    //// 점프
    [SerializeField] List<Transform> plyerSpot;
    [SerializeField] TrailRenderer trailRanderer; 
    [SerializeField] float jumpPower = 5;
    Rigidbody rb;
    bool IsRight = false, IsGround = false;

    void Start()
    {
        SetPosition();        
        rb = GetComponent<Rigidbody>();
        //Debug.Log("TestTESTSTESDSASD " + GameManager.instance.colorNumber);
        playerColorChange();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGround)
            {
                int num = GameManager.instance.colorNumber;
                Debug.Log(num);
                if (num == 2)
                {
                    Jump();
                }
                else
                {
                    movePosition();
                }
            }
        }
    }

    public void toggleGround()
    {
        if(IsGround) IsGround = false;
        else IsGround = true;
    }

    void Jump()
    {
        Debug.Log("JUMP");
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        
    }

    public void playerColorChange()
    {
        transform.GetComponent<MeshRenderer>().material.color = GameManager.instance.setPlayerColor();
        FeverTime.instance.setColorFeverGauage(transform.GetComponent<MeshRenderer>().material.color);
        trailRanderer.startColor = transform.GetComponent<MeshRenderer>().material.color;
    }

    private void SetPosition()
    {
        int posNum = Random.Range(0, 2);
        if (posNum == 0)
        {
            transform.position = plyerSpot[posNum].position;
            IsRight = true;
        }
        else
        {
            transform.position = plyerSpot[posNum].position;
        }
    }

    void movePosition()
    {
        if (IsRight)
        {
            transform.position = plyerSpot[1].position;
            IsRight = false;
        }
        else
        {
            transform.position = plyerSpot[0].position;
            IsRight = true;
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    switch (movementType)
    //    {
    //        case MovementType.Move:
    //            Move();
    //            break;
    //        case MovementType.Jump:
    //            Jump();
    //            break;
    //        default:
    //            break;
    //    }

    //    Ray ray = new Ray(transform.position, Vector3.forward);
    //    //left Ray
    //    Debug.DrawRay(transform.position, ray.direction*5, Color.red);
    //    Debug.DrawRay(transform.position, new Vector3(0.7f,0,1)*5, Color.red);
    //    //right Ray
    //    Debug.DrawRay(transform.position, ray.direction * 5, Color.red);
    //    Debug.DrawRay(transform.position, new Vector3(-0.7f, 0, 1) * 5, Color.red);

    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        if (hit.collider.CompareTag("ENEMY"))
    //        {
    //            SetState("Jump");
    //            return;
    //        }

    //    }

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (IsRight) IsRight = false;
    //        else IsRight = true;
    //    }

    //    if (IsRight)
    //    {
    //        transform.position = Vector3.Lerp(transform.position, rightPos.position, 0.1f);
    //    }
    //    else
    //    {
    //        transform.position = Vector3.Lerp(transform.position, leftPos.position, 0.1f);
    //    }
    //}



    //private void SetState(string state)
    //{

    //}
    //private bool Move()
    //{
    //    if (IsRight) IsRight = false;
    //    else IsRight = true;

    //    return IsRight;
    //}

    //private void Jump()
    //{
    //    if (!IsGround) return;

    //    IsGround = false;
    //    rb.AddForce(transform.up * jumpPower);


    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision == null) return;
    //    if (collision.gameObject.CompareTag("GROUND"))
    //    {
    //        IsGround = true;
    //    }
    //}
}
