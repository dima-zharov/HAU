using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{
    public GameController gk;
    public InputField inp;
    Cameras cam;
    PlayerMove move;
    public int i = 0;

    private void Start()
    {
        cam = gk.GetComponent<Cameras>();
        move = gk.GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (inp.text == "on")
        {
            if(i <= 2)
            {
                i++;
            }
            if(i == 1)
                cam.OnSpecial();

            cam.enabled = true;
            move.enabled = false;
            inp.gameObject.SetActive(false);
        }
    }

    public void OnSingleClick()
    {
        inp.gameObject.SetActive(false);
    }    
    public void OnDoubleClick()
    {
        inp.gameObject.SetActive(true);
        inp.ActivateInputField();
    }
}
