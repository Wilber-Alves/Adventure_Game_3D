using DG.Tweening;
using UnityEngine;

[CreateAssetMenu]

public class SOPlayer : ScriptableObject
{
    public Animator player;


    [Header("Speed Settings")]
    public Vector2 friction = new Vector2(-.1f, 0);
    public float speed = 10.0f;
    public float speedRun = 13.0f;

    [Header("Animation Settings")]
    public Vector2 idleScale = new Vector2(1f, 1f);
    public Vector2 jumpScale = new Vector2(0.75f, 1.5f);
    public Vector2 landScale = new Vector2(1.5f, 0.75f);

    public Ease jumpEase = Ease.OutQuad;
    public Ease landEase = Ease.InQuad;
    public Ease delayEase = Ease.InBack;

    public float jumpScaleDuration = 0.02f;
    public float landScaleDuration = 0.08f;
    public float landDelay = 0.05f;
    public float forceJump = 30.0f;
    public float doubleJumpForce = 25.0f;

}
