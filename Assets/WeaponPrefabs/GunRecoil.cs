using System.Collections;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    public Vector3 recoilRotation = new Vector3(3f, 0f, 0f);
    public float recoilDuration = 0.1f;
    public GameObject vfxPrefab; // Reference to the VFX prefab
    public float vfxScale = 0.5f; // Adjust the scale of the VFX prefab
    public Transform barrelEnd; // Reference to the transform at the end of the barrel

    private Quaternion originalRotation;
    private bool isRecoiling = false;

    void Start()
    {
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRecoiling)
        {
            StartCoroutine(Recoil());
            SpawnVFX();
        }
    }

    IEnumerator Recoil()
    {
        isRecoiling = true;

        transform.localRotation *= Quaternion.Euler(recoilRotation);

        yield return new WaitForSeconds(recoilDuration);

        float elapsedTime = 0f;
        float transitionTime = 0.2f;

        while (elapsedTime < transitionTime)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, originalRotation, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = originalRotation;

        isRecoiling = false;
    }

    void SpawnVFX()
    {
        if (vfxPrefab != null && barrelEnd != null)
        {
            // Instantiate the VFX prefab at the position of the barrelEnd with scaling
            GameObject vfxInstance = Instantiate(vfxPrefab, barrelEnd.position, barrelEnd.rotation);

            // Adjust the scale of the VFX prefab
            vfxInstance.transform.localScale = new Vector3(vfxScale, vfxScale, vfxScale);
        }
        else
        {
            Debug.LogError("VFX Prefab or barrelEnd transform not assigned to GunRecoil script.");
        }
    }
}
