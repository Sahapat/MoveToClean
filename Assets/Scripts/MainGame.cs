using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ItemType
{
    Blue,
    Green,
    Yellow,
}

public class MainGame : MonoBehaviour
{
    public float maxTime = 30;
    public Text timeText;
    public Text correctText;
    public Text incorrectText;

    public LayerMask dragLayer;
    public LayerMask dropLayer;

    public GameObject ObjectToMove;
    public Vector3 objCenter;
    public Vector3 lastPos;
    public Vector3 clickPosition;
    public Vector3 offset;
    public Vector3 newObjCenter;

    private RaycastHit dragHit;
    private RaycastHit dropHit;
    public bool isDrag = false;

    void Start()
    {
        GameCore.Instance.correct = 0;
        GameCore.Instance.incorrect = 0;
        GameCore.Instance.time = maxTime;
    }

    void Update()
    {
        if (GameCore.Instance.isPause)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out dragHit, float.MaxValue, dragLayer))
            {
                ObjectToMove = dragHit.collider.gameObject;
                objCenter = ObjectToMove.transform.position;
                lastPos = ObjectToMove.transform.position;
                clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset = clickPosition - objCenter;
                isDrag = true;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (isDrag)
            {
                clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newObjCenter = clickPosition - offset;
                ObjectToMove.transform.position = new Vector3(newObjCenter.x, newObjCenter.y, objCenter.z);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;

            if (this.ObjectToMove != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out dropHit, float.MaxValue, dropLayer))
                {
                    var drag = ObjectToMove.GetComponent<Drag>();
                    var drop = dropHit.collider.GetComponent<Drop>();

                    if (drag.type == drop.type)
                    {
                        GameCore.Instance.correct += 1;
                        this.correctText.text = GameCore.Instance.correct.ToString();
                        GameCore.Instance.PlayCorrect();
                    }
                    else
                    {
                        GameCore.Instance.incorrect += 1;
                        this.incorrectText.text = GameCore.Instance.incorrect.ToString();
                        GameCore.Instance.PlayInCorrect();
                    }

                    Destroy(ObjectToMove);

                    if (FindObjectsOfType<Drag>().Length - 1 <= 0)
                    {
                        SceneManager.LoadScene("Ending");
                    }
                }
                else
                {
                    ObjectToMove.transform.position = lastPos;
                    ObjectToMove = null;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (GameCore.Instance.isPause)
        {
            return;
        }
        
        GameCore.Instance.time -= Time.deltaTime;
        timeText.text = GameCore.Instance.time.ToString("0.00");
        if (GameCore.Instance.time <= 0)
        {
            SceneManager.LoadScene("Timesup");
        }
    }
}
