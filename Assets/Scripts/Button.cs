using UnityEngine;

public class Button : MonoBehaviour
{
    public int id = 0;
    public int award = 1;
    public bool canGlow = false;
    public bool glow = false;
    public float glowIntensity = 1f;
    public float remainingGlowTime = 0f;
    public float remainingReloadTime = 0f;

    [SerializeField]
    public MK.Glow.MinMaxRange emissionColorIntensity = new MK.Glow.MinMaxRange(0f, 0f);
    private readonly MK.Glow.MinMaxRange _colorChangeTime = new MK.Glow.MinMaxRange(0f, 0f);
    private readonly MK.Glow.MinMaxRange _colorIntensityChangeTime = new MK.Glow.MinMaxRange(0f, 0f);

    private float _nextColorChangeTime = 0f;
    private float _nextColorIntensityChangeTime = 0f;
    private int _nextColorIndex = 0;
    private float _nextColorIntensity;
        
    [SerializeField]
    public Color[] colors = new Color[1];
    private Color _originalColor;
    private Color _currentColor = new Color();
    private float _currentColorIntensity = 1;

    private Material _baseMaterial;
    private Material _usedMaterial;

    private int _emissionColorId;
    private int _colorId;
        

    private void Glow()
    {
        emissionColorIntensity.minValue = glowIntensity;
        emissionColorIntensity.maxValue = glowIntensity;
    }

    private void Dim()
    {
        colors[_nextColorIndex] = _originalColor;
        emissionColorIntensity.minValue = 0;
        emissionColorIntensity.maxValue = 0;
    }

    private void Awake()
    {
        GameManager.Instance.RegisterButton(id, this);

        _emissionColorId = Shader.PropertyToID("_EmissionColor");
        _colorId = Shader.PropertyToID("_Color");
            
        _baseMaterial = new Material(GetComponent<Renderer>().material);
        _usedMaterial = new Material(_baseMaterial);
        GetComponent<Renderer>().material = _usedMaterial;
        _nextColorIndex = Random.Range(0, colors.Length);
        _originalColor = _currentColor = colors[_nextColorIndex];
        _currentColorIntensity = Random.Range(emissionColorIntensity.minValue, emissionColorIntensity.maxValue);
        _usedMaterial.SetColor(_emissionColorId, _currentColor * _currentColorIntensity);
        _usedMaterial.SetColor(_colorId, _currentColor * 0.5f);
    }

    private void FixedUpdate()
    {
        if (canGlow)
        {
            remainingReloadTime = Mathf.Max(remainingReloadTime - Time.deltaTime, 0.0f);
            if (remainingReloadTime <= 0)
            {
                glow = true;
                remainingGlowTime = Mathf.Max(remainingGlowTime - Time.deltaTime, 0.0f);
                if (remainingGlowTime <= 0)
                {
                    canGlow = false;
                    glow = false;
                    GameManager.Instance.ButtonActivated();
                }
            }
            else
            {
                colors[_nextColorIndex] = _originalColor;
            }

            if (glow)
            {
                Glow();
            }
            else
            {
                Dim();
            }
        }
        else
        {
            Dim();
        }
    }

    private void Update()
    {
        _nextColorIndex = 0;
        _nextColorIntensity = Random.Range(emissionColorIntensity.minValue, emissionColorIntensity.maxValue);

        // _currentColor = Color.Lerp(_currentColor, colors[_nextColorIndex], 10 * Time.smoothDeltaTime);
        _currentColor = colors[_nextColorIndex];
        _currentColorIntensity = Mathf.Lerp(_currentColorIntensity, _nextColorIntensity, 10 * Time.smoothDeltaTime);
        //_currentColorIntensity = _nextColorIntensity;

        _usedMaterial.SetColor(_emissionColorId, _currentColor * _currentColorIntensity);
        _usedMaterial.SetColor(_colorId, _currentColor * 0.5f);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (glow)
        {
            Debug.Log("Button hit!");
            GameManager.Instance.score += award;
            colors[_nextColorIndex] = new Color(1.0f, 0.5f, 0.0f);
        }
    }
}