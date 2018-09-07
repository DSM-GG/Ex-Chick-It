using UnityEngine;

public class DestroyWall : MonoBehaviour {

    [SerializeField]
    private int numOfDeffense;
    private Transform escape = null;

    private void Awake() {
        if (transform.childCount > 0)
            escape = transform.GetChild(0);
    }

    public void Hit() {
        if (--numOfDeffense <= 0) {
            if (escape != null) {
                escape.gameObject.SetActive(true);
                escape.parent = null;
            }

            gameObject.SetActive(false);
        }
    }
}
