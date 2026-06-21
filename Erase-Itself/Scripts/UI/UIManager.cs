using UnityEngine;

namespace Kakky
{
    public class UIManager : MonoBehaviour
    {
        public void OpenPanel(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public void ClosePanel(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
