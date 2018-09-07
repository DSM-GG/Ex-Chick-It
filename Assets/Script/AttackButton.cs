using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [SerializeField]
    private PlayerAttack player;

    private bool bAttack;

    public void OnPointerDown(PointerEventData ped) {
        bAttack = true;
        StartCoroutine("Attack");
    }

    public void OnPointerUp(PointerEventData ped) {
        bAttack = false;
    }

    private IEnumerator Attack() {
        while (bAttack) {
            player.Attack();
            yield return null;
        }
    }
}
