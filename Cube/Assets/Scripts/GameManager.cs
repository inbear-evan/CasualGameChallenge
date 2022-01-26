using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Transform> plyerSpot;
    static public Color[] obsColor = new Color[2];
    public GameObject gameOver;
    public TMP_Text score; //Plane바닥에 있는 점수
    public Text scoreUI; //게임오버시 나오는 점수

    public int colorNumber = 0;
    static public GameManager instance;
    public bool isPause = false;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
        startPlayerColor();
    }

    private void Start()
    {
        gameOver.gameObject.SetActive(false);
        score.text = "0";
    }
    private void Update()
    {
        startPlayerColor(); 
    }

    public void startPlayerColor()
    {
        Ray rightRay = new Ray(plyerSpot[0].position + new Vector3(0, 0, 0), Vector3.forward);
        Ray leftRay = new Ray(plyerSpot[1].position + new Vector3(0, 0, 0), Vector3.forward);

        if (Physics.Raycast(rightRay, out RaycastHit rightHit))
        {
            //Debug.Log(rightHit.transform.name);
            Debug.DrawLine(plyerSpot[0].position + new Vector3(0, 0, 0), rightHit.point, Color.red);
            obsColor[0] = rightHit.transform.GetComponent<MeshRenderer>().material.color;
        }
        if (Physics.Raycast(leftRay, out RaycastHit leftHit))
        {
            //Debug.Log(leftHit.transform.name);
            Debug.DrawLine(plyerSpot[1].position + new Vector3(0, 0, 0), leftHit.point, Color.red);
            obsColor[1] = leftHit.transform.GetComponent<MeshRenderer>().material.color;
        }
    }

    public Color setPlayerColor()
    {
        Color color;
        int num = Random.Range(0, obsColor.Length+1);
        if(num >= obsColor.Length)
        {
            color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        else color = obsColor[num];
        colorNumber = num;
        Debug.Log($"GameManager {num}");
        return color;
    }

    public void addScore()
    {
        int scoreBuf = int.Parse(score.text) + 1;
        score.text = scoreBuf.ToString();
    }

    public void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        scoreUI.text = score.text;
        Time.timeScale = 0.1f;
        StartCoroutine(Fade(1, gameOver.GetComponent<Image>().color, 0.005f));
    }

    IEnumerator Fade(float alpha, Color color, float value)
    {
        Color panelColor = color;
        while (panelColor.a != alpha)
        {
            if (Input.GetKeyDown(KeyCode.Space)) break;
            //Debug.Log(panelColor + " " + alpha);
            panelColor.a = Mathf.Lerp(panelColor.a, alpha, value);
            gameOver.GetComponent<Image>().color = panelColor;
            yield return null;
        }
    }

}
