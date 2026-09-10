using UnityEngine;

public class SimpleDoorToggle : MonoBehaviour
{
    [Header("Door Object")]
    [SerializeField] private GameObject doorModel;
    
    private bool isOpen = false;

    // Call this from your button's OnPressed() event
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        
        if (doorModel != null)
        {
            // For an "instant" change, we just disable/enable the mesh
            // Later you can replace this with an Animator trigger if you want it to slide/spin
            doorModel.SetActive(!isOpen);
        }
    }
}