using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gateUp;
    public GameObject gateDown;
    public GameObject DownPlayer;
    private Animator anim;

    public TextMeshProUGUI down_score;
    public TextMeshProUGUI up_score;
    public TextMeshProUGUI Fdown_score;
    public TextMeshProUGUI Fup_score;
    public int scoreDown = 0;
    public int scoreUp = 0;

    public Ball ball;

    public Canvas Game_ov;

    void Start()
    {
        Game_ov.gameObject.SetActive(false);
        anim = DownPlayer.GetComponent<Animator>();
        Image image = gateUp.GetComponent<Image>();
        gateUp.GetComponent<BoxCollider>().size = new Vector3(image.sprite.rect.width + 80, image.sprite.rect.height - 100, gateUp.GetComponent<BoxCollider>().size.z);
        gateDown.GetComponent<BoxCollider>().size = new Vector3(image.sprite.rect.width + 80, image.sprite.rect.height - 100, gateUp.GetComponent<BoxCollider>().size.z);
    }

    public void DownScore()
    {

        scoreDown++;
        down_score.text = scoreDown.ToString();
        Fdown_score.text = ball.scoreDwn.ToString();


    }

    public void UpScore()
    {

        scoreUp++;
        up_score.text = scoreUp.ToString();
        Fup_score.text = ball.scoreUp.ToString();


    }

    public IEnumerator DownScoreEn()
    {
        anim.Play("goal");
        down_score.GetComponent<Animator>().SetBool("Goal", true);
        yield return new WaitForSeconds(1.05f);
        DownScore();
        yield return new WaitForSeconds(0.5f);
        down_score.GetComponent<Animator>().SetBool("Goal", false);
    }
    public IEnumerator UpScoreEn()
    {
        anim.Play("Lose");
        up_score.GetComponent<Animator>().SetBool("Goal", true);
        yield return new WaitForSeconds(1.05f);
        UpScore();
        yield return new WaitForSeconds(0.5f);
        up_score.GetComponent<Animator>().SetBool("Goal", false);
    }

    private void FixedUpdate()
    {
        down_score.text = scoreDown.ToString();
        up_score.text = scoreUp.ToString();       
        Fdown_score.text = ball.scoreDwn.ToString();
        Fup_score.text = ball.scoreUp.ToString();
        GameOver();
    }

    public void GameOver()
    {
        if (ball.scoreDwn >= 10 || ball.scoreUp >= 10)
        {

            Game_ov.gameObject.SetActive(true);
            gameObject.GetComponent<PlayerMove>().enabled = false;
          
        }
    }

    public IEnumerator RestCor()
    {
        ball.scoreDwn = 0;
        ball.scoreUp = 0;
        yield return new WaitForSeconds(0.7f);
        scoreDown = 0;
        scoreUp = 0;
        ball.goal.mute = false;
        Game_ov.gameObject.SetActive(false);
        gameObject.GetComponent<PlayerMove>().enabled = true;
        ball.transform.position = new Vector3(0, 0, 3.621f);
        ball.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        ball.gameObject.GetComponent<MeshRenderer>().enabled = true;
        ball.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        ball.gameObject.GetComponent<TrailRenderer>().enabled = true;
        ball.anim.Play("ball");
        ball._new.gameObject.SetActive(true);
        ball.sound.mute = false;
        yield return new WaitForSeconds(1f);
        ball.rb.isKinematic = false;
        ball._parts.gameObject.SetActive(true);
    }

    public void Rest()
    {
        StartCoroutine(RestCor());
    }

}
