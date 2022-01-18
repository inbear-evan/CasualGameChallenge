using UnityEngine;

public class TestCode : MonoBehaviour
{

    Vector3 st, ed;
    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                hit.transform.GetComponent<Debris>().Explosion();
            }
        }
    }

    public void SetCube()
    {
        transform.rotation = Quaternion.identity;
    }
}
