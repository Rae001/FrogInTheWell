using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] Transform tfPlayer = null; // �÷��̾� ��ġ ���� ���� ����

    [SerializeField] Rigidbody2D player = null; // �÷��̾� Rigidbody2D ���� ����

    [SerializeField] PhysicsMaterial2D bounceMat, normalMat; // 2D ���� ������Ʈ ���� �浹�� �߻��� �� �Ͼ�� ������ ź���� ���� ���� ����

    public LayerMask groundLayer; // Physics.Raycast���� ����� ���̾� ���� ���� ����

    public bool isGrounded; // Physics2D.OverlapBox�� �浹�� ������ bool ���� ����

    private Camera _camera = null; // ī�޶� �⺻��ǥ������ ����

    private Vector2 firstClickPosition; // ���콺 Ŭ�� ��ǥ ��ġ�� ���� ���ͺ��� ����
    private Vector2 lastClickPosition;
    
    private float twoClickPositionDistance; // �� Ŭ�� ������ �Ÿ��� ���� ���� ���� ����

    public float jumpSpeed = 4; // ���� �ӵ� �ʱ�ȭ
    //========================================================================================================================================


    void Awake()
    {
        #region CS1612
        //gameObject.transform.position.x = DataManager.Instance.nowPlayer.playerX;
        //gameObject.transform.position.y = DataManager.Instance.nowPlayer.playerY;
        #endregion
        gameObject.transform.position = new Vector2(DataManager.Instance.nowPlayer.playerX, DataManager.Instance.nowPlayer.playerY);
    }
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        // Collider�� Box ���� �ִ��� Ȯ���Ͽ� true���� �ƴ��� üũ
        // Physics2D.OverlapBox(����Ʈ ��ġ, ũ�� , ����, LayerMask);
        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f), new Vector2(0.2f, 0.2f), 0, groundLayer);

        Drag();
        Drop();

        if (isGrounded) 
        {      
            player.sharedMaterial = normalMat; // isGrounded == true �϶� Friction 
        }
        else
        {   
            player.sharedMaterial = bounceMat; // isGrounded == false �϶� Bounciness
        }
    }

    void LateUpdate()
    {
        MoveCamera();
    }

    float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = start - end;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }

    public void Drag()
    {
        if (Input.GetMouseButtonDown(0)) // ù Ŭ�� ��ǥ�� firstClickPosition ������ �Ҵ�
        {
            firstClickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0)) // Drag�ϸ鼭 Ŭ���Ǵ� ��ǥ�� lastClickPosition ������ �Ҵ�
        {
            lastClickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        // �� ��ǥ ������ �Ÿ� ������ ���̸� twoClickPositionDistance ������ �Ҵ�
        twoClickPositionDistance = (firstClickPosition - lastClickPosition).magnitude;

        // �� ��ǥ ������ �Ÿ� ������ ���̰� 3.0f �̻��̸� ���� 3.0f�� ����
        if (twoClickPositionDistance >= 3.0f)
        {
            twoClickPositionDistance = 3.0f;
        }
        // Player ������Ʈ �ڽ�(playerVector)�� ������ ���
        tfPlayer.transform.rotation = Quaternion.Euler(0, 0, GetAngle(firstClickPosition, lastClickPosition));
    }

    public void Drop()
    {
        if (Input.GetMouseButtonUp(0) && isGrounded)
        {
            // Player ������Ʈ ���Ͱ� = playerVector ���� * �� Ŭ�� ��ǥ������ �Ÿ� * �ӵ�
            player.velocity = tfPlayer.transform.right * twoClickPositionDistance * jumpSpeed;
        }
    }


    public void MoveCamera()
    {
        if (player.transform.position.y < 5)
        {
            _camera.transform.position = new Vector3(0, 0, -10);
            
        }
        else if (player.transform.position.y < 15)
        {
            _camera.transform.position = new Vector3(0, 10, -10);
        }
        else if (player.transform.position.y < 20)
        {
            _camera.transform.position = new Vector3(0, 20, -10);
        }
    }
}
