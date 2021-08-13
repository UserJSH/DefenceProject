using UnityEngine;


public class WeaphonesController : MonoBehaviour
{
    [SerializeField] protected GameObject[] obj; // ������ ������Ʈ
    [SerializeField] private Animator BowAnim; //���� �ִϸ�����

    protected Camera mainCam; // ����ī�޶�
    private AudioSource audio; //�����
    private Player player;
    private float currTime = 0;
    private float fillTime = 0.5f; //�����ð�
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

    //    //�����ġ�� ������Ʈ�� ������ų�� position�� �޾ƿ�
    //    var pos = mainCam.ViewportToWorldPoint(new Vector3(0.45f, positionY, mainCam.nearClipPlane * cameraDistance));

    // Update is called once per frame
    void Update()
    {
        player.ScoreUp();
        //�հ����� ��ġ���� �� ������Ʈ�� ����
        if (Input.touchCount == 0) return;

        if (TouchHelper.IsDown)
        {
                WeaphonesShoot();

        }
    }

    //���� ������Ʈ�� ī�޶��� �ڽ�����
    //HoldingObj.transform.SetParent(mainCam.transform); // �ڽĵ��

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
                // enemy�� AddDamage �Լ��� ȣ���ϰ� �ʹ�.
                enemy.EnemyTakeDamage(1);
            }

        }
    }

}

