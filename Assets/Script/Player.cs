using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private JoyStick joyStick;

    [SerializeField]
    private GameObject winScreen, loseScreen;

    [SerializeField]
    private float speed;

    private bool bControl = true;

    private void Update() {
        if (gameObject.name != "Player") return;
        if (!bControl) return;

        Vector3 moveVec = Vector3.zero;
        moveVec.x = joyStick.Horizontal;
        moveVec.y = joyStick.Vertical;

        transform.position += moveVec * speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, joyStick.Angle);
    }

    private void OnCollisionStay2D(Collision2D col) {
        StopCoroutine("KnockBack");
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Escape")) {
            Debug.Log(string.Format("{0} is win!", gameObject.name));
            bControl = false;
            winScreen.SetActive(true);
        }
    }

    public void Hit(Vector3 destPos) {
        StartCoroutine("KnockBack", destPos);
    }

    public void Death() {
        bControl = false;
        loseScreen.SetActive(true);
    }

    private IEnumerator KnockBack(Vector3 destPos) {
        for (float passTime = 0; passTime < 0.5f; passTime += Time.deltaTime) {
            Vector3 movePos = Vector3.Lerp(transform.position, destPos, Time.deltaTime * 10);
            transform.position = movePos;
            yield return null;
        }
    }
}
