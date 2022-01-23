using UnityEngine;

public class TestCode : MonoBehaviour
{
    
    Vector3 st, ed;
    public GameObject effect;
    public TrailRenderer trailRenderer;
    Rigidbody body;
    public float speed = 5;
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        //effect.transform.position = ObjectToScreenPosition(effect, Camera.main);
    }
    // Update is called once per frame
    int i = 0;
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            body.AddForce(Vector3.up * speed, ForceMode.Impulse);
        //if (Input.GetMouseButtonDown(0))
        //{
        //    st = Input.mousePosition;
        //}
        //else if (Input.GetMouseButton(0))
        //{
        //    ed = Input.mousePosition;
        //    Vector3 dir = (st - ed).normalized;
        //    transform.Rotate(new Vector3(-dir.y, dir.x, dir.z) * 2, Space.World);
        //    st = ed;
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        hit.transform.GetComponent<Debris>().Explosion();
        //    }
        //}
    }

    Vector3 ObjectToScreenPosition(GameObject go, Camera cam)
    {
        return cam.WorldToScreenPoint(go.transform.position);
    }
    
    public void SetCube()
    {
        Instantiate(effect,Vector3.zero,Quaternion.identity);
    }
}
