using UnityEngine;


public class WeaphonesController : MonoBehaviour
{
    [SerializeField] protected GameObject[] obj; // 생성할 오브젝트
    [SerializeField] private Animator BowAnim; //석궁 애니메이터

    protected Camera mainCam; // 메인카메라
    private AudioSource audio; //오디오
    private Player player;
    private float currTime = 0;
    private float fillTime = 0.5f; //장전시간
    private TextUI tu;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        obj[0].SetActive(true);
        audio = obj[0].GetComponent<AudioSource>();
        player = GameObject.Find("HP").GetComponent<Player>();
        tu = GetComponent<TextUI>();
        tu.HitText();
        tu.ExText();
    }

    //    //어느위치에 오브젝트를 생성시킬지 position을 받아옴
    //    var pos = mainCam.ViewportToWorldPoint(new Vector3(0.45f, positionY, mainCam.nearClipPlane * cameraDistance));

    // Update is called once per frame
    void Update()
    {
        player.ScoreUp();
        //손가락을 터치했을 때 오브젝트를 생성
        if (Input.touchCount == 0) return;

        if (TouchHelper.IsDown)
        {
                WeaphonesShoot();

        }
    }

    //잡은 오브젝트를 카메라의 자식으로
    //HoldingObj.transform.SetParent(mainCam.transform); // 자식등록

    public void WeaphonesShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            BowAnim.SetTrigger("Shoot");
            audio.Play();

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                // enemy의 AddDamage 함수를 호출하고 싶다.
                enemy.EnemyTakeDamage(1);
            }

        }
    }

}

