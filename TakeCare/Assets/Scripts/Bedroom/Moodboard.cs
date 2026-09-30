using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moodboard : MonoBehaviour
{
    [SerializeField] private Transform[] slots;
    [SerializeField] private float photoWidth = 0.4f;
    [SerializeField] private Material photoMaterial;

    private int nextSlot;

    public bool HasFreeSlot => nextSlot < slots.Length;

    public bool PlacePhoto(Texture2D photo)
    {
        if (!HasFreeSlot)
        {
            Debug.Log("Moodboard is full");
            return false;
        }

        Transform slot = slots[nextSlot];
        nextSlot++;

        GameObject photoObject = new GameObject("Photo_" + nextSlot);
        photoObject.transform.SetParent(slot, false);
        photoObject.transform.localPosition = Vector3.zero;
        photoObject.transform.localRotation = Quaternion.identity;

        float pixelsPerUnit = photo.width / photoWidth;

        Sprite sprite = Sprite.Create(photo, new Rect(0f, 0f, photo.width, photo.height), new Vector2(0.5f, 0.5f),
            pixelsPerUnit);

        SpriteRenderer renderer = photoObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;

        if (photoMaterial != null)
        {
            renderer.material = photoMaterial;
        }

        return true;
    }
}
