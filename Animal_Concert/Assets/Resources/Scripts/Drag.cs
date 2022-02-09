//***************************************
// EDITOR : CHA Hee Gyoung
// LAST EDITED DATE : 2022.02.09
// Script Purpose : Main UI [Drag Function]
//***************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Text text1;
    public Vector2 DirectionToMove;
    public MusicInfo musicInfo;
    public bool isStart;
    public bool isDone;

    public GameObject panel0;

    private Vector2 mouseOffset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseOffset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
        
        //Debug.Log(string.Format("{0}",mouseOffset.x) + String.Format("{0}",mouseOffset.y));
    }

    public void OnDrag(PointerEventData eventData)
    {
        float x = transform.position.x;
        //transform.position = eventData.position + mouseOffset;
        x = eventData.position.x + mouseOffset.x;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        //Debug.Log(transform.position);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("드래그 중지(오브젝트 안에서 마우스 뗌)");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 중지(오브젝트 안이든 밖이든)");
        DirectionToMove = transform.position; 
        SelectNumber();
    }

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < transform.childCount; i++)
        {
            //Debug.Log(transform.GetChild(i).transform.position);
        }
        
        
    }

    void SelectNumber()//자식객체의 포지션 가져오고, 가운데에 위치시킬 객체 정하고, 뮤직인포의 셀렉트넘버 바꿔주기, 그리고 가운데로 위치시키기
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector2 value = transform.GetChild(i).position;
            
            if (Mathf.Abs(1440 - value.x) < 393)
            {
                Debug.Log("지금!");
                musicInfo.SelectedNumber = i;
                if (1440 - value.x > 0)
                {
                    //StartCoroutine(MoveLeft());
                }
                    
                else
                {
                    //StartCoroutine(MoveRight());
                }
                break;
            }
        }
        
    }

    IEnumerator MoveRight()
    {
        isDone = false;
        int loopNum = 0;
        //Debug.Log(1440 - transform.GetChild(musicInfo.SelectedNumber).position.x);
        while (Mathf.Abs(1440 - transform.GetChild(musicInfo.SelectedNumber).position.x) > 0.5)
        {
            isStart = true;
            Debug.Log("코루틴 실행중");
            transform.Translate(new Vector3(15,0,0) * Time.deltaTime );
            Debug.Log(1440 - transform.GetChild(musicInfo.SelectedNumber).position.x);
            
            //transform.position = Vector3.Lerp(transform.position,new Vector3(transform.GetChild(musicInfo.SelectedNumber).position.x, transform.position.y, 0f),Time.deltaTime);
            
            if(loopNum++ > 10000)
                throw new Exception("Infinite Loop");
        }
        isDone = true;

        
        yield return null;
    }
    
    IEnumerator MoveLeft()
    {
        isDone = false;
        int loopNum = 0;
        Debug.Log(1440 - transform.GetChild(musicInfo.SelectedNumber).position.x);
        while (Mathf.Abs(1440 - transform.GetChild(musicInfo.SelectedNumber).position.x) > 0.5)
        {
            isStart = true;
            Debug.Log("코루틴 실행중");
            transform.Translate(new Vector3(-15,0,0) * Time.deltaTime );
            
            //transform.position = Vector3.Lerp(transform.position,new Vector3(transform.GetChild(musicInfo.SelectedNumber).position.x, transform.position.y, 0f),Time.deltaTime);
            
            if(loopNum++ > 10000)
                throw new Exception("Infinite Loop");
        }
        isDone = true;

        
        yield return null;
    }
    

    void StartPositioning()//더할건지 뺄건지
    {
        
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            text1.text = "터치됨!";

        }

        if (isStart)
        {
            if (isDone)
            {
                Debug.Log("코루틴 정지");
                StopCoroutine(MoveLeft());
                StopCoroutine(MoveRight());
                isStart = false;
            }
                
        }
            
    }

    
}
