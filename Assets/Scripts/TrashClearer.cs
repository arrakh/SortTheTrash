using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TrashClearer : MonoBehaviour
{
    public static Action OnTrashCleared;

    //Jika yang menabrak adalah sampah, Destroy sampah lalu panggil OnTrashCleared
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trash trash = collision.GetComponent<Trash>();

        if (trash)
        {
            OnTrashCleared?.Invoke();
            Destroy(trash.gameObject);
        }
    }
}
