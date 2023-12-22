using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTypeChecker : MonoBehaviour
{
    #region Layer Number
    private const int groundLayer = 3; // ground ���̾� ��ȣ
    private const int waterLayer = 4; // water ���̾� ��ȣ
    #endregion

    #region Info
    [Header("Info")]

    [SerializeField, Tooltip("���Ƿ� ���� �������� ������!")] private char currentMoveType; // ���� �̵� Ÿ�� �����
    #region currentMoveType ĸ��ȭ
    public char CurrentMoveType { get { return currentMoveType; } }
    #endregion

    [SerializeField] private bool isGrounded = false; // ���� ���� �����
    #region isGrounded ĸ��ȭ
    public bool IsGrounded { get { return isGrounded; } }
    #endregion

    [SerializeField] private bool isInWater = false; // ���� ���� �����
    #region isInWater ĸ��ȭ
    public bool IsInWater { get { return isInWater; } }
    #endregion
    #endregion

    #region Triggers
    [Space(5), Header("Sundry  Status")]

    [SerializeField]
    private bool waterProof = false;

    /*
    [SerializeField] private bool groundOut = false; // ������ �������� �� Ʈ����ó�� �۵��� ����
    #region statusChanged ĸ��ȭ
    public bool GroundOut { get { return groundOut; } }
    #endregion

    [SerializeField] private bool waterOut = false; // ������ ���������� �� Ʈ����ó�� �۵��� ����
    #region statusChanged ĸ��ȭ
    public bool WaterOut { get { return waterOut; } }
    #endregion
    */
    #endregion

    private Stack<char> groundStack = new Stack<char>(); // �ٸ� Ÿ���� ���� ���ÿ� ����� �� ����� ������ �����ϴ� �뵵
    private char preWater;
    
    private char thisMoveType = '0';
    private int thisLayerType;
    private int groundCount = 0; // ��� �ִ� ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        thisMoveType = collision.tag[0]; // ���� ���� Ÿ���� ����
        thisLayerType = collision.gameObject.layer; // ���� ���� ���̾ ����

        if (thisLayerType == groundLayer) // Ground ���̾ ����� ���
        {
            if (collision.CompareTag("WaterProof")) // ��� ������ ���
            {
                waterProof = true; // ��� ON
            }
            else
            {
                isGrounded = true; // ���� ���� ON

                groundCount++; // ��� �ִ� �� �� + 1

                groundStack.Push(thisMoveType); // ���� ���� Ÿ���� groundStack��  �׾Ƶ�

                if (!isInWater) // ������ �ƴ� ���
                {
                    currentMoveType = thisMoveType; // ���� �̵�Ÿ�� �� ���� ���� Ÿ��
                }
            }
        }

        else if (thisLayerType == waterLayer) // Water ���̾ ����� ���
        {
            if (waterProof) // ����� ���
            {
                preWater = thisMoveType; // ���� �� ������ �ش� �� �Ӽ� ����
            }
            else // ����� �ƴ� ���
            {
                isInWater = true; // ���� ���� ON
                currentMoveType = thisMoveType; // ���� �̵�Ÿ�� �� ���� ���� Ÿ��
            }
        }
    }
    private char peek; // ���� ����� �ӽ� �����
    private void OnTriggerExit2D(Collider2D collision)
    {
        thisMoveType = collision.tag[0]; // ���� ���� ģ���� �±׸� ����
        thisLayerType = collision.gameObject.layer; // ���� ���� ģ���� ���̾ ����

        if (thisLayerType == groundLayer) // : Ground ���̾ ������ ���
        {
            if (collision.CompareTag("WaterProof")) // ���� ���� ��� �Ӽ��� ���
            {
                waterProof = false;

                if (preWater != '0') // ���� ��� �־��� ���
                {
                    isInWater = true; // ���� ���� ON
                    currentMoveType = preWater; // �̵� Ÿ���� ��� �ִ� ���� Ÿ������ ����
                }
            }
            else
            {
                groundCount--; // ��� �ִ� �� �� - 1

                if (groundCount == 0) // ����ִ� ���� ���� ���
                {
                    groundStack.Clear(); // GroundStack �ʱ�ȭ

                    isGrounded = false; // ���� ���� OFF
                }
                else // ����ִ� ���� ���� ���
                {
                    if (thisMoveType == groundStack.Peek()) // ���� ���� ģ���� Ÿ���� groundStack�� ������ ���� ��
                    {
                        groundStack.Pop(); // groundStack�� ����� �ϳ� ����

                        if ((!isInWater) && groundStack.TryPeek(out peek)) // ������ �ƴ� ��� + ������ ����Ⱑ ���� ���
                        {
                            currentMoveType = peek; // �̵� Ÿ���� groundStack�� ���ο� ������ ����
                        }
                    }
                }
            }
        }

        else if (thisLayerType == waterLayer) // Water ���̾ ������ ���
        {
            if (isInWater) // ���� �����̾��� ���
            {
                isInWater = false; // ���� ���� OFF
            }
            else // ���� ������ �ƴϾ��� ���( ��������� �Ǿ� �־��� ���� ���� �� )
            {
                preWater = '0'; // ���� ���� ���� OFF
            }
        }
    }
    /*
    private IEnumerator GroundOutTrigger() // Info ���ι� �� ������ �ٲ� �� ����� �ڷ�ƾ
    {
        waterOut = true; // ���� Ʈ���� Ȱ��ȭ
        
        yield return wait_1_Frame; // null ���� �� 1������ ��ٸ�

        waterOut = false; // ���� Ʈ���� ��Ȱ��ȭ
    }
    private IEnumerator WaterOutTrigger() // Info ���ι� �� ������ �ٲ� �� ����� �ڷ�ƾ
    {
        groundOut = true; // ���� Ʈ���� Ȱ��ȭ

        yield return wait_1_Frame; // null ���� �� 1������ ��ٸ�

        groundOut = false; // ���� Ʈ���� ��Ȱ��ȭ
    }
    */
}