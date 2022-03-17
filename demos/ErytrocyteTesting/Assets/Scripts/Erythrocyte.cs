using UnityEngine;
public class Erythrocyte : MonoBehaviour
{
    private GameObject aimMineral = null;

    [SerializeField] private float velocity = 5f;

    private void findMineral()
    {
        aimMineral = MineralManager.instance.getAim(this.transform.position);
    }

    private void Update()
    {
        if (aimMineral == null)
        {
            findMineral();
        }
    }

    private void FixedUpdate()
    {
        if (aimMineral != null)
        {
            Vector3 delta = aimMineral.transform.position - this.transform.position;
            delta.Normalize();
            this.transform.position += delta * velocity * Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (aimMineral != null && other.tag == "Mineral" && other == aimMineral.GetComponent("Collider2D"))
        {
            MineralManager.instance.deleteObject(aimMineral);
            aimMineral = null;
        }
    }
}
