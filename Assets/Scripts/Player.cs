using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // 플레이어 위치 정보 변수 선언
    [SerializeField] Transform tfPlayer = null;


    // 플레이어 Rigidbody2D 변수 선언
    [SerializeField] Rigidbody2D player = null;


    // 2D 물리 오브젝트 간에 충돌이 발생할 때 일어나는 마찰과 탄성을 조정 변수 선언
    [SerializeField] PhysicsMaterial2D bounceMat, normalMat;


    // Physics.Raycast에서 사용할 레이어 지정 변수 선언
    public LayerMask groundLayer;

    // Physics2D.OverlapBox의 충돌을 감지할 bool 변수 선언
    public bool isGrounded;


    // 카메라가 기본좌표지도로 설정
    private Camera _camera = null;


    // 마우스 클릭 좌표 위치를 담을 벡터변수 선언
    private Vector2 firstClickPosition;
    private Vector2 lastClickPosition;


    // 두 클릭 사이의 거리의 값을 담을 변수 선언
    private float twoClickPositionDistance;


    // 점프 속도 초기화
    public float jumpSpeed = 4;

    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        // Collider가 Box 내에 있는지 확인하여 true인지 아닌지 체크
        // Physics2D.OverlapBox(포인트 위치, 크기 , 각도, LayerMask);
        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f), new Vector2(0.2f, 0.2f), 0, groundLayer);

        Drag();
        Drop();

        if (isGrounded) 
        {
            // isGrounded == true 일때 Friction 
            player.sharedMaterial = normalMat;
        }
        else
        {
            // isGrounded == false 일때 Bounciness
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
        if (Input.GetMouseButtonDown(0)) // 첫 클릭 좌표를 firstClickPosition 변수에 할당
        {
            firstClickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0)) // Drag하면서 클릭되는 좌표를 lastClickPosition 변수에 할당
        {
            lastClickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        // 두 좌표 사이의 거리 벡터의 길이를 twoClickPositionDistance 변수에 할당
        twoClickPositionDistance = (firstClickPosition - lastClickPosition).magnitude;

        // 두 좌표 사이의 거리 벡터의 길이가 3.0f 이상이면 값을 3.0f로 고정
        if (twoClickPositionDistance >= 3.0f)
        {
            twoClickPositionDistance = 3.0f;
        }
        // Player 오브젝트 자식(playerVector)의 방향을 계산
        tfPlayer.transform.rotation = Quaternion.Euler(0, 0, GetAngle(firstClickPosition, lastClickPosition));
    }

    public void Drop()
    {
        if (Input.GetMouseButtonUp(0) && isGrounded)
        {
            // Player 오브젝트 벡터값 = playerVector 방향 * 두 클릭 좌표사이의 거리 * 속도
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
