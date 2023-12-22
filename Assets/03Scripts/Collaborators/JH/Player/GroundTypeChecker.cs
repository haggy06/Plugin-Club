using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTypeChecker : MonoBehaviour
{
    #region Layer Number
    private const int groundLayer = 3; // ground 레이어 번호
    private const int waterLayer = 4; // water 레이어 번호
    #endregion

    #region Info
    [Header("Info")]

    [SerializeField, Tooltip("임의로 값을 변경하지 마세요!")] private char currentMoveType; // 현재 이동 타입 저장용
    #region currentMoveType 캡슐화
    public char CurrentMoveType { get { return currentMoveType; } }
    #endregion

    [SerializeField] private bool isGrounded = false; // 지상 판정 저장용
    #region isGrounded 캡슐화
    public bool IsGrounded { get { return isGrounded; } }
    #endregion

    [SerializeField] private bool isInWater = false; // 수중 판정 저장용
    #region isInWater 캡슐화
    public bool IsInWater { get { return isInWater; } }
    #endregion
    #endregion

    #region Triggers
    [Space(5), Header("Sundry  Status")]

    [SerializeField]
    private bool waterProof = false;

    /*
    [SerializeField] private bool groundOut = false; // 땅에서 떨어졌을 때 트리거처럼 작동할 거임
    #region statusChanged 캡슐화
    public bool GroundOut { get { return groundOut; } }
    #endregion

    [SerializeField] private bool waterOut = false; // 물에서 빠져나왔을 때 트리거처럼 작동할 거임
    #region statusChanged 캡슐화
    public bool WaterOut { get { return waterOut; } }
    #endregion
    */
    #endregion

    private Stack<char> groundStack = new Stack<char>(); // 다른 타입의 땅을 동시에 밟았을 때 생기는 오류를 방지하는 용도
    private char preWater;
    
    private char thisMoveType = '0';
    private int thisLayerType;
    private int groundCount = 0; // 밟고 있는 땅의 갯수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        thisMoveType = collision.tag[0]; // 지금 닿은 타입을 저장
        thisLayerType = collision.gameObject.layer; // 지금 닿은 레이어를 저장

        if (thisLayerType == groundLayer) // Ground 레이어에 닿았을 경우
        {
            if (collision.CompareTag("WaterProof")) // 방수 구역일 경우
            {
                waterProof = true; // 방수 ON
            }
            else
            {
                isGrounded = true; // 지상 판정 ON

                groundCount++; // 밟고 있는 땅 수 + 1

                groundStack.Push(thisMoveType); // 지금 닿은 타입을 groundStack에  쌓아둠

                if (!isInWater) // 수중이 아닐 경우
                {
                    currentMoveType = thisMoveType; // 현재 이동타입 → 지금 닿은 타입
                }
            }
        }

        else if (thisLayerType == waterLayer) // Water 레이어에 닿았을 경우
        {
            if (waterProof) // 방수일 경우
            {
                preWater = thisMoveType; // 예비 물 판정에 해당 물 속성 대입
            }
            else // 방수가 아닐 경우
            {
                isInWater = true; // 수중 판정 ON
                currentMoveType = thisMoveType; // 현재 이동타입 → 지금 닿은 타입
            }
        }
    }
    private char peek; // 스택 꼭대기 임시 저장용
    private void OnTriggerExit2D(Collider2D collision)
    {
        thisMoveType = collision.tag[0]; // 지금 나간 친구의 태그를 저장
        thisLayerType = collision.gameObject.layer; // 지금 나간 친구의 레이어를 저장

        if (thisLayerType == groundLayer) // : Ground 레이어를 나갔을 경우
        {
            if (collision.CompareTag("WaterProof")) // 나간 땅이 방수 속성일 경우
            {
                waterProof = false;

                if (preWater != '0') // 물에 잠겨 있었을 경우
                {
                    isInWater = true; // 수중 판정 ON
                    currentMoveType = preWater; // 이동 타입을 잠겨 있던 물의 타입으로 변경
                }
            }
            else
            {
                groundCount--; // 밟고 있는 땅 수 - 1

                if (groundCount == 0) // 밟고있는 땅이 없을 경우
                {
                    groundStack.Clear(); // GroundStack 초기화

                    isGrounded = false; // 지상 판정 OFF
                }
                else // 밟고있는 땅이 있을 경우
                {
                    if (thisMoveType == groundStack.Peek()) // 지금 나간 친구의 타입이 groundStack의 꼭대기와 같을 때
                    {
                        groundStack.Pop(); // groundStack의 꼭대기 하나 삭제

                        if ((!isInWater) && groundStack.TryPeek(out peek)) // 수중이 아닐 경우 + 스택의 꼭대기가 있을 경우
                        {
                            currentMoveType = peek; // 이동 타입을 groundStack의 새로운 꼭대기로 변경
                        }
                    }
                }
            }
        }

        else if (thisLayerType == waterLayer) // Water 레이어를 나갔을 경우
        {
            if (isInWater) // 수중 판정이었을 경우
            {
                isInWater = false; // 수중 판정 OFF
            }
            else // 수중 판정이 아니었을 경우( 방수코팅이 되어 있었을 때만 말이 됨 )
            {
                preWater = '0'; // 예비 물속 판정 OFF
            }
        }
    }
    /*
    private IEnumerator GroundOutTrigger() // Info 삼인방 중 뭔가가 바뀔 시 실행될 코루틴
    {
        waterOut = true; // 스탯 트리거 활성화
        
        yield return wait_1_Frame; // null 리턴 시 1프레임 기다림

        waterOut = false; // 스탯 트리거 비활성화
    }
    private IEnumerator WaterOutTrigger() // Info 삼인방 중 뭔가가 바뀔 시 실행될 코루틴
    {
        groundOut = true; // 스탯 트리거 활성화

        yield return wait_1_Frame; // null 리턴 시 1프레임 기다림

        groundOut = false; // 스탯 트리거 비활성화
    }
    */
}