using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CameraMove : MonoBehaviour
{
    public List<Transform> camSpot;

    public int minTime = 10;
    public int maxTime = 30;
    public TMP_Text moveTimingText;
    public TMP_Text score;
    float moveTiming;
    float currentTime = 0;
    float moveTimingBuf = 0;


    private void Start()
    {
        moveTiming = maxTime;
        moveTimingText.text = moveTiming.ToString();
        moveTimingBuf = 0;
        StartCoroutine(nameof(downTiming));
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isPause)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= moveTiming)
            {
                currentTime = 0;
                int spot = Random.Range(0, camSpot.Count);
                if (spot != moveTimingBuf) moveTimingBuf = spot;
                else
                {
                    spot = (spot + 1) % camSpot.Count;
                }
                score.transform.rotation = Quaternion.Euler(90, 0, -camSpot[spot].eulerAngles.y);

                //if (spot == 3)
                //{
                //    score.transform.rotation = Quaternion.Euler(90, 0, camSpot[spot].eulerAngles.y);
                //}
                //else
                //{
                //    score.transform.rotation = Quaternion.Euler(90, 0, 0);
                //}
                StartCoroutine(moveSpot(spot));
            }
        }

    }
    IEnumerator downTiming()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!GameManager.instance.isPause)
            {
                int timingBuf = int.Parse(moveTimingText.text) - 1;
                moveTimingText.text = timingBuf.ToString();
            }
        }
    }
    IEnumerator moveSpot(int _spot)
    {
        //while (transform.position != camSpot[_spot].position)
        //while (transform.rotation != camSpot[_spot].rotation)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space)) break;
        //    transform.position = Vector3.MoveTowards(transform.position, camSpot[_spot].position, 0.05f);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, camSpot[_spot].rotation, 5f);
        //    yield return null;
        //}
        yield return null;
        transform.position = camSpot[_spot].position;
        transform.rotation = camSpot[_spot].rotation;

        moveTiming = Random.Range(minTime, maxTime);
        moveTimingText.text = moveTiming.ToString();
        currentTime = 0;
        
    }
}

