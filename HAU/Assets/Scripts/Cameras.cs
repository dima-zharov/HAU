using UnityEngine;


public class Cameras : MonoBehaviour
{
    public Camera MainCamera;
    public Camera FrontalCamera;
    public Camera OlegCamera;
    public GameObject InputField;
    public Transform Target;
    [SerializeField] private float speed;
    private Quaternion rotation;
    private Touch touch;
    PlayerMove pm;
    Cameras cam;
    public InputText inp;
    float x = 0;
    float y = 173;    
    void Start()
    {
        pm = GetComponent<PlayerMove>();
        cam = GetComponent<Cameras>();
    }

    public void OnSpecial()
    {
        MainLogic(false, false, true);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && OlegCamera.gameObject.activeSelf)
        {
            touch = Input.GetTouch(0);


            x -= FrontalCamera.ScreenToWorldPoint(touch.position).y * speed * Time.deltaTime;
            y += FrontalCamera.ScreenToWorldPoint(touch.position).x * speed * Time.deltaTime;

            y = Mathf.Clamp(y, 140, 195);
            x = Mathf.Clamp(x, -5, 15);

            rotation = Quaternion.Euler(x, y, 0);

            OlegCamera.transform.rotation = rotation;                                                    
        }
    }

    public void BackButton()
    {

        pm.enabled = true;
        cam.enabled = false;
        inp.inp.text = "";
        inp.i = 0;
        MainLogic(true, false, false);
    }

    public void ButtonChangeCamera()
    {
        if (FrontalCamera.gameObject.activeSelf)
        {
            MainLogic(false, true, false);
        }
        else if (OlegCamera.gameObject.activeSelf)
        {
            MainLogic(false, false, true);
        }
    }

    public void MainLogic(bool t1, bool t2, bool t3)
    {
        MainCamera.gameObject.SetActive(t1);
        OlegCamera.gameObject.SetActive(t2);
        FrontalCamera.gameObject.SetActive(t3);
    }

}
