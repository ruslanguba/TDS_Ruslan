using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDamage : MonoBehaviour
{  // этот класс я написал по туториалу из сети
    private class ActiveText
    {
        public Text UIText;
        public float maxTime;
        public float Timer;
        public Vector3 unitPosition;

        public void MoveText(Camera camera)
        {
            float delta = 1.0f - (Timer / maxTime);
            Vector3 pos = unitPosition + new Vector3(delta, delta, 0.0f);
            pos = camera.WorldToScreenPoint(pos);
            pos.z = 0.0f;

            UIText.transform.position = pos;
        }
    }
    public static ShowDamage Instance { get; private set; }
    const int POOL_SIZE = 64;
    public Text m_TextPrefab;

    Queue<Text> m_TextPool = new Queue<Text>();
    List<ActiveText> m_ActiveText = new List<ActiveText>();

    private void Awake()
    {
        Instance = this;
    }
    Camera m_Camera;
    Transform m_Transform;
    void Start()
    {
        m_Camera = Camera.main;
        m_Transform = transform;

        for (int i = 0; i < POOL_SIZE; i++)
        {
            Text temp = Instantiate(m_TextPrefab, m_Transform);
            temp.gameObject.SetActive(false);
            m_TextPool.Enqueue(temp);
        }
    }

    void Update()
    {
        for (int i = 0; i < m_ActiveText.Count; i++)
        {
            ActiveText at = m_ActiveText[i];
            at.Timer -= Time.deltaTime;
            if(at.Timer <= 0.0f)
            {
                at.UIText.gameObject.SetActive(false);
                m_TextPool.Enqueue(at.UIText);
                m_ActiveText.RemoveAt(i);
                --i;
            }
            else
            {
                var color = at.UIText.color;
                color.a = at.Timer / at.maxTime;
                at.UIText.color = color;

                at.MoveText(m_Camera);
            }
        }             
    }

    public void AddText(int amount, Vector3 unitPos)
    {
        var t = m_TextPool.Dequeue();
        t.text = amount.ToString();
        t.gameObject.SetActive(true);

        ActiveText at = new ActiveText() { maxTime = 1.0f };
        at.Timer = at.maxTime;
        at.UIText = t;
        at.unitPosition = unitPos + Vector3.up;

        at.MoveText(m_Camera);
        m_ActiveText.Add(at);
    }
}

