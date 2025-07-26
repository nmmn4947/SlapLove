using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character Sprite Data", order = 2)]
public class CharacterSpritesData : ScriptableObject
{
    [SerializeField] private Sprite _spriteOpen;
    [SerializeField] private Sprite _spriteWindUp;
    [SerializeField] private Sprite _spriteClose;
    public Sprite SpriteOpen => _spriteOpen;
    public Sprite SpriteWindUp => _spriteWindUp;
    public Sprite SpriteClose => _spriteClose;
}
