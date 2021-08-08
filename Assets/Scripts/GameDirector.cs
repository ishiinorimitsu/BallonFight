using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private GoalChecker goalHousePrefab;            // �S�[���n�_�̃v���t�@�u���A�T�C��

    [SerializeField]
    private PlayerController playerController;      // �q�G�����L�[�ɂ��� Yuko_Player �Q�[���I�u�W�F�N�g���A�T�C��

    [SerializeField]
    private FloorGenerator[] floorGenerators;       // floorGenerator �X�N���v�g�̃A�^�b�`����Ă���Q�[���I�u�W�F�N�g���A�T�C��

    [SerializeField]
    private RandomObjectGenerator[] randomObjectGenerators;

    [SerializeField]
    private AudioManager audioManager;

    private bool isSetUp;                           // �Q�[���̏�������p�Btrue �ɂȂ�ƃQ�[���J�n

    private bool isGameUp;                          // �Q�[���I������p�Btrue �ɂȂ�ƃQ�[���I��

    private int generateCount;                      //���̐�����

    public int GenerateCount
    {
        set
        {
            generateCount = value;
            Debug.Log("������ / �N���A�ڕW�� : " + generateCount + " / " + clearCount);

            if(generateCount >= clearCount)
            {
                GenerateGoal();
                GameUp();
            }
        }
        get
        {
            return generateCount;
        }
    }

    public int clearCount;

    void Start()
    {
        StartCoroutine(audioManager.PlayBGM(0));

        isGameUp = false;
        isSetUp = false;

        SetUpFloarGenerators();

        StopGenerators();
    }

    private void SetUpFloarGenerators()
    {
        for(int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SetUpGenerator(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isFirstGenerateBallon && isSetUp == false)
        {
            StartCoroutine(audioManager.PlayBGM(1));

            // ��������
            isSetUp = true;

            // TODO �e�W�F�l���[�^�𓮂����n�߂�
            Debug.Log("�����X�^�[�g");

            ActivateGenerators();
        }
    }
    private void GenerateGoal()
    {
        // �S�[���n�_�𐶐�
        GoalChecker goalHouse = Instantiate(goalHousePrefab);

        // TODO �S�[���n�_�̏����ݒ�
        Debug.Log("�S�[���n�_ ����");
    }

    /// <summary>
    /// �Q�[���I��
    /// </summary>
    public void GameUp()
    {

        // �Q�[���I��
        isGameUp = true;

        // TODO �e�W�F�l���[�^���~
        Debug.Log("������~");

        StopGenerators();
    }

    private void StopGenerators()
    {
        for(int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(false);
        }
        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(false);
        }
    }

    private void ActivateGenerators()
    {
        for (int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(true);
        }

        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(true);
        }
    }
}
