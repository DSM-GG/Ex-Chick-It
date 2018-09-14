using UnityEngine;

public class CamFollow : MonoBehaviour {

    [SerializeField]
    private Transform player;

    private void FixedUpdate() {
        Vector3 moveVec = Vector2.Lerp(transform.position, player.position, Time.deltaTime * 3);
        transform.position = new Vector3(moveVec.x, moveVec.y, -10);
    }
}
