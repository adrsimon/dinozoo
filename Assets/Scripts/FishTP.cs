using UnityEngine;

public class FishTP : MonoBehaviour
{
    public Transform backpackOutTP; 

    private void OnTriggerEnter(Collider other)
    {
        // Vérifier si l'objet qui entre dans le trigger a le tag "Fish"
        if (other.gameObject.tag == "Fish")
        {
            // Téléporter l'objet au point de sortie
            other.transform.position = backpackOutTP.position;
        }
    }
}
