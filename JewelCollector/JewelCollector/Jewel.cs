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

    #region Fields

    #endregion Fields

    #region Properties
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

    internal Types Type { get; }
    #endregion Properties

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="position"> Posição no mapa. </param>
    /// <param name="type"> Tipo de joia. </param>
    internal Jewel(byte y, byte x, Types type) : base(y, x)
    {
        Type = type;
    }
    #endregion Constructor

    #region Visible Methods
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
