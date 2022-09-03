namespace JewelCollector;

/// <summary>
/// Representa o personagem controlado pelo jogador.
/// </summary>
internal class Robot : Placeable
{
    #region Enums
    /// <summary>
    /// Possíveis direções que o robô pode se mover.
    /// </summary>
    internal enum MoveDirections
    {
        North, /// Norte
        West,  /// Oeste
        South, /// Sul
        East   /// Leste
    }
    #endregion Enums

    #region Fields
    private readonly ushort[] _bag = new ushort[Enum.GetNames(typeof(Jewel.Types)).Length];
    #endregion Fields

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="position"> Posição inicial no mapa. </param>
    internal Robot(Tuple<byte, byte> position)  : base(position)
    {
    }
    #endregion Constructor

    #region Visible Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    internal void Move(MoveDirections direction)
    {

    }
    #endregion Visible Methods

    #region Private Methods
    /// <summary>
    /// 
    /// </summary>
    private void CollectJewel()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    private void PrintBag()
    {
        
    }
    #endregion Private Methods
}
