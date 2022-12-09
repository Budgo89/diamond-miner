using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsMenuView : MonoBehaviour
{
    [SerializeField] private TMP_Text _diamondPlayer1Text;
    [SerializeField] private TMP_Text _diamondPlayer2Text;
    [SerializeField] private GameObject _victorys;
    [SerializeField] private Image _victoryPlayer1;
    [SerializeField] private Image _victoryPlayer2;
    [SerializeField] private Button _ok;

    public TMP_Text DiamondPlayer1Text => _diamondPlayer1Text;
    public TMP_Text DiamondPlayer2Text => _diamondPlayer2Text;
    public GameObject Victorys => _victorys;
    public Image VictoryPlayer1 => _victoryPlayer1;
    public Image VictoryPlayer2 => _victoryPlayer2;
    public Button Ok => _ok;
}
