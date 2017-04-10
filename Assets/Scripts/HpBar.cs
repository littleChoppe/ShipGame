using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class HpBar : MonoBehaviour {
    [SerializeField]
    Color _teamColor = Color.green;
    [SerializeField]
    Color _enemyColor = Color.red;
    [SerializeField]
    Color _backgroundColor = Color.black;
    [SerializeField]
    Unit _self;

    TextMesh _textMesh;
    Color _color = Color.white;
	void Awake () 
    {
        _textMesh = GetComponent<TextMesh>();
        _color = _self.Team == TeamType.Good ? _teamColor : _enemyColor;
	}
	
	// Update is called once per frame
    //void Update () 
    //{
    //    if (_self.Hp > 0)
    //        _textMesh.text = DrawHpBar(Mathf.RoundToInt(_self.HpPrecent() * 10), _color);
    //    else
    //        _textMesh.text = "";
    //}

    public void UpdateHpBar()
    {
        if (_self.Hp > 0)
            _textMesh.text = DrawHpBar(Mathf.RoundToInt(_self.HpPrecent() * 10), _color);
    }

    /// <summary>
    /// 画血条：一格10%血，一共 10 格，不满血的用黑格填充
    /// </summary>
    /// <param name="n">剩余多少格血</param>
    /// <param name="color">血量填充的颜色</param>
    /// <returns></returns>
    string DrawHpBar(int n, Color color)
    {
        return "<color=#" + color.ToHexString() + ">" +
            new string('_', n) +
                "</color>" +
                "<color=#" + _backgroundColor.ToHexString() + ">" +
                new string('_', 10 - n) +
                "</color>";
    }

    string ColorToHexString(Color color)
    {
        Color32 c = color;
        return c.r.ToString("x").PadRight(2, '0') + c.g.ToString("x").PadRight(2, '0') +
            c.b.ToString("x").PadRight(2, '0') + c.a.ToString("x").PadRight(2, '0');
    }

    public void ShowHpBar()
    {
        this.gameObject.SetActive(true);
    }

    public void HideHpBar()
    {
        this.gameObject.SetActive(false);
    }
}

