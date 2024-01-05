using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 oldPosition;
    public GameController gk;
    public float strong;
    public float rotatstr;
    public Rigidbody rb;
    public GameObject p1;
    public GameObject p2;
    public Animator anim;
    public AudioSource sound;
    public ParticleSystem goal_parts;
    public ParticleSystem _parts;
    public ParticleSystem _new;

    public AudioClip[] clips;
    public AudioSource goal;

    public int scoreDwn;
    public int scoreUp;

    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.angularDrag = rotatstr * 0.1f;
    }


    private void OnTriggerEnter(Collider other)
    {




        if (other.tag == "Player")
        {
            Force(p2);
            _parts.Play();
            sound.pitch = Random.Range(1f, 1.2f);
            sound.volume = Random.Range(0.7f, 0.85f);
            sound.PlayOneShot(clips[0]);
        }
        else if (other.tag == "PlayerDown")
        {
            Force(p1);
            _parts.Play();
            sound.pitch = Random.Range(1f, 1.2f);
            sound.volume = Random.Range(0.7f, 0.85f);
            sound.PlayOneShot(clips[0]);
        }
        else if (other.tag == "PlayerUp")
        {
            Force(p2);
            _parts.Play();
            sound.pitch = Random.Range(1f, 1.2f);
            sound.volume = Random.Range(0.7f, 0.85f);
            sound.PlayOneShot(clips[0], Random.Range(0.7f, 0.85f));
        }

        else if (other.tag == "gate_down")
        {
            scoreUp++;
            StartCoroutine(gk.UpScoreEn());
            StartCoroutine(BallNew(-1));



        }
        else if (other.tag == "gate_up")
        {
            scoreDwn++;
            StartCoroutine(gk.DownScoreEn());
            StartCoroutine(BallNew(1));


        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "grass")
        {
            _parts.Play();
            sound.volume = 1;
            sound.pitch = Random.Range(0.8f, 0.9f);
            sound.PlayOneShot(clips[0]);
        }
    }

    private void Force(GameObject player)
    {
        Vector3 pf = player.GetComponent<Transform>().position;

        float strUp = 120;
        float strDown = 120;

        if (transform.position.y <= 0.75 && transform.position.y >= -0.75)
        {
            strUp *= 1.8f;
            strDown *= 1.8f;
        }

        if (gameObject.transform.position.y >= pf.y && player.tag == "PlayerDown")
            rb.AddForce(-pf * strong * strDown * Time.deltaTime, ForceMode.Impulse);
        else if (gameObject.transform.position.y < pf.y && player.tag == "PlayerDown")
            rb.AddForce(pf * strong * strDown * Time.deltaTime, ForceMode.Impulse);
        else if (gameObject.transform.position.y >= pf.y && (player.tag == "PlayerUp" || player.tag == "Player"))
            rb.AddForce(pf * strong * strUp * Time.deltaTime, ForceMode.Impulse);
        else if (gameObject.transform.position.y < pf.y && (player.tag == "PlayerUp" || player.tag == "Player"))
            rb.AddForce(-pf * strong * strUp * Time.deltaTime, ForceMode.Impulse);

        strUp = 120;
        strDown = 120;
    }

    public IEnumerator BallNew(int y, int y1 = 0)
    {
        if (scoreUp > 9 || scoreDwn > 9)
        {

            gameObject.GetComponent<TrailRenderer>().enabled = false;
            anim.Play("ball0");
            _parts.gameObject.SetActive(false);
            goal.mute = true;
            sound.mute = true;
            yield return new WaitForSeconds(0.07f);
            _new.gameObject.SetActive(false);
            rb.isKinematic = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            transform.position = new Vector3(0, 0, 3.62f);

        }
        else if (scoreUp < 10 && scoreDwn < 10)
        {
            goal.Play();
            anim.Play("ball0");
            goal_parts.GetComponent<ShadowScript>().enabled = false;
            goal_parts.Play();
            yield return new WaitForSeconds(0.07f);
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            anim.Play("ball");
            rb.isKinematic = true;
            _parts.Stop(true);
            transform.position = new Vector3(0, y, 3.62f);
            _new.Play();
            yield return new WaitForSeconds(0.8f);
            rb.isKinematic = false;
            gameObject.GetComponent<TrailRenderer>().enabled = true;
            yield return new WaitForSeconds(1.1f);
            goal_parts.GetComponent<ShadowScript>().enabled = true;
        }
        else if (scoreDwn == 0 && scoreUp == 0)
        {
            goal.Play();
            goal_parts.GetComponent<ShadowScript>().enabled = false;
            goal_parts.Play();
            anim.Play("ball0");
            yield return new WaitForSeconds(0.07f);
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            anim.Play("ball");
            rb.isKinematic = true;
            _parts.Stop(true);
            transform.position = new Vector3(0, y1, 3.62f);
            _new.Play();
            yield return new WaitForSeconds(0.8f);
            rb.isKinematic = false;
            gameObject.GetComponent<TrailRenderer>().enabled = true;
            yield return new WaitForSeconds(1.1f);
            goal_parts.GetComponent<ShadowScript>().enabled = true;
        }
    }
}
