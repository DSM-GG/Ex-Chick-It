using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [System.Serializable]
    private struct Status {
        public float knockBackPower;
        public float attackSpeed;
        public float range;
        public float reLoadTime;
        public int magazine;
    }

    [SerializeField]
    private Status status;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private LayerMask attackLayer;

    private WaitForSeconds delay, reLoad;
    private int maxMagazine;
    private bool bAttack, bReLoad;

    private void Awake() {
        delay = new WaitForSeconds(status.attackSpeed);
        reLoad = new WaitForSeconds(status.reLoadTime);

        maxMagazine = status.magazine;
        bAttack = true;
    }

    // 벽에 부딫히면 넉백을 멈춘다
    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Destroy Wall"))
            StopCoroutine("EnemyKnockBack");
    }

    public void Attack() {
        if (!bAttack || bReLoad) return;

        RaycastHit2D hit = Physics2D.Raycast(muzzle.position, transform.up, status.range, attackLayer.value);
        Debug.DrawRay(muzzle.position, transform.up * status.range, Color.red, 0.1f);

        if (hit.collider != null) {
            // 현재 진행 중인 넉백을 취소하고 다시 넉백시킨다.
            if (hit.collider.CompareTag("Player")) {
                Vector3 destPos = (transform.rotation * Vector2.up * status.knockBackPower) + hit.transform.position;
                hit.collider.GetComponent<Player>().Hit(destPos);
            }

            // 벽에 공격을 가한다.
            else if (hit.collider.CompareTag("Destroy Wall"))
                hit.collider.GetComponent<DestroyWall>().Hit();
        }

        if (--status.magazine <= 0)
            StartCoroutine("ReLoading");

        StartCoroutine("AttackDelay");
    }

    private IEnumerator AttackDelay() {
        bAttack = false;
        yield return delay;
        bAttack = true;
    }

    private IEnumerator ReLoading() {
        bReLoad = true;
        yield return reLoad;

        status.magazine = maxMagazine;
        bReLoad = false;
    }
}
