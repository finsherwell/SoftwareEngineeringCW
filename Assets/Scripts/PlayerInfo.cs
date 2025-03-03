//using UnityEngine;
//using TMPro; // Import TextMeshPro namespace

//public class PlayerInfo : MonoBehaviour
//{
//    [SerializeField] private string name;             // Player's name
//    [SerializeField] private Sprite token;            // Player's token (sprite)
//    [SerializeField] private TMP_InputField nameinput; // Use TMP_InputField if using TextMeshPro

//    void Start()
//    {
//        // Register the function to be called when the user finishes editing the input field
//        if (nameinput != null)
//        {
//            nameinput.onEndEdit.AddListener(UpdateName);
//        }
//    }

//    // This function is called when the user finishes editing the input field
//    public void UpdateName(string givenName)
//    {
//        // Update the name with the value entered in the InputField
//        name = givenName;

//        // Optionally, log the new name for debugging purposes
//        Debug.Log("Player's name updated to: " + name);
//    }
//}
