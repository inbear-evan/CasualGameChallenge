using UnityEngine;

public class PlayerMovement : MonoBehaviour
{



    //    // 좌우로 이동
    //// 앞에 있는 장애물을 판별하기
    //// 점프
    [SerializeField] Transform leftPos, rightPos;
    [SerializeField] float jumpPower = 5;
    bool IsRight = false, IsGround = true;
    Rigidbody rb;

    //enum MovementType
    //{
    //    Move,
    //    Jump
    //}
    //MovementType movementType;
    //// Start is called before the first frame update
    void Start()
    {
        SetPosition();
        rb = GetComponent<Rigidbody>();
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

    private void SetPosition()
    {
        //movementType = MovementType.Move;
        int posNum = Random.Range(0, 2);
        if (posNum == 0)
        {
            transform.position = rightPos.position;
            IsRight = true;
        }
        else
        {
            transform.position = leftPos.position;
        }
    }

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
