using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image _image;
    private Vector3 _scale;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
        _scale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
            transform.DOScale(new Vector3(_scale.x + 0.2f, _scale.y + 0.2f, _scale.z + 0.2f), 0.5f).SetUpdate(true);
            Debug.Log("bigger");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();

            transform.DOScale(_scale, 0.5f).SetUpdate(true);
            Debug.Log("smaller");


    }
}
