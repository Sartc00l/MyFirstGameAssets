using UnityEngine;
using UnityEngine.UI;

public class heartUi : MonoBehaviour
{

    public Image playerHP;
    public Sprite[] hearts =  new Sprite[5];

    public void SetSprite(int _sprite)
    {
        playerHP.sprite = hearts[_sprite];
    }
}

