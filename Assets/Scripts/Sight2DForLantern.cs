using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sight2DForLantern : MonoBehaviour
{
    [SerializeField] private bool m_bDebugMode = false;

    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float m_horizontalViewAngle = 0f; // �þ߰�
    [SerializeField] private float m_viewRadius = 1f; // �þ� ����
    [Range(-180f, 180f)]
    [SerializeField] private float m_viewRotateZ = 0f; // �þ߰��� ȸ����

    [SerializeField] private LayerMask m_viewTargetMask;       // �ν� ������ Ÿ���� ����ũ
    [SerializeField] private LayerMask m_viewObstacleMask;     // �ν� ���ع��� ����ũ 

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>(); // �ν��� ��ü���� ������ �����̳�

    private List<Enemy> enemyList = new List<Enemy>();

    private Player playerProp;

    private Enemy enemyProp;

    private Lantern lanternProp;

    private float m_horizontalViewHalfAngle = 0f; // �þ߰��� ���� ��


    private void Start()
    {
        lanternProp = GetComponent<Lantern>();
        playerProp = GetComponentInParent<Player>();

        m_horizontalViewAngle = lanternProp.Angle;
        m_viewRadius = lanternProp.Radius;
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;
    }

    private void OnDrawGizmos()
    {
        if (m_bDebugMode)
        {
            m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

            Vector3 originPos = transform.position;

            Gizmos.DrawWireSphere(originPos, m_viewRadius);

            Vector3 horizontalRightDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 horizontalLeftDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 lookDir = AngleToDirZ(m_viewRotateZ);

            Debug.DrawRay(originPos, horizontalLeftDir * m_viewRadius, Color.cyan);
            Debug.DrawRay(originPos, lookDir * m_viewRadius, Color.green);
            Debug.DrawRay(originPos, horizontalRightDir * m_viewRadius, Color.cyan);

            FindViewTargets();
        }
    }
    private void Update()
    {
        FindViewTargets();
    }

    public Collider2D[] FindViewTargets()
    {
        hitedTargetContainer.Clear();
        enemyList.Clear();

        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, m_viewRadius, m_viewTargetMask);

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

            // float angle = Vector3.Angle(lookDir, dir)
            // �Ʒ� �� ���� ���� �ڵ�� �����ϰ� ������. ���� ������ ����
            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= m_horizontalViewHalfAngle)
            {
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, m_viewRadius, m_viewObstacleMask);
                if (rayHitedTarget)
                {
                   
                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                }
                else
                {
                    enemyProp = hitedTarget.GetComponent<Enemy>();
                    if (enemyProp != null)
                        enemyProp.TriggerEnterOn(playerProp.collider2DProp);
                        enemyList.Add(enemyProp);
                    hitedTargetContainer.Add(hitedTarget);
                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, targetPos, Color.red);
                }
            }
        }
        lanternProp.enemyList = enemyList;

        if (hitedTargetContainer.Count > 0)
            return hitedTargetContainer.ToArray();
        else
            return null;
    }

    // -180~180�� ���� Up Vector ���� Local Direction���� ��ȯ������.
    private Vector2 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian));
    }
}