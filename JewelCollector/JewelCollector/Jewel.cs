namespace JewelCollector;

/// <summary>
/// Representa uma joia que pode ser coletada pelo robô.
/// </summary>
internal class Jewel : Placeable
{
    #region Enums
    /// <summary>
    /// Tipo da joia.
    /// </summary>
    internal enum Types
    {
        Red,   /// Vermelho
        Green, /// Verde
        Blue   /// Azul
    }
    #endregion

    #region Properties
    /// <summary>
    /// Símbolo usado para representar a joia no mapa.
    /// </summary>
    internal override string Symbol
    {
        get => Type switch
        {
            Types.Red => "JR",
            Types.Green => "JG",
            Types.Blue => "JB",
            _ => "J?"
        };
    }
    /// <summary>
    /// Tipo da joia.
    /// </summary>
    internal Types Type { get; }
    #endregion Properties

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="y"> Posição 'y' no mapa. </param>
    /// <param name="x"> Posição 'x' no mapa. </param>
    /// <param name="type"> Tipo da joia. </param>
    /// <returns></returns>
    internal Jewel(byte y, byte x, Types type) : base(y, x)
    {
        Type = type;
    }
    #endregion Constructor

    #region Visible Methods
    /// <summary>
    /// Obtém o valor de uma joia dado seu tipo.
    /// </summary>
    /// <param name="type"> Tipo da joia. </param>
    /// <returns> Valor da joia. </returns>
    internal static byte GetValue(Types type)
    {
        return type switch
        {
            Types.Red => 100,
            Types.Green => 50,
            Types.Blue => 10,
            _ => 0
        };
    }
    #endregion Visible Methods
}
