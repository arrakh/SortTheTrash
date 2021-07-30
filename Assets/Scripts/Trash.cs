using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class Trash : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static Action<bool> OnTrashDrop; //Param: Is Trash Correct?

    public float speed = 3f;
    public Vector3 direction = new Vector3(-1, 0, 0);
    public bool isGrabbed = false;

    [SerializeField] private TrashType type;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    private TrashBin currentBin;
    private Camera cam;
    private Vector3 lastPos;

    private void Start()
    {
        spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];

        cam = Camera.main;
    }

    private void Update()
    {
        //Jika sampah sedang dipegang, maka set posisi sampah dengan posisi kursor
        if (isGrabbed)
        {
            Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
        //Jika tidak, maka gerakkan sampah sesuai arah
        else
        {
            MoveTrash();
        }
    }

    private void MoveTrash()
    {
        transform.position += direction.normalized * (speed * Time.deltaTime);
    }

    //(Dipanggil oleh Event System)
    //Saat mouse di klik di sampah, set isGrabbed dan setor posisi akhir
    public void OnPointerDown(PointerEventData eventData)
    {
        isGrabbed = true;
        lastPos = transform.position;
        Debug.Log("CLICKED");
    }

    //(Dipanggil oleh Event System)
    public void OnPointerUp(PointerEventData eventData)
    {
        //Lakukan CheckDrop dan kembalikan ke posisi akhir jika gagal
        if (!CheckDrop())
        {
            transform.position = lastPos;
        }

        isGrabbed = false;
    }

    //Jika sampah bertabrakan dengan bin, setor nilai currentBin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrashBin bin = collision.GetComponent<TrashBin>();

        if (bin) currentBin = bin;
    }

    //Jika currentBin tidak kosong, panggil OnTrashDrop selagi mengecek tipe nya, lalu Destroy samapah ini)
    private bool CheckDrop()
    {
        bool isDropSuccess = false;

        //Check if current bin is NOT empty
        if (currentBin)
        {
            //Invoke OnTrashDrop based on whether the type matches
            OnTrashDrop?.Invoke(currentBin.binType == type);

            Destroy(this.gameObject);
        }

        return isDropSuccess;
    }

}

public enum TrashType
{
    Organic,
    NonOrganic
}
