using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // �÷��̾� ��ġ ���� ���� ����
    [SerializeField] Transform tfPlayer = null;


    // �÷��̾� Rigidbody2D ���� ����
    [SerializeField] Rigidbody2D player = null;


    // 2D ���� ������Ʈ ���� �浹�� �߻��� �� �Ͼ�� ������ ź���� ���� ���� ����
    [SerializeField] PhysicsMaterial2D bounceMat, normalMat;


    // Physics.Raycast���� ����� ���̾� ���� ���� ����
    public LayerMask groundLayer;

    // Physics2D.OverlapBox�� �浹�� ������ bool ���� ����
    public bool isGrounded;


    // ī�޶� �⺻��ǥ������ ����
    private Camera _camera = null;


    // ���콺 Ŭ�� ��ǥ ��ġ�� ���� ���ͺ��� ����
    private Vector2 firstClickPosition;
    private Vector2 lastClickPosition;


    // �� Ŭ�� ������ �Ÿ��� ���� ���� ���� ����
    private float twoClickPositionDistance;


    // ���� �ӵ� �ʱ�ȭ
    public float jumpSpeed = 4;

    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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
            // isGrounded == true �϶� Friction 
            player.sharedMaterial = normalMat;
        }
        else
        {
            // isGrounded == false �϶� Bounciness
            player.sharedMaterial = bounceMat;
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
