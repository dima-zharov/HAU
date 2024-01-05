using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject _down_player;
    public GameObject _up_player;
    public GameObject dust;
    public int _speed;
    private float _yD;
    private float _xD;
    private float _yUP;
    private float _xUP;
    private float _minX_up;
    private float _maxX_up;
    private float _minY_up;
    private float _maxY_up;
    private float _minX_d;
    private float _maxX_d;
    private float _minY_d;
    private float _maxY_d;
    [SerializeField] public RectTransform _placeDown;
    [SerializeField] public RectTransform _placeUp;
    [SerializeField] private int _rotSpeed;
    private Animator anim;
    private float touch;
    private float eulerX;

    void Start()
    {
        Input.multiTouchEnabled = true;
        Corners(_placeDown, ref _minY_d, ref _maxY_d, ref _minX_d, ref _maxX_d);
        Corners(_placeUp, ref _minY_up, ref _maxY_up, ref _minX_up, ref _maxX_up);

        if (_down_player.gameObject.tag == "PlayerDown" || _up_player.gameObject.tag == "PlayerUp")
        {
            _down_player.gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 0.55f, 0.6f, 1f, 1f);
        }


        anim = _down_player.GetComponent<Animator>();
        dust.SetActive(false);
    }

    private void Awake()
    {
        if (_up_player.gameObject.tag == "Player" || gameObject.tag == "PlayerUp")
        {
            _up_player.transform.Rotate(180, 0, 0);
        }

    }

    void Update()
    {
        Move(_up_player, _down_player, _minY_d, _maxY_d, _minX_d, _maxX_d, _minY_up, _maxY_up, _minX_up, _maxX_up);

    }

    private Vector3[] Move(GameObject playerUp, GameObject playerDown, float _minYD, float _maxYD, float _minXD, float _maxXD, float _minYUP, float _maxYUP, float _minXUP, float _maxXUP)
    {
        Vector3[] vect = { playerUp.transform.position, playerDown.transform.position };

        if (Input.touchCount > 0 && Input.touchCount <= 5)
        {
            for (int i = 0; i < Input.touchCount && i < 2; i++)
            {

                if (Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y < 0)
                {
                    playerDown.transform.position = Vector3.Lerp(playerDown.transform.position, Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Time.deltaTime * _speed);
                    _yD = Mathf.Clamp(playerDown.transform.position.y, _minYD, _maxYD);
                    _xD = Mathf.Clamp(playerDown.transform.position.x, _minXD, _maxXD);
                    playerDown.transform.position = new Vector3(_xD, _yD, 3.2f);

                    DownAnim(i);


                }
                if (Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y > 0)
                {
                    playerUp.transform.position = Vector3.Lerp(playerUp.transform.position, Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Time.deltaTime * _speed);
                    _yUP = Mathf.Clamp(playerUp.transform.position.y, _minYUP, _maxYUP);
                    _xUP = Mathf.Clamp(playerUp.transform.position.x, _minXUP, _maxXUP);
                    playerUp.transform.position = new Vector3(_xUP, _yUP, 3.2f);

                }
            }
        }
        else
        {
            playerUp.transform.position = playerUp.transform.position;
            playerDown.transform.position = playerDown.transform.position;


        }

        if (Input.touchCount == 0)
        {
            anim.SetBool("isMoving", false);
            if(eulerX > -0.05 && eulerX < 0.05)
            {

                _down_player.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                BaseRotat();
            }
            dust.SetActive(false);
        }

        return vect;
    }

    public void Corners(RectTransform _place, ref float _minY, ref float _maxY, ref float _minX, ref float _maxX)
    {
        Vector3[] corners = new Vector3[4];
        _place.GetWorldCorners(corners);
        _minX = corners[0].x;
        _maxX = corners[2].x;
        _minY = corners[0].y;
        _maxY = corners[1].y;
    }


    public void DownAnim(int i)
    {
        anim.SetBool("isMoving", true);

        dust.SetActive(true);

        if (Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x > _down_player.transform.position.x)
        {

            if (dust.transform.localPosition.x > 0)
                dust.transform.localPosition = new Vector3(_down_player.transform.localPosition.x - 0.25f, dust.transform.localPosition.y, dust.transform.localPosition.z);


            float deltaX = Input.GetTouch(i).position.x - touch;
            touch = Input.GetTouch(i).position.x;

            eulerX += deltaX * Time.deltaTime * _rotSpeed;
            eulerX = Mathf.Clamp(eulerX, -6.5f, 6.5f);
            _down_player.transform.eulerAngles = new Vector3(0, 0, -eulerX);
        }
        else if (Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x < _down_player.transform.position.x)
        {
            if (dust.transform.localPosition.x < 0)
                dust.transform.localPosition = new Vector3(_down_player.transform.localPosition.x + 0.25f, dust.transform.localPosition.y, dust.transform.localPosition.z);


            float deltaX = Input.GetTouch(i).position.x - touch;
            touch = Input.GetTouch(i).position.x;

            eulerX += deltaX * Time.deltaTime * _rotSpeed;
            eulerX = Mathf.Clamp(eulerX, -6.5f, 6.5f);
            _down_player.transform.eulerAngles = new Vector3(0, 0, -eulerX);
        }
    }

    public void BaseRotat()
    {
        if (_down_player.transform.localRotation.z > 0)
            eulerX += 28f * Time.deltaTime;
        else if (_down_player.transform.localRotation.z < 0)
            eulerX -= 28f * Time.deltaTime;
        

        _down_player.transform.eulerAngles = new Vector3(0, 0, -eulerX);
    }
}