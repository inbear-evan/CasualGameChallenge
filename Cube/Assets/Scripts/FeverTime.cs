using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverTime : MonoBehaviour
{
    static public FeverTime instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
    }

    public Slider feverGauage;
    public Image sliderColor;
    public Material speedLineEffect;
    public ObstacleMove ObsMove;
    public Player player;
    public float gauageScore = 1;
    public float maxGauage = 10;
    // Start is called before the first frame update

    private void Start()
    {
        feverGauage.maxValue = maxGauage;
        feverGauage.value = 0;
        addFeverGauage(true);
    }
    private void FixedUpdate()
    {
        if (feverGauage.value + gauageScore >= feverGauage.maxValue)
        {
            addFeverGauage(false);
            ObstaclesManager.instance.activeFever();
        }
        else if (feverGauage.value - gauageScore <= 0)
        {
            ObstaclesManager.instance.inActiveFever();
            GameManager.instance.startPlayerColor();
            player.playerColorChange();
            addFeverGauage(true);
        }
    }
    public void setColor(Color color)
    {
        speedLineEffect.color = color;
    }

    public void setColorFeverGauage(Color color)
    {
        sliderColor.color = color;
    }

    /// <summary>
    /// 피버타임 게이지
    /// </summary>
    /// <param name="_bool">
    ///  true : Start
    ///  false : Stop
    /// </param>
    void addFeverGauage(bool _bool)
    {
        //GameManager.instance.setPlayerColor();
        //Debug.Log(_bool + " Add Gauage");
        if (_bool) StartCoroutine(nameof(toFeverTime));
        else StopCoroutine(nameof(toFeverTime));
    }
    /// <summary>
    /// 플레이어 속도에 따라 천천히 증가되도록 변경
    /// </summary>
    /// <returns></returns>
    IEnumerator toFeverTime()
    {
        while (feverGauage.value < feverGauage.maxValue)
        {
            if (Time.timeScale != 0 && !GameManager.instance.isPause)
            {
                feverGauage.value += gauageScore / ObsMove.speed;
            }
            yield return null;
        }

    }

    public void Fever()
    {
        StartCoroutine(nameof(feverTime));
    }
    IEnumerator feverTime()
    {
        while (feverGauage.value > 0)
        {
            //if (Input.GetKeyDown(KeyCode.Space)) break;
            if(!GameManager.instance.isPause)
                feverGauage.value -= gauageScore;
            yield return null;
        }
    }
}
