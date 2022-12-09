using UnityEngine;
using UnityEngine.UI;

public class GameUIOnlineView : MonoBehaviour
{
    [SerializeField] private Button _pauseMenuButton;

    public Button PauseMenuButton => _pauseMenuButton;
}
